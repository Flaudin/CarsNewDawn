using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CARS.Customized_Components
{
    public partial class CustomMaskDiscountSchemeTextbox : UserControl
    {
        private Color borderColor = Color.Silver;
        private int borderSize = 1;
        private bool underlinedStyle = false;
        private int borderRadius = 8;
        private Color borderFocusColor = Color.MidnightBlue;
        private bool isFocused = false;

        private Color placeholderColor = Color.Gray;
        private string placeholderText = "";
        private bool isPlaceholder = false;

        public CustomMaskDiscountSchemeTextbox()
        {
            InitializeComponent();
        }

        [Category("Flaudin MaskedTextbox Controls")]
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

        [Category("Flaudin MaskedTextbox Controls")]
        public int BorderSize
        {
            get
            {
                return borderSize;
            }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        [Category("Flaudin MaskedTextbox Controls")]
        public bool UnderLinedStyle
        {
            get
            {
                return underlinedStyle;
            }
            set
            {
                underlinedStyle = value;
                this.Invalidate();
            }
        }

        [Category("Flaudin MaskedTextbox Controls")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                mskTextbox.BackColor = value;
            }
        }

        [Category("Flaudin MaskedTextbox Controls")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                mskTextbox.ForeColor = value;
            }
        }

        [Category("Flaudin MaskedTextbox Controls")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                mskTextbox.Font = value;
                //if (this.DesignMode)
                //    UpdateControlHeight();
            }
        }

        [Category("Flaudin MaskedTextbox Controls")]
        public string Textt
        {
            get
            {
                return mskTextbox.Text;
            }
            set
            {
                mskTextbox.Text = value;
                //if (this.DesignMode)
                //    UpdateControlHeight();
            }
        }

        [Category("Flaudin MaskedTextbox Controls")]
        public int BorderRadius
        {
            get
            {
                return borderRadius;
            }
            set
            {
               if(value >= 0)
                {
                    borderRadius = value;
                    this.Invalidate();
                }
            }
        }

        [Category("Flaudin MaskedTextbox Controls")]
        public Color BorderFocusColor
        {
            get
            {
                return borderFocusColor;
            }
            set
            {
                borderFocusColor = value;
            }
        }

        [Category("Flaudin MaskedTextbox Controls")]
        public Color PlaceholderColor
        {
            get
            {
                return placeholderColor;
            }
            set
            {
                placeholderColor = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            if(borderRadius > 1)
            {
                var rectBorderSmooth = this.ClientRectangle;
                var rectBorder = Rectangle.Inflate(rectBorderSmooth, - borderSize, - borderSize);
                int smoothSize = borderSize > 0 ? borderSize : 1;

                using(GraphicsPath pathBorderSmooth = getFigurePath(rectBorderSmooth, borderRadius))
                using(GraphicsPath pathBorder = getFigurePath(rectBorder, borderRadius - borderSize))
                using(Pen penBorderSmooth = new Pen(this.Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    this.Region = new Region(pathBorderSmooth);
                    if (borderRadius > 15) SetTextBoxRoundedRegion();
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                    if (underlinedStyle)
                    {
                        g.DrawPath(penBorderSmooth,pathBorderSmooth);
                        g.SmoothingMode = SmoothingMode.None;
                        g.DrawLine(penBorder, 0, this.Height - 1, this.Width - 1, this.Height -1);
                    }
                    else
                    {
                        g.DrawPath(penBorderSmooth, pathBorderSmooth);
                        g.DrawPath(penBorder, pathBorder);
                    }
                }
            }
            else
            {
                using(Pen penBorder = new Pen(borderColor, borderSize))
                {
                    this.Region = new Region(this.ClientRectangle);
                    penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    if (isFocused) penBorder.Color = borderFocusColor;
                    if (underlinedStyle)
                        g.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                    else
                    {
                        g.DrawRectangle(penBorder, 0, 0, this.Width -0.5F, this.Height - 0.5F);
                    }
                }
            }


        }
            private void SetTextBoxRoundedRegion()
            {
                GraphicsPath pathText;
                    pathText = getFigurePath(mskTextbox.ClientRectangle, borderSize * 2);
                    mskTextbox.Region = new Region(pathText);
                
            }
            private GraphicsPath getFigurePath(Rectangle rect, int radius)
            {
                GraphicsPath path = new GraphicsPath();
                float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

    }
}
