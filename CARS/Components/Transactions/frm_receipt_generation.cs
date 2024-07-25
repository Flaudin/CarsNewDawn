using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model.Transactions;
using CARS.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CARS.Components.Transactions
{
    public partial class frm_receipt_generation : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private TransactionController _TransactionController = new TransactionController();
        private ReceiptGenerationController _ReceiptGenerationController = new ReceiptGenerationController();
        private ReceiptGenerationModel _ReceiptGenerationModel = new ReceiptGenerationModel();
        private SortedDictionary<string, string> _CreditTermDictionary = new SortedDictionary<string, string>();
        private DataTable SOTable = new DataTable();
        private DataTable PartsTable = new DataTable();
        //print
        private Image headerImage;
        private SalesOrderReportModel OwnerCompany = new SalesOrderReportModel();
        private SalesOrderReportCustomer CustomerOrder = new SalesOrderReportCustomer();

        public frm_receipt_generation(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = PnlHeaderOutstanding.BackColor = PnlHeaderParts.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblFilter.ForeColor = LblOutstanding.ForeColor = LblParts.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _CreditTermDictionary = _TransactionController.GetDictionary("Term");
            ComboTerm.DataSource = new BindingSource(_CreditTermDictionary, null);
            ComboTerm.DisplayMember = "Key";
            ComboTerm.ValueMember = "Value";
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
                MessageBox.Show("The selected date range is incorrect.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _ReceiptGenerationModel = new ReceiptGenerationModel
                {
                    SONo = TxtSoNo.Textt.TrimEnd(),
                    CustomerName = TxtCustomer.Textt.TrimEnd(),
                    CreditTerm = ComboTerm.SelectedValue.ToString(),
                    DateFrom = DateFrom.Value.Date.ToString("yyyy-MM-dd"),
                    DateTo = DateTo.Value.Date.ToString("yyyy-MM-dd"),
                };
                SOTable = _ReceiptGenerationController.dt(_ReceiptGenerationModel);
                foreach (DataRow row in SOTable.Rows)
                {
                    string custTin = row["CustTin"].ToString().Substring(0, 14).Replace(" ", "0");
                    string formattedNumber = string.Format("{0:000-000-000-00000}", long.Parse(custTin));
                    row["CustTin"] = formattedNumber;
                }
                DataGridSalesOrder.DataSource = SOTable;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtSoNo.Textt = TxtCustomer.Textt = "";
                ComboTerm.SelectedIndex = 0;
                DateFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
                DateTo.Value = DateTime.Now;
                SOTable.Rows.Clear();
                PartsTable.Rows.Clear();
                TxtSoNo.Focus();
            }
        }

        private void DataGridSalesOrder_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            PartsTable = _ReceiptGenerationController.PartTable(DataGridSalesOrder.Rows[e.RowIndex].Cells["SONo"].Value.ToString());
            DataGridParts.DataSource = PartsTable;
            TxtTotalQty.Textt = Convert.ToDecimal(PartsTable.Compute("Sum(Qty)", string.Empty)).ToString("N0");
            TxtTotalAmount.Textt = Convert.ToDecimal(PartsTable.Compute("Sum(TotalAmount)", string.Empty)).ToString("N2");
        }

        private void BtnPrintPOS_Click(object sender, EventArgs e)
        {
            frm_receipt_generation_invoice generateInvoice = new frm_receipt_generation_invoice();
            generateInvoice.ShowDialog();
        }

        private void BtnPayment_Click(object sender, EventArgs e)
        {
            if (DataGridSalesOrder.CurrentRow != null)
            {
                if (_ReceiptGenerationController.GenerateInvoiceNo(DataGridSalesOrder.CurrentRow.Cells["SONo"].Value.ToString()))
                {
                    OwnerCompany = _TransactionController.GetOwnerCompany();
                    headerImage = getImage();
                    CustomerOrder = _ReceiptGenerationController.GetCustomerSalesOrder(DataGridSalesOrder.CurrentRow.Cells["SONo"].Value.ToString());
                    ((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized;
                    printPreviewDialog1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Something went wrong with Invoice Generation.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("There are no SO selected for payment.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            e.PageSettings.PaperSize = new PaperSize("Bond Paper", 612, 792);
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
            string companyTin = (OwnerCompany.TinNo + "              ").Substring(0,14).Replace(" ", "0");
            string formattedTin = string.Format("{0:000-000-000-00000}", long.Parse(companyTin));
            g.DrawString("VAT REG. TIN: " + string.Format("{0:000-000-000-00000}", formattedTin), normalFont, Brushes.Black, textX, textY + 50);
            //g.DrawString("VAT REG. TIN: " + OwnerCompany.TinNo, normalFont, Brushes.Black, textX, textY + 50);

            // Header - Right
            float additionalTextX = e.PageBounds.Width - 50;
            float additionalTextY = destRect.Top;
            g.DrawString("INVOICE NO.", subheaderFont, Brushes.Black, additionalTextX, additionalTextY, rigthAlign);
            g.DrawString(CustomerOrder.InvoiceNo, subheaderFont, Brushes.Black, additionalTextX, additionalTextY + 15, rigthAlign);
            g.DrawString(DataGridSalesOrder.CurrentRow.Cells["SONo"].Value.ToString(), subheaderFont, Brushes.Black, additionalTextX, additionalTextY + 30, rigthAlign);

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
            g.DrawString("SOLD TO:", normalFont, Brushes.Black, x + 10, y);
            g.DrawString("INVOICE DATE:", normalFont, Brushes.Black, x + 550, y);
            if (CustomerOrder.CustName.Length > 60)
            {
                g.DrawString(CustomerOrder.CustName.Substring(0, 60), normalBoldFont, Brushes.Black, x + 15, y + 15);
                g.DrawString(CustomerOrder.CustName.Substring(60), normalBoldFont, Brushes.Black, x + 15, y + 25);
            }
            else
            {
                g.DrawString(CustomerOrder.CustName, normalBoldFont, Brushes.Black, x + 15, y + 15);
            }
            g.DrawString(DateTime.Now.Date.ToString("MM/dd/yyyy"), normalBoldFont, Brushes.Black, x + 555, y + 20);

            y += 55;
            g.DrawString("ADDRESS:", normalFont, Brushes.Black, x + 10, y);
            if (CustomerOrder.Address.Length > 80)
            {
                float addressy = y + 15;
                for (int i = 0; i < CustomerOrder.Address.Length; i += 80)
                {
                    if (CustomerOrder.Address.Length - i > 80)
                    {
                        g.DrawString(CustomerOrder.Address.Substring(i, 80), normalBoldFont, Brushes.Black, x + 15, addressy);
                    }
                    else
                    {
                        g.DrawString(CustomerOrder.Address.Substring(i), normalBoldFont, Brushes.Black, x + 15, addressy);
                    }
                    addressy += 10;
                }
            }
            else
            {
                g.DrawString(CustomerOrder.Address, normalBoldFont, Brushes.Black, x + 15, y + 15);
            }

            y += 55;
            g.DrawString("TIN:", normalFont, Brushes.Black, x + 10, y);
            //g.DrawString(CustomerOrder.TinNo, normalBoldFont, Brushes.Black, x + 15, y + 15);
            string tin = (CustomerOrder.TinNo + "              ").Substring(0,14).Replace(" ", "0");
            string formattedNumber = string.Format("{0:000-000-000-00000}", long.Parse(tin));
            g.DrawString(string.Format("{0:000-000-000-00000}", formattedNumber), normalBoldFont, Brushes.Black, x + 15, y + 15);
            g.DrawString("BUSINESS NAME/STYLE:", normalFont, Brushes.Black, x + 250, y);
            if (CustomerOrder.RegName.Length > 30)
            {
                g.DrawString(CustomerOrder.RegName.Substring(0, 30), normalBoldFont, Brushes.Black, x + 255, y + 15);
                g.DrawString(CustomerOrder.RegName.Substring(30), normalBoldFont, Brushes.Black, x + 255, y + 25);
            }
            else
            {
                g.DrawString(CustomerOrder.RegName, normalBoldFont, Brushes.Black, x + 255, y + 15);
            }
            g.DrawString("MODE OF PAYMENT:", normalFont, Brushes.Black, x + 550, y);
            g.DrawString(CustomerOrder.TermName, normalBoldFont, Brushes.Black, x + 555, y + 15);

            y += 55;
            g.DrawString("No", subdetailFont, Brushes.Black, x + 10, y);
            g.DrawString("Part No.", subdetailFont, Brushes.Black, x + 50, y);
            g.DrawString("Part Name", subdetailFont, Brushes.Black, x + 130, y);
            g.DrawString("Qty", subdetailFont, Brushes.Black, x + 455, y);
            g.DrawString("UOM", subdetailFont, Brushes.Black, x + 495, y);
            g.DrawString("Price", subdetailFont, Brushes.Black, x + 555, y);
            g.DrawString("Amount", subdetailFont, Brushes.Black, x + 635, y);
            float partY = y + 20;
            decimal Vat = 0, Total = 0;
            foreach (SalesOrderReportParts parts in CustomerOrder.PartsList.ToList())
            {
                g.DrawString(parts.ItemNo, subdetailFont, Brushes.Black, x + 15, partY);
                g.DrawString(parts.PartNo, subdetailFont, Brushes.Black, x + 55, partY);
                if (parts.PartName.Length > 55)
                {
                    g.DrawString(parts.PartName.Substring(0,55), subdetailFont, Brushes.Black, x + 130, partY);
                    g.DrawString(parts.PartName.Substring(55), subdetailFont, Brushes.Black, x + 130, partY+10);
                }
                else
                {
                    g.DrawString(parts.PartName, subdetailFont, Brushes.Black, x + 130, partY);
                }
                g.DrawString(parts.Qty.ToString("N0"), subdetailFont, Brushes.Black, x + 455, partY);
                g.DrawString(parts.UomName, subdetailFont, Brushes.Black, x + 495, partY);
                g.DrawString(parts.NetPrice.ToString("N2"), subdetailFont, Brushes.Black, x + 555, partY);
                g.DrawString(parts.TotalAmount.ToString("N2"), subdetailFont, Brushes.Black, x + 635, partY);
                Total += parts.TotalAmount;
                Vat += parts.VATAmt;
                partY += 25;
            }
            y += 690;
            g.DrawString("VAT Table Sales", subdetailFont, Brushes.Black, x + 250, y);
            g.DrawString((Total - Vat).ToString("N2"), subdetailFont, Brushes.Black, x + 350, y);
            g.DrawString("VAT-Exempt Sales", subdetailFont, Brushes.Black, x + 250, y + 10);
            g.DrawString("Zero Rated Sales", subdetailFont, Brushes.Black, x + 250, y + 20);
            g.DrawString("Total Sales", subdetailFont, Brushes.Black, x + 250, y + 30);
            g.DrawString((Total - Vat).ToString("N2"), subdetailFont, Brushes.Black, x + 350, y + 30);
            g.DrawString("VAT Amount", subdetailFont, Brushes.Black, x + 250, y + 40);
            g.DrawString(Vat.ToString("N2"), subdetailFont, Brushes.Black, x + 350, y + 40);
            g.DrawString("Total Amount Due", subdetailFont, Brushes.Black, x + 250, y + 50);
            g.DrawString(Total.ToString("N2"), subdetailFont, Brushes.Black, x + 350, y + 50);
            g.DrawString("TOTAL", headerFont, Brushes.Black, x + 450, y + 40);
            g.DrawString(Total.ToString("N2"), headerFont, Brushes.Black, x + 530, y + 40);

            // Footer
            y += 70;
            g.DrawString("The undersigned declares to have received from " + OwnerCompany.CompName + "\n" +
                         "the goods detailed above in good order and condition and promise \n" +
                         "to Pay the full amount of this invoice within the terms and stated hereon, \n" +
                         "with the full understanding that all said goods will still be the property \n" +
                         "of " + OwnerCompany.CompName + " until the amount hereof is paid in full.", normalFont, Brushes.Black, x, y);
            g.DrawString("_____________________________________", normalFont, Brushes.Black, x + 490, y + 10);
            g.DrawString("Signature of Customer", normalFont, Brushes.Black, x + 550, y + 25);

            y += 50;
            g.DrawString("THIS INVOICE SHALL BE VALID", normalBoldFont, Brushes.Black, x + 550, y + 20);
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
