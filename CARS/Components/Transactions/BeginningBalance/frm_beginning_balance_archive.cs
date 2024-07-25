using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model.Masterfiles;
using CARS.Model.Transactions;
using CARS.Model.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Transactions.BeginningBalance
{
    public partial class frm_beginning_balance_archive : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private BeginningBalanceController _BeginningBalanceController = new BeginningBalanceController();
        private DataTable BegBalTable = new DataTable();
        private DataTable PartsTable = new DataTable();
        private DataTable LocationTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();
        //print
        private List<BeginningBalancePrint> begBalPrint = new List<BeginningBalancePrint>();
        private int holder = 0;
        private int pageRowContentCount = 0;
        private TransactionController _TransactionController = new TransactionController();
        private Image headerImage;
        private SalesOrderReportModel OwnerCompany = new SalesOrderReportModel();

        public frm_beginning_balance_archive()
        {
            InitializeComponent();
            PnlHeader.BackColor = BtnClose.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = PnlHeaderBegBal.BackColor = PnlHeaderLocation.BackColor = PnlHeaderParts.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblHeader.ForeColor = BtnClose.ForeColor = LblFilter.ForeColor = LblBegBal.ForeColor = LblLocation.ForeColor = LblParts.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            TxtColumnSearch.Visible = false;
            DateFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
            DateTo.Value = DateTime.Now;
            //print
            printDocument1.DefaultPageSettings.Landscape = false;
            printDocument1.PrintPage += printDocument1_PrintPage;
            printPreviewDialog1.Document = printDocument1;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close Beginning Balance Archive?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Close();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (DateFrom.Value.Date > DateTo.Value.Date)
            {
                MessageBox.Show("Please input a proper date range before filtering.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                PartsTable.Rows.Clear();
                LocationTable.Rows.Clear();
                BegBalTable = _BeginningBalanceController.BegBalDataTable(TxtBeginningBalanceNo.Textt.TrimEnd(), DateFrom.Value.Date.ToString("yyyy-MM-dd"), DateTo.Value.Date.ToString("yyyy-MM-dd"));
                DataGridBeginningBalance.DataSource = BegBalTable;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtBeginningBalanceNo.Textt = "";
                DateFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
                DateTo.Value = DateTime.Now;
                BegBalTable.Rows.Clear();
                PartsTable.Rows.Clear();
                LocationTable.Rows.Clear();
                TxtBeginningBalanceNo.Focus();
            }
        }

        private void DataGridBeginningBalance_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            PartsTable = _BeginningBalanceController.PartsArchiveDataTable(DataGridBeginningBalance.Rows[e.RowIndex].Cells["BegBalNo"].Value.ToString());
            DataGridParts.DataSource = PartsTable;
            DataGridParts.ClearSelection();
            //LocationTable.Rows.Clear();
        }

        private void DataGridParts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string BegBal = DataGridBeginningBalance.Rows[e.RowIndex].Cells["BegBalNo"].Value.ToString();
            //if (DataGridBeginningBalance.Rows[e.RowIndex] != null)
            //{
            //    BegBal = ;
            //}
            LocationTable = _BeginningBalanceController.LocationArchiveDataTable(BegBal,
                DataGridParts.Rows[e.RowIndex].Cells["UniqueID"].Value.ToString().TrimEnd());
            DataGridLocation.DataSource = LocationTable;
            DataGridLocation.ClearSelection();
        }

        int CurrentCol = 1;
        DataGridView CurrentDgv = new DataGridView();
        DataTable CurrentTable = new DataTable();
        private void DataGridBeginningBalance_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetColumnSearch(DataGridBeginningBalance);
            if (!TxtColumnSearch.Visible && BegBalTable.Rows.Count > 0)
            {
                CurrentDgv = DataGridBeginningBalance;
                CurrentTable = BegBalTable;
                if (DataGridBeginningBalance.Rows.Count == 0)
                {
                    BegBalTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridBeginningBalance.Columns[e.ColumnIndex].HeaderText;
                TxtColumnSearch.Visible = true;
                TxtColumnSearch.Focus();
            }
        }

        private void DataGridParts_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetColumnSearch(DataGridParts);
            int PriceIndex = DataGridParts.Columns["UnitPrice"].Index;
            int QtyIndex = DataGridParts.Columns["Qty"].Index;
            if (!TxtColumnSearch.Visible && PartsTable.Rows.Count > 0 && e.ColumnIndex != PriceIndex && e.ColumnIndex != QtyIndex)
            {
                CurrentDgv = DataGridParts;
                CurrentTable = PartsTable;
                if (DataGridParts.Rows.Count == 0)
                {
                    PartsTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridParts.Columns[e.ColumnIndex].HeaderText;
                TxtColumnSearch.Visible = true;
                TxtColumnSearch.Focus();
            }
        }

        private void DataGridLocation_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetColumnSearch(DataGridLocation);
            int QtyIndex = DataGridLocation.Columns["QtyLocation"].Index;
            if (!TxtColumnSearch.Visible && LocationTable.Rows.Count > 0 && e.ColumnIndex != QtyIndex)
            {
                CurrentDgv = DataGridLocation;
                CurrentTable = LocationTable;
                if (DataGridLocation.Rows.Count == 0)
                {
                    LocationTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridLocation.Columns[e.ColumnIndex].HeaderText;
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

        private void frm_beginning_balance_archive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && !TxtColumnSearch.Visible)
            {
                BtnClose.PerformClick();
            }
        }

        private void GetColumnSearch(DataGridView dgv)
        {
            TxtColumnSearch = Helper.ColoumnSearcher(dgv, 16, 350);
            TxtColumnSearch.Location = new Point(dgv.Width / 3, 50);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (DataGridBeginningBalance.CurrentRow != null)
            {
                holder = 0;
                begBalPrint = _BeginningBalanceController.PrintBeginningBalance(DataGridBeginningBalance.CurrentRow.Cells["BegBalNo"].Value.ToString());
                if (begBalPrint.Count() != 0)
                {
                    OwnerCompany = _TransactionController.GetOwnerCompany();
                    headerImage = getImage();
                    ((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized;
                    printPreviewDialog1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("There are no records found with the current filter.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a beginning balance before printing.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Graphics g = e.Graphics;
            Font headerFont = new Font("Arial", 14, FontStyle.Bold);
            Font subheaderFont = new Font("Arial", 10, FontStyle.Bold);
            Font normalBoldFont = new Font("Arial", 8, FontStyle.Bold);
            Font normalFont = new Font("Arial", 8);
            Font subdetailFont = new Font("Arial", 6);
            StringFormat rigthAlign = new StringFormat
            {
                Alignment = StringAlignment.Far,
                //LineAlignment = StringAlignment.Center
            };

            float x = 50;
            float y = 50;
            float footerTextX = e.PageBounds.Width - 50;
            float footerTextY = e.PageBounds.Height - 50;
            int CurrentRecord = 0;

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

            // Header - Right
            float additionalTextX = e.PageBounds.Width - 50;
            float additionalTextY = destRect.Top;
            g.DrawString("BEGNNING BALANCE", subheaderFont, Brushes.Black, additionalTextX, additionalTextY, rigthAlign);
            g.DrawString("NO : " + DataGridBeginningBalance.CurrentRow.Cells["BegBalNo"].Value.ToString(), subheaderFont, Brushes.Black, additionalTextX, additionalTextY + 15, rigthAlign);

            // Body
            y += 60;
            float boxWidth = e.PageBounds.Width - 2 * x;
            float largeBoxY = Math.Max(destRect.Bottom, 30) + 20 + (3 * 55);
            RectangleF boxRectLarge = new RectangleF(x, largeBoxY, boxWidth, 760);
            g.DrawRectangle(Pens.Black, boxRectLarge.X, y, boxRectLarge.Width, 20);

            y += 5;
            decimal totalPages = 1;
            if (begBalPrint.ToList().Count != 0)
            {
                pageRowContentCount = 30;
                g.DrawString("Part No", normalBoldFont, Brushes.Black, x + 10, y);
                g.DrawString("Warehouse", normalBoldFont, Brushes.Black, x + 100, y);
                g.DrawString("Location", normalBoldFont, Brushes.Black, x + 300, y);
                g.DrawString("Quantity", normalBoldFont, Brushes.Black, x + 550, y);
                g.DrawString("Unity Price", normalBoldFont, Brushes.Black, x + 650, y);

                y += 25;
                float partY = y;

                for (int i = holder; i < begBalPrint.ToList().Count(); i++)
                {
                    g.DrawString(begBalPrint[i].PartNo.ToString(), normalFont, Brushes.Black, x + 5, partY);
                    g.DrawString(begBalPrint[i].WhName.ToString(), normalFont, Brushes.Black, x + 95, partY);
                    g.DrawString(begBalPrint[i].BinName.ToString(), normalFont, Brushes.Black, x + 295, partY);
                    g.DrawString(begBalPrint[i].Qty.ToString("N0"), normalFont, Brushes.Black, x + 605, partY, rigthAlign);
                    g.DrawString(begBalPrint[i].UnitPrice.ToString("N2"), normalFont, Brushes.Black, x + 715, partY, rigthAlign);
                    partY += 20;

                    CurrentRecord++;
                    if (CurrentRecord >= pageRowContentCount)
                    {
                        holder += CurrentRecord;
                        totalPages = Math.Ceiling(Convert.ToDecimal(begBalPrint.ToList().Count()) / pageRowContentCount);
                        if (totalPages < 1)
                        {
                            totalPages = 1;
                        }
                        g.DrawString("Page:   " + (holder / pageRowContentCount).ToString() + "  of    " + totalPages.ToString(), subdetailFont, Brushes.Black, footerTextX, footerTextY, rigthAlign);
                        if (holder != begBalPrint.ToList().Count())
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
                //g.DrawString("_________________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x, partY);
                //g.DrawString("TOTAL SALES:", normalBoldFont, Brushes.Black, x + 745, partY + 15);
                //g.DrawString(SalesSummary.SummaryList.Sum(summary => summary.TotalAmount).ToString("N2"), normalFont, Brushes.Black, x + 945, partY + 15);
            }
            g.DrawString("Page:   " + ((begBalPrint.ToList().Count() / pageRowContentCount) + 1).ToString() + "  of    " + totalPages.ToString(), subdetailFont, Brushes.Black, footerTextX, footerTextY, rigthAlign);
        }
    }
}
