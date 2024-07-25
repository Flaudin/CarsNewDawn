using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model.Masterfiles;
using CARS.Model.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Transactions.StockAdjustment
{
    public partial class frm_stock_adjustment_parts_encode : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private TransactionController _TransactionController = new TransactionController();
        private StockAdjustmentController _StockAdjustmentController = new StockAdjustmentController();
        private List<string> PartsList = new List<string>();
        public event Action<List<dynamic[]>> StringArraySent;
        private DataTable PartTable  = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_stock_adjustment_parts_encode(List<string> Parts)
        {
            InitializeComponent();
            PnlHeader.BackColor = BtnClose.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderParts.BackColor = PnlHeaderSelection.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblHeader.ForeColor = BtnClose.ForeColor = LblParts.ForeColor = LblSelection.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            PartTable = _TransactionController.PartsWithBegBalDataTable("", "", "");
            var boolColumn = new DataColumn("ForSelection", typeof(bool));
            boolColumn.DefaultValue = false;
            PartTable.Columns.Add(boolColumn);
            DataGridPart.DataSource = PartTable;
            DataGridPart.ClearSelection();
            PartsList = Parts;
            DataGridPart.KeyDown += new KeyEventHandler(DataGridPart_KeyDown);
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridPart, 16, 300);
            TxtColumnSearch.Location = new Point(DataGridPart.Width / 3, 50);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close Parts selection?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (DataGridSelection.Rows.Count > 0)
            {
                List<dynamic[]> stringArrayToSend = new List<dynamic[]>();
                foreach (DataGridViewRow row in DataGridSelection.Rows)
                {
                    for (int i = 0; i != Convert.ToInt32(row.Cells["SelectedCount"].Value); i++)
                    {
                        stringArrayToSend.Add(new[] { row.Cells["PartNoSelected"].Value.ToString(), row.Cells["PartNameSelected"].Value.ToString(), row.Cells["OtherNameSelected"].Value.ToString(),
                                                row.Cells["DescNameSelected"].Value.ToString(), row.Cells["BrandNameSelected"].Value.ToString() });
                    }
                }
                StringArraySent?.Invoke(stringArrayToSend);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a part before proceeding.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear all the selected rows that are selected during the current session?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                ImagePart.Image = null;
                PartTable.DefaultView.RowFilter = "";
                DataGridSelection.Rows.Clear();
                foreach (DataGridViewRow row in DataGridPart.Rows)
                {
                    row.Cells["ForSelection"].Value = false;
                }
            }
        }

        private void DataGridPart_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ImagePart.Image = null;
            if (e.RowIndex > -1)
            {
                byte[] imageBytes = null;
                string imageString = _TransactionController.GetPartImage(DataGridPart.CurrentRow.Cells["PartNo"].Value?.ToString());
                if (imageString != "")
                {
                    if (Helper.IsBase64Encoded(imageString))
                    {
                        imageBytes = Convert.FromBase64String(imageString);
                    }
                    else
                    {
                        imageBytes = Encoding.Default.GetBytes(imageString);
                    }
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        Image NewImage = Image.FromStream(ms);
                        ImagePart.Image = NewImage;
                        ms.Dispose();
                    }
                }
                else
                {
                    ImagePart.Image = null;
                }
            }
        }

        private void frm_stock_adjustment_parts_encode_Load(object sender, EventArgs e)
        {
            foreach (string str in PartsList)
            {
                foreach (DataGridViewRow row in DataGridPart.Rows)
                {
                    if (row.Cells["PartNo"].Value.ToString() == str)
                    {
                        row.Cells["ForSelection"].Value = true;
                        row.Cells["ForSelection"].ReadOnly = true;
                        break;
                    }
                }
            }
        }

        void DataGridPart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSave.PerformClick();
                e.Handled = true;
            }
        }

        private void DataGridPart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = DataGridPart.Columns["ForSelection"].Index;
            if (e.RowIndex >= 0 && e.ColumnIndex != columnIndex)
            {
                PartSelection(e.RowIndex, false);
            }
        }

        int CurrentCol = 1;
        private void DataGridPart_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int SelectionIndex = DataGridPart.Columns["ForSelection"].Index;
            if (!TxtColumnSearch.Visible && PartTable.Rows.Count > 0 && e.ColumnIndex != SelectionIndex)
            {
                if (DataGridPart.Rows.Count == 0)
                {
                    PartTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridPart.Columns[e.ColumnIndex].HeaderText;
                TxtColumnSearch.Visible = true;
                TxtColumnSearch.Focus();
            }
            else if (e.ColumnIndex == SelectionIndex)
            {
                bool AllSelected = false;
                var uncheckedRows = DataGridPart.Rows.Cast<DataGridViewRow>()
                                    .Where(row => !(bool)row.Cells["ForSelection"].Value);

                if (uncheckedRows.Any())
                {
                    AllSelected = true;
                }

                foreach (DataGridViewRow row in DataGridPart.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["ForSelection"] as DataGridViewCheckBoxCell;
                    if (!checkBoxCell.ReadOnly)
                    {
                        checkBoxCell.Value = AllSelected;
                        PartSelection(row.Index, true);
                    }
                    DataGridPart.EndEdit();
                }

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
                string searchCol = DataGridPart.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = PartTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridPart.DataSource = bs;
            }
        }

        private void TxtColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtColumnSearch.Visible = false;
        }

        private void frm_stock_adjustment_parts_encode_KeyDown(object sender, KeyEventArgs e)
        {
            if (!TxtColumnSearch.Visible)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        BtnClose.PerformClick();
                        break;

                    case Keys.Enter:
                        BtnSave.PerformClick();
                        break;
                }
            }
        }

        private void DataGridPart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = DataGridPart.Columns["ForSelection"].Index;
            if (e.RowIndex >= 0 && e.ColumnIndex == columnIndex)
            {
                PartSelection(e.RowIndex, false);
            }
        }

        private void BtnDeleteParts_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to deleted the selected rows?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                for (int i = DataGridSelection.Rows.Count - 1; i >= 0; i--)
                {
                    if (Convert.ToBoolean(DataGridSelection.Rows[i].Cells[0].Value))
                    {
                        DataGridSelection.Rows.RemoveAt(i);
                    }
                }
            }
        }

        private void DataGridSelection_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int SelectionIndex = DataGridSelection.Columns["ForSelectionSelected"].Index;
            if (e.ColumnIndex == SelectionIndex)
            {
                bool AllSelected = false;
                var uncheckedRows = DataGridSelection.Rows.Cast<DataGridViewRow>()
                                    .Where(row => !(bool)row.Cells["ForSelectionSelected"].Value);

                if (uncheckedRows.Any())
                {
                    AllSelected = true;
                }

                foreach (DataGridViewRow row in DataGridSelection.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["ForSelectionSelected"] as DataGridViewCheckBoxCell;
                    if (!checkBoxCell.ReadOnly)
                    {
                        checkBoxCell.Value = AllSelected;
                    }
                }
                DataGridSelection.EndEdit();
            }
        }

        private void PartSelection(int rowIndex, bool isFromSelect)
        {
            bool isChecked = Convert.ToBoolean(DataGridPart.Rows[rowIndex].Cells["ForSelection"].Value);
            if (!isFromSelect)
            {
                DataGridPart.Rows[rowIndex].Cells["ForSelection"].Value = !isChecked;
            }
            else
            {
                isChecked = !isChecked;
            }

            if (isChecked)
            {
                int deletedIndex = -1;
                DataGridViewRow row = DataGridSelection.Rows
                                    .Cast<DataGridViewRow>()
                                    .FirstOrDefault(r => r.Cells["PartNoSelected"].Value.ToString().Equals(DataGridPart.Rows[rowIndex].Cells["PartNo"].Value.ToString()));

                if (row != null)
                {
                    deletedIndex = row.Index;
                    DataGridSelection.Rows.RemoveAt(deletedIndex);
                }
            }
            else
            {
                DataGridViewRow row = DataGridSelection.Rows
                                    .Cast<DataGridViewRow>()
                                    .FirstOrDefault(r => r.Cells["PartNoSelected"].Value.ToString().Equals(DataGridPart.Rows[rowIndex].Cells["PartNo"].Value.ToString()));
                if (row == null)
                {
                    DataGridSelection.Rows.Add(false, DataGridPart.Rows[rowIndex].Cells["PartNo"].Value.ToString(),
                                                    DataGridPart.Rows[rowIndex].Cells["PartName"].Value.ToString(), 1,
                                                    DataGridPart.Rows[rowIndex].Cells["OtherName"].Value.ToString(),
                                                    DataGridPart.Rows[rowIndex].Cells["DescName"].Value.ToString(),
                                                    DataGridPart.Rows[rowIndex].Cells["BrandName"].Value.ToString());
                }
            }
        }

        private void DataGridSelection_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += Numeric_KeyPress;
        }

        private void Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }

            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }
    }
}
