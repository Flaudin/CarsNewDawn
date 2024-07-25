using CARS.Model.Inquiry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Controller.Inquiry
{
    internal class PartsInquiryController
    {
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public DataTable PartsDataTable(string Keyword)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(a.PartNo) AS PartNo, RTRIM(a.PartName) AS PartName, RTRIM(a.OtherName) AS OtherName, " +
                                                "       RTRIM(ISNULL(d.DescName,'')) AS DescName, RTRIM(a.Sku) AS Sku, RTRIM(b.UomName) AS UomName, " +
                                                "       RTRIM(c.BrandName) AS BrandName, RTRIM(a.OemNo) AS OemNo, RTRIM(a.PartApply) AS PartApply, " +
                                                "       RTRIM(a.PSize) AS PSize, RTRIM(a.PType) AS PType, a.ListPrice, a.IsActive " +
                                                "   FROM TblPartsMainMF a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsUomMF b WITH(READPAST) ON b.UomID = a.UomID " +
                                                "   LEFT JOIN TblPartsBrandMF c WITH(READPAST) ON c.BrandID = a.BrandID " +
                                                "   LEFT JOIN TblPartsDescriptionMF d WITH(READPAST) ON d.DescID = a.DescID" +
                                                "   WHERE (1=(CASE WHEN ISNULL(@keyword, '') = '' THEN 1 ELSE 0 END) " +
                                                "           OR a.PartNo LIKE '%' + @keyword + '%' " +
                                                "           OR a.PartName LIKE '%' + @keyword + '%' " +
                                                "           OR a.OtherName LIKE '%' + @keyword + '%' " +
                                                "           OR c.BrandName LIKE '%' + @keyword + '%' " +
                                                "           OR b.UomName LIKE '%' + @keyword + '%' " +
                                                "           OR d.DescName LIKE '%' + @keyword + '%' " +
                                                "           OR a.Sku LIKE '%' + @keyword + '%' " +
                                                "           OR a.PartApply LIKE '%' + @keyword + '%')", connection);
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

        public DataTable PartsDataTableFilterBrand(string Keyword)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(a.PartNo) AS PartNo, RTRIM(a.PartName) AS PartName, RTRIM(a.OtherName) AS OtherName, " +
                                                "       RTRIM(ISNULL(d.DescName,'')) AS DescName, RTRIM(a.Sku) AS Sku, RTRIM(b.UomName) AS UomName, " +
                                                "       RTRIM(c.BrandName) AS BrandName, RTRIM(a.OemNo) AS OemNo, RTRIM(a.PartApply) AS PartApply, " +
                                                "       RTRIM(a.PSize) AS PSize, RTRIM(a.PType) AS PType, a.ListPrice, a.IsActive " +
                                                "   FROM TblPartsMainMF a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsUomMF b WITH(READPAST) ON b.UomID = a.UomID " +
                                                "   LEFT JOIN TblPartsBrandMF c WITH(READPAST) ON c.BrandID = a.BrandID " +
                                                "   LEFT JOIN TblPartsDescriptionMF d WITH(READPAST) ON d.DescID = a.DescID" +
                                                "   WHERE (1=(CASE WHEN ISNULL(@keyword, '') = '' THEN 1 ELSE 0 END) " +
                                                "           OR c.BrandName=@keyword)", connection);
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

        public DataTable PartsDataTableFilterDescription(string Keyword)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(a.PartNo) AS PartNo, RTRIM(a.PartName) AS PartName, RTRIM(a.OtherName) AS OtherName, " +
                                                "       RTRIM(ISNULL(d.DescName,'')) AS DescName, RTRIM(a.Sku) AS Sku, RTRIM(b.UomName) AS UomName, " +
                                                "       RTRIM(c.BrandName) AS BrandName, RTRIM(a.OemNo) AS OemNo, RTRIM(a.PartApply) AS PartApply, " +
                                                "       RTRIM(a.PSize) AS PSize, RTRIM(a.PType) AS PType, a.ListPrice, a.IsActive " +
                                                "   FROM TblPartsMainMF a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsUomMF b WITH(READPAST) ON b.UomID = a.UomID " +
                                                "   LEFT JOIN TblPartsBrandMF c WITH(READPAST) ON c.BrandID = a.BrandID " +
                                                "   LEFT JOIN TblPartsDescriptionMF d WITH(READPAST) ON d.DescID = a.DescID" +
                                                "   WHERE (1=(CASE WHEN ISNULL(@keyword, '') = '' THEN 1 ELSE 0 END) " +
                                                "           OR d.DescName=@keyword)", connection);
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

        public string GetPartImage(string id)
        {
            string img = "";
            try
            {
                connection.Open();
                command = Connection.setCommand($"SELECT ISNULL(PartImg, '') AS PartImg " +
                                                $"  FROM TblPartsMainMF WITH(READPAST) " +
                                                $"  WHERE PartNo = @PartNo", connection);
                command.Parameters.AddWithValue("@PartNo", id);
                img = command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return img;
        }

        public SortedDictionary<string, string> GetDictionary(string Type)
        {
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();

            try
            {
                connection.Open();

                if (Type == "Uom")
                {
                    command = Connection.setCommand($"SELECT UomName, UomID " +
                                                    $"  FROM TblPartsUomMF WITH(READPAST)", connection);
                }
                else if (Type == "Brand")
                {
                    command = Connection.setCommand($"SELECT BrandName, BrandID " +
                                                    $"  FROM TblPartsBrandMF WITH(READPAST)", connection);
                }
                else
                {
                    command = Connection.setCommand($"SELECT (RTRIM(DescName) + ' (' + RTRIM(DescSku) + ')') AS DescName, DescID " +
                                                    $"  FROM TblPartsDescriptionMF WITH(READPAST)", connection);
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
    }
}
