using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS
{
    internal class Gradient : Panel
    {
        public Color TopColor {  set; get; }
        public Color BottomColor { set; get;}

        public float Angle {  set; get; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, Width, Height);

            LinearGradientBrush brush = new LinearGradientBrush(rect, TopColor, BottomColor, Angle);
            g.FillRectangle(brush, rect);
        }
    }
}
