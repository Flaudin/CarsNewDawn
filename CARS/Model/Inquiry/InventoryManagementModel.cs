using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Inquiry
{
    internal class InventoryManagementModel
    {
        public string PartNo { get; set; } = String.Empty;
        public string Brand { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Uom { get; set; } = String.Empty;
        public string PartName { get; set; } = String.Empty;
        public string Sku { get; set; } = String.Empty;
        public string OtherName { get; set; } = String.Empty;
        public decimal ListPrice { get; set; }
        public decimal SGO { get; set; }
        public decimal ReorderPoint { get; set; }
        public decimal LeadTime { get; set; }
        public bool ApplyAll { get; set; } = false;
    }
}
