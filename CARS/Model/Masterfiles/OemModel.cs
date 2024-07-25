using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class OemModel
    {
        public string UniqueID { get; set; } = String.Empty;
        public string OemNo { get; set; } = String.Empty;
        public string MakeID { get; set; } = String.Empty;
        public string CreatedBy { get; set; } = String.Empty;
        public bool BOwn{ get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<OemPartModel> DetailsList { get; set; } = null;
    }

    internal class OemPartModel
    {
        public string ParentID { get; set; } = String.Empty;
        public string PartNo { get; set; } = String.Empty;
        public bool BOwn { get; set; }
        public bool IsActive { get; set; }
        public bool IsNew { get; set; }
    }
}
