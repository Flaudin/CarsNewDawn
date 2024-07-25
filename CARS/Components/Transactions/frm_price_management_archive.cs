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

namespace CARS.Components.Transactions
{
    public partial class frm_price_management_archive : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private PriceManagementController _PriceManagementController = new PriceManagementController();
        private DataTable PriceManagementTable = new DataTable();
        private DataTable PartsTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_price_management_archive()
        {
            InitializeComponent();
            PnlHeader.BackColor = BtnClose.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = PnlHeaderParts.BackColor = PnlHeaderPrice.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblHeader.ForeColor = BtnClose.ForeColor = LblFilter.ForeColor = LblPrice.ForeColor = LblParts.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            TxtColumnSearch.Visible = false;
            DateFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
            DateTo.Value = DateTime.Now;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close Price Management Archive?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
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
                PriceManagementTable = _PriceManagementController.ControlNoDataTable(TxtPartNo.Textt.TrimEnd(), DateFrom.Value.Date.ToString("yyyy-MM-dd"), DateTo.Value.Date.ToString("yyyy-MM-dd"));
                DataGridPrice.DataSource = PriceManagementTable;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtPartNo.Textt = "";
                DateFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
                DateTo.Value = DateTime.Now;
                PriceManagementTable.Rows.Clear();
                PartsTable.Rows.Clear();
                TxtPartNo.Focus();
            }
        }

        int CurrentCol = 1;
        DataGridView CurrentDgv = new DataGridView();
        DataTable CurrentTable = new DataTable();
        private void DataGridPrice_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetColumnSearch(DataGridPrice, 225, 6);
            if (!TxtColumnSearch.Visible && PriceManagementTable.Rows.Count > 0)
            {
                CurrentDgv = DataGridPrice;
                CurrentTable = PriceManagementTable;
                if (DataGridPrice.Rows.Count == 0)
                {
                    PriceManagementTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridPrice.Columns[e.ColumnIndex].HeaderText;
                TxtColumnSearch.Visible = true;
                TxtColumnSearch.Focus();
            }
        }

        private void DataGridParts_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetColumnSearch(DataGridPart, 300, 3);
            int CostIndex = DataGridPart.Columns["UnitCost"].Index;
            int PriceIndex = DataGridPart.Columns["ListPrice"].Index;
            if (!TxtColumnSearch.Visible && PartsTable.Rows.Count > 0 && e.ColumnIndex != CostIndex && e.ColumnIndex != PriceIndex)
            {
                CurrentDgv = DataGridPart;
                CurrentTable = PartsTable;
                if (DataGridPart.Rows.Count == 0)
                {
                    PartsTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridPart.Columns[e.ColumnIndex].HeaderText;
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

        private void GetColumnSearch(DataGridView dgv, int boxwidth, int widthdivider)
        {
            TxtColumnSearch = Helper.ColoumnSearcher(dgv, 16, boxwidth);
            TxtColumnSearch.Location = new Point(dgv.Width / widthdivider, 50);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
        }

        private void frm_price_management_archive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && !TxtColumnSearch.Visible)
            {
                BtnClose.PerformClick();
            }
        }

        private void DataGridPrice_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            PartsTable = _PriceManagementController.PricePartsDataTable(DataGridPrice.Rows[e.RowIndex].Cells["ControlNo"].Value.ToString());
            DataGridPart.DataSource = PartsTable;
        }
    }
}
