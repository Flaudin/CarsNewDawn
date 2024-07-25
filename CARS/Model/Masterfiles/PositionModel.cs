using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class PositionModel
    {
        public string PosID { get; set; } = String.Empty;
        public string PosName { get; set; } = String.Empty;
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = String.Empty;
    }
}
