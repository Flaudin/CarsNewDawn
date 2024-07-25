using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Reports
{
    internal class SalesReturnReportModel
    {
        public IEnumerable<SalesReturnSummary> SummaryList { get; set; } = null;
        public IEnumerable<SalesReturnRegister> RegisterList { get; set; } = null;
        public IEnumerable<SalesReturnGroup> GroupList { get; set; } = null;
    }

    internal class SalesReturnReportFilter
    {
        public string SRDateFrom { get; set; }
        public string SRDateTo { get; set; }
        public string SODateFrom { get; set; }
        public string SODateTo { get; set; }
        public string CustName { get; set; } = string.Empty;
        public string Salesman { get; set; } = string.Empty;
        public string PartNo { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Term { get; set; } = string.Empty;
        public string GroupBy { get; set; } = string.Empty;
    }

    internal class SalesReturnSummary
    {
        public string SRNo { get; set; }
        public string SRDate { get; set; }
        public string SONo { get; set; } = string.Empty;
        public string SODate { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public string Salesman { get; set; } = string.Empty;
        public string Term { get; set; } = string.Empty;
        public decimal GoodQty { get; set; }
        public decimal DefectiveQty { get; set; }
        public decimal NetPrice { get; set; }
    }

    internal class SalesReturnRegister
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
        public IEnumerable<SalesReturnRegisterSubTotal> SubTotalList { get; set; } = null;
    }

    internal class SalesReturnRegisterSubTotal
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

    internal class SalesReturnGroup
    {
        public string ItemNo { get; set; }
        //can be SO/Invoice
        public string SRNo { get; set; } = string.Empty;
        public string SRDate { get; set; } = string.Empty;
        public string SONo { get; set; } = string.Empty;
        public string SODate { get; set; } = string.Empty;
        public string PartNo { get; set; } = string.Empty;
        public string PartDescription { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Salesman { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public decimal GoodQty { get; set; }
        public decimal DefectiveQty { get; set; }
        public decimal NetPrice { get; set; }
        //public IEnumerable<SalesGroupDetailsTotal> TotalList { get; set; } = null;
    }
}
