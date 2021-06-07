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
        private bool FirstTime = true;
        public ListarSesiones()
        {
            InitializeComponent();
            pacienteUserControl1.OnSelected += PacienteUserControl1_OnSelected;
            this.SizeChanged += ListarSesiones_ResizeEnd;
        }

        private void PacienteUserControl1_OnSelected(object sender, Paciente e)
        {
            Buscar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            panel1.Controls.Clear();
            if (!pacienteUserControl1.IdPacienteSeleccionado.HasValue)
                return;
            Logica logica = new Logica();
            var sesiones = logica.ListarSesiones(pacienteUserControl1.IdPacienteSeleccionado.Value);
            if (sesiones != null && sesiones.Count > 0)
            {
                sesiones = sesiones.OrderByDescending(c => c.FechaHora).ToList();
                int x = 10;
                int y = 10;
                foreach (var s in sesiones)
                {
                    SesionVisual control = new SesionVisual(s);
                    control.Location = new Point(x, y);
                    control.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
          | AnchorStyles.Left)
          | AnchorStyles.Right);
                    y = y + control.Size.Height + 10;
                    int width = panel1.Size.Width - 40;
                    control.SetTamaño(width);
                    panel1.Controls.Add(control);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void ListarSesiones_ResizeEnd(object sender, EventArgs e)
        {
            if (!FirstTime)
                Buscar();
            FirstTime = false;
        }
    }
}
