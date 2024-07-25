using CARS.Components.Transactions.Order_Taking;
using CARS.Controller.Transactions;
using CARS.Controllers.Masterfiles;
using CARS.Functions;
using CARS.Model.Masterfiles;
using CARS.Model.Transactions;
using CARS.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CARS.Components.Transactions
{
    public partial class frm_order_taking : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        poOrderTakingController _orderTakingController = new poOrderTakingController();
        PartsModel _partsModel = new PartsModel();
        OrderTakingDet orderTakingDet = new OrderTakingDet();
        private PoOrderTaking orderTaker = new PoOrderTaking();
        private SortedDictionary<string, string> _brandsDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _descsDictionary = new SortedDictionary<string, string>();
        private TextBox TxtPartsColumnSearch = new TextBox();
        private DataTable PartsTable = new DataTable();
        private TextBox TxtOrderColumnSearch = new TextBox();
        private DataTable OrdersTable = new DataTable();
        private DataTable OrderListTable = new DataTable();
        private string msgResult = "";
        private string ctrlNoRes = "";
        private bool isEditing = false;
        private TransactionController _TransactionController = new TransactionController();
        public frm_order_taking(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = PnlHeaderFilter.BackColor = PnlHeaderInformation.BackColor = PnlHeaderTable.BackColor = PnlSaveOrder.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblFilter.ForeColor = LblHeaderInformation.ForeColor = LblHeaderTable.ForeColor = LblSaveOrder.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            TxtOrderColumnSearch = Helper.ColoumnSearcher(dgvOrderList, 20, 300);
            TxtOrderColumnSearch.KeyUp += TxtOrderColumnSearch_KeyUp;
            TxtOrderColumnSearch.Leave += TxtOrderColumnSearch_Leave;
            PartsInformation();
            getSavedOrder();
            dgvSavedOrder.ClearSelection();
            dashboardCall = DashboardCall;
            //TxtOrderColumnSearch.Location = new System.Drawing.Point(100, 30);
            dgvOrderList.DataSource = OrderListTable;
            ComboFilter.SelectedIndex = 0;
        }


        private void DgvOrderList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dgvOrderList.Columns["Qty"].Index)
            {
                dgvOrderList.BeginEdit(true);
                dgvOrderList.EditingControl.Select();
            }
        }

        private void getSavedOrder()
        {
            dgvSavedOrder.DataSource = _orderTakingController.getSavedOrder();
            dgvSavedOrder.ClearSelection();
        }

        private void DgvOrderList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvOrderList.Columns[e.ColumnIndex].Name == "Qty")
            {
                string val = e.FormattedValue.ToString();

                if (!int.TryParse(val, out _))
                {
                    Helper.Confirmator("Please input a quantity", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        private void frm_po_generation_Load(object sender, EventArgs e)
        {
            dgvSavedOrder.ClearSelection();
        }

        private PartsModel getPartsModel(string partname) => _orderTakingController.getPartsInfo(partname);

        private void customRoundedButton2_Click(object sender, EventArgs e)
        {
            if (dgvOrderList.Rows.Count == 0)
            {
                Helper.Confirmator("No orders found, Please add an item/s.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var rowsToRemove = dgvOrderList.Rows
                                            .Cast<DataGridViewRow>()
                                            .Where(row => Convert.ToBoolean(row.Cells["CheckOrder"].Value))
                                            .ToList();

            if (rowsToRemove.Count == 0)
            {
                Helper.Confirmator("No selected item to delete.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Helper.Confirmator("Are you sure you want to delete this Part No.?", "System Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                foreach (var row in rowsToRemove)
                {
                    dgvOrderList.Rows.Remove(row);
                }
                Helper.Confirmator("Removed successfully!", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Save Order List Result (2)
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvOrderList.Rows.Count != 0)
            {
                List<OrderTakingDet> orderList = new List<OrderTakingDet>();
                bool hasEmptyQty = false;

                foreach (DataGridViewRow row in dgvOrderList.Rows)
                {
                    //decimal qty = Convert.ToDecimal(row.Cells["Qty"].Value);
                    if (row.Cells["Qty"].Value == null || string.IsNullOrWhiteSpace(row.Cells["Qty"].Value.ToString()) || Convert.ToDecimal(row.Cells["Qty"].Value) == 0)
                    {
                        hasEmptyQty = true;
                        break;
                    }
                }
                if (!hasEmptyQty)
                {
                    foreach (DataGridViewRow row in dgvOrderList.Rows) {
                        OrderTakingDet orderLists = new OrderTakingDet
                        {
                            PartNo = row.Cells["PartNoOrder"].Value.ToString(),
                            Qty = Convert.ToDecimal(row.Cells["Qty"].Value),
                            ItmRemarks = "",
                            PONo = "",
                            Status = 1
                        };
                        orderList.Add(orderLists);
                        orderTaker = new PoOrderTaking 
                        { 
                            Remarks = "Save",
                            Status = 1,
                            Orderlist = orderList 
                        };

                    }
                    SortedDictionary<string, string> keyValuePairs = _orderTakingController.saveOrder(orderTaker,ctrlNoRes);
                    KeyValuePair<string, string> msg = keyValuePairs.First();
                    KeyValuePair<string, string> ctrlNo = keyValuePairs.Last();
                    msgResult = msg.Key;
                    ctrlNoRes = ctrlNo.Value;
                    
                    Helper.Confirmator(msgResult, "System Information",MessageBoxButtons.OK,MessageBoxIcon.Information).ToString();
                    if(msgResult == "Order saved successfully")
                    {
                        //dgvOrderList.Rows.Clear();
                        dgvSavedOrder.DataSource = _orderTakingController.getSavedOrder();
                        dgvSavedOrder.Refresh();
                    }
                }
                else
                {
                    Helper.Confirmator("The quantity is empty please check your items.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Helper.Confirmator("Please add items.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(ctrlNoRes))
            {
                if (dgvOrderList.Rows.Count != 0)
                {
                    List<OrderTakingDet> orderList = new List<OrderTakingDet>();
                    bool hasEmptyQty = false;

                    foreach (DataGridViewRow row in dgvOrderList.Rows)
                    {
                        if (row.Cells["Qty"].Value == null || string.IsNullOrWhiteSpace(row.Cells["Qty"].Value.ToString()) || Convert.ToDecimal(row.Cells["Qty"].Value) == 0)
                        {
                            hasEmptyQty = true;
                            break;
                        }

                    }
                    if (!hasEmptyQty)
                    {
                        foreach (DataGridViewRow row in dgvOrderList.Rows)
                        {
                            OrderTakingDet orderLists = new OrderTakingDet
                            {
                                PartNo = row.Cells["PartNoOrder"].Value.ToString(),
                                Qty = Convert.ToDecimal(row.Cells["Qty"].Value.ToString()),
                                ItmRemarks = "",
                                PONo = "",
                                Status = 2
                            };
                            orderList.Add(orderLists);
                            orderTaker = new PoOrderTaking
                            {
                                Remarks = "Post",
                                Status = 2,
                                Orderlist = orderList
                            };

                        }
                        string msgResultt  = _orderTakingController.PostOrderTaking(ctrlNoRes.ToString(),orderTaker);
                        Helper.Confirmator(msgResultt, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information).ToString();
                        if (msgResultt == "Posted Successfully")
                        {
                            dgvOrderList.Rows.Clear();
                            txtBPartno.Textt ="";
                            txtIUpack.Textt = "";
                            txtListprice.Textt = "";
                            txtMUpack.Textt = "";
                            txtOem.Textt = "";
                            txtOthername.Textt = "";
                            txtPartapply.Textt = "";
                            txtPartName.Textt = "";
                            txtPosition.Textt = "";
                            txtSize.Textt = "";
                            txtSku.Textt = "";
                            txtType.Textt = "";
                            txtUom.Textt = "";
                            dgvSavedOrder.DataSource = _orderTakingController.getSavedOrder();
                            dgvSavedOrder.Refresh();
                            dgvSavedOrder.ClearSelection();
                            isEditing = false;
                            ctrlNoRes = "";
                        }
                    }
                    else
                    {
                        Helper.Confirmator("The quantity is empty please check your items.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    Helper.Confirmator("Please add items.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Helper.Confirmator("Please select order to be posted", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void customRoundedButton1_Click_1(object sender, EventArgs e)
        {
            if(txtPartName.Textt != "")
            {
                if(imgLnk.Text != "")
                {
                    frm_parts_image_view partsImage = new frm_parts_image_view(txtPartName.Textt);
                    partsImage.ShowDialog(this);
                }
                else
                {
                    Helper.Confirmator("This item has no Image.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Helper.Confirmator("No selected data", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
        private void TxtPartsColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtPartsColumnSearch.Visible = false;
        }

        int currentOrderCol = 1;
        private void dgvOrderList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int SelectionIndex = dgvOrderList.Columns["CheckOrder"].Index;
            if(e.ColumnIndex == SelectionIndex)
            {
                bool AllSelected = false;
                var uncheckedRows = dgvOrderList.Rows.Cast<DataGridViewRow>().
                    Where(row => !Convert.ToBoolean(row.Cells["CheckOrder"].Value));

                if (uncheckedRows.Any())
                {
                    AllSelected = true;
                }

                foreach(DataGridViewRow row in dgvOrderList.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["CheckOrder"] as DataGridViewCheckBoxCell;
                    if (!checkBoxCell.ReadOnly)
                    {
                        checkBoxCell.Value = AllSelected;
                    }
                }
                dgvOrderList.EndEdit();
            }
        }

        private void TxtOrderColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtOrderColumnSearch.Visible = false;
        }

        private void TxtOrderColumnSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                TxtOrderColumnSearch.Visible = false;
            }
            else
            {
                string searchCol = dgvOrderList.Columns[currentOrderCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtOrderColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = OrdersTable;
                bs.Filter = $"[{searchCol}] LIKE '%{valueSearch}%'";
                dgvOrderList.DataSource = bs;
                dgvOrderList.ClearSelection();
            }
        }

        private void dgvOrderList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(dgvOrderListCell_KeyPress);
            if (dgvOrderList.CurrentCell.ColumnIndex == dgvOrderList.Columns["Qty"].Index)
            {
                e.Control.KeyPress += new KeyPressEventHandler(dgvOrderListCell_KeyPress);
                e.Control.KeyPress += Helper.Numeric_KeyPress;
            }
        }

        private void dgvOrderListCell_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<string> PartData = new List<string>();
            foreach (DataGridViewRow row in dgvOrderList.Rows)
            {
                if (row.Cells[dgvOrderList.Columns["PartNoOrder"].Index] != null &&
                row.Cells[dgvOrderList.Columns["PartNoOrder"].Index].Value != null)
                {
                    PartData.Add(row.Cells[dgvOrderList.Columns["PartNoOrder"].Index].Value.ToString());
                }

            }
            frm_order_taking_add_parts frm_ot_addingparts = new frm_order_taking_add_parts(PartData.ToList());
            frm_ot_addingparts.StringArraySent += ReceiveArrayFromChild;
            frm_ot_addingparts.ShowDialog(this);
        }
        private void ReceiveArrayFromChild(List<dynamic[]> stringArray)
        {
            foreach (var item in stringArray)
            {
                string partName = item[2].ToString();
                string partNo = item[1].ToString();
                if (!PartAlreadyExists(partNo))
                {
                    OrderListTable.Rows.Add(item);
                }
                else
                {
                    Helper.Confirmator($"Part {partName} has already been added.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void dgvOrderList_CellEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            var partName = dgvOrderList.CurrentRow.Cells["PartNameOrder"].Value?.ToString().Trim();
            this._partsModel = this.getPartsModel(partName);

            if(this._partsModel != null)
            {
                txtPartNo.Textt = this._partsModel.PartNo != ""? this._partsModel.PartNo : "N/A";
                txtPartName.Textt = this._partsModel.PartName != "" ? this._partsModel.PartName : "N/A";
                txtSku.Textt = this._partsModel.Sku != "" ? this._partsModel.Sku : "N/A";
                txtOthername.Textt = this._partsModel.OtherName != "" ? this._partsModel.OtherName : "N/A";
                txtPartapply.Textt = this._partsModel.PartApplication != "" ? this._partsModel.PartApplication : "N/A";
                txtUom.Textt = this._partsModel.Uom != "" ? this._partsModel.Uom : "N/A";
                txtOem.Textt = this._partsModel.Oem != "" ? this._partsModel.Oem : "N/A";
                txtBPartno.Textt = this._partsModel.BPartNo != "" ? this._partsModel.BPartNo : "N/A";
                txtListprice.Textt = this._partsModel.ListPrice.ToString();
                txtPosition.Textt = this._partsModel.PPosition != "" ? this._partsModel.PPosition : "N/A";
                txtSize.Textt = this._partsModel.PSize != "" ? this._partsModel.PSize : "N/A";
                txtType.Textt = this._partsModel.Ptype != "" ? this._partsModel.Ptype : "N/A";
                txtIUpack.Textt = this._partsModel.IUpack.ToString();
                txtMUpack.Textt = this._partsModel.MUpack.ToString();
            }
            else
            {
                txtPartNo.Text = "N/A";
                txtPartName.Text = "N/A";
                txtSku.Text = "N/A";
                txtOthername.Text = "N/A";
                txtPartapply.Text = "N/A";
                txtUom.Text = "N/A";
                txtOem.Text = "N/A";
                txtBPartno.Text = "N/A";
                txtListprice.Text = "N/A";
                txtPosition.Text = "N/A";
                txtSize.Text = "N/A";
                txtType.Text = "N/A";
                txtIUpack.Text = "N/A";
                txtMUpack.Text = "N/A";
            }
            //this._partsModel = this.getPartsModel(dgvOrderList.CurrentRow.Cells["PartNameOrder"].Value.ToString().Trim()) != null ? this.getPartsModel(dgvOrderList.CurrentRow.Cells["PartNameOrder"].Value.ToString().Trim()) :  ;
        }

        private void PartsInformation()
        {
            if(txtBPartno.ReadOnly && txtIUpack.ReadOnly && txtListprice.ReadOnly && txtMUpack.ReadOnly && txtOem.ReadOnly && 
                txtOthername.ReadOnly && txtPartapply.ReadOnly && txtPartName.ReadOnly && txtPosition.ReadOnly && txtSize.ReadOnly && txtSku.ReadOnly && txtType.ReadOnly &&
                txtUom.ReadOnly == true)
            {
                txtPartNo.BackColor = Color.White;
                txtBPartno.BackColor = Color.White;
                txtIUpack.BackColor = Color.White;
                txtListprice.BackColor = Color.White;
                txtMUpack.BackColor = Color.White;
                txtOem.BackColor = Color.White;
                txtOthername.BackColor = Color.White;
                txtPartapply.BackColor = Color.White;
                txtPartName.BackColor = Color.White;
                txtPosition.BackColor = Color.White;
                txtSize.BackColor = Color.White;
                txtSku.BackColor = Color.White;
                txtType.BackColor = Color.White;
                txtUom.BackColor = Color.White;

                txtPartNo.ForeColor = Color.Black;
                txtBPartno.ForeColor = Color.Black;
                txtIUpack.ForeColor = Color.Black;
                txtListprice.ForeColor = Color.Black;
                txtMUpack.ForeColor = Color.Black;
                txtOem.ForeColor = Color.Black;
                txtOthername.ForeColor = Color.Black;
                txtPartapply.ForeColor = Color.Black;
                txtPartName.ForeColor = Color.Black;
                txtPosition.ForeColor = Color.Black;
                txtSize.ForeColor = Color.Black;
                txtSku.ForeColor = Color.Black;
                txtType.ForeColor = Color.Black;
                txtUom.ForeColor = Color.Black;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will clear the current transaction. Proceed?", "System Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                OrdersTable.Rows.Clear();
                txtPartNo.Textt = "";
                txtBPartno.Textt = "";
                txtIUpack.Textt = "";
                txtListprice.Textt = "";
                txtMUpack.Textt = "";
                txtOem.Textt = "";
                txtOthername.Textt = "";
                txtPartapply.Textt = "";
                txtPartName.Textt = "";
                txtPosition.Textt = "";
                txtSize.Textt = "";
                txtSku.Textt = "";
                txtType.Textt = "";
                txtUom.Textt = "";
                dgvSavedOrder.ClearSelection();
                isEditing = false;
                ctrlNoRes = "";
            }
        }

        private bool PartAlreadyExists(string partNo)
        {
            return dgvOrderList.Rows
            .Cast<DataGridViewRow>()
            .Any(row => row.Cells["PartNoOrder"].Value?.ToString() == partNo);
        }

        private void dgvSavedOrder_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(!isEditing && dgvOrderList.Rows.Count > 0)
            {
                if(Helper.Confirmator("This will replace order list. Proceed?", "System Confirmator", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    string ctrlNo = dgvSavedOrder.Rows[e.RowIndex].Cells["CtrlNo"].Value.ToString().Trim();
                        dgvOrderList.Rows.Clear();

                    AddSavedOrder(ctrlNo);
                    ctrlNoRes = ctrlNo;
                    isEditing = true;
                }
            }
            else if (e.ColumnIndex == dgvSavedOrder.Columns["CtrlNo"].Index)
            {
                string ctrlNo = dgvSavedOrder.Rows[e.RowIndex].Cells["CtrlNo"].Value.ToString().Trim();

                if (dgvOrderList.Rows.Count > 0 && Helper.Confirmator("This will replace order list. Proceed?", "System Confirmator", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    dgvOrderList.Rows.Clear();
                }

                AddSavedOrder(ctrlNo);
                ctrlNoRes = ctrlNo;
                isEditing = true;
            }
        }

        private void AddSavedOrder(string ctrlNo)
        {
            DataTable addedOrder = _orderTakingController.addSavedOrder(ctrlNo);

            foreach (DataRow row in addedOrder.Rows)
            {
                string partNo = row["PartNo"].ToString();

                if (!PartAlreadyExists(partNo))
                {
                    dgvOrderList.Rows.Add(row.ItemArray);
                }
                else
                {
                    MessageBox.Show($"Part with PartNo {partNo} has already been added.", "Duplicate Part", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            frm_order_taking_excel_upload frm_uploadExcelFile = new frm_order_taking_excel_upload(UpdateOrderList);
            frm_uploadExcelFile.Show(this);
        }

        public void UpdateOrderList(DataTable newDataTable)
        {
            DataTable currentTable = dgvOrderList.DataSource as DataTable ?? new DataTable();
            DataTable masterTable = _orderTakingController.getPartno();
            //DataTable
            DataTable mergerTable = _orderTakingController.fetchData(newDataTable);
            OrderListTable.Merge(mergerTable);
            if (mergerTable !=null)
            {
                Helper.Confirmator("Uploaded Successfully", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Helper.Confirmator("Uploaded Unsuccessful", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ComboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                _TransactionController.ChangeDistinctFilter(comboBox.SelectedItem.ToString());
            }
        }

        private void Combo_Order_Taking_Parts_ItemSelected(object sender, Customized_Components.ItemSelectedEventArgs e)
        {
            DataTable PartTable = new DataTable();
            OrdersTable.Columns.Add("CheckOrder");
            OrdersTable.Columns.Add("PartNoParts");
            OrdersTable.Columns.Add("PartName");
            OrdersTable.Columns.Add("BrandName");
            OrdersTable.Columns.Add("DescName");
            OrdersTable.Columns.Add("Qty");
            //OrdersTable.Columns.Add("Status");
            var boolColumn = new DataColumn("ForSelection", typeof(bool));
            switch (ComboFilter.SelectedItem.ToString())
            {
                case "ALL":
                    if (e.SelectedItem != "")
                    {
                        _partsModel = _orderTakingController.suggestedParts(e.SelectedItem);
                        //var boolColumn = new DataColumn("ForSelection", typeof(bool));
                        DataGridViewRow row = dgvOrderList.Rows
                                              .Cast<DataGridViewRow>()
                                              .FirstOrDefault(x => x.Cells["PartNo"].Value.ToString() == orderTakingDet.PartNo);
                        if (row == null)
                        {
                            OrdersTable.Rows.Add(false, _partsModel.PartNo, _partsModel.PartName,
                                                    _partsModel.Brand, _partsModel.Description, 1);
                            //TxtTotalRow.Textt = DataGridPart.Rows.Count.ToString();
                            ComboSalesParts.Text = "";
                            dgvOrderList.DataSource = OrdersTable;
                        }
                        else
                        {
                            MessageBox.Show("The selected part is already included in the parts list.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        PartTable = _orderTakingController.getPartLists(_partsModel,ComboSalesParts.Text.TrimEnd());
                        boolColumn.DefaultValue = false;
                        PartTable.Columns.Add(boolColumn);
                        DataGridPartFilter.DataSource = PartTable;
                        DataGridPartFilter.ClearSelection();
                    }
                    break;

                case "BRAND":
                    PartTable = _orderTakingController.getPartLists(_partsModel, ComboSalesParts.Text.TrimEnd());
                    boolColumn.DefaultValue = false;
                    PartTable.Columns.Add(boolColumn);
                    DataGridPartFilter.DataSource = PartTable;
                    DataGridPartFilter.ClearSelection();
                    break;

                case "DESCRIPTION":
                    PartTable = _orderTakingController.getPartLists(_partsModel, ComboSalesParts.Text.TrimEnd());
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
            if(e.ColumnIndex == DataGridPartFilter.Columns["ForSelection"].Index)
            {
                bool isChecked = (bool)DataGridPartFilter.Rows[e.RowIndex].Cells["ForSelection"].Value;
                if (isChecked)
                {
                    DataGridViewRow row = dgvOrderList.Rows
                        .Cast<DataGridViewRow>().FirstOrDefault(x => x.Cells["PartNoParts"].Value.ToString() == DataGridPartFilter.CurrentRow.Cells["PartNoFilter"].Value.ToString());
                    if(row == null)
                    {
                        DataGridViewRow selectedRow = DataGridPartFilter.CurrentRow;
                        OrdersTable.Rows.Add(false, selectedRow.Cells["PartNoParts"].Value.ToString(), selectedRow.Cells["PartNameFilter"].Value.ToString(),
                                                    selectedRow.Cells["BrandNameFilter"].Value.ToString(), selectedRow.Cells["DescNameFilter"].Value.ToString(), 1);
                        dgvOrderList.DataSource = OrdersTable;
                    }
                }
            }
        }
    }
}
