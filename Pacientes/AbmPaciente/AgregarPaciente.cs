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
    public partial class AgregarPaciente : Form
    {
        public delegate void OnSaveHandler(object sender, Paciente e);
        public event OnSaveHandler OnSave;

        public AgregarPaciente()
        {
            InitializeComponent();
            GenHombre.Checked = true;
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NombreApellido.Text))
            {
                MessageBox.Show("Ingrese un nombre y apellido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Paciente p = new Paciente();
            p.NombreApellido = NombreApellido.Text.Trim();
            p.DiaNacimiento = Nacimiento.Value;
            p.Direccion = Direccion.Text;
            p.DNI = Documento.Text;
            p.Genero = GenHombre.Checked ? GenHombre.Text : GenMujer.Text;
            p.NroTelefono = Telefono.Text;
            p.Observaciones = Observaciones.Text;
            p.Sesiones = new List<Sesion>();
            try
            {
                Logica l = new Logica(Properties.Settings.Default.databaseName);
                var pDB = l.GuardarPaciente(p);
                OnSave?.Invoke(this, pDB);

                MessageBox.Show("Paciente agregado correctamente");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); 

            }
        }
    }
}
