using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class PurchaseReturnModel
    {
        public PurchaseReturnMain purchaseReturnMains { get; set; }
        public IEnumerable<PurchaseReturnDet> purchaseReturnDets { get; set; }
        public IEnumerable<PurchaseReturnDetLoc> purchaseReturnDetLocs { get; set; }
    }

    internal class PurchaseReturnMain
    {
        public string PurchRetNo { get; set; }
        public string RRNo { get; set; }
        public string SupplierID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDt { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string ReasonID { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDt { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDt { get; set; }
    }

    internal class PurchaseReturnDet
    {
        public string PurchRetNo { get; set; }
        public string PartNo { get; set; }
        public decimal Qty { get; set; }
        public string Status { get; set; }
        public string FreeItem { get; set; }
        public string ReasonID { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDt { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDt { get; set; }
    }

    internal class PurchaseReturnDetLoc
    {
        public string PurchRetNo { get; set; }
        public string PartNo { get; set; }
        public decimal Qty { get; set; }
        public string LotNo { get; set; }
        public string WhID { get; set; }
        public string BinID { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDt { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDt { get; set; }
    }

    internal class PurhcaseReturnLocatorGetter
    {
        public string PartNo { get; set; }
        public string LotNo { get; set; }
        public string BinID { get; set; }
        public string WhID { get; set; }
        public decimal Qty { get; set; }
        public string WhPriority { get; set; }
        public decimal Boh { get; set; }
        public decimal RcvdQty { get; set; }
    }
}
