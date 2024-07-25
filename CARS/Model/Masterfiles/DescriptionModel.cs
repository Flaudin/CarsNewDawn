using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class DescriptionModel
    {
        public string DescID { get; set; } = String.Empty;
        public string DescName { get; set; } = String.Empty;
        public string CreatedBy { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;
    }
}
