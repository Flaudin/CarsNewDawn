using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class SalesReturnModel
    {
        public string SRNo { get; set; } = string.Empty;
        public string SRDate { get; set; } = string.Empty;
        public string InvoiceNo { get; set; } = string.Empty;

        public string SONo { get; set; } = string.Empty;
        public string SLID { get; set; } = string.Empty;
        public string SalesmanID { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public int Status { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public IEnumerable<SalesReturnDetailModel> ReturnDetails { get; set; } = null;
    }

    internal class SalesReturnDetailModel
    {
        public string ItemID { get; set; } = string.Empty;
        public string PartNo { get; set; } = string.Empty;
        public decimal GoodQty { get; set; }
        public decimal DefectiveQty { get; set; }
        public bool FreeItem { get; set; } = false;
        public string ItemNo { get; set; } = string.Empty;
        public int Status { get; set; }
        public string LotNo { get; set; } = String.Empty;
        public string WhID { get; set; } = String.Empty;
        public string BinID { get; set; } = String.Empty;
        public string ReasonID { get; set; } = String.Empty;
    }
}
