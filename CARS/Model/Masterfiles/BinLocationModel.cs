using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class BinLocationModel
    {
        public string BinID { get; set; } = String.Empty;
        public string BinName { get; set; } = String.Empty;
        public string WhID { get; set; } = String.Empty;
        public string BinDesc { get; set; } = String.Empty;
        public decimal BinLength { get; set; }
        public decimal BinHeigth { get; set; }
        public decimal BinWidth { get; set; }
        public decimal BinArea { get; set; }
        public string CreatedBy { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;
    }
}
