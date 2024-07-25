using CARS.Components.Transactions.StockAdjustment;
using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model.Transactions;
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

namespace CARS.Components.Transactions.SalesReturn
{
    public partial class frm_sales_return : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private SalesReturnController _SalesReturnCotroller = new SalesReturnController();
        private SalesReturnModel _SalesReturnModel = new SalesReturnModel();
        private DataTable SalesOrderTable = new DataTable();
        private SortedDictionary<string, string> _SalesOrderDictionary = new SortedDictionary<string, string>();
        private List<string> SalesReturnIDs = new List<string>();

        public frm_sales_return(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderDetail.BackColor = PnlHeaderTable.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblDetail.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _SalesOrderDictionary = _SalesReturnCotroller.GetDictionary();
            ComboSalesOrder.DataSource = new BindingSource(_SalesOrderDictionary, null);
            ComboSalesOrder.DisplayMember = "Key";
            ComboSalesOrder.ValueMember = "Value";
            dashboardCall = DashboardCall;
        }

        private void ComboSalesOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SalesReturnModel = new SalesReturnModel { SONo = ComboSalesOrder.Text };
            SalesOrderTable = _SalesReturnCotroller.dt(_SalesReturnModel);
            DataGridSalesOrderDetails.Rows.Clear();
            if (SalesOrderTable.Rows.Count != 0)
            {
                TxtSODate.Textt = SalesOrderTable.Rows[0]["SODate"].ToString();
                TxtInvoiceNo.Textt = SalesOrderTable.Rows[0]["InvoiceNo"].ToString();
                TxtInvoiceDate.Textt = SalesOrderTable.Rows[0]["InvoiceDate"].ToString();
                TxtCustomer.Textt = SalesOrderTable.Rows[0]["CustName"].ToString();
                TxtAddress.Textt = SalesOrderTable.Rows[0]["CustAdd"].ToString();
                TxtTin.Textt = SalesOrderTable.Rows[0]["CustTin"].ToString();
                TxtSalesman.Textt = SalesOrderTable.Rows[0]["SLName"].ToString();
                TxtReferenceNo.Textt = SalesOrderTable.Rows[0]["InvoiceRefNo"].ToString();
                TxtTerm.Textt = SalesOrderTable.Rows[0]["TermName"].ToString();
                SalesReturnIDs = new List<string> { SalesOrderTable.Rows[0]["SLID"].ToString().TrimEnd(), SalesOrderTable.Rows[0]["SalesmanID"].ToString().TrimEnd() };
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }

