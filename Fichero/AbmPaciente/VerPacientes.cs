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
        List<Paciente> ListaCompleta;


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
            ListaCompleta = logica.ObtenerPacientes().OrderBy(x => x.NombreApellido).ToList();

            ListaPacientesGrid.DataSource = ListaCompleta;
            int count = ListaCompleta != null ? ListaCompleta.Count : 0;
            lblCantRegistros.Text = $"Se encontraron {count} registros";

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
            //if (p.Sesiones != null && p.Sesiones.Count > 0)
            //{
            //    var rsp = MessageBox.Show("El paciente tiene sesiones registradas. ¿Igualmente desea eliminarlo?", "", MessageBoxButtons.YesNo);
            //    if (rsp == DialogResult.Yes)
            //    {
            //        EliminarPaciente(p);
            //    }
            //}
            //else
            EliminarPaciente(p);
        }

        private void EliminarPaciente(Paciente p)
        {
            try
            {
                Logica log = new Logica(Settings.Properties.DatabaseName);
                log.EliminarPaciente(p, false);
                recargarTabla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Paciente GetSeleccionado()
        {
            if (ListaPacientesGrid.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.ListaPacientesGrid.SelectedRows[0];

                int id = (int)row.Cells["Id"].Value;

                Logica log = new Logica(Settings.Properties.DatabaseName);
                return log.ObtenerPaciente(id);
            }
            return null;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                EditarPaciente form = new EditarPaciente(GetSeleccionado());
                form.ShowDialog();
                recargarTabla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            if (ListaPacientesGrid.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.ListaPacientesGrid.SelectedRows[0];

                int id = (int)row.Cells[0].Value;
                string nombre = (string)row.Cells[1].Value;
                string dni = (string)row.Cells[2].Value;
                string observaciones = (string)row.Cells[6].Value;
                OnSearched?.Invoke(this, new Paciente() { Id = id, DNI = dni, NombreApellido = nombre, Observaciones = observaciones });
                this.Close();
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            var textToFilter = txtSearchApenom.Text.ToLower();
            List<Paciente> pacientes = ListaCompleta;
            if (!string.IsNullOrEmpty(textToFilter))
                pacientes = ListaCompleta.Where(x =>
                x.NombreApellido.ToLower().Contains(textToFilter)
                || x.DNI.ToLower().Contains(textToFilter)
                || x.Localidad.ToLower().Contains(textToFilter)
                || x.Observaciones.ToLower().Contains(textToFilter)
                || x.NroTelefono.ToLower().Contains(textToFilter)

                ).ToList();

            ListaPacientesGrid.DataSource = pacientes;
            int count = pacientes != null ? pacientes.Count : 0;
            lblCantRegistros.Text = $"Se encontraron {count} registros";
        }

        private void txtSearchApenom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnFiltrar_Click(null, null);
        }

        private void ListaPacientesGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Seleccionar();
        }
    }
}
