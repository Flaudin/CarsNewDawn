using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class PromoModel
    {

    }

    internal class PromoPartDetail
    {
        public string PartNo { get; set; } = String.Empty;
        public string PartName { get; set; } = String.Empty;
        public string DescName { get; set; } = String.Empty;
        public string BrandName { get; set; } = String.Empty;
        public string Sku { get; set; } = String.Empty;
        public string UomName { get; set; } = String.Empty;
        public decimal Qty { get; set; }
        public double ListPrice { get; set; }
    }
}
