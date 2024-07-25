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
    internal class StockAdjustmentController : Universal<StockAdjustmentModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(StockAdjustmentModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                string AdjustmentNo = GenerateStockAdjustmentNo(connection, command, transaction);
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT AdjNo FROM TblInvAdjustsMain WITH(READPAST) WHERE AdjNo=@AdjNo) " +
                                                           $"BEGIN " +
                                                           $"   INSERT INTO TblInvAdjustsMain(AdjNo, ReasonID, Remarks, Status, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                           $"       VALUES(@AdjNo, @ReasonID, @Remarks, @Status, @CreatedBy, GETDATE(), @CreatedBy, GETDATE()) " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@AdjNo", AdjustmentNo);
                command.Parameters.AddWithValue("@ReasonID", entity.ReasonID);
                command.Parameters.AddWithValue("@Remarks", entity.Remarks);
                command.Parameters.AddWithValue("@Status", 1);
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

                if (entity.DetailsList != null)
                {
                    foreach (var data in entity.DetailsList)
                    {
                        int j = 0, k = 0, l = 0;
                        command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT AdjNo FROM TblInvAdjustsDet WITH(READPAST) WHERE AdjNo=@AdjNo AND LotNo=@LotNo AND BinID=@BinID) " +
                                                                   $"BEGIN " +
                                                                   $"   INSERT INTO TblInvAdjustsDet(AdjNo, PartNo, ReasonID, TakeUpQty, DropQty, LotNo, WhID, BinID, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                                   $"       VALUES(@AdjNo, @PartNo, @ReasonID, @TakeUpQty, @DropQty, @LotNo, @WhID, @BinID, @CreatedBy, GETDATE(), @CreatedBy, GETDATE()) " +
                                                                   $"END", connection, transaction);
                        command.Parameters.AddWithValue("@AdjNo", AdjustmentNo);
                        command.Parameters.AddWithValue("@PartNo", data.PartNo);
                        command.Parameters.AddWithValue("@ReasonID", entity.ReasonID);
                        command.Parameters.AddWithValue("@TakeUpQty", data.TakeUpQty);
                        command.Parameters.AddWithValue("@DropQty", data.DropQty);
                        command.Parameters.AddWithValue("@LotNo", data.LotNo);
                        command.Parameters.AddWithValue("@WhID", data.WhID);
                        command.Parameters.AddWithValue("@BinID", data.BinID);
                        command.Parameters.AddWithValue("@CreatedBy", Name01);
                        j = command.ExecuteNonQuery();

                        if (data.TakeUpQty != 0)
                        {
                            command = Connection.setTransactionCommand($"UPDATE TblInvLot" +
                                                                       $"  SET TakeUp = TakeUp + @Qty, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                                       $"  WHERE LotNo=@LotNo", connection, transaction);
                            command.Parameters.AddWithValue("@LotNo", data.LotNo);
                            command.Parameters.AddWithValue("@Qty", data.TakeUpQty);
                            command.Parameters.AddWithValue("@CreatedBy", Name01);
                            k = command.ExecuteNonQuery();

                            command = Connection.setTransactionCommand($"UPDATE TblInvLotLoc " +
                                                                       $"   SET TakeUp = TakeUp + @Qty, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                                       $"   WHERE LotNo=@LotNo " +
                                                                       $"       AND BinID=@BinID", connection, transaction);
                            command.Parameters.AddWithValue("@LotNo", data.LotNo);
                            command.Parameters.AddWithValue("@BinID", data.BinID);
                            command.Parameters.AddWithValue("@Qty", data.TakeUpQty);
                            command.Parameters.AddWithValue("@CreatedBy", Name01);
                            l = command.ExecuteNonQuery();
                        }
                        else
                        {
                            command = Connection.setTransactionCommand($"SELECT Rcvd-PReturns-Picked+SReturns-DefReturns+BegBal+TakeUp-StockDrop+TrfIn-TrfOut AS total " +
                                                                       $"    FROM TblInvLotLoc WITH(READPAST) " +
                                                                       $"    WHERE LotNo=@LotNo " +
                                                                       $"        AND BinID=@BinID", connection, transaction);
                            command.Parameters.AddWithValue("@LotNo", data.LotNo);
                            command.Parameters.AddWithValue("@BinID", data.BinID);
                            decimal total = Convert.ToDecimal(command.ExecuteScalar());
                            if (data.DropQty > total)
                            {
                                message = "The drop quantity exceeded the balance on hand of part no " + data.PartNo;
                                transaction.Rollback();
                                connection.Close();
                                return message;
                            }
                            command = Connection.setTransactionCommand($"UPDATE TblInvLot" +
                                                                       $"  SET StockDrop = StockDrop + @Qty, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                                       $"  WHERE LotNo=@LotNo", connection, transaction);
                            command.Parameters.AddWithValue("@LotNo", data.LotNo);
                            command.Parameters.AddWithValue("@Qty", data.DropQty);
                            command.Parameters.AddWithValue("@CreatedBy", Name01);
                            k = command.ExecuteNonQuery();

                            command = Connection.setTransactionCommand($"UPDATE TblInvLotLoc " +
                                                                       $"   SET StockDrop = StockDrop + @Qty, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                                       $"   WHERE LotNo=@LotNo " +
                                                                       $"       AND BinID=@BinID", connection, transaction);
                            command.Parameters.AddWithValue("@LotNo", data.LotNo);
                            command.Parameters.AddWithValue("@BinID", data.BinID);
                            command.Parameters.AddWithValue("@Qty", data.DropQty);
                            command.Parameters.AddWithValue("@CreatedBy", Name01);
                            l = command.ExecuteNonQuery();
                        }
                        if (j > 0 && k > 0 && l > 0)
                        {
                            Helper.TranLog("StockAdj Details", "Added new Stock Adjustment:" + AdjustmentNo + " for Lot No:" + data.LotNo, connection, command, transaction);
                            if (data.TakeUpQty != 0)
                            {
                                Helper.TranLog("Inventory Lot", "Modified LotNo:" + data.LotNo + " increased TakeUp by:" + data.TakeUpQty, connection, command, transaction);
                                Helper.TranLog("Inventory LotLoc", "Modified LotNo:" + data.LotNo + " with Bin:" + data.BinID + " increased TakeUp by:" + data.TakeUpQty, connection, command, transaction);
                            }
                            else
                            {
                                Helper.TranLog("Inventory Lot", "Modified LotNo:" + data.LotNo + " increased StockDrop by:" + data.DropQty, connection, command, transaction);
                                Helper.TranLog("Inventory LotLoc", "Modified LotNo:" + data.LotNo + " with Bin:" + data.BinID + " increased StockDrop by:" + data.DropQty, connection, command, transaction);
                            }
                        }
                        else
                        {
                            message = "The stock adjustment detail entered is already present in the database.";
                            transaction.Rollback();
                            connection.Close();
                            return message;
                        }
                    }
                }
                Helper.TranLog("Stock Adjustment", "Created new Stock Adjustment:" + AdjustmentNo, connection, command, transaction);
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

        public override void Delete(StockAdjustmentModel entity)
        {
            throw new NotImplementedException();
        }

        public DataTable LocationDataTable(string PartNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(b.BinName) AS BinName, RTRIM(c.WhName) AS WhName, b.BinID, b.WhID " +
                                                "   FROM TblInvLotLoc a WITH(READPAST) " +
                                                "   LEFT JOIN TblWHLocationMF b WITH(READPAST) ON b.BinID = a.BinID " +
                                                "   LEFT JOIN TblWarehouseMF c WITH(READPAST) ON c.WhID = a.WhID  " +
                                                "   WHERE a.PartNo=@PartNo " +
                                                "       AND ISNULL(c.IsWebStore, 0) = 0", connection);
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

        public DataTable LotNoDataTable(string PartNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT LotNo, UnitCost " +
                                                "   FROM TblInvLot " +
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
        public DataTable StockAdjustmentDataTable(string stockadjustmentno, string fromdt, string todt)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.AdjNo, RTRIM(a.Remarks) AS Remarks, RTRIM(b.ReasonName) AS ReasonName, CONVERT(varchar, a.CreatedDt, 23) AS CreatedDt " +
                                                "   FROM TblInvAdjustsMain a WITH(READPAST) " +
                                                "   LEFT JOIN TblReasonMF b WITH(READPAST) ON b.ReasonID = a.ReasonID " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@AdjNo,'') = '' THEN 1 ELSE 0 END) OR a.AdjNo LIKE '%' + @AdjNo + '%') " +
                                                "       AND a.CreatedDt BETWEEN DATEADD(day, -1, @DateFrom) AND DATEADD(day, 1, @DateTo)", connection);
                command.Parameters.AddWithValue("@AdjNo", stockadjustmentno);
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

        public DataTable PartsDataTable(string adjustmentno)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(b.PartNo) AS PartNo, RTRIM(b.PartName) AS PartName, RTRIM(b.OtherName) AS OtherName, RTRIM(c.DescName) AS DescName, " +
                                                "       RTRIM(d.BrandName) AS BrandName, RTRIM(e.BinName) AS BinName, RTRIM(f.WhName) AS WhName, RTRIM(a.LotNo) AS LotNo, " +
                                                "       a.TakeUpQty, a.DropQty " +
                                                "   FROM TblInvAdjustsDet a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                "   LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                                                "   LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                                                "   LEFT JOIN TblWHLocationMF e WITH(READPAST) ON e.BinID = a.BinID" +
                                                "   LEFT JOIN TblWarehouseMF f WITH(READPAST) ON f.WhID = e.WhID" +
                                                "   WHERE a.AdjNo=@AdjNo", connection);
                command.Parameters.AddWithValue("@AdjNo", adjustmentno);
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

        public string GenerateStockAdjustmentNo(SqlConnection connection, SqlCommand command, SqlTransaction transaction)
        {
            string id = "";
            try
            {
                command = Connection.setTransactionCommand("DECLARE @adjustmentno varchar(10) = '' " +
                                                           "DECLARE @newadjustmentno varchar(10) = '' " +
                                                           "DECLARE @date varchar(4) = ((SELECT FORMAT(GETDATE(), 'yy'))+(SELECT FORMAT(GETDATE(),'MM'))) " +
                                                           "DECLARE @prefix varchar(10) = 'SA'+@date " +
                                                           "DECLARE @countable int = (SELECT COUNT(*) FROM TblCtrlNo WITH(READPAST)) " +
                                                           "SET @adjustmentno = @prefix+'0001'; " +
                                                           "BEGIN " +
                                                           "   SET @adjustmentno = (SELECT TOP 1 WhAdjustNo FROM TblCtrlNo WITH(READPAST) WHERE CAST(SUBSTRING(WhAdjustNo,1,6) AS varchar) = @prefix) " +
                                                           "   IF @adjustmentno IS NULL " +
                                                           "       BEGIN " +
                                                           "           SET @adjustmentno = @prefix+'0001'; " +
                                                           "           IF @countable > 0 " +
                                                           "           BEGIN " +
                                                           "               UPDATE TblCtrlNo " +
                                                           "                   SET WhAdjustNo = @prefix+'0002' " +
                                                           "           END " +
                                                           "           ELSE " +
                                                           "           BEGIN " +
                                                           "               INSERT INTO TblCtrlNo(WhAdjustNo) " +
                                                           "                   VALUES(@prefix+'0002') " +
                                                           "           END" +
                                                           "       END " +
                                                           "   ELSE " +
                                                           "       BEGIN " +
                                                           "           SET @newadjustmentno = (SELECT TOP 1 @prefix+ REPLICATE('0',4-LEN(SUBSTRING(@adjustmentno,7,4)+1)) + CAST(SUBSTRING(@adjustmentno,7,4)+1 AS varchar)) " +
                                                           "           UPDATE TblCtrlNo " +
                                                           "               SET WhAdjustNo = @newadjustmentno " +
                                                           "       END " +
                                                           "END " +
                                                           "SELECT @adjustmentno AS AdjustmentNo", connection, transaction);
                id = Convert.ToString(command.ExecuteScalar() ?? "");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return id;
        }

        public override DataTable dt(StockAdjustmentModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(StockAdjustmentModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(StockAdjustmentModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
