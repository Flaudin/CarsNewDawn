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
    internal class StockTransferController : Universal<StockTransferModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;
        public override string Create(StockTransferModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                string StockTransferNo = GenerateStockTransferNo(connection, command, transaction);
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT CtrlNo FROM TblWHTransfersMain WITH(READPAST) WHERE CtrlNo=@CtrlNo) " +
                                                           $"BEGIN " +
                                                           $"   INSERT INTO TblWHTransfersMain(CtrlNo, TransferType, ReasonID, Status, " +
                                                           $"           CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                           $"       VALUES(@CtrlNo, @TransferType, @ReasonID, @Status, @CreatedBy, GETDATE(), @CreatedBy, GETDATE()) " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@CtrlNo", StockTransferNo);
                command.Parameters.AddWithValue("@TransferType", entity.TransferType);
                command.Parameters.AddWithValue("@ReasonID", entity.ReasonID);
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
                    //problem not able to pas some transfer to webstore
                    foreach (var data in entity.DetailsList)
                    {
                        int j = 0, k = 0, l = 0;
                        command = Connection.setTransactionCommand($"SELECT Rcvd-PReturns-Picked+SReturns-DefReturns+BegBal+TakeUp-StockDrop+TrfIn-TrfOut AS total " +
                                                                   $"    FROM TblInvLotLoc WITH(READPAST) " +
                                                                   $"    WHERE LotNo=@LotNo " +
                                                                   $"        AND BinID=@BinID", connection, transaction);
                        command.Parameters.AddWithValue("@LotNo", data.LotNo);
                        command.Parameters.AddWithValue("@BinID", data.FromBinID);
                        decimal total = Convert.ToDecimal(command.ExecuteScalar());
                        if (data.Qty > total)
                        {
                            message = "The transfer quantity exceeded the balance on hand of part no " + data.PartNo + " for location " + data.FromBinName;
                            transaction.Rollback();
                            connection.Close();
                            return message;
                        }

                        command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT CtrlNo " +
                                                                   $"                   FROM TblWHTransfersDet WITH(READPAST) " +
                                                                   $"                   WHERE CtrlNo=@CtrlNo " +
                                                                   $"                       AND LotNo=@LotNo " +
                                                                   $"                       AND FromBinID=@FromBinID " +
                                                                   $"                       AND ToBinID=@ToBinID) " +
                                                                   $"BEGIN " +
                                                                   $"   INSERT INTO TblWHTransfersDet(CtrlNo, PartNo, LotNo, Qty, FromWhID, ToWhID, FromBinID, " +
                                                                   $"           ToBinID, Status, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                                   $"       VALUES(@CtrlNo, @PartNo, @LotNo, @Qty, @FromWhID, @ToWhID, @FromBinID, @ToBinID, @Status, " +
                                                                   $"           @CreatedBy, GETDATE(), @CreatedBy, GETDATE()) " +
                                                                   $"END", connection, transaction);
                        command.Parameters.AddWithValue("@CtrlNo", StockTransferNo);
                        command.Parameters.AddWithValue("@LotNo", data.LotNo);
                        command.Parameters.AddWithValue("@FromBinID", data.FromBinID);
                        command.Parameters.AddWithValue("@ToBinID", data.ToBinID);
                        command.Parameters.AddWithValue("@PartNo", data.PartNo);
                        command.Parameters.AddWithValue("@Qty", data.Qty);
                        command.Parameters.AddWithValue("@FromWhID", data.FromWhID);
                        command.Parameters.AddWithValue("@ToWhID", data.ToWhID);
                        command.Parameters.AddWithValue("@Status", data.Status);
                        command.Parameters.AddWithValue("@CreatedBy", Name01);
                        j = command.ExecuteNonQuery();

                        command = Connection.setTransactionCommand($"UPDATE TblInvLotLoc " +
                                                                   $"   SET TrfOut=TrfOut + @Qty " +
                                                                   $"   WHERE PartNo=@PartNo " +
                                                                   $"       AND WhID=@WhID " +
                                                                   $"       AND BinID=@BinID " +
                                                                   $"IF NOT EXISTS(SELECT CtrlNo " +
                                                                   $"                   FROM TblInvLotLoc WITH(READPAST) " +
                                                                   $"                   WHERE PartNo=@PartNo " +
                                                                   $"                       AND WhID=@ToWhID " +
                                                                   $"                       AND BinID=@ToBinID) " +
                                                                   $"BEGIN " +
                                                                   $"   DECLARE @CtrlNo varchar(10) = '' " +
                                                                   $"   SET @CtrlNo = (SELECT CtrlNo FROM TblInvLotLoc WITH(READPAST) WHERE LotNo=@LotNo AND WhID=@WhID AND BinID=@BinID)" +
                                                                   $"   INSERT TblInvLotLoc(PartNo, CtrlNo, LotNo, WhID, BinID, TrfIn, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                                   $"       VALUES(@PartNo, @CtrlNo, @LotNo, @ToWhID, @ToBinID, @Qty, @CreatedBy, GETDATE(), @CreatedBy, GETDATE())" +
                                                                   $"END " +
                                                                   $"ELSE " +
                                                                   $"BEGIN " +
                                                                   $"   UPDATE TblInvLotLoc " +
                                                                   $"       SET TrfIn=TrfIn + @Qty " +
                                                                   $"       WHERE PartNo=@PartNo " +
                                                                   $"           AND WhID=@ToWhID " +
                                                                   $"           AND BinID=@ToBinID " +
                                                                   $"END", connection, transaction);
                        command.Parameters.AddWithValue("@PartNo", data.PartNo);
                        command.Parameters.AddWithValue("@LotNo", data.LotNo);
                        command.Parameters.AddWithValue("@WhID", data.FromWhID);
                        command.Parameters.AddWithValue("@ToWhID", data.ToWhID);
                        command.Parameters.AddWithValue("@BinID", data.FromBinID);
                        command.Parameters.AddWithValue("@ToBinID", data.ToBinID);
                        command.Parameters.AddWithValue("@Qty", data.Qty);
                        command.Parameters.AddWithValue("@CreatedBy", Name01);
                        k = command.ExecuteNonQuery();

                        command = Connection.setTransactionCommand($"UPDATE TblInvLot " +
                                                                   $"   SET TrfIn = TrfIn + @Qty, TrfOut = TrfOut + @Qty, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                                   $"   WHERE LotNo=@LotNo " +
                                                                   $"       AND PartNo=@PartNo", connection, transaction);
                        command.Parameters.AddWithValue("@LotNo", data.LotNo);
                        command.Parameters.AddWithValue("@PartNo", data.PartNo);
                        command.Parameters.AddWithValue("@Qty", data.Qty);
                        command.Parameters.AddWithValue("@CreatedBy", Name01);
                        l = command.ExecuteNonQuery();

                        if (j > 0 && k > 0 && l > 0)
                        {
                            Helper.TranLog("StockTrans Details", "Transfered a total of " + data.Qty + " to warehouse: " + data.ToWhID + ", bin: " + data.ToBinID + 
                                " from warehouse: " + data.FromWhID + ", bin: " + data.FromBinID, connection, command, transaction);
                            Helper.TranLog("Inventory Lot", "Modified LotNo:" + data.LotNo + " transfered out:" + data.Qty, connection, command, transaction);
                            Helper.TranLog("Inventory LotLoc", "Modified LotNo:" + data.LotNo + " with Bin:" + data.FromBinID + " transfered out:" + data.Qty + 
                                " to LotNo:" + data.LotNo + " with Bin:" + data.ToBinID, connection, command, transaction);
                        }
                        else
                        {
                            message = "The stock transfered detail entered is already present in the database.";
                            transaction.Rollback();
                            connection.Close();
                            return message;
                        }
                    }
                }
                Helper.TranLog("Stock Transfer", "Created new Stock Transfer:" + StockTransferNo, connection, command, transaction);
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

        public override void Delete(StockTransferModel entity)
        {
            throw new NotImplementedException();
        }

        public string GenerateStockTransferNo(SqlConnection connection, SqlCommand command, SqlTransaction transaction)
        {
            string id = "";
            try
            {
                command = Connection.setTransactionCommand("DECLARE @transferno varchar(10) = '' " +
                                                           "DECLARE @newtransferno varchar(10) = '' " +
                                                           "DECLARE @date varchar(4) = ((SELECT FORMAT(GETDATE(), 'yy'))+(SELECT FORMAT(GETDATE(),'MM'))) " +
                                                           "DECLARE @prefix varchar(10) = 'ST'+@date " +
                                                           "DECLARE @countable int = (SELECT COUNT(*) FROM TblCtrlNo WITH(READPAST)) " +
                                                           "SET @transferno = @prefix+'0001'; " +
                                                           "BEGIN " +
                                                           "   SET @transferno = (SELECT TOP 1 WhStockTransferNo FROM TblCtrlNo WITH(READPAST) WHERE CAST(SUBSTRING(WhStockTransferNo,1,6) AS varchar) = @prefix) " +
                                                           "   IF @transferno IS NULL " +
                                                           "       BEGIN " +
                                                           "           SET @transferno = @prefix+'0001'; " +
                                                           "           IF @countable > 0 " +
                                                           "           BEGIN " +
                                                           "               UPDATE TblCtrlNo " +
                                                           "                   SET WhStockTransferNo = @prefix+'0002' " +
                                                           "           END " +
                                                           "           ELSE " +
                                                           "           BEGIN " +
                                                           "               INSERT INTO TblCtrlNo(WhStockTransferNo) " +
                                                           "                   VALUES(@prefix+'0002') " +
                                                           "           END" +
                                                           "       END " +
                                                           "   ELSE " +
                                                           "       BEGIN " +
                                                           "           SET @newtransferno = (SELECT TOP 1 @prefix+ REPLICATE('0',4-LEN(SUBSTRING(@transferno,7,4)+1)) + CAST(SUBSTRING(@transferno,7,4)+1 AS varchar)) " +
                                                           "           UPDATE TblCtrlNo " +
                                                           "               SET WhStockTransferNo = @newtransferno " +
                                                           "       END " +
                                                           "END " +
                                                           "SELECT @transferno AS WhStockTransferNo", connection, transaction);
                id = Convert.ToString(command.ExecuteScalar() ?? "");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return id;
        }

        public override DataTable dt(StockTransferModel entity)
        {
            throw new NotImplementedException();
        }

        public DataTable LocationFromDataTable(string PartNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(b.BinName) AS BinName, RTRIM(c.WhName) AS WhName, RTRIM(a.LotNo) AS LotNo, b.BinID, b.WhID " +
                                                "   FROM TblInvLotLoc a WITH(READPAST) " +
                                                "   LEFT JOIN TblWHLocationMF b WITH(READPAST) ON b.BinID = a.BinID " +
                                                "   LEFT JOIN TblWarehouseMF c WITH(READPAST) ON c.WhID = a.WhID " +
                                                "   WHERE a.PartNo=@PartNo " +
                                                "       AND ISNULL(c.IsWebStore,0) = 0", connection);
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

        public DataTable LocationToDataTable(string Bin, string Warehouse, int Transfer)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(a.BinName) AS BinName, RTRIM(b.WhName) AS WhName, a.BinID, a.WhID, RTRIM(b.WhLocation) AS WhLocation " +
                                                "   FROM TblWHLocationMF a WITH(READPAST) " +
                                                "   LEFT JOIN TblWarehouseMF b WITH(READPAST) ON b.WhID = a.WhID" +
                                                "   WHERE a.IsActive = 1 " +
                                                "       AND a.BinID!=@BinID " +
                                                "       AND ((1=(CASE WHEN @TransferType=1 THEN 1 ELSE 0 END) AND a.WhID!=@WhID) " +
                                                "           OR (1=(CASE WHEN @TransferType=2 THEN 1 ELSE 0 END) AND a.WhID=@WhID))", connection);
                command.Parameters.AddWithValue("@BinID", Bin);
                command.Parameters.AddWithValue("@WhID", Warehouse);
                command.Parameters.AddWithValue("@TransferType", Transfer);
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

        //ARCHIVE
        public DataTable StockTransferDataTable(string stocktransferno, string fromdt, string todt)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.CtrlNo, CASE WHEN a.TransferType = 1 THEN 'WAREHOUSE' ELSE 'BIN' END AS TransferName, " +
                                                "       RTRIM(b.ReasonName) AS ReasonName, CONVERT(varchar, a.CreatedDt, 23) AS CreatedDt " +
                                                "   FROM TblWHTransfersMain a WITH(READPAST) " +
                                                "   LEFT JOIN TblReasonMF b WITH(READPAST) ON b.ReasonID = a.ReasonID " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@CtrlNo,'') = '' THEN 1 ELSE 0 END) OR a.CtrlNo LIKE '%' + @CtrlNo + '%') " +
                                                "       AND a.CreatedDt BETWEEN DATEADD(day, -1, @DateFrom) AND DATEADD(day, 1, @DateTo)", connection);
                command.Parameters.AddWithValue("@CtrlNo", stocktransferno);
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

        public DataTable PartsDataTable(string transferno)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(b.PartNo) AS PartNo, RTRIM(b.PartName) AS PartName, RTRIM(b.OtherName) AS OtherName, RTRIM(c.DescName) AS DescName, " +
                                                "       RTRIM(d.BrandName) AS BrandName, RTRIM(e.BinName) AS BinName, RTRIM(f.WhName) AS WhName, RTRIM(a.LotNo) AS LotNo, " +
                                                "       RTRIM(g.BinName) AS BinToName, RTRIM(h.WhName) AS WhToName, a.Qty " +
                                                "   FROM TblWHTransfersDet a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                "   LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                                                "   LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                                                "   LEFT JOIN TblWHLocationMF e WITH(READPAST) ON e.BinID = a.FromBinID " +
                                                "   LEFT JOIN TblWarehouseMF f WITH(READPAST) ON f.WhID = e.WhID " +
                                                "   LEFT JOIN TblWHLocationMF g WITH(READPAST) ON g.BinID = a.ToBinID " +
                                                "   LEFT JOIN TblWarehouseMF h WITH(READPAST) ON h.WhID = e.WhID " +
                                                "   WHERE a.CtrlNo=@CtrlNo", connection);
                command.Parameters.AddWithValue("@CtrlNo", transferno);
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

        public override void Read(StockTransferModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(StockTransferModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
