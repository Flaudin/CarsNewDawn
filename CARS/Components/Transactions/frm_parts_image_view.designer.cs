namespace CARS.Components.Transactions
{
    partial class frm_parts_image_view
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
            this.BtnClose = new System.Windows.Forms.Label();
            this.lblPartName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbParts = new System.Windows.Forms.PictureBox();
            this.zoomOut = new System.Windows.Forms.Button();
            this.zoomIn = new System.Windows.Forms.Button();
            this.PnlHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbParts)).BeginInit();
            this.SuspendLayout();
            // 
            // PnlHeader
            // 
            this.PnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(215)))), ((int)(((byte)(243)))));
            this.PnlHeader.Controls.Add(this.BtnClose);
            this.PnlHeader.Controls.Add(this.lblPartName);
            this.PnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlHeader.Location = new System.Drawing.Point(0, 0);
            this.PnlHeader.Name = "PnlHeader";
            this.PnlHeader.Padding = new System.Windows.Forms.Padding(10);
            this.PnlHeader.Size = new System.Drawing.Size(586, 40);
            this.PnlHeader.TabIndex = 0;
            // 
            // BtnClose
            // 
            this.BtnClose.AutoSize = true;
            this.BtnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.BtnClose.Location = new System.Drawing.Point(558, 10);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(18, 19);
            this.BtnClose.TabIndex = 2;
            this.BtnClose.Text = "X";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click_1);
            // 
            // lblPartName
            // 
            this.lblPartName.AutoSize = true;
            this.lblPartName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPartName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPartName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(33)))), ((int)(((byte)(66)))));
            this.lblPartName.Location = new System.Drawing.Point(10, 10);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(81, 19);
            this.lblPartName.TabIndex = 1;
            this.lblPartName.Text = "Part Name";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pbParts);
            this.panel1.Location = new System.Drawing.Point(14, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(550, 550);
            this.panel1.TabIndex = 9;
            // 
            // pbParts
            // 
            this.pbParts.Location = new System.Drawing.Point(3, 0);
            this.pbParts.Name = "pbParts";
            this.pbParts.Size = new System.Drawing.Size(550, 550);
            this.pbParts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbParts.TabIndex = 1;
            this.pbParts.TabStop = false;
            this.pbParts.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbParts_MouseDown);
            this.pbParts.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbParts_MouseMove);
            this.pbParts.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbParts_MouseUp);
            // 
            // zoomOut
            // 
            this.zoomOut.AccessibleName = "";
            this.zoomOut.Location = new System.Drawing.Point(398, 31);
            this.zoomOut.Name = "zoomOut";
            this.zoomOut.Size = new System.Drawing.Size(75, 23);
            this.zoomOut.TabIndex = 10;
            this.zoomOut.Text = "Zoom Out";
            this.zoomOut.UseVisualStyleBackColor = true;
            this.zoomOut.Click += new System.EventHandler(this.button1_Click);
            // 
            // zoomIn
            // 
            this.zoomIn.Location = new System.Drawing.Point(479, 31);
            this.zoomIn.Name = "zoomIn";
            this.zoomIn.Size = new System.Drawing.Size(75, 23);
            this.zoomIn.TabIndex = 11;
            this.zoomIn.Text = "Zoom In";
            this.zoomIn.UseVisualStyleBackColor = true;
            this.zoomIn.Click += new System.EventHandler(this.zoomIn_Click);
            // 
            // frm_parts_image_view
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(586, 650);
            this.Controls.Add(this.zoomIn);
            this.Controls.Add(this.zoomOut);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_parts_image_view";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_parts_image_view";
            this.Load += new System.EventHandler(this.frm_parts_image_view_Load);
            this.PnlHeader.ResumeLayout(false);
            this.PnlHeader.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbParts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlHeader;
        private System.Windows.Forms.Label lblPartName;
        private System.Windows.Forms.Label BtnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbParts;
        private System.Windows.Forms.Button zoomOut;
        private System.Windows.Forms.Button zoomIn;
    }
}