namespace CARS.Customized_Components
{
    partial class CustomMaskTextbox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mskTextbox = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // mskTextbox
            // 
            this.mskTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mskTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mskTextbox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskTextbox.Location = new System.Drawing.Point(10, 7);
            this.mskTextbox.Mask = "000-000-000-00000";
            this.mskTextbox.Name = "mskTextbox";
            this.mskTextbox.PromptChar = '0';
            this.mskTextbox.Size = new System.Drawing.Size(230, 16);
            this.mskTextbox.TabIndex = 0;
            this.mskTextbox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // CustomMaskTextbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mskTextbox);
            this.Name = "CustomMaskTextbox";
            this.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.Size = new System.Drawing.Size(250, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox mskTextbox;
    }
}
