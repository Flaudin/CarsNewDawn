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
    public partial class frm_description : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private Action dashboardCall;
        private DescriptionController _DescriptionController = new DescriptionController();
        private DescriptionModel _DescriptionModel = new DescriptionModel();
        private DataTable DescriptionTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_description(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderEntry.BackColor = PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblEncode.ForeColor = LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridDescription, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            dashboardCall = DashboardCall;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ClearEncode();
            _DescriptionModel = new DescriptionModel { DescName = TxtDescriptionFilter.Textt.TrimEnd() };
            DescriptionTable = _DescriptionController.dt(_DescriptionModel);
            DataGridDescription.DataSource = DescriptionTable;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtDescriptionFilter.Textt = "";
                ClearEncode();
                DescriptionTable.Rows.Clear();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtDescription.Textt.TrimEnd() != "")
            {
                if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    if (LblEncode.Text != "Entry")
                    {
                        _DescriptionModel = new DescriptionModel
                        {
                            DescID = DataGridDescription.CurrentRow.Cells["DescID"].Value.ToString(),
                            DescName = TxtDescription.Textt.TrimEnd(),
                            IsActive = CheckActive.Checked
                        };
                        Helper.Confirmator(_DescriptionController.Update(_DescriptionModel), "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _DescriptionModel = new DescriptionModel { DescID = Helper.GenerateUID(), DescName = TxtDescription.Textt.TrimEnd(), IsActive = CheckActive.Checked };
                        Helper.Confirmator(_DescriptionController.Create(_DescriptionModel), "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    BtnSearch.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("Record cannot be saved. Please fill in the Description field to proceed.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnClearEncode_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Unsaved entries will be discarded. Are you sure you want to clear the input field(s)?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                ClearEncode();
            }
        }

        private void DataGridDescription_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            TxtDescription.Textt = DataGridDescription.Rows[e.RowIndex].Cells["DescName"].Value?.ToString().TrimEnd();
            CheckActive.Checked = Convert.ToBoolean(DataGridDescription.Rows[e.RowIndex].Cells["IsActive"].Value);
            LblEncode.Text = "Edit";
        }

        private void ClearEncode()
        {
            DataGridDescription.ClearSelection();
            TxtDescription.Textt = "";
            CheckActive.Checked = true;
            LblEncode.Text = "Entry";
        }

        int CurrentCol = 1;
        private void DataGridDescription_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int ActiveIndex = DataGridDescription.Columns["IsActive"].Index;
            if (!TxtColumnSearch.Visible && DescriptionTable.Rows.Count > 0 && e.ColumnIndex != ActiveIndex)
            {
                if (DataGridDescription.Rows.Count == 0)
                {
                    DescriptionTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridDescription.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = DataGridDescription.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = DescriptionTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridDescription.DataSource = bs;

                DataGridViewRow row = DataGridDescription.CurrentRow;
                if (row != null)
                {
                    TxtDescription.Textt = row.Cells["DescName"].Value?.ToString().TrimEnd();
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
            DataGridDescription.Focus();
        }

        private void frm_description_KeyDown(object sender, KeyEventArgs e)
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
