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

namespace CARS.Components.Transactions.StockTransfer
{
    public partial class frm_stock_transfer_warehouse_encode_to : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private StockTransferController _StockTransferController = new StockTransferController();
        private List<string> BinsList = new List<string>();
        public event Action<List<dynamic>> LocationArray;
        private DataTable BinTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_stock_transfer_warehouse_encode_to(string Bin, string Warehouse, int TransferType, List<string> Bins)
        {
            InitializeComponent();
            PnlHeader.BackColor = BtnClose.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblHeader.ForeColor = LblTable.ForeColor = BtnClose.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            BinTable = _StockTransferController.LocationToDataTable(Bin, Warehouse, TransferType);
            var boolColumn = new DataColumn("ForSelection", typeof(bool));
            boolColumn.DefaultValue = false;
            BinTable.Columns.Add(boolColumn);
            DataGridBin.DataSource = BinTable;
            DataGridBin.ClearSelection();
            BinsList = Bins;
            DataGridBin.KeyDown += new KeyEventHandler(DataGridPart_KeyDown);
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridBin, 16, 250);
            TxtColumnSearch.Location = new Point(DataGridBin.Width/3, 50);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
        }

        private void frm_stock_transfer_warehouse_encode_to_Load(object sender, EventArgs e)
        {
            if (BinsList != null)
            {
                foreach (string str in BinsList)
                {
                    foreach (DataGridViewRow row in DataGridBin.Rows)
                    {
                        if (row.Cells["BinID"].Value.ToString() == str)
                        {
                            DataGridBin.Rows.RemoveAt(row.Index);
                        }
                        BinTable.AcceptChanges();
                    }
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close location selection?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int checkedRowCount = BinTable.AsEnumerable()
            .Where(row => Convert.ToBoolean(row["ForSelection"]))
            .Count();
            if (checkedRowCount > 0)
            {
                List<dynamic> stringArrayToSend = new List<dynamic>();
                foreach (DataRow row in BinTable.Rows)
                {
                    if (Convert.ToBoolean(row["ForSelection"]))
                    {
                        stringArrayToSend.Add(new[] { row["BinName"].ToString(), row["WhName"].ToString(),
                                                    row["BinID"].ToString(), row["WhID"].ToString() });
                    }
                }
                LocationArray?.Invoke(stringArrayToSend);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a bin before proceeding.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear all the selected rows?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                BinTable.DefaultView.RowFilter = "";
                foreach (DataGridViewRow row in DataGridBin.Rows)
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
            foreach (DataGridViewRow row in DataGridBin.Rows)
            {
                if (row.Index != DataGridBin.CurrentCell.RowIndex)
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
            int SelectionIndex = DataGridBin.Columns["ForSelection"].Index;
            if (!TxtColumnSearch.Visible && BinTable.Rows.Count > 0 && e.ColumnIndex != SelectionIndex)
            {
                if (DataGridBin.Rows.Count == 0)
                {
                    BinTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridBin.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = DataGridBin.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = BinTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridBin.DataSource = bs;
            }
        }

        private void TxtColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtColumnSearch.Visible = false;
        }

        private void frm_stock_transfer_warehouse_encode_to_KeyDown(object sender, KeyEventArgs e)
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
