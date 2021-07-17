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
    public partial class VerSesionesDia : Form
    {
        public VerSesionesDia()
        {
            InitializeComponent();
            this.Load += VerSesionesDia_Load;
            
        }

        private void VerSesionesDia_Load(object sender, EventArgs e)
        {
            RecargarData();
        }

        private void fecha_ValueChanged(object sender, EventArgs e)
        {
            RecargarData();
        }

        private void RecargarData()
        {
            panel1.Controls.Clear();

            Logica logica = new Logica(Settings.Properties.DatabaseName);
            List<Sesion> sesiones = logica.ObtenerSesionesDia(fecha.Value);

            if (sesiones != null && sesiones.Count > 0)
            {
                sesiones = sesiones.OrderBy(c => c.FechaHora).ToList();
                int xCol1 = 10;
                int xCol2 = 54;
                int xCol3 = 200;
                int y = 10;
                foreach (var s in sesiones)
                {
                    if (s.Paciente == null)
                        break;

                    Label control = GetLabel(s.GetFechaStr(true));
                    control.Location = new System.Drawing.Point(xCol1,y);
                    panel1.Controls.Add(control);

                    control = GetLabel(s.Paciente.NombreApellido.Trim());
                    control.Location = new System.Drawing.Point(xCol2, y);
                    panel1.Controls.Add(control);


                    Button buttonSearch = new Button();
                    buttonSearch.Tag = s;
                    buttonSearch.Image = global::Pacientes.Properties.Resources.search;
                    buttonSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    buttonSearch.Name = "buttonSearch" + s.Id;
                    buttonSearch.Size = new System.Drawing.Size(23, 23);
                    buttonSearch.UseVisualStyleBackColor = true;
                    buttonSearch.Click += new System.EventHandler(this.btnBuscar_Click);
                    buttonSearch.Location = new System.Drawing.Point(xCol3, y);

                    panel1.Controls.Add(buttonSearch);

                    y = y + buttonSearch.Size.Height + 10;
                }
            }
            else
            {
                Label control = GetLabel("Sin sesiones");
                control.Location = new System.Drawing.Point(45, 10);
                panel1.Controls.Add(control);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Button control = (Button)sender;
            Sesion sesion = (Sesion)control.Tag;
            Pacientes.AgregarSesion form = new AgregarSesion(sesion);
            form.Show();
        }

        private Label GetLabel(string text)
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Name = "lbl"+text;
            label.Text = text;

            return label;
        }
    }
}
