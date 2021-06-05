
namespace Pacientes
{
    partial class Principal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AbmPacientes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AbmPacientes
            // 
            this.AbmPacientes.Location = new System.Drawing.Point(75, 40);
            this.AbmPacientes.Name = "AbmPacientes";
            this.AbmPacientes.Size = new System.Drawing.Size(75, 23);
            this.AbmPacientes.TabIndex = 0;
            this.AbmPacientes.Text = "Pacientes";
            this.AbmPacientes.UseVisualStyleBackColor = true;
            this.AbmPacientes.Click += new System.EventHandler(this.button1_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.AbmPacientes);
            this.Name = "Principal";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AbmPacientes;
    }
}

