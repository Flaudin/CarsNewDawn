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
    public partial class frm_stock_adjustment_lotno_encode : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private StockAdjustmentController _StockAdjustmentController = new StockAdjustmentController();
        public event Action<List<dynamic>> LotArray;
        private DataTable LotTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_stock_adjustment_lotno_encode(string PartNo)
        {
            InitializeComponent();
            PnlHeader.BackColor = BtnClose.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblHeader.ForeColor = LblTable.ForeColor = BtnClose.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            LotTable = _StockAdjustmentController.LotNoDataTable(PartNo);
            var boolColumn = new DataColumn("ForSelection", typeof(bool));
            boolColumn.DefaultValue = false;
            LotTable.Columns.Add(boolColumn);
            DataGridLot.DataSource = LotTable;
            DataGridLot.ClearSelection();
            DataGridLot.KeyDown += new KeyEventHandler(DataGridPart_KeyDown);
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridLot, 16, 200);
            TxtColumnSearch.Location = new Point(DataGridLot.Width/3, 50);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
        }

        private void frm_stock_adjustment_warehouse_encode_Load(object sender, EventArgs e)
        {
            
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close lot selection?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int checkedRowCount = LotTable.AsEnumerable()
            .Where(row => Convert.ToBoolean(row["ForSelection"]))
            .Count();
            if (checkedRowCount > 0)
            {
                List<dynamic> stringArrayToSend = new List<dynamic>();
                foreach (DataRow row in LotTable.Rows)
                {
                    if (Convert.ToBoolean(row["ForSelection"]))
                    {
                        stringArrayToSend.Add(new[] { row["LotNo"].ToString().TrimEnd(), row["UnitCost"] });
                    }
                }
                LotArray?.Invoke(stringArrayToSend);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a lot number before proceeding.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear all the selected rows?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                LotTable.DefaultView.RowFilter = "";
                foreach (DataGridViewRow row in DataGridLot.Rows)
                {
                    row.Cells["ForSelection"].Value = false;
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

        private void DataGridBin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in DataGridLot.Rows)
            {
                if (row.Index != DataGridLot.CurrentCell.RowIndex)
                {
                    (row.Cells["ForSelection"] as DataGridViewCheckBoxCell).Value = false;
                }
                else
                {
                    (row.Cells["ForSelection"] as DataGridViewCheckBoxCell).Value = true;
                }
            }
        }

        int CurrentCol = 1;
        private void DataGridBin_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int SelectionIndex = DataGridLot.Columns["ForSelection"].Index;
            int CostIndex = DataGridLot.Columns["UnitCost"].Index;
            if (!TxtColumnSearch.Visible && LotTable.Rows.Count > 0 && e.ColumnIndex != CostIndex && e.ColumnIndex != SelectionIndex)
            {
                if (DataGridLot.Rows.Count == 0)
                {
                    LotTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridLot.Columns[e.ColumnIndex].HeaderText;
                TxtColumnSearch.Visible = true;
                TxtColumnSearch.Focus();
                this.CancelButton = null;
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
                string searchCol = DataGridLot.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = LotTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridLot.DataSource = bs;
            }
        }

        private void TxtColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtColumnSearch.Visible = false;
        }

        private void frm_stock_adjustment_warehouse_encode_KeyDown(object sender, KeyEventArgs e)
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
    }
}
