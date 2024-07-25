using CARS.Controller.Transactions;
using CARS.Customized_Components;
using CARS.Functions;
using CARS.Model;
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

namespace CARS.Components.Transactions.SalesOrder
{
    public partial class frm_sales_order_entry : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private TransactionController _TransactionController = new TransactionController();
        private SalesOrderController _SalesOrderController = new SalesOrderController();
        private SalesOrderModel _SalesOrderModel = new SalesOrderModel();
        private SortedDictionary<string, string> _CustomerDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _SalesmanDictionary = new SortedDictionary<string, string>();
        private Dictionary<string, string> _PartNoDictionary = new Dictionary<string, string>();
        private List<string> CacheList = new List<string>();
        private DataTable PartsTable = new DataTable();
        private SalesOrderModel OldSo = new SalesOrderModel();
        private SalesOrderDetailModel SuggestedPartDetails = new SalesOrderDetailModel();

        //print
        private Image headerImage;
        private SalesOrderReportModel OwnerCompany = new SalesOrderReportModel();
        //public static string matha = "";

        public frm_sales_order_entry(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = LblTotal.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderTable.BackColor = PnlHeaderFilter.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblSalesman.ForeColor = LblSO.ForeColor = LblTable.ForeColor = LblFilter.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _CustomerDictionary = _TransactionController.GetDictionary("Customer");
            _SalesmanDictionary = _TransactionController.GetDictionary("Salesman");
            //_PartNoDictionary = _TransactionController.GetDictionary("PartNo");
            ComboSalesman.DataSource = new BindingSource(_SalesmanDictionary, null);
            //ComboPartNo.DataSource = new BindingSource(_PartNoDictionary, null);
            ComboSalesman.DisplayMember = "Key";
            ComboSalesman.ValueMember = "Value";
            OldSo = _SalesOrderController.GetUnfinishedOrder();
            ComboSalesParts.ItemSelected += CustomTextBox_ItemSelected;
            ComboFilter.SelectedIndex = 0;
            dashboardCall = DashboardCall;

            //print
            printDocument1.DefaultPageSettings.Landscape = false;
            printDocument1.PrintPage += printDocument1_PrintPage;
            printPreviewDialog1.FormClosed += printPreviewDialog1_FormClosed;
            printPreviewDialog1.Document = printDocument1;
        }

        private void frm_sales_order_entry_Load(object sender, EventArgs e)
        {
            if (OldSo.SoNo != "")
            {
                TxtSoNo.Textt = OldSo.SoNo;
                ComboSalesman.Text = _SalesmanDictionary.FirstOrDefault(pair => pair.Value == OldSo.SalesmanID).Key;
                if (OldSo.DetailsList.Count() > 0)
                {
                    foreach (SalesOrderDetailModel detail in OldSo.DetailsList)
                    {
                        DataGridPart.Rows.Add(false, (DataGridPart.Rows.Count + 1).ToString(), detail.PartNo, detail.PartName, detail.DescName, detail.BrandName,
                                                detail.Sku, detail.UomName, detail.FreeItem, detail.Qty, detail.ListPrice, detail.Discount, detail.NetPrice, 
                                                detail.FreeReason, detail.AllowBelCost, detail.ItemID);
                    }
                    TxtTotalRow.Textt = DataGridPart.Rows.Count.ToString();
                    RecalculateTotal();
                }
            }
        }

