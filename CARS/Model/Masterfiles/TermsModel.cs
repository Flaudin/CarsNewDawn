using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class TermsModel
    {
        public string uniqueid { get; set; } = String.Empty;
        public string TermID { get; set; } = String.Empty;
        public string TermName { get; set; } = String.Empty;
        public decimal TermDays { get; set; }
        public string term_code2   { get; set; } = String.Empty;
        public string CreatedBy   { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;
    }
}
