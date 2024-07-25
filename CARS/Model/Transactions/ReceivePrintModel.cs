using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class ReceivePrintModel
    {
        
            public string CompanyName { get; set; }
            public string Address { get; set; }
            public string TelNo { get; set; }
            public string TinNo { get; set; }
            public string CompLogo { get; set; }
        

        internal class ReceiveReportOrder
        {
            public string Supplier { get; set; }
            public string InvoiceNo { get; set; }
            public string Remarks { get; set; }
            public string ReceiveDate { get; set; }
            public string PONo { get; set; }
            public string Terms { get; set; }
            public IEnumerable<ReceiveReportParts> receiveReportParts { get; set; }
        }

        internal class ReceiveReportParts
        {
            public string ItemNo { get; set; }
            public string SKU { get; set; }
            public string PartNo { get; set; }
            public string Description { get; set; }
            public string Brand { get; set; }
            public decimal Qty { get; set; }
            public string Uom { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal TotalPrice { get; set; }
            public List<string> Location { get; set; }
        }
    }
}
