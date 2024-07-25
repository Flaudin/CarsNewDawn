using CARS.Controller.Inquiry;
using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model;
using CARS.Model.Inquiry;
using CARS.Model.Transactions;
using CARS.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace CARS.Components.Masterfiles
{
    public partial class frm_sales_order_archive : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private TransactionController _TransactionController = new TransactionController();
        private SalesOrderArchiveController _SalesOrderArchiveController = new SalesOrderArchiveController();
        private SalesOrderArchiveModel _SalesOrderArchiveModel = new SalesOrderArchiveModel();
        private DataTable SalesOrderTable = new DataTable();
        private DataTable DetailsTable = new DataTable();
        private DataTable LocationTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        //print
        private Image headerImage;
        private SalesOrderReportModel OwnerCompany = new SalesOrderReportModel();

        public frm_sales_order_archive(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = PnlHeaderSales.BackColor = PnlHeaderDetails.BackColor = PnlHeaderLoc.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblFilter.ForeColor = LblSales.ForeColor = LblNew.ForeColor = LblPosted.ForeColor = LblCancelled.ForeColor = LblDetails.ForeColor = 
                LblBellowCost.ForeColor = LblLoc.ForeColor = RadioSO.ForeColor = RadioInvoice.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            TxtColumnSearch.Visible = false;
            DateFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
            DateTo.Value = DateTime.Now;
            dashboardCall = DashboardCall;

            //print
            printDocument1.DefaultPageSettings.Landscape = false;
            printDocument1.PrintPage += printDocument1_PrintPage;
            printPreviewDialog1.Document = printDocument1;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (DateFrom.Value.Date > DateTo.Value.Date)
            {
                MessageBox.Show("Please input a proper date range before filtering.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DetailsTable.Rows.Clear();
                LocationTable.Rows.Clear();
                SalesOrderTable = _SalesOrderArchiveController.SearchSalesArchive(TxtCustomer.Textt.TrimEnd(), TxtInvoiceNo.Textt.TrimEnd(), TxtSalesOrderNo.Textt.TrimEnd(),
                                                                                  TxtReferenceNo.Textt, DateFrom.Value.Date.ToString("yyyy-MM-dd"), 
                                                                                  DateTo.Value.Date.ToString("yyyy-MM-dd"));
                //SalesOrderTable = _SalesOrderArchiveController.SearchSalesArchive();
                DataGridSalesOrder.DataSource = SalesOrderTable;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                ClearData();
            }
        }

        private void ClearData()
        {
            TxtCustomer.Textt = TxtInvoiceNo.Textt = TxtSalesOrderNo.Textt = TxtReferenceNo.Textt = "";
            DateFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
            DateTo.Value = DateTime.Now;
            SalesOrderTable.Rows.Clear();
            DetailsTable.Rows.Clear();
            LocationTable.Rows.Clear();
            TxtCustomer.Focus();
        }

        private void DataGridSalesOrder_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DetailsTable = _SalesOrderArchiveController.SalesDetailsArchive(DataGridSalesOrder.Rows[e.RowIndex].Cells["SONo"].Value.ToString());
            DataGridSalesDetail.DataSource = DetailsTable;
            DataGridSalesDetail.ClearSelection();
            //LocationTable.Rows.Clear();
        }

        private void DataGridSalesDetail_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string ItemID = "";
            if (DataGridSalesDetail.Rows[e.RowIndex] != null)
            {
                ItemID = DataGridSalesDetail.Rows[e.RowIndex].Cells["ItemID"].Value.ToString();
            }
            LocationTable = _SalesOrderArchiveController.SalesLocationArchive(DataGridSalesDetail.Rows[e.RowIndex].Cells["ItemID"].Value.ToString());
            DataGridSalesLocation.DataSource = LocationTable;
            DataGridSalesLocation.ClearSelection();
        }

        private void DataGridSalesOrder_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            switch (Convert.ToInt32(DataGridSalesOrder.Rows[e.RowIndex].Cells["MainStatus"].Value))
            {
                case 2:
                    DataGridSalesOrder.Rows[e.RowIndex].Cells["Legend"].Style.BackColor = Color.LightSkyBlue;
                    DataGridSalesOrder.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
                    break;

                case 3:
                    DataGridSalesOrder.Rows[e.RowIndex].Cells["Legend"].Style.BackColor = Color.LightGreen;
                    DataGridSalesOrder.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.LightGreen;
                    break;

                case 9:
                    DataGridSalesOrder.Rows[e.RowIndex].Cells["Legend"].Style.BackColor = Color.LightCoral;
                    DataGridSalesOrder.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.LightCoral;
                    break;
            }
            DataGridSalesOrder.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (DataGridSalesOrder.CurrentRow != null)
            {
                switch (Convert.ToInt32(DataGridSalesOrder.CurrentRow.Cells["MainStatus"].Value))
                {
                    case 2:
                        if (Helper.Confirmator("This will cancel the selected sales order. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            List<SalesOrderArchivParts> DetailsList = new List<SalesOrderArchivParts>();
                            foreach (DataGridViewRow row in DataGridSalesDetail.Rows)
                            {
                                SalesOrderArchivParts Detail = new SalesOrderArchivParts
                                {
                                    PartNo = row.Cells["PartNo"].Value.ToString(),
                                    ItemID = row.Cells["ItemID"].Value.ToString(),
                                    Qty = Convert.ToDecimal(row.Cells["Qty"].Value),
                                };
                                DetailsList.Add(Detail);
                            }
                            _SalesOrderArchiveModel = new SalesOrderArchiveModel { SONo = DataGridSalesOrder.CurrentRow.Cells["SONo"].Value.ToString().TrimEnd(), DetailsList = DetailsList };
                            string CustomMsg = _SalesOrderArchiveController.Update(_SalesOrderArchiveModel);
                            MessageBox.Show(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (CustomMsg == "Sales order cancelled successfully.")
                            {
                                ClearData();
                            }
                        }
                        break;

                    default:
                        MessageBox.Show("Cannot cancel the selected sale order.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
            else
            {
                MessageBox.Show("There is no row selected for cancellation.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TxtCustomer_KeyDown(object sender, KeyEventArgs e)
        {

        }

        int CurrentCol = 1;
        DataGridView CurrentDgv = new DataGridView();
        DataTable CurrentTable = new DataTable();
        private void DataGridSalesOrder_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetColumnSearch(DataGridSalesOrder);
            int LegendIndex = DataGridSalesOrder.Columns["Legend"].Index;
            int PriceIndex = DataGridSalesOrder.Columns["TotalPrice"].Index;
            int SODateIndex = DataGridSalesOrder.Columns["SODate"].Index;
            int InvoiceDateIndex = DataGridSalesOrder.Columns["InvoiceDate"].Index;
            if (!TxtColumnSearch.Visible && SalesOrderTable.Rows.Count > 0 && e.ColumnIndex != PriceIndex && e.ColumnIndex != SODateIndex && 
                e.ColumnIndex != InvoiceDateIndex && e.ColumnIndex != LegendIndex)
            {
                CurrentDgv = DataGridSalesOrder;
                CurrentTable = SalesOrderTable;
                if (DataGridSalesOrder.Rows.Count == 0)
                {
                    SalesOrderTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridSalesOrder.Columns[e.ColumnIndex].HeaderText;
                TxtColumnSearch.Visible = true;
                TxtColumnSearch.Focus();
            }
        }

        private void DataGridSalesDetail_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetColumnSearch(DataGridSalesDetail);
            int ItemIndex = DataGridSalesDetail.Columns["ItemNo"].Index;
            int QtyIndex = DataGridSalesDetail.Columns["Qty"].Index;
            int PriceIndex = DataGridSalesDetail.Columns["ListPrice"].Index;
            int DiscountIndex = DataGridSalesDetail.Columns["Discount"].Index;
            int AmountIndex = DataGridSalesDetail.Columns["TotalAmount"].Index;
            int FreeIndex = DataGridSalesDetail.Columns["FreeItem"].Index;
            int BelowIndex = DataGridSalesDetail.Columns["AllowBelCost"].Index;
            if (!TxtColumnSearch.Visible && DetailsTable.Rows.Count > 0 && e.ColumnIndex != QtyIndex && e.ColumnIndex != PriceIndex 
                && e.ColumnIndex != DiscountIndex && e.ColumnIndex != AmountIndex && e.ColumnIndex != ItemIndex && e.ColumnIndex != FreeIndex && e.ColumnIndex != BelowIndex)
            {
                CurrentDgv = DataGridSalesDetail;
                CurrentTable = DetailsTable;
                if (DataGridSalesDetail.Rows.Count == 0)
                {
                    DetailsTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridSalesDetail.Columns[e.ColumnIndex].HeaderText;
                TxtColumnSearch.Visible = true;
                TxtColumnSearch.Focus();
            }
        }

        private void DataGridSalesLocation_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetColumnSearch(DataGridSalesLocation);
            int QtyIndex = DataGridSalesLocation.Columns["LocationQty"].Index;
            if (!TxtColumnSearch.Visible && LocationTable.Rows.Count > 0 && e.ColumnIndex != QtyIndex)
            {
                CurrentDgv = DataGridSalesLocation;
                CurrentTable = LocationTable;
                if (DataGridSalesLocation.Rows.Count == 0)
                {
                    LocationTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridSalesLocation.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = CurrentDgv.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = CurrentTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                CurrentDgv.DataSource = bs;
            }
        }

        private void TxtColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtColumnSearch.Visible = false;
            CurrentDgv.Focus();
        }

        private void GetColumnSearch(DataGridView dgv)
        {
            TxtColumnSearch = Helper.ColoumnSearcher(dgv, 16, 300);
            TxtColumnSearch.Location = new Point(dgv.Width / 3, 50);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
        }

        private void frm_sales_order_archive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && !TxtColumnSearch.Visible)
            {
                BtnClose.PerformClick();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }

        private void BtnRePrint_Click(object sender, EventArgs e)
        {
            if (DataGridSalesOrder.CurrentRow != null)
            {
                switch (Convert.ToInt32(DataGridSalesOrder.CurrentRow.Cells["MainStatus"].Value))
                {
                    case 2:
                    case 3:
                        OwnerCompany = _TransactionController.GetOwnerCompany();
                        headerImage = getImage();
                        ((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized;
                        printPreviewDialog1.ShowDialog();
                        break;

                    default:
                        MessageBox.Show("Cannot re-print cancelled sale order.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
            else
            {
                MessageBox.Show("There is no row selected for re-print.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private Image getImage()
        {
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
            DataGridViewRow mainrow = DataGridSalesOrder.CurrentRow;
            Graphics g = e.Graphics;
            Font headerFont = new Font("Arial", 14, FontStyle.Bold);
            Font subheaderFont = new Font("Arial", 10, FontStyle.Bold);
            Font normalBoldFont = new Font("Arial", 8, FontStyle.Bold);
            Font normalFont = new Font("Arial", 8);
            Font subdetailFont = new Font("Arial", 6);
            Font subdetailBoldFont = new Font("Arial", 6, FontStyle.Bold);
            StringFormat rigthAlign = new StringFormat
            {
                Alignment = StringAlignment.Far,
            };

            float x = 50;
            float y = 50;

            //e.PageSettings.PaperSize = new PaperSize("Bond Paper", 612, 792);
            float pageHeight = e.PageSettings.PrintableArea.Height;

            // Header
            int desiredWidth = 50;
            int actualHeight = (int)(desiredWidth);
            //int actualHeight = (int)(desiredWidth * ((float)headerImage.Height / headerImage.Width));
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
            g.DrawString("SALES ORDER", subheaderFont, Brushes.Black, additionalTextX, additionalTextY, rigthAlign);
            g.DrawString("NO : " + mainrow.Cells["SONo"].Value.ToString(), subheaderFont, Brushes.Black, additionalTextX, additionalTextY + 15, rigthAlign);
            if (mainrow.Cells["InvoiceRefNo"].Value.ToString().TrimEnd() != "")
            {
                g.DrawString("REF: " + mainrow.Cells["InvoiceRefNo"].Value.ToString(), subheaderFont, Brushes.Black, additionalTextX, additionalTextY + 30, rigthAlign);
                g.DrawString("RE-PRINT", subheaderFont, Brushes.Black, additionalTextX, additionalTextY + 45, rigthAlign);
            }
            else
            {
                g.DrawString("RE-PRINT", subheaderFont, Brushes.Black, additionalTextX, additionalTextY + 30, rigthAlign);
            }

            // Body
            float boxWidth = e.PageBounds.Width - 2 * x;
            for (int i = 0; i < 3; i++)
            {
                float boxY = Math.Max(destRect.Bottom, textY) + 20 + (i * 55);
                RectangleF boxRect = new RectangleF(x, boxY, boxWidth, 50);
                g.DrawRectangle(Pens.Black, boxRect.X, boxRect.Y, boxRect.Width, boxRect.Height);
            }
            float largeBoxY = Math.Max(destRect.Bottom, textY) + 20 + (3 * 55);
            RectangleF boxRectLarge = new RectangleF(x, largeBoxY, boxWidth, 760);
            g.DrawRectangle(Pens.Black, boxRectLarge.X, largeBoxY, boxRectLarge.Width, boxRectLarge.Height);

            y += 50;
            g.DrawString("CUSTOMER:", normalFont, Brushes.Black, x + 10, y);
            g.DrawString("SALES ORDER DATE:", normalFont, Brushes.Black, x + 550, y);
            if (mainrow.Cells["CustName"].Value.ToString().Length > 60)
            {
                g.DrawString(mainrow.Cells["CustName"].Value.ToString().Substring(0, 60), normalBoldFont, Brushes.Black, x + 15, y + 15);
                g.DrawString(mainrow.Cells["CustName"].Value.ToString().Substring(60), normalBoldFont, Brushes.Black, x + 15, y + 25);
            }
            else
            {
                g.DrawString(mainrow.Cells["CustName"].Value.ToString(), normalBoldFont, Brushes.Black, x + 15, y + 15);
            }

            g.DrawString(mainrow.Cells["SODate"].Value.ToString(), normalBoldFont, Brushes.Black, x + 555, y + 15);

            y += 55;
            g.DrawString("ADDRESS:", normalFont, Brushes.Black, x + 10, y);
            if (mainrow.Cells["CustAdd"].Value.ToString().Length > 80)
            {
                float addressy = y + 15;
                for (int i = 0; i < mainrow.Cells["CustAdd"].Value.ToString().Length; i += 80)
                {
                    if (mainrow.Cells["CustAdd"].Value.ToString().Length - i > 80)
                    {
                        g.DrawString(mainrow.Cells["CustAdd"].Value.ToString().Substring(i, 80), normalBoldFont, Brushes.Black, x + 15, addressy);
                    }
                    else
                    {
                        g.DrawString(mainrow.Cells["CustAdd"].Value.ToString().Substring(i), normalBoldFont, Brushes.Black, x + 15, addressy);
                    }
                    addressy += 10;
                }
            }
            else
            {
                g.DrawString(mainrow.Cells["CustAdd"].Value.ToString(), normalBoldFont, Brushes.Black, x + 15, y + 15);
            }

            y += 55;
            g.DrawString("TIN:", normalFont, Brushes.Black, x + 10, y);
            //g.DrawString(mainrow.Cells["CustTin"].Value.ToString(), normalBoldFont, Brushes.Black, x + 15, y + 15);
            string tin = (mainrow.Cells["CustTin"].Value.ToString() + "              ").Substring(0, 14).Replace(" ", "0");
            string formattedNumber = string.Format("{0:000-000-000-00000}", long.Parse(tin));
            g.DrawString(string.Format("{0:000-000-000-00000}", formattedNumber), normalBoldFont, Brushes.Black, x + 15, y + 15);
            g.DrawString("SALESMAN:", normalFont, Brushes.Black, x + 250, y);
            if (mainrow.Cells["SLName"].Value.ToString().Length > 30)
            {
                g.DrawString(mainrow.Cells["SLName"].Value.ToString().Substring(0, 30), normalBoldFont, Brushes.Black, x + 255, y + 15);
                g.DrawString(mainrow.Cells["SLName"].Value.ToString().Substring(30), normalBoldFont, Brushes.Black, x + 255, y + 25);
            }
            else
            {
                g.DrawString(mainrow.Cells["SLName"].Value.ToString(), normalBoldFont, Brushes.Black, x + 255, y + 15);
            }
            g.DrawString("MODE OF PAYMENT:", normalFont, Brushes.Black, x + 550, y);
            g.DrawString(mainrow.Cells["TermName"].Value.ToString(), normalBoldFont, Brushes.Black, x + 555, y + 15);


            y += 55;
            g.DrawString("No", subdetailFont, Brushes.Black, x + 10, y);
            g.DrawString("Part No.", subdetailFont, Brushes.Black, x + 50, y);
            g.DrawString("Part Name", subdetailFont, Brushes.Black, x + 130, y);
            g.DrawString("Qty", subdetailFont, Brushes.Black, x + 405, y);
            g.DrawString("UOM", subdetailFont, Brushes.Black, x + 445, y);
            g.DrawString("Price", subdetailFont, Brushes.Black, x + 515, y);
            g.DrawString("Discount", subdetailFont, Brushes.Black, x + 575, y);
            g.DrawString("Amount", subdetailFont, Brushes.Black, x + 655, y);
            float partY = y + 20;
            decimal qty = 0, discount = 0, srp = 0, total = 0;
            foreach (DataGridViewRow row in DataGridSalesDetail.Rows)
            {
                g.DrawString(row.Cells["ItemNo"].Value.ToString(), subdetailFont, Brushes.Black, x + 15, partY);
                g.DrawString(row.Cells["PartNo"].Value.ToString(), subdetailFont, Brushes.Black, x + 55, partY);
                if (row.Cells["PartName"].Value.ToString().Length > 50)
                {
                    g.DrawString(row.Cells["PartName"].Value.ToString().Substring(0, 50), subdetailFont, Brushes.Black, x + 130, partY);
                    g.DrawString(row.Cells["PartName"].Value.ToString().Substring(50), subdetailFont, Brushes.Black, x + 130, partY + 10);
                }
                else
                {
                    g.DrawString(row.Cells["PartName"].Value.ToString(), subdetailFont, Brushes.Black, x + 130, partY);
                }
                g.DrawString(Convert.ToDecimal(row.Cells["Qty"].Value).ToString("N0"), subdetailFont, Brushes.Black, x + 440, partY, rigthAlign);
                g.DrawString(row.Cells["UomName"].Value.ToString(), subdetailFont, Brushes.Black, x + 445, partY);
                g.DrawString(Convert.ToDecimal(row.Cells["ListPrice"].Value).ToString("N2"), subdetailFont, Brushes.Black, x + 560, partY, rigthAlign);
                g.DrawString(Convert.ToDecimal(row.Cells["Discount"].Value).ToString("N2"), subdetailFont, Brushes.Black, x + 635, partY, rigthAlign);
                g.DrawString(Convert.ToDecimal(row.Cells["TotalAmount"].Value).ToString("N2"), subdetailFont, Brushes.Black, x + 705, partY, rigthAlign);
                qty += Convert.ToDecimal(row.Cells["Qty"].Value);
                srp += Convert.ToDecimal(row.Cells["ListPrice"].Value);
                discount += Convert.ToDecimal(row.Cells["Discount"].Value);
                total += Convert.ToDecimal(row.Cells["TotalAmount"].Value);
                partY += 25;
            }
            g.DrawString("__________________________________________________________________________________", subdetailFont, Brushes.Black, x + 315, partY);
            partY += 15;
            g.DrawString("TOTAL:", subdetailFont, Brushes.Black, x + 350, partY, rigthAlign);
            g.DrawString(qty.ToString("N0"), subdetailFont, Brushes.Black, x + 440, partY, rigthAlign);
            g.DrawString(srp.ToString("N2"), subdetailFont, Brushes.Black, x + 560, partY, rigthAlign);
            g.DrawString(discount.ToString("N2"), subdetailFont, Brushes.Black, x + 635, partY, rigthAlign);
            g.DrawString(total.ToString("N2"), subdetailFont, Brushes.Black, x + 705, partY, rigthAlign);

            // Footer
            y += 760;
            g.DrawString("Prepared By:" + Universal<SalesOrderArchiveModel>.Name01, normalFont, Brushes.Black, x, y + 10);
            g.DrawString("Date:" + DateTime.Now, normalFont, Brushes.Black, x, y + 25);
        }
    }
}
