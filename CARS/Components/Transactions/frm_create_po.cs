using CARS.Controller.Transactions;
using CARS.Functions;
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
    public partial class frm_create_po : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        POMonitoring poMonitoringController = new POMonitoring();
        DataTable MonitoringTable = new DataTable();
        DataTable DetailsTable = new DataTable();
        DataTable PrintableTable = new DataTable();
        PritingPO pritingPO = new PritingPO();  
        POMonitoringModel monitoringModel = new POMonitoringModel();
        CreatePo createPo = new CreatePo();
        private PrintDocument printDocument;
        private PrintPreviewDialog previewDialog;
        private string headerText = "";
        private Image headerImage;
        private string subhead = "";
        private string tinNo = "";
        private string msgResult = "";
        private string poNo = "";
        private int holder = 0;
        private int itemCounter = 1;
        private int pageRowContentCount = 0;
        public frm_create_po()
        {
            InitializeComponent();
            BtnClose.BackColor = PnlHeader.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlFilter.BackColor = PnlOrderTable.BackColor = PnlDetailsTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            BtnClose.ForeColor = LblHeader.ForeColor = LblFilter.ForeColor = LblHeader.ForeColor = LblOrderTable.ForeColor = LblDetailsTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            printDocument1.DefaultPageSettings.Landscape = true;
            printDocument1.PrintPage += printDocument1_PrintPage;
            printPreviewDialog1.Document = printDocument1;
            headerImage = getImage(); //Properties.Resources.gs_logo_v;
            headerText = poMonitoringController.getCompanyName().ToString().TrimEnd();
            subhead = poMonitoringController.getSubheader().ToString().TrimEnd();
            tinNo = poMonitoringController.getTin().ToString().TrimEnd();
            printingTableStyle();
            dgvCreatePo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCreatePo.MultiSelect = false;
            dgvMonitoringBreakdown.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMonitoringBreakdown.MultiSelect = false;
        }

        private void printingTableStyle()
        {
            dgvPrintable.DataSource = PrintGenerated();
            if (dgvPrintable.Columns.Count < 7)
            {
                throw new InvalidOperationException("The DataGridView does not have the expected number of columns.");
            }

            // Set column widths
            dgvPrintable.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvPrintable.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvPrintable.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvPrintable.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvPrintable.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvPrintable.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvPrintable.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Set column alignment and format
            dgvPrintable.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPrintable.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPrintable.Columns[6].DefaultCellStyle.Format = "C4";
            dgvPrintable.Columns[5].DefaultCellStyle.Format = "C4";

            // Set the overall DataGridView width
            dgvPrintable.Width = 200;
            //dgvPrintable.Height = dgvPrintable.Rows.Count;
            dgvPrintable.BorderStyle = BorderStyle.FixedSingle;
            dgvPrintable.ClearSelection();
        }

        private void customRoundedButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            MonitoringTable = poMonitoringController.createPoDt(monitoringModel);
            dgvCreatePo.DataSource = MonitoringTable;
        }

        private void dgvCreatePo_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCreatePo.CurrentRow != null)
            {
                DetailsTable = dgvMonitoringBreakdown.DataSource as DataTable;
                if (DetailsTable != null)
                {
                    DetailsTable.Rows.Clear();
                }
                DataGridViewRow currentOrderRow = dgvCreatePo.Rows[e.RowIndex];
                string controlNo = currentOrderRow.Cells["CtrlNo"].Value.ToString();
                //dgvCreatePo.CurrentRow.Cells["Chkbx"].Value = true;

                foreach (DataGridViewRow row in dgvMonitoringBreakdown.Rows)
                {
                    string existingControlNo = row.Cells["CtrlNo"].Value.ToString();
                    if (existingControlNo == controlNo)
                    {
                        MessageBox.Show("The selected order is already been selected");
                        return;
                    }
                }
                MonitoringTable = poMonitoringController.getMonitoringDets(controlNo);
                if (MonitoringTable.Rows.Count == 0)
                {
                    Helper.Confirmator("No results found.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dgvMonitoringBreakdown.DataSource = MonitoringTable;
                    dgvMonitoringBreakdown.ClearSelection();
                }

            }
            dgvCreatePo.ClearSelection();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to generate PO?", "System Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Convert.ToDecimal(dgvMonitoringBreakdown.CurrentRow.Cells["UnitPrice"].Value) != 0 || Convert.ToDecimal(dgvMonitoringBreakdown.CurrentRow.Cells["OrdrQty"].Value) != 0)
                {
                    try
                    {
                        if (dgvCreatePo.Rows.Count != 0)
                        {
                            List<string> dets = poMonitoringController.getSlid(dgvCreatePo.CurrentRow.Cells["Supplier"].Value.ToString());
                            List<decimal> priceval = new List<decimal>();
                            if (dets != null)
                            {
                                List<PoDet> poDetails = new List<PoDet>();
                                foreach (DataGridViewRow row in dgvMonitoringBreakdown.Rows)
                                {
                                    if (Convert.ToDecimal(row.Cells["OrdrQty"].Value) != 0)
                                    priceval = poMonitoringController.getPricesVal(dets[0], row.Cells["PartNo"].Value.ToString());
                                    PoDet poDet = new PoDet
                                    {
                                        PartNo = row.Cells["PartNo"].Value.ToString(),
                                        Qty = Convert.ToDecimal(row.Cells["OrdrQty"].Value),
                                        UnitPrice = Convert.ToDecimal(row.Cells["UnitPrice"].Value),
                                        Discount = priceval[0],
                                        NetPrice = priceval[1],
                                        PoPrice = Convert.ToDecimal(row.Cells["UnitPrice"].Value),
                                        Quantity = Convert.ToInt32(row.Cells["OrdrQty"].Value ??0),
                                        Status = 1,
                                    };
                                    poDetails.Add(poDet);
                                    createPo = new CreatePo
                                    {
                                        SupplierID = dets[0],
                                        TermID = dets[1],
                                        Status = 1,
                                        poDetails = poDetails
                                    };
                                }
                                if (dgvCreatePo.CurrentRow.Cells["Supplier"].Value.ToString().Trim() == "BSB SOBIDA")
                                {
                                    ResponseModel sendToBsBResult = await poMonitoringController.sendToBSB(createPo);
                                    if (sendToBsBResult.Success)
                                    {
                                        SortedDictionary<string, string> keyValuePairs = poMonitoringController.savePO(createPo);
                                        KeyValuePair<string, string> msg = keyValuePairs.First();
                                        KeyValuePair<string, string> pono = keyValuePairs.Last();
                                        msgResult = msg.Key;
                                        poNo = pono.Value;
                                        Helper.Confirmator(msgResult, "PO Generation Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        if (msgResult == "PO Generated Success")
                                        {
                                            MonitoringTable.Rows.Clear();
                                            createPo = poMonitoringController.poPrinting(poNo);
                                            PrintableTable.Rows.Clear();
                                            dgvPrintable.DataSource = pritingPO;
                                            if (dgvPrintable.Rows.Count != 0)
                                            {
                                                (printPreviewDialog1 as Form).WindowState = FormWindowState.Maximized;
                                                printPreviewDialog1.ShowDialog();
                                            }
                                        }
                                    } else
                                    {
                                        Helper.Confirmator("Failed to create PO. "+ sendToBsBResult.Message, "PO Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                }
                                else
                                {
                                    SortedDictionary<string, string> keyValuePairs = poMonitoringController.savePO(createPo);
                                    KeyValuePair<string, string> msg = keyValuePairs.First();
                                    KeyValuePair<string, string> pono = keyValuePairs.Last();
                                    msgResult = msg.Key;
                                    poNo = pono.Value;
                                    Helper.Confirmator(msgResult, "PO Generation Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    if (msgResult == "PO Generated Success")
                                    {
                                        MonitoringTable.Rows.Clear();
                                        createPo = poMonitoringController.poPrinting(poNo);
                                        PrintableTable.Rows.Clear();
                                        if (createPo.printingPO.Count() != 0)
                                        {
                                            (printPreviewDialog1 as Form).WindowState = FormWindowState.Maximized;
                                            printPreviewDialog1.ShowDialog();
                                        }
                                    }
                                }
                                dgvCreatePo.Refresh();
                                dgvMonitoringBreakdown.Refresh();
                            }
                        }
                        else
                        {
                            Helper.Confirmator("No item found", "PO Generation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Helper.Confirmator(ex.Message, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    Helper.Confirmator("Please check your P.O.", "P.O. Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Helper.Confirmator("Please select an PO to generate", "PO Generation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private DataTable PrintGenerated()
        {
            DataTable printableTable = new DataTable();
            printableTable.Columns.Add("SKU", typeof(string));
            printableTable.Columns.Add("PartNo", typeof(string));
            printableTable.Columns.Add("Description", typeof(string));
            printableTable.Columns.Add("Brand", typeof(string));
            printableTable.Columns.Add("UOM", typeof(string));
            printableTable.Columns.Add("PO Qty", typeof(string));
            printableTable.Columns.Add("BOH", typeof(string));
            return printableTable;
        }

        private Image getImage()
        {
            string compImage = poMonitoringController.getCompanyImage();
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

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int yPos = 20;
            int leftMargin = e.MarginBounds.Left;
            int topMargin = e.MarginBounds.Top;
            int rightMargin = e.MarginBounds.Right;
            int headerHeight = 0;
            float x = 50;
            float y = 50;
            float footerTextX = e.PageBounds.Width - 50;
            float footerTextY = e.PageBounds.Height - 50;
            int CurrentRecord = 0;

            Font headerFont = new Font("Segoe UI", 14, FontStyle.Bold);
            Font subheaderFont = new Font("Segoe UI", 10, FontStyle.Regular);
            Font normalBoldFont = new Font("Arial", 8, FontStyle.Bold);
            Font normalFont = new Font("Arial", 8);
            Font subdetailFont = new Font("Arial", 6);
            Font controlNoFont = new Font("Segoe UI", 10, FontStyle.Bold);
            StringFormat rigthAlign = new StringFormat
            {
                Alignment = StringAlignment.Far,
                //LineAlignment = StringAlignment.Center
            };

            float pageHeight = e.PageSettings.PrintableArea.Height;

            // Header
            int desiredWidth = 50;
            int actualHeight = (int)(desiredWidth * 10);
            Rectangle destRect = new Rectangle(50, 50, desiredWidth, actualHeight);
            g.DrawString(headerText.ToString(), headerFont, Brushes.Black, x, y);
            g.DrawString(subhead.ToString(), subheaderFont, Brushes.Black, x, y+20);
            g.DrawString(tinNo.ToString(), subheaderFont, Brushes.Black, x, y + 35);

            // Header - Right
            float additionalTextX = e.PageBounds.Width - 50;
            float additionalTextY = destRect.Top;
            g.DrawString("Purchase Order", subheaderFont, Brushes.Black, additionalTextX, additionalTextY, rigthAlign);
            g.DrawString("========================", subheaderFont, Brushes.Black, additionalTextX, additionalTextY + 15, rigthAlign);
            g.DrawString("Purchase Order No  ", subheaderFont, Brushes.Black, additionalTextX - 90, additionalTextY + 30, rigthAlign);
            g.DrawString(poNo, controlNoFont, Brushes.Red, additionalTextX, additionalTextY + 30, rigthAlign);

            y += 90;
            float boxWidth = e.PageBounds.Width - 2 * x;
            float largeBoxY = Math.Max(destRect.Bottom, 30) + 20 + (3 * 55);
            RectangleF boxRectLarge = new RectangleF(x, largeBoxY, boxWidth, 760);
            g.DrawRectangle(Pens.Black, boxRectLarge.X, y, boxRectLarge.Width, 20);

            y += 5;
            decimal totalPages = 0;
            if (createPo.printingPO.Count() != 0)
            {
                pageRowContentCount = 10;
                g.DrawString("NO", normalBoldFont, Brushes.Black, x + 10, y);
                g.DrawString("QTY.", normalBoldFont, Brushes.Black, x + 60, y);
                g.DrawString("UNIT", normalBoldFont, Brushes.Black, x + 120, y);
                g.DrawString("DESCRIPTION", normalBoldFont, Brushes.Black, x + 200, y);
                g.DrawString("BRAND", normalBoldFont, Brushes.Black, x + 640, y);
                g.DrawString("UNIT PRICE", normalBoldFont, Brushes.Black, x + 850, y);
                g.DrawString("AMOUNT", normalBoldFont, Brushes.Black, x + 950, y);

                y += 25;
                float partY = y;

                for (int i = holder; i < createPo.printingPO.Count(); i++)
                {
                    g.DrawString(itemCounter.ToString(), normalFont, Brushes.Black, x + 10, partY);
                    g.DrawString(createPo.printingPO.ToList()[i].Qty.ToString("N0"), normalFont, Brushes.Black, x + 60, partY);
                    g.DrawString(createPo.printingPO.ToList()[i].Unit, normalFont, Brushes.Black, x + 120, partY);
                    g.DrawString(createPo.printingPO.ToList()[i].Description, normalFont, Brushes.Black, x + 200, partY);
                    g.DrawString(createPo.printingPO.ToList()[i].Brand, normalFont, Brushes.Black, x + 640, partY);
                    g.DrawString(createPo.printingPO.ToList()[i].UnitPrice.ToString("N2"), normalFont, Brushes.Black, x + 850, partY);
                    g.DrawString(createPo.printingPO.ToList()[i].Amount.ToString("N2"), normalFont, Brushes.Black, x + 950, partY);
                    partY += 20;

                    itemCounter++;
                    CurrentRecord++;
                    if (CurrentRecord >= pageRowContentCount)
                    {
                        holder += CurrentRecord;
                        totalPages = Math.Ceiling(Convert.ToDecimal(createPo.printingPO.Count()) / pageRowContentCount);
                        g.DrawString("Page:   " + (holder / pageRowContentCount).ToString() + "  of    " + totalPages.ToString(), subdetailFont, Brushes.Black, footerTextX, footerTextY, rigthAlign);
                        if (holder != createPo.printingPO.Count())
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
            

            }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            (printPreviewDialog1 as Form).WindowState = FormWindowState.Maximized;
            printPreviewDialog1.ShowDialog();
        }

        private void frm_create_po_Load(object sender, EventArgs e)
        {

        }
    }
    }

