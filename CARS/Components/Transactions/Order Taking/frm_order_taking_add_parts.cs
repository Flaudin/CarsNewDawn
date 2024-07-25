using CARS.Controller.Transactions;
using CARS.Functions;
using CARS.Model.Masterfiles;
using CARS.Model.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Transactions.Order_Taking
{
    public partial class frm_order_taking_add_parts : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private DataTable PartTable = new DataTable();
        public event Action<List<object[]>> StringArraySent;
        private List<string> PartsList = new List<string>();
        private TextBox TxtColumnSearch = new TextBox();
        PartsModel _partsModel = new PartsModel();
        poOrderTakingController _orderTakingController = new poOrderTakingController();
        private SortedDictionary<string, string> _brandsDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _descsDictionary = new SortedDictionary<string, string>();
        public frm_order_taking_add_parts(List<string> Parts)
        {
            InitializeComponent();
            BtnClose.BackColor = PnlHeader.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = PnlPartsTable.BackColor =  Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            BtnClose.ForeColor = LblHeader.ForeColor = LblFilter.ForeColor = LblTable.ForeColor  = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);

            _brandsDictionary = _orderTakingController.getBrands();
            cmbBrands.DataSource = new BindingSource(_brandsDictionary, null);
            cmbBrands.DisplayMember = "Key";
            cmbBrands.ValueMember = "Value";

            _descsDictionary = _orderTakingController.getDescriptions();
            cmbDesc.DataSource = new BindingSource(_descsDictionary, null);
            cmbDesc.DisplayMember = "Value";
            cmbDesc.ValueMember = "Key";

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

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            List<dynamic[]> stringArraytoSend = new List<dynamic[]>();
            foreach (DataRow row in PartTable.Rows)
            {
                if (Convert.ToBoolean(row["ForSelection"]))
                {
                    stringArraytoSend.Add(new object[]
                    {
                "false",
                row["PartNoParts"].ToString(),
                row["PartName"].ToString(),
                row["BrandName"].ToString(),
                row["DescName"].ToString(),
                "0"
                    });
                }
            }
            StringArraySent?.Invoke(stringArraytoSend);
            this.Close();
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
        private void dgvParts_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int SelectioonIndex = dgvParts.Columns["ForSelection"].Index;
            if (!TxtColumnSearch.Visible && PartTable.Rows.Count > 0 && e.ColumnIndex != SelectioonIndex)
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
            else if(e.ColumnIndex == SelectioonIndex)
            {
                bool AllSelected = false;
                var uncheckedRows = dgvParts.Rows.Cast<DataGridViewRow>()
                    .Where(row => !Convert.ToBoolean(row.Cells["ForSelection"].Value));
                if (uncheckedRows.Any())
                {
                    AllSelected = true;
                }
                foreach (DataGridViewRow row in dgvParts.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["ForSelection"] as DataGridViewCheckBoxCell;
                    if (!checkBoxCell.ReadOnly)
                    {
                        checkBoxCell.Value = AllSelected;
                    }
                }
                dgvParts.EndEdit();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PartsModel partsModel = new PartsModel();
            partsModel.PartName = "";
            partsModel.PartNo = "";
            partsModel.Brand = cmbBrands.SelectedValue.ToString();
            partsModel.Description = cmbDesc.SelectedValue.ToString();
            PartTable = _orderTakingController.getPartLists(partsModel, txtSearch.Textt.TrimEnd());
            if (PartTable.Rows.Count == 0)
            {
                Helper.Confirmator("No results found.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var boolColumn = new DataColumn("ForSelection", typeof(bool));
                boolColumn.DefaultValue = false;
                PartTable.Columns.Add(boolColumn);
                dgvParts.DataSource = PartTable;
                dgvParts.ClearSelection();
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Textt = "";
            cmbBrands.SelectedIndex = 0;
            cmbDesc.SelectedIndex = 0;
            rdbtnBsb.Checked = false;
            rdbtnCritItems.Checked = false;
            PartTable.Clear();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if(Helper.Confirmator("Are you sure you want to close Part Selection?","System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
            }
                this.Close();
        }
    }
}
