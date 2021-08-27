
namespace Fichero.AbmPaciente
{
    partial class EditarPaciente
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
            this.label1 = new System.Windows.Forms.Label();
            this.NombreApellido = new System.Windows.Forms.TextBox();
            this.Documento = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Telefono = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Direccion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.GenHombre = new System.Windows.Forms.RadioButton();
            this.GenMujer = new System.Windows.Forms.RadioButton();
            this.Nacimiento = new System.Windows.Forms.DateTimePicker();
            this.Observaciones = new System.Windows.Forms.RichTextBox();
            this.Guardar = new System.Windows.Forms.Button();
            this.Localidad = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre y Apellido";
            // 
            // NombreApellido
            // 
            this.NombreApellido.Location = new System.Drawing.Point(159, 40);
            this.NombreApellido.Name = "NombreApellido";
            this.NombreApellido.Size = new System.Drawing.Size(172, 20);
            this.NombreApellido.TabIndex = 1;
            // 
            // Documento
            // 
            this.Documento.Location = new System.Drawing.Point(159, 65);
            this.Documento.Name = "Documento";
            this.Documento.Size = new System.Drawing.Size(172, 20);
            this.Documento.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "DNI";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Genero";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Fecha de Nacimiento";
            // 
            // Telefono
            // 
            this.Telefono.Location = new System.Drawing.Point(159, 140);
            this.Telefono.Name = "Telefono";
            this.Telefono.Size = new System.Drawing.Size(172, 20);
            this.Telefono.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Telefono";
            // 
            // Direccion
            // 
            this.Direccion.Location = new System.Drawing.Point(159, 166);
            this.Direccion.Name = "Direccion";
            this.Direccion.Size = new System.Drawing.Size(172, 20);
            this.Direccion.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Dirección";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 219);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Observaciones";
            // 
            // GenHombre
            // 
            this.GenHombre.AutoSize = true;
            this.GenHombre.Location = new System.Drawing.Point(159, 119);
            this.GenHombre.Name = "GenHombre";
            this.GenHombre.Size = new System.Drawing.Size(62, 17);
            this.GenHombre.TabIndex = 4;
            this.GenHombre.TabStop = true;
            this.GenHombre.Text = "Hombre";
            this.GenHombre.UseVisualStyleBackColor = true;
            // 
            // GenMujer
            // 
            this.GenMujer.AutoSize = true;
            this.GenMujer.Location = new System.Drawing.Point(249, 119);
            this.GenMujer.Name = "GenMujer";
            this.GenMujer.Size = new System.Drawing.Size(51, 17);
            this.GenMujer.TabIndex = 5;
            this.GenMujer.TabStop = true;
            this.GenMujer.Text = "Mujer";
            this.GenMujer.UseVisualStyleBackColor = true;
            // 
            // Nacimiento
            // 
            this.Nacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Nacimiento.Location = new System.Drawing.Point(159, 91);
            this.Nacimiento.Name = "Nacimiento";
            this.Nacimiento.Size = new System.Drawing.Size(172, 20);
            this.Nacimiento.TabIndex = 3;
            // 
            // Observaciones
            // 
            this.Observaciones.Location = new System.Drawing.Point(158, 218);
            this.Observaciones.Name = "Observaciones";
            this.Observaciones.Size = new System.Drawing.Size(172, 84);
            this.Observaciones.TabIndex = 8;
            this.Observaciones.Text = "";
            // 
            // Guardar
            // 
            this.Guardar.Location = new System.Drawing.Point(265, 327);
            this.Guardar.Name = "Guardar";
            this.Guardar.Size = new System.Drawing.Size(64, 20);
            this.Guardar.TabIndex = 17;
            this.Guardar.Text = "Guardar";
            this.Guardar.UseVisualStyleBackColor = true;
            this.Guardar.Click += new System.EventHandler(this.Guardar_Click);
            // 
            // Localidad
            // 
            this.Localidad.Location = new System.Drawing.Point(158, 192);
            this.Localidad.Name = "Localidad";
            this.Localidad.Size = new System.Drawing.Size(172, 20);
            this.Localidad.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(43, 194);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Localidad";
            // 
            // EditarPaciente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 362);
            this.Controls.Add(this.Localidad);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Guardar);
            this.Controls.Add(this.Observaciones);
            this.Controls.Add(this.Nacimiento);
            this.Controls.Add(this.GenMujer);
            this.Controls.Add(this.GenHombre);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Direccion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Telefono);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Documento);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NombreApellido);
            this.Controls.Add(this.label1);
            this.Name = "EditarPaciente";
            this.Text = "Editar Paciente";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NombreApellido;
        private System.Windows.Forms.TextBox Documento;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Telefono;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Direccion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton GenHombre;
        private System.Windows.Forms.RadioButton GenMujer;
        private System.Windows.Forms.DateTimePicker Nacimiento;
        private System.Windows.Forms.RichTextBox Observaciones;
        private System.Windows.Forms.Button Guardar;
        private System.Windows.Forms.TextBox Localidad;
        private System.Windows.Forms.Label label8;
    }
}