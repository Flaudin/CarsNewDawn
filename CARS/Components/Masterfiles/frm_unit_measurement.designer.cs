namespace CARS.Components.Masterfiles
{
    partial class frm_unit_measurement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_unit_measurement));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel5 = new System.Windows.Forms.Panel();
            this.PnlEncode = new System.Windows.Forms.Panel();
            this.TxtUomName = new CARS.Customized_Components.RifinedCustomTextbox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.CheckActive = new CARS.Customized_Components.CustomCheckbox();
            this.PnlHeaderEntry = new System.Windows.Forms.Panel();
            this.LblEncode = new System.Windows.Forms.Label();
            this.TxtUomID = new CARS.Customized_Components.RifinedCustomTextbox();
            this.BtnSave = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnClearEncode = new CARS.Customized_Components.CustomRoundedButton();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.PnlFilter = new System.Windows.Forms.Panel();
            this.TxtUomNameFilter = new CARS.Customized_Components.RifinedCustomTextbox();
            this.PnlHeaderFilter = new System.Windows.Forms.Panel();
            this.LblFilter = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnClear = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnSearch = new CARS.Customized_Components.CustomRoundedButton();
            this.PnlMain = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DataGridMeasurement = new System.Windows.Forms.DataGridView();
            this.uniqueid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UomID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UomName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PnlHeaderTable = new System.Windows.Forms.Panel();
            this.LblTable = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnClose = new CARS.Customized_Components.CustomCloseButton();
            this.panel9 = new System.Windows.Forms.Panel();
            this.LblHeader = new System.Windows.Forms.Label();
            this.PnlDesign = new System.Windows.Forms.Panel();
            this.panel5.SuspendLayout();
            this.PnlEncode.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.PnlHeaderEntry.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.PnlFilter.SuspendLayout();
            this.PnlHeaderFilter.SuspendLayout();
            this.PnlMain.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridMeasurement)).BeginInit();
            this.PnlHeaderTable.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.panel5.TabIndex = 13;
            // 
            // PnlEncode
            // 
            this.PnlEncode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlEncode.Controls.Add(this.TxtUomName);
            this.PnlEncode.Controls.Add(this.flowLayoutPanel1);
            this.PnlEncode.Controls.Add(this.CheckActive);
            this.PnlEncode.Controls.Add(this.PnlHeaderEntry);
            this.PnlEncode.Controls.Add(this.TxtUomID);
            this.PnlEncode.Controls.Add(this.BtnSave);
            this.PnlEncode.Controls.Add(this.BtnClearEncode);
            this.PnlEncode.Controls.Add(this.flowLayoutPanel2);
            this.PnlEncode.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlEncode.Location = new System.Drawing.Point(10, 205);
            this.PnlEncode.Name = "PnlEncode";
            this.PnlEncode.Padding = new System.Windows.Forms.Padding(5, 35, 5, 5);
            this.PnlEncode.Size = new System.Drawing.Size(348, 194);
            this.PnlEncode.TabIndex = 49;
            // 
            // TxtUomName
            // 
            this.TxtUomName.BackColor = System.Drawing.SystemColors.Window;
            this.TxtUomName.BorderColor = System.Drawing.Color.Silver;
            this.TxtUomName.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtUomName.BorderRadius = 8;
            this.TxtUomName.BorderSize = 1;
            this.TxtUomName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtUomName.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtUomName.Location = new System.Drawing.Point(5, 119);
            this.TxtUomName.MaxLegnth = 10;
            this.TxtUomName.MultiLine = false;
            this.TxtUomName.Name = "TxtUomName";
            this.TxtUomName.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtUomName.PasswordChar = false;
            this.TxtUomName.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtUomName.ReadOnly = false;
            this.TxtUomName.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtUomName.Size = new System.Drawing.Size(336, 32);
            this.TxtUomName.TabIndex = 54;
            this.TxtUomName.Textt = "";
            this.TxtUomName.UnderlinedStyle = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.label7);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 93);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(336, 26);
            this.flowLayoutPanel1.TabIndex = 91;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label4.Size = new System.Drawing.Size(154, 20);
            this.label4.TabIndex = 56;
            this.label4.Text = "Unit of Measurement Name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Crimson;
            this.label7.Location = new System.Drawing.Point(160, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.MaximumSize = new System.Drawing.Size(15, 23);
            this.label7.MinimumSize = new System.Drawing.Size(15, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 23);
            this.label7.TabIndex = 55;
            this.label7.Text = "*";
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
            this.CheckActive.TabIndex = 83;
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
            this.PnlHeaderEntry.TabIndex = 50;
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
            // TxtUomID
            // 
            this.TxtUomID.BackColor = System.Drawing.SystemColors.Window;
            this.TxtUomID.BorderColor = System.Drawing.Color.Silver;
            this.TxtUomID.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtUomID.BorderRadius = 8;
            this.TxtUomID.BorderSize = 1;
            this.TxtUomID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtUomID.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtUomID.Location = new System.Drawing.Point(5, 61);
            this.TxtUomID.MaxLegnth = 10;
            this.TxtUomID.MultiLine = false;
            this.TxtUomID.Name = "TxtUomID";
            this.TxtUomID.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtUomID.PasswordChar = false;
            this.TxtUomID.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtUomID.ReadOnly = false;
            this.TxtUomID.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtUomID.Size = new System.Drawing.Size(336, 32);
            this.TxtUomID.TabIndex = 52;
            this.TxtUomID.Textt = "";
            this.TxtUomID.UnderlinedStyle = false;
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
            this.BtnSave.Location = new System.Drawing.Point(145, 158);
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
            this.BtnClearEncode.Location = new System.Drawing.Point(246, 158);
            this.BtnClearEncode.Name = "BtnClearEncode";
            this.BtnClearEncode.Size = new System.Drawing.Size(95, 30);
            this.BtnClearEncode.TabIndex = 37;
            this.BtnClearEncode.TabStop = false;
            this.BtnClearEncode.Text = "C&lear";
            this.BtnClearEncode.UseVisualStyleBackColor = false;
            this.BtnClearEncode.Click += new System.EventHandler(this.BtnClearEncode_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label5);
            this.flowLayoutPanel2.Controls.Add(this.label11);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(5, 35);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(336, 26);
            this.flowLayoutPanel2.TabIndex = 90;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label5.Size = new System.Drawing.Size(133, 20);
            this.label5.TabIndex = 56;
            this.label5.Text = "Unit of Measurement ID";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.Crimson;
            this.label11.Location = new System.Drawing.Point(139, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.MaximumSize = new System.Drawing.Size(15, 23);
            this.label11.MinimumSize = new System.Drawing.Size(15, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 23);
            this.label11.TabIndex = 55;
            this.label11.Text = "*";
            // 
            // panel11
            // 
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(10, 150);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(348, 55);
            this.panel11.TabIndex = 58;
            // 
            // PnlFilter
            // 
            this.PnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlFilter.Controls.Add(this.TxtUomNameFilter);
            this.PnlFilter.Controls.Add(this.PnlHeaderFilter);
            this.PnlFilter.Controls.Add(this.label1);
            this.PnlFilter.Controls.Add(this.BtnClear);
            this.PnlFilter.Controls.Add(this.BtnSearch);
            this.PnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlFilter.Location = new System.Drawing.Point(10, 10);
            this.PnlFilter.Name = "PnlFilter";
            this.PnlFilter.Padding = new System.Windows.Forms.Padding(5, 35, 5, 5);
            this.PnlFilter.Size = new System.Drawing.Size(348, 140);
            this.PnlFilter.TabIndex = 44;
            // 
            // TxtUomNameFilter
            // 
            this.TxtUomNameFilter.BackColor = System.Drawing.SystemColors.Window;
            this.TxtUomNameFilter.BorderColor = System.Drawing.Color.Silver;
            this.TxtUomNameFilter.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtUomNameFilter.BorderRadius = 8;
            this.TxtUomNameFilter.BorderSize = 1;
            this.TxtUomNameFilter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtUomNameFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtUomNameFilter.Location = new System.Drawing.Point(5, 55);
            this.TxtUomNameFilter.MaxLegnth = 10;
            this.TxtUomNameFilter.MultiLine = false;
            this.TxtUomNameFilter.Name = "TxtUomNameFilter";
            this.TxtUomNameFilter.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtUomNameFilter.PasswordChar = false;
            this.TxtUomNameFilter.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtUomNameFilter.ReadOnly = false;
            this.TxtUomNameFilter.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtUomNameFilter.Size = new System.Drawing.Size(336, 32);
            this.TxtUomNameFilter.TabIndex = 50;
            this.TxtUomNameFilter.Textt = "";
            this.TxtUomNameFilter.UnderlinedStyle = false;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(5, 35);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(154, 20);
            this.label1.TabIndex = 49;
            this.label1.Text = "Unit of Measurement Name";
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
            this.BtnClear.Location = new System.Drawing.Point(246, 103);
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
            this.BtnSearch.Location = new System.Drawing.Point(145, 103);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(95, 30);
            this.BtnSearch.TabIndex = 33;
            this.BtnSearch.TabStop = false;
            this.BtnSearch.Text = "S&earch";
            this.BtnSearch.UseVisualStyleBackColor = false;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
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
            this.PnlMain.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.DataGridMeasurement);
            this.panel3.Controls.Add(this.PnlHeaderTable);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(10, 10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(589, 855);
            this.panel3.TabIndex = 0;
            // 
            // DataGridMeasurement
            // 
            this.DataGridMeasurement.AllowUserToAddRows = false;
            this.DataGridMeasurement.AllowUserToDeleteRows = false;
            this.DataGridMeasurement.AllowUserToResizeColumns = false;
            this.DataGridMeasurement.AllowUserToResizeRows = false;
            this.DataGridMeasurement.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.DataGridMeasurement.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridMeasurement.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridMeasurement.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridMeasurement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridMeasurement.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.uniqueid,
            this.UomID,
            this.UomName,
            this.IsActive});
            this.DataGridMeasurement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridMeasurement.Location = new System.Drawing.Point(0, 32);
            this.DataGridMeasurement.MultiSelect = false;
            this.DataGridMeasurement.Name = "DataGridMeasurement";
            this.DataGridMeasurement.ReadOnly = true;
            this.DataGridMeasurement.RowHeadersVisible = false;
            this.DataGridMeasurement.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridMeasurement.Size = new System.Drawing.Size(587, 821);
            this.DataGridMeasurement.TabIndex = 5;
            this.DataGridMeasurement.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridMeasurement_ColumnHeaderMouseClick);
            this.DataGridMeasurement.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridMeasurement_RowEnter);
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
            // UomID
            // 
            this.UomID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UomID.DataPropertyName = "UomID";
            this.UomID.HeaderText = "Unit of Measurement ID";
            this.UomID.Name = "UomID";
            this.UomID.ReadOnly = true;
            // 
            // UomName
            // 
            this.UomName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UomName.DataPropertyName = "UomName";
            this.UomName.HeaderText = "Unit of Measurement Name";
            this.UomName.Name = "UomName";
            this.UomName.ReadOnly = true;
            // 
            // IsActive
            // 
            this.IsActive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IsActive.DataPropertyName = "IsActive";
            this.IsActive.FillWeight = 20F;
            this.IsActive.HeaderText = "Active";
            this.IsActive.Name = "IsActive";
            this.IsActive.ReadOnly = true;
            this.IsActive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
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
            this.LblTable.Size = new System.Drawing.Size(175, 19);
            this.LblTable.TabIndex = 0;
            this.LblTable.Text = "Unit of Measurement List";
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
            this.panel1.TabIndex = 16;
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
            this.LblHeader.Size = new System.Drawing.Size(370, 25);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "  UNIT OF MEASUREMENT MASTERFILE  ";
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
            // frm_unit_measurement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 917);
            this.Controls.Add(this.PnlMain);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frm_unit_measurement";
            this.Text = "Uom Masterfile";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_unit_measurement_KeyDown);
            this.panel5.ResumeLayout(false);
            this.PnlEncode.ResumeLayout(false);
            this.PnlEncode.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.PnlHeaderEntry.ResumeLayout(false);
            this.PnlHeaderEntry.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.PnlFilter.ResumeLayout(false);
            this.PnlFilter.PerformLayout();
            this.PnlHeaderFilter.ResumeLayout(false);
            this.PnlHeaderFilter.PerformLayout();
            this.PnlMain.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridMeasurement)).EndInit();
            this.PnlHeaderTable.ResumeLayout(false);
            this.PnlHeaderTable.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel PnlHeaderEntry;
        private System.Windows.Forms.Label LblEncode;
        private System.Windows.Forms.Panel PnlEncode;
        private Customized_Components.CustomRoundedButton BtnSave;
        private Customized_Components.CustomRoundedButton BtnClearEncode;
        private System.Windows.Forms.Panel PnlFilter;
        private Customized_Components.CustomRoundedButton BtnClear;
        private Customized_Components.CustomRoundedButton BtnSearch;
        private System.Windows.Forms.Panel PnlHeaderFilter;
        private System.Windows.Forms.Label LblFilter;
        private System.Windows.Forms.Panel PnlMain;
        private Customized_Components.RifinedCustomTextbox TxtUomNameFilter;
        private System.Windows.Forms.Label label1;
        private Customized_Components.RifinedCustomTextbox TxtUomName;
        private Customized_Components.RifinedCustomTextbox TxtUomID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel PnlDesign;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView DataGridMeasurement;
        private System.Windows.Forms.Panel PnlHeaderTable;
        private System.Windows.Forms.Label LblTable;
        private Customized_Components.CustomCheckbox CheckActive;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn uniqueid;
        private System.Windows.Forms.DataGridViewTextBoxColumn UomID;
        private System.Windows.Forms.DataGridViewTextBoxColumn UomName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private Customized_Components.CustomCloseButton BtnClose;
    }
}