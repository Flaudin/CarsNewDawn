using CARS.Customized_Components;
using CARS.Model.Transactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Controller.Transactions
{
    internal class TransactionController
    {
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;
        private static string DistinctFilter = "ALL";

        public DataTable PartsWithBegBalDataTable(string Brand, string Description, string Keyword)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT DISTINCT RTRIM(a.PartNo) AS PartNo, RTRIM(b.PartName) AS PartName, RTRIM(b.OtherName) AS OtherName, " +
                                                "       RTRIM(c.DescName) AS DescName, RTRIM(d.BrandName) AS BrandName " +
                                                "   FROM TblInvLotLoc a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                "   LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                                                "   LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@brandID,'') = '' THEN 1 ELSE 0 END) OR d.BrandID=@brandID ) " +
                                                "       AND (1=(CASE WHEN ISNULL(@descID, '') = '' THEN 1 ELSE 0 END) OR c.DescID=@descID) " +
                                                "       AND (1=(CASE WHEN ISNULL(@keyword, '') = '' THEN 1 ELSE 0 END) " +
                                                "           OR b.PartName LIKE '%' + @keyword + '%' " +
                                                "           OR d.BrandName LIKE '%' + @keyword + '%' " +
                                                "           OR c.DescName LIKE '%' + @keyword + '%')", connection);
                command.Parameters.AddWithValue("@brandID", Brand);
                command.Parameters.AddWithValue("@descID", Description);
                command.Parameters.AddWithValue("@keyword", Keyword);
                reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public string GetPartImage(string PartNo)
        {
            string imageString = "";
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT PartImg " +
                                                "   FROM TblPartsMainMF WITH(READPAST) " +
                                                "   WHERE PartNo=@PartNo", connection);
                command.Parameters.AddWithValue("@PartNo", PartNo);
                imageString = command.ExecuteScalar().ToString() ?? "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return imageString;
        }

        public DataTable GetDictionaryTable(string searchable)
        {
            DataTable dt = new DataTable();
            if (connection.State == ConnectionState.Closed)
            {
                try
                {
                    connection.Open();
                    command = Connection.setCommand("SELECT PartNo " +
                                                    "   FROM TblPartsMainMF WITH(READPAST) " +
                                                    "   WHERE LEFT(PartNo,@count)=@PartNo", connection);
                    command.Parameters.AddWithValue("@count", searchable.TrimEnd().Length);
                    command.Parameters.AddWithValue("@PartNo", searchable.TrimEnd());
                    reader = command.ExecuteReader();
                    dt.Load(reader);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return dt;
        }

        public Dictionary<string, string> GetDynamicDictionaryParts(string PartNo)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            try
            {
                connection.Open();
                switch (GetDistinctFilter())
                {
                    case "ALL":
                        command = Connection.setCommand($"SELECT TOP 100 RTRIM(ISNULL(a.PartNo,'')) + ', ' + RTRIM(ISNULL(a.OtherName,'')) + " +
                                                        $"      CASE WHEN ISNULL(a.OtherName,'') != '' THEN ', ' ELSE '' END + RTRIM(ISNULL(a.Sku, '')) + " +
                                                        $"      CASE WHEN ISNULL(a.Sku, '') != '' THEN ', ' ELSE '' END + RTRIM(ISNULL(b.DescName, '')) + ', ' + " +
                                                        $"      RTRIM(ISNULL(c.BrandName, '')) + ', ' + RTRIM(ISNULL(d.UomName, '')), RTRIM(ISNULL(a.PartNo, ''))  " +
                                                        $"  FROM TblPartsMainMF a WITH(READPAST) " +
                                                        $"  LEFT JOIN TblPartsDescriptionMF b WITH(READPAST) ON b.DescID = a.DescID " +
                                                        $"  LEFT JOIN TblPartsBrandMF c WITH(READPAST) ON c.BrandID = a.BrandID " +
                                                        $"  LEFT JOIN TblPartsUomMF d WITH(READPAST) ON d.UomID= a.UomID " +
                                                        $"  WHERE REPLACE(a.PartNo, '-', '') LIKE '%'+@PartNo+'%' " +
                                                        $"      OR REPLACE(a.OtherName, '-', '') LIKE '%'+@PartNo+'%' " +
                                                        $"      OR REPLACE(a.Sku, '-', '') LIKE '%'+@PartNo+'%' " +
                                                        $"      OR REPLACE(b.DescName, '-', '') LIKE '%'+@PartNo+'%' " +
                                                        $"      OR REPLACE(c.BrandName, '-', '') LIKE '%'+@PartNo+'%'" +
                                                        $"      OR REPLACE(d.UomName, '-', '') LIKE '%'+@PartNo+'%'", connection);
                        command.Parameters.AddWithValue("@PartNo", PartNo.ToUpper());
                        break;

                    case "BRAND":
                        command = Connection.setCommand($"SELECT DISTINCT RTRIM(b.BrandName), RTRIM(ISNULL(a.BrandID, '')) " +
                                                        $"  FROM TblPartsMainMF a WITH(READPAST) " +
                                                        $"  LEFT JOIN TblPartsBrandMF b WITH(READPAST) ON b.BrandID = a.BrandID " +
                                                        $"  WHERE REPLACE(b.BrandName, '-', '') LIKE '%'+@BrandName+'%'", connection);
                        command.Parameters.AddWithValue("@BrandName", PartNo.ToUpper());
                        break;

                    case "DESCRIPTION":
                        command = Connection.setCommand($"SELECT DISTINCT RTRIM(b.DescName), RTRIM(ISNULL(a.DescID, '')) " +
                                                        $"  FROM TblPartsMainMF a WITH(READPAST) " +
                                                        $"  LEFT JOIN TblPartsDescriptionMF b WITH(READPAST) ON b.DescID = a.DescID " +
                                                        $"  WHERE b.DescName IS NOT NULL" +
                                                        $"      AND REPLACE(b.DescName, '-', '') LIKE '%'+@DescName+'%'", connection);
                        command.Parameters.AddWithValue("@DescName", PartNo.ToUpper());
                        break;
                }
                reader = command.ExecuteReader();

                dictionary.Add("", "");
                while (reader.Read())
                {
                    string dictionaryKey = reader.GetString(0);
                    string dictionaryValue = reader.GetString(1);

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

        public Dictionary<string, string> GetDynamicDictionaryPartsWithBalance(string PartNo)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            try
            {
                connection.Open();
                switch(GetDistinctFilter())
                {
                    case "ALL":
                        command = Connection.setCommand($"SELECT DISTINCT TOP 100 RTRIM(ISNULL(a.PartNo,'')) + ', ' + RTRIM(ISNULL(b.OtherName,'')) +  " +
                                                        $"      CASE WHEN ISNULL(b.OtherName, '') != '' THEN ', ' ELSE '' END + RTRIM(ISNULL(b.Sku, '')) + " +
                                                        $"      CASE WHEN ISNULL(b.Sku, '') != '' THEN ', ' ELSE '' END + RTRIM(ISNULL(c.DescName, '')) + ', ' + " +
                                                        $"      RTRIM(ISNULL(d.BrandName, '')) + ', ' + RTRIM(ISNULL(e.UomName, '')), RTRIM(ISNULL(a.PartNo, '')) " +
                                                        $"  FROM TblInvLot a WITH(READPAST) " +
                                                        $"  LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                        $"  LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                                                        $"  LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                                                        $"  LEFT JOIN TblPartsUomMF e WITH(READPAST) ON e.UomID = b.UomID " +
                                                        $"  WHERE REPLACE(a.PartNo, '-', '') LIKE '%'+@PartNo+'%' " +
                                                        $"      OR REPLACE(b.OtherName, '-', '') LIKE '%'+@PartNo+'%' " +
                                                        $"      OR REPLACE(b.Sku, '-', '') LIKE '%'+@PartNo+'%' " +
                                                        $"      OR REPLACE(c.DescName, '-', '') LIKE '%'+@PartNo+'%' " +
                                                        $"      OR REPLACE(d.BrandName, '-', '') LIKE '%'+@PartNo+'%'" +
                                                        $"      OR REPLACE(e.UomName, '-', '') LIKE '%'+@PartNo+'%'", connection);
                        command.Parameters.AddWithValue("@PartNo", PartNo.ToUpper());
                        break;

                    case "BRAND":
                        command = Connection.setCommand($"SELECT DISTINCT RTRIM(c.BrandName), RTRIM(ISNULL(b.BrandID, '')) " +
                                                        $"  FROM TblInvLot a WITH(READPAST) " +
                                                        $"  LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                        $"  LEFT JOIN TblPartsBrandMF c WITH(READPAST) ON c.BrandID = b.BrandID " +
                                                        $"  WHERE REPLACE(c.BrandName, '-', '') LIKE '%'+@BrandName+'%'", connection);
                        command.Parameters.AddWithValue("@BrandName", PartNo.ToUpper());
                        break;

                    case "DESCRIPTION":
                        command = Connection.setCommand($"SELECT DISTINCT RTRIM(c.DescName), RTRIM(ISNULL(b.DescID, '')) " +
                                                        $"  FROM TblInvLot a WITH(READPAST) " +
                                                        $"  LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                        $"  LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                                                        $"  WHERE c.DescName IS NOT NULL " +
                                                        $"      AND REPLACE(c.DescName, '-', '') LIKE '%'+@DescName+'%'", connection);
                        command.Parameters.AddWithValue("@DescName", PartNo.ToUpper());
                        break;
                }
                reader = command.ExecuteReader();

                dictionary.Add("", "");
                while (reader.Read())
                {
                    string dictionaryKey = reader.GetString(0);
                    string dictionaryValue = reader.GetString(1);

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

        public void ChangeDistinctFilter(string Filter)
        {
            DistinctFilter = Filter;
        }

        public string GetDistinctFilter()
        {
            return DistinctFilter;
        }

        public SortedDictionary<string, string> GetDictionary(string Type)
        {
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();

            try
            {
                connection.Open();

                switch (Type)
                {
                    case "PartNo":
                        command = Connection.setCommand($"SELECT PartNo, PartNo " +
                                                        $"  FROM TblPartsMainMF WITH(READPAST)", connection);
                        break;

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
