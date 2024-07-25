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
    public partial class frm_oem : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private OemController _OemController = new OemController();
        private OemModel _OemModel = new OemModel();
        private SortedDictionary<string, string> _MakeDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _MakeDictionaryFilter = new SortedDictionary<string, string>();
        private DataTable OemTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_oem(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderEntry.BackColor = PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = PnlHeaderParts.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblEncode.ForeColor = LblFilter.ForeColor = LblTable.ForeColor = LblParts.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _MakeDictionary = _MakeDictionaryFilter = _OemController.GetDictionary();
            ComboMakeFilter.DataSource = new BindingSource(_MakeDictionaryFilter, null);
            ComboMake.DataSource = new BindingSource(_MakeDictionary, null);
            ComboMakeFilter.DisplayMember = ComboMake.DisplayMember = "Key";
            ComboMakeFilter.ValueMember = ComboMake.ValueMember = "Value";
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridOem, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            dashboardCall = DashboardCall;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ClearEncode();
            _OemModel = new OemModel { OemNo = TxtOemFilter.Textt.TrimEnd(), MakeID = ComboMakeFilter.SelectedValue.ToString().TrimEnd() };
            OemTable = _OemController.dt(_OemModel);
            DataGridOem.DataSource = OemTable;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtOemFilter.Textt = "";
                ClearEncode();
                OemTable.Rows.Clear();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtOem.Textt.TrimEnd() != "" && ComboMake.SelectedIndex != 0)
            {
                if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    List<OemPartModel> OemPartsList = new List<OemPartModel>();
                    foreach (DataGridViewRow row in DataGridParts.Rows)
                    {
                        if (!row.IsNewRow) // Skip the new row if present
                        {
                            OemPartModel model = new OemPartModel { PartNo = row.Cells[1].Value.ToString(), IsActive = true, IsNew = Convert.ToBoolean(row.Cells[3].Value) };
                            OemPartsList.Add(model);
                        }
                    }

                    string CustomMsg = "";
                    if (LblEncode.Text != "Entry")
                    {
                        _OemModel = new OemModel { UniqueID = DataGridOem.CurrentRow.Cells["uniqueid"].Value.ToString(), OemNo = TxtOem.Textt.TrimEnd(), MakeID = ComboMake.SelectedValue.ToString().TrimEnd(),
                                                   BOwn = false, IsActive = CheckActive.Checked, DetailsList = OemPartsList };
                        CustomMsg = _OemController.Update(_OemModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _OemModel = new OemModel { UniqueID = Helper.GenerateUID(), OemNo = TxtOem.Textt.TrimEnd(), MakeID = ComboMake.SelectedValue.ToString().TrimEnd(),
                                                   IsActive = CheckActive.Checked, DetailsList = OemPartsList };
                        CustomMsg = _OemController.Create(_OemModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (CustomMsg == "Information saved successfully" || CustomMsg == "Information updated successfully")
                    {
                        BtnSearch.PerformClick();
                        TxtOem.Focus();
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

        private void DataGridOem_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            TxtOem.Textt = DataGridOem.Rows[e.RowIndex].Cells["OemNo"].Value?.ToString().TrimEnd();
            ComboMake.SelectedValue = DataGridOem.Rows[e.RowIndex].Cells["MakeID"].Value?.ToString() ?? "";
            CheckActive.Checked = Convert.ToBoolean(DataGridOem.Rows[e.RowIndex].Cells["IsActive"].Value);
            LblEncode.Text = "Edit";

            OemPartModel _OemPartModel = new OemPartModel { ParentID = DataGridOem.Rows[e.RowIndex].Cells["UniqueID"].Value?.ToString().TrimEnd() };
            DataGridParts.DataSource = _OemController.dt(_OemPartModel);
            DataGridParts.ClearSelection();
            foreach (DataGridViewRow partrow in DataGridParts.Rows)
            {
                partrow.Cells[0].ReadOnly = true;
            }
        }

        private void ClearEncode()
        {
            if (DataGridParts.DataSource != null)
            {
                DataTable dt = DataGridParts.DataSource as DataTable;
                if (dt != null)
                {
                    dt.Rows.Clear();
                }
            }
            else
            {
                DataGridParts.Rows.Clear();
            }
            TxtOem.Textt = ComboMake.Text = "";
            CheckActive.Checked = true;
            LblEncode.Text = "Entry";
        }

        private void BtnAddPart_Click(object sender, EventArgs e)
        {
            List<string> PartData = new List<string>();
            foreach (DataGridViewRow row in DataGridParts.Rows)
            {
                PartData.Add(row.Cells[1].Value.ToString());
            }
            frm_oem_parts_encode partEncode = new frm_oem_parts_encode(PartData);
            partEncode.StringArraySent += ReceiveArrayFromChild;
            partEncode.ShowDialog(this);
        }

        private void BtnDeletePart_Click(object sender, EventArgs e)
        {
            bool hasCheckedCell = DataGridParts.Rows.Cast<DataGridViewRow>().Any(row => Convert.ToBoolean(row.Cells["ToDelete"].Value));
            switch (hasCheckedCell)
            {
                case true:
                    if (DataGridParts.CurrentRow != null && Helper.Confirmator("Are you sure you want to deleted the selected rows?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        for (int i = DataGridParts.Rows.Count - 1; i >= 0; i--)
                        {
                            DataGridViewRow row = DataGridParts.Rows[i];
                            DataGridViewCheckBoxCell checkboxCell = row.Cells[0] as DataGridViewCheckBoxCell;

                            if (Convert.ToBoolean(row.Cells[3].Value) && checkboxCell != null && Convert.ToBoolean(checkboxCell.Value))
                            {
                                DataGridParts.Rows.RemoveAt(i);
                            }
                        }
                    }
                    break;

                case false:
                    if (DataGridParts.CurrentRow != null)
                    {
                        if (Convert.ToBoolean(DataGridParts.CurrentRow.Cells["IsNew"].Value) &&
                            Helper.Confirmator("Are you sure you want to deleted the selected row?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            DataGridParts.Rows.RemoveAt(DataGridParts.CurrentRow.Index);
                        }
                        else if (!Convert.ToBoolean(DataGridParts.CurrentRow.Cells["IsNew"].Value))
                        {
                            MessageBox.Show("User cannot delete saved Part No.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("There is no row selected for deletion.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
            }
        }

        private void ReceiveArrayFromChild(List<string> stringArray)
        {
            foreach (string str in stringArray)
            {
                DataGridViewRow row = DataGridParts.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r => r.Cells["PartNo"].Value?.ToString() == str);
                if (row == null)
                {
                    if (DataGridParts.DataSource != null)
                    {
                        DataTable dt = DataGridParts.DataSource as DataTable;
                        DataRow newrow = dt.NewRow();
                        newrow["PartNo"] = str;
                        newrow["IsActivePart"] = true;
                        newrow["IsNew"] = true;
                        dt.Rows.Add(newrow);
                    }
                    else
                    {
                        DataGridParts.Rows.Add(false, str, true, true);
                    }
                }
            }
        }

        int CurrentCol = 1;
        private void DataGridOem_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int BsbIndex = DataGridOem.Columns["BOwn"].Index;
            int ActiveIndex = DataGridOem.Columns["IsActive"].Index;
            if (!TxtColumnSearch.Visible && OemTable.Rows.Count > 0 && e.ColumnIndex != BsbIndex && e.ColumnIndex != ActiveIndex)
            {
                if (DataGridOem.Rows.Count == 0)
                {
                    OemTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridOem.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = DataGridOem.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = OemTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridOem.DataSource = bs;

                DataGridViewRow row = DataGridOem.CurrentRow;
                if (row != null)
                {
                    TxtOem.Textt = row.Cells["OemNo"].Value?.ToString().TrimEnd();
                    ComboMake.SelectedValue = row.Cells["MakeID"].Value?.ToString() ?? "";
                    CheckActive.Checked = Convert.ToBoolean(row.Cells["IsActive"].Value);
                    LblEncode.Text = "Edit";

                    OemPartModel _OemPartModel = new OemPartModel { ParentID = row.Cells["UniqueID"].Value?.ToString().TrimEnd() };
                    DataGridParts.DataSource = _OemController.dt(_OemPartModel);
                    DataGridParts.ClearSelection();
                    foreach (DataGridViewRow partrow in DataGridParts.Rows)
                    {
                        partrow.Cells[0].ReadOnly = true;
                    }
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
        }

        private void DataGridParts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && !Convert.ToBoolean(DataGridParts.Rows[e.RowIndex].Cells["IsNew"].Value ?? false))
            {
                MessageBox.Show("User cannot delete saved Part No.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frm_oem_KeyDown(object sender, KeyEventArgs e)
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
