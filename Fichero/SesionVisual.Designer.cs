
namespace Fichero
{
    partial class SesionVisual
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNota = new System.Windows.Forms.RichTextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtNota
            // 
            this.txtNota.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNota.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNota.Location = new System.Drawing.Point(0, 20);
            this.txtNota.Name = "txtNota";
            this.txtNota.ReadOnly = true;
            this.txtNota.Size = new System.Drawing.Size(150, 43);
            this.txtNota.TabIndex = 0;
            this.txtNota.Text = "";
            this.txtNota.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.txtNota_ContentsResized);
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFecha.Location = new System.Drawing.Point(0, 2);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(111, 15);
            this.lblFecha.TabIndex = 1;
            this.lblFecha.Text = "99/99/9999 99:99";
            // 
            // SesionVisual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.txtNota);
            this.Name = "SesionVisual";
            this.Size = new System.Drawing.Size(150, 63);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtNota;
        private System.Windows.Forms.Label lblFecha;
    }
}
