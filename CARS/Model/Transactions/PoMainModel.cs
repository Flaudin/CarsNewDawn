using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Transactions
{
    internal class PoMainModel
    {
        public string PoNo {  get; set; } = string.Empty;
        public string PoDt { get; set; } = string.Empty;
        public string SuppId { get; set; } = string.Empty;
        public string TermId { get; set; } = string.Empty;
        public int PoMainStatus { get; set; }
        public int PoType { get; set;}
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedDt { get; set; } = string.Empty;
        public string ModifiedBy { get; set; } = string.Empty;
        public string ModifiedDt { get; set; } = string.Empty;
    }
}
