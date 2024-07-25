using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class CityModel
    {
        public string uniqueid { get; set; } = String.Empty;
        public string CityID { get; set; } = String.Empty;
        public string CityName { get; set; } = String.Empty;
        public string zip_code { get; set; } = String.Empty;
        public string ProvID { get; set; } = String.Empty;
        public bool with_gma { get; set; } = false;
        public string CreatedBy { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;
    }
}
