using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class CustomerModel
    {
        public string SLID { get; set; } = String.Empty;
        public string SLName { get; set; } = String.Empty;
        public string RegName { get; set; } = String.Empty;
        public string TinNo { get; set; } = String.Empty;
        public string BusinessName { get; set; } = String.Empty;
        public string FlrNo { get; set; } = String.Empty;
        public string Brgy { get; set; } = String.Empty;
        public decimal VATType { get; set; }
        public string NoStreet { get; set; } = String.Empty;
        public string CityID { get; set; } = String.Empty;
        public string ProvID { get; set; } = String.Empty;
        public string RegionID { get; set; } = String.Empty;
        public string Zip { get; set; } = String.Empty;
        public string TelNo { get; set; } = String.Empty;
        public string CelNo { get; set; } = String.Empty;
        public string Website { get; set; } = String.Empty;
        public string EmailAdd { get; set; } = String.Empty;
        public string SLType { get; set; }
        public string SupplierType { get; set; }
        public string TermID { get; set; } = String.Empty;
        public bool IsActive { get; set; } = false;

        public IEnumerable<CustomerContactModel> ContactList { get; set; }
    }

    internal class CustomerContactModel
    {
        public string SLID { get; set; } = String.Empty;
        public string UniqueID { get; set; } = String.Empty;
        public string ContactPerson { get; set; } = String.Empty;
        public string ContactNo { get; set; } = String.Empty;
        public string EmailAdd { get; set; } = String.Empty;
        public string Remarks { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;
    }
}
