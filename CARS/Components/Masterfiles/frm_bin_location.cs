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
    public partial class frm_bin_location : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private BinLocationController _BinLocationController = new BinLocationController();
        private BinLocationModel _BinLocationModel = new BinLocationModel();
        private SortedDictionary<string, string> _WarehouseDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _WarehouseDictionaryFilter = new SortedDictionary<string, string>();
        private DataTable BinTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_bin_location(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = BtnClose.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderEntry.BackColor = PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            BtnClose.ForeColor = LblEncode.ForeColor = LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _WarehouseDictionaryFilter = _WarehouseDictionary = _BinLocationController.GetDictionary();
            ComboWarehouse.DataSource = new BindingSource(_WarehouseDictionary, null);
            ComboWarehouseFilter.DataSource = new BindingSource(_WarehouseDictionaryFilter, null);
            ComboWarehouse.DisplayMember = ComboWarehouseFilter.DisplayMember = "Key";
            ComboWarehouse.ValueMember = ComboWarehouseFilter.ValueMember = "Value";
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridBin, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            dashboardCall = DashboardCall;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ClearEncode();
            _BinLocationModel = new BinLocationModel { WhID = ComboWarehouseFilter.SelectedValue.ToString(), BinName = TxtBinNameFilter.Textt.TrimEnd(), };
            BinTable= _BinLocationController.dt(_BinLocationModel);
            DataGridBin.DataSource = BinTable;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                ComboWarehouseFilter.Text = TxtBinNameFilter.Textt = "";
                ClearEncode();
                BinTable.Rows.Clear();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtBin.Textt.TrimEnd() != "" && NumericLength.Value != 0 && NumericHeight.Value != 0 && NumericWidth.Value != 0 && NumericArea.Value != 0 && ComboWarehouse.SelectedIndex != 0)
            {
                if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string CustomMsg = "";
                    if (LblEncode.Text != "Entry")
                    {
                        _BinLocationModel = new BinLocationModel { BinID = DataGridBin.CurrentRow.Cells["BinID"].Value.ToString(), WhID = ComboWarehouse.SelectedValue.ToString(),
                                                                   BinName = TxtBin.Textt.TrimEnd(), BinDesc = TxtDescription.Textt.TrimEnd(), BinLength = NumericLength.Value,
                                                                   BinHeigth = NumericHeight.Value, BinWidth = NumericWidth.Value, BinArea = NumericArea.Value, IsActive = CheckActive.Checked };
                        CustomMsg = _BinLocationController.Update(_BinLocationModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _BinLocationModel = new BinLocationModel { BinID = _BinLocationController.GenerateBinID(), WhID = ComboWarehouse.SelectedValue.ToString(), BinName = TxtBin.Textt.TrimEnd(),
                                                                   BinDesc = TxtDescription.Textt.TrimEnd(), BinLength = NumericLength.Value, BinHeigth = NumericHeight.Value,
                                                                   BinWidth = NumericWidth.Value, BinArea = NumericArea.Value, IsActive = CheckActive.Checked, };
                        CustomMsg = _BinLocationController.Create(_BinLocationModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (CustomMsg == "Information saved successfully" || CustomMsg == "Information updated successfully")
                    {
                        BtnSearch.PerformClick();
                        ComboWarehouse.Focus();
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

        private void DataGridBin_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ComboWarehouse.SelectedValue = DataGridBin.Rows[e.RowIndex].Cells["WhID"].Value.ToString();
            TxtBin.Textt = DataGridBin.Rows[e.RowIndex].Cells["BinName"].Value.ToString();
            TxtDescription.Textt = DataGridBin.Rows[e.RowIndex].Cells["BinDesc"].Value.ToString();
            NumericLength.Value = Convert.ToDecimal(DataGridBin.Rows[e.RowIndex].Cells["BinLength"].Value);
            NumericHeight.Value = Convert.ToDecimal(DataGridBin.Rows[e.RowIndex].Cells["BinHeight"].Value);
            NumericWidth.Value = Convert.ToDecimal(DataGridBin.Rows[e.RowIndex].Cells["BinWidth"].Value);
            NumericArea.Value = Convert.ToDecimal(DataGridBin.Rows[e.RowIndex].Cells["BinArea"].Value);
            CheckActive.Checked = Convert.ToBoolean(DataGridBin.Rows[e.RowIndex].Cells["IsActive"].Value);
            LblEncode.Text = "Edit";
        }

        private void ClearEncode()
        {
            TxtBin.Textt = TxtDescription.Textt = ComboWarehouse.Text = "";
            NumericLength.Value = NumericHeight.Value = NumericWidth.Value = NumericArea.Value = 0;
            CheckActive.Checked = true;
            LblEncode.Text = "Entry";
        }

        int CurrentCol = 1;
        private void DataGridBin_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int LengthIndex = DataGridBin.Columns["BinLength"].Index;
            int HeightIndex = DataGridBin.Columns["BinHeight"].Index;
            int WidthIndex = DataGridBin.Columns["BinWidth"].Index;
            int AreaIndex = DataGridBin.Columns["BinArea"].Index;
            int ActiveIndex = DataGridBin.Columns["IsActive"].Index;
            if (!TxtColumnSearch.Visible && BinTable.Rows.Count > 0 && e.ColumnIndex != LengthIndex && e.ColumnIndex != HeightIndex && 
                e.ColumnIndex != WidthIndex && e.ColumnIndex != AreaIndex && e.ColumnIndex != ActiveIndex)
            {
                if (DataGridBin.Rows.Count == 0)
                {
                    BinTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridBin.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = DataGridBin.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = BinTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridBin.DataSource = bs;

                DataGridViewRow row = DataGridBin.CurrentRow;
                if (row != null)
                {
                    ComboWarehouse.SelectedValue = row.Cells["WhID"].Value.ToString();
                    TxtBin.Textt = row.Cells["BinName"].Value.ToString();
                    TxtDescription.Textt = row.Cells["BinDesc"].Value.ToString();
                    NumericLength.Value = Convert.ToDecimal(row.Cells["BinLength"].Value);
                    NumericHeight.Value = Convert.ToDecimal(row.Cells["BinHeight"].Value);
                    NumericWidth.Value = Convert.ToDecimal(row.Cells["BinWidth"].Value);
                    NumericArea.Value = Convert.ToDecimal(row.Cells["BinArea"].Value);
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
            DataGridBin.Focus();
        }

        private void frm_bin_location_KeyDown(object sender, KeyEventArgs e)
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

        private void ComboWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboWarehouse.SelectedIndex != 0)
            {
                if(_BinLocationController.CheckWarehouse(ComboWarehouse.SelectedValue.ToString()))
                {
                    NumericLength.Value = NumericLength.Maximum;
                    NumericHeight.Value = NumericHeight.Maximum;
                    NumericWidth.Value = NumericWidth.Maximum;
                    NumericArea.Value = NumericArea.Maximum;
                    TxtDescription.Textt = "";
                    NumericLength.Enabled = NumericHeight.Enabled = NumericWidth.Enabled = NumericArea.Enabled = TxtDescription.Enabled = false;
                }
                else
                {
                    NumericLength.Value = NumericHeight.Value = NumericWidth.Value = NumericArea.Value = 0;
                    NumericLength.Enabled = NumericHeight.Enabled = NumericWidth.Enabled = NumericArea.Enabled = TxtDescription.Enabled = true;
                }
            }
        }
    }
}
