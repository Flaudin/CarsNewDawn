using CARS.Controller.Masterfiles;
using CARS.Controllers.Masterfiles;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace CARS.Components.Masterfiles
{
    public partial class frm_customer_encode : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private CustomerController customerController = new CustomerController();
        private CustomerModel customerModel = new CustomerModel();
        private SortedDictionary<string, string> _cityDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _termsDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _provinceDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _regionDictionary = new SortedDictionary<string, string>();
        private CustomerModel _customerDetails = new CustomerModel();
        private bool isinitial = true;
        DataTable ContactTable = new DataTable();

        public frm_customer_encode(string slid, string header)
        {
            InitializeComponent();
            BtnClose.BackColor = PnlHeader.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderContacts.BackColor = PnlHeaderDetails.BackColor = PnlHeaderAddress.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            BtnClose.ForeColor = LblHeader.ForeColor = LblDetails.ForeColor = LblAddress.ForeColor = LblContacts.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            _cityDictionary = customerController.GetCity();
            cmbCity.DataSource = new BindingSource(_cityDictionary, null);
            cmbCity.DisplayMember = "Key";
            cmbCity.ValueMember = "Value";

            _termsDictionary = customerController.getTerms();
            cmbTerms.DataSource = new BindingSource(_termsDictionary, null);
            cmbTerms.DisplayMember = "Value";
            cmbTerms.ValueMember = "Key";


            this._customerDetails = this.GetCustomer(slid);
            this._customerDetails.ContactList = this.GetSupplierContact(slid);
            TxtCustomer.Textt = slid;
            LblHeader.Text = header;

            dtContacts.EditingControlShowing += dtContacts_EditingControlShowing;
            dtContacts.DataSource = ContactTable;

            //cmbRegion.SelectedIndexChanged += cmbProvince_SelectedIndexChanged;

            if (this._customerDetails.VATType == 0) { radVat1.Checked = true; }
            else if (this._customerDetails.VATType == 1) { radVat3.Checked = true; }
            else if (this._customerDetails.VATType == 2) { radVat5.Checked = true; } else { radVat4.Checked = true; }
            txtRegisName.Textt = this._customerDetails.RegName;
            txtTin.Textt = this._customerDetails.TinNo;
            txtWeb.Textt = this._customerDetails.Website;
            txtEmail.Textt = this._customerDetails.EmailAdd;
            txtStreet.Textt = this._customerDetails.NoStreet;
            txtName.Textt = this._customerDetails.SLName;
            cmbTerms.SelectedValue = _customerDetails.TermID;
            cmbCity.SelectedValue = _customerDetails.CityID;
            txtBusinessName.Textt = this._customerDetails.BusinessName;
            txtFlrNo.Textt = this._customerDetails.FlrNo;
            txtBrgy.Textt = this._customerDetails.Brgy;
            //cmbProvince.SelectedValue = _customerDetails.ProvID;
            //cmbRegion.SelectedValue = _customerDetails.RegionID;
            radVat1.Tag = 0; radVat3.Tag = 1;
            radVat5.Tag = 2; radVat4.Tag = 3;
            dtContacts.AutoGenerateColumns = false;
            dtContacts.DataSource = this._customerDetails.ContactList;
        }



        private CustomerModel GetCustomer(string code) => customerController.get(code);
        private List<CustomerContactModel> GetSupplierContact(string slid) => customerController.getContacts(slid);

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close Customer encode?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Close();
            }
        }

        private decimal getVatType()
        {
            decimal selectedVatType = 0;

            if (radVat1.Checked)
            {
                selectedVatType = decimal.Parse(radVat1.Tag.ToString());
            }
            else if (radVat2.Checked)
            {
                selectedVatType = decimal.Parse(radVat3.Tag.ToString());
            }
            else if (radVat3.Checked)
            {
                selectedVatType = decimal.Parse(radVat3.Tag.ToString());
            }
            else if (radVat5.Checked)
            {
                selectedVatType = decimal.Parse(radVat5.Tag.ToString());
            }
            else if (radVat4.Checked)
            {
                selectedVatType = decimal.Parse(radVat4.Tag.ToString());
            }
            else if (radVat5.Checked)
            {
                selectedVatType = decimal.Parse(radVat5.Tag.ToString());
            }
            //MessageBox.Show("Selected VAT Value " + selectedVatType);
            return selectedVatType;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string createMessage = "";
            foreach (DataGridViewRow rows in dtContacts.Rows)
            {
                if (rows.Cells["EmailAdd"].Value != null)
                {
                    string email = rows.Cells["EmailAdd"].Value.ToString();
                    ValidateEmail(email);
                }
            }
            if (txtName.Textt == "" || txtRegisName.Textt == "" || txtTin.Textt == "" || cmbTerms.SelectedIndex < -1 || txtWeb.Textt == ""
                || txtEmail.Textt == "" || txtStreet.Textt == "" || //txt ||
                cmbCity.SelectedIndex < -1 || txtBusinessName.Textt == "" || txtFlrNo.Textt == "" || txtBrgy.Textt == "")//cmbProvince.SelectedIndex < -1)
            {
                Helper.Confirmator("Please complete all required fields", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ValidateEmail(txtEmail.Textt);
                decimal selectedVatType = getVatType();
                //string selectedCustomerType = getCustomerType();
                List<CustomerContactModel> customerContactList = new List<CustomerContactModel>();
                if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    foreach (DataGridViewRow row in dtContacts.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            CustomerContactModel contactModel = new CustomerContactModel
                            {
                                SLID = TxtCustomer.Textt.ToString(),
                                UniqueID = getUniqueID(row.Index),
                                ContactPerson = row.Cells[2].Value.ToString(),
                                ContactNo = row.Cells[3].Value.ToString(),
                                EmailAdd = row.Cells[4].Value.ToString(),
                                Remarks = row.Cells[5].Value.ToString(),
                                IsActive = Convert.ToBoolean(row.Cells[6].Value),
                            };
                            customerContactList.Add(contactModel);
                        }
                    }
                    customerModel = new CustomerModel
                    {
                        SLID = TxtCustomer.Textt.ToString(),
                        RegName = txtRegisName.Textt,
                        SLName = txtName.Textt,
                        SLType = "C",
                        SupplierType = "",
                        VATType = selectedVatType,
                        TinNo = txtTin.Textt,
                        NoStreet = txtStreet.Textt,
                        CityID = cmbCity.SelectedValue.ToString().TrimEnd(),
                        ProvID = lblProvince.Text,
                        RegionID = lblRegion.Text,
                        EmailAdd = txtEmail.Textt,
                        Website = txtWeb.Textt,
                        BusinessName = txtBusinessName.Textt,
                        FlrNo = txtFlrNo.Textt,
                        Brgy = txtBrgy.Textt,
                        TermID = cmbTerms.SelectedValue.ToString().TrimEnd(),
                        ContactList = customerContactList
                    };
                    if (LblHeader.Text == "CUSTOMER ENTRY")
                    {
                        createMessage = customerController.Create(customerModel);
                        Helper.Confirmator(createMessage, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (createMessage == "Information saved successfully")
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        createMessage = customerController.Update(customerModel);
                        if (Helper.Confirmator(createMessage, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information))
                        {
                            this.Close();
                        }
                    }
                }
            }
        }

        private string getUniqueID(int rowindex)
        {
            string uniqueid = "";
            if (dtContacts.Rows[rowindex].Cells[0].Value.ToString() != "")
            {
                uniqueid = dtContacts.Rows[rowindex].Cells[0].Value.ToString();
            }
            else
            {
                uniqueid = Helper.GenerateUID();
            }
            return uniqueid;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Unsaved entries will be discarded. Are you sure you want to clear the input field(s)?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                txtWeb.Textt = "";
                txtEmail.Textt = "";
                txtName.Textt = "";
                txtRegisName.Textt = "";
                txtStreet.Textt = "";
                txtFlrNo.Textt = "";
                txtBrgy.Textt = "";
                lblRegion.Text = "";
                txtTin.Textt = "";
                cmbTerms.SelectedIndex = 0;
                if (radVat1.Checked || radVat3.Checked || radVat5.Checked || radVat4.Checked)
                {
                    radVat3.Checked = false; radVat5.Checked = false; radVat4.Checked = false;
                    radVat1.Checked = true;
                }
            }
        }


        private void btnAddContact_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn asd in dtContacts.Columns)
            {
                Type columnType = asd.ValueType ?? typeof(string);
                DataColumn col = new DataColumn(asd.Name, columnType);
                dt.Columns.Add(col);
            }
            if (dtContacts.Rows.Count != 0)
            {
                int lastrowindex = dtContacts.Rows.Count - 1;
                bool lastrowcheck = false;
                foreach (DataGridViewRow dgr in dtContacts.Rows)
                {
                    DataRow dataRow2 = dt.NewRow();
                    foreach (DataGridViewCell dgc in dgr.Cells)
                    {
                        string name = dtContacts.Columns[dgc.ColumnIndex].Name;
                        dataRow2[name] = dgc.Value;
                        //if (string.IsNullOrWhiteSpace(dgc.Value.ToString()) && (dgc.ColumnIndex == 1 || dgc.ColumnIndex == 2 || dgc.ColumnIndex == 3)) 
                        //{
                        //    lastrowcheck = false;
                        //    break;
                        //}
                        if (lastrowindex == dgr.Index)
                        {
                            if (dgc.Value != null && !string.IsNullOrWhiteSpace(dgc.Value.ToString()))
                            {
                                lastrowcheck = true;
                            }
                        }
                    }

                    dt.Rows.Add(dataRow2);
                }
                if (lastrowcheck)
                {
                    lastrowindex = lastrowindex + 1;
                    dt.Rows.Add(false, "", "", "", "", "", false, true);
                    dtContacts.DataSource = dt;
                }
                else
                {
                    Helper.Confirmator("Please complete the fields", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //dtContacts.DataSource = dt;
            }
            else
            {
                dtContacts.DataSource = dt;
                dt.Rows.Add("", false, "", "", "", "", false, true);
            }
        }

        private void btnDeleteContact_Click(object sender, EventArgs e)
        {
            int newIndex = dtContacts.Columns["New"].Index;

            if (dtContacts.Rows.Count == 0)
            {
                MessageBox.Show("There are no rows to delete.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Helper.Confirmator("Are you sure you want to delete the selected rows?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                foreach (DataGridViewRow row in dtContacts.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["New"].Value) && Convert.ToBoolean(row.Cells["IsSelected"].Value))
                    {

                        dtContacts.Rows.Remove(row);
                    }
                }
            }
        }
        // Collect rows to delete
        //List<DataGridViewRow> rowsToDelete = new List<DataGridViewRow>();
        //foreach (DataGridViewRow row in dtContacts.Rows)
        //{
        //    if (Convert.ToBoolean(row.Cells[newIndex].Value))
        //    {
        //        rowsToDelete.Add(row);
        //    }
        //}

        //if (rowsToDelete.Count == 0)
        //{
        //    MessageBox.Show("No rows are selected for deletion.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    return;
        //}

        //if (Helper.Confirmator("Are you sure you want to delete the selected rows?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        //{
        //    foreach (DataGridViewRow row in rowsToDelete)
        //    {
        //        DataRowView rowView = row.DataBoundItem as DataRowView;
        //        if (rowView != null)
        //        {
        //            rowView.Row.Delete();
        //        }
        //    }
        //    dtContacts.Refresh();
        //}

        private void dtContacts_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textBox)
            {
                textBox.KeyPress -= TextBox_KeyPressToUpper;

                int columnIndex = dtContacts.CurrentCell.ColumnIndex;
                int name = (int)dtContacts.Columns["ContactPerson"].Index;
                int contact = (int)dtContacts.Columns["ContactNo"].Index;
                int email = (int)dtContacts.Columns["EmailAdd"].Index;
                int remark = (int)dtContacts.Columns["Remarks"].Index;
                if (columnIndex == name ||
                    columnIndex == remark)
                {
                    textBox.KeyPress -= TextBox_KeyPressNumeric;
                    textBox.KeyPress += TextBox_KeyPressToUpper;
                }
                else if (columnIndex == contact)
                {
                    textBox.KeyPress += TextBox_KeyPressNumeric;
                    textBox.KeyPress -= TextBox_KeyPressToUpper;
                }
                else if (columnIndex == email)
                {
                    textBox.KeyPress += TextBox_KeyPressToLower;
                    textBox.KeyPress -= TextBox_KeyPressNumeric;
                }
            }
        }

        private void TextBox_KeyPressNumeric(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void TextBox_KeyPressToUpper(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void TextBox_KeyPressToLower(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToLower(e.KeyChar);
        }

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCity.SelectedItem is KeyValuePair<string, string> selectedCity &&
        !string.IsNullOrEmpty(selectedCity.Key))
            {
                Dictionary<string, string> keyvaluepairs = customerController.getSelectedProvice(selectedCity.Key);
                KeyValuePair<string, string> selectedProvince = keyvaluepairs.First();
                KeyValuePair<string, string> selectedRegion = keyvaluepairs.Last();
                lblProvince.Text = selectedProvince.Key;
                lblRegion.Text = selectedRegion.Value;
            }
            else
            {
                // Optionally, handle the case when the selected item is null or empty
                lblProvince.Text = string.Empty;
            }
        }

        private void ValidateEmail(string email)
        {
            if (IsValidEmail(email))
            {
                return;
            }
            else
            {
                string title = "Invalid email address, Please try again.";
                Helper.Confirmator(title, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))

                return false;

            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
                return regex.IsMatch(email);
            }
            catch (Exception)
            {
                return false;
            }

        }

        private void dtContacts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dtContacts.Columns["EmailAdd"].Index)
            {
                string email = dtContacts.Rows[e.RowIndex].Cells["EmailAdd"].Value.ToString();
                ValidateEmail(email);
            }
        }
    }
}
