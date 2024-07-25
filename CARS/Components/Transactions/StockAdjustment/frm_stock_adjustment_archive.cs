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
    public partial class frm_stock_adjustment_archive : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private StockAdjustmentController _StockAdjustmentController = new StockAdjustmentController();
        private DataTable StockAdjustmentTable = new DataTable();
        private DataTable PartsTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_stock_adjustment_archive()
        {
            InitializeComponent();
            PnlHeader.BackColor = BtnClose.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = PnlHeaderStockAdj.BackColor = PnlHeaderParts.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblHeader.ForeColor = BtnClose.ForeColor = LblFilter.ForeColor = LblStockAdj.ForeColor = LblParts.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            TxtColumnSearch.Visible = false;
            DateFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
            DateTo.Value = DateTime.Now;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close Stock Adjustment Archive?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Close();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (DateFrom.Value.Date > DateTo.Value.Date)
            {
                MessageBox.Show("Please input a proper date range before filtering.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                PartsTable.Rows.Clear();
                StockAdjustmentTable = _StockAdjustmentController.StockAdjustmentDataTable(TxtSANo.Textt.TrimEnd(), DateFrom.Value.Date.ToString("yyyy-MM-dd"), DateTo.Value.Date.ToString("yyyy-MM-dd"));
                DataGridStockAdjustment.DataSource = StockAdjustmentTable;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtSANo.Textt = "";
                DateFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
                DateTo.Value = DateTime.Now;
                StockAdjustmentTable.Rows.Clear();
                PartsTable.Rows.Clear();
                TxtSANo.Focus();
            }
        }

        private void DataGridStockAdjustment_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            PartsTable = _StockAdjustmentController.PartsDataTable(DataGridStockAdjustment.Rows[e.RowIndex].Cells["AdjNo"].Value.ToString());
            DataGridParts.DataSource = PartsTable;
            DataGridParts.ClearSelection();
        }

        int CurrentCol = 1;
        DataGridView CurrentDgv = new DataGridView();
        DataTable CurrentTable = new DataTable();
        private void DataGridStockAdjustment_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetColumnSearch(DataGridStockAdjustment);
            if (!TxtColumnSearch.Visible && StockAdjustmentTable.Rows.Count > 0)
            {
                CurrentDgv = DataGridStockAdjustment;
                CurrentTable = StockAdjustmentTable;
                if (DataGridStockAdjustment.Rows.Count == 0)
                {
                    StockAdjustmentTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridStockAdjustment.Columns[e.ColumnIndex].HeaderText;
                TxtColumnSearch.Visible = true;
                TxtColumnSearch.Focus();
            }
        }

        private void DataGridParts_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetColumnSearch(DataGridParts);
            int TakeUpIndex = DataGridParts.Columns["TakeUpQty"].Index;
            int DropIndex = DataGridParts.Columns["DropQty"].Index;
            if (!TxtColumnSearch.Visible && PartsTable.Rows.Count > 0 && e.ColumnIndex != TakeUpIndex && e.ColumnIndex != DropIndex)
            {
                CurrentDgv = DataGridParts;
                CurrentTable = PartsTable;
                if (DataGridParts.Rows.Count == 0)
                {
                    PartsTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridParts.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = CurrentDgv.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = CurrentTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                CurrentDgv.DataSource = bs;
            }
        }

        private void TxtColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtColumnSearch.Visible = false;
            CurrentDgv.Focus();
        }

        private void GetColumnSearch(DataGridView dgv)
        {
            TxtColumnSearch = Helper.ColoumnSearcher(dgv, 16, 300);
            TxtColumnSearch.Location = new Point(dgv.Width / 3, 50);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
        }

        private void frm_stock_adjustment_archive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && !TxtColumnSearch.Visible)
            {
                BtnClose.PerformClick();
            }
        }
    }
}
