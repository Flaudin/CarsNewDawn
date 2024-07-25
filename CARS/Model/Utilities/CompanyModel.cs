using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Utilities
{
    internal class CompanyModel
    {
        public string CompName { get; set; } = string.Empty;
        public string RegName { get; set; } = string.Empty ;
        public string TinNo { get; set; } = string.Empty;
        public string NoStreet { get; set; } = string.Empty;
        public string CityID { get; set; } = string.Empty;
        public string ProvID { get; set; } = string.Empty;
        public string RegionID { get; set; } = string.Empty;
        public string CellNo { get; set; } = string.Empty;
        public string TelNo { get; set; } = string.Empty;
        public string Web { get; set; } = string.Empty;
        public string EmailAdd { get; set; } = string.Empty;
        public decimal VatType { get; set; } 
        public int CustomerId { get; set; }
        public int CompanyId { get; set; }
        public string CompLogo { get; set; }
    }
}
