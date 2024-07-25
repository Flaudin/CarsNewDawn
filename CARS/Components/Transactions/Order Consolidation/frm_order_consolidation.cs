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
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Transactions
{
    public partial class frm_order_consolidation : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        PoOrderConsolidationController poOrderConsolidationController = new PoOrderConsolidationController();
        OrderList orderList = new OrderList();
        private OrderConsolidate finalOrder = new OrderConsolidate();
        PartsModel partsModel = new PartsModel();
        private SortedDictionary<string, string> _brandsDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _descsDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _supplierDictionary = new SortedDictionary<string, string>();
        private Dictionary<string, string> selectedSuppliers = new Dictionary<string, string>();
        private Label[] monthLabels;
        private TextBox TxtColumnSearch = new TextBox();
        private TextBox TxtPartsColumnSearch = new TextBox();
        private TextBox TxtBreakColumnSearch = new TextBox();
        private DataTable OrderTable = new DataTable();
        private DataTable PartsTable = new DataTable();
        private DataTable BreakTable = new DataTable();
        private DataTable SupplierTable = new DataTable();
        private DataTable TempTable = new DataTable();
        private List<TempSupplierQuotationModel> templist = new List<TempSupplierQuotationModel>();
        private string orderNo = "";

        public frm_order_consolidation(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = PnlOrderTable.BackColor = PnlPartsTable.BackColor = PnlBreakdownTable.BackColor = PnlConsoTable.BackColor = PnlSupplierTable.BackColor = PnlOrderTable.BackColor = PnlTopPart.BackColor =
               PnlTopBSB.BackColor = PnlTopBrand.BackColor = PnlMonthlySales.BackColor = PnlInventoryManage.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblFilter.ForeColor = LblOrderTable.ForeColor = LblPartsTable.ForeColor = LblConsoTable.ForeColor = LblSupplierTable.ForeColor = LblOrderTable.ForeColor =
              LblTopPart.ForeColor = LblTopBSB.ForeColor = LblTopBrands.ForeColor = LblMonthlySales.ForeColor = LblInventoryManage.ForeColor =
              LblBreakdownTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _brandsDictionary = poOrderConsolidationController.getBrands();
            cmbBrands.DataSource = new BindingSource(_brandsDictionary, null);
            cmbBrands.DisplayMember = "Value";
            cmbBrands.ValueMember = "Key";

            _descsDictionary = poOrderConsolidationController.getDescriptions();
            cmbDesc.DataSource = new BindingSource(_descsDictionary, null);
            cmbDesc.DisplayMember = "Value";
            cmbDesc.ValueMember = "Key";
            dashboardCall = DashboardCall;
            

            InitializeMonthLabels();
            UpdateMonthLabels();
            TxtColumnSearch = Helper.ColoumnSearcher(dgvOrders, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            TxtColumnSearch.Location = new Point(dgvOrders.Width / 12, 30);
            TxtPartsColumnSearch = Helper.ColoumnSearcher(dgvParts, 20, 300);
            TxtPartsColumnSearch.KeyUp += TxtPartsColumnSearch_KeyUp;
            TxtPartsColumnSearch.Leave += TxtPartsColumnSearch_Leave;
            TxtPartsColumnSearch.Location = new Point(/*dgvParts.Width*/ 100, 30);
            TxtBreakColumnSearch = Helper.ColoumnSearcher(dgvOrderBreakdown, 20, 300);
            TxtBreakColumnSearch.KeyUp += TxtBreakColumnSearch_KeyUp;
            TxtBreakColumnSearch.Leave += TxtBreakColumnSearch_Leave;
            TxtBreakColumnSearch.Location = new Point(/*dgvParts.Width*/ 100, 30);
            copyQuantity();

            dgvSupplier.CellEndEdit += dgvSupplier_CellEndEdit;
            dgvSupplier.CellValueChanged += dgvSupplier_CellValueChanged;
            dgvSupplier.CurrentCellDirtyStateChanged += dgvSupplier_CurrentCellDirtyStateChanged;
            dgvOrders.Columns["ComputedQty"].DefaultCellStyle.Format = "N0";
            dgvConsolidation.Columns["QtyC"].DefaultCellStyle.Format = "N0";
            dgvOrderBreakdown.Columns["Qty"].DefaultCellStyle.Format = "N0";
            InitializeTempTable();
        }

        private void InitializeTempTable()
        {
            TempTable.Columns.Add("Checker", typeof(bool));
            TempTable.Columns.Add("PartNo",typeof(string));
            TempTable.Columns.Add("BrandName", typeof(string));
            TempTable.Columns.Add("DescName", typeof(string));
            TempTable.Columns.Add("Qty", typeof(decimal));
            TempTable.Columns.Add("CreatedDt", typeof(string));
            TempTable.Columns.Add("CtrlNo", typeof(string));
            TempTable.Columns.Add("OrderQty", typeof(decimal));

            SupplierTable.Columns.Add("isChecked", typeof(bool));
            SupplierTable.Columns.Add("SLName", typeof(string));
            SupplierTable.Columns.Add("SupplierID", typeof(string));
            SupplierTable.Columns.Add("SuppName", typeof(string));
            SupplierTable.Columns.Add("PartNo", typeof(string));
            SupplierTable.Columns.Add("SupplierQuotation", typeof(decimal));
            SupplierTable.Columns.Add("UnitPrice", typeof(decimal));
            SupplierTable.Columns["isChecked"].ReadOnly = false;

        }

        private void InitializeMonthLabels()
        {
            monthLabels = new Label[]
            {
                lblMonth12, lblMonth11, lblMonth10, lblMonth9, lblMonth8, lblMonth7, lblMonth6, lblMonth5, lblMonth4, lblMonth3, lblMonth2, lblMonth1,
            };
        }

        private void UpdateMonthLabels()
        {
            DateTime currentDate = DateTime.Now;
            int currentYear = currentDate.Year % 100;

            for (int i =0; i < 12; i++)
            {
                DateTime date = currentDate.AddMonths(-i);
                string monthAbbrev = date.ToString("MMM");
                int year = date.Year % 100;

                int labelIndex = 11 - i;

                monthLabels[labelIndex].Text = $"{monthAbbrev}-{year:D2}";
            }
        }

        

        private void copyQuantity()
        {
            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                row.Cells["ComputedQty"].Value = row.Cells["TotalQty"].Value;
            }
        }

        

        private void customRoundedButton1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
            orderList = new OrderList { ControlNo ="", Date ="", TotalQty=0};
            OrderTable = poOrderConsolidationController.getOrders(orderList, txtOrderSearch.Textt);
             OrderTable.Columns["CheckOrder"].ReadOnly = false;
            dgvOrders.Columns["CheckOrder"].ReadOnly = false;
                if (OrderTable.Rows.Count == 0 )
            {
                Helper.Confirmator("No Results found.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                    dgvOrders.DataSource = OrderTable;
                    dgvOrders.ClearSelection();
                    
            }
            }
            else if(tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                partsModel = new PartsModel { PartName = "", PartNo = "", Brand = cmbBrands.SelectedValue.ToString(), Description = cmbDesc.SelectedValue.ToString() };
                PartsTable = poOrderConsolidationController.getPartLists(partsModel, txtSearch.Textt.TrimEnd(),rdBSB.Checked,rdCrit.Checked);
                if (PartsTable.Rows.Count == 0)
                {
                    Helper.Confirmator("No results found.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dgvParts.DataSource = PartsTable;
                    dgvParts.ClearSelection();
                }
            }
            
        }

        private void AddConsoItems(DataTable selectedItems)
        {
            TempTable.Columns["Qty"].ReadOnly = false;
            foreach (DataRow item in selectedItems.Rows)
            {
                string partNo = item["PartNo"].ToString();
                decimal qty = Convert.ToDecimal(item["Qty"]);


                var existingRow = TempTable.AsEnumerable()
                    .FirstOrDefault(row => row.Field<string>("PartNo").Trim() == partNo.Trim());

                if (existingRow != null)
                {
                    existingRow["Qty"] = existingRow.Field<decimal>("Qty") + qty;
                }
                else
                {
                    DataRow newRow = TempTable.NewRow();
                    newRow["Checker"] = false;
                    newRow["PartNo"] = item["PartNo"].ToString();
                    newRow["BrandName"] = item["BrandName"].ToString();
                    newRow["DescName"] = item["DescName"].ToString();
                    newRow["Qty"] = Convert.ToDecimal(item["Qty"]);
                    newRow["CreatedDt"] = "";
                    newRow["CtrlNo"] = "";
                    newRow["OrderQty"] = 0;
                    TempTable.Rows.Add(newRow);
                }
            }
            dgvConsolidation.Refresh();
            TempTable.Columns["Qty"].ReadOnly = true;
        }

        private void RemoveConsoItems(DataTable selectedItems) 
        {
            TempTable.Columns["Qty"].ReadOnly = false;
            foreach (DataRow item in selectedItems.Rows)
            {
                string partNo = item["PartNo"].ToString();
                decimal qty = Convert.ToDecimal(item["Qty"]);

                var existingRow = TempTable.AsEnumerable()
                    .FirstOrDefault(row => row.Field<string>("PartNo").Trim() == partNo.Trim());

                if (existingRow != null)
                {
                    decimal currentQty = existingRow.Field<decimal>("Qty") - qty;

                    if (currentQty <= 0)
                    {
                        templist.RemoveAll(x => x.PartNo.Trim() == partNo.Trim());
                        TempTable.Rows.Remove(existingRow);
                    }
                    else
                    {
                        existingRow["Qty"] = currentQty;
                    }
                }
            }
            TempTable.Columns["Qty"].ReadOnly = true;
        }

        //Datagridview for Order
        private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //if(dgvOrders.CurrentRow != null)
            //{
            //    DataTable dt = dgvOrderBreakdown.DataSource as DataTable;
            //    if(dt != null)
            //    {
            //        dt.Rows.Clear();
            //    }
            //    DataGridViewRow currentOrderRow = dgvOrders.CurrentRow;
            //    string ordersControlNo = currentOrderRow.Cells["CtrlNo"].Value.ToString();

            //    foreach(DataGridViewRow row in dgvOrderBreakdown.Rows)
            //    {
            //        string existingOrderControlNo = row.Cells["CtrlNo"].Value.ToString();
            //        if(existingOrderControlNo == ordersControlNo)
            //        {
            //            MessageBox.Show("The selected order has already been added.");
            //            return;
            //        }
            //    }
            //    BreakTable = poOrderConsolidationController.orderBreakdown(ordersControlNo);
            //    if(BreakTable.Rows.Count == 0)
            //    {
            //        Helper.Confirmator("No results found.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else
            //    {
            //        dgvOrderBreakdown.DataSource = BreakTable;
            //        dgvOrderBreakdown.ClearSelection();
            //        var selectedItems = BreakTable;
            //        dgvOrders.Refresh();
            //        if (Convert.ToBoolean(currentOrderRow.Cells["CheckOrder"].Value))
            //        {
            //            AddConsoItems(selectedItems);

            //        } else
            //        {
            //            RemoveConsoItems(selectedItems);
            //        }
            //        //dgvConsolidation.DataSource = BreakTable;
            //    }
            //    dgvConsolidation.DataSource = TempTable;
            //    dgvConsolidation.Refresh();

            //}
        }

        private void dgvOrderBreakdown_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvOrderBreakdown != null)
            {
                DataTable dt = dgvOrderBreakdown.DataSource as DataTable;
                if (dt != null)
                {
                    DataGridViewRow currentBreakDownRow = dgvOrderBreakdown.CurrentRow;
                    string breakDownPartNo = currentBreakDownRow.Cells["PartNo"].Value.ToString().TrimEnd();
                    string breakDownCtrlNo = currentBreakDownRow.Cells["CtrlNoBreak"].Value.ToString().TrimEnd();
                    dgvOrderBreakdown.CurrentRow.Cells[0].Value = true;

                    foreach(DataGridViewRow row in dgvConsolidation.Rows)
                    {
                        string existingPartNo = row.Cells["PartC"].Value.ToString();
                        if(existingPartNo == breakDownPartNo)
                        {
                            Helper.Confirmator("The selected item cannot be added to the table.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        
                    }
                    DataTable dtCons = new DataTable();
                    dtCons = poOrderConsolidationController.orderConsolidation(breakDownPartNo,breakDownCtrlNo);
                    if (dtCons.Rows.Count == 0)
                    {
                        Helper.Confirmator("No results found.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        foreach(DataRow data in dtCons.Rows)
                        {
                            DataRow parte = data;
                            DataGridViewRow dataRpw = new DataGridViewRow();
                            dataRpw.CreateCells(dgvConsolidation);
                            dataRpw.Cells[0].Value = false;
                            dataRpw.Cells[4].Value = data[3].ToString().TrimEnd();
                            dataRpw.Cells[5].Value = data[6].ToString().TrimEnd();
                            dataRpw.Cells[6].Value = data[5].ToString().TrimEnd();
                            //dataRpw.Cells[7].Value = data[4].ToString().TrimEnd();
                            dataRpw.Cells[7].Value = Convert.ToDecimal(data[4]).ToString("N0");
                            dgvConsolidation.Rows.Add(dataRpw);
                        }
                        if(dgvOrders.CurrentRow.Cells["CtrlNo"].Value.ToString().TrimEnd() == dgvOrderBreakdown.CurrentRow.Cells["CtrlNoBreak"].Value.ToString().TrimEnd())
                        {
                            if (Convert.ToBoolean(dgvOrderBreakdown.CurrentRow.Cells["Chkbx"].Value))
                            {
                                dgvConsolidation.Columns["QtyC"].DefaultCellStyle.Format = "N0";
                                decimal nutz = Convert.ToDecimal(dgvOrders.CurrentRow.Cells["ComputedQty"].Value) - Convert.ToDecimal(dgvOrderBreakdown.CurrentRow.Cells["Qty"].Value);
                                dgvOrders.CurrentRow.Cells["ComputedQty"].Value = nutz.ToString("N0");
                            }
                        }
                        dgvConsolidation.ClearSelection();
                    }
                }
            }
        }

        private void dgvParts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = dgvParts.Columns["Chckbx"].Index;
            if (e.RowIndex >= 0 && e.ColumnIndex == columnIndex)
            {
                //PartSelection(e.RowIndex, false);
            }
            //DataTable dt = dgvParts.DataSource as DataTable;
            //if(dt != null)
            //{
            //    DataGridViewRow currentPartsRow = dgvParts.CurrentRow;
            //    string partsPartNo = currentPartsRow.Cells["PartNoName"].Value.ToString().TrimEnd();
            //    dgvParts.CurrentRow.Cells[0].Value = true;


            //    foreach (DataGridViewRow row in dgvConsolidation.Rows)
            //    {
            //        string existingBreakDownControlNo = row.Cells["PartC"].Value.ToString();
            //        if (existingBreakDownControlNo == partsPartNo)
            //        {
            //            MessageBox.Show("The selected item has already been added.");
            //            return;
            //        }

            //    }
            //    string partBrand = currentPartsRow.Cells["BrandName"].Value.ToString().TrimEnd();
            //    string partDesc = currentPartsRow.Cells["DescName"].Value.ToString().TrimEnd();
            //    DataGridViewRow dataRpw = new DataGridViewRow();
            //    dataRpw.CreateCells(dgvConsolidation);
            //    dataRpw.Cells[0].Value = false;
            //    dataRpw.Cells[4].Value = partsPartNo.ToString().TrimEnd();
            //    dataRpw.Cells[5].Value = partBrand.ToString().TrimEnd();
            //    dataRpw.Cells[6].Value = partDesc.ToString().TrimEnd();
            //    dgvConsolidation.Rows.Add(dataRpw);
            //}
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            templist.Clear();

            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
            txtOrderSearch.Textt = "";
                if(dgvOrders.Rows.Count != 0)
                {
                DataTable dt = dgvOrders.DataSource as DataTable;
                dt.Rows.Clear();
                    if (dgvOrderBreakdown.Rows.Count != 0)
                    {
                    DataTable dtBreak = dgvOrderBreakdown.DataSource as DataTable;
                    dtBreak.Rows.Clear();
                    }
                }
            dtStart.Value = DateTime.Today;
                dtEnd.Value = DateTime.Today;
            }else if(tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
            txtSearch.Textt = "";
                cmbBrands.SelectedIndex = 0;
                cmbDesc.SelectedIndex = 0;
                if(dgvParts.Rows.Count != 0)
                {
                DataTable dt = dgvParts.DataSource as DataTable;
                if(dt.Rows.Count != 0)
                {
                dt.Rows.Clear();
                }
                }
            }
            OrderTable.Rows.Clear();
            BreakTable.Rows.Clear();
            SupplierTable.Rows.Clear();
            PartsTable.Rows.Clear();
            TempTable.Rows.Clear();
            dgvSupplier.Refresh();
            dgvConsolidation.Refresh();
            dgvParts.Refresh();
            dgvOrderBreakdown.Refresh();
            dgvOrders.Refresh();
        }

        

        private void dgvSupplier_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvSupplier.Columns["cmbPartNo"].Index && e.RowIndex != -1)
            {
                if(dgvConsolidation.Rows.Count == 0)
                {
                    Helper.Confirmator("There's no data found in Consolidation", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //cmbPartNo.ReadOnly = true;
                }
            }
        }

        //Order Consolidation Status 1 'Post / Send to BSB', 2 'Save Order Consolidation'

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool hasZeroQuantity = TempTable.AsEnumerable().Any(row => Convert.ToInt32(row["OrderQty"]) == 0);
            if (!hasZeroQuantity)
            {
                if (dgvConsolidation.Rows.Count == 0)
                {
                    Helper.Confirmator("No order items found", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //foreach (DataGridViewRow row in dgvConsolidation.Rows)
                    //{
                    //    if (Convert.ToDecimal(row.Cells["QtyC"].Value) == 0)
                    //    {
                    //        Helper.Confirmator("Please complete the provided items", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    }
                    //}

                }
                //else if(dgvSupplier.Rows.Count == 0)
                //{
                //    Helper.Confirmator("No supplier assigned found", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}\
                else
                {
                    string msg = poOrderConsolidationController.checkSuppQuot(TempTable);
                    if (String.IsNullOrEmpty(msg))
                    {
                        List<OrderDetails> orderDetList = new List<OrderDetails>();
                        List<TempSupplierQuotationModel> asdasd2 = templist;
                        templist.RemoveAll(x => x.IsCheck == false);

                        if(dgvConsolidation.Rows.Count == templist.Count)
                        {
                            finalOrder = new OrderConsolidate
                            {
                                SupplierID = "",
                                Status = 1
                            };
                            foreach (DataGridViewRow row in dgvConsolidation.Rows)
                            {
                                decimal unitPrice = poOrderConsolidationController.getUnitPrice(row.Cells["PartC"].Value.ToString());
                                string supplierIDString = templist
                                .Where(s => s.PartNo.Trim() == row.Cells["PartC"].Value.ToString().Trim())
                                .Select(s => s.SuppID.ToString())
                                .FirstOrDefault();
                                OrderDetails orderDetails = new OrderDetails
                                {
                                    PartNo = row.Cells["PartC"].Value.ToString(),
                                    OrdrQty = Convert.ToDecimal(row.Cells["QtyC"].Value),
                                    Status = 1,
                                    UnitPrice = unitPrice,
                                    SupplierID = supplierIDString
                                };
                                finalOrder.SupplierID = supplierIDString;
                                orderDetList.Add(orderDetails);
                            }
                            finalOrder.orderDetails = orderDetList;
                            Dictionary<string, string> keyValuePairs = poOrderConsolidationController.saveConsolidate(finalOrder);
                            KeyValuePair<string, string> msgRes = keyValuePairs.First();
                            KeyValuePair<string, string> ctrlNo = keyValuePairs.Last();
                            string messageResult = msgRes.Key;
                            orderNo = ctrlNo.Value;
                            Helper.Confirmator(messageResult, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (messageResult == "Consolidation saved successfully")
                            {
                                //TempTable.Rows.Clear();
                                //dgvConsolidation.Refresh();
                                //OrderTable.Rows.Clear();
                               // dgvOrders.Refresh();
                                //dgvOrderBreakdown.Rows.Clear();
                                //dgvSupplier.Rows.Clear();
                            }
                        } else
                        {
                            Helper.Confirmator("Please select supplier", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        Helper.Confirmator(msg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            } else
            {
                Helper.Confirmator("One item have zero quantity", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void btnSendToBSB_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(orderNo))
            {
                Helper.Confirmator("Saved it first to proceed", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string msgResult = poOrderConsolidationController.post(orderNo);
                Helper.Confirmator(msgResult, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (msgResult == "Post Successfully")
                {
                    OrderTable.Rows.Clear();
                    BreakTable.Rows.Clear();
                    SupplierTable.Rows.Clear();
                    PartsTable.Rows.Clear();
                    TempTable.Rows.Clear();
                    dgvSupplier.Refresh();
                    dgvConsolidation.Refresh();
                    dgvParts.Refresh();
                    dgvOrderBreakdown.Refresh();
                    dgvOrders.Refresh();
                    templist.Clear();
                }

            }
            ////Check first if the item in order Consolidate Table has a value/entry
            //if (dgvConsolidation.Rows.Count == 0)
            //{
            //    Helper.Confirmator("No order items found", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    foreach (DataGridViewRow row in dgvConsolidation.Rows)
            //    {
            //        if (row.Cells["QtyC"].Value == null)
            //        {
            //            Helper.Confirmator("Please complete the provided items", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }

            //}
            //else if(SupplierTable.Rows.Count <= 0)
            //{
            //    Helper.Confirmator("No supplier assigned found", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    List<OrderDetails> orderDetList = new List<OrderDetails>();
            //    foreach (DataGridViewRow row in dgvConsolidation.Rows)
            //    {
            //        OrderDetails orderDetails = new OrderDetails
            //        {
            //            PartNo = row.Cells["PartC"].Value.ToString(),
            //            OrdrQty = Convert.ToDecimal(row.Cells["QtyC"].Value),
            //            Status = 1,
            //            UnitPrice = Convert.ToDecimal(dgvSupplier.CurrentRow.Cells["UnitPrice"].Value),
            //        };
            //        orderDetList.Add(orderDetails);
            //        foreach (DataGridViewRow supplierRow in dgvSupplier.Rows)
            //        {
            //            finalOrder = new OrderConsolidate
            //            {
            //                orderDetails = orderDetList,
            //                SupplierID = selectedSupplierF(),
            //                Status = 1
            //            };
            //        }
            //    }
            //    string msgResult = poOrderConsolidationController.saveConsolidate(finalOrder);
            //    Helper.Confirmator(msgResult, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (msgResult == "Consolidation Saved")
            //    {
            //        dgvConsolidation.Rows.Clear();
            //        OrderTable.Rows.Clear();
            //        BreakTable.Rows.Clear();
            //        SupplierTable.Rows.Clear();
            //    }
            //}
        }

        int currentCol = 1;
        private void dgvOrders_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(!TxtColumnSearch.Visible && dgvOrders.Rows.Count > 0)
            {
                if (dgvOrders.Rows.Count == 0)
                {
                    //DataTable dt = dgvOrders.DataSource as DataTable;
                    OrderTable.DefaultView.RowFilter = "";
                }
                currentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + dgvOrders.Columns[e.ColumnIndex].HeaderText;
                TxtColumnSearch.Visible = true;
                TxtColumnSearch.Focus();
            }
        }

        private void TxtColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtColumnSearch.Visible =false;
        }

        private void TxtColumnSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                TxtColumnSearch.Visible = false;
            }
            else
            {
                string searchCol = dgvOrders.Columns[currentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = OrderTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                dgvOrders.DataSource = bs;
                dgvOrders.ClearSelection();
            }
        }

        private void TxtPartsColumnSearch_Leave(object sender,EventArgs e)
        {
            TxtPartsColumnSearch.Visible = false;
        }

        private void TxtPartsColumnSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                TxtPartsColumnSearch.Visible = false;
            }
            else
            {
                string searchCol = dgvParts.Columns[currentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtPartsColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = PartsTable;
                bs.Filter = $"[{searchCol}] LIKE '%{valueSearch}%'";
                dgvParts.DataSource = bs;
                dgvParts.ClearSelection();
            }
        }

        int partsCurrentCol = 1;
        private void dgvParts_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int SelectionIndex = dgvParts.Columns["Chckbx"].Index;
            if(!TxtPartsColumnSearch.Visible && dgvParts.Rows.Count > 0 && e.ColumnIndex != SelectionIndex)
            {
                if(dgvParts.Rows.Count == 0)
                {
                    PartsTable.DefaultView.RowFilter = "";
                }
                partsCurrentCol = e.ColumnIndex;
                TxtPartsColumnSearch.Text = "Search" + dgvParts.Columns[e.ColumnIndex].HeaderText;
                TxtPartsColumnSearch.Visible = true;
                TxtPartsColumnSearch.Focus();
            }else if(e.ColumnIndex == SelectionIndex)
            {
                bool AllSelected = false;
                var uncheckedRows = dgvParts.Rows.Cast<DataGridViewRow>()
                                    .Where(row => !Convert.ToBoolean(row.Cells["Chckbx"].Value));

                if (uncheckedRows.Any())
                {
                    AllSelected = true;
                }

                foreach (DataGridViewRow row in dgvParts.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["Chckbx"] as DataGridViewCheckBoxCell;
                    if (!checkBoxCell.ReadOnly)
                    {
                        checkBoxCell.Value = AllSelected;

                    }
                }
                dgvParts.EndEdit();
            }
        }

        private void TxtBreakColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtBreakColumnSearch.Visible = false;
        }

        private void TxtBreakColumnSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                TxtBreakColumnSearch.Visible = false;
            }
            else
            {
                string searchCol = dgvOrderBreakdown.Columns[currentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtBreakColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = PartsTable;
                bs.Filter = $"[{searchCol}] LIKE '%{valueSearch}%'";
                dgvOrderBreakdown.DataSource = bs;
                dgvOrderBreakdown.ClearSelection();
            }
        }

        int breakCurrentCol = 1;
        private void dgvOrderBreakdown_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int SelectedIndex = dgvOrderBreakdown.Columns["Chkbx"].Index;
            if (TxtBreakColumnSearch.Visible && dgvOrderBreakdown.Rows.Count > 0 && e.ColumnIndex != SelectedIndex)
            {
                if (dgvOrderBreakdown.Rows.Count == 0)
                {
                    BreakTable.DefaultView.RowFilter = "";
                }
                breakCurrentCol = e.ColumnIndex;
                TxtBreakColumnSearch.Text = "Search" + dgvOrderBreakdown.Columns[e.ColumnIndex].HeaderText;
                TxtBreakColumnSearch.Visible = true;
                TxtBreakColumnSearch.Focus();
            }else if (e.ColumnIndex == SelectedIndex)
            {
                bool AllSelected = false;
                var uncheckedRows = dgvOrderBreakdown.Rows.Cast<DataGridViewRow>()
                    .Where(row => !Convert.ToBoolean(row.Cells["Chkbx"].Value));
                if (uncheckedRows.Any())
                {
                    AllSelected = true;
                }
                foreach (DataGridViewRow row in dgvOrderBreakdown.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["Chkbx"] as DataGridViewCheckBoxCell;
                    if (!checkBoxCell.ReadOnly)
                    {
                        checkBoxCell.Value = AllSelected;
                    }
                }
                dgvParts.EndEdit();
            }
        }

        public void dgvSupplier_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvSupplier.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        public void dgvSupplier_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSupplier_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvSupplier.IsCurrentCellDirty)
            {
                DataGridViewCell currentCell = dgvSupplier.CurrentCell;
                dgvSupplier.CommitEdit(DataGridViewDataErrorContexts.Commit);

                if (currentCell.ColumnIndex == dgvSupplier.Columns["isChecked"].Index)
                {
                    bool isChecked = Convert.ToBoolean(dgvSupplier.Rows[currentCell.RowIndex].Cells["isChecked"].Value);
                    var cellValue = dgvSupplier.Rows[currentCell.RowIndex].Cells["cmbPartNo"].Value;

                    if (cellValue != null)
                    {
                        TempSupplierQuotationModel checktempsuppquot2 = templist.FirstOrDefault(temp => temp.PartNo == dgvSupplier.Rows[currentCell.RowIndex].Cells["cmbPartNo"].Value.ToString().Trim() && temp.SuppID.Trim() == dgvSupplier.Rows[currentCell.RowIndex].Cells["SupplierID"].Value.ToString().Trim());
                        if (isChecked)
                        {
                            if (checktempsuppquot2 != null)
                            {
                                checktempsuppquot2.IsCheck = true;
                            }
                        }
                        else
                        {
                            if (checktempsuppquot2 != null)
                            {
                                checktempsuppquot2.IsCheck = false;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("The PONo value is null. Please check the data.");
                    }
                }
            }
        }
    
        private string selectedSupplierF()
        {
            string selectedSupp = "";
            if(dgvSupplier.Rows.Count > 0)
            {
                foreach(DataGridViewRow row in dgvSupplier.Rows)
                {
                    bool isChecked = Convert.ToBoolean(row.Cells["isChecked"].Value);
                    if (isChecked)
                    {
                        selectedSupp = row.Cells["SupplierID"].Value.ToString();
                    }

                }
            }
            else
            {
                Helper.Confirmator("No Supplier found Please Add Supplier Quotation first.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return selectedSupp;
        }

        private void btnRecDel_Click(object sender, EventArgs e)
        {
            if (dgvConsolidation.Rows.Count == 0)
            {
                Helper.Confirmator("No orders found, Please add an item/s.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var rowsToRemove = dgvConsolidation.Rows
                                            .Cast<DataGridViewRow>()
                                            .Where(row => Convert.ToBoolean(row.Cells["chck"].Value))
                                            .ToList();

            var partNumbersToRemove = dgvConsolidation.Rows
                                .Cast<DataGridViewRow>()
                                .Where(row => Convert.ToBoolean(row.Cells["chck"].Value))
                                .Select(row => row.Cells["PartC"].Value.ToString())
                                .ToList();

            if (rowsToRemove.Count == 0)
            {
                Helper.Confirmator("No selected item to delete.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Helper.Confirmator("Are you sure you want to delete this order?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                foreach (var row in rowsToRemove)
                {
                    dgvConsolidation.Rows.Remove(row);
                }

                foreach(var partno in partNumbersToRemove)
                {
                    templist.RemoveAll(x => x.PartNo.Trim() == partno.Trim());
                }
                Helper.Confirmator("Removed successfully!", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SupplierTable.Rows.Clear();
                txtBegBal.Textt = "";
                txtRcvd.Textt = "";
                txtTakeUp.Textt = "";
                txtSales.Textt = "";
                txtUnitCost.Textt = "";
                txtAveCost.Textt = "";
                txtDropped.Textt = "";
                txtBOH.Textt = "";
            }
        }

        private void dgvConsolidation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBegBal.Textt = "";
            txtRcvd.Textt = "";
            txtTakeUp.Textt = "";
            txtSales.Textt = "";
            txtUnitCost.Textt = "";
            txtAveCost.Textt = "";
            txtDropped.Textt = "";
            txtBOH.Textt = "";

            if (dgvConsolidation.Rows.Count != 0)
            {
                SupplierTable.Rows.Clear();
                dgvSupplier.Refresh();
                string partNo = dgvConsolidation.CurrentRow.Cells["PartC"].Value.ToString().TrimEnd();
                DataTable dtResult = poOrderConsolidationController.inventoryMovement(dgvConsolidation.Rows[e.RowIndex].Cells["PartC"].Value.ToString().TrimEnd());
                if(dtResult.Rows.Count > 0)
                {
                    txtBegBal.Textt = Convert.ToDouble(dtResult.Rows[0][1] ?? 0).ToString("N2");
                    txtRcvd.Textt = Convert.ToDouble(dtResult.Rows[0][0] ?? 0).ToString("N2");
                    txtTakeUp.Textt = Convert.ToDouble(dtResult.Rows[0][2] ?? 0).ToString("N2");
                    txtSales.Textt = Convert.ToDouble(dtResult.Rows[0][5] ?? 0).ToString("N2");
                    txtUnitCost.Textt = Convert.ToDouble(dtResult.Rows[0][3] ?? 0).ToString("N2");
                    txtAveCost.Textt = Convert.ToDouble(dtResult.Rows[0][4] ?? 0).ToString("N2");
                    txtDropped.Textt = Convert.ToDouble(dtResult.Rows[0][6] ?? 0).ToString("N2");
                    txtBOH.Textt = Convert.ToDouble(dtResult.Rows[0][7] ?? 0).ToString("N2");
                }
                else
                {
                    txtBegBal.Textt = "0.00";
                    txtRcvd.Textt = "0.00";
                    txtTakeUp.Textt = "0.00";
                    txtSales.Textt = "0.00";
                    txtUnitCost.Textt = "0.00";
                    txtAveCost.Textt = "0.00";
                    txtDropped.Textt = "0.00";
                    txtBOH.Textt = "0.00";
                }
                //dgvConsolidation.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                dgvConsolidation.CurrentRow.Cells["chck"].Value = true;
                List<TempSupplierQuotationModel> checktempsuppquot = templist.Where(temp => temp.PartNo.Trim() == partNo.Trim()).ToList();
                if (checktempsuppquot.Any())
                {
                    foreach (var item in checktempsuppquot)
                    {
                        DataRow newrow = SupplierTable.NewRow();
                        newrow["PartNo"] = item.PartNo;
                        newrow["SupplierQuotation"] = item.UnitPrice;
                        newrow["SupplierID"] = item.SuppID;
                        newrow["SLName"] = item.SuppName;
                        newrow["isChecked"] = item.IsCheck;
                        newrow["UnitPrice"] = item.UnitPrice;
                        SupplierTable.Rows.Add(newrow);
                    }
                }
                else
                {
                    //LocationTable = txtRRN.Textt == "NEW RECORD" ? receivingController.LocationDisplay(partNo) : receivingController.LocationDisplayWRR(partNo, txtRRN.Textt.ToString().TrimEnd(),1);
                    SupplierTable = poOrderConsolidationController.getSupplierQuotation(dgvConsolidation.CurrentRow.Cells["PartC"].Value.ToString().TrimEnd());
                    foreach (DataRow row in SupplierTable.Rows)
                    {
                        bool isExist = templist.Where(ex => ex.PartNo.Trim() == row["PartNo"].ToString().Trim() && ex.SuppID.Trim() == row["SupplierID"].ToString().Trim()).Any();
                        if (!isExist)
                        {
                            TempSupplierQuotationModel item = new TempSupplierQuotationModel
                            {
                                PartNo = row["PartNo"].ToString().Trim(),
                                IsCheck = false,
                                UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                                SupplierQuotation = Convert.ToDecimal(row["UnitPrice"]),
                                SuppID = row["SupplierID"].ToString(),
                                SuppName = row["SLName"].ToString().Trim(),
                            };
                            templist.Add(item);
                        }
                    }
                }

                dgvSupplier.DataSource = SupplierTable;
                SupplierTable.Columns["isChecked"].ReadOnly = false;
                dgvSupplier.Columns["isChecked"].ReadOnly = false;
                SupplierTable.Columns["SupplierQuotation"].ReadOnly = false;
                dgvSupplier.Columns["SupplierQuotation"].ReadOnly = false;
                foreach (DataGridViewRow row in dgvSupplier.Rows)
                {
                    decimal price = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
                    if (price != 0)
                    {
                        row.Cells["SupplierQuotation"].Value = Convert.ToDecimal(dgvConsolidation.CurrentRow.Cells["QtyC"].Value) * price;
                    }
                }
                SupplierTable.Columns["SupplierQuotation"].ReadOnly = true;
                dgvSupplier.Columns["SupplierQuotation"].ReadOnly = true;
                /*if (selectedSuppliers.ContainsKey(partNo))
                {
                    string selectedSupplierId = selectedSuppliers[partNo];
                    foreach (DataGridViewRow row in dgvSupplier.Rows)
                    {
                        TempSupplierQuotationModel checktempsuppquot2 = templist.FirstOrDefault(temp => temp.PartNo == partNo.Trim());
                        if (row.Cells["SupplierID"].Value.ToString() == selectedSupplierId)
                        {
                            row.Cells["isChecked"].Value = true; 
                            if (checktempsuppquot2 != null)
                            {
                                checktempsuppquot2.IsCheck = true;
                            }
                        }
                        else
                        {
                            row.Cells["isChecked"].Value = false;
                            if (checktempsuppquot2 != null)
                            {
                                checktempsuppquot2.IsCheck = false;
                            }
                        }
                    }
                }*/
            }
            else
            {
                Helper.Confirmator("No results found", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvConsolidation_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(dgvConsolidation_KeyPress);
            if (dgvConsolidation.CurrentCell.ColumnIndex == dgvConsolidation.Columns["QtyC"].Index)
            {
                e.Control.KeyPress += new KeyPressEventHandler(dgvConsolidation_KeyPress);
                e.Control.KeyPress += Numeric_KeyPress;
            }
        }

        private void dgvConsolidation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
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

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }

        private void dgvParts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = dgvParts.Columns["Chckbx"].Index;
            if (e.RowIndex >= 0 && e.ColumnIndex != columnIndex)
            {
                
            }
        }
        //private void PartSelection(int rowIndex, bool isFromSelect)
        //{
        //    bool isChecked = Convert.ToBoolean(dgvParts.Rows[rowIndex].Cells["Chckbx"].Value);
        //    if (!isFromSelect)
        //    {
        //        dgvParts.Rows[rowIndex].Cells["Chckbx"].Value = !isChecked;
        //    }
        //    else
        //    {
        //        isChecked = !isChecked;
        //    }

        //    if (isChecked)
        //    {
        //        int deletedIndex = -1;
        //        DataGridViewRow row = dgvConsolidation.Rows
        //                            .Cast<DataGridViewRow>()
        //                            .FirstOrDefault(r => r.Cells["PartC"].Value.ToString().Equals(dgvParts.Rows[rowIndex].Cells["PartNoName"].Value.ToString()));

        //        if (row != null)
        //        {
        //            deletedIndex = row.Index;
        //            dgvConsolidation.Rows.RemoveAt(deletedIndex);
        //        }
        //    }
        //    else
        //    {
        //        DataGridViewRow row = dgvConsolidation.Rows
        //                            .Cast<DataGridViewRow>()
        //                            .FirstOrDefault(r => r.Cells["PartC"].Value.ToString().Equals(dgvParts.Rows[rowIndex].Cells["PartNoName"].Value.ToString()));
        //        if (row == null)
        //        {
        //            DataGridViewRow currentPartsRow = dgvParts.CurrentRow;
        //            string partsPartNo = currentPartsRow.Cells["PartNoName"].Value.ToString().TrimEnd();
        //            string partBrand = currentPartsRow.Cells["BrandName"].Value.ToString().TrimEnd();
        //            string partDesc = currentPartsRow.Cells["DescName"].Value.ToString().TrimEnd();
        //            DataGridViewRow dataRpw = new DataGridViewRow();
        //            dataRpw.CreateCells(dgvConsolidation);
        //            dataRpw.Cells[0].Value = false;
        //            dataRpw.Cells[4].Value = partsPartNo.ToString().TrimEnd();
        //            dataRpw.Cells[5].Value = partBrand.ToString().TrimEnd();
        //            dataRpw.Cells[6].Value = partDesc.ToString().TrimEnd();
        //            dgvConsolidation.Rows.Add(dataRpw);
        //        }
        //    } 
        //}

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvOrderBreakdown_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //private void SelectedOrder(int rowIndex, bool isFromSelect)
        //{
        //    bool isChecked = Convert.ToBoolean(dgvOrders.Rows[rowIndex].Cells["CheckOrder"].Value);
        //    if (!isFromSelect)
        //    {
        //        dgvOrders.Rows[rowIndex].Cells["CheckOrder"].Value = !isChecked;
        //    }
        //    else
        //    {
        //        isChecked = !isChecked;
        //    }
        //    if (isChecked)
        //    {
        //        int deletedIndex = -1;
        //        DataGridViewRow row = dgvConsolidation.Rows
        //                            .Cast<DataGridViewRow>()
        //                            .FirstOrDefault(r => r.Cells["PartC"].Value.ToString().Equals(dgvOrderBreakdown.Rows[rowIndex].Cells["PartNo"].Value.ToString()));

        //        if (row != null)
        //        {
        //            deletedIndex = row.Index;
        //            dgvConsolidation.Rows.RemoveAt(deletedIndex);
        //        }
        //    }
        //    else
        //    {
        //        DataGridViewRow row = dgvConsolidation.Rows
        //                            .Cast<DataGridViewRow>()
        //                            .FirstOrDefault(r => r.Cells["PartC"].Value.ToString().Equals(dgvOrderBreakdown.Rows[rowIndex].Cells["PartNo"].Value.ToString()));
        //        if (row == null)
        //        {
        //            DataGridViewRow currentPartsRow = dgvOrderBreakdown.Rows[rowIndex];
        //            string partsPartNo = currentPartsRow.Cells["PartNo"].Value.ToString().TrimEnd();
        //            string partBrand = currentPartsRow.Cells["BreakBrand"].Value.ToString().TrimEnd();
        //            string partDesc = currentPartsRow.Cells["BreakDescription"].Value.ToString().TrimEnd();
        //            DataGridViewRow dataRpw = new DataGridViewRow();
        //            dataRpw.CreateCells(dgvConsolidation);
        //            dataRpw.Cells[0].Value = false;
        //            dataRpw.Cells[4].Value = partsPartNo.ToString().TrimEnd();
        //            dataRpw.Cells[5].Value = partBrand.ToString().TrimEnd();
        //            dataRpw.Cells[6].Value = partDesc.ToString().TrimEnd();
        //            dataRpw.Cells[7].Value = 0;
        //            dgvConsolidation.Rows.Add(dataRpw);
        //        }
        //    }
        //}

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedIndex = dgvOrders.Columns["CheckOrder"].Index;
            dgvOrders.Columns["CheckOrder"].ReadOnly = false;
            OrderTable.Columns["CheckOrder"].ReadOnly = false;
            if (e.RowIndex >=0 && !dgvOrders.Rows[e.RowIndex].Cells["CheckOrder"].ReadOnly)
            {
                bool isChecked =  Convert.ToBoolean(dgvOrders.Rows[e.RowIndex].Cells["CheckOrder"].Value);
                dgvOrders.Rows[e.RowIndex].Cells["CheckOrder"].Value = !isChecked;
                //SelectedOrder(e.RowIndex, false);
                dgvOrders.Refresh();
            }
            if (dgvOrders.CurrentRow != null)
            {
                DataTable dt = dgvOrderBreakdown.DataSource as DataTable;
                if (dt != null)
                {
                    dt.Rows.Clear();
                }
                DataGridViewRow currentOrderRow = dgvOrders.CurrentRow;
                string ordersControlNo = currentOrderRow.Cells["CtrlNo"].Value.ToString();

                foreach (DataGridViewRow row in dgvOrderBreakdown.Rows)
                {
                    string existingOrderControlNo = row.Cells["CtrlNo"].Value.ToString();
                    if (existingOrderControlNo == ordersControlNo)
                    {
                        MessageBox.Show("The selected order has already been added.");
                        return;
                    }
                }
                BreakTable = poOrderConsolidationController.orderBreakdown(ordersControlNo);
                if (BreakTable.Rows.Count == 0)
                {
                    Helper.Confirmator("No results found.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SupplierTable.Rows.Clear();
                    dgvSupplier.Refresh();
                    dgvOrderBreakdown.DataSource = BreakTable;
                    dgvOrderBreakdown.ClearSelection();
                    var selectedItems = BreakTable;
                    if (Convert.ToBoolean(dgvOrders.Rows[e.RowIndex].Cells["CheckOrder"].Value))
                    {
                        AddConsoItems(selectedItems);
                    }
                    else
                    {
                        RemoveConsoItems(selectedItems);
                    }
                    //dgvConsolidation.DataSource = BreakTable;
                }
                dgvConsolidation.DataSource = TempTable;
                dgvConsolidation.Refresh();
            }
        }

        private void dgvParts_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgvConsolidation_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dgvConsolidation.Columns["TotalOrderQty"].Index)
            {
                var OrderQty = Convert.ToDecimal(dgvConsolidation.Rows[e.RowIndex].Cells["QtyC"].Value);
                var OrderQtyInput = Convert.ToDecimal(dgvConsolidation.Rows[e.RowIndex].Cells["TotalOrderQty"].Value);

                if(OrderQtyInput > OrderQty)
                {
                    Helper.Confirmator("The input quantity is invalid, Please input valid quantity", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvConsolidation.Rows[e.RowIndex].Cells["TotalOrderQty"].Value = 0;
                }
            }
        }
    }
    }
    

