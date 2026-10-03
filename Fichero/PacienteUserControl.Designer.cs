
namespace Fichero
{
    partial class PacienteUserControl
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
            this.btnNew = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtNombrePaciente = new System.Windows.Forms.TextBox();
            this.txtDniPaciente = new System.Windows.Forms.TextBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.flpAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.flpAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpAcciones
            // 
            this.flpAcciones.AutoSize = true;
            this.flpAcciones.Controls.Add(this.btnBuscar);
            this.flpAcciones.Controls.Add(this.btnNew);
            this.flpAcciones.Controls.Add(this.btnEliminar);
            this.flpAcciones.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpAcciones.Name = "flpAcciones";
            this.flpAcciones.TabIndex = 12;
            this.flpAcciones.WrapContents = false;
            // 
            // btnNew
            // 
            this.btnNew.Margin = new System.Windows.Forms.Padding(12, 2, 0, 0);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(110, 48);
            this.btnNew.TabIndex = 10;
            this.btnNew.Text = "Nuevo";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(12, 2, 0, 0);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(110, 48);
            this.btnBuscar.TabIndex = 9;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtNombrePaciente
            // 
            this.txtNombrePaciente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNombrePaciente.Name = "txtNombrePaciente";
            this.txtNombrePaciente.TabIndex = 8;
            this.txtNombrePaciente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombrePaciente_KeyPress);
            this.txtNombrePaciente.Leave += new System.EventHandler(this.txtNombrePaciente_Leave);
            // 
            // txtDniPaciente
            // 
            this.txtDniPaciente.Location = new System.Drawing.Point(0, 0);
            this.txtDniPaciente.Name = "txtDniPaciente";
            this.txtDniPaciente.Size = new System.Drawing.Size(52, 20);
            this.txtDniPaciente.TabIndex = 7;
            this.txtDniPaciente.Visible = false;
            this.txtDniPaciente.Leave += new System.EventHandler(this.txtDniPaciente_Leave);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(12, 2, 0, 0);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(110, 48);
            this.btnEliminar.TabIndex = 11;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // PacienteUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtNombrePaciente);
            this.Controls.Add(this.flpAcciones);
            this.Controls.Add(this.txtDniPaciente);
            this.Name = "PacienteUserControl";
            this.Size = new System.Drawing.Size(700, 52);
            this.flpAcciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtNombrePaciente;
        private System.Windows.Forms.TextBox txtDniPaciente;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.FlowLayoutPanel flpAcciones;
    }
}
