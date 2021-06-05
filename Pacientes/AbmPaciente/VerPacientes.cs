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
            Logica logica = new Logica();
            var data = logica.ObtenerPacientes();
            ListaPacientes.DataSource = data;
        }
    }
}
