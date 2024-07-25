using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class AppPartsModel
    {
        public string PartNo { get; set; } = String.Empty;
        public string DescName { get; set; } = String.Empty;
        public string BrandName { get; set; } = String.Empty;
        public string BrandCode { get; set; } = String.Empty;
        public string UomName { get; set; } = String.Empty;
        public string PartPosition { get; set; } = String.Empty;
        public string PartSize { get; set; } = String.Empty;
        public string Application { get; set; } = String.Empty;
        public int InnerUnitPack { get; set; }
        public string Type { get; set; } = String.Empty;
        public int MotherUnitPack { get; set; }
        public string DateAdded { get; set; }
        public string ModifiedDt { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<AppAlternateModel> AppAlternateList { get; set; } = null;
        public IEnumerable<AppOemModel> AppOemList { get; set; } = null;
    }

    internal class AppAlternateModel
    {
        public string PartNo { get; set; } = String.Empty;
        public string AltPartNo { get; set; } = String.Empty;
    }

    internal class AppOemModel
    {
        public string PartNo { get; set; } = String.Empty;
        public string OEMNo { get; set; } = String.Empty;
    }

    internal class PartsSynch
    {

    }
}
