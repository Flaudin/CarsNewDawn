using CARS.Components.Reports.Receive_Report;
using CARS.Controller.Masterfiles;
using CARS.Controller.Reports;
using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model;
using CARS.Model.Reports;
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

namespace CARS.Components.Transactions
{
    public partial class frm_receiving_report : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private SortedDictionary<string, string> _supplier = new SortedDictionary<string, string>();
        private ReceiveReportController _receiveReportController;
        private ReceiveReportModel ReceiveSummary = new ReceiveReportModel();
        private ReceivePrintModel ownerCompany = new ReceivePrintModel();
        private int holder = 0;
        private int itemCounter = 1;
        private int pageRowContentCount = 0;
        private string printCategory;
        private Image headerImage;
        private ReceiveReportController receivingReport = new ReceiveReportController();
        public frm_receiving_report()
        {
            InitializeComponent();
            LblHeader.ForeColor =  PnlDesign.BackColor =  Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlFilter.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblFilter.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _receiveReportController = new ReceiveReportController();
            rrChecker();
            printDocument1.DefaultPageSettings.Landscape = true;
            printDocument1.PrintPage += printDocument1_PrintPage;
            printPreviewDialog1.Document = printDocument1;
            receivingReport = new ReceiveReportController();
            ownerCompany = receivingReport.GetOwnerCompany();
            getImage();
            //_supplier = _receiveReportController.getSupplier();
            //cmbSupplier.DataSource = new BindingSource(_supplier, null);
            //cmbSupplier.DisplayMember = "Key";
            //cmbSupplier.ValueMember = "Value";
            //dashboardCall = dashboardCall;

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
            g.DrawString("RR SUMMARY", headerFont, Brushes.Black, x, y);

            // Header - Right
            float additionalTextX = e.PageBounds.Width - 50;
            float additionalTextY = destRect.Top;
            g.DrawString("RECEIVE REPORT", subheaderFont, Brushes.Black, additionalTextX, additionalTextY, rigthAlign);
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
            if (ReceiveSummary.SummaryList.ToList().Count != 0)
            {
                pageRowContentCount = 1;
                //g.DrawString("NO", normalBoldFont, Brushes.Black, x + 10, y);
                g.DrawString("RR NO.", normalBoldFont, Brushes.Black, x + 45, y);
                g.DrawString("RR DATE", normalBoldFont, Brushes.Black, x + 120, y);
                g.DrawString("SUPPLIER NAME", normalBoldFont, Brushes.Black, x + 200, y);
                g.DrawString("REF NO", normalBoldFont, Brushes.Black, x + 450, y);
                g.DrawString("REF DATE", normalBoldFont, Brushes.Black, x + 640, y);
                g.DrawString("TERMS", normalBoldFont, Brushes.Black, x + 750, y);
                g.DrawString("STATUS", normalBoldFont, Brushes.Black, x + 850, y);
                g.DrawString("AMOUNT", normalBoldFont, Brushes.Black, x + 950, y);

                y += 25;
                float partY = y;

                for (int i = holder; i < ReceiveSummary.SummaryList.Count(); i++)
                {
                    g.DrawString(itemCounter.ToString(), normalFont, Brushes.Black, x + 10, partY);
                    g.DrawString(ReceiveSummary.SummaryList.ToList()[i].RRNo, normalFont, Brushes.Black, x + 45, partY);
                    g.DrawString(ReceiveSummary.SummaryList.ToList()[i].RRDate, normalFont, Brushes.Black, x + 120, partY);
                    g.DrawString(ReceiveSummary.SummaryList.ToList()[i].SupplierName, normalFont, Brushes.Black, x + 200, partY);
                    g.DrawString(ReceiveSummary.SummaryList.ToList()[i].RefNo, normalFont, Brushes.Black, x + 450, partY);
                    g.DrawString(ReceiveSummary.SummaryList.ToList()[i].RefDate, normalFont, Brushes.Black, x + 640, partY);
                    g.DrawString(ReceiveSummary.SummaryList.ToList()[i].Terms, normalFont, Brushes.Black, x + 750, partY);
                    g.DrawString(ReceiveSummary.SummaryList.ToList()[i].Status, normalFont, Brushes.Black, x + 850, partY);
                    g.DrawString(ReceiveSummary.SummaryList.ToList()[i].Amount.ToString("N2"), normalFont, Brushes.Black, x + 950, partY);
                    partY += 20;

                    itemCounter++;
                    CurrentRecord++;
                    if (CurrentRecord >= pageRowContentCount)
                    {
                        holder += CurrentRecord;
                        totalPages = Math.Ceiling(Convert.ToDecimal(ReceiveSummary.SummaryList.Count()) / pageRowContentCount);
                        g.DrawString("Page:   " + (holder / pageRowContentCount).ToString() + "  of    " + totalPages.ToString(), subdetailFont, Brushes.Black, footerTextX, footerTextY, rigthAlign);
                        if (holder != ReceiveSummary.SummaryList.Count())
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
            }
            else if (ReceiveSummary.ReceivingDetailsList.ToList().Count != 0)
            {
                g.DrawString("RR Date", normalBoldFont, Brushes.Black, x + 20, y);
                g.DrawString("RR NO.", normalBoldFont, Brushes.Black, x + 85, y);
                g.DrawString("SUPPLIER", normalBoldFont, Brushes.Black, x + 160, y);
                g.DrawString("SKU", normalBoldFont, Brushes.Black, x + 380, y);
                g.DrawString("PART NO.", normalBoldFont, Brushes.Black, x + 450, y);
                g.DrawString("PART DESCRIPTION", normalBoldFont, Brushes.Black, x + 530, y);
                g.DrawString("BRAND", normalBoldFont, Brushes.Black, x + 680, y);
                g.DrawString("QTY", normalBoldFont, Brushes.Black, x + 750, y);
                g.DrawString("UNIT COST", normalBoldFont, Brushes.Black, x + 850, y);
                g.DrawString("TOTAL AMOUNT", normalBoldFont, Brushes.Black, x + 950, y);

                y += 25;
                float partY = y;

                totalPages = Math.Ceiling(Convert.ToDecimal(ReceiveSummary.ReceivingDetailsList.Count()) / 5);
                switch (cmbGroupBy.Text)
                {
                    case "RR DETAILS LIST":
                        pageRowContentCount = 5;
                        for (int i = holder; i < ReceiveSummary.ReceivingDetailsList.Count(); i++)
                        {
                            //if (i == 0)
                            //{
                             //   itemCounter = 1;
                            //    g.DrawString(ReceiveSummary.ReceivingDetailsList.ToList()[i].Brand, normalBoldFont, Brushes.DodgerBlue, x + 5, partY);
                            //}
                            //else if (i != 0 && ReceiveSummary.ReceivingDetailsList.ToList()[i - 1].Brand != ReceiveSummary.ReceivingDetailsList.ToList()[i].Brand)
                            //{
                            //    partY += 20;
                            //    g.DrawString(ReceiveSummary.ReceivingDetailsList.ToList()[i].Brand, normalBoldFont, Brushes.DodgerBlue, x + 5, partY);
                            //}

                            //partY += 20;
                            //if ((i != 0 && (ReceiveSummary.ReceivingDetailsList.ToList()[i - 1].Salesman != ReceiveSummary.ReceivingDetailsList.ToList()[i].Salesman ||
                            //    ReceiveSummary.ReceivingDetailsList.ToList()[i - 1].Brand != ReceiveSummary.ReceivingDetailsList.ToList()[i].Brand)) || i == 0)
                            //{
                            //    itemCounter = 1;
                            //    g.DrawString(ReceiveSummary.ReceivingDetailsList.ToList()[i].Salesman, normalBoldFont, Brushes.Black, x + 5, partY);
                            //}
                            //else
                            //{
                                itemCounter++;
                            //}

                            //partY += 20;
                            //g.DrawString(ReceiveSummary.ReceivingDetailsList.ToList()[i].Customer, normalBoldFont, Brushes.Black, x + 25, partY);
                            //g.DrawString("_____________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x + 5, partY);
                            partY += 20;
                            g.DrawString(itemCounter.ToString(), normalFont, Brushes.Black, x + 5, partY);
                            g.DrawString(ReceiveSummary.ReceivingDetailsList.ToList()[i].RRDate, normalFont, Brushes.Black, x + 20, partY);
                            g.DrawString(ReceiveSummary.ReceivingDetailsList.ToList()[i].RRNo, normalFont, Brushes.Black, x + 85, partY);
                            g.DrawString(ReceiveSummary.ReceivingDetailsList.ToList()[i].Supplier, normalFont, Brushes.Black, x + 160, partY);
                            g.DrawString(ReceiveSummary.ReceivingDetailsList.ToList()[i].SKU, normalFont, Brushes.Black, x + 380, partY);
                            g.DrawString(ReceiveSummary.ReceivingDetailsList.ToList()[i].PartNo, normalFont, Brushes.Black, x + 450, partY);
                            g.DrawString(ReceiveSummary.ReceivingDetailsList.ToList()[i].PartDesc, normalFont, Brushes.Black, x + 530, partY);
                            g.DrawString(ReceiveSummary.ReceivingDetailsList.ToList()[i].Brand, normalFont, Brushes.Black, x + 680, partY);
                            g.DrawString(ReceiveSummary.ReceivingDetailsList.ToList()[i].Qty.ToString("N0"), normalFont, Brushes.Black, x + 750, partY);
                            g.DrawString(ReceiveSummary.ReceivingDetailsList.ToList()[i].UnitCost.ToString("N2"), normalFont, Brushes.Black, x + 850, partY);
                            g.DrawString(ReceiveSummary.ReceivingDetailsList.ToList()[i].TotalAmt.ToString("N2"), normalFont, Brushes.Black, x + 950, partY);
                            g.DrawString("_____________________________________________________________________________________________________________________________________________________________________", normalFont, Brushes.Black, x + 5, partY + 5);
                            partY += 20;
                            g.DrawString("Total Amount for RR", normalBoldFont, Brushes.Black, x + 675, partY);
                            g.DrawString(ReceiveSummary.ReceivingDetailsList.ToList()[i].RRNo, normalFont, Brushes.Black, x + 810, partY);
                            g.DrawString(ReceiveSummary.ReceivingDetailsList.ToList()[i].TotalAmt.ToString("N2"), normalFont, Brushes.Black, x + 950, partY);
                            g.DrawString("____________________", normalBoldFont, Brushes.Black, x + 675, partY);
                            //if ((i + 1) != ReceiveSummary.ReceivingDetailsList.Count() && (ReceiveSummary.ReceivingDetailsList.ToList()[i + 1].Salesman == ReceiveSummary.ReceivingDetailsList.ToList()[i].Salesman &&
                            //    ReceiveSummary.ReceivingDetailsList.ToList()[i + 1].Brand == ReceiveSummary.ReceivingDetailsList.ToList()[i].Brand))
                            //{
                            //    salesmanNetCounter += ReceiveSummary.ReceivingDetailsList.ToList()[i].Qty;
                            //}
                            //else
                            //{
                            //    partY += 20;
                            //    salesmanNetCounter += ReceiveSummary.ReceivingDetailsList.ToList()[i].Qty;
                            //    g.DrawString("TOTAL SALESMAN NET PRC", normalBoldFont, Brushes.Black, x + 395, partY);
                            //    g.DrawString(salesmanNetCounter.ToString("N0"), normalBoldFont, Brushes.Black, x + 645, partY);
                            //    g.DrawString("____________________", normalBoldFont, Brushes.Black, x + 565, partY);
                            //    salesmanNetCounter = 0;
                            //}

                            CurrentRecord++;
                            if (CurrentRecord >= pageRowContentCount)
                            {
                                holder += CurrentRecord;
                                g.DrawString("Page:   " + (holder / pageRowContentCount).ToString() + "  of    " + totalPages.ToString(), subdetailFont, Brushes.Black, footerTextX, footerTextY, rigthAlign);
                                if (holder != ReceiveSummary.ReceivingDetailsList.Count())
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
            g.DrawString("Page:   " + ((ReceiveSummary.ReceivingDetailsList.Count() / pageRowContentCount) + 1).ToString() + "  of    " + totalPages.ToString(), subdetailFont, Brushes.Black, footerTextX, footerTextY, rigthAlign);
            g.DrawString("Printed By:   " + ReceiveSummary.user, subdetailFont, Brushes.Black, footerTextX - 960, footerTextY, rigthAlign);
            g.DrawString("Rundatetime:   " + DateTime.Now.ToString() , subdetailFont, Brushes.Black, footerTextX, footerTextY +20, rigthAlign);
            g.DrawString("Filter Option By:   ", subdetailFont, Brushes.Black, footerTextX - 960, footerTextY + 20, rigthAlign);
        }

        private void rifinedCustomTextbox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
            {
                MessageBox.Show("Ayay");
            }
        }

        private void rifinedCustomTextbox1_MouseDown(object sender, MouseEventArgs e)
        {
            frm_receiving_report_rr_selection receive_report_rr_selection = new frm_receiving_report_rr_selection();
            receive_report_rr_selection.SelectedRR += selectedRRStart;
            receive_report_rr_selection.Show(this);
        }

        private void rifinedCustomTextbox2_MouseDown(object sender, MouseEventArgs e)
        {
            frm_receiving_report_rr_selection receive_report_rr_selection = new frm_receiving_report_rr_selection();
            receive_report_rr_selection.SelectedRR += selectedRREnd;
            receive_report_rr_selection.Show(this);
        }

        private void selectedRRStart(string rrno)
        {
            txtRRStart.Textt = rrno;
            rrChecker();
        }

        private void selectedRREnd(string rrno)
        {
            txtRRStart.Textt = rrno;
        }
        private void rrChecker()
        {
            if (!string.IsNullOrWhiteSpace(txtRRStart.Textt) || !string.IsNullOrWhiteSpace(txtRREnd.Textt))
            {
                // Enable the ComboBox
                cmbSupplier.Enabled = true;

                // Retrieve supplier data
                _supplier = _receiveReportController.getSupplier(txtRRStart.Textt);

                // Set the data source for the ComboBox
                cmbSupplier.DataSource = new BindingSource(_supplier, null);
                cmbSupplier.DisplayMember = "Key";
                cmbSupplier.ValueMember = "Value";
                
            }
            else
            {
                // Disable the ComboBox if both textboxes are empty
                cmbSupplier.Enabled = false;
            }
        }
        private void rifinedCustomTextbox3_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void txtPartNo_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void txtPartDesc_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void txtBrand_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if(cmbGroupBy.Text != "")
            {
                if(DateDrFrom.Value.Date > DateDrTo.Value.Date)
                {
                    MessageBox.Show("Please input a proper date range before filtering.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    holder = 0;
                    itemCounter = 1;
                    if (rdReceive.Checked)
                    {
                        printCategory = "Receiving";
                    }
                    ReceiveSummary = _receiveReportController.getReceiveSummary(cmbGroupBy.Text,DateDrFrom.Text,DateDrTo.Text);
                    if(ReceiveSummary.SummaryList.Count() == 0 && ReceiveSummary.ReceivingDetailsList.Count() == 0)
                    {
                        Helper.Confirmator("No records found", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized;
                        printPreviewDialog1.ShowDialog();
                    }
                }
            }
            else
            {
                Helper.Confirmator("Unable to Process, Please select Group by value first", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private Image getImage()
        {
            string compImage = ownerCompany.CompLogo;
            if (compImage != "")
            {
                byte[] CompanyImage = Convert.FromBase64String(compImage);
                using (MemoryStream ms = new MemoryStream(CompanyImage))
                {
                    Image newImage = Image.FromStream(ms);
                    headerImage = newImage;
                }
            }

            return headerImage;
        }
    }
}
