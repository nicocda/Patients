
namespace Pacientes
{
    partial class VerSesionesDia
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerSesionesDia));
            this.label1 = new System.Windows.Forms.Label();
            this.fecha = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Día";
            // 
            // fecha
            // 
            this.fecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fecha.Location = new System.Drawing.Point(72, 25);
            this.fecha.Name = "fecha";
            this.fecha.Size = new System.Drawing.Size(103, 23);
            this.fecha.TabIndex = 1;
            this.fecha.ValueChanged += new System.EventHandler(this.fecha_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(13, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(349, 393);
            this.panel1.TabIndex = 2;
            // 
            // VerSesionesDia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 470);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.fecha);
            this.Controls.Add(this.label1);
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VerSesionesDia";
            this.Text = "Sesiones Dia";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker fecha;
        private System.Windows.Forms.Panel panel1;
    }
}