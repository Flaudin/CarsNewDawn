namespace CARS.Components.Masterfiles
{
    partial class frm_city
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_city));
            this.PnlMain = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DataGridCity = new System.Windows.Forms.DataGridView();
            this.uniqueid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CityName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CityID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zip_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProvID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.within_gma = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PnlHeaderTable = new System.Windows.Forms.Panel();
            this.LblTable = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.PnlEncode = new System.Windows.Forms.Panel();
            this.CheckGma = new CARS.Customized_Components.CustomCheckbox();
            this.ComboProvince = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.TxtZip = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label11 = new System.Windows.Forms.Label();
            this.TxtCityName = new CARS.Customized_Components.RifinedCustomTextbox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.CheckActive = new CARS.Customized_Components.CustomCheckbox();
            this.PnlHeaderEntry = new System.Windows.Forms.Panel();
            this.LblEncode = new System.Windows.Forms.Label();
            this.BtnSave = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnClearEncode = new CARS.Customized_Components.CustomRoundedButton();
            this.TxtCityID = new CARS.Customized_Components.RifinedCustomTextbox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.PnlFilter = new System.Windows.Forms.Panel();
            this.PnlHeaderFilter = new System.Windows.Forms.Panel();
            this.LblFilter = new System.Windows.Forms.Label();
            this.CheckGmaFilter = new CARS.Customized_Components.CustomCheckbox();
            this.BtnClear = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnSearch = new CARS.Customized_Components.CustomRoundedButton();
            this.ComboProvinceFilter = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtZipFilter = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCityNameFilter = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnClose = new CARS.Customized_Components.CustomCloseButton();
            this.panel9 = new System.Windows.Forms.Panel();
            this.LblHeader = new System.Windows.Forms.Label();
            this.PnlDesign = new System.Windows.Forms.Panel();
            this.PnlMain.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridCity)).BeginInit();
            this.PnlHeaderTable.SuspendLayout();
            this.panel5.SuspendLayout();
            this.PnlEncode.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.PnlHeaderEntry.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.PnlFilter.SuspendLayout();
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
            this.PnlMain.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.DataGridCity);
            this.panel3.Controls.Add(this.PnlHeaderTable);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(10, 10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(589, 855);
            this.panel3.TabIndex = 0;
            // 
            // DataGridCity
            // 
            this.DataGridCity.AllowUserToAddRows = false;
            this.DataGridCity.AllowUserToDeleteRows = false;
            this.DataGridCity.AllowUserToResizeColumns = false;
            this.DataGridCity.AllowUserToResizeRows = false;
            this.DataGridCity.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.DataGridCity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridCity.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridCity.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridCity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridCity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.uniqueid,
            this.CityName,
            this.CityID,
            this.zip_code,
            this.ProvID,
            this.IsActive,
            this.within_gma});
            this.DataGridCity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridCity.Location = new System.Drawing.Point(0, 32);
            this.DataGridCity.MultiSelect = false;
            this.DataGridCity.Name = "DataGridCity";
            this.DataGridCity.ReadOnly = true;
            this.DataGridCity.RowHeadersVisible = false;
            this.DataGridCity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridCity.Size = new System.Drawing.Size(587, 821);
            this.DataGridCity.TabIndex = 5;
            this.DataGridCity.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridCity_ColumnHeaderMouseClick);
            this.DataGridCity.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridCity_RowEnter);
            // 
            // uniqueid
            // 
            this.uniqueid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.uniqueid.DataPropertyName = "uniqueid";
            this.uniqueid.HeaderText = "Unique ID";
            this.uniqueid.Name = "uniqueid";
            this.uniqueid.ReadOnly = true;
            this.uniqueid.Visible = false;
            // 
            // CityName
            // 
            this.CityName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CityName.DataPropertyName = "CityName";
            this.CityName.FillWeight = 200F;
            this.CityName.HeaderText = "City";
            this.CityName.Name = "CityName";
            this.CityName.ReadOnly = true;
            // 
            // CityID
            // 
            this.CityID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CityID.DataPropertyName = "CityID";
            this.CityID.FillWeight = 35F;
            this.CityID.HeaderText = "City ID";
            this.CityID.Name = "CityID";
            this.CityID.ReadOnly = true;
            // 
            // zip_code
            // 
            this.zip_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.zip_code.DataPropertyName = "zip_code";
            this.zip_code.FillWeight = 40F;
            this.zip_code.HeaderText = "Zip Code";
            this.zip_code.Name = "zip_code";
            this.zip_code.ReadOnly = true;
            // 
            // ProvID
            // 
            this.ProvID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProvID.DataPropertyName = "ProvID";
            this.ProvID.HeaderText = "Province";
            this.ProvID.Name = "ProvID";
            this.ProvID.ReadOnly = true;
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
            // within_gma
            // 
            this.within_gma.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.within_gma.DataPropertyName = "within_gma";
            this.within_gma.FillWeight = 50F;
            this.within_gma.HeaderText = "Within GMA";
            this.within_gma.Name = "within_gma";
            this.within_gma.ReadOnly = true;
            this.within_gma.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
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
            this.LblTable.Size = new System.Drawing.Size(61, 19);
            this.LblTable.TabIndex = 0;
            this.LblTable.Text = "City List";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.PnlEncode);
            this.panel5.Controls.Add(this.panel11);
            this.panel5.Controls.Add(this.PnlFilter);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(0, 32);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
            this.panel5.Size = new System.Drawing.Size(358, 885);
            this.panel5.TabIndex = 0;
            // 
            // PnlEncode
            // 
            this.PnlEncode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlEncode.Controls.Add(this.CheckGma);
            this.PnlEncode.Controls.Add(this.ComboProvince);
            this.PnlEncode.Controls.Add(this.flowLayoutPanel1);
            this.PnlEncode.Controls.Add(this.TxtZip);
            this.PnlEncode.Controls.Add(this.label11);
            this.PnlEncode.Controls.Add(this.TxtCityName);
            this.PnlEncode.Controls.Add(this.flowLayoutPanel3);
            this.PnlEncode.Controls.Add(this.CheckActive);
            this.PnlEncode.Controls.Add(this.PnlHeaderEntry);
            this.PnlEncode.Controls.Add(this.BtnSave);
            this.PnlEncode.Controls.Add(this.BtnClearEncode);
            this.PnlEncode.Controls.Add(this.TxtCityID);
            this.PnlEncode.Controls.Add(this.flowLayoutPanel2);
            this.PnlEncode.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlEncode.Location = new System.Drawing.Point(10, 325);
            this.PnlEncode.Name = "PnlEncode";
            this.PnlEncode.Padding = new System.Windows.Forms.Padding(5, 35, 5, 5);
            this.PnlEncode.Size = new System.Drawing.Size(348, 369);
            this.PnlEncode.TabIndex = 46;
            // 
            // CheckGma
            // 
            this.CheckGma.AutoSize = true;
            this.CheckGma.backGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(208)))), ((int)(((byte)(0)))));
            this.CheckGma.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(208)))), ((int)(((byte)(0)))));
            this.CheckGma.checkColor = System.Drawing.Color.Empty;
            this.CheckGma.Dock = System.Windows.Forms.DockStyle.Top;
            this.CheckGma.Location = new System.Drawing.Point(5, 300);
            this.CheckGma.Name = "CheckGma";
            this.CheckGma.Padding = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.CheckGma.Size = new System.Drawing.Size(336, 29);
            this.CheckGma.TabIndex = 4;
            this.CheckGma.Text = "Within GMA";
            this.CheckGma.UseVisualStyleBackColor = true;
            // 
            // ComboProvince
            // 
            this.ComboProvince.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ComboProvince.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboProvince.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComboProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboProvince.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ComboProvince.FormattingEnabled = true;
            this.ComboProvince.Location = new System.Drawing.Point(5, 275);
            this.ComboProvince.Name = "ComboProvince";
            this.ComboProvince.Size = new System.Drawing.Size(336, 25);
            this.ComboProvince.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label9);
            this.flowLayoutPanel1.Controls.Add(this.label12);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 249);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(336, 26);
            this.flowLayoutPanel1.TabIndex = 86;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label9.Size = new System.Drawing.Size(53, 20);
            this.label9.TabIndex = 54;
            this.label9.Text = "Province";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.Crimson;
            this.label12.Location = new System.Drawing.Point(53, 0);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.MaximumSize = new System.Drawing.Size(15, 23);
            this.label12.MinimumSize = new System.Drawing.Size(15, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(15, 23);
            this.label12.TabIndex = 53;
            this.label12.Text = "*";
            // 
            // TxtZip
            // 
            this.TxtZip.BackColor = System.Drawing.SystemColors.Window;
            this.TxtZip.BorderColor = System.Drawing.Color.Silver;
            this.TxtZip.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtZip.BorderRadius = 8;
            this.TxtZip.BorderSize = 1;
            this.TxtZip.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtZip.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtZip.Location = new System.Drawing.Point(5, 217);
            this.TxtZip.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TxtZip.MaxLegnth = 10;
            this.TxtZip.MultiLine = false;
            this.TxtZip.Name = "TxtZip";
            this.TxtZip.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtZip.PasswordChar = false;
            this.TxtZip.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtZip.ReadOnly = false;
            this.TxtZip.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtZip.Size = new System.Drawing.Size(336, 32);
            this.TxtZip.TabIndex = 2;
            this.TxtZip.Textt = "";
            this.TxtZip.UnderlinedStyle = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Location = new System.Drawing.Point(5, 197);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label11.Size = new System.Drawing.Size(55, 20);
            this.label11.TabIndex = 83;
            this.label11.Text = "Zip Code";
            // 
            // TxtCityName
            // 
            this.TxtCityName.BackColor = System.Drawing.SystemColors.Window;
            this.TxtCityName.BorderColor = System.Drawing.Color.Silver;
            this.TxtCityName.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtCityName.BorderRadius = 8;
            this.TxtCityName.BorderSize = 1;
            this.TxtCityName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtCityName.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtCityName.Location = new System.Drawing.Point(5, 119);
            this.TxtCityName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TxtCityName.MaxLegnth = 100;
            this.TxtCityName.MultiLine = true;
            this.TxtCityName.Name = "TxtCityName";
            this.TxtCityName.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtCityName.PasswordChar = false;
            this.TxtCityName.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtCityName.ReadOnly = false;
            this.TxtCityName.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtCityName.Size = new System.Drawing.Size(336, 78);
            this.TxtCityName.TabIndex = 1;
            this.TxtCityName.Textt = "";
            this.TxtCityName.UnderlinedStyle = false;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.label7);
            this.flowLayoutPanel3.Controls.Add(this.label14);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(5, 93);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(336, 26);
            this.flowLayoutPanel3.TabIndex = 88;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label7.Size = new System.Drawing.Size(63, 20);
            this.label7.TabIndex = 54;
            this.label7.Text = "City Name";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.Crimson;
            this.label14.Location = new System.Drawing.Point(63, 0);
            this.label14.Margin = new System.Windows.Forms.Padding(0);
            this.label14.MaximumSize = new System.Drawing.Size(15, 23);
            this.label14.MinimumSize = new System.Drawing.Size(15, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 23);
            this.label14.TabIndex = 53;
            this.label14.Text = "*";
            // 
            // CheckActive
            // 
            this.CheckActive.AutoSize = true;
            this.CheckActive.backGroundColor = System.Drawing.Color.Empty;
            this.CheckActive.BackgroundColor = System.Drawing.Color.Empty;
            this.CheckActive.checkColor = System.Drawing.Color.Empty;
            this.CheckActive.Checked = true;
            this.CheckActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckActive.Location = new System.Drawing.Point(279, 35);
            this.CheckActive.Name = "CheckActive";
            this.CheckActive.Size = new System.Drawing.Size(59, 19);
            this.CheckActive.TabIndex = 82;
            this.CheckActive.Text = "Active";
            this.CheckActive.UseVisualStyleBackColor = true;
            // 
            // PnlHeaderEntry
            // 
            this.PnlHeaderEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlHeaderEntry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeaderEntry.Controls.Add(this.LblEncode);
            this.PnlHeaderEntry.Location = new System.Drawing.Point(-1, -1);
            this.PnlHeaderEntry.Name = "PnlHeaderEntry";
            this.PnlHeaderEntry.Size = new System.Drawing.Size(348, 32);
            this.PnlHeaderEntry.TabIndex = 47;
            // 
            // LblEncode
            // 
            this.LblEncode.AutoSize = true;
            this.LblEncode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblEncode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEncode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblEncode.Location = new System.Drawing.Point(5, 6);
            this.LblEncode.Name = "LblEncode";
            this.LblEncode.Size = new System.Drawing.Size(44, 19);
            this.LblEncode.TabIndex = 0;
            this.LblEncode.Text = "Entry";
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
            this.BtnSave.Location = new System.Drawing.Point(145, 333);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(95, 30);
            this.BtnSave.TabIndex = 36;
            this.BtnSave.TabStop = false;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnClearEncode
            // 
            this.BtnClearEncode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClearEncode.BackColor = System.Drawing.Color.MidnightBlue;
            this.BtnClearEncode.BorderColor = System.Drawing.Color.White;
            this.BtnClearEncode.BorderRadius = 8;
            this.BtnClearEncode.BorderSize = 0;
            this.BtnClearEncode.FlatAppearance.BorderSize = 0;
            this.BtnClearEncode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClearEncode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearEncode.ForeColor = System.Drawing.Color.White;
            this.BtnClearEncode.Image = ((System.Drawing.Image)(resources.GetObject("BtnClearEncode.Image")));
            this.BtnClearEncode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnClearEncode.Location = new System.Drawing.Point(246, 333);
            this.BtnClearEncode.Name = "BtnClearEncode";
            this.BtnClearEncode.Size = new System.Drawing.Size(95, 30);
            this.BtnClearEncode.TabIndex = 37;
            this.BtnClearEncode.TabStop = false;
            this.BtnClearEncode.Text = "C&lear";
            this.BtnClearEncode.UseVisualStyleBackColor = false;
            this.BtnClearEncode.Click += new System.EventHandler(this.BtnClearEncode_Click);
            // 
            // TxtCityID
            // 
            this.TxtCityID.BackColor = System.Drawing.SystemColors.Window;
            this.TxtCityID.BorderColor = System.Drawing.Color.Silver;
            this.TxtCityID.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtCityID.BorderRadius = 8;
            this.TxtCityID.BorderSize = 1;
            this.TxtCityID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtCityID.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtCityID.Location = new System.Drawing.Point(5, 61);
            this.TxtCityID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TxtCityID.MaxLegnth = 10;
            this.TxtCityID.MultiLine = false;
            this.TxtCityID.Name = "TxtCityID";
            this.TxtCityID.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtCityID.PasswordChar = false;
            this.TxtCityID.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtCityID.ReadOnly = false;
            this.TxtCityID.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtCityID.Size = new System.Drawing.Size(336, 32);
            this.TxtCityID.TabIndex = 0;
            this.TxtCityID.Textt = "";
            this.TxtCityID.UnderlinedStyle = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label8);
            this.flowLayoutPanel2.Controls.Add(this.label13);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(5, 35);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(336, 26);
            this.flowLayoutPanel2.TabIndex = 87;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label8.Size = new System.Drawing.Size(42, 20);
            this.label8.TabIndex = 54;
            this.label8.Text = "City ID";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.Color.Crimson;
            this.label13.Location = new System.Drawing.Point(42, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.MaximumSize = new System.Drawing.Size(15, 23);
            this.label13.MinimumSize = new System.Drawing.Size(15, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 23);
            this.label13.TabIndex = 53;
            this.label13.Text = "*";
            // 
            // panel11
            // 
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(10, 270);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(348, 55);
            this.panel11.TabIndex = 55;
            // 
            // PnlFilter
            // 
            this.PnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlFilter.Controls.Add(this.PnlHeaderFilter);
            this.PnlFilter.Controls.Add(this.CheckGmaFilter);
            this.PnlFilter.Controls.Add(this.BtnClear);
            this.PnlFilter.Controls.Add(this.BtnSearch);
            this.PnlFilter.Controls.Add(this.ComboProvinceFilter);
            this.PnlFilter.Controls.Add(this.label5);
            this.PnlFilter.Controls.Add(this.TxtZipFilter);
            this.PnlFilter.Controls.Add(this.label1);
            this.PnlFilter.Controls.Add(this.TxtCityNameFilter);
            this.PnlFilter.Controls.Add(this.label4);
            this.PnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlFilter.Location = new System.Drawing.Point(10, 10);
            this.PnlFilter.Name = "PnlFilter";
            this.PnlFilter.Padding = new System.Windows.Forms.Padding(5, 35, 5, 5);
            this.PnlFilter.Size = new System.Drawing.Size(348, 260);
            this.PnlFilter.TabIndex = 44;
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
            // CheckGmaFilter
            // 
            this.CheckGmaFilter.AutoSize = true;
            this.CheckGmaFilter.backGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(208)))), ((int)(((byte)(0)))));
            this.CheckGmaFilter.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(208)))), ((int)(((byte)(0)))));
            this.CheckGmaFilter.checkColor = System.Drawing.Color.Empty;
            this.CheckGmaFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.CheckGmaFilter.Location = new System.Drawing.Point(5, 184);
            this.CheckGmaFilter.Name = "CheckGmaFilter";
            this.CheckGmaFilter.Padding = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.CheckGmaFilter.Size = new System.Drawing.Size(336, 29);
            this.CheckGmaFilter.TabIndex = 3;
            this.CheckGmaFilter.Text = "Within GMA";
            this.CheckGmaFilter.UseVisualStyleBackColor = true;
            this.CheckGmaFilter.Visible = false;
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
            this.BtnClear.Image = ((System.Drawing.Image)(resources.GetObject("BtnClear.Image")));
            this.BtnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnClear.Location = new System.Drawing.Point(246, 223);
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
            this.BtnSearch.Location = new System.Drawing.Point(145, 223);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(95, 30);
            this.BtnSearch.TabIndex = 33;
            this.BtnSearch.TabStop = false;
            this.BtnSearch.Text = "S&earch";
            this.BtnSearch.UseVisualStyleBackColor = false;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // ComboProvinceFilter
            // 
            this.ComboProvinceFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ComboProvinceFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboProvinceFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComboProvinceFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboProvinceFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ComboProvinceFilter.FormattingEnabled = true;
            this.ComboProvinceFilter.Location = new System.Drawing.Point(5, 159);
            this.ComboProvinceFilter.Name = "ComboProvinceFilter";
            this.ComboProvinceFilter.Size = new System.Drawing.Size(336, 25);
            this.ComboProvinceFilter.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(5, 139);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label5.Size = new System.Drawing.Size(53, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "Province";
            // 
            // TxtZipFilter
            // 
            this.TxtZipFilter.BackColor = System.Drawing.SystemColors.Window;
            this.TxtZipFilter.BorderColor = System.Drawing.Color.Silver;
            this.TxtZipFilter.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtZipFilter.BorderRadius = 8;
            this.TxtZipFilter.BorderSize = 1;
            this.TxtZipFilter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtZipFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtZipFilter.Location = new System.Drawing.Point(5, 107);
            this.TxtZipFilter.MaxLegnth = 10;
            this.TxtZipFilter.MultiLine = false;
            this.TxtZipFilter.Name = "TxtZipFilter";
            this.TxtZipFilter.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtZipFilter.PasswordChar = false;
            this.TxtZipFilter.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtZipFilter.ReadOnly = false;
            this.TxtZipFilter.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtZipFilter.Size = new System.Drawing.Size(336, 32);
            this.TxtZipFilter.TabIndex = 1;
            this.TxtZipFilter.Textt = "";
            this.TxtZipFilter.UnderlinedStyle = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(5, 87);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 20;
            this.label1.Text = "Zip Code";
            // 
            // TxtCityNameFilter
            // 
            this.TxtCityNameFilter.BackColor = System.Drawing.SystemColors.Window;
            this.TxtCityNameFilter.BorderColor = System.Drawing.Color.Silver;
            this.TxtCityNameFilter.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtCityNameFilter.BorderRadius = 8;
            this.TxtCityNameFilter.BorderSize = 1;
            this.TxtCityNameFilter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtCityNameFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtCityNameFilter.Location = new System.Drawing.Point(5, 55);
            this.TxtCityNameFilter.MaxLegnth = 100;
            this.TxtCityNameFilter.MultiLine = false;
            this.TxtCityNameFilter.Name = "TxtCityNameFilter";
            this.TxtCityNameFilter.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtCityNameFilter.PasswordChar = false;
            this.TxtCityNameFilter.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtCityNameFilter.ReadOnly = false;
            this.TxtCityNameFilter.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtCityNameFilter.Size = new System.Drawing.Size(336, 32);
            this.TxtCityNameFilter.TabIndex = 0;
            this.TxtCityNameFilter.Textt = "";
            this.TxtCityNameFilter.UnderlinedStyle = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(5, 35);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "City Name";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.BtnClose);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.LblHeader);
            this.panel1.Controls.Add(this.PnlDesign);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(967, 32);
            this.panel1.TabIndex = 11;
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
            // panel9
            // 
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(957, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(10, 32);
            this.panel9.TabIndex = 51;
            // 
            // LblHeader
            // 
            this.LblHeader.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LblHeader.AutoSize = true;
            this.LblHeader.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblHeader.Location = new System.Drawing.Point(0, 5);
            this.LblHeader.Name = "LblHeader";
            this.LblHeader.Size = new System.Drawing.Size(189, 25);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "  CITY MASTERFILE  ";
            // 
            // PnlDesign
            // 
            this.PnlDesign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlDesign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.PnlDesign.Location = new System.Drawing.Point(-8, 17);
            this.PnlDesign.Name = "PnlDesign";
            this.PnlDesign.Size = new System.Drawing.Size(973, 4);
            this.PnlDesign.TabIndex = 50;
            // 
            // frm_city
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 917);
            this.Controls.Add(this.PnlMain);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frm_city";
            this.Text = "City Masterfile";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_city_KeyDown);
            this.PnlMain.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridCity)).EndInit();
            this.PnlHeaderTable.ResumeLayout(false);
            this.PnlHeaderTable.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.PnlEncode.ResumeLayout(false);
            this.PnlEncode.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.PnlHeaderEntry.ResumeLayout(false);
            this.PnlHeaderEntry.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.PnlFilter.ResumeLayout(false);
            this.PnlFilter.PerformLayout();
            this.PnlHeaderFilter.ResumeLayout(false);
            this.PnlHeaderFilter.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PnlMain;
        private System.Windows.Forms.Panel panel5;
        private Customized_Components.CustomRoundedButton BtnClear;
        private Customized_Components.CustomRoundedButton BtnSearch;
        private Customized_Components.CustomRoundedButton BtnSave;
        private System.Windows.Forms.Panel PnlFilter;
        private Customized_Components.CustomCheckbox CheckGmaFilter;
        private Customized_Components.RifinedCustomTextbox TxtZipFilter;
        private System.Windows.Forms.Label label1;
        private Customized_Components.RifinedCustomTextbox TxtCityNameFilter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel PnlEncode;
        private Customized_Components.CustomCheckbox CheckGma;
        private Customized_Components.RifinedCustomTextbox TxtCityName;
        private Customized_Components.RifinedCustomTextbox TxtCityID;
        private Customized_Components.CustomRoundedButton BtnClearEncode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel PnlDesign;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel PnlHeaderEntry;
        private System.Windows.Forms.Label LblEncode;
        private System.Windows.Forms.Panel PnlHeaderFilter;
        private System.Windows.Forms.Label LblFilter;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView DataGridCity;
        private System.Windows.Forms.Panel PnlHeaderTable;
        private System.Windows.Forms.Label LblTable;
        private Customized_Components.CustomCheckbox CheckActive;
        private Customized_Components.RifinedCustomTextbox TxtZip;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ComboProvince;
        private System.Windows.Forms.ComboBox ComboProvinceFilter;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private Customized_Components.CustomCloseButton BtnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn uniqueid;
        private System.Windows.Forms.DataGridViewTextBoxColumn CityName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CityID;
        private System.Windows.Forms.DataGridViewTextBoxColumn zip_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProvID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private System.Windows.Forms.DataGridViewCheckBoxColumn within_gma;
    }
}