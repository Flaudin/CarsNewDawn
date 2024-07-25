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

namespace CARS.Components.Transactions.BeginningBalance
{
    public partial class frm_beginning_balance : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private BeginningBalanceController _BeginningBalanceController = new BeginningBalanceController();
        private BeginningBalanceModel _BeginningBalanceModel = new BeginningBalanceModel();

        public frm_beginning_balance(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderLocation.BackColor = PnlHeaderDetails.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblLocation.ForeColor = LblDetails.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            dashboardCall = DashboardCall;
        }

        private void BtnAddParts_Click(object sender, EventArgs e)
        {
            DataGridBeginningBalance.Focus();
            List<string> PartData = new List<string>();
            foreach (DataGridViewRow row in DataGridBeginningBalance.Rows)
            {
                PartData.Add(row.Cells[DataGridBeginningBalance.Columns["PartNo"].Index].Value.ToString());
            }
            frm_beginning_balance_parts_encode partEncode = new frm_beginning_balance_parts_encode(PartData);
            partEncode.StringArraySent += ReceiveArrayFromChild;
            partEncode.ShowDialog(this);
        }

        private void ReceiveArrayFromChild(List<dynamic[]> stringArray)
        {
            for (int i = 0; i != stringArray.Count; i++)
            {
                DataGridViewRow row = DataGridBeginningBalance.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r => r.Cells["PartNo"].Value?.ToString() == stringArray[i][0]);
                if (row == null)
                {
                    DataGridBeginningBalance.Rows.Add(false, stringArray[i][0], stringArray[i][1], stringArray[i][2], stringArray[i][3],
                                                            stringArray[i][4], stringArray[i][5], stringArray[i][6]);
                }
            }
        }

        private void BtnDeleteParts_Click(object sender, EventArgs e)
        {
            bool hasCheckedCell = DataGridBeginningBalance.Rows.Cast<DataGridViewRow>().Any(row => Convert.ToBoolean(row.Cells["IsSelected"].Value));
            switch (hasCheckedCell)
            {
                case true:
                    if (DataGridBeginningBalance.CurrentRow != null && Helper.Confirmator("Are you sure you want to deleted the selected rows?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        for (int i = DataGridBeginningBalance.Rows.Count - 1; i >= 0; i--)
                        {
                            if (Convert.ToBoolean(DataGridBeginningBalance.Rows[i].Cells[0].Value))
                            {
                                for (int j = DataGridLocation.Rows.Count - 1; j >= 0; j--)
                                {
                                    if (DataGridLocation.Rows[j].Cells[DataGridLocation.Columns["PartNoLocation"].Index].Value.ToString() ==
                                        DataGridBeginningBalance.Rows[i].Cells[DataGridBeginningBalance.Columns["PartNo"].Index].Value.ToString())
                                    {
                                        DataGridLocation.Rows.RemoveAt(j);
                                    }
                                }
                                DataGridBeginningBalance.Rows.RemoveAt(i);
                            }
                        }
                    }
                    break;

                case false:
                    if (DataGridBeginningBalance.CurrentRow != null)
                    {
                        if (Helper.Confirmator("Are you sure you want to deleted the selected row?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            for (int j = DataGridLocation.Rows.Count - 1; j >= 0; j--)
                            {
                                if (DataGridLocation.Rows[j].Cells[DataGridLocation.Columns["PartNoLocation"].Index].Value.ToString() ==
                                    DataGridBeginningBalance.CurrentRow.Cells["PartNo"].Value.ToString())
                                {
                                    DataGridLocation.Rows.RemoveAt(j);
                                }
                            }
                            DataGridBeginningBalance.Rows.RemoveAt(DataGridBeginningBalance.CurrentRow.Index);
                        }
                    }
                    else
                    {
                        MessageBox.Show("There is no row selected for deletion.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
            }
        }

        private void BtnAddLocation_Click(object sender, EventArgs e)
        {
            if (DataGridBeginningBalance.CurrentRow != null)
            {
                DataGridLocation.Focus();
                List<string> LocationData = new List<string>();
                foreach (DataGridViewRow row in DataGridLocation.Rows)
                {
                    if (row.Visible)
                    {
                        LocationData.Add(row.Cells[DataGridLocation.Columns["BinName"].Index].Value.ToString());
                    }
                }
                frm_beginning_balance_warehouse_encode locationEncode = new frm_beginning_balance_warehouse_encode(LocationData, 
                    DataGridBeginningBalance.CurrentRow.Cells[DataGridBeginningBalance.Columns["PartNo"].Index].Value.ToString().TrimEnd());
                locationEncode.StringArraySent += ReceiveArrayFromLocation;
                locationEncode.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Please add a part before selecting a location.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ReceiveArrayFromLocation(List<dynamic[]> stringArray)
        {
            for (int i = 0; i != stringArray.Count; i++)
            {
                DataGridViewRow dgvrow = DataGridLocation.Rows
                    .Cast<DataGridViewRow>().Where(row => row.Visible)
                    .FirstOrDefault(r => r.Cells["BinName"].Value?.ToString() == stringArray[i][0]);
                if (dgvrow == null)
                {
                    DataGridLocation.Rows.Add(false, stringArray[i][0], stringArray[i][1], stringArray[i][2], stringArray[i][3],
                                                  stringArray[i][4], Convert.ToDecimal(0), DataGridBeginningBalance.CurrentRow.Cells["PartNo"].Value.ToString());
                }
            }
        }

        private void DataGridLocation_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int editIndex = DataGridLocation.Columns["QtyLocation"].Index;
            if (DataGridLocation.CurrentCell.ColumnIndex == editIndex)
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

        private void DataGridLocation_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int editIndex = DataGridLocation.Columns["QtyLocation"].Index;
            decimal totalValue = 0;
            if (e.ColumnIndex == editIndex && DataGridLocation.Rows[e.RowIndex].Cells[editIndex].Value.ToString() != "")
            {
                decimal price = Convert.ToDecimal(DataGridLocation.Rows[e.RowIndex].Cells[editIndex].Value);
                DataGridLocation.Rows[e.RowIndex].Cells[editIndex].Value = price;

                foreach (DataGridViewRow row in DataGridLocation.Rows)
                {
                    if (row.Visible)
                    {
                        totalValue = totalValue + Convert.ToDecimal(row.Cells["QtyLocation"].Value);
                    }
                }
                DataGridBeginningBalance.CurrentRow.Cells["Qty"].Value = totalValue;
            }
            else if (e.ColumnIndex == editIndex && DataGridLocation.Rows[e.RowIndex].Cells[editIndex].Value.ToString() == "")
            {
                DataGridLocation.Rows[e.RowIndex].Cells[editIndex].Value = "0.00";
                foreach (DataGridViewRow row in DataGridLocation.Rows)
                {
                    if (row.Visible)
                    {
                        totalValue = totalValue + Convert.ToDecimal(row.Cells["QtyLocation"].Value);
                    }
                }
                DataGridBeginningBalance.CurrentRow.Cells["Qty"].Value = totalValue;
            }
        }

        private void DataGridBeginningBalance_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            string filterText = DataGridBeginningBalance.Rows[rowIndex].Cells["PartNo"].Value.ToString() ?? null;

            foreach (DataGridViewRow row in DataGridLocation.Rows)
            {
                bool rowVisible = row.Cells.Cast<DataGridViewCell>()
                    .Any(cell => cell.Value != null && cell.Value.ToString().Contains(filterText));

                row.Visible = rowVisible;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (DataGridBeginningBalance.Rows.Count > 0 && DataGridLocation.Rows.Count > 0)
            {
                bool AllowedSave = true;
                foreach (DataGridViewRow row in DataGridBeginningBalance.Rows)
                {
                    object cellValue = row.Cells["Qty"].Value;
                    if (Convert.ToDouble(cellValue ?? 0.00) == 0.00)
                    {
                        AllowedSave = false;
                        break;
                    }
                }

                if (AllowedSave)
                {
                    if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        string CustomMsg = ""; //PartNoLocation
                        List<BeginningBalanceDetail> DetailsList = new List<BeginningBalanceDetail>();
                        foreach (DataGridViewRow DetailsRow in DataGridBeginningBalance.Rows)
                        {
                            List<BeginningBalanceLocation> LocationList = new List<BeginningBalanceLocation>();
                            foreach (DataGridViewRow LocationRow in DataGridLocation.Rows)
                            {
                                if (LocationRow.Cells[DataGridLocation.Columns["PartNoLocation"].Index].Value.ToString().TrimEnd() == 
                                    DetailsRow.Cells[DataGridBeginningBalance.Columns["PartNo"].Index].Value.ToString())
                                {
                                    BeginningBalanceLocation LocationModel = new BeginningBalanceLocation { WhID = LocationRow.Cells[DataGridLocation.Columns["WhID"].Index].Value.ToString(), 
                                                                                                            BinID = LocationRow.Cells[DataGridLocation.Columns["BinID"].Index].Value.ToString(), 
                                                                                                            Qty = Convert.ToDecimal(LocationRow.Cells[DataGridLocation.Columns["QtyLocation"].Index].Value )};
                                LocationList.Add(LocationModel);
                                }
                            }
                            
                            BeginningBalanceDetail DetailsModel = new BeginningBalanceDetail { UniqueID = Helper.GenerateUID(), PartNo = DetailsRow.Cells[DataGridBeginningBalance.Columns["PartNo"].Index].Value.ToString(),
                                                                                               Qty = Convert.ToDecimal(DetailsRow.Cells[DataGridBeginningBalance.Columns["Qty"].Index].Value), 
                                                                                               UnitPrice = Convert.ToDecimal(DetailsRow.Cells[DataGridBeginningBalance.Columns["UnitPrice"].Index].Value), 
                                                                                               LocationsList = LocationList };
                            DetailsList.Add(DetailsModel);
                        }

                        _BeginningBalanceModel = new BeginningBalanceModel { /*BegBalNo = _BeginningBalanceController.GenerateBeginningBalanceID(),*/ Remarks = TxtRemarks.Textt.TrimEnd(), 
                                                                             Status = 1, DetailsList = DetailsList };
                        CustomMsg = _BeginningBalanceController.Create(_BeginningBalanceModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (CustomMsg == "Information saved successfully")
                        {
                            ClearData();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please fill all the location with quantity before saving.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please add a part and a location with corresponding quantity before saving", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            TxtRemarks.Textt = "";
            DataGridBeginningBalance.Rows.Clear();
            DataGridLocation.Rows.Clear();
        }

        private void BtnDeleteLocation_Click(object sender, EventArgs e)
        {
            bool hasCheckedCell = DataGridLocation.Rows.Cast<DataGridViewRow>().Any(row => Convert.ToBoolean(row.Cells["IsSelectedLoc"].Value));
            if (hasCheckedCell)
            {
                if (Helper.Confirmator("Are you sure you want to deleted the selected rows?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    for (int i = DataGridLocation.Rows.Count - 1; i >= 0; i--)
                    {
                        if (Convert.ToBoolean(DataGridLocation.Rows[i].Cells[0].Value))
                        {
                            DataGridLocation.Rows.RemoveAt(i);
                        }
                    }

                    if (DataGridLocation.Rows.Count > 0)
                    {
                        decimal totalValue = 0;
                        foreach (DataGridViewRow row in DataGridLocation.Rows)
                        {
                            if (row.Visible)
                            {
                                totalValue = totalValue + Convert.ToDecimal(row.Cells["QtyLocation"].Value);
                            }
                        }
                        DataGridBeginningBalance.CurrentRow.Cells["Qty"].Value = totalValue;
                    }
                }
            }
            else
            {
                MessageBox.Show("There are no rows selected for deletion.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnArchive_Click(object sender, EventArgs e)
        {
            frm_beginning_balance_archive archiveBegBal = new frm_beginning_balance_archive();
            archiveBegBal.ShowDialog(this);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }

        private void DataGridBeginningBalance_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int SelectionIndex = DataGridBeginningBalance.Columns["IsSelected"].Index;
            if (e.ColumnIndex == SelectionIndex)
            {
                bool AllSelected = false;
                var uncheckedRows = DataGridBeginningBalance.Rows.Cast<DataGridViewRow>()
                                    .Where(row => !(bool)row.Cells["IsSelected"].Value);

                if (uncheckedRows.Any())
                {
                    AllSelected = true;
                }

                foreach (DataGridViewRow row in DataGridBeginningBalance.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["IsSelected"] as DataGridViewCheckBoxCell;
                    if (!checkBoxCell.ReadOnly)
                    {
                        checkBoxCell.Value = AllSelected;
                    }
                }
                DataGridBeginningBalance.EndEdit();
            }
        }

        private void DataGridLocation_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int SelectionIndex = DataGridLocation.Columns["IsSelectedLoc"].Index;
            if (e.ColumnIndex == SelectionIndex)
            {
                bool AllSelected = false;
                var uncheckedRows = DataGridLocation.Rows.Cast<DataGridViewRow>()
                                    .Where(row => !(bool)row.Cells["IsSelectedLoc"].Value);

                if (uncheckedRows.Any())
                {
                    AllSelected = true;
                }

                foreach (DataGridViewRow row in DataGridLocation.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["IsSelectedLoc"] as DataGridViewCheckBoxCell;
                    if (!checkBoxCell.ReadOnly)
                    {
                        checkBoxCell.Value = AllSelected;
                    }
                }
                DataGridLocation.EndEdit();
            }
        }
    }
}
