using CARS.Controller.Masterfiles;
using CARS.Customized_Components;
using CARS.Functions;
using CARS.Model.Masterfiles;
using CARS.Model.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Masterfiles
{
    public partial class frm_supplier_encode : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private SupplierController _supplierController = new SupplierController();
        private SupplierModel _supplierModel = new SupplierModel();
        private SortedDictionary<string, string> _cityDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _termsDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _provinceDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _regionDictionary = new SortedDictionary<string, string>();
        private SupplierModel supplierDetails = new SupplierModel();
        private DataTable ContactTable = new DataTable();
        private bool isinitial = true;
        private string selectedSupplierType = "";
        //private List<dynamic[]> details;

        public frm_supplier_encode(string slid, string header)
        {
            InitializeComponent();
            BtnClose.BackColor = PnlHeader.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderContacts.BackColor = PnlHeaderDetails.BackColor = PnlHeaderAddress.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            BtnClose.ForeColor = LblHeader.ForeColor = LblDetails.ForeColor = LblAddress.ForeColor = LblContacts.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
            //cmbCity.Items.Clear();

            this.supplierDetails = this.GetSupplier(slid);
            this.supplierDetails.ContactList = this.GetSupplierContact(slid);

            _cityDictionary = _supplierController.GetCity();
            cmbCity.DataSource = new BindingSource(_cityDictionary, null);
            cmbCity.DisplayMember = "Key";
            cmbCity.ValueMember = "Value";

            _termsDictionary = _supplierController.getTerms();
            cmbTerms.DataSource = new BindingSource(_termsDictionary, null);
            cmbTerms.DisplayMember = "Value";
            cmbTerms.ValueMember = "Key";

            isinitial = false;
            //cmbRegion.SelectedValue = supplierDetails.RegionID;
            //dtContacts.EditingControlShowing += dtContacts_EditingControlShowing;
            dtContacts.DataSource = ContactTable;

            TxtCustomer.Textt = slid;
            LblHeader.Text = header;
            //if(this.supplierDetails.SLType == "0") { radCust1.Checked = true; } else { radCust2.Checked = true; };
            if (this.supplierDetails.VATType == 0) { radVat1.Checked = true; }
            else if (this.supplierDetails.VATType == 1) { radVat2.Checked = true; }
            else if (this.supplierDetails.VATType == 2) { radVat3.Checked = true; } else { radVat4.Checked = true; }
            if (this.supplierDetails.SupplierType == "0") { radSup1.Checked = true; } else { radSup2.Checked = true; }
            txtRegisName.Textt = this.supplierDetails.RegName;
            txtTin.Textt = this.supplierDetails.TinNo;
            txtWeb.Textt = this.supplierDetails.Website;
            txtEmail.Textt = this.supplierDetails.EmailAdd;
            txtStreet.Textt = this.supplierDetails.NoStreet;
            txtName.Textt = this.supplierDetails.SLName;
            cmbTerms.SelectedValue = supplierDetails.TermID;
            txtBrgy.Textt = this.supplierDetails.Brgy;
            txtFlrNo.Textt = this.supplierDetails.FlrNo;
            txtBusinessName.Textt = this.supplierDetails.BusinessName;
            //cmbProvince.Text = _provinceDictionary.FirstOrDefault(r => r.Value == supplierDetails.ProvID.TrimEnd()).Key;
            cmbCity.SelectedValue = supplierDetails.CityID;
            //cmbProvince.SelectedValue = supplierDetails.ProvID;
            radSup1.Tag = "L"; radSup2.Tag = "I";
            radVat1.Tag = 0; radVat2.Tag = 1;
            radVat3.Tag = 2; radVat4.Tag = 3;
            dtContacts.AutoGenerateColumns = false;
            dtContacts.DataSource = this.supplierDetails.ContactList;

            List<dynamic[]> Contactus = new List<dynamic[]>();
            DataTable ContactusDt = _supplierController.dt(new SupplierModel { });
            foreach (DataRow datarow in ContactusDt.Rows)
            {
                Contactus.Add(new dynamic[] { 0, datarow[0].ToString().TrimEnd(), datarow[1], datarow[2], 0 });
            }
            if (Contactus.Count != 0)
            {
                for (int i = 0; Contactus.Count != dtContacts.Rows.Count; i++)
                {
                    dtContacts.Rows.Add(Contactus[i]);
                    dtContacts.Rows[i].Cells[0].ReadOnly = true;
                    dtContacts.Rows[i].Cells[1].ReadOnly = true;
                }
            }
        }

        private string getSupplierType()
        {
            string selectedSupplierType = null;

            if (radSup1.Checked)
            {
                selectedSupplierType = radSup1.Tag.ToString();
            }
            else
            {
                selectedSupplierType = radSup2.Tag.ToString();
            }
            //MessageBox.Show("Selected Supplier Value "+ selectedSupplierType);
            return selectedSupplierType;

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
                selectedVatType = decimal.Parse(radVat2.Tag.ToString());
            }
            else if (radVat3.Checked)
            {
                selectedVatType = decimal.Parse(radVat3.Tag.ToString());
            }
            else if (radVat4.Checked)
            {
                selectedVatType = decimal.Parse(radVat4.Tag.ToString());
            }
            //MessageBox.Show("Selected VAT Value " + selectedVatType);
            return selectedVatType;
        }

        private List<SupplierContactModel> GetSupplierContact(string slid) => _supplierController.getContacts(slid);
        private SupplierModel GetSupplier(string code) => _supplierController.Read(code);

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close Customer encode?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Close();
            }
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
                txtTin.Textt = "";
                txtBrgy.Textt = "";
                txtFlrNo.Textt = "";
                lblRegion.Text = "";
                cmbTerms.SelectedIndex = 0;
                cmbCity.SelectedIndex = 0;
                if (radVat1.Checked || radVat2.Checked || radVat3.Checked || radVat4.Checked)
                {
                    radVat2.Checked = false; radVat3.Checked = false; radVat4.Checked = false;
                    radVat1.Checked = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string notificationMessage = "";

            if (txtName.Textt == "" || txtRegisName.Textt == "" || txtTin.Textt == "" || cmbTerms.SelectedIndex <= 1 || txtWeb.Textt == ""
                || txtEmail.Textt == "" || txtStreet.Textt == "" || cmbCity.SelectedIndex <= 0 || txtBusinessName.Textt == "" || txtFlrNo.Textt == "" || txtBrgy.Textt == "")
            {
                if (Helper.Confirmator("Please complete all required fields", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Error))
                {
                    return;
                }
            }
            else
            {
                ValidateEmail(txtEmail.Textt);
                string selectedSupplierType = getSupplierType();
                decimal selectedVatType = getVatType();
                //string selectedCustomerType = getCustomerType();
                List<SupplierContactModel> supplierContactList = new List<SupplierContactModel>();
                if (Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    foreach (DataGridViewRow row in dtContacts.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            SupplierContactModel contactModel = new SupplierContactModel
                            {
                                SLID = TxtCustomer.Textt.ToString(),
                                UniqueID = getUniqueID(row.Index),
                                ContactPerson = row.Cells[2].Value.ToString(),
                                ContactNo = row.Cells[3].Value.ToString(),
                                EmailAdd = row.Cells[4].Value.ToString(),
                                Remarks = row.Cells[5].Value.ToString(),
                                IsActive = Convert.ToBoolean(row.Cells[6].Value),
                            };
                            supplierContactList.Add(contactModel);
                        }
                    }
                }
                _supplierModel = new SupplierModel
                {
                    SLID = TxtCustomer.Textt.ToString(),
                    RegName = txtRegisName.Textt,
                    SLName = txtName.Textt,
                    SLType = "S",
                    SupplierType = selectedSupplierType,
                    VATType = selectedVatType,
                    TinNo = txtTin.Textt,
                    NoStreet = txtStreet.Textt,
                    CityID = cmbCity.SelectedValue.ToString().TrimEnd(),
                    ProvID = lblProvince.Text,
                    RegionID = lblRegion.Text,
                    EmailAdd = txtEmail.Textt,
                    Website = txtWeb.Textt,
                    TermID = cmbTerms.SelectedValue.ToString().TrimEnd(),
                    BusinessName = txtBusinessName.Textt,
                    FlrNo = txtFlrNo.Textt,
                    Brgy = txtBrgy.Textt,

                    ContactList = supplierContactList
                };
                if (LblHeader.Text == "SUPPLIER ENTRY")
                {
                    notificationMessage = _supplierController.Create(_supplierModel);

                    Helper.Confirmator(notificationMessage, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (notificationMessage == "Information saved successfully")
                    {
                        this.Close();
                    }

                }
                else
                {
                    notificationMessage = _supplierController.Update(_supplierModel);
                    Helper.Confirmator(notificationMessage, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (notificationMessage == "Information saved successfully")
                    {
                        this.Close();
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

        private void customRoundedButton1_Click(object sender, EventArgs e)
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
                dt.Rows.Add(false, "", "", "", "", "", false, true);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
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

        //private void cmbRegion_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    if (isinitial)
        //    {
        //        return;
        //    } else
        //    {
        //        _provinceDictionary = _supplierController.getSelectedProvince(cmbRegion.SelectedValue.ToString());
        //        cmbProvince.DataSource = new BindingSource(_provinceDictionary, null);
        //        if (_provinceDictionary.Any())
        //        {
        //        cmbProvince.DisplayMember = "Key";
        //        cmbProvince.ValueMember = "Value";
        //            var ifprovexist = _provinceDictionary.FirstOrDefault(r => r.Value.Trim() == supplierDetails.ProvID.TrimEnd());
        //            if (ifprovexist.Value != null)
        //            {
        //                cmbProvince.SelectedValue = ifprovexist.Value;
        //            }
        //        }
        //        _cityDictionary.Clear();

        //    }
        //}

        //private void cmbProvince_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    if (isinitial)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //    _cityDictionary = _supplierController.getSelectedCity(cmbProvince.SelectedValue.ToString());
        //    cmbCity.DataSource = new BindingSource(_cityDictionary, null);
        //        if (_cityDictionary.Any())
        //        {
        //    cmbCity.DisplayMember = "Value";
        //    cmbCity.ValueMember = "Key";
        //            var ifcityexist = _cityDictionary.FirstOrDefault(c => c.Key.Trim() == supplierDetails.CityID.Trim());
        //            if(ifcityexist.Key != null)
        //            {
        //                cmbCity.SelectedValue = ifcityexist.Key;
        //            }
        //        }
        //    }
        //}

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

        private void frm_supplier_encode_Load(object sender, EventArgs e)
        {
            //cmbProvince.Text = _provinceDictionary.FirstOrDefault(r => r.Value == supplierDetails.ProvID.TrimEnd()).Key;
            //cmbCity.Text = _cityDictionary.FirstOrDefault(c => c.Value.Trim() == supplierDetails.CityID.Trim()).Key;
        }

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCity.SelectedItem is KeyValuePair<string, string> selectedCity &&
            !string.IsNullOrEmpty(selectedCity.Key))
            {
                Dictionary<string, string> keyvaluepairs = _supplierController.getSelectedProvice(selectedCity.Key);
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

        private void dtContacts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dtContacts.Columns["Email"].Index)
            {
                string email = dtContacts.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                ValidateEmail(email);
            }
        }
    }
}
