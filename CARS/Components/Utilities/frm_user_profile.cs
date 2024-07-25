using CARS.Components.Utilities;
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

namespace CARS.Components.Utilities
{
    public partial class frm_user_profile : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private EmployeeController _EmployeeController = new EmployeeController();
        private EmployeeModel _EmployeeModel = new EmployeeModel();
        private SortedDictionary<string, string> _DepartmetnDictionaryFilter = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _PositionDictionaryFilter = new SortedDictionary<string, string>();
        private DataTable EmployeeTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_user_profile(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _DepartmetnDictionaryFilter = _EmployeeController.GetDictionary("Dept");
            _PositionDictionaryFilter = _EmployeeController.GetDictionary("Position");
            ComboDeptFilter.DataSource = new BindingSource(_DepartmetnDictionaryFilter, null);
            ComboPositionFilter.DataSource = new BindingSource(_PositionDictionaryFilter, null);
            ComboDeptFilter.DisplayMember = ComboPositionFilter.DisplayMember = "Key";
            ComboDeptFilter.ValueMember = ComboPositionFilter.ValueMember = "Value";
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridEmployee, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            dashboardCall = DashboardCall;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (DataGridEmployee.CurrentRow != null)
            {
                frm_user_profile_encode encode = new frm_user_profile_encode(DataGridEmployee.CurrentRow.Cells["EmployeeID"].Value.ToString(), 
                    DataGridEmployee.CurrentRow.Cells["EmployeeName"].Value.ToString());
                encode.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please search and select a employee first.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            _EmployeeModel = new EmployeeModel
            {
                EmployeeName = TxtEmployeeFilter.Textt.TrimEnd(),
                DeptID = ComboDeptFilter.SelectedValue.ToString(),
                PosID = ComboPositionFilter.SelectedValue.ToString()
            };
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
                EmployeeTable.Rows.Clear();
            }
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
            }
        }

        private void TxtColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtColumnSearch.Visible = false;
            DataGridEmployee.Focus();
        }

        private void frm_user_profile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !TxtColumnSearch.Visible)
            {
                if (PnlFilter.ContainsFocus)
                {
                    BtnSearch.PerformClick();
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

        private void BtnTranLog_Click(object sender, EventArgs e)
        {
            if (DataGridEmployee.CurrentRow != null)
            {
                frm_user_profile_log log = new frm_user_profile_log();
                log.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please search and select a employee first.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DataGridEmployee_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BtnEdit_Click(sender, e);
        }
    }
}
