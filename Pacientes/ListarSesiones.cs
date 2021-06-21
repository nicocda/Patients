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

            panel1.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
          | AnchorStyles.Left)
          | AnchorStyles.Right);
            AgregarScroll();
        }

        private void AgregarScroll()
        {
            ScrollBar vScrollBar1 = new VScrollBar();
            vScrollBar1.Dock = DockStyle.Left;
            vScrollBar1.Scroll += (sender, e) => { panel1.VerticalScroll.Value = vScrollBar1.Value; };
            panel1.Controls.Add(vScrollBar1);
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
            AgregarScroll();
            if (!pacienteUserControl1.TieneValor())
                return;

            Logica logica = new Logica();
            var sesiones = logica.ListarSesiones(pacienteUserControl1.IdPacienteSeleccionado.Value);
            if (sesiones != null && sesiones.Count > 0)
            {
                sesiones = sesiones.OrderByDescending(c => c.FechaHora).ToList();
                int x = 30;
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!pacienteUserControl1.TieneValor())
                return;

            Sesion s = new Sesion();
            s.FechaHora = DateTime.Today;
            s.Notas = txtNuevoDetalle.Text;
            //s.PacienteId = IdPacienteSeleccionado.Value;

            try
            {
                Logica log = new Logica();
                log.AgregarSesion(s, pacienteUserControl1.IdPacienteSeleccionado.Value);
                Buscar();
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }
    }
}
