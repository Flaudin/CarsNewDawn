using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class SalesOrderReportModel
    {
        public string CompName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string TelNo { get; set; } = string.Empty;
        public string TinNo { get; set; } = string.Empty;
        public string CompLogo { get; set; } = string.Empty;
    }

    internal class SalesOrderReportCustomer
    {
        public string InvoiceNo { get; set; } = string.Empty;
        public string CustName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string TinNo { get; set; } = string.Empty;
        public string RegName { get; set; } = string.Empty;
        public string TermName { get; set; } = string.Empty;
        public IEnumerable<SalesOrderReportParts> PartsList { get; set; } = null;
    }

    internal class SalesOrderReportParts
    {
        public string ItemNo { get; set; }
        public string PartNo { get; set; } = string.Empty;
        public string PartName { get; set; } = string.Empty;
        public decimal Qty { get; set; }
        public string UomName { get; set; } = string.Empty;
        public decimal NetPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VATAmt { get; set; }
    }
}
