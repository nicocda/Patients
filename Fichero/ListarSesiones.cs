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
            pacienteUserControl1.OnDeleted += PacienteUserControl1_OnDeleted;
            this.SizeChanged += ListarSesiones_ResizeEnd;

            panel1.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
          | AnchorStyles.Left)
          | AnchorStyles.Right);
            AgregarScroll();


            dateTimePicker.Value = DateTime.Today;
        }

        private void PacienteUserControl1_OnDeleted(object sender, Paciente e)
        {
            LimpiarPanel();
        }

        private void AgregarScroll()
        {
            panel1.AutoScroll = true;
            panel1.BorderStyle = BorderStyle.FixedSingle;


            panel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            panel1.WrapContents = false;
            // panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.AutoScroll = true;
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

            Cursor.Current = Cursors.WaitCursor;
            Logica logica = new Logica(Settings.Properties.DatabaseName);
            var sesiones = logica.ListarSesiones(pacienteUserControl1.IdPacienteSeleccionado.Value);
            if (sesiones != null && sesiones.Count > 0)
            {
                sesiones = sesiones.OrderBy(c => c.FechaHora).ToList();
                int x = 30;
                int y = 10;
                SesionVisual control = null;
                foreach (var s in sesiones)
                {
                    control = new SesionVisual(s);
                    y = y + control.Size.Height + 10;
                    int width = panel1.Size.Width-30;
                    control.SetTamaño(width);
                    panel1.Controls.Add(control);

                }

                //Voy a la ultima posicion del scroll
                if(control != null && panel1.VerticalScroll.Visible)
                    panel1.ScrollControlIntoView(control);
                Cursor.Current = Cursors.Default;
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

            if (string.IsNullOrEmpty(txtNuevoDetalle.Text))
                return; 

            Sesion s = new Sesion();
            s.FechaHora = dateTimePicker.Value;
            s.Notas = txtNuevoDetalle.Text;
            //s.PacienteId = IdPacienteSeleccionado.Value;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                Logica log = new Logica(Settings.Properties.DatabaseName);
                log.AgregarSesion(s, pacienteUserControl1.IdPacienteSeleccionado.Value);
                Buscar();

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;

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
            Cursor.Current = Cursors.WaitCursor;
            Logica l = new Logica(Settings.Properties.DatabaseName);
            l.EditarObservacionPaciente(pacienteUserControl1.IdPacienteSeleccionado.Value, txtExamenes.Text);
            Cursor.Current = Cursors.Default;
        }
    }
}
