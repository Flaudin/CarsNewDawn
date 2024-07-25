using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model.Masterfiles
{
    internal class DepartmentModel
    {
        public string DeptID { get; set; } = String.Empty;
        public string DeptName { get; set; } = String.Empty;
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = String.Empty;
    }
}
