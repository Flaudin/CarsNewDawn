namespace CARS.Components.Utilities
{
    partial class frm_user_profile
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtnEditNew = new CARS.Customized_Components.CustomCloseButton();
            this.BtnLog = new CARS.Customized_Components.CustomCloseButton();
            this.BtnClose = new CARS.Customized_Components.CustomCloseButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.LblHeader = new System.Windows.Forms.Label();
            this.PnlDesign = new System.Windows.Forms.Panel();
            this.PnlMain = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DataGridEmployee = new System.Windows.Forms.DataGridView();
            this.EmployeeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateHired = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PosName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmploymentStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BsbAppUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateOfBirth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PnlHeaderTable = new System.Windows.Forms.Panel();
            this.LblTable = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.BtnTranLog = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnEdit = new CARS.Customized_Components.CustomRoundedButton();
            this.PnlFilter = new System.Windows.Forms.Panel();
            this.ComboPositionFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ComboDeptFilter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtEmployeeFilter = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label4 = new System.Windows.Forms.Label();
            this.PnlHeaderFilter = new System.Windows.Forms.Panel();
            this.LblFilter = new System.Windows.Forms.Label();
            this.BtnClear = new CARS.Customized_Components.CustomRoundedButton();
            this.BtnSearch = new CARS.Customized_Components.CustomRoundedButton();
            this.panel3.SuspendLayout();
            this.PnlMain.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridEmployee)).BeginInit();
            this.PnlHeaderTable.SuspendLayout();
            this.panel8.SuspendLayout();
            this.PnlFilter.SuspendLayout();
            this.PnlHeaderFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.BtnEditNew);
            this.panel3.Controls.Add(this.BtnLog);
            this.panel3.Controls.Add(this.BtnClose);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.LblHeader);
            this.panel3.Controls.Add(this.PnlDesign);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(967, 32);
            this.panel3.TabIndex = 50;
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
            this.BtnEditNew.Location = new System.Drawing.Point(723, 3);
            this.BtnEditNew.Name = "BtnEditNew";
            this.BtnEditNew.Size = new System.Drawing.Size(95, 29);
            this.BtnEditNew.TabIndex = 62;
            this.BtnEditNew.Text = "&Edit";
            this.BtnEditNew.UseVisualStyleBackColor = false;
            this.BtnEditNew.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnLog
            // 
            this.BtnLog.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnLog.BorderRadius = 0;
            this.BtnLog.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnLog.FlatAppearance.BorderSize = 0;
            this.BtnLog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(83)))), ((int)(((byte)(116)))));
            this.BtnLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLog.ForeColor = System.Drawing.SystemColors.Control;
            this.BtnLog.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(83)))), ((int)(((byte)(116)))));
            this.BtnLog.Location = new System.Drawing.Point(824, 3);
            this.BtnLog.Name = "BtnLog";
            this.BtnLog.Size = new System.Drawing.Size(95, 29);
            this.BtnLog.TabIndex = 61;
            this.BtnLog.Text = "User Log";
            this.BtnLog.UseVisualStyleBackColor = false;
            this.BtnLog.Click += new System.EventHandler(this.BtnTranLog_Click);
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
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(957, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(10, 32);
            this.panel5.TabIndex = 51;
            // 
            // LblHeader
            // 
            this.LblHeader.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LblHeader.AutoSize = true;
            this.LblHeader.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblHeader.Location = new System.Drawing.Point(0, 5);
            this.LblHeader.Name = "LblHeader";
            this.LblHeader.Size = new System.Drawing.Size(158, 25);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "  USER PROFILE  ";
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
            // PnlMain
            // 
            this.PnlMain.Controls.Add(this.panel2);
            this.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMain.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlMain.Location = new System.Drawing.Point(358, 32);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Padding = new System.Windows.Forms.Padding(10, 10, 10, 20);
            this.PnlMain.Size = new System.Drawing.Size(609, 1057);
            this.PnlMain.TabIndex = 52;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.DataGridEmployee);
            this.panel2.Controls.Add(this.PnlHeaderTable);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(589, 1027);
            this.panel2.TabIndex = 0;
            // 
            // DataGridEmployee
            // 
            this.DataGridEmployee.AllowUserToAddRows = false;
            this.DataGridEmployee.AllowUserToDeleteRows = false;
            this.DataGridEmployee.AllowUserToResizeColumns = false;
            this.DataGridEmployee.AllowUserToResizeRows = false;
            this.DataGridEmployee.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.DataGridEmployee.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridEmployee.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridEmployee.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridEmployee.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmployeeID,
            this.EmployeeName,
            this.DateHired,
            this.DeptName,
            this.PosName,
            this.Gender,
            this.EmploymentStatus,
            this.Remarks,
            this.BsbAppUserName,
            this.FName,
            this.MName,
            this.LName,
            this.DateOfBirth});
            this.DataGridEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridEmployee.Location = new System.Drawing.Point(0, 32);
            this.DataGridEmployee.MultiSelect = false;
            this.DataGridEmployee.Name = "DataGridEmployee";
            this.DataGridEmployee.ReadOnly = true;
            this.DataGridEmployee.RowHeadersVisible = false;
            this.DataGridEmployee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridEmployee.Size = new System.Drawing.Size(587, 993);
            this.DataGridEmployee.TabIndex = 7;
            this.DataGridEmployee.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridEmployee_CellDoubleClick);
            this.DataGridEmployee.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridEmployee_ColumnHeaderMouseClick);
            // 
            // EmployeeID
            // 
            this.EmployeeID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmployeeID.DataPropertyName = "EmployeeID";
            this.EmployeeID.FillWeight = 30F;
            this.EmployeeID.HeaderText = "Employee ID";
            this.EmployeeID.Name = "EmployeeID";
            this.EmployeeID.ReadOnly = true;
            // 
            // EmployeeName
            // 
            this.EmployeeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmployeeName.DataPropertyName = "EmployeeName";
            this.EmployeeName.HeaderText = "Employee Name";
            this.EmployeeName.Name = "EmployeeName";
            this.EmployeeName.ReadOnly = true;
            // 
            // DateHired
            // 
            this.DateHired.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DateHired.DataPropertyName = "DateHired";
            this.DateHired.FillWeight = 25F;
            this.DateHired.HeaderText = "Date Hired";
            this.DateHired.Name = "DateHired";
            this.DateHired.ReadOnly = true;
            // 
            // DeptName
            // 
            this.DeptName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DeptName.DataPropertyName = "DeptName";
            this.DeptName.FillWeight = 40F;
            this.DeptName.HeaderText = "Department";
            this.DeptName.Name = "DeptName";
            this.DeptName.ReadOnly = true;
            // 
            // PosName
            // 
            this.PosName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PosName.DataPropertyName = "PosName";
            this.PosName.FillWeight = 40F;
            this.PosName.HeaderText = "Position";
            this.PosName.Name = "PosName";
            this.PosName.ReadOnly = true;
            // 
            // Gender
            // 
            this.Gender.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Gender.DataPropertyName = "Gender";
            this.Gender.FillWeight = 20F;
            this.Gender.HeaderText = "Gender";
            this.Gender.Name = "Gender";
            this.Gender.ReadOnly = true;
            // 
            // EmploymentStatus
            // 
            this.EmploymentStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmploymentStatus.DataPropertyName = "EmploymentStatus";
            this.EmploymentStatus.FillWeight = 20F;
            this.EmploymentStatus.HeaderText = "Status";
            this.EmploymentStatus.Name = "EmploymentStatus";
            this.EmploymentStatus.ReadOnly = true;
            // 
            // Remarks
            // 
            this.Remarks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Remarks.DataPropertyName = "Remarks";
            this.Remarks.HeaderText = "Remarks";
            this.Remarks.Name = "Remarks";
            this.Remarks.ReadOnly = true;
            // 
            // BsbAppUserName
            // 
            this.BsbAppUserName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BsbAppUserName.DataPropertyName = "BsbAppUserName";
            this.BsbAppUserName.FillWeight = 50F;
            this.BsbAppUserName.HeaderText = "Bsb Username";
            this.BsbAppUserName.Name = "BsbAppUserName";
            this.BsbAppUserName.ReadOnly = true;
            // 
            // FName
            // 
            this.FName.DataPropertyName = "FName";
            this.FName.HeaderText = "First Name";
            this.FName.Name = "FName";
            this.FName.ReadOnly = true;
            this.FName.Visible = false;
            // 
            // MName
            // 
            this.MName.DataPropertyName = "MName";
            this.MName.HeaderText = "Middle Name";
            this.MName.Name = "MName";
            this.MName.ReadOnly = true;
            this.MName.Visible = false;
            // 
            // LName
            // 
            this.LName.DataPropertyName = "LName";
            this.LName.HeaderText = "Last Name";
            this.LName.Name = "LName";
            this.LName.ReadOnly = true;
            this.LName.Visible = false;
            // 
            // DateOfBirth
            // 
            this.DateOfBirth.DataPropertyName = "DateOfBirth";
            this.DateOfBirth.HeaderText = "Date of Birth";
            this.DateOfBirth.Name = "DateOfBirth";
            this.DateOfBirth.ReadOnly = true;
            this.DateOfBirth.Visible = false;
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
            this.LblTable.Size = new System.Drawing.Size(65, 19);
            this.LblTable.TabIndex = 0;
            this.LblTable.Text = "User List";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.BtnTranLog);
            this.panel8.Controls.Add(this.BtnEdit);
            this.panel8.Controls.Add(this.PnlFilter);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel8.Location = new System.Drawing.Point(0, 32);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
            this.panel8.Size = new System.Drawing.Size(358, 1057);
            this.panel8.TabIndex = 51;
            // 
            // BtnTranLog
            // 
            this.BtnTranLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnTranLog.BackColor = System.Drawing.Color.MidnightBlue;
            this.BtnTranLog.BorderColor = System.Drawing.Color.White;
            this.BtnTranLog.BorderRadius = 8;
            this.BtnTranLog.BorderSize = 0;
            this.BtnTranLog.FlatAppearance.BorderSize = 0;
            this.BtnTranLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnTranLog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnTranLog.ForeColor = System.Drawing.Color.White;
            this.BtnTranLog.Location = new System.Drawing.Point(111, 1017);
            this.BtnTranLog.Name = "BtnTranLog";
            this.BtnTranLog.Size = new System.Drawing.Size(95, 30);
            this.BtnTranLog.TabIndex = 48;
            this.BtnTranLog.TabStop = false;
            this.BtnTranLog.Text = "User Log";
            this.BtnTranLog.UseVisualStyleBackColor = false;
            this.BtnTranLog.Visible = false;
            this.BtnTranLog.Click += new System.EventHandler(this.BtnTranLog_Click);
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
            this.BtnEdit.Location = new System.Drawing.Point(10, 1017);
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
            this.PnlFilter.Controls.Add(this.ComboPositionFilter);
            this.PnlFilter.Controls.Add(this.label2);
            this.PnlFilter.Controls.Add(this.ComboDeptFilter);
            this.PnlFilter.Controls.Add(this.label3);
            this.PnlFilter.Controls.Add(this.TxtEmployeeFilter);
            this.PnlFilter.Controls.Add(this.label4);
            this.PnlFilter.Controls.Add(this.PnlHeaderFilter);
            this.PnlFilter.Controls.Add(this.BtnClear);
            this.PnlFilter.Controls.Add(this.BtnSearch);
            this.PnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlFilter.Location = new System.Drawing.Point(10, 10);
            this.PnlFilter.Name = "PnlFilter";
            this.PnlFilter.Padding = new System.Windows.Forms.Padding(5, 35, 5, 5);
            this.PnlFilter.Size = new System.Drawing.Size(348, 227);
            this.PnlFilter.TabIndex = 44;
            // 
            // ComboPositionFilter
            // 
            this.ComboPositionFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComboPositionFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboPositionFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboPositionFilter.FormattingEnabled = true;
            this.ComboPositionFilter.Items.AddRange(new object[] {
            "HELLO",
            "WORLD"});
            this.ComboPositionFilter.Location = new System.Drawing.Point(5, 154);
            this.ComboPositionFilter.Name = "ComboPositionFilter";
            this.ComboPositionFilter.Size = new System.Drawing.Size(336, 25);
            this.ComboPositionFilter.TabIndex = 69;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(5, 134);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label2.Size = new System.Drawing.Size(50, 20);
            this.label2.TabIndex = 67;
            this.label2.Text = "Position";
            // 
            // ComboDeptFilter
            // 
            this.ComboDeptFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComboDeptFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboDeptFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboDeptFilter.FormattingEnabled = true;
            this.ComboDeptFilter.Items.AddRange(new object[] {
            "HELLO",
            "WORLD"});
            this.ComboDeptFilter.Location = new System.Drawing.Point(5, 109);
            this.ComboDeptFilter.Name = "ComboDeptFilter";
            this.ComboDeptFilter.Size = new System.Drawing.Size(336, 25);
            this.ComboDeptFilter.TabIndex = 68;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(5, 89);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label3.Size = new System.Drawing.Size(70, 20);
            this.label3.TabIndex = 66;
            this.label3.Text = "Department";
            // 
            // TxtEmployeeFilter
            // 
            this.TxtEmployeeFilter.BackColor = System.Drawing.SystemColors.Window;
            this.TxtEmployeeFilter.BorderColor = System.Drawing.Color.Silver;
            this.TxtEmployeeFilter.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtEmployeeFilter.BorderRadius = 8;
            this.TxtEmployeeFilter.BorderSize = 1;
            this.TxtEmployeeFilter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtEmployeeFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtEmployeeFilter.Location = new System.Drawing.Point(5, 55);
            this.TxtEmployeeFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TxtEmployeeFilter.MaxLegnth = 32767;
            this.TxtEmployeeFilter.MultiLine = false;
            this.TxtEmployeeFilter.Name = "TxtEmployeeFilter";
            this.TxtEmployeeFilter.Padding = new System.Windows.Forms.Padding(12, 9, 12, 9);
            this.TxtEmployeeFilter.PasswordChar = false;
            this.TxtEmployeeFilter.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtEmployeeFilter.ReadOnly = false;
            this.TxtEmployeeFilter.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtEmployeeFilter.Size = new System.Drawing.Size(336, 34);
            this.TxtEmployeeFilter.TabIndex = 65;
            this.TxtEmployeeFilter.Textt = "";
            this.TxtEmployeeFilter.UnderlinedStyle = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(5, 35);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label4.Size = new System.Drawing.Size(94, 20);
            this.label4.TabIndex = 64;
            this.label4.Text = "Employee Name";
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
            this.BtnClear.Location = new System.Drawing.Point(246, 190);
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
            this.BtnSearch.Location = new System.Drawing.Point(145, 190);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(95, 30);
            this.BtnSearch.TabIndex = 33;
            this.BtnSearch.TabStop = false;
            this.BtnSearch.Text = "S&earch";
            this.BtnSearch.UseVisualStyleBackColor = false;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // frm_user_profile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 1089);
            this.Controls.Add(this.PnlMain);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_user_profile";
            this.Text = "frm_po_approval";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_user_profile_KeyDown);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.PnlMain.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridEmployee)).EndInit();
            this.PnlHeaderTable.ResumeLayout(false);
            this.PnlHeaderTable.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.PnlFilter.ResumeLayout(false);
            this.PnlFilter.PerformLayout();
            this.PnlHeaderFilter.ResumeLayout(false);
            this.PnlHeaderFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel PnlDesign;
        private System.Windows.Forms.Panel PnlMain;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel PnlHeaderTable;
        private System.Windows.Forms.Label LblTable;
        private System.Windows.Forms.Panel panel8;
        private Customized_Components.CustomRoundedButton BtnEdit;
        private System.Windows.Forms.Panel PnlFilter;
        private System.Windows.Forms.Panel PnlHeaderFilter;
        private System.Windows.Forms.Label LblFilter;
        private Customized_Components.CustomRoundedButton BtnClear;
        private Customized_Components.CustomRoundedButton BtnSearch;
        private System.Windows.Forms.ComboBox ComboPositionFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComboDeptFilter;
        private System.Windows.Forms.Label label3;
        private Customized_Components.RifinedCustomTextbox TxtEmployeeFilter;
        private System.Windows.Forms.Label label4;
        private Customized_Components.CustomRoundedButton BtnTranLog;
        private System.Windows.Forms.DataGridView DataGridEmployee;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateHired;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PosName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gender;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmploymentStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn BsbAppUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateOfBirth;
        private Customized_Components.CustomCloseButton BtnClose;
        private Customized_Components.CustomCloseButton BtnEditNew;
        private Customized_Components.CustomCloseButton BtnLog;
    }
}