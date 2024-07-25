using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model.Transactions;
using CARS.Model.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace CARS.Components.Transactions
{
    public partial class frm_price_management : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private PriceManagementController _PriceManagementController = new PriceManagementController();
        private PriceManagementModel _PriceManagementModel = new PriceManagementModel();
        private TransactionController _TransactionController = new TransactionController();
        private SortedDictionary<string, string> _MeasurementDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _BrandDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _DescriptionDictionary = new SortedDictionary<string, string>();
        private DataTable PartTable = new DataTable();
        private DataTable PurchaseTable = new DataTable();
        private DataTable PriceTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();
        private Image headerImage;
        private SalesOrderReportModel OwnerCompany = new SalesOrderReportModel();
        private SalesOrderReportCustomer CustomerOrder = new SalesOrderReportCustomer();

        public frm_price_management(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderDetails.BackColor = PnlHeaderFilter.BackColor = PnlHeaderParts.BackColor = PnlHeaderPriceHistory.BackColor = 
                PnlHeaderPurchaseHistory.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblPurchaseHistory.ForeColor = LblFilter.ForeColor = LblDetails.ForeColor = LblParts.ForeColor = LblPriceHistory.ForeColor = 
                LblPriceHistory.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _MeasurementDictionary = _PriceManagementController.GetDictionary("Uom");
            _BrandDictionary = _PriceManagementController.GetDictionary("Brand");
            _DescriptionDictionary = _TransactionController.GetDictionary("Description");
            ComboUomFilter.DataSource = new BindingSource(_MeasurementDictionary, null);
            ComboBrandFilter.DataSource = new BindingSource(_BrandDictionary, null);
            ComboDescriptionFilter.DataSource = new BindingSource(_DescriptionDictionary, null);
            ComboDescriptionFilter.DisplayMember = ComboBrandFilter.DisplayMember = ComboUomFilter.DisplayMember = "Key";
            ComboDescriptionFilter.ValueMember = ComboBrandFilter.ValueMember = ComboUomFilter.ValueMember = "Value";
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridPart, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            dashboardCall = DashboardCall;
            //
            printDocument1.DefaultPageSettings.Landscape = true;
            printDocument1.PrintPage += printDocument1_PrintPage;
            printPreviewDialog1.Document = printDocument1;
        }

        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
            if (PartTable.Rows.Count != 0)
            {
                if (Helper.Confirmator("This will clear all unsaved changes, continue?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    SearchParts();
                }
            }
            else
            {
                SearchParts();
            }
        }

        private void BtnClear_Click(object sender, System.EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                ClearData();
            }
        }

        private void SearchParts()
        {
            PartsPricePartModel _PartsPricePartModel = new PartsPricePartModel { PartNo = TxtPartNoFilter.Textt.TrimEnd(), PartName = TxtPartNameFilter.Textt.TrimEnd(), 
                                                                                 Sku = TxtSkuFiIter.Textt.TrimEnd(), Desc = ComboDescriptionFilter.SelectedValue.ToString().TrimEnd(),
                                                                                 Brand = ComboBrandFilter.SelectedValue.ToString().TrimEnd(),
                                                                                 Uom = ComboUomFilter.SelectedValue.ToString().TrimEnd() };
            PartTable = _PriceManagementController.PartsWithBegBalDataTable(_PartsPricePartModel);
            var decimalColumn = new DataColumn("NewPrice", typeof(string));
            decimalColumn.DefaultValue = 0.00;
            PartTable.Columns.Add(decimalColumn);
            DataGridPart.DataSource = PartTable;
        }

        private void DataGridPart_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            PurchaseTable.Rows.Clear();
            PriceTable.Rows.Clear();
            TxtLastPurchPrice.Textt = "0.00";
            TxtMarkUp.Textt = "0.00";
            TxtSuggRetailPrice.Textt = "0.00";
            TxtListPrice.Textt = DataGridPart.Rows[e.RowIndex].Cells["ListPrice"].Value.ToString();
            //NumericNewPrice.Value = Convert.ToDecimal(DataGridPart.CurrentRow.Cells["NewPrice"].Value ?? 0.00);
            PurchaseTable = _PriceManagementController.PurchaseHistoryDataTable(DataGridPart.Rows[e.RowIndex].Cells["PartNo"].Value.ToString());
            DataGridPurchase.DataSource = PurchaseTable;
            DataGridPurchase.ClearSelection();
            PriceTable = _PriceManagementController.PartsHistoryDataTable(DataGridPart.Rows[e.RowIndex].Cells["PartNo"].Value.ToString());
            DataGridPrice.DataSource = PriceTable;
            DataGridPrice.Sort(DataGridPrice.Columns[0], ListSortDirection.Descending);
            DataGridPrice.ClearSelection();
        }

        private void DataGridPurchase_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (PurchaseTable.Rows.Count > 0)
            {
                var LastPurchase = DataGridPurchase.Rows[0].Cells["UnitPrice"].Value;
                var MarkUp = Convert.ToDecimal(LastPurchase) / 2;
                TxtLastPurchPrice.Textt = LastPurchase.ToString();
                TxtMarkUp.Textt = (MarkUp).ToString();
                TxtSuggRetailPrice.Textt = (Convert.ToDecimal(LastPurchase) + MarkUp).ToString();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (DataGridPart.Rows.Count > 0)
            {
                bool AllowedSave = false;
                foreach (DataRow row in PartTable.Rows)
                {
                    object cellValue = row["NewPrice"];
                    if (Convert.ToDouble(cellValue ?? 0.00) != 0.00)
                    {
                        AllowedSave = true;
                        break;
                    }
                }

                if (AllowedSave)
                {
                    if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        string CustomMsg = "";
                        List<PartPriceModel> DetailsList = new List<PartPriceModel>();
                        foreach (DataRow row in PartTable.Rows)
                        {
                            if (row["NewPrice"] != null)
                            {
                                PartPriceModel Details = new PartPriceModel { PartNo = row["PartNo"].ToString(), 
                                                                              ListPrice = Convert.ToDecimal(row["NewPrice"]), Status = 1, };
                                DetailsList.Add(Details);
                            }
                        }

                        _PriceManagementModel = new PriceManagementModel { DetailsList = DetailsList };
                        CustomMsg = _PriceManagementController.Create(_PriceManagementModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (CustomMsg == "Information saved successfully")
                        {
                            ClearData();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please modify at least one part price before saving.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please modify at least one part price before saving.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DataGridPart_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int editIndex = DataGridPart.Columns["NewPrice"].Index;
            if (DataGridPart.CurrentCell.ColumnIndex == editIndex)
            {
                e.Control.KeyPress += Numeric_KeyPress;
            }
        }

        private void Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }

            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        private void ClearData()
        {
            TxtPartNoFilter.Textt = TxtPartNameFilter.Textt = TxtSkuFiIter.Textt = "";
            ComboBrandFilter.SelectedIndex = ComboUomFilter.SelectedIndex = ComboDescriptionFilter.SelectedIndex =  0;
            PartTable.Rows.Clear();
            PurchaseTable.Rows.Clear();
            PriceTable.Rows.Clear();
        }

        private void BtnApplyAll_Click(object sender, EventArgs e)
        {
            if (DataGridPart.CurrentRow != null && Helper.Confirmator("This will apply the selected part new price to all rows. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                foreach (DataGridViewRow row in DataGridPart.Rows)
                {
                    if (row != DataGridPart.CurrentRow)
                    {
                        row.Cells["NewPrice"].Value = DataGridPart.CurrentRow.Cells["NewPrice"].Value;
                    }
                }
            }
        }

        private void frm_price_management_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter && !TxtColumnSearch.Visible)
            if (e.KeyCode == Keys.Enter)
            {
                if (PnlFilter.ContainsFocus)
                {
                    BtnSearch.PerformClick();
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }

        int CurrentCol = 1;
        private void DataGridPart_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int PriceIndex = DataGridPart.Columns["ListPrice"].Index;
            int NewPriceIndex = DataGridPart.Columns["NewPrice"].Index;
            if (!TxtColumnSearch.Visible && PartTable.Rows.Count > 0 && e.ColumnIndex != PriceIndex && e.ColumnIndex != NewPriceIndex)
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
                //if (CurrentCol == DataGridPart.Columns["ListPrice"].Index && valueSearch != "")
                //{
                //    bs.Filter = $"[{ searchCol}] = '{valueSearch}'";
                //}
                //else if (CurrentCol != DataGridPart.Columns["ListPrice"].Index)
                //{
                //    bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                //}
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridPart.DataSource = bs;
            }
        }

        private void TxtColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtColumnSearch.Visible = false;
            DataGridPart.Focus();
        }

        private void frm_parts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !TxtColumnSearch.Visible)
            {
                if (PnlFilter.ContainsFocus)
                {
                    BtnSearch.PerformClick();
                }
            }
        }

        private void DataGridPart_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int editIndex = DataGridPart.Columns["NewPrice"].Index;
            if (e.ColumnIndex == editIndex && DataGridPart.Rows[e.RowIndex].Cells[editIndex].Value.ToString() == "")
            {
                DataGridPart.Rows[e.RowIndex].Cells[editIndex].Value = "0.00";
            }
        }

        private void BtnArchive_Click(object sender, EventArgs e)
        {
            frm_price_management_archive archivePriceMngmt = new frm_price_management_archive();
            archivePriceMngmt.ShowDialog(this);
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (DataGridPart.CurrentRow != null)
            {
                OwnerCompany = _TransactionController.GetOwnerCompany();
                headerImage = getImage();
                ((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized;
                printPreviewDialog1.ShowDialog();
            }
            else
            {
                MessageBox.Show("There are no SO selected for payment.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private int holder = 0;
        private Image getImage()
        {
            holder = 0;
            string compImage = _TransactionController.getCompanyImage();
            if (OwnerCompany.CompLogo != "")
            {
                byte[] CompanyImage = Convert.FromBase64String(OwnerCompany.CompLogo);
                using (MemoryStream ms = new MemoryStream(CompanyImage))
                {
                    Image newImage = Image.FromStream(ms);
                    headerImage = newImage;
                    ms.Dispose();
                }
            }
            return headerImage;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font headerFont = new Font("Arial", 14, FontStyle.Bold);
            Font subheaderFont = new Font("Arial", 10, FontStyle.Bold);
            Font normalBoldFont = new Font("Arial", 8, FontStyle.Bold);
            Font normalFont = new Font("Arial", 8);
            Font subdetailFont = new Font("Arial", 6);
            StringFormat rigthAlign = new StringFormat
            {
                Alignment = StringAlignment.Far,
            };

            float x = 50;
            float y = 50;
            float footerTextX = e.PageBounds.Width - 50;
            float footerTextY = e.PageBounds.Height - 50;
            int CurrentRecord = 0;
            int pageRowContentCount = 30; //25

            //e.PageSettings.PaperSize = new PaperSize("Bond Paper", 612, 792);
            float pageHeight = e.PageSettings.PrintableArea.Height;

            // Header
            int desiredWidth = 50;
            int actualHeight = (int)(desiredWidth);
            Rectangle destRect = new Rectangle(50, 50, desiredWidth, actualHeight);
            if (headerImage != null)
            {
                g.DrawImage(headerImage, destRect);
            }
            float textX = destRect.Right + 20;
            float textY = destRect.Top;
            g.DrawString(OwnerCompany.CompName, headerFont, Brushes.Black, textX, textY);
            y += 30;
            g.DrawString(OwnerCompany.Address, normalFont, Brushes.Black, textX, textY + 20);
            g.DrawString("Tel No.: " + OwnerCompany.TelNo, normalFont, Brushes.Black, textX, textY + 35);
            string companyTin = (OwnerCompany.TinNo + "              ").Substring(0, 14).Replace(" ", "0");
            string formattedTin = string.Format("{0:000-000-000-00000}", long.Parse(companyTin));
            g.DrawString("VAT REG. TIN: " + string.Format("{0:000-000-000-00000}", formattedTin), normalFont, Brushes.Black, textX, textY + 50);
            //g.DrawString("VAT REG. TIN: " + OwnerCompany.TinNo, normalFont, Brushes.Black, textX, textY + 50);

            // Header - Right
            float additionalTextX = e.PageBounds.Width - 50;
            float additionalTextY = destRect.Top;
            g.DrawString("PARTS PRICE LIST", subheaderFont, Brushes.Black, additionalTextX, additionalTextY, rigthAlign);

            // Body
            float boxWidth = e.PageBounds.Width - 2 * x;
            float boxY = Math.Max(destRect.Bottom, textY) + 20;
            RectangleF boxRectLarge = new RectangleF(x, boxY, boxWidth, 640);
            g.DrawRectangle(Pens.Black, boxRectLarge.X, boxY, boxRectLarge.Width, boxRectLarge.Height);

            y += 50;
            g.DrawString("Part No.", normalBoldFont, Brushes.Black, x + 10, y);
            g.DrawString("Part Name", normalBoldFont, Brushes.Black, x + 200, y);
            g.DrawString("Description", normalBoldFont, Brushes.Black, x + 400, y);
            g.DrawString("SKU", normalBoldFont, Brushes.Black, x + 700, y);
            g.DrawString("UOM", normalBoldFont, Brushes.Black, x + 850, y);
            //g.DrawString("Brand", normalBoldFont, Brushes.Black, x + 850, y);
            g.DrawString("List Price", normalBoldFont, Brushes.Black, x + 950, y);
            float partY = y + 20;
            decimal totalPages = 0;
            totalPages = Math.Ceiling(Convert.ToDecimal(DataGridPart.Rows.Count) / pageRowContentCount);
            for (int i = holder; i < DataGridPart.Rows.Count; i++)
            {
                g.DrawString(DataGridPart.Rows[i].Cells["PartNo"].Value.ToString(), normalFont, Brushes.Black, x + 5, partY);
                g.DrawString(DataGridPart.Rows[i].Cells["PartName"].Value.ToString(), normalFont, Brushes.Black, x + 195, partY);
                g.DrawString(DataGridPart.Rows[i].Cells["DescName"].Value.ToString(), normalFont, Brushes.Black, x + 395, partY);
                g.DrawString(DataGridPart.Rows[i].Cells["Sku"].Value.ToString(), normalFont, Brushes.Black, x + 695, partY);
                g.DrawString(DataGridPart.Rows[i].Cells["UomName"].Value.ToString(), normalFont, Brushes.Black, x + 845, partY);
                //g.DrawString(DataGridPart.Rows[i].Cells["BrandName"].Value.ToString(), normalFont, Brushes.Black, x + 845, partY);
                g.DrawString(Convert.ToDecimal(DataGridPart.Rows[i].Cells["ListPrice"].Value).ToString("N2"), normalFont, Brushes.Black, x + 1045, partY, rigthAlign);
                partY += 20;

                CurrentRecord++;
                if (CurrentRecord >= pageRowContentCount)
                {
                    holder += CurrentRecord;
                    g.DrawString("Page:   " + (holder / pageRowContentCount).ToString() + "  of    " + totalPages.ToString(), subdetailFont, Brushes.Black, footerTextX, footerTextY, rigthAlign);
                    if (holder != DataGridPart.Rows.Count)
                    {
                        e.HasMorePages = true;
                        return;
                    }
                }
                else
                {
                    e.HasMorePages = false;
                }
            }
            g.DrawString("Page:   " + ((DataGridPart.Rows.Count / pageRowContentCount) + 1).ToString() + "  of    " + totalPages.ToString(), subdetailFont, Brushes.Black, footerTextX, footerTextY, rigthAlign);
            //foreach (SalesOrderReportParts parts in CustomerOrder.PartsList.ToList())
            //{
            //    g.DrawString(parts.ItemNo, subdetailFont, Brushes.Black, x + 15, partY);
            //    g.DrawString(parts.PartNo, subdetailFont, Brushes.Black, x + 55, partY);
            //    if (parts.PartName.Length > 55)
            //    {
            //        g.DrawString(parts.PartName.Substring(0, 55), subdetailFont, Brushes.Black, x + 130, partY);
            //        g.DrawString(parts.PartName.Substring(55), subdetailFont, Brushes.Black, x + 130, partY + 10);
            //    }
            //    else
            //    {
            //        g.DrawString(parts.PartName, subdetailFont, Brushes.Black, x + 130, partY);
            //    }
            //    g.DrawString(parts.Qty.ToString("N0"), subdetailFont, Brushes.Black, x + 455, partY);
            //    g.DrawString(parts.UomName, subdetailFont, Brushes.Black, x + 495, partY);
            //    g.DrawString(parts.NetPrice.ToString("N2"), subdetailFont, Brushes.Black, x + 555, partY);
            //    g.DrawString(parts.TotalAmount.ToString("N2"), subdetailFont, Brushes.Black, x + 635, partY);
            //    Total += parts.TotalAmount;
            //    Vat += parts.VATAmt;
            //    partY += 25;
            //}

            // Footer
            y += 900;
            //g.DrawString("_____________________________________", normalFont, Brushes.Black, x + 490, y + 10);
            //g.DrawString("Signature of Customer", normalFont, Brushes.Black, x + 550, y + 25);

            //y += 50;
            //g.DrawString("THIS INVOICE SHALL BE VALID", normalBoldFont, Brushes.Black, x + 550, y + 20);
        }
    }
}
