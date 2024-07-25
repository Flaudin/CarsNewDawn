using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class VehicleMakeModel
    {
        public string MakeID { get; set; } = String.Empty;
        public string MakeName { get; set; } = String.Empty;
        public string CreatedBy { get; set; } = String.Empty;
        public bool BOwn { get; set; }
        public bool IsActive { get; set; }
    }
}
