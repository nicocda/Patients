using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Http;
using Server;

namespace Pacientes.Api.Controllers
{
    [RoutePrefix("pacientes")]
    public class PacientesController : ApiController
    {
        const int MaxNotas = 4500;

        // GET /pacientes?q=texto  (busca por nombre, DNI o telefono; sin acentos ni mayusculas)
        [HttpGet, Route("")]
        public IHttpActionResult Listar(string q = null)
        {
            var todos = ApiContext.Run(l => l.ObtenerPacientes());
            IEnumerable<Paciente> res = todos;

            var terminos = Normalizar(q).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (terminos.Length > 0)
                res = res.Where(p =>
                {
                    var texto = Normalizar(p.NombreApellido + " " + p.DNI + " " + p.NroTelefono);
                    return terminos.All(t => texto.Contains(t));
                });

            return Ok(res.OrderBy(p => p.NombreApellido, StringComparer.CurrentCultureIgnoreCase)
                .Select(p => new PacienteResumenDto
                {
                    Id = p.Id,
                    NombreApellido = p.NombreApellido,
                    DNI = p.DNI,
                    NroTelefono = p.NroTelefono
                }));
        }

        [HttpGet, Route("{id:int}")]
        public IHttpActionResult Obtener(int id)
        {
            return Ok(ApiContext.Run(l => PacienteDto.Desde(l.ObtenerPaciente(id), true)));
        }

        [HttpPost, Route("")]
        public IHttpActionResult Crear([FromBody] PacienteDto dto)
        {
            ValidarPaciente(dto);
            var creado = ApiContext.Run(l => PacienteDto.Desde(l.GuardarDatosPaciente(dto.ANegocio(0)), false));
            return Created("pacientes/" + creado.Id, creado);
        }

        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Actualizar(int id, [FromBody] PacienteDto dto)
        {
            ValidarPaciente(dto);
            return Ok(ApiContext.Run(l => PacienteDto.Desde(l.GuardarDatosPaciente(dto.ANegocio(id)), false)));
        }

        [HttpPut, Route("{id:int}/observaciones")]
        public IHttpActionResult EditarObservaciones(int id, [FromBody] ObservacionesRequest req)
        {
            if (req == null)
                throw new Exception("Solicitud vacia");
            ApiContext.Run(l =>
            {
                l.ObtenerPaciente(id);
                l.EditarObservacionPaciente(id, req.Observaciones);
            });
            return Ok();
        }

        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Eliminar(int id, bool eliminarSesiones = false)
        {
            ApiContext.Run(l =>
            {
                l.ObtenerPaciente(id);
                l.EliminarPaciente(new Paciente { Id = id }, eliminarSesiones);
            });
            return Ok();
        }

        // ---- Sesiones del paciente ----

        // Si ya existe una sesion con esa fecha y hora, se actualizan sus notas (comportamiento de Logica).
        [HttpPost, Route("{id:int}/sesiones")]
        public IHttpActionResult CrearSesion(int id, [FromBody] SesionRequest req)
        {
            ValidarSesion(req);
            var dto = ApiContext.Run(l =>
            {
                l.AgregarSesion(new Sesion { FechaHora = req.FechaHora, Notas = req.Notas }, id);
                var p = l.ObtenerPaciente(id);
                return SesionDto.Desde(p.Sesiones.First(s => s.FechaHora == req.FechaHora), p.NombreApellido);
            });
            return Created("pacientes/" + id + "/sesiones/" + dto.Id, dto);
        }

        [HttpPut, Route("{id:int}/sesiones/{sesionId:int}")]
        public IHttpActionResult EditarSesion(int id, int sesionId, [FromBody] SesionRequest req)
        {
            ValidarSesion(req);
            var dto = ApiContext.Run(l =>
            {
                l.EditarSesion(id, sesionId, req.FechaHora, req.Notas);
                var p = l.ObtenerPaciente(id);
                return SesionDto.Desde(p.Sesiones.First(s => s.Id == sesionId), p.NombreApellido);
            });
            return Ok(dto);
        }

        [HttpDelete, Route("{id:int}/sesiones/{sesionId:int}")]
        public IHttpActionResult EliminarSesion(int id, int sesionId)
        {
            // Logica.EliminarSesion(int, int) quita la sesion de la coleccion del paciente y EF lo rechaza
            // (la FK es obligatoria). Se usa la baja directa de la sesion, igual que la WinForms.
            ApiContext.Run(l =>
            {
                var p = l.ObtenerPaciente(id);
                var s = p.Sesiones == null ? null : p.Sesiones.FirstOrDefault(x => x.Id == sesionId);
                if (s == null)
                    throw new Exception("Sesion no encontrada");
                l.EliminarSesion(s);
            });
            return Ok();
        }

        // ---- helpers ----

        static void ValidarPaciente(PacienteDto dto)
        {
            if (dto == null)
                throw new Exception("Solicitud vacia");
            if (string.IsNullOrWhiteSpace(dto.NombreApellido))
                throw new Exception("Indique nombre y apellido");
            if (dto.NombreApellido.Length > 200)
                throw new Exception("El nombre es demasiado largo");
        }

        static void ValidarSesion(SesionRequest req)
        {
            if (req == null)
                throw new Exception("Solicitud vacia");
            if (req.FechaHora == default(DateTime))
                throw new Exception("Indique fecha y hora");
            if (req.Notas != null && req.Notas.Length > MaxNotas)
                throw new Exception("Las notas superan los " + MaxNotas + " caracteres");
        }

        static string Normalizar(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return "";
            var sb = new StringBuilder();
            foreach (var c in s.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD))
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            return sb.ToString();
        }
    }
}
