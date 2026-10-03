using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fichero
{
    /// <summary>
    /// Paleta y estilos centralizados. Pensado para leerse a ~5 metros:
    /// alto contraste, tipografía grande y botones con texto (no solo íconos).
    /// </summary>
    static class Tema
    {
        public static readonly Color Fondo = Color.FromArgb(0xF3, 0xF5, 0xF8);
        public static readonly Color Cabecera = Color.FromArgb(0x12, 0x30, 0x4F);
        public static readonly Color Tarjeta = Color.White;
        public static readonly Color Texto = Color.FromArgb(0x10, 0x14, 0x1A);
        public static readonly Color Acento = Color.FromArgb(0x0B, 0x5C, 0xAD);
        public static readonly Color Exito = Color.FromArgb(0x1B, 0x7F, 0x3B);
        public static readonly Color Peligro = Color.FromArgb(0xB3, 0x26, 0x1E);
        public static readonly Color Aviso = Color.FromArgb(0xFF, 0xF4, 0xC2);
        public static readonly Color FilaAlterna = Color.FromArgb(0xE8, 0xEE, 0xF5);

        const string Fuente = "Segoe UI";

        /// <summary>
        /// Factor de escala según el área de trabajo de la pantalla, tomando 1920x1080 como referencia.
        /// Todas las fuentes y medidas fijas del tema se multiplican por este factor.
        /// </summary>
        public static readonly float Escala = CalcularEscala();

        static float CalcularEscala()
        {
            var area = Screen.PrimaryScreen.WorkingArea;
            float e = Math.Min(area.Width / 1920F, area.Height / 1080F);
            return Math.Max(0.8F, Math.Min(1.3F, e));
        }

        public static int Px(int valor)
        {
            return (int)Math.Round(valor * Escala);
        }

        public static Font Normal(float puntos, FontStyle estilo = FontStyle.Regular, string fuente = Fuente)
        {
            return new Font(fuente, puntos * Escala, estilo, GraphicsUnit.Point);
        }

        public static void Boton(Button b, Color fondo, float puntos = 14F)
        {
            b.UseVisualStyleBackColor = false;
            b.BackgroundImage = null;
            b.Image = null;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = ControlPaint.Dark(fondo, 0.1F);
            b.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(fondo, 0.25F);
            b.BackColor = fondo;
            b.ForeColor = Color.White;
            b.Font = Normal(puntos, FontStyle.Bold);
            b.Cursor = Cursors.Hand;
            b.MinimumSize = new Size(Px(110), Px(48));
            if (b.Dock == DockStyle.None)
            {
                b.AutoSize = true;
                b.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                b.Padding = new Padding(Px(14), 0, Px(14), 0);
            }
        }

        public static void Campo(TextBoxBase t, float puntos = 18F)
        {
            t.Font = Normal(puntos);
            t.BackColor = Color.White;
            t.ForeColor = Texto;
        }

        public static void Grilla(DataGridView g)
        {
            g.EnableHeadersVisualStyles = false;
            g.BorderStyle = BorderStyle.None;
            g.BackgroundColor = Fondo;
            g.GridColor = Color.FromArgb(0xC9, 0xD3, 0xDF);
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.RowHeadersVisible = false;
            g.AllowUserToAddRows = false;
            g.AllowUserToResizeRows = false;

            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = Px(52);
            g.ColumnHeadersDefaultCellStyle.BackColor = Cabecera;
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.SelectionBackColor = Cabecera;
            g.ColumnHeadersDefaultCellStyle.Font = Normal(16F, FontStyle.Bold);
            g.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);

            g.RowTemplate.Height = Px(48);
            g.DefaultCellStyle.Font = Normal(16F);
            g.DefaultCellStyle.BackColor = Color.White;
            g.DefaultCellStyle.ForeColor = Texto;
            g.DefaultCellStyle.SelectionBackColor = Acento;
            g.DefaultCellStyle.SelectionForeColor = Color.White;
            g.DefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            g.AlternatingRowsDefaultCellStyle.BackColor = FilaAlterna;
        }

        /// <summary>
        /// Estilo genérico para formularios simples: fondo, colores de texto y campos.
        /// Los botones se estilan según su nombre (Guardar/Agregar = verde, Eliminar = rojo, resto = azul).
        /// </summary>
        public static void Aplicar(Control raiz)
        {
            raiz.BackColor = Fondo;
            raiz.ForeColor = Texto;
            foreach (Control c in raiz.Controls)
                AplicarHijo(c);
        }

        /// <summary>
        /// Para diálogos con layout absoluto diseñado a ~8pt: agranda posiciones, tamaños y fuentes
        /// de los controles (un solo nivel) y el tamaño del formulario.
        /// </summary>
        public static void Ampliar(Form f, float zoom)
        {
            float z = zoom * Escala;
            f.SuspendLayout();
            int inferior = 0;
            foreach (Control c in f.Controls)
            {
                var boton = c as Button;
                if (boton != null)
                {
                    boton.AutoSize = false;
                    boton.MinimumSize = Size.Empty;
                    boton.Font = Normal(14F, FontStyle.Bold);
                    int ancho = Px(160), alto = Px(52);
                    boton.Bounds = new Rectangle((int)Math.Round(c.Right * z) - ancho, (int)Math.Round(c.Top * z), ancho, alto);
                }
                else
                {
                    c.Font = Normal(15F);
                    c.Bounds = new Rectangle(
                        (int)Math.Round(c.Left * z), (int)Math.Round(c.Top * z),
                        (int)Math.Round(c.Width * z), (int)Math.Round(c.Height * z));
                }
                inferior = Math.Max(inferior, c.Bottom);
            }
            f.ClientSize = new Size(
                (int)Math.Round(f.ClientSize.Width * z),
                Math.Max((int)Math.Round(f.ClientSize.Height * z), inferior + Px(24)));
            f.ResumeLayout(true);
        }

        static void AplicarHijo(Control c)
        {
            var boton = c as Button;
            var texto = c as TextBoxBase;
            if (boton != null)
            {
                string n = boton.Name.ToLowerInvariant();
                Color color = Acento;
                if (n.Contains("eliminar")) color = Peligro;
                else if (n.Contains("guardar") || n.Contains("agregar")) color = Exito;
                Boton(boton, color);
            }
            else if (texto != null)
            {
                texto.BackColor = Color.White;
                texto.ForeColor = Texto;
            }

            foreach (Control hijo in c.Controls)
                AplicarHijo(hijo);
        }
    }
}
