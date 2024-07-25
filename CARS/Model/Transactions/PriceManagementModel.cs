using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class PriceManagementModel
    {
        public IEnumerable<PartPriceModel> DetailsList { get; set; } = null;
    }

    internal class PartsPricePartModel
    {
        public string PartNo { get; set; } = String.Empty;
        public string PartName { get; set; } = String.Empty;
        public string Desc { get; set; } = String.Empty;
        public string Sku { get; set; } = String.Empty;
        public string Brand { get; set; } = String.Empty;
        public string Uom { get; set; } = String.Empty;
    }

    internal class PartPriceModel
    {
        public string PartNo { get; set; } = String.Empty;
        public decimal ListPrice { get; set; }
        public decimal Status { get; set; }
    }
}
