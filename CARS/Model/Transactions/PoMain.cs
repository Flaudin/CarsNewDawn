using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class PoMain
    {
        public string PONo {  get; set; }
        public string SupplierID { get; set; }
        public string TermID { get; set;}
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDt { get; set; }
        public string UpdatedBy { get; set;}
        public string UpdatedDt { get; set;}
    }

    internal class PoDetMain
    {
        public string PONo { get; set; }
        public string PartNo { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal NetPrice { get; set; }
        public string Status { get; set;}
        public decimal DeliveredQty {  get; set; }
        public string CreatedBy { get; set;}
        public string CreatedDt { get; set;}
        public string ModifiedBy { get; set;}
        public string ModifiedDt { get; set;}    
    }
}
