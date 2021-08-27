
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
            this.txtNuevoDetalle = new System.Windows.Forms.RichTextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.txtExamenes = new System.Windows.Forms.RichTextBox();
            this.btnEditarObservacion = new System.Windows.Forms.Button();
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
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.Image = global::Pacientes.Properties.Resources.refresh;
            this.btnBuscar.Location = new System.Drawing.Point(484, 54);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(23, 26);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
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
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(537, -5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "i";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(33, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(503, 287);
            this.panel1.TabIndex = 7;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker.CustomFormat = "dd/mm/yyyy";
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker.Location = new System.Drawing.Point(303, 415);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(94, 23);
            this.dateTimePicker.TabIndex = 8;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiar.Image = global::Pacientes.Properties.Resources.delete;
            this.btnLimpiar.Location = new System.Drawing.Point(513, 54);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(23, 26);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtExamenes
            // 
            this.txtExamenes.Location = new System.Drawing.Point(373, 31);
            this.txtExamenes.Name = "txtExamenes";
            this.txtExamenes.Size = new System.Drawing.Size(108, 49);
            this.txtExamenes.TabIndex = 10;
            this.txtExamenes.Text = "";
            // 
            // btnEditarObservacion
            // 
            this.btnEditarObservacion.Image = global::Pacientes.Properties.Resources.pen;
            this.btnEditarObservacion.Location = new System.Drawing.Point(457, 54);
            this.btnEditarObservacion.Name = "btnEditarObservacion";
            this.btnEditarObservacion.Size = new System.Drawing.Size(23, 26);
            this.btnEditarObservacion.TabIndex = 11;
            this.btnEditarObservacion.UseVisualStyleBackColor = true;
            this.btnEditarObservacion.Click += new System.EventHandler(this.btnEditarObservacion_Click);
            // 
            // ListarSesiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 450);
            this.Controls.Add(this.btnEditarObservacion);
            this.Controls.Add(this.txtExamenes);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.txtNuevoDetalle);
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
        private System.Windows.Forms.RichTextBox txtNuevoDetalle;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.RichTextBox txtExamenes;
        private System.Windows.Forms.Button btnEditarObservacion;
    }
}