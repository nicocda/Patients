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
    public partial class BuscarPaciente : Form
    {

        public delegate void OnSearchedHandler(object sender, Paciente e);
        public event OnSearchedHandler OnSearched;

        public BuscarPaciente()
        {
            InitializeComponent();

            Logica log = new Logica(Settings.Properties.DatabaseName);
            var data = log.ObtenerPacientes();
            CargarTabla(data);
        }

        public BuscarPaciente(List<Paciente> data)
        {
            InitializeComponent();

            CargarTabla(data);
        }

        private void CargarTabla(List<Paciente> data)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DataSource = data;
            dataGridView1.Refresh();
        }

        private void BuscarPaciente_Load(object sender, EventArgs e)
        {
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Paciente> data = new List<Paciente>();
            Logica log = new Logica(Settings.Properties.DatabaseName);
            if (!string.IsNullOrEmpty(txtDni.Text))
            {
                var p = log.ObtenerPacienteDni(txtDni.Text);
                if (p != null)
                    data.Add(p);
            }

            else if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                var pacientes = log.ObtenerPacienteNombre(txtNombre.Text);
                if (pacientes != null && pacientes.Count > 0)
                    data.AddRange(pacientes);
            }

            if (data.Count > 0)
                CargarTabla(data);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Seleccionar();
        }

        private void Seleccionar()
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.dataGridView1.SelectedRows[0];

                int id = (int)row.Cells["Id"].Value;
                string dni = (string)row.Cells["DNI"].Value;
                string nombre = (string)row.Cells["NombreApellido"].Value;
                OnSearched?.Invoke(this, new Paciente() { Id = id, DNI = dni, NombreApellido = nombre });
                this.Close();
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            Seleccionar();
        }
    }
}
