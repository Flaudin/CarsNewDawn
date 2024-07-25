using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class PoOrderConsolidationModel
    {
        public IEnumerable<OrderList> orders { get; set; }
        public IEnumerable<OrderBreakdown> orderBreakdowns { get; set; }
        public IEnumerable<Consolidations> consolidations { get; set; }
        public IEnumerable<Supplier> suppliers { get; set; }
    }

    internal class OrderList
    {
        public string ControlNo { get; set; }
        public int TotalQty { get; set; }
        public string Date { get; set; }
    }

    internal class OrderBreakdown
    {
        public string User { get; set;}
        public string Date { get; set;}
        public string ControlNo { get; set;}
        public string PartNo { get; set;}
        public int Qty { get; set;}
    }

    internal class Consolidations
    {
        public string PartNo { get; set; }
        public string Brand { get; set;}
        public string Description { get; set;}
    }

    internal class Supplier
    {
        public string User { get; set; }
        public string Date { get; set; }
        public string ControlNo { get; set; }
        public string PartNo { get; set; }
        public int Qty { get; set; }
    }

    internal class OrderConsolidate
    {
        public string OrderNo { get; set; }
        public string SupplierID { get; set; }
        public decimal Status { get; set; }
        public string CreadtedBy { get; set; }
        public string CreadtedDt { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDt { get; set;}
        public IEnumerable<OrderDetails> orderDetails { get; set; }
    }

    internal class OrderDetails
    {
        public string OrderNo { get; set; }
        public string PartNo { get; set; }
        public string SupplierID { get; set; }
        public decimal Status { get; set;}
        public decimal OrdrQty { get; set; }
        public decimal UnitPrice { get; set;}
        public string CreatedBy { get; set; }
        public string CreatedDt { get; set;}
        public string ModifiedBy { get; set;}
        public string ModifiedDt { get; set;}
    }
}
