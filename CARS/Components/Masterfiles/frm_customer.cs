using CARS.Controller.Masterfiles;
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

namespace CARS.Components.Masterfiles
{
    public partial class frm_customer : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private CustomerController controller = new CustomerController();
        private List<CustomerContactModel> customerContactModelsList;
        private CustomerModel _customerModel;
        private SortedDictionary<string, string> _termsDictionary = new SortedDictionary<string, string>();
        private TextBox TxtCustomerColumnSearch = new TextBox();
        private DataTable CustomerTable  = new DataTable();
        public frm_customer(Action DashboardCall)
        {
            InitializeComponent();
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderFilter.BackColor = PnlHeaderTable.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            LblFilter.ForeColor = LblTable.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            customerContactModelsList = GetContactList();
            _termsDictionary = controller.getTerms();
            cmbTerms.DataSource = new BindingSource(_termsDictionary, null);
            cmbTerms.DisplayMember = "Value";
            cmbTerms.ValueMember = "Key";
            dashboardCall = DashboardCall;

            TxtCustomerColumnSearch = Helper.ColoumnSearcher(tblCustomer, 20, 300);
            TxtCustomerColumnSearch.KeyUp += TxtCustomerColumnSearch_KeyUp;
            TxtCustomerColumnSearch.Leave += TxtCustomerColumnSearch_Leave;
            TxtCustomerColumnSearch.Location = new Point(tblCustomer.Width /12, 30);
        }


        private List<CustomerContactModel> GetContactList()
        {
            List<CustomerContactModel> contact = new List<CustomerContactModel>();

            contact.Add(new CustomerContactModel { });

            return contact;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            frm_customer_encode encoding = new frm_customer_encode("","CUSTOMER ENTRY");
            encoding.ShowDialog(this);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to clear the data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                txtRegname.Textt = "";
                txtSLName.Textt = "";
                txtTinNo.Textt = "";
                if (vatType1.Checked || vatType4.Checked || vatType3.Checked || vatType5.Checked)
                {
                    vatType1.Checked = true;
                    vatType4.Checked = false;
                    vatType3.Checked = false;
                    vatType5.Checked = false;
                    vatType2.Checked = false;
                }
                cmbTerms.SelectedIndex = 0;
                CustomerTable.Rows.Clear();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (tblCustomer.CurrentRow != null)
            {
                frm_customer_encode encodingEdit = new frm_customer_encode(tblCustomer.CurrentRow.Cells[0].Value.ToString(), "EDIT " + tblCustomer.CurrentRow.Cells[0].Value.ToString());
                encodingEdit.ShowDialog(this);
            }
            else
            {
                Helper.Confirmator("Please search and select a data.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            decimal getselectedvatype = getvatType();
            _customerModel = new CustomerModel
            {
                SLName = txtSLName.Textt.TrimEnd(),
                RegName = txtRegname.Textt.TrimEnd(),
                TermID = cmbTerms.SelectedValue.ToString().TrimEnd(),
                VATType = getselectedvatype,
                TinNo = txtTinNo.Textt
            };
            CustomerTable = controller.dt(_customerModel);
            tblCustomer.DataSource = CustomerTable;
            if (tblCustomer.Rows.Count <= 0)
            {
                Helper.Confirmator("No Records found", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            tblCustomer.ClearSelection();
        }

        private decimal getvatType()
        {
            decimal selectedVatType = 0;

            if (vatType1.Checked)
            {
                selectedVatType = 0;
            }
            else if (vatType2.Checked)
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
                selectedVatType = 4;
            }
            else if (vatType6.Checked)
            {
                selectedVatType = 5;
            }
            return selectedVatType;
        }

        int currentCustomerCol = 1;
        private void tblCustomer_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!TxtCustomerColumnSearch.Visible && tblCustomer.Rows.Count > 0)
            {
                if (tblCustomer.Rows.Count == 0)
                {
                    //DataTable dt = dgvOrders.DataSource as DataTable;
                    CustomerTable.DefaultView.RowFilter = "";
                }
                currentCustomerCol = e.ColumnIndex;
                TxtCustomerColumnSearch.Text = "Search " + tblCustomer.Columns[e.ColumnIndex].HeaderText;
                TxtCustomerColumnSearch.Visible = true;
                TxtCustomerColumnSearch.Focus();
            }
        }
        private void TxtCustomerColumnSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                TxtCustomerColumnSearch.Visible = false;
            }
            else
            {
                string searchCol = tblCustomer.Columns[currentCustomerCol].Name;
                string valueSearch = Helper.EscapeLikeValue(TxtCustomerColumnSearch.Text.TrimEnd());
                BindingSource bs = new BindingSource();
                bs.DataSource = CustomerTable;
                bs.Filter = $"[{searchCol}] LIKE '%{valueSearch}%'";
                tblCustomer.DataSource = bs;
                tblCustomer.ClearSelection();
            }
        }

        private void TxtCustomerColumnSearch_Leave(object sender, EventArgs e)
        {
            TxtCustomerColumnSearch.Visible = false;
        }

        private void tblCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BtnEdit_Click(sender, e);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("This will close the current form. Proceed?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                dashboardCall?.Invoke();
            }
        }
    }
}
