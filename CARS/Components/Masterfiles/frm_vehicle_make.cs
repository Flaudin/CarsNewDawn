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
    public partial class frm_vehicle_make : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private VehicleMakeController _VehicleMakeController = new VehicleMakeController();
        private VehicleMakeModel _VehicleMakeModel = new VehicleMakeModel();
        private DataTable MakeTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_vehicle_make(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderEntry.BackColor = PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblEncode.ForeColor = LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridMake, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            dashboardCall = DashboardCall;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ClearEncode();
            _VehicleMakeModel = new VehicleMakeModel { MakeName = TxtVehicleMakeFilter.Textt.TrimEnd() };
            MakeTable = _VehicleMakeController.dt(_VehicleMakeModel);
            DataGridMake.DataSource = MakeTable;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtVehicleMakeFilter.Textt = "";
                ClearEncode();
                MakeTable.Rows.Clear();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtVehicleCode.Textt.TrimEnd() != "" && TxtVehicleMake.Textt.TrimEnd() != "")
            {
                if (LblEncode.Text == "Edit" && (bool)DataGridMake.CurrentRow.Cells["BOwn"].Value)
                {
                    MessageBox.Show("Cannot edit BSB product.", "System Informatin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string CustomMsg = "";
                    if (LblEncode.Text != "Entry")
                    {
                        _VehicleMakeModel = new VehicleMakeModel { MakeID = DataGridMake.CurrentRow.Cells["MakeID"].Value.ToString(), MakeName = TxtVehicleMake.Textt.TrimEnd(), IsActive = CheckActive.Checked };
                        CustomMsg = _VehicleMakeController.Update(_VehicleMakeModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _VehicleMakeModel = new VehicleMakeModel { MakeID = TxtVehicleCode.Textt.TrimEnd(), MakeName = TxtVehicleMake.Textt.TrimEnd(), IsActive = CheckActive.Checked };
                        CustomMsg = _VehicleMakeController.Create(_VehicleMakeModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (CustomMsg == "Information saved successfully" || CustomMsg == "Information updated successfully")
                    {
                        BtnSearch.PerformClick();
                        TxtVehicleCode.Focus();
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

        private void DataGridMake_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            TxtVehicleCode.Textt = DataGridMake.Rows[e.RowIndex].Cells["MakeID"].Value?.ToString().TrimEnd();
            TxtVehicleMake.Textt = DataGridMake.Rows[e.RowIndex].Cells["MakeName"].Value?.ToString().TrimEnd();
            CheckActive.Checked = Convert.ToBoolean(DataGridMake.Rows[e.RowIndex].Cells["IsActive"].Value);
            TxtVehicleCode.ReadOnly = true;
            LblEncode.Text = "Edit";
        }

        private void ClearEncode()
        {
            TxtVehicleCode.Textt = TxtVehicleMake.Textt = "";
            CheckActive.Checked = true;
            TxtVehicleCode.ReadOnly = false;
            LblEncode.Text = "Entry";
        }

        int CurrentCol = 1;
        private void DataGridMake_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int BsbIndex = DataGridMake.Columns["BOwn"].Index;
            int ActiveIndex = DataGridMake.Columns["IsActive"].Index;
            if (!TxtColumnSearch.Visible && MakeTable.Rows.Count > 0 && e.ColumnIndex != BsbIndex && e.ColumnIndex != ActiveIndex)
            {
                if (DataGridMake.Rows.Count == 0)
                {
                    MakeTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridMake.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = DataGridMake.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = MakeTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridMake.DataSource = bs;

                DataGridViewRow row = DataGridMake.CurrentRow;
                if (row != null)
                {
                    TxtVehicleCode.Enabled = false;
                    TxtVehicleCode.Textt = row.Cells["MakeID"].Value?.ToString().TrimEnd();
                    TxtVehicleMake.Textt = row.Cells["MakeName"].Value?.ToString().TrimEnd();
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
            DataGridMake.Focus();
        }

        private void frm_vehicle_make_KeyDown(object sender, KeyEventArgs e)
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
