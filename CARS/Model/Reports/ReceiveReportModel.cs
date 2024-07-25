using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Reports
{
    internal class ReceiveReportModel
    {
        public IEnumerable<ReceiveSummary> SummaryList { get; set; } = null;
        public IEnumerable<ReceivingDetailsListing> ReceivingDetailsList { get; set; } = null;
        public string user { get; set; }
    }

    internal class RRSelection
    {
        public string RRNo { get; set; }
        public string RRDate { get; set; }
        public string Status { get; set; }
    }


    internal class HeaderContent
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string TelNo { get; set; }
        public string TinNo { get; set; }
        public string CompLogo { get; set; }
    }

    internal class ReceiveSummary
    {
        public string RRNo { get; set;}
        public string RRDate { get; set;}
        public string SupplierName { get; set;}
        public string RefNo { get; set;}
        public string RefDate { get; set;}
        public string Terms { get; set;}
        public string PONo{ get; set;}
        public string Status { get; set;}
        public decimal Amount { get; set;}
    }

    internal class ReceivingDetailsListing
    {
        public string RRDate { get; set; }
        public string RRNo { get; set; }
        public string Supplier {  get; set; }
        public string SKU { get; set; }
        public string PartNo { get; set; }
        public string PartDesc { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public decimal Qty { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalAmt { get; set; }
    }
}
