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
    internal class SalesReturnController : Universal<SalesReturnModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(SalesReturnModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                string SRNo = GenerateSalesReturnNo(connection, command, transaction);
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT SRNo FROM TblSalesReturnMain WITH(READPAST) WHERE SRNo=@SRNo) " +
                                                           $"BEGIN " +
                                                           $"   INSERT INTO TblSalesReturnMain(SRNo, SRDate, InvoiceNo, SONo, SLID, SalesmanID, Remarks, Status, CreatedBy, " +
                                                           $"           CreatedDt, ModifiedBy, ModifiedDt) " +
                                                           $"       VALUES(@SRNo, GETDATE(), @InvoiceNo, @SONo, @SLID, @SalesmanID, @Remarks, @Status, @CreatedBy, " +
                                                           $"           GETDATE(), @CreatedBy, GETDATE()) " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@SRNo", SRNo);
                command.Parameters.AddWithValue("@InvoiceNo", entity.InvoiceNo);
                command.Parameters.AddWithValue("@SONo", entity.SONo);
                command.Parameters.AddWithValue("@SalesmanID", entity.SalesmanID);
                command.Parameters.AddWithValue("@Remarks", entity.Remarks);
                command.Parameters.AddWithValue("@Status", 1);
                if (entity.SLID == null)
                {
                    command.Parameters.AddWithValue("@SLID", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@SLID", entity.SLID);
                }
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                int i = command.ExecuteNonQuery();
                if (i != 1)
                {
                    message = "The information entered is already present in the database.";
                    transaction.Rollback();
                    transaction.Dispose();
                    connection.Close();
                    return message;
                }

                if (entity.ReturnDetails != null)
                {
                    foreach (var data in entity.ReturnDetails)
                    {
                        int j = 0, k = 0, l = 0;
                        //NEW
                        command = Connection.setTransactionCommand($"SELECT Qty " +
                                                                   $"    FROM TblSalesDet WITH(READPAST) " +
                                                                   $"    WHERE SONo=@SONo", connection, transaction);
                        command.Parameters.AddWithValue("@SONo", entity.SONo);
                        decimal total = Convert.ToDecimal(command.ExecuteScalar());
                        if ((data.GoodQty + data.DefectiveQty) > total)
                        {
                            message = "The return quantity exceeded the sales order quantity";
                            transaction.Rollback();
                            connection.Close();
                            return message;
                        }
                        //
                        command = Connection.setTransactionCommand($"IF EXISTS(SELECT SONo FROM TblSalesDet WITH(READPAST) WHERE SONo=@SONo AND ItemID=@ItemID) " +
                                                                   $"BEGIN " +
                                                                   $"   INSERT INTO TblSalesReturnDet(SRNo, ItemID, PartNo, GoodQty, DefectiveQty, ReasonID, FreeItem, ItemNo, Status, " +
                                                                   $"           CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                                   $"       VALUES(@SRNo, @ItemID, @PartNo, @GoodQty, @DefectiveQty, @ReasonID, @FreeItem, @ItemNo, @Status, " +
                                                                   $"           @CreatedBy, GETDATE(), @CreatedBy, GETDATE()) " +
                                                                   $"END", connection, transaction);
                        command.Parameters.AddWithValue("@SONo", entity.SONo);
                        command.Parameters.AddWithValue("@ItemID", data.ItemID);
                        command.Parameters.AddWithValue("@SRNo", SRNo);
                        command.Parameters.AddWithValue("@PartNo", data.PartNo);
                        command.Parameters.AddWithValue("@GoodQty", data.GoodQty);
                        command.Parameters.AddWithValue("@DefectiveQty", data.DefectiveQty);
                        command.Parameters.AddWithValue("@ReasonID", data.ReasonID);
                        command.Parameters.AddWithValue("@FreeItem", data.FreeItem);
                        command.Parameters.AddWithValue("@ItemNo", data.ItemNo);
                        command.Parameters.AddWithValue("@Status", data.Status);
                        command.Parameters.AddWithValue("@CreatedBy", Name01);
                        j = command.ExecuteNonQuery();
                        if (j > 0)
                        {
                            //Helper.TranLog("Sales Return", "Added new Stock Adjustment:" + AdjustmentNo + " for Lot No:" + data.LotNo, connection, command, transaction);
                            command = Connection.setTransactionCommand($"IF EXISTS(SELECT ItemID FROM TblSalesDetLoc WITH(READPAST) WHERE SONo=@SONo AND ItemID=@ItemID) " +
                                                                       $"BEGIN " +
                                                                       $"   INSERT INTO TblSalesReturnDetLoc(SRNo, ItemID, PartNo, GoodQty, DefectiveQty, LotNo, WhID, " +
                                                                       $"           BinID, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                                       $"       VALUES(@SRNo, @ItemID, @PartNo, @GoodQty, @DefectiveQty, @LotNo, @WhID, " +
                                                                       $"           @BinID, @CreatedBy, GETDATE(), @CreatedBy, GETDATE()) " +
                                                                       $"END", connection, transaction);
                            command.Parameters.AddWithValue("@SONo", entity.SONo);
                            command.Parameters.AddWithValue("@ItemID", data.ItemID);
                            command.Parameters.AddWithValue("@SRNo", SRNo);
                            command.Parameters.AddWithValue("@PartNo", data.PartNo);
                            command.Parameters.AddWithValue("@GoodQty", data.GoodQty);
                            command.Parameters.AddWithValue("@DefectiveQty", data.DefectiveQty);
                            command.Parameters.AddWithValue("@LotNo", data.LotNo);
                            command.Parameters.AddWithValue("@WhID", data.WhID);
                            command.Parameters.AddWithValue("@BinID", data.BinID);
                            command.Parameters.AddWithValue("@CreatedBy", Name01);
                            k = command.ExecuteNonQuery();
                            if (k > 0)
                            {
                                command = Connection.setTransactionCommand($"UPDATE TblInvLot " +
                                                                           $"   SET SReturns=SReturns + @GoodQty, DefReturns=DefReturns + @DefectiveQty, " +
                                                                           $"       ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                                           $"   WHERE LotNo=@LotNo " +
                                                                           $"       AND PartNo=@PartNo " +
                                                                           $"UPDATE TblInvLotLoc " +
                                                                           $"   SET SReturns=SReturns + @GoodQty, DefReturns=DefReturns + @DefectiveQty, " +
                                                                           $"       ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                                           $"   WHERE PartNo=@PartNo " +
                                                                           $"       AND LotNo=@LotNo " +
                                                                           $"       AND WhID=@WhID " +
                                                                           $"       AND BinID=@BinID", connection, transaction);
                                command.Parameters.AddWithValue("@PartNo", data.PartNo);
                                command.Parameters.AddWithValue("@LotNo", data.LotNo);
                                command.Parameters.AddWithValue("@GoodQty", data.GoodQty);
                                command.Parameters.AddWithValue("@DefectiveQty", data.DefectiveQty);
                                command.Parameters.AddWithValue("@WhID", data.WhID);
                                command.Parameters.AddWithValue("@BinID", data.BinID);
                                command.Parameters.AddWithValue("@CreatedBy", Name01);
                                l = command.ExecuteNonQuery();
                                if (l == 0)
                                {
                                    message = "Something went wrong.";
                                    transaction.Rollback();
                                    transaction.Dispose();
                                    connection.Close();
                                    return message;
                                }
                            }
                            else
                            {
                                message = "Something went wrong.";
                                transaction.Rollback();
                                transaction.Dispose();
                                connection.Close();
                                return message;
                            }
                        }
                        else
                        {
                            message = "Something went wrong.";
                            transaction.Rollback();
                            connection.Close();
                            return message;
                        }
                    }
                }
                Helper.TranLog("Sales Return", "Created new Sales Return:" + SRNo, connection, command, transaction);
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

        public override void Delete(SalesReturnModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(SalesReturnModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT CONVERT(varchar, a.SODate, 23) AS SODate, a.InvoiceNo, CONVERT(varchar, a.InvoiceDate, 23) AS InvoiceDate, a.CustName, " +
                                                "       a.CustAdd, a.CustTin, b.SLName, a.InvoiceRefNo, c.TermName, ISNULL(a.SLID,'') AS SLID, a.SalesmanID " +
                                                "   FROM TblSalesMain a WITH(READPAST) " +
                                                "   LEFT JOIN TblSubsidiaryMain b WITH(READPAST) ON b.SLID = a.SLID " +
                                                "   LEFT JOIN TblTermsMF c WITH(READPAST) ON c.TermID = a.TermID " +
                                                "   WHERE a.Status = 3 " +
                                                "       AND a.SONo=@SONo", connection); 
                command.Parameters.AddWithValue("@SONo", entity.SONo);
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

        public override void Read(SalesReturnModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(SalesReturnModel entity)
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<string, string> GetDictionary()
        {
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();

            try
            {
                connection.Open();
                command = Connection.setCommand($"SELECT SONo, SONo" +
                                                $"  FROM TblSalesMain WITH(READPAST) " +
                                                $"  WHERE Status = 3", connection);
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

        public DataTable SalesDetailsDataTable(string SONo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.ItemNo, RTRIM(a.PartNo) AS PartNo, RTRIM(b.PartName) AS PartName, RTRIM(b.OtherName) AS OtherName, " +
                                                "       RTRIM(c.DescName) AS DescName, RTRIM(d.BrandName) AS BrandName, RTRIM(b.Sku) AS Sku, RTRIM(e.UomName) AS UomName, " +
                                                "       a.ListPrice, a.Qty - ISNULL((SUM(g.GoodQty) + SUM(g.DefectiveQty)),0) AS Qty, a.ItemID, f.LotNo, f.WhID, " +
                                                "       f.BinID, a.FreeItem AS Free " +
                                                "   FROM TblSalesDet a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                "   LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                                                "   LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                                                "   LEFT JOIN TblPartsUomMF e WITH(READPAST) ON e.UomID = b.UomID " +
                                                "   LEFT JOIN TblSalesDetLoc f WITH(READPAST) ON f.ItemID = a.ItemID " +
                                                "   LEFT JOIN TblSalesReturnDet g WITH(READPAST) ON g.ItemID = a.ItemID" +
                                                "   WHERE a.SONo=@SONo " +
                                                "   GROUP BY a.ItemNo, a.PartNo, b.PartName, b.OtherName, c.DescName, d.BrandName, b.Sku, e.UomName, a.ListPrice, a.Qty, " +
                                                "       a.ItemID, f.LotNo, f.WhID, f.BinID, a.FreeItem " +
                                                "   HAVING a.Qty - ISNULL((SUM(g.GoodQty) + SUM(g.DefectiveQty)),0) > 0 " +
                                                "   ORDER BY a.ItemNo", connection);
                command.Parameters.AddWithValue("@SONo", SONo);
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

        public DataTable ReasonDataTable()
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(ReasonName) AS ReasonName, ReasonID " +
                                                "   FROM TblReasonMF WITH(READPAST)", connection);
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

        public string GenerateSalesReturnNo(SqlConnection connection, SqlCommand command, SqlTransaction transaction)
        {
            string id = "";
            try
            {
                command = Connection.setTransactionCommand("DECLARE @salesreturnno varchar(10) = '' " +
                                                           "DECLARE @newsalesretunno varchar(10) = '' " +
                                                           "DECLARE @date varchar(4) = ((SELECT FORMAT(GETDATE(), 'yy'))+(SELECT FORMAT(GETDATE(),'MM'))) " +
                                                           "DECLARE @prefix varchar(10) = 'SR'+@date " +
                                                           "DECLARE @countable int = (SELECT COUNT(*) FROM TblCtrlNo WITH(READPAST)) " +
                                                           "SET @salesreturnno = @prefix+'0001'; " +
                                                           "BEGIN " +
                                                           "   SET @salesreturnno = (SELECT TOP 1 SalesRetNo FROM TblCtrlNo WITH(READPAST) WHERE CAST(SUBSTRING(SalesRetNo,1,6) AS varchar) = @prefix) " +
                                                           "   IF @salesreturnno IS NULL " +
                                                           "       BEGIN " +
                                                           "           SET @salesreturnno = @prefix+'0001'; " +
                                                           "           IF @countable > 0 " +
                                                           "           BEGIN " +
                                                           "               UPDATE TblCtrlNo " +
                                                           "                   SET SalesRetNo = @prefix+'0002' " +
                                                           "           END " +
                                                           "           ELSE " +
                                                           "           BEGIN " +
                                                           "               INSERT INTO TblCtrlNo(SalesRetNo) " +
                                                           "                   VALUES(@prefix+'0002') " +
                                                           "           END" +
                                                           "       END " +
                                                           "   ELSE " +
                                                           "       BEGIN " +
                                                           "           SET @newsalesretunno = (SELECT TOP 1 @prefix+ REPLICATE('0',4-LEN(SUBSTRING(@salesreturnno,7,4)+1)) + CAST(SUBSTRING(@salesreturnno,7,4)+1 AS varchar)) " +
                                                           "           UPDATE TblCtrlNo " +
                                                           "               SET SalesRetNo = @newsalesretunno " +
                                                           "       END " +
                                                           "END " +
                                                           "SELECT @salesreturnno AS SalesRetNo", connection, transaction);
                id = Convert.ToString(command.ExecuteScalar() ?? "");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return id;
        }

        public DataTable SearchSalesArchive(string salesReturn, string salesman, string dateFrom, string dateTo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.SRNo, CONVERT(varchar, a.SRDate, 23) AS SRDate, a.InvoiceNo, a.SONo, RTRIM(b.CustName) AS CustName, " +
                                                "       RTRIM(c.EmployeeName) AS SLName, RTRIM(a.Remarks) AS Remarks " +
                                                "   FROM TblSalesReturnMain a WITH(READPAST) " +
                                                "   LEFT JOIN TblSalesMain b WITH(READPAST) ON b.SONo = a.SONo " +
                                                "   LEFT JOIN TblEmployeeMF c WITH(READPAST) ON c.EmployeeID = a.SalesmanID  " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@SRNo,'') = '' THEN 1 ELSE 0 END) OR a.SRNo LIKE '%' + @SRNo + '%' ) " +
                                                "       AND (1=(CASE WHEN ISNULL(@SalesmanID, '') = '' THEN 1 ELSE 0 END) OR a.SalesmanID=@SalesmanID) " +
                                                "       AND a.SRDate BETWEEN DATEADD(day, -1, @DateFrom) AND DATEADD(day, 1, @DateTo) ", connection);
                command.Parameters.AddWithValue("@SRNo", salesReturn);
                command.Parameters.AddWithValue("@SalesmanID", salesman);
                command.Parameters.AddWithValue("@DateFrom", dateFrom);
                command.Parameters.AddWithValue("@DateTo", dateTo);
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

        public DataTable SalesDetailsArchive(string SRNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.ItemNo, RTRIM(a.PartNo) AS PartNo, RTRIM(b.PartName) AS PartName, SUM(a.GoodQty) AS GoodQty, " +
                                                "       SUM(a.DefectiveQty) AS DefectiveQty, a.FreeItem, a.ItemID " +
                                                "   FROM TblSalesReturnDet a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                "   WHERE a.SRNo = SRNo " +
                                                "   GROUP BY a.ItemNo, a.PartNo, b.PartName, a.FreeItem, a.ItemID", connection);
                command.Parameters.AddWithValue("@SRNo", SRNo);
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

        public DataTable SalesLocationArchive(string ItemID)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(c.BinName) AS BinName, RTRIM(b.WhName) AS WhName, RTRIM(a.LotNo) AS LotNo, SUM(a.GoodQty) AS LocGoodQty, " +
                                                "       SUM(a.DefectiveQty) AS LocDefectiveQty " +
                                                "   FROM TblSalesReturnDetLoc a WITH(READPAST) " +
                                                "   LEFT JOIN TblWarehouseMF b WITH(READPAST) ON b.WhID = a.WhID " +
                                                "   LEFT JOIN TblWHLocationMF c WITH(READPAST) ON c.BinID = a.BinID " +
                                                "   WHERE a.ItemID=@ItemID " +
                                                "   GROUP BY c.BinName, b.WhName, a.LotNo", connection);
                command.Parameters.AddWithValue("@ItemID", ItemID);
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
