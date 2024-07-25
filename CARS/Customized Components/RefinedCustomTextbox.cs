using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace CARS.Customized_Components
{
    [DefaultEvent("_TextChanged")]
    public partial class RifinedCustomTextbox : UserControl
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
        private bool isPasswordchar = false;
        private CharacterCasing _characterCasing;

        public RifinedCustomTextbox()
        {
            InitializeComponent();
        }

        //Events
        public event EventHandler _TextChanged;

        [Category("Flaudin Textbox Controls")]
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

        [Category("Flaudin Textbox Controls")]
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

        [Category("Flaudin Textbox Controls")]
        public bool UnderlinedStyle
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

        [Category("Flaudin Textbox Controls")]
        public bool PasswordChar
        {
            get
            {
                return isPasswordchar;
            }
            set
            {
                isPasswordchar = value;
                textBoxx.UseSystemPasswordChar = value;
            }
        }
        [Category("Flaudin Textbox Controls")]
        public bool MultiLine
        {
            get
            {
                return textBoxx.Multiline;
            }
            set
            {
                textBoxx.Multiline = value;
            }
        }

        [Category("Flaudin Textbox Controls")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                textBoxx.BackColor = value;
            }
        }

        [Category("Flaudin Textbox Controls")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                textBoxx.ForeColor = value;
            }
        }

        [Category("Flaudin Textbox Controls")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                textBoxx.Font = value;
                if (this.DesignMode)
                    UpdateControlHeight();
            }
        }
        [Category("Flaudin Textbox Controls")]
        public string Textt
        {
            get
            {
                if (isPlaceholder) return "";
                else return textBoxx.Text;
            }
            set
            {
                textBoxx.Text = value;
                SetPlaceholder();
            }
        }


        [Category("Flaudin Textbox Controls")]
        public int BorderRadius
        {
            get
            {
                return borderRadius;
            }
            set
            {
                if (value >= 0)
                {
                    borderRadius = value;
                    this.Invalidate();
                }
            }
        }

        [Category("Flaudin Textbox Controls")]
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

        [Category("Flaudin Textbox Controls")]
        public Color PlaceholderColor
        {
            get
            {
                return placeholderColor;
            }
            set
            {
                placeholderColor = value;
                if (isPasswordchar)
                    textBoxx.ForeColor = value;
            }
        }

        [Category("Flaudin Textbox Controls")]
        public void SetPlaceholderText(string value)
        {
            placeholderText = value;
            textBoxx.Text = "";
            SetPlaceholder();
        }

        [Category("Flaudin Textbox Controls")]
        public int MaxLegnth
        {
            get
            {
                return textBoxx.MaxLength;
            }
            set
            {
                textBoxx.MaxLength = value;
            }
        }

        [Category("Flaudin Textbox Controls")]
        public bool ReadOnly
        {
            get
            {
                return textBoxx.ReadOnly;
            }
            set
            {
                textBoxx.ReadOnly = value;
            }
        }

        
        [Category("Flaudin Textbox Controls")]
        public CharacterCasing CharacterCasing
        {
            get
            {
                return textBoxx.CharacterCasing;
            }
            set
            {
                if(value == CharacterCasing.Upper || value == CharacterCasing.Lower || value == CharacterCasing.Normal) 
                {
                    textBoxx.CharacterCasing = value;
                }
                else
                {
                    textBoxx.CharacterCasing = CharacterCasing.Upper;
                }
            }
        }

        [Category("Flaudin Textbox Controls")]
        public ScrollBars ScrollBar
        {
            get
            {
                return textBoxx.ScrollBars;
            }
            set
            {
                if(value == ScrollBars.Vertical || value == ScrollBars.Horizontal || value == ScrollBars.Both)
                {
                    textBoxx.ScrollBars = value;
                }
            }
        }

        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(textBoxx.Text) && placeholderText != "")
            {
                isPlaceholder = true;
                textBoxx.Text = placeholderText;
                textBoxx.ForeColor = placeholderColor;
                if (isPasswordchar)
                    textBoxx.UseSystemPasswordChar = false;
            }
        }

        private void RemovePlaceholder()
        {
            if (isPlaceholder && placeholderText != "")
            {
                isPlaceholder = false;
                textBoxx.Text = "";
                textBoxx.ForeColor = this.ForeColor;
                if (isPasswordchar)
                    textBoxx.UseSystemPasswordChar = true;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            if (borderRadius > 1)
            {
                var rectBorderSmooth = this.ClientRectangle;
                var rectBorder = Rectangle.Inflate(rectBorderSmooth, -borderSize, -borderSize);
                int smoothSize = borderSize > 0 ? borderSize : 1;

                using (GraphicsPath pathBorderSmooth = getFigurePath(rectBorderSmooth, borderRadius))
                using (GraphicsPath pathBorder = getFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penBorderSmooth = new Pen(this.Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    this.Region = new Region(pathBorderSmooth);
                    if (borderRadius > 15) SetTextBoxRoundedRegion();
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                    if (underlinedStyle)
                    {
                        g.DrawPath(penBorderSmooth, pathBorderSmooth);
                        g.SmoothingMode = SmoothingMode.None;
                        g.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
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
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    this.Region = new Region(this.ClientRectangle);
                    penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    if (isFocused) penBorder.Color = borderFocusColor;
                    if (underlinedStyle) //Line Style
                        g.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                    else //Normal Style
                        g.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
                }
            }

        }

        private void SetTextBoxRoundedRegion()
        {
            GraphicsPath pathText;
            if (MultiLine)
            {
                pathText = getFigurePath(textBoxx.ClientRectangle, borderRadius - borderSize);
                textBoxx.Region = new Region(pathText);
            }
            else
            {
                pathText = getFigurePath(textBoxx.ClientRectangle, borderSize * 2);
                textBoxx.Region = new Region(pathText);
            }
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

        //Events
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.DesignMode)
                UpdateControlHeight();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateControlHeight();
        }

        private void UpdateControlHeight()
        {
            if (textBoxx.Multiline == false)
            {
                int txtHeight = TextRenderer.MeasureText("Text", this.Font).Height + 1;
                textBoxx.Multiline = true;
                textBoxx.MinimumSize = new Size(0, txtHeight);
                textBoxx.Multiline = false;

                this.Height = textBoxx.Height + this.Padding.Top + this.Padding.Bottom;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_TextChanged != null)
                _TextChanged.Invoke(sender, e);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            isFocused = true;
            this.Invalidate();
            RemovePlaceholder();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            isFocused = false;
            this.Invalidate();
            SetPlaceholder();
        }
    }
}