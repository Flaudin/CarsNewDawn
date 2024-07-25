namespace CARS.Components.Transactions
{
    partial class frm_receiving_report
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_receiving_report));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.cmbGroupBy = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cmbBrand = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cmbDesc = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbPart = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbReceiveType = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.BtnPrint = new CARS.Customized_Components.CustomRoundedButton();
            this.label15 = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.txtRREnd = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtRRStart = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label13 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.layout_date = new System.Windows.Forms.TableLayoutPanel();
            this.DateDrTo = new CARS.Customized_Components.CustomDateTime();
            this.DateDrFrom = new CARS.Customized_Components.CustomDateTime();
            this.LblDrDateTo = new System.Windows.Forms.Label();
            this.PnlFilter = new System.Windows.Forms.Panel();
            this.LblFilter = new System.Windows.Forms.Label();
            this.customRoundedButton1 = new CARS.Customized_Components.CustomRoundedButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rdDelv = new CARS.Customized_Components.CustomRadioButton();
            this.rdReceive = new CARS.Customized_Components.CustomRadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnClose = new CARS.Customized_Components.CustomCloseButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.LblHeader = new System.Windows.Forms.Label();
            this.PnlDesign = new System.Windows.Forms.Panel();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.panel2.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.layout_date.SuspendLayout();
            this.PnlFilter.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel14);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(750, 100, 750, 0);
            this.panel2.Size = new System.Drawing.Size(967, 885);
            this.panel2.TabIndex = 4;
            // 
            // panel14
            // 
            this.panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel14.Controls.Add(this.cmbGroupBy);
            this.panel14.Controls.Add(this.label20);
            this.panel14.Controls.Add(this.cmbBrand);
            this.panel14.Controls.Add(this.label19);
            this.panel14.Controls.Add(this.cmbDesc);
            this.panel14.Controls.Add(this.label17);
            this.panel14.Controls.Add(this.cmbPart);
            this.panel14.Controls.Add(this.label16);
            this.panel14.Controls.Add(this.cmbSupplier);
            this.panel14.Controls.Add(this.label14);
            this.panel14.Controls.Add(this.cmbReceiveType);
            this.panel14.Controls.Add(this.panel5);
            this.panel14.Controls.Add(this.label15);
            this.panel14.Controls.Add(this.tableLayoutPanel6);
            this.panel14.Controls.Add(this.label13);
            this.panel14.Controls.Add(this.checkBox2);
            this.panel14.Controls.Add(this.layout_date);
            this.panel14.Controls.Add(this.PnlFilter);
            this.panel14.Controls.Add(this.customRoundedButton1);
            this.panel14.Controls.Add(this.tableLayoutPanel1);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel14.Location = new System.Drawing.Point(750, 100);
            this.panel14.Name = "panel14";
            this.panel14.Padding = new System.Windows.Forms.Padding(5, 35, 5, 5);
            this.panel14.Size = new System.Drawing.Size(0, 506);
            this.panel14.TabIndex = 48;
            // 
            // cmbGroupBy
            // 
            this.cmbGroupBy.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbGroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroupBy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGroupBy.FormattingEnabled = true;
            this.cmbGroupBy.Items.AddRange(new object[] {
            "RR SUMMARY",
            "RR DETAILS LIST"});
            this.cmbGroupBy.Location = new System.Drawing.Point(5, 400);
            this.cmbGroupBy.Name = "cmbGroupBy";
            this.cmbGroupBy.Size = new System.Drawing.Size(0, 27);
            this.cmbGroupBy.TabIndex = 72;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Location = new System.Drawing.Point(5, 380);
            this.label20.Name = "label20";
            this.label20.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label20.Size = new System.Drawing.Size(56, 20);
            this.label20.TabIndex = 65;
            this.label20.Text = "Group By";
            // 
            // cmbBrand
            // 
            this.cmbBrand.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBrand.Enabled = false;
            this.cmbBrand.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBrand.FormattingEnabled = true;
            this.cmbBrand.Location = new System.Drawing.Point(5, 353);
            this.cmbBrand.Name = "cmbBrand";
            this.cmbBrand.Size = new System.Drawing.Size(0, 27);
            this.cmbBrand.TabIndex = 52;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Location = new System.Drawing.Point(5, 333);
            this.label19.Name = "label19";
            this.label19.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label19.Size = new System.Drawing.Size(38, 20);
            this.label19.TabIndex = 63;
            this.label19.Text = "Brand";
            // 
            // cmbDesc
            // 
            this.cmbDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbDesc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDesc.Enabled = false;
            this.cmbDesc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDesc.FormattingEnabled = true;
            this.cmbDesc.Location = new System.Drawing.Point(5, 306);
            this.cmbDesc.Name = "cmbDesc";
            this.cmbDesc.Size = new System.Drawing.Size(0, 27);
            this.cmbDesc.TabIndex = 51;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Location = new System.Drawing.Point(5, 286);
            this.label17.Name = "label17";
            this.label17.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label17.Size = new System.Drawing.Size(91, 20);
            this.label17.TabIndex = 61;
            this.label17.Text = "Part Description";
            // 
            // cmbPart
            // 
            this.cmbPart.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPart.Enabled = false;
            this.cmbPart.FormattingEnabled = true;
            this.cmbPart.Location = new System.Drawing.Point(5, 263);
            this.cmbPart.Name = "cmbPart";
            this.cmbPart.Size = new System.Drawing.Size(0, 23);
            this.cmbPart.TabIndex = 50;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Location = new System.Drawing.Point(5, 243);
            this.label16.Name = "label16";
            this.label16.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label16.Size = new System.Drawing.Size(75, 20);
            this.label16.TabIndex = 59;
            this.label16.Text = "Part Number";
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.Enabled = false;
            this.cmbSupplier.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(5, 216);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(0, 27);
            this.cmbSupplier.TabIndex = 49;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Location = new System.Drawing.Point(5, 196);
            this.label14.Name = "label14";
            this.label14.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label14.Size = new System.Drawing.Size(50, 20);
            this.label14.TabIndex = 57;
            this.label14.Text = "Supplier";
            // 
            // cmbReceiveType
            // 
            this.cmbReceiveType.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbReceiveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReceiveType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReceiveType.FormattingEnabled = true;
            this.cmbReceiveType.Items.AddRange(new object[] {
            "REGULAR RECEIVING",
            "RUSH RECEIVING"});
            this.cmbReceiveType.Location = new System.Drawing.Point(5, 169);
            this.cmbReceiveType.Name = "cmbReceiveType";
            this.cmbReceiveType.Size = new System.Drawing.Size(0, 27);
            this.cmbReceiveType.TabIndex = 71;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.BtnPrint);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(5, 467);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(0, 32);
            this.panel5.TabIndex = 70;
            // 
            // BtnPrint
            // 
            this.BtnPrint.BackColor = System.Drawing.Color.MidnightBlue;
            this.BtnPrint.BorderColor = System.Drawing.Color.MidnightBlue;
            this.BtnPrint.BorderRadius = 8;
            this.BtnPrint.BorderSize = 3;
            this.BtnPrint.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnPrint.FlatAppearance.BorderSize = 0;
            this.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPrint.ForeColor = System.Drawing.Color.White;
            this.BtnPrint.Location = new System.Drawing.Point(-95, 0);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(95, 32);
            this.BtnPrint.TabIndex = 68;
            this.BtnPrint.TabStop = false;
            this.BtnPrint.Text = "Print";
            this.BtnPrint.UseVisualStyleBackColor = false;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Location = new System.Drawing.Point(5, 149);
            this.label15.Name = "label15";
            this.label15.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label15.Size = new System.Drawing.Size(123, 20);
            this.label15.TabIndex = 54;
            this.label15.Text = "Receiving Report Type";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.44444F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.44444F));
            this.tableLayoutPanel6.Controls.Add(this.txtRREnd, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.label18, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.txtRRStart, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(5, 112);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(0, 37);
            this.tableLayoutPanel6.TabIndex = 51;
            // 
            // txtRREnd
            // 
            this.txtRREnd.BackColor = System.Drawing.SystemColors.Window;
            this.txtRREnd.BorderColor = System.Drawing.Color.Silver;
            this.txtRREnd.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.txtRREnd.BorderRadius = 8;
            this.txtRREnd.BorderSize = 1;
            this.txtRREnd.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtRREnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRREnd.Location = new System.Drawing.Point(3, 3);
            this.txtRREnd.MaxLegnth = 32767;
            this.txtRREnd.MultiLine = false;
            this.txtRREnd.Name = "txtRREnd";
            this.txtRREnd.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.txtRREnd.PasswordChar = false;
            this.txtRREnd.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtRREnd.ReadOnly = false;
            this.txtRREnd.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.txtRREnd.Size = new System.Drawing.Size(1, 32);
            this.txtRREnd.TabIndex = 9;
            this.txtRREnd.Textt = "";
            this.txtRREnd.UnderlinedStyle = false;
            this.txtRREnd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rifinedCustomTextbox2_MouseDown);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(3, 0);
            this.label18.Name = "label18";
            this.label18.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label18.Size = new System.Drawing.Size(1, 25);
            this.label18.TabIndex = 7;
            this.label18.Text = "To";
            // 
            // txtRRStart
            // 
            this.txtRRStart.BackColor = System.Drawing.SystemColors.Window;
            this.txtRRStart.BorderColor = System.Drawing.Color.Silver;
            this.txtRRStart.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.txtRRStart.BorderRadius = 8;
            this.txtRRStart.BorderSize = 1;
            this.txtRRStart.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtRRStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRRStart.Location = new System.Drawing.Point(3, 3);
            this.txtRRStart.MaxLegnth = 32767;
            this.txtRRStart.MultiLine = false;
            this.txtRRStart.Name = "txtRRStart";
            this.txtRRStart.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.txtRRStart.PasswordChar = false;
            this.txtRRStart.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtRRStart.ReadOnly = false;
            this.txtRRStart.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.txtRRStart.Size = new System.Drawing.Size(1, 32);
            this.txtRRStart.TabIndex = 8;
            this.txtRRStart.Textt = "";
            this.txtRRStart.UnderlinedStyle = false;
            this.txtRRStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rifinedCustomTextbox1_KeyDown);
            this.txtRRStart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rifinedCustomTextbox1_MouseDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(5, 92);
            this.label13.Name = "label13";
            this.label13.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label13.Size = new System.Drawing.Size(143, 20);
            this.label13.TabIndex = 69;
            this.label13.Text = "Receiving Report Number";
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(-8646, 158);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.checkBox2.Size = new System.Drawing.Size(113, 22);
            this.checkBox2.TabIndex = 55;
            this.checkBox2.Text = "Partial Receiving";
            this.checkBox2.UseVisualStyleBackColor = true;
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
            this.layout_date.Location = new System.Drawing.Point(5, 60);
            this.layout_date.Name = "layout_date";
            this.layout_date.RowCount = 1;
            this.layout_date.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout_date.Size = new System.Drawing.Size(0, 32);
            this.layout_date.TabIndex = 47;
            // 
            // DateDrTo
            // 
            this.DateDrTo.BoderSize = 0;
            this.DateDrTo.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.DateDrTo.CustomFormat = "MM/dd/yyyy";
            this.DateDrTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateDrTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateDrTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateDrTo.Location = new System.Drawing.Point(3, 3);
            this.DateDrTo.MinimumSize = new System.Drawing.Size(4, 32);
            this.DateDrTo.Name = "DateDrTo";
            this.DateDrTo.Size = new System.Drawing.Size(4, 32);
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
            this.DateDrFrom.Size = new System.Drawing.Size(4, 32);
            this.DateDrFrom.SkinColor = System.Drawing.Color.White;
            this.DateDrFrom.TabIndex = 5;
            this.DateDrFrom.TextColor = System.Drawing.Color.Black;
            // 
            // LblDrDateTo
            // 
            this.LblDrDateTo.AutoSize = true;
            this.LblDrDateTo.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblDrDateTo.Location = new System.Drawing.Point(3, 0);
            this.LblDrDateTo.Name = "LblDrDateTo";
            this.LblDrDateTo.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.LblDrDateTo.Size = new System.Drawing.Size(1, 25);
            this.LblDrDateTo.TabIndex = 7;
            this.LblDrDateTo.Text = "To";
            // 
            // PnlFilter
            // 
            this.PnlFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlFilter.Controls.Add(this.LblFilter);
            this.PnlFilter.Location = new System.Drawing.Point(-1, -1);
            this.PnlFilter.Name = "PnlFilter";
            this.PnlFilter.Size = new System.Drawing.Size(0, 32);
            this.PnlFilter.TabIndex = 45;
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
            // customRoundedButton1
            // 
            this.customRoundedButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.customRoundedButton1.BackColor = System.Drawing.Color.MidnightBlue;
            this.customRoundedButton1.BorderColor = System.Drawing.Color.White;
            this.customRoundedButton1.BorderRadius = 8;
            this.customRoundedButton1.BorderSize = 0;
            this.customRoundedButton1.FlatAppearance.BorderSize = 0;
            this.customRoundedButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customRoundedButton1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customRoundedButton1.ForeColor = System.Drawing.Color.White;
            this.customRoundedButton1.Location = new System.Drawing.Point(-8629, 466);
            this.customRoundedButton1.Name = "customRoundedButton1";
            this.customRoundedButton1.Size = new System.Drawing.Size(95, 30);
            this.customRoundedButton1.TabIndex = 45;
            this.customRoundedButton1.TabStop = false;
            this.customRoundedButton1.Text = "Print";
            this.customRoundedButton1.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.87023F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.05343F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.92366F));
            this.tableLayoutPanel1.Controls.Add(this.rdDelv, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rdReceive, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 35);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(0, 25);
            this.tableLayoutPanel1.TabIndex = 49;
            // 
            // rdDelv
            // 
            this.rdDelv.CheckedColor = System.Drawing.Color.Orange;
            this.rdDelv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdDelv.Location = new System.Drawing.Point(3, 3);
            this.rdDelv.MinimumSize = new System.Drawing.Size(0, 21);
            this.rdDelv.Name = "rdDelv";
            this.rdDelv.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.rdDelv.Size = new System.Drawing.Size(121, 21);
            this.rdDelv.TabIndex = 50;
            this.rdDelv.Text = "Delivery Receipt";
            this.rdDelv.UncheckedColor = System.Drawing.Color.Gray;
            this.rdDelv.UseVisualStyleBackColor = true;
            // 
            // rdReceive
            // 
            this.rdReceive.Checked = true;
            this.rdReceive.CheckedColor = System.Drawing.Color.Orange;
            this.rdReceive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdReceive.Location = new System.Drawing.Point(3, 3);
            this.rdReceive.MinimumSize = new System.Drawing.Size(0, 21);
            this.rdReceive.Name = "rdReceive";
            this.rdReceive.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.rdReceive.Size = new System.Drawing.Size(88, 21);
            this.rdReceive.TabIndex = 49;
            this.rdReceive.TabStop = true;
            this.rdReceive.Text = "Receiving";
            this.rdReceive.UncheckedColor = System.Drawing.Color.Gray;
            this.rdReceive.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label12.Size = new System.Drawing.Size(1, 20);
            this.label12.TabIndex = 47;
            this.label12.Text = "Date Range";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.BtnClose);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.LblHeader);
            this.panel1.Controls.Add(this.PnlDesign);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(967, 32);
            this.panel1.TabIndex = 31;
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
            this.BtnClose.TabIndex = 61;
            this.BtnClose.Text = "X";
            this.BtnClose.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(957, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 32);
            this.panel3.TabIndex = 51;
            // 
            // LblHeader
            // 
            this.LblHeader.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LblHeader.AutoSize = true;
            this.LblHeader.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblHeader.Location = new System.Drawing.Point(0, 5);
            this.LblHeader.Name = "LblHeader";
            this.LblHeader.Size = new System.Drawing.Size(217, 25);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "  RECEIVING REPORTS  ";
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
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // frm_receiving_report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 917);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_receiving_report";
            this.Text = "frm_po_approval";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel2.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.layout_date.ResumeLayout(false);
            this.layout_date.PerformLayout();
            this.PnlFilter.ResumeLayout(false);
            this.PnlFilter.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel PnlFilter;
        private System.Windows.Forms.Label LblFilter;
        private Customized_Components.CustomRoundedButton customRoundedButton1;
        private System.Windows.Forms.TableLayoutPanel layout_date;
        private Customized_Components.CustomDateTime DateDrTo;
        private Customized_Components.CustomDateTime DateDrFrom;
        private System.Windows.Forms.Label LblDrDateTo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private Customized_Components.RifinedCustomTextbox txtRREnd;
        private System.Windows.Forms.Label label18;
        private Customized_Components.RifinedCustomTextbox txtRRStart;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel PnlDesign;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Customized_Components.CustomRadioButton rdDelv;
        private Customized_Components.CustomRadioButton rdReceive;
        private System.Windows.Forms.Label label12;
        private Customized_Components.CustomRoundedButton BtnPrint;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label13;
        private Customized_Components.CustomCloseButton BtnClose;
        private System.Windows.Forms.ComboBox cmbReceiveType;
        private System.Windows.Forms.ComboBox cmbGroupBy;
        private System.Windows.Forms.ComboBox cmbPart;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.ComboBox cmbBrand;
        private System.Windows.Forms.ComboBox cmbDesc;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
    }
}