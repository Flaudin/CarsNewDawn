using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class PoDetailsModel
    {
        public string PoNo { get; set; } = string.Empty;
        public string PartNo { get; set; } = string.Empty;
        public int DiscPrcnt { get; set; }
        public int DiscAmt { get; set;}
        public int UnitPrice { get; set;}
        public int NetPrice { get; set;}
        public int Qty { get; set;}
        public int TotalAmt { get; set;}
        public int DelivrdQty { get; set;}
        public int PoDetStatus { get; set;}
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedDt { get; set; } = string.Empty;
        public string ModifiedBy { get; set; } = string.Empty;
        public string ModifiedDt { get; set; } = string.Empty;
    }
}
