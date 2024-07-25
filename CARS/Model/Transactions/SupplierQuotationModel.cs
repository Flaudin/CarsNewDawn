using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class SupplierQuotationModel
    {
        public string SuppQuotNo { get; set; }
        public string SuppID { get; set; }
        public string QuotRefNo { get; set; }
        public string QuotDate { get; set; }
        public string TermID { get; set; }
        public decimal Status { get; set;}
        public string CreatedBy { get; set;}
        public string CreatedDt { get; set;}
        public string ModifiedBy { get; set;}
        public string ModifiedDt { get; set;}
        public IEnumerable<SupplierQuotationDet> supplierQuotationDets { get; set;}
    }

    internal class SupplierQuotationDet
    {
        public string SuppQuotNo { get; set; }
        public string PartNo { get; set; }
        public decimal Qty { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Discount { get; set;}
        public decimal UnitPrice { get; set;}
        public string CreatedBy { get; set; }
        public string CreatedDt { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDt { get; set; }
    }


    internal class TempSupplierQuotationModel
    {
        public bool IsCheck { get; set; }
        public string PartNo { get; set; }
        public string SuppName { get; set; }
        public string SuppID { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SupplierQuotation { get; set; }
    }
}
