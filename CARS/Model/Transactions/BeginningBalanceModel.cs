using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class BeginningBalanceModel
    {
        public string BegBalNo { get; set; } = String.Empty;
        public string Remarks { get; set; } = String.Empty;
        public decimal Status { get; set;}
        public string CreatedBy { get; set; } = String.Empty;
        public IEnumerable<BeginningBalanceDetail> DetailsList { get; set; } = null;
    }

    internal class BeginningBalanceDetail
    {
        public string UniqueID { get; set; } = String.Empty;
        public string PartNo { get; set; } = String.Empty;
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public IEnumerable<BeginningBalanceLocation> LocationsList { get; set; } = null;
    }

    internal class BeginningBalanceLocation
    {
        //public string ParentID { get; set; } = String.Empty;
        //public string PartNo { get; set; } = String.Empty;
        public string WhID { get; set; } = String.Empty;
        public string BinID { get; set; } = String.Empty;
        public decimal Qty { get; set; }
    }

    internal class BeginningBalancePrint
    {
        public string PartNo { get; set; } = String.Empty;
        public string WhName { get; set; } = String.Empty;
        public string BinName { get; set; } = String.Empty;
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
