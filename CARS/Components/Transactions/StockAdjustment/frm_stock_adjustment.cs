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

namespace CARS.Components.Transactions.StockAdjustment
{
    public partial class frm_stock_adjustment : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private TransactionController _TransactionController = new TransactionController();
        private StockAdjustmentController _StockAdjustmentController = new StockAdjustmentController();
        private StockAdjustmentModel _StockAdjustmentModel = new StockAdjustmentModel();
        private SortedDictionary<string, string> _ReasonDictionary = new SortedDictionary<string, string>();

        public frm_stock_adjustment(Action DashboardCall)
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
            frm_stock_adjustment_archive archiveStockAdj = new frm_stock_adjustment_archive();
            archiveStockAdj.ShowDialog(this);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear all the rows selected during the current session?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                ClearData();
            }
        }

        private void ClearData()
        {
            TxtRemarks.Textt = "";
            ComboReason.SelectedIndex = 0;
            DataGridParts.Rows.Clear();
        }

        private void BtnAddParts_Click(object sender, EventArgs e)
        {
            DataGridParts.Focus();
            List<string> PartData = new List<string>();
            //foreach (DataGridViewRow row in DataGridParts.Rows)
            //{
            //    PartData.Add(row.Cells[DataGridParts.Columns["PartNo"].Index].Value.ToString());
            //}
            frm_stock_adjustment_parts_encode partEncode = new frm_stock_adjustment_parts_encode(PartData);
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
                    if (DataGridParts.CurrentRow != null && Helper.Confirmator("Are you sure you want to delete the selected rows?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
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
                        if (Helper.Confirmator("Are you sure you want to delete the selected row?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
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
            int TakeUpIndex = DataGridParts.Columns["TakeUpQty"].Index;
            int DropIndex = DataGridParts.Columns["DropQty"].Index;
            if ((e.ColumnIndex == TakeUpIndex || e.ColumnIndex == DropIndex) && DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null &&
                DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
            {
                decimal price = 0;
                if (DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Contains("."))
                {
                    price = Convert.ToDecimal(DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() ?? "0");
                }
                else
                {
                    if (DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Length > 6)
                    {
                        price = Convert.ToDecimal(DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value??"0".ToString().Substring(0, 6));
                    }
                    else
                    {
                        price = Convert.ToDecimal(DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() ?? "0");
                    }
                }
                DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = price;
                DataGridParts.Rows[e.RowIndex].Cells[DropIndex].ReadOnly = (price != 0 && e.ColumnIndex == TakeUpIndex);
                DataGridParts.Rows[e.RowIndex].Cells[TakeUpIndex].ReadOnly = (price != 0 && e.ColumnIndex == DropIndex);
            }
            else if ((e.ColumnIndex == TakeUpIndex || e.ColumnIndex == DropIndex) && DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && 
                DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
            {
                DataGridParts.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0.00";
                DataGridParts.Rows[e.RowIndex].Cells[TakeUpIndex].ReadOnly = DataGridParts.Rows[e.RowIndex].Cells[DropIndex].ReadOnly = false;
            }
        }

        private void DataGridParts_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //int u = e.ColumnIndex;
            //int i = DataGridParts.Columns["BinName"].Index;
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
                frm_stock_adjustment_warehouse_encode partEncode = new frm_stock_adjustment_warehouse_encode(DataGridParts.Rows[e.RowIndex].Cells["PartNo"].Value.ToString(), BinData);
                partEncode.LocationArray += ReceiveLocation;
                partEncode.ShowDialog(this);
            }
            else if (e.ColumnIndex == DataGridParts.Columns["LotNo"].Index && e.RowIndex != -1)
            {
                frm_stock_adjustment_lotno_encode lotEncode = new frm_stock_adjustment_lotno_encode(DataGridParts.Rows[e.RowIndex].Cells["PartNo"].Value.ToString());
                lotEncode.LotArray += ReceiveLot;
                lotEncode.ShowDialog(this);
            }
        }

        private void ReceiveLocation(List<dynamic> stringArray)
        {
            if (stringArray.FirstOrDefault() != null)
            {
                var matchingRows = DataGridParts.Rows
                                .Cast<DataGridViewRow>()
                                .Where(r => r.Cells["PartNo"].Value?.ToString() == DataGridParts.CurrentRow.Cells["PartNo"].Value?.ToString() &&
                                       stringArray[0][2] == r.Cells["LotNo"].Value?.ToString() && stringArray[0][4] == r.Cells["BinID"].Value?.ToString()).ToList();
                if (matchingRows.Count() == 0)
                {
                    DataGridParts.CurrentRow.Cells["BinName"].Value = stringArray[0][0];
                    DataGridParts.CurrentRow.Cells["WhName"].Value = stringArray[0][1];
                    DataGridParts.CurrentRow.Cells["BinID"].Value = stringArray[0][2];
                    DataGridParts.CurrentRow.Cells["WhID"].Value = stringArray[0][3];
                }
                else
                {
                    MessageBox.Show("The selected location is already used for this part no for this transaction.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
        }

        private void ReceiveLot(List<dynamic> stringArray)
        {
            if (stringArray.FirstOrDefault() != null)
            {
                DataGridParts.CurrentRow.Cells["LotNo"].Value = stringArray[0][0];
                DataGridParts.CurrentRow.Cells["UnitPrice"].Value = stringArray[0][1];
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (DataGridParts.Rows.Count > 0)
            {
                bool AllowedSave = true;
                foreach (DataGridViewRow row in DataGridParts.Rows)
                {
                    string locationValue = row.Cells["BinName"].Value != null ? row.Cells["BinName"].Value.ToString() : string.Empty;
                    string lotValue = row.Cells["LotNo"].Value != null ? row.Cells["LotNo"].Value.ToString() : string.Empty;
                    object takeupValue = row.Cells["TakeUpQty"].Value;
                    object dropValue = row.Cells["DropQty"].Value;
                    if (ComboReason.SelectedIndex == 0 || locationValue == "" || (takeupValue == null && dropValue == null) || lotValue == "" ||
                        (Convert.ToDouble(takeupValue ?? 0.00) == 0.00 && Convert.ToDouble(dropValue ?? 0.00) == 0.00))
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
                        List<StockAdjustmentDetail> DetailsList = new List<StockAdjustmentDetail>();
                        foreach (DataGridViewRow DetailsRow in DataGridParts.Rows)
                        {
                            decimal takeup = DetailsRow.Cells["TakeUpQty"].Value as decimal? ?? 0;
                            decimal drop = DetailsRow.Cells["DropQty"].Value as decimal? ?? 0;
                            StockAdjustmentDetail Details = new StockAdjustmentDetail
                            {
                                PartNo = DetailsRow.Cells["PartNo"].Value.ToString(),
                                TakeUpQty = takeup,
                                DropQty = drop,
                                LotNo = DetailsRow.Cells["LotNo"].Value.ToString(),
                                WhID = DetailsRow.Cells["WhID"].Value.ToString(),
                                BinID = DetailsRow.Cells["BinID"].Value.ToString(),
                            };
                            DetailsList.Add(Details);
                        }
                        //AdjNo Generation will be handled on service
                        _StockAdjustmentModel = new StockAdjustmentModel { ReasonID = ComboReason.SelectedValue.ToString().TrimEnd(), Remarks = TxtRemarks.Textt.TrimEnd(), Status = 1,
                                                                            DetailsList = DetailsList };
                        CustomMsg = _StockAdjustmentController.Create(_StockAdjustmentModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (CustomMsg == "Information saved successfully")
                        {
                            ClearData();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please fill all parts with a location, Lot No., and either takeup/drop quantity before saving.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please fill all parts with a location, Lot No., and either takeup/drop quantity before saving.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DataGridParts_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int TakeupIndex = DataGridParts.Columns["TakeUpQty"].Index;
            int DropIndex = DataGridParts.Columns["DropQty"].Index;
            if (DataGridParts.CurrentCell.ColumnIndex == TakeupIndex || DataGridParts.CurrentCell.ColumnIndex == DropIndex)
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
