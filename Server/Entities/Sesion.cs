using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Sesion
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }

        //[ForeignKey("PacienteId")]
        public virtual Paciente Paciente { get; set; }

        [MaxLength(4500)]
        public string Notas { get; set; }

        public string GetFechaStr(bool soloTime = false)
        {
            var f = this.FechaHora;
            if (soloTime)
                return string.Concat(f.Hour.ToString().PadLeft(2, '0'), ":", f.Minute.ToString().PadLeft(2, '0'));
            else
                return string.Concat(f.Day.ToString().PadLeft(2, '0'), "/", f.Month.ToString().PadLeft(2, '0'), "/", f.Year.ToString().PadLeft(4, '0'), " ", f.Hour.ToString().PadLeft(2, '0'), ":", f.Minute.ToString().PadLeft(2, '0'));

        }
    }
}
