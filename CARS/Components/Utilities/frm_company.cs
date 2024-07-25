using CARS.Controller.Masterfiles;
using CARS.Controller.Utilities;
using CARS.Functions;
using CARS.Model;
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

namespace CARS.Components.Utilities
{
    public partial class frm_company : Form
    {
        private Action dashboardCall;
        private ColorManager _ColorManager = new ColorManager();
        private CompanyController companyControl = new CompanyController();
        private CompanyModel companyModel = new CompanyModel();
        private SortedDictionary<string, string> _provinceDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _regionDictionary = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> _cityDictionary = new SortedDictionary<string, string>();
        public frm_company(Action DashboardCall)
        {
            InitializeComponent();
            dashboardCall = DashboardCall;
            LblHeader.ForeColor = PnlDesign.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            panel24.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            if(Helper.Confirmator("Are you sure you want to save this data?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                byte[] CompanyImage = Helper.ImageToByteArray(logoImage.Image);
                companyModel = new CompanyModel 
                { 
                    CompName = txtName.Textt.Trim(),
                    RegName = txtRegisName.Textt.Trim(),
                    TinNo = txtTin.Textt.Trim(),
                    NoStreet = txtStreet.Textt.Trim(),
                    EmailAdd = txtEmail.Textt.Trim(),
                    CityID = cmbCity.SelectedValue.ToString().TrimEnd(),
                    ProvID = cmbProvince.SelectedValue.ToString().TrimEnd(),
                    TelNo = txtTel.Textt.Trim(),
                    CellNo = txtContact.Textt.Trim(),
                    RegionID = cmbRegion.SelectedValue.ToString().TrimEnd(),
                    Web = txtWeb.Text.Trim(),
                    CompLogo = Convert.ToBase64String(CompanyImage),
                    VatType = selectedVatType(),
                };
                Helper.Confirmator(companyControl.Update(companyModel), "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frm_company_Load(object sender, EventArgs e)
        {
            companyModel = companyControl.companyMods();
            Console.WriteLine(companyModel);

            txtName.Textt = String.IsNullOrEmpty(companyModel.CompName) ? "" : companyModel.CompName.Trim();
            txtRegisName.Textt = String.IsNullOrEmpty(companyModel.RegName) ? "" : companyModel.RegName.Trim();
            txtEmail.Textt = String.IsNullOrEmpty(companyModel.EmailAdd)? companyModel.EmailAdd : companyModel.EmailAdd.Trim();
            txtStreet.Textt = companyModel.NoStreet == "" ? companyModel.NoStreet : companyModel.NoStreet.Trim();
            txtTel.Textt = companyModel.TelNo.ToString();
            txtTin.Textt = String.IsNullOrEmpty(companyModel.TinNo)? "" : companyModel.TinNo.Trim();
            txtWeb.Text = companyModel.Web == "" ? companyModel.Web : companyModel.Web.Trim();
            txtCompanyCode.Textt = CompanyController.cmpCode.ToString();
            _cityDictionary = companyControl.getCity();
            cmbCity.DataSource = new BindingSource(_cityDictionary, null);
            cmbCity.DisplayMember = "Value";
            cmbCity.ValueMember = "Key";
            _provinceDictionary = companyControl.GetProvince();
            cmbProvince.DataSource = new BindingSource(_provinceDictionary, null);
            cmbProvince.DisplayMember = "Value";
            cmbProvince.ValueMember = "Key";
            _regionDictionary = companyControl.GetRegion();
            cmbRegion.DataSource = new BindingSource(_regionDictionary, null);
            cmbRegion.DisplayMember = "Value";
            cmbRegion.ValueMember = "Key";
            cmbCity.SelectedValue = companyModel.CityID;
            cmbProvince.SelectedValue = companyModel.ProvID;
            cmbRegion.SelectedValue = companyModel.RegionID;

            if (companyModel.CompLogo != null)
            {
                byte[] CompanyImage = Convert.FromBase64String(companyModel.CompLogo.ToString());
                using (MemoryStream ms = new MemoryStream(CompanyImage))
                {
                    Image newImage = Image.FromStream(ms);
                    logoImage.Image = newImage;
                    ms.Dispose();
                }
            }

            if (companyModel.VatType == 1)
            {
                radVat1.Checked = true;
            }else if(companyModel.VatType ==2)
            {
                radVat2.Checked = true;
            }else if (companyModel.VatType == 3)
            {
                radVat3.Checked = true;
            }
            else
            {
                radVat0.Checked = true;
            }
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog uploadImage = new OpenFileDialog())
            {
                uploadImage.Title = "Select an Image";
                uploadImage.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif";

                if (uploadImage.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logoImage.Image = Image.FromFile(uploadImage.FileName);
                    }
                    catch(Exception ex) {
                        MessageBox.Show($"Error loading Image : {ex.Message}");
                    }
                }
            }
        }

        private decimal selectedVatType()
        {
            decimal selectedVat = 0;
            if (radVat0.Checked)
            {
                selectedVat = 0;
            }else if (radVat1.Checked)
            {
                selectedVat = 1;
            }else if (radVat2.Checked)
            {
                selectedVat = 2;
            }
            else
            {
                selectedVat = 3;
            }
            return selectedVat;
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
