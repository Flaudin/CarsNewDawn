using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Reports
{
    internal class SalesReportModel
    {
        public IEnumerable<SalesSummary> SummaryList { get; set; } = null;
        public IEnumerable<SalesRegister> RegisterList { get; set; } = null;
        public IEnumerable<SalesGroup> GroupList { get; set; } = null;
    }

    internal class SalesReportFilter
    {
        public string Type { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string CustName { get; set; } = string.Empty;
        public string Salesman { get; set; } = string.Empty;
        public string PartNo { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Term { get; set; } = string.Empty;
        public string GroupBy { get; set; } = string.Empty;
    }

    internal class SalesSummary
    {
        public string ItemNo { get; set; }
        public string SONo { get; set; } = string.Empty;
        public string SODate { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public string Salesman { get; set; } = string.Empty;
        public string Term { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
    }

    internal class SalesRegister
    {
        public string PartNumber { get; set; }
        public string PartDescription { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public decimal Qty { get; set; }
        public decimal GrossSales { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal SalesReturn { get; set; }
        public decimal SalesReturnPercent { get; set; }
        public decimal NetSales { get; set; }
        public decimal NetSalesPercent { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalCostPercent { get; set; }
        public decimal GrossProfit { get; set; }
        public decimal GrossProfitPercent { get; set; }
        public IEnumerable<SalesRegisterSubTotal> SubTotalList { get; set; } = null;
    }

    internal class SalesRegisterSubTotal
    {
        public decimal TotalGrossSales { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalDiscountPercent { get; set; }
        public decimal TotalSalesReturn { get; set; }
        public decimal TotalSalesReturnPercent { get; set; }
        public decimal TotaNetSales { get; set; }
        public decimal TotalNetSalesPercent { get; set; }
        public decimal TotalTotalCost { get; set; }
        public decimal TotalTotalCostPercent { get; set; }
        public decimal TotalGrossProfit { get; set; }
        public decimal TotalGrossProfitPercent { get; set; }
    }

    //internal class SalesGroup
    //{
    //    public string Brand { get; set; } = string.Empty;
    //    public string PartNo { get; set; } = string.Empty;
    //    public string Description { get; set; } = string.Empty;
    //    public string Customer { get; set; } = string.Empty;
    //    public string Salesman { get; set; } = string.Empty;
    //    public IEnumerable<SalesGroupDetails> DetailsList { get; set; } = null;
    //}

    internal class SalesGroup
    {
        public string ItemNo { get; set; }
        //can be SO/Invoice
        public string SONo { get; set; } = string.Empty;
        public string PartNo { get; set; } = string.Empty;
        public string PartDescription { get; set; } = string.Empty;
        public decimal Qty { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal NetPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Salesman { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        //public IEnumerable<SalesGroupDetailsTotal> TotalList { get; set; } = null;
    }

    internal class SalesGroupDetailsTotal
    {
        public decimal CustomerNetPRC { get; set; }
        public decimal SalesmanNetPRC { get; set; }
        public decimal TotalNetPRC { get; set; }
    }
}
