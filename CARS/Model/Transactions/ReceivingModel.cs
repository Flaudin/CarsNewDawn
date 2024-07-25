using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class ReceivingModel
    {
        public string RRNo { get; set; }
        public string PONo { get; set; }
        public string SupplierID { get; set; }
        public string TermId { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDt { get; set; }
        public string DRNo { get; set; }
        public string ReceivedBy { get; set; }
        public string Remarks { get; set; }
        public decimal Status { get; set; }
        public string ReasonId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDt { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDt { get; set; }
        public int RushOrder { get; set; }

        public IEnumerable<ReceivingDet> receivingDets { get; set; } = null;
        
    }

        internal class ReceivingMF
    {
        public IEnumerable<ReceivingModel> receivingModels { get; set; } = null;
        public IEnumerable<ReceivingDet> receivingItems { get; set; } = null;
        public IEnumerable<RecItemLoc> receivingLoc { get; set; } = null;
        public IEnumerable<ReceivingPO> receivingPO { get; set; } = null;
    }

        internal class ReceivingDet
         {
            public string RRNo { get; set;}
            public string PartNo { get; set;}
            public decimal Qty { get; set;}
            public decimal UnitPrice { get; set;}
            public decimal VATAmt {  get; set;}
            public decimal Status { get; set;}
            public string Freeitem { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDt { get; set;}
            public string ModifiedBy { get; set;}
            public string ModifiedDt { get; set;}
            public string PONo { get; set;}
         }

        internal class ReceivedItems
        {
            public string Sku {  get; set;}
            public string PartNo { get; set;}
            public string PartDescription { get; set;}
            public string Brand { get; set;}
            public string UOM { get; set;}
            public decimal Promo { get; set;}
            public decimal NetPrice { get; set;}
            public decimal Qty { get; set;}
            public decimal TotalPrice { get; set;}
            //public IEnumerable<RecItemLoc> Location { get; set;}
        }

        public class RecItemLoc
        {
            public bool ischecked {  get; set;}
            public string UniqueID { get; set;}
            public string PartNo { get; set;}
            public decimal Qty { get; set;}
            public string LotNo { get; set;}
            public string WhID { get; set; }
            public string WhName { get; set; }
            public string BinID { get; set; }
            public string BinName { get; set; }
            public string PONo { get; set; }
            public string ReceivedDate { get; set; }
        }

        public class POItemsForReceiving
        {
            public string PONo { get; set; }
            public string PartNo { get; set; }
             public decimal Qty { get; set; }
            public decimal QtyAlreadyInserted { get; set; }
        }

    internal class ReceivingPO
        {
            public string PartNo { get; set;}
            public decimal Qty { get; set;}
            public string  PONo {  get; set;}
        }

    public class LocationInfo
    {
        public string PartNo { get; set; }
        public string LocationBin { get; set; }
        public bool IsChecked { get; set; } 
        public decimal Qty { get; set;}
        public string PONo { get; set;}

    }

    internal class ReceivingPORec
    {
        public string PartNo { get; set;}
        public decimal Qty { get; set;}
        public string RRNo { get; set;}
        public string BinID { get; set;}
        public string WhID { get; set;}
    }
    
}
