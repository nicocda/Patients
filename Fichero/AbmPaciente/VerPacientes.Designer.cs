
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
            this.ListaPacientesGrid = new System.Windows.Forms.DataGridView();
            this.pacienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtSearchApenom = new System.Windows.Forms.TextBox();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.lblCantRegistros = new System.Windows.Forms.Label();
            this.pnlCabecera = new System.Windows.Forms.Panel();
            this.flpAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlFiltro = new System.Windows.Forms.Panel();
            this.pnlPie = new System.Windows.Forms.Panel();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreApellidoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dNIDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.generoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direccionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.localidadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.observacionesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ListaPacientesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pacienteBindingSource)).BeginInit();
            this.pnlCabecera.SuspendLayout();
            this.flpAcciones.SuspendLayout();
            this.pnlFiltro.SuspendLayout();
            this.pnlPie.SuspendLayout();
            this.SuspendLayout();
            //
            // ListaPacientesGrid
            //
            this.ListaPacientesGrid.AllowUserToOrderColumns = true;
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
            this.ListaPacientesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListaPacientesGrid.MultiSelect = false;
            this.ListaPacientesGrid.Name = "ListaPacientesGrid";
            this.ListaPacientesGrid.RowTemplate.Height = 25;
            this.ListaPacientesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListaPacientesGrid.ShowEditingIcon = false;
            this.ListaPacientesGrid.TabIndex = 6;
            this.ListaPacientesGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListaPacientesGrid_MouseDoubleClick);
            //
            // pacienteBindingSource
            //
            this.pacienteBindingSource.DataSource = typeof(Server.Paciente);
            //
            // pnlCabecera
            //
            this.pnlCabecera.Controls.Add(this.pnlFiltro);
            this.pnlCabecera.Controls.Add(this.flpAcciones);
            this.pnlCabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCabecera.Name = "pnlCabecera";
            this.pnlCabecera.Padding = new System.Windows.Forms.Padding(20, 14, 20, 14);
            this.pnlCabecera.Size = new System.Drawing.Size(1120, 80);
            this.pnlCabecera.TabIndex = 8;
            //
            // flpAcciones
            //
            this.flpAcciones.AutoSize = true;
            this.flpAcciones.Controls.Add(this.btnSelect);
            this.flpAcciones.Controls.Add(this.btnEditar);
            this.flpAcciones.Controls.Add(this.btnNuevo);
            this.flpAcciones.Controls.Add(this.btnEliminar);
            this.flpAcciones.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpAcciones.Name = "flpAcciones";
            this.flpAcciones.TabIndex = 9;
            this.flpAcciones.WrapContents = false;
            //
            // btnSelect
            //
            this.btnSelect.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(130, 52);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Elegir";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            //
            // btnEditar
            //
            this.btnEditar.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(130, 52);
            this.btnEditar.TabIndex = 4;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            //
            // btnNuevo
            //
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(130, 52);
            this.btnNuevo.TabIndex = 3;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            //
            // btnEliminar
            //
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(130, 52);
            this.btnEliminar.TabIndex = 5;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            //
            // pnlFiltro
            //
            this.pnlFiltro.Controls.Add(this.txtSearchApenom);
            this.pnlFiltro.Controls.Add(this.btnFiltrar);
            this.pnlFiltro.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlFiltro.Name = "pnlFiltro";
            this.pnlFiltro.Size = new System.Drawing.Size(520, 52);
            this.pnlFiltro.TabIndex = 0;
            //
            // txtSearchApenom
            //
            this.txtSearchApenom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchApenom.Name = "txtSearchApenom";
            this.txtSearchApenom.TabIndex = 0;
            this.txtSearchApenom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchApenom_KeyPress);
            //
            // btnFiltrar
            //
            this.btnFiltrar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(130, 52);
            this.btnFiltrar.TabIndex = 1;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            //
            // pnlPie
            //
            this.pnlPie.Controls.Add(this.lblCantRegistros);
            this.pnlPie.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPie.Name = "pnlPie";
            this.pnlPie.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.pnlPie.Size = new System.Drawing.Size(1120, 48);
            this.pnlPie.TabIndex = 7;
            //
            // lblCantRegistros
            //
            this.lblCantRegistros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCantRegistros.Name = "lblCantRegistros";
            this.lblCantRegistros.TabIndex = 7;
            this.lblCantRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.nombreApellidoDataGridViewTextBoxColumn.FillWeight = 26F;
            this.nombreApellidoDataGridViewTextBoxColumn.HeaderText = "Nombre y Apellido";
            this.nombreApellidoDataGridViewTextBoxColumn.Name = "nombreApellidoDataGridViewTextBoxColumn";
            //
            // dNIDataGridViewTextBoxColumn
            //
            this.dNIDataGridViewTextBoxColumn.DataPropertyName = "DNI";
            this.dNIDataGridViewTextBoxColumn.FillWeight = 12F;
            this.dNIDataGridViewTextBoxColumn.HeaderText = "DNI";
            this.dNIDataGridViewTextBoxColumn.Name = "dNIDataGridViewTextBoxColumn";
            //
            // generoDataGridViewTextBoxColumn
            //
            this.generoDataGridViewTextBoxColumn.DataPropertyName = "NroTelefono";
            this.generoDataGridViewTextBoxColumn.FillWeight = 14F;
            this.generoDataGridViewTextBoxColumn.HeaderText = "Telefono";
            this.generoDataGridViewTextBoxColumn.Name = "generoDataGridViewTextBoxColumn";
            //
            // direccionDataGridViewTextBoxColumn
            //
            this.direccionDataGridViewTextBoxColumn.DataPropertyName = "Direccion";
            this.direccionDataGridViewTextBoxColumn.FillWeight = 18F;
            this.direccionDataGridViewTextBoxColumn.HeaderText = "Direccion";
            this.direccionDataGridViewTextBoxColumn.Name = "direccionDataGridViewTextBoxColumn";
            //
            // localidadDataGridViewTextBoxColumn
            //
            this.localidadDataGridViewTextBoxColumn.DataPropertyName = "Localidad";
            this.localidadDataGridViewTextBoxColumn.FillWeight = 14F;
            this.localidadDataGridViewTextBoxColumn.HeaderText = "Localidad";
            this.localidadDataGridViewTextBoxColumn.Name = "localidadDataGridViewTextBoxColumn";
            //
            // observacionesDataGridViewTextBoxColumn
            //
            this.observacionesDataGridViewTextBoxColumn.DataPropertyName = "Observaciones";
            this.observacionesDataGridViewTextBoxColumn.FillWeight = 30F;
            this.observacionesDataGridViewTextBoxColumn.HeaderText = "Observaciones";
            this.observacionesDataGridViewTextBoxColumn.Name = "observacionesDataGridViewTextBoxColumn";
            //
            // VerPacientes
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 510);
            this.Controls.Add(this.ListaPacientesGrid);
            this.Controls.Add(this.pnlPie);
            this.Controls.Add(this.pnlCabecera);
            this.MinimumSize = new System.Drawing.Size(900, 400);
            this.Name = "VerPacientes";
            this.Text = "Pacientes";
            this.Load += new System.EventHandler(this.VerPacientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListaPacientesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pacienteBindingSource)).EndInit();
            this.pnlCabecera.ResumeLayout(false);
            this.pnlCabecera.PerformLayout();
            this.flpAcciones.ResumeLayout(false);
            this.pnlFiltro.ResumeLayout(false);
            this.pnlFiltro.PerformLayout();
            this.pnlPie.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Panel pnlCabecera;
        private System.Windows.Forms.FlowLayoutPanel flpAcciones;
        private System.Windows.Forms.Panel pnlFiltro;
        private System.Windows.Forms.Panel pnlPie;
    }
}