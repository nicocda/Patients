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
    public partial class SesionVisual : UserControl
    {
        public SesionVisual(Sesion s)
        {
            InitializeComponent();
            SetFecha(s.FechaHora);

            SetNota(s.Notas);
        }

        private void SetNota(string notas)
        {
            txtNota.Text = notas;
            txtNota.WordWrap = true;
            txtNota.ScrollBars = RichTextBoxScrollBars.None;


        }

        private void SetFecha(DateTime f)
        {
            string fechaStr = string.Concat(f.Day,"/",f.Month, "/",f.Year," ",f.Hour,":",f.Minute);
            lblFecha.Text = fechaStr;

        }

        internal void SetTamaño(int width)
        {
            this.Size = new Size(width+20, this.Size.Height);
            txtNota.Size = new Size(width, txtNota.Size.Height);

        }

        private void txtNota_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            const int margin = 0;
            RichTextBox rch = sender as RichTextBox;
            rch.ClientSize = new Size(
                e.NewRectangle.Width + margin,
                e.NewRectangle.Height + margin);
        }
    }
}
