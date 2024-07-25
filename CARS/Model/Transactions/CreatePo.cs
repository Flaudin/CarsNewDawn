using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class CreatePo
    {
        public string PONo {  get; set; }
        public string SupplierID { get; set; }
        public string TermID { get; set;}
        public decimal Status { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDt { get; set; }
        public string UpdatedBy { get; set;}
        public string UpdatedDt { get; set;}
        public IEnumerable<PoDet> poDetails { get; set; }
        public IEnumerable<PritingPO> printingPO { get; set; }
    }

    internal class ResponseModel
    { 
        public bool Success { get; set; }
        public string PoNo { get; set; }
        public string PoRef { get; set; }
        public int PoStatus { get; set; }
        public string Message { get; set; }
        public string SentDate { get; set; }
        public decimal TotalAmount { get; set; }
    }

    

    internal class PoDet
    {
        public string PONo { get; set; }
        public string PartNo { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal NetPrice { get; set; }
        public decimal Status { get; set;}
        public decimal DeliveredQty {  get; set; }
        public string CreatedBy { get; set;}
        public string CreatedDt { get; set;}
        public string ModifiedBy { get; set;}
        public string ModifiedDt { get; set;}   
        public decimal PoPrice { get; set; }
        public string UOM {  get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }

    internal class PritingPO
    {
        public string Supplier { get; set; }
        public string Date { get; set; }
        public string Pono { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set;}
    }

    internal class SendToBsb
    {
        public string CustId { get; set; }
        public string Username { get; set; }
        public string Transcode { get; set; }
        public string Area { get; set; }
        public string PoName { get; set; }
        public string NoteIfno { get; set; }
        public string Term { get; set; }
        public IEnumerable<PoDet> PoItemsList { get; set; }
    }
}
