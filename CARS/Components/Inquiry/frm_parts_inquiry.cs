using CARS.Controller.Inquiry;
using CARS.Customized_Components;
using CARS.Functions;
using CARS.Model.Inquiry;
using CARS.Model.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Masterfiles
{
    public partial class frm_parts_inquiry : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private InquiryController _InquiryController = new InquiryController();
        private PartsInquiryController _PartsInquiryController = new PartsInquiryController();
        private PartsInquiryModel _PartsInquiryModel = new PartsInquiryModel();
        private DataTable PartTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_parts_inquiry(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridPart, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            ComboInquiryParts.ItemSelected += CustomTextBox_ItemSelected;
            ComboFilter.SelectedIndex = 0;
            dashboardCall = DashboardCall;
        }

        private void ComboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                _InquiryController.ChangeDistinctFilter(comboBox.SelectedItem.ToString());
                ComboInquiryParts.Text = "";
            }
        }

        private void CustomTextBox_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            DataTable PartTable = new DataTable();
            var boolColumn = new DataColumn("ForSelection", typeof(bool));
            switch (ComboFilter.SelectedItem.ToString())
            {
                case "ALL":
                    if (e.SelectedItem != "")
                    {
                        PartTable = _PartsInquiryController.PartsDataTable(e.SelectedItem);
                    }
                    else
                    {
                        PartTable = _PartsInquiryController.PartsDataTable(ComboInquiryParts.Text.TrimEnd());
                    }
                    break;

                case "BRAND":
                    PartTable = _PartsInquiryController.PartsDataTableFilterBrand(ComboInquiryParts.Text.TrimEnd());
                    break;

                case "DESCRIPTION":
                    PartTable = _PartsInquiryController.PartsDataTableFilterDescription(ComboInquiryParts.Text.TrimEnd());
                    break;
            }
            boolColumn.DefaultValue = false;
            PartTable.Columns.Add(boolColumn);
            ImagePart.Image = null;
            DataGridPart.RowEnter -= DataGridPart_RowEnter;
            DataGridPart.DataSource = PartTable;
            DataGridPart.ClearSelection();
            DataGridPart.RowEnter += DataGridPart_RowEnter;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtListPrice.Textt = "0.00";
                ImagePart.Image = null;
                PartTable.Rows.Clear();
            }
        }

        private void DataGridPart_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            TxtListPrice.Textt = DataGridPart.Rows[e.RowIndex].Cells["ListPrice"].Value?.ToString().TrimEnd();
            string ImageFromData = _PartsInquiryController.GetPartImage(DataGridPart.Rows[e.RowIndex].Cells["PartNo"].Value.ToString().TrimEnd());
            if (ImageFromData != "")
            {
                byte[] PartImages;
                if (Helper.IsBase64Encoded(ImageFromData))
                {
                    PartImages = Convert.FromBase64String(ImageFromData);
                }
                else
                {
                    PartImages = Encoding.Default.GetBytes(ImageFromData);
                    //async Task LoadImageAsync()
                    //{
                    //    using (MemoryStream ms = new MemoryStream(PartImages))
                    //    {
                    //        Image newImage = await Task.Run(() => Image.FromStream(ms));
                    //        ImagePart.Image = newImage;
                    //    }
                    //}
                    //LoadImageAsync();
                }
                using (MemoryStream ms = new MemoryStream(PartImages))
                {
                    Image NewImage = Image.FromStream(ms);
                    //Image resizedImage = NewImage.GetThumbnailImage(568, 320, null, IntPtr.Zero);
                    ImagePart.Image = NewImage;
                    ms.Dispose();
                }
            }
            else
            {
                ImagePart.Image = null;
            }
        }

        int CurrentCol = 1;
        private void DataGridPart_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int PriceIndex = DataGridPart.Columns["ListPrice"].Index;
            int ActiveIndex = DataGridPart.Columns["IsActive"].Index;
            if (!TxtColumnSearch.Visible && PartTable.Rows.Count > 0 && e.ColumnIndex != PriceIndex && e.ColumnIndex != ActiveIndex)
            {
                if (DataGridPart.Rows.Count == 0)
                {
                    PartTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridPart.Columns[e.ColumnIndex].HeaderText;
                TxtColumnSearch.Visible = true;
                TxtColumnSearch.Focus();
            }
        }

        private void TxtColumnSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                TxtColumnSearch.Visible = false;
            }
            else
            {
                string searchCol = DataGridPart.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = PartTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridPart.DataSource = bs;
            }
        }

        private void TxtColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtColumnSearch.Visible = false;
            DataGridPart.Focus();
        }

        private void frm_parts_inquiry_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter && !TxtColumnSearch.Visible)
            //{
            //    if (PnlFilter.ContainsFocus)
            //    {
            //        BtnSearch.PerformClick();
            //    }
            //}
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }
    }
}
