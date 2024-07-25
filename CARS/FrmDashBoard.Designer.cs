namespace CARS
{
    partial class FrmDashBoard
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
            this.PictureDashboard = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureDashboard)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureDashboard
            // 
            this.PictureDashboard.BackColor = System.Drawing.Color.White;
            this.PictureDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureDashboard.Image = global::CARS.Properties.Resources.WarehouseNew;
            this.PictureDashboard.Location = new System.Drawing.Point(0, 0);
            this.PictureDashboard.Name = "PictureDashboard";
            this.PictureDashboard.Size = new System.Drawing.Size(967, 917);
            this.PictureDashboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureDashboard.TabIndex = 17;
            this.PictureDashboard.TabStop = false;
            // 
            // FrmDashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(967, 917);
            this.Controls.Add(this.PictureDashboard);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDashBoard";
            this.Text = "FrmDashBoard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.PictureDashboard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox PictureDashboard;
    }
}