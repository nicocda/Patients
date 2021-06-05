
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
            this.btnAbmPacientes = new System.Windows.Forms.Button();
            this.lblTituloApp = new System.Windows.Forms.Label();
            this.btnAgregarSesion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAbmPacientes
            // 
            this.btnAbmPacientes.Location = new System.Drawing.Point(75, 40);
            this.btnAbmPacientes.Name = "btnAbmPacientes";
            this.btnAbmPacientes.Size = new System.Drawing.Size(137, 115);
            this.btnAbmPacientes.TabIndex = 0;
            this.btnAbmPacientes.Text = "Pacientes";
            this.btnAbmPacientes.UseVisualStyleBackColor = true;
            this.btnAbmPacientes.Click += new System.EventHandler(this.btnAbmPaciente_Click);
            // 
            // lblTituloApp
            // 
            this.lblTituloApp.AutoSize = true;
            this.lblTituloApp.Font = new System.Drawing.Font("OCR A Extended", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTituloApp.ForeColor = System.Drawing.Color.Blue;
            this.lblTituloApp.Location = new System.Drawing.Point(315, 22);
            this.lblTituloApp.Name = "lblTituloApp";
            this.lblTituloApp.Size = new System.Drawing.Size(206, 37);
            this.lblTituloApp.TabIndex = 1;
            this.lblTituloApp.Text = "Pacientes";
            // 
            // btnAgregarSesion
            // 
            this.btnAgregarSesion.Location = new System.Drawing.Point(332, 168);
            this.btnAgregarSesion.Name = "btnAgregarSesion";
            this.btnAgregarSesion.Size = new System.Drawing.Size(137, 115);
            this.btnAgregarSesion.TabIndex = 2;
            this.btnAgregarSesion.Text = "Agregar Sesion";
            this.btnAgregarSesion.UseVisualStyleBackColor = true;
            this.btnAgregarSesion.Click += new System.EventHandler(this.btnAgregarSesion_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAgregarSesion);
            this.Controls.Add(this.lblTituloApp);
            this.Controls.Add(this.btnAbmPacientes);
            this.Name = "Principal";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Principal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAbmPacientes;
        private System.Windows.Forms.Label lblTituloApp;
        private System.Windows.Forms.Button btnAgregarSesion;
    }
}

