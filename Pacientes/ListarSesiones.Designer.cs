
namespace Pacientes
{
    partial class ListarSesiones
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
            Server.Paciente paciente1 = new Server.Paciente();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListarSesiones));
            this.pacienteUserControl1 = new Pacientes.PacienteUserControl();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtNuevoDetalle = new System.Windows.Forms.RichTextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pacienteUserControl1
            // 
            this.pacienteUserControl1.Location = new System.Drawing.Point(102, 44);
            this.pacienteUserControl1.Name = "pacienteUserControl1";
            this.pacienteUserControl1.Size = new System.Drawing.Size(265, 24);
            this.pacienteUserControl1.TabIndex = 0;
            paciente1.DiaNacimiento = null;
            paciente1.Direccion = null;
            paciente1.DNI = "";
            paciente1.Genero = null;
            paciente1.Id = 0;
            paciente1.NombreApellido = "";
            paciente1.NroTelefono = null;
            paciente1.Observaciones = null;
            paciente1.Sesiones = null;
            this.pacienteUserControl1.Value = paciente1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Paciente";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(406, 42);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(104, 26);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar Sesiones";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(33, 92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(507, 271);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // txtNuevoDetalle
            // 
            this.txtNuevoDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNuevoDetalle.Location = new System.Drawing.Point(33, 379);
            this.txtNuevoDetalle.Name = "txtNuevoDetalle";
            this.txtNuevoDetalle.Size = new System.Drawing.Size(364, 59);
            this.txtNuevoDetalle.TabIndex = 4;
            this.txtNuevoDetalle.Text = "";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregar.Location = new System.Drawing.Point(406, 379);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(134, 59);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // ListarSesiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 450);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.txtNuevoDetalle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pacienteUserControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ListarSesiones";
            this.Text = "Listado de Sesiones de un Paciente";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PacienteUserControl pacienteUserControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox txtNuevoDetalle;
        private System.Windows.Forms.Button btnAgregar;
    }
}