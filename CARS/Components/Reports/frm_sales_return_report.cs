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
    public partial class frm_sales_return_report : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private ReportsController _ReportsController = new ReportsController();
        private SalesReturnReportController _SalesReturnReportController = new SalesReturnReportController();
        private SalesReportFilter _SalesReportFilter = new SalesReportFilter();
        private SortedDictionary<string, string> _SalesmanDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _DescriptionDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _BrandDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _TermDictionary = new SortedDictionary<string, string>();
        //print
        //private SalesReportController OwnerCompany = new SalesOrderReportModel();
        private SalesReturnReportModel SalesSummary = new SalesReturnReportModel();
        private int holder = 0;
        private int itemCounter = 1;
        private int pageRowContentCount = 0;
        private decimal goodNetCounter = 0, defectiveNetCounter = 0, priceNetCounter = 0;

        public frm_sales_return_report(Action DashboardCall)
        {
            InitializeComponent();
            DateSRFrom.Value = DateSOFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
            DateSRTo.Value = DateSOTo.Value = DateTime.Now;
            LblHeader.ForeColor = LblDetails.ForeColor = LblSales.ForeColor = PnlDesign.BackColor = PnlDesignDetails.BackColor = 
                PnlDesignSales.BackColor = PnlDesignSales.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
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

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtCustomer.Textt = TxtPartNo.Textt = "";
                ComboGroup.SelectedIndex = -1;
                ComboSalesman.SelectedIndex = ComboDescription.SelectedIndex = ComboBrand.SelectedIndex = ComboTerm.SelectedIndex = 0;
                DateSRFrom.Value = DateSOFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
                DateSRTo.Value = DateSOTo.Value = DateTime.Now;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (ComboGroup.Text != "")
            {
                holder = 0;
                goodNetCounter = defectiveNetCounter = priceNetCounter = 0;
                itemCounter = 1;
                SalesReturnReportFilter _SalesReturnReportFilter = new SalesReturnReportFilter
                {
                    SRDateFrom = DateSRFrom.Value.Date.ToString("yyyy-MM-dd"),
                    SRDateTo = DateSRTo.Value.Date.ToString("yyyy-MM-dd"),
                    SODateFrom = DateSOFrom.Value.Date.ToString("yyyy-MM-dd"),
                    SODateTo = DateSOTo.Value.Date.ToString("yyyy-MM-dd"),
                    CustName = TxtCustomer.Textt.TrimEnd(),
                    Salesman = ComboSalesman.SelectedValue.ToString(),
                    PartNo = TxtPartNo.Textt.TrimEnd(),
                    Desc = ComboDescription.SelectedValue.ToString(),
                    Brand = ComboBrand.SelectedValue.ToString(),
                    Term = ComboTerm.SelectedValue.ToString(),
                    GroupBy = ComboGroup.Text
                };
                SalesSummary = _SalesReturnReportController.GetSalesReturnSummary(_SalesReturnReportFilter);
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
            g.DrawString("Period: " + DateSOFrom.Value.ToString() + " - " + DateSOTo.Value.ToString(), subheaderFont, Brushes.Black, additionalTextX, additionalTextY + 30, rigthAlign);

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
                g.DrawString("SR No", normalBoldFont, Brushes.Black, x + 10, y);
                g.DrawString("SR Date", normalBoldFont, Brushes.Black, x + 85, y);
                g.DrawString("SO No", normalBoldFont, Brushes.Black, x + 150, y);
                g.DrawString("SO Date", normalBoldFont, Brushes.Black, x + 225, y);
                g.DrawString("Customer Name", normalBoldFont, Brushes.Black, x + 295, y);
                g.DrawString("Salesman", normalBoldFont, Brushes.Black, x + 500, y);
                g.DrawString("Terms", normalBoldFont, Brushes.Black, x + 750, y);
                g.DrawString("Good Qty", normalBoldFont, Brushes.Black, x + 850, y);
                g.DrawString("Defective Qty", normalBoldFont, Brushes.Black, x + 920, y);
                g.DrawString("Net Price", normalBoldFont, Brushes.Black, x + 1015, y);

                y += 25;
                float partY = y;

                for (int i = holder; i < SalesSummary.SummaryList.Count(); i++)
                {
                    g.DrawString(SalesSummary.SummaryList.ToList()[i].SRNo, normalFont, Brushes.Black, x + 5, partY);
                    g.DrawString(SalesSummary.SummaryList.ToList()[i].SRDate, normalFont, Brushes.Black, x + 80, partY);
                    g.DrawString(SalesSummary.SummaryList.ToList()[i].SONo, normalFont, Brushes.Black, x + 145, partY);
                    g.DrawString(SalesSummary.SummaryList.ToList()[i].SODate, normalFont, Brushes.Black, x + 220, partY);
                    g.DrawString(SalesSummary.SummaryList.ToList()[i].Customer, normalFont, Brushes.Black, x + 290, partY);
                    g.DrawString(SalesSummary.SummaryList.ToList()[i].Salesman, normalFont, Brushes.Black, x + 495, partY);
                    g.DrawString(SalesSummary.SummaryList.ToList()[i].Term, normalFont, Brushes.Black, x + 745, partY);
                    g.DrawString(SalesSummary.SummaryList.ToList()[i].GoodQty.ToString("N0"), normalFont, Brushes.Black, x + 905, partY, rigthAlign);
                    g.DrawString(SalesSummary.SummaryList.ToList()[i].DefectiveQty.ToString("N0"), normalFont, Brushes.Black, x + 985, partY, rigthAlign);
                    g.DrawString(SalesSummary.SummaryList.ToList()[i].NetPrice.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                    partY += 20;

                    itemCounter++;
                    CurrentRecord++;
                    if (CurrentRecord >= pageRowContentCount)
                    {
                        holder += CurrentRecord;
                        totalPages = Math.Ceiling(Convert.ToDecimal(SalesSummary.SummaryList.Count()) / pageRowContentCount);
                        if (totalPages < 1)
                        {
                            totalPages = 1;
                        }
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
                g.DrawString("TOTAL SALES RETURN:", normalBoldFont, Brushes.Black, x + 745, partY + 15);
                g.DrawString(SalesSummary.SummaryList.Sum(summary => summary.GoodQty).ToString("N0"), normalFont, Brushes.Black, x + 905, partY + 15, rigthAlign);
                g.DrawString(SalesSummary.SummaryList.Sum(summary => summary.DefectiveQty).ToString("N0"), normalFont, Brushes.Black, x + 985, partY + 15, rigthAlign);
                g.DrawString(SalesSummary.SummaryList.Sum(summary => summary.NetPrice).ToString("N2"), normalFont, Brushes.Black, x + 1065, partY + 15, rigthAlign);
            }
            else if (SalesSummary.GroupList.ToList().Count != 0)
            {
                g.DrawString("Item", normalBoldFont, Brushes.Black, x + 10, y);
                g.DrawString("SR No", normalBoldFont, Brushes.Black, x + 40, y);
                g.DrawString("SR Date", normalBoldFont, Brushes.Black, x + 115, y);
                g.DrawString("SO No", normalBoldFont, Brushes.Black, x + 180, y);
                g.DrawString("SO Date", normalBoldFont, Brushes.Black, x + 255, y);
                g.DrawString("Part No", normalBoldFont, Brushes.Black, x + 325, y);
                g.DrawString("Part Description", normalBoldFont, Brushes.Black, x + 530, y);
                g.DrawString("Brand", normalBoldFont, Brushes.Black, x + 780, y);
                g.DrawString("Good Qty", normalBoldFont, Brushes.Black, x + 850, y);
                g.DrawString("Defective Qty", normalBoldFont, Brushes.Black, x + 920, y);
                g.DrawString("NetPrice", normalBoldFont, Brushes.Black, x + 1015, y);

                y += 25;
                float partY = y;

                totalPages = Math.Ceiling(Convert.ToDecimal(SalesSummary.GroupList.Count()) / 5);
                if (totalPages < 0)
                {
                    totalPages = 1;
                }
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
                            g.DrawString(SalesSummary.GroupList.ToList()[i].ItemNo, normalFont, Brushes.Black, x + 5, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SRNo, normalFont, Brushes.Black, x + 35, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SRDate, normalFont, Brushes.Black, x + 110, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SONo, normalFont, Brushes.Black, x + 175, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SODate, normalFont, Brushes.Black, x + 250, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].PartNo, normalFont, Brushes.Black, x + 320, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].PartDescription, normalFont, Brushes.Black, x + 525, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].Brand, normalFont, Brushes.Black, x + 775, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].GoodQty.ToString("N0"), normalFont, Brushes.Black, x + 900, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].DefectiveQty.ToString("N0"), normalFont, Brushes.Black, x + 970, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].NetPrice.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                            g.DrawString("________________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x + 5, partY + 5);
                            partY += 20;
                            g.DrawString("TOTAL CUSTOMER NET PRC", normalBoldFont, Brushes.Black, x + 685, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].GoodQty.ToString("N0"), normalFont, Brushes.Black, x + 900, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].DefectiveQty.ToString("N0"), normalFont, Brushes.Black, x + 970, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].NetPrice.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                            g.DrawString("__________________________________", normalBoldFont, Brushes.Black, x + 855, partY);
                            if ((i + 1) != SalesSummary.GroupList.Count() && (SalesSummary.GroupList.ToList()[i + 1].Salesman == SalesSummary.GroupList.ToList()[i].Salesman &&
                                SalesSummary.GroupList.ToList()[i + 1].Brand == SalesSummary.GroupList.ToList()[i].Brand))
                            {
                                goodNetCounter += SalesSummary.GroupList.ToList()[i].GoodQty;
                                defectiveNetCounter += SalesSummary.GroupList.ToList()[i].DefectiveQty;
                                priceNetCounter += SalesSummary.GroupList.ToList()[i].NetPrice;
                            }
                            else
                            {
                                partY += 20;
                                goodNetCounter += SalesSummary.GroupList.ToList()[i].GoodQty;
                                defectiveNetCounter += SalesSummary.GroupList.ToList()[i].DefectiveQty;
                                priceNetCounter += SalesSummary.GroupList.ToList()[i].NetPrice;
                                g.DrawString("TOTAL SALESMAN NET PRC", normalBoldFont, Brushes.Black, x + 685, partY);
                                g.DrawString(goodNetCounter.ToString("N0"), normalFont, Brushes.Black, x + 900, partY, rigthAlign);
                                g.DrawString(defectiveNetCounter.ToString("N0"), normalFont, Brushes.Black, x + 970, partY, rigthAlign);
                                g.DrawString(priceNetCounter.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                                g.DrawString("__________________________________", normalBoldFont, Brushes.Black, x + 855, partY);
                                goodNetCounter = defectiveNetCounter = priceNetCounter = 0;
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
                            g.DrawString(SalesSummary.GroupList.ToList()[i].ItemNo, normalFont, Brushes.Black, x + 5, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SRNo, normalFont, Brushes.Black, x + 35, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SRDate, normalFont, Brushes.Black, x + 110, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SONo, normalFont, Brushes.Black, x + 175, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SODate, normalFont, Brushes.Black, x + 250, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].PartNo, normalFont, Brushes.Black, x + 320, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].PartDescription, normalFont, Brushes.Black, x + 525, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].Brand, normalFont, Brushes.Black, x + 775, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].GoodQty.ToString("N0"), normalFont, Brushes.Black, x + 900, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].DefectiveQty.ToString("N0"), normalFont, Brushes.Black, x + 970, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].NetPrice.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                            if (((i + 1) != SalesSummary.GroupList.Count() && (SalesSummary.GroupList.ToList()[i + 1].Customer != SalesSummary.GroupList.ToList()[i].Customer ||
                                SalesSummary.GroupList.ToList()[i + 1].Salesman != SalesSummary.GroupList.ToList()[i].Salesman)) || i + 1 == SalesSummary.GroupList.Count())
                            {
                                partY += 20;
                                goodNetCounter += SalesSummary.GroupList.ToList()[i].GoodQty;
                                defectiveNetCounter += SalesSummary.GroupList.ToList()[i].DefectiveQty;
                                priceNetCounter += SalesSummary.GroupList.ToList()[i].NetPrice;
                                g.DrawString("TOTAL SALESMAN NET PRC", normalBoldFont, Brushes.Black, x + 685, partY);
                                g.DrawString(goodNetCounter.ToString("N0"), normalFont, Brushes.Black, x + 900, partY, rigthAlign);
                                g.DrawString(defectiveNetCounter.ToString("N0"), normalFont, Brushes.Black, x + 970, partY, rigthAlign);
                                g.DrawString(priceNetCounter.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                                g.DrawString("__________________________________", normalBoldFont, Brushes.Black, x + 855, partY);
                                goodNetCounter = defectiveNetCounter = priceNetCounter = 0;
                            }
                            else
                            {
                                goodNetCounter += SalesSummary.GroupList.ToList()[i].GoodQty;
                                defectiveNetCounter += SalesSummary.GroupList.ToList()[i].DefectiveQty;
                                priceNetCounter += SalesSummary.GroupList.ToList()[i].NetPrice;
                            }

                            if ((i + 1) == SalesSummary.GroupList.Count() || SalesSummary.GroupList.ToList()[i + 1].Customer != SalesSummary.GroupList.ToList()[i].Customer)
                            {
                                var goodQty = SalesSummary.GroupList.Where(s => s.Customer == SalesSummary.GroupList.ToList()[i].Customer).Sum(s => s.GoodQty);
                                var defQty = SalesSummary.GroupList.Where(s => s.Customer == SalesSummary.GroupList.ToList()[i].Customer).Sum(s => s.DefectiveQty);
                                var netPrice = SalesSummary.GroupList.Where(s => s.Customer == SalesSummary.GroupList.ToList()[i].Customer).Sum(s => s.NetPrice);
                                partY += 5;
                                g.DrawString("________________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x + 5, partY + 5);
                                partY += 20;
                                g.DrawString("TOTAL CUSTOMER NET PRC", normalBoldFont, Brushes.Black, x + 685, partY);
                                g.DrawString(goodQty.ToString("N0"), normalFont, Brushes.Black, x + 900, partY, rigthAlign);
                                g.DrawString(defQty.ToString("N0"), normalFont, Brushes.Black, x + 970, partY, rigthAlign);
                                g.DrawString(netPrice.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                                g.DrawString("__________________________________", normalBoldFont, Brushes.Black, x + 855, partY);
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

                            //here
                            partY += 20;
                            g.DrawString(SalesSummary.GroupList.ToList()[i].ItemNo, normalFont, Brushes.Black, x + 5, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SRNo, normalFont, Brushes.Black, x + 35, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SRDate, normalFont, Brushes.Black, x + 110, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SONo, normalFont, Brushes.Black, x + 175, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SODate, normalFont, Brushes.Black, x + 250, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].PartNo, normalFont, Brushes.Black, x + 320, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].PartDescription, normalFont, Brushes.Black, x + 525, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].Brand, normalFont, Brushes.Black, x + 775, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].GoodQty.ToString("N0"), normalFont, Brushes.Black, x + 900, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].DefectiveQty.ToString("N0"), normalFont, Brushes.Black, x + 970, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].NetPrice.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                            if (((i + 1) != SalesSummary.GroupList.Count() && (SalesSummary.GroupList.ToList()[i + 1].Salesman != SalesSummary.GroupList.ToList()[i].Salesman ||
                                SalesSummary.GroupList.ToList()[i + 1].PartNo != SalesSummary.GroupList.ToList()[i].PartNo)) || i + 1 == SalesSummary.GroupList.Count())
                            {
                                partY += 20;
                                goodNetCounter += SalesSummary.GroupList.ToList()[i].GoodQty;
                                defectiveNetCounter += SalesSummary.GroupList.ToList()[i].DefectiveQty;
                                priceNetCounter += SalesSummary.GroupList.ToList()[i].NetPrice;
                                g.DrawString("TOTAL SALESMAN NET PRC:", normalBoldFont, Brushes.Black, x + 685, partY);
                                g.DrawString(goodNetCounter.ToString("N0"), normalFont, Brushes.Black, x + 900, partY, rigthAlign);
                                g.DrawString(defectiveNetCounter.ToString("N0"), normalFont, Brushes.Black, x + 970, partY, rigthAlign);
                                g.DrawString(priceNetCounter.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                                g.DrawString("__________________________________", normalBoldFont, Brushes.Black, x + 855, partY);
                                goodNetCounter = defectiveNetCounter = priceNetCounter = 0;
                            }
                            else
                            {
                                goodNetCounter += SalesSummary.GroupList.ToList()[i].GoodQty;
                                defectiveNetCounter += SalesSummary.GroupList.ToList()[i].DefectiveQty;
                                priceNetCounter += SalesSummary.GroupList.ToList()[i].NetPrice;
                            }

                            if ((i + 1) == SalesSummary.GroupList.Count() || (SalesSummary.GroupList.ToList()[i + 1].PartNo != SalesSummary.GroupList.ToList()[i].PartNo ||
                                (SalesSummary.GroupList.ToList()[i + 1].PartNo == SalesSummary.GroupList.ToList()[i].PartNo &&
                                SalesSummary.GroupList.ToList()[i + 1].Customer != SalesSummary.GroupList.ToList()[i].Customer)))
                            {
                                var goodQty = SalesSummary.GroupList.Where(s => s.PartNo == SalesSummary.GroupList.ToList()[i].PartNo &&
                                                                                s.Customer == SalesSummary.GroupList.ToList()[i].Customer).Sum(s => s.GoodQty);
                                var defQty = SalesSummary.GroupList.Where(s => s.PartNo == SalesSummary.GroupList.ToList()[i].PartNo &&
                                                                                s.Customer == SalesSummary.GroupList.ToList()[i].Customer).Sum(s => s.DefectiveQty);
                                var netPrice = SalesSummary.GroupList.Where(s => s.PartNo == SalesSummary.GroupList.ToList()[i].PartNo &&
                                                                                s.Customer == SalesSummary.GroupList.ToList()[i].Customer).Sum(s => s.NetPrice);
                                //var results = SalesSummary.GroupList.Where(s => s.PartNo == SalesSummary.GroupList.ToList()[i].PartNo).ToList().Count();
                                partY += 5;
                                g.DrawString("________________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x + 5, partY + 5);
                                partY += 20;
                                g.DrawString("TOTAL CUSTOMER NET PRC", normalBoldFont, Brushes.Black, x + 685, partY);
                                g.DrawString(goodQty.ToString("N0"), normalFont, Brushes.Black, x + 900, partY, rigthAlign);
                                g.DrawString(defQty.ToString("N0"), normalFont, Brushes.Black, x + 970, partY, rigthAlign);
                                g.DrawString(netPrice.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                                g.DrawString("__________________________________", normalBoldFont, Brushes.Black, x + 855, partY);
                            }

                            if ((i + 1) == SalesSummary.GroupList.Count() || (SalesSummary.GroupList.ToList()[i + 1].PartNo != SalesSummary.GroupList.ToList()[i].PartNo))
                            {
                                var goodQty = SalesSummary.GroupList.Where(s => s.PartNo == SalesSummary.GroupList.ToList()[i].PartNo).Sum(s => s.GoodQty);
                                var defQty = SalesSummary.GroupList.Where(s => s.PartNo == SalesSummary.GroupList.ToList()[i].PartNo).Sum(s => s.DefectiveQty);
                                var netPrice = SalesSummary.GroupList.Where(s => s.PartNo == SalesSummary.GroupList.ToList()[i].PartNo).Sum(s => s.NetPrice);
                                partY += 20;
                                g.DrawString("TOTAL (" + SalesSummary.GroupList.ToList()[i].PartNo + ") NET PRC", normalBoldFont, Brushes.Black, x + 685, partY);
                                g.DrawString(goodQty.ToString("N0"), normalFont, Brushes.Black, x + 900, partY, rigthAlign);
                                g.DrawString(defQty.ToString("N0"), normalFont, Brushes.Black, x + 970, partY, rigthAlign);
                                g.DrawString(netPrice.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                                g.DrawString("__________________________________", normalBoldFont, Brushes.Black, x + 855, partY);
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
                            if ((i != 0 && (SalesSummary.GroupList.ToList()[i - 1].Salesman != SalesSummary.GroupList.ToList()[i].Salesman ||
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
                            g.DrawString(SalesSummary.GroupList.ToList()[i].ItemNo, normalFont, Brushes.Black, x + 5, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SRNo, normalFont, Brushes.Black, x + 35, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SRDate, normalFont, Brushes.Black, x + 110, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SONo, normalFont, Brushes.Black, x + 175, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].SODate, normalFont, Brushes.Black, x + 250, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].PartNo, normalFont, Brushes.Black, x + 320, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].PartDescription, normalFont, Brushes.Black, x + 525, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].Brand, normalFont, Brushes.Black, x + 775, partY);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].GoodQty.ToString("N0"), normalFont, Brushes.Black, x + 900, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].DefectiveQty.ToString("N0"), normalFont, Brushes.Black, x + 970, partY, rigthAlign);
                            g.DrawString(SalesSummary.GroupList.ToList()[i].NetPrice.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                            if (((i + 1) != SalesSummary.GroupList.Count() && (SalesSummary.GroupList.ToList()[i + 1].Salesman != SalesSummary.GroupList.ToList()[i].Salesman ||
                                SalesSummary.GroupList.ToList()[i + 1].Customer != SalesSummary.GroupList.ToList()[i].Customer)) || i + 1 == SalesSummary.GroupList.Count())
                            {
                                goodNetCounter += SalesSummary.GroupList.ToList()[i].GoodQty;
                                defectiveNetCounter += SalesSummary.GroupList.ToList()[i].DefectiveQty;
                                priceNetCounter += SalesSummary.GroupList.ToList()[i].NetPrice;
                                partY += 5;
                                g.DrawString("________________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x + 5, partY + 5);
                                partY += 20;
                                g.DrawString("TOTAL CUSTOMER NET PRC", normalBoldFont, Brushes.Black, x + 685, partY);
                                g.DrawString(goodNetCounter.ToString("N0"), normalFont, Brushes.Black, x + 900, partY, rigthAlign);
                                g.DrawString(defectiveNetCounter.ToString("N0"), normalFont, Brushes.Black, x + 970, partY, rigthAlign);
                                g.DrawString(priceNetCounter.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                                g.DrawString("__________________________________", normalBoldFont, Brushes.Black, x + 855, partY);
                            }
                            else
                            {
                                goodNetCounter += SalesSummary.GroupList.ToList()[i].GoodQty;
                                defectiveNetCounter += SalesSummary.GroupList.ToList()[i].DefectiveQty;
                                priceNetCounter += SalesSummary.GroupList.ToList()[i].NetPrice;
                            }

                            if ((i + 1) == SalesSummary.GroupList.Count() || SalesSummary.GroupList.ToList()[i + 1].Salesman != SalesSummary.GroupList.ToList()[i].Salesman)
                            {
                                partY += 20;
                                var goodQty = SalesSummary.GroupList.Where(s => s.Salesman == SalesSummary.GroupList.ToList()[i].Salesman).Sum(s => s.GoodQty);
                                var defQty = SalesSummary.GroupList.Where(s => s.Salesman == SalesSummary.GroupList.ToList()[i].Salesman).Sum(s => s.DefectiveQty);
                                var netPrice = SalesSummary.GroupList.Where(s => s.Salesman == SalesSummary.GroupList.ToList()[i].Salesman).Sum(s => s.NetPrice);
                                g.DrawString("TOTAL SALESMAN NET PRC", normalBoldFont, Brushes.Black, x + 685, partY);
                                g.DrawString(goodQty.ToString("N0"), normalFont, Brushes.Black, x + 900, partY, rigthAlign);
                                g.DrawString(defQty.ToString("N0"), normalFont, Brushes.Black, x + 970, partY, rigthAlign);
                                g.DrawString(netPrice.ToString("N2"), normalFont, Brushes.Black, x + 1065, partY, rigthAlign);
                                g.DrawString("__________________________________", normalBoldFont, Brushes.Black, x + 855, partY);
                                goodNetCounter = defectiveNetCounter = priceNetCounter = 0;
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

            if (totalPages < 1)
            {
                totalPages = 1;
            }
            g.DrawString("Page:   " + ((SalesSummary.GroupList.Count() / pageRowContentCount) + 1).ToString() + "  of    " + totalPages.ToString(), subdetailFont, Brushes.Black, footerTextX, footerTextY, rigthAlign);
        }
    }
}
