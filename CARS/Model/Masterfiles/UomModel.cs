using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class UomModel
    {
        public string uniqueid { get; set; } = String.Empty;
        public string UomID { get; set; } = String.Empty;
        public string UomName { get; set; } = String.Empty;
        public string CreatedBy { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;
    }
}
