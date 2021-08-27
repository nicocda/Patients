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
    public partial class EditarPaciente : Form
    {
        Paciente Paciente;
        public delegate void OnSaveHandler(object sender, Paciente e);
        public event OnSaveHandler OnSave;

        public EditarPaciente(Paciente p)
        {
            InitializeComponent();


            NombreApellido.Text = p.NombreApellido;
            Documento.Text = p.DNI;
            if(p.DiaNacimiento.HasValue)
            Nacimiento.Value = p.DiaNacimiento.Value;
            if (p.Genero == "Hombre")
                GenHombre.Checked = true;
            else GenMujer.Checked = true;
            Telefono.Text = p.NroTelefono;
            Direccion.Text = p.Direccion;
            Observaciones.Text = p.Observaciones;
            Localidad.Text = p.Localidad;
            this.Paciente = p;
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NombreApellido.Text))
            {
                MessageBox.Show("Ingrese un nombre y apellido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Paciente.NombreApellido = NombreApellido.Text.Trim();
            Paciente.DiaNacimiento = Nacimiento.Value;
            Paciente.Direccion = Direccion.Text;
            Paciente.DNI = Documento.Text;
            Paciente.Genero = GenHombre.Checked ? GenHombre.Text : GenMujer.Text;
            Paciente.NroTelefono = Telefono.Text;
            Paciente.Observaciones = Observaciones.Text;
            Paciente.Localidad = Localidad.Text;
            try
            {
                Logica l = new Logica(Settings.Properties.DatabaseName);
                var pDB = l.GuardarPaciente(Paciente);
                OnSave?.Invoke(this, pDB);

                MessageBox.Show("Paciente editado correctamente");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); 

            }
        }
    }
}
