namespace CARS.Components.Masterfiles
{
    partial class frm_parts_inquiry
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.ImagePart = new System.Windows.Forms.PictureBox();
            this.TxtListPrice = new CARS.Customized_Components.RifinedCustomTextbox();
            this.PnlFilter = new System.Windows.Forms.Panel();
            this.ComboInquiryParts = new CARS.Customized_Components.ClassControlAutoSuggest();
            this.label3 = new System.Windows.Forms.Label();
            this.PnlHeaderFilter = new System.Windows.Forms.Panel();
            this.LblFilter = new System.Windows.Forms.Label();
            this.BtnClear = new CARS.Customized_Components.CustomRoundedButton();
            this.ComboFilter = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
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
            this.PSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ListPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PnlHeaderTable = new System.Windows.Forms.Panel();
            this.LblTable = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnClearNew = new CARS.Customized_Components.CustomCloseButton();
            this.BtnClose = new CARS.Customized_Components.CustomCloseButton();
            this.panel11 = new System.Windows.Forms.Panel();
            this.LblHeader = new System.Windows.Forms.Label();
            this.PnlDesign = new System.Windows.Forms.Panel();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePart)).BeginInit();
            this.PnlFilter.SuspendLayout();
            this.PnlHeaderFilter.SuspendLayout();
            this.PnlMain.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridPart)).BeginInit();
            this.PnlHeaderTable.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.PnlFilter);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(0, 32);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(10, 10, 0, 20);
            this.panel5.Size = new System.Drawing.Size(358, 885);
            this.panel5.TabIndex = 9;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.ImagePart);
            this.panel7.Controls.Add(this.TxtListPrice);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(10, 150);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(348, 715);
            this.panel7.TabIndex = 45;
            // 
            // ImagePart
            // 
            this.ImagePart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImagePart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ImagePart.Location = new System.Drawing.Point(0, 435);
            this.ImagePart.Name = "ImagePart";
            this.ImagePart.Size = new System.Drawing.Size(348, 220);
            this.ImagePart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImagePart.TabIndex = 47;
            this.ImagePart.TabStop = false;
            // 
            // TxtListPrice
            // 
            this.TxtListPrice.BackColor = System.Drawing.Color.Black;
            this.TxtListPrice.BorderColor = System.Drawing.Color.Silver;
            this.TxtListPrice.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtListPrice.BorderRadius = 8;
            this.TxtListPrice.BorderSize = 1;
            this.TxtListPrice.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.TxtListPrice.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TxtListPrice.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtListPrice.ForeColor = System.Drawing.Color.White;
            this.TxtListPrice.Location = new System.Drawing.Point(0, 655);
            this.TxtListPrice.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.TxtListPrice.MaxLegnth = 32767;
            this.TxtListPrice.MultiLine = false;
            this.TxtListPrice.Name = "TxtListPrice";
            this.TxtListPrice.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.TxtListPrice.PasswordChar = false;
            this.TxtListPrice.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtListPrice.ReadOnly = true;
            this.TxtListPrice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtListPrice.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtListPrice.Size = new System.Drawing.Size(348, 60);
            this.TxtListPrice.TabIndex = 46;
            this.TxtListPrice.Textt = "0.00";
            this.TxtListPrice.UnderlinedStyle = false;
            // 
            // PnlFilter
            // 
            this.PnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlFilter.Controls.Add(this.ComboInquiryParts);
            this.PnlFilter.Controls.Add(this.label3);
            this.PnlFilter.Controls.Add(this.PnlHeaderFilter);
            this.PnlFilter.Controls.Add(this.ComboFilter);
            this.PnlFilter.Controls.Add(this.label6);
            this.PnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlFilter.Location = new System.Drawing.Point(10, 10);
            this.PnlFilter.Name = "PnlFilter";
            this.PnlFilter.Padding = new System.Windows.Forms.Padding(5, 35, 5, 5);
            this.PnlFilter.Size = new System.Drawing.Size(348, 140);
            this.PnlFilter.TabIndex = 44;
            // 
            // ComboInquiryParts
            // 
            this.ComboInquiryParts.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ComboInquiryParts.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComboInquiryParts.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ComboInquiryParts.Location = new System.Drawing.Point(5, 101);
            this.ComboInquiryParts.MaxLength = 200;
            this.ComboInquiryParts.Name = "ComboInquiryParts";
            this.ComboInquiryParts.Size = new System.Drawing.Size(336, 25);
            this.ComboInquiryParts.TabIndex = 56;
            this.ComboInquiryParts.Values = new string[0];
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 81);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label3.Size = new System.Drawing.Size(53, 20);
            this.label3.TabIndex = 55;
            this.label3.Text = "Keyword";
            // 
            // PnlHeaderFilter
            // 
            this.PnlHeaderFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlHeaderFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeaderFilter.Controls.Add(this.LblFilter);
            this.PnlHeaderFilter.Controls.Add(this.BtnClear);
            this.PnlHeaderFilter.Location = new System.Drawing.Point(-1, -1);
            this.PnlHeaderFilter.Name = "PnlHeaderFilter";
            this.PnlHeaderFilter.Size = new System.Drawing.Size(348, 32);
            this.PnlHeaderFilter.TabIndex = 46;
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
            this.BtnClear.Location = new System.Drawing.Point(244, 0);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(95, 30);
            this.BtnClear.TabIndex = 32;
            this.BtnClear.TabStop = false;
            this.BtnClear.Text = "Clear";
            this.BtnClear.UseVisualStyleBackColor = false;
            this.BtnClear.Visible = false;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // ComboFilter
            // 
            this.ComboFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboFilter.FormattingEnabled = true;
            this.ComboFilter.Items.AddRange(new object[] {
            "ALL",
            "BRAND",
            "DESCRIPTION"});
            this.ComboFilter.Location = new System.Drawing.Point(5, 56);
            this.ComboFilter.Name = "ComboFilter";
            this.ComboFilter.Size = new System.Drawing.Size(336, 25);
            this.ComboFilter.TabIndex = 75;
            this.ComboFilter.SelectedIndexChanged += new System.EventHandler(this.ComboFilter_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(5, 35);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 5, 0, 1);
            this.label6.Size = new System.Drawing.Size(55, 21);
            this.label6.TabIndex = 74;
            this.label6.Text = "Category";
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
            this.PSize,
            this.PType,
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
            this.DataGridPart.TabIndex = 8;
            this.DataGridPart.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridPart_ColumnHeaderMouseClick);
            this.DataGridPart.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridPart_RowEnter);
            // 
            // PartNo
            // 
            this.PartNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PartNo.DataPropertyName = "PartNo";
            this.PartNo.FillWeight = 80F;
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
            this.UomName.FillWeight = 30F;
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
            // PSize
            // 
            this.PSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PSize.DataPropertyName = "PSize";
            this.PSize.FillWeight = 50F;
            this.PSize.HeaderText = "Size";
            this.PSize.Name = "PSize";
            this.PSize.ReadOnly = true;
            // 
            // PType
            // 
            this.PType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PType.DataPropertyName = "PType";
            this.PType.FillWeight = 50F;
            this.PType.HeaderText = "Type";
            this.PType.Name = "PType";
            this.PType.ReadOnly = true;
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
            this.ListPrice.Visible = false;
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
            this.IsActive.Visible = false;
            // 
            // PnlHeaderTable
            // 
            this.PnlHeaderTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeaderTable.Controls.Add(this.LblTable);
            this.PnlHeaderTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlHeaderTable.Location = new System.Drawing.Point(0, 0);
            this.PnlHeaderTable.Name = "PnlHeaderTable";
            this.PnlHeaderTable.Size = new System.Drawing.Size(587, 32);
            this.PnlHeaderTable.TabIndex = 7;
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.BtnClearNew);
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
            this.BtnClearNew.Location = new System.Drawing.Point(824, 3);
            this.BtnClearNew.Name = "BtnClearNew";
            this.BtnClearNew.Size = new System.Drawing.Size(95, 29);
            this.BtnClearNew.TabIndex = 69;
            this.BtnClearNew.Text = "&Clear";
            this.BtnClearNew.UseVisualStyleBackColor = false;
            this.BtnClearNew.Click += new System.EventHandler(this.BtnClear_Click);
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
            this.BtnClose.TabIndex = 56;
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
            this.LblHeader.Size = new System.Drawing.Size(174, 25);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "  PARTS INQUIRY  ";
            // 
            // PnlDesign
            // 
            this.PnlDesign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlDesign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.PnlDesign.Location = new System.Drawing.Point(-8, 17);
            this.PnlDesign.Name = "PnlDesign";
            this.PnlDesign.Size = new System.Drawing.Size(820, 4);
            this.PnlDesign.TabIndex = 50;
            // 
            // frm_parts_inquiry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 917);
            this.Controls.Add(this.PnlMain);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frm_parts_inquiry";
            this.Text = "frm_po_generation";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_parts_inquiry_KeyDown);
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImagePart)).EndInit();
            this.PnlFilter.ResumeLayout(false);
            this.PnlFilter.PerformLayout();
            this.PnlHeaderFilter.ResumeLayout(false);
            this.PnlHeaderFilter.PerformLayout();
            this.PnlMain.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridPart)).EndInit();
            this.PnlHeaderTable.ResumeLayout(false);
            this.PnlHeaderTable.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel PnlFilter;
        private Customized_Components.CustomRoundedButton BtnClear;
        private System.Windows.Forms.Panel PnlMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel PnlDesign;
        private System.Windows.Forms.Panel panel3;
        private Customized_Components.RifinedCustomTextbox TxtListPrice;
        private System.Windows.Forms.PictureBox ImagePart;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel PnlHeaderTable;
        private System.Windows.Forms.Label LblTable;
        private System.Windows.Forms.DataGridView DataGridPart;
        private System.Windows.Forms.Panel PnlHeaderFilter;
        private System.Windows.Forms.Label LblFilter;
        private Customized_Components.CustomCloseButton BtnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OtherName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sku;
        private System.Windows.Forms.DataGridViewTextBoxColumn UomName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrandName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartApply;
        private System.Windows.Forms.DataGridViewTextBoxColumn PSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn PType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ListPrice;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private Customized_Components.ClassControlAutoSuggest ComboInquiryParts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComboFilter;
        private System.Windows.Forms.Label label6;
        private Customized_Components.CustomCloseButton BtnClearNew;
    }
}