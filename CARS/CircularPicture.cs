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
    internal class CircularPicture : PictureBox
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(new Rectangle(0, 0, this.Width - 1, this.Height - 1));
            Region = new System.Drawing.Region(gp);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawEllipse(new Pen(Color.Gray, 2), new Rectangle(0,0,this.Width -1, this.Height -1));

        }
    }
}
