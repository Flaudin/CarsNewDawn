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

namespace CARS.Components.Transactions.SalesReturn
{
    public partial class frm_sales_return_reason : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private TransactionController _TransactionController = new TransactionController();
        private SalesReturnController _SalesReturnController = new SalesReturnController();
        public event Action<string, string> StringSent;
        private DataTable ReasonTable  = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();
        private string Reason = "";

        public frm_sales_return_reason(string reason)
        {
            InitializeComponent();
            PnlHeader.BackColor = BtnClose.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblHeader.ForeColor = BtnClose.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            ReasonTable = _SalesReturnController.ReasonDataTable();
            var boolColumn = new DataColumn("ForSelection", typeof(bool));
            boolColumn.DefaultValue = false;
            ReasonTable.Columns.Add(boolColumn);
            DataGridReason.DataSource = ReasonTable;
            DataGridReason.ClearSelection();
            Reason = reason;
            DataGridReason.KeyDown += new KeyEventHandler(DataGridPart_KeyDown);
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridReason, 16, 200);
            TxtColumnSearch.Location = new Point(DataGridReason.Width / 3, 50);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
        }

        private void frm_sales_return_parts_encode_Load(object sender, EventArgs e)
        {
            if (Reason != "")
            {
                var firstCheckedRow = DataGridReason.Rows.Cast<DataGridViewRow>()
                          .FirstOrDefault(r => r.Cells["ReasonName"].Value.ToString() == Reason);
                firstCheckedRow.Cells["ForSelection"].Value = true;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close Reason selection?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var selectedRow = DataGridReason.Rows.Cast<DataGridViewRow>()
                              .Where(r => Convert.ToBoolean(r.Cells["ForSelection"].Value) == true)
                              .Select(r => new
                              {
                                  Name = r.Cells["ReasonName"].Value.ToString(),
                                  Id = r.Cells["ReasonID"].Value.ToString()
                              }).ToList();
            string reason = selectedRow.FirstOrDefault().Name;
            string id = selectedRow.FirstOrDefault().Id;
            StringSent?.Invoke(reason, id);
            this.Close();
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
            foreach (DataGridViewRow row in DataGridReason.Rows)
            {
                if (row.Index != DataGridReason.CurrentCell.RowIndex)
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
        private void DataGridPart_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!TxtColumnSearch.Visible && ReasonTable.Rows.Count > 0)
            {
                if (DataGridReason.Rows.Count == 0)
                {
                    ReasonTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + DataGridReason.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = DataGridReason.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = ReasonTable;
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridReason.DataSource = bs;
            }
        }

        private void TxtColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtColumnSearch.Visible = false;
        }

        private void frm_sales_return_parts_encode_KeyDown(object sender, KeyEventArgs e)
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
