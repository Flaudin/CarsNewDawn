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

namespace CARS.Components.Transactions.SalesOrder
{
    public partial class frm_sales_order_parts_encode : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private TransactionController _TransactionController = new TransactionController();
        private SalesOrderController _SalesOrderController = new SalesOrderController();
        private SortedDictionary<string, string> _BrandDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _DescriptionDictionary = new SortedDictionary<string, string>();
        private List<string> PartsList = new List<string>();
        private int SOPartCounter = 0;
        public event Action<List<dynamic[]>> StringArraySent;
        private DataTable PartTable  = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_sales_order_parts_encode(List<string> Parts,int currentPartsCount)
        {
            InitializeComponent();
            PnlHeader.BackColor = BtnClose.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderTable.BackColor = PnlHeaderFilter.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblHeader.ForeColor = LblFilter.ForeColor = LblTable.ForeColor = BtnClose.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            PartsList = Parts;
            SOPartCounter = currentPartsCount;
            _BrandDictionary = _TransactionController.GetDictionary("Brand");
            _DescriptionDictionary = _TransactionController.GetDictionary("Description");
            ComboBrand.DataSource = new BindingSource(_BrandDictionary, null);
            ComboDescription.DataSource = new BindingSource(_DescriptionDictionary, null);
            ComboBrand.DisplayMember = ComboDescription.DisplayMember = "Key";
            ComboBrand.ValueMember = ComboDescription.ValueMember = "Value";
            DataGridPart.KeyDown += new KeyEventHandler(DataGridPart_KeyDown);
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridPart, 16, 300);
            TxtColumnSearch.Location = new Point(DataGridPart.Width / 3, 50);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
        }

        private void frm_stock_transfer_parts_encode_Load(object sender, EventArgs e)
        {
            
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
            int checkedRowCount = PartTable.AsEnumerable()
            .Where(row => Convert.ToBoolean(row["ForSelection"]))
            .Count();
            if (checkedRowCount > 0)
            {
                int checkedRowWithHitCount = 0;
                foreach (string str in PartsList)
                {
                    checkedRowWithHitCount += PartTable.AsEnumerable()
                    .Where(row => Convert.ToBoolean(row["ForSelection"]) && row["PartNo"].ToString() == str)
                    .Count();
                }

                if (SOPartCounter + (checkedRowCount - checkedRowWithHitCount) <= 27)
                {
                    List<dynamic[]> stringArrayToSend = new List<dynamic[]>();
                    foreach (DataRow row in PartTable.Rows)
                    {
                        if (Convert.ToBoolean(row["ForSelection"]))
                        {
                            stringArrayToSend.Add(new[] { row["PartNo"].ToString(), row["PartName"].ToString(), row["DescName"].ToString(), row["BrandName"].ToString(),
                                                  row["Sku"].ToString(), row["UomName"].ToString(), Convert.ToDecimal(row["ListPrice"]).ToString("N2")});
                        }
                    }
                    StringArraySent?.Invoke(stringArrayToSend);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sales Order only allows twenty six parts per order, sorry for the inconvenience.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a part before proceeding.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                ComboBrand.Text = ComboDescription.Text = TxtKeyword.Textt = "";
                ImagePart.Image = null;
                PartTable.Rows.Clear();
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
                bool isChecked = Convert.ToBoolean(DataGridPart.Rows[e.RowIndex].Cells["ForSelection"].Value ?? false);
                DataGridPart.Rows[e.RowIndex].Cells["ForSelection"].Value = !isChecked;
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
                    }
                }
                DataGridPart.EndEdit();
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

        private void frm_stock_transfer_parts_encode_KeyDown(object sender, KeyEventArgs e)
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

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            PartTable = _SalesOrderController.PartsWithBegBalDataTable( TxtKeyword.Textt.TrimEnd());
            var boolColumn = new DataColumn("ForSelection", typeof(bool));
            boolColumn.DefaultValue = false;
            PartTable.Columns.Add(boolColumn);
            DataGridPart.DataSource = PartTable;
            DataGridPart.ClearSelection();
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

        private void DataGridPart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //var senderGrid = (DataGridView)sender;

            //if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
            //{
            //    var uncheckedRows = DataGridPart.Rows.Cast<DataGridViewRow>()
            //                        .Where(row => !(bool)row.Cells["ForSelection"].Value);

            //    if (uncheckedRows.Any())
            //    {
            //        AllSelected = false;
            //    }
            //    else
            //    {
            //        AllSelected = true;
            //    }
            //}
        }
    }
}
