using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Inquiry
{
    internal class SalesOrderArchiveModel
    {
        public string SONo { get; set; } = string.Empty;
        public IEnumerable<SalesOrderArchivParts> DetailsList { get; set; } = null;
    }

    internal class SalesOrderArchivParts
    {
        public string PartNo { get; set; } = string.Empty;
        public string ItemID { get; set; } = string.Empty;
        public decimal Qty { get; set; }
    }
}
