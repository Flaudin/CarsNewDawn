using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class EmployeeModel
    {
        public string EmployeeID { get; set; } = String.Empty;
        public string EmployeeName { get; set; } = String.Empty;
        public string LName { get; set; } = String.Empty;
        public string FName { get; set; } = String.Empty;
        public string MName { get; set; } = String.Empty;
        public string DateOfBirth { get; set; } = String.Empty;
        public int Gender { get; set; }
        public string DateHired { get; set; } = String.Empty;
        public int EmploymentStatus{ get; set; }
        public string DateInactive { get; set; } = String.Empty;
        public string Remarks { get; set; } = String.Empty;
        public string DeptID { get; set; } = String.Empty;
        public string PosID { get; set; } = String.Empty;
        public string BsbUsername { get; set; } = String.Empty;
        public string CreatedBy { get; set; } = String.Empty;
        public bool IsActive { get; set; } = true;
    }
}
