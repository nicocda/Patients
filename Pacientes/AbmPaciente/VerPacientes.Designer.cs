
namespace Pacientes.AbmPaciente
{
    partial class VerPacientes
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
            this.ListaPacientes = new System.Windows.Forms.DataGridView();
            this.Opciones = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ListaPacientes)).BeginInit();
            this.SuspendLayout();
            // 
            // ListaPacientes
            // 
            this.ListaPacientes.AllowUserToAddRows = false;
            this.ListaPacientes.AllowUserToDeleteRows = false;
            this.ListaPacientes.AllowUserToOrderColumns = true;
            this.ListaPacientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListaPacientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Opciones});
            this.ListaPacientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListaPacientes.Location = new System.Drawing.Point(0, 0);
            this.ListaPacientes.Name = "ListaPacientes";
            this.ListaPacientes.ReadOnly = true;
            this.ListaPacientes.RowTemplate.Height = 25;
            this.ListaPacientes.Size = new System.Drawing.Size(800, 450);
            this.ListaPacientes.TabIndex = 0;
            // 
            // Opciones
            // 
            this.Opciones.HeaderText = "";
            this.Opciones.Name = "Opciones";
            this.Opciones.ReadOnly = true;
            // 
            // VerPacientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ListaPacientes);
            this.Name = "VerPacientes";
            this.Text = "VerPacientes";
            this.Load += new System.EventHandler(this.VerPacientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListaPacientes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ListaPacientes;
        private System.Windows.Forms.DataGridViewButtonColumn Opciones;
    }
}