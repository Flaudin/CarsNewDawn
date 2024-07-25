namespace CARS.Components.Utilities
{
    partial class frm_user_profile_log
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
            this.panel20 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DataGridLog = new System.Windows.Forms.DataGridView();
            this.PnlHeaderTable = new System.Windows.Forms.Panel();
            this.LblTable = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.module_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.action_taken = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PnlMain.SuspendLayout();
            this.PnlHeader.SuspendLayout();
            this.Pnl.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridLog)).BeginInit();
            this.PnlHeaderTable.SuspendLayout();
            this.SuspendLayout();
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
            this.LblHeader.Size = new System.Drawing.Size(97, 25);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "USER LOG";
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
            this.Pnl.Size = new System.Drawing.Size(656, 368);
            this.Pnl.TabIndex = 11;
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.panel3);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel20.Location = new System.Drawing.Point(5, 5);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(646, 363);
            this.panel20.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.DataGridLog);
            this.panel3.Controls.Add(this.PnlHeaderTable);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(646, 363);
            this.panel3.TabIndex = 1;
            // 
            // DataGridLog
            // 
            this.DataGridLog.AllowUserToAddRows = false;
            this.DataGridLog.AllowUserToDeleteRows = false;
            this.DataGridLog.AllowUserToResizeColumns = false;
            this.DataGridLog.AllowUserToResizeRows = false;
            this.DataGridLog.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.DataGridLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridLog.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DataGridLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.module_name,
            this.action_taken,
            this.date_time});
            this.DataGridLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridLog.Location = new System.Drawing.Point(0, 32);
            this.DataGridLog.MultiSelect = false;
            this.DataGridLog.Name = "DataGridLog";
            this.DataGridLog.RowHeadersVisible = false;
            this.DataGridLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridLog.Size = new System.Drawing.Size(644, 329);
            this.DataGridLog.TabIndex = 5;
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
            this.LblTable.Size = new System.Drawing.Size(60, 19);
            this.LblTable.TabIndex = 0;
            this.LblTable.Text = "Log List";
            // 
            // panel10
            // 
            this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel10.Location = new System.Drawing.Point(0, 398);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(656, 10);
            this.panel10.TabIndex = 10;
            // 
            // module_name
            // 
            this.module_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.module_name.DataPropertyName = "module_name";
            this.module_name.FillWeight = 30F;
            this.module_name.HeaderText = "Module";
            this.module_name.Name = "module_name";
            this.module_name.ReadOnly = true;
            // 
            // action_taken
            // 
            this.action_taken.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.action_taken.DataPropertyName = "action_taken";
            this.action_taken.HeaderText = "Action";
            this.action_taken.Name = "action_taken";
            this.action_taken.ReadOnly = true;
            // 
            // date_time
            // 
            this.date_time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.date_time.DataPropertyName = "date_time";
            this.date_time.FillWeight = 30F;
            this.date_time.HeaderText = "Date";
            this.date_time.Name = "date_time";
            this.date_time.ReadOnly = true;
            // 
            // frm_user_profile_log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 410);
            this.Controls.Add(this.PnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_user_profile_log";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_purchase_return_reason_selection";
            this.PnlMain.ResumeLayout(false);
            this.PnlHeader.ResumeLayout(false);
            this.PnlHeader.PerformLayout();
            this.Pnl.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridLog)).EndInit();
            this.PnlHeaderTable.ResumeLayout(false);
            this.PnlHeaderTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PnlMain;
        private System.Windows.Forms.Panel PnlHeader;
        private System.Windows.Forms.Panel panel14;
        private Customized_Components.CustomRoundedButton BtnClose;
        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.Panel Pnl;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView DataGridLog;
        private System.Windows.Forms.Panel PnlHeaderTable;
        private System.Windows.Forms.Label LblTable;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.DataGridViewTextBoxColumn module_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn action_taken;
        private System.Windows.Forms.DataGridViewTextBoxColumn date_time;
    }
}