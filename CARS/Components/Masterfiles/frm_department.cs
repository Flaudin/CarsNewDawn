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
    public partial class frm_department : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private DepartmentController _DepartmentController = new DepartmentController();
        private DepartmentModel _DepartmentModel = new DepartmentModel();
        private DataTable DescriptionTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();
        private Action dashboardCall;

        public frm_department(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderEntry.BackColor = PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblEncode.ForeColor = LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridDepartment, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            dashboardCall = DashboardCall;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ClearEncode();
            _DepartmentModel = new DepartmentModel { DeptName = TxtDepartmentFilter.Textt.TrimEnd() };
            DescriptionTable = _DepartmentController.dt(_DepartmentModel);
            DataGridDepartment.DataSource = DescriptionTable;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtDepartmentFilter.Textt = "";
                ClearEncode();
                DescriptionTable.Rows.Clear();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtDepartment.Textt.TrimEnd() != "")
            {
                if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string CustomMsg = "";
                    if (LblEncode.Text != "Entry")
                    {
                        _DepartmentModel = new DepartmentModel { DeptID = DataGridDepartment.CurrentRow.Cells["DeptID"].Value.ToString(), 
                                                                 DeptName = TxtDepartment.Textt.TrimEnd(), IsActive = CheckActive.Checked };
                        CustomMsg = _DepartmentController.Update(_DepartmentModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _DepartmentModel = new DepartmentModel { DeptID = Helper.GenerateUID(), DeptName = TxtDepartment.Textt.TrimEnd(), 
                                                                 IsActive = CheckActive.Checked };
                        CustomMsg = _DepartmentController.Create(_DepartmentModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (CustomMsg == "Information saved successfully" || CustomMsg == "Information updated successfully")
                    {
                        BtnSearch.PerformClick();
                        TxtDepartment.Focus();
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

        private void DataGridDepartment_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            TxtDepartment.Textt = DataGridDepartment.Rows[e.RowIndex].Cells["DeptName"].Value?.ToString().TrimEnd();
            CheckActive.Checked = Convert.ToBoolean(DataGridDepartment.Rows[e.RowIndex].Cells["IsActive"].Value);
            LblEncode.Text = "Edit";
        }

        private void ClearEncode()
        {
            TxtDepartment.Textt = "";
            CheckActive.Checked = true;
            LblEncode.Text = "Entry";
        }

        int CurrentCol = 1;
        private void DataGridDepartment_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!TxtColumnSearch.Visible && DescriptionTable.Rows.Count > 0)
            {
                if (DataGridDepartment.Rows.Count == 0)
                {
                    DescriptionTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridDepartment.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = DataGridDepartment.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = DescriptionTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridDepartment.DataSource = bs;

                DataGridViewRow row = DataGridDepartment.CurrentRow;
                if (row != null)
                {
                    TxtDepartment.Textt = row.Cells["DeptName"].Value?.ToString().TrimEnd();
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
            DataGridDepartment.Focus();
        }

        private void frm_department_KeyDown(object sender, KeyEventArgs e)
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
