using Pacientes.AbmPaciente;
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
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void btnAbmPaciente_Click(object sender, EventArgs e)
        {
            VerPacientes form = new VerPacientes();
            form.Show();
        }

        private void Principal_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregarSesion_Click(object sender, EventArgs e)
        {
            AgregarSesion form = new AgregarSesion();
            form.Show();
        }

        private void btnVerSesiones_Click(object sender, EventArgs e)
        {
            ListarSesiones form = new ListarSesiones();
            form.Show();
        }

        private void SesionesDia_Click(object sender, EventArgs e)
        {
            VerSesionesDia form = new VerSesionesDia();
            form.Show();
        }
    }
}
