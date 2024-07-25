using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model.Masterfiles;
using CARS.Model.Utilities;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CARS.Components.Transactions
{
    public partial class frm_parts_image_view : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private PartsModel partsModel = new PartsModel();
        private poOrderTakingController poOrderTakingController = new poOrderTakingController();
        private Image imgOriginal;
        private int _zoomFactor;
        private string imageUrl;
        private Point magnifier;
        private int magnifierSize = 100;
        private float zoomFactor = 1.0f;
        private const float ZoomStep = 0.5f;
        private Size pictureBoxInitialSize = new Size(550, 550);
        private Point zoomOrigin;
        private bool isDragging = false;
        private Point lastMousePosition;
        private Point currentOffset = new Point(0, 0);
        bool isButtonZoomClicked = false;
        public frm_parts_image_view(string partName)
        {
            InitializeComponent();


            pbParts.Size = pictureBoxInitialSize; // Set initial size


            SetFormAppearance(partName);
            GetImage(partName);
            LoadImage(imageUrl);
            CenterImage();

            pbParts.MouseWheel += PictureBox1_MouseWheel;
            //panel1.AutoScroll = true;
            //panel1.MouseWheel += PictureBox1_MouseWheel;
            pbParts.MouseDown += pbParts_MouseDown;
            pbParts.MouseMove += pbParts_MouseMove;
            pbParts.MouseUp += pbParts_MouseUp;
        }
        private void Panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            // Prevent the Panel from scrolling with the mouse wheel
            ((HandledMouseEventArgs)e).Handled = true;
        }


        private void pbParts_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            { 
                isDragging = true;
                lastMousePosition = e.Location;
            }
        }

        // Event handler for mouse move to handle dragging
        private void pbParts_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && pbParts.Width > 600)
            {
                int deltaX = e.Location.X - lastMousePosition.X;
                int deltaY = e.Location.Y - lastMousePosition.Y;

                currentOffset.X += deltaX;
                currentOffset.Y += deltaY;
                if (currentOffset.X > 0)
                    currentOffset.X = 0;
                if (currentOffset.Y > 0)
                    currentOffset.Y = 0;
                if (currentOffset.X < panel1.Width - pbParts.Width)
                    currentOffset.X = panel1.Width - pbParts.Width;
                if (currentOffset.Y < panel1.Height - pbParts.Height)
                    currentOffset.Y = panel1.Height - pbParts.Height;

                pbParts.Location = currentOffset;

                lastMousePosition = e.Location;
            }
        }

        // Event handler for mouse up to stop dragging
        private void pbParts_MouseUp(object sender, MouseEventArgs e)
        {
            lastMousePosition = Point.Empty;
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void SetFormAppearance(string partName)
        {
            PnlHeader.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            BtnClose.ForeColor = lblPartName.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            lblPartName.Text = partName;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            // Forward the mouse wheel event to the PictureBox
            if (pbParts.Focused)
            {
                base.OnMouseWheel(e);
            }
        }

        private void GetImage(string partName)
        {
            imageUrl = poOrderTakingController.GetImage(partName);
        }

        private void LoadImage(string imageUrl)
        {
            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                byte[] partImage;
                if (Helper.IsBase64Encoded(imageUrl))
                {
                    partImage = Convert.FromBase64String(imageUrl);
                }
                else
                {
                    partImage = Encoding.Default.GetBytes(imageUrl);
                }

                using (MemoryStream ms = new MemoryStream(partImage))
                {
                    imgOriginal = Image.FromStream(ms);
                    imgOriginal = ScaleImage(imgOriginal, 550, 550);
                    pbParts.Image = imgOriginal;
                }
            }
        }

        private Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            // Calculate aspect ratio
            double ratioX = (double)maxWidth / image.Width;
            double ratioY = (double)maxHeight / image.Height;
            double ratio = Math.Min(ratioX, ratioY);

            // New image dimensions
            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);

            // Create new resized image
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }




        private void BtnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_parts_image_view_Load(object sender, EventArgs e)
        {
        }


        private void PictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pbParts.Focus();
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            Point mouseLocation = pbParts.PointToClient(MousePosition);

            if (isButtonZoomClicked)
            {
                // Use the center of the PictureBox when zooming with buttons
                mouseLocation = new Point(pbParts.Width / 2, pbParts.Height / 2);
                isButtonZoomClicked = false; // Reset the flag
            }
            else
            {
                // Use the actual mouse location when using the mouse wheel
                mouseLocation = pbParts.PointToClient(MousePosition);
            }


            float previousZoomFactor = zoomFactor;
            if (e.Delta > 0)
            {
                if (pbParts.Width >= pictureBoxInitialSize.Width*4 && pbParts.Height >= pictureBoxInitialSize.Height * 4)
                    return; // Prevent zooming in if already at max size
                            // Zoom in
                zoomFactor += ZoomStep;
            }
            else if (e.Delta < 0)
            {
                if (pbParts.Width <= pictureBoxInitialSize.Width && pbParts.Height <= pictureBoxInitialSize.Height)
                {
                    panel1.Width = 550;
                    panel1.Height = 550;   
                    CenterImage();
                    return; // Prevent zooming out if already at minimum size
                }


                // Zoom out
                zoomFactor = Math.Max(zoomFactor - ZoomStep, ZoomStep);
            }
            UpdatePictureBoxSize();
            AdjustImagePosition(mouseLocation, previousZoomFactor);

        }
        private void CenterImage()
        {
            int x = (pbParts.Width - pbParts.Image.Width) / 2;
            int y = (pbParts.Height - pbParts.Image.Height) / 2;

            // Ensure the image stays within bounds
            x = Math.Max(x, 0);
            y = Math.Max(y, 0);

            pbParts.Location = new Point(x, y);
        }

        private void UpdatePictureBoxSize()
        {
            double newWidth = (int)(imgOriginal.Width * zoomFactor);
            double newHeight = (int)(imgOriginal.Height * zoomFactor);

            if(newWidth > pictureBoxInitialSize.Width * 4)
            {
                newWidth = pictureBoxInitialSize.Width * 4;
            }
            if (newHeight > pictureBoxInitialSize.Height * 4)
            {
                newHeight = pictureBoxInitialSize.Height * 4;
            }
            // Update PictureBox size
            pbParts.Size = new Size((int)newWidth, (int)newHeight);
        }

        private void AdjustImagePosition(Point mouseLocation, float previousZoomFactor)
        {
            int centerX = pbParts.Width / 2;
            int centerY = pbParts.Height / 2;

            // Calculate the delta based on the zoom factors
            float deltaX = mouseLocation.X * (zoomFactor - previousZoomFactor);
            float deltaY = mouseLocation.Y * (zoomFactor - previousZoomFactor);

            // Update the offset
            currentOffset.X -= (int)deltaX;
            currentOffset.Y -= (int)deltaY;

            // Ensure the image stays within bounds
            if (currentOffset.X > 0)
                currentOffset.X = 0;
            if (currentOffset.Y > 0)
                currentOffset.Y = 0;
            if (currentOffset.X < panel1.Width - pbParts.Width)
                currentOffset.X = panel1.Width - pbParts.Width;
            if (currentOffset.Y < panel1.Height - pbParts.Height)
                currentOffset.Y = panel1.Height - pbParts.Height;

            // Update PictureBox location
            pbParts.Location = currentOffset;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mouseEventArgs = new MouseEventArgs(MouseButtons.None, 0, pbParts.Width / 2, pbParts.Height / 2, -120);
            isButtonZoomClicked = true;
            PictureBox1_MouseWheel(sender, mouseEventArgs);
        }

        private void zoomIn_Click(object sender, EventArgs e)
        {
            var mouseEventArgs = new MouseEventArgs(MouseButtons.None, 0, pbParts.Width / 2, pbParts.Height / 2, 120);
            isButtonZoomClicked = true;
            PictureBox1_MouseWheel(sender, mouseEventArgs);
        }
    }
}
