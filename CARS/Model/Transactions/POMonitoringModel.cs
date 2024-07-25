using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class POMonitoringModel
    {
        public string OrderNo { get; set; }
        public string SupplierName { get; set; }
        public string CreatedDt { get; set; }
        public string CreatedBy { get; set; }

        public string SuppId { get; set; }

        public IEnumerable<OrderDetModel> poMonitoringDet { get; set; }
    }

    internal class OrderDetModel
    {
        public string CtrlNo {  get; set; }
        public string PartNo { get; set; }
        public decimal OrderQty { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
