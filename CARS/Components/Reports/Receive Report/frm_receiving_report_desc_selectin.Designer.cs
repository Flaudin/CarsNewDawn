namespace CARS.Components.Reports.Receive_Report
{
    partial class frm_receiving_report_desc_selectin
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
            this.PnlHeader = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.BtnClose = new CARS.Customized_Components.CustomRoundedButton();
            this.LblHeader = new System.Windows.Forms.Label();
            this.pnlTable = new System.Windows.Forms.Panel();
            this.dgvRecevingList = new System.Windows.Forms.DataGridView();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.BtnSave = new CARS.Customized_Components.CustomRoundedButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DescName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PnlHeader.SuspendLayout();
            this.pnlTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecevingList)).BeginInit();
            this.panel13.SuspendLayout();
            this.panel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlHeader
            // 
            this.PnlHeader.BackColor = System.Drawing.Color.Yellow;
            this.PnlHeader.Controls.Add(this.panel14);
            this.PnlHeader.Controls.Add(this.BtnClose);
            this.PnlHeader.Controls.Add(this.LblHeader);
            this.PnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlHeader.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlHeader.Location = new System.Drawing.Point(0, 0);
            this.PnlHeader.Name = "PnlHeader";
            this.PnlHeader.Size = new System.Drawing.Size(577, 32);
            this.PnlHeader.TabIndex = 25;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.panel14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel14.Location = new System.Drawing.Point(0, 30);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(577, 2);
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
            this.BtnClose.Location = new System.Drawing.Point(541, 0);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.BtnClose.Size = new System.Drawing.Size(33, 31);
            this.BtnClose.TabIndex = 39;
            this.BtnClose.TabStop = false;
            this.BtnClose.Text = "X";
            this.BtnClose.UseVisualStyleBackColor = false;
            // 
            // LblHeader
            // 
            this.LblHeader.AccessibleName = "";
            this.LblHeader.AutoSize = true;
            this.LblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.LblHeader.Location = new System.Drawing.Point(3, 3);
            this.LblHeader.Name = "LblHeader";
            this.LblHeader.Size = new System.Drawing.Size(182, 25);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "RECEIVING REPORT";
            // 
            // pnlTable
            // 
            this.pnlTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTable.Controls.Add(this.dgvRecevingList);
            this.pnlTable.Controls.Add(this.panel13);
            this.pnlTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTable.Location = new System.Drawing.Point(8, 32);
            this.pnlTable.Name = "pnlTable";
            this.pnlTable.Size = new System.Drawing.Size(561, 205);
            this.pnlTable.TabIndex = 26;
            // 
            // dgvRecevingList
            // 
            this.dgvRecevingList.AllowUserToAddRows = false;
            this.dgvRecevingList.AllowUserToDeleteRows = false;
            this.dgvRecevingList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecevingList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecevingList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DescName});
            this.dgvRecevingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecevingList.Location = new System.Drawing.Point(0, 32);
            this.dgvRecevingList.Name = "dgvRecevingList";
            this.dgvRecevingList.ReadOnly = true;
            this.dgvRecevingList.RowHeadersVisible = false;
            this.dgvRecevingList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecevingList.Size = new System.Drawing.Size(559, 171);
            this.dgvRecevingList.TabIndex = 1;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.panel13.Controls.Add(this.label2);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel13.Location = new System.Drawing.Point(0, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(559, 32);
            this.panel13.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(16, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Select Description";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.BtnSave);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel10.Location = new System.Drawing.Point(8, 237);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(561, 40);
            this.panel10.TabIndex = 29;
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(122)))));
            this.BtnSave.BorderColor = System.Drawing.Color.White;
            this.BtnSave.BorderRadius = 8;
            this.BtnSave.BorderSize = 0;
            this.BtnSave.FlatAppearance.BorderSize = 0;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.ForeColor = System.Drawing.Color.White;
            this.BtnSave.Location = new System.Drawing.Point(6, 5);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(108, 30);
            this.BtnSave.TabIndex = 67;
            this.BtnSave.TabStop = false;
            this.BtnSave.Text = "&Select";
            this.BtnSave.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(569, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(8, 245);
            this.panel2.TabIndex = 28;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(8, 245);
            this.panel1.TabIndex = 27;
            // 
            // DescName
            // 
            this.DescName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DescName.DataPropertyName = "DescName";
            this.DescName.HeaderText = "Description";
            this.DescName.Name = "DescName";
            this.DescName.ReadOnly = true;
            // 
            // frm_receiving_report_desc_selectin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 277);
            this.Controls.Add(this.pnlTable);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_receiving_report_desc_selectin";
            this.Text = "frm_receiving_report_desc_selectin";
            this.PnlHeader.ResumeLayout(false);
            this.PnlHeader.PerformLayout();
            this.pnlTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecevingList)).EndInit();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlHeader;
        private System.Windows.Forms.Panel panel14;
        private Customized_Components.CustomRoundedButton BtnClose;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel pnlTable;
        private System.Windows.Forms.DataGridView dgvRecevingList;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescName;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel10;
        private Customized_Components.CustomRoundedButton BtnSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}