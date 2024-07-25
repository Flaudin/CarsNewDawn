using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class ReasonModel
    {
        public string ReasonID { get; set; } = String.Empty;
        public string ReasonName { get; set; } = String.Empty;
        public bool CreatedBy { get; set; } = false;
    }
}
