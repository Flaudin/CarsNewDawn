using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class RegionModel
    {
        public string uniqueid { get; set; } = String.Empty;
        public string RegionID { get; set; } = String.Empty;
        public string RegionName { get; set; } = String.Empty;
        public string CreatedBy { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;
    }
}
