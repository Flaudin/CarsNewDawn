namespace CARS.Components.Transactions.SalesOrder
{
    partial class frm_sales_order_discount
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.BtnClearDiscount = new CARS.Customized_Components.CustomRoundedButton();
            this.NumericNetPrice = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.maskScheme = new CARS.Customized_Components.CustomMaskDiscountSchemeTextbox();
            this.label2 = new System.Windows.Forms.Label();
            this.NumericDiscountPercent = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.NumericDiscount = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.BtnSave = new CARS.Customized_Components.CustomRoundedButton();
            this.PnlMain.SuspendLayout();
            this.PnlHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericNetPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericDiscountPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericDiscount)).BeginInit();
            this.SuspendLayout();
            // 
            // PnlMain
            // 
            this.PnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlMain.Controls.Add(this.PnlHeader);
            this.PnlMain.Controls.Add(this.panel2);
            this.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMain.Location = new System.Drawing.Point(0, 0);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.PnlMain.Size = new System.Drawing.Size(320, 279);
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
            this.PnlHeader.Size = new System.Drawing.Size(319, 32);
            this.PnlHeader.TabIndex = 9;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.panel14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel14.Location = new System.Drawing.Point(0, 30);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(319, 2);
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
            this.BtnClose.Location = new System.Drawing.Point(284, 0);
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
            this.LblHeader.Size = new System.Drawing.Size(106, 25);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "DISCOUNT";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BtnClearDiscount);
            this.panel2.Controls.Add(this.NumericNetPrice);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.maskScheme);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.NumericDiscountPercent);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.NumericDiscount);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.BtnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 30);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.panel2.Size = new System.Drawing.Size(318, 247);
            this.panel2.TabIndex = 12;
            // 
            // BtnClearDiscount
            // 
            this.BtnClearDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClearDiscount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(122)))));
            this.BtnClearDiscount.BorderColor = System.Drawing.Color.White;
            this.BtnClearDiscount.BorderRadius = 8;
            this.BtnClearDiscount.BorderSize = 0;
            this.BtnClearDiscount.FlatAppearance.BorderSize = 0;
            this.BtnClearDiscount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClearDiscount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearDiscount.ForeColor = System.Drawing.Color.White;
            this.BtnClearDiscount.Location = new System.Drawing.Point(122, 206);
            this.BtnClearDiscount.Name = "BtnClearDiscount";
            this.BtnClearDiscount.Size = new System.Drawing.Size(108, 30);
            this.BtnClearDiscount.TabIndex = 79;
            this.BtnClearDiscount.TabStop = false;
            this.BtnClearDiscount.Text = "&No Discount";
            this.BtnClearDiscount.UseVisualStyleBackColor = false;
            this.BtnClearDiscount.Click += new System.EventHandler(this.BtnClearDiscount_Click);
            // 
            // NumericNetPrice
            // 
            this.NumericNetPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumericNetPrice.DecimalPlaces = 2;
            this.NumericNetPrice.Dock = System.Windows.Forms.DockStyle.Top;
            this.NumericNetPrice.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.NumericNetPrice.Location = new System.Drawing.Point(5, 165);
            this.NumericNetPrice.Maximum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            131072});
            this.NumericNetPrice.Name = "NumericNetPrice";
            this.NumericNetPrice.Size = new System.Drawing.Size(308, 25);
            this.NumericNetPrice.TabIndex = 76;
            this.NumericNetPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericNetPrice.ThousandsSeparator = true;
            this.NumericNetPrice.Enter += new System.EventHandler(this.Discount_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 145);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label3.Size = new System.Drawing.Size(55, 20);
            this.label3.TabIndex = 75;
            this.label3.Text = "Net Price";
            // 
            // maskScheme
            // 
            this.maskScheme.BackColor = System.Drawing.Color.White;
            this.maskScheme.BorderColor = System.Drawing.Color.Silver;
            this.maskScheme.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.maskScheme.BorderRadius = 8;
            this.maskScheme.BorderSize = 1;
            this.maskScheme.Dock = System.Windows.Forms.DockStyle.Top;
            this.maskScheme.Location = new System.Drawing.Point(5, 115);
            this.maskScheme.Name = "maskScheme";
            this.maskScheme.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.maskScheme.PlaceholderColor = System.Drawing.Color.Gray;
            this.maskScheme.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.maskScheme.Size = new System.Drawing.Size(308, 30);
            this.maskScheme.TabIndex = 78;
            this.maskScheme.Textt = "00000000";
            this.maskScheme.UnderLinedStyle = false;
            this.maskScheme.Enter += new System.EventHandler(this.Discount_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 95);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.TabIndex = 73;
            this.label2.Text = "Discount Scheme";
            // 
            // NumericDiscountPercent
            // 
            this.NumericDiscountPercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumericDiscountPercent.DecimalPlaces = 2;
            this.NumericDiscountPercent.Dock = System.Windows.Forms.DockStyle.Top;
            this.NumericDiscountPercent.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.NumericDiscountPercent.Location = new System.Drawing.Point(5, 70);
            this.NumericDiscountPercent.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            131072});
            this.NumericDiscountPercent.Name = "NumericDiscountPercent";
            this.NumericDiscountPercent.Size = new System.Drawing.Size(308, 25);
            this.NumericDiscountPercent.TabIndex = 72;
            this.NumericDiscountPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericDiscountPercent.ThousandsSeparator = true;
            this.NumericDiscountPercent.Enter += new System.EventHandler(this.Discount_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 50);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 71;
            this.label1.Text = "Discount(%)";
            // 
            // NumericDiscount
            // 
            this.NumericDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumericDiscount.DecimalPlaces = 2;
            this.NumericDiscount.Dock = System.Windows.Forms.DockStyle.Top;
            this.NumericDiscount.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.NumericDiscount.Location = new System.Drawing.Point(5, 25);
            this.NumericDiscount.Maximum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            131072});
            this.NumericDiscount.Name = "NumericDiscount";
            this.NumericDiscount.Size = new System.Drawing.Size(308, 25);
            this.NumericDiscount.TabIndex = 70;
            this.NumericDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericDiscount.ThousandsSeparator = true;
            this.NumericDiscount.Enter += new System.EventHandler(this.Discount_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 5);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label6.Size = new System.Drawing.Size(54, 20);
            this.label6.TabIndex = 69;
            this.label6.Text = "Discount";
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
            this.BtnSave.Location = new System.Drawing.Point(8, 206);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(108, 30);
            this.BtnSave.TabIndex = 67;
            this.BtnSave.TabStop = false;
            this.BtnSave.Text = "&Proceed";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // frm_sales_order_discount
            // 
            this.AcceptButton = this.BtnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 279);
            this.Controls.Add(this.PnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frm_sales_order_discount";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Oem Parts Selection";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_stock_transfer_parts_encode_KeyDown);
            this.PnlMain.ResumeLayout(false);
            this.PnlHeader.ResumeLayout(false);
            this.PnlHeader.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericNetPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericDiscountPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericDiscount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Customized_Components.CustomRoundedButton BtnSave;
        private System.Windows.Forms.Panel PnlMain;
        private System.Windows.Forms.Panel PnlHeader;
        private Customized_Components.CustomRoundedButton BtnClose;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown NumericDiscountPercent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NumericDiscount;
        private System.Windows.Forms.NumericUpDown NumericNetPrice;
        private System.Windows.Forms.Label label3;
        private Customized_Components.CustomMaskDiscountSchemeTextbox maskScheme;
        private Customized_Components.CustomRoundedButton BtnClearDiscount;
    }
}