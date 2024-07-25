using System;
using System.Drawing;
using System.Windows.Forms;

namespace CARS.Customized_Components
{
    internal class CustomButton : Button
    {
        private Color hoverColor = Color.Aquamarine; // Default hover color
        private Color defaultColor = Color.Azure; // Default background color
        private int borderRadius = 15; // Default border radius
        private Color foreColor = Color.DarkGray;


        public CustomButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseOverBackColor = hoverColor;
            this.BackColor = defaultColor;
            this.ForeColor = Color.White;
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
            this.BackColor = defaultColor;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            int radius = 8;
            System.Drawing.Drawing2D.GraphicsPath gph = new System.Drawing.Drawing2D.GraphicsPath();
            gph.AddArc(ClientRectangle.X,  ClientRectangle.Y, radius, radius, 180,90);
            gph.AddArc(ClientRectangle.X + ClientRectangle.Width - radius, ClientRectangle.Y, radius, radius, 270, 90);
            gph.AddArc(ClientRectangle.X + ClientRectangle.Width - radius, ClientRectangle.Y + ClientRectangle.Height - radius, radius, radius, 0, 90);
            gph.AddArc(ClientRectangle.X, ClientRectangle.Y + ClientRectangle.Height - radius, radius, radius, 90, 90);
            this.Region = new Region(gph);
            // Draw text in the center of the button
            TextRenderer.DrawText(pevent.Graphics, this.Text, this.Font, this.ClientRectangle, this.ForeColor, Color.Transparent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
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
                this.BackColor = defaultColor;
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
