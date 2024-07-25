using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class PoOrderTaking
    {
        public string CtrlNo { get; set; }
        public string Remarks { get; set;}
        public decimal Status {  get; set; }
        public IEnumerable<OrderTakingDet> Orderlist { get; set; }
       
    }

    internal class OrderTakingDet
    {
        public string CtrlNo { get; set; }
        public string PartNo { get; set; }
        public decimal Qty { get; set; }
        public string ItmRemarks { get; set;}
        public string PONo { get; set; }
        public decimal Status { get; set; }
    }
}
