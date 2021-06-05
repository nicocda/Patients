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
        int? IdPacienteSeleccionado;
        public AgregarSesion()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AgregarPaciente form = new AgregarPaciente();
            form.OnSave += Form_OnSave;
            form.ShowDialog();

        }

        private void Form_OnSave(object sender, Server.Paciente e)
        {
            SeleccionarPaciente(e);
        }

        private void SeleccionarPaciente(Server.Paciente e)
        {
            txtDniPaciente.Text = e.DNI;
            txtNombrePaciente.Text = e.NombreApellido;
            IdPacienteSeleccionado = e.Id;
        }

        private void txtDniPaciente_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDniPaciente.Text?.Trim()))
                return;
            Server.Logica logica = new Server.Logica();

            var p = logica.ObtenerPacienteDni(txtDniPaciente.Text);

            if (p != null)
                SeleccionarPaciente(p);
        }

        private void txtNombrePaciente_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombrePaciente.Text?.Trim()))
                return;
            Server.Logica logica = new Server.Logica();

           List<Paciente> pacientes = logica.ObtenerPacienteNombre(txtNombrePaciente.Text);
            if(pacientes != null)
            {
                if (pacientes.Count == 1)
                    SeleccionarPaciente(pacientes[0]);
                else if(pacientes.Count >1)
                    AbrirBuscardorPacientes(pacientes);
            }
        }

        private void AbrirBuscardorPacientes(List<Paciente> pacientes)
        {
            throw new NotImplementedException();
        }
    }
}
