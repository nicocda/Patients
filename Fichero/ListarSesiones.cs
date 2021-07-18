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

namespace Fichero
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


            dateTimePicker.Value = DateTime.Today;
        }


        private void AgregarScroll()
        {
            panel1.AutoScroll = true;
            panel1.BorderStyle = BorderStyle.FixedSingle;


            panel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            panel1.WrapContents = false;
            // panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.AutoScroll = true;

            //panel.AutoScroll = true;
            //panel1.AutoScroll = false;
            //panel1.HorizontalScroll.Enabled = false;
            //panel1.HorizontalScroll.Visible = false;
            //panel1.HorizontalScroll.Maximum = 0;
            //panel1.AutoScroll = true;


            //ScrollBar vScrollBar1 = new VScrollBar();

            //vScrollBar1.Dock = DockStyle.Left;
            //vScrollBar1.Scroll += (sender, e) =>
            //{
            //    panel1.VerticalScroll.Value = vScrollBar1.Value;
            //};
            //panel1.Controls.Add(vScrollBar1);
        }

        private void PacienteUserControl1_OnSelected(object sender, Paciente e)
        {
            Buscar();
            txtExamenes.Text = e.Observaciones;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            dateTimePicker.Value = DateTime.Today;
            txtNuevoDetalle.Text = null;

            //panel1.Controls.Clear();
            //AgregarScroll();
            LimpiarPanel();

            if (!pacienteUserControl1.TieneValor())
                return;

            Logica logica = new Logica(Settings.Properties.DatabaseName);
            var sesiones = logica.ListarSesiones(pacienteUserControl1.IdPacienteSeleccionado.Value);
            if (sesiones != null && sesiones.Count > 0)
            {
                sesiones = sesiones.OrderByDescending(c => c.FechaHora).ToList();
                int x = 30;
                int y = 10;
                foreach (var s in sesiones)
                {
                    SesionVisual control = new SesionVisual(s);
                    //   control.Location = new Point(x, y);
                    //          control.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
                    //| AnchorStyles.Left)
                    //| AnchorStyles.Right);
                    y = y + control.Size.Height + 10;
                    int width = panel1.Size.Width - 140;
                    control.SetTamaño(width);
                    panel1.Controls.Add(control);

                }
                // panel1.Size = new Size( panel1.Size.Width, y);
            }
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
            s.FechaHora = dateTimePicker.Value;
            s.Notas = txtNuevoDetalle.Text;
            //s.PacienteId = IdPacienteSeleccionado.Value;

            try
            {
                Logica log = new Logica(Settings.Properties.DatabaseName);
                log.AgregarSesion(s, pacienteUserControl1.IdPacienteSeleccionado.Value);
                Buscar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "Principal")
                    return;
            }
            //Principal frm = new Principal();
            //frm.WindowState = FormWindowState.Maximized;
            //frm.Show();

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarPanel();
        }
        void LimpiarPanel()
        {
            panel1.Controls.Clear();
            //foreach (Control item in panel1.Controls.OfType<SesionVisual>())
            //{
            //    panel1.Controls.Remove(item);
            //}
        }

        private void btnEditarObservacion_Click(object sender, EventArgs e)
        {
            if (!pacienteUserControl1.IdPacienteSeleccionado.HasValue)
                return;
            Logica l = new Logica(Settings.Properties.DatabaseName);
            l.EditarObservacionPaciente(pacienteUserControl1.IdPacienteSeleccionado.Value, txtExamenes.Text);
        }
    }
}
