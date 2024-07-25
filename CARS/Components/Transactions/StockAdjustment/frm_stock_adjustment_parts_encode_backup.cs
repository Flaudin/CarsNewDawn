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
    public partial class frm_stock_adjustment_parts_encode_backup : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private TransactionController _TransactionController = new TransactionController();
        private StockAdjustmentController _StockAdjustmentController = new StockAdjustmentController();
        private List<string> PartsList = new List<string>();
        public event Action<List<dynamic[]>> StringArraySent;
        private DataTable PartTable  = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_stock_adjustment_parts_encode_backup(List<string> Parts)
        {
            InitializeComponent();
            PnlHeader.BackColor = BtnClose.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblHeader.ForeColor = LblTable.ForeColor = BtnClose.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
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
            List<dynamic[]> stringArrayToSend = new List<dynamic[]>();
            foreach (DataRow row in PartTable.Rows)
            {
                if (Convert.ToBoolean(row["ForSelection"]))
                {
                    stringArrayToSend.Add(new[] { row["PartNo"].ToString(), row["PartName"].ToString(), row["OtherName"].ToString(), row["DescName"].ToString(), row["BrandName"].ToString() });
                }
            }
            StringArraySent?.Invoke(stringArrayToSend);
            this.Close();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear all the selected rows that are selected during the current session?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = PartTable;
                bs.RemoveFilter();
                DataGridPart.DataSource = bs;
                foreach (DataGridViewRow row in DataGridPart.Rows)
                {
                    if (!row.Cells["ForSelection"].ReadOnly)
                    {
                        row.Cells["ForSelection"].Value = false;
                    }
                }
            }
        }

        private void DataGridPart_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
            if (e.RowIndex >= 0 && e.ColumnIndex != columnIndex && !DataGridPart.Rows[e.RowIndex].Cells["ForSelection"].ReadOnly)
            {
                bool isChecked = Convert.ToBoolean(DataGridPart.Rows[e.RowIndex].Cells["ForSelection"].Value);
                DataGridPart.Rows[e.RowIndex].Cells["ForSelection"].Value = !isChecked;
            }
        }

        int CurrentCol = 1;
        private void DataGridPart_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!TxtColumnSearch.Visible && DataGridPart.Rows.Count > 0)
            {
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
    }
}
