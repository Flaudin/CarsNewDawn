using System;
using System.Drawing;
using System.Windows.Forms;

namespace CARS.Customized_Components
{
    internal class CustomTextbox : UserControl
    {
        private TextBox textBox;
        private PictureBox leftIconPictureBox;
        private PictureBox rightIconPictureBox;
        private string hintText = "";
        private Image leftIcon;
        private Image rightIcon;

        public CustomTextbox()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.BackColor = Color.White;
            this.Padding = new Padding(8);
            this.Size = new Size(150, 26);

            // TextBox
            textBox = new TextBox();
            textBox.Dock = DockStyle.Fill;
            textBox.BorderStyle = BorderStyle.None;
            textBox.TextChanged += TextBox_TextChanged;
            textBox.Enter += TextBox_Enter;
            textBox.Leave += TextBox_Leave;

            // Left Icon PictureBox
            leftIconPictureBox = new PictureBox();
            leftIconPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            leftIconPictureBox.Dock = DockStyle.Left;
            leftIconPictureBox.Width = 18;

            // Right Icon PictureBox
            rightIconPictureBox = new PictureBox();
            rightIconPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            rightIconPictureBox.Dock = DockStyle.Right;
            rightIconPictureBox.Width = 18;

            this.Controls.Add(textBox);
            this.Controls.Add(rightIconPictureBox);
            this.Controls.Add(leftIconPictureBox);
        }



        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (textBox.Text == hintText)
            {
                textBox.Text = "";
                textBox.ForeColor = SystemColors.WindowText;
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = hintText;
                textBox.ForeColor = SystemColors.GrayText;
            }
        }

        public string text
        {
            get { return textBox.Text == hintText ? "" : textBox.Text; }
            set { textBox.Text = value; }
        }

        public string HintText
        {
            get { return hintText; }
            set
            {
                hintText = value;
                textBox.Text = hintText;
                textBox.ForeColor = SystemColors.GrayText;
            }
        }

        public Image LeftIcon
        {
            get { return leftIcon; }
            set
            {
                leftIcon = value;
                leftIconPictureBox.Image = leftIcon;
            }
        }

        public Image RightIcon
        {
            get { return rightIcon; }
            set
            {
                rightIcon = value;
                rightIconPictureBox.Image = rightIcon;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int radius = 8; // Change this value to adjust the roundness of the corners
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddArc(ClientRectangle.X, ClientRectangle.Y, radius, radius, 180, 90);
            gp.AddArc(ClientRectangle.X + ClientRectangle.Width - radius, ClientRectangle.Y, radius, radius, 270, 90);
            gp.AddArc(ClientRectangle.X + ClientRectangle.Width - radius, ClientRectangle.Y + ClientRectangle.Height - radius, radius, radius, 0, 90);
            gp.AddArc(ClientRectangle.X, ClientRectangle.Y + ClientRectangle.Height - radius, radius, radius, 90, 90);
            this.Region = new Region(gp);
        }
    }
}
