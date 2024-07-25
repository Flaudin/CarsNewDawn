using CARS.Controller.Masterfiles;
using CARS.Functions;
using CARS.Model.Masterfiles;
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
    public partial class frm_warehouse : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private WarehouseController _WarehouseController = new WarehouseController();
        private WarehouseModel _WarehouseModel = new WarehouseModel();
        private SortedDictionary<string, string> _EmployeeDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _EmployeeDictionaryFilter = new SortedDictionary<string, string>();
        private DataTable WarehouseTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_warehouse(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderEntry.BackColor = PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblEncode.ForeColor = LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _EmployeeDictionaryFilter = _EmployeeDictionary = _WarehouseController.GetDictionary();
            ComboIncharge.DataSource = new BindingSource(_EmployeeDictionary, null);
            ComboInChargeFilter.DataSource = new BindingSource(_EmployeeDictionaryFilter, null);
            ComboIncharge.DisplayMember = ComboInChargeFilter.DisplayMember = "Key";
            ComboIncharge.ValueMember = ComboInChargeFilter.ValueMember = "Value";
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridWarehouse, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            dashboardCall = DashboardCall;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ClearEncode();
            _WarehouseModel = new WarehouseModel { WhName = TxtWarehouseFilter.Textt.TrimEnd(), WhDesc = TxtDescriptionFilter.Textt.TrimEnd(), WhLocation = TxtLocationFilter.Textt.TrimEnd(), 
                                                   WhInCharge = ComboInChargeFilter.SelectedValue.ToString().TrimEnd() };
            WarehouseTable = _WarehouseController.dt(_WarehouseModel);
            DataGridWarehouse.DataSource = WarehouseTable;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtWarehouseFilter.Textt = TxtDescriptionFilter.Textt = TxtLocationFilter.Textt = ComboInChargeFilter.Text = "";
                ClearEncode();
                WarehouseTable.Rows.Clear();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtWarehouse.Textt.TrimEnd() != "" && NumericArea.Value != 0 && NumericStorage.Value != 0 && ((ComboIncharge.SelectedIndex == 0 && CheckWebstore.Checked) 
                || (ComboIncharge.SelectedIndex != 0 && !CheckWebstore.Checked)))
            {
                if (LblEncode.Text != "Entry" && CheckWebstore.Checked)
                {
                    MessageBox.Show("Cannot modify webstore", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string CustomMsg = "";
                    if (LblEncode.Text != "Entry")
                    {
                        _WarehouseModel = new WarehouseModel { WhID = DataGridWarehouse.CurrentRow.Cells["WhID"].Value.ToString(), WhName= TxtWarehouse.Textt.TrimEnd(), 
                                                               WhDesc = TxtDescription.Textt.TrimEnd(), WhLocation = TxtLocation.Textt.TrimEnd(), AreaSqm = NumericArea.Value, 
                                                               StorageSqm = NumericStorage.Value, WhPriority = NumericPriority.Value, WhInCharge = ComboIncharge.SelectedValue.ToString().TrimEnd(),
                                                               IsActive = CheckActive.Checked };
                        CustomMsg = _WarehouseController.Update(_WarehouseModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _WarehouseModel = new WarehouseModel { WhID = _WarehouseController.GenerateWarehouseID(), WhName = TxtWarehouse.Textt.TrimEnd(), WhDesc = TxtDescription.Textt.TrimEnd(),
                                                               WhLocation = TxtLocation.Textt.TrimEnd(), AreaSqm = NumericArea.Value, StorageSqm = NumericStorage.Value, WhPriority = NumericPriority.Value,
                                                               WhInCharge = ComboIncharge.SelectedValue.ToString().TrimEnd(), IsActive = CheckActive.Checked, IsWebStore = CheckWebstore.Checked };
                        CustomMsg = _WarehouseController.Create(_WarehouseModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                
                    if (CustomMsg == "Information saved successfully" || CustomMsg ==  "Information updated successfully")
                    {
                        BtnSearch.PerformClick();
                        TxtWarehouse.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill all the required fields before saving.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnClearEncode_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Unsaved entries will be discarded. Are you sure you want to clear the input field(s)?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                ClearEncode();
            }
        }

        private void DataGridWarehouse_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            TxtWarehouse.Textt = DataGridWarehouse.Rows[e.RowIndex].Cells["WhName"].Value.ToString();
            TxtDescription.Textt = DataGridWarehouse.Rows[e.RowIndex].Cells["WhDesc"].Value.ToString();
            TxtLocation.Textt = DataGridWarehouse.Rows[e.RowIndex].Cells["WhLocation"].Value.ToString();
            NumericArea.Value = Convert.ToDecimal(DataGridWarehouse.Rows[e.RowIndex].Cells["AreaSqm"].Value);
            NumericStorage.Value = Convert.ToDecimal(DataGridWarehouse.Rows[e.RowIndex].Cells["StorageSqm"].Value);
            NumericPriority.Value = Convert.ToDecimal(DataGridWarehouse.Rows[e.RowIndex].Cells["WhPriority"].Value);
            ComboIncharge.SelectedValue = _EmployeeDictionary.TryGetValue(DataGridWarehouse.Rows[e.RowIndex].Cells["EmployeeName"].Value.ToString(), out string incharge) ? incharge : "";
            CheckActive.Checked = Convert.ToBoolean(DataGridWarehouse.Rows[e.RowIndex].Cells["IsActive"].Value);
            CheckWebstore.Checked = Convert.ToBoolean(DataGridWarehouse.Rows[e.RowIndex].Cells["IsWebStore"].Value);
            CheckWebstore.Enabled = false;
            LblEncode.Text = "Edit";
        }

        private void ClearEncode()
        {
            TxtWarehouse.Textt = TxtLocation.Textt = TxtDescription.Textt = ComboIncharge.Text = "";
            NumericArea.Value = NumericStorage.Value = NumericPriority.Value = 0;
            CheckActive.Checked = true;
            CheckWebstore.Checked = false;
            CheckWebstore.Enabled = true;
            LblEncode.Text = "Entry";
        }

        int CurrentCol = 1;
        private void DataGridWarehouse_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int AreaIndex = DataGridWarehouse.Columns["AreaSqm"].Index;
            int StorageIndex = DataGridWarehouse.Columns["StorageSqm"].Index;
            int PriorityIndex = DataGridWarehouse.Columns["WhPriority"].Index;
            int ActiveIndex = DataGridWarehouse.Columns["IsActive"].Index;
            if (!TxtColumnSearch.Visible && WarehouseTable.Rows.Count > 0 && e.ColumnIndex != AreaIndex && e.ColumnIndex != StorageIndex && 
                e.ColumnIndex != PriorityIndex && e.ColumnIndex != ActiveIndex)
            {
                if (DataGridWarehouse.Rows.Count == 0)
                {
                    WarehouseTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridWarehouse.Columns[e.ColumnIndex].HeaderText;
                TxtColumnSearch.Visible = true;
                TxtColumnSearch.Focus();
            }
        }

        private void TxtColumnSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                TxtColumnSearch.Visible = false;
            }
            else
            {
                string searchCol = DataGridWarehouse.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = WarehouseTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridWarehouse.DataSource = bs;

                DataGridViewRow row = DataGridWarehouse.CurrentRow;
                if (row != null)
                {
                    TxtWarehouse.Textt = row.Cells["WhName"].Value.ToString();
                    TxtDescription.Textt = row.Cells["WhDesc"].Value.ToString();
                    TxtLocation.Textt = row.Cells["WhLocation"].Value.ToString();
                    NumericArea.Value = Convert.ToDecimal(row.Cells["AreaSqm"].Value);
                    NumericStorage.Value = Convert.ToDecimal(row.Cells["StorageSqm"].Value);
                    NumericPriority.Value = Convert.ToDecimal(row.Cells["WhPriority"].Value);
                    ComboIncharge.SelectedValue = _EmployeeDictionary.TryGetValue(row.Cells["EmployeeName"].Value.ToString(), out string incharge) ? incharge : "";
                    CheckActive.Checked = Convert.ToBoolean(row.Cells["IsActive"].Value);
                    LblEncode.Text = "Edit";
                }
                else
                {
                    ClearEncode();
                }
            }
        }

        private void TxtColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtColumnSearch.Visible = false;
            DataGridWarehouse.Focus();
        }

        private void frm_warehouse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !TxtColumnSearch.Visible)
            {
                if (PnlFilter.ContainsFocus)
                {
                    BtnSearch.PerformClick();
                }
                else if (PnlEncode.ContainsFocus)
                {
                    BtnSave.PerformClick();
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

        private void CheckWebstore_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckWebstore.Checked)
            {
                NumericArea.Value = NumericArea.Maximum;
                NumericStorage.Value = NumericStorage.Maximum;
                TxtDescription.Textt = TxtLocation.Textt = "";
                ComboIncharge.SelectedIndex = 0;
                CheckActive.Checked = true;
                NumericArea.Enabled = NumericStorage.Enabled = NumericPriority.Enabled = TxtDescription.Enabled = TxtLocation.Enabled = 
                    ComboIncharge.Enabled = CheckActive.Enabled = false;
            }
            else
            {
                NumericArea.Enabled = NumericStorage.Enabled = NumericPriority.Enabled = TxtDescription.Enabled = TxtLocation.Enabled = 
                    ComboIncharge.Enabled = CheckActive.Enabled = true;
                if (LblEncode.Text == "Entry")
                {
                    NumericArea.Value = NumericStorage.Value = NumericPriority.Value = 0;
                }
            }
        }
    }
}
