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
    public partial class frm_unit_measurement : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private MeasurementController _MeasurementController = new MeasurementController();
        private MeasurementModel _MeasurementModel = new MeasurementModel();
        private DataTable MeasurementTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_unit_measurement(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderEntry.BackColor = PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblEncode.ForeColor = LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridMeasurement, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            dashboardCall = DashboardCall;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ClearEncode();
            _MeasurementModel = new MeasurementModel { UomName = TxtUomNameFilter.Textt.TrimEnd() };
            MeasurementTable = _MeasurementController.dt(_MeasurementModel);
            DataGridMeasurement.DataSource = MeasurementTable;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtUomNameFilter.Textt = "";
                ClearEncode();
                MeasurementTable.Rows.Clear();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtUomID.Textt.TrimEnd() != "" && TxtUomName.Textt.TrimEnd() != "")
            {
                if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string CustomMsg = "";
                    if (LblEncode.Text != "Entry")
                    {
                        _MeasurementModel = new MeasurementModel { uniqueid = DataGridMeasurement.CurrentRow.Cells["uniqueid"].Value.ToString(), UomID = TxtUomID.Textt.TrimEnd(),
                                                                   UomName = TxtUomName.Textt.TrimEnd(), IsActive = CheckActive.Checked };
                        CustomMsg = _MeasurementController.Update(_MeasurementModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _MeasurementModel = new MeasurementModel { uniqueid = Helper.GenerateUID(), UomID = TxtUomID.Textt.TrimEnd(), UomName = TxtUomName.Textt.TrimEnd(), IsActive = CheckActive.Checked };
                        CustomMsg = _MeasurementController.Create(_MeasurementModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                    if (CustomMsg == "Information saved successfully" || CustomMsg == "Information updated successfully")
                    {
                        BtnSearch.PerformClick();
                        TxtUomID.Focus();
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

        private void DataGridMeasurement_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            TxtUomID.Textt = DataGridMeasurement.Rows[e.RowIndex].Cells["UomID"].Value?.ToString().TrimEnd();
            TxtUomName.Textt = DataGridMeasurement.Rows[e.RowIndex].Cells["UomName"].Value?.ToString().TrimEnd();
            CheckActive.Checked = Convert.ToBoolean(DataGridMeasurement.Rows[e.RowIndex].Cells["IsActive"].Value);
            TxtUomID.ReadOnly = true;
            LblEncode.Text = "Edit";
        }

        private void ClearEncode()
        {
            TxtUomID.Textt = TxtUomName.Textt = "";
            CheckActive.Checked = true;
            TxtUomID.ReadOnly = false;
            LblEncode.Text = "Entry";
        }

        int CurrentCol = 1;
        private void DataGridMeasurement_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int ActiveIndex = DataGridMeasurement.Columns["IsActive"].Index;
            if (!TxtColumnSearch.Visible && MeasurementTable.Rows.Count > 0 && e.ColumnIndex != ActiveIndex)
            {
                if (DataGridMeasurement.Rows.Count == 0)
                {
                    MeasurementTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridMeasurement.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = DataGridMeasurement.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = MeasurementTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridMeasurement.DataSource = bs;

                DataGridViewRow row = DataGridMeasurement.CurrentRow;
                if (row != null)
                {
                    TxtUomID.Textt = row.Cells["UomID"].Value?.ToString().TrimEnd();
                    TxtUomName.Textt = row.Cells["UomName"].Value?.ToString().TrimEnd();
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
            DataGridMeasurement.Focus();
        }

        private void frm_unit_measurement_KeyDown(object sender, KeyEventArgs e)
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
