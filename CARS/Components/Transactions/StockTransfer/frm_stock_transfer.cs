using CARS.Controller.Transactions;
using CARS.Customized_Components;
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

namespace CARS.Components.Transactions.StockTransfer
{
    public partial class frm_stock_transfer : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private TransactionController _TransactionController = new TransactionController();
        private StockTransferController _StockTransferController = new StockTransferController();
        private StockTransferModel _StockTransferModel = new StockTransferModel();
        private SortedDictionary<string, string> _ReasonDictionary = new SortedDictionary<string, string>();

        public frm_stock_transfer(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderDetail.BackColor = PnlHeaderTable.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblDetail.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _ReasonDictionary = _TransactionController.GetDictionary("Reason");
            ComboReason.DataSource = new BindingSource(_ReasonDictionary, null);
            ComboReason.DisplayMember = "Key";
            ComboReason.ValueMember = "Value";
            dashboardCall = DashboardCall;
        }

        private void BtnArchive_Click(object sender, EventArgs e)
        {
            frm_stock_transfer_archive archiveStockTransfer = new frm_stock_transfer_archive();
            archiveStockTransfer.ShowDialog(this);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear all fields?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                ClearData();
            }
        }

        private void ClearData()
        {
            ComboReason.SelectedIndex = 0;
            RadioWarehouse.Select();
            DataGridParts.Rows.Clear();
        }

        private void BtnAddParts_Click(object sender, EventArgs e)
        {
            DataGridParts.Focus();
            List<string> PartData = new List<string>();
            frm_stock_transfer_parts_encode partEncode = new frm_stock_transfer_parts_encode(PartData);
            partEncode.StringArraySent += ReceiveArrayFromChild;
            partEncode.ShowDialog(this);
        }

        private void ReceiveArrayFromChild(List<dynamic[]> stringArray)
        {
            for (int i = 0; i != stringArray.Count; i++)
            {
                DataGridParts.Rows.Add(false, stringArray[i][0], stringArray[i][1], stringArray[i][2], stringArray[i][3], stringArray[i][4]);
            }
        }

        private void BtnDeleteParts_Click(object sender, EventArgs e)
        {
            bool hasCheckedCell = DataGridParts.Rows.Cast<DataGridViewRow>().Any(row => Convert.ToBoolean(row.Cells["IsSelected"].Value));
            switch (hasCheckedCell)
            {
                case true:
                    if (DataGridParts.CurrentRow != null && Helper.Confirmator("Are you sure you want to deleted the selected rows?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        for (int i = DataGridParts.Rows.Count - 1; i >= 0; i--)
                        {
                            if (Convert.ToBoolean(DataGridParts.Rows[i].Cells[0].Value))
                            {
                                DataGridParts.Rows.RemoveAt(i);
                            }
                        }
                    }
                    break;

                case false:
                    if (DataGridParts.CurrentRow != null)
                    {
                        if (Helper.Confirmator("Are you sure you want to deleted the selected row?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            DataGridParts.Rows.RemoveAt(DataGridParts.CurrentRow.Index);
                        }
                    }
                    else
                    {
                        MessageBox.Show("There is no row selected for deletion.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
            }
        }

        private void DataGridParts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int Qty = DataGridParts.Columns["Qty"].Index;
            if (e.ColumnIndex == Qty && DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
            {
                decimal price = 0;
                if (DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Contains("."))
                {
                    price = Convert.ToDecimal(DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                }
                else
                {
                    if (DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Length > 15)
                    {
                        price = Convert.ToDecimal(DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Substring(0, 15));
                    }
                    else
                    {
                        price = Convert.ToDecimal(DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    }
                }
                DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = price;

            }
            else if (e.ColumnIndex == Qty && DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
            {
                DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0.00";
            }
        }

        private void DataGridParts_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == DataGridParts.Columns["BinName"].Index && e.RowIndex != -1)
            {
                List<string> BinData = new List<string>();
                foreach (DataGridViewRow row in DataGridParts.Rows)
                {
                    if (row.Cells["BinID"].Value != null && row.Cells["PartNo"].Value.ToString() == DataGridParts.CurrentRow.Cells["PartNo"].Value.ToString())
                    {
                        BinData.Add(row.Cells["BinID"].Value.ToString());
                    }
                }
                frm_stock_transfer_warehouse_encode binEncode = new frm_stock_transfer_warehouse_encode(DataGridParts.Rows[e.RowIndex].Cells["PartNo"].Value.ToString(), BinData);
                binEncode.LocationArray += ReceiveLocation;
                binEncode.ShowDialog(this);
            }
            else if (e.ColumnIndex == DataGridParts.Columns["BinToName"].Index && e.RowIndex != -1)
            {
                List<string> BinData = new List<string>();
                foreach (DataGridViewRow row in DataGridParts.Rows)
                {
                    if (row.Cells["BinToID"].Value != null && row.Cells["PartNo"].Value.ToString() == DataGridParts.CurrentRow.Cells["PartNo"].Value.ToString())
                    {
                        BinData.Add(row.Cells["BinToID"].Value.ToString());
                    }
                }

                string Warehouse = DataGridParts.Rows[e.RowIndex].Cells["WhID"].Value?.ToString() ?? string.Empty;
                int TransferType = 1; //Warehouse to warehouse
                if (RadioBin.Checked)
                {
                    TransferType = 2;
                }
                frm_stock_transfer_warehouse_encode_to binToEncode = new frm_stock_transfer_warehouse_encode_to(DataGridParts.Rows[e.RowIndex].Cells["BinID"].Value?.ToString() ?? string.Empty,
                    Warehouse, TransferType, BinData);
                binToEncode.LocationArray += ReceiveLocationTo;
                binToEncode.ShowDialog(this);
            }
        }

        private void ReceiveLocation(List<dynamic> stringArray)
        {
            if (stringArray.FirstOrDefault() != null)
            {
                var matchingRows = DataGridParts.Rows
                                .Cast<DataGridViewRow>()
                                .Where(r => r.Cells["PartNo"].Value?.ToString() == DataGridParts.CurrentRow.Cells["PartNo"].Value?.ToString() &&
                                       stringArray[0][2] == r.Cells["LotNo"].Value?.ToString() && stringArray[0][3] == r.Cells["BinID"].Value?.ToString()).ToList();
                if (matchingRows.Count() == 0)
                {
                    DataGridParts.CurrentRow.Cells["BinName"].Value = stringArray[0][0];
                    DataGridParts.CurrentRow.Cells["WhName"].Value = stringArray[0][1];
                    DataGridParts.CurrentRow.Cells["LotNo"].Value = stringArray[0][2];
                    DataGridParts.CurrentRow.Cells["BinID"].Value = stringArray[0][3];
                    DataGridParts.CurrentRow.Cells["WhID"].Value = stringArray[0][4];
                    if (RadioBin.Checked)
                    {
                        DataGridParts.CurrentRow.Cells["WhToName"].Value = stringArray[0][1];
                        DataGridParts.CurrentRow.Cells["WhToID"].Value = stringArray[0][4];
                    }
                    else
                    {
                        DataGridParts.CurrentRow.Cells["BinToName"].Value = "";
                        DataGridParts.CurrentRow.Cells["BinToID"].Value = "";
                        DataGridParts.CurrentRow.Cells["WhToName"].Value = "";
                        DataGridParts.CurrentRow.Cells["WhToID"].Value = "";
                    }
                }
                else
                {
                    MessageBox.Show("The selected location is already used for this part no for this transaction.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
        }

        private void ReceiveLocationTo(List<dynamic> stringArray)
        {
            if (stringArray.FirstOrDefault() != null)
            {
                var matchingRows = DataGridParts.Rows
                                .Cast<DataGridViewRow>()
                                .Where(r => r.Cells["PartNo"].Value?.ToString() == DataGridParts.CurrentRow.Cells["PartNo"].Value?.ToString() &&
                                       stringArray[0][2] == r.Cells["LotNo"].Value?.ToString() && stringArray[0][3] == r.Cells["BinToID"].Value?.ToString()).ToList();
                if (matchingRows.Count() == 0)
                {
                    DataGridParts.CurrentRow.Cells["BinToName"].Value = stringArray[0][0];
                    DataGridParts.CurrentRow.Cells["WhToName"].Value = stringArray[0][1];
                    DataGridParts.CurrentRow.Cells["BinToID"].Value = stringArray[0][2];
                    DataGridParts.CurrentRow.Cells["WhToID"].Value = stringArray[0][3];
                }
                else
                {
                    MessageBox.Show("The selected location is already used for this part no for this transaction.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (DataGridParts.Rows.Count > 0)
            {
                bool AllowedSave = true;
                foreach (DataGridViewRow row in DataGridParts.Rows)
                {
                    string locationFrom = row.Cells["BinName"].Value != null ? row.Cells["BinName"].Value.ToString() : string.Empty;
                    string locationTo = row.Cells["BinToName"].Value != null ? row.Cells["BinToName"].Value.ToString() : string.Empty;
                    object qty = row.Cells["Qty"].Value;
                    if (ComboReason.SelectedIndex == 0 || locationFrom == "" || locationTo == "" || qty == null ||  Convert.ToDouble(qty ?? 0.00) == 0.00)
                    {
                        AllowedSave = false;
                        break;
                    }
                }

                if (AllowedSave)
                {
                    if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        string CustomMsg = "";
                        List<StockTransferDetail> DetailsList = new List<StockTransferDetail>();
                        foreach (DataGridViewRow DetailsRow in DataGridParts.Rows)
                        {
                            StockTransferDetail Details = new StockTransferDetail
                            {
                                PartNo = DetailsRow.Cells["PartNo"].Value.ToString(),
                                LotNo = DetailsRow.Cells["LotNo"].Value.ToString(),
                                Qty = Convert.ToDecimal(DetailsRow.Cells["Qty"].Value),
                                FromWhID = DetailsRow.Cells["WhID"].Value.ToString(),
                                ToWhID = DetailsRow.Cells["WhToID"].Value.ToString(),
                                FromBinID = DetailsRow.Cells["BinID"].Value.ToString(),
                                ToBinID = DetailsRow.Cells["BinToID"].Value.ToString(),
                                Status = 1,
                                FromBinName = DetailsRow.Cells["BinName"].Value.ToString(),
                            };
                            DetailsList.Add(Details);
                        }
                        //CtrlNo Generation will be handled on service
                        int type = RadioWarehouse.Checked ? 1 : 2;
                        _StockTransferModel = new StockTransferModel
                        {
                            TransferType = type,
                            ReasonID = ComboReason.SelectedValue.ToString().TrimEnd(),
                            Status = 1,
                            DetailsList = DetailsList
                        };
                        CustomMsg = _StockTransferController.Create(_StockTransferModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (CustomMsg == "Information saved successfully")
                        {
                            ClearData();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please fill all the parts, location from, location to and transfer quantity before saving.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please add a record with corresponding parts, location from, location to and transfer quantity before saving", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DataGridParts_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int Qty = DataGridParts.Columns["Qty"].Index;
            if (DataGridParts.CurrentCell.ColumnIndex == Qty)
            {
                e.Control.KeyPress += Numeric_KeyPress;
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

        private void TransferType_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectedRadioButton = sender as RadioButton;
            if (selectedRadioButton != null)
            {
                if (selectedRadioButton.Checked)
                {
                    if (selectedRadioButton == RadioWarehouse)
                    {
                        foreach (DataGridViewRow row in DataGridParts.Rows)
                        {
                            row.Cells["BinToName"].Value = null;
                            row.Cells["WhToName"].Value = null;
                            row.Cells["BinToID"].Value = null;
                            row.Cells["WhToID"].Value = null;
                        }
                    }
                    else if (selectedRadioButton == RadioBin)
                    {
                        foreach (DataGridViewRow row in DataGridParts.Rows)
                        {
                            if (row.Cells["WhName"].Value != null)
                            {
                                row.Cells["WhToName"].Value = row.Cells["WhName"].Value.ToString();
                                row.Cells["WhToID"].Value = row.Cells["WhID"].Value.ToString();
                                row.Cells["BinToName"].Value = null;
                                row.Cells["BinToID"].Value = null;
                            }
                        }
                    }
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }

        private void DataGridParts_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int SelectionIndex = DataGridParts.Columns["IsSelected"].Index;
            if (e.ColumnIndex == SelectionIndex)
            {
                bool AllSelected = false;
                var uncheckedRows = DataGridParts.Rows.Cast<DataGridViewRow>()
                                    .Where(row => !(bool)row.Cells["IsSelected"].Value);

                if (uncheckedRows.Any())
                {
                    AllSelected = true;
                }

                foreach (DataGridViewRow row in DataGridParts.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["IsSelected"] as DataGridViewCheckBoxCell;
                    if (!checkBoxCell.ReadOnly)
                    {
                        checkBoxCell.Value = AllSelected;
                    }
                }
                DataGridParts.EndEdit();
            }
        }
    }
}
