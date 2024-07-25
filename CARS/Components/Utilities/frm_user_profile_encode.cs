using CARS.Controller.Utilities;
using CARS.Controllers.Masterfiles;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Components.Utilities
{
    public partial class frm_user_profile_encode : Form
    {
        private ColorManager _ColorManager = new ColorManager();
        private CustomerProfileModel _CustomerProfileModel = new CustomerProfileModel();
        private UserProfileController _UserProfileController = new UserProfileController();
        private string employeeID = "";

        public frm_user_profile_encode(string EmpID, string EmpName)
        {
            InitializeComponent();
            employeeID = EmpID;
            if (EmpID != "")
            {
                LblHeader.Text = "EDIT " + EmpName;
                GetEmpAccess(EmpID);
            }
            BtnClose.BackColor = PnlHeader.BackColor = Color.FromArgb(_ColorManager.BannerRGB[0], _ColorManager.BannerRGB[1], _ColorManager.BannerRGB[2]);
            PnlHeaderDetails.BackColor = Color.FromArgb(_ColorManager.TableHeaderRGB[0], _ColorManager.TableHeaderRGB[1], _ColorManager.TableHeaderRGB[2]);
            BtnClose.ForeColor = LblHeader.ForeColor = LblDetails.ForeColor = Color.FromArgb(_ColorManager.TableHeaderFontRGB[0], _ColorManager.TableHeaderFontRGB[1], _ColorManager.TableHeaderFontRGB[2]);
        }

        private CustomerProfileModel GetAccess(string id) => _UserProfileController.Read(id);

        private void GetEmpAccess(string id)
        {
            _CustomerProfileModel = GetAccess(id);
            TxtUserID.Textt = _CustomerProfileModel.UserID;
            TxtPassword.Textt = Helper.DecryptPasswordDesktopAppVersion(_CustomerProfileModel.UserPassword);
            switch (_CustomerProfileModel.UserType)
            {
                case 1:
                    RadioEmployee.Select();
                    break;

                case 2:
                    RadioAdmin.Select();
                    break;

                case 3:
                    RadioOwner.Select();
                    break;
            }
            CheckTransactions.Checked = _CustomerProfileModel.MnuTransaction;
            CheckOrderTaking.Checked = _CustomerProfileModel.OrderTaking;
            CheckOrderTakingCreate.Checked = _CustomerProfileModel.OrderTakingCreate;
            CheckOrderTakingPost.Checked = _CustomerProfileModel.OrderTakingPost;
            CheckOrderConsolidation.Checked = _CustomerProfileModel.OrderConsolidation;
            CheckOrderConsolidationCreate.Checked = _CustomerProfileModel.OrderConsolidationCreate;
            CheckOrderConsolidationBSB.Checked = _CustomerProfileModel.OrderConsolidationToBSB;
            CheckPurchaseOrder.Checked = _CustomerProfileModel.PurchaseOrderMonitoring;
            CheckPurchaseOrderCreate.Checked = _CustomerProfileModel.PurchaseOrderMonitoringCreate;
            CheckPurchaseOrderCancel.Checked = _CustomerProfileModel.PurchaseOrderMonitoringCancel;
            CheckPurchaseOrderClose.Checked = _CustomerProfileModel.PurchaseOrderMonitoringClose;
            CheckPurchaseOrderQuotation.Checked = _CustomerProfileModel.PurchaseOrderMonitoringQuotation;
            CheckReceiving.Checked = _CustomerProfileModel.Receiving;
            CheckReceivingSave.Checked = _CustomerProfileModel.ReceivingSave;
            CheckReceivingPost.Checked = _CustomerProfileModel.ReceivingPost;
            CheckReceivingArchive.Checked = _CustomerProfileModel.ReceivingArchive;
            CheckPurchaseReturn.Checked = _CustomerProfileModel.PurchaseReturn;
            CheckPurchaseReturnSave.Checked = _CustomerProfileModel.PurchaseReturnSave;
            CheckPurchaseReturnArchive.Checked = _CustomerProfileModel.PurchaseReturnArchive;
            CheckPriceManagement.Checked = _CustomerProfileModel.PriceManagement;
            CheckPriceManagementModify.Checked = _CustomerProfileModel.PriceManagementModify;
            CheckPriceManagementArchive.Checked = _CustomerProfileModel.PriceManagementArchive;
            CheckPriceManagementPrint.Checked = _CustomerProfileModel.PriceManagementPrint;
            CheckSalesOrder.Checked = _CustomerProfileModel.SalesOrder;
            CheckSalesOrderCrud.Checked = _CustomerProfileModel.SalesOrderCrud;
            CheckInvoiceGeneration.Checked = _CustomerProfileModel.InvoiceGeneration;
            CheckInvoiceGenerationGenerate.Checked = _CustomerProfileModel.InvoiceGenerationPayment;
            CheckSalesReturn.Checked = _CustomerProfileModel.SalesReturn;
            CheckSalesReturnCreate.Checked = _CustomerProfileModel.SalesReturnCreate;
            CheckSalesReturnArchive.Checked = _CustomerProfileModel.SalesReturnArchive;
            CheckBegBal.Checked = _CustomerProfileModel.BeginningBalance;
            CheckBegBalCreate.Checked = _CustomerProfileModel.BeginningBalanceCreate;
            CheckBegBalArchive.Checked = _CustomerProfileModel.BeginningBalanceArchive;
            CheckStockTransfer.Checked = _CustomerProfileModel.StockTransfer;
            CheckStockTransferCreate.Checked = _CustomerProfileModel.StockTransferCreate;
            CheckStockTransferArchive.Checked = _CustomerProfileModel.StockTransferArchive;
            CheckStockAdjustment.Checked = _CustomerProfileModel.StockAdjustment;
            CheckStockAdjustmentCreate.Checked = _CustomerProfileModel.StockAdjustmentCreate;
            CheckStockAdjustmentArchive.Checked = _CustomerProfileModel.StockAdjustmentArchive;
            CheckInquiry.Checked = _CustomerProfileModel.MnuInquiry;
            CheckPartsInquiry.Checked = _CustomerProfileModel.PartsInquiry;
            CheckInventoryManagement.Checked = _CustomerProfileModel.InventoryManagement;
            CheckAuditTrail.Checked = _CustomerProfileModel.AuditTrail;
            CheckSalesOrderArchive.Checked = _CustomerProfileModel.SalesOrderArchive;
            CheckMasterfiles.Checked = _CustomerProfileModel.MnuMasterfiles;
            CheckPartsLibrary.Checked = _CustomerProfileModel.PartsLibrary;
            CheckPartsLibraryCreate.Checked = _CustomerProfileModel.PartsLibraryCreate;
            CheckPartsLibraryUpdate.Checked = _CustomerProfileModel.PartsLibraryUpdate;
            CheckSupplierLibrary.Checked = _CustomerProfileModel.SupplierLibrary;
            CheckSupplierLibraryCreate.Checked = _CustomerProfileModel.SupplierLibraryCreate;
            CheckSupplierLibraryUpdate.Checked = _CustomerProfileModel.SupplierLibraryUpdate;
            CheckCustomerLibrary.Checked = _CustomerProfileModel.CustomerLibrary;
            CheckCustomerLibraryCreate.Checked = _CustomerProfileModel.CustomerLibraryCreate;
            CheckCustomerLibraryUpdate.Checked = _CustomerProfileModel.CustomerLibraryUpdate;
            CheckWarehouse.Checked = _CustomerProfileModel.WarehouseMaster;
            CheckWarehouseCreate.Checked = _CustomerProfileModel.WarehouseMasterCreate;
            CheckWarehouseUpdate.Checked = _CustomerProfileModel.WarehouseMasterUpdate;
            CheckBin.Checked = _CustomerProfileModel.LocationMaster;
            CheckBinCreate.Checked = _CustomerProfileModel.LocationMasterCreate;
            CheckBinUpdate.Checked = _CustomerProfileModel.LocationMasterUpdate;
            CheckReason.Checked = _CustomerProfileModel.ReasonMaster;
            CheckReasonCreate.Checked = _CustomerProfileModel.ReasonMasterCreate;
            CheckReasonUpdate.Checked = _CustomerProfileModel.ReasonMasterUpdate;
            CheckRegion.Checked = _CustomerProfileModel.RegionMaster;
            CheckRegionCreate.Checked = _CustomerProfileModel.RegionMasterCreate;
            CheckRegionUpdate.Checked = _CustomerProfileModel.RegionMasterUpdate;
            CheckProvince.Checked = _CustomerProfileModel.ProvinceMaster;
            CheckProvinceCreate.Checked = _CustomerProfileModel.ProvinceMasterCreate;
            CheckProvinceUpdate.Checked = _CustomerProfileModel.ProvinceMasterUpdate;
            CheckCity.Checked = _CustomerProfileModel.CityMaster;
            CheckCityCreate.Checked = _CustomerProfileModel.CityMasterCreate;
            CheckCityUpdate.Checked = _CustomerProfileModel.CityMasterUpdate;
            CheckTerm.Checked = _CustomerProfileModel.TermMaster;
            CheckTermCreate.Checked = _CustomerProfileModel.TermMasterCreate;
            CheckTermUpdate.Checked = _CustomerProfileModel.TermMasterUpdate;
            CheckBrand.Checked = _CustomerProfileModel.BrandMaster;
            CheckBrandCreate.Checked = _CustomerProfileModel.BrandMasterCreate;
            CheckBrandUpdate.Checked = _CustomerProfileModel.BrandMasterUpdate;
            CheckDescription.Checked = _CustomerProfileModel.DescriptionMaster;
            CheckDescriptionCreate.Checked = _CustomerProfileModel.DescriptionMasterCreate;
            CheckDescriptionUpdate.Checked = _CustomerProfileModel.DescriptionMasterUpdate;
            CheckUOM.Checked = _CustomerProfileModel.PartsUOMMaster;
            CheckUOMCreate.Checked = _CustomerProfileModel.PartsUOMMasterCreate;
            CheckUOMUpdate.Checked = _CustomerProfileModel.PartsUOMMasterUpdate;
            CheckDepartment.Checked = _CustomerProfileModel.DepartmentMaster;
            CheckDepartmentCreate.Checked = _CustomerProfileModel.DepartmentMasterCreate;
            CheckDepartmentUpdate.Checked = _CustomerProfileModel.DepartmentMasterUpdate;
            CheckPosition.Checked = _CustomerProfileModel.PositionMaster;
            CheckPositionCreate.Checked = _CustomerProfileModel.PositionMasterCreate;
            CheckPositionUpdate.Checked = _CustomerProfileModel.PositionMasterUpdate;
            CheckOEM.Checked = _CustomerProfileModel.PartsOEMMaster;
            CheckOEMCreate.Checked = _CustomerProfileModel.PartsOEMMasterCreate;
            CheckOEMUpdate.Checked = _CustomerProfileModel.PartsOEMMasterUpdate;
            CheckMake.Checked = _CustomerProfileModel.PartsVehicleMakeMaster;
            CheckMakeCreate.Checked = _CustomerProfileModel.PartsVehicleMakeMasterCreate;
            CheckMakeUpdate.Checked = _CustomerProfileModel.PartsVehicleMakeMasterUpdate;
            CheckReports.Checked = _CustomerProfileModel.MnuReports;
            CheckSalesReport.Checked = _CustomerProfileModel.SalesReport;
            CheckReceivingReport.Checked = _CustomerProfileModel.ReceivingReport;
            CheckInventoryReport.Checked = _CustomerProfileModel.InventoryReports;
            CheckMonthlyInventoryReport.Checked = _CustomerProfileModel.MonthlyInventoryReport;
            CheckUtilities.Checked = _CustomerProfileModel.MnuUtilities;
            CheckCompanyProfile.Checked = _CustomerProfileModel.CompanyProfile;
            CheckUserProfile.Checked = _CustomerProfileModel.UserProfile;
            CheckSync.Checked = _CustomerProfileModel.SyncProfile;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Helper.Confirmator("Are you sure you want to close Parts encode?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TxtUserID.Textt != "" && TxtPassword.Textt != "" && (RadioAdmin.Checked || RadioOwner.Checked || RadioEmployee.Checked))
            {
                if (Helper.Confirmator("Are you sure you want to save this profile?", "System Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    string CustomMsg = "";
                    int type = 1;
                    if (RadioOwner.Checked)
                    {
                        type = 3;
                    }
                    else if (RadioAdmin.Checked)
                    {
                        type = 2;
                    }

                    _CustomerProfileModel = new CustomerProfileModel
                    {
                        EmployeeID = employeeID,
                        UserID = TxtUserID.Textt,
                        UserPassword = Helper.EncryptPasswordDesktopAppVersion(TxtPassword.Textt),
                        UserType = type,
                        MnuTransaction = CheckTransactions.Checked,
                        OrderTaking = CheckOrderTaking.Checked,
                        OrderTakingCreate = CheckOrderTakingCreate.Checked,
                        OrderTakingPost = CheckOrderTakingPost.Checked,
                        OrderConsolidation = CheckOrderConsolidation.Checked,
                        OrderConsolidationCreate = CheckOrderConsolidationCreate.Checked,
                        OrderConsolidationToBSB = CheckOrderConsolidationBSB.Checked,
                        PurchaseOrderMonitoring = CheckPurchaseOrder.Checked,
                        PurchaseOrderMonitoringCreate = CheckPurchaseOrderCreate.Checked,
                        PurchaseOrderMonitoringCancel = CheckPurchaseOrderCancel.Checked,
                        PurchaseOrderMonitoringClose = CheckPurchaseOrderClose.Checked,
                        PurchaseOrderMonitoringQuotation = CheckPurchaseOrderQuotation.Checked,
                        Receiving = CheckReceiving.Checked,
                        ReceivingSave = CheckReceivingSave.Checked,
                        ReceivingPost = CheckReceivingPost.Checked,
                        ReceivingArchive = CheckReceivingArchive.Checked,
                        PurchaseReturn = CheckPurchaseReturn.Checked,
                        PurchaseReturnSave = CheckPurchaseReturnSave.Checked,
                        PurchaseReturnArchive = CheckPurchaseReturnArchive.Checked,
                        PriceManagement = CheckPriceManagement.Checked,
                        PriceManagementModify = CheckPriceManagementModify.Checked,
                        PriceManagementArchive = CheckPriceManagementArchive.Checked,
                        PriceManagementPrint = CheckPriceManagementPrint.Checked,
                        SalesOrder = CheckSalesOrder.Checked,
                        SalesOrderCrud = CheckSalesOrderCrud.Checked,
                        InvoiceGeneration = CheckInvoiceGeneration.Checked,
                        InvoiceGenerationPayment = CheckInvoiceGenerationGenerate.Checked,
                        SalesReturn = CheckSalesReturn.Checked,
                        SalesReturnCreate = CheckSalesReturnCreate.Checked,
                        SalesReturnArchive = CheckSalesReturnArchive.Checked,
                        BeginningBalance = CheckBegBal.Checked,
                        BeginningBalanceCreate = CheckBegBalCreate.Checked,
                        BeginningBalanceArchive = CheckBegBalArchive.Checked,
                        StockTransfer = CheckStockTransfer.Checked,
                        StockTransferCreate = CheckStockTransferCreate.Checked,
                        StockTransferArchive = CheckStockTransferArchive.Checked,
                        StockAdjustment = CheckStockAdjustment.Checked,
                        StockAdjustmentCreate = CheckStockAdjustmentCreate.Checked,
                        StockAdjustmentArchive = CheckStockAdjustmentArchive.Checked,
                        MnuInquiry = CheckInquiry.Checked,
                        PartsInquiry = CheckPartsInquiry.Checked,
                        InventoryManagement = CheckInventoryManagement.Checked,
                        AuditTrail = CheckAuditTrail.Checked,
                        SalesOrderArchive = CheckSalesOrderArchive.Checked,
                        MnuMasterfiles = CheckMasterfiles.Checked,
                        PartsLibrary = CheckPartsLibrary.Checked,
                        PartsLibraryCreate = CheckPartsLibraryCreate.Checked,
                        PartsLibraryUpdate = CheckPartsLibraryUpdate.Checked,
                        SupplierLibrary = CheckSupplierLibrary.Checked,
                        SupplierLibraryCreate = CheckSupplierLibraryCreate.Checked,
                        SupplierLibraryUpdate = CheckSupplierLibraryUpdate.Checked,
                        CustomerLibrary = CheckCustomerLibrary.Checked,
                        CustomerLibraryCreate = CheckCustomerLibraryCreate.Checked,
                        CustomerLibraryUpdate = CheckCustomerLibraryUpdate.Checked,
                        WarehouseMaster = CheckWarehouse.Checked,
                        WarehouseMasterCreate = CheckWarehouseCreate.Checked,
                        WarehouseMasterUpdate = CheckWarehouseUpdate.Checked,
                        LocationMaster = CheckBin.Checked,
                        LocationMasterCreate = CheckBinCreate.Checked,
                        LocationMasterUpdate = CheckBinUpdate.Checked,
                        ReasonMaster = CheckReason.Checked,
                        ReasonMasterCreate = CheckReasonCreate.Checked,
                        ReasonMasterUpdate = CheckReasonUpdate.Checked,
                        RegionMaster = CheckRegion.Checked,
                        RegionMasterCreate = CheckRegionCreate.Checked,
                        RegionMasterUpdate = CheckRegionUpdate.Checked,
                        ProvinceMaster = CheckProvince.Checked,
                        ProvinceMasterCreate = CheckProvinceCreate.Checked,
                        ProvinceMasterUpdate = CheckProvinceUpdate.Checked,
                        CityMaster = CheckCity.Checked,
                        CityMasterCreate = CheckCityCreate.Checked,
                        CityMasterUpdate = CheckCityUpdate.Checked,
                        TermMaster = CheckTerm.Checked,
                        TermMasterCreate = CheckTermCreate.Checked,
                        TermMasterUpdate = CheckTermUpdate.Checked,
                        BrandMaster = CheckBrand.Checked,
                        BrandMasterCreate = CheckBrandCreate.Checked,
                        BrandMasterUpdate = CheckBrandUpdate.Checked,
                        DescriptionMaster = CheckDescription.Checked,
                        DescriptionMasterCreate = CheckDescriptionCreate.Checked,
                        DescriptionMasterUpdate = CheckDescriptionUpdate.Checked,
                        PartsUOMMaster = CheckUOM.Checked,
                        PartsUOMMasterCreate = CheckUOMCreate.Checked,
                        PartsUOMMasterUpdate = CheckUOMUpdate.Checked,
                        DepartmentMaster = CheckDepartment.Checked,
                        DepartmentMasterCreate = CheckDepartmentCreate.Checked,
                        DepartmentMasterUpdate = CheckDepartmentUpdate.Checked,
                        PositionMaster = CheckPosition.Checked,
                        PositionMasterCreate = CheckPositionCreate.Checked,
                        PositionMasterUpdate = CheckPositionUpdate.Checked,
                        PartsOEMMaster = CheckOEM.Checked,
                        PartsOEMMasterCreate = CheckOEMCreate.Checked,
                        PartsOEMMasterUpdate = CheckOEMUpdate.Checked,
                        PartsVehicleMakeMaster = CheckMake.Checked,
                        PartsVehicleMakeMasterCreate = CheckMakeCreate.Checked,
                        PartsVehicleMakeMasterUpdate = CheckMakeUpdate.Checked,
                        MnuReports = CheckReports.Checked,
                        SalesReport = CheckSalesReport.Checked,
                        ReceivingReport = CheckReceivingReport.Checked,
                        InventoryReports = CheckInventoryReport.Checked,
                        MonthlyInventoryReport = CheckMonthlyInventoryReport.Checked,
                        MnuUtilities = CheckUtilities.Checked,
                        CompanyProfile = CheckCompanyProfile.Checked,
                        UserProfile = CheckUserProfile.Checked,
                        SyncProfile = CheckSync.Checked,
                    };
                    if (employeeID != "")
                    {
                        CustomMsg = _UserProfileController.Update(_CustomerProfileModel);
                        Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //else
                    //{
                    //    CustomMsg = _UserProfileController.Create(_CustomerProfileModel);
                    //    Helper.Confirmator(CustomMsg, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}

                    if (CustomMsg == "Information saved successfully" || CustomMsg == "Information updated successfully")
                    {
                        this.Close();
                    }
                }
            }
            else
            {
                Helper.Confirmator("Please fill all the required fields before saving.", "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            TxtUserID.Textt = "";
            TxtPassword.Textt = "";
            Clear();
        }

        private void Clear()
        {
            foreach (Control pnl in TableLayoutAcces.Controls)
            {
                if (pnl is Panel)
                {
                    foreach (Control checkBox in pnl.Controls)
                    {
                        if (checkBox is CheckBox)
                        {
                            ((CheckBox)checkBox).Checked = false;
                        }
                    }
                }
            }
        }

        private void frm_parts_encodeNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BtnClose.PerformClick();
            }
        }

        private void RadioType_CheckedChanged(object sender, EventArgs e)
        {
            Clear();
            if (RadioOwner.Checked)
            {
                foreach (Control pnl in TableLayoutAcces.Controls)
                {
                    if (pnl is Panel)
                    {
                        foreach (Control checkBox in pnl.Controls)
                        {
                            if (checkBox is CheckBox)
                            {
                                ((CheckBox)checkBox).Checked = true;
                            }
                        }
                    }
                }
            }
            else if (RadioAdmin.Checked)
            {
                foreach (Control checkBox in PnlTransactions.Controls)
                {
                    if (checkBox is CheckBox)
                    {
                        ((CheckBox)checkBox).Checked = true;
                    }
                }
                foreach (Control checkBox in PnlMasterfiles.Controls)
                {
                    if (checkBox is CheckBox)
                    {
                        ((CheckBox)checkBox).Checked = true;
                    }
                }
                //inquiry
                CheckInquiry.Checked = CheckPartsInquiry.Checked = CheckInventoryManagement.Checked = CheckAuditTrail.Checked = CheckAuditTrail.Checked = 
                    CheckSalesOrderArchive.Checked = true;
                //reports
                CheckReports.Checked = CheckSalesReport.Checked = CheckSalesReturnReport.Checked = CheckReceivingReport.Checked = CheckReceivingReport.Checked = 
                    CheckInventoryReport.Checked = CheckMonthlyInventoryReport.Checked = true;
            }
            else
            {
                foreach (Control checkBox in PnlTransactions.Controls)
                {
                    if (checkBox is CheckBox)
                    {
                        ((CheckBox)checkBox).Checked = true;
                    }
                }
                //inquiry
                CheckInquiry.Checked = CheckPartsInquiry.Checked = CheckInventoryManagement.Checked = CheckAuditTrail.Checked = CheckAuditTrail.Checked = CheckSalesOrderArchive.Checked = true;
                //reports
                CheckReports.Checked = CheckSalesReport.Checked = CheckSalesReturnReport.Checked = CheckReceivingReport.Checked = CheckReceivingReport.Checked = CheckInventoryReport.Checked =
                    CheckMonthlyInventoryReport.Checked = true;
            }
        }

        private void BtnShowPass_Click(object sender, EventArgs e)
        {
            bool shown = TxtPassword.PasswordChar;
            TxtPassword.PasswordChar = !shown;
        }
    }
}
