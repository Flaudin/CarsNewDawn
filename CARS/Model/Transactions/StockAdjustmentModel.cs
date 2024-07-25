using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class StockAdjustmentModel
    {
        public string AdjNo { get; set; } = String.Empty;
        public string ReasonID { get; set; } = String.Empty;
        public string Remarks { get; set; } = String.Empty;
        public decimal Status { get; set; }
        public string CreatedBy { get; set; } = String.Empty;
        public IEnumerable<StockAdjustmentDetail> DetailsList { get; set; } = null;
    }

    internal class StockAdjustmentDetail
    {
        public string PartNo { get; set; } = String.Empty;
        public string ReasonID { get; set; } = String.Empty;
        public decimal TakeUpQty { get; set; }
        public decimal DropQty { get; set; }
        public string LotNo { get; set; } = String.Empty;
        public string WhID { get; set; } = String.Empty;
        public string BinID { get; set; } = String.Empty;
    }
}
