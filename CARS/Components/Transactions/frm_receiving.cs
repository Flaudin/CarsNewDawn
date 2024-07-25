using CARS.Components.Transactions.PORecieving;
using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model.Transactions;
using CARS.Model.Utilities;

//using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using Rectangle = System.Drawing.Rectangle;

namespace CARS.Components.Transactions
{
    public partial class frm_receiving : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private System.Data.DataTable ReceivingItem;
        ReceivingController receivingController;
        private System.Data.DataTable LocationTable;
        private System.Data.DataTable POTable;
        private SortedDictionary<string, string> _supplierDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _termsDictionary = new SortedDictionary<string, string>();
        private Dictionary<string, string> selectedLocation = new Dictionary<string, string>();
        private Dictionary<string, HashSet<string>> supplierReceiver = new Dictionary<string, HashSet<string>>();
        private List<LocationInfo> locationList = new List<LocationInfo>();
        public List<RecItemLoc> temporaryLoc = new List<RecItemLoc>();
        private DataRow row;
        ReceivingModel receivingModel;
        //private ReceiveReportModels ownerCompany = new ReceiveReportModels();
        private ReceivePrintModel.ReceiveReportOrder orderReceive = new ReceivePrintModel.ReceiveReportOrder();
        //ReceiveReportControllers ReceiveReportController;
        //ReceiveReportControllers receivingReport = new ReceiveReportControllers();
        private static ReceivingMF receivingMFP;
        ReceivingDet receivingDet;
        RecItemLoc recItemLoc;
        POMonitoring poMonitoringController = new POMonitoring();
        private string pono = "";
        private string headerText = "";
        private Image headerImage;
        private string subhead = "";
        private string msgResult = "";
        private string rrNo = "";
        private string selectedSupp = "";
        private string selectedTerm = "";
        int holder = 0;
        private DataTable aggregatedTable = new DataTable();
        public bool isRushOrder = false;
        public bool isEditingRR = false;
        public frm_receiving(Action DashboardCall)
        {
            InitializeComponent();
            ReceivingItem = new System.Data.DataTable();
            receivingController = new ReceivingController();
            LocationTable = new System.Data.DataTable();
            ReceivingItem.Columns.Add("WhIDd", typeof(string));
            POTable = new System.Data.DataTable();
            dgvLocation.ReadOnly = false;
            dgvReceivingItems.EditingControlShowing += DataGridView_EditingControlShowing;
            DataRow row;
            txtRRN.ReadOnly = true;
            dgvReceivingItems.Columns["TotalPrice"].DefaultCellStyle.Format = "C2";
            dgvReceivingItems.Columns["TotalPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvReceivingItems.Columns["NetPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvReceivingItems.Columns["NetPrice"].DefaultCellStyle.Format = "C2";
            dgvReceivingItems.Columns["QuantityRec"].DefaultCellStyle.Format = "N0";
            dgvReceivingItems.CellValueChanged += dgvReceivingItems_CellValueChanged;
            dgvReceivingItems.CellEndEdit += dgvReceivingItems_CellEndEdit;
            dgvReceivingItems.CellValidating += dgvReceivingItems_CellValidating;
            dgvReceivingItems.EditingControlShowing += dgvReceivingItems_EditingControlShowing;
            _supplierDictionary = receivingController.getSupplier(false);
            cmbSupplier.DataSource = new BindingSource(_supplierDictionary, null);
            cmbSupplier.DisplayMember = "Value";
            cmbSupplier.ValueMember = "Key";
            var boolCol = new DataColumn("ChckLoc", typeof(bool));
            boolCol.DefaultValue = false;
            LocationTable.Columns.Add(boolCol); 
            var txtQty = new DataColumn("Qty", typeof(decimal));
            txtQty.DefaultValue = 0;
            LocationTable.Columns.Add(txtQty);
            this.KeyPreview = true;
            headerImage = getImage(); //Properties.Resources.gs_logo_v;
            headerText = poMonitoringController.getCompanyName().ToString().TrimEnd();
            subhead = poMonitoringController.getSubheader().ToString().TrimEnd();
            printDocument.PrintPage += printDocument_PrintPage;
            printPreviewDialog.Document = printDocument;
            printDocument.DefaultPageSettings.Landscape = true;
            InitializeAggregatedTable();
            dgvLocation.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLocation.MultiSelect = false;
            dgvReceivingItems.MultiSelect = false;
            dgvPO.CellValueChanged += new DataGridViewCellEventHandler(dgvPO_CellValueChanged);
            dgvPO.CurrentCellDirtyStateChanged += new EventHandler(dgvPO_CurrentCellDirtyStateChanged);
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderLocation.BackColor = PnlHeader.BackColor = PnlOrderList.BackColor = PnlItemsReceive.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblOrderList.ForeColor = LblItemsReceive.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            dashboardCall = DashboardCall;
            //ReceiveReportControllers receivingReport = new ReceiveReportControllers();
        }


        private void DataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvReceivingItems.CurrentCell.ColumnIndex == dgvReceivingItems.Columns["TtlRecQuantity"].Index &&
                e.Control is TextBox textBox)
            {
                textBox.KeyPress -= TextBoxNumeric_KeyPress;
                textBox.KeyPress += TextBoxNumeric_KeyPress;
            }
        }


        private void TextBoxNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is a digit or a control key (like backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press
            }
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

        private void dgvReceivingItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvReceivingItems.CurrentCell.ColumnIndex == dgvReceivingItems.Columns["QuantityRec"].Index ||
                dgvReceivingItems.CurrentCell.ColumnIndex == dgvReceivingItems.Columns["Discount"].Index)
            {
                if (e.Control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.KeyDown -= TextBox_KeyDown;
                    textBox.KeyDown += TextBox_KeyDown;
                }
            }
        }

        private void dgvReceivingItems_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var qty = dgvReceivingItems.Columns["QuantityRec"].Index;
            if (e.ColumnIndex == qty)
            {
                if (!decimal.TryParse(e.FormattedValue.ToString(), out _))
                {
                    MessageBox.Show("Please enter a valid numeric value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        private void dgvReceivingItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvReceivingItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvReceivingItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvReceivingItems.Columns["NetPrice"].Index || e.ColumnIndex == dgvReceivingItems.Columns["TotalPrice"].Index)
            {
                //CalculateTotalPrice(e.RowIndex);
            }
        }



        

        private void customRoundedButton4_Click(object sender, System.EventArgs e)
        {
            if (chkRushP.Checked)
            {
                frm_po_receiving_parts_selection frm_Po_Receiving_Parts = new frm_po_receiving_parts_selection(this);
                frm_Po_Receiving_Parts.ShowDialog();
            }
            else
            {
                Helper.Confirmator("Add item is for Rush Purchase only.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        public void ReceivedData(string data)
        {
            if (chkRushP.Checked)
            {
                var selectedItems = receivingController.ReceivingParts(data);
                foreach (DataRow row in selectedItems.Rows)
                {
                    string partNo = row["PartNo"].ToString();
                    bool isExist = aggregatedTable.AsEnumerable().Any(ex => ex.Field<string>("PartNo").Trim() == partNo.Trim());
                    if (!isExist)
                    {
                        AddPOItems(selectedItems);
                        dgvReceivingItems.DataSource = aggregatedTable;
                        dgvReceivingItems.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("PartNo already exists", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                /*System.Data.DataTable newData = receivingController.ReceivingParts(data);
                if (ReceivingItem == null)
                {
                    ReceivingItem = new System.Data.DataTable();
                    ReceivingItem = receivingController.ReceivingParts(data);
                }
                ReceivingItem.Merge(newData);
                dgvReceivingItems.DataSource = ReceivingItem;
                dgvReceivingItems.ClearSelection();*/

                /*int i = 1;
                foreach (DataGridViewRow row in dgvReceivingItems.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        int currentColumn = dgvReceivingItems.Columns["ID"].Index;
                        row.Cells[currentColumn].Value = i;
                        i++;
                    }
                }*/
                //DataRow row = ReceivingItem.Rows[0];
            }





        }

        private void customRoundedButton1_Click(object sender, System.EventArgs e)
        {
            if (dgvReceivingItems.Rows.Count <= 0)
            {
                Helper.Confirmator("Please select an item to delete", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dgvReceivingItems.CurrentRow.Cells["PONoo"].Value == null)
            {
                if (MessageBox.Show("Are you sure you want to delete this item?", "System Confimation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    foreach (DataGridViewRow row in dgvReceivingItems.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            DataRowView rowView = row.DataBoundItem as DataRowView;
                            if (rowView != null)
                            {
                                temporaryLoc.RemoveAll(x => x.PartNo.Trim() == dgvReceivingItems.CurrentRow.Cells["PartNo"].Value.ToString().Trim());
                                rowView.Row.Delete();
                            }
                        }
                    }
                    dgvReceivingItems.Refresh();
                }

            }
            else
            {
                if (MessageBox.Show("Are you sure you want to delete this item?", "System Confimation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    foreach (DataGridViewRow row in dgvReceivingItems.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            DataRowView rowView = row.DataBoundItem as DataRowView;
                            if (rowView != null)
                            {
                                temporaryLoc.RemoveAll(x => x.PartNo.Trim() == dgvReceivingItems.CurrentRow.Cells["PartNo"].Value.ToString().Trim());
                                rowView.Row.Delete();
                            }
                        }
                    }
                    ReceivingItem.Clear();
                    dgvReceivingItems.Refresh();
                    foreach (DataGridViewRow row in dgvPO.Rows)
                    {
                        if (Convert.ToBoolean(dgvPO.CurrentRow.Cells["ChckPO"].Value))
                        {
                            dgvPO.CurrentRow.Cells["ChckPO"].Value = false;
                            chkRushP.Enabled = true;
                        }
                    }
                }
            }
            LocationTable.Clear();
            dgvLocation.Refresh();
        }

        private void customRoundedButton3_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear?", "System Confimation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _termsDictionary.Clear();
                cmbTerms.DataSource = null;
                cmbSupplier.SelectedIndex = 0;
                cmbTerms.Text = "";
                ClearAll();
                EnableTextBoxes(true);
                chkRushP.Enabled = true;
                isRushOrder = false; 
                isEditingRR = false;
            }
        }

        private void LocationSelection(int rowIndex)
        {
            if (dgvReceivingItems.Rows.Count <= 0)
            {
                Helper.Confirmator("Please add items first.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (dgvReceivingItems.Rows.Count > 0)
            {
                // Ensure a cell is selected
                if (dgvReceivingItems.CurrentRow == null)
                {
                    // Select the first row if no current row is selected
                    dgvReceivingItems.Rows[0].Selected = true;
                }
            }

            if (dgvReceivingItems.CurrentRow != null)
            {
                if (dgvReceivingItems.CurrentRow != null && dgvReceivingItems.CurrentRow.Cells["PartNo"] != null && dgvReceivingItems.CurrentRow.Cells["PartNo"].Value != null)
                {
                    if (LocationTable.Rows.Count > 0)
                    {
                        LocationTable.Rows.Clear();
                    }
                    //string poNo = dgvPO.Rows[rowIndex].Cells["PONNo"].Value.ToString().Trim();
                    string partNo = dgvReceivingItems.Rows[rowIndex].Cells["PartNo"].Value.ToString().Trim();
                    //if may temploc na
                    List<RecItemLoc> checktemploc = temporaryLoc.Where(temploc => temploc.PartNo.Trim() == partNo.Trim()).ToList();
                    if (checktemploc.Any())
                    {
                        foreach (var item in checktemploc)
                        {
                            DataRow newrow = LocationTable.NewRow();
                            newrow["BinName"] = item.BinName;
                            newrow["WhName"] = item.WhName;
                            newrow["LotNo"] = "";
                            newrow["BinID"] = item.BinID;
                            newrow["WhID"] = item.WhID;
                            newrow["Qty"] = item.Qty;
                            newrow["ChckLoc"] = item.Qty > 0;
                            // Set other column values as needed
                            LocationTable.Rows.Add(newrow);
                        }
                    }
                    else
                    {
                        //LocationTable = txtRRN.Textt == "NEW RECORD" ? receivingController.LocationDisplay(partNo) : receivingController.LocationDisplayWRR(partNo, txtRRN.Textt.ToString().TrimEnd(),1);
                        LocationTable = receivingController.LocationDisplay(partNo);
                        var boolCol = new DataColumn("ChckLoc", typeof(bool));
                        boolCol.DefaultValue = false;
                        LocationTable.Columns.Add(boolCol);
                        foreach (DataRow row in LocationTable.Rows)
                        {
                            bool isExist = temporaryLoc.Where(ex => ex.BinID == row["BinID"].ToString().Trim() && ex.PartNo.Trim() == partNo.Trim()).Any();
                            if (!isExist)
                            {
                                RecItemLoc item = new RecItemLoc
                                {
                                    PartNo = partNo.Trim(),
                                    LotNo = row["LotNo"].ToString(),
                                    Qty = Convert.ToDecimal(row["Qty"] ?? "0.00"),
                                    WhID = row["WhID"].ToString(),
                                    BinID = row["BinID"].ToString().Trim(),
                                    UniqueID = Helper.GenerateUID(),
                                    WhName = row["WhName"].ToString(),
                                    BinName = row["BinName"].ToString(),
                                    PONo = pono,
                                    ischecked = Convert.ToDecimal(row["Qty"] ?? "0.00") > 0,
                                };
                                temporaryLoc.Add(item);
                            }
                        }
                    }


                    if (txtRRN.Textt == "NEW RECORD")
                    {
                        /*var txtQty = new DataColumn("Qty", typeof(decimal));
                        txtQty.DefaultValue = 0;
                        LocationTable.Columns.Add(txtQty);*/
                    }
                    dgvLocation.DataSource = LocationTable;
                }
            }
            else
            {
                /*string poNo = dgvPO.Rows[rowIndex].Cells["PONNo"].Value.ToString().Trim();
                string partNo = dgvReceivingItems.CurrentRow.Cells["PartNo"].Value.ToString().Trim();
                LocationTable = receivingController.LocationDisplay(poNo);
                var boolCol = new DataColumn("ChckLoc", typeof(bool));
                boolCol.DefaultValue = false;
                LocationTable.Columns.Add(boolCol);
                var txtQty = new DataColumn("Qty", typeof(decimal));
                txtQty.DefaultValue = 0;
                LocationTable.Columns.Add(txtQty);
                //var PoNoLocc = new DataColumn("PONoLoc", typeof(string));
                //PoNoLocc.DefaultValue = poNo;
                // LocationTable.Columns.Add(PoNoLocc);
                dgvLocation.DataSource = LocationTable;
                //pono = poNo;*/
                return;
            }

            if(LocationTable.Rows.Count > 0)
            {
                LocationTable.Columns["Qty"].ReadOnly = false;
                dgvLocation.Columns["Qty"].ReadOnly = false;
                dgvLocation.ReadOnly = false;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsControl((char)e.KeyCode) && !char.IsDigit((char)e.KeyCode) && (e.KeyCode != Keys.Decimal) && (e.KeyCode != Keys.OemPeriod))
            {
                e.SuppressKeyPress = true;
            }
        }

        /*private void CalculateTotalPrice(int rowIndex)
        {
            //var qty = dgvReceivingItems.Rows[rowIndex].Cells["QuantityRec"].Value;
            var qty = dgvReceivingItems.Rows[rowIndex].Cells["TtlRecQuantity"].Value;
            var netPrice = dgvReceivingItems.Rows[rowIndex].Cells["NetPrice"].Value;
            var promo = dgvReceivingItems.Rows[rowIndex].Cells["Discount"].Value;

            if (netPrice != null && promo != null)
            {
                decimal quantity = Convert.ToDecimal(qty);
                decimal netP = Convert.ToDecimal(netPrice);
                decimal discount = Convert.ToDecimal(promo);
                decimal totalNetPrice = discount * netP;
                decimal totalPrice = totalNetPrice * quantity;

                dgvReceivingItems.Rows[rowIndex].Cells["NetPrice"].Value = totalNetPrice;
                dgvReceivingItems.Rows[rowIndex].Cells["TotalPrice"].Value = totalPrice;
            }
        }*/


        //Medyo bagong code
        private void InitializeAggregatedTable()
        {
            aggregatedTable.Columns.Add("SKU", typeof(string));
            aggregatedTable.Columns.Add("PartNo", typeof(string));
            aggregatedTable.Columns.Add("DescName", typeof(string));
            aggregatedTable.Columns.Add("BrandName", typeof(string));
            aggregatedTable.Columns.Add("UomID", typeof(string));
            aggregatedTable.Columns.Add("Discount", typeof(decimal));
            aggregatedTable.Columns.Add("NetPrice", typeof(decimal));
            aggregatedTable.Columns.Add("Qty", typeof(decimal));
            aggregatedTable.Columns.Add("TotalPrice", typeof(decimal));
            aggregatedTable.Columns.Add("FreeItem", typeof(bool));
            aggregatedTable.Columns.Add("TtlRecQuantity", typeof(decimal));

            LocationTable.Columns.Add("BinName", typeof(string));
            LocationTable.Columns.Add("WhName", typeof(string));
            LocationTable.Columns.Add("LotNo", typeof(string));
            LocationTable.Columns.Add("BinID", typeof(string));
            LocationTable.Columns.Add("WhID", typeof(string));
        }

        private void AddPOItems(DataTable selectedItems)
        {
            aggregatedTable.Columns["TotalPrice"].ReadOnly = false;
            aggregatedTable.Columns["TtlRecQuantity"].ReadOnly = false;
            foreach (DataRow item in selectedItems.Rows)
            {
                string sku = item["SKU"].ToString();
                string partNo = item["PartNo"].ToString();
                string description = item["DescName"].ToString();
                string brand = item["BrandName"].ToString();
                string uom = item["UomID"].ToString();
                decimal promo = Convert.ToDecimal(item["Discount"]);
                decimal netprice = Convert.ToDecimal(item["NetPrice"]);
                decimal qty = Convert.ToDecimal(item["Qty"]);
                decimal totalPrice = Convert.ToDecimal(item["TotalPrice"]);
                decimal quantityreceived = Convert.ToDecimal(item["TtlRecQuantity"] ?? "0.00"); 
                bool free = Convert.ToBoolean(item["FreeItem"]);


                var existingRow = aggregatedTable.AsEnumerable()
                    .FirstOrDefault(row => row.Field<string>("PartNo").Trim() == partNo.Trim());

                if (existingRow != null)
                {
                    existingRow["Qty"] = existingRow.Field<decimal>("Qty") + qty;
                    //existingRow["TotalPrice"] = ((existingRow.Field<decimal>("TotalPrice") * (existingRow.Field<decimal>("Qty") - qty)) + totalPrice) / existingRow.Field<decimal>("Qty");
                    //existingRow["TotalPrice"] = (existingRow.Field<decimal>("NetPrice") * (existingRow.Field<decimal>("TtlRecQuantity")));
                }
                else
                {
                    DataRow newRow = aggregatedTable.NewRow();
                    newRow["SKU"] = sku;
                    newRow["PartNo"] = partNo;
                    newRow["DescName"] = description;
                    newRow["BrandName"] = brand;
                    newRow["UomID"] = uom;
                    newRow["Discount"] = promo;
                    newRow["NetPrice"] = netprice;
                    newRow["Qty"] = qty;
                    newRow["TotalPrice"] = totalPrice;
                    newRow["FreeItem"] = free;
                    newRow["TtlRecQuantity"] = quantityreceived;
                    //newRow["PONo"] = "";
                    //newRow["SLName"] = ""; 
                    //newRow["TermName"] = "";
                    aggregatedTable.Rows.Add(newRow);
                }
            }
            dgvReceivingItems.Refresh();
            aggregatedTable.Columns["TotalPrice"].ReadOnly = true;
            aggregatedTable.Columns["TtlRecQuantity"].ReadOnly = true;
        }

        private void RemovePOItems(DataTable selectedItems)
        {
            aggregatedTable.Columns["TotalPrice"].ReadOnly = false;
            aggregatedTable.Columns["TtlRecQuantity"].ReadOnly = false;
            foreach (DataRow item in selectedItems.Rows)
            {
                string partNo = item["PartNo"].ToString();
                decimal qty = Convert.ToDecimal(item["Qty"]);
                decimal totalPrice = Convert.ToDecimal(item["TotalPrice"]);

                var existingRow = aggregatedTable.AsEnumerable()
                    .FirstOrDefault(row => row.Field<string>("PartNo") == partNo);
                
                if (existingRow != null)
                {
                    decimal currentQty = existingRow.Field<decimal>("Qty") - qty;

                    if (currentQty <= 0)
                    {
                        aggregatedTable.Rows.Remove(existingRow);
                    }
                    else
                    {
                        existingRow["Qty"] = currentQty;
                        //existingRow["TotalPrice"] = (existingRow.Field<decimal>("NetPrice") * (existingRow.Field<decimal>("TtlRecQuantity")));
                        //existingRow["TotalPrice"] = ((existingRow.Field<decimal>("TotalPrice") * (existingRow.Field<decimal>("Qty") + qty)) - totalPrice) / currentQty;
                    }
                }
            }
            aggregatedTable.Columns["TotalPrice"].ReadOnly = true;
            aggregatedTable.Columns["TtlRecQuantity"].ReadOnly = true;
        }

        private void SetTotalReceivedandPrice()
        {
            if(aggregatedTable.Rows.Count > 0)
            {
                foreach (DataRow item in aggregatedTable.Rows)
                {
                    aggregatedTable.Columns["TtlRecQuantity"].ReadOnly = false;
                    aggregatedTable.Columns["TotalPrice"].ReadOnly = false;
                    item["TtlRecQuantity"] = 0;
                    item["TotalPrice"] = 0;
                    aggregatedTable.Columns["TtlRecQuantity"].ReadOnly = true;
                    aggregatedTable.Columns["TotalPrice"].ReadOnly = true;
                   
                }
            }
            
        }



        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtRRN.Textt == "NEW RECORD")
            {
                if (!chkRushP.Checked && cmbSupplier.DataSource != null)
                {
                    selectedSupp = cmbSupplier.SelectedValue.ToString();
                    if (!String.IsNullOrEmpty(selectedSupp))
                    {
                        ClearTables();
                        ClearFields();
                        CheckUnCheckRushOrder(false);
                        _termsDictionary.Clear();
                        _termsDictionary = receivingController.getTermsSupplier(selectedSupp);
                        cmbTerms.DataSource = new BindingSource(_termsDictionary, null);
                        cmbTerms.DisplayMember = "Value";
                        cmbTerms.ValueMember = "Key";
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }
        private void cmbTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtRRN.Textt == "NEW RECORD")
            {
                if (!chkRushP.Checked && cmbTerms.DataSource != null)
                {
                    selectedTerm = cmbTerms.SelectedValue.ToString();
                    if (!String.IsNullOrEmpty(selectedSupp) && !String.IsNullOrEmpty(selectedTerm))
                    {
                        ClearTables();
                        CheckUnCheckRushOrder(false);
                        POTable = receivingController.PODisplay(selectedSupp, selectedTerm,txtRRN.Textt);
                        dgvReceivingItems.DataSource = aggregatedTable;
                        dgvReceivingItems.Refresh();
                        dgvLocation.Refresh();
                        dgvLocation.DataSource = LocationTable;
                        dgvPO.DataSource = POTable;
                    }
                    else
                    {
                        ClearTables();
                        return;
                    }
                }else
                {
                    return;
                }
            }
        }

        private void ClearTables()
        {
            POTable.Clear();
            ReceivingItem.Clear();
            LocationTable.Clear();
            aggregatedTable.Clear();
            locationList.Clear();
            temporaryLoc.Clear();
        }

        private void ClearFields()
        {
            txtDRRN.Textt = "";
            txtIRN.Textt = "";
            txtRemarks.Textt = "";
            txtDRD.Value = DateTime.Today;
            txtInvD.Value = DateTime.Today;
            customDateTime1.Value = DateTime.Today;
        }
        private void CheckUnCheckRushOrder(bool isTrue)
        {
            chkRushP.Checked = isTrue;
        }

        private void ClearAll()
        {
            ClearTables();
            ClearFields();
            CheckUnCheckRushOrder(false);
            EnableTextBoxes(true);
            txtRRN.Textt = "NEW RECORD";
        }

        private void EnableTextBoxes(bool isTrue)
        {
            chkRushP.Enabled = isTrue;
            cmbTerms.Enabled = isTrue;
            cmbSupplier.Enabled = isTrue;
            txtRRN.Enabled = isTrue;
        }

        private string statuses(int stats)
        {
            switch (stats)
            {
                case 1:
                    return "FOR RECEIVING";
                case 2:
                    return "SAVE";
                default:
                    return "Unknown";
            }
        }

        private void dgvPO_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPO.Columns[e.ColumnIndex].Name == "Status")
            {
                if (e.Value != null)
                {
                    int statusCode = Convert.ToInt32(e.Value);
                    e.Value = statuses(statusCode);
                    e.FormattingApplied = true;
                }
            }
        }

        private void dgvLocation_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            bool selectedPO = false;
            if (dgvPO.CurrentRow != null)
            {
                selectedPO = Convert.ToBoolean(dgvPO.CurrentRow.Cells["ChckPO"].Value);
            }

            //LocationInfo locationInfo = null;
            if (selectedPO == true || isRushOrder)
            {
                decimal totalQty = 0;
                int editIndex = dgvLocation.Columns["Qty"].Index;

                if (e.ColumnIndex == editIndex && dgvLocation.Rows[e.RowIndex].Cells[editIndex].Value.ToString() != "")
                {
                    decimal quantity = Convert.ToDecimal(dgvLocation.Rows[e.RowIndex].Cells[editIndex].Value);
                    dgvLocation.Rows[e.RowIndex].Cells[editIndex].Value = quantity;

                    string partNo = dgvReceivingItems.CurrentRow.Cells["PartNo"].Value.ToString().Trim();
                    string locationBin = dgvLocation.Rows[e.RowIndex].Cells["BinIDd"].Value.ToString();
                    /*locationInfo = locationList.FirstOrDefault(info => info.PartNo == partNo && info.LocationBin == locationBin && info.PONo == pono);

                    if (locationInfo != null)
                    {
                        locationInfo.Qty = (int)quantity;
                        locationInfo.IsChecked = quantity > 0;
                    } else
                    {
                        locationInfo = new LocationInfo
                        {
                            PartNo = partNo,
                            LocationBin = locationBin,
                            Qty = quantity,
                            PONo = pono
                        };
                        locationList.Add(locationInfo);
                    }*/


                    //foreach (DataGridViewRow row in dgvLocation.Rows)
                    //{
                    if (e.RowIndex >= 0)
                    {
                        DataGridViewRow row = dgvLocation.Rows[e.RowIndex];
                        if (row.Visible)
                        {
                            totalQty = dgvLocation.Rows.Cast<DataGridViewRow>().Sum(row2 => Convert.ToDecimal(row2.Cells["Qty"].Value));
                            //totalQty += Convert.ToDecimal(row.Cells["Qty"].Value);
                        }

                        if (Convert.ToDecimal(row.Cells["Qty"].Value) > 0)
                        {
                            row.Cells["ChckLoc"].Value = true;
                            RecItemLoc checktemploc = temporaryLoc.FirstOrDefault(temploc => temploc.PartNo == partNo.Trim() && temploc.BinID == locationBin.Trim() && temploc.PONo == pono.Trim());
                            if (checktemploc != null)
                            {
                                checktemploc.Qty = Convert.ToDecimal(row.Cells["Qty"].Value);
                            }
                            else
                            {
                                recItemLoc = new RecItemLoc
                                {
                                    PartNo = partNo.Trim(),
                                    LotNo = row.Cells["LotNo"].Value.ToString(),
                                    Qty = Convert.ToDecimal(row.Cells["Qty"].Value.ToString()),
                                    WhID = row.Cells["WhIDd"].Value.ToString(),
                                    BinID = row.Cells["BinIDd"].Value.ToString().Trim(),
                                    UniqueID = Helper.GenerateUID(),
                                    WhName = row.Cells["Warehouse"].Value.ToString(),
                                    BinName = row.Cells["LocationBin"].Value.ToString(),
                                    PONo = pono
                                };
                                temporaryLoc.Add(recItemLoc);
                            }
                        }
                        else
                        {
                            row.Cells["ChckLoc"].Value = false; 
                            RecItemLoc checktemploc = temporaryLoc.FirstOrDefault(temploc => temploc.PartNo == partNo.Trim() && temploc.BinID == locationBin.Trim() && temploc.PONo == pono.Trim());
                            if (checktemploc != null)
                            {
                                checktemploc.Qty = 0;
                            }
                            string selectedlocation = (row.Cells["ChckLoc"].Value = false).ToString();
                        }
                    }

                    decimal initialQty = Convert.ToDecimal(dgvReceivingItems.CurrentRow.Cells["QuantityRec"].Value);
                    if (totalQty > initialQty && !isRushOrder)
                    {
                        MessageBox.Show("The entered quantity exceeds the available received quantity.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RecItemLoc checktemploc = temporaryLoc.FirstOrDefault(temploc => temploc.PartNo == partNo.Trim() && temploc.BinID == locationBin.Trim() && temploc.PONo == pono.Trim());
                        if (checktemploc != null)
                        {
                            checktemploc.Qty = 0;
                        }
                        dgvLocation.CurrentRow.Cells["Qty"].Value = 0;
                        dgvLocation.CurrentCell = dgvLocation[e.ColumnIndex, e.RowIndex];
                    }
                    getReceivedSum();
                }
            }

            else
            {
                Helper.Confirmator("Please check the current selected PO to continue editing this location", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void getReceivedSum()
        {
            aggregatedTable.Columns["TtlRecQuantity"].ReadOnly = false;
            aggregatedTable.Columns["TotalPrice"].ReadOnly = false;
            dgvReceivingItems.Columns["TotalPrice"].ReadOnly = false;
            dgvReceivingItems.Columns["TtlRecQuantity"].ReadOnly = false;
            DataGridViewRow parentCurrentRow = dgvReceivingItems.CurrentRow;
            if (temporaryLoc.Any())
            {
                if (parentCurrentRow != null)
                {
                    decimal filteredSum = temporaryLoc.Where(a => a.PartNo == dgvReceivingItems.CurrentRow.Cells["PartNo"].Value.ToString().Trim()).Sum(s => s.Qty);

                    DataGridViewCell parentCell = parentCurrentRow.Cells["TtlRecQuantity"]; 
                    if (parentCell != null && !parentCell.ReadOnly)
                    {
                        parentCell.Value = filteredSum;
                        parentCurrentRow.Cells["TotalPrice"].Value = Convert.ToDecimal(dgvReceivingItems.CurrentRow.Cells["NetPrice"].Value ?? "0") * Convert.ToDecimal(parentCurrentRow.Cells["TtlRecQuantity"].Value ?? "0");
                    }
                    //dgvReceivingItems.CurrentRow.Cells["TotalPrice"].Value = Convert.ToDecimal(dgvReceivingItems.CurrentRow.Cells["NetPrice"].Value ?? "0") * Convert.ToDecimal(parentCurrentRow.Cells["TtlRecQuantity"].Value ?? "0");
                }

            }
            if(!temporaryLoc.Any() && parentCurrentRow != null)
            {
                parentCurrentRow.Cells["TtlRecQuantity"].Value = 0;
                parentCurrentRow.Cells["TotalPrice"].Value = 0;
            }
            aggregatedTable.Columns["TotalPrice"].ReadOnly = true;
            aggregatedTable.Columns["TtlRecQuantity"].ReadOnly = true;
            dgvReceivingItems.Columns["TotalPrice"].ReadOnly = true;
            dgvReceivingItems.Columns["TtlRecQuantity"].ReadOnly = true;
        }

        private void dgvLocation_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvLocation.Columns["ChckLoc"].Index && e.RowIndex != -1)
            {
                if (dgvReceivingItems.Rows.Count == 0)
                {
                    Helper.Confirmator("There's no data found in Receiving Items", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void dgvLocation_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvLocation.IsCurrentCellDirty)
            {
                dgvLocation.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvReceivingItems_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LocationSelection(e.RowIndex);
            loadCheckedLocations(e.RowIndex);
        }

        private void dgvReceivingItems_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvReceivingItems.CurrentRow != null && dgvPO.Rows.Count > 0)
            {
                int selectedIndex = dgvReceivingItems.CurrentRow.Index;
                string selectedOrderNo = dgvPO.CurrentRow.Cells["PONNo"].Value.ToString();
               // SaveCheckboxState(selectedIndex);
            }
        }

        private void getCheckedLocation()
        {
            string currentRow = dgvReceivingItems.CurrentRow.Cells["PartNo"].Value.ToString();
            foreach (DataGridViewRow row in dgvLocation.Rows)
            {
                if ((bool)row.Cells["ChckLoc"].Value == true)
                {
                    string locationBin = row.Cells["BinIDd"].Value.ToString();
                    int quantity = 0;
                    if (int.TryParse(row.Cells["Qty"].Value.ToString(), out quantity))
                    {
                        var locationInfo = new LocationInfo
                        {
                            PartNo = currentRow,
                            LocationBin = locationBin,
                            IsChecked = true,
                            Qty = quantity,
                            PONo = pono
                        };
                        locationList.Add(locationInfo);
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid quantity.");
                    }
                }
            }
        }

        private void loadCheckedLocations(int rowIndex)
        {
            string partNo = dgvReceivingItems.Rows[rowIndex].Cells["PartNo"].Value.ToString().Trim();

            foreach (DataGridViewRow row in dgvLocation.Rows)
            {
                string locationBin = row.Cells["BinIDd"].Value.ToString();
                var locationInfo = temporaryLoc.FirstOrDefault(info => info.PartNo.Trim() == partNo && info.BinID == locationBin && info.PONo == pono);

                if (locationInfo != null)
                {
                    row.Cells["ChckLoc"].Value = locationInfo.Qty > 0;
                    row.Cells["Qty"].Value = locationInfo.Qty;
                }
                else
                {
                    row.Cells["ChckLoc"].Value = false;
                    //row.Cells["Qty"].Value = 0;
                }
            }
        }
   
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (selectedTerm != "" || selectedTerm != null || !String.IsNullOrEmpty(cmbSupplier.SelectedValue.ToString()))
            {
                if (!String.IsNullOrEmpty(txtIRN.Textt) || !String.IsNullOrEmpty(txtDRRN.Textt))
                {
                    //ReceivingMF receivingMf = new ReceivingMF();
                    List<ReceivingModel> receivingModels = new List<ReceivingModel>();
                    List<RecItemLoc> receivingLocations = new List<RecItemLoc>();
                    int errorcounter = 0;
                    if (dgvReceivingItems.Rows.Count != 0 && temporaryLoc.Count > 0)
                    {
                        RecItemLoc recLoc = new RecItemLoc();
                        List<ReceivingDet> receivingDetss = new List<ReceivingDet>();
                        List<ReceivedItems> receivedItems = new List<ReceivedItems>();
                        List<RecItemLoc> recIteem = new List<RecItemLoc>();
                        List<ReceivingModel> receivings = new List<ReceivingModel>();

                        string formattedDate = "";
                        DateTime selectedInvoiceDate;
                        if (DateTime.TryParse(txtInvD.Text, out selectedInvoiceDate))
                        {
                            // Format the DateTime object as required
                            formattedDate = selectedInvoiceDate.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        if (dgvReceivingItems.Rows.Count != 0 || temporaryLoc.Count > 0)
                        {
                            if (isRushOrder)
                            {
                                receivingModel = new ReceivingModel
                                {
                                    SupplierID = cmbSupplier.SelectedValue.ToString(),
                                    TermId = selectedTerm,
                                    Status = 1, //Save
                                    InvoiceNo = txtIRN.Textt.ToString(),
                                    DRNo = txtDRRN.Textt.ToString(),
                                    InvoiceDt = formattedDate,
                                    Remarks = txtRemarks.Textt.ToString(),
                                    ReasonId = receivingController.getTemporaryReason(),
                                    ReceivedBy = receivingController.getTemporaryEmployee(),
                                    PONo = "",
                                    receivingDets = receivingDetss,
                                };
                                receivings.Add(receivingModel);
                            }
                            else
                            {
                                foreach (DataGridViewRow rows in dgvPO.Rows)
                                {
                                    if (Convert.ToBoolean(rows.Cells["ChckPO"].Value))
                                    {
                                        //string termID = receivingController.getTermID(cmbSupplier.SelectedValue.ToString());
                                        receivingModel = new ReceivingModel
                                        {
                                            SupplierID = cmbSupplier.SelectedValue.ToString(),
                                            TermId = selectedTerm,
                                            Status = 1, //Save
                                            InvoiceNo = txtIRN.Textt.ToString(),
                                            DRNo = txtDRRN.Textt.ToString(),
                                            InvoiceDt = formattedDate,
                                            Remarks = txtRemarks.Textt.ToString(),
                                            ReasonId = receivingController.getTemporaryReason(),
                                            ReceivedBy = receivingController.getTemporaryEmployee(),
                                            PONo = rows.Cells["PONNo"].Value.ToString(),
                                            receivingDets = receivingDetss,
                                        };
                                        receivings.Add(receivingModel);
                                    }
                                }
                            }
                            foreach (DataRow row in aggregatedTable.Rows)
                            {
                                if (Convert.ToBoolean(row["FreeItem"]))
                                {
                                    if (Convert.ToDecimal(row["TtlRecQuantity"] ?? "0") < 1)
                                    {
                                        errorcounter++;
                                        Helper.Confirmator("Free Item should have quantity and atleast 1 location", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        receivingDet = new ReceivingDet
                                        {
                                            PartNo = row["PartNo"].ToString(),
                                            Qty = Convert.ToDecimal(row["TtlRecQuantity"].ToString()),
                                            UnitPrice = Convert.ToDecimal(row["NetPrice"]),
                                            Freeitem = Convert.ToBoolean(row["FreeItem"]).ToString(),
                                            Status = 1, //Save,
                                            VATAmt = Convert.ToDecimal(0.00),
                                        };
                                    }
                                }
                                else
                                {
                                    receivingDet = new ReceivingDet
                                    {
                                        PartNo = row["PartNo"].ToString(),
                                        Qty = Convert.ToDecimal(row["TtlRecQuantity"].ToString()),
                                        UnitPrice = Convert.ToDecimal(row["NetPrice"]),
                                        Freeitem = Convert.ToBoolean(row["FreeItem"]).ToString(),
                                        Status = 1, //Save,
                                        VATAmt = Convert.ToDecimal(0.00),
                                    };
                                }
                                receivingDetss.Add(receivingDet);
                            }
                            temporaryLoc.RemoveAll(x => x.Qty == 0 && x.PartNo != "" && x.BinID != "");
                            if (errorcounter == 0)
                            {
                                if (temporaryLoc.Any())
                                {
                                    receivingMFP = new ReceivingMF
                                    {
                                        receivingModels = receivings,
                                        receivingLoc = temporaryLoc,
                                        receivingItems = receivingDetss
                                    };
                                    SortedDictionary<string, string> keyValuePairs = new SortedDictionary<string, string>();
                                    if (txtRRN.Textt == "NEW RECORD")
                                    {
                                        keyValuePairs = receivingController.CreateAll(receivingMFP, "", isRushOrder);
                                    }
                                    else
                                    {
                                        keyValuePairs = receivingController.CreateAll(receivingMFP, txtRRN.Textt.ToString().TrimEnd(), isRushOrder);
                                    }

                                    KeyValuePair<string, string> msg = keyValuePairs.First();
                                    KeyValuePair<string, string> rrno = keyValuePairs.Last();
                                    msgResult = msg.Key;
                                    rrNo = rrno.Value;
                                    //string msgResult = receivingController.CreateAll(receivingMF);
                                    Helper.Confirmator(msgResult, "Received Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    if (msgResult == "Information saved successfully")
                                    {
                                        if (!chkRushP.Checked)
                                        {
                                            isRushOrder = false;
                                        }
                                        isEditingRR = true;
                                        txtRRN.Textt = rrNo;
                                        EnableTextBoxes(false);
                                    }
                                }
                                else
                                {

                                    Helper.Confirmator("Please input quantity per location", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                        {
                            Helper.Confirmator("There are no PO Items to save", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        Helper.Confirmator("There are no PO Items to save", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    Helper.Confirmator("Please input Invoice No. Or Delivery Receipt No.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                Helper.Confirmator("Please select supplier and term.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {

            //ReceivingMF receivingMf = new ReceivingMF();
            string rrno = txtRRN.Textt;
            if(rrno == "NEW RECORD")
            {
                MessageBox.Show("There is no RR No. for posting", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(txtIRN.Textt == "")
            {
                Helper.Confirmator("Please input Invoice No. Or Delivery Receipt No.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            } 
            else
            {
                string msgResult = receivingController.POReceiving(receivingMFP, txtRRN.Textt);
                Helper.Confirmator(msgResult, "Received Posted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (msgResult == "Information posted successfully")
                {
                    ClearAll();
                    EnableTextBoxes(true);
                    _termsDictionary.Clear();
                    cmbTerms.DataSource = null;
                    cmbTerms.Text = "";
                    cmbSupplier.SelectedIndex = 0;
                    chkRushP.Enabled = true; 
                    isRushOrder = false;
                }
            }
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            frm_po_receiving_rr_selection frm_rrSelection = new frm_po_receiving_rr_selection();
            frm_rrSelection.DataPassed += OnDataPassed;
            frm_rrSelection.ShowDialog(this);
        }

        private void OnDataPassed(string rrno,string suppID, string termID,string termName)
        {

            /*txtRRN.Textt = rrno;
            List<string> ponum = receivingController.SelectedRRForPO(rrno).ToList();
            isEditingRR = true;
            cmbSupplier.SelectedValue = suppID;
            //cmbTerms.SelectedValue = termID;
            _termsDictionary.Clear();
            _termsDictionary = receivingController.getTermsIfSelectedRR(rrno,false); //Ieedit ko pa bukas
            cmbTerms.DataSource = new BindingSource(_termsDictionary, null);
            cmbTerms.DisplayMember = "Value";
            cmbTerms.ValueMember = "Key";
            ClearTables();
            ReceivingModel receivingModels = new ReceivingModel();
            receivingModels = receivingController.getRRDetails(rrno);




            DataTable fortemp = new DataTable();
            fortemp = receivingController.LocationDisplayWRR("", txtRRN.Textt.ToString().TrimEnd(), 2);
            foreach (DataRow row in fortemp.Rows)
            {
                bool isExist = temporaryLoc.Where(ex => ex.BinID == row["BinID"].ToString().Trim() && ex.PartNo.Trim() == row["PartNo"].ToString().Trim()).Any();
                if (!isExist)
                {
                    RecItemLoc item = new RecItemLoc
                    {
                        PartNo = row["PartNo"].ToString().Trim(),
                        LotNo = row["LotNo"].ToString(),
                        Qty = Convert.ToDecimal(row["Qty"] ?? "0.00"),
                        WhID = row["WhID"].ToString(),
                        BinID = row["BinID"].ToString().Trim(),
                        UniqueID = Helper.GenerateUID(),
                        WhName = row["WhName"].ToString(),
                        BinName = row["BinName"].ToString(),
                        PONo = pono,
                        ischecked = Convert.ToDecimal(row["Qty"] ?? "0.00") > 0,
                    };
                    temporaryLoc.Add(item);
                }
            }

            selectedTerm = termID;
            cmbTerms.Text = termName;
            txtRemarks.Textt = receivingModels.Remarks;
            txtDRD.Value = DateTime.Today;
            txtDRRN.Textt = "";
            txtInvD.Text = receivingModels.InvoiceDt;
            txtIRN.Textt = receivingModels.InvoiceNo;
            customDateTime1.Text = receivingModels.CreatedDt;

            EnableTextBoxes(false);
            CheckUnCheckRushOrder(false);
            displayPOSelectedRR();
            displayPOReceiveItems();
            Checkponum(ponum);
            isRushOrder = false;*/

            
            txtRRN.Textt = rrno;
            isEditingRR = true;
            ClearTables();

            List<string> ponum = receivingController.SelectedRRForPO(rrno).ToList();
            ReceivingModel receivingModels = new ReceivingModel();
            receivingModels = receivingController.getRRDetails(rrno);

            //cmbSupplier.Text = receivingModels.SupplierID;


            _termsDictionary.Clear();
            _termsDictionary = receivingController.getTermsIfSelectedRR(rrno, false); //Ieedit ko pa bukas
            cmbTerms.DataSource = new BindingSource(_termsDictionary, null);
            cmbTerms.DisplayMember = "Value";
            cmbTerms.ValueMember = "Key";



            _supplierDictionary = receivingController.getSupplier(true);
            cmbSupplier.DataSource = new BindingSource(_supplierDictionary, null);
            cmbSupplier.DisplayMember = "Value";
            cmbSupplier.ValueMember = "Key";

            //cmbTerms.SelectedValue = termID;

            cmbSupplier.SelectedValue = suppID.Trim();
            DataTable fortemp = new DataTable();
            fortemp = receivingController.LocationDisplayWRR("", txtRRN.Textt.ToString().TrimEnd(), 2);
            foreach (DataRow row in fortemp.Rows)
            {
                bool isExist = temporaryLoc.Where(ex => ex.BinID == row["BinID"].ToString().Trim() && ex.PartNo.Trim() == row["PartNo"].ToString().Trim()).Any();
                if (!isExist)
                {
                    RecItemLoc item = new RecItemLoc
                    {
                        PartNo = row["PartNo"].ToString().Trim(),
                        LotNo = row["LotNo"].ToString(),
                        Qty = Convert.ToDecimal(row["Qty"] ?? "0.00"),
                        WhID = row["WhID"].ToString(),
                        BinID = row["BinID"].ToString().Trim(),
                        UniqueID = Helper.GenerateUID(),
                        WhName = row["WhName"].ToString(),
                        BinName = row["BinName"].ToString(),
                        PONo = pono,
                        ischecked = Convert.ToDecimal(row["Qty"] ?? "0.00") > 0,
                    };
                    temporaryLoc.Add(item);
                }
            }

            selectedTerm = termID;
            cmbTerms.Text = termName;
            txtRemarks.Textt = receivingModels.Remarks;
            txtDRD.Value = DateTime.Today;
            txtDRRN.Textt = "";
            txtInvD.Text = receivingModels.InvoiceDt;
            txtIRN.Textt = receivingModels.InvoiceNo;
            customDateTime1.Text = receivingModels.CreatedDt;
            EnableTextBoxes(false);
            if (receivingModels.RushOrder == 1)
            {
                isRushOrder = true;
                CheckUnCheckRushOrder(true);
            }
            else
            {
                isRushOrder = false;
                CheckUnCheckRushOrder(false);
                displayPOSelectedRR();
                Checkponum(ponum);
            }
            displayPOReceiveItems();
        }

        private void displayPOSelectedRR()
        {
            string selectedSupp = cmbSupplier.SelectedValue.ToString();
            POTable = receivingController.PODisplay(selectedSupp,selectedTerm,txtRRN.Textt);
            dgvReceivingItems.DataSource = aggregatedTable;
            dgvLocation.DataSource = LocationTable;
            dgvPO.DataSource = POTable;
            dgvPO.Refresh();
            dgvReceivingItems.Refresh();
            dgvLocation.Refresh();
        }

        private void displayPOReceiveItems()
        {
            var selectedItems = receivingController.ReceivingItemRR(txtRRN.Textt);
            AddPOItems(selectedItems);
            dgvReceivingItems.DataSource = aggregatedTable;
            dgvReceivingItems.Refresh();
        }

        private void Checkponum(List<string> ponum)
        {
            foreach(string poNo in ponum)
            {
                foreach(DataGridViewRow row in dgvPO.Rows)
                {
                    if (row.Cells["PONNo"].Value != null && row.Cells["PONNo"].Value.ToString() == poNo)
                    {
                        row.Cells["ChckPO"].Value = true;
                    }
                }
            }
            dgvPO.Refresh();
        }

        private void btnAddLoc_Click(object sender, EventArgs e)
        {
            if(dgvReceivingItems.Rows.Count != 0)
            {
                string partNo = dgvReceivingItems.CurrentRow.Cells["PartNo"].Value.ToString();
                /*string distinctBinsForPartNo = string.Join(",", temporaryLoc
                        .Where(item => item.PartNo == partNo.Trim()) // Filter by PartNo
                        .Select(item => item.BinID) // Select BinID
                        .Distinct());*/

                string distinctBinsForPartNo = string.Join(",", temporaryLoc
                        .Where(item => item.PartNo == partNo.Trim()) // Filter by PartNo
                        .Select(item => $"'{item.BinID}'") // Select BinID enclosed in single quotes
                        .Distinct());
                Console.WriteLine($"Bins: [{distinctBinsForPartNo}]");
                frm_po_receiving_loc_selection frm_location = new frm_po_receiving_loc_selection(partNo, distinctBinsForPartNo);
                    frm_location.RowSelected += onLocationpassed;
                frm_location.ShowDialog(this);
            }
            else
            {
                Helper.Confirmator("Please select an item", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void onLocationpassed(object sender, DataRowEventArgs e)
        {
            DataRow selectedLoc = e.Row;
            DataRow newrow = LocationTable.NewRow();
            newrow["BinName"] = selectedLoc["BinName"];
            newrow["WhName"] = selectedLoc["WhName"];
            newrow["LotNo"] = "";
            newrow["BinID"] = selectedLoc["BinID"];
            newrow["WhID"] = selectedLoc["WhID"];
            newrow["Qty"] = 0;
            string currentpartno = dgvReceivingItems.CurrentRow.Cells["PartNo"].Value.ToString();
            bool isExist = temporaryLoc.Where(ex => ex.BinID == selectedLoc["BinID"].ToString().Trim() && ex.PartNo.Trim() == currentpartno.Trim()).Any();
            if (!isExist) { 
                if(currentpartno != null)
                {
                    RecItemLoc checktemploc = temporaryLoc.FirstOrDefault(temploc => temploc.PartNo == currentpartno.Trim() && temploc.BinID == selectedLoc["BinID"].ToString().Trim() && temploc.PONo == pono.Trim());
                    recItemLoc = new RecItemLoc
                    {
                        PartNo = currentpartno.Trim(),
                        LotNo = "",
                        Qty = 0,
                        WhID = selectedLoc["WhID"].ToString().Trim(),
                        BinID = selectedLoc["BinID"].ToString().Trim(),
                        UniqueID = Helper.GenerateUID(),
                        PONo = pono,
                        WhName = selectedLoc["WhName"].ToString(),
                        BinName = selectedLoc["BinName"].ToString(),
                    };
                    temporaryLoc.Add(recItemLoc);
                }
                LocationTable.Rows.Add(newrow);
                dgvLocation.DataSource = LocationTable;
                dgvLocation.Refresh();
            } else
            {
                MessageBox.Show("Location already exists", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            System.Drawing.Font headerFont = new Font("Arial", 14, FontStyle.Bold);
            Font subheaderFont = new Font("Arial", 10, FontStyle.Bold);
            Font normalBoldFont = new Font("Arial", 8, FontStyle.Bold);
            Font normalFont = new Font("Arial", 8);
            Font subdetailFont = new Font("Arial", 6);
            Font subdetHeaderFont = new Font("Arial", 7, FontStyle.Bold);
            Font subdetailFontBold = new Font("Arial", 6, FontStyle.Bold);

            float x = 50;
            float y = 50;
            float z = 50;

            // Set page size (e.g., A4)
            //e.PageSettings.PaperSize = new PaperSize("A4", 850, 1100);
            float pageHeight = e.PageSettings.PrintableArea.Height;

            // Initialize variables
            int CurrentRecord = 0;
            int RecordsPerPage = 10;

            if (holder == 0)
            {
                int desiredWidth = 50;
                int actualHeight = (int)(desiredWidth);
                Rectangle destRect = new Rectangle(50, 50, desiredWidth, actualHeight);
                if (headerImage != null)
                {
                    g.DrawImage(headerImage, destRect);
                }
                float textX = destRect.Right + 20;
                float textY = destRect.Top;
                g.DrawString(headerText, headerFont, Brushes.Black, textX, textY);
                y += 30;
                //g.DrawString(ownerCompany.Address, normalFont, Brushes.Black, textX, textY + 20);
                //g.DrawString("Tel No.: " + ownerCompany.TelNo, normalFont, Brushes.Black, textX, textY + 30);
                //g.DrawString("VAT REG. TIN: " + ownerCompany.TinNo, normalFont, Brushes.Black, textX, textY + 40);

                // Header - Right
                float additionalTextX = e.PageBounds.Width - g.MeasureString("RECEIPT REPORT", subheaderFont).Width - 50;
                float additionalTextY = destRect.Top;
                g.DrawString("RECEIPT", subheaderFont, Brushes.Black, additionalTextX, additionalTextY);
                g.DrawString("(RECEIVE REPORT)", subdetHeaderFont, Brushes.Black, additionalTextX, additionalTextY + 15);
                g.DrawString("No. "+ txtRRN.Textt.ToString(), subheaderFont, Brushes.Black, additionalTextX, additionalTextY + 35);

                // Body
                float boxWidth = e.PageBounds.Width - 2 * x; // Width of the box
                for (int i = 0; i < 1; i++)
                {
                    //float boxY = Math.Max(destRect.Bottom, textY) + y + (i * 35); // Adjust spacing between boxes
                    float boxY = Math.Max(destRect.Bottom, textY) + 20 + (i * 55);
                    RectangleF boxRect = new RectangleF(x, boxY, boxWidth, 65);
                    g.DrawRectangle(Pens.Black, boxRect.X, boxRect.Y, boxRect.Width, boxRect.Height);
                }
                float largeBoxY = Math.Max(destRect.Bottom, textY) + 20 + (1 * 85);
                RectangleF boxRectLarge = new RectangleF(x, largeBoxY, boxWidth, 400);
                g.DrawRectangle(Pens.Black, boxRectLarge.X, largeBoxY, boxRectLarge.Width, boxRectLarge.Height);

                y += 60;
                g.DrawString("SUPPLIER:", normalFont, Brushes.Black, x + 10, y);
                g.DrawString("INVOICE NO:", normalFont, Brushes.Black, x + 10, y + 15);
                g.DrawString("REMARKS:", normalFont, Brushes.Black, x + 10, y + 32);
                g.DrawString("RECEIVE DATE:", normalFont, Brushes.Black, x + 480, y);
                g.DrawString("PURCHASE ORDER NO:", normalFont, Brushes.Black, x + 480, y + 15);
                g.DrawString("TERMS:", normalFont, Brushes.Black, x + 480, y + 32);
                g.DrawString(orderReceive.Supplier, normalBoldFont, Brushes.Black, x + 100, y);
                g.DrawString(txtIRN.Textt.ToString().TrimEnd(), normalBoldFont, Brushes.Black, x + 100, y + 15);
                g.DrawString(orderReceive.Remarks, normalBoldFont, Brushes.Black, x + 100, y + 32);

                //y += 55;
                //g.DrawString("ADDRESS:", normalFont, Brushes.Black, x + 10, y);
                ////g.DrawString(CustomerOrder.Address, normalBoldFont, Brushes.Black, x + 15, y + 20);

                //y += 55;
                //g.DrawString("TIN:", normalFont, Brushes.Black, x + 10, y);
                ////g.DrawString(CustomerOrder.TinNo, normalBoldFont, Brushes.Black, x + 15, y + 20);
                //g.DrawString("BUSINESS NAME/STYLE:", normalFont, Brushes.Black, x + 400, y);
                ////g.DrawString(CustomerOrder.RegName, normalBoldFont, Brushes.Black, x + 405, y + 20);
                //g.DrawString("MODE OF PAYMENT:", normalFont, Brushes.Black, x + 800, y);
                ////g.DrawString(CustomerOrder.TermName, normalBoldFont, Brushes.Black, x + 805, y + 20);

                y += 110;
                g.DrawString("NO", subdetHeaderFont, Brushes.Black, x + 10, y - 30);
                g.DrawString("SKU", subdetHeaderFont, Brushes.Black, x + 80, y - 30);
                g.DrawString("PART NO", subdetHeaderFont, Brushes.Black, x + 180, y - 30);
                g.DrawString("DESCRIPTION", subdetHeaderFont, Brushes.Black, x + 300, y - 30);
                g.DrawString("SIZE", subdetHeaderFont, Brushes.Black, x + 460, y - 30);
                g.DrawString("BRAND", subdetHeaderFont, Brushes.Black, x + 520, y - 30);
                g.DrawString("QTY", subdetHeaderFont, Brushes.Black, x + 600, y - 30);
                g.DrawString("UOM", subdetHeaderFont, Brushes.Black, x + 660, y - 30);
                g.DrawString("U-PRICE", subdetHeaderFont, Brushes.Black, x + 720, y - 30);
                g.DrawString("AMOUNT", subdetHeaderFont, Brushes.Black, x + 800, y - 30);
                g.DrawString("TTL-PRICE", subdetHeaderFont, Brushes.Black, x + 880, y - 30);
                g.DrawString("LOCATION", subdetHeaderFont, Brushes.Black, x + 960, y - 30);
                g.DrawString("QTY", subdetHeaderFont, Brushes.Black, x + 1030, y - 30);



                float partY = y + 10;
                if(orderReceive.receiveReportParts != null) 
                {
                    for (int i = holder; i < orderReceive.receiveReportParts.Count(); i++)
                    {
                        g.DrawString(orderReceive.receiveReportParts.ToList()[i].ItemNo, subdetailFont, Brushes.Black, x + 15, partY);
                        g.DrawString(orderReceive.receiveReportParts.ToList()[i].SKU, subdetailFont, Brushes.Black, x + 60, partY);
                        g.DrawString(orderReceive.receiveReportParts.ToList()[i].PartNo, subdetailFont, Brushes.Black, x + 170, partY);
                        g.DrawString(orderReceive.receiveReportParts.ToList()[i].Description.ToString(), subdetailFont, Brushes.Black, x + 300, partY);
                        g.DrawString(orderReceive.receiveReportParts.ToList()[i].Brand, subdetailFont, Brushes.Black, x + 520, partY);
                        g.DrawString(orderReceive.receiveReportParts.ToList()[i].Qty.ToString("N0"), subdetailFont, Brushes.Black, x + 600, partY);
                        g.DrawString(orderReceive.receiveReportParts.ToList()[i].Uom.ToString(), subdetailFont, Brushes.Black, x + 660, partY);
                        g.DrawString(orderReceive.receiveReportParts.ToList()[i].UnitPrice.ToString("N2"), subdetailFont, Brushes.Black, x + 725, partY);
                        g.DrawString(orderReceive.receiveReportParts.ToList()[i].TotalPrice.ToString("N2"), subdetailFont, Brushes.Black, x + 800, partY);
                        CurrentRecord++;
                        if (CurrentRecord >= holder + 10)
                        {
                            holder = CurrentRecord;
                            e.HasMorePages = true;
                            return;
                        }
                        else
                        {
                            partY += 20;
                            e.HasMorePages = false;
                        }
                    }

                }
                else
                {

                }
            }
            //else
            //{
            //    float partY = 50;
            //    for (int i = holder; i < orderReceive.receiveReportParts.Count(); i++)
            //    {
            //        g.DrawString(orderReceive.receiveReportParts.ToList()[i].ItemNo, normalFont, Brushes.Black, x + 15, partY);
            //        g.DrawString(orderReceive.receiveReportParts.ToList()[i].PartNo, normalFont, Brushes.Black, x + 55, partY);
            //        g.DrawString(orderReceive.receiveReportParts.ToList()[i].PartName, normalFont, Brushes.Black, x + 155, partY);
            //        g.DrawString(orderReceive.receiveReportParts.ToList()[i].Qty.ToString("N0"), normalFont, Brushes.Black, x + 515, partY);
            //        g.DrawString(orderReceive.receiveReportParts.ToList()[i].UomName, normalFont, Brushes.Black, x + 555, partY);
            //        g.DrawString(orderReceive.receiveReportParts.ToList()[i].NetPrice.ToString("N2"), normalFont, Brushes.Black, x + 715, partY);
            //        g.DrawString(orderReceive.receiveReportParts.ToList()[i].TotalAmount.ToString("N2"), normalFont, Brushes.Black, x + 815, partY);
            //        CurrentRecord++;
            //        if (CurrentRecord >= holder + 10)
            //        {
            //            holder = CurrentRecord;
            //            e.HasMorePages = true;
            //            return;
            //        }
            //        else
            //        {
            //            partY += 20;
            //            e.HasMorePages = false;
            //        }
            //    }
            //}
            //float VatY = y + 20;
            //for (int i = 0; i < 20; i++)
            //{
            //    g.DrawString(CustomerOrder.PartsList.ToList()[0].ItemNo, normalFont, Brushes.Black, x + 15, VatY + (20 * i));
            //    g.DrawString(CustomerOrder.PartsList.ToList()[0].PartNo, normalFont, Brushes.Black, x + 55, VatY + (20 * i));
            //    g.DrawString(CustomerOrder.PartsList.ToList()[0].PartName, normalFont, Brushes.Black, x + 155, VatY + (20 * i));
            //    g.DrawString(CustomerOrder.PartsList.ToList()[0].Qty.ToString("N0"), normalFont, Brushes.Black, x + 515, VatY + (20 * i));
            //    g.DrawString(CustomerOrder.PartsList.ToList()[0].UomName, normalFont, Brushes.Black, x + 555, VatY + (20 * i));
            //    g.DrawString(CustomerOrder.PartsList.ToList()[0].NetPrice.ToString("N2"), normalFont, Brushes.Black, x + 715, VatY + (20 * i));
            //    g.DrawString(CustomerOrder.PartsList.ToList()[0].TotalAmount.ToString("N2"), normalFont, Brushes.Black, x + 815, VatY + (20 * i));
            //}
                y += 340;
                z += 572;
                int desiredWidth1 = 50;
                int actualHeight1 = (int)(desiredWidth1 * ((float)headerImage.Height / headerImage.Width));
                Rectangle destRect1 = new Rectangle(50, 50, desiredWidth1, actualHeight1);
                float textX1 = destRect1.Right + 20;
                float textY1 = destRect1.Top;
                float totalAmtboxY = Math.Max(destRect1.Bottom, textY1) + 20 + y;
                float boxWidth1 = e.PageBounds.Width - 2 * x;
                RectangleF totalboxRect = new RectangleF(x, totalAmtboxY, boxWidth1, 20);
                g.DrawRectangle(Pens.Black, totalboxRect.X + 800, totalboxRect.Y, totalboxRect.Width - 800, totalboxRect.Height);
                g.DrawString("TOTAL AMOUNT", normalBoldFont, Brushes.Black, x + 810, z);
                
                //g.DrawString("DATE:", normalFont, Brushes.Black, x + 10, y + 20);

                y += 220;
                g.DrawString("PREPARED BY:", normalFont, Brushes.Black, x + 10, y);
                g.DrawString("DATE:", normalFont, Brushes.Black, x + 10, y + 20);
                g.DrawString("CHECKED BY:", normalFont, Brushes.Black, x + 380, y);
                g.DrawString("DATE:", normalFont, Brushes.Black, x + 380, y + 20);
                g.DrawString("BINNED BY:", normalFont, Brushes.Black, x + 780, y);
                g.DrawString("DATE:", normalFont, Brushes.Black, x + 780, y + 20);
                //g.DrawString("VAT Amount", subdetailFont, Brushes.Black, x + 700, y + 20);
                //g.DrawString("Total Amount Due", subdetailFont, Brushes.Black, x + 700, y + 50);
                //g.DrawString("TOTAL", headerFont, Brushes.Black, x + 880, y + 40);
                //End of Body

                //Footer
                y += 60;
                g.DrawString("Page "+holder, subdetailFontBold, Brushes.Black, x, y);
                g.DrawString("Run/Date/Time: " + DateTime.Now, subdetailFontBold, Brushes.Black, x, y +10);
                g.DrawString("Printed By: " /*+ DateTime.Now*/, subdetailFontBold, Brushes.Black, x, y + 20);
                //g.DrawString("_____________________________________", normalFont, Brushes.Black, x + 800, y + 10);
                //g.DrawString("Signature of Customer", normalFont, Brushes.Black, x + 860, y + 25);

                //y += 50;
                //g.DrawString("RP24060001 / AM-13960", subdetailFont, Brushes.Black, x + 860, y + 10);
                //g.DrawString("THIS INVOICE SHALL BE VALID", normalBoldFont, Brushes.Black, x + 860, y + 20);
                holder = 0;
            }

        private void printingTableStyle()
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ownerCompany = receivingReport.GetOwnerCompany();
            //orderReceive = receivingReport.GetReceiveOrder(txtRRN.Textt);
            (printPreviewDialog as Form).WindowState = FormWindowState.Maximized;
            printPreviewDialog.ShowDialog();
        }

        private void dgvPO_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvPO.IsCurrentCellDirty)
            {
                DataGridViewCell currentCell = dgvPO.CurrentCell;
                dgvPO.CommitEdit(DataGridViewDataErrorContexts.Commit);
                if (currentCell.ColumnIndex == dgvPO.Columns["ChckPO"].Index)
                {
                    bool isChecked = Convert.ToBoolean(dgvPO.Rows[currentCell.RowIndex].Cells["ChckPO"].Value);
                    var cellValue = dgvPO.Rows[currentCell.RowIndex].Cells["PONNo"].Value;

                    if (cellValue != null)
                    {
                        string poNo = cellValue.ToString();
                        var selectedItems = receivingController.ReceivingItems(poNo,txtRRN.Textt);
                        if (isChecked)
                        {
                            if (selectedItems != null && selectedItems.Rows.Count > 0)
                            {
                                LocationTable.Rows.Clear();
                                dgvLocation.DataSource = LocationTable;
                                AddPOItems(selectedItems);

                            }
                            else
                            {
                                MessageBox.Show("This PO has no item to receive", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dgvPO.Rows[currentCell.RowIndex].Cells["ChckPO"].Value = false;
                            }
                        }
                        else
                        {
                            RemovePOItems(selectedItems);
                        }
                        SetTotalReceivedandPrice();
                        getReceivedSum();
                        dgvReceivingItems.DataSource = aggregatedTable;
                        dgvReceivingItems.Refresh();
                        dgvPO.Refresh();

                    }
                    else
                    {
                        MessageBox.Show("The PONo value is null. Please check the data.");
                    }
                }
            }
            temporaryLoc.Clear();
            LocationTable.Rows.Clear();
            dgvLocation.DataSource = LocationTable;
            dgvLocation.Refresh();
        }

        private void dgvPO_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == dgvPO.Columns["ChckPO"].Index)
            //{
            //    bool isChecked = Convert.ToBoolean(dgvPO.Rows[e.RowIndex].Cells["ChckPO"].Value);
            //    var cellValue = dgvPO.Rows[e.RowIndex].Cells["PONNo"].Value;

            //    if (cellValue != null)
            //    {
            //        string poNo = cellValue.ToString();
            //        var selectedItems = receivingController.ReceivingItems(poNo);

            //        if (isChecked)
            //        {
            //            AddPOItems(selectedItems);
            //        }
            //        else
            //        {
            //            RemovePOItems(selectedItems);
            //            locationList.RemoveAll(s => s.PONo == poNo);
            //        }

            //        dgvReceivingItems.DataSource = aggregatedTable;
            //        dgvReceivingItems.Refresh();
            //    }
            //    else
            //    {
            //        MessageBox.Show("The PONo value is null. Please check the data.");
            //    }
            //}
        }

        private void btnLocDel_Click(object sender, EventArgs e)
        {
            if (dgvLocation.Rows.Count <= 0)
            {
                Helper.Confirmator("Please select location to delete.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
                if (MessageBox.Show("Are you sure you want to delete location " + dgvLocation.CurrentRow.Cells["LocationBin"].Value.ToString().Trim() + "?" , "System Confimation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    int rowIndex = dgvLocation.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgvLocation.Rows[rowIndex];
                    foreach (DataGridViewRow row in dgvLocation.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            DataRowView rowView = row.DataBoundItem as DataRowView;
                            if (rowView != null)
                            {
                                string bindid = dgvLocation.CurrentRow.Cells["BinIDd"].Value.ToString().Trim();
                                rowView.Row.Delete();
                                temporaryLoc.RemoveAll(x => x.PartNo.Trim() == dgvReceivingItems.CurrentRow.Cells["PartNo"].Value.ToString().Trim() && x.BinID == bindid);
                                getReceivedSum();
                            }
                        }
                    }
                    dgvLocation.Refresh();

                }

            }
        }

        private void customDateTime1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chkRushP_CheckedChanged(object sender, EventArgs e)
        {
            if (!isEditingRR)
            {
                _termsDictionary.Clear();
                cmbTerms.DataSource = null;
                cmbTerms.Text = "";
                if (chkRushP.Checked)
                {
                    ClearTables();
                    ClearFields();
                    EnableTextBoxes(true);
                    txtRRN.Textt = "NEW RECORD";
                    _supplierDictionary = receivingController.getSupplier(true);
                    cmbSupplier.DataSource = new BindingSource(_supplierDictionary, null);
                    cmbSupplier.DisplayMember = "Value";
                    cmbSupplier.ValueMember = "Key";

                    _termsDictionary = receivingController.getTermsIfSelectedRR(txtRRN.Textt, true);
                    cmbTerms.DataSource = new BindingSource(_termsDictionary, null);
                    cmbTerms.DisplayMember = "Value";
                    cmbTerms.ValueMember = "Key";
                    isRushOrder = true;
                }
                else
                {
                    ClearAll();
                    _supplierDictionary = receivingController.getSupplier(false);
                    cmbSupplier.DataSource = new BindingSource(_supplierDictionary, null);
                    cmbSupplier.DisplayMember = "Value";
                    cmbSupplier.ValueMember = "Key";
                    isRushOrder = false;
                }
            }
            
        }

        private void dgvLocation_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(dgvLocation_KeyPress);
            if (dgvLocation.CurrentCell.ColumnIndex == dgvLocation.Columns["Qty"].Index)
            {
                e.Control.KeyPress += new KeyPressEventHandler(dgvLocation_KeyPress);
                e.Control.KeyPress += Helper.Numeric_KeyPress;
            }
        }

        private void dgvLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
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

        private void btnSearchRR_Click(object sender, EventArgs e)
        {
            frm_po_receiving_rr_selection frm_rrSelection = new frm_po_receiving_rr_selection();
            frm_rrSelection.DataPassed += OnDataPassed;
            frm_rrSelection.ShowDialog(this);
        }



        /*private void dgvOrderList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
        }*/
    }
}

public class DataRowEventArgs : EventArgs
{
    public DataRow Row { get; }
    public DataRowEventArgs(DataRow row)
    {
        Row = row;
    }
}

