using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Paciente
    {
        [Key]
        
        [Display(AutoGenerateField = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Nombre y Apellido")]
        public string NombreApellido { get; set; }
        public string DNI { get; set; }
        public string NroTelefono { get; set; }
        public string Genero { get; set; }
        public string Direccion { get; set; }
        public DateTime? DiaNacimiento { get; set; }
        public string Observaciones { get; set; }

        [ForeignKey("PacienteId")]
        public virtual List<Sesion> Sesiones{ get; set; }

        internal void AgregarSesion(Sesion sesion)
        {
            if (this.Sesiones == null)
                this.Sesiones = new List<Sesion>();
            Sesion actualSesion = this.Sesiones.FirstOrDefault(x => x.FechaHora == sesion.FechaHora);
            if (actualSesion != null)
                actualSesion.Notas = sesion.Notas;
            else
                this.Sesiones.Add(sesion);
        }
    }
}
