using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class SalesOrderModel
    {
        public string SoNo { get; set; } = String.Empty;
        public string SoDate { get; set; } = String.Empty;
        public string InvoiceRefNo { get; set; } = String.Empty;
        public bool CashTran { get; set; }
        public string CustName { get; set; } = String.Empty;
        public string CustAdd { get; set; } = String.Empty;
        public string CustTin { get; set; } = String.Empty;
        public string SLID { get; set; } = String.Empty;
        public string TermID { get; set; } = String.Empty;
        public string SalesmanID { get; set; } = String.Empty;
        public string Remarks { get; set; } = String.Empty;
        public decimal Status { get; set; }
        public string CreatedBy { get; set; } = String.Empty;
        public IEnumerable<SalesOrderDetailModel> DetailsList { get; set; } = null;
    }

    internal class SalesOrderDetailModel
    {
        public string SoNo { get; set; } = String.Empty;
        public string ItemID { get; set; } = String.Empty;
        public string ItemNo { get; set; } = String.Empty;
        public string PartNo { get; set; } = String.Empty;
        public string PartName { get; set; } = String.Empty;
        public string DescName { get; set; } = String.Empty;
        public string BrandName { get; set; } = String.Empty;
        public string Sku { get; set; } = String.Empty;
        public string UomName { get; set; } = String.Empty;
        public decimal Qty { get; set; }
        public double ListPrice { get; set; }
        public double Discount { get; set; }
        public decimal NetPrice { get; set; }
        public decimal VATAmt { get; set; }
        public bool FreeItem { get; set; }
        public string FreeReason { get; set; } = String.Empty;
        public string FreeAppBy { get; set; } = String.Empty;
        public bool AllowBelCost { get; set; }
        public string SLID { get; set; } = String.Empty;
        public decimal Status { get; set; }
        public IEnumerable<SalesOrderDetailLocationModel> LocationList { get; set; } = null;
    }

    internal class SalesOrderDetailLocationModel
    {
        public decimal Qty { get; set; }
        public string LotNo { get; set; } = String.Empty;
        public string WhID { get; set; } = String.Empty;
        public string BinID { get; set; } = String.Empty;
    }
}
