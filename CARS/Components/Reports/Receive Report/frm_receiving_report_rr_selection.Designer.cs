namespace CARS.Components.Reports.Receive_Report
{
    partial class frm_receiving_report_rr_selection
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
            this.panel10 = new System.Windows.Forms.Panel();
            this.BtnSave = new CARS.Customized_Components.CustomRoundedButton();
            this.PnlHeader = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.BtnClose = new CARS.Customized_Components.CustomRoundedButton();
            this.LblHeader = new System.Windows.Forms.Label();
            this.pnlTable = new System.Windows.Forms.Panel();
            this.dgvRRList = new System.Windows.Forms.DataGridView();
            this.RRNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PnlSearch = new System.Windows.Forms.Panel();
            this.LblSearch = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnRRSearch = new CARS.Customized_Components.CustomRoundedButton();
            this.txtRRSearch = new CARS.Customized_Components.RifinedCustomTextbox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel10.SuspendLayout();
            this.PnlHeader.SuspendLayout();
            this.pnlTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRRList)).BeginInit();
            this.PnlSearch.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.BtnSave);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel10.Location = new System.Drawing.Point(8, 237);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(561, 40);
            this.panel10.TabIndex = 19;
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
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
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
            this.PnlHeader.TabIndex = 15;
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
            this.LblHeader.Size = new System.Drawing.Size(182, 25);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "RECEIVING REPORT";
            // 
            // pnlTable
            // 
            this.pnlTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTable.Controls.Add(this.dgvRRList);
            this.pnlTable.Controls.Add(this.PnlSearch);
            this.pnlTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTable.Location = new System.Drawing.Point(8, 72);
            this.pnlTable.Name = "pnlTable";
            this.pnlTable.Size = new System.Drawing.Size(561, 165);
            this.pnlTable.TabIndex = 16;
            // 
            // dgvRRList
            // 
            this.dgvRRList.AllowUserToAddRows = false;
            this.dgvRRList.AllowUserToDeleteRows = false;
            this.dgvRRList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRRList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRRList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRRList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RRNo,
            this.CreatedDt,
            this.Status});
            this.dgvRRList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRRList.Location = new System.Drawing.Point(0, 32);
            this.dgvRRList.Name = "dgvRRList";
            this.dgvRRList.ReadOnly = true;
            this.dgvRRList.RowHeadersVisible = false;
            this.dgvRRList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRRList.Size = new System.Drawing.Size(559, 131);
            this.dgvRRList.TabIndex = 1;
            // 
            // RRNo
            // 
            this.RRNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RRNo.DataPropertyName = "RRNo";
            this.RRNo.HeaderText = "RR no.";
            this.RRNo.Name = "RRNo";
            this.RRNo.ReadOnly = true;
            // 
            // CreatedDt
            // 
            this.CreatedDt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CreatedDt.DataPropertyName = "CreatedDt";
            this.CreatedDt.HeaderText = "Receive Date";
            this.CreatedDt.Name = "CreatedDt";
            this.CreatedDt.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Visible = false;
            // 
            // PnlSearch
            // 
            this.PnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlSearch.Controls.Add(this.LblSearch);
            this.PnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlSearch.Location = new System.Drawing.Point(0, 0);
            this.PnlSearch.Name = "PnlSearch";
            this.PnlSearch.Size = new System.Drawing.Size(559, 32);
            this.PnlSearch.TabIndex = 3;
            // 
            // LblSearch
            // 
            this.LblSearch.AutoSize = true;
            this.LblSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblSearch.Location = new System.Drawing.Point(16, 9);
            this.LblSearch.Name = "LblSearch";
            this.LblSearch.Size = new System.Drawing.Size(61, 15);
            this.LblSearch.TabIndex = 1;
            this.LblSearch.Text = "Select RR";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(569, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(8, 245);
            this.panel2.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(8, 245);
            this.panel1.TabIndex = 17;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnRRSearch);
            this.panel3.Controls.Add(this.txtRRSearch);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(8, 32);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(561, 40);
            this.panel3.TabIndex = 2;
            // 
            // btnRRSearch
            // 
            this.btnRRSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRRSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(122)))));
            this.btnRRSearch.BorderColor = System.Drawing.Color.White;
            this.btnRRSearch.BorderRadius = 8;
            this.btnRRSearch.BorderSize = 0;
            this.btnRRSearch.FlatAppearance.BorderSize = 0;
            this.btnRRSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRRSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRRSearch.ForeColor = System.Drawing.Color.White;
            this.btnRRSearch.Image = global::CARS.Properties.Resources.Search;
            this.btnRRSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRRSearch.Location = new System.Drawing.Point(416, 5);
            this.btnRRSearch.Name = "btnRRSearch";
            this.btnRRSearch.Size = new System.Drawing.Size(108, 30);
            this.btnRRSearch.TabIndex = 68;
            this.btnRRSearch.TabStop = false;
            this.btnRRSearch.Text = "&Search";
            this.btnRRSearch.UseVisualStyleBackColor = false;
            this.btnRRSearch.Click += new System.EventHandler(this.btnRRSearch_Click);
            // 
            // txtRRSearch
            // 
            this.txtRRSearch.BackColor = System.Drawing.SystemColors.Window;
            this.txtRRSearch.BorderColor = System.Drawing.Color.Silver;
            this.txtRRSearch.BorderFocusColor = System.Drawing.Color.MidnightBlue;
            this.txtRRSearch.BorderRadius = 8;
            this.txtRRSearch.BorderSize = 1;
            this.txtRRSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtRRSearch.Location = new System.Drawing.Point(80, 5);
            this.txtRRSearch.MaxLegnth = 32767;
            this.txtRRSearch.MultiLine = false;
            this.txtRRSearch.Name = "txtRRSearch";
            this.txtRRSearch.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtRRSearch.PasswordChar = false;
            this.txtRRSearch.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtRRSearch.ReadOnly = false;
            this.txtRRSearch.ScrollBar = System.Windows.Forms.ScrollBars.None;
            this.txtRRSearch.Size = new System.Drawing.Size(320, 30);
            this.txtRRSearch.TabIndex = 1;
            this.txtRRSearch.Textt = "";
            this.txtRRSearch.UnderlinedStyle = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search";
            // 
            // frm_receiving_report_rr_selection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 277);
            this.Controls.Add(this.pnlTable);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_receiving_report_rr_selection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_receiving_report_rr_selection";
            this.panel10.ResumeLayout(false);
            this.PnlHeader.ResumeLayout(false);
            this.PnlHeader.PerformLayout();
            this.pnlTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRRList)).EndInit();
            this.PnlSearch.ResumeLayout(false);
            this.PnlSearch.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel10;
        private Customized_Components.CustomRoundedButton BtnSave;
        private System.Windows.Forms.Panel PnlHeader;
        private System.Windows.Forms.Panel panel14;
        private Customized_Components.CustomRoundedButton BtnClose;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel pnlTable;
        private System.Windows.Forms.DataGridView dgvRRList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel PnlSearch;
        private System.Windows.Forms.Panel panel3;
        private Customized_Components.CustomRoundedButton btnRRSearch;
        private Customized_Components.RifinedCustomTextbox txtRRSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn RRNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}