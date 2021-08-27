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

namespace Fichero.AbmPaciente
{
    public partial class VerPacientes : Form
    {
        public delegate void OnSearchedHandler(object sender, Paciente e);
        public event OnSearchedHandler OnSearched;


        public VerPacientes()
        {
            InitializeComponent();
        }

        private void VerPacientes_Load(object sender, EventArgs e)
        {
            recargarTabla();
        }

        private void recargarTabla()
        {
            Logica logica = new Logica(Settings.Properties.DatabaseName);
            var data = logica.ObtenerPacientes();
            
            ListaPacientes.DataSource = data.OrderBy(x=>x.NombreApellido).ToList();

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            AgregarPaciente form = new AgregarPaciente();
            form.ShowDialog();
            recargarTabla();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var p = GetSeleccionado();
            if (p == null)
                return;
            if (p.Sesiones != null && p.Sesiones.Count > 0)
            {
                var rsp = MessageBox.Show("El paciente tiene sesiones registradas. ¿Igualmente desea eliminarlo?", "", MessageBoxButtons.YesNo);
                if (rsp == DialogResult.Yes)
                {
                    EliminarPaciente(p);
                }
            }
            else
                EliminarPaciente(p);
        }

        private void EliminarPaciente(Paciente p)
        {
            Logica log = new Logica(Settings.Properties.DatabaseName);
            log.EliminarPaciente(p, false);
            recargarTabla();
        }

        private Paciente GetSeleccionado()
        {
            if (ListaPacientes.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.ListaPacientes.SelectedRows[0];

                int id = (int)row.Cells["Id"].Value;

                Logica log = new Logica(Settings.Properties.DatabaseName);
                return log.ObtenerPaciente(id);
            }
            return null;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarPaciente form = new EditarPaciente(GetSeleccionado());
            form.ShowDialog();
            recargarTabla();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Seleccionar();
        }

        private void ListaPacientes_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            Seleccionar();
        }

        void Seleccionar()
        {
            if (ListaPacientes.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.ListaPacientes.SelectedRows[0];

                int id = (int)row.Cells[0].Value;
                string nombre = (string)row.Cells[1].Value;
                string dni = (string)row.Cells[2].Value;
                OnSearched?.Invoke(this, new Paciente() { Id = id, DNI = dni, NombreApellido = nombre });
                this.Close();
            }
        }
    }
}
