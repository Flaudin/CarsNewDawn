using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class BrandModel
    {
        public string uniqueid { get; set; } = String.Empty;
        public string BrandID { get; set; } = String.Empty;
        public string BrandName { get; set; } = String.Empty;
        public int BrandType { get; set; }
        public string CreatedBy { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;
    }
}
