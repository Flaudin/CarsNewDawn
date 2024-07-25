namespace CARS.Components.Masterfiles
{
    partial class frm_parts
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PnlMain = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DataGridPart = new System.Windows.Forms.DataGridView();
            this.PartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OtherName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sku = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UomName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrandName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartApply = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ListPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PnlHeaderTable = new System.Windows.Forms.Panel();
            this.LblTable = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.BtnAdd = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnPrint = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnEdit = new CARS.Customized_Components.CustomRoundedButton();
            this.PnlFilter = new System.Windows.Forms.Panel();
            this.TxtApplicationFilter = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label25 = new System.Windows.Forms.Label();
            this.TxtSkuFiIter = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label19 = new System.Windows.Forms.Label();
            this.ComboDescription = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.ComboUomFilter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel19 = new System.Windows.Forms.Panel();
            this.ComboBrandFilter = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.PnlHeaderFilter = new System.Windows.Forms.Panel();
            this.LblFilter = new System.Windows.Forms.Label();
            this.TxtOtherNameFilter = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label13 = new System.Windows.Forms.Label();
            this.TxtPartNameFilter = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label17 = new System.Windows.Forms.Label();
            this.TxtPartNoFilter = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label18 = new System.Windows.Forms.Label();
            this.BtnClear = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnSearch = new CARS.Customized_Components.CustomRoundedButton();
            this.LblHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnEditNew = new CARS.Customized_Components.CustomCloseButton();
            this.BtnAddNew = new CARS.Customized_Components.CustomCloseButton();
            this.BtnClose = new CARS.Customized_Components.CustomCloseButton();
            this.panel8 = new System.Windows.Forms.Panel();
            this.PnlDesign = new System.Windows.Forms.Panel();
            this.PnlMain.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridPart)).BeginInit();
            this.PnlHeaderTable.SuspendLayout();
            this.panel5.SuspendLayout();
            this.PnlFilter.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel19.SuspendLayout();
            this.PnlHeaderFilter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlMain
            // 
            this.PnlMain.Controls.Add(this.panel3);
            this.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMain.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlMain.Location = new System.Drawing.Point(358, 32);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Padding = new System.Windows.Forms.Padding(10, 10, 10, 20);
            this.PnlMain.Size = new System.Drawing.Size(609, 885);
            this.PnlMain.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.DataGridPart);
            this.panel3.Controls.Add(this.PnlHeaderTable);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(10, 10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(589, 855);
            this.panel3.TabIndex = 0;
            // 
            // DataGridPart
            // 
            this.DataGridPart.AllowUserToAddRows = false;
            this.DataGridPart.AllowUserToDeleteRows = false;
            this.DataGridPart.AllowUserToResizeColumns = false;
            this.DataGridPart.AllowUserToResizeRows = false;
            this.DataGridPart.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.DataGridPart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridPart.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridPart.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridPart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridPart.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PartNo,
            this.PartName,
            this.OtherName,
            this.DescName,
            this.Sku,
            this.UomName,
            this.BrandName,
            this.OemNo,
            this.PartApply,
            this.ListPrice,
            this.IsActive});
            this.DataGridPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridPart.Location = new System.Drawing.Point(0, 32);
            this.DataGridPart.MultiSelect = false;
            this.DataGridPart.Name = "DataGridPart";
            this.DataGridPart.ReadOnly = true;
            this.DataGridPart.RowHeadersVisible = false;
            this.DataGridPart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridPart.Size = new System.Drawing.Size(587, 821);
            this.DataGridPart.TabIndex = 5;
            this.DataGridPart.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridPart_CellDoubleClick);
            this.DataGridPart.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridPart_ColumnHeaderMouseClick);
            // 
            // PartNo
            // 
            this.PartNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PartNo.DataPropertyName = "PartNo";
            this.PartNo.FillWeight = 50F;
            this.PartNo.HeaderText = "Part No.";
            this.PartNo.Name = "PartNo";
            this.PartNo.ReadOnly = true;
            // 
            // PartName
            // 
            this.PartName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PartName.DataPropertyName = "PartName";
            this.PartName.HeaderText = "Part Name";
            this.PartName.Name = "PartName";
            this.PartName.ReadOnly = true;
            // 
            // OtherName
            // 
            this.OtherName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OtherName.DataPropertyName = "OtherName";
            this.OtherName.HeaderText = "Other Name";
            this.OtherName.Name = "OtherName";
            this.OtherName.ReadOnly = true;
            // 
            // DescName
            // 
            this.DescName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DescName.DataPropertyName = "DescName";
            this.DescName.HeaderText = "Description";
            this.DescName.Name = "DescName";
            this.DescName.ReadOnly = true;
            // 
            // Sku
            // 
            this.Sku.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Sku.DataPropertyName = "Sku";
            this.Sku.FillWeight = 80F;
            this.Sku.HeaderText = "Stock Keeping Unit";
            this.Sku.Name = "Sku";
            this.Sku.ReadOnly = true;
            // 
            // UomName
            // 
            this.UomName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UomName.DataPropertyName = "UomName";
            this.UomName.FillWeight = 60F;
            this.UomName.HeaderText = "UOM";
            this.UomName.Name = "UomName";
            this.UomName.ReadOnly = true;
            // 
            // BrandName
            // 
            this.BrandName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BrandName.DataPropertyName = "BrandName";
            this.BrandName.FillWeight = 60F;
            this.BrandName.HeaderText = "Brand";
            this.BrandName.Name = "BrandName";
            this.BrandName.ReadOnly = true;
            // 
            // OemNo
            // 
            this.OemNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OemNo.DataPropertyName = "OemNo";
            this.OemNo.HeaderText = "OEM";
            this.OemNo.Name = "OemNo";
            this.OemNo.ReadOnly = true;
            this.OemNo.Visible = false;
            // 
            // PartApply
            // 
            this.PartApply.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PartApply.DataPropertyName = "PartApply";
            this.PartApply.HeaderText = "Application";
            this.PartApply.Name = "PartApply";
            this.PartApply.ReadOnly = true;
            // 
            // ListPrice
            // 
            this.ListPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ListPrice.DataPropertyName = "ListPrice";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0.00";
            this.ListPrice.DefaultCellStyle = dataGridViewCellStyle2;
            this.ListPrice.FillWeight = 60F;
            this.ListPrice.HeaderText = "List Price";
            this.ListPrice.Name = "ListPrice";
            this.ListPrice.ReadOnly = true;
            // 
            // IsActive
            // 
            this.IsActive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IsActive.DataPropertyName = "IsActive";
            this.IsActive.FillWeight = 30F;
            this.IsActive.HeaderText = "Active";
            this.IsActive.Name = "IsActive";
            this.IsActive.ReadOnly = true;
            this.IsActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // PnlHeaderTable
            // 
            this.PnlHeaderTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeaderTable.Controls.Add(this.LblTable);
            this.PnlHeaderTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlHeaderTable.Location = new System.Drawing.Point(0, 0);
            this.PnlHeaderTable.Name = "PnlHeaderTable";
            this.PnlHeaderTable.Size = new System.Drawing.Size(587, 32);
            this.PnlHeaderTable.TabIndex = 6;
            // 
            // LblTable
            // 
            this.LblTable.AutoSize = true;
            this.LblTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblTable.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblTable.Location = new System.Drawing.Point(5, 6);
            this.LblTable.Name = "LblTable";
            this.LblTable.Size = new System.Drawing.Size(69, 19);
            this.LblTable.TabIndex = 0;
            this.LblTable.Text = "Parts List";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.BtnAdd);
            this.panel5.Controls.Add(this.BtnPrint);
            this.panel5.Controls.Add(this.BtnEdit);
            this.panel5.Controls.Add(this.PnlFilter);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(0, 32);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
            this.panel5.Size = new System.Drawing.Size(358, 885);
            this.panel5.TabIndex = 10;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnAdd.BackColor = System.Drawing.Color.MidnightBlue;
            this.BtnAdd.BorderColor = System.Drawing.Color.White;
            this.BtnAdd.BorderRadius = 8;
            this.BtnAdd.BorderSize = 0;
            this.BtnAdd.FlatAppearance.BorderSize = 0;
            this.BtnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.ForeColor = System.Drawing.Color.White;
            this.BtnAdd.Location = new System.Drawing.Point(10, 845);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(95, 30);
            this.BtnAdd.TabIndex = 46;
            this.BtnAdd.TabStop = false;
            this.BtnAdd.Text = "&Add";
            this.BtnAdd.UseVisualStyleBackColor = false;
            this.BtnAdd.Visible = false;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnPrint.BackColor = System.Drawing.Color.MidnightBlue;
            this.BtnPrint.BorderColor = System.Drawing.Color.White;
            this.BtnPrint.BorderRadius = 8;
            this.BtnPrint.BorderSize = 0;
            this.BtnPrint.FlatAppearance.BorderSize = 0;
            this.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPrint.ForeColor = System.Drawing.Color.White;
            this.BtnPrint.Location = new System.Drawing.Point(212, 845);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(95, 30);
            this.BtnPrint.TabIndex = 48;
            this.BtnPrint.TabStop = false;
            this.BtnPrint.Text = "&Print";
            this.BtnPrint.UseVisualStyleBackColor = false;
            this.BtnPrint.Visible = false;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnEdit.BackColor = System.Drawing.Color.MidnightBlue;
            this.BtnEdit.BorderColor = System.Drawing.Color.White;
            this.BtnEdit.BorderRadius = 8;
            this.BtnEdit.BorderSize = 0;
            this.BtnEdit.FlatAppearance.BorderSize = 0;
            this.BtnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.ForeColor = System.Drawing.Color.White;
            this.BtnEdit.Image = global::CARS.Properties.Resources.Edit;
            this.BtnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnEdit.Location = new System.Drawing.Point(111, 845);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(95, 30);
            this.BtnEdit.TabIndex = 47;
            this.BtnEdit.TabStop = false;
            this.BtnEdit.Text = "E&dit";
            this.BtnEdit.UseVisualStyleBackColor = false;
            this.BtnEdit.Visible = false;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // PnlFilter
            // 
            this.PnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlFilter.Controls.Add(this.TxtApplicationFilter);
            this.PnlFilter.Controls.Add(this.label25);
            this.PnlFilter.Controls.Add(this.TxtSkuFiIter);
            this.PnlFilter.Controls.Add(this.label19);
            this.PnlFilter.Controls.Add(this.ComboDescription);
            this.PnlFilter.Controls.Add(this.label2);
            this.PnlFilter.Controls.Add(this.tableLayoutPanel3);
            this.PnlFilter.Controls.Add(this.PnlHeaderFilter);
            this.PnlFilter.Controls.Add(this.TxtOtherNameFilter);
            this.PnlFilter.Controls.Add(this.label13);
            this.PnlFilter.Controls.Add(this.TxtPartNameFilter);
            this.PnlFilter.Controls.Add(this.label17);
            this.PnlFilter.Controls.Add(this.TxtPartNoFilter);
            this.PnlFilter.Controls.Add(this.label18);
            this.PnlFilter.Controls.Add(this.BtnClear);
            this.PnlFilter.Controls.Add(this.BtnSearch);
            this.PnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlFilter.Location = new System.Drawing.Point(10, 10);
            this.PnlFilter.Name = "PnlFilter";
            this.PnlFilter.Padding = new System.Windows.Forms.Padding(5, 35, 5, 5);
            this.PnlFilter.Size = new System.Drawing.Size(348, 459);
            this.PnlFilter.TabIndex = 44;
            // 
            // TxtApplicationFilter
            // 
            this.TxtApplicationFilter.BackColor = System.Drawing.SystemColors.Window;
            this.TxtApplicationFilter.BorderColor = System.Drawing.Color.Silver;
            this.TxtApplicationFilter.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtApplicationFilter.BorderRadius = 8;
            this.TxtApplicationFilter.BorderSize = 1;
            this.TxtApplicationFilter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtApplicationFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtApplicationFilter.Location = new System.Drawing.Point(5, 368);
            this.TxtApplicationFilter.MaxLegnth = 200;
            this.TxtApplicationFilter.MultiLine = false;
            this.TxtApplicationFilter.Name = "TxtApplicationFilter";
            this.TxtApplicationFilter.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtApplicationFilter.PasswordChar = false;
            this.TxtApplicationFilter.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtApplicationFilter.ReadOnly = false;
            this.TxtApplicationFilter.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtApplicationFilter.Size = new System.Drawing.Size(336, 32);
            this.TxtApplicationFilter.TabIndex = 6;
            this.TxtApplicationFilter.Textt = "";
            this.TxtApplicationFilter.UnderlinedStyle = false;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Dock = System.Windows.Forms.DockStyle.Top;
            this.label25.Location = new System.Drawing.Point(5, 348);
            this.label25.Name = "label25";
            this.label25.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label25.Size = new System.Drawing.Size(68, 20);
            this.label25.TabIndex = 59;
            this.label25.Text = "Application";
            // 
            // TxtSkuFiIter
            // 
            this.TxtSkuFiIter.BackColor = System.Drawing.SystemColors.Window;
            this.TxtSkuFiIter.BorderColor = System.Drawing.Color.Silver;
            this.TxtSkuFiIter.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtSkuFiIter.BorderRadius = 8;
            this.TxtSkuFiIter.BorderSize = 1;
            this.TxtSkuFiIter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtSkuFiIter.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtSkuFiIter.Location = new System.Drawing.Point(5, 316);
            this.TxtSkuFiIter.MaxLegnth = 50;
            this.TxtSkuFiIter.MultiLine = false;
            this.TxtSkuFiIter.Name = "TxtSkuFiIter";
            this.TxtSkuFiIter.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtSkuFiIter.PasswordChar = false;
            this.TxtSkuFiIter.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtSkuFiIter.ReadOnly = false;
            this.TxtSkuFiIter.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtSkuFiIter.Size = new System.Drawing.Size(336, 32);
            this.TxtSkuFiIter.TabIndex = 5;
            this.TxtSkuFiIter.Textt = "";
            this.TxtSkuFiIter.UnderlinedStyle = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Location = new System.Drawing.Point(5, 296);
            this.label19.Name = "label19";
            this.label19.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label19.Size = new System.Drawing.Size(107, 20);
            this.label19.TabIndex = 71;
            this.label19.Text = "Stock Keeping Unit";
            // 
            // ComboDescription
            // 
            this.ComboDescription.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ComboDescription.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComboDescription.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ComboDescription.FormattingEnabled = true;
            this.ComboDescription.Location = new System.Drawing.Point(5, 271);
            this.ComboDescription.Name = "ComboDescription";
            this.ComboDescription.Size = new System.Drawing.Size(336, 25);
            this.ComboDescription.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(5, 251);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 88;
            this.label2.Text = "Description";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.panel9, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel19, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(5, 191);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(336, 60);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.ComboUomFilter);
            this.panel9.Controls.Add(this.label1);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(171, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(162, 54);
            this.panel9.TabIndex = 1;
            // 
            // ComboUomFilter
            // 
            this.ComboUomFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ComboUomFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboUomFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComboUomFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboUomFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ComboUomFilter.FormattingEnabled = true;
            this.ComboUomFilter.Location = new System.Drawing.Point(0, 20);
            this.ComboUomFilter.Name = "ComboUomFilter";
            this.ComboUomFilter.Size = new System.Drawing.Size(162, 25);
            this.ComboUomFilter.TabIndex = 87;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 71;
            this.label1.Text = "Unit of Measurement";
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.ComboBrandFilter);
            this.panel19.Controls.Add(this.label22);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel19.Location = new System.Drawing.Point(3, 3);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(162, 54);
            this.panel19.TabIndex = 0;
            // 
            // ComboBrandFilter
            // 
            this.ComboBrandFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ComboBrandFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboBrandFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComboBrandFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBrandFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ComboBrandFilter.FormattingEnabled = true;
            this.ComboBrandFilter.Location = new System.Drawing.Point(0, 20);
            this.ComboBrandFilter.Name = "ComboBrandFilter";
            this.ComboBrandFilter.Size = new System.Drawing.Size(162, 25);
            this.ComboBrandFilter.TabIndex = 86;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.Location = new System.Drawing.Point(0, 0);
            this.label22.Name = "label22";
            this.label22.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label22.Size = new System.Drawing.Size(38, 20);
            this.label22.TabIndex = 63;
            this.label22.Text = "Brand";
            // 
            // PnlHeaderFilter
            // 
            this.PnlHeaderFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlHeaderFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeaderFilter.Controls.Add(this.LblFilter);
            this.PnlHeaderFilter.Location = new System.Drawing.Point(-1, -1);
            this.PnlHeaderFilter.Name = "PnlHeaderFilter";
            this.PnlHeaderFilter.Size = new System.Drawing.Size(348, 32);
            this.PnlHeaderFilter.TabIndex = 45;
            // 
            // LblFilter
            // 
            this.LblFilter.AutoSize = true;
            this.LblFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblFilter.Location = new System.Drawing.Point(5, 6);
            this.LblFilter.Name = "LblFilter";
            this.LblFilter.Size = new System.Drawing.Size(43, 19);
            this.LblFilter.TabIndex = 0;
            this.LblFilter.Text = "Filter";
            // 
            // TxtOtherNameFilter
            // 
            this.TxtOtherNameFilter.BackColor = System.Drawing.SystemColors.Window;
            this.TxtOtherNameFilter.BorderColor = System.Drawing.Color.Silver;
            this.TxtOtherNameFilter.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtOtherNameFilter.BorderRadius = 8;
            this.TxtOtherNameFilter.BorderSize = 1;
            this.TxtOtherNameFilter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtOtherNameFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtOtherNameFilter.Location = new System.Drawing.Point(5, 159);
            this.TxtOtherNameFilter.MaxLegnth = 100;
            this.TxtOtherNameFilter.MultiLine = false;
            this.TxtOtherNameFilter.Name = "TxtOtherNameFilter";
            this.TxtOtherNameFilter.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtOtherNameFilter.PasswordChar = false;
            this.TxtOtherNameFilter.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtOtherNameFilter.ReadOnly = false;
            this.TxtOtherNameFilter.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtOtherNameFilter.Size = new System.Drawing.Size(336, 32);
            this.TxtOtherNameFilter.TabIndex = 2;
            this.TxtOtherNameFilter.Textt = "";
            this.TxtOtherNameFilter.UnderlinedStyle = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(5, 139);
            this.label13.Name = "label13";
            this.label13.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label13.Size = new System.Drawing.Size(72, 20);
            this.label13.TabIndex = 44;
            this.label13.Text = "Other Name";
            // 
            // TxtPartNameFilter
            // 
            this.TxtPartNameFilter.BackColor = System.Drawing.SystemColors.Window;
            this.TxtPartNameFilter.BorderColor = System.Drawing.Color.Silver;
            this.TxtPartNameFilter.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtPartNameFilter.BorderRadius = 8;
            this.TxtPartNameFilter.BorderSize = 1;
            this.TxtPartNameFilter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtPartNameFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtPartNameFilter.Location = new System.Drawing.Point(5, 107);
            this.TxtPartNameFilter.MaxLegnth = 110;
            this.TxtPartNameFilter.MultiLine = false;
            this.TxtPartNameFilter.Name = "TxtPartNameFilter";
            this.TxtPartNameFilter.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtPartNameFilter.PasswordChar = false;
            this.TxtPartNameFilter.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtPartNameFilter.ReadOnly = false;
            this.TxtPartNameFilter.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtPartNameFilter.Size = new System.Drawing.Size(336, 32);
            this.TxtPartNameFilter.TabIndex = 1;
            this.TxtPartNameFilter.Textt = "";
            this.TxtPartNameFilter.UnderlinedStyle = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Location = new System.Drawing.Point(5, 87);
            this.label17.Name = "label17";
            this.label17.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label17.Size = new System.Drawing.Size(63, 20);
            this.label17.TabIndex = 40;
            this.label17.Text = "Part Name";
            // 
            // TxtPartNoFilter
            // 
            this.TxtPartNoFilter.BackColor = System.Drawing.SystemColors.Window;
            this.TxtPartNoFilter.BorderColor = System.Drawing.Color.Silver;
            this.TxtPartNoFilter.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtPartNoFilter.BorderRadius = 8;
            this.TxtPartNoFilter.BorderSize = 1;
            this.TxtPartNoFilter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtPartNoFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtPartNoFilter.Location = new System.Drawing.Point(5, 55);
            this.TxtPartNoFilter.MaxLegnth = 10;
            this.TxtPartNoFilter.MultiLine = false;
            this.TxtPartNoFilter.Name = "TxtPartNoFilter";
            this.TxtPartNoFilter.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtPartNoFilter.PasswordChar = false;
            this.TxtPartNoFilter.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtPartNoFilter.ReadOnly = false;
            this.TxtPartNoFilter.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtPartNoFilter.Size = new System.Drawing.Size(336, 32);
            this.TxtPartNoFilter.TabIndex = 0;
            this.TxtPartNoFilter.Textt = "";
            this.TxtPartNoFilter.UnderlinedStyle = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(5, 35);
            this.label18.Name = "label18";
            this.label18.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label18.Size = new System.Drawing.Size(50, 20);
            this.label18.TabIndex = 39;
            this.label18.Text = "Part No.";
            // 
            // BtnClear
            // 
            this.BtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClear.BackColor = System.Drawing.Color.MidnightBlue;
            this.BtnClear.BorderColor = System.Drawing.Color.White;
            this.BtnClear.BorderRadius = 8;
            this.BtnClear.BorderSize = 0;
            this.BtnClear.FlatAppearance.BorderSize = 0;
            this.BtnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClear.ForeColor = System.Drawing.Color.White;
            this.BtnClear.Image = global::CARS.Properties.Resources.Delete;
            this.BtnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnClear.Location = new System.Drawing.Point(246, 422);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(95, 30);
            this.BtnClear.TabIndex = 32;
            this.BtnClear.TabStop = false;
            this.BtnClear.Text = "&Clear";
            this.BtnClear.UseVisualStyleBackColor = false;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSearch.BackColor = System.Drawing.Color.MidnightBlue;
            this.BtnSearch.BorderColor = System.Drawing.Color.White;
            this.BtnSearch.BorderRadius = 8;
            this.BtnSearch.BorderSize = 0;
            this.BtnSearch.FlatAppearance.BorderSize = 0;
            this.BtnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.ForeColor = System.Drawing.Color.White;
            this.BtnSearch.Image = global::CARS.Properties.Resources.Search;
            this.BtnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnSearch.Location = new System.Drawing.Point(145, 422);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(95, 30);
            this.BtnSearch.TabIndex = 33;
            this.BtnSearch.TabStop = false;
            this.BtnSearch.Text = "S&earch";
            this.BtnSearch.UseVisualStyleBackColor = false;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // LblHeader
            // 
            this.LblHeader.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LblHeader.AutoSize = true;
            this.LblHeader.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblHeader.Location = new System.Drawing.Point(0, 5);
            this.LblHeader.Name = "LblHeader";
            this.LblHeader.Size = new System.Drawing.Size(172, 25);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "  PARTS LIBRARY  ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.BtnEditNew);
            this.panel1.Controls.Add(this.BtnAddNew);
            this.panel1.Controls.Add(this.BtnClose);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.LblHeader);
            this.panel1.Controls.Add(this.PnlDesign);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(967, 32);
            this.panel1.TabIndex = 8;
            // 
            // BtnEditNew
            // 
            this.BtnEditNew.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnEditNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnEditNew.BorderRadius = 0;
            this.BtnEditNew.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnEditNew.FlatAppearance.BorderSize = 0;
            this.BtnEditNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(83)))), ((int)(((byte)(116)))));
            this.BtnEditNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEditNew.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEditNew.ForeColor = System.Drawing.SystemColors.Control;
            this.BtnEditNew.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(83)))), ((int)(((byte)(116)))));
            this.BtnEditNew.Image = global::CARS.Properties.Resources.Edit;
            this.BtnEditNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnEditNew.Location = new System.Drawing.Point(824, 3);
            this.BtnEditNew.Name = "BtnEditNew";
            this.BtnEditNew.Size = new System.Drawing.Size(95, 29);
            this.BtnEditNew.TabIndex = 56;
            this.BtnEditNew.Text = "&Edit";
            this.BtnEditNew.UseVisualStyleBackColor = false;
            this.BtnEditNew.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnAddNew
            // 
            this.BtnAddNew.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnAddNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnAddNew.BorderRadius = 0;
            this.BtnAddNew.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnAddNew.FlatAppearance.BorderSize = 0;
            this.BtnAddNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(83)))), ((int)(((byte)(116)))));
            this.BtnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddNew.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAddNew.ForeColor = System.Drawing.SystemColors.Control;
            this.BtnAddNew.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(83)))), ((int)(((byte)(116)))));
            this.BtnAddNew.Location = new System.Drawing.Point(723, 3);
            this.BtnAddNew.Name = "BtnAddNew";
            this.BtnAddNew.Size = new System.Drawing.Size(95, 29);
            this.BtnAddNew.TabIndex = 55;
            this.BtnAddNew.Text = "&New";
            this.BtnAddNew.UseVisualStyleBackColor = false;
            this.BtnAddNew.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnClose.BorderRadius = 0;
            this.BtnClose.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnClose.FlatAppearance.BorderSize = 0;
            this.BtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(83)))), ((int)(((byte)(116)))));
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Font = new System.Drawing.Font("Segoe UI Black", 11.25F, System.Drawing.FontStyle.Bold);
            this.BtnClose.ForeColor = System.Drawing.SystemColors.Control;
            this.BtnClose.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(83)))), ((int)(((byte)(116)))));
            this.BtnClose.Location = new System.Drawing.Point(925, 3);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(33, 29);
            this.BtnClose.TabIndex = 54;
            this.BtnClose.Text = "X";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // panel8
            // 
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(957, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(10, 32);
            this.panel8.TabIndex = 51;
            // 
            // PnlDesign
            // 
            this.PnlDesign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlDesign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.PnlDesign.Location = new System.Drawing.Point(-8, 17);
            this.PnlDesign.Name = "PnlDesign";
            this.PnlDesign.Size = new System.Drawing.Size(720, 4);
            this.PnlDesign.TabIndex = 50;
            // 
            // frm_parts
            // 
            this.AcceptButton = this.BtnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 917);
            this.Controls.Add(this.PnlMain);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frm_parts";
            this.Text = "Parts Library";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_parts_KeyDown);
            this.PnlMain.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridPart)).EndInit();
            this.PnlHeaderTable.ResumeLayout(false);
            this.PnlHeaderTable.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.PnlFilter.ResumeLayout(false);
            this.PnlFilter.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            this.PnlHeaderFilter.ResumeLayout(false);
            this.PnlHeaderFilter.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PnlMain;
        private System.Windows.Forms.Panel panel5;
        private Customized_Components.CustomRoundedButton BtnAdd;
        private Customized_Components.CustomRoundedButton BtnEdit;
        private System.Windows.Forms.Panel PnlFilter;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private Customized_Components.CustomRoundedButton BtnClear;
        private Customized_Components.CustomRoundedButton BtnSearch;
        private System.Windows.Forms.Panel PnlHeaderFilter;
        private System.Windows.Forms.Label LblFilter;
        private Customized_Components.RifinedCustomTextbox TxtOtherNameFilter;
        private Customized_Components.RifinedCustomTextbox TxtPartNameFilter;
        private Customized_Components.RifinedCustomTextbox TxtPartNoFilter;
        private Customized_Components.CustomRoundedButton BtnPrint;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel PnlDesign;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView DataGridPart;
        private System.Windows.Forms.Panel PnlHeaderTable;
        private System.Windows.Forms.Label LblTable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel19;
        private Customized_Components.RifinedCustomTextbox TxtApplicationFilter;
        private System.Windows.Forms.Label label25;
        private Customized_Components.RifinedCustomTextbox TxtSkuFiIter;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox ComboBrandFilter;
        private System.Windows.Forms.ComboBox ComboUomFilter;
        private Customized_Components.CustomCloseButton BtnClose;
        private System.Windows.Forms.ComboBox ComboDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OtherName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sku;
        private System.Windows.Forms.DataGridViewTextBoxColumn UomName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrandName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartApply;
        private System.Windows.Forms.DataGridViewTextBoxColumn ListPrice;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private Customized_Components.CustomCloseButton BtnAddNew;
        private Customized_Components.CustomCloseButton BtnEditNew;
    }
}