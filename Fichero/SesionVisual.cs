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
    public partial class SesionVisual : UserControl
    {
        Sesion Data;
        public SesionVisual(Sesion s)
        {
            InitializeComponent();
            lblFecha.Text = s.GetFechaStr();
            SetNota(s.Notas);
            Data = s;

        }

        private void SetNota(string notas)
        {
            txtNota.Text = notas;
            txtNota.WordWrap = true;
            txtNota.ScrollBars = RichTextBoxScrollBars.None;
        }


        internal void SetTamaño(int width)
        {
            int lines = 1 + ContarEnters(txtNota.Text);
            int margin = 0;
            txtNota.Size = new Size(width, Convert.ToInt32(txtNota.Font.Size) * 2 * lines);
            this.Size = new Size(width + 20, this.lblFecha.Size.Height + margin + txtNota.Size.Height);

        }


        internal int ContarEnters(string str)
        {
            string needle = "\n";
            int needleCount = (str.Length - str.Replace(needle, "").Length) / needle.Length;
            return needleCount;
        }

        private void txtNota_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            const int margin = 5;
            RichTextBox rch = sender as RichTextBox;
            rch.ClientSize = new Size(
                e.NewRectangle.Width + margin,
                e.NewRectangle.Height + margin);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("¿Realmente desea eliminar la sesión?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    Logica l = new Logica(Settings.Properties.DatabaseName);
                    l.EliminarSesion(Data);
                    this.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
