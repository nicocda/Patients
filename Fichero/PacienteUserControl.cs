using Fichero.AbmPaciente;
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
    public partial class PacienteUserControl : UserControl
    {

        public delegate void OnSelectedHandler(object sender, Paciente e);
        public event OnSelectedHandler OnSelected;
        public event OnSelectedHandler OnDeleted;

        public int? IdPacienteSeleccionado = null;

        public Paciente Value
        {
            get
            {
                if (IdPacienteSeleccionado.HasValue)
                    return new Paciente() { Id = IdPacienteSeleccionado.Value, DNI = txtDniPaciente.Text, NombreApellido = txtNombrePaciente.Text };
                else return null;
            }
            set
            {
                IdPacienteSeleccionado = value.Id;
                txtDniPaciente.Text = value.DNI;
                txtNombrePaciente.Text = value.NombreApellido;
            }
        }


        public bool TieneValor()
        {
            if (IdPacienteSeleccionado == null || IdPacienteSeleccionado == 0)
                return false;
            else return true;
        }

        public PacienteUserControl()
        {
            InitializeComponent();
            LimpiarCampos();


            //btnNew.Visible = false;
            btnEliminar.Visible = true;
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
            OnSelected?.Invoke(this, e);
        }

        private void txtDniPaciente_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDniPaciente.Text?.Trim()))
                return;

            Cursor.Current = Cursors.WaitCursor;

            Server.Logica logica = new Server.Logica(Settings.Properties.DatabaseName);

            var p = logica.ObtenerPacienteDni(txtDniPaciente.Text);

            if (p != null)
                SeleccionarPaciente(p);

            Cursor.Current = Cursors.Default;
        }

        private void txtNombrePaciente_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombrePaciente.Text?.Trim()))
                return;
            BuscarPaciente();
        }

        private void BuscarPaciente()
        {

            Cursor.Current = Cursors.WaitCursor;
            Server.Logica logica = new Server.Logica(Settings.Properties.DatabaseName);

            List<Paciente> pacientes = logica.ObtenerPacienteNombre(txtNombrePaciente.Text);
            if (pacientes != null)
            {
                if (pacientes.Count == 1)
                    SeleccionarPaciente(pacientes[0]);
                else if (pacientes.Count > 1)
                    AbrirBuscardorPacientes(pacientes);
            }
            Cursor.Current = Cursors.Default;
        }

        private void AbrirBuscardorPacientes(List<Paciente> pacientes)
        {
            BuscarPaciente form = null;
            if (pacientes != null && pacientes.Count > 0)
                form = new BuscarPaciente(pacientes);
            else
                form = new BuscarPaciente();
            form.OnSearched += Form_OnSearched;
            form.ShowDialog();
        }

        private void Form_OnSearched(object sender, Paciente e)
        {
            SeleccionarPaciente(e);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            AbrirBuscardorPacientes(null);
        }

        public void LimpiarCampos()
        {
            IdPacienteSeleccionado = null;
            txtDniPaciente.Text = null;
            txtNombrePaciente.Text = null;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!IdPacienteSeleccionado.HasValue)
                return;

            var dialogResult = MessageBox.Show("¿Realmente desea eliminar el paciente?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    Logica l = new Logica(Settings.Properties.DatabaseName);

                    var sesiones = l.ListarSesiones(IdPacienteSeleccionado.Value);
                    bool resp = true;
                    if (sesiones != null && sesiones.Count > 0)
                    {
                        dialogResult = MessageBox.Show("El paciente tiene Sesiones, desea eliminarlo de todas formas? Dichas sesiones se perderan", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        resp = (dialogResult == DialogResult.Yes);

                    }

                    l.EliminarPaciente(new Paciente()
                    {
                        Id = IdPacienteSeleccionado.Value
                    }, resp);
                    LimpiarCampos();
                    OnDeleted?.Invoke(this, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtNombrePaciente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (string.IsNullOrEmpty(txtNombrePaciente.Text?.Trim()))
                    AbrirBuscardorPacientes(null);
                else
                    BuscarPaciente();
            }
        }
    }
}
