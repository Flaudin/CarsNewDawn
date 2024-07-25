namespace CARS.Components.Transactions
{
    partial class frm_po_receiving_loc_selection
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvLocaitonSelector = new System.Windows.Forms.DataGridView();
            this.panel7 = new System.Windows.Forms.Panel();
            this.customRoundedButton1 = new CARS.Customized_Components.CustomRoundedButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtsearchLoc = new CARS.Customized_Components.RifinedCustomTextbox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSelect = new CARS.Customized_Components.CustomRoundedButton();
            this.btnClose = new CARS.Customized_Components.CustomRoundedButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Warehouse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WhID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BinID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocaitonSelector)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(603, 309);
            this.panel2.TabIndex = 19;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgvLocaitonSelector);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 32);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(601, 232);
            this.panel4.TabIndex = 20;
            // 
            // dgvLocaitonSelector
            // 
            this.dgvLocaitonSelector.AllowUserToAddRows = false;
            this.dgvLocaitonSelector.AllowUserToDeleteRows = false;
            this.dgvLocaitonSelector.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocaitonSelector.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Location,
            this.Warehouse,
            this.WhID,
            this.BinID,
            this.LotNo});
            this.dgvLocaitonSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLocaitonSelector.Location = new System.Drawing.Point(8, 40);
            this.dgvLocaitonSelector.Name = "dgvLocaitonSelector";
            this.dgvLocaitonSelector.ReadOnly = true;
            this.dgvLocaitonSelector.Size = new System.Drawing.Size(585, 192);
            this.dgvLocaitonSelector.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.customRoundedButton1);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Controls.Add(this.txtsearchLoc);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(8, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(585, 40);
            this.panel7.TabIndex = 3;
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
            this.customRoundedButton1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customRoundedButton1.ForeColor = System.Drawing.Color.White;
            this.customRoundedButton1.Location = new System.Drawing.Point(488, 5);
            this.customRoundedButton1.Name = "customRoundedButton1";
            this.customRoundedButton1.Size = new System.Drawing.Size(71, 30);
            this.customRoundedButton1.TabIndex = 21;
            this.customRoundedButton1.Text = ">>";
            this.customRoundedButton1.UseVisualStyleBackColor = false;
            this.customRoundedButton1.Click += new System.EventHandler(this.customRoundedButton1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Search";
            // 
            // txtsearchLoc
            // 
            this.txtsearchLoc.BackColor = System.Drawing.SystemColors.Window;
            this.txtsearchLoc.BorderColor = System.Drawing.Color.Silver;
            this.txtsearchLoc.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.txtsearchLoc.BorderRadius = 8;
            this.txtsearchLoc.BorderSize = 1;
            this.txtsearchLoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtsearchLoc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearchLoc.Location = new System.Drawing.Point(88, 5);
            this.txtsearchLoc.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtsearchLoc.MaxLegnth = 32767;
            this.txtsearchLoc.MultiLine = false;
            this.txtsearchLoc.Name = "txtsearchLoc";
            this.txtsearchLoc.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtsearchLoc.PasswordChar = false;
            this.txtsearchLoc.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtsearchLoc.ReadOnly = false;
            this.txtsearchLoc.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.txtsearchLoc.Size = new System.Drawing.Size(392, 30);
            this.txtsearchLoc.TabIndex = 0;
            this.txtsearchLoc.Textt = "";
            this.txtsearchLoc.UnderlinedStyle = false;
            this.txtsearchLoc.Enter += new System.EventHandler(this.txtsearchLoc_Enter);
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(593, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(8, 232);
            this.panel6.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(8, 232);
            this.panel5.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnSelect);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 264);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(601, 43);
            this.panel3.TabIndex = 19;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnSelect.BorderColor = System.Drawing.Color.White;
            this.btnSelect.BorderRadius = 8;
            this.btnSelect.BorderSize = 0;
            this.btnSelect.FlatAppearance.BorderSize = 0;
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.ForeColor = System.Drawing.Color.White;
            this.btnSelect.Location = new System.Drawing.Point(494, 6);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(95, 30);
            this.btnSelect.TabIndex = 21;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.BorderColor = System.Drawing.Color.White;
            this.btnClose.BorderRadius = 8;
            this.btnClose.BorderSize = 0;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(390, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 30);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel10);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(601, 32);
            this.panel1.TabIndex = 18;
            // 
            // panel9
            // 
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(589, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(10, 30);
            this.panel9.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "LOCATION SELECTION";
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.panel10.Location = new System.Drawing.Point(-8, 13);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(605, 4);
            this.panel10.TabIndex = 50;
            // 
            // Location
            // 
            this.Location.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Location.DataPropertyName = "BinName";
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            // 
            // Warehouse
            // 
            this.Warehouse.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Warehouse.DataPropertyName = "WhName";
            this.Warehouse.HeaderText = "Warehouse";
            this.Warehouse.Name = "Warehouse";
            this.Warehouse.ReadOnly = true;
            // 
            // WhID
            // 
            this.WhID.DataPropertyName = "WhID";
            this.WhID.HeaderText = "WhID";
            this.WhID.Name = "WhID";
            this.WhID.ReadOnly = true;
            this.WhID.Visible = false;
            // 
            // BinID
            // 
            this.BinID.DataPropertyName = "BinID";
            this.BinID.HeaderText = "BinID";
            this.BinID.Name = "BinID";
            this.BinID.ReadOnly = true;
            this.BinID.Visible = false;
            // 
            // LotNo
            // 
            this.LotNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LotNo.DataPropertyName = "LotNo";
            this.LotNo.HeaderText = "LotNo";
            this.LotNo.Name = "LotNo";
            this.LotNo.ReadOnly = true;
            this.LotNo.Visible = false;
            // 
            // frm_po_receiving_loc_selection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 309);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_po_receiving_loc_selection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_po_receiving_loc_selection";
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocaitonSelector)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel3;
        private Customized_Components.CustomRoundedButton btnSelect;
        private Customized_Components.CustomRoundedButton btnClose;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgvLocaitonSelector;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label2;
        private Customized_Components.RifinedCustomTextbox txtsearchLoc;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private Customized_Components.CustomRoundedButton customRoundedButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn Warehouse;
        private System.Windows.Forms.DataGridViewTextBoxColumn WhID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BinID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotNo;
    }
}