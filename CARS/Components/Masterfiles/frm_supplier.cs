using CARS.Controller.Masterfiles;
using CARS.Functions;
using CARS.Model.Masterfiles;
using CARS.Model.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Masterfiles
{
    public partial class frm_supplier : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private SupplierController controller = new SupplierController();
        private List<SupplierContactModel> supplierContactModelsList;
        private SupplierModel _supplierModel;
        private SortedDictionary<string, string> _termsDictionary = new SortedDictionary<string, string>();
        private TextBox TxtSuppColumnSearch = new TextBox();
        private DataTable SupplierTable = new DataTable();

        public frm_supplier(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            supplierContactModelsList = GetContactList();
            _termsDictionary = controller.getTerms();
            cmbTerms.DataSource = new BindingSource(_termsDictionary, null);
            cmbTerms.DisplayMember = "Value";
            cmbTerms.ValueMember = "Key";
            dashboardCall = DashboardCall;

            TxtSuppColumnSearch = Helper.ColoumnSearcher(tblSupplierList, 20, 300);
            TxtSuppColumnSearch.KeyUp += TxtPartsColumnSearch_KeyUp;
            TxtSuppColumnSearch.Leave += TxtPartsColumnSearch_Leave;
            TxtSuppColumnSearch.Location = new Point(/*tblSupplierList.Width*/ 620, 30);
        }


        private List<SupplierContactModel> GetContactList()
        {
            List<SupplierContactModel> contact = new List<SupplierContactModel>();

            contact.Add(new SupplierContactModel { });

            return contact;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            frm_supplier_encode encoding = new frm_supplier_encode("","SUPPLIER ENTRY");
            encoding.ShowDialog();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string getselectedsuptype = getsupType();
            decimal getselectedvatype = getvatType();
            _supplierModel = new SupplierModel { 
                SLName = txtSLName.Textt.TrimEnd(), 
                RegName = txtRegname.Textt.TrimEnd(),
                TermID = cmbTerms.SelectedValue.ToString().TrimEnd(), 
                SupplierType = getselectedsuptype, 
                VATType = getselectedvatype, 
                TinNo = txtTinNo.Textt };

            SupplierTable = controller.dt(_supplierModel);
            if (SupplierTable.Rows.Count !=0)
            {
            tblSupplierList.DataSource = SupplierTable;
            tblSupplierList.ClearSelection();
            }
            else
            {
                Helper.Confirmator("No Results found", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string getsupType()
        {
            string selectedSupType = "";
            if (supType1.Checked)
            {
                selectedSupType = "L";
            }else if (supType2.Checked) 
            {
                selectedSupType = "I";
            }
            else
            {
                selectedSupType = "ALL";
            }
            return selectedSupType;
        }

        private decimal getvatType()
        {
            decimal selectedVatType = 0;
            
            if (vatType1.Checked)
            {
                selectedVatType = 0;
            }
            else if(vatType2.Checked)
            {
                selectedVatType = 1;
            }
            else if (vatType3.Checked)
            {
                selectedVatType = 2;
            }
            else if (vatType4.Checked)
            {
                selectedVatType = 3;
            }
            else if (vatType5.Checked)
            {
                selectedVatType = 3;
            }
            else if(vatType6.Checked)
            {
                selectedVatType = 5;
            }
            return selectedVatType;
        }

        private void btnSupplierEdit_Click(object sender, EventArgs e)
        {
            if(tblSupplierList.CurrentRow != null)
            {
            frm_supplier_encode encodingEdit = new frm_supplier_encode(tblSupplierList.CurrentRow.Cells[0].Value.ToString(),"EDIT "+ tblSupplierList.CurrentRow.Cells[0].Value.ToString());
           encodingEdit.ShowDialog(this);   
            }
            else
            {
                Helper.Confirmator("Please search and select a data.","System Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private SupplierModel GetSupplierModel() => this._supplierModel;

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to clear the data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                txtRegname.Textt = "";
                txtSLName.Textt = "";
                txtTinNo.Textt = "";
                if(supType1.Checked || supType2.Checked)
                {
                    supType1.Checked = false;
                    supType2.Checked = false;
                    supDefault.Checked = true;
                }
                if(vatType1.Checked || vatType2.Checked || vatType3.Checked || vatType4.Checked)
                {
                    vatType1.Checked = false;
                    vatType2.Checked = false;
                    vatType3.Checked = false;
                    vatType4.Checked = false;
                    vatType5.Checked = false;
                    vatType6.Checked = true;
                }
                cmbTerms.SelectedIndex = 0;
                SupplierTable.Rows.Clear();
            }
        }
        int currentSuppCol =1;
        private void tblSupplierList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!TxtSuppColumnSearch.Visible && tblSupplierList.Rows.Count > 0)
            {
                if (tblSupplierList.Rows.Count == 0)
                {
                    //DataTable dt = dgvOrders.DataSource as DataTable;
                    SupplierTable.DefaultView.RowFilter = "";
                }
                currentSuppCol = e.ColumnIndex;
                TxtSuppColumnSearch.Text = "Search " + tblSupplierList.Columns[e.ColumnIndex].HeaderText;
                TxtSuppColumnSearch.Visible = true;
                TxtSuppColumnSearch.Focus();
            }
        }
        private void TxtPartsColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtSuppColumnSearch.Visible = false;
        }

        private void TxtPartsColumnSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                TxtSuppColumnSearch.Visible = false;
            }
            else
            {
                string searchCol = tblSupplierList.Columns[currentSuppCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtSuppColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = SupplierTable;
                bs.Filter = $"[{searchCol}] LIKE '%{valueSearch}%'";
                tblSupplierList.DataSource = bs;
                tblSupplierList.ClearSelection();
            }
        }


        private void tblSupplierList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSupplierEdit_Click(sender, e);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if(Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }
    }
}
