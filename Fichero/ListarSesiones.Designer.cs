
namespace Fichero
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
            this.pacienteUserControl1 = new Fichero.PacienteUserControl();
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
            this.pnlCabecera = new System.Windows.Forms.Panel();
            this.flpAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlObservaciones = new System.Windows.Forms.Panel();
            this.pnlBotonObservacion = new System.Windows.Forms.Panel();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.pnlNuevaSesion = new System.Windows.Forms.Panel();
            this.pnlBotonAgregar = new System.Windows.Forms.Panel();
            this.pnlFecha = new System.Windows.Forms.Panel();
            this.lblFechaSesion = new System.Windows.Forms.Label();
            this.pnlCabecera.SuspendLayout();
            this.flpAcciones.SuspendLayout();
            this.pnlObservaciones.SuspendLayout();
            this.pnlBotonObservacion.SuspendLayout();
            this.pnlNuevaSesion.SuspendLayout();
            this.pnlBotonAgregar.SuspendLayout();
            this.pnlFecha.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlCabecera
            //
            this.pnlCabecera.Controls.Add(this.pacienteUserControl1);
            this.pnlCabecera.Controls.Add(this.flpAcciones);
            this.pnlCabecera.Controls.Add(this.label1);
            this.pnlCabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCabecera.Name = "pnlCabecera";
            this.pnlCabecera.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
            this.pnlCabecera.Size = new System.Drawing.Size(1000, 84);
            this.pnlCabecera.TabIndex = 0;
            //
            // label1
            //
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 52);
            this.label1.TabIndex = 1;
            this.label1.Text = "Paciente";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pacienteUserControl1
            //
            this.pacienteUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pacienteUserControl1.Name = "pacienteUserControl1";
            this.pacienteUserControl1.TabIndex = 0;
            paciente1.DiaNacimiento = null;
            paciente1.Direccion = null;
            paciente1.DNI = "";
            paciente1.Genero = null;
            paciente1.Id = 0;
            paciente1.Localidad = null;
            paciente1.NombreApellido = "";
            paciente1.NroTelefono = null;
            paciente1.Observaciones = null;
            paciente1.Sesiones = null;
            this.pacienteUserControl1.Value = paciente1;
            //
            // flpAcciones
            //
            this.flpAcciones.AutoSize = true;
            this.flpAcciones.Controls.Add(this.btnBuscar);
            this.flpAcciones.Controls.Add(this.btnLimpiar);
            this.flpAcciones.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpAcciones.Name = "flpAcciones";
            this.flpAcciones.TabIndex = 2;
            this.flpAcciones.WrapContents = false;
            //
            // btnBuscar
            //
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(12, 2, 0, 0);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(140, 48);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Actualizar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            //
            // btnLimpiar
            //
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(12, 2, 0, 0);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(110, 48);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            //
            // pnlObservaciones
            //
            this.pnlObservaciones.Controls.Add(this.txtExamenes);
            this.pnlObservaciones.Controls.Add(this.pnlBotonObservacion);
            this.pnlObservaciones.Controls.Add(this.lblObservaciones);
            this.pnlObservaciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlObservaciones.Name = "pnlObservaciones";
            this.pnlObservaciones.Padding = new System.Windows.Forms.Padding(24, 12, 24, 12);
            this.pnlObservaciones.Size = new System.Drawing.Size(1000, 100);
            this.pnlObservaciones.TabIndex = 1;
            //
            // lblObservaciones
            //
            this.lblObservaciones.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(190, 76);
            this.lblObservaciones.TabIndex = 0;
            this.lblObservaciones.Text = "Observaciones";
            this.lblObservaciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlBotonObservacion
            //
            this.pnlBotonObservacion.Controls.Add(this.btnEditarObservacion);
            this.pnlBotonObservacion.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBotonObservacion.Name = "pnlBotonObservacion";
            this.pnlBotonObservacion.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.pnlBotonObservacion.Size = new System.Drawing.Size(240, 76);
            this.pnlBotonObservacion.TabIndex = 2;
            //
            // btnEditarObservacion
            //
            this.btnEditarObservacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEditarObservacion.Name = "btnEditarObservacion";
            this.btnEditarObservacion.TabIndex = 11;
            this.btnEditarObservacion.Text = "Guardar observaciones";
            this.btnEditarObservacion.UseVisualStyleBackColor = true;
            this.btnEditarObservacion.Click += new System.EventHandler(this.btnEditarObservacion_Click);
            //
            // txtExamenes
            //
            this.txtExamenes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExamenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExamenes.Name = "txtExamenes";
            this.txtExamenes.TabIndex = 10;
            this.txtExamenes.Text = "";
            //
            // panel1
            //
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(24, 12, 24, 12);
            this.panel1.TabIndex = 7;
            //
            // pnlNuevaSesion
            //
            this.pnlNuevaSesion.Controls.Add(this.txtNuevoDetalle);
            this.pnlNuevaSesion.Controls.Add(this.pnlFecha);
            this.pnlNuevaSesion.Controls.Add(this.pnlBotonAgregar);
            this.pnlNuevaSesion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNuevaSesion.Name = "pnlNuevaSesion";
            this.pnlNuevaSesion.Padding = new System.Windows.Forms.Padding(24, 12, 24, 16);
            this.pnlNuevaSesion.Size = new System.Drawing.Size(1000, 190);
            this.pnlNuevaSesion.TabIndex = 3;
            //
            // pnlFecha
            //
            this.pnlFecha.Controls.Add(this.dateTimePicker);
            this.pnlFecha.Controls.Add(this.lblFechaSesion);
            this.pnlFecha.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFecha.Name = "pnlFecha";
            this.pnlFecha.Size = new System.Drawing.Size(900, 48);
            this.pnlFecha.TabIndex = 1;
            //
            // lblFechaSesion
            //
            this.lblFechaSesion.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFechaSesion.Name = "lblFechaSesion";
            this.lblFechaSesion.Size = new System.Drawing.Size(190, 48);
            this.lblFechaSesion.TabIndex = 0;
            this.lblFechaSesion.Text = "Nueva sesión";
            this.lblFechaSesion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dateTimePicker
            //
            this.dateTimePicker.CustomFormat = "dd/mm/yyyy";
            this.dateTimePicker.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(240, 29);
            this.dateTimePicker.TabIndex = 8;
            //
            // pnlBotonAgregar
            //
            this.pnlBotonAgregar.Controls.Add(this.btnAgregar);
            this.pnlBotonAgregar.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBotonAgregar.Name = "pnlBotonAgregar";
            this.pnlBotonAgregar.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.pnlBotonAgregar.Size = new System.Drawing.Size(250, 162);
            this.pnlBotonAgregar.TabIndex = 5;
            //
            // btnAgregar
            //
            this.btnAgregar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.Text = "Agregar sesión";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            //
            // txtNuevoDetalle
            //
            this.txtNuevoDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNuevoDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNuevoDetalle.Name = "txtNuevoDetalle";
            this.txtNuevoDetalle.TabIndex = 4;
            this.txtNuevoDetalle.Text = "";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(460, -4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(9, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "i";
            this.label2.Visible = false;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            //
            // ListarSesiones
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlNuevaSesion);
            this.Controls.Add(this.pnlObservaciones);
            this.Controls.Add(this.pnlCabecera);
            this.Controls.Add(this.label2);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ListarSesiones";
            this.Text = "Listado de Sesiones de un Paciente";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlCabecera.ResumeLayout(false);
            this.pnlCabecera.PerformLayout();
            this.flpAcciones.ResumeLayout(false);
            this.pnlObservaciones.ResumeLayout(false);
            this.pnlBotonObservacion.ResumeLayout(false);
            this.pnlNuevaSesion.ResumeLayout(false);
            this.pnlBotonAgregar.ResumeLayout(false);
            this.pnlFecha.ResumeLayout(false);
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
        private System.Windows.Forms.Panel pnlCabecera;
        private System.Windows.Forms.FlowLayoutPanel flpAcciones;
        private System.Windows.Forms.Panel pnlObservaciones;
        private System.Windows.Forms.Panel pnlBotonObservacion;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.Panel pnlNuevaSesion;
        private System.Windows.Forms.Panel pnlBotonAgregar;
        private System.Windows.Forms.Panel pnlFecha;
        private System.Windows.Forms.Label lblFechaSesion;
    }
}