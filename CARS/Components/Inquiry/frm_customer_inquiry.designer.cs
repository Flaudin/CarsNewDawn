namespace CARS.Components.Masterfiles
{
    partial class frm_customer_inquiry
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
            this.PnlMain = new System.Windows.Forms.Panel();
            this.DgvPurchaseHistory = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.PnlMenu = new System.Windows.Forms.Panel();
            this.LblMenu = new System.Windows.Forms.Label();
            this.BtnClear = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnSearch = new CARS.Customized_Components.CustomRoundedButton();
            this.PnlFooter = new System.Windows.Forms.Panel();
            this.TxtCustomerListPrice = new CARS.Customized_Components.RifinedCustomTextbox();
            this.LblCurrentListPrice = new System.Windows.Forms.Label();
            this.LayoutFilter = new System.Windows.Forms.TableLayoutPanel();
            this.PnlDrDate = new System.Windows.Forms.Panel();
            this.layout_date = new System.Windows.Forms.TableLayoutPanel();
            this.DateDrTo = new CARS.Customized_Components.CustomDateTime();
            this.DateDrFrom = new CARS.Customized_Components.CustomDateTime();
            this.LblDrDateTo = new System.Windows.Forms.Label();
            this.LblDrDate = new System.Windows.Forms.Label();
            this.PnlSku = new System.Windows.Forms.Panel();
            this.TxtSku = new CARS.Customized_Components.RifinedCustomTextbox();
            this.LblSku = new System.Windows.Forms.Label();
            this.PnlPartNumber = new System.Windows.Forms.Panel();
            this.TxtPartNumber = new CARS.Customized_Components.RifinedCustomTextbox();
            this.LblPartNumber = new System.Windows.Forms.Label();
            this.PnlCustomer = new System.Windows.Forms.Panel();
            this.TxtCustomer = new CARS.Customized_Components.RifinedCustomTextbox();
            this.LblCustomer = new System.Windows.Forms.Label();
            this.PnlHeader = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvPurchaseHistory)).BeginInit();
            this.panel4.SuspendLayout();
            this.PnlMenu.SuspendLayout();
            this.PnlFooter.SuspendLayout();
            this.LayoutFilter.SuspendLayout();
            this.PnlDrDate.SuspendLayout();
            this.layout_date.SuspendLayout();
            this.PnlSku.SuspendLayout();
            this.PnlPartNumber.SuspendLayout();
            this.PnlCustomer.SuspendLayout();
            this.PnlHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlMain
            // 
            this.PnlMain.Controls.Add(this.DgvPurchaseHistory);
            this.PnlMain.Controls.Add(this.panel4);
            this.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMain.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlMain.Location = new System.Drawing.Point(0, 117);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.PnlMain.Size = new System.Drawing.Size(967, 730);
            this.PnlMain.TabIndex = 2;
            // 
            // DgvPurchaseHistory
            // 
            this.DgvPurchaseHistory.AllowUserToAddRows = false;
            this.DgvPurchaseHistory.AllowUserToDeleteRows = false;
            this.DgvPurchaseHistory.AllowUserToResizeColumns = false;
            this.DgvPurchaseHistory.AllowUserToResizeRows = false;
            this.DgvPurchaseHistory.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.DgvPurchaseHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvPurchaseHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.DgvPurchaseHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvPurchaseHistory.Location = new System.Drawing.Point(10, 32);
            this.DgvPurchaseHistory.Name = "DgvPurchaseHistory";
            this.DgvPurchaseHistory.RowHeadersVisible = false;
            this.DgvPurchaseHistory.Size = new System.Drawing.Size(947, 698);
            this.DgvPurchaseHistory.TabIndex = 2;
            this.DgvPurchaseHistory.TabStop = false;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Customer Name";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Part Number";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "SKU";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "DR Date";
            this.Column4.Name = "Column4";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(10, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(947, 32);
            this.panel4.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.label2.Location = new System.Drawing.Point(5, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "PURCHASE PRICE HISTORY";
            // 
            // PnlMenu
            // 
            this.PnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(206)))), ((int)(((byte)(0)))));
            this.PnlMenu.Controls.Add(this.LblMenu);
            this.PnlMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlMenu.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlMenu.Location = new System.Drawing.Point(0, 0);
            this.PnlMenu.Name = "PnlMenu";
            this.PnlMenu.Size = new System.Drawing.Size(967, 32);
            this.PnlMenu.TabIndex = 0;
            // 
            // LblMenu
            // 
            this.LblMenu.AutoSize = true;
            this.LblMenu.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblMenu.Location = new System.Drawing.Point(5, 4);
            this.LblMenu.Name = "LblMenu";
            this.LblMenu.Size = new System.Drawing.Size(197, 25);
            this.LblMenu.TabIndex = 2;
            this.LblMenu.Text = "CUSTOMER INQUIRY";
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
            this.BtnClear.Location = new System.Drawing.Point(744, 24);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(95, 30);
            this.BtnClear.TabIndex = 13;
            this.BtnClear.TabStop = false;
            this.BtnClear.Text = "Clear";
            this.BtnClear.UseVisualStyleBackColor = false;
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
            this.BtnSearch.Location = new System.Drawing.Point(845, 24);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(95, 30);
            this.BtnSearch.TabIndex = 13;
            this.BtnSearch.TabStop = false;
            this.BtnSearch.Text = "Search";
            this.BtnSearch.UseVisualStyleBackColor = false;
            // 
            // PnlFooter
            // 
            this.PnlFooter.Controls.Add(this.TxtCustomerListPrice);
            this.PnlFooter.Controls.Add(this.LblCurrentListPrice);
            this.PnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlFooter.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.PnlFooter.Location = new System.Drawing.Point(0, 847);
            this.PnlFooter.Name = "PnlFooter";
            this.PnlFooter.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.PnlFooter.Size = new System.Drawing.Size(967, 70);
            this.PnlFooter.TabIndex = 3;
            // 
            // TxtCustomerListPrice
            // 
            this.TxtCustomerListPrice.BackColor = System.Drawing.SystemColors.Window;
            this.TxtCustomerListPrice.BorderColor = System.Drawing.Color.Silver;
            this.TxtCustomerListPrice.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtCustomerListPrice.BorderRadius = 8;
            this.TxtCustomerListPrice.BorderSize = 1;
            this.TxtCustomerListPrice.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.TxtCustomerListPrice.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtCustomerListPrice.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCustomerListPrice.Location = new System.Drawing.Point(10, 13);
            this.TxtCustomerListPrice.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.TxtCustomerListPrice.MaximumSize = new System.Drawing.Size(245, 42);
            this.TxtCustomerListPrice.MaxLegnth = 32767;
            this.TxtCustomerListPrice.MultiLine = false;
            this.TxtCustomerListPrice.Name = "TxtCustomerListPrice";
            this.TxtCustomerListPrice.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.TxtCustomerListPrice.PasswordChar = false;
            this.TxtCustomerListPrice.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtCustomerListPrice.ReadOnly = false;
            this.TxtCustomerListPrice.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtCustomerListPrice.Size = new System.Drawing.Size(245, 38);
            this.TxtCustomerListPrice.TabIndex = 5;
            this.TxtCustomerListPrice.Textt = "";
            this.TxtCustomerListPrice.UnderlinedStyle = false;
            // 
            // LblCurrentListPrice
            // 
            this.LblCurrentListPrice.AutoSize = true;
            this.LblCurrentListPrice.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblCurrentListPrice.Location = new System.Drawing.Point(10, 0);
            this.LblCurrentListPrice.Name = "LblCurrentListPrice";
            this.LblCurrentListPrice.Size = new System.Drawing.Size(93, 13);
            this.LblCurrentListPrice.TabIndex = 2;
            this.LblCurrentListPrice.Text = "Current List Price";
            // 
            // LayoutFilter
            // 
            this.LayoutFilter.ColumnCount = 4;
            this.LayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.LayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.LayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.LayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.LayoutFilter.Controls.Add(this.PnlDrDate, 3, 0);
            this.LayoutFilter.Controls.Add(this.PnlSku, 2, 0);
            this.LayoutFilter.Controls.Add(this.PnlPartNumber, 1, 0);
            this.LayoutFilter.Controls.Add(this.PnlCustomer, 0, 0);
            this.LayoutFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutFilter.Location = new System.Drawing.Point(0, 0);
            this.LayoutFilter.Name = "LayoutFilter";
            this.LayoutFilter.RowCount = 1;
            this.LayoutFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutFilter.Size = new System.Drawing.Size(745, 63);
            this.LayoutFilter.TabIndex = 0;
            // 
            // PnlDrDate
            // 
            this.PnlDrDate.Controls.Add(this.layout_date);
            this.PnlDrDate.Controls.Add(this.LblDrDate);
            this.PnlDrDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlDrDate.Location = new System.Drawing.Point(561, 3);
            this.PnlDrDate.Name = "PnlDrDate";
            this.PnlDrDate.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.PnlDrDate.Size = new System.Drawing.Size(181, 57);
            this.PnlDrDate.TabIndex = 3;
            // 
            // layout_date
            // 
            this.layout_date.ColumnCount = 3;
            this.layout_date.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.44444F));
            this.layout_date.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.layout_date.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.44444F));
            this.layout_date.Controls.Add(this.DateDrTo, 2, 0);
            this.layout_date.Controls.Add(this.DateDrFrom, 0, 0);
            this.layout_date.Controls.Add(this.LblDrDateTo, 1, 0);
            this.layout_date.Dock = System.Windows.Forms.DockStyle.Top;
            this.layout_date.Location = new System.Drawing.Point(10, 20);
            this.layout_date.Name = "layout_date";
            this.layout_date.RowCount = 1;
            this.layout_date.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout_date.Size = new System.Drawing.Size(161, 32);
            this.layout_date.TabIndex = 4;
            // 
            // DateDrTo
            // 
            this.DateDrTo.BoderSize = 0;
            this.DateDrTo.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.DateDrTo.CustomFormat = "MM/dd/yyyy";
            this.DateDrTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateDrTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateDrTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateDrTo.Location = new System.Drawing.Point(91, 3);
            this.DateDrTo.MinimumSize = new System.Drawing.Size(4, 32);
            this.DateDrTo.Name = "DateDrTo";
            this.DateDrTo.Size = new System.Drawing.Size(67, 32);
            this.DateDrTo.SkinColor = System.Drawing.Color.White;
            this.DateDrTo.TabIndex = 6;
            this.DateDrTo.TextColor = System.Drawing.Color.Black;
            // 
            // DateDrFrom
            // 
            this.DateDrFrom.BoderSize = 0;
            this.DateDrFrom.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.DateDrFrom.CustomFormat = "MM/dd/yyyy";
            this.DateDrFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateDrFrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateDrFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateDrFrom.Location = new System.Drawing.Point(3, 3);
            this.DateDrFrom.MinimumSize = new System.Drawing.Size(4, 32);
            this.DateDrFrom.Name = "DateDrFrom";
            this.DateDrFrom.Size = new System.Drawing.Size(65, 32);
            this.DateDrFrom.SkinColor = System.Drawing.Color.White;
            this.DateDrFrom.TabIndex = 5;
            this.DateDrFrom.TextColor = System.Drawing.Color.Black;
            // 
            // LblDrDateTo
            // 
            this.LblDrDateTo.AutoSize = true;
            this.LblDrDateTo.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblDrDateTo.Location = new System.Drawing.Point(74, 0);
            this.LblDrDateTo.Name = "LblDrDateTo";
            this.LblDrDateTo.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.LblDrDateTo.Size = new System.Drawing.Size(11, 32);
            this.LblDrDateTo.TabIndex = 7;
            this.LblDrDateTo.Text = "To";
            // 
            // LblDrDate
            // 
            this.LblDrDate.AutoSize = true;
            this.LblDrDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblDrDate.Location = new System.Drawing.Point(10, 5);
            this.LblDrDate.Name = "LblDrDate";
            this.LblDrDate.Size = new System.Drawing.Size(49, 15);
            this.LblDrDate.TabIndex = 1;
            this.LblDrDate.Text = "DR Date";
            // 
            // PnlSku
            // 
            this.PnlSku.Controls.Add(this.TxtSku);
            this.PnlSku.Controls.Add(this.LblSku);
            this.PnlSku.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlSku.Location = new System.Drawing.Point(375, 3);
            this.PnlSku.Name = "PnlSku";
            this.PnlSku.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.PnlSku.Size = new System.Drawing.Size(180, 57);
            this.PnlSku.TabIndex = 2;
            // 
            // TxtSku
            // 
            this.TxtSku.BackColor = System.Drawing.SystemColors.Window;
            this.TxtSku.BorderColor = System.Drawing.Color.Silver;
            this.TxtSku.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtSku.BorderRadius = 8;
            this.TxtSku.BorderSize = 1;
            this.TxtSku.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.TxtSku.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtSku.Location = new System.Drawing.Point(10, 20);
            this.TxtSku.MaxLegnth = 32767;
            this.TxtSku.MultiLine = false;
            this.TxtSku.Name = "TxtSku";
            this.TxtSku.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtSku.PasswordChar = false;
            this.TxtSku.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtSku.ReadOnly = false;
            this.TxtSku.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtSku.Size = new System.Drawing.Size(160, 32);
            this.TxtSku.TabIndex = 3;
            this.TxtSku.Textt = "";
            this.TxtSku.UnderlinedStyle = false;
            // 
            // LblSku
            // 
            this.LblSku.AutoSize = true;
            this.LblSku.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblSku.Location = new System.Drawing.Point(10, 5);
            this.LblSku.Name = "LblSku";
            this.LblSku.Size = new System.Drawing.Size(28, 15);
            this.LblSku.TabIndex = 1;
            this.LblSku.Text = "SKU";
            // 
            // PnlPartNumber
            // 
            this.PnlPartNumber.Controls.Add(this.TxtPartNumber);
            this.PnlPartNumber.Controls.Add(this.LblPartNumber);
            this.PnlPartNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlPartNumber.Location = new System.Drawing.Point(189, 3);
            this.PnlPartNumber.Name = "PnlPartNumber";
            this.PnlPartNumber.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.PnlPartNumber.Size = new System.Drawing.Size(180, 57);
            this.PnlPartNumber.TabIndex = 1;
            // 
            // TxtPartNumber
            // 
            this.TxtPartNumber.BackColor = System.Drawing.SystemColors.Window;
            this.TxtPartNumber.BorderColor = System.Drawing.Color.Silver;
            this.TxtPartNumber.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtPartNumber.BorderRadius = 8;
            this.TxtPartNumber.BorderSize = 1;
            this.TxtPartNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.TxtPartNumber.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtPartNumber.Location = new System.Drawing.Point(10, 20);
            this.TxtPartNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TxtPartNumber.MaxLegnth = 32767;
            this.TxtPartNumber.MultiLine = false;
            this.TxtPartNumber.Name = "TxtPartNumber";
            this.TxtPartNumber.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtPartNumber.PasswordChar = false;
            this.TxtPartNumber.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtPartNumber.ReadOnly = false;
            this.TxtPartNumber.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtPartNumber.Size = new System.Drawing.Size(160, 32);
            this.TxtPartNumber.TabIndex = 2;
            this.TxtPartNumber.Textt = "";
            this.TxtPartNumber.UnderlinedStyle = false;
            // 
            // LblPartNumber
            // 
            this.LblPartNumber.AutoSize = true;
            this.LblPartNumber.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblPartNumber.Location = new System.Drawing.Point(10, 5);
            this.LblPartNumber.Name = "LblPartNumber";
            this.LblPartNumber.Size = new System.Drawing.Size(75, 15);
            this.LblPartNumber.TabIndex = 1;
            this.LblPartNumber.Text = "Part Number";
            // 
            // PnlCustomer
            // 
            this.PnlCustomer.Controls.Add(this.TxtCustomer);
            this.PnlCustomer.Controls.Add(this.LblCustomer);
            this.PnlCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlCustomer.Location = new System.Drawing.Point(3, 3);
            this.PnlCustomer.Name = "PnlCustomer";
            this.PnlCustomer.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.PnlCustomer.Size = new System.Drawing.Size(180, 57);
            this.PnlCustomer.TabIndex = 0;
            // 
            // TxtCustomer
            // 
            this.TxtCustomer.BackColor = System.Drawing.SystemColors.Window;
            this.TxtCustomer.BorderColor = System.Drawing.Color.Silver;
            this.TxtCustomer.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtCustomer.BorderRadius = 8;
            this.TxtCustomer.BorderSize = 1;
            this.TxtCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.TxtCustomer.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtCustomer.Location = new System.Drawing.Point(10, 20);
            this.TxtCustomer.MaxLegnth = 32767;
            this.TxtCustomer.MultiLine = false;
            this.TxtCustomer.Name = "TxtCustomer";
            this.TxtCustomer.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtCustomer.PasswordChar = false;
            this.TxtCustomer.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtCustomer.ReadOnly = false;
            this.TxtCustomer.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtCustomer.Size = new System.Drawing.Size(160, 32);
            this.TxtCustomer.TabIndex = 1;
            this.TxtCustomer.Textt = "";
            this.TxtCustomer.UnderlinedStyle = false;
            // 
            // LblCustomer
            // 
            this.LblCustomer.AutoSize = true;
            this.LblCustomer.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblCustomer.Location = new System.Drawing.Point(10, 5);
            this.LblCustomer.Name = "LblCustomer";
            this.LblCustomer.Size = new System.Drawing.Size(102, 15);
            this.LblCustomer.TabIndex = 0;
            this.LblCustomer.Text = "Customer\'s Name";
            // 
            // PnlHeader
            // 
            this.PnlHeader.BackColor = System.Drawing.SystemColors.Control;
            this.PnlHeader.Controls.Add(this.panel2);
            this.PnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlHeader.Location = new System.Drawing.Point(0, 32);
            this.PnlHeader.Name = "PnlHeader";
            this.PnlHeader.Padding = new System.Windows.Forms.Padding(10);
            this.PnlHeader.Size = new System.Drawing.Size(967, 85);
            this.PnlHeader.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.BtnSearch);
            this.panel2.Controls.Add(this.LayoutFilter);
            this.panel2.Controls.Add(this.BtnClear);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 200, 0);
            this.panel2.Size = new System.Drawing.Size(947, 65);
            this.panel2.TabIndex = 6;
            // 
            // frm_customer_inquiry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 917);
            this.Controls.Add(this.PnlMain);
            this.Controls.Add(this.PnlHeader);
            this.Controls.Add(this.PnlFooter);
            this.Controls.Add(this.PnlMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_customer_inquiry";
            this.Text = "frm_po_generation";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.PnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvPurchaseHistory)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.PnlMenu.ResumeLayout(false);
            this.PnlMenu.PerformLayout();
            this.PnlFooter.ResumeLayout(false);
            this.PnlFooter.PerformLayout();
            this.LayoutFilter.ResumeLayout(false);
            this.PnlDrDate.ResumeLayout(false);
            this.PnlDrDate.PerformLayout();
            this.layout_date.ResumeLayout(false);
            this.layout_date.PerformLayout();
            this.PnlSku.ResumeLayout(false);
            this.PnlSku.PerformLayout();
            this.PnlPartNumber.ResumeLayout(false);
            this.PnlPartNumber.PerformLayout();
            this.PnlCustomer.ResumeLayout(false);
            this.PnlCustomer.PerformLayout();
            this.PnlHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PnlMenu;
        private System.Windows.Forms.Panel PnlMain;
        private System.Windows.Forms.Panel PnlFooter;
        private System.Windows.Forms.Label LblMenu;
        private System.Windows.Forms.TableLayoutPanel LayoutFilter;
        private System.Windows.Forms.Panel PnlDrDate;
        private System.Windows.Forms.Panel PnlSku;
        private System.Windows.Forms.Panel PnlPartNumber;
        private System.Windows.Forms.Panel PnlCustomer;
        private System.Windows.Forms.Label LblCustomer;
        private System.Windows.Forms.Label LblDrDate;
        private System.Windows.Forms.Label LblSku;
        private System.Windows.Forms.Label LblPartNumber;
        private System.Windows.Forms.TableLayoutPanel layout_date;
        private System.Windows.Forms.Label LblDrDateTo;
        private System.Windows.Forms.Label LblCurrentListPrice;
        private Customized_Components.CustomRoundedButton BtnSearch;
        private Customized_Components.CustomRoundedButton BtnClear;
        private System.Windows.Forms.DataGridView DgvPurchaseHistory;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private Customized_Components.RifinedCustomTextbox TxtSku;
        private Customized_Components.RifinedCustomTextbox TxtPartNumber;
        private Customized_Components.RifinedCustomTextbox TxtCustomer;
        private Customized_Components.CustomDateTime DateDrTo;
        private Customized_Components.CustomDateTime DateDrFrom;
        private Customized_Components.RifinedCustomTextbox TxtCustomerListPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Panel PnlHeader;
        private System.Windows.Forms.Panel panel2;
    }
}