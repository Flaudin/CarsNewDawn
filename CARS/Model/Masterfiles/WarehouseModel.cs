using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class WarehouseModel
    {
        public string WhID { get; set; } = String.Empty;
        public string WhName { get; set; } = String.Empty;
        public string WhDesc { get; set; } = String.Empty;
        public string WhLocation { get; set; } = String.Empty;
        public decimal AreaSqm { get; set; }
        public decimal StorageSqm { get; set; }
        public decimal WhPriority { get; set; }
        public string WhInCharge { get; set; } = String.Empty;
        public string CreatedBy { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsWebStore { get; set; } = false;
    }
}
