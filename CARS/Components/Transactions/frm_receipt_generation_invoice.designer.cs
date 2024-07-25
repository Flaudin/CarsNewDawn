namespace CARS.Components.Transactions
{
    partial class frm_receipt_generation_invoice
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
            this.PnlHeader = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.BtnClose = new CARS.Customized_Components.CustomRoundedButton();
            this.LblHeader = new System.Windows.Forms.Label();
            this.Pnl = new System.Windows.Forms.Panel();
            this.BtnSave = new CARS.Customized_Components.CustomRoundedButton();
            this.PnlDetails = new System.Windows.Forms.Panel();
            this.TxtAddress = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label12 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TxtTin = new CARS.Customized_Components.CustomMaskTextbox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ComboTerm = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ComboCustomer = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.PnlHeaderFilter = new System.Windows.Forms.Panel();
            this.LblFilter = new System.Windows.Forms.Label();
            this.PnlMain.SuspendLayout();
            this.PnlHeader.SuspendLayout();
            this.Pnl.SuspendLayout();
            this.PnlDetails.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.PnlHeaderFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlMain
            // 
            this.PnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlMain.Controls.Add(this.PnlHeader);
            this.PnlMain.Controls.Add(this.Pnl);
            this.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMain.Location = new System.Drawing.Point(0, 0);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.PnlMain.Size = new System.Drawing.Size(650, 267);
            this.PnlMain.TabIndex = 9;
            // 
            // PnlHeader
            // 
            this.PnlHeader.BackColor = System.Drawing.Color.Yellow;
            this.PnlHeader.Controls.Add(this.panel14);
            this.PnlHeader.Controls.Add(this.BtnClose);
            this.PnlHeader.Controls.Add(this.LblHeader);
            this.PnlHeader.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlHeader.Location = new System.Drawing.Point(0, 0);
            this.PnlHeader.Name = "PnlHeader";
            this.PnlHeader.Size = new System.Drawing.Size(650, 32);
            this.PnlHeader.TabIndex = 9;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.panel14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel14.Location = new System.Drawing.Point(0, 30);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(650, 2);
            this.panel14.TabIndex = 52;
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
            this.BtnClose.Location = new System.Drawing.Point(615, 0);
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
            this.LblHeader.Size = new System.Drawing.Size(146, 25);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "PRINT INVOICE";
            // 
            // Pnl
            // 
            this.Pnl.BackColor = System.Drawing.SystemColors.Control;
            this.Pnl.Controls.Add(this.BtnSave);
            this.Pnl.Controls.Add(this.PnlDetails);
            this.Pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pnl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pnl.Location = new System.Drawing.Point(0, 30);
            this.Pnl.Name = "Pnl";
            this.Pnl.Padding = new System.Windows.Forms.Padding(5);
            this.Pnl.Size = new System.Drawing.Size(648, 235);
            this.Pnl.TabIndex = 11;
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(122)))));
            this.BtnSave.BorderColor = System.Drawing.Color.White;
            this.BtnSave.BorderRadius = 8;
            this.BtnSave.BorderSize = 0;
            this.BtnSave.FlatAppearance.BorderSize = 0;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.ForeColor = System.Drawing.Color.White;
            this.BtnSave.Location = new System.Drawing.Point(5, 200);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(108, 30);
            this.BtnSave.TabIndex = 67;
            this.BtnSave.TabStop = false;
            this.BtnSave.Text = "Print";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // PnlDetails
            // 
            this.PnlDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlDetails.Controls.Add(this.TxtAddress);
            this.PnlDetails.Controls.Add(this.label12);
            this.PnlDetails.Controls.Add(this.label26);
            this.PnlDetails.Controls.Add(this.tableLayoutPanel1);
            this.PnlDetails.Controls.Add(this.PnlHeaderFilter);
            this.PnlDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlDetails.Location = new System.Drawing.Point(5, 5);
            this.PnlDetails.Name = "PnlDetails";
            this.PnlDetails.Padding = new System.Windows.Forms.Padding(5, 35, 5, 5);
            this.PnlDetails.Size = new System.Drawing.Size(638, 191);
            this.PnlDetails.TabIndex = 81;
            // 
            // TxtAddress
            // 
            this.TxtAddress.BackColor = System.Drawing.SystemColors.Window;
            this.TxtAddress.BorderColor = System.Drawing.Color.Silver;
            this.TxtAddress.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtAddress.BorderRadius = 8;
            this.TxtAddress.BorderSize = 1;
            this.TxtAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtAddress.Location = new System.Drawing.Point(5, 117);
            this.TxtAddress.MaxLegnth = 200;
            this.TxtAddress.MultiLine = true;
            this.TxtAddress.Name = "TxtAddress";
            this.TxtAddress.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.TxtAddress.PasswordChar = false;
            this.TxtAddress.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtAddress.ReadOnly = false;
            this.TxtAddress.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.TxtAddress.Size = new System.Drawing.Size(626, 66);
            this.TxtAddress.TabIndex = 74;
            this.TxtAddress.Textt = "";
            this.TxtAddress.UnderlinedStyle = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(5, 97);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label12.Size = new System.Drawing.Size(49, 20);
            this.label12.TabIndex = 73;
            this.label12.Text = "Address";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label26.ForeColor = System.Drawing.Color.Crimson;
            this.label26.Location = new System.Drawing.Point(59, 97);
            this.label26.Margin = new System.Windows.Forms.Padding(0);
            this.label26.MaximumSize = new System.Drawing.Size(15, 23);
            this.label26.MinimumSize = new System.Drawing.Size(15, 23);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(15, 23);
            this.label26.TabIndex = 75;
            this.label26.Text = "*";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.37607F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.62393F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 35);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(626, 62);
            this.tableLayoutPanel1.TabIndex = 46;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.TxtTin);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(458, 3);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.panel4.Size = new System.Drawing.Size(165, 56);
            this.panel4.TabIndex = 2;
            // 
            // TxtTin
            // 
            this.TxtTin.BackColor = System.Drawing.Color.White;
            this.TxtTin.BorderColor = System.Drawing.Color.Silver;
            this.TxtTin.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.TxtTin.BorderRadius = 8;
            this.TxtTin.BorderSize = 1;
            this.TxtTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtTin.Location = new System.Drawing.Point(5, 20);
            this.TxtTin.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TxtTin.Name = "TxtTin";
            this.TxtTin.Padding = new System.Windows.Forms.Padding(12, 9, 12, 9);
            this.TxtTin.PlaceholderColor = System.Drawing.Color.Gray;
            this.TxtTin.Size = new System.Drawing.Size(160, 31);
            this.TxtTin.TabIndex = 72;
            this.TxtTin.Textt = "";
            this.TxtTin.UnderLinedStyle = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 15);
            this.label6.TabIndex = 71;
            this.label6.Text = "TIN";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.Crimson;
            this.label10.Location = new System.Drawing.Point(33, 5);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.MaximumSize = new System.Drawing.Size(15, 23);
            this.label10.MinimumSize = new System.Drawing.Size(15, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 23);
            this.label10.TabIndex = 71;
            this.label10.Text = "*";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ComboTerm);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(314, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(138, 56);
            this.panel2.TabIndex = 3;
            // 
            // ComboTerm
            // 
            this.ComboTerm.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComboTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboTerm.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ComboTerm.FormattingEnabled = true;
            this.ComboTerm.Location = new System.Drawing.Point(5, 20);
            this.ComboTerm.Name = "ComboTerm";
            this.ComboTerm.Size = new System.Drawing.Size(128, 25);
            this.ComboTerm.TabIndex = 74;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 71;
            this.label1.Text = "Terms";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Crimson;
            this.label2.Location = new System.Drawing.Point(33, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.MaximumSize = new System.Drawing.Size(15, 23);
            this.label2.MinimumSize = new System.Drawing.Size(15, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 23);
            this.label2.TabIndex = 71;
            this.label2.Text = "*";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ComboCustomer);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.panel1.Size = new System.Drawing.Size(305, 56);
            this.panel1.TabIndex = 1;
            // 
            // ComboCustomer
            // 
            this.ComboCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ComboCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboCustomer.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComboCustomer.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ComboCustomer.FormattingEnabled = true;
            this.ComboCustomer.Location = new System.Drawing.Point(0, 20);
            this.ComboCustomer.Name = "ComboCustomer";
            this.ComboCustomer.Size = new System.Drawing.Size(300, 25);
            this.ComboCustomer.TabIndex = 73;
            this.ComboCustomer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboCustomer_KeyPress);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label24.ForeColor = System.Drawing.Color.Crimson;
            this.label24.Location = new System.Drawing.Point(42, 5);
            this.label24.Margin = new System.Windows.Forms.Padding(0);
            this.label24.MaximumSize = new System.Drawing.Size(15, 23);
            this.label24.MinimumSize = new System.Drawing.Size(15, 23);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(15, 23);
            this.label24.TabIndex = 79;
            this.label24.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 15);
            this.label13.TabIndex = 74;
            this.label13.Text = "Name";
            // 
            // PnlHeaderFilter
            // 
            this.PnlHeaderFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlHeaderFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeaderFilter.Controls.Add(this.LblFilter);
            this.PnlHeaderFilter.Location = new System.Drawing.Point(-1, -1);
            this.PnlHeaderFilter.Name = "PnlHeaderFilter";
            this.PnlHeaderFilter.Size = new System.Drawing.Size(638, 32);
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
            this.LblFilter.Size = new System.Drawing.Size(54, 19);
            this.LblFilter.TabIndex = 0;
            this.LblFilter.Text = "Details";
            // 
            // frm_receipt_generation_invoice
            // 
            this.AcceptButton = this.BtnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 267);
            this.Controls.Add(this.PnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frm_receipt_generation_invoice";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Oem Parts Selection";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_stock_transfer_parts_encode_KeyDown);
            this.PnlMain.ResumeLayout(false);
            this.PnlHeader.ResumeLayout(false);
            this.PnlHeader.PerformLayout();
            this.Pnl.ResumeLayout(false);
            this.PnlDetails.ResumeLayout(false);
            this.PnlDetails.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PnlHeaderFilter.ResumeLayout(false);
            this.PnlHeaderFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Customized_Components.CustomRoundedButton BtnSave;
        private System.Windows.Forms.Panel PnlMain;
        private System.Windows.Forms.Panel PnlHeader;
        private Customized_Components.CustomRoundedButton BtnClose;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel Pnl;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel PnlDetails;
        private System.Windows.Forms.Panel PnlHeaderFilter;
        private System.Windows.Forms.Label LblFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel4;
        private Customized_Components.CustomMaskTextbox TxtTin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox ComboCustomer;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label13;
        private Customized_Components.RifinedCustomTextbox TxtAddress;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox ComboTerm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}