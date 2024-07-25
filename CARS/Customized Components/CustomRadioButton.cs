using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CARS.Customized_Components
{
    internal class CustomRadioButton : RadioButton
    {
        //private Color checkedColor = Color.Orange;
        //private Color uncheckedColor = Color.Gray;
        private Color checkedColor = Color.FromArgb(255, 206, 0);
        private Color uncheckedColor = Color.FromArgb(230, 231, 232);
        public Color CheckedColor
        {
            get { return checkedColor; }
            set { checkedColor = value; }
        }
        public Color UncheckedColor
        {
            get { return uncheckedColor; }
            set { uncheckedColor = value;
            this.Invalidate();}
        }
        public CustomRadioButton()
        {
            this.MinimumSize = new Size(0, 21);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float rbBorderSize = 18F;
            float rbCheckSize = 12F;
            RectangleF rectRbBorder = new RectangleF()
            {
                X = 0.5F,
                Y = (this.Height - rbBorderSize) / 2,
                Width = rbBorderSize,
                Height = rbBorderSize
            };
            RectangleF rectRbCheck = new RectangleF()
            {
                X = rectRbBorder.X + ((rectRbBorder.Width - rbCheckSize) / 2),
                Y = (this.Height - rbCheckSize) / 2,
                Width = rbCheckSize,
                Height = rbCheckSize
            };
            using (Pen penBorder = new Pen(checkedColor,1.6F))
                using(SolidBrush brushBorder = new SolidBrush(checkedColor))
                using(SolidBrush brushtext = new SolidBrush(this.ForeColor))
            {
                g.Clear(this.BackColor);
                if (this.Checked)
                {
                    g.DrawEllipse(penBorder, rectRbBorder);
                    g.FillEllipse(brushBorder, rectRbCheck);
                }
                else
                {
                    penBorder.Color = uncheckedColor;
                    g.DrawEllipse(penBorder, rectRbBorder);
                }
                g.DrawString(this.Text, this.Font, brushtext, rbBorderSize + 8, (this.Height - TextRenderer.MeasureText(this.Text, this.Font).Height) / 2);
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Width = TextRenderer.MeasureText(this.Text, this.Font).Width + 30;
        }
    }
}
