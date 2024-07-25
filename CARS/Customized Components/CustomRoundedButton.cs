using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CARS.Customized_Components
{
    public class CustomRoundedButton : Button
    {
            private int borderSize = 0;
            private int borderRadius = 40;
            private Color borderColor = Color.White;

            public int BorderSize { get => borderSize; set { borderSize = value; this.Invalidate(); } }
            public int BorderRadius { get => borderRadius; set { borderRadius = value; this.Invalidate(); } }
            public Color BorderColor { get => borderColor; set { borderColor = value; this.Invalidate(); } }

            public CustomRoundedButton()
            {
                this.FlatStyle = FlatStyle.Flat;
                this.FlatAppearance.BorderSize = 0;
                this.Size = new Size(150, 40);
                this.BackColor = Color.MediumSlateBlue;
                this.ForeColor = Color.Aqua;
            }
            private GraphicsPath GetFigurePath(RectangleF rect, float radius)
            {
                GraphicsPath path = new GraphicsPath();
                path.StartFigure();
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();

                return path;
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);
                RectangleF rectBorder = new RectangleF(1, 1, this.Width - 0.8F, this.Height - 1);

                if (BorderRadius > 2)
                {
                    using (GraphicsPath pathSurface = GetFigurePath(rectSurface, BorderRadius))
                    using (GraphicsPath pathBorder = GetFigurePath(rectBorder, BorderRadius - 1F))
                    using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
                    using (Pen penBorder = new Pen(this.Parent.BackColor, BorderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        this.Region = new Region(pathSurface);
                        e.Graphics.DrawPath(penSurface, pathSurface);

                        if (BorderSize >= 1)
                            e.Graphics.DrawPath(penBorder, pathBorder);
                    }
                }
                else
                {
                    this.Region = new Region(rectSurface);
                    if (BorderSize >= 1)
                    {
                        using (Pen penBorder = new Pen(BorderColor, BorderSize))
                        {
                            penBorder.Alignment = PenAlignment.Inset;
                            e.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                        }
                    }
                }
            }
            protected override void OnHandleCreated(EventArgs e)
            {
                base.OnHandleCreated(e);
                this.Parent.BackColorChanged += new EventHandler(Container_BackColorChange);

            }

            private void Container_BackColorChange(object sender, EventArgs e)
            {
                if (this.DesignMode)
                    this.Invalidate();
            }
        }
}
