using CARS.Functions;
using CARS.Model;
using CARS.Model.Login;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Controller.Login
{
    internal class LoginController : Universal<LoginModel>
    {
        private static SqlConnection conn = Connection.GetConnection();
        private static SqlCommand cmd = null;
        private static SqlDataReader rd = null;
        private static SqlTransaction tr = null;
        private static MessageBox msgbx = null;
        private static LoginModel loginModel;

        public override string Create(LoginModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(LoginModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(LoginModel entity)
        {
            throw new NotImplementedException();
        }

        public LoginModel Login(string userId)
        {
            LoginModel loginModel = null;
            try
            {
                loginModel = new LoginModel();
                conn.Open();
                cmd = Connection.setCommand("SELECT * FROM " +
                                            "   TblUserProfileAccess WITH(READPAST) " +
                                            "   WHERE UserID = @UserID", conn);
                cmd.Parameters.AddWithValue("@UserID", userId);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    loginModel.UserID = rd.GetString(rd.GetOrdinal("UserID")).TrimEnd();
                    loginModel.UserPassword = rd.GetString(rd.GetOrdinal("UserPassword")).TrimEnd();
                    loginModel.UserType = rd.GetDecimal(rd.GetOrdinal("UserType"));
                    loginModel.MnuTransaction = rd.GetBoolean(rd.GetOrdinal("MnuTransaction"));
                    loginModel.OrderTaking = rd.GetBoolean(rd.GetOrdinal("OrderTaking"));
                    loginModel.OrderTakingCreate = rd.GetBoolean(rd.GetOrdinal("OrderTakingCreate"));
                    loginModel.OrderTakingPost = rd.GetBoolean(rd.GetOrdinal("OrderTakingPost"));
                    loginModel.OrderConsolidation = rd.GetBoolean(rd.GetOrdinal("OrderConsolidation"));
                    loginModel.OrderConsolidationCreate = rd.GetBoolean(rd.GetOrdinal("OrderConsolidationCreate"));
                    loginModel.OrderConsolidationToBSB = rd.GetBoolean(rd.GetOrdinal("OrderConsolidationToBSB"));
                    loginModel.PurchaseOrderMonitoring = rd.GetBoolean(rd.GetOrdinal("PurchaseOrderMonitoring"));
                    loginModel.PurchaseOrderMonitoringCreate = rd.GetBoolean(rd.GetOrdinal("PurchaseOrderMonitoringCreate"));
                    loginModel.PurchaseOrderMonitoringCancel = rd.GetBoolean(rd.GetOrdinal("PurchaseOrderMonitoringCancel"));
                    loginModel.PurchaseOrderMonitoringClose = rd.GetBoolean(rd.GetOrdinal("PurchaseOrderMonitoringClose"));
                    loginModel.PurchaseOrderMonitoringQuotation = rd.GetBoolean(rd.GetOrdinal("PurchaseOrderMonitoringQuotation"));
                    loginModel.Receiving = rd.GetBoolean(rd.GetOrdinal("Receiving"));
                    loginModel.ReceivingSave = rd.GetBoolean(rd.GetOrdinal("ReceivingSave"));
                    loginModel.ReceivingPost = rd.GetBoolean(rd.GetOrdinal("ReceivingPost"));
                    loginModel.ReceivingArchive = rd.GetBoolean(rd.GetOrdinal("ReceivingArchive"));
                    loginModel.PurchaseReturn = rd.GetBoolean(rd.GetOrdinal("PurchaseReturn"));
                    loginModel.PurchaseReturnSave = rd.GetBoolean(rd.GetOrdinal("PurchaseReturnSave"));
                    loginModel.PurchaseReturnArchive = rd.GetBoolean(rd.GetOrdinal("PurchaseReturnArchive"));
                    loginModel.PriceManagement = rd.GetBoolean(rd.GetOrdinal("PriceManagement"));
                    loginModel.PriceManagementModify = rd.GetBoolean(rd.GetOrdinal("PriceManagementModify"));
                    loginModel.PriceManagementArchive = rd.GetBoolean(rd.GetOrdinal("PriceManagementArchive"));
                    loginModel.PriceManagementPrint = rd.GetBoolean(rd.GetOrdinal("PriceManagementPrint"));
                    loginModel.SalesOrder = rd.GetBoolean(rd.GetOrdinal("SalesOrder"));
                    loginModel.SalesOrderCrud = rd.GetBoolean(rd.GetOrdinal("SalesOrderCrud"));
                    loginModel.InvoiceGeneration = rd.GetBoolean(rd.GetOrdinal("InvoiceGeneration"));
                    loginModel.InvoiceGenerationPayment = rd.GetBoolean(rd.GetOrdinal("InvoiceGenerationPayment"));
                    loginModel.SalesReturn = rd.GetBoolean(rd.GetOrdinal("SalesReturn"));
                    loginModel.SalesReturnCreate = rd.GetBoolean(rd.GetOrdinal("SalesReturnCreate"));
                    loginModel.SalesReturnArchive = rd.GetBoolean(rd.GetOrdinal("SalesReturnArchive"));
                    loginModel.BeginningBalance = rd.GetBoolean(rd.GetOrdinal("BeginningBalance"));
                    loginModel.BeginningBalanceCreate = rd.GetBoolean(rd.GetOrdinal("BeginningBalanceCreate"));
                    loginModel.BeginningBalanceArchive = rd.GetBoolean(rd.GetOrdinal("BeginningBalanceArchive"));
                    loginModel.StockTransfer = rd.GetBoolean(rd.GetOrdinal("StockTransfer"));
                    loginModel.StockTransferCreate = rd.GetBoolean(rd.GetOrdinal("StockTransferCreate"));
                    loginModel.StockTransferArchive = rd.GetBoolean(rd.GetOrdinal("StockTransferArchive"));
                    loginModel.StockAdjustment = rd.GetBoolean(rd.GetOrdinal("StockAdjustment"));
                    loginModel.StockAdjustmentCreate = rd.GetBoolean(rd.GetOrdinal("StockAdjustmentCreate"));
                    loginModel.StockAdjustmentArchive = rd.GetBoolean(rd.GetOrdinal("StockAdjustmentArchive"));
                    loginModel.MnuInquiry = rd.GetBoolean(rd.GetOrdinal("MnuInquiry"));
                    loginModel.PartsInquiry = rd.GetBoolean(rd.GetOrdinal("PartsInquiry"));
                    loginModel.InventoryManagement = rd.GetBoolean(rd.GetOrdinal("InventoryManagement"));
                    loginModel.AuditTrail = rd.GetBoolean(rd.GetOrdinal("AuditTrail"));
                    loginModel.SalesOrderArchive = rd.GetBoolean(rd.GetOrdinal("SalesOrderArchive"));
                    loginModel.MnuMasterfiles = rd.GetBoolean(rd.GetOrdinal("MnuMasterfiles"));
                    loginModel.PartsLibrary = rd.GetBoolean(rd.GetOrdinal("PartsLibrary"));
                    loginModel.PartsLibraryCreate = rd.GetBoolean(rd.GetOrdinal("PartsLibraryCreate"));
                    loginModel.PartsLibraryUpdate = rd.GetBoolean(rd.GetOrdinal("PartsLibraryUpdate"));
                    loginModel.SupplierLibrary = rd.GetBoolean(rd.GetOrdinal("SupplierLibrary"));
                    loginModel.SupplierLibraryCreate = rd.GetBoolean(rd.GetOrdinal("SupplierLibraryCreate"));
                    loginModel.SupplierLibraryUpdate = rd.GetBoolean(rd.GetOrdinal("SupplierLibraryUpdate"));
                    loginModel.CustomerLibrary = rd.GetBoolean(rd.GetOrdinal("CustomerLibrary"));
                    loginModel.CustomerLibraryCreate = rd.GetBoolean(rd.GetOrdinal("CustomerLibraryCreate"));
                    loginModel.CustomerLibraryUpdate = rd.GetBoolean(rd.GetOrdinal("CustomerLibraryUpdate"));
                    loginModel.WarehouseMaster = rd.GetBoolean(rd.GetOrdinal("WarehouseMaster"));
                    loginModel.WarehouseMasterCreate = rd.GetBoolean(rd.GetOrdinal("WarehouseMasterCreate"));
                    loginModel.WarehouseMasterUpdate = rd.GetBoolean(rd.GetOrdinal("WarehouseMasterUpdate"));
                    loginModel.LocationMaster = rd.GetBoolean(rd.GetOrdinal("LocationMaster"));
                    loginModel.LocationMasterCreate = rd.GetBoolean(rd.GetOrdinal("LocationMasterCreate"));
                    loginModel.LocationMasterUpdate = rd.GetBoolean(rd.GetOrdinal("LocationMasterUpdate"));
                    loginModel.ReasonMaster = rd.GetBoolean(rd.GetOrdinal("ReasonMaster"));
                    loginModel.ReasonMasterCreate = rd.GetBoolean(rd.GetOrdinal("ReasonMasterCreate"));
                    loginModel.ReasonMasterUpdate = rd.GetBoolean(rd.GetOrdinal("ReasonMasterUpdate"));
                    loginModel.RegionMaster = rd.GetBoolean(rd.GetOrdinal("RegionMaster"));
                    loginModel.RegionMasterCreate = rd.GetBoolean(rd.GetOrdinal("RegionMasterCreate"));
                    loginModel.RegionMasterUpdate = rd.GetBoolean(rd.GetOrdinal("RegionMasterUpdate"));
                    loginModel.ProvinceMaster = rd.GetBoolean(rd.GetOrdinal("ProvinceMaster"));
                    loginModel.ProvinceMasterCreate = rd.GetBoolean(rd.GetOrdinal("ProvinceMasterCreate"));
                    loginModel.ProvinceMasterUpdate = rd.GetBoolean(rd.GetOrdinal("ProvinceMasterUpdate"));
                    loginModel.CityMaster = rd.GetBoolean(rd.GetOrdinal("CityMaster"));
                    loginModel.CityMasterCreate = rd.GetBoolean(rd.GetOrdinal("CityMasterCreate"));
                    loginModel.CityMasterUpdate = rd.GetBoolean(rd.GetOrdinal("CityMasterUpdate"));
                    loginModel.TermMaster = rd.GetBoolean(rd.GetOrdinal("TermMaster"));
                    loginModel.TermMasterCreate = rd.GetBoolean(rd.GetOrdinal("TermMasterCreate"));
                    loginModel.TermMasterUpdate = rd.GetBoolean(rd.GetOrdinal("TermMasterUpdate"));
                    loginModel.BrandMaster = rd.GetBoolean(rd.GetOrdinal("BrandMaster"));
                    loginModel.BrandMasterCreate = rd.GetBoolean(rd.GetOrdinal("BrandMasterCreate"));
                    loginModel.BrandMasterUpdate = rd.GetBoolean(rd.GetOrdinal("BrandMasterUpdate"));
                    loginModel.DescriptionMaster = rd.GetBoolean(rd.GetOrdinal("DescriptionMaster"));
                    loginModel.DescriptionMasterCreate = rd.GetBoolean(rd.GetOrdinal("DescriptionMasterCreate"));
                    loginModel.DescriptionMasterUpdate = rd.GetBoolean(rd.GetOrdinal("DescriptionMasterUpdate"));
                    loginModel.PartsUOMMaster = rd.GetBoolean(rd.GetOrdinal("PartsUOMMaster"));
                    loginModel.PartsUOMMasterCreate = rd.GetBoolean(rd.GetOrdinal("PartsUOMMasterCreate"));
                    loginModel.PartsUOMMasterUpdate = rd.GetBoolean(rd.GetOrdinal("PartsUOMMasterUpdate"));
                    loginModel.DepartmentMaster = rd.GetBoolean(rd.GetOrdinal("DepartmentMaster"));
                    loginModel.DepartmentMasterCreate = rd.GetBoolean(rd.GetOrdinal("DepartmentMasterCreate"));
                    loginModel.DepartmentMasterUpdate = rd.GetBoolean(rd.GetOrdinal("DepartmentMasterUpdate"));
                    loginModel.PositionMaster = rd.GetBoolean(rd.GetOrdinal("PositionMaster"));
                    loginModel.PositionMasterCreate = rd.GetBoolean(rd.GetOrdinal("PositionMasterCreate"));
                    loginModel.PositionMasterUpdate = rd.GetBoolean(rd.GetOrdinal("PositionMasterUpdate"));
                    loginModel.PartsOEMMaster = rd.GetBoolean(rd.GetOrdinal("PartsOEMMaster"));
                    loginModel.PartsOEMMasterCreate = rd.GetBoolean(rd.GetOrdinal("PartsOEMMasterCreate"));
                    loginModel.PartsOEMMasterUpdate = rd.GetBoolean(rd.GetOrdinal("PartsOEMMasterUpdate"));
                    loginModel.PartsVehicleMakeMaster = rd.GetBoolean(rd.GetOrdinal("PartsVehicleMakeMaster"));
                    loginModel.PartsVehicleMakeMasterCreate = rd.GetBoolean(rd.GetOrdinal("PartsVehicleMakeMasterCreate"));
                    loginModel.PartsVehicleMakeMasterUpdate = rd.GetBoolean(rd.GetOrdinal("PartsVehicleMakeMasterUpdate"));
                    loginModel.MnuReports = rd.GetBoolean(rd.GetOrdinal("MnuReports"));
                    loginModel.SalesReport = rd.GetBoolean(rd.GetOrdinal("SalesReport"));
                    loginModel.ReceivingReport = rd.GetBoolean(rd.GetOrdinal("ReceivingReport"));
                    loginModel.InventoryReports = rd.GetBoolean(rd.GetOrdinal("InventoryReports"));
                    loginModel.MonthlyInventoryReport = rd.GetBoolean(rd.GetOrdinal("MonthlyInventoryReport"));
                    loginModel.MnuUtilities = rd.GetBoolean(rd.GetOrdinal("MnuUtilities"));
                    loginModel.CompanyProfile = rd.GetBoolean(rd.GetOrdinal("CompanyProfile"));
                    loginModel.UserProfile = rd.GetBoolean(rd.GetOrdinal("UserProfile"));
                    loginModel.SyncProfile = rd.GetBoolean(rd.GetOrdinal("SyncProfile"));
                }
                Helper.TranLog("Login", loginModel.EmployeeID, conn, cmd,tr);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return loginModel;
        }

        public override void Read(LoginModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(LoginModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
