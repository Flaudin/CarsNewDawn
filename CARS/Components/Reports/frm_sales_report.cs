using CARS.Controller.Reports;
using CARS.Functions;
using CARS.Model.Reports;
using CARS.Model.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Transactions
{
    public partial class frm_sales_report : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private ReportsController _ReportsController = new ReportsController();
        private SalesReportController _SalesReportController = new SalesReportController();
        private SalesReportFilter _SalesReportFilter = new SalesReportFilter();
        private SortedDictionary<string, string> _SalesmanDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _DescriptionDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _BrandDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _TermDictionary = new SortedDictionary<string, string>();
        //print
        //private SalesReportController OwnerCompany = new SalesOrderReportModel();
        private SalesReportModel SalesSummary = new SalesReportModel();
        private int holder = 0;
        private int itemCounter = 1;
        private int pageRowContentCount = 0;
        private decimal salesmanNetCounter = 0;
        private string printCategory;

        public frm_sales_report(Action DashboardCall)
        {
            InitializeComponent();
            DateDrFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
            DateDrTo.Value = DateTime.Now;
            LblHeader.ForeColor = LblDetails.ForeColor = LblSales.ForeColor = PnlDesign.BackColor = PnlDesignDetails.BackColor = 
                PnlDesignSales.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblFilter.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _SalesmanDictionary = _ReportsController.GetDictionary("Salesman");
            _DescriptionDictionary = _ReportsController.GetDictionary("Description");
            _BrandDictionary = _ReportsController.GetDictionary("Brand");
            _TermDictionary = _ReportsController.GetDictionary("Term");
            ComboSalesman.DataSource = new BindingSource(_SalesmanDictionary, null);
            ComboDescription.DataSource = new BindingSource(_DescriptionDictionary, null);
            ComboBrand.DataSource = new BindingSource(_BrandDictionary, null);
            ComboTerm.DataSource = new BindingSource(_TermDictionary, null);
            ComboSalesman.DisplayMember = ComboDescription.DisplayMember = ComboBrand.DisplayMember = ComboTerm.DisplayMember = "Key";
            ComboSalesman.ValueMember = ComboDescription.ValueMember = ComboBrand.ValueMember = ComboTerm.ValueMember = "Value";
            //print
            printDocument1.DefaultPageSettings.Landscape = true;
            printDocument1.PrintPage += printDocument1_PrintPage;
            printPreviewDialog1.Document = printDocument1;
            dashboardCall = DashboardCall;
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (ComboGroup.Text != "")
            {
                holder = 0;
                salesmanNetCounter = 0;
                itemCounter = 1;
                if (RadioSO.Checked)
                {
                    printCategory = "Sale Order";
                }
                else
                {
                    printCategory = "Invoice";
                }
                _SalesReportFilter = new SalesReportFilter
                {
                    Type = printCategory,
                    DateFrom = DateDrFrom.Value.Date.ToString("yyyy-MM-dd"),
                    DateTo = DateDrTo.Value.Date.ToString("yyyy-MM-dd"),
                    CustName = TxtCustomer.Textt.TrimEnd(),
                    Salesman = ComboSalesman.SelectedValue.ToString(),
                    PartNo = TxtPartNo.Textt.TrimEnd(),
                    Desc = ComboDescription.SelectedValue.ToString(),
                    Brand = ComboBrand.SelectedValue.ToString(),
                    Term = ComboTerm.SelectedValue.ToString(),
                    GroupBy = ComboGroup.Text
                };
                SalesSummary = _SalesReportController.GetSalesSummary(_SalesReportFilter);
                if (SalesSummary.SummaryList.Count() != 0 || SalesSummary.GroupList.Count() != 0)
                {
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
                MessageBox.Show("Please select a specific group by before printing.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            int actualHeight = (int)(desiredWidth * 10);
            Rectangle destRect = new Rectangle(50, 50, desiredWidth, actualHeight);
            g.DrawString("SALES SUMMARY", headerFont, Brushes.Black, x, y);

            // Header - Right
            float additionalTextX = e.PageBounds.Width - 50;
            float additionalTextY = destRect.Top;
            g.DrawString("SALES REPORT", subheaderFont, Brushes.Black, additionalTextX, additionalTextY, rigthAlign);
            g.DrawString("========================", subheaderFont, Brushes.Black, additionalTextX, additionalTextY + 15, rigthAlign);
            g.DrawString("Period: " + DateDrFrom.Value.ToString() + " - " + DateDrTo.Value.ToString(), subheaderFont, Brushes.Black, additionalTextX, additionalTextY + 30, rigthAlign);

            // Body
            y += 60;
            float boxWidth = e.PageBounds.Width - 2 * x;
            float largeBoxY = Math.Max(destRect.Bottom, 30) + 20 + (3 * 55);
            RectangleF boxRectLarge = new RectangleF(x, largeBoxY, boxWidth, 760);
            g.DrawRectangle(Pens.Black, boxRectLarge.X, y, boxRectLarge.Width, 20);

            y += 5;
            decimal totalPages = 0;
            if (SalesSummary.SummaryList.ToList().Count != 0)
            {
                pageRowContentCount = 10;
                g.DrawString("Item", normalBoldFont, Brushes.Black, x + 10, y);
                g.DrawString(printCategory, normalBoldFont, Brushes.Black, x + 50, y);
                g.DrawString("Date", normalBoldFont, Brushes.Black, x + 170, y);
                g.DrawString("Customer Name", normalBoldFont, Brushes.Black, x + 240, y);
                g.DrawString("Salesman", normalBoldFont, Brushes.Black, x + 600, y);
                g.DrawString("Terms", normalBoldFont, Brushes.Black, x + 850, y);
                g.DrawString("Amount", normalBoldFont, Brushes.Black, x + 950, y);

                y += 25;
                float partY = y;

                for (int i = holder; i < SalesSummary.SummaryList.Count(); i++)
                {
                    g.DrawString(itemCounter.ToString(), normalFont, Brushes.Black, x + 5, partY);
                    g.DrawString(SalesSummary.SummaryList.ToList()[i].SONo, normalFont, Brushes.Black, x + 45, partY);
                    g.DrawString(SalesSummary.SummaryList.ToList()[i].SODate, normalFont, Brushes.Black, x + 165, partY);
                    g.DrawString(SalesSummary.SummaryList.ToList()[i].Customer, normalFont, Brushes.Black, x + 235, partY);
                    g.DrawString(SalesSummary.SummaryList.ToList()[i].Salesman, normalFont, Brushes.Black, x + 595, partY);
                    g.DrawString(SalesSummary.SummaryList.ToList()[i].Term, normalFont, Brushes.Black, x + 845, partY);
                    g.DrawString(SalesSummary.SummaryList.ToList()[i].TotalAmount.ToString("N2"), normalFont, Brushes.Black, x + 945, partY);
                    partY += 20;

                    itemCounter++;
                    CurrentRecord++;
                    if (CurrentRecord >= pageRowContentCount)
                    {
                        holder += CurrentRecord;
                        totalPages = Math.Ceiling(Convert.ToDecimal(SalesSummary.SummaryList.Count()) / pageRowContentCount);
                        g.DrawString("Page:   " + (holder / pageRowContentCount).ToString() + "  of    " + totalPages.ToString(), subdetailFont, Brushes.Black, footerTextX, footerTextY, rigthAlign);
                        if (holder != SalesSummary.SummaryList.Count())
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
                g.DrawString("_________________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x, partY);
                g.DrawString("TOTAL SALES:", normalBoldFont, Brushes.Black, x + 745, partY + 15);
                g.DrawString(SalesSummary.SummaryList.Sum(summary => summary.TotalAmount).ToString("N2"), normalFont, Brushes.Black, x + 945, partY + 15);
            }
            else if (SalesSummary.GroupList.ToList().Count != 0)
            {
                g.DrawString("Item", normalBoldFont, Brushes.Black, x + 10, y);
                g.DrawString(printCategory, normalBoldFont, Brushes.Black, x + 50, y);
                g.DrawString("Part No", normalBoldFont, Brushes.Black, x + 180, y);
                g.DrawString("Part Description", normalBoldFont, Brushes.Black, x + 430, y);
                g.DrawString("List Price", normalBoldFont, Brushes.Black, x + 630, y);
                g.DrawString("Qty", normalBoldFont, Brushes.Black, x + 730, y);
                g.DrawString("Net Price", normalBoldFont, Brushes.Black, x + 780, y);
                g.DrawString("Discount", normalBoldFont, Brushes.Black, x + 880, y);
                g.DrawString("Total Amount", normalBoldFont, Brushes.Black, x + 980, y);

                y += 25;
                float partY = y;

                totalPages = Math.Ceiling(Convert.ToDecimal(SalesSummary.GroupList.Count()) / 5);
                switch (ComboGroup.Text)
                {
                    case "BRAND":
                        pageRowContentCount = 5;
                        for (int i = holder; i < SalesSummary.GroupList.Count(); i++)
                        {
                            if (i == 0)
                            {
                                itemCounter = 1;
                                g.DrawString(SalesSummary.GroupList.ToList()[i].Brand, normalBoldFont, Brushes.DodgerBlue, x + 5, partY);
                            }
                            else if (i != 0 && SalesSummary.GroupList.ToList()[i - 1].Brand != SalesSummary.GroupList.ToList()[i].Brand)
                            {
                                partY += 20;
                                g.DrawString(SalesSummary.GroupList.ToList()[i].Brand, normalBoldFont, Brushes.DodgerBlue, x + 5, partY);
                            }

                            partY += 20;
                            if ((i != 0 && (SalesSummary.GroupList.ToList()[i - 1].Salesman != SalesSummary.GroupList.ToList()[i].Salesman || 
                                SalesSummary.GroupList.ToList()[i - 1].Brand != SalesSummary.GroupList.ToList()[i].Brand)) || i == 0)
                            {
                                itemCounter = 1;
                                g.DrawString(SalesSummary.GroupList.ToList()[i].Salesman, normalBoldFont, Brushes.Black, x + 5, partY);
                            }
                            else
                            {
                                itemCounter++;
                            }

                            partY += 20;
                            g.DrawString(SalesSummary.GroupList.ToList()[i].Customer, normalBoldFont, Brushes.Black, x + 25, partY);
                            g.DrawString("________________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x + 5, partY);
                            partY += 20;
                            g.DrawString(itemCounter.ToString(), normalFont, Brushes.Black, x + 5, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SONo, normalFont, Brushes.Black, x + 45, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].PartNo, normalFont, Brushes.Black, x + 175, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].PartDescription, normalFont, Brushes.Black, x + 425, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].ListPrice.ToString("N2"), normalFont, Brushes.Black, x + 715, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].Qty.ToString("N0"), normalFont, Brushes.Black, x + 765, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].NetPrice.ToString("N2"), normalFont, Brushes.Black, x + 865, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].Discount.ToString("N2"), normalFont, Brushes.Black, x + 965, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].TotalAmount.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                            g.DrawString("________________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x + 5, partY + 5);
                            partY += 20;
                            g.DrawString("TOTAL CUSTOMER NET PRC", normalBoldFont, Brushes.Black, x + 505, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].Qty.ToString("N0"), normalBoldFont, Brushes.Black, x + 765, partY, rigthAlign);
                            g.DrawString("____________________", normalBoldFont, Brushes.Black, x + 665, partY);
                            if ((i + 1) != SalesSummary.GroupList.Count() && (SalesSummary.GroupList.ToList()[i + 1].Salesman == SalesSummary.GroupList.ToList()[i].Salesman &&
                                SalesSummary.GroupList.ToList()[i + 1].Brand == SalesSummary.GroupList.ToList()[i].Brand))
                            {
                                salesmanNetCounter += SalesSummary.GroupList.ToList()[i].Qty;
                            }
                            else
                            {
                                partY += 20;
                                salesmanNetCounter += SalesSummary.GroupList.ToList()[i].Qty;
                                g.DrawString("TOTAL SALESMAN NET PRC", normalBoldFont, Brushes.Black, x + 505, partY);
                                g.DrawString(salesmanNetCounter.ToString("N0"), normalBoldFont, Brushes.Black, x + 765, partY, rigthAlign);
                                g.DrawString("____________________", normalBoldFont, Brushes.Black, x + 665, partY);
                                salesmanNetCounter = 0;
                            }

                            CurrentRecord++;
                            if (CurrentRecord >= pageRowContentCount)
                            {
                                holder += CurrentRecord;
                                g.DrawString("Page:   " + (holder / pageRowContentCount).ToString() + "  of    " + totalPages.ToString(), subdetailFont, Brushes.Black, footerTextX, footerTextY, rigthAlign);
                                if (holder != SalesSummary.GroupList.Count())
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
                        break;

                    case "CUSTOMER":
                        pageRowContentCount = 8;
                        for (int i = holder; i < SalesSummary.GroupList.Count(); i++)
                        {
                            if (i == 0)
                            {
                                g.DrawString(SalesSummary.GroupList.ToList()[i].Customer, normalBoldFont, Brushes.DodgerBlue, x + 5, partY);
                                g.DrawString("________________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x + 5, partY);
                            }
                            else if (i != 0 && SalesSummary.GroupList.ToList()[i - 1].Customer != SalesSummary.GroupList.ToList()[i].Customer)
                            {
                                itemCounter = 1;
                                partY += 30;
                                g.DrawString(SalesSummary.GroupList.ToList()[i].Customer, normalBoldFont, Brushes.DodgerBlue, x + 5, partY);
                                g.DrawString("________________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x + 5, partY);
                            }

                            partY += 20;
                            if ((i != 0 && (SalesSummary.GroupList.ToList()[i - 1].Salesman != SalesSummary.GroupList.ToList()[i].Salesman ||
                                SalesSummary.GroupList.ToList()[i - 1].Customer != SalesSummary.GroupList.ToList()[i].Customer)) || i == 0)
                            {
                                itemCounter = 1;
                                g.DrawString(SalesSummary.GroupList.ToList()[i].Salesman, normalBoldFont, Brushes.Black, x + 5, partY);
                            }
                            else
                            {
                                itemCounter++;
                            }

                            partY += 20;
                            g.DrawString(itemCounter.ToString(), normalFont, Brushes.Black, x + 5, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SONo, normalFont, Brushes.Black, x + 45, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].PartNo, normalFont, Brushes.Black, x + 175, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].PartDescription, normalFont, Brushes.Black, x + 425, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].ListPrice.ToString("N2"), normalFont, Brushes.Black, x + 715, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].Qty.ToString("N0"), normalFont, Brushes.Black, x + 765, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].NetPrice.ToString("N2"), normalFont, Brushes.Black, x + 865, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].Discount.ToString("N2"), normalFont, Brushes.Black, x + 965, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].TotalAmount.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                            if (((i + 1) != SalesSummary.GroupList.Count() && (SalesSummary.GroupList.ToList()[i + 1].Customer != SalesSummary.GroupList.ToList()[i].Customer ||
                                SalesSummary.GroupList.ToList()[i + 1].Salesman != SalesSummary.GroupList.ToList()[i].Salesman)) || i + 1 == SalesSummary.GroupList.Count())
                            {
                                partY += 20;
                                salesmanNetCounter += SalesSummary.GroupList.ToList()[i].Qty;
                                g.DrawString("TOTAL SALESMAN NET PRC", normalBoldFont, Brushes.Black, x + 505, partY);
                                g.DrawString(salesmanNetCounter.ToString("N0"), normalBoldFont, Brushes.Black, x + 765, partY, rigthAlign);
                                g.DrawString("____________________", normalBoldFont, Brushes.Black, x + 665, partY);
                                salesmanNetCounter = 0;
                            }
                            else
                            {
                                salesmanNetCounter += SalesSummary.GroupList.ToList()[i].Qty;
                            }
                            
                            if ((i + 1) == SalesSummary.GroupList.Count() || SalesSummary.GroupList.ToList()[i + 1].Customer != SalesSummary.GroupList.ToList()[i].Customer)
                            {
                                var results = SalesSummary.GroupList.Where(s => s.Customer == SalesSummary.GroupList.ToList()[i].Customer).ToList().Count();
                                partY += 5;
                                g.DrawString("________________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x + 5, partY + 5);
                                partY += 20;
                                g.DrawString("TOTAL CUSTOMER NET PRC", normalBoldFont, Brushes.Black, x + 505, partY);
                                g.DrawString(results.ToString(), normalBoldFont, Brushes.Black, x + 765, partY, rigthAlign);
                                g.DrawString("____________________", normalBoldFont, Brushes.Black, x + 665, partY);
                            }

                            CurrentRecord++;
                            if (CurrentRecord >= pageRowContentCount)
                            {
                                holder += CurrentRecord;
                                totalPages = Math.Ceiling(Convert.ToDecimal(SalesSummary.GroupList.Count()) / pageRowContentCount);
                                g.DrawString("Page:   " + (holder / pageRowContentCount).ToString() + "  of    " + totalPages.ToString(), subdetailFont, Brushes.Black, footerTextX, footerTextY, rigthAlign);
                                if (holder != SalesSummary.GroupList.Count())
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
                        break;

                    case "PART NUMBER":
                        pageRowContentCount = 5;
                        for (int i = holder; i < SalesSummary.GroupList.Count(); i++)
                        {
                            if (i == 0)
                            {
                                g.DrawString(SalesSummary.GroupList.ToList()[i].PartNo, normalBoldFont, Brushes.DodgerBlue, x + 5, partY);
                            }
                            else if (i != 0 && SalesSummary.GroupList.ToList()[i - 1].PartNo != SalesSummary.GroupList.ToList()[i].PartNo)
                            {
                                itemCounter = 1;
                                partY += 30;
                                g.DrawString(SalesSummary.GroupList.ToList()[i].PartNo, normalBoldFont, Brushes.DodgerBlue, x + 5, partY);
                            }

                            partY += 20;
                            if ((i != 0 && (SalesSummary.GroupList.ToList()[i - 1].PartNo != SalesSummary.GroupList.ToList()[i].PartNo ||
                                SalesSummary.GroupList.ToList()[i - 1].Customer != SalesSummary.GroupList.ToList()[i].Customer)) || i == 0)
                            {
                                g.DrawString(SalesSummary.GroupList.ToList()[i].Customer, normalBoldFont, Brushes.Black, x + 5, partY);
                                g.DrawString("________________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x + 5, partY);
                            }

                            if ((i != 0 && (SalesSummary.GroupList.ToList()[i - 1].PartNo != SalesSummary.GroupList.ToList()[i].PartNo ||
                                SalesSummary.GroupList.ToList()[i - 1].Salesman != SalesSummary.GroupList.ToList()[i].Salesman)) || i == 0)
                            {
                                partY += 20;
                                g.DrawString(SalesSummary.GroupList.ToList()[i].Salesman, normalBoldFont, Brushes.Black, x + 5, partY);
                                itemCounter = 1;
                            }
                            else
                            {
                                itemCounter++;
                            }

                            partY += 20;
                            g.DrawString(itemCounter.ToString(), normalFont, Brushes.Black, x + 5, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SONo, normalFont, Brushes.Black, x + 45, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].PartNo, normalFont, Brushes.Black, x + 175, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].PartDescription, normalFont, Brushes.Black, x + 425, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].ListPrice.ToString("N2"), normalFont, Brushes.Black, x + 715, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].Qty.ToString("N0"), normalFont, Brushes.Black, x + 765, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].NetPrice.ToString("N2"), normalFont, Brushes.Black, x + 865, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].Discount.ToString("N2"), normalFont, Brushes.Black, x + 965, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].TotalAmount.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                            if (((i + 1) != SalesSummary.GroupList.Count() && (SalesSummary.GroupList.ToList()[i + 1].Salesman != SalesSummary.GroupList.ToList()[i].Salesman ||
                                SalesSummary.GroupList.ToList()[i + 1].PartNo != SalesSummary.GroupList.ToList()[i].PartNo)) || i + 1 == SalesSummary.GroupList.Count())
                            {
                                partY += 20;
                                salesmanNetCounter += SalesSummary.GroupList.ToList()[i].Qty;
                                g.DrawString("TOTAL SALESMAN NET PRC", normalBoldFont, Brushes.Black, x + 505, partY);
                                g.DrawString(salesmanNetCounter.ToString("N0"), normalBoldFont, Brushes.Black, x + 765, partY, rigthAlign);
                                g.DrawString("____________________", normalBoldFont, Brushes.Black, x + 665, partY);
                                salesmanNetCounter = 0;
                            }
                            else
                            {
                                salesmanNetCounter += SalesSummary.GroupList.ToList()[i].Qty;
                            }

                            if ((i + 1) == SalesSummary.GroupList.Count() || (SalesSummary.GroupList.ToList()[i + 1].PartNo != SalesSummary.GroupList.ToList()[i].PartNo ||
                                (SalesSummary.GroupList.ToList()[i + 1].PartNo == SalesSummary.GroupList.ToList()[i].PartNo && 
                                SalesSummary.GroupList.ToList()[i + 1].Customer != SalesSummary.GroupList.ToList()[i].Customer)))
                            {
                                var results = SalesSummary.GroupList.Where(s => s.PartNo == SalesSummary.GroupList.ToList()[i].PartNo && 
                                                                                s.Customer == SalesSummary.GroupList.ToList()[i].Customer).Sum(s => s.Qty);
                                //var results = SalesSummary.GroupList.Where(s => s.PartNo == SalesSummary.GroupList.ToList()[i].PartNo).ToList().Count();
                                partY += 5;
                                g.DrawString("________________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x + 5, partY + 5);
                                partY += 20;
                                g.DrawString("TOTAL CUSTOMER NET PRC", normalBoldFont, Brushes.Black, x + 505, partY);
                                g.DrawString(results.ToString("N0"), normalBoldFont, Brushes.Black, x + 765, partY, rigthAlign);
                                g.DrawString("____________________", normalBoldFont, Brushes.Black, x + 665, partY);
                            }

                            if ((i + 1) == SalesSummary.GroupList.Count() || (SalesSummary.GroupList.ToList()[i + 1].PartNo != SalesSummary.GroupList.ToList()[i].PartNo))
                            {
                                var results = SalesSummary.GroupList.Where(s => s.PartNo == SalesSummary.GroupList.ToList()[i].PartNo).Sum(s => s.Qty);
                                partY += 20;
                                g.DrawString("TOTAL (" + SalesSummary.GroupList.ToList()[i].PartNo + ") NET PRC", normalBoldFont, Brushes.Black, x + 505, partY);
                                g.DrawString(results.ToString("N0"), normalBoldFont, Brushes.Black, x + 765, partY, rigthAlign);
                                g.DrawString("____________________", normalBoldFont, Brushes.Black, x + 665, partY);
                            }

                            CurrentRecord++;
                            if (CurrentRecord >= pageRowContentCount)
                            {
                                holder += CurrentRecord;
                                g.DrawString("Page:   " + (holder / pageRowContentCount).ToString() + "  of    " + totalPages.ToString(), subdetailFont, Brushes.Black, footerTextX, footerTextY, rigthAlign);
                                if (holder != SalesSummary.GroupList.Count())
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
                        break;

                    case "SALESMAN":
                        pageRowContentCount = 8;
                        for (int i = holder; i < SalesSummary.GroupList.Count(); i++)
                        {
                            if (i == 0)
                            {
                                g.DrawString(SalesSummary.GroupList.ToList()[i].Salesman, normalBoldFont, Brushes.DodgerBlue, x + 5, partY);
                                g.DrawString("________________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x + 5, partY);
                            }
                            else if (i != 0 && SalesSummary.GroupList.ToList()[i - 1].Salesman != SalesSummary.GroupList.ToList()[i].Salesman)
                            {
                                itemCounter = 1;
                                partY += 30;
                                g.DrawString(SalesSummary.GroupList.ToList()[i].Salesman, normalBoldFont, Brushes.DodgerBlue, x + 5, partY);
                                g.DrawString("________________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x + 5, partY);
                            }

                            partY += 20;
                            if ((i != 0 && (SalesSummary.GroupList.ToList()[i - 1].Customer != SalesSummary.GroupList.ToList()[i].Customer ||
                                SalesSummary.GroupList.ToList()[i - 1].Salesman != SalesSummary.GroupList.ToList()[i].Salesman)) || i == 0)
                            {
                                itemCounter = 1;
                                g.DrawString(SalesSummary.GroupList.ToList()[i].Customer, normalBoldFont, Brushes.Black, x + 5, partY);
                            }
                            else
                            {
                                itemCounter++;
                            }

                            partY += 20;
                            g.DrawString(itemCounter.ToString(), normalFont, Brushes.Black, x + 5, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SONo, normalFont, Brushes.Black, x + 45, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].PartNo, normalFont, Brushes.Black, x + 175, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].PartDescription, normalFont, Brushes.Black, x + 425, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].ListPrice.ToString("N2"), normalFont, Brushes.Black, x + 715, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].Qty.ToString("N0"), normalFont, Brushes.Black, x + 765, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].NetPrice.ToString("N2"), normalFont, Brushes.Black, x + 865, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].Discount.ToString("N2"), normalFont, Brushes.Black, x + 965, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].TotalAmount.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                            if (((i + 1) != SalesSummary.GroupList.Count() && (SalesSummary.GroupList.ToList()[i + 1].Salesman != SalesSummary.GroupList.ToList()[i].Salesman ||
                                SalesSummary.GroupList.ToList()[i + 1].Customer != SalesSummary.GroupList.ToList()[i].Customer)) || i + 1 == SalesSummary.GroupList.Count())
                            {
                                var results = SalesSummary.GroupList.Where(s => s.Salesman == SalesSummary.GroupList.ToList()[i].Salesman &&
                                                                                s.Customer == SalesSummary.GroupList.ToList()[i].Customer).Sum(s => s.Qty);
                                partY += 5;
                                g.DrawString("________________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x + 5, partY + 5);
                                partY += 20;
                                g.DrawString("TOTAL CUSTOMER NET PRC", normalBoldFont, Brushes.Black, x + 505, partY);
                                g.DrawString(results.ToString("N0"), normalBoldFont, Brushes.Black, x + 765, partY, rigthAlign);
                                g.DrawString("____________________", normalBoldFont, Brushes.Black, x + 665, partY);
                            }

                            if ((i + 1) == SalesSummary.GroupList.Count() || SalesSummary.GroupList.ToList()[i + 1].Salesman != SalesSummary.GroupList.ToList()[i].Salesman)
                            {
                                partY += 20;
                                var results = SalesSummary.GroupList.Where(s => s.Salesman == SalesSummary.GroupList.ToList()[i].Salesman).Sum(s => s.Qty);
                                g.DrawString("TOTAL SALESMAN NET PRC", normalBoldFont, Brushes.Black, x + 505, partY);
                                g.DrawString(results.ToString("N0"), normalBoldFont, Brushes.Black, x + 765, partY, rigthAlign);
                                g.DrawString("____________________", normalBoldFont, Brushes.Black, x + 665, partY);
                            }

                            CurrentRecord++;
                            if (CurrentRecord >= pageRowContentCount)
                            {
                                holder += CurrentRecord;
                                totalPages = Math.Ceiling(Convert.ToDecimal(SalesSummary.GroupList.Count()) / pageRowContentCount);
                                g.DrawString("Page:   " + (holder / pageRowContentCount).ToString() + "  of    " + totalPages.ToString(), subdetailFont, Brushes.Black, footerTextX, footerTextY, rigthAlign);
                                if (holder != SalesSummary.GroupList.Count())
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
                        break;
                }
            }
            g.DrawString("Page:   " + ((SalesSummary.GroupList.Count() / pageRowContentCount) + 1).ToString() + "  of    " + totalPages.ToString(), subdetailFont, Brushes.Black, footerTextX, footerTextY, rigthAlign);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                RadioSO.Select();
                TxtCustomer.Textt = TxtPartNo.Textt = "";
                ComboGroup.SelectedIndex = -1;
                ComboSalesman.SelectedIndex = ComboDescription.SelectedIndex = ComboBrand.SelectedIndex = ComboTerm.SelectedIndex = 0;
                DateDrFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
                DateDrTo.Value = DateTime.Now;
            }
        }
    }
}
