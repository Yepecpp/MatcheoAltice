﻿namespace MatcheoAltice.Estafeta
{
    partial class VentasForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VentasForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPay = new FontAwesome.Sharp.IconButton();
            this.btnExportar = new FontAwesome.Sharp.IconButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.iconButton6 = new FontAwesome.Sharp.IconButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.iconButton5 = new FontAwesome.Sharp.IconButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.fechaDigitacionOrdenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuarioCreoOrdenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vendedorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomSubcanalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomActividadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipoActividadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.razonServicioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grupoActivacionOrdenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomFacturaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cedulaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pasaporteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telefonoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.simDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomPlanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telContactoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telContacto2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.montoOrdenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.montoOrdenConImpuestosDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estaFirmadoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estadoTransaccionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ventaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ventaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnPay);
            this.panel1.Controls.Add(this.btnExportar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(986, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 837);
            this.panel1.TabIndex = 50;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(66, 79);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 17);
            this.label3.TabIndex = 50;
            this.label3.Text = "Total Monto: 0$";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(49, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 19);
            this.label2.TabIndex = 48;
            this.label2.Text = "0 filas";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 663);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 47;
            this.label1.Text = "Exportando...";
            this.label1.Visible = false;
            // 
            // btnPay
            // 
            this.btnPay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPay.FlatAppearance.BorderSize = 0;
            this.btnPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPay.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPay.IconChar = FontAwesome.Sharp.IconChar.FileInvoice;
            this.btnPay.IconColor = System.Drawing.Color.Black;
            this.btnPay.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPay.IconSize = 35;
            this.btnPay.Location = new System.Drawing.Point(0, 692);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(188, 71);
            this.btnPay.TabIndex = 46;
            this.btnPay.Text = "Importar Ventas";
            this.btnPay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPay.UseVisualStyleBackColor = true;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnExportar.Enabled = false;
            this.btnExportar.FlatAppearance.BorderSize = 0;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.IconChar = FontAwesome.Sharp.IconChar.ArrowRightFromFile;
            this.btnExportar.IconColor = System.Drawing.Color.Black;
            this.btnExportar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExportar.IconSize = 35;
            this.btnExportar.Location = new System.Drawing.Point(0, 763);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(188, 74);
            this.btnExportar.TabIndex = 4;
            this.btnExportar.Text = "Exportar Excel";
            this.btnExportar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.iconButton5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(986, 107);
            this.panel3.TabIndex = 52;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel2.Controls.Add(this.iconButton6);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Location = new System.Drawing.Point(324, 39);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(482, 29);
            this.panel2.TabIndex = 50;
            // 
            // iconButton6
            // 
            this.iconButton6.BackColor = System.Drawing.Color.Transparent;
            this.iconButton6.Dock = System.Windows.Forms.DockStyle.Left;
            this.iconButton6.Enabled = false;
            this.iconButton6.FlatAppearance.BorderSize = 0;
            this.iconButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton6.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.iconButton6.IconColor = System.Drawing.Color.Black;
            this.iconButton6.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton6.IconSize = 25;
            this.iconButton6.Location = new System.Drawing.Point(0, 0);
            this.iconButton6.Name = "iconButton6";
            this.iconButton6.Size = new System.Drawing.Size(28, 29);
            this.iconButton6.TabIndex = 45;
            this.iconButton6.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(34, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(445, 23);
            this.textBox1.TabIndex = 44;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // iconButton5
            // 
            this.iconButton5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.iconButton5.BackColor = System.Drawing.SystemColors.Control;
            this.iconButton5.Enabled = false;
            this.iconButton5.FlatAppearance.BorderSize = 0;
            this.iconButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton5.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton5.ForeColor = System.Drawing.Color.Black;
            this.iconButton5.IconChar = FontAwesome.Sharp.IconChar.FileInvoice;
            this.iconButton5.IconColor = System.Drawing.Color.Black;
            this.iconButton5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton5.IconSize = 35;
            this.iconButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton5.Location = new System.Drawing.Point(3, 14);
            this.iconButton5.Name = "iconButton5";
            this.iconButton5.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.iconButton5.Size = new System.Drawing.Size(256, 82);
            this.iconButton5.TabIndex = 42;
            this.iconButton5.Text = "Reporte Ventas";
            this.iconButton5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton5.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fechaDigitacionOrdenDataGridViewTextBoxColumn,
            this.usuarioCreoOrdenDataGridViewTextBoxColumn,
            this.vendedorDataGridViewTextBoxColumn,
            this.nomSubcanalDataGridViewTextBoxColumn,
            this.nomActividadDataGridViewTextBoxColumn,
            this.tipoActividadDataGridViewTextBoxColumn,
            this.razonServicioDataGridViewTextBoxColumn,
            this.grupoActivacionOrdenDataGridViewTextBoxColumn,
            this.nomFacturaDataGridViewTextBoxColumn,
            this.cedulaDataGridViewTextBoxColumn,
            this.pasaporteDataGridViewTextBoxColumn,
            this.telefonoDataGridViewTextBoxColumn,
            this.simDataGridViewTextBoxColumn,
            this.nomPlanDataGridViewTextBoxColumn,
            this.telContactoDataGridViewTextBoxColumn,
            this.telContacto2DataGridViewTextBoxColumn,
            this.montoOrdenDataGridViewTextBoxColumn,
            this.montoOrdenConImpuestosDataGridViewTextBoxColumn,
            this.estaFirmadoDataGridViewTextBoxColumn,
            this.estadoTransaccionDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.ventaBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView1.Location = new System.Drawing.Point(3, 109);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(969, 719);
            this.dataGridView1.TabIndex = 53;
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            // 
            // fechaDigitacionOrdenDataGridViewTextBoxColumn
            // 
            this.fechaDigitacionOrdenDataGridViewTextBoxColumn.DataPropertyName = "FechaDigitacionOrden";
            this.fechaDigitacionOrdenDataGridViewTextBoxColumn.HeaderText = "Fecha";
            this.fechaDigitacionOrdenDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.fechaDigitacionOrdenDataGridViewTextBoxColumn.Name = "fechaDigitacionOrdenDataGridViewTextBoxColumn";
            this.fechaDigitacionOrdenDataGridViewTextBoxColumn.Width = 75;
            // 
            // usuarioCreoOrdenDataGridViewTextBoxColumn
            // 
            this.usuarioCreoOrdenDataGridViewTextBoxColumn.DataPropertyName = "UsuarioCreoOrden";
            this.usuarioCreoOrdenDataGridViewTextBoxColumn.HeaderText = "Usuario";
            this.usuarioCreoOrdenDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.usuarioCreoOrdenDataGridViewTextBoxColumn.Name = "usuarioCreoOrdenDataGridViewTextBoxColumn";
            this.usuarioCreoOrdenDataGridViewTextBoxColumn.Width = 82;
            // 
            // vendedorDataGridViewTextBoxColumn
            // 
            this.vendedorDataGridViewTextBoxColumn.DataPropertyName = "Vendedor";
            this.vendedorDataGridViewTextBoxColumn.HeaderText = "Vendedor";
            this.vendedorDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.vendedorDataGridViewTextBoxColumn.Name = "vendedorDataGridViewTextBoxColumn";
            // 
            // nomSubcanalDataGridViewTextBoxColumn
            // 
            this.nomSubcanalDataGridViewTextBoxColumn.DataPropertyName = "NomSubcanal";
            this.nomSubcanalDataGridViewTextBoxColumn.HeaderText = "Tienda";
            this.nomSubcanalDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nomSubcanalDataGridViewTextBoxColumn.Name = "nomSubcanalDataGridViewTextBoxColumn";
            this.nomSubcanalDataGridViewTextBoxColumn.Width = 78;
            // 
            // nomActividadDataGridViewTextBoxColumn
            // 
            this.nomActividadDataGridViewTextBoxColumn.DataPropertyName = "NomActividad";
            this.nomActividadDataGridViewTextBoxColumn.HeaderText = "Actividad";
            this.nomActividadDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nomActividadDataGridViewTextBoxColumn.Name = "nomActividadDataGridViewTextBoxColumn";
            this.nomActividadDataGridViewTextBoxColumn.Width = 98;
            // 
            // tipoActividadDataGridViewTextBoxColumn
            // 
            this.tipoActividadDataGridViewTextBoxColumn.DataPropertyName = "TipoActividad";
            this.tipoActividadDataGridViewTextBoxColumn.HeaderText = "Tipo Actividad";
            this.tipoActividadDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.tipoActividadDataGridViewTextBoxColumn.Name = "tipoActividadDataGridViewTextBoxColumn";
            this.tipoActividadDataGridViewTextBoxColumn.Width = 119;
            // 
            // razonServicioDataGridViewTextBoxColumn
            // 
            this.razonServicioDataGridViewTextBoxColumn.DataPropertyName = "RazonServicio";
            this.razonServicioDataGridViewTextBoxColumn.HeaderText = "Razon Servicio";
            this.razonServicioDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.razonServicioDataGridViewTextBoxColumn.Name = "razonServicioDataGridViewTextBoxColumn";
            this.razonServicioDataGridViewTextBoxColumn.Width = 119;
            // 
            // grupoActivacionOrdenDataGridViewTextBoxColumn
            // 
            this.grupoActivacionOrdenDataGridViewTextBoxColumn.DataPropertyName = "GrupoActivacionOrden";
            this.grupoActivacionOrdenDataGridViewTextBoxColumn.HeaderText = "Grupo Activacion";
            this.grupoActivacionOrdenDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.grupoActivacionOrdenDataGridViewTextBoxColumn.Name = "grupoActivacionOrdenDataGridViewTextBoxColumn";
            this.grupoActivacionOrdenDataGridViewTextBoxColumn.Width = 139;
            // 
            // nomFacturaDataGridViewTextBoxColumn
            // 
            this.nomFacturaDataGridViewTextBoxColumn.DataPropertyName = "NomFactura";
            this.nomFacturaDataGridViewTextBoxColumn.HeaderText = "Nombre";
            this.nomFacturaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nomFacturaDataGridViewTextBoxColumn.Name = "nomFacturaDataGridViewTextBoxColumn";
            this.nomFacturaDataGridViewTextBoxColumn.Width = 88;
            // 
            // cedulaDataGridViewTextBoxColumn
            // 
            this.cedulaDataGridViewTextBoxColumn.DataPropertyName = "Cedula";
            this.cedulaDataGridViewTextBoxColumn.HeaderText = "Cedula";
            this.cedulaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.cedulaDataGridViewTextBoxColumn.Name = "cedulaDataGridViewTextBoxColumn";
            this.cedulaDataGridViewTextBoxColumn.Width = 83;
            // 
            // pasaporteDataGridViewTextBoxColumn
            // 
            this.pasaporteDataGridViewTextBoxColumn.DataPropertyName = "Pasaporte";
            this.pasaporteDataGridViewTextBoxColumn.HeaderText = "Pasaporte";
            this.pasaporteDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.pasaporteDataGridViewTextBoxColumn.Name = "pasaporteDataGridViewTextBoxColumn";
            // 
            // telefonoDataGridViewTextBoxColumn
            // 
            this.telefonoDataGridViewTextBoxColumn.DataPropertyName = "Telefono";
            this.telefonoDataGridViewTextBoxColumn.HeaderText = "Numero Servicio";
            this.telefonoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.telefonoDataGridViewTextBoxColumn.Name = "telefonoDataGridViewTextBoxColumn";
            this.telefonoDataGridViewTextBoxColumn.Width = 132;
            // 
            // simDataGridViewTextBoxColumn
            // 
            this.simDataGridViewTextBoxColumn.DataPropertyName = "Sim";
            this.simDataGridViewTextBoxColumn.HeaderText = "Sim";
            this.simDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.simDataGridViewTextBoxColumn.Name = "simDataGridViewTextBoxColumn";
            this.simDataGridViewTextBoxColumn.Width = 58;
            // 
            // nomPlanDataGridViewTextBoxColumn
            // 
            this.nomPlanDataGridViewTextBoxColumn.DataPropertyName = "NomPlan";
            this.nomPlanDataGridViewTextBoxColumn.HeaderText = "Nombre Plan";
            this.nomPlanDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nomPlanDataGridViewTextBoxColumn.Name = "nomPlanDataGridViewTextBoxColumn";
            this.nomPlanDataGridViewTextBoxColumn.Width = 111;
            // 
            // telContactoDataGridViewTextBoxColumn
            // 
            this.telContactoDataGridViewTextBoxColumn.DataPropertyName = "TelContacto";
            this.telContactoDataGridViewTextBoxColumn.HeaderText = "TelContacto";
            this.telContactoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.telContactoDataGridViewTextBoxColumn.Name = "telContactoDataGridViewTextBoxColumn";
            this.telContactoDataGridViewTextBoxColumn.Width = 115;
            // 
            // telContacto2DataGridViewTextBoxColumn
            // 
            this.telContacto2DataGridViewTextBoxColumn.DataPropertyName = "TelContacto2";
            this.telContacto2DataGridViewTextBoxColumn.HeaderText = "TelContacto2";
            this.telContacto2DataGridViewTextBoxColumn.MinimumWidth = 6;
            this.telContacto2DataGridViewTextBoxColumn.Name = "telContacto2DataGridViewTextBoxColumn";
            this.telContacto2DataGridViewTextBoxColumn.Width = 123;
            // 
            // montoOrdenDataGridViewTextBoxColumn
            // 
            this.montoOrdenDataGridViewTextBoxColumn.DataPropertyName = "MontoOrden";
            this.montoOrdenDataGridViewTextBoxColumn.HeaderText = "Monto";
            this.montoOrdenDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.montoOrdenDataGridViewTextBoxColumn.Name = "montoOrdenDataGridViewTextBoxColumn";
            this.montoOrdenDataGridViewTextBoxColumn.Width = 76;
            // 
            // montoOrdenConImpuestosDataGridViewTextBoxColumn
            // 
            this.montoOrdenConImpuestosDataGridViewTextBoxColumn.DataPropertyName = "MontoOrdenConImpuestos";
            this.montoOrdenConImpuestosDataGridViewTextBoxColumn.HeaderText = "Monto Con Impuestos";
            this.montoOrdenConImpuestosDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.montoOrdenConImpuestosDataGridViewTextBoxColumn.Name = "montoOrdenConImpuestosDataGridViewTextBoxColumn";
            this.montoOrdenConImpuestosDataGridViewTextBoxColumn.Width = 165;
            // 
            // estaFirmadoDataGridViewTextBoxColumn
            // 
            this.estaFirmadoDataGridViewTextBoxColumn.DataPropertyName = "EstaFirmado";
            this.estaFirmadoDataGridViewTextBoxColumn.HeaderText = "EstaFirmado";
            this.estaFirmadoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.estaFirmadoDataGridViewTextBoxColumn.Name = "estaFirmadoDataGridViewTextBoxColumn";
            this.estaFirmadoDataGridViewTextBoxColumn.Width = 115;
            // 
            // estadoTransaccionDataGridViewTextBoxColumn
            // 
            this.estadoTransaccionDataGridViewTextBoxColumn.DataPropertyName = "EstadoTransaccion";
            this.estadoTransaccionDataGridViewTextBoxColumn.HeaderText = "EstadoTransaccion";
            this.estadoTransaccionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.estadoTransaccionDataGridViewTextBoxColumn.Name = "estadoTransaccionDataGridViewTextBoxColumn";
            this.estadoTransaccionDataGridViewTextBoxColumn.Width = 158;
            // 
            // ventaBindingSource
            // 
            this.ventaBindingSource.DataSource = typeof(MatcheoAltice.Estafeta.Venta);
            // 
            // VentasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 837);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "VentasForm";
            this.Text = "EstafetaForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ventaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton btnPay;
        private FontAwesome.Sharp.IconButton btnExportar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton iconButton6;
        private System.Windows.Forms.TextBox textBox1;
        private FontAwesome.Sharp.IconButton iconButton5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn companiaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn transactionIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingSource ventaBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaDigitacionOrdenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuarioCreoOrdenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendedorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomSubcanalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomActividadDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoActividadDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn razonServicioDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn grupoActivacionOrdenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomFacturaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cedulaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pasaporteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn telefonoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn simDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomPlanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn telContactoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn telContacto2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn montoOrdenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn montoOrdenConImpuestosDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn estaFirmadoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn estadoTransaccionDataGridViewTextBoxColumn;
    }
}