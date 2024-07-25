using CARS.Model.Reports;
using CARS.Model.Transactions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Controller.Transactions
{
    internal class SalesOrderReportController
    {
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;
        private static SqlTransaction transaction = null;

        
    }
}
