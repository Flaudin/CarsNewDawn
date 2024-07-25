namespace CARS.Components.Transactions
{
    partial class frm_price_management_archive
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
            this.PnlMain = new System.Windows.Forms.Panel();
            this.PnlHeader = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.BtnClose = new CARS.Customized_Components.CustomRoundedButton();
            this.LblHeader = new System.Windows.Forms.Label();
            this.PnlBody = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.DataGridPart = new System.Windows.Forms.DataGridView();
            this.PartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OtherName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sku = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UomName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrandName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ListPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PnlHeaderParts = new System.Windows.Forms.Panel();
            this.LblParts = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DataGridPrice = new System.Windows.Forms.DataGridView();
            this.ControlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PnlHeaderPrice = new System.Windows.Forms.Panel();
            this.LblPrice = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.PnlFilter = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.DateTo = new CARS.Customized_Components.CustomDateTime();
            this.label7 = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.DateFrom = new CARS.Customized_Components.CustomDateTime();
            this.label5 = new System.Windows.Forms.Label();
            this.PnlHeaderFilter = new System.Windows.Forms.Panel();
            this.LblFilter = new System.Windows.Forms.Label();
            this.TxtPartNo = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnClear = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnSearch = new CARS.Customized_Components.CustomRoundedButton();
            this.PnlMain.SuspendLayout();
            this.PnlHeader.SuspendLayout();
            this.PnlBody.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridPart)).BeginInit();
            this.PnlHeaderParts.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridPrice)).BeginInit();
            this.PnlHeaderPrice.SuspendLayout();
            this.PnlFilter.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel13.SuspendLayout();
            this.PnlHeaderFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlMain
            // 
            this.PnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlMain.Controls.Add(this.PnlHeader);
            this.PnlMain.Controls.Add(this.PnlBody);
            this.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMain.Location = new System.Drawing.Point(0, 0);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.PnlMain.Size = new System.Drawing.Size(1300, 800);
            this.PnlMain.TabIndex = 9;
            // 
            // PnlHeader
            // 
            this.PnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlHeader.BackColor = System.Drawing.Color.Yellow;
            this.PnlHeader.Controls.Add(this.panel14);
            this.PnlHeader.Controls.Add(this.BtnClose);
            this.PnlHeader.Controls.Add(this.LblHeader);
            this.PnlHeader.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlHeader.Location = new System.Drawing.Point(0, 0);
            this.PnlHeader.Name = "PnlHeader";
            this.PnlHeader.Size = new System.Drawing.Size(1300, 32);
            this.PnlHeader.TabIndex = 9;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.panel14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel14.Location = new System.Drawing.Point(0, 30);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(1300, 2);
            this.panel14.TabIndex = 51;
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.BackColor = System.Drawing.Color.Transparent;
            this.BtnClose.BorderColor = System.Drawing.Color.White;
            this.BtnClose.BorderRadius = 0;
            this.BtnClose.BorderSize = 0;
            this.BtnClose.FlatAppearance.BorderSize = 0;
            this.BtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Font = new System.Drawing.Font("Segoe UI Black", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnClose.Location = new System.Drawing.Point(1264, 0);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.BtnClose.Size = new System.Drawing.Size(33, 31);
            this.BtnClose.TabIndex = 39;
            this.BtnClose.TabStop = false;
            this.BtnClose.Text = "X";
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // LblHeader
            // 
            this.LblHeader.AccessibleName = "";
            this.LblHeader.AutoSize = true;
            this.LblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblHeader.Location = new System.Drawing.Point(3, 3);
            this.LblHeader.Name = "LblHeader";
            this.LblHeader.Size = new System.Drawing.Size(287, 25);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "PRICE MANAGEMENT ARCHIVE";
            // 
            // PnlBody
            // 
            this.PnlBody.BackColor = System.Drawing.SystemColors.Control;
            this.PnlBody.Controls.Add(this.panel2);
            this.PnlBody.Controls.Add(this.panel5);
            this.PnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlBody.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlBody.Location = new System.Drawing.Point(0, 30);
            this.PnlBody.Name = "PnlBody";
            this.PnlBody.Size = new System.Drawing.Size(1298, 768);
            this.PnlBody.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(301, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(997, 768);
            this.panel2.TabIndex = 25;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.DataGridPart);
            this.panel8.Controls.Add(this.PnlHeaderParts);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(10, 10);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(977, 747);
            this.panel8.TabIndex = 2;
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
            this.Sku,
            this.UomName,
            this.BrandName,
            this.UnitCost,
            this.ListPrice});
            this.DataGridPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridPart.Location = new System.Drawing.Point(0, 32);
            this.DataGridPart.MultiSelect = false;
            this.DataGridPart.Name = "DataGridPart";
            this.DataGridPart.ReadOnly = true;
            this.DataGridPart.RowHeadersVisible = false;
            this.DataGridPart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridPart.Size = new System.Drawing.Size(975, 713);
            this.DataGridPart.TabIndex = 49;
            this.DataGridPart.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridParts_ColumnHeaderMouseClick);
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
            // Sku
            // 
            this.Sku.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Sku.DataPropertyName = "Sku";
            this.Sku.FillWeight = 80F;
            this.Sku.HeaderText = "SKU";
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
            // UnitCost
            // 
            this.UnitCost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UnitCost.DataPropertyName = "UnitCost";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.UnitCost.DefaultCellStyle = dataGridViewCellStyle2;
            this.UnitCost.HeaderText = "Unit Cost";
            this.UnitCost.Name = "UnitCost";
            this.UnitCost.ReadOnly = true;
            // 
            // ListPrice
            // 
            this.ListPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ListPrice.DataPropertyName = "ListPrice";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0.00";
            this.ListPrice.DefaultCellStyle = dataGridViewCellStyle3;
            this.ListPrice.HeaderText = "List Price";
            this.ListPrice.Name = "ListPrice";
            this.ListPrice.ReadOnly = true;
            // 
            // PnlHeaderParts
            // 
            this.PnlHeaderParts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeaderParts.Controls.Add(this.LblParts);
            this.PnlHeaderParts.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlHeaderParts.Location = new System.Drawing.Point(0, 0);
            this.PnlHeaderParts.Name = "PnlHeaderParts";
            this.PnlHeaderParts.Size = new System.Drawing.Size(975, 32);
            this.PnlHeaderParts.TabIndex = 7;
            // 
            // LblParts
            // 
            this.LblParts.AutoSize = true;
            this.LblParts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblParts.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblParts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblParts.Location = new System.Drawing.Point(5, 7);
            this.LblParts.Name = "LblParts";
            this.LblParts.Size = new System.Drawing.Size(69, 19);
            this.LblParts.TabIndex = 0;
            this.LblParts.Text = "Parts List";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel3);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.PnlFilter);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
            this.panel5.Size = new System.Drawing.Size(301, 768);
            this.panel5.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.DataGridPrice);
            this.panel3.Controls.Add(this.PnlHeaderPrice);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 217);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(291, 538);
            this.panel3.TabIndex = 47;
            // 
            // DataGridPrice
            // 
            this.DataGridPrice.AllowUserToAddRows = false;
            this.DataGridPrice.AllowUserToDeleteRows = false;
            this.DataGridPrice.AllowUserToResizeColumns = false;
            this.DataGridPrice.AllowUserToResizeRows = false;
            this.DataGridPrice.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.DataGridPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridPrice.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridPrice.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DataGridPrice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridPrice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ControlNo,
            this.PriceDate});
            this.DataGridPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridPrice.Location = new System.Drawing.Point(0, 32);
            this.DataGridPrice.MultiSelect = false;
            this.DataGridPrice.Name = "DataGridPrice";
            this.DataGridPrice.ReadOnly = true;
            this.DataGridPrice.RowHeadersVisible = false;
            this.DataGridPrice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridPrice.Size = new System.Drawing.Size(289, 504);
            this.DataGridPrice.TabIndex = 6;
            this.DataGridPrice.TabStop = false;
            this.DataGridPrice.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridPrice_ColumnHeaderMouseClick);
            this.DataGridPrice.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridPrice_RowEnter);
            // 
            // ControlNo
            // 
            this.ControlNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ControlNo.DataPropertyName = "ControlNo";
            this.ControlNo.FillWeight = 80F;
            this.ControlNo.HeaderText = "Control No.";
            this.ControlNo.Name = "ControlNo";
            this.ControlNo.ReadOnly = true;
            // 
            // PriceDate
            // 
            this.PriceDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PriceDate.DataPropertyName = "PriceDate";
            this.PriceDate.HeaderText = "Price Date";
            this.PriceDate.Name = "PriceDate";
            this.PriceDate.ReadOnly = true;
            // 
            // PnlHeaderPrice
            // 
            this.PnlHeaderPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeaderPrice.Controls.Add(this.LblPrice);
            this.PnlHeaderPrice.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlHeaderPrice.Location = new System.Drawing.Point(0, 0);
            this.PnlHeaderPrice.Name = "PnlHeaderPrice";
            this.PnlHeaderPrice.Size = new System.Drawing.Size(289, 32);
            this.PnlHeaderPrice.TabIndex = 7;
            // 
            // LblPrice
            // 
            this.LblPrice.AutoSize = true;
            this.LblPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblPrice.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblPrice.Location = new System.Drawing.Point(5, 7);
            this.LblPrice.Name = "LblPrice";
            this.LblPrice.Size = new System.Drawing.Size(69, 19);
            this.LblPrice.TabIndex = 0;
            this.LblPrice.Text = "Price List";
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(10, 207);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(291, 10);
            this.panel7.TabIndex = 46;
            // 
            // PnlFilter
            // 
            this.PnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlFilter.Controls.Add(this.tableLayoutPanel1);
            this.PnlFilter.Controls.Add(this.PnlHeaderFilter);
            this.PnlFilter.Controls.Add(this.TxtPartNo);
            this.PnlFilter.Controls.Add(this.label1);
            this.PnlFilter.Controls.Add(this.BtnClear);
            this.PnlFilter.Controls.Add(this.BtnSearch);
            this.PnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlFilter.Location = new System.Drawing.Point(10, 10);
            this.PnlFilter.Name = "PnlFilter";
            this.PnlFilter.Padding = new System.Windows.Forms.Padding(5, 35, 5, 5);
            this.PnlFilter.Size = new System.Drawing.Size(291, 197);
            this.PnlFilter.TabIndex = 45;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Controls.Add(this.panel16, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel15, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel13, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 87);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(279, 61);
            this.tableLayoutPanel1.TabIndex = 52;
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.DateTo);
            this.panel16.Controls.Add(this.label7);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel16.Location = new System.Drawing.Point(155, 3);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(121, 55);
            this.panel16.TabIndex = 24;
            // 
            // DateTo
            // 
            this.DateTo.BoderSize = 0;
            this.DateTo.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.DateTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.DateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTo.Location = new System.Drawing.Point(0, 20);
            this.DateTo.MinimumSize = new System.Drawing.Size(4, 32);
            this.DateTo.Name = "DateTo";
            this.DateTo.Size = new System.Drawing.Size(121, 32);
            this.DateTo.SkinColor = System.Drawing.Color.White;
            this.DateTo.TabIndex = 23;
            this.DateTo.TextColor = System.Drawing.Color.Black;
            this.DateTo.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label7.Size = new System.Drawing.Size(97, 20);
            this.label7.TabIndex = 24;
            this.label7.Text = "                              ";
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.label8);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(128, 3);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(21, 55);
            this.panel15.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 15);
            this.label8.TabIndex = 23;
            this.label8.Text = "To";
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.DateFrom);
            this.panel13.Controls.Add(this.label5);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(3, 3);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(119, 55);
            this.panel13.TabIndex = 21;
            // 
            // DateFrom
            // 
            this.DateFrom.BoderSize = 0;
            this.DateFrom.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.DateFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.DateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateFrom.Location = new System.Drawing.Point(0, 20);
            this.DateFrom.MinimumSize = new System.Drawing.Size(4, 32);
            this.DateFrom.Name = "DateFrom";
            this.DateFrom.Size = new System.Drawing.Size(119, 32);
            this.DateFrom.SkinColor = System.Drawing.Color.White;
            this.DateFrom.TabIndex = 23;
            this.DateFrom.TextColor = System.Drawing.Color.Black;
            this.DateFrom.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label5.Size = new System.Drawing.Size(67, 20);
            this.label5.TabIndex = 22;
            this.label5.Text = "Date Range";
            // 
            // PnlHeaderFilter
            // 
            this.PnlHeaderFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlHeaderFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeaderFilter.Controls.Add(this.LblFilter);
            this.PnlHeaderFilter.Location = new System.Drawing.Point(-1, -1);
            this.PnlHeaderFilter.Name = "PnlHeaderFilter";
            this.PnlHeaderFilter.Size = new System.Drawing.Size(291, 32);
            this.PnlHeaderFilter.TabIndex = 51;
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
            // TxtPartNo
            // 
            this.TxtPartNo.BackColor = System.Drawing.SystemColors.Window;
            this.TxtPartNo.BorderColor = System.Drawing.Color.Silver;
            this.TxtPartNo.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtPartNo.BorderRadius = 8;
            this.TxtPartNo.BorderSize = 1;
            this.TxtPartNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtPartNo.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtPartNo.Location = new System.Drawing.Point(5, 55);
            this.TxtPartNo.MaxLegnth = 25;
            this.TxtPartNo.MultiLine = false;
            this.TxtPartNo.Name = "TxtPartNo";
            this.TxtPartNo.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtPartNo.PasswordChar = false;
            this.TxtPartNo.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtPartNo.ReadOnly = false;
            this.TxtPartNo.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtPartNo.Size = new System.Drawing.Size(279, 32);
            this.TxtPartNo.TabIndex = 50;
            this.TxtPartNo.Textt = "";
            this.TxtPartNo.UnderlinedStyle = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(5, 35);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 49;
            this.label1.Text = "Part No.";
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
            this.BtnClear.Location = new System.Drawing.Point(189, 160);
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
            this.BtnSearch.Location = new System.Drawing.Point(88, 160);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(95, 30);
            this.BtnSearch.TabIndex = 33;
            this.BtnSearch.TabStop = false;
            this.BtnSearch.Text = "S&earch";
            this.BtnSearch.UseVisualStyleBackColor = false;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // frm_price_management_archive
            // 
            this.AcceptButton = this.BtnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 800);
            this.Controls.Add(this.PnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frm_price_management_archive";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Oem Parts Selection";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_price_management_archive_KeyDown);
            this.PnlMain.ResumeLayout(false);
            this.PnlHeader.ResumeLayout(false);
            this.PnlHeader.PerformLayout();
            this.PnlBody.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridPart)).EndInit();
            this.PnlHeaderParts.ResumeLayout(false);
            this.PnlHeaderParts.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridPrice)).EndInit();
            this.PnlHeaderPrice.ResumeLayout(false);
            this.PnlHeaderPrice.PerformLayout();
            this.PnlFilter.ResumeLayout(false);
            this.PnlFilter.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.PnlHeaderFilter.ResumeLayout(false);
            this.PnlHeaderFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PnlMain;
        private System.Windows.Forms.Panel PnlHeader;
        private Customized_Components.CustomRoundedButton BtnClose;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel PnlBody;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel PnlFilter;
        private System.Windows.Forms.Panel PnlHeaderFilter;
        private System.Windows.Forms.Label LblFilter;
        private Customized_Components.RifinedCustomTextbox TxtPartNo;
        private System.Windows.Forms.Label label1;
        private Customized_Components.CustomRoundedButton BtnClear;
        private Customized_Components.CustomRoundedButton BtnSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel PnlHeaderParts;
        private System.Windows.Forms.Label LblParts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label5;
        private Customized_Components.CustomDateTime DateFrom;
        private System.Windows.Forms.Panel panel16;
        private Customized_Components.CustomDateTime DateTo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView DataGridPrice;
        private System.Windows.Forms.Panel PnlHeaderPrice;
        private System.Windows.Forms.Label LblPrice;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.DataGridViewTextBoxColumn ControlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceDate;
        private System.Windows.Forms.DataGridView DataGridPart;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OtherName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sku;
        private System.Windows.Forms.DataGridViewTextBoxColumn UomName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrandName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn ListPrice;
    }
}