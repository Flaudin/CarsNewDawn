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
    public partial class frm_term : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private TermController _TermController = new TermController();
        private TermsModel _TermsModel = new TermsModel();
        private DataTable TermTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_term(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderEntry.BackColor = PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblEncode.ForeColor = LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridTerm, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            dashboardCall = DashboardCall;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ClearEncode();
            _TermsModel = new TermsModel { TermName = TxtTermNameFilter.Textt.TrimEnd(), TermDays = NumericDaysFilter.Value };
            TermTable = _TermController.dt(_TermsModel);
            DataGridTerm.DataSource = TermTable;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtTermNameFilter.Textt = "";
                NumericDaysFilter.Value = 0;
                ClearEncode();
                TermTable.Rows.Clear();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtTermID.Textt.TrimEnd() != "" && TxtTermName.Textt.TrimEnd() != "")
            {
                if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string CustomMsg = "";
                    if (LblEncode.Text != "Entry")
                    {
                        _TermsModel = new TermsModel { uniqueid = DataGridTerm.CurrentRow.Cells["uniqueid"].Value.ToString(), TermID = TxtTermID.Textt.TrimEnd(), TermName = TxtTermName.Textt.TrimEnd(),
                                                       TermDays = NumericDays.Value, IsActive = CheckActive.Checked };
                        CustomMsg = _TermController.Update(_TermsModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _TermsModel = new TermsModel { uniqueid = Helper.GenerateUID(), TermID = TxtTermID.Textt.TrimEnd(), TermName = TxtTermName.Textt.TrimEnd(), TermDays = NumericDays.Value,
                                                       IsActive = CheckActive.Checked };
                        CustomMsg = _TermController.Create(_TermsModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (CustomMsg == "Information saved successfully" || CustomMsg == "Information updated successfully")
                    {
                        BtnSearch.PerformClick();
                        TxtTermID.Focus();
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

        private void DataGridTerm_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            TxtTermID.Textt = DataGridTerm.Rows[e.RowIndex].Cells["TermID"].Value?.ToString().TrimEnd();
            TxtTermName.Textt = DataGridTerm.Rows[e.RowIndex].Cells["TermName"].Value?.ToString().TrimEnd();
            NumericDays.Value = Convert.ToDecimal(DataGridTerm.Rows[e.RowIndex].Cells["TermDays"].Value ?? 0);
            CheckActive.Checked = Convert.ToBoolean(DataGridTerm.Rows[e.RowIndex].Cells["IsActive"].Value);
            TxtTermID.ReadOnly = true;
            LblEncode.Text = "Edit";
        }

        private void ClearEncode()
        {
            TxtTermID.Textt = TxtTermName.Textt = "";
            NumericDays.Value = 0;
            CheckActive.Checked = true;
            TxtTermID.ReadOnly = false;
            LblEncode.Text = "Entry";
        }

        int CurrentCol = 1;
        private void DataGridTerm_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int DaysIndex = DataGridTerm.Columns["TermDays"].Index;
            int ActiveIndex = DataGridTerm.Columns["IsActive"].Index;
            if (!TxtColumnSearch.Visible && TermTable.Rows.Count > 0 && e.ColumnIndex != DaysIndex && e.ColumnIndex != ActiveIndex)
            {
                if (DataGridTerm.Rows.Count == 0)
                {
                    TermTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridTerm.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = DataGridTerm.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = TermTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridTerm.DataSource = bs;

                DataGridViewRow row = DataGridTerm.CurrentRow;
                if (row != null)
                {
                    TxtTermID.Textt = row.Cells["TermID"].Value?.ToString().TrimEnd();
                    TxtTermName.Textt = row.Cells["TermName"].Value?.ToString().TrimEnd();
                    NumericDays.Value = Convert.ToDecimal(row.Cells["TermDays"].Value ?? 0);
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
            DataGridTerm.Focus();
        }

        private void frm_term_KeyDown(object sender, KeyEventArgs e)
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
