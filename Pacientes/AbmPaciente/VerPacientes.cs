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

namespace Pacientes.AbmPaciente
{
    public partial class VerPacientes : Form
    {
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
            Logica logica = new Logica(Properties.Settings.Default.databaseName);
            var data = logica.ObtenerPacientes();
            ListaPacientes.DataSource = data;

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
            if (p.Sesiones != null && p.Sesiones.Count>0)
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
            Logica log = new Logica(Properties.Settings.Default.databaseName);
            log.EliminarPaciente(p);
            recargarTabla();
        }

        private Paciente GetSeleccionado()
        {
            if (ListaPacientes.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.ListaPacientes.SelectedRows[0];

                int id = (int)row.Cells["Id"].Value;

                Logica log = new Logica(Properties.Settings.Default.databaseName);
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
    }
}
