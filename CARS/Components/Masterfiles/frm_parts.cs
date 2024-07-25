using CARS.Model;
using CARS.Model.Masterfiles;
using CARS.Controllers.Masterfiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CARS.Functions;
using CARS.Model.Utilities;

namespace CARS.Components.Masterfiles
{
    public partial class frm_parts : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private PartsController _PartsController = new PartsController();
        private PartsModel _PartsModel = new PartsModel();
        private SortedDictionary<string, string> _MeasurementDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _BrandDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _DescriptionDictionary = new SortedDictionary<string, string>();
        private DataTable PartTable = new DataTable();
        private TextBox TxtColumnSearch = new TextBox();

        public frm_parts(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _MeasurementDictionary = _PartsController.GetDictionary("Uom");
            _BrandDictionary = _PartsController.GetDictionary("Brand");
            _DescriptionDictionary = _PartsController.GetDictionary("Description");
            ComboUomFilter.DataSource = new BindingSource(_MeasurementDictionary, null);
            ComboBrandFilter.DataSource = new BindingSource(_BrandDictionary, null);
            ComboDescription.DataSource = new BindingSource(_DescriptionDictionary, null);
            ComboBrandFilter.DisplayMember = ComboUomFilter.DisplayMember = ComboDescription.DisplayMember = "Key";
            ComboBrandFilter.ValueMember = ComboUomFilter.ValueMember = ComboDescription.ValueMember = "Value";
            TxtColumnSearch = Helper.ColoumnSearcher(DataGridPart, 20, 300);
            TxtColumnSearch.KeyUp += TxtColumnSearch_KeyUp;
            TxtColumnSearch.Leave += TxtColumnSearch_Leave;
            dashboardCall = DashboardCall;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            _PartsModel = new PartsModel { PartNo = TxtPartNoFilter.Textt.TrimEnd(), PartName = TxtPartNameFilter.Textt.TrimEnd(), OtherName = TxtOtherNameFilter.Textt.TrimEnd(), 
                                           PartApplication = TxtApplicationFilter.Textt.TrimEnd(), Sku = TxtSkuFiIter.Textt.TrimEnd(), Brand = ComboBrandFilter.SelectedValue.ToString().TrimEnd(),
                                           Uom = ComboUomFilter.SelectedValue.ToString().TrimEnd(), Description = ComboDescription.SelectedValue.ToString() };
            PartTable = _PartsController.dt(_PartsModel);
            DataGridPart.DataSource = PartTable;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to clear the filter field?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                TxtPartNoFilter.Textt = TxtPartNameFilter.Textt = TxtOtherNameFilter.Textt = TxtSkuFiIter.Textt = TxtApplicationFilter.Textt = "";
                ComboBrandFilter.SelectedIndex = ComboUomFilter.SelectedIndex = ComboDescription.SelectedIndex = 0;
                PartTable.Rows.Clear();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            //Bitmap bmp = Helper.ModalEffect(this.PointToScreen(new Point(0, 0)), this.ClientRectangle);

            //using (Panel p = new Panel())
            //{
            //    p.Location = new Point(0, 0);
            //    p.Size = this.ClientRectangle.Size;
            //    p.BackgroundImage = bmp;
            //    this.Controls.Add(p);
            //    p.BringToFront();

            //    frm_parts_encodeNew encode = new frm_parts_encodeNew("");
            //    encode.ShowDialog(this);
            //}
            frm_parts_encodeNew encode = new frm_parts_encodeNew("");
            encode.ShowDialog(this);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (DataGridPart.CurrentRow != null)
            {
                //Bitmap bmp = Helper.ModalEffect(this.PointToScreen(new Point(0, 0)), this.ClientRectangle);

                //using (Panel p = new Panel())
                //{
                //    p.Location = new Point(0, 0);
                //    p.Size = this.ClientRectangle.Size;
                //    p.BackgroundImage = bmp;
                //    this.Controls.Add(p);
                //    p.BringToFront();

                //    frm_parts_encodeNew encode = new frm_parts_encodeNew(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString());
                //    encode.SearchButton += BtnSearch_Click;
                //    encode.ShowDialog(this);
                //}
                frm_parts_encodeNew encode = new frm_parts_encodeNew(DataGridPart.CurrentRow.Cells["PartNo"].Value.ToString());
                encode.SearchButton += BtnSearch_Click;
                encode.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Please search and select a data first.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        int CurrentCol = 1;
        private void DataGridPart_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int PriceIndex = DataGridPart.Columns["ListPrice"].Index;
            int ActiveIndex = DataGridPart.Columns["IsActive"].Index;
            if (!TxtColumnSearch.Visible && PartTable.Rows.Count > 0 && e.ColumnIndex != PriceIndex && e.ColumnIndex != ActiveIndex)
            {
                if (DataGridPart.Rows.Count == 0)
                {
                    PartTable.DefaultView.RowFilter = "";
                }
                CurrentCol = e.ColumnIndex;
                //if (e.ColumnIndex == PriceIndex)
                //{
                //    TxtColumnSearch.KeyPress += Helper.Numeric_KeyPress;
                //}
                //else
                //{
                //    TxtColumnSearch.KeyPress -= Helper.Numeric_KeyPress;
                //}
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
                //if (CurrentCol == DataGridPart.Columns["ListPrice"].Index && valueSearch != "")
                //{
                //    bs.Filter = $"[{ searchCol}] = '{valueSearch}'";
                //}
                //else if (CurrentCol != DataGridPart.Columns["ListPrice"].Index)
                //{
                //    bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                //}
                bs.Filter = $"[{ searchCol}] LIKE '%{valueSearch}%'";
                DataGridPart.DataSource = bs;
            }
        }

        private void TxtColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtColumnSearch.Visible = false;
            DataGridPart.Focus();
        }

        private void frm_parts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !TxtColumnSearch.Visible)
            {
                if (PnlFilter.ContainsFocus)
                {
                    BtnSearch.PerformClick();
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }

        private void DataGridPart_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                BtnEditNew.PerformClick();
            }
        }
    }
}
