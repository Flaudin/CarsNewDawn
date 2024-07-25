using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace CARS.Customized_Components
{
    public class CustomDateTime : DateTimePicker
    {
        private Color skinColor = Color.MediumSlateBlue;
        private Color textColor = Color.White;
        private Color borderColor = Color.PaleVioletRed;
        private int boderSize = 0;

        private bool droppedDown = false;
        private Image calendarIcon = Properties.Resources.Calendar_light;
        private RectangleF iconButtonArea;
        private const int calendarIconWidth = 34;
        private const int arrowIconWidth = 17;

        [Category("Flaudin DateTimePicker Controls")]
        public Color SkinColor
        {
            get
            {
                return skinColor;
            }
            set
            {
                skinColor = value;
                if (skinColor.GetBrightness() >= 0.8F)
                    calendarIcon = Properties.Resources.Calendar_dark;
                else calendarIcon = Properties.Resources.Calendar_light;
                this.Invalidate();
            }
        }
        [Category("Flaudin DateTimePicker Controls")]
        public Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                textColor = value;
                this.Invalidate();
            }
        }
        [Category("Flaudin DateTimePicker Controls")]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }
        [Category("Flaudin DateTimePicker Controls")]
        public int BoderSize
        {
            get
            {
                return boderSize;
            }
            set
            {
                boderSize = value;
                this.Invalidate();
            }
        }

        //Builder
        public CustomDateTime()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.MinimumSize = new Size(0, 35);
            this.Font = new Font(this.Font.Name, 9.5F);
        }

        //-> Behaviors
        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);
            droppedDown = true;
        }
        protected override void OnCloseUp(EventArgs e)
        {
            base.OnCloseUp(e);
            droppedDown = false;
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            e.Handled = true;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Graphics g = this.CreateGraphics())
            using (Pen penBorder = new Pen(borderColor, boderSize))
            using (SolidBrush skinBrush = new SolidBrush(skinColor))
            using (SolidBrush openIconBrush = new SolidBrush(Color.FromArgb(50, 64, 64, 64)))
            using (SolidBrush textBrush = new SolidBrush(textColor))
            using (StringFormat textFormat = new StringFormat())
            {
                RectangleF clientArea = new RectangleF(0, 0, this.Width - 0.5F, this.Height - 0.5F);
                RectangleF iconArea = new RectangleF(clientArea.Width - calendarIconWidth, 0, calendarIconWidth, clientArea.Height);
                penBorder.Alignment = PenAlignment.Inset;
                textFormat.LineAlignment = StringAlignment.Center;

                g.FillRectangle(skinBrush, clientArea);

                g.DrawString("   " + this.Text, this.Font, textBrush, clientArea, textFormat);
                if (droppedDown == true) g.FillRectangle(openIconBrush, iconArea);
                if (boderSize >= 1) g.DrawRectangle(penBorder, clientArea.X, clientArea.Y, clientArea.Width, clientArea.Height);
                g.DrawImage(calendarIcon, this.Width + 9 - calendarIconWidth, (this.Height - calendarIcon.Height) / 2);
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            int iconWidth = GetIconButtonWidth();
            iconButtonArea = new RectangleF(this.Width - iconWidth, 0, iconWidth, this.Height);
        }

        private int GetIconButtonWidth()
        {
            int textWidh = TextRenderer.MeasureText(this.Text, this.Font).Width;
            if (textWidh <= this.Width - (calendarIconWidth + 20))
                return calendarIconWidth;
            else return arrowIconWidth;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (iconButtonArea.Contains(e.Location))
                this.Cursor = Cursors.Hand;
            else this.Cursor = Cursors.Default;
        }
    }
}