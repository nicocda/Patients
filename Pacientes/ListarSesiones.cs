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
    public partial class ListarSesiones : Form
    {
        public ListarSesiones()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!pacienteUserControl1.IdPacienteSeleccionado.HasValue)
                return;
            Logica logica = new Logica();
            var sesiones = logica.ListarSesiones(pacienteUserControl1.IdPacienteSeleccionado.Value);
            if(sesiones != null && sesiones.Count > 0)
            {
                int x = 10;
                int y = 10;
                foreach(var s in sesiones)
                {
                    SesionVisual control = new SesionVisual(s);
                    control.Location = new Point(x, y);
                    y = y + control.Size.Height + 10;
                    panel1.Controls.Add(control);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
