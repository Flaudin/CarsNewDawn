using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Customized_Components
{
    internal class CustomGroupbox : GroupBox
    {
        public Color borderColor {  get; set; }
        public int borderWidth { get; set; }
        public int shadowDepth { get; set; }

        public Color shadowColor { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (GraphicsPath borderPath = CreateBorderPath(ClientRectangle,borderWidth))
            using (GraphicsPath shadowPath = CreateBorderPath(ClientRectangle, borderWidth + shadowDepth))
            using (Pen borderPen = new Pen(borderColor,borderWidth))
            using (Pen shadowPen = new Pen(shadowColor,shadowDepth))
            using (Graphics g = e.Graphics)
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                g.DrawPath(shadowPen, shadowPath);

                g.DrawPath(borderPen, borderPath);
            }    
        }



        private GraphicsPath CreateBorderPath(Rectangle rect, int borderWidth)
        {
            GraphicsPath path = new GraphicsPath();

            int offset = borderWidth / 2;
            path.AddRectangle(new Rectangle(rect.X + offset, rect.Y + offset, rect.Width - borderWidth, rect.Height - borderWidth));

            return path;
        }
    }
}
