using CARS.Controller.Transactions;
using CARS.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Transactions.Purchase_Return
{
    public partial class frm_purchase_return_parts_selection : Form
    {
        private DataTable PartTable = new DataTable();
        public event Action<List<dynamic[]>> StringArraySent;
        private PurchaseReturnController purchaseReturnController = new PurchaseReturnController();
        private List<string> PartsList = new List<string>();
        private TextBox TxtColumnSearch = new TextBox();
        public frm_purchase_return_parts_selection(List<string> Parts, string rrno)
        {
            InitializeComponent();
            PartTable = purchaseReturnController.PartsSelection(rrno);
            if (PartTable.Rows.Count > 0)
            {
                var boolColumn = new DataColumn("ForSelection", typeof(bool));
                boolColumn.DefaultValue = false;
                PartTable.Columns.Add(boolColumn);
                dgvParts.DataSource = PartTable;
                dgvParts.ClearSelection();
                PartsList = Parts;
                dgvParts.KeyDown += new KeyEventHandler(DataGridPart_KeyDown);
                TxtColumnSearch = Helper.ColoumnSearcher(dgvParts, 16, 300);
                TxtColumnSearch.Location = new Point(dgvParts.Width / 3, 50);
                TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
                TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            }
            else
            {
                MessageBox.Show("There are no parts to return.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frm_purchase_return_parts_selection_Load(object sender, EventArgs e)
        {
            foreach(string str in PartsList)
            {
                foreach(DataGridViewRow row in dgvParts.Rows)
                {
                    if (row.Cells["PartNo"].Value.ToString() == str)
                    {
                        row.Cells["ForSelection"].Value = true;
                        row.Cells["ForSelection"].ReadOnly = true;
                    }    
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if(Helper.Confirmator("Are you sure you want to close Parts selection?","System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            List<dynamic[]> stringArraytoSend = new List<dynamic[]>();
            foreach(DataRow row in PartTable.Rows)
            {
                if (Convert.ToBoolean(row["ForSelection"]))
                {
                    stringArraytoSend.Add(new[] { row["PartNo"].ToString(), row["DescName"].ToString(), row["BrandName"].ToString(),"0",row["TTLQtyRcvd"].ToString(),
                                                  Convert.ToDecimal(row["UnitPrice"]).ToString("N2"), "0", ""});
                }
            }
            StringArraySent?.Invoke(stringArraytoSend);
            this.Close();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear all the selected rows that are selected during the current session?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                PartTable.DefaultView.RowFilter = "";
                foreach (DataGridViewRow row in dgvParts.Rows)
                {
                    if (!row.Cells["ForSelection"].ReadOnly)
                    {
                        row.Cells["ForSelection"].Value = false;
                    }
                }
            }
        }
        void DataGridPart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSelect.PerformClick();
                e.Handled = true;
            }
        }

        int CurrentCol = 1;
        private void DataGridPart_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!TxtColumnSearch.Visible && PartTable.Rows.Count > 0)
            {
                if (dgvParts.Rows.Count == 0)
                {
                    PartTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                TxtColumnSearch.Text = "Search " + dgvParts.Columns[e.ColumnIndex].HeaderText;
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
                string searchCol = dgvParts.Columns[CurrentCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = PartTable;
                bs.Filter = $"[{searchCol}] LIKE '%{valueSearch}%'";
                dgvParts.DataSource = bs;
            }
        }

        private void TxtColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtColumnSearch.Visible = false;
        }
    }
}
