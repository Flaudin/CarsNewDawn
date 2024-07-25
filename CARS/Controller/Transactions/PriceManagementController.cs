using CARS.Functions;
using CARS.Model;
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
    internal class PriceManagementController : Universal<PriceManagementModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(PriceManagementModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                string PriceNo = GeneratePartPriceID(connection, command, transaction);
                if (entity.DetailsList != null)
                {
                    foreach (var data in entity.DetailsList)
                    {
                        int j = 0, k = 0;
                        command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT PartNo FROM TblPartPrice WITH(READPAST) WHERE ControlNo=@ControlNo AND PartNo=@PartNo) " +
                                                                   $"BEGIN " +
                                                                   $"   IF NOT EXISTS(SELECT PartNo FROM TblPartPrice WITH(READPAST) WHERE PartNo=@PartNo) " +
                                                                   $"   BEGIN " +
                                                                   $"       INSERT INTO TblPartPrice(ControlNo, PartNo, ListPrice, Status, CreatedBy, CreatedDt) " +
                                                                   $"           VALUES(@ControlNo, @PartNo, @ListPrice, @Status, @CreatedBy, GETDATE()) " +
                                                                   $"   END " +
                                                                   $"   ELSE " +
                                                                   $"   BEGIN " +
                                                                   $"       INSERT INTO TblPartPrice(ControlNo, PartNo, ListPrice, Status, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                                   $"           VALUES(@ControlNo, @PartNo, @ListPrice, @Status, @CreatedBy, " +
                                                                   $"               (SELECT TOP 1 CreatedDt FROM TblPartPrice WHERE PartNo=@PartNo), @CreatedBy, GETDATE()) " +
                                                                   $"   END " +
                                                                   $"END", connection, transaction);
                        command.Parameters.AddWithValue("@ControlNo", PriceNo);
                        command.Parameters.AddWithValue("@PartNo", data.PartNo);
                        command.Parameters.AddWithValue("@ListPrice", data.ListPrice);
                        command.Parameters.AddWithValue("@Status", 1);
                        command.Parameters.AddWithValue("@CreatedBy", Name01);
                        j = command.ExecuteNonQuery();

                        command = Connection.setTransactionCommand($"UPDATE TblPartsMainMF " +
                                                                   $"   SET ListPrice=@ListPrice, ModifiedBy=@ModifiedBy, ModifiedDt=GETDATE() " +
                                                                   $"   WHERE PartNo=@PartNo", connection, transaction);
                        command.Parameters.AddWithValue("@PartNo", data.PartNo);
                        command.Parameters.AddWithValue("@ListPrice", data.ListPrice);
                        command.Parameters.AddWithValue("@ModifiedBy", Name01);
                        k = command.ExecuteNonQuery();

                        if (j > 0 && k > 0)
                        {
                            Helper.TranLog("Price Management", "Added new price: " + data.ListPrice + " to part: " + data.PartNo + ", in control no: " + PriceNo + ".", connection, command, transaction);
                            Helper.TranLog("Parts Library", "Modified "+ data.PartNo +" list price to " + data.ListPrice + ".", connection, command, transaction);
                        }
                        else
                        {
                            message = "The part: " + data.PartNo + " is already present in the current session.";
                            transaction.Rollback();
                            connection.Close();
                            return message;
                        }
                    }
                }
                Helper.TranLog("Price Management", "Added new control no:" + PriceNo + ".", connection, command, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                message = ex.Message;
                transaction.Rollback();
                Console.WriteLine(ex.Message);
            }
            finally
            {
                transaction.Dispose();
                connection.Close();
            }
            return message;
        }

        public override void Delete(PriceManagementModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(PriceManagementModel entity)
        {
            throw new NotImplementedException();
        }

        public DataTable PartsWithBegBalDataTable(PartsPricePartModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(a.PartNo) AS PartNo, RTRIM(a.PartName) AS PartName, RTRIM(ISNULL(e.DescName,'')) AS DescName, RTRIM(a.Sku) AS Sku, " +
                                                "       RTRIM(d.UomName) AS UomName, RTRIM(c.BrandName) AS BrandName, CASE WHEN b.ListPrice IS NULL THEN a.ListPrice " +
                                                "       ELSE b.ListPrice END AS ListPrice " +
                                                "   FROM TblPartsMainMF a WITH(READPAST) " +
                                                "   OUTER APPLY ( " +
                                                "       SELECT TOP 1 ListPrice " +
                                                "       FROM TblPartPrice b " +
                                                "       WHERE a.PartNo = b.PartNo " +
                                                "       ORDER BY b.ModifiedDt DESC " +
                                                "   ) AS b" +
                                                "   LEFT JOIN TblPartsBrandMF c WITH(READPAST) ON c.BrandID = a.BrandID " +
                                                "   LEFT JOIN TblPartsUomMF d WITH(READPAST) ON d.UomID = a.UomID " +
                                                "   LEFT JOIN TblPartsDescriptionMF e WITH(READPAST) ON e.DescID = a.DescID " +
                                                "   WHERE a.IsActive = 1 " +
                                                "       AND (1=(CASE WHEN ISNULL(@PartNo,'') = '' THEN 1 ELSE 0 END) OR a.PartNo LIKE '%' + @PartNo + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@PartName,'') = '' THEN 1 ELSE 0 END) OR a.PartName LIKE '%' + @PartName + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@DescID,'') = '' THEN 1 ELSE 0 END) OR a.DescID=@DescID) " +
                                                "       AND (1=(CASE WHEN ISNULL(@Sku,'') = '' THEN 1 ELSE 0 END) OR a.Sku LIKE '%' + @Sku + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@BrandID,'') = '' THEN 1 ELSE 0 END) OR c.BrandID=@BrandID) " +
                                                "       AND (1=(CASE WHEN ISNULL(@UomID,'') = '' THEN 1 ELSE 0 END) OR d.UomID=@UomID) ", connection);
                command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                command.Parameters.AddWithValue("@PartName", entity.PartName);
                command.Parameters.AddWithValue("@DescID", entity.Desc);
                command.Parameters.AddWithValue("@Sku", entity.Sku);
                command.Parameters.AddWithValue("@BrandID", entity.Brand);
                command.Parameters.AddWithValue("@UomID", entity.Uom);
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

        public DataTable PurchaseHistoryDataTable(string PartNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT CONVERT(varchar, a.CreatedDt, 23) AS RRDate, a.RRNo, RTRIM(c.SLName) AS SLName, b.UnitPrice, b.Qty " +
                                                "   FROM TblReceivingMain a WITH(READPAST) " +
                                                "   LEFT JOIN TblReceivingDet b WITH(READPAST) ON b.RRNo = a.RRNo " +
                                                "   LEFT JOIN TblSubsidiaryMain c WITH(READPAST) ON c.SLID = a.SupplierID " +
                                                "   WHERE b.PartNo=@PartNo " +
                                                "   ORDER BY a.CreatedDt DESC", connection);
                command.Parameters.AddWithValue("@PartNo", PartNo);
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

        public DataTable PartsHistoryDataTable(string PartNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT ControlNo, CASE WHEN ModifiedDt IS NULL THEN CONVERT(varchar, CreatedDt, 23) ELSE CONVERT(varchar, ModifiedDt, 23) END AS PriceDate, " +
                                                "       CreatedBy, CONVERT(varchar, CreatedDt, 23) AS CreatedDt, ModifiedBy, CONVERT(varchar, ModifiedDt, 23) AS ModifiedDt, " +
                                                "       0 AS UnitCost, ListPrice AS ListPriceHistory " +
                                                "   FROM TblPartPrice WITH(READPAST) " +
                                                "   WHERE PartNo=@PartNo", connection);
                command.Parameters.AddWithValue("@PartNo", PartNo);
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

        //Archive
        public DataTable ControlNoDataTable(string partno, string fromdt, string todt)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT DISTINCT ControlNo, CONVERT(varchar, CreatedDt,23) AS PriceDate " +
                                                "   FROM TblPartPrice WITH(READPAST) " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@PartNo,'') = '' THEN 1 ELSE 0 END) OR PartNo LIKE '%' + @PartNo + '%') " +
                                                "       AND CreatedDt BETWEEN DATEADD(day, -1, @DateFrom) AND DATEADD(day, 1, @DateTo)", connection);
                command.Parameters.AddWithValue("@PartNo", partno);
                command.Parameters.AddWithValue("@DateFrom", fromdt);
                command.Parameters.AddWithValue("@DateTo", todt);
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

        public DataTable PricePartsDataTable(string controlno)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(b.PartNo) AS PartNo, RTRIM(b.PartName) AS PartName, RTRIM(b.OtherName) AS OtherName, RTRIM(b.Sku) AS Sku, " +
                                                "       RTRIM(d.UomName) AS UomName, RTRIM(c.BrandName) AS BrandName, a.ListPrice " +
                                                "   FROM TblPartPrice a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                "   LEFT JOIN TblPartsBrandMF c WITH(READPAST) ON c.BrandID = b.BrandID " +
                                                "   LEFT JOIN TblPartsUomMF d WITH(READPAST) ON d.UomID = b.UomID " +
                                                "   WHERE a.ControlNo=@ControlNo", connection);
                command.Parameters.AddWithValue("@ControlNo", controlno);
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

        public string GeneratePartPriceID(SqlConnection connection, SqlCommand command, SqlTransaction transaction)
        {
            string id = "";
            try
            {
                command = Connection.setTransactionCommand("DECLARE @priceno varchar(10) = '' " +
                                                           "DECLARE @newpriceno varchar(10) = '' " +
                                                           "DECLARE @date varchar(4) = ((SELECT FORMAT(GETDATE(), 'yy'))+(SELECT FORMAT(GETDATE(),'MM'))) " +
                                                           "DECLARE @prefix varchar(10) = 'PP'+@date " +
                                                           "DECLARE @countable int = (SELECT COUNT(*) FROM TblCtrlNo WITH(READPAST)) " +
                                                           "SET @priceno = @prefix+'0001'; " +
                                                           "BEGIN " +
                                                           "   SET @priceno = (SELECT TOP 1 PriceNo FROM TblCtrlNo WITH(READPAST) WHERE CAST(SUBSTRING(PriceNo,1,6) AS varchar) = @prefix) " +
                                                           "   IF @priceno IS NULL " +
                                                           "       BEGIN " +
                                                           "           SET @priceno = @prefix+'0001'; " +
                                                           "           IF @countable > 0 " +
                                                           "           BEGIN " +
                                                           "               UPDATE TblCtrlNo " +
                                                           "                   SET PriceNo = @prefix+'0002' " +
                                                           "           END " +
                                                           "           ELSE " +
                                                           "           BEGIN " +
                                                           "               INSERT INTO TblCtrlNo(PriceNo) " +
                                                           "                   VALUES(@prefix+'0002') " +
                                                           "           END" +
                                                           "       END " +
                                                           "   ELSE " +
                                                           "       BEGIN " +
                                                           "           SET @newpriceno = (SELECT TOP 1 @prefix+ REPLICATE('0',4-LEN(SUBSTRING(@priceno,7,4)+1)) + CAST(SUBSTRING(@priceno,7,4)+1 AS varchar)) " +
                                                           "           UPDATE TblCtrlNo " +
                                                           "               SET PriceNo = @newpriceno " +
                                                           "       END " +
                                                           "END " +
                                                           "SELECT @priceno AS PriceNo", connection, transaction);
                id = Convert.ToString(command.ExecuteScalar() ?? "");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return id;
        }

        public override void Read(PriceManagementModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(PriceManagementModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
