using CARS.Model.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CARS.Customized_Components
{
    internal class CustomCloseButton : Button
    {
        private ColorManager _ColorManager = new ColorManager();
        //private Color hoverColor = Color.FromArgb(181, 215, 243); // Default hover color
        private Color hoverColor = Color.FromArgb(55, 83, 116); // Default hover color
        private Color defaultColor = Color.FromArgb(5, 33, 66); // Default background color
        private int borderRadius = 0; // Default border radius
        private Color foreColor = SystemColors.Control;


        public CustomCloseButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseOverBackColor = hoverColor;
            this.BackColor = defaultColor;
            this.ForeColor = Color.FromArgb(_ColorManager.BackgroundRGB[0], _ColorManager.BackgroundRGB[1], _ColorManager.BackgroundRGB[2]);
            this.Font = new Font("Arial", 10, FontStyle.Regular);
            this.Size = new Size(100, 40); // Default size
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.BackColor = hoverColor;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
        }

        public Color HoverColor
        {
            get { return hoverColor; }
            set
            {
                hoverColor = value;
                this.FlatAppearance.MouseOverBackColor = hoverColor;
            }
        }

        public Color DefaultColor
        {
            get { return defaultColor; }
            set
            {
                defaultColor = value;
                this.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            }
        }

        public int BorderRadius
        {
            get { return borderRadius; }
            set { borderRadius = value; }
        }

        public new Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }
    }
}