        private void ComboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                _TransactionController.ChangeDistinctFilter(comboBox.SelectedItem.ToString());
                ComboSalesParts.Text = "";
            }
        }

        private void CustomTextBox_ItemSelected(object sender, ItemSelectedEventArgs e)
        {
            DataTable PartTable = new DataTable();
            var boolColumn = new DataColumn("ForSelection", typeof(bool));
            switch (ComboFilter.SelectedItem.ToString())
            {
                case "ALL":
                    if (e.SelectedItem != "")
                    {
                        SuggestedPartDetails = _SalesOrderController.GetSuggestedPart(e.SelectedItem);
                        DataGridViewRow row = DataGridPart.Rows
                                              .Cast<DataGridViewRow>()
                                              .FirstOrDefault(x => x.Cells["PartNo"].Value.ToString() == SuggestedPartDetails.PartNo);
                        if (row == null)
                        {
                            DataGridPart.Rows.Add(false, (DataGridPart.Rows.Count + 1).ToString(), SuggestedPartDetails.PartNo, SuggestedPartDetails.PartName,
                                                    SuggestedPartDetails.DescName, SuggestedPartDetails.BrandName, SuggestedPartDetails.Sku, SuggestedPartDetails.UomName,
                                                    false, Convert.ToDecimal(1), SuggestedPartDetails.ListPrice, Convert.ToDouble(0),
                                                    Convert.ToDecimal(SuggestedPartDetails.ListPrice));
                            //SalesOrderDetailModel Detail = new SalesOrderDetailModel
                            //{
                            //    SoNo = TxtSoNo.Textt,
                            //    ItemNo = DataGridPart.Rows[DataGridPart.Rows.Count - 1].Cells["ItemNo"].Value.ToString(),
                            //    PartNo = DataGridPart.Rows[DataGridPart.Rows.Count - 1].Cells["PartNo"].Value.ToString(),
                            //    Qty = 1,
                            //    ListPrice = Convert.ToDouble(DataGridPart.Rows[DataGridPart.Rows.Count - 1].Cells["SRP"].Value),
                            //    Discount = 0,
                            //    NetPrice = 0,
                            //    //SLID = slid,
                            //    VATAmt = 0,
                            //    FreeItem = false,
                            //    FreeReason = "",
                            //    AllowBelCost = false,
                            //    Status = 1
                            //};
                            //DataGridPart.Rows[DataGridPart.Rows.Count - 1].Cells["ItemID"].Value = _SalesOrderController.PickNew(Detail);
                            TxtTotalRow.Textt = DataGridPart.Rows.Count.ToString();
                            ComboSalesParts.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("The selected part is already included in the parts list.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        PartTable = _SalesOrderController.PartsWithBegBalDataTable(ComboSalesParts.Text.TrimEnd());
                        boolColumn.DefaultValue = false;
                        PartTable.Columns.Add(boolColumn);
                        DataGridPartFilter.DataSource = PartTable;
                        DataGridPartFilter.ClearSelection();
                    }
                    break;

                case "BRAND":
                    PartTable = _SalesOrderController.PartsWithBegBalDataTableFilterBrand(ComboSalesParts.Text.TrimEnd());
                    boolColumn.DefaultValue = false;
                    PartTable.Columns.Add(boolColumn);
                    DataGridPartFilter.DataSource = PartTable;
                    DataGridPartFilter.ClearSelection();
                    break;

                case "DESCRIPTION":
                    PartTable = _SalesOrderController.PartsWithBegBalDataTableFilterDesc(ComboSalesParts.Text.TrimEnd());
                    boolColumn.DefaultValue = false;
                    PartTable.Columns.Add(boolColumn);
                    DataGridPartFilter.DataSource = PartTable;
                    DataGridPartFilter.ClearSelection();
                    break;
            }
        }

        private void DataGridPartFilter_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DataGridPartFilter.IsCurrentCellDirty)
            {
                DataGridPartFilter.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DataGridPartFilter_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DataGridPartFilter.Columns["ForSelection"].Index)
            {
                bool isChecked = (bool)DataGridPartFilter.Rows[e.RowIndex].Cells["ForSelection"].Value;
                if (isChecked)
                {
                    DataGridViewRow row = DataGridPart.Rows
                                      .Cast<DataGridViewRow>()
                                      .FirstOrDefault(x => x.Cells["PartNo"].Value.ToString() == DataGridPartFilter.CurrentRow.Cells["PartNoFilter"].Value.ToString());
                    if (row == null)
                    {
                        DataGridViewRow selectedRow = DataGridPartFilter.CurrentRow;
                        DataGridPart.Rows.Add(false, (DataGridPart.Rows.Count + 1).ToString(), selectedRow.Cells["PartNoFilter"].Value.ToString(),
                                                selectedRow.Cells["PartNameFilter"].Value.ToString(), selectedRow.Cells["DescNameFilter"].Value.ToString(),
                                                selectedRow.Cells["BrandNameFilter"].Value.ToString(), selectedRow.Cells["SkuFilter"].Value.ToString(),
                                                selectedRow.Cells["UomNameFilter"].Value.ToString(), false, Convert.ToDecimal(1),
                                                Convert.ToDouble(selectedRow.Cells["ListPriceFilter"].Value), Convert.ToDouble(0), Convert.ToDecimal(selectedRow.Cells["ListPriceFilter"].Value));
                    }
                    else
                    {
                        MessageBox.Show("The selected part is already included in the parts list.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            if (ComboSalesman.SelectedIndex != 0)
            {
                if (Helper.Confirmator("This will generate SO No. for the selected customer and salesman. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    string cust = "", term = "", slid= "", SoNo = "";
                    _SalesOrderModel = new SalesOrderModel
                    {
                        //CashTran = CheckCash.Checked,
                        CustName = cust,
                        //CustAdd = TxtAddress.Textt,
                        //CustTin = TxtTin.Textt,
                        TermID = term,
                        SalesmanID = ComboSalesman.SelectedValue.ToString(),
                        SLID = slid,
                        Status = 1
                    };
                    SoNo = _SalesOrderController.Create(_SalesOrderModel);
                    if (SoNo.Substring(0,2) != "SO")
                    {
                        MessageBox.Show("Something went wrong", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please first encode a customer and a salesman before proceeding", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnAddParts_Click(object sender, EventArgs e)
        {
            //if (TxtSoNo.Textt != "")
            //{
            //    if (DataGridPart.Rows.Count != 26)
            //    {
            //        DataGridPart.Focus();
            //        List<string> PartData = new List<string>();
            //        foreach (DataGridViewRow row in DataGridPart.Rows)
            //        {
            //            PartData.Add(row.Cells[DataGridPart.Columns["PartNo"].Index].Value.ToString());
            //        }
            //        frm_sales_order_parts_encode partEncode = new frm_sales_order_parts_encode(PartData, DataGridPart.Rows.Count);
            //        partEncode.StringArraySent += ReceiveArrayFromChild;
            //        partEncode.ShowDialog(this);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Sales Order only allows twenty six parts per order, sorry for the inconvenience", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("SO No. is required before adding Part No.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void ReceiveArrayFromChild(List<dynamic[]> stringArray)
        {
            //for (int i = 0; i != stringArray.Count; i++)
            //{
            //    DataGridViewRow row = DataGridPart.Rows
            //        .Cast<DataGridViewRow>()
            //        .FirstOrDefault(r => r.Cells["PartNo"].Value?.ToString() == stringArray[i][0]);
            //    if (row == null)
            //    {
            //        string slid = string.IsNullOrEmpty(ComboCustomer.Text) ? null : ComboCustomer.SelectedValue.ToString();
            //        DataGridPart.Rows.Add(false, (DataGridPart.Rows.Count+1).ToString(), stringArray[i][0], stringArray[i][1], stringArray[i][2], 
            //                                stringArray[i][3], stringArray[i][4], stringArray[i][5], false, Convert.ToDecimal(1), Convert.ToDouble(stringArray[i][6]), 
            //                                Convert.ToDouble(0), Convert.ToDecimal(stringArray[i][6]));
            //        SalesOrderDetailModel Detail = new SalesOrderDetailModel
            //        {
            //            SoNo = TxtSoNo.Textt,
            //            ItemNo = DataGridPart.Rows[DataGridPart.Rows.Count - 1].Cells["ItemNo"].Value.ToString(),
            //            PartNo = DataGridPart.Rows[DataGridPart.Rows.Count - 1].Cells["PartNo"].Value.ToString(),
            //            Qty = 1,
            //            ListPrice = Convert.ToDouble(DataGridPart.Rows[DataGridPart.Rows.Count - 1].Cells["SRP"].Value),
            //            Discount = 0,
            //            NetPrice = 0,
            //            SLID = slid,
            //            VATAmt = 0,
            //            FreeItem = false,
            //            FreeReason = "",
            //            AllowBelCost = false,
            //            Status = 1
            //        };
            //        DataGridPart.Rows[DataGridPart.Rows.Count - 1].Cells["ItemID"].Value = _SalesOrderController.PickNew(Detail);
            //        TxtTotalRow.Textt = DataGridPart.Rows.Count.ToString();
            //    }
            //}
            //RecalculateTotal();
        }

        private void BtnDeleteParts_Click(object sender, EventArgs e)
        {
            //bool hasCheckedCell = DataGridPart.Rows.Cast<DataGridViewRow>().Any(row => Convert.ToBoolean(row.Cells["IsSelected"].Value));
            //switch (hasCheckedCell)
            //{
            //    case true:
            //        if (DataGridPart.CurrentRow != null && Helper.Confirmator("Are you sure you want to deleted the selected rows?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            //        {
            //            for (int i = DataGridPart.Rows.Count - 1; i >= 0; i--)
            //            {
            //                if (Convert.ToBoolean(DataGridPart.Rows[i].Cells[0].Value))
            //                {
            //                    _SalesOrderController.ResetPick(DataGridPart.Rows[i].Cells["PartNo"].Value.ToString(), Convert.ToDecimal(DataGridPart.Rows[i].Cells["Qty"].Value));
            //                    _SalesOrderController.ResetSalesDetails(TxtSoNo.Textt, DataGridPart.Rows[i].Cells["ItemID"].Value.ToString());
            //                    DataGridPart.Rows.RemoveAt(i);
            //                }
            //            }
            //            RecalculateTotal();
            //        }
            //        break;

            //    case false:
            //        if (DataGridPart.CurrentRow != null)
            //        {
            //            if (Helper.Confirmator("Are you sure you want to deleted the selected row?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            //            {
            //                _SalesOrderController.ResetPick(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString(), Convert.ToDecimal(DataGridPart.CurrentRow.Cells["Qty"].Value));
            //                _SalesOrderController.ResetSalesDetails(TxtSoNo.Textt, DataGridPart.CurrentRow.Cells["ItemID"].Value.ToString());
            //                DataGridPart.Rows.RemoveAt(DataGridPart.CurrentRow.Index);
            //                RecalculateTotal();
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("There is no row selected for deletion.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        break;
            //}
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will clear the current transaction. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                ClearSOData();
                ClearEncode();
            }
        }

        private void ClearEncode()
        {
            TxtSoNo.Textt = "";
            ComboSalesman.SelectedIndex = 0;
            TxtTotal.Textt = "0.00";
            TxtTotalRow.Textt = TxtTotalQty.Textt = "0";
            DataGridPart.Rows.Clear();
        }

        private void ClearSOData()
        {
            foreach (DataGridViewRow row in DataGridPart.Rows)
            {
                _SalesOrderController.ResetPick(row.Cells["PartNo"].Value.ToString(), Convert.ToDecimal(row.Cells["Qty"].Value));
                _SalesOrderController.ResetSalesDetails(row.Cells["PartNo"].Value.ToString(), row.Cells["ItemID"].Value.ToString());
            }
            _SalesOrderController.CancelSO(TxtSoNo.Textt);
        }

        //private void ComboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ComboCustomer.SelectedIndex != 0)
        //    {
        //        CacheList = GetCustomerDetail(ComboCustomer.SelectedValue.ToString());
        //        TxtAddress.Textt = CacheList[0];
        //        TxtTerm.Textt = CacheList[1];
        //        TxtTin.Textt = CacheList[2];
        //    }
        //    else
        //    {
        //        TxtAddress.Textt = TxtTin.Textt = TxtTerm.Textt = "";
        //    }
        //}

        //private List<string> GetCustomerDetail(string id) => _SalesOrderController.CustomerDetailsRead(id);

        //private void RadioSOTerm_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (RadioCash.Checked)
        //    {
        //        ComboTerm.SelectedIndex = 0;
        //        ComboTerm.Enabled = false;
        //    }
        //    else if (RadioTerm.Checked)
        //    {
        //        ComboTerm.Text = TxtTerm.Textt;
        //        ComboTerm.Enabled = true;
        //    }
        //}

        decimal OldQty = 0;
        private void DataGridPart_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (DataGridPart.CurrentCell.ColumnIndex == DataGridPart.Columns["Qty"].Index)
            {
                OldQty = Convert.ToDecimal(DataGridPart.CurrentCell.Value);
            }
        }

        private void DataGridPart_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DataGridPart.Columns["Qty"].Index)
            {
                HandleQtyColumn(e.RowIndex);
            }
        }

        private void HandleQtyColumn(int rowIndex)
        {
            var currentCell = DataGridPart.CurrentCell;
            if (currentCell.Value != null && currentCell.Value.ToString() != "" && currentCell.Value.ToString() != "0")
            {
                _SalesOrderController.ResetPick(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString(), OldQty);
                decimal Boh = _SalesOrderController.GetBoh(DataGridPart.Rows[rowIndex].Cells["PartNo"].Value.ToString());
                if (Boh >= Convert.ToDecimal(currentCell.Value))
                {
                    decimal total = (Convert.ToDecimal(DataGridPart.Rows[rowIndex].Cells["SRP"].Value) - Convert.ToDecimal(DataGridPart.Rows[rowIndex].Cells["Discount"].Value)) *
                             Convert.ToDecimal(currentCell.Value);
                    DataGridPart.Rows[rowIndex].Cells["Amount"].Value = total;
                    RecalculateTotal();
                    SavePart(rowIndex);
                }
                else
                {
                    MessageBox.Show("Quantity exceeds the current balance on hand.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    currentCell.Value = OldQty;
                }
            }
        }
        
        private void DataGridPart_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (DataGridPart.CurrentCell.ColumnIndex == DataGridPart.Columns["Qty"].Index)
            {
                e.Control.KeyPress += Numeric_KeyPress;
            }
        }

        private void Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }

            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        private void RecalculateTotal()
        {
            decimal total = 0, qty = 0;
            foreach (DataGridViewRow row in DataGridPart.Rows)
            {
                total = total + Convert.ToDecimal(row.Cells["Amount"].Value);
                qty = qty + Convert.ToDecimal(row.Cells["Qty"].Value);
            }
            TxtTotal.Textt = total.ToString("N2");
            TxtTotalQty.Textt = qty.ToString("N2");
        }

        private void SavePart(int rowIndex)
        {
            string reason = "";
            reason = DataGridPart.Rows[rowIndex].Cells["FreeReason"].Value?.ToString() ?? reason;
            SalesOrderDetailModel Detail = new SalesOrderDetailModel
            {
                SoNo = TxtSoNo.Textt,
                ItemNo = DataGridPart.Rows[rowIndex].Cells["ItemNo"].Value.ToString(),
                PartNo = DataGridPart.Rows[rowIndex].Cells["PartNo"].Value.ToString(),
                Qty = Convert.ToDecimal(DataGridPart.Rows[rowIndex].Cells["Qty"].Value),
                ListPrice = Convert.ToDouble(DataGridPart.Rows[rowIndex].Cells["SRP"].Value),
                Discount = Convert.ToDouble(DataGridPart.Rows[rowIndex].Cells["Discount"].Value),
                //SLID = slid,
                FreeItem = Convert.ToBoolean(DataGridPart.Rows[rowIndex].Cells["Free"].Value),
                FreeReason = reason,
                AllowBelCost = Convert.ToBoolean(DataGridPart.Rows[rowIndex].Cells["BelowCost"].Value),
                Status = 1,
                ItemID = DataGridPart.Rows[rowIndex].Cells["ItemID"].Value.ToString(),
            };
            _SalesOrderController.PickNew(Detail);
        }

        private void DataGridPart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridPart.Rows.Count > 0)
            {
                if (DataGridPart.CurrentCell.ColumnIndex == DataGridPart.Columns["Free"].Index && !Convert.ToBoolean(DataGridPart.CurrentCell.Value))
                {
                    frm_sales_order_free_check freeCheck = new frm_sales_order_free_check();
                    freeCheck.StringReason += ReceiveReasonFromChild;
                    freeCheck.ShowDialog(this);
                }
                else if (DataGridPart.CurrentCell.ColumnIndex == DataGridPart.Columns["Free"].Index && Convert.ToBoolean(DataGridPart.CurrentCell.Value))
                {
                    DataGridPart.CurrentRow.Cells["FreeReason"].Value = "";
                    DataGridPart.CurrentRow.Cells["Free"].Value = false;
                    SavePart(DataGridPart.CurrentRow.Index);
                }
                else if (DataGridPart.CurrentCell.ColumnIndex == DataGridPart.Columns["Discount"].Index)
                {
                    frm_sales_order_discount discount = new frm_sales_order_discount(Convert.ToDecimal(DataGridPart.CurrentRow.Cells["SRP"].Value), DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString());
                    discount.DiscountVal += ReceiveDiscountFromChild;
                    discount.ShowDialog(this);
                }
            }
        }

        private void ReceiveReasonFromChild(string stringReason)
        {
            if (stringReason != "")
            {
                DataGridPart.CurrentRow.Cells["FreeReason"].Value = stringReason;
                DataGridPart.CurrentRow.Cells["Free"].Value = true;
                _SalesOrderController.ResetPick(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString(), Convert.ToDecimal(DataGridPart.CurrentRow.Cells["Qty"].Value));
                SavePart(DataGridPart.CurrentRow.Index);
            }
        }

        private void ReceiveDiscountFromChild(decimal discount, bool belowcost)
        {
            if (discount != 0)
            {
                DataGridPart.CurrentRow.Cells["Discount"].Value = Convert.ToDouble(discount);
                DataGridPart.CurrentRow.Cells["BelowCost"].Value = belowcost;
                decimal total = (Convert.ToDecimal(DataGridPart.CurrentRow.Cells["SRP"].Value) - Convert.ToDecimal(DataGridPart.CurrentRow.Cells["Discount"].Value)) * 
                             Convert.ToDecimal(DataGridPart.CurrentRow.Cells["Qty"].Value);
                DataGridPart.CurrentRow.Cells["Amount"].Value = total;
                _SalesOrderController.ResetPick(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString(), Convert.ToDecimal(DataGridPart.CurrentRow.Cells["Qty"].Value));
                SavePart(DataGridPart.CurrentRow.Index);
                RecalculateTotal();
            }
            else
            {
                DataGridPart.CurrentRow.Cells["Discount"].Value = 0.00;
                DataGridPart.CurrentRow.Cells["BelowCost"].Value = false;
                decimal total = (Convert.ToDecimal(DataGridPart.CurrentRow.Cells["SRP"].Value) * Convert.ToDecimal(DataGridPart.CurrentRow.Cells["Qty"].Value));
                DataGridPart.CurrentRow.Cells["Amount"].Value = total;
                _SalesOrderController.ResetPick(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString(), Convert.ToDecimal(DataGridPart.CurrentRow.Cells["Qty"].Value));
                SavePart(DataGridPart.CurrentRow.Index);
                RecalculateTotal();
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {

        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }

        private void DataGridPart_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int SelectionIndex = DataGridPart.Columns["IsSelected"].Index;
            if (e.ColumnIndex == SelectionIndex)
            {
                bool AllSelected = false;
                var uncheckedRows = DataGridPart.Rows.Cast<DataGridViewRow>()
                                    .Where(row => !(bool)row.Cells["IsSelected"].Value);

                if (uncheckedRows.Any())
                {
                    AllSelected = true;
                }

                foreach (DataGridViewRow row in DataGridPart.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["IsSelected"] as DataGridViewCheckBoxCell;
                    if (!checkBoxCell.ReadOnly)
                    {
                        checkBoxCell.Value = AllSelected;
                    }
                }
                DataGridPart.EndEdit();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //if (TxtSoNo.Textt != "" && DataGridPart.Rows.Count > 0 &&
            //    Helper.Confirmator("This will process the created SO. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            //{
            //    List<SalesOrderDetailModel> DetailsList = new List<SalesOrderDetailModel>();
            //    foreach (DataGridViewRow row in DataGridPart.Rows)
            //    {
            //        //string slid = string.IsNullOrEmpty(ComboCustomer.Text) ? null : ComboCustomer.SelectedValue.ToString();
            //        string reason = "";
            //        reason = row.Cells["FreeReason"].Value?.ToString() ?? reason;
            //        SalesOrderDetailModel Detail = new SalesOrderDetailModel
            //        {
            //            ItemNo = row.Cells["ItemNo"].Value.ToString(),
            //            PartNo = row.Cells["PartNo"].Value.ToString(),
            //            Qty = Convert.ToDecimal(row.Cells["Qty"].Value),
            //            ListPrice = Convert.ToDouble(row.Cells["SRP"].Value),
            //            Discount = Convert.ToDouble(row.Cells["Discount"].Value),
            //            NetPrice = 0,
            //            //SLID = slid,
            //            VATAmt = 0,
            //            FreeItem = Convert.ToBoolean(row.Cells["Free"].Value),
            //            FreeReason = reason,
            //            AllowBelCost = false,
            //            Status = 2
            //        };
            //        DetailsList.Add(Detail);
            //    }

            //    _SalesOrderModel = new SalesOrderModel
            //    {
            //        SoNo = TxtSoNo.Textt,
            //        //InvoiceRefNo = TxtReference.Textt.TrimEnd(),
            //        Status = 2,
            //        DetailsList = DetailsList
            //    };
            //    string CustomMsg = _SalesOrderController.Update(_SalesOrderModel);
            //    MessageBox.Show(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (CustomMsg == "Information saved successfully" || CustomMsg == "Information updated successfully")
            //    {
            //        ClearEncode();
            //        //OwnerCompany = _TransactionController.GetOwnerCompany();
            //        //headerImage = getImage();
            //        //((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized;
            //        //printPreviewDialog1.ShowDialog();
            //    }
            //}
            //else if (TxtSoNo.Textt == "")
            //{
            //    MessageBox.Show("Please fill all the required fields before saving.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else if (DataGridPart.Rows.Count == 0)
            //{
            //    MessageBox.Show("Please make sure that there is a part selected for the current transaction.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
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
            Font subdetailBoldFont = new Font("Arial", 6, FontStyle.Bold);
            StringFormat rigthAlign = new StringFormat
            {
                Alignment = StringAlignment.Far,
                //LineAlignment = StringAlignment.Center
            };

            float x = 50;
            float y = 50;

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
            g.DrawString("SALES ORDER", subheaderFont, Brushes.Black, additionalTextX, additionalTextY, rigthAlign);
            g.DrawString("NO : " + TxtSoNo.Textt, subheaderFont, Brushes.Black, additionalTextX, additionalTextY + 15, rigthAlign);
            //g.DrawString("REF: " + TxtReference.Textt.TrimEnd(), subheaderFont, Brushes.Black, additionalTextX, additionalTextY + 30, rigthAlign);

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
            //switch (CheckCash.Checked)
            //{
            //    case true:
            //        if (TxtCustomer.Textt.TrimEnd().Length > 60)
            //        {
            //            g.DrawString(TxtCustomer.Textt.TrimEnd().Substring(0, 60), normalBoldFont, Brushes.Black, x + 15, y + 15);
            //            g.DrawString(TxtCustomer.Textt.TrimEnd().Substring(60), normalBoldFont, Brushes.Black, x + 15, y + 25);
            //        }
            //        else
            //        {
            //            g.DrawString(TxtCustomer.Textt.TrimEnd(), normalBoldFont, Brushes.Black, x + 15, y + 15);
            //        }
            //        break;

            //    case false:
            //        if (ComboCustomer.Text.Length > 60)
            //        {
            //            g.DrawString(ComboCustomer.Text.Substring(0, 60), normalBoldFont, Brushes.Black, x + 15, y + 15);
            //            g.DrawString(ComboCustomer.Text.Substring(60), normalBoldFont, Brushes.Black, x + 15, y + 25);
            //        }
            //        else
            //        {
            //            g.DrawString(ComboCustomer.Text, normalBoldFont, Brushes.Black, x + 15, y + 15);
            //        }
            //        break;
            //}
            
            //g.DrawString(TxtSoDateNot.Textt, normalBoldFont, Brushes.Black, x + 555, y + 15);

            //y += 55;
            //g.DrawString("ADDRESS:", normalFont, Brushes.Black, x + 10, y);
            //if (TxtAddress.Textt.TrimEnd().Length > 80)
            //{
            //    float addressy = y + 15;
            //    for (int i = 0; i < TxtAddress.Textt.TrimEnd().Length; i += 80)
            //    {
            //        if (TxtAddress.Textt.TrimEnd().Length - i > 80)
            //        {
            //            g.DrawString(TxtAddress.Textt.TrimEnd().Substring(i, 80), normalBoldFont, Brushes.Black, x + 15, addressy);
            //        }
            //        else
            //        {
            //            g.DrawString(TxtAddress.Textt.TrimEnd().Substring(i), normalBoldFont, Brushes.Black, x + 15, addressy);
            //        }
            //        addressy += 10;
            //    }
            //}
            //else
            //{
            //    g.DrawString(TxtAddress.Textt.TrimEnd(), normalBoldFont, Brushes.Black, x + 15, y + 15);
            //}

            //y += 55;
            //g.DrawString("TIN:", normalFont, Brushes.Black, x + 10, y);
            //g.DrawString(TxtTin.Textt, normalBoldFont, Brushes.Black, x + 15, y + 15);
            //g.DrawString("SALESMAN:", normalFont, Brushes.Black, x + 250, y);
            //if (ComboSalesman.Text.Length > 30)
            //{
            //    g.DrawString(ComboSalesman.Text.ToString().Substring(0, 30), normalBoldFont, Brushes.Black, x + 255, y + 15);
            //    g.DrawString(ComboSalesman.Text.ToString().Substring(30), normalBoldFont, Brushes.Black, x + 255, y + 25);
            //}
            //else
            //{
            //    g.DrawString(ComboSalesman.Text.ToString(), normalBoldFont, Brushes.Black, x + 255, y + 15);
            //}
            //g.DrawString("MODE OF PAYMENT:", normalFont, Brushes.Black, x + 550, y);
            //if (RadioCash.Checked)
            //{
            //    g.DrawString("CASH", normalBoldFont, Brushes.Black, x + 555, y + 15);
            //}
            //else
            //{
            //    g.DrawString(ComboTerm.Text, normalBoldFont, Brushes.Black, x + 555, y + 15);
            //}

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
            foreach (DataGridViewRow row in DataGridPart.Rows)
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
                g.DrawString(Convert.ToDecimal(row.Cells["SRP"].Value).ToString("N2"), subdetailFont, Brushes.Black, x + 560, partY, rigthAlign);
                g.DrawString(Convert.ToDecimal(row.Cells["Discount"].Value).ToString("N2"), subdetailFont, Brushes.Black, x + 635, partY, rigthAlign);
                g.DrawString(Convert.ToDecimal(row.Cells["Amount"].Value).ToString("N2"), subdetailFont, Brushes.Black, x + 705, partY, rigthAlign);
                qty += Convert.ToDecimal(row.Cells["Qty"].Value);
                srp += Convert.ToDecimal(row.Cells["SRP"].Value);
                discount += Convert.ToDecimal(row.Cells["Discount"].Value);
                total += Convert.ToDecimal(row.Cells["Amount"].Value);
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
            g.DrawString("Prepared By:" + Universal<SalesOrderModel>.Name01, normalFont, Brushes.Black, x, y + 10);
            g.DrawString("Date:" + DateTime.Now, normalFont, Brushes.Black, x, y + 25);
        }
        
        private void printPreviewDialog1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //ClearEncode();
            //ComboCustomer.Focus();
            //MessageBox.Show("Information saved successfully", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
