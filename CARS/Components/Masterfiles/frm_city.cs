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
    public partial class frm_city : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private CityController _CityController = new CityController();
        private CityModel _CityModel = new CityModel();
        private SortedDictionary<string, string> _ProvinceDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _ProvinceDictionaryFilter = new SortedDictionary<string, string>();
        private DataTable CityTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_city(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderEntry.BackColor = PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblEncode.ForeColor = LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _ProvinceDictionaryFilter = _ProvinceDictionary = _CityController.GetDictionary();
            ComboProvince.DataSource = new BindingSource(_ProvinceDictionary, null);
            ComboProvinceFilter.DataSource = new BindingSource(_ProvinceDictionaryFilter, null);
            ComboProvinceFilter.DisplayMember = ComboProvince.DisplayMember = "Key";
            ComboProvinceFilter.ValueMember = ComboProvince.ValueMember = "Value";
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridCity, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            dashboardCall = DashboardCall;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ClearEncode();
            _CityModel = new CityModel { ProvID = ComboProvinceFilter.SelectedValue.ToString().TrimEnd(), CityName = TxtCityNameFilter.Textt.TrimEnd(), zip_code = TxtZipFilter.Textt.TrimEnd() };
            CityTable = _CityController.dt(_CityModel);
            DataGridCity.DataSource = CityTable;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                ComboProvinceFilter.Text = TxtCityNameFilter.Textt = TxtZipFilter.Textt = "";
                ClearEncode();
                CityTable.Rows.Clear();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtCityID.Textt.TrimEnd() != "" && TxtCityName.Textt.TrimEnd() != "" && ComboProvince.SelectedIndex != 0)
            {
                if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string CustomMsg = "";
                    if (LblEncode.Text != "Entry")
                    {
                        _CityModel = new CityModel { uniqueid = DataGridCity.CurrentRow.Cells["uniqueid"].Value.ToString(), CityID = TxtCityID.Textt.TrimEnd(),
                                                     CityName = TxtCityName.Textt.TrimEnd(), zip_code = TxtZip.Textt.TrimEnd(), ProvID = ComboProvince.SelectedValue.ToString().TrimEnd(),
                                                     with_gma = CheckGma.Checked, IsActive = CheckActive.Checked };
                        CustomMsg = _CityController.Update(_CityModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _CityModel = new CityModel { uniqueid = Helper.GenerateUID(), CityID = TxtCityID.Textt.TrimEnd(), CityName = TxtCityName.Textt.TrimEnd(), zip_code = TxtZip.Textt.TrimEnd(), 
                                                     ProvID = ComboProvince.SelectedValue.ToString().TrimEnd(), with_gma = CheckGma.Checked, IsActive = CheckActive.Checked };
                        CustomMsg = _CityController.Create(_CityModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (CustomMsg == "Information saved successfully" || CustomMsg == "Information updated successfully")
                    {
                        BtnSearch.PerformClick();
                        ComboProvince.Focus();
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

        private void DataGridCity_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var keyToSelect = DataGridCity.Rows[e.RowIndex].Cells["ProvID"].Value?.ToString();
            if (_ProvinceDictionary.TryGetValue(keyToSelect, out string value))
            {
                ComboProvince.SelectedValue = value;
            }
            TxtCityID.Textt = DataGridCity.Rows[e.RowIndex].Cells["CityID"].Value?.ToString().TrimEnd();
            TxtCityName.Textt = DataGridCity.Rows[e.RowIndex].Cells["CityName"].Value?.ToString().TrimEnd();
            TxtZip.Textt = DataGridCity.Rows[e.RowIndex].Cells["zip_code"].Value?.ToString().TrimEnd();
            CheckGma.Checked = Convert.ToBoolean(DataGridCity.Rows[e.RowIndex].Cells["within_gma"].Value);
            CheckActive.Checked = Convert.ToBoolean(DataGridCity.Rows[e.RowIndex].Cells["IsActive"].Value);
            TxtCityID.ReadOnly = true;
            LblEncode.Text = "Edit";
        }

        private void ClearEncode()
        {
            ComboProvince.Text = TxtCityID.Textt = TxtCityName.Textt = TxtZip.Textt = "";
            CheckActive.Checked = true;
            CheckGma.Checked = false;
            TxtCityID.ReadOnly = false;
            LblEncode.Text = "Entry";
        }

        int CurrentCol = 1;
        private void DataGridCity_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int ActiveIndex = DataGridCity.Columns["IsActive"].Index;
            int GmaIndex = DataGridCity.Columns["within_gma"].Index;
            if (!TxtColumnSearch.Visible && CityTable.Rows.Count > 0 && e.ColumnIndex != ActiveIndex && e.ColumnIndex != GmaIndex)
            {
                if (DataGridCity.Rows.Count == 0)
                {
                    CityTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridCity.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = DataGridCity.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = CityTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridCity.DataSource = bs;

                DataGridViewRow row = DataGridCity.CurrentRow;
                if (row != null)
                {
                    var keyToSelect = row.Cells["ProvID"].Value?.ToString();
                    if (_ProvinceDictionary.TryGetValue(keyToSelect, out string value))
                    {
                        ComboProvince.SelectedValue = value;
                    }
                    TxtCityID.Textt = row.Cells["CityID"].Value?.ToString().TrimEnd();
                    TxtCityName.Textt = row.Cells["CityName"].Value?.ToString().TrimEnd();
                    TxtZip.Textt = row.Cells["zip_code"].Value?.ToString().TrimEnd();
                    CheckGma.Checked = Convert.ToBoolean(row.Cells["within_gma"].Value);
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
            DataGridCity.Focus();
        }

        private void frm_city_KeyDown(object sender, KeyEventArgs e)
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
