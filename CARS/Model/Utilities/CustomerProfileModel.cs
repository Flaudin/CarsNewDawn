using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Utilities
{
    internal class CustomerProfileModel
    {
        public string EmployeeID { get; set; } = String.Empty;
        public string UserID { get; set; } = String.Empty;
        public string UserPassword { get; set; } = String.Empty;
        public decimal UserType { get; set; }
        public bool MnuTransaction { get; set; }
        public bool OrderTaking { get; set; }
        public bool OrderTakingCreate { get; set; }
        public bool OrderTakingPost { get; set; }
        public bool OrderConsolidation { get; set; }
        public bool OrderConsolidationCreate { get; set; }
        public bool OrderConsolidationToBSB { get; set; }
        public bool PurchaseOrderMonitoring { get; set; }
        public bool PurchaseOrderMonitoringCreate { get; set; }
        public bool PurchaseOrderMonitoringCancel { get; set; }
        public bool PurchaseOrderMonitoringClose { get; set; }
        public bool PurchaseOrderMonitoringQuotation { get; set; }
        public bool Receiving { get; set; }
        public bool ReceivingSave { get; set; }
        public bool ReceivingPost { get; set; }
        public bool ReceivingArchive { get; set; }
        public bool PurchaseReturn { get; set; }
        public bool PurchaseReturnSave { get; set; }
        public bool PurchaseReturnArchive { get; set; }
        public bool PriceManagement { get; set; }
        public bool PriceManagementModify { get; set; }
        public bool PriceManagementArchive { get; set; }
        public bool PriceManagementPrint { get; set; }
        public bool SalesOrder { get; set; }
        public bool SalesOrderCrud { get; set; }
        public bool InvoiceGeneration { get; set; }
        public bool InvoiceGenerationPayment { get; set; }
        public bool SalesReturn { get; set; }
        public bool SalesReturnCreate { get; set; }
        public bool SalesReturnArchive { get; set; }
        public bool BeginningBalance { get; set; }
        public bool BeginningBalanceCreate { get; set; }
        public bool BeginningBalanceArchive { get; set; }
        public bool StockTransfer { get; set; }
        public bool StockTransferCreate { get; set; }
        public bool StockTransferArchive { get; set; }
        public bool StockAdjustment { get; set; }
        public bool StockAdjustmentCreate { get; set; }
        public bool StockAdjustmentArchive { get; set; }
        public bool MnuInquiry { get; set; }
        public bool PartsInquiry { get; set; }
        public bool InventoryManagement { get; set; }
        public bool AuditTrail { get; set; }
        public bool SalesOrderArchive { get; set; }
        public bool MnuMasterfiles { get; set; }
        public bool PartsLibrary { get; set; }
        public bool PartsLibraryCreate { get; set; }
        public bool PartsLibraryUpdate { get; set; }
        public bool SupplierLibrary { get; set; }
        public bool SupplierLibraryCreate { get; set; }
        public bool SupplierLibraryUpdate { get; set; }
        public bool CustomerLibrary { get; set; }
        public bool CustomerLibraryCreate { get; set; }
        public bool CustomerLibraryUpdate { get; set; }
        public bool WarehouseMaster { get; set; }
        public bool WarehouseMasterCreate { get; set; }
        public bool WarehouseMasterUpdate { get; set; }
        public bool LocationMaster { get; set; }
        public bool LocationMasterCreate { get; set; }
        public bool LocationMasterUpdate { get; set; }
        public bool ReasonMaster { get; set; }
        public bool ReasonMasterCreate { get; set; }
        public bool ReasonMasterUpdate { get; set; }
        public bool RegionMaster { get; set; }
        public bool RegionMasterCreate { get; set; }
        public bool RegionMasterUpdate { get; set; }
        public bool ProvinceMaster { get; set; }
        public bool ProvinceMasterCreate { get; set; }
        public bool ProvinceMasterUpdate { get; set; }
        public bool CityMaster { get; set; }
        public bool CityMasterCreate { get; set; }
        public bool CityMasterUpdate { get; set; }
        public bool TermMaster { get; set; }
        public bool TermMasterCreate { get; set; }
        public bool TermMasterUpdate { get; set; }
        public bool BrandMaster { get; set; }
        public bool BrandMasterCreate { get; set; }
        public bool BrandMasterUpdate { get; set; }
        public bool DescriptionMaster { get; set; }
        public bool DescriptionMasterCreate { get; set; }
        public bool DescriptionMasterUpdate { get; set; }
        public bool PartsUOMMaster { get; set; }
        public bool PartsUOMMasterCreate { get; set; }
        public bool PartsUOMMasterUpdate { get; set; }
        public bool DepartmentMaster { get; set; }
        public bool DepartmentMasterCreate { get; set; }
        public bool DepartmentMasterUpdate { get; set; }
        public bool PositionMaster { get; set; }
        public bool PositionMasterCreate { get; set; }
        public bool PositionMasterUpdate { get; set; }
        public bool PartsOEMMaster { get; set; }
        public bool PartsOEMMasterCreate { get; set; }
        public bool PartsOEMMasterUpdate { get; set; }
        public bool PartsVehicleMakeMaster { get; set; }
        public bool PartsVehicleMakeMasterCreate { get; set; }
        public bool PartsVehicleMakeMasterUpdate { get; set; }
        public bool MnuReports { get; set; }
        public bool SalesReport { get; set; }
        public bool ReceivingReport { get; set; }
        public bool InventoryReports { get; set; }
        public bool MonthlyInventoryReport { get; set; }
        public bool MnuUtilities { get; set; }
        public bool CompanyProfile { get; set; }
        public bool UserProfile { get; set; }
        public bool SyncProfile { get; set; }
    }
}
