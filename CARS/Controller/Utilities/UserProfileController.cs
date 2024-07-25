using CARS.Functions;
using CARS.Model;
using CARS.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Controller.Utilities
{
    internal class UserProfileController : Universal<CustomerProfileModel>
    {
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(CustomerProfileModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                //command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT EmployeeID FROM TblUserProfileAccess WITH(READPAST) WHERE EmployeeID=@EmployeeID OR UserID=@UserID) " +
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT EmployeeID FROM TblUserProfileAccess WITH(READPAST) WHERE EmployeeID=@EmployeeID) " +
                                                           $"BEGIN " +
                                                           $"  INSERT INTO TblUserProfileAccess(EmployeeID, UserID, UserPassword, UserType, MnuTransaction, OrderTaking, " +
                                                           $"       OrderTakingCreate, OrderTakingPost, OrderConsolidation, OrderConsolidationCreate, " +
                                                           $"       OrderConsolidationToBSB, PurchaseOrderMonitoring, PurchaseOrderMonitoringCreate, " +
                                                           $"       PurchaseOrderMonitoringCancel, PurchaseOrderMonitoringClose, PurchaseOrderMonitoringQuotation, " +
                                                           $"       Receiving, ReceivingSave, ReceivingPost, ReceivingArchive, PurchaseReturn, PurchaseReturnSave, " +
                                                           $"       PurchaseReturnArchive, PriceManagement, PriceManagementModify, PriceManagementArchive, " +
                                                           $"       PriceManagementPrint, SalesOrder, SalesOrderCrud, InvoiceGeneration, InvoiceGenerationPayment, " +
                                                           $"       SalesReturn, SalesReturnCreate, SalesReturnArchive, BeginningBalance, BeginningBalanceCreate, " +
                                                           $"       BeginningBalanceArchive, StockTransfer, StockTransferCreate, StockTransferArchive, StockAdjustment, " +
                                                           $"       StockAdjustmentCreate, StockAdjustmentArchive, MnuInquiry, PartsInquiry, InventoryManagement, " +
                                                           $"       AuditTrail, SalesOrderArchive, MnuMasterfiles, PartsLibrary, PartsLibraryCreate, PartsLibraryUpdate, " +
                                                           $"       SupplierLibrary, SupplierLibraryCreate, SupplierLibraryUpdate, CustomerLibrary, CustomerLibraryCreate, " +
                                                           $"       CustomerLibraryUpdate, WarehouseMaster, WarehouseMasterCreate, WarehouseMasterUpdate, LocationMaster, " +
                                                           $"       LocationMasterCreate, LocationMasterUpdate, ReasonMaster, ReasonMasterCreate, ReasonMasterUpdate, " +
                                                           $"       RegionMaster, RegionMasterCreate, RegionMasterUpdate, ProvinceMaster, ProvinceMasterCreate, " +
                                                           $"       ProvinceMasterUpdate, CityMaster, CityMasterCreate, CityMasterUpdate, TermMaster, TermMasterCreate, " +
                                                           $"       TermMasterUpdate, BrandMaster, BrandMasterCreate, BrandMasterUpdate, DescriptionMaster, DescriptionMasterCreate, " +
                                                           $"       DescriptionMasterUpdate, PartsUOMMaster, PartsUOMMasterCreate, PartsUOMMasterUpdate, DepartmentMaster, " +
                                                           $"       DepartmentMasterCreate, DepartmentMasterUpdate, PositionMaster, PositionMasterCreate, PositionMasterUpdate, " +
                                                           $"       PartsOEMMaster, PartsOEMMasterCreate, PartsOEMMasterUpdate, PartsVehicleMakeMaster, PartsVehicleMakeMasterCreate, " +
                                                           $"       PartsVehicleMakeMasterUpdate, MnuReports, SalesReport, ReceivingReport, InventoryReports, MonthlyInventoryReport, " +
                                                           $"       MnuUtilities, CompanyProfile, UserProfile, SyncProfile, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                           $"   VALUES(@EmployeeID, @UserID, @UserPassword, @UserType, @MnuTransaction, @OrderTaking, " +
                                                           $"       @OrderTakingCreate, @OrderTakingPost, @OrderConsolidation, @OrderConsolidationCreate, " +
                                                           $"       @OrderConsolidationToBSB, @PurchaseOrderMonitoring, @PurchaseOrderMonitoringCreate, " +
                                                           $"       @PurchaseOrderMonitoringCancel, @PurchaseOrderMonitoringClose, @PurchaseOrderMonitoringQuotation, " +
                                                           $"       @Receiving, @ReceivingSave, @ReceivingPost, @ReceivingArchive, @PurchaseReturn, @PurchaseReturnSave, " +
                                                           $"       @PurchaseReturnArchive, @PriceManagement, @PriceManagementModify, @PriceManagementArchive, " +
                                                           $"       @PriceManagementPrint, @SalesOrder, @SalesOrderCrud, @InvoiceGeneration, @InvoiceGenerationPayment, " +
                                                           $"       @SalesReturn, @SalesReturnCreate, @SalesReturnArchive, @BeginningBalance, @BeginningBalanceCreate, " +
                                                           $"       @BeginningBalanceArchive, @StockTransfer, @StockTransferCreate, @StockTransferArchive, @StockAdjustment, " +
                                                           $"       @StockAdjustmentCreate, @StockAdjustmentArchive, @MnuInquiry, @PartsInquiry, @InventoryManagement, " +
                                                           $"       @AuditTrail, @SalesOrderArchive, @MnuMasterfiles, @PartsLibrary, @PartsLibraryCreate, @PartsLibraryUpdate, " +
                                                           $"       @SupplierLibrary, @SupplierLibraryCreate, @SupplierLibraryUpdate, @CustomerLibrary, @CustomerLibraryCreate, " +
                                                           $"       @CustomerLibraryUpdate, @WarehouseMaster, @WarehouseMasterCreate, @WarehouseMasterUpdate, @LocationMaster, " +
                                                           $"       @LocationMasterCreate, @LocationMasterUpdate, @ReasonMaster, @ReasonMasterCreate, @ReasonMasterUpdate, " +
                                                           $"       @RegionMaster, @RegionMasterCreate, @RegionMasterUpdate, @ProvinceMaster, @ProvinceMasterCreate, " +
                                                           $"       @ProvinceMasterUpdate, @CityMaster, @CityMasterCreate, @CityMasterUpdate, @TermMaster, @TermMasterCreate, " +
                                                           $"       @TermMasterUpdate, @BrandMaster, @BrandMasterCreate, @BrandMasterUpdate, @DescriptionMaster, @DescriptionMasterCreate, " +
                                                           $"       @DescriptionMasterUpdate, @PartsUOMMaster, @PartsUOMMasterCreate, @PartsUOMMasterUpdate, @DepartmentMaster, " +
                                                           $"       @DepartmentMasterCreate, @DepartmentMasterUpdate, @PositionMaster, @PositionMasterCreate, @PositionMasterUpdate, " +
                                                           $"       @PartsOEMMaster, @PartsOEMMasterCreate, @PartsOEMMasterUpdate, @PartsVehicleMakeMaster, @PartsVehicleMakeMasterCreate, " +
                                                           $"       @PartsVehicleMakeMasterUpdate, @MnuReports, @SalesReport, @ReceivingReport, @InventoryReports, @MonthlyInventoryReport, " +
                                                           $"       @MnuUtilities, @CompanyProfile, @UserProfile, @SyncProfile, @CreatedBy, GETDATE(), @CreatedBy, GETDATE())" +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@EmployeeID", entity.EmployeeID);
                command.Parameters.AddWithValue("@UserID", entity.UserID);
                command.Parameters.AddWithValue("@UserPassword", entity.UserPassword);
                command.Parameters.AddWithValue("@UserType", entity.UserType);
                command.Parameters.AddWithValue("@MnuTransaction", entity.MnuTransaction);
                command.Parameters.AddWithValue("@OrderTaking", entity.OrderTaking);
                command.Parameters.AddWithValue("@OrderTakingCreate", entity.OrderTakingCreate);
                command.Parameters.AddWithValue("@OrderTakingPost", entity.OrderTakingPost);
                command.Parameters.AddWithValue("@OrderConsolidation", entity.OrderConsolidation);
                command.Parameters.AddWithValue("@OrderConsolidationCreate", entity.OrderConsolidationCreate);
                command.Parameters.AddWithValue("@OrderConsolidationToBSB", entity.OrderConsolidationToBSB);
                command.Parameters.AddWithValue("@PurchaseOrderMonitoring", entity.PurchaseOrderMonitoring);
                command.Parameters.AddWithValue("@PurchaseOrderMonitoringCreate", entity.PurchaseOrderMonitoringCreate);
                command.Parameters.AddWithValue("@PurchaseOrderMonitoringCancel", entity.PurchaseOrderMonitoringCancel);
                command.Parameters.AddWithValue("@PurchaseOrderMonitoringClose", entity.PurchaseOrderMonitoringClose);
                command.Parameters.AddWithValue("@PurchaseOrderMonitoringQuotation", entity.PurchaseOrderMonitoringQuotation);
                command.Parameters.AddWithValue("@Receiving", entity.Receiving);
                command.Parameters.AddWithValue("@ReceivingSave", entity.ReceivingSave);
                command.Parameters.AddWithValue("@ReceivingPost", entity.ReceivingPost);
                command.Parameters.AddWithValue("@ReceivingArchive", entity.ReceivingArchive);
                command.Parameters.AddWithValue("@PurchaseReturn", entity.PurchaseReturn);
                command.Parameters.AddWithValue("@PurchaseReturnSave", entity.PurchaseReturnSave);
                command.Parameters.AddWithValue("@PurchaseReturnArchive", entity.PurchaseReturnArchive);
                command.Parameters.AddWithValue("@PriceManagement", entity.PriceManagement);
                command.Parameters.AddWithValue("@PriceManagementModify", entity.PriceManagementModify);
                command.Parameters.AddWithValue("@PriceManagementArchive", entity.PriceManagementArchive);
                command.Parameters.AddWithValue("@PriceManagementPrint", entity.PriceManagementPrint);
                command.Parameters.AddWithValue("@SalesOrder", entity.SalesOrder);
                command.Parameters.AddWithValue("@SalesOrderCrud", entity.SalesOrderCrud);
                command.Parameters.AddWithValue("@InvoiceGeneration", entity.InvoiceGeneration);
                command.Parameters.AddWithValue("@InvoiceGenerationPayment", entity.InvoiceGenerationPayment);
                command.Parameters.AddWithValue("@SalesReturn", entity.SalesReturn);
                command.Parameters.AddWithValue("@SalesReturnCreate", entity.SalesReturnCreate);
                command.Parameters.AddWithValue("@SalesReturnArchive", entity.SalesReturnArchive);
                command.Parameters.AddWithValue("@BeginningBalance", entity.BeginningBalance);
                command.Parameters.AddWithValue("@BeginningBalanceCreate", entity.BeginningBalanceCreate);
                command.Parameters.AddWithValue("@BeginningBalanceArchive", entity.BeginningBalanceArchive);
                command.Parameters.AddWithValue("@StockTransfer", entity.StockTransfer);
                command.Parameters.AddWithValue("@StockTransferCreate", entity.StockTransferCreate);
                command.Parameters.AddWithValue("@StockTransferArchive", entity.StockTransferArchive);
                command.Parameters.AddWithValue("@StockAdjustment", entity.StockAdjustment);
                command.Parameters.AddWithValue("@StockAdjustmentCreate", entity.StockAdjustmentCreate);
                command.Parameters.AddWithValue("@StockAdjustmentArchive", entity.StockAdjustmentArchive);
                command.Parameters.AddWithValue("@MnuInquiry", entity.MnuInquiry);
                command.Parameters.AddWithValue("@PartsInquiry", entity.PartsInquiry);
                command.Parameters.AddWithValue("@InventoryManagement", entity.InventoryManagement);
                command.Parameters.AddWithValue("@AuditTrail", entity.AuditTrail);
                command.Parameters.AddWithValue("@SalesOrderArchive", entity.SalesOrderArchive);
                command.Parameters.AddWithValue("@MnuMasterfiles", entity.MnuMasterfiles);
                command.Parameters.AddWithValue("@PartsLibrary", entity.PartsLibrary);
                command.Parameters.AddWithValue("@PartsLibraryCreate", entity.PartsLibraryCreate);
                command.Parameters.AddWithValue("@PartsLibraryUpdate", entity.PartsLibraryUpdate);
                command.Parameters.AddWithValue("@SupplierLibrary", entity.SupplierLibrary);
                command.Parameters.AddWithValue("@SupplierLibraryCreate", entity.SupplierLibraryCreate);
                command.Parameters.AddWithValue("@SupplierLibraryUpdate", entity.SupplierLibraryUpdate);
                command.Parameters.AddWithValue("@CustomerLibrary", entity.CustomerLibrary);
                command.Parameters.AddWithValue("@CustomerLibraryCreate", entity.CustomerLibraryCreate);
                command.Parameters.AddWithValue("@CustomerLibraryUpdate", entity.CustomerLibraryUpdate);
                command.Parameters.AddWithValue("@WarehouseMaster", entity.WarehouseMaster);
                command.Parameters.AddWithValue("@WarehouseMasterCreate", entity.WarehouseMasterCreate);
                command.Parameters.AddWithValue("@WarehouseMasterUpdate", entity.WarehouseMasterUpdate);
                command.Parameters.AddWithValue("@LocationMaster", entity.LocationMaster);
                command.Parameters.AddWithValue("@LocationMasterCreate", entity.LocationMasterCreate);
                command.Parameters.AddWithValue("@LocationMasterUpdate", entity.LocationMasterUpdate);
                command.Parameters.AddWithValue("@ReasonMaster", entity.ReasonMaster);
                command.Parameters.AddWithValue("@ReasonMasterCreate", entity.ReasonMasterCreate);
                command.Parameters.AddWithValue("@ReasonMasterUpdate", entity.ReasonMasterUpdate);
                command.Parameters.AddWithValue("@RegionMaster", entity.RegionMaster);
                command.Parameters.AddWithValue("@RegionMasterCreate", entity.RegionMasterCreate);
                command.Parameters.AddWithValue("@RegionMasterUpdate", entity.RegionMasterUpdate);
                command.Parameters.AddWithValue("@ProvinceMaster", entity.ProvinceMaster);
                command.Parameters.AddWithValue("@ProvinceMasterCreate", entity.ProvinceMasterCreate);
                command.Parameters.AddWithValue("@ProvinceMasterUpdate", entity.ProvinceMasterUpdate);
                command.Parameters.AddWithValue("@CityMaster", entity.CityMaster);
                command.Parameters.AddWithValue("@CityMasterCreate", entity.CityMasterCreate);
                command.Parameters.AddWithValue("@CityMasterUpdate", entity.CityMasterUpdate);
                command.Parameters.AddWithValue("@TermMaster", entity.TermMaster);
                command.Parameters.AddWithValue("@TermMasterCreate", entity.TermMasterCreate);
                command.Parameters.AddWithValue("@TermMasterUpdate", entity.TermMasterUpdate);
                command.Parameters.AddWithValue("@BrandMaster", entity.BrandMaster);
                command.Parameters.AddWithValue("@BrandMasterCreate", entity.BrandMasterCreate);
                command.Parameters.AddWithValue("@BrandMasterUpdate", entity.BrandMasterUpdate);
                command.Parameters.AddWithValue("@DescriptionMaster", entity.DescriptionMaster);
                command.Parameters.AddWithValue("@DescriptionMasterCreate", entity.DescriptionMasterCreate);
                command.Parameters.AddWithValue("@DescriptionMasterUpdate", entity.DescriptionMasterUpdate);
                command.Parameters.AddWithValue("@PartsUOMMaster", entity.PartsUOMMaster);
                command.Parameters.AddWithValue("@PartsUOMMasterCreate", entity.PartsUOMMasterCreate);
                command.Parameters.AddWithValue("@PartsUOMMasterUpdate", entity.PartsUOMMasterUpdate);
                command.Parameters.AddWithValue("@DepartmentMaster", entity.DepartmentMaster);
                command.Parameters.AddWithValue("@DepartmentMasterCreate", entity.DepartmentMasterCreate);
                command.Parameters.AddWithValue("@DepartmentMasterUpdate", entity.DepartmentMasterUpdate);
                command.Parameters.AddWithValue("@PositionMaster", entity.PositionMaster);
                command.Parameters.AddWithValue("@PositionMasterCreate", entity.PositionMasterCreate);
                command.Parameters.AddWithValue("@PositionMasterUpdate", entity.PositionMasterUpdate);
                command.Parameters.AddWithValue("@PartsOEMMaster", entity.PartsOEMMaster);
                command.Parameters.AddWithValue("@PartsOEMMasterCreate", entity.PartsOEMMasterCreate);
                command.Parameters.AddWithValue("@PartsOEMMasterUpdate", entity.PartsOEMMasterUpdate);
                command.Parameters.AddWithValue("@PartsVehicleMakeMaster", entity.PartsVehicleMakeMaster);
                command.Parameters.AddWithValue("@PartsVehicleMakeMasterCreate", entity.PartsVehicleMakeMasterCreate);
                command.Parameters.AddWithValue("@PartsVehicleMakeMasterUpdate", entity.PartsVehicleMakeMasterUpdate);
                command.Parameters.AddWithValue("@MnuReports", entity.MnuReports);
                command.Parameters.AddWithValue("@SalesReport", entity.SalesReport);
                command.Parameters.AddWithValue("@ReceivingReport", entity.ReceivingReport);
                command.Parameters.AddWithValue("@InventoryReports", entity.InventoryReports);
                command.Parameters.AddWithValue("@MonthlyInventoryReport", entity.MonthlyInventoryReport);
                command.Parameters.AddWithValue("@MnuUtilities", entity.MnuUtilities);
                command.Parameters.AddWithValue("@CompanyProfile", entity.CompanyProfile);
                command.Parameters.AddWithValue("@UserProfile", entity.UserProfile);
                command.Parameters.AddWithValue("@SyncProfile", entity.SyncProfile);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                int i = command.ExecuteNonQuery();
                if (i != 1)
                {
                    message = "The information entered is already present in the database.";
                    transaction.Rollback();
                    transaction.Dispose();
                    connection.Close();
                    return message;
                }
                Helper.TranLog("User Profile", "Added a new profile:" + entity.EmployeeID, connection, command, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                message = ex.Message;
                transaction.Rollback();
                Console.WriteLine(ex.Message);
            }
            finally
            {
                transaction.Dispose();
                connection.Close();
            }
            return message;
        }

        public override void Delete(CustomerProfileModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(CustomerProfileModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(CustomerProfileModel entity)
        {
            throw new NotImplementedException();
        }

        public CustomerProfileModel Read(string id)
        {
            CustomerProfileModel profile = null;
            try
            {
                profile = new CustomerProfileModel();
                connection.Open();
                command = Connection.setCommand($"SELECT * " +
                                                $"  FROM TblUserProfileAccess WITH(READPAST) " +
                                                $"  WHERE EmployeeID = @EmployeeID", connection);
                command.Parameters.AddWithValue("@EmployeeID", id);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    profile.UserID = reader.GetString(reader.GetOrdinal("UserID")).TrimEnd();
                    profile.UserPassword = reader.GetString(reader.GetOrdinal("UserPassword")).TrimEnd();
                    profile.UserType = reader.GetDecimal(reader.GetOrdinal("UserType"));
                    profile.MnuTransaction = reader.GetBoolean(reader.GetOrdinal("MnuTransaction"));
                    profile.OrderTaking = reader.GetBoolean(reader.GetOrdinal("OrderTaking"));
                    profile.OrderTakingCreate = reader.GetBoolean(reader.GetOrdinal("OrderTakingCreate"));
                    profile.OrderTakingPost = reader.GetBoolean(reader.GetOrdinal("OrderTakingPost"));
                    profile.OrderConsolidation = reader.GetBoolean(reader.GetOrdinal("OrderConsolidation"));
                    profile.OrderConsolidationCreate = reader.GetBoolean(reader.GetOrdinal("OrderConsolidationCreate"));
                    profile.OrderConsolidationToBSB = reader.GetBoolean(reader.GetOrdinal("OrderConsolidationToBSB"));
                    profile.PurchaseOrderMonitoring = reader.GetBoolean(reader.GetOrdinal("PurchaseOrderMonitoring"));
                    profile.PurchaseOrderMonitoringCreate = reader.GetBoolean(reader.GetOrdinal("PurchaseOrderMonitoringCreate"));
                    profile.PurchaseOrderMonitoringCancel = reader.GetBoolean(reader.GetOrdinal("PurchaseOrderMonitoringCancel"));
                    profile.PurchaseOrderMonitoringClose = reader.GetBoolean(reader.GetOrdinal("PurchaseOrderMonitoringClose"));
                    profile.PurchaseOrderMonitoringQuotation = reader.GetBoolean(reader.GetOrdinal("PurchaseOrderMonitoringQuotation"));
                    profile.Receiving = reader.GetBoolean(reader.GetOrdinal("Receiving"));
                    profile.ReceivingSave = reader.GetBoolean(reader.GetOrdinal("ReceivingSave"));
                    profile.ReceivingPost = reader.GetBoolean(reader.GetOrdinal("ReceivingPost"));
                    profile.ReceivingArchive = reader.GetBoolean(reader.GetOrdinal("ReceivingArchive"));
                    profile.PurchaseReturn = reader.GetBoolean(reader.GetOrdinal("PurchaseReturn"));
                    profile.PurchaseReturnSave = reader.GetBoolean(reader.GetOrdinal("PurchaseReturnSave"));
                    profile.PurchaseReturnArchive = reader.GetBoolean(reader.GetOrdinal("PurchaseReturnArchive"));
                    profile.PriceManagement = reader.GetBoolean(reader.GetOrdinal("PriceManagement"));
                    profile.PriceManagementModify = reader.GetBoolean(reader.GetOrdinal("PriceManagementModify"));
                    profile.PriceManagementArchive = reader.GetBoolean(reader.GetOrdinal("PriceManagementArchive"));
                    profile.PriceManagementPrint = reader.GetBoolean(reader.GetOrdinal("PriceManagementPrint"));
                    profile.SalesOrder = reader.GetBoolean(reader.GetOrdinal("SalesOrder"));
                    profile.SalesOrderCrud = reader.GetBoolean(reader.GetOrdinal("SalesOrderCrud"));
                    profile.InvoiceGeneration = reader.GetBoolean(reader.GetOrdinal("InvoiceGeneration"));
                    profile.InvoiceGenerationPayment = reader.GetBoolean(reader.GetOrdinal("InvoiceGenerationPayment"));
                    profile.SalesReturn = reader.GetBoolean(reader.GetOrdinal("SalesReturn"));
                    profile.SalesReturnCreate = reader.GetBoolean(reader.GetOrdinal("SalesReturnCreate"));
                    profile.SalesReturnArchive = reader.GetBoolean(reader.GetOrdinal("SalesReturnArchive"));
                    profile.BeginningBalance = reader.GetBoolean(reader.GetOrdinal("BeginningBalance"));
                    profile.BeginningBalanceCreate = reader.GetBoolean(reader.GetOrdinal("BeginningBalanceCreate"));
                    profile.BeginningBalanceArchive = reader.GetBoolean(reader.GetOrdinal("BeginningBalanceArchive"));
                    profile.StockTransfer = reader.GetBoolean(reader.GetOrdinal("StockTransfer"));
                    profile.StockTransferCreate = reader.GetBoolean(reader.GetOrdinal("StockTransferCreate"));
                    profile.StockTransferArchive = reader.GetBoolean(reader.GetOrdinal("StockTransferArchive"));
                    profile.StockAdjustment = reader.GetBoolean(reader.GetOrdinal("StockAdjustment"));
                    profile.StockAdjustmentCreate = reader.GetBoolean(reader.GetOrdinal("StockAdjustmentCreate"));
                    profile.StockAdjustmentArchive = reader.GetBoolean(reader.GetOrdinal("StockAdjustmentArchive"));
                    profile.MnuInquiry = reader.GetBoolean(reader.GetOrdinal("MnuInquiry"));
                    profile.PartsInquiry = reader.GetBoolean(reader.GetOrdinal("PartsInquiry"));
                    profile.InventoryManagement = reader.GetBoolean(reader.GetOrdinal("InventoryManagement"));
                    profile.AuditTrail = reader.GetBoolean(reader.GetOrdinal("AuditTrail"));
                    profile.SalesOrderArchive = reader.GetBoolean(reader.GetOrdinal("SalesOrderArchive"));
                    profile.MnuMasterfiles = reader.GetBoolean(reader.GetOrdinal("MnuMasterfiles"));
                    profile.PartsLibrary = reader.GetBoolean(reader.GetOrdinal("PartsLibrary"));
                    profile.PartsLibraryCreate = reader.GetBoolean(reader.GetOrdinal("PartsLibraryCreate"));
                    profile.PartsLibraryUpdate = reader.GetBoolean(reader.GetOrdinal("PartsLibraryUpdate"));
                    profile.SupplierLibrary = reader.GetBoolean(reader.GetOrdinal("SupplierLibrary"));
                    profile.SupplierLibraryCreate = reader.GetBoolean(reader.GetOrdinal("SupplierLibraryCreate"));
                    profile.SupplierLibraryUpdate = reader.GetBoolean(reader.GetOrdinal("SupplierLibraryUpdate"));
                    profile.CustomerLibrary = reader.GetBoolean(reader.GetOrdinal("CustomerLibrary"));
                    profile.CustomerLibraryCreate = reader.GetBoolean(reader.GetOrdinal("CustomerLibraryCreate"));
                    profile.CustomerLibraryUpdate = reader.GetBoolean(reader.GetOrdinal("CustomerLibraryUpdate"));
                    profile.WarehouseMaster = reader.GetBoolean(reader.GetOrdinal("WarehouseMaster"));
                    profile.WarehouseMasterCreate = reader.GetBoolean(reader.GetOrdinal("WarehouseMasterCreate"));
                    profile.WarehouseMasterUpdate = reader.GetBoolean(reader.GetOrdinal("WarehouseMasterUpdate"));
                    profile.LocationMaster = reader.GetBoolean(reader.GetOrdinal("LocationMaster"));
                    profile.LocationMasterCreate = reader.GetBoolean(reader.GetOrdinal("LocationMasterCreate"));
                    profile.LocationMasterUpdate = reader.GetBoolean(reader.GetOrdinal("LocationMasterUpdate"));
                    profile.ReasonMaster = reader.GetBoolean(reader.GetOrdinal("ReasonMaster"));
                    profile.ReasonMasterCreate = reader.GetBoolean(reader.GetOrdinal("ReasonMasterCreate"));
                    profile.ReasonMasterUpdate = reader.GetBoolean(reader.GetOrdinal("ReasonMasterUpdate"));
                    profile.RegionMaster = reader.GetBoolean(reader.GetOrdinal("RegionMaster"));
                    profile.RegionMasterCreate = reader.GetBoolean(reader.GetOrdinal("RegionMasterCreate"));
                    profile.RegionMasterUpdate = reader.GetBoolean(reader.GetOrdinal("RegionMasterUpdate"));
                    profile.ProvinceMaster = reader.GetBoolean(reader.GetOrdinal("ProvinceMaster"));
                    profile.ProvinceMasterCreate = reader.GetBoolean(reader.GetOrdinal("ProvinceMasterCreate"));
                    profile.ProvinceMasterUpdate = reader.GetBoolean(reader.GetOrdinal("ProvinceMasterUpdate"));
                    profile.CityMaster = reader.GetBoolean(reader.GetOrdinal("CityMaster"));
                    profile.CityMasterCreate = reader.GetBoolean(reader.GetOrdinal("CityMasterCreate"));
                    profile.CityMasterUpdate = reader.GetBoolean(reader.GetOrdinal("CityMasterUpdate"));
                    profile.TermMaster = reader.GetBoolean(reader.GetOrdinal("TermMaster"));
                    profile.TermMasterCreate = reader.GetBoolean(reader.GetOrdinal("TermMasterCreate"));
                    profile.TermMasterUpdate = reader.GetBoolean(reader.GetOrdinal("TermMasterUpdate"));
                    profile.BrandMaster = reader.GetBoolean(reader.GetOrdinal("BrandMaster"));
                    profile.BrandMasterCreate = reader.GetBoolean(reader.GetOrdinal("BrandMasterCreate"));
                    profile.BrandMasterUpdate = reader.GetBoolean(reader.GetOrdinal("BrandMasterUpdate"));
                    profile.DescriptionMaster = reader.GetBoolean(reader.GetOrdinal("DescriptionMaster"));
                    profile.DescriptionMasterCreate = reader.GetBoolean(reader.GetOrdinal("DescriptionMasterCreate"));
                    profile.DescriptionMasterUpdate = reader.GetBoolean(reader.GetOrdinal("DescriptionMasterUpdate"));
                    profile.PartsUOMMaster = reader.GetBoolean(reader.GetOrdinal("PartsUOMMaster"));
                    profile.PartsUOMMasterCreate = reader.GetBoolean(reader.GetOrdinal("PartsUOMMasterCreate"));
                    profile.PartsUOMMasterUpdate = reader.GetBoolean(reader.GetOrdinal("PartsUOMMasterUpdate"));
                    profile.DepartmentMaster = reader.GetBoolean(reader.GetOrdinal("DepartmentMaster"));
                    profile.DepartmentMasterCreate = reader.GetBoolean(reader.GetOrdinal("DepartmentMasterCreate"));
                    profile.DepartmentMasterUpdate = reader.GetBoolean(reader.GetOrdinal("DepartmentMasterUpdate"));
                    profile.PositionMaster = reader.GetBoolean(reader.GetOrdinal("PositionMaster"));
                    profile.PositionMasterCreate = reader.GetBoolean(reader.GetOrdinal("PositionMasterCreate"));
                    profile.PositionMasterUpdate = reader.GetBoolean(reader.GetOrdinal("PositionMasterUpdate"));
                    profile.PartsOEMMaster = reader.GetBoolean(reader.GetOrdinal("PartsOEMMaster"));
                    profile.PartsOEMMasterCreate = reader.GetBoolean(reader.GetOrdinal("PartsOEMMasterCreate"));
                    profile.PartsOEMMasterUpdate = reader.GetBoolean(reader.GetOrdinal("PartsOEMMasterUpdate"));
                    profile.PartsVehicleMakeMaster = reader.GetBoolean(reader.GetOrdinal("PartsVehicleMakeMaster"));
                    profile.PartsVehicleMakeMasterCreate = reader.GetBoolean(reader.GetOrdinal("PartsVehicleMakeMasterCreate"));
                    profile.PartsVehicleMakeMasterUpdate = reader.GetBoolean(reader.GetOrdinal("PartsVehicleMakeMasterUpdate"));
                    profile.MnuReports = reader.GetBoolean(reader.GetOrdinal("MnuReports"));
                    profile.SalesReport = reader.GetBoolean(reader.GetOrdinal("SalesReport"));
                    profile.ReceivingReport = reader.GetBoolean(reader.GetOrdinal("ReceivingReport"));
                    profile.InventoryReports = reader.GetBoolean(reader.GetOrdinal("InventoryReports"));
                    profile.MonthlyInventoryReport = reader.GetBoolean(reader.GetOrdinal("MonthlyInventoryReport"));
                    profile.MnuUtilities = reader.GetBoolean(reader.GetOrdinal("MnuUtilities"));
                    profile.CompanyProfile = reader.GetBoolean(reader.GetOrdinal("CompanyProfile"));
                    profile.UserProfile = reader.GetBoolean(reader.GetOrdinal("UserProfile"));
                    profile.SyncProfile = reader.GetBoolean(reader.GetOrdinal("SyncProfile"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return profile;
        }

        public override string Update(CustomerProfileModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                //command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT EmployeeID FROM TblUserProfileAccess WITH(READPAST) WHERE EmployeeID=@EmployeeID AND UserID!=@UserID) " +
                command = Connection.setTransactionCommand($"IF EXISTS(SELECT EmployeeID FROM TblUserProfileAccess WITH(READPAST) WHERE EmployeeID=@EmployeeID) " +
                                                           $"BEGIN " +
                                                           $"  UPDATE TblUserProfileAccess SET UserID=@UserID, UserPassword=@UserPassword, UserType=@UserType, " +
                                                           $"       MnuTransaction=@MnuTransaction, OrderTaking=@OrderTaking, OrderTakingCreate=@OrderTakingCreate, " +
                                                           $"       OrderTakingPost=@OrderTakingPost, OrderConsolidation=@OrderConsolidation, " +
                                                           $"       OrderConsolidationCreate=@OrderConsolidationCreate, OrderConsolidationToBSB=@OrderConsolidationToBSB, " +
                                                           $"       PurchaseOrderMonitoring=@PurchaseOrderMonitoring, PurchaseOrderMonitoringCreate=@PurchaseOrderMonitoringCreate, " +
                                                           $"       PurchaseOrderMonitoringCancel=@PurchaseOrderMonitoringCancel, PurchaseOrderMonitoringClose=@PurchaseOrderMonitoringClose, " +
                                                           $"       PurchaseOrderMonitoringQuotation=@PurchaseOrderMonitoringQuotation, Receiving=@Receiving, " +
                                                           $"       ReceivingSave=@ReceivingSave, ReceivingPost=@ReceivingPost, ReceivingArchive=@ReceivingArchive, " +
                                                           $"       PurchaseReturn=@PurchaseReturn, PurchaseReturnSave=@PurchaseReturnSave, PurchaseReturnArchive=@PurchaseReturnArchive, " +
                                                           $"       PriceManagement=@PriceManagement, PriceManagementModify=@PriceManagementModify, PriceManagementArchive=@PriceManagementArchive, " +
                                                           $"       PriceManagementPrint=@PriceManagementPrint, SalesOrder=@SalesOrder, SalesOrderCrud=@SalesOrderCrud, " +
                                                           $"       InvoiceGeneration=@InvoiceGeneration, InvoiceGenerationPayment=@InvoiceGenerationPayment, SalesReturn=@SalesReturn, " +
                                                           $"       SalesReturnCreate=@SalesReturnCreate, SalesReturnArchive=@SalesReturnArchive, BeginningBalance=@BeginningBalance, BeginningBalanceCreate=@BeginningBalanceCreate, " +
                                                           $"       BeginningBalanceArchive=@BeginningBalanceArchive, StockTransfer=@StockTransfer, " +
                                                           $"       StockTransferCreate=@StockTransferCreate, StockTransferArchive=@StockTransferArchive, " +
                                                           $"       StockAdjustment=@StockAdjustment, StockAdjustmentCreate=@StockAdjustmentCreate, " +
                                                           $"       StockAdjustmentArchive=@StockAdjustmentArchive, MnuInquiry=@MnuInquiry, PartsInquiry=@PartsInquiry, " +
                                                           $"       InventoryManagement=@InventoryManagement, AuditTrail=@AuditTrail, SalesOrderArchive=@SalesOrderArchive, " +
                                                           $"       MnuMasterfiles=@MnuMasterfiles, PartsLibrary=@PartsLibrary, PartsLibraryCreate=@PartsLibraryCreate, " +
                                                           $"       PartsLibraryUpdate=@PartsLibraryUpdate, SupplierLibrary=@SupplierLibrary, SupplierLibraryCreate=@SupplierLibraryCreate, " +
                                                           $"       SupplierLibraryUpdate=@SupplierLibraryUpdate, CustomerLibrary=@CustomerLibrary, CustomerLibraryCreate=@CustomerLibraryCreate, " +
                                                           $"       CustomerLibraryUpdate=@CustomerLibraryUpdate, WarehouseMaster=@WarehouseMaster, " +
                                                           $"       WarehouseMasterCreate=@WarehouseMasterCreate, WarehouseMasterUpdate=@WarehouseMasterUpdate, " +
                                                           $"       LocationMaster=@LocationMaster, LocationMasterCreate=@LocationMasterCreate, " +
                                                           $"       LocationMasterUpdate=@LocationMasterUpdate, ReasonMaster=@ReasonMaster, ReasonMasterCreate=@ReasonMasterCreate, " +
                                                           $"       ReasonMasterUpdate=@ReasonMasterUpdate, RegionMaster=@RegionMaster, RegionMasterCreate=@RegionMasterCreate, " +
                                                           $"       RegionMasterUpdate=@RegionMasterUpdate, ProvinceMaster=@ProvinceMaster, ProvinceMasterCreate=@ProvinceMasterCreate, " +
                                                           $"       ProvinceMasterUpdate=@ProvinceMasterUpdate, CityMaster=@CityMaster, CityMasterCreate=@CityMasterCreate, " +
                                                           $"       CityMasterUpdate=@CityMasterUpdate, TermMaster=@TermMaster, TermMasterCreate=@TermMasterCreate, " +
                                                           $"       TermMasterUpdate=@TermMasterUpdate, BrandMaster=@BrandMaster, BrandMasterCreate=@BrandMasterCreate, " +
                                                           $"       BrandMasterUpdate=@BrandMasterUpdate, DescriptionMaster=@DescriptionMaster, " +
                                                           $"       DescriptionMasterCreate=@DescriptionMasterCreate, DescriptionMasterUpdate=@DescriptionMasterUpdate, " +
                                                           $"       PartsUOMMaster=@PartsUOMMaster, PartsUOMMasterCreate=@PartsUOMMasterCreate, " +
                                                           $"       PartsUOMMasterUpdate=@PartsUOMMasterUpdate, DepartmentMaster=@DepartmentMaster, " +
                                                           $"       DepartmentMasterCreate=@DepartmentMasterCreate, DepartmentMasterUpdate=@DepartmentMasterUpdate, " +
                                                           $"       PositionMaster=@PositionMaster, PositionMasterCreate=@PositionMasterCreate, " +
                                                           $"       PositionMasterUpdate=@PositionMasterUpdate, PartsOEMMaster=@PartsOEMMaster, " +
                                                           $"       PartsOEMMasterCreate=@PartsOEMMasterCreate, PartsOEMMasterUpdate=@PartsOEMMasterUpdate, " +
                                                           $"       PartsVehicleMakeMaster=@PartsVehicleMakeMaster, PartsVehicleMakeMasterCreate=@PartsVehicleMakeMasterCreate, " +
                                                           $"       PartsVehicleMakeMasterUpdate=@PartsVehicleMakeMasterUpdate, MnuReports=@MnuReports, " +
                                                           $"       SalesReport=@SalesReport, ReceivingReport=@ReceivingReport, InventoryReports=@InventoryReports, " +
                                                           $"       MonthlyInventoryReport=@MonthlyInventoryReport, MnuUtilities=@MnuUtilities, " +
                                                           $"       CompanyProfile=@CompanyProfile, UserProfile=@UserProfile, SyncProfile=@SyncProfile, " +
                                                           $"       ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                           $"   WHERE EmployeeID=@EmployeeID " +
                                                           $"END " +
                                                           $"ELSE " +
                                                           $"BEGIN " +
                                                           $"  INSERT INTO TblUserProfileAccess(EmployeeID, UserID, UserPassword, UserType, MnuTransaction, OrderTaking, " +
                                                           $"       OrderTakingCreate, OrderTakingPost, OrderConsolidation, OrderConsolidationCreate, " +
                                                           $"       OrderConsolidationToBSB, PurchaseOrderMonitoring, PurchaseOrderMonitoringCreate, " +
                                                           $"       PurchaseOrderMonitoringCancel, PurchaseOrderMonitoringClose, PurchaseOrderMonitoringQuotation, " +
                                                           $"       Receiving, ReceivingSave, ReceivingPost, ReceivingArchive, PurchaseReturn, PurchaseReturnSave, " +
                                                           $"       PurchaseReturnArchive, PriceManagement, PriceManagementModify, PriceManagementArchive, " +
                                                           $"       PriceManagementPrint, SalesOrder, SalesOrderCrud, InvoiceGeneration, InvoiceGenerationPayment, " +
                                                           $"       SalesReturn, SalesReturnCreate, SalesReturnArchive, BeginningBalance, BeginningBalanceCreate, " +
                                                           $"       BeginningBalanceArchive, StockTransfer, StockTransferCreate, StockTransferArchive, StockAdjustment, " +
                                                           $"       StockAdjustmentCreate, StockAdjustmentArchive, MnuInquiry, PartsInquiry, InventoryManagement, " +
                                                           $"       AuditTrail, SalesOrderArchive, MnuMasterfiles, PartsLibrary, PartsLibraryCreate, PartsLibraryUpdate, " +
                                                           $"       SupplierLibrary, SupplierLibraryCreate, SupplierLibraryUpdate, CustomerLibrary, CustomerLibraryCreate, " +
                                                           $"       CustomerLibraryUpdate, WarehouseMaster, WarehouseMasterCreate, WarehouseMasterUpdate, LocationMaster, " +
                                                           $"       LocationMasterCreate, LocationMasterUpdate, ReasonMaster, ReasonMasterCreate, ReasonMasterUpdate, " +
                                                           $"       RegionMaster, RegionMasterCreate, RegionMasterUpdate, ProvinceMaster, ProvinceMasterCreate, " +
                                                           $"       ProvinceMasterUpdate, CityMaster, CityMasterCreate, CityMasterUpdate, TermMaster, TermMasterCreate, " +
                                                           $"       TermMasterUpdate, BrandMaster, BrandMasterCreate, BrandMasterUpdate, DescriptionMaster, DescriptionMasterCreate, " +
                                                           $"       DescriptionMasterUpdate, PartsUOMMaster, PartsUOMMasterCreate, PartsUOMMasterUpdate, DepartmentMaster, " +
                                                           $"       DepartmentMasterCreate, DepartmentMasterUpdate, PositionMaster, PositionMasterCreate, PositionMasterUpdate, " +
                                                           $"       PartsOEMMaster, PartsOEMMasterCreate, PartsOEMMasterUpdate, PartsVehicleMakeMaster, PartsVehicleMakeMasterCreate, " +
                                                           $"       PartsVehicleMakeMasterUpdate, MnuReports, SalesReport, ReceivingReport, InventoryReports, MonthlyInventoryReport, " +
                                                           $"       MnuUtilities, CompanyProfile, UserProfile, SyncProfile, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                           $"   VALUES(@EmployeeID, @UserID, @UserPassword, @UserType, @MnuTransaction, @OrderTaking, " +
                                                           $"       @OrderTakingCreate, @OrderTakingPost, @OrderConsolidation, @OrderConsolidationCreate, " +
                                                           $"       @OrderConsolidationToBSB, @PurchaseOrderMonitoring, @PurchaseOrderMonitoringCreate, " +
                                                           $"       @PurchaseOrderMonitoringCancel, @PurchaseOrderMonitoringClose, @PurchaseOrderMonitoringQuotation, " +
                                                           $"       @Receiving, @ReceivingSave, @ReceivingPost, @ReceivingArchive, @PurchaseReturn, @PurchaseReturnSave, " +
                                                           $"       @PurchaseReturnArchive, @PriceManagement, @PriceManagementModify, @PriceManagementArchive, " +
                                                           $"       @PriceManagementPrint, @SalesOrder, @SalesOrderCrud, @InvoiceGeneration, @InvoiceGenerationPayment, " +
                                                           $"       @SalesReturn, @SalesReturnCreate, @SalesReturnArchive, @BeginningBalance, @BeginningBalanceCreate, " +
                                                           $"       @BeginningBalanceArchive, @StockTransfer, @StockTransferCreate, @StockTransferArchive, @StockAdjustment, " +
                                                           $"       @StockAdjustmentCreate, @StockAdjustmentArchive, @MnuInquiry, @PartsInquiry, @InventoryManagement, " +
                                                           $"       @AuditTrail, @SalesOrderArchive, @MnuMasterfiles, @PartsLibrary, @PartsLibraryCreate, @PartsLibraryUpdate, " +
                                                           $"       @SupplierLibrary, @SupplierLibraryCreate, @SupplierLibraryUpdate, @CustomerLibrary, @CustomerLibraryCreate, " +
                                                           $"       @CustomerLibraryUpdate, @WarehouseMaster, @WarehouseMasterCreate, @WarehouseMasterUpdate, @LocationMaster, " +
                                                           $"       @LocationMasterCreate, @LocationMasterUpdate, @ReasonMaster, @ReasonMasterCreate, @ReasonMasterUpdate, " +
                                                           $"       @RegionMaster, @RegionMasterCreate, @RegionMasterUpdate, @ProvinceMaster, @ProvinceMasterCreate, " +
                                                           $"       @ProvinceMasterUpdate, @CityMaster, @CityMasterCreate, @CityMasterUpdate, @TermMaster, @TermMasterCreate, " +
                                                           $"       @TermMasterUpdate, @BrandMaster, @BrandMasterCreate, @BrandMasterUpdate, @DescriptionMaster, @DescriptionMasterCreate, " +
                                                           $"       @DescriptionMasterUpdate, @PartsUOMMaster, @PartsUOMMasterCreate, @PartsUOMMasterUpdate, @DepartmentMaster, " +
                                                           $"       @DepartmentMasterCreate, @DepartmentMasterUpdate, @PositionMaster, @PositionMasterCreate, @PositionMasterUpdate, " +
                                                           $"       @PartsOEMMaster, @PartsOEMMasterCreate, @PartsOEMMasterUpdate, @PartsVehicleMakeMaster, @PartsVehicleMakeMasterCreate, " +
                                                           $"       @PartsVehicleMakeMasterUpdate, @MnuReports, @SalesReport, @ReceivingReport, @InventoryReports, @MonthlyInventoryReport, " +
                                                           $"       @MnuUtilities, @CompanyProfile, @UserProfile, @SyncProfile, @CreatedBy, GETDATE(), @CreatedBy, GETDATE()) " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@EmployeeID", entity.EmployeeID);
                command.Parameters.AddWithValue("@UserID", entity.UserID);
                command.Parameters.AddWithValue("@UserPassword", entity.UserPassword);
                command.Parameters.AddWithValue("@UserType", entity.UserType);
                command.Parameters.AddWithValue("@MnuTransaction", entity.MnuTransaction);
                command.Parameters.AddWithValue("@OrderTaking", entity.OrderTaking);
                command.Parameters.AddWithValue("@OrderTakingCreate", entity.OrderTakingCreate);
                command.Parameters.AddWithValue("@OrderTakingPost", entity.OrderTakingPost);
                command.Parameters.AddWithValue("@OrderConsolidation", entity.OrderConsolidation);
                command.Parameters.AddWithValue("@OrderConsolidationCreate", entity.OrderConsolidationCreate);
                command.Parameters.AddWithValue("@OrderConsolidationToBSB", entity.OrderConsolidationToBSB);
                command.Parameters.AddWithValue("@PurchaseOrderMonitoring", entity.PurchaseOrderMonitoring);
                command.Parameters.AddWithValue("@PurchaseOrderMonitoringCreate", entity.PurchaseOrderMonitoringCreate);
                command.Parameters.AddWithValue("@PurchaseOrderMonitoringCancel", entity.PurchaseOrderMonitoringCancel);
                command.Parameters.AddWithValue("@PurchaseOrderMonitoringClose", entity.PurchaseOrderMonitoringClose);
                command.Parameters.AddWithValue("@PurchaseOrderMonitoringQuotation", entity.PurchaseOrderMonitoringQuotation);
                command.Parameters.AddWithValue("@Receiving", entity.Receiving);
                command.Parameters.AddWithValue("@ReceivingSave", entity.ReceivingSave);
                command.Parameters.AddWithValue("@ReceivingPost", entity.ReceivingPost);
                command.Parameters.AddWithValue("@ReceivingArchive", entity.ReceivingArchive);
                command.Parameters.AddWithValue("@PurchaseReturn", entity.PurchaseReturn);
                command.Parameters.AddWithValue("@PurchaseReturnSave", entity.PurchaseReturnSave);
                command.Parameters.AddWithValue("@PurchaseReturnArchive", entity.PurchaseReturnArchive);
                command.Parameters.AddWithValue("@PriceManagement", entity.PriceManagement);
                command.Parameters.AddWithValue("@PriceManagementModify", entity.PriceManagementModify);
                command.Parameters.AddWithValue("@PriceManagementArchive", entity.PriceManagementArchive);
                command.Parameters.AddWithValue("@PriceManagementPrint", entity.PriceManagementPrint);
                command.Parameters.AddWithValue("@SalesOrder", entity.SalesOrder);
                command.Parameters.AddWithValue("@SalesOrderCrud", entity.SalesOrderCrud);
                command.Parameters.AddWithValue("@InvoiceGeneration", entity.InvoiceGeneration);
                command.Parameters.AddWithValue("@InvoiceGenerationPayment", entity.InvoiceGenerationPayment);
                command.Parameters.AddWithValue("@SalesReturn", entity.SalesReturn);
                command.Parameters.AddWithValue("@SalesReturnCreate", entity.SalesReturnCreate);
                command.Parameters.AddWithValue("@SalesReturnArchive", entity.SalesReturnArchive);
                command.Parameters.AddWithValue("@BeginningBalance", entity.BeginningBalance);
                command.Parameters.AddWithValue("@BeginningBalanceCreate", entity.BeginningBalanceCreate);
                command.Parameters.AddWithValue("@BeginningBalanceArchive", entity.BeginningBalanceArchive);
                command.Parameters.AddWithValue("@StockTransfer", entity.StockTransfer);
                command.Parameters.AddWithValue("@StockTransferCreate", entity.StockTransferCreate);
                command.Parameters.AddWithValue("@StockTransferArchive", entity.StockTransferArchive);
                command.Parameters.AddWithValue("@StockAdjustment", entity.StockAdjustment);
                command.Parameters.AddWithValue("@StockAdjustmentCreate", entity.StockAdjustmentCreate);
                command.Parameters.AddWithValue("@StockAdjustmentArchive", entity.StockAdjustmentArchive);
                command.Parameters.AddWithValue("@MnuInquiry", entity.MnuInquiry);
                command.Parameters.AddWithValue("@PartsInquiry", entity.PartsInquiry);
                command.Parameters.AddWithValue("@InventoryManagement", entity.InventoryManagement);
                command.Parameters.AddWithValue("@AuditTrail", entity.AuditTrail);
                command.Parameters.AddWithValue("@SalesOrderArchive", entity.SalesOrderArchive);
                command.Parameters.AddWithValue("@MnuMasterfiles", entity.MnuMasterfiles);
                command.Parameters.AddWithValue("@PartsLibrary", entity.PartsLibrary);
                command.Parameters.AddWithValue("@PartsLibraryCreate", entity.PartsLibraryCreate);
                command.Parameters.AddWithValue("@PartsLibraryUpdate", entity.PartsLibraryUpdate);
                command.Parameters.AddWithValue("@SupplierLibrary", entity.SupplierLibrary);
                command.Parameters.AddWithValue("@SupplierLibraryCreate", entity.SupplierLibraryCreate);
                command.Parameters.AddWithValue("@SupplierLibraryUpdate", entity.SupplierLibraryUpdate);
                command.Parameters.AddWithValue("@CustomerLibrary", entity.CustomerLibrary);
                command.Parameters.AddWithValue("@CustomerLibraryCreate", entity.CustomerLibraryCreate);
                command.Parameters.AddWithValue("@CustomerLibraryUpdate", entity.CustomerLibraryUpdate);
                command.Parameters.AddWithValue("@WarehouseMaster", entity.WarehouseMaster);
                command.Parameters.AddWithValue("@WarehouseMasterCreate", entity.WarehouseMasterCreate);
                command.Parameters.AddWithValue("@WarehouseMasterUpdate", entity.WarehouseMasterUpdate);
                command.Parameters.AddWithValue("@LocationMaster", entity.LocationMaster);
                command.Parameters.AddWithValue("@LocationMasterCreate", entity.LocationMasterCreate);
                command.Parameters.AddWithValue("@LocationMasterUpdate", entity.LocationMasterUpdate);
                command.Parameters.AddWithValue("@ReasonMaster", entity.ReasonMaster);
                command.Parameters.AddWithValue("@ReasonMasterCreate", entity.ReasonMasterCreate);
                command.Parameters.AddWithValue("@ReasonMasterUpdate", entity.ReasonMasterUpdate);
                command.Parameters.AddWithValue("@RegionMaster", entity.RegionMaster);
                command.Parameters.AddWithValue("@RegionMasterCreate", entity.RegionMasterCreate);
                command.Parameters.AddWithValue("@RegionMasterUpdate", entity.RegionMasterUpdate);
                command.Parameters.AddWithValue("@ProvinceMaster", entity.ProvinceMaster);
                command.Parameters.AddWithValue("@ProvinceMasterCreate", entity.ProvinceMasterCreate);
                command.Parameters.AddWithValue("@ProvinceMasterUpdate", entity.ProvinceMasterUpdate);
                command.Parameters.AddWithValue("@CityMaster", entity.CityMaster);
                command.Parameters.AddWithValue("@CityMasterCreate", entity.CityMasterCreate);
                command.Parameters.AddWithValue("@CityMasterUpdate", entity.CityMasterUpdate);
                command.Parameters.AddWithValue("@TermMaster", entity.TermMaster);
                command.Parameters.AddWithValue("@TermMasterCreate", entity.TermMasterCreate);
                command.Parameters.AddWithValue("@TermMasterUpdate", entity.TermMasterUpdate);
                command.Parameters.AddWithValue("@BrandMaster", entity.BrandMaster);
                command.Parameters.AddWithValue("@BrandMasterCreate", entity.BrandMasterCreate);
                command.Parameters.AddWithValue("@BrandMasterUpdate", entity.BrandMasterUpdate);
                command.Parameters.AddWithValue("@DescriptionMaster", entity.DescriptionMaster);
                command.Parameters.AddWithValue("@DescriptionMasterCreate", entity.DescriptionMasterCreate);
                command.Parameters.AddWithValue("@DescriptionMasterUpdate", entity.DescriptionMasterUpdate);
                command.Parameters.AddWithValue("@PartsUOMMaster", entity.PartsUOMMaster);
                command.Parameters.AddWithValue("@PartsUOMMasterCreate", entity.PartsUOMMasterCreate);
                command.Parameters.AddWithValue("@PartsUOMMasterUpdate", entity.PartsUOMMasterUpdate);
                command.Parameters.AddWithValue("@DepartmentMaster", entity.DepartmentMaster);
                command.Parameters.AddWithValue("@DepartmentMasterCreate", entity.DepartmentMasterCreate);
                command.Parameters.AddWithValue("@DepartmentMasterUpdate", entity.DepartmentMasterUpdate);
                command.Parameters.AddWithValue("@PositionMaster", entity.PositionMaster);
                command.Parameters.AddWithValue("@PositionMasterCreate", entity.PositionMasterCreate);
                command.Parameters.AddWithValue("@PositionMasterUpdate", entity.PositionMasterUpdate);
                command.Parameters.AddWithValue("@PartsOEMMaster", entity.PartsOEMMaster);
                command.Parameters.AddWithValue("@PartsOEMMasterCreate", entity.PartsOEMMasterCreate);
                command.Parameters.AddWithValue("@PartsOEMMasterUpdate", entity.PartsOEMMasterUpdate);
                command.Parameters.AddWithValue("@PartsVehicleMakeMaster", entity.PartsVehicleMakeMaster);
                command.Parameters.AddWithValue("@PartsVehicleMakeMasterCreate", entity.PartsVehicleMakeMasterCreate);
                command.Parameters.AddWithValue("@PartsVehicleMakeMasterUpdate", entity.PartsVehicleMakeMasterUpdate);
                command.Parameters.AddWithValue("@MnuReports", entity.MnuReports);
                command.Parameters.AddWithValue("@SalesReport", entity.SalesReport);
                command.Parameters.AddWithValue("@ReceivingReport", entity.ReceivingReport);
                command.Parameters.AddWithValue("@InventoryReports", entity.InventoryReports);
                command.Parameters.AddWithValue("@MonthlyInventoryReport", entity.MonthlyInventoryReport);
                command.Parameters.AddWithValue("@MnuUtilities", entity.MnuUtilities);
                command.Parameters.AddWithValue("@CompanyProfile", entity.CompanyProfile);
                command.Parameters.AddWithValue("@UserProfile", entity.UserProfile);
                command.Parameters.AddWithValue("@SyncProfile", entity.SyncProfile);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                command.ExecuteNonQuery();
                int i = command.ExecuteNonQuery();
                if (i != 1)
                {
                    message = "Something went wrong.";
                    transaction.Rollback();
                    transaction.Dispose();
                    connection.Close();
                    return message;
                }
                Helper.TranLog("User Profile", "Modified profile:" + entity.EmployeeID, connection, command, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                message = ex.Message;
                transaction.Rollback();
                Console.WriteLine(ex.Message);
            }
            finally
            {
                transaction.Dispose();
                connection.Close();
            }
            return message;
        }

        public CustomerProfileModel GetUserProfile(string EmpID)
        {
            CustomerProfileModel profile = new CustomerProfileModel();
            try
            {
                connection.Open();
                command = Connection.setCommand($"SELECT *" +
                                                $"  FROM TblUserProfileAccess WITH(READPAST) " +
                                                $"  WHERE EmployeeID=@EmployeeID", connection);
                command.Parameters.AddWithValue("@EmployeeID", EmpID);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    profile = new CustomerProfileModel()
                    {
                        UserID = reader.GetString(0).TrimEnd(),
                        UserPassword = reader.GetString(1).TrimEnd(),
                        UserType = reader.GetDecimal(2),
                        MnuTransaction = reader.GetBoolean(3),
                        OrderTaking = reader.GetBoolean(4),
                        OrderTakingCreate = reader.GetBoolean(5),
                        OrderTakingPost = reader.GetBoolean(6),
                        OrderConsolidation = reader.GetBoolean(7),
                        OrderConsolidationCreate = reader.GetBoolean(8),
                        OrderConsolidationToBSB = reader.GetBoolean(9),
                        PurchaseOrderMonitoring = reader.GetBoolean(10),
                        PurchaseOrderMonitoringCreate = reader.GetBoolean(11),
                        PurchaseOrderMonitoringCancel = reader.GetBoolean(12),
                        PurchaseOrderMonitoringClose = reader.GetBoolean(13),
                        PurchaseOrderMonitoringQuotation = reader.GetBoolean(14),
                        Receiving = reader.GetBoolean(15),
                        ReceivingSave = reader.GetBoolean(16),
                        ReceivingPost = reader.GetBoolean(17),
                        ReceivingArchive = reader.GetBoolean(18),
                        PurchaseReturn = reader.GetBoolean(19),
                        PurchaseReturnSave = reader.GetBoolean(20),
                        PurchaseReturnArchive = reader.GetBoolean(21),
                        PriceManagement = reader.GetBoolean(22),
                        PriceManagementModify = reader.GetBoolean(23),
                        PriceManagementArchive = reader.GetBoolean(24),
                        PriceManagementPrint = reader.GetBoolean(25),
                        SalesOrder = reader.GetBoolean(26),
                        SalesOrderCrud = reader.GetBoolean(27),
                        InvoiceGeneration = reader.GetBoolean(28),
                        InvoiceGenerationPayment = reader.GetBoolean(29),
                        SalesReturn = reader.GetBoolean(30),
                        SalesReturnCreate = reader.GetBoolean(31),
                        SalesReturnArchive = reader.GetBoolean(32),
                        BeginningBalance = reader.GetBoolean(33),
                        BeginningBalanceCreate = reader.GetBoolean(34),
                        BeginningBalanceArchive = reader.GetBoolean(35),
                        StockTransfer = reader.GetBoolean(36),
                        StockTransferCreate = reader.GetBoolean(37),
                        StockTransferArchive = reader.GetBoolean(38),
                        StockAdjustment = reader.GetBoolean(39),
                        StockAdjustmentCreate = reader.GetBoolean(40),
                        StockAdjustmentArchive = reader.GetBoolean(41),
                        MnuInquiry = reader.GetBoolean(42),
                        PartsInquiry = reader.GetBoolean(43),
                        InventoryManagement = reader.GetBoolean(44),
                        AuditTrail = reader.GetBoolean(45),
                        SalesOrderArchive = reader.GetBoolean(46),
                        MnuMasterfiles = reader.GetBoolean(47),
                        PartsLibrary = reader.GetBoolean(48),
                        PartsLibraryCreate = reader.GetBoolean(49),
                        PartsLibraryUpdate = reader.GetBoolean(50),
                        SupplierLibrary = reader.GetBoolean(51),
                        SupplierLibraryCreate = reader.GetBoolean(52),
                        SupplierLibraryUpdate = reader.GetBoolean(53),
                        CustomerLibrary = reader.GetBoolean(54),
                        CustomerLibraryCreate = reader.GetBoolean(55),
                        CustomerLibraryUpdate = reader.GetBoolean(56),
                        WarehouseMaster = reader.GetBoolean(57),
                        WarehouseMasterCreate = reader.GetBoolean(58),
                        WarehouseMasterUpdate = reader.GetBoolean(59),
                        LocationMaster = reader.GetBoolean(60),
                        LocationMasterCreate = reader.GetBoolean(61),
                        LocationMasterUpdate = reader.GetBoolean(62),
                        ReasonMaster = reader.GetBoolean(63),
                        ReasonMasterCreate = reader.GetBoolean(64),
                        ReasonMasterUpdate = reader.GetBoolean(65),
                        RegionMaster = reader.GetBoolean(66),
                        RegionMasterCreate = reader.GetBoolean(67),
                        RegionMasterUpdate = reader.GetBoolean(68),
                        ProvinceMaster = reader.GetBoolean(69),
                        ProvinceMasterCreate = reader.GetBoolean(70),
                        ProvinceMasterUpdate = reader.GetBoolean(71),
                        CityMaster = reader.GetBoolean(72),
                        CityMasterCreate = reader.GetBoolean(73),
                        CityMasterUpdate = reader.GetBoolean(74),
                        TermMaster = reader.GetBoolean(75),
                        TermMasterCreate = reader.GetBoolean(76),
                        TermMasterUpdate = reader.GetBoolean(77),
                        BrandMaster = reader.GetBoolean(78),
                        BrandMasterCreate = reader.GetBoolean(79),
                        BrandMasterUpdate = reader.GetBoolean(80),
                        DescriptionMaster = reader.GetBoolean(81),
                        DescriptionMasterCreate = reader.GetBoolean(82),
                        DescriptionMasterUpdate = reader.GetBoolean(83),
                        PartsUOMMaster = reader.GetBoolean(84),
                        PartsUOMMasterCreate = reader.GetBoolean(85),
                        PartsUOMMasterUpdate = reader.GetBoolean(86),
                        DepartmentMaster = reader.GetBoolean(87),
                        DepartmentMasterCreate = reader.GetBoolean(88),
                        DepartmentMasterUpdate = reader.GetBoolean(89),
                        PositionMaster = reader.GetBoolean(90),
                        PositionMasterCreate = reader.GetBoolean(91),
                        PositionMasterUpdate = reader.GetBoolean(92),
                        PartsOEMMaster = reader.GetBoolean(93),
                        PartsOEMMasterCreate = reader.GetBoolean(94),
                        PartsOEMMasterUpdate = reader.GetBoolean(95),
                        PartsVehicleMakeMaster = reader.GetBoolean(96),
                        PartsVehicleMakeMasterCreate = reader.GetBoolean(97),
                        PartsVehicleMakeMasterUpdate = reader.GetBoolean(98),
                        MnuReports = reader.GetBoolean(99),
                        SalesReport = reader.GetBoolean(100),
                        ReceivingReport = reader.GetBoolean(101),
                        InventoryReports = reader.GetBoolean(102),
                        MonthlyInventoryReport = reader.GetBoolean(103),
                        MnuUtilities = reader.GetBoolean(104),
                        CompanyProfile = reader.GetBoolean(105),
                        UserProfile = reader.GetBoolean(106),
                        SyncProfile = reader.GetBoolean(107),
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return profile;
        }

        public DataTable UserLog(string User)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand($"SELECT module_name, action_taken, date_time " +
                                                $"  FROM TblTranLog WITH(READPAST) " +
                                                $"  WHERE userid=@userid", connection);
                command.Parameters.AddWithValue("@userid", User);
                reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
    }
}
