namespace CARS.Components.Transactions
{
    partial class frm_po_receiving_rr_selection
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSelect = new CARS.Customized_Components.CustomRoundedButton();
            this.btnClose = new CARS.Customized_Components.CustomRoundedButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.dgvRRSelection = new System.Windows.Forms.DataGridView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnRRSearch = new CARS.Customized_Components.CustomRoundedButton();
            this.txtRRSearch = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.RRNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SLName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Term = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TermID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RushOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRRSelection)).BeginInit();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(603, 32);
            this.panel1.TabIndex = 17;
            // 
            // panel9
            // 
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(591, 0);
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
            this.label1.Size = new System.Drawing.Size(140, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "RR SELECTION";
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.panel10.Location = new System.Drawing.Point(-8, 13);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(607, 4);
            this.panel10.TabIndex = 50;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSelect);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 256);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(603, 53);
            this.panel2.TabIndex = 18;
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
            this.btnSelect.Location = new System.Drawing.Point(496, 11);
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
            this.btnClose.Location = new System.Drawing.Point(392, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 30);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 32);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(603, 224);
            this.panel3.TabIndex = 19;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.dgvRRSelection);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(8, 40);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(587, 184);
            this.panel7.TabIndex = 3;
            // 
            // dgvRRSelection
            // 
            this.dgvRRSelection.AllowUserToAddRows = false;
            this.dgvRRSelection.AllowUserToDeleteRows = false;
            this.dgvRRSelection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRRSelection.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RRNo,
            this.SLName,
            this.CreatedDt,
            this.Qty,
            this.Term,
            this.CreatedBy,
            this.SupplierID,
            this.TermID,
            this.Status,
            this.RushOrder});
            this.dgvRRSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRRSelection.Location = new System.Drawing.Point(0, 0);
            this.dgvRRSelection.Name = "dgvRRSelection";
            this.dgvRRSelection.Size = new System.Drawing.Size(587, 184);
            this.dgvRRSelection.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnRRSearch);
            this.panel6.Controls.Add(this.txtRRSearch);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(8, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(587, 40);
            this.panel6.TabIndex = 2;
            // 
            // btnRRSearch
            // 
            this.btnRRSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRRSearch.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnRRSearch.BorderColor = System.Drawing.Color.Transparent;
            this.btnRRSearch.BorderRadius = 8;
            this.btnRRSearch.BorderSize = 1;
            this.btnRRSearch.FlatAppearance.BorderSize = 0;
            this.btnRRSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRRSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRRSearch.ForeColor = System.Drawing.Color.White;
            this.btnRRSearch.Location = new System.Drawing.Point(456, 5);
            this.btnRRSearch.Name = "btnRRSearch";
            this.btnRRSearch.Size = new System.Drawing.Size(95, 30);
            this.btnRRSearch.TabIndex = 21;
            this.btnRRSearch.Text = "Search";
            this.btnRRSearch.UseVisualStyleBackColor = false;
            this.btnRRSearch.Click += new System.EventHandler(this.customRoundedButton1_Click);
            // 
            // txtRRSearch
            // 
            this.txtRRSearch.BackColor = System.Drawing.SystemColors.Window;
            this.txtRRSearch.BorderColor = System.Drawing.Color.Silver;
            this.txtRRSearch.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.txtRRSearch.BorderRadius = 8;
            this.txtRRSearch.BorderSize = 1;
            this.txtRRSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRRSearch.Location = new System.Drawing.Point(72, 5);
            this.txtRRSearch.MaxLegnth = 50;
            this.txtRRSearch.MultiLine = false;
            this.txtRRSearch.Name = "txtRRSearch";
            this.txtRRSearch.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtRRSearch.PasswordChar = false;
            this.txtRRSearch.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtRRSearch.ReadOnly = false;
            this.txtRRSearch.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.txtRRSearch.Size = new System.Drawing.Size(368, 30);
            this.txtRRSearch.TabIndex = 3;
            this.txtRRSearch.Textt = "";
            this.txtRRSearch.UnderlinedStyle = false;
            this.txtRRSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRRSearch_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Search";
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(595, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(8, 224);
            this.panel5.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(8, 224);
            this.panel4.TabIndex = 0;
            // 
            // RRNo
            // 
            this.RRNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RRNo.DataPropertyName = "RRNo";
            this.RRNo.HeaderText = "RR No.";
            this.RRNo.Name = "RRNo";
            this.RRNo.ReadOnly = true;
            // 
            // SLName
            // 
            this.SLName.DataPropertyName = "SLName";
            this.SLName.HeaderText = "Supplier";
            this.SLName.Name = "SLName";
            this.SLName.ReadOnly = true;
            // 
            // CreatedDt
            // 
            this.CreatedDt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CreatedDt.DataPropertyName = "CreatedDt";
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.CreatedDt.DefaultCellStyle = dataGridViewCellStyle1;
            this.CreatedDt.HeaderText = "Date";
            this.CreatedDt.Name = "CreatedDt";
            this.CreatedDt.ReadOnly = true;
            // 
            // Qty
            // 
            this.Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Qty.DataPropertyName = "Qty";
            this.Qty.HeaderText = "Quantity";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            // 
            // Term
            // 
            this.Term.DataPropertyName = "Term";
            this.Term.HeaderText = "Term";
            this.Term.Name = "Term";
            this.Term.ReadOnly = true;
            // 
            // CreatedBy
            // 
            this.CreatedBy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CreatedBy.DataPropertyName = "CreatedBy";
            this.CreatedBy.HeaderText = "Created By";
            this.CreatedBy.Name = "CreatedBy";
            this.CreatedBy.ReadOnly = true;
            // 
            // SupplierID
            // 
            this.SupplierID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SupplierID.DataPropertyName = "SupplierID";
            this.SupplierID.HeaderText = "SupplierID";
            this.SupplierID.Name = "SupplierID";
            this.SupplierID.Visible = false;
            // 
            // TermID
            // 
            this.TermID.DataPropertyName = "TermID";
            this.TermID.HeaderText = "TermID";
            this.TermID.Name = "TermID";
            this.TermID.Visible = false;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Status.DataPropertyName = "Status";
            this.Status.FillWeight = 60F;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Visible = false;
            // 
            // RushOrder
            // 
            this.RushOrder.HeaderText = "RushOrder";
            this.RushOrder.Name = "RushOrder";
            this.RushOrder.Visible = false;
            // 
            // frm_po_receiving_rr_selection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 309);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_po_receiving_rr_selection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_po_receiving_rr_selection";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRRSelection)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private Customized_Components.CustomRoundedButton btnClose;
        private Customized_Components.CustomRoundedButton btnSelect;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private Customized_Components.CustomRoundedButton btnRRSearch;
        private Customized_Components.RifinedCustomTextbox txtRRSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.DataGridView dgvRRSelection;
        private System.Windows.Forms.DataGridViewTextBoxColumn RRNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Term;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TermID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn RushOrder;
    }
}