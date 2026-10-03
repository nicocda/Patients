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
        // Ancho que se descuenta del contenedor: sangría del texto + margen derecho de la tarjeta
        internal const int MargenHorizontal = 44;
        const int AnchoAcento = 10;

        Sesion Data;
        public SesionVisual(Sesion s)
        {
            InitializeComponent();
            AplicarTema();
            lblFecha.Text = s.GetFechaStr();
            SetNota(s.Notas);
            Data = s;

        }

        private void AplicarTema()
        {
            BackColor = Tema.Tarjeta;
            Margin = new Padding(0, 0, 0, 14);
            lblFecha.ForeColor = Tema.Acento;
            txtNota.BackColor = Tema.Tarjeta;
            txtNota.ForeColor = Tema.Texto;
            Tema.Boton(btnEliminar, Tema.Peligro, 13F);

            lblFecha.Font = Tema.Normal(22F, FontStyle.Bold, "Segoe UI Symbol");
            txtNota.Font = Tema.Normal(36F, FontStyle.Regular, "Segoe UI Symbol");
            txtNota.Top = lblFecha.Top + lblFecha.PreferredHeight + Tema.Px(4);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var acento = new SolidBrush(Tema.Acento))
                e.Graphics.FillRectangle(acento, 0, 0, AnchoAcento, Height);
            using (var linea = new Pen(Color.FromArgb(0xC9, 0xD3, 0xDF)))
                e.Graphics.DrawRectangle(linea, 0, 0, Width - 1, Height - 1);
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
            this.Size = new Size(width + txtNota.Left + 12, txtNota.Top + margin + txtNota.Size.Height + 14);
            btnEliminar.Left = this.Width - btnEliminar.Width - 16;

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
            this.Height = rch.Bottom + 14;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Desea eliminar la sesión \r\n"+ txtNota.Text +" ("+ lblFecha.Text+")", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
