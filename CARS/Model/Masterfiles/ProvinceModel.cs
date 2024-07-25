using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class ProvinceModel
    {
        public string uniqueid { get; set; } = String.Empty;
        public string region_code { get; set; } = String.Empty;
        public string ProvID { get; set; } = String.Empty;
        public string ProvName { get; set; } = String.Empty;
        public string RegionID { get; set; } = String.Empty;
        public string CreatedBy { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;

        public string selectedCity {  get; set; } = String.Empty;
        public string selectedRegion { get; set; } 
    }
}
