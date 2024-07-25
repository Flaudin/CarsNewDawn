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
    public partial class frm_sales_return_archive : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private TransactionController _TransactionController = new TransactionController();
        private SalesReturnController _SalesReturnCotroller = new SalesReturnController();
        private SortedDictionary<string, string> _SalesmanDictionary = new SortedDictionary<string, string>();
        private DataTable SalesOrderTable = new DataTable();
        private DataTable DetailsTable = new DataTable();
        private DataTable LocationTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_sales_return_archive()
        {
            InitializeComponent();
            PnlHeader.BackColor = BtnClose.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = PnlHeaderSales.BackColor = PnlHeaderDetails.BackColor = PnlHeaderLoc.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblHeader.ForeColor = BtnClose.ForeColor = LblFilter.ForeColor = LblSales.ForeColor = LblDetails.ForeColor = 
                LblLoc.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _SalesmanDictionary = _TransactionController.GetDictionary("Salesman");
            ComboSalesman.DataSource = new BindingSource(_SalesmanDictionary, null);
            ComboSalesman.DisplayMember = "Key";
            ComboSalesman.ValueMember = "Value";
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
                DetailsTable.Rows.Clear();
                LocationTable.Rows.Clear();
                SalesOrderTable = _SalesReturnCotroller.SearchSalesArchive(TxtSRNo.Textt.TrimEnd(), ComboSalesman.SelectedValue.ToString() ?? "",
                                                                           DateFrom.Value.Date.ToString("yyyy-MM-dd"), DateTo.Value.Date.ToString("yyyy-MM-dd"));
                DataGridSalesOrder.DataSource = SalesOrderTable;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                ClearData();
            }
        }

        private void ClearData()
        {
            TxtSRNo.Textt = "";
            ComboSalesman.SelectedIndex = 0;
            DateFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
            DateTo.Value = DateTime.Now;
            SalesOrderTable.Rows.Clear();
            DetailsTable.Rows.Clear();
            LocationTable.Rows.Clear();
            TxtSRNo.Focus();
        }

        private void DataGridSales_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DetailsTable = _SalesReturnCotroller.SalesDetailsArchive(DataGridSalesOrder.Rows[e.RowIndex].Cells["SRNo"].Value.ToString());
            DataGridSalesDetail.DataSource = DetailsTable;
            DataGridSalesDetail.ClearSelection();
            LocationTable.Rows.Clear();
        }

        private void DataGridSalesDetail_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string ItemID = "";
            if (DataGridSalesDetail.CurrentRow != null)
            {
                ItemID = DataGridSalesDetail.CurrentRow.Cells["ItemID"].Value.ToString();
            }
            LocationTable = _SalesReturnCotroller.SalesLocationArchive(DataGridSalesDetail.Rows[e.RowIndex].Cells["ItemID"].Value.ToString());
            DataGridSalesLocation.DataSource = LocationTable;
            DataGridSalesLocation.ClearSelection();
        }

        int CurrentCol = 1;
        DataGridView CurrentDgv = new DataGridView();
        DataTable CurrentTable = new DataTable();
        private void DataGridSalesOrder_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetColumnSearch(DataGridSalesOrder);
            int DateIndex = DataGridSalesOrder.Columns["SRDate"].Index;
            if (!TxtColumnSearch.Visible && SalesOrderTable.Rows.Count > 0 && e.ColumnIndex != DateIndex)
            {
                CurrentDgv = DataGridSalesOrder;
                CurrentTable = SalesOrderTable;
                if (DataGridSalesOrder.Rows.Count == 0)
                {
                    SalesOrderTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridSalesOrder.Columns[e.ColumnIndex].HeaderText;
                TxtColumnSearch.Visible = true;
                TxtColumnSearch.Focus();
            }
        }

        private void DataGridSalesDetail_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetColumnSearch(DataGridSalesDetail);
            int GoodIndex = DataGridSalesDetail.Columns["GoodQty"].Index;
            int DefectiveIndex = DataGridSalesDetail.Columns["DefectiveQty"].Index;
            int FreeIndex = DataGridSalesDetail.Columns["FreeItem"].Index;
            int ItemIndex  = DataGridSalesDetail.Columns["ItemNo"].Index;
            if (!TxtColumnSearch.Visible && DetailsTable.Rows.Count > 0 && e.ColumnIndex != GoodIndex && e.ColumnIndex != DefectiveIndex && e.ColumnIndex != FreeIndex && 
                e.ColumnIndex != ItemIndex)
            {
                CurrentDgv = DataGridSalesDetail;
                CurrentTable = DetailsTable;
                if (DataGridSalesDetail.Rows.Count == 0)
                {
                    DetailsTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridSalesDetail.Columns[e.ColumnIndex].HeaderText;
                TxtColumnSearch.Visible = true;
                TxtColumnSearch.Focus();
            }
        }

        private void DataGridSalesLocation_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetColumnSearch(DataGridSalesLocation);
            int GoodIndex = DataGridSalesLocation.Columns["LocGoodQty"].Index;
            int DefectiveIndex = DataGridSalesLocation.Columns["LocDefectiveQty"].Index;
            if (!TxtColumnSearch.Visible && LocationTable.Rows.Count > 0 && e.ColumnIndex != GoodIndex && e.ColumnIndex != DefectiveIndex)
            {
                CurrentDgv = DataGridSalesLocation;
                CurrentTable = LocationTable;
                if (DataGridSalesLocation.Rows.Count == 0)
                {
                    LocationTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridSalesLocation.Columns[e.ColumnIndex].HeaderText;
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
