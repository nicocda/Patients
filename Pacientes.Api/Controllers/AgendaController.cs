using System;
using System.Linq;
using System.Web.Http;

namespace Pacientes.Api.Controllers
{
    public class AgendaController : ApiController
    {
        // GET /agenda?fecha=2026-10-03  (sin fecha: hoy)
        [HttpGet, Route("agenda")]
        public IHttpActionResult Dia(DateTime? fecha = null)
        {
            var dia = (fecha ?? DateTime.Today).Date;
            var sesiones = ApiContext.Run(l =>
            {
                var nombres = l.ObtenerPacientes().ToDictionary(p => p.Id, p => p.NombreApellido);
                return l.ObtenerSesionesDia(dia)
                    .Select(s => SesionDto.Desde(s, nombres.ContainsKey(s.PacienteId) ? nombres[s.PacienteId] : null))
                    .ToList();
            });
            return Ok(sesiones);
        }
    }
}
