using System;
using System.Collections.Generic;
using Server;

namespace Pacientes.Api
{
    public class LoginRequest
    {
        public string Pin { get; set; }
    }

    public class VincularRequest
    {
        public string Codigo { get; set; }
    }

    public class PacienteResumenDto
    {
        public int Id { get; set; }
        public string NombreApellido { get; set; }
        public string DNI { get; set; }
        public string NroTelefono { get; set; }
    }

    public class PacienteDto : PacienteResumenDto
    {
        public string Genero { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public DateTime? DiaNacimiento { get; set; }
        public string Observaciones { get; set; }
        public List<SesionDto> Sesiones { get; set; }

        public static PacienteDto Desde(Paciente p, bool conSesiones)
        {
            var dto = new PacienteDto
            {
                Id = p.Id,
                NombreApellido = p.NombreApellido,
                DNI = p.DNI,
                NroTelefono = p.NroTelefono,
                Genero = p.Genero,
                Direccion = p.Direccion,
                Localidad = p.Localidad,
                DiaNacimiento = p.DiaNacimiento,
                Observaciones = p.Observaciones
            };
            if (conSesiones)
            {
                dto.Sesiones = new List<SesionDto>();
                if (p.Sesiones != null)
                    foreach (var s in p.Sesiones)
                        dto.Sesiones.Add(SesionDto.Desde(s, p.NombreApellido));
                dto.Sesiones.Sort((a, b) => b.FechaHora.CompareTo(a.FechaHora));
            }
            return dto;
        }

        public Paciente ANegocio(int id)
        {
            return new Paciente
            {
                Id = id,
                NombreApellido = NombreApellido == null ? null : NombreApellido.Trim(),
                DNI = DNI,
                NroTelefono = NroTelefono,
                Genero = Genero,
                Direccion = Direccion,
                Localidad = Localidad,
                DiaNacimiento = DiaNacimiento,
                Observaciones = Observaciones
            };
        }
    }

    public class SesionDto
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string PacienteNombre { get; set; }
        public DateTime FechaHora { get; set; }
        public string Notas { get; set; }

        public static SesionDto Desde(Sesion s, string pacienteNombre)
        {
            return new SesionDto
            {
                Id = s.Id,
                PacienteId = s.PacienteId,
                PacienteNombre = pacienteNombre,
                FechaHora = s.FechaHora,
                Notas = s.Notas
            };
        }
    }

    public class SesionRequest
    {
        public DateTime FechaHora { get; set; }
        public string Notas { get; set; }
    }

    public class ObservacionesRequest
    {
        public string Observaciones { get; set; }
    }
}
