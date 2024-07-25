using System.Drawing;
using System.Windows.Forms;

namespace CARS.Customized_Components
{
    internal class CustomCheckbox : CheckBox
    {
        public Color checkColor { get; set; } // Change this to the desired color
        public Color backGroundColor { get; set; }

        public CustomCheckbox()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle backgroundRect = new Rectangle(1, (Height - 14) / 2, 14, 14);

            // Draw the background
            using (SolidBrush brush = new SolidBrush(checkColor))
            {
                e.Graphics.FillRectangle(brush, backgroundRect);
            }

            // Draw the checkmark
            if (Checked)
            {
                using (Pen pen = new Pen(backGroundColor, 2))
                {
                    e.Graphics.DrawLine(pen, backgroundRect.Left + 1, backgroundRect.Top + 6, backgroundRect.Left + 5, backgroundRect.Bottom - 4);
                    e.Graphics.DrawLine(pen, backgroundRect.Left + 2, backgroundRect.Bottom - 5, backgroundRect.Right - 4, backgroundRect.Top + 2);
                }
            }
        }

        public Color BackgroundColor
        {
            get { return backGroundColor; }
            set
            {
                backGroundColor = value;
                Invalidate(); // Redraw the control when the background color changes
            }
        }
    }
}

