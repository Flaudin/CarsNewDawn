using CARS.Components.Inquiry;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Masterfiles
{
    public partial class frm_inventory_management : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private InventoryManagementController _InventoryManagementController = new InventoryManagementController();
        private InventoryManagementModel _InventoryManagementModel = new InventoryManagementModel();
        private SortedDictionary<string, string> _MeasurementDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _BrandDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _DescriptionDictionary = new SortedDictionary<string, string>();
        private DataTable BegBalTable = new DataTable();
        private DataTable PartTable = new DataTable();
        private Label[] monthLabels;

        public frm_inventory_management(Action DashboardCall)
        {
            InitializeComponent();
            InitializeMonthLabels();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = PnlHeaderHistory.BackColor = PnlHeaderParts.BackColor = PnlHeaderMovement.BackColor = PnlHeaderSetup.BackColor =
                PnlHeaderChild.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblFilter.ForeColor = LblHistory.ForeColor = LblParts.ForeColor = LblMovement.ForeColor = LblSetup.ForeColor = 
                LblChild.ForeColor = LblPicking.ForeColor = LblPicked.ForeColor = LblIssued.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _MeasurementDictionary = _InventoryManagementController.GetDictionary("Uom");
            _BrandDictionary = _InventoryManagementController.GetDictionary("Brand");
            _DescriptionDictionary = _InventoryManagementController.GetDictionary("Description");
            ComboUomFilter.DataSource = new BindingSource(_MeasurementDictionary, null);
            ComboBrandFilter.DataSource = new BindingSource(_BrandDictionary, null);
            ComboDescription.DataSource = new BindingSource(_DescriptionDictionary, null);
            ComboBrandFilter.DisplayMember = ComboUomFilter.DisplayMember = ComboDescription.DisplayMember = "Key";
            ComboBrandFilter.ValueMember = ComboUomFilter.ValueMember = ComboDescription.ValueMember = "Value";
            dashboardCall = DashboardCall;
        }

        private void InitializeMonthLabels()
        {
            monthLabels = new Label[]
            {
                lblMonth12, lblMonth11, lblMonth10, lblMonth9, lblMonth8, lblMonth7, lblMonth6, lblMonth5, lblMonth4, lblMonth3, lblMonth2, lblMonth1,
            };
            UpdateMonthLabels();
        }

        private void UpdateMonthLabels()
        {
            DateTime currentDate = DateTime.Now;
            int currentYear = currentDate.Year % 100;

            for (int i = 0; i < 12; i++)
            {
                DateTime date = currentDate.AddMonths(-i);
                string monthAbbrev = date.ToString("MMM");
                int year = date.Year % 100;

                int labelIndex = 11 - i;

                monthLabels[labelIndex].Text = $"{monthAbbrev}-{year:D2}";
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            PartTable.Rows.Clear();
            BegBalTable.Rows.Clear();
            TxtMonth1.Textt = TxtMonth2.Textt = TxtMonth3.Textt = TxtMonth4.Textt = TxtMonth5.Textt = TxtMonth6.Textt = TxtMonth7.Textt = TxtMonth8.Textt =
                TxtMonth9.Textt = TxtMonth10.Textt = TxtMonth11.Textt = TxtMonth12.Textt = "";
            TxtBegBal.Textt = TxtReceived.Textt = TxtTakeUp.Textt = TxtReturns.Textt = TxtPicked.Textt = TxtStockDrop.Textt =
                TxtPurchaseReturns.Textt = TxtDefective.Textt = TxtBoh.Textt = TxtInventory.Textt = TxtListPrice.Textt = "0.00";
            NumericReorder.Value = NumericReorderNew.Value = NumericSGO.Value = NumericSGONew.Value = NumericLead.Value = NumericLeadNew.Value = 0;
            LblChild.Text = "Balance On Hand";
            List<dynamic[]> NameList = new List<dynamic[]> {
                new string[] { "WhName", "Warehouse", "" },
                new string[] { "BinName", "Location", "" },
                new string[] { "BegBal", "Beginning Balance", "N2" },
                new string[] { "Rcvd", "Received", "N2" },
                new string[] { "TakeUp", "Take Up", "N2" },
                new string[] { "SReturns", "Sales Returns", "N2" },
                new string[] { "Picked", "Sales", "N2" },
                new string[] { "StockDrop", "Stock Drop", "N2" },
                new string[] { "PReturns", "Purchase Returns", "N2" },
                new string[] { "DefReturns", "Defective Returns", "N2" },
                new string[] { "TrfIn", "Transfer In", "N2" },
                new string[] { "TrfOut", "Transfer Out", "N2" },
            };
            ColumnEncoderNew(NameList);
            _InventoryManagementModel = new InventoryManagementModel
            {
                PartNo = TxtPartNoFilter.Textt.TrimEnd(),
                PartName = TxtPartNameFilter.Textt.TrimEnd(),
                OtherName = TxtOtherNameFilter.Textt.TrimEnd(),
                Sku = TxtSkuFiIter.Textt.TrimEnd(),
                Brand = ComboBrandFilter.SelectedValue.ToString().TrimEnd(),
                Uom = ComboUomFilter.SelectedValue.ToString().TrimEnd(),
                Description = ComboDescription.SelectedValue.ToString()
            };
            PartTable = _InventoryManagementController.PartsWithBegBalDataTable(_InventoryManagementModel);
            DataGridPart.DataSource = PartTable;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                Clear();
            }
        }

        private void Clear()
        {
            TxtPartNoFilter.Textt = TxtPartNameFilter.Textt = TxtOtherNameFilter.Textt = TxtSkuFiIter.Textt = "";
            ComboBrandFilter.SelectedIndex = ComboUomFilter.SelectedIndex = 0;
            PartTable.Rows.Clear();
            BegBalTable.Rows.Clear();
            TxtMonth1.Textt = TxtMonth2.Textt = TxtMonth3.Textt = TxtMonth4.Textt = TxtMonth5.Textt = TxtMonth6.Textt = TxtMonth7.Textt = TxtMonth8.Textt =
                TxtMonth9.Textt = TxtMonth10.Textt = TxtMonth11.Textt = TxtMonth12.Textt = "";
            TxtBegBal.Textt = TxtReceived.Textt = TxtTakeUp.Textt = TxtReturns.Textt = TxtPicked.Textt = TxtStockDrop.Textt =
                TxtPurchaseReturns.Textt = TxtDefective.Textt = TxtBoh.Textt = TxtInventory.Textt = TxtListPrice.Textt = "0.00";
            NumericReorder.Value = NumericReorderNew.Value = NumericSGO.Value = NumericSGONew.Value = NumericLead.Value = NumericLeadNew.Value = 0;
            LblChild.Text = "Balance On Hand";
            List<dynamic[]> NameList = new List<dynamic[]> {
                new string[] { "WhName", "Warehouse", "" },
                new string[] { "BinName", "Location", "" },
                new string[] { "BegBal", "Beginning Balance", "N2" },
                new string[] { "Rcvd", "Received", "N2" },
                new string[] { "TakeUp", "Take Up", "N2" },
                new string[] { "SReturns", "Sales Returns", "N2" },
                new string[] { "Picked", "Sales", "N2" },
                new string[] { "StockDrop", "Stock Drop", "N2" },
                new string[] { "PReturns", "Purchase Returns", "N2" },
                new string[] { "DefReturns", "Defective Returns", "N2" },
                new string[] { "TrfIn", "Transfer In", "N2" },
                new string[] { "TrfOut", "Transfer Out", "N2" },
            };
            ColumnEncoderNew(NameList);
        }

        private void DataGridPart_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LblChild.Text = "Balance On Hand";
            List<dynamic[]> NameList = new List<dynamic[]> {
                new string[] { "WhName", "Warehouse", "" },
                new string[] { "BinName", "Location", "" },
                new string[] { "BegBal", "Beginning Balance", "N2" },
                new string[] { "Rcvd", "Received", "N2" },
                new string[] { "TakeUp", "Take Up", "N2" },
                new string[] { "SReturns", "Sales Returns", "N2" },
                new string[] { "Picked", "Sales", "N2" },
                new string[] { "StockDrop", "Stock Drop", "N2" },
                new string[] { "PReturns", "Purchase Returns", "N2" },
                new string[] { "DefReturns", "Defective Returns", "N2" },
                new string[] { "TrfIn", "Transfer In", "N2" },
                new string[] { "TrfOut", "Transfer Out", "N2" },
            };
            ColumnEncoderNew(NameList);
            BegBalTable = _InventoryManagementController.InventoryDataTable(DataGridPart.Rows[e.RowIndex].Cells["PartNo"].Value.ToString());
            DataGridChild.DataSource = BegBalTable;

            List<decimal> movement = _InventoryManagementController.InventoryMovement(DataGridPart.Rows[e.RowIndex].Cells["PartNo"].Value.ToString());
            decimal begbal = movement[0];
            decimal rcvd = movement[1];
            decimal takeup = movement[2];
            decimal sreturns = movement[3];
            decimal picked = movement[4];
            decimal stockdrop = movement[5];
            decimal preturns = movement[6];
            decimal defreturns = movement[7];
            decimal trfin = movement[8];
            decimal trfout = movement[9];
            var Boh = (rcvd + sreturns + begbal + takeup + trfin) - (preturns + picked + defreturns + stockdrop + trfout);

            TxtBegBal.Textt = begbal.ToString("N2");
            TxtReceived.Textt = rcvd.ToString("N2");
            TxtTakeUp.Textt = takeup.ToString("N2");
            TxtReturns.Textt = sreturns.ToString("N2");
            TxtPicked.Textt = picked.ToString("N2");
            TxtStockDrop.Textt = stockdrop.ToString("N2");
            TxtPurchaseReturns.Textt = preturns.ToString("N2");
            TxtBegBal.Textt = begbal.ToString("N2");
            TxtListPrice.Textt = Convert.ToDecimal(DataGridPart.Rows[e.RowIndex].Cells["ListPrice"].Value).ToString("N2");
            TxtDefective.Textt = defreturns.ToString("N2");
            TxtBoh.Textt = Boh.ToString("N2");
            NumericLeadNew.Value = NumericReorderNew.Value = NumericSGONew.Value = 0;
            SetupFill(DataGridPart.Rows[e.RowIndex].Cells["PartNo"].Value.ToString());
            List<decimal> quantities = new List<decimal>();
            quantities = _InventoryManagementController.SalesQty(DataGridPart.Rows[e.RowIndex].Cells["PartNo"].Value.ToString());
            TxtMonth1.Textt = quantities[0].ToString("N0");
            TxtMonth2.Textt = quantities[1].ToString("N0");
            TxtMonth3.Textt = quantities[2].ToString("N0");
            TxtMonth4.Textt = quantities[3].ToString("N0");
            TxtMonth5.Textt = quantities[4].ToString("N0");
            TxtMonth6.Textt = quantities[5].ToString("N0");
            TxtMonth7.Textt = quantities[6].ToString("N0");
            TxtMonth8.Textt = quantities[7].ToString("N0");
            TxtMonth9.Textt = quantities[8].ToString("N0");
            TxtMonth10.Textt = quantities[9].ToString("N0");
            TxtMonth11.Textt = quantities[10].ToString("N0");
            TxtMonth12.Textt = quantities[11].ToString("N0");
        }


        private void BtnBegBal_Click(object sender, EventArgs e)
        {
            LblChild.Text = "Beginning Balance";
            List<dynamic[]> NameList = new List<dynamic[]> {
                new string[] { "BegBalNo", "Beginning Balance No", "" },
                new string[] { "CreatedDt", "Date", "" },
                new string[] { "PartNo", "Part Number", "" },
                new string[] { "BinName", "Location", "" },
                new string[] { "WhName", "Warehouse", "" },
                new string[] { "Qty", "Quantity", "N2" },
                new string[] { "UnitPrice", "Unit Price", "N2" }
            };
            ColumnEncoderNew(NameList);
            if (DataGridPart.CurrentRow != null)
            {
                BegBalTable = _InventoryManagementController.BegBalDataTable(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString());
                DataGridChild.DataSource = BegBalTable;
            }
        }

        private void BtnReceived_Click(object sender, EventArgs e)
        {
            LblChild.Text = "Received";
            List<dynamic[]> NameList = new List<dynamic[]>
            {
                new string[] { "RRNo", "Control No.", "" },
                new string[] { "CreatedDt", "Date", "" },
                new string[] { "UnitPrice", "Unit Price", "N2" },
                new string[] { "Qty", "Received Qty", "N0" },
                new string[] { "VATAmt", "VAT", "N0" },
                new string[] { "TotalPrice", "Total", "N2" },
            };
            ColumnEncoderNew(NameList);
            if (DataGridPart.CurrentRow != null)
            {
                BegBalTable = _InventoryManagementController.ReceivedDataTable(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString());
                DataGridChild.DataSource = BegBalTable;
            }
        }

        private void BtnTakeUp_Click(object sender, EventArgs e)
        {
            LblChild.Text = "Take Up";
            List<dynamic[]> NameList = new List<dynamic[]>
            {
                new string[] { "AdjNo", "Control No.", "" },
                new string[] { "CreatedDt", "Date", "" },
                new string[] { "TakeUpQty", "Qty", "N2" },
                new string[] { "AveCost", "Average Cost", "N2" },
                new string[] { "Total", "Total Cost", "N2" },
                new string[] { "ReasonName", "Reason", "" }
            };
            ColumnEncoderNew(NameList);
            if (DataGridPart.CurrentRow != null)
            {
                BegBalTable = _InventoryManagementController.TakeUpDataTable(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString());
                DataGridChild.DataSource = BegBalTable;
            }
        }

        private void BtnReturns_Click(object sender, EventArgs e)
        {
            //Status = free item(yellow)
            LblChild.Text = "Returns";
            List<dynamic[]> NameList = new List<dynamic[]>
            {
                new string[] { "SrNo", "Stock Return No.", "" },
                new string[] { "InvoiceNo", "Stock Return No.", "" },
                new string[] { "Customer", "Customer", "" },
                new string[] { "Supplier", "Supplier", "" },
                new string[] { "GoodQty", "Qty", "N2" },
                new string[] { "NetPrice", "Net Price", "N2" },
                new string[] { "Total", "Total", "N2" },
            };
            ColumnEncoderNew(NameList);
            if (DataGridPart.CurrentRow != null)
            {
                BegBalTable = _InventoryManagementController.SalesReturnGoodDataTable(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString());
                DataGridChild.DataSource = BegBalTable;
            }
        }

        private void BtnSales_Click(object sender, EventArgs e)
        {
            //Status = free item(yellow), low profit(red), high profit(green)
            LblChild.Text = "Sales";
            PnlSalesLegend.Visible = true;
            List<dynamic[]> NameList = new List<dynamic[]>
            {
                new string[] { "MainStatus", "", "Hide" },
                new string[] { "Legend", "", "Legend" },
                new string[] { "SONo", "SO No.", "" },
                new string[] { "SODate", "SO Date", "" },
                new string[] { "InvoiceNo", "Invoice No.", "" },
                new string[] { "InvoiceDate", "Invoice Date", "" },
                new string[] { "InvoiceRefNo", "Ref. No.", "" },
                new string[] { "CustName", "Customer", "" },
                new string[] { "SLName", "Salesman", "" },
                new string[] { "TermName", "Term", "" },
                new string[] { "Qty", "Quantity", "N2" },
                new string[] { "ListPrice", "List Price", "N2" },
                new string[] { "Discount", "Discount", "N2" },
                new string[] { "TotalPrice", "Price", "N2" },
            };
            ColumnEncoderNew(NameList);
            if (DataGridPart.CurrentRow != null)
            {
                BegBalTable = _InventoryManagementController.SalesDataTable(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString());
                DataGridChild.DataSource = BegBalTable;
            }
        }

        private void BtnDropped_Click(object sender, EventArgs e)
        {
            //Control No, Date, Drop Qty, Damage, Average Cost, Total Cost, Reason
            LblChild.Text = "Stock Drop";
            List<dynamic[]> NameList = new List<dynamic[]>
            {
                new string[] { "AdjNo", "Control No.", "" },
                new string[] { "CreatedDt", "Date", "" },
                new string[] { "DropQty", "Qty", "N2" },
                new string[] { "AveCost", "Average Cost", "N2" },
                new string[] { "Total", "Total Cost", "N2" },
                new string[] { "ReasonName", "Reason", "" }
            };
            ColumnEncoderNew(NameList);
            if (DataGridPart.CurrentRow != null)
            {
                BegBalTable = _InventoryManagementController.DropDataTable(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString());
                DataGridChild.DataSource = BegBalTable;
            }
        }

        private void BtnBoh_Click(object sender, EventArgs e)
        {
            //Warehouse, Location, Available, BegBal, Received, TakeUp, Returns, Sales, Internal Sales, Drop, Transfer In, Transfer Out, Defective, Purchase Returns
            LblChild.Text = "Balance On Hand";
            List<dynamic[]> NameList = new List<dynamic[]> {
                new string[] { "WhName", "Warehouse", "" },
                new string[] { "BinName", "Location", "" },
                new string[] { "BegBal", "Beginning Balance", "N2" },
                new string[] { "Rcvd", "Received", "N2" },
                new string[] { "TakeUp", "Take Up", "N2" },
                new string[] { "SReturns", "Sales Returns", "N2" },
                new string[] { "Picked", "Sales", "N2" },
                new string[] { "StockDrop", "Stock Drop", "N2" },
                new string[] { "PReturns", "Purchase Returns", "N2" },
                new string[] { "DefReturns", "Defective Returns", "N2" },
                new string[] { "TrfIn", "Transfer In", "N2" },
                new string[] { "TrfOut", "Transfer Out", "N2" },
            };
            ColumnEncoderNew(NameList);
            if (DataGridPart.CurrentRow != null)
            {
                BegBalTable = _InventoryManagementController.InventoryDataTable(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString());
                DataGridChild.DataSource = BegBalTable;
            }
        }

        private void BtnListPrice_Click(object sender, EventArgs e)
        {
            //Control No, Date, Modified By, Unit Cost, List Price
            LblChild.Text = "List Price";
            List<dynamic[]> NameList = new List<dynamic[]>
            {
                new string[] { "ControlNo", "Control No.", "" },
                new string[] { "PriceDate", "Price Date", "" },
                new string[] { "CreatedDt", "Created Date", "" },
                new string[] { "ModifiedDt", "Modified Date", "" },
                new string[] { "UnitCost", "Unit Cost", "N2" },
                new string[] { "ListPriceHistory", "List Price", "N2" }
            };
            ColumnEncoderNew(NameList);
            if (DataGridPart.CurrentRow != null)
            {
                BegBalTable = _InventoryManagementController.ListPriceDataTable(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString());
                DataGridChild.DataSource = BegBalTable;
            }
        }

        private void BtnPurchaseReturns_Click(object sender, EventArgs e)
        {
            //Control No, Control Date, RR No, Supplier name, Qty, Unit Cost, Reason
            LblChild.Text = "Purchase Returns";
            List<dynamic[]> NameList = new List<dynamic[]>
            {
                new string[] { "PurchRetNo", "Purchase Return No.", "" },
                new string[] { "CreatedDt", "Date", "" },
                new string[] { "RRNo", "RR No.", "" },
                new string[] { "Qty", "Quantity", "N2" },
                new string[] { "ListPrice", "List Price", "N2" },
                new string[] { "ReasonName", "Reason", "" },
            };
            ColumnEncoderNew(NameList);
            if (DataGridPart.CurrentRow != null)
            {
                BegBalTable = _InventoryManagementController.PurchaseReturnDataTable(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString());
                DataGridChild.DataSource = BegBalTable;
            }
        }

        private void BtnDefective_Click(object sender, EventArgs e)
        {
            //Control No, Date, Qty, Reason, Transaction Reference
            LblChild.Text = "Defective";
            List<dynamic[]> NameList = new List<dynamic[]>
            {
                new string[] { "SrNo", "Stock Return No.", "" },
                new string[] { "InvoiceNo", "Stock Return No.", "" },
                new string[] { "Customer", "Customer", "" },
                new string[] { "Supplier", "Supplier", "" },
                new string[] { "DefectiveQty", "Qty", "N2" },
                new string[] { "NetPrice", "Net Price", "N2" },
                new string[] { "Total", "Total", "N2" },
            };
            ColumnEncoderNew(NameList);
            if (DataGridPart.CurrentRow != null)
            {
                BegBalTable = _InventoryManagementController.SalesReturnDefectiveDataTable(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString());
                DataGridChild.DataSource = BegBalTable;
            }
        }

        private void ColumnEncoderNew(List<dynamic[]> NameList)
        {
            BegBalTable.Rows.Clear();
            DataGridChild.Columns.Clear();
            if (LblChild.Text != "Sales")
            {
                PnlSalesLegend.Visible = false;
            }
            for (int i = 0; i != NameList.ToList().Count(); i++)
            {
                DataGridViewColumn col = new DataGridViewTextBoxColumn();
                col.Name = col.DataPropertyName = NameList[i][0];
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                col.HeaderText = NameList[i][1];
                col.DefaultCellStyle.Format = NameList[i][2];
                switch(NameList[i][2])
                {
                    case "N2":
                    case "N0":
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;

                    case "Hide":
                        col.Visible = false;
                        break;

                    case "Legend":
                        col.FillWeight = 10;
                        break;
                }
                DataGridChild.Columns.Add(col);
            }
        }

        private void frm_inventory_management_KeyDown(object sender, KeyEventArgs e)
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (NumericLeadNew.Value > 0 && NumericReorderNew.Value > 0 && NumericSGONew.Value > 0)
            {
                if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SetupSave(false);
                }
            }
            else
            {
                MessageBox.Show("Please fill all the fields before saving.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (NumericLeadNew.Value > 0 && NumericReorderNew.Value > 0 && NumericSGONew.Value > 0)
            {
                if (Helper.Confirmator("Are you sure you want apply this data to all parts?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SetupSave(true);
                }
            }
            else
            {
                MessageBox.Show("Please fill all the fields before saving.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SetupSave(bool ApplyAll)
        {
            string CustomMsg = "";
            _InventoryManagementModel = new InventoryManagementModel { PartNo = DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString(), 
                SGO = NumericSGONew.Value, ReorderPoint = NumericReorderNew.Value, LeadTime = NumericLeadNew.Value, ApplyAll = ApplyAll };
            CustomMsg = _InventoryManagementController.Update(_InventoryManagementModel);
            Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (CustomMsg == "Information saved successfully")
            {
                BtnSearch.PerformClick();
                NumericLeadNew.Value = NumericReorderNew.Value = NumericSGONew.Value = 0;
                SetupFill(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString());
            }
        }

        private void SetupFill(string PartNo)
        {
            List<decimal> setup = _InventoryManagementController.Setup(PartNo);
            NumericSGO.Value = setup[0];
            NumericReorder.Value = setup[1];
            NumericLead.Value = setup[2];
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }

        private void BtnQty_Click(object sender, EventArgs e)
        {
            if (DataGridPart.CurrentRow != null)
            {
                List<decimal> quantities = new List<decimal>();
                quantities = _InventoryManagementController.SalesQty(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString());
                TxtMonth1.Textt = quantities[0].ToString("N0");
                TxtMonth2.Textt = quantities[1].ToString("N0");
                TxtMonth3.Textt = quantities[2].ToString("N0");
                TxtMonth4.Textt = quantities[3].ToString("N0");
                TxtMonth5.Textt = quantities[4].ToString("N0");
                TxtMonth6.Textt = quantities[5].ToString("N0");
                TxtMonth7.Textt = quantities[6].ToString("N0");
                TxtMonth8.Textt = quantities[7].ToString("N0");
                TxtMonth9.Textt = quantities[8].ToString("N0");
                TxtMonth10.Textt = quantities[9].ToString("N0");
                TxtMonth11.Textt = quantities[10].ToString("N0");
                TxtMonth12.Textt = quantities[11].ToString("N0");
            }
        }

        private void BtnValue_Click(object sender, EventArgs e)
        {
            List<decimal> values = new List<decimal>();
            if (DataGridPart.CurrentRow != null)
            {
                values = _InventoryManagementController.SalesValue(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString());
                TxtMonth1.Textt = values[0].ToString("N2");
                TxtMonth2.Textt = values[1].ToString("N2");
                TxtMonth3.Textt = values[2].ToString("N2");
                TxtMonth4.Textt = values[3].ToString("N2");
                TxtMonth5.Textt = values[4].ToString("N2");
                TxtMonth6.Textt = values[5].ToString("N2");
                TxtMonth7.Textt = values[6].ToString("N2");
                TxtMonth8.Textt = values[7].ToString("N2");
                TxtMonth9.Textt = values[8].ToString("N2");
                TxtMonth10.Textt = values[9].ToString("N2");
                TxtMonth11.Textt = values[10].ToString("N2");
                TxtMonth12.Textt = values[11].ToString("N2");
            }
        }

        private void DataGridChild_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (LblChild.Text == "Sales")
            {
                switch (Convert.ToInt32(DataGridChild.Rows[e.RowIndex].Cells["MainStatus"].Value))
                {
                    case 1:
                        DataGridChild.Rows[e.RowIndex].Cells["Legend"].Style.BackColor = Color.LightSkyBlue;
                        DataGridChild.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
                        break;

                    case 2:
                        DataGridChild.Rows[e.RowIndex].Cells["Legend"].Style.BackColor = Color.LightYellow;
                        DataGridChild.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.LightYellow;
                        break;

                    case 3:
                        DataGridChild.Rows[e.RowIndex].Cells["Legend"].Style.BackColor = Color.LightGreen;
                        DataGridChild.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.LightGreen;
                        break;
                }
                DataGridChild.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }
    }
}
