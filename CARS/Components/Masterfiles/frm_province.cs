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
    public partial class frm_province : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private ProvinceController _ProvinceController = new ProvinceController();
        private ProvinceModel _ProvinceModel = new ProvinceModel();
        private SortedDictionary<string, string> _RegionDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _RegionDictionaryFilter = new SortedDictionary<string, string>();
        private DataTable ProvinceTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_province(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderEntry.BackColor = PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblEncode.ForeColor = LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _RegionDictionary = _RegionDictionaryFilter = _ProvinceController.GetDictionary();
            ComboRegion.DataSource = new BindingSource(_RegionDictionary, null);
            ComboRegionFilter.DataSource = new BindingSource(_RegionDictionaryFilter, null);
            ComboRegion.DisplayMember = ComboRegionFilter.DisplayMember = "Key";
            ComboRegion.ValueMember = ComboRegionFilter.ValueMember = "Value";
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridProvince, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            dashboardCall = DashboardCall;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ClearEncode();
            _ProvinceModel = new ProvinceModel { RegionID = ComboRegionFilter.SelectedValue.ToString().TrimEnd(), ProvName = TxtProvinceNameFilter.Textt.TrimEnd() };
            ProvinceTable = _ProvinceController.dt(_ProvinceModel);
            DataGridProvince.DataSource = ProvinceTable;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                ComboRegionFilter.Text = TxtProvinceNameFilter.Textt = "";
                ClearEncode();
                ProvinceTable.Rows.Clear();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtProvinceID.Textt.TrimEnd() != "" && TxtProvinceName.Textt.TrimEnd() != "" && ComboRegion.SelectedIndex != 0)
            {
                if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string CustomMsg = "";
                    if (LblEncode.Text != "Entry")
                    {
                        _ProvinceModel = new ProvinceModel { uniqueid = DataGridProvince.CurrentRow.Cells["uniqueid"].Value.ToString(), RegionID = ComboRegion.SelectedValue.ToString().TrimEnd(),
                                                             ProvID = TxtProvinceID.Textt.TrimEnd(), ProvName = TxtProvinceName.Textt.TrimEnd(), IsActive = CheckActive.Checked };
                        CustomMsg = _ProvinceController.Update(_ProvinceModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _ProvinceModel = new ProvinceModel { uniqueid = Helper.GenerateUID(), RegionID = ComboRegion.SelectedValue.ToString().TrimEnd(), ProvID = TxtProvinceID.Textt.TrimEnd(),
                                                             ProvName = TxtProvinceName.Textt.TrimEnd(), IsActive = CheckActive.Checked };
                        CustomMsg = _ProvinceController.Create(_ProvinceModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (CustomMsg == "Information saved successfully" || CustomMsg == "Information updated successfully")
                    {
                        BtnSearch.PerformClick();
                        ComboRegion.Focus();
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

        private void DataGridProvince_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var keyToSelect = DataGridProvince.Rows[e.RowIndex].Cells["RegionID"].Value?.ToString();
            if (_RegionDictionary.TryGetValue(keyToSelect, out string value))
            {
                ComboRegion.SelectedValue = value;
            }
            TxtProvinceID.Textt = DataGridProvince.Rows[e.RowIndex].Cells["ProvID"].Value?.ToString().TrimEnd();
            TxtProvinceName.Textt = DataGridProvince.Rows[e.RowIndex].Cells["ProvName"].Value?.ToString().TrimEnd();
            CheckActive.Checked = Convert.ToBoolean(DataGridProvince.Rows[e.RowIndex].Cells["IsActive"].Value);
            TxtProvinceID.ReadOnly = true;
            LblEncode.Text = "Edit";
        }

        private void ClearEncode()
        {
            ComboRegion.Text = TxtProvinceID.Textt = TxtProvinceName.Textt = "";
            CheckActive.Checked = true;
            TxtProvinceID.ReadOnly = false;
            LblEncode.Text = "Entry";
        }

        int CurrentCol = 1;
        private void DataGridProvince_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int ActiveIndex = DataGridProvince.Columns["IsActive"].Index;
            if (!TxtColumnSearch.Visible && ProvinceTable.Rows.Count > 0 && e.ColumnIndex != ActiveIndex)
            {
                if (DataGridProvince.Rows.Count == 0)
                {
                    ProvinceTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridProvince.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = DataGridProvince.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = ProvinceTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridProvince.DataSource = bs;

                DataGridViewRow row = DataGridProvince.CurrentRow;
                if (row != null)
                {
                    var keyToSelect = row.Cells["RegionID"].Value?.ToString();
                    if (_RegionDictionary.TryGetValue(keyToSelect, out string value))
                    {
                        ComboRegion.SelectedValue = value;
                    }
                    TxtProvinceID.Textt = row.Cells["ProvID"].Value?.ToString().TrimEnd();
                    TxtProvinceName.Textt = row.Cells["ProvName"].Value?.ToString().TrimEnd();
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
            DataGridProvince.Focus();
        }

        private void frm_province_KeyDown(object sender, KeyEventArgs e)
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
    }
}
