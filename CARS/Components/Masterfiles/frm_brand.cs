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
    public partial class frm_brand : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private BrandController _BrandController = new BrandController();
        private BrandModel _BrandModel = new BrandModel();
        private DataTable BrandTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_brand(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderEntry.BackColor = PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblEncode.ForeColor = LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridBrand, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            dashboardCall = DashboardCall;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ClearEncode();
            _BrandModel = new BrandModel { BrandName = TxtBrandNameFilter.Textt.TrimEnd() };
            BrandTable = _BrandController.dt(_BrandModel);
            DataGridBrand.DataSource = BrandTable;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtBrandNameFilter.Textt = "";
                ClearEncode();
                BrandTable.Rows.Clear();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtBrandID.Textt.TrimEnd() != "" && TxtBrandName.Textt.TrimEnd() != "")
            {
                if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string CustomMsg = "";
                    if (LblEncode.Text != "Entry")
                    {
                        _BrandModel = new BrandModel { uniqueid = DataGridBrand.CurrentRow.Cells["uniqueid"].Value.ToString(), BrandID = TxtBrandID.Textt.TrimEnd(),
                                                       BrandName = TxtBrandName.Textt.TrimEnd(), IsActive = CheckActive.Checked };
                        CustomMsg = _BrandController.Update(_BrandModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _BrandModel = new BrandModel { uniqueid = Helper.GenerateUID(), BrandID = TxtBrandID.Textt.TrimEnd(), BrandName = TxtBrandName.Textt.TrimEnd(), IsActive = CheckActive.Checked };
                        CustomMsg = _BrandController.Create(_BrandModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (CustomMsg == "Information saved successfully" || CustomMsg == "Information updated successfully")
                    {
                        BtnSearch.PerformClick();
                        TxtBrandID.Focus();
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

        private void DataGridBrand_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            TxtBrandID.Textt = DataGridBrand.Rows[e.RowIndex].Cells["BrandID"].Value?.ToString().TrimEnd();
            TxtBrandName.Textt = DataGridBrand.Rows[e.RowIndex].Cells["BrandName"].Value?.ToString().TrimEnd();
            CheckActive.Checked = Convert.ToBoolean(DataGridBrand.Rows[e.RowIndex].Cells["IsActive"].Value);
            TxtBrandID.ReadOnly = true;
            LblEncode.Text = "Edit";
        }

        private void ClearEncode()
        {
            TxtBrandID.Textt = TxtBrandName.Textt = "";
            TxtBrandID.ReadOnly = false;
            CheckActive.Checked = true;
            LblEncode.Text = "Entry";
        }

        int CurrentCol = 1;
        private void DataGridBrand_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int ActiveIndex = DataGridBrand.Columns["IsActive"].Index;
            if (!TxtColumnSearch.Visible && BrandTable.Rows.Count > 0 && e.ColumnIndex != ActiveIndex)
            {
                if (DataGridBrand.Rows.Count == 0)
                {
                    BrandTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridBrand.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = DataGridBrand.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = BrandTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridBrand.DataSource = bs;

                DataGridViewRow row = DataGridBrand.CurrentRow;
                if (row != null)
                {
                    TxtBrandID.Textt = row.Cells["BrandID"].Value?.ToString().TrimEnd();
                    TxtBrandName.Textt = row.Cells["BrandName"].Value?.ToString().TrimEnd();
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
            DataGridBrand.Focus();
        }

        private void frm_brand_KeyDown(object sender, KeyEventArgs e)
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
