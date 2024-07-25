using CARS.Model.Transactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Controller.Reports
{
    internal class ReportsController
    {
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public SortedDictionary<string, string> GetDictionary(string Type)
        {
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();

            try
            {
                connection.Open();

                switch (Type)
                {
                    case "Reason":
                        command = Connection.setCommand($"SELECT ReasonName, ReasonID " +
                                                        $"  FROM TblReasonMF WITH(READPAST)", connection);
                        break;

                    case "Brand":
                        command = Connection.setCommand($"SELECT BrandName, BrandID " +
                                                        $"  FROM TblPartsBrandMF WITH(READPAST)", connection);
                        break;

                    case "Description":
                        command = Connection.setCommand($"SELECT (RTRIM(DescName) + ' (' + RTRIM(DescSku) + ')') AS DescName, DescID " +
                                                        $"  FROM TblPartsDescriptionMF WITH(READPAST)", connection);
                        break;

                    case "Customer":
                        command = Connection.setCommand($"SELECT SLName, SLID" +
                                                        $"  FROM TblSubsidiaryMain WITH(READPAST) " +
                                                        $"  WHERE SLType = 'C' " +
                                                        $"      AND IsActive = 1", connection);
                        break;
                    
                    case "Salesman":
                        command = Connection.setCommand($"SELECT RTRIM(EmployeeName) + ' (' + EmployeeID + ')' AS EmployeeName , EmployeeID " +
                                                        $"  FROM TblEmployeeMF WITH(READPAST) " +
                                                        $"  WHERE PosID = 'auUlQWtNXY' " +
                                                        $"      AND EmploymentStatus < 4", connection);
                        break;

                    case "Term":
                        command = Connection.setCommand($"SELECT TermName, TermID " +
                                                        $"  FROM TblTermsMF WITH(READPAST) " +
                                                        $"  WHERE IsActive = 1", connection);
                        break;
                }
                reader = command.ExecuteReader();

                dictionary.Add("", "");
                while (reader.Read())
                {
                    string dictionaryKey = reader.GetString(0).TrimEnd();
                    string dictionaryValue = reader.GetString(1).TrimEnd();

                    dictionary.Add(dictionaryKey, dictionaryValue);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dictionary;
        }

        //Print
        public string getCompanyImage()
        {
            string companyImage = "";
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT CompLogo FROM TblCompanyProfile", connection);
                companyImage = command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return companyImage;
        }

        public SalesOrderReportModel GetOwnerCompany()
        {
            SalesOrderReportModel company = new SalesOrderReportModel();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.CompName, RTRIM(a.NoStreet) + ' ' + RTRIM(c.CityName) + ', ' + RTRIM(b.ProvName) AS Address, a.TelNo, a.TinNo, a.CompLogo " +
                                                "   FROM TblCompanyProfile a WITH(READPAST) " +
                                                "   LEFT JOIN TblProvinceMF b WITH(READPAST) ON b.ProvID = a.ProvID " +
                                                "   LEFT JOIN TblCityMF c WITH(READPAST) ON c.CityID = a.CityID", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    company = new SalesOrderReportModel
                    {
                        CompName = reader.GetString(0).TrimEnd(),
                        Address = reader.GetString(1),
                        TelNo = reader.GetString(2),
                        TinNo = reader.GetString(3),
                        CompLogo = reader.GetString(4),
                    };
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return company;
        }
    }
}
