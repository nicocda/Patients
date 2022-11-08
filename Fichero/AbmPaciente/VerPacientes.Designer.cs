
namespace Fichero.AbmPaciente
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerPacientes));
            this.ListaPacientesGrid = new System.Windows.Forms.DataGridView();
            this.pacienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtSearchApenom = new System.Windows.Forms.TextBox();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.lblCantRegistros = new System.Windows.Forms.Label();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreApellidoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dNIDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.generoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direccionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.localidadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.observacionesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ListaPacientesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pacienteBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ListaPacientesGrid
            // 
            this.ListaPacientesGrid.AllowUserToOrderColumns = true;
            this.ListaPacientesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListaPacientesGrid.AutoGenerateColumns = false;
            this.ListaPacientesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ListaPacientesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListaPacientesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.nombreApellidoDataGridViewTextBoxColumn,
            this.dNIDataGridViewTextBoxColumn,
            this.generoDataGridViewTextBoxColumn,
            this.direccionDataGridViewTextBoxColumn,
            this.localidadDataGridViewTextBoxColumn,
            this.observacionesDataGridViewTextBoxColumn});
            this.ListaPacientesGrid.DataSource = this.pacienteBindingSource;
            this.ListaPacientesGrid.Location = new System.Drawing.Point(0, 48);
            this.ListaPacientesGrid.MultiSelect = false;
            this.ListaPacientesGrid.Name = "ListaPacientesGrid";
            this.ListaPacientesGrid.RowTemplate.Height = 25;
            this.ListaPacientesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListaPacientesGrid.ShowEditingIcon = false;
            this.ListaPacientesGrid.Size = new System.Drawing.Size(1120, 450);
            this.ListaPacientesGrid.TabIndex = 6;
            this.ListaPacientesGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListaPacientesGrid_MouseDoubleClick);
            // 
            // pacienteBindingSource
            // 
            this.pacienteBindingSource.DataSource = typeof(Server.Paciente);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.Location = new System.Drawing.Point(1006, 10);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(30, 30);
            this.btnNuevo.TabIndex = 3;
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.Location = new System.Drawing.Point(1078, 10);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(30, 30);
            this.btnEliminar.TabIndex = 5;
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.Location = new System.Drawing.Point(1042, 10);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(30, 30);
            this.btnEditar.TabIndex = 4;
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.Location = new System.Drawing.Point(970, 10);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(30, 30);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtSearchApenom
            // 
            this.txtSearchApenom.Location = new System.Drawing.Point(39, 22);
            this.txtSearchApenom.Name = "txtSearchApenom";
            this.txtSearchApenom.Size = new System.Drawing.Size(186, 20);
            this.txtSearchApenom.TabIndex = 0;
            this.txtSearchApenom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchApenom_KeyPress);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(231, 20);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(75, 23);
            this.btnFiltrar.TabIndex = 1;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // lblCantRegistros
            // 
            this.lblCantRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCantRegistros.AutoSize = true;
            this.lblCantRegistros.Location = new System.Drawing.Point(970, 498);
            this.lblCantRegistros.Name = "lblCantRegistros";
            this.lblCantRegistros.Size = new System.Drawing.Size(0, 13);
            this.lblCantRegistros.TabIndex = 7;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // nombreApellidoDataGridViewTextBoxColumn
            // 
            this.nombreApellidoDataGridViewTextBoxColumn.DataPropertyName = "NombreApellido";
            this.nombreApellidoDataGridViewTextBoxColumn.HeaderText = "Nombre y Apellido";
            this.nombreApellidoDataGridViewTextBoxColumn.Name = "nombreApellidoDataGridViewTextBoxColumn";
            // 
            // dNIDataGridViewTextBoxColumn
            // 
            this.dNIDataGridViewTextBoxColumn.DataPropertyName = "DNI";
            this.dNIDataGridViewTextBoxColumn.HeaderText = "DNI";
            this.dNIDataGridViewTextBoxColumn.Name = "dNIDataGridViewTextBoxColumn";
            // 
            // generoDataGridViewTextBoxColumn
            // 
            this.generoDataGridViewTextBoxColumn.DataPropertyName = "NroTelefono";
            this.generoDataGridViewTextBoxColumn.HeaderText = "Telefono";
            this.generoDataGridViewTextBoxColumn.Name = "generoDataGridViewTextBoxColumn";
            // 
            // direccionDataGridViewTextBoxColumn
            // 
            this.direccionDataGridViewTextBoxColumn.DataPropertyName = "Direccion";
            this.direccionDataGridViewTextBoxColumn.HeaderText = "Direccion";
            this.direccionDataGridViewTextBoxColumn.Name = "direccionDataGridViewTextBoxColumn";
            // 
            // localidadDataGridViewTextBoxColumn
            // 
            this.localidadDataGridViewTextBoxColumn.DataPropertyName = "Localidad";
            this.localidadDataGridViewTextBoxColumn.HeaderText = "Localidad";
            this.localidadDataGridViewTextBoxColumn.Name = "localidadDataGridViewTextBoxColumn";
            // 
            // observacionesDataGridViewTextBoxColumn
            // 
            this.observacionesDataGridViewTextBoxColumn.DataPropertyName = "Observaciones";
            this.observacionesDataGridViewTextBoxColumn.HeaderText = "Observaciones";
            this.observacionesDataGridViewTextBoxColumn.Name = "observacionesDataGridViewTextBoxColumn";
            // 
            // VerPacientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 510);
            this.Controls.Add(this.lblCantRegistros);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.txtSearchApenom);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.ListaPacientesGrid);
            this.Name = "VerPacientes";
            this.Text = "Pacientes";
            this.Load += new System.EventHandler(this.VerPacientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListaPacientesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pacienteBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.DataGridView ListaPacientesGrid;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.BindingSource pacienteBindingSource;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn nroTelefonoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diaNacimientoDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox txtSearchApenom;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Label lblCantRegistros;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreApellidoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dNIDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn generoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn direccionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn localidadDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn observacionesDataGridViewTextBoxColumn;
    }
}