using Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacientes
{
    public partial class SesionVisual : UserControl
    {
        public SesionVisual(Sesion s)
        {
            InitializeComponent();
            SetFecha(s.FechaHora);

            SetNota(s.Notas);
        }

        private void SetNota(string notas)
        {
            txtNota.Text = notas;

        }

        private void SetFecha(DateTime f)
        {
            string fechaStr = string.Concat(f.Day,"/",f.Month, "/",f.Year," ",f.Hour,":",f.Minute);
            lblFecha.Text = fechaStr;

        }
    }
}
