namespace CARS.Components.Masterfiles
{
    partial class frm_oem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_oem));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel5 = new System.Windows.Forms.Panel();
            this.PnlEncode = new System.Windows.Forms.Panel();
            this.panel25 = new System.Windows.Forms.Panel();
            this.DataGridParts = new System.Windows.Forms.DataGridView();
            this.ToDelete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActivePart = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsNew = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PnlHeaderParts = new System.Windows.Forms.Panel();
            this.BtnAddPart = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnDeletePart = new CARS.Customized_Components.CustomRoundedButton();
            this.LblParts = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.ComboMake = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.CheckActive = new CARS.Customized_Components.CustomCheckbox();
            this.PnlHeaderEntry = new System.Windows.Forms.Panel();
            this.LblEncode = new System.Windows.Forms.Label();
            this.TxtOem = new CARS.Customized_Components.RifinedCustomTextbox();
            this.BtnSave = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnClearEncode = new CARS.Customized_Components.CustomRoundedButton();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.PnlFilter = new System.Windows.Forms.Panel();
            this.ComboMakeFilter = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.PnlHeaderFilter = new System.Windows.Forms.Panel();
            this.LblFilter = new System.Windows.Forms.Label();
            this.TxtOemFilter = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnClear = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnSearch = new CARS.Customized_Components.CustomRoundedButton();
            this.PnlMain = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.DataGridOem = new System.Windows.Forms.DataGridView();
            this.UniqueID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MakeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BOwn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MakeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PnlHeaderTable = new System.Windows.Forms.Panel();
            this.LblTable = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnClose = new CARS.Customized_Components.CustomCloseButton();
            this.panel9 = new System.Windows.Forms.Panel();
            this.LblHeader = new System.Windows.Forms.Label();
            this.PnlDesign = new System.Windows.Forms.Panel();
            this.panel5.SuspendLayout();
            this.PnlEncode.SuspendLayout();
            this.panel25.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridParts)).BeginInit();
            this.PnlHeaderParts.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.PnlHeaderEntry.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.PnlFilter.SuspendLayout();
            this.PnlHeaderFilter.SuspendLayout();
            this.PnlMain.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridOem)).BeginInit();
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
            this.PnlEncode.Controls.Add(this.panel25);
            this.PnlEncode.Controls.Add(this.panel12);
            this.PnlEncode.Controls.Add(this.ComboMake);
            this.PnlEncode.Controls.Add(this.flowLayoutPanel1);
            this.PnlEncode.Controls.Add(this.CheckActive);
            this.PnlEncode.Controls.Add(this.PnlHeaderEntry);
            this.PnlEncode.Controls.Add(this.TxtOem);
            this.PnlEncode.Controls.Add(this.BtnSave);
            this.PnlEncode.Controls.Add(this.BtnClearEncode);
            this.PnlEncode.Controls.Add(this.flowLayoutPanel2);
            this.PnlEncode.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlEncode.Location = new System.Drawing.Point(10, 254);
            this.PnlEncode.Name = "PnlEncode";
            this.PnlEncode.Padding = new System.Windows.Forms.Padding(5, 35, 5, 5);
            this.PnlEncode.Size = new System.Drawing.Size(348, 471);
            this.PnlEncode.TabIndex = 49;
            // 
            // panel25
            // 
            this.panel25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel25.Controls.Add(this.DataGridParts);
            this.panel25.Controls.Add(this.PnlHeaderParts);
            this.panel25.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel25.Location = new System.Drawing.Point(5, 200);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(336, 229);
            this.panel25.TabIndex = 84;
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
            this.ToDelete,
            this.PartNo,
            this.IsActivePart,
            this.IsNew});
            this.DataGridParts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridParts.Location = new System.Drawing.Point(0, 32);
            this.DataGridParts.Name = "DataGridParts";
            this.DataGridParts.RowHeadersVisible = false;
            this.DataGridParts.Size = new System.Drawing.Size(334, 195);
            this.DataGridParts.TabIndex = 2;
            this.DataGridParts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridParts_CellContentClick);
            // 
            // ToDelete
            // 
            this.ToDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ToDelete.FillWeight = 10F;
            this.ToDelete.HeaderText = "";
            this.ToDelete.Name = "ToDelete";
            this.ToDelete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // PartNo
            // 
            this.PartNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PartNo.DataPropertyName = "PartNo";
            this.PartNo.HeaderText = "Part No.";
            this.PartNo.Name = "PartNo";
            // 
            // IsActivePart
            // 
            this.IsActivePart.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IsActivePart.DataPropertyName = "IsActivePart";
            this.IsActivePart.FillWeight = 20F;
            this.IsActivePart.HeaderText = "Active";
            this.IsActivePart.Name = "IsActivePart";
            this.IsActivePart.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IsNew
            // 
            this.IsNew.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IsNew.DataPropertyName = "IsNew";
            this.IsNew.FillWeight = 20F;
            this.IsNew.HeaderText = "New";
            this.IsNew.Name = "IsNew";
            this.IsNew.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsNew.Visible = false;
            // 
            // PnlHeaderParts
            // 
            this.PnlHeaderParts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeaderParts.Controls.Add(this.BtnAddPart);
            this.PnlHeaderParts.Controls.Add(this.BtnDeletePart);
            this.PnlHeaderParts.Controls.Add(this.LblParts);
            this.PnlHeaderParts.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlHeaderParts.Location = new System.Drawing.Point(0, 0);
            this.PnlHeaderParts.Name = "PnlHeaderParts";
            this.PnlHeaderParts.Size = new System.Drawing.Size(334, 32);
            this.PnlHeaderParts.TabIndex = 71;
            // 
            // BtnAddPart
            // 
            this.BtnAddPart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAddPart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(122)))));
            this.BtnAddPart.BorderColor = System.Drawing.Color.White;
            this.BtnAddPart.BorderRadius = 8;
            this.BtnAddPart.BorderSize = 0;
            this.BtnAddPart.FlatAppearance.BorderSize = 0;
            this.BtnAddPart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAddPart.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAddPart.ForeColor = System.Drawing.Color.White;
            this.BtnAddPart.Location = new System.Drawing.Point(195, 2);
            this.BtnAddPart.Name = "BtnAddPart";
            this.BtnAddPart.Size = new System.Drawing.Size(63, 27);
            this.BtnAddPart.TabIndex = 25;
            this.BtnAddPart.TabStop = false;
            this.BtnAddPart.Text = "&1.) Add";
            this.BtnAddPart.UseVisualStyleBackColor = false;
            this.BtnAddPart.Click += new System.EventHandler(this.BtnAddPart_Click);
            // 
            // BtnDeletePart
            // 
            this.BtnDeletePart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnDeletePart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(122)))));
            this.BtnDeletePart.BorderColor = System.Drawing.Color.White;
            this.BtnDeletePart.BorderRadius = 8;
            this.BtnDeletePart.BorderSize = 0;
            this.BtnDeletePart.FlatAppearance.BorderSize = 0;
            this.BtnDeletePart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDeletePart.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeletePart.ForeColor = System.Drawing.Color.White;
            this.BtnDeletePart.Location = new System.Drawing.Point(264, 2);
            this.BtnDeletePart.Name = "BtnDeletePart";
            this.BtnDeletePart.Size = new System.Drawing.Size(63, 27);
            this.BtnDeletePart.TabIndex = 26;
            this.BtnDeletePart.TabStop = false;
            this.BtnDeletePart.Text = "&2.) Delete";
            this.BtnDeletePart.UseVisualStyleBackColor = false;
            this.BtnDeletePart.Click += new System.EventHandler(this.BtnDeletePart_Click);
            // 
            // LblParts
            // 
            this.LblParts.AutoSize = true;
            this.LblParts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblParts.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblParts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblParts.Location = new System.Drawing.Point(5, 6);
            this.LblParts.Name = "LblParts";
            this.LblParts.Size = new System.Drawing.Size(43, 19);
            this.LblParts.TabIndex = 0;
            this.LblParts.Text = "Parts";
            // 
            // panel12
            // 
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(5, 190);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(336, 10);
            this.panel12.TabIndex = 85;
            // 
            // ComboMake
            // 
            this.ComboMake.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ComboMake.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboMake.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComboMake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboMake.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ComboMake.FormattingEnabled = true;
            this.ComboMake.Location = new System.Drawing.Point(5, 165);
            this.ComboMake.Name = "ComboMake";
            this.ComboMake.Size = new System.Drawing.Size(336, 25);
            this.ComboMake.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.label9);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 139);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(336, 26);
            this.flowLayoutPanel1.TabIndex = 88;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label5.Size = new System.Drawing.Size(36, 20);
            this.label5.TabIndex = 83;
            this.label5.Text = "Make";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Crimson;
            this.label9.Location = new System.Drawing.Point(36, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.MaximumSize = new System.Drawing.Size(15, 23);
            this.label9.MinimumSize = new System.Drawing.Size(15, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 23);
            this.label9.TabIndex = 55;
            this.label9.Text = "*";
            // 
            // CheckActive
            // 
            this.CheckActive.AutoSize = true;
            this.CheckActive.backGroundColor = System.Drawing.Color.Empty;
            this.CheckActive.BackgroundColor = System.Drawing.Color.Empty;
            this.CheckActive.checkColor = System.Drawing.Color.Empty;
            this.CheckActive.Checked = true;
            this.CheckActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckActive.Location = new System.Drawing.Point(282, 35);
            this.CheckActive.Name = "CheckActive";
            this.CheckActive.Size = new System.Drawing.Size(59, 19);
            this.CheckActive.TabIndex = 81;
            this.CheckActive.TabStop = false;
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
            this.PnlHeaderEntry.TabIndex = 55;
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
            // TxtOem
            // 
            this.TxtOem.BackColor = System.Drawing.SystemColors.Window;
            this.TxtOem.BorderColor = System.Drawing.Color.Silver;
            this.TxtOem.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtOem.BorderRadius = 8;
            this.TxtOem.BorderSize = 1;
            this.TxtOem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtOem.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtOem.Location = new System.Drawing.Point(5, 61);
            this.TxtOem.MaxLegnth = 100;
            this.TxtOem.MultiLine = true;
            this.TxtOem.Name = "TxtOem";
            this.TxtOem.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtOem.PasswordChar = false;
            this.TxtOem.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtOem.ReadOnly = false;
            this.TxtOem.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtOem.Size = new System.Drawing.Size(336, 78);
            this.TxtOem.TabIndex = 0;
            this.TxtOem.Textt = "";
            this.TxtOem.UnderlinedStyle = false;
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
            this.BtnSave.Location = new System.Drawing.Point(145, 435);
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
            this.BtnClearEncode.Location = new System.Drawing.Point(246, 435);
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
            this.flowLayoutPanel2.Controls.Add(this.label4);
            this.flowLayoutPanel2.Controls.Add(this.label11);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(5, 35);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(336, 26);
            this.flowLayoutPanel2.TabIndex = 87;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label4.Size = new System.Drawing.Size(55, 20);
            this.label4.TabIndex = 56;
            this.label4.Text = "OEM No.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.Crimson;
            this.label11.Location = new System.Drawing.Point(55, 0);
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
            this.panel11.Location = new System.Drawing.Point(10, 199);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(348, 55);
            this.panel11.TabIndex = 54;
            // 
            // PnlFilter
            // 
            this.PnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlFilter.Controls.Add(this.ComboMakeFilter);
            this.PnlFilter.Controls.Add(this.label7);
            this.PnlFilter.Controls.Add(this.PnlHeaderFilter);
            this.PnlFilter.Controls.Add(this.TxtOemFilter);
            this.PnlFilter.Controls.Add(this.label1);
            this.PnlFilter.Controls.Add(this.BtnClear);
            this.PnlFilter.Controls.Add(this.BtnSearch);
            this.PnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlFilter.Location = new System.Drawing.Point(10, 10);
            this.PnlFilter.Name = "PnlFilter";
            this.PnlFilter.Padding = new System.Windows.Forms.Padding(5, 35, 5, 5);
            this.PnlFilter.Size = new System.Drawing.Size(348, 189);
            this.PnlFilter.TabIndex = 44;
            // 
            // ComboMakeFilter
            // 
            this.ComboMakeFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ComboMakeFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboMakeFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComboMakeFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboMakeFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ComboMakeFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ComboMakeFilter.FormattingEnabled = true;
            this.ComboMakeFilter.Location = new System.Drawing.Point(5, 107);
            this.ComboMakeFilter.Name = "ComboMakeFilter";
            this.ComboMakeFilter.Size = new System.Drawing.Size(336, 25);
            this.ComboMakeFilter.TabIndex = 87;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(5, 87);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label7.Size = new System.Drawing.Size(36, 20);
            this.label7.TabIndex = 52;
            this.label7.Text = "Make";
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
            // TxtOemFilter
            // 
            this.TxtOemFilter.BackColor = System.Drawing.SystemColors.Window;
            this.TxtOemFilter.BorderColor = System.Drawing.Color.Silver;
            this.TxtOemFilter.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtOemFilter.BorderRadius = 8;
            this.TxtOemFilter.BorderSize = 1;
            this.TxtOemFilter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtOemFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtOemFilter.Location = new System.Drawing.Point(5, 55);
            this.TxtOemFilter.MaxLegnth = 100;
            this.TxtOemFilter.MultiLine = false;
            this.TxtOemFilter.Name = "TxtOemFilter";
            this.TxtOemFilter.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtOemFilter.PasswordChar = false;
            this.TxtOemFilter.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtOemFilter.ReadOnly = false;
            this.TxtOemFilter.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtOemFilter.Size = new System.Drawing.Size(336, 32);
            this.TxtOemFilter.TabIndex = 50;
            this.TxtOemFilter.Textt = "";
            this.TxtOemFilter.UnderlinedStyle = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(5, 35);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 49;
            this.label1.Text = "OEM No.";
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
            this.BtnClear.Location = new System.Drawing.Point(246, 152);
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
            this.BtnSearch.Location = new System.Drawing.Point(145, 152);
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
            this.PnlMain.Controls.Add(this.panel4);
            this.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMain.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlMain.Location = new System.Drawing.Point(358, 32);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Padding = new System.Windows.Forms.Padding(10, 10, 10, 20);
            this.PnlMain.Size = new System.Drawing.Size(609, 885);
            this.PnlMain.TabIndex = 14;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.DataGridOem);
            this.panel4.Controls.Add(this.PnlHeaderTable);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(10, 10);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(589, 855);
            this.panel4.TabIndex = 0;
            // 
            // DataGridOem
            // 
            this.DataGridOem.AllowUserToAddRows = false;
            this.DataGridOem.AllowUserToDeleteRows = false;
            this.DataGridOem.AllowUserToResizeColumns = false;
            this.DataGridOem.AllowUserToResizeRows = false;
            this.DataGridOem.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.DataGridOem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridOem.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridOem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridOem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridOem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UniqueID,
            this.OemNo,
            this.MakeName,
            this.BOwn,
            this.IsActive,
            this.MakeID});
            this.DataGridOem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridOem.Location = new System.Drawing.Point(0, 32);
            this.DataGridOem.MultiSelect = false;
            this.DataGridOem.Name = "DataGridOem";
            this.DataGridOem.ReadOnly = true;
            this.DataGridOem.RowHeadersVisible = false;
            this.DataGridOem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridOem.Size = new System.Drawing.Size(587, 821);
            this.DataGridOem.TabIndex = 5;
            this.DataGridOem.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridOem_ColumnHeaderMouseClick);
            this.DataGridOem.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridOem_RowEnter);
            // 
            // UniqueID
            // 
            this.UniqueID.DataPropertyName = "UniqueID";
            this.UniqueID.HeaderText = "uniqueid";
            this.UniqueID.Name = "UniqueID";
            this.UniqueID.ReadOnly = true;
            this.UniqueID.Visible = false;
            // 
            // OemNo
            // 
            this.OemNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OemNo.DataPropertyName = "OemNo";
            this.OemNo.FillWeight = 180F;
            this.OemNo.HeaderText = "OEM No.";
            this.OemNo.Name = "OemNo";
            this.OemNo.ReadOnly = true;
            // 
            // MakeName
            // 
            this.MakeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MakeName.DataPropertyName = "MakeName";
            this.MakeName.FillWeight = 180F;
            this.MakeName.HeaderText = "Vehicle Make";
            this.MakeName.Name = "MakeName";
            this.MakeName.ReadOnly = true;
            // 
            // BOwn
            // 
            this.BOwn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BOwn.DataPropertyName = "BOwn";
            this.BOwn.FillWeight = 20F;
            this.BOwn.HeaderText = "BSB";
            this.BOwn.Name = "BOwn";
            this.BOwn.ReadOnly = true;
            this.BOwn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IsActive
            // 
            this.IsActive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IsActive.DataPropertyName = "IsActive";
            this.IsActive.FillWeight = 20F;
            this.IsActive.HeaderText = "Active";
            this.IsActive.Name = "IsActive";
            this.IsActive.ReadOnly = true;
            this.IsActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // MakeID
            // 
            this.MakeID.DataPropertyName = "MakeID";
            this.MakeID.HeaderText = "Make ID";
            this.MakeID.Name = "MakeID";
            this.MakeID.ReadOnly = true;
            this.MakeID.Visible = false;
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
            this.LblTable.Size = new System.Drawing.Size(66, 19);
            this.LblTable.TabIndex = 0;
            this.LblTable.Text = "OEM List";
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
            this.panel1.TabIndex = 15;
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
            this.LblHeader.Size = new System.Drawing.Size(511, 25);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "  ORIGINAL EQUIPMENT MANUFACTURER MASTERFILE  ";
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
            // frm_oem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 917);
            this.Controls.Add(this.PnlMain);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frm_oem";
            this.Text = "Oem Masterfile";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_oem_KeyDown);
            this.panel5.ResumeLayout(false);
            this.PnlEncode.ResumeLayout(false);
            this.PnlEncode.PerformLayout();
            this.panel25.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridParts)).EndInit();
            this.PnlHeaderParts.ResumeLayout(false);
            this.PnlHeaderParts.PerformLayout();
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
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridOem)).EndInit();
            this.PnlHeaderTable.ResumeLayout(false);
            this.PnlHeaderTable.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel PnlEncode;
        private Customized_Components.CustomRoundedButton BtnSave;
        private Customized_Components.CustomRoundedButton BtnClearEncode;
        private System.Windows.Forms.Panel PnlFilter;
        private Customized_Components.CustomRoundedButton BtnClear;
        private Customized_Components.CustomRoundedButton BtnSearch;
        private System.Windows.Forms.Panel PnlMain;
        private Customized_Components.RifinedCustomTextbox TxtOemFilter;
        private System.Windows.Forms.Label label1;
        private Customized_Components.RifinedCustomTextbox TxtOem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel PnlDesign;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel PnlHeaderEntry;
        private System.Windows.Forms.Label LblEncode;
        private System.Windows.Forms.Panel PnlHeaderFilter;
        private System.Windows.Forms.Label LblFilter;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView DataGridOem;
        private System.Windows.Forms.Panel PnlHeaderTable;
        private System.Windows.Forms.Label LblTable;
        private Customized_Components.CustomCheckbox CheckActive;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel25;
        private System.Windows.Forms.DataGridView DataGridParts;
        private System.Windows.Forms.Panel PnlHeaderParts;
        private Customized_Components.CustomRoundedButton BtnAddPart;
        private Customized_Components.CustomRoundedButton BtnDeletePart;
        private System.Windows.Forms.Label LblParts;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.ComboBox ComboMake;
        private System.Windows.Forms.ComboBox ComboMakeFilter;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn UniqueID;
        private System.Windows.Forms.DataGridViewTextBoxColumn OemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn MakeName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn BOwn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn MakeID;
        private Customized_Components.CustomCloseButton BtnClose;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ToDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActivePart;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsNew;
    }
}