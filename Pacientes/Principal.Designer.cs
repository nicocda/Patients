
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.btnAbmPacientes = new System.Windows.Forms.Button();
            this.btnAgregarSesion = new System.Windows.Forms.Button();
            this.btnVerSesiones = new System.Windows.Forms.Button();
            this.SesionesDia = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAbmPacientes
            // 
            this.btnAbmPacientes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbmPacientes.Location = new System.Drawing.Point(651, 492);
            this.btnAbmPacientes.Name = "btnAbmPacientes";
            this.btnAbmPacientes.Size = new System.Drawing.Size(137, 115);
            this.btnAbmPacientes.TabIndex = 4;
            this.btnAbmPacientes.Text = "Pacientes";
            this.btnAbmPacientes.UseVisualStyleBackColor = true;
            this.btnAbmPacientes.Click += new System.EventHandler(this.btnAbmPaciente_Click);
            // 
            // btnAgregarSesion
            // 
            this.btnAgregarSesion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarSesion.Location = new System.Drawing.Point(508, 492);
            this.btnAgregarSesion.Name = "btnAgregarSesion";
            this.btnAgregarSesion.Size = new System.Drawing.Size(137, 115);
            this.btnAgregarSesion.TabIndex = 3;
            this.btnAgregarSesion.Text = "Agregar Sesion";
            this.btnAgregarSesion.UseVisualStyleBackColor = true;
            this.btnAgregarSesion.Click += new System.EventHandler(this.btnAgregarSesion_Click);
            // 
            // btnVerSesiones
            // 
            this.btnVerSesiones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerSesiones.Location = new System.Drawing.Point(365, 492);
            this.btnVerSesiones.Name = "btnVerSesiones";
            this.btnVerSesiones.Size = new System.Drawing.Size(137, 115);
            this.btnVerSesiones.TabIndex = 2;
            this.btnVerSesiones.Text = "Ver sesiones";
            this.btnVerSesiones.UseVisualStyleBackColor = true;
            this.btnVerSesiones.Click += new System.EventHandler(this.btnVerSesiones_Click);
            // 
            // SesionesDia
            // 
            this.SesionesDia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SesionesDia.Location = new System.Drawing.Point(222, 492);
            this.SesionesDia.Name = "SesionesDia";
            this.SesionesDia.Size = new System.Drawing.Size(137, 115);
            this.SesionesDia.TabIndex = 1;
            this.SesionesDia.Text = "Ver sesiones del día";
            this.SesionesDia.UseVisualStyleBackColor = true;
            this.SesionesDia.Click += new System.EventHandler(this.SesionesDia_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 619);
            this.Controls.Add(this.SesionesDia);
            this.Controls.Add(this.btnVerSesiones);
            this.Controls.Add(this.btnAgregarSesion);
            this.Controls.Add(this.btnAbmPacientes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Principal";
            this.Text = "Pacientes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Principal_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAbmPacientes;
        private System.Windows.Forms.Button btnAgregarSesion;
        private System.Windows.Forms.Button btnVerSesiones;
        private System.Windows.Forms.Button SesionesDia;
    }
}

