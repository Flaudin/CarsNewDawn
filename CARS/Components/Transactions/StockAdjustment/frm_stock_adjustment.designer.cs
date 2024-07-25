namespace CARS.Components.Transactions.StockAdjustment
{
    partial class frm_stock_adjustment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TxtRemarks = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label4 = new System.Windows.Forms.Label();
            this.ComboReason = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.rifinedCustomTextbox2 = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label5 = new System.Windows.Forms.Label();
            this.PnlHeaderDetail = new System.Windows.Forms.Panel();
            this.LblDetail = new System.Windows.Forms.Label();
            this.BtnClear = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnArchive = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnSave = new CARS.Customized_Components.CustomRoundedButton();
            this.PnlMain = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DataGridParts = new System.Windows.Forms.DataGridView();
            this.IsSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OtherName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrandName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BinName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WhName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TakeUpQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DropQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BinID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WhID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PnlHeaderTable = new System.Windows.Forms.Panel();
            this.BtnAddParts = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnDeleteParts = new CARS.Customized_Components.CustomRoundedButton();
            this.LblTable = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnArchiveNew = new CARS.Customized_Components.CustomCloseButton();
            this.BtnClearNew = new CARS.Customized_Components.CustomCloseButton();
            this.BtnSaveNew = new CARS.Customized_Components.CustomCloseButton();
            this.BtnClose = new CARS.Customized_Components.CustomCloseButton();
            this.panel11 = new System.Windows.Forms.Panel();
            this.LblHeader = new System.Windows.Forms.Label();
            this.PnlDesign = new System.Windows.Forms.Panel();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.PnlHeaderDetail.SuspendLayout();
            this.PnlMain.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridParts)).BeginInit();
            this.PnlHeaderTable.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Controls.Add(this.BtnClear);
            this.panel5.Controls.Add(this.BtnArchive);
            this.panel5.Controls.Add(this.BtnSave);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(0, 32);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(10, 10, 0, 20);
            this.panel5.Size = new System.Drawing.Size(358, 885);
            this.panel5.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.TxtRemarks);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.ComboReason);
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Controls.Add(this.rifinedCustomTextbox2);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.PnlHeaderDetail);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5, 35, 5, 5);
            this.panel2.Size = new System.Drawing.Size(348, 468);
            this.panel2.TabIndex = 51;
            // 
            // TxtRemarks
            // 
            this.TxtRemarks.BackColor = System.Drawing.SystemColors.Window;
            this.TxtRemarks.BorderColor = System.Drawing.Color.Silver;
            this.TxtRemarks.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtRemarks.BorderRadius = 8;
            this.TxtRemarks.BorderSize = 1;
            this.TxtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtRemarks.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtRemarks.Location = new System.Drawing.Point(5, 151);
            this.TxtRemarks.MaxLegnth = 8000;
            this.TxtRemarks.MultiLine = true;
            this.TxtRemarks.Name = "TxtRemarks";
            this.TxtRemarks.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtRemarks.PasswordChar = false;
            this.TxtRemarks.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtRemarks.ReadOnly = false;
            this.TxtRemarks.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtRemarks.Size = new System.Drawing.Size(336, 304);
            this.TxtRemarks.TabIndex = 2;
            this.TxtRemarks.Textt = "";
            this.TxtRemarks.UnderlinedStyle = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(5, 131);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label4.Size = new System.Drawing.Size(52, 20);
            this.label4.TabIndex = 90;
            this.label4.Text = "Remarks";
            // 
            // ComboReason
            // 
            this.ComboReason.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ComboReason.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboReason.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComboReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboReason.FormattingEnabled = true;
            this.ComboReason.Location = new System.Drawing.Point(5, 108);
            this.ComboReason.Name = "ComboReason";
            this.ComboReason.Size = new System.Drawing.Size(336, 23);
            this.ComboReason.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.label9);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 82);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(336, 26);
            this.flowLayoutPanel1.TabIndex = 102;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(45, 20);
            this.label1.TabIndex = 83;
            this.label1.Text = "Reason";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Crimson;
            this.label9.Location = new System.Drawing.Point(45, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.MaximumSize = new System.Drawing.Size(15, 23);
            this.label9.MinimumSize = new System.Drawing.Size(15, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 23);
            this.label9.TabIndex = 55;
            this.label9.Text = "*";
            // 
            // rifinedCustomTextbox2
            // 
            this.rifinedCustomTextbox2.BackColor = System.Drawing.SystemColors.Window;
            this.rifinedCustomTextbox2.BorderColor = System.Drawing.Color.Silver;
            this.rifinedCustomTextbox2.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.rifinedCustomTextbox2.BorderRadius = 8;
            this.rifinedCustomTextbox2.BorderSize = 1;
            this.rifinedCustomTextbox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.rifinedCustomTextbox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.rifinedCustomTextbox2.Location = new System.Drawing.Point(5, 50);
            this.rifinedCustomTextbox2.MaxLegnth = 10;
            this.rifinedCustomTextbox2.MultiLine = false;
            this.rifinedCustomTextbox2.Name = "rifinedCustomTextbox2";
            this.rifinedCustomTextbox2.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.rifinedCustomTextbox2.PasswordChar = false;
            this.rifinedCustomTextbox2.PlaceholderColor = System.Drawing.Color.Gray;
            this.rifinedCustomTextbox2.ReadOnly = true;
            this.rifinedCustomTextbox2.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.rifinedCustomTextbox2.Size = new System.Drawing.Size(336, 32);
            this.rifinedCustomTextbox2.TabIndex = 0;
            this.rifinedCustomTextbox2.TabStop = false;
            this.rifinedCustomTextbox2.Textt = "JUSTIN";
            this.rifinedCustomTextbox2.UnderlinedStyle = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(5, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 15);
            this.label5.TabIndex = 99;
            this.label5.Text = "Prepared By";
            // 
            // PnlHeaderDetail
            // 
            this.PnlHeaderDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlHeaderDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeaderDetail.Controls.Add(this.LblDetail);
            this.PnlHeaderDetail.Location = new System.Drawing.Point(-1, -1);
            this.PnlHeaderDetail.Name = "PnlHeaderDetail";
            this.PnlHeaderDetail.Size = new System.Drawing.Size(348, 32);
            this.PnlHeaderDetail.TabIndex = 45;
            // 
            // LblDetail
            // 
            this.LblDetail.AutoSize = true;
            this.LblDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblDetail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblDetail.Location = new System.Drawing.Point(5, 6);
            this.LblDetail.Name = "LblDetail";
            this.LblDetail.Size = new System.Drawing.Size(54, 19);
            this.LblDetail.TabIndex = 0;
            this.LblDetail.Text = "Details";
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
            this.BtnClear.Location = new System.Drawing.Point(111, 845);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(95, 30);
            this.BtnClear.TabIndex = 48;
            this.BtnClear.TabStop = false;
            this.BtnClear.Text = "&Clear";
            this.BtnClear.UseVisualStyleBackColor = false;
            this.BtnClear.Visible = false;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnArchive
            // 
            this.BtnArchive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnArchive.BackColor = System.Drawing.Color.MidnightBlue;
            this.BtnArchive.BorderColor = System.Drawing.Color.White;
            this.BtnArchive.BorderRadius = 8;
            this.BtnArchive.BorderSize = 0;
            this.BtnArchive.FlatAppearance.BorderSize = 0;
            this.BtnArchive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnArchive.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnArchive.ForeColor = System.Drawing.Color.White;
            this.BtnArchive.Location = new System.Drawing.Point(212, 845);
            this.BtnArchive.Name = "BtnArchive";
            this.BtnArchive.Size = new System.Drawing.Size(95, 30);
            this.BtnArchive.TabIndex = 49;
            this.BtnArchive.TabStop = false;
            this.BtnArchive.Text = "&Archive";
            this.BtnArchive.UseVisualStyleBackColor = false;
            this.BtnArchive.Visible = false;
            this.BtnArchive.Click += new System.EventHandler(this.BtnArchive_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.BackColor = System.Drawing.Color.MidnightBlue;
            this.BtnSave.BorderColor = System.Drawing.Color.White;
            this.BtnSave.BorderRadius = 8;
            this.BtnSave.BorderSize = 0;
            this.BtnSave.FlatAppearance.BorderSize = 0;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.ForeColor = System.Drawing.Color.White;
            this.BtnSave.Location = new System.Drawing.Point(10, 845);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(95, 30);
            this.BtnSave.TabIndex = 50;
            this.BtnSave.TabStop = false;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Visible = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
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
            this.PnlMain.TabIndex = 24;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.DataGridParts);
            this.panel3.Controls.Add(this.PnlHeaderTable);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(10, 10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(589, 855);
            this.panel3.TabIndex = 0;
            // 
            // DataGridParts
            // 
            this.DataGridParts.AllowUserToAddRows = false;
            this.DataGridParts.AllowUserToDeleteRows = false;
            this.DataGridParts.AllowUserToResizeColumns = false;
            this.DataGridParts.AllowUserToResizeRows = false;
            this.DataGridParts.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.DataGridParts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridParts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridParts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridParts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsSelected,
            this.PartNo,
            this.PartName,
            this.OtherName,
            this.PartDesc,
            this.BrandName,
            this.BinName,
            this.WhName,
            this.LotNo,
            this.TakeUpQty,
            this.DropQty,
            this.UnitPrice,
            this.BinID,
            this.WhID});
            this.DataGridParts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridParts.Location = new System.Drawing.Point(0, 32);
            this.DataGridParts.MultiSelect = false;
            this.DataGridParts.Name = "DataGridParts";
            this.DataGridParts.RowHeadersVisible = false;
            this.DataGridParts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGridParts.Size = new System.Drawing.Size(587, 821);
            this.DataGridParts.TabIndex = 8;
            this.DataGridParts.TabStop = false;
            this.DataGridParts.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridParts_CellEndEdit);
            this.DataGridParts.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridParts_CellMouseClick);
            this.DataGridParts.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridParts_ColumnHeaderMouseClick);
            this.DataGridParts.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DataGridParts_EditingControlShowing);
            // 
            // IsSelected
            // 
            this.IsSelected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IsSelected.FillWeight = 10F;
            this.IsSelected.HeaderText = "";
            this.IsSelected.Name = "IsSelected";
            // 
            // PartNo
            // 
            this.PartNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PartNo.DataPropertyName = "PartNo";
            this.PartNo.FillWeight = 40F;
            this.PartNo.HeaderText = "Part No.";
            this.PartNo.Name = "PartNo";
            this.PartNo.ReadOnly = true;
            // 
            // PartName
            // 
            this.PartName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PartName.DataPropertyName = "PartName";
            this.PartName.FillWeight = 60F;
            this.PartName.HeaderText = "Part Name";
            this.PartName.Name = "PartName";
            this.PartName.ReadOnly = true;
            // 
            // OtherName
            // 
            this.OtherName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OtherName.DataPropertyName = "OtherName";
            this.OtherName.FillWeight = 60F;
            this.OtherName.HeaderText = "Other Name";
            this.OtherName.Name = "OtherName";
            this.OtherName.ReadOnly = true;
            // 
            // PartDesc
            // 
            this.PartDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PartDesc.FillWeight = 180F;
            this.PartDesc.HeaderText = "Description";
            this.PartDesc.Name = "PartDesc";
            this.PartDesc.ReadOnly = true;
            // 
            // BrandName
            // 
            this.BrandName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BrandName.FillWeight = 50F;
            this.BrandName.HeaderText = "Brand";
            this.BrandName.Name = "BrandName";
            this.BrandName.ReadOnly = true;
            // 
            // BinName
            // 
            this.BinName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            this.BinName.DefaultCellStyle = dataGridViewCellStyle2;
            this.BinName.FillWeight = 60F;
            this.BinName.HeaderText = "Location";
            this.BinName.Name = "BinName";
            this.BinName.ReadOnly = true;
            // 
            // WhName
            // 
            this.WhName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WhName.FillWeight = 60F;
            this.WhName.HeaderText = "Warehouse";
            this.WhName.Name = "WhName";
            this.WhName.ReadOnly = true;
            // 
            // LotNo
            // 
            this.LotNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            this.LotNo.DefaultCellStyle = dataGridViewCellStyle3;
            this.LotNo.FillWeight = 70F;
            this.LotNo.HeaderText = "Lot No.";
            this.LotNo.Name = "LotNo";
            this.LotNo.ReadOnly = true;
            // 
            // TakeUpQty
            // 
            this.TakeUpQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0.00";
            this.TakeUpQty.DefaultCellStyle = dataGridViewCellStyle4;
            this.TakeUpQty.FillWeight = 50F;
            this.TakeUpQty.HeaderText = "Take Up";
            this.TakeUpQty.MaxInputLength = 8;
            this.TakeUpQty.Name = "TakeUpQty";
            // 
            // DropQty
            // 
            this.DropQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = "0.00";
            this.DropQty.DefaultCellStyle = dataGridViewCellStyle5;
            this.DropQty.FillWeight = 50F;
            this.DropQty.HeaderText = "Drop";
            this.DropQty.MaxInputLength = 8;
            this.DropQty.Name = "DropQty";
            // 
            // UnitPrice
            // 
            this.UnitPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UnitPrice.DataPropertyName = "UnitPrice";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = "0.00";
            this.UnitPrice.DefaultCellStyle = dataGridViewCellStyle6;
            this.UnitPrice.FillWeight = 50F;
            this.UnitPrice.HeaderText = "Unit Price";
            this.UnitPrice.Name = "UnitPrice";
            this.UnitPrice.ReadOnly = true;
            // 
            // BinID
            // 
            this.BinID.HeaderText = "Bin ID";
            this.BinID.Name = "BinID";
            this.BinID.ReadOnly = true;
            this.BinID.Visible = false;
            // 
            // WhID
            // 
            this.WhID.HeaderText = "Wh ID";
            this.WhID.Name = "WhID";
            this.WhID.ReadOnly = true;
            this.WhID.Visible = false;
            // 
            // PnlHeaderTable
            // 
            this.PnlHeaderTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeaderTable.Controls.Add(this.BtnAddParts);
            this.PnlHeaderTable.Controls.Add(this.BtnDeleteParts);
            this.PnlHeaderTable.Controls.Add(this.LblTable);
            this.PnlHeaderTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlHeaderTable.Location = new System.Drawing.Point(0, 0);
            this.PnlHeaderTable.Name = "PnlHeaderTable";
            this.PnlHeaderTable.Size = new System.Drawing.Size(587, 32);
            this.PnlHeaderTable.TabIndex = 7;
            // 
            // BtnAddParts
            // 
            this.BtnAddParts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAddParts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(122)))));
            this.BtnAddParts.BorderColor = System.Drawing.Color.White;
            this.BtnAddParts.BorderRadius = 8;
            this.BtnAddParts.BorderSize = 0;
            this.BtnAddParts.FlatAppearance.BorderSize = 0;
            this.BtnAddParts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddParts.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAddParts.ForeColor = System.Drawing.Color.White;
            this.BtnAddParts.Location = new System.Drawing.Point(450, 2);
            this.BtnAddParts.Name = "BtnAddParts";
            this.BtnAddParts.Size = new System.Drawing.Size(63, 27);
            this.BtnAddParts.TabIndex = 29;
            this.BtnAddParts.TabStop = false;
            this.BtnAddParts.Text = "&1.) Add";
            this.BtnAddParts.UseVisualStyleBackColor = false;
            this.BtnAddParts.Click += new System.EventHandler(this.BtnAddParts_Click);
            // 
            // BtnDeleteParts
            // 
            this.BtnDeleteParts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnDeleteParts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(122)))));
            this.BtnDeleteParts.BorderColor = System.Drawing.Color.White;
            this.BtnDeleteParts.BorderRadius = 8;
            this.BtnDeleteParts.BorderSize = 0;
            this.BtnDeleteParts.FlatAppearance.BorderSize = 0;
            this.BtnDeleteParts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDeleteParts.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeleteParts.ForeColor = System.Drawing.Color.White;
            this.BtnDeleteParts.Location = new System.Drawing.Point(519, 2);
            this.BtnDeleteParts.Name = "BtnDeleteParts";
            this.BtnDeleteParts.Size = new System.Drawing.Size(63, 27);
            this.BtnDeleteParts.TabIndex = 30;
            this.BtnDeleteParts.TabStop = false;
            this.BtnDeleteParts.Text = "&2.) Delete";
            this.BtnDeleteParts.UseVisualStyleBackColor = false;
            this.BtnDeleteParts.Click += new System.EventHandler(this.BtnDeleteParts_Click);
            // 
            // LblTable
            // 
            this.LblTable.AutoSize = true;
            this.LblTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblTable.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblTable.Location = new System.Drawing.Point(5, 7);
            this.LblTable.Name = "LblTable";
            this.LblTable.Size = new System.Drawing.Size(69, 19);
            this.LblTable.TabIndex = 0;
            this.LblTable.Text = "Parts List";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.BtnArchiveNew);
            this.panel1.Controls.Add(this.BtnClearNew);
            this.panel1.Controls.Add(this.BtnSaveNew);
            this.panel1.Controls.Add(this.BtnClose);
            this.panel1.Controls.Add(this.panel11);
            this.panel1.Controls.Add(this.LblHeader);
            this.panel1.Controls.Add(this.PnlDesign);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(967, 32);
            this.panel1.TabIndex = 26;
            // 
            // BtnArchiveNew
            // 
            this.BtnArchiveNew.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnArchiveNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnArchiveNew.BorderRadius = 0;
            this.BtnArchiveNew.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnArchiveNew.FlatAppearance.BorderSize = 0;
            this.BtnArchiveNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(83)))), ((int)(((byte)(116)))));
            this.BtnArchiveNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnArchiveNew.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnArchiveNew.ForeColor = System.Drawing.SystemColors.Control;
            this.BtnArchiveNew.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(83)))), ((int)(((byte)(116)))));
            this.BtnArchiveNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnArchiveNew.Location = new System.Drawing.Point(824, 3);
            this.BtnArchiveNew.Name = "BtnArchiveNew";
            this.BtnArchiveNew.Size = new System.Drawing.Size(95, 29);
            this.BtnArchiveNew.TabIndex = 69;
            this.BtnArchiveNew.Text = "&Archive";
            this.BtnArchiveNew.UseVisualStyleBackColor = false;
            this.BtnArchiveNew.Click += new System.EventHandler(this.BtnArchive_Click);
            // 
            // BtnClearNew
            // 
            this.BtnClearNew.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnClearNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnClearNew.BorderRadius = 0;
            this.BtnClearNew.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnClearNew.FlatAppearance.BorderSize = 0;
            this.BtnClearNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(83)))), ((int)(((byte)(116)))));
            this.BtnClearNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClearNew.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearNew.ForeColor = System.Drawing.SystemColors.Control;
            this.BtnClearNew.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(83)))), ((int)(((byte)(116)))));
            this.BtnClearNew.Image = global::CARS.Properties.Resources.Delete;
            this.BtnClearNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnClearNew.Location = new System.Drawing.Point(723, 3);
            this.BtnClearNew.Name = "BtnClearNew";
            this.BtnClearNew.Size = new System.Drawing.Size(95, 29);
            this.BtnClearNew.TabIndex = 68;
            this.BtnClearNew.Text = "&Clear";
            this.BtnClearNew.UseVisualStyleBackColor = false;
            this.BtnClearNew.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnSaveNew
            // 
            this.BtnSaveNew.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnSaveNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnSaveNew.BorderRadius = 0;
            this.BtnSaveNew.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnSaveNew.FlatAppearance.BorderSize = 0;
            this.BtnSaveNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(83)))), ((int)(((byte)(116)))));
            this.BtnSaveNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSaveNew.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSaveNew.ForeColor = System.Drawing.SystemColors.Control;
            this.BtnSaveNew.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(83)))), ((int)(((byte)(116)))));
            this.BtnSaveNew.Location = new System.Drawing.Point(622, 3);
            this.BtnSaveNew.Name = "BtnSaveNew";
            this.BtnSaveNew.Size = new System.Drawing.Size(95, 29);
            this.BtnSaveNew.TabIndex = 67;
            this.BtnSaveNew.Text = "&Save";
            this.BtnSaveNew.UseVisualStyleBackColor = false;
            this.BtnSaveNew.Click += new System.EventHandler(this.BtnSave_Click);
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
            this.BtnClose.TabIndex = 55;
            this.BtnClose.Text = "X";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // panel11
            // 
            this.panel11.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel11.Location = new System.Drawing.Point(957, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(10, 32);
            this.panel11.TabIndex = 51;
            // 
            // LblHeader
            // 
            this.LblHeader.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LblHeader.AutoSize = true;
            this.LblHeader.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblHeader.Location = new System.Drawing.Point(0, 5);
            this.LblHeader.Name = "LblHeader";
            this.LblHeader.Size = new System.Drawing.Size(221, 25);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "  STOCK ADJUSTMENT  ";
            // 
            // PnlDesign
            // 
            this.PnlDesign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlDesign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.PnlDesign.Location = new System.Drawing.Point(-8, 17);
            this.PnlDesign.Name = "PnlDesign";
            this.PnlDesign.Size = new System.Drawing.Size(620, 4);
            this.PnlDesign.TabIndex = 50;
            // 
            // frm_stock_adjustment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 917);
            this.Controls.Add(this.PnlMain);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_stock_adjustment";
            this.Text = "frm_po_generation";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.PnlHeaderDetail.ResumeLayout(false);
            this.PnlHeaderDetail.PerformLayout();
            this.PnlMain.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridParts)).EndInit();
            this.PnlHeaderTable.ResumeLayout(false);
            this.PnlHeaderTable.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel PnlMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel PnlDesign;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel PnlHeaderTable;
        private System.Windows.Forms.Label LblTable;
        private Customized_Components.CustomRoundedButton BtnClear;
        private Customized_Components.CustomRoundedButton BtnArchive;
        private Customized_Components.CustomRoundedButton BtnSave;
        private System.Windows.Forms.Panel panel2;
        private Customized_Components.RifinedCustomTextbox TxtRemarks;
        private System.Windows.Forms.Label label4;
        private Customized_Components.RifinedCustomTextbox rifinedCustomTextbox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel PnlHeaderDetail;
        private System.Windows.Forms.Label LblDetail;
        private System.Windows.Forms.ComboBox ComboReason;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private Customized_Components.CustomRoundedButton BtnAddParts;
        private Customized_Components.CustomRoundedButton BtnDeleteParts;
        private System.Windows.Forms.DataGridView DataGridParts;
        private Customized_Components.CustomCloseButton BtnClose;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OtherName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrandName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BinName;
        private System.Windows.Forms.DataGridViewTextBoxColumn WhName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TakeUpQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn DropQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn BinID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WhID;
        private Customized_Components.CustomCloseButton BtnArchiveNew;
        private Customized_Components.CustomCloseButton BtnClearNew;
        private Customized_Components.CustomCloseButton BtnSaveNew;
    }
}