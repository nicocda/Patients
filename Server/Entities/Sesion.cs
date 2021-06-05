using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public  class Sesion
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        //public int PacienteId { get; set; }

        [MaxLength(4500)]
        public string Notas { get; set; }
    }
}
