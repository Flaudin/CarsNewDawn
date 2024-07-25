using CARS.Model.Masterfiles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Controller.Masterfiles
{
    internal class PromoController
    {
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public PromoPartDetail GetSuggestedPart(string PartNo)
        {
            PromoPartDetail partDetail = new PromoPartDetail();
            try
            {
                connection.Open();
                command = Connection.setCommand($"SELECT a.PartNo, a.PartName, b.DescName, d.BrandName, a.Sku, c.UomName, a.ListPrice " +
                                                $"  FROM TblPartsMainMF a WITH(READPAST) " +
                                                $"  LEFT JOIN TblPartsDescriptionMF b WITH(READPAST) ON b.DescID = a.DescID " +
                                                $"  LEFT JOIN TblPartsUomMF c WITH(READPAST) ON c.UomID = a.UomID " +
                                                $"  LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = a.BrandID " +
                                                $"  WHERE a.PartNo=@PartNo", connection);
                command.Parameters.AddWithValue("@PartNo", PartNo);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    partDetail = new PromoPartDetail
                    {
                        PartNo = reader.GetString(0).TrimEnd(),
                        PartName = reader.GetString(1).TrimEnd(),
                        DescName = reader.GetString(2).TrimEnd(),
                        BrandName = reader.GetString(3).TrimEnd(),
                        Sku = reader.GetString(4).TrimEnd(),
                        UomName = reader.GetString(5).TrimEnd(),
                        ListPrice = Convert.ToDouble(reader.GetDecimal(6)),
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return partDetail;
        }

        public DataTable PartsWithBegBalDataTable(string Keyword)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT DISTINCT RTRIM(a.PartNo) AS PartNoFilter, RTRIM(b.PartName) AS PartNameFilter, RTRIM(b.OtherName) AS OtherNameFilter, " +
                                                "       RTRIM(c.DescName) AS DescNameFilter, RTRIM(d.BrandName) AS BrandNameFilter, RTRIM(b.Sku) AS SkuFilter, " +
                                                "       RTRIM(e.UomName) AS UomNameFilter, b.ListPrice AS ListPriceFilter" +
                                                "   FROM TblInvLotLoc a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                "   LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                                                "   LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                                                "   LEFT JOIN TblPartsUomMF e WITH(READPAST) ON e.UomID = b.UomID " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@keyword, '') = '' THEN 1 ELSE 0 END) " +
                                                "           OR a.PartNo LIKE '%' + @keyword + '%' " +
                                                "           OR b.PartName LIKE '%' + @keyword + '%' " +
                                                "           OR b.OtherName LIKE '%' + @keyword + '%' " +
                                                "           OR d.BrandName LIKE '%' + @keyword + '%' " +
                                                "           OR c.DescName LIKE '%' + @keyword + '%' " +
                                                "           OR b.Sku LIKE '%' + @keyword + '%' " +
                                                "           OR e.UomName LIKE '%' + @keyword + '%' )", connection);
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

        public DataTable PartsWithBegBalDataTableFilterBrand(string Keyword)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT DISTINCT RTRIM(a.PartNo) AS PartNoFilter, RTRIM(b.PartName) AS PartNameFilter, RTRIM(b.OtherName) AS OtherNameFilter, " +
                                                "       RTRIM(c.DescName) AS DescNameFilter, RTRIM(d.BrandName) AS BrandNameFilter, RTRIM(b.Sku) AS SkuFilter, " +
                                                "       RTRIM(e.UomName) AS UomNameFilter, b.ListPrice AS ListPriceFilter" +
                                                "   FROM TblInvLotLoc a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                "   LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                                                "   LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                                                "   LEFT JOIN TblPartsUomMF e WITH(READPAST) ON e.UomID = b.UomID " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@keyword, '') = '' THEN 1 ELSE 0 END) " +
                                                "           OR d.BrandName=@keyword)", connection);
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

        public DataTable PartsWithBegBalDataTableFilterDesc(string Keyword)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT DISTINCT RTRIM(a.PartNo) AS PartNoFilter, RTRIM(b.PartName) AS PartNameFilter, RTRIM(b.OtherName) AS OtherNameFilter, " +
                                                "       RTRIM(c.DescName) AS DescNameFilter, RTRIM(d.BrandName) AS BrandNameFilter, RTRIM(b.Sku) AS SkuFilter, " +
                                                "       RTRIM(e.UomName) AS UomNameFilter, b.ListPrice AS ListPriceFilter" +
                                                "   FROM TblInvLotLoc a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                "   LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                                                "   LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                                                "   LEFT JOIN TblPartsUomMF e WITH(READPAST) ON e.UomID = b.UomID " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@keyword, '') = '' THEN 1 ELSE 0 END) " +
                                                "           OR c.DescName=@keyword)", connection);
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
    }
}