        private void BtnAddParts_Click(object sender, EventArgs e)
        {
            if (ComboSalesOrder.SelectedIndex > 0)
            {
                List<string> PartData = new List<string>();
                foreach (DataGridViewRow row in DataGridSalesOrderDetails.Rows)
                {
                    PartData.Add(row.Cells[DataGridSalesOrderDetails.Columns["PartNo"].Index].Value.ToString());
                }
                frm_sales_return_parts_encode partEncode = new frm_sales_return_parts_encode(PartData, ComboSalesOrder.Text);
                partEncode.StringArraySent += ReceiveArrayFromChild;
                partEncode.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Please select a sales order before selecting a part", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ReceiveArrayFromChild(List<dynamic[]> stringArray)
        {
            for (int i = 0; i != stringArray.Count; i++)
            {
                DataGridViewRow row = DataGridSalesOrderDetails.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r => r.Cells["PartNo"].Value?.ToString() == stringArray[i][2]);
                if (row == null)
                {
                    DataGridSalesOrderDetails.Rows.Add(false, stringArray[i][0], stringArray[i][1], stringArray[i][2], stringArray[i][3],
                                                            stringArray[i][4], stringArray[i][5], stringArray[i][6], stringArray[i][7], stringArray[i][8],
                                                            Convert.ToDecimal(stringArray[i][9]), "", stringArray[i][11], stringArray[i][12],
                                                            stringArray[i][13], stringArray[i][14], stringArray[i][15], "");
                }
            }
        }

        private void BtnDeleteParts_Click(object sender, EventArgs e)
        {
            bool hasCheckedCell = DataGridSalesOrderDetails.Rows.Cast<DataGridViewRow>().Any(row => Convert.ToBoolean(row.Cells["IsSelected"].Value));
            switch (hasCheckedCell)
            {
                case true:
                    if (DataGridSalesOrderDetails.CurrentRow != null && Helper.Confirmator("Are you sure you want to deleted the selected rows?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        for (int i = DataGridSalesOrderDetails.Rows.Count - 1; i >= 0; i--)
                        {
                            if (Convert.ToBoolean(DataGridSalesOrderDetails.Rows[i].Cells[0].Value))
                            {
                                DataGridSalesOrderDetails.Rows.RemoveAt(i);
                            }
                        }
                    }
                    break;

                case false:
                    if (DataGridSalesOrderDetails.CurrentRow != null)
                    {
                        if (Helper.Confirmator("Are you sure you want to deleted the selected row?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            DataGridSalesOrderDetails.Rows.RemoveAt(DataGridSalesOrderDetails.CurrentRow.Index);
                        }
                    }
                    else
                    {
                        MessageBox.Show("There is no row selected for deletion.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
            }
        }

        private void DataGridSalesOrderDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal good = Convert.ToDecimal(DataGridSalesOrderDetails.CurrentRow.Cells["GoodQty"].Value);
            decimal defective = Convert.ToDecimal(DataGridSalesOrderDetails.CurrentRow.Cells["DefQty"].Value);
            if ((good + defective) > Convert.ToDecimal(DataGridSalesOrderDetails.CurrentRow.Cells["Qty"].Value))
            {
                DataGridSalesOrderDetails.CurrentCell.Value = 0;
                MessageBox.Show("The quantity provided exceeds the quantity ordered.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DataGridSalesOrderDetails.CurrentRow.Cells["Total"].Value = (good + defective) * Convert.ToDecimal(DataGridSalesOrderDetails.CurrentRow.Cells["ListPrice"].Value);
            }
        }

        private void DataGridSalesOrderDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DataGridSalesOrderDetails.Columns["ReasonName"].Index)
            {
                frm_sales_return_reason reasonEncode = new frm_sales_return_reason("");
                reasonEncode.StringSent += ReceivesStringFromChild;
                reasonEncode.ShowDialog(this);
            }
        }

        private void ReceivesStringFromChild(string reason, string reasonId)
        {
            DataGridSalesOrderDetails.CurrentCell.Value = reason;
            DataGridSalesOrderDetails.CurrentRow.Cells["ReasonID"].Value = reasonId;
        }

        private void DataGridSalesOrderDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int goodIndex = DataGridSalesOrderDetails.Columns["GoodQty"].Index;
            int defIndex = DataGridSalesOrderDetails.Columns["DefQty"].Index;
            if (DataGridSalesOrderDetails.CurrentCell.ColumnIndex == goodIndex || DataGridSalesOrderDetails.CurrentCell.ColumnIndex == defIndex)
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (ComboSalesOrder.SelectedIndex > 0 && DataGridSalesOrderDetails.Rows.Count > 0)
            {
                bool AllowedSave = true;
                foreach (DataGridViewRow row in DataGridSalesOrderDetails.Rows)
                {
                    object goodValue = row.Cells["GoodQty"].Value;
                    object defValue = row.Cells["DefQty"].Value;
                    string reasonValue = row.Cells["ReasonName"].Value.ToString().TrimEnd();
                    if ((Convert.ToDouble(goodValue ?? 0.00) == 0.00 || Convert.ToDouble(defValue ?? 0.00) == 0.00) && reasonValue == "")
                    {
                        AllowedSave = false;
                        break;
                    }
                }

                if (AllowedSave)
                {
                    if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        List<SalesReturnDetailModel> DetailsList = new List<SalesReturnDetailModel>();
                        foreach (DataGridViewRow row in DataGridSalesOrderDetails.Rows)
                        {
                            SalesReturnDetailModel Detail = new SalesReturnDetailModel
                            {
                                ItemNo = row.Cells["ItemNo"].Value.ToString(),
                                PartNo = row.Cells["PartNo"].Value.ToString(),
                                GoodQty = Convert.ToDecimal(row.Cells["GoodQty"].Value),
                                DefectiveQty = Convert.ToDecimal(row.Cells["DefQty"].Value),
                                FreeItem = Convert.ToBoolean(row.Cells["Free"].Value),
                                ItemID = row.Cells["ItemID"].Value.ToString(),
                                Status = 1,
                                LotNo = row.Cells["LotNo"].Value.ToString(),
                                WhID = row.Cells["WhID"].Value.ToString(),
                                BinID = row.Cells["BinID"].Value.ToString(),
                                ReasonID = row.Cells["ReasonID"].Value.ToString(),
                            };
                            DetailsList.Add(Detail);
                        }

                        _SalesReturnModel = new SalesReturnModel
                        {
                            InvoiceNo = TxtInvoiceNo.Textt,
                            SONo = ComboSalesOrder.Text,
                            SLID = SalesReturnIDs.ToList()[0].ToString() == "" ? null : SalesReturnIDs.ToList()[0].ToString(),
                            SalesmanID = SalesReturnIDs.ToList()[1].ToString(),
                            Remarks = TxtRemarks.Textt.TrimEnd(),
                            Status = 1,
                            ReturnDetails = DetailsList
                        };
                        string CustomMsg = _SalesReturnCotroller.Create(_SalesReturnModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (CustomMsg == "Information saved successfully" || CustomMsg == "Information updated successfully")
                        {
                            ClearEncode();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please fill all the required fields and add a part before saving.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please fill all the required fields and add a part before saving.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearEncode();
        }

        private void ClearEncode()
        {
            ComboSalesOrder.SelectedIndex = 0;
            TxtInvoiceNo.Textt = TxtCustomer.Textt = TxtAddress.Textt = TxtTin.Textt = TxtSalesman.Textt = TxtReferenceNo.Textt = TxtTerm.Textt = 
                TxtSODate.Textt = TxtInvoiceDate.Textt = "";
            SalesReturnIDs = new List<string>();
            DataGridSalesOrderDetails.Rows.Clear();
        }

        private void DataGridSalesOrderDetails_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int SelectionIndex = DataGridSalesOrderDetails.Columns["IsSelected"].Index;
            if (e.ColumnIndex == SelectionIndex)
            {
                bool AllSelected = false;
                var uncheckedRows = DataGridSalesOrderDetails.Rows.Cast<DataGridViewRow>()
                                    .Where(row => !(bool)row.Cells["IsSelected"].Value);

                if (uncheckedRows.Any())
                {
                    AllSelected = true;
                }

                foreach (DataGridViewRow row in DataGridSalesOrderDetails.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["IsSelected"] as DataGridViewCheckBoxCell;
                    if (!checkBoxCell.ReadOnly)
                    {
                        checkBoxCell.Value = AllSelected;
                    }
                }
                DataGridSalesOrderDetails.EndEdit();
            }
        }

        private void BtnArchive_Click(object sender, EventArgs e)
        {
            //Bitmap bmp = Helper.ModalEffect(this.PointToScreen(new Point(0, 0)), this.ClientRectangle);

            //using (Panel p = new Panel())
            //{
            //    p.Location = new Point(0, 0);
            //    p.Size = this.ClientRectangle.Size;
            //    p.BackgroundImage = bmp;
            //    this.Controls.Add(p);
            //    p.BringToFront();

            //    var salesReturnArchive = new frm_sales_return_archive();
            //    salesReturnArchive.ShowDialog(this);
            //}
            var salesReturnArchive = new frm_sales_return_archive();
            salesReturnArchive.ShowDialog(this);
        }
    }
}
