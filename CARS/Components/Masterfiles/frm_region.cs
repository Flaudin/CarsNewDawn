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
    public partial class frm_region : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private RegionController _RegionController = new RegionController();
        private RegionModel _RegionModel = new RegionModel();
        private DataTable RegionTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_region(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderEntry.BackColor = PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblEncode.ForeColor = LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridRegion, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            dashboardCall = DashboardCall;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ClearEncode();
            _RegionModel = new RegionModel { RegionName = TxtRegionNameFilter.Textt.TrimEnd() };
            RegionTable = _RegionController.dt(_RegionModel);
            DataGridRegion.DataSource = RegionTable;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtRegionNameFilter.Textt = "";
                ClearEncode();
                RegionTable.Rows.Clear();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtRegionID.Textt.TrimEnd() != "" && TxtRegionName.Textt.TrimEnd() != "")
            {
                if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string CustomMsg = "";
                    if (LblEncode.Text != "Entry")
                    {
                        _RegionModel = new RegionModel { uniqueid = DataGridRegion.CurrentRow.Cells["uniqueid"].Value.ToString(), RegionID = TxtRegionID.Textt.TrimEnd(), 
                                                         RegionName = TxtRegionName.Textt.TrimEnd(), IsActive = CheckActive.Checked };
                        CustomMsg = _RegionController.Update(_RegionModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _RegionModel = new RegionModel { uniqueid = Helper.GenerateUID(), RegionID = TxtRegionID.Textt.TrimEnd(), RegionName = TxtRegionName.Textt.TrimEnd(), IsActive = CheckActive.Checked };
                        CustomMsg = _RegionController.Create(_RegionModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (CustomMsg == "Information saved successfully" || CustomMsg == "Information updated successfully")
                    {
                        BtnSearch.PerformClick();
                        TxtRegionID.Focus();
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

        private void DataGridRegion_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            TxtRegionID.Textt = DataGridRegion.Rows[e.RowIndex].Cells["RegionID"].Value?.ToString().TrimEnd();
            TxtRegionName.Textt = DataGridRegion.Rows[e.RowIndex].Cells["RegionName"].Value?.ToString().TrimEnd();
            CheckActive.Checked = Convert.ToBoolean(DataGridRegion.Rows[e.RowIndex].Cells["IsActive"].Value);
            TxtRegionID.ReadOnly = true;
            LblEncode.Text = "Edit";
        }

        private void ClearEncode()
        {
            TxtRegionID.Textt = TxtRegionName.Textt = "";
            CheckActive.Checked = true;
            TxtRegionID.ReadOnly = false;
            LblEncode.Text = "Entry";
        }

        int CurrentCol = 1;
        private void DataGridRegion_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int ActiveIndex = DataGridRegion.Columns["IsActive"].Index;
            if (!TxtColumnSearch.Visible && RegionTable.Rows.Count > 0 && e.ColumnIndex != ActiveIndex)
            {
                if (DataGridRegion.Rows.Count == 0)
                {
                    RegionTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridRegion.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = DataGridRegion.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = RegionTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridRegion.DataSource = bs;

                DataGridViewRow row = DataGridRegion.CurrentRow;
                if (row != null)
                {
                    TxtRegionID.Textt = row.Cells["RegionID"].Value?.ToString().TrimEnd();
                    TxtRegionName.Textt = row.Cells["RegionName"].Value?.ToString().TrimEnd();
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
            DataGridRegion.Focus();
        }

        private void frm_region_KeyDown(object sender, KeyEventArgs e)
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
