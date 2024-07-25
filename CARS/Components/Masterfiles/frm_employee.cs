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
    public partial class frm_employee : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private EmployeeController _EmployeeController = new EmployeeController();
        private EmployeeModel _EmployeeModel = new EmployeeModel();
        private SortedDictionary<string, string> _DepartmetnDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _DepartmetnDictionaryFilter = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _PositionDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _PositionDictionaryFilter = new SortedDictionary<string, string>();
        private SortedDictionary<string, int> _StatusDictionary = new SortedDictionary<string, int>();
        private DataTable EmployeeTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_employee(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderEntry.BackColor = PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblEncode.ForeColor = LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _DepartmetnDictionary = _DepartmetnDictionaryFilter = _EmployeeController.GetDictionary("Dept");
            _PositionDictionary = _PositionDictionaryFilter = _EmployeeController.GetDictionary("Position");
            _StatusDictionary = _EmployeeController.GetStatuses();
            ComboDept.DataSource = new BindingSource(_DepartmetnDictionary, null);
            ComboDeptFilter.DataSource = new BindingSource(_DepartmetnDictionaryFilter, null);
            ComboPosition.DataSource = new BindingSource(_PositionDictionary, null);
            ComboPositionFilter.DataSource = new BindingSource(_PositionDictionaryFilter, null);
            ComboStatus.DataSource = new BindingSource(_StatusDictionary, null);
            ComboDept.DisplayMember = ComboDeptFilter.DisplayMember = ComboPosition.DisplayMember = ComboPositionFilter.DisplayMember = ComboStatus.DisplayMember = "Key";
            ComboDept.ValueMember = ComboDeptFilter.ValueMember = ComboPosition.ValueMember = ComboPositionFilter.ValueMember = ComboStatus.ValueMember = "Value";
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridEmployee, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            dashboardCall = DashboardCall;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ClearEncode();
            _EmployeeModel = new EmployeeModel { EmployeeName = TxtEmployeeFilter.Textt.TrimEnd(), DeptID = ComboDeptFilter.SelectedValue.ToString(), 
                                                 PosID = ComboPositionFilter.SelectedValue.ToString() };
            EmployeeTable = _EmployeeController.dt(_EmployeeModel);
            DataGridEmployee.DataSource = EmployeeTable;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtEmployeeFilter.Textt = "";
                ComboDeptFilter.SelectedIndex = 0;
                ComboPositionFilter.SelectedIndex = 0;
                ClearEncode();
                EmployeeTable.Rows.Clear();
            }
        }

        int GenderType = 1;
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtFirstName.Textt.TrimEnd() != "" && TxtEmployeeID.Textt.TrimEnd() != "" && ComboDept.SelectedIndex != 0 && 
                ComboPosition.SelectedIndex != 0 && ComboStatus.SelectedIndex != 0)
            {
                if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string CustomMsg = "";
                    if (LblEncode.Text != "Entry")
                    {
                        _EmployeeModel = new EmployeeModel { EmployeeID = DataGridEmployee.CurrentRow.Cells["EmployeeID"].Value.ToString(), FName = TxtFirstName.Textt.TrimEnd(),
                                                             MName = TxtMiddleName.Textt.TrimEnd(), LName = TxtLastName.Textt.TrimEnd(), DateOfBirth = DateBirth.Value.Date.ToString("yyyy-MM-dd"), 
                                                             Gender = GenderType, DateHired = DateHiredd.Value.Date.ToString("yyyy-MM-dd"), EmploymentStatus = Convert.ToInt32(ComboStatus.SelectedValue), 
                                                             DeptID = ComboDept.SelectedValue.ToString(), PosID = ComboPosition.SelectedValue.ToString(), 
                                                             Remarks = TxtRemarks.Textt.TrimEnd(), BsbUsername = TxtBsbUsername.Textt.TrimEnd() };
                        CustomMsg = _EmployeeController.Update(_EmployeeModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _EmployeeModel = new EmployeeModel { EmployeeID = TxtEmployeeID.Textt.TrimEnd(), FName = TxtFirstName.Textt.TrimEnd(),
                                                             MName = TxtMiddleName.Textt.TrimEnd(), LName = TxtLastName.Textt.TrimEnd(), DateOfBirth = DateBirth.Value.Date.ToString("yyyy-MM-dd"), 
                                                             Gender = GenderType, DateHired = DateHiredd.Value.Date.ToString("yyyy-MM-dd"), EmploymentStatus = Convert.ToInt32(ComboStatus.SelectedValue), 
                                                             DeptID = ComboDept.SelectedValue.ToString(), PosID = ComboPosition.SelectedValue.ToString(), 
                                                             Remarks = TxtRemarks.Textt.TrimEnd(), BsbUsername = TxtBsbUsername.Textt.TrimEnd()  };
                        CustomMsg = _EmployeeController.Create(_EmployeeModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (CustomMsg == "Information saved successfully" || CustomMsg == "Information updated successfully")
                    {
                        BtnSearch.PerformClick();
                        TxtFirstName.Focus();
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

        private void DataGridEmployee_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            TxtFirstName.Textt = DataGridEmployee.Rows[e.RowIndex].Cells["FName"].Value?.ToString().TrimEnd();
            TxtMiddleName.Textt = DataGridEmployee.Rows[e.RowIndex].Cells["MName"].Value?.ToString().TrimEnd();
            TxtLastName.Textt = DataGridEmployee.Rows[e.RowIndex].Cells["LName"].Value?.ToString().TrimEnd();
            TxtEmployeeID.Textt = DataGridEmployee.Rows[e.RowIndex].Cells["EmployeeID"].Value?.ToString().TrimEnd();
            DateBirth.Value = Convert.ToDateTime(DataGridEmployee.Rows[e.RowIndex].Cells["DateOfBirth"].Value);
            DateHiredd.Value = Convert.ToDateTime(DataGridEmployee.Rows[e.RowIndex].Cells["DateHired"].Value);
            //ComboStatus.SelectedValue = DataGridEmployee.Rows[e.RowIndex].Cells["EmploymentStatus"].Value?.ToString().TrimEnd();
            if (_StatusDictionary.TryGetValue(DataGridEmployee.Rows[e.RowIndex].Cells["EmploymentStatus"].Value?.ToString().TrimEnd(), out int status))
            {
                ComboStatus.SelectedValue = status;
            }
            if (_DepartmetnDictionary.TryGetValue(DataGridEmployee.Rows[e.RowIndex].Cells["DeptName"].Value?.ToString().TrimEnd(), out string dept))
            {
                ComboDept.SelectedValue = dept;
            }
            if (_PositionDictionary.TryGetValue(DataGridEmployee.Rows[e.RowIndex].Cells["PosName"].Value?.ToString().TrimEnd(), out string pos))
            {
                ComboPosition.SelectedValue = pos;
            }
            if (DataGridEmployee.Rows[e.RowIndex].Cells["Gender"].Value.ToString() == "MALE")
            {
                RadioMale.Checked = true;
            }
            else
            {
                RadioFemale.Checked = true;
            }
            TxtRemarks.Textt = DataGridEmployee.Rows[e.RowIndex].Cells["Remarks"].Value?.ToString().TrimEnd();
            TxtEmployeeID.ReadOnly = true;
            LblEncode.Text = "Edit";
        }

        private void ClearEncode()
        {
            TxtFirstName.Textt = TxtMiddleName.Textt = TxtLastName.Textt = TxtRemarks.Textt = TxtEmployeeID.Textt = TxtBsbUsername.Textt = "";
            DateHiredd.Value = DateBirth.Value = new DateTime(2000, 1, 1);
            ComboStatus.SelectedIndex = ComboDept.SelectedIndex = ComboPosition.SelectedIndex = 0;
            RadioMale.Select();
            TxtEmployeeID.ReadOnly = false;
            LblEncode.Text = "Entry";
        }

        int CurrentCol = 1;
        private void DataGridEmployee_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!TxtColumnSearch.Visible && EmployeeTable.Rows.Count > 0)
            {
                if (DataGridEmployee.Rows.Count == 0)
                {
                    EmployeeTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridEmployee.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = DataGridEmployee.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = EmployeeTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridEmployee.DataSource = bs;

                DataGridViewRow row = DataGridEmployee.CurrentRow;
                if (row != null)
                {
                    TxtFirstName.Textt = row.Cells["FName"].Value?.ToString().TrimEnd();
                    TxtMiddleName.Textt = row.Cells["MName"].Value?.ToString().TrimEnd();
                    TxtLastName.Textt = row.Cells["LName"].Value?.ToString().TrimEnd();
                    TxtEmployeeID.Textt = row.Cells["EmployeeID"].Value?.ToString().TrimEnd();
                    DateBirth.Value = Convert.ToDateTime(row.Cells["DateOfBirth"].Value);
                    DateHiredd.Value = Convert.ToDateTime(row.Cells["DateHired"].Value);
                    ComboStatus.SelectedItem = row.Cells["EmploymentStatus"].Value?.ToString().TrimEnd();
                    ComboDept.SelectedValue = row.Cells["DeptName"].Value?.ToString().TrimEnd();
                    ComboPosition.SelectedValue = row.Cells["PosName"].Value?.ToString().TrimEnd();
                    TxtRemarks.Textt = row.Cells["Remarks"].Value?.ToString().TrimEnd();
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
            DataGridEmployee.Focus();
        }

        private void frm_employee_KeyDown(object sender, KeyEventArgs e)
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

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioMale.Checked)
            {
                GenderType = 1;
            }
            else if (RadioFemale.Checked)
            {
                GenderType = 2;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }

        private void DateHiredd_ValueChanged(object sender, EventArgs e)
        {

        }

        private void DateBirth_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}
