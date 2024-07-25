using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Inquiry
{
    internal class PartsInquiryModel
    {
        public string PartNo { get; set; } = String.Empty;
        public string Brand { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Uom { get; set; } = String.Empty;
        public string PartName { get; set; } = String.Empty;
        public string Sku { get; set; } = String.Empty;
        public string OtherName { get; set; } = String.Empty;
        public string PartApplication { get; set; } = String.Empty;
        public string Oem { get; set; } = String.Empty;
        public string Remarks { get; set; } = String.Empty;
        public string BPartNo { get; set; } = "0";
        public decimal ListPrice { get; set; }
        public string PPosition { get; set; } = String.Empty;
        public string PSize { get; set; } = String.Empty;
        public string Ptype { get; set; } = String.Empty;
        public decimal BListPrice { get; set; }
        public decimal IUpack { get; set; }
        public decimal MUpack { get; set; }
        public string Image { get; set; } = "";
        public string CreatedBy { get; set; } = String.Empty;
        public bool IsActive { get; set; }
        public string Keyword { get; set; } = String.Empty;
    }
}
