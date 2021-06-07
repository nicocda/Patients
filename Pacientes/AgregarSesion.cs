using Pacientes.AbmPaciente;
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
    public partial class AgregarSesion : Form
    {
        private Sesion sesion;

        public AgregarSesion()
        {
            InitializeComponent();

            Inicializar();

        }

        public AgregarSesion(Sesion sesion)
        {

            InitializeComponent();

            this.sesion = sesion;
            txtNotas.Text = sesion.Notas;
            dateFechaHora.Value = sesion.FechaHora;
            pacienteUserControl1.Value = sesion.Paciente;


            btnGuardar.Visible = false;
        }

        private void Inicializar()
        {
            var f = DateTime.Now;
            int min;
            if (f.Minute < 15)
                min = 0;
            else if (f.Minute < 30)
                min = 15;
            else if (f.Minute < 45)
                min = 30;
            else
                min = 45;
            dateFechaHora.Value = new DateTime(f.Year, f.Month, f.Day, f.Hour, min, 0);
            pacienteUserControl1.LimpiarCampos();
            txtNotas.Text = null;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!pacienteUserControl1.IdPacienteSeleccionado.HasValue)
                MessageBox.Show("Indique el paciente");

            string notas;
            if (string.IsNullOrEmpty(txtNotas.Text))
                notas = "[Sin Notas]";

            Sesion s = new Sesion();
            s.FechaHora = dateFechaHora.Value;
            s.Notas = txtNotas.Text;
            //s.PacienteId = IdPacienteSeleccionado.Value;

            try
            {
                Logica log = new Logica();
                log.AgregarSesion(s, pacienteUserControl1.IdPacienteSeleccionado.Value);


                Inicializar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
