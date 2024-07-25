namespace CARS.Components.Transactions.Purchase_Return
{
    partial class frm_purchase_return_reason_selection
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
            this.BtnSave = new CARS.Customized_Components.CustomRoundedButton();
            this.PnlMain = new System.Windows.Forms.Panel();
            this.PnlHeader = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.BtnClose = new CARS.Customized_Components.CustomRoundedButton();
            this.LblHeader = new System.Windows.Forms.Label();
            this.Pnl = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvReason = new System.Windows.Forms.DataGridView();
            this.ReasonName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PnlHeaderTable = new System.Windows.Forms.Panel();
            this.LblTable = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.BtnClear = new CARS.Customized_Components.CustomRoundedButton();
            this.PnlMain.SuspendLayout();
            this.PnlHeader.SuspendLayout();
            this.Pnl.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReason)).BeginInit();
            this.PnlHeaderTable.SuspendLayout();
            this.panel10.SuspendLayout();
            this.SuspendLayout();
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
            // PnlMain
            // 
            this.PnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlMain.Controls.Add(this.PnlHeader);
            this.PnlMain.Controls.Add(this.Pnl);
            this.PnlMain.Controls.Add(this.panel10);
            this.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMain.Location = new System.Drawing.Point(0, 0);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.PnlMain.Size = new System.Drawing.Size(658, 410);
            this.PnlMain.TabIndex = 11;
            // 
            // PnlHeader
            // 
            this.PnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlHeader.BackColor = System.Drawing.Color.Yellow;
            this.PnlHeader.Controls.Add(this.panel14);
            this.PnlHeader.Controls.Add(this.BtnClose);
            this.PnlHeader.Controls.Add(this.LblHeader);
            this.PnlHeader.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlHeader.Location = new System.Drawing.Point(0, 0);
            this.PnlHeader.Name = "PnlHeader";
            this.PnlHeader.Size = new System.Drawing.Size(658, 32);
            this.PnlHeader.TabIndex = 9;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.panel14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel14.Location = new System.Drawing.Point(0, 30);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(658, 2);
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
            this.BtnClose.Location = new System.Drawing.Point(622, 0);
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
            this.LblHeader.Size = new System.Drawing.Size(187, 25);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "REASON SELECTION";
            // 
            // Pnl
            // 
            this.Pnl.BackColor = System.Drawing.SystemColors.Control;
            this.Pnl.Controls.Add(this.panel20);
            this.Pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pnl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pnl.Location = new System.Drawing.Point(0, 30);
            this.Pnl.Name = "Pnl";
            this.Pnl.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.Pnl.Size = new System.Drawing.Size(656, 338);
            this.Pnl.TabIndex = 11;
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.panel3);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel20.Location = new System.Drawing.Point(5, 5);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(646, 333);
            this.panel20.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.dgvReason);
            this.panel3.Controls.Add(this.PnlHeaderTable);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(646, 333);
            this.panel3.TabIndex = 1;
            // 
            // dgvReason
            // 
            this.dgvReason.AllowUserToAddRows = false;
            this.dgvReason.AllowUserToDeleteRows = false;
            this.dgvReason.AllowUserToResizeColumns = false;
            this.dgvReason.AllowUserToResizeRows = false;
            this.dgvReason.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvReason.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReason.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvReason.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReason.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReasonName});
            this.dgvReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReason.Location = new System.Drawing.Point(0, 32);
            this.dgvReason.MultiSelect = false;
            this.dgvReason.Name = "dgvReason";
            this.dgvReason.RowHeadersVisible = false;
            this.dgvReason.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReason.Size = new System.Drawing.Size(644, 299);
            this.dgvReason.TabIndex = 5;
            // 
            // ReasonName
            // 
            this.ReasonName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ReasonName.DataPropertyName = "ReasonName";
            this.ReasonName.HeaderText = "Reason";
            this.ReasonName.Name = "ReasonName";
            this.ReasonName.ReadOnly = true;
            // 
            // PnlHeaderTable
            // 
            this.PnlHeaderTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeaderTable.Controls.Add(this.LblTable);
            this.PnlHeaderTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlHeaderTable.Location = new System.Drawing.Point(0, 0);
            this.PnlHeaderTable.Name = "PnlHeaderTable";
            this.PnlHeaderTable.Size = new System.Drawing.Size(644, 32);
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
            this.LblTable.Size = new System.Drawing.Size(83, 19);
            this.LblTable.TabIndex = 0;
            this.LblTable.Text = "Reason List";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.BtnClear);
            this.panel10.Controls.Add(this.BtnSave);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel10.Location = new System.Drawing.Point(0, 368);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(656, 40);
            this.panel10.TabIndex = 10;
            // 
            // BtnClear
            // 
            this.BtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(122)))));
            this.BtnClear.BorderColor = System.Drawing.Color.White;
            this.BtnClear.BorderRadius = 8;
            this.BtnClear.BorderSize = 0;
            this.BtnClear.FlatAppearance.BorderSize = 0;
            this.BtnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClear.ForeColor = System.Drawing.Color.White;
            this.BtnClear.Image = global::CARS.Properties.Resources.Delete;
            this.BtnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnClear.Location = new System.Drawing.Point(120, 5);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(108, 30);
            this.BtnClear.TabIndex = 38;
            this.BtnClear.TabStop = false;
            this.BtnClear.Text = "Clear";
            this.BtnClear.UseVisualStyleBackColor = false;
            this.BtnClear.Visible = false;
            // 
            // frm_purchase_return_reason_selection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 410);
            this.Controls.Add(this.PnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_purchase_return_reason_selection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_purchase_return_reason_selection";
            this.PnlMain.ResumeLayout(false);
            this.PnlHeader.ResumeLayout(false);
            this.PnlHeader.PerformLayout();
            this.Pnl.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReason)).EndInit();
            this.PnlHeaderTable.ResumeLayout(false);
            this.PnlHeaderTable.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Customized_Components.CustomRoundedButton BtnSave;
        private System.Windows.Forms.Panel PnlMain;
        private System.Windows.Forms.Panel PnlHeader;
        private System.Windows.Forms.Panel panel14;
        private Customized_Components.CustomRoundedButton BtnClose;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel Pnl;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvReason;
        private System.Windows.Forms.Panel PnlHeaderTable;
        private System.Windows.Forms.Label LblTable;
        private System.Windows.Forms.Panel panel10;
        private Customized_Components.CustomRoundedButton BtnClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReasonName;
    }
}