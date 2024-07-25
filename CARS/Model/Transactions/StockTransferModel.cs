using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class StockTransferModel
    {
        public string CtrlNo { get; set; } = String.Empty;
        public decimal TransferType { get; set; }
        public string ReasonID { get; set; } = String.Empty;
        public decimal Status { get; set; }
        public string CreatedBy { get; set; } = String.Empty;
        public IEnumerable<StockTransferDetail> DetailsList { get; set; } = null;
    }

    internal class StockTransferDetail
    {
        public string PartNo { get; set; } = String.Empty;
        public string LotNo { get; set; } = String.Empty;
        public decimal Qty { get; set; }
        public string FromWhID { get; set; } = String.Empty;
        public string ToWhID { get; set; } = String.Empty;
        public string FromBinID { get; set; } = String.Empty;
        public string ToBinID { get; set; } = String.Empty;
        public decimal Status { get; set; }
        public string CtrlNo { get; set; }
        public string FromBinName { get; set; }
    }
}
