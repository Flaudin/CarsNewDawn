using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Controller.Inquiry
{
    internal class InquiryController
    {
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;
        private static string DistinctFilter = "ALL";

        public void ChangeDistinctFilter(string Filter)
        {
            DistinctFilter = Filter;
        }

        public string GetDistinctFilter()
        {
            return DistinctFilter;
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
                                                         //$"  WHERE REPLACE(a.PartNo, '-', '') LIKE '%'+@PartNo+'%' " +
                                                         //$"      OR REPLACE(a.OtherName, '-', '') LIKE '%'+@PartNo+'%' " +
                                                         //$"      OR REPLACE(a.Sku, '-', '') LIKE '%'+@PartNo+'%' " +
                                                         //$"      OR REPLACE(b.DescName, '-', '') LIKE '%'+@PartNo+'%' " +
                                                         //$"      OR REPLACE(c.BrandName, '-', '') LIKE '%'+@PartNo+'%'" +
                                                         //$"      OR REPLACE(d.UomName, '-', '') LIKE '%'+@PartNo+'%'", connection);
                                                        $"  WHERE REPLACE(a.PartNo, '-', '') LIKE '%'+@PartNo+'%' " +
                                                        $"      OR REPLACE(b.DescName, '-', '') LIKE '%'+@PartNo+'%' " +
                                                        $"      OR REPLACE(c.BrandName, '-', '') LIKE '%'+@PartNo+'%'", connection);
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
    }
}
