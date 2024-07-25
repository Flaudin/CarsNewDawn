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
    internal class SalesOrderController : Universal<SalesOrderModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(SalesOrderModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                string SoNo = GenerateSalesOrderNo(connection, command, transaction);
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT SONo FROM TblSalesMain WITH(READPAST) WHERE SONo=@SONo) " +
                                                           $"BEGIN " +
                                                           $"   INSERT INTO TblSalesMain(SONo, SODate, CashTran, CustName, CustAdd, CustTin, TermID, SalesmanID, " +
                                                           $"           Remarks, Status, SLID, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                           $"       VALUES(@SONo, GETDATE(), @CashTran, @CustName, @CustAdd, @CustTin, @TermID, @SalesmanID, @Remarks, @Status, " +
                                                           $"           @SLID, @CreatedBy, GETDATE(), @CreatedBy, GETDATE()) " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@SONo", SoNo);
                command.Parameters.AddWithValue("@CashTran", entity.CashTran);
                command.Parameters.AddWithValue("@CustName", entity.CustName);
                command.Parameters.AddWithValue("@CustAdd", entity.CustAdd);
                command.Parameters.AddWithValue("@CustTin", entity.CustTin);
                command.Parameters.AddWithValue("@TermID", entity.TermID);
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
                Helper.TranLog("Sales Order", "Created new Sales Order:" + SoNo, connection, command, transaction);
                message = SoNo;
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

        public string PickNew(SalesOrderDetailModel entity)
        {
            string message = entity.ItemID;
            try
            {
                var ToPick = entity.Qty;
                int i = 0, j = 0, begbalcount = 0, begbalaffected = 0, adjustmentcount = 0, adjustmentaffected = 0;
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                int vat = 1;
                if (entity.SLID != null)
                {
                    command = Connection.setTransactionCommand($"SELECT VATType " +
                                                               $"   FROM TblSubsidiaryMain WITH(READPAST) " +
                                                               $"   WHERE SLID=@SLID", connection, transaction);
                    command.Parameters.AddWithValue("@SLID", entity.SLID);
                    vat = Convert.ToInt32(command.ExecuteScalar());
                }

                string unique = entity.ItemID;
                if (unique == "")
                {
                    message = unique = Helper.GenerateUID();
                }

                double netprice = 0;
                if (!entity.FreeItem)
                {
                    netprice = entity.ListPrice - entity.Discount;
                }

                int x = 0;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT SONo FROM TblSalesDet WITH(READPAST) WHERE SONo=@SONo AND ItemID=@ItemID) " +
                                                           $" BEGIN " +
                                                           $"   INSERT INTO TblSalesDet(SONo, ItemID, ItemNo, PartNo, Qty, ListPrice, Discount, NetPrice, VATAmt, FreeItem, " +
                                                           $"           FreeReason, FreeAppBy, AllowBelCost, Status, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                           $"       VALUES(@SONo, @ItemID, @ItemNo, @PartNo, @Qty, @ListPrice, @Discount, @NetPrice, @VATAmt, @FreeItem, " +
                                                           $"           @FreeReason, @FreeAppBy, @AllowBelCost, @Status, @CreatedBy, GETDATE(), @CreatedBy, GETDATE()) " +
                                                           $" END " +
                                                           $"ELSE " +
                                                           $" BEGIN " +
                                                           $"   UPDATE TblSalesDet " +
                                                           $"       SET Qty=@Qty, Discount=@Discount, NetPrice=@NetPrice, VATAmt=@VATAmt, FreeItem=@FreeItem, FreeReason=@FreeReason, " +
                                                           $"           FreeAppBy=@FreeAppBy, AllowBelCost=@AllowBelCost, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                           $"       WHERE SONo=@SONo " +
                                                           $"           AND ItemID=@ItemID" +
                                                           $"   DELETE TblSalesDetLoc " +
                                                           $"       WHERE SONo=@SONo" +
                                                           $"           AND ItemID=@ItemID " +
                                                           $" END", connection, transaction);
                command.Parameters.AddWithValue("@SONo", entity.SoNo);
                command.Parameters.AddWithValue("@ItemID", unique);
                command.Parameters.AddWithValue("@ItemNo", entity.ItemNo);
                command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                command.Parameters.AddWithValue("@Qty", entity.Qty);
                command.Parameters.AddWithValue("@ListPrice", entity.ListPrice);
                command.Parameters.AddWithValue("@Discount", entity.Discount);
                command.Parameters.AddWithValue("@NetPrice", netprice);
                switch (vat)
                {
                    case 0:
                        command.Parameters.AddWithValue("@VATAmt", netprice / 1.12);
                        break;

                    case 1:
                        command.Parameters.AddWithValue("@VATAmt", netprice + (netprice * 0.12));
                        break;

                    default:
                        command.Parameters.AddWithValue("@VATAmt", 0);
                        break;
                }
                command.Parameters.AddWithValue("@FreeItem", entity.FreeItem);
                command.Parameters.AddWithValue("@FreeReason", entity.FreeReason);
                command.Parameters.AddWithValue("@FreeAppBy", Name01);
                command.Parameters.AddWithValue("@AllowBelCost", entity.AllowBelCost);
                command.Parameters.AddWithValue("@Status", entity.Status);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                x = command.ExecuteNonQuery();
                if (x == 0)
                {
                    message = "The Sales Order details entered is already present in the database.";
                    transaction.Rollback();
                    connection.Close();
                    return message;
                }

                DataTable BegBalTable = new DataTable();
                command = Connection.setTransactionCommand("SELECT BegBalNo, WhID, BinID, Qty, CreatedDt " +
                                                           "    FROM TblInvBegbalDetLoc WITH(READPAST) " +
                                                           "    WHERE PartNo=@PartNo " +
                                                           "    ORDER BY CreatedDt", connection, transaction);
                command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                reader = command.ExecuteReader();
                BegBalTable.Load(reader);
                begbalcount = BegBalTable.Rows.Count;
                foreach (DataRow row in BegBalTable.Rows)
                {
                    if (ToPick > 0)
                    {
                        i = 0;
                        command = Connection.setTransactionCommand("IF NOT EXISTS(SELECT SONo FROM TblSalesDetLoc WITH(READPAST) " +
                                                                   "                   WHERE SONo=@SONo " +
                                                                   "                       AND PartNo=@PartNo " +
                                                                   "                       AND LotNo=@LotNo " +
                                                                   "                       AND WhID=@WhID " +
                                                                   "                       AND BinID=@BinID) " +
                                                                   " BEGIN " +
                                                                   "   INSERT INTO TblSalesDetLoc(SONo, ItemID, PartNo, Qty, LotNo, WhID, BinID, CreatedBy, CreatedDt, " +
                                                                   "           ModifiedBy, ModifiedDt) " +
                                                                   "       VALUES(@SONo, @ItemID, @PartNo, @Qty, @LotNo, @WhID, @BinID, @CreatedBy, GETDATE(), " +
                                                                   "           @CreatedBy, GETDATE()) " +
                                                                   " END " +
                                                                   "ELSE " +
                                                                   " BEGIN " +
                                                                   "   UPDATE TblSalesDetLoc " +
                                                                   "       SET Qty= CASE WHEN @StoredQty>=@Qty THEN Qty+@Qty ELSE Qty+@StoredQty END" +
                                                                   "       WHERE  @SONo=@SONo" +
                                                                   "           AND ItemID=@ItemID " +
                                                                   " END " +
                                                                   "UPDATE TblInvLotLoc " +
                                                                   "    SET Picked= CASE WHEN @StoredQty>=@Qty THEN Picked+@Qty ELSE Picked+@StoredQty END, " +
                                                                   "        ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                                   "    WHERE WhID=@WhID " +
                                                                   "        AND BinID=@BinID " +
                                                                   "        AND CtrlNo=@CtrlNo " +
                                                                   "UPDATE TblInvLot " +
                                                                   "    SET Picked= CASE WHEN @StoredQty>=@Qty THEN Picked+@Qty ELSE Picked+@StoredQty END " +
                                                                   "    WHERE CtrlNo=@CtrlNo " +
                                                                   "        AND PartNo=@PartNo", connection, transaction);
                        command.Parameters.AddWithValue("@SONo", entity.SoNo);
                        command.Parameters.AddWithValue("@ItemID", unique);
                        command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                        command.Parameters.AddWithValue("@LotNo", row["BegBalNo"].ToString().TrimEnd() + entity.PartNo);
                        command.Parameters.AddWithValue("@StoredQty", Convert.ToDecimal(row["Qty"]));
                        command.Parameters.AddWithValue("@Qty", ToPick);
                        command.Parameters.AddWithValue("@WhID", row["WhID"].ToString());
                        command.Parameters.AddWithValue("@BinID", row["BinID"].ToString());
                        command.Parameters.AddWithValue("@CtrlNo", row["BegBalNo"].ToString());
                        command.Parameters.AddWithValue("@CreatedBy", Name01);
                        i = command.ExecuteNonQuery();
                        if (i > 0)
                        {
                            begbalaffected++;
                            ToPick = ToPick - Convert.ToDecimal(row["Qty"]);
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
                        break;
                    }
                }

                if (ToPick > 0 && i > 0)
                {
                    DataTable TransferTable = new DataTable();
                    command = Connection.setTransactionCommand("SELECT LotNo, WhID, BinID, TakeUpQty, CreatedDt " +
                                                               "    FROM TblInvAdjustsDet WITH(READPAST) " +
                                                               "    WHERE PartNo=@PartNo " +
                                                               "        AND TakeUpQty!=0 " +
                                                               "    ORDER BY CreatedDt", connection, transaction);
                    command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                    reader = command.ExecuteReader();
                    TransferTable.Load(reader);
                    adjustmentcount = TransferTable.Rows.Count;
                    foreach (DataRow row in TransferTable.Rows)
                    {
                        if (ToPick > 0)
                        {
                            command = Connection.setTransactionCommand("IF EXISTS(SELECT SONo FROM TblSalesDetLoc WITH(READPAST) " +
                                                                       "                   WHERE SONo=@SONo " +
                                                                       "                       AND PartNo=@PartNo " +
                                                                       "                       AND LotNo=@LotNo " +
                                                                       "                       AND WhID=@WhID " +
                                                                       "                       AND BinID=@BinID) " +
                                                                       " BEGIN " +
                                                                       "   UPDATE TblSalesDetLoc " +
                                                                       "       SET Qty= CASE WHEN @StoredQty>=@Qty THEN Qty+@Qty ELSE Qty+@StoredQty END " +
                                                                       "       WHERE SONo=@SONo " +
                                                                       "           AND ItemID=@ItemID " +
                                                                       "           AND LotNo=@LotNo " +
                                                                       "           AND WhID=@WhID " +
                                                                       "           AND BinID=@BinID " +
                                                                       " END " +
                                                                       "UPDATE TblInvLotLoc " +
                                                                       "    SET Picked= CASE WHEN @StoredQty>=@Qty THEN Picked+@Qty ELSE Picked+@StoredQty END, " +
                                                                       "        ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                                       "    WHERE WhID=@WhID " +
                                                                       "        AND BinID=@BinID " +
                                                                       "        AND LotNo=@LotNo " +
                                                                       "UPDATE TblInvLot " +
                                                                       "    SET Picked= CASE WHEN @StoredQty>=@Qty THEN Picked+@Qty ELSE Picked+@StoredQty END " +
                                                                       "    WHERE LotNo=@LotNo " +
                                                                       "        AND PartNo=@PartNo", connection, transaction);
                            command.Parameters.AddWithValue("@SONo", entity.SoNo);
                            command.Parameters.AddWithValue("@ItemID", unique);
                            command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                            command.Parameters.AddWithValue("@StoredQty", Convert.ToDecimal(row["TakeUpQty"]));
                            command.Parameters.AddWithValue("@Qty", ToPick);
                            command.Parameters.AddWithValue("@WhID", row["WhID"].ToString());
                            command.Parameters.AddWithValue("@BinID", row["BinID"].ToString());
                            command.Parameters.AddWithValue("@LotNo", row["LotNo"].ToString());
                            command.Parameters.AddWithValue("@CreatedBy", Name01);
                            j = command.ExecuteNonQuery();
                            if (j > 0)
                            {
                                adjustmentaffected++;
                                ToPick = ToPick - Convert.ToDecimal(row["TakeUpQty"]);
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
                            break;
                        }
                    }
                }

                if (i > 0)
                {
                    transaction.Commit();
                }
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

        public string ResetPick(string PartNo, decimal Qty)
        {
            string message = "Inventory reset complete";
            try
            {
                var ToPick = Qty;
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                DataTable BegBalTable = new DataTable();
                command = Connection.setTransactionCommand("SELECT BegBalNo, WhID, BinID, Qty, CreatedDt " +
                                                           "    FROM TblInvBegbalDetLoc WITH(READPAST) " +
                                                           "    WHERE PartNo=@PartNo " +
                                                           "    ORDER BY CreatedDt", connection, transaction);
                command.Parameters.AddWithValue("@PartNo", PartNo);
                reader = command.ExecuteReader();
                BegBalTable.Load(reader);
                int i = 0, j = 0;
                foreach (DataRow row in BegBalTable.Rows)
                {
                    if (ToPick > 0)
                    {
                        i = 0;
                        command = Connection.setTransactionCommand("UPDATE TblInvLotLoc " +
                                                                   "    SET Picked= CASE WHEN @StoredQty>=@Qty THEN Picked-@Qty ELSE Picked-@StoredQty END, " +
                                                                   "        ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                                   "    WHERE WhID=@WhID " +
                                                                   "        AND BinID=@BinID " +
                                                                   "        AND CtrlNo=@CtrlNo " +
                                                                   "UPDATE TblInvLot " +
                                                                   "    SET Picked= CASE WHEN @StoredQty>=@Qty THEN Picked-@Qty ELSE Picked-@StoredQty END " +
                                                                   "    WHERE CtrlNo=@CtrlNo " +
                                                                   "        AND PartNo=@PartNo", connection, transaction);
                        command.Parameters.AddWithValue("@PartNo", PartNo);
                        command.Parameters.AddWithValue("@StoredQty", Convert.ToDecimal(row["Qty"]));
                        command.Parameters.AddWithValue("@Qty", ToPick);
                        command.Parameters.AddWithValue("@WhID", row["WhID"].ToString());
                        command.Parameters.AddWithValue("@BinID", row["BinID"].ToString());
                        command.Parameters.AddWithValue("@CtrlNo", row["BegBalNo"].ToString());
                        command.Parameters.AddWithValue("@CreatedBy", Name01);
                        i = command.ExecuteNonQuery();
                        if (i > 0)
                        {
                            ToPick = ToPick - Convert.ToDecimal(row["Qty"]);
                        }
                        else
                        {
                            message = "Something went wrong";
                            transaction.Rollback();
                            transaction.Dispose();
                            connection.Close();
                            return message;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (ToPick > 0 && i > 0)
                {
                    DataTable TransferTable = new DataTable();
                    command = Connection.setTransactionCommand("SELECT LotNo, WhID, BinID, TakeUpQty, CreatedDt " +
                                                               "    FROM TblInvAdjustsDet WITH(READPAST) " +
                                                               "    WHERE PartNo=@PartNo " +
                                                               "        AND TakeUpQty!=0 " +
                                                               "    ORDER BY CreatedDt", connection, transaction);
                    command.Parameters.AddWithValue("@PartNo", PartNo);
                    reader = command.ExecuteReader();
                    TransferTable.Load(reader);
                    foreach (DataRow row in TransferTable.Rows)
                    {
                        if (ToPick > 0)
                        {
                            j = 0;
                            command = Connection.setTransactionCommand("UPDATE TblInvLotLoc " +
                                                                       "    SET Picked= CASE WHEN @StoredQty>=@Qty THEN Picked-@Qty ELSE Picked-@StoredQty END, " +
                                                                       "        ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                                       "    WHERE WhID=@WhID " +
                                                                       "        AND BinID=@BinID " +
                                                                       "        AND LotNo=@LotNo " +
                                                                       "UPDATE TblInvLot " +
                                                                       "    SET Picked= CASE WHEN @StoredQty>=@Qty THEN Picked-@Qty ELSE Picked-@StoredQty END " +
                                                                       "    WHERE LotNo=@LotNo " +
                                                                       "        AND PartNo=@PartNo", connection, transaction);
                            command.Parameters.AddWithValue("@PartNo", PartNo);
                            command.Parameters.AddWithValue("@StoredQty", Convert.ToDecimal(row["TakeUpQty"]));
                            command.Parameters.AddWithValue("@Qty", ToPick);
                            command.Parameters.AddWithValue("@WhID", row["WhID"].ToString());
                            command.Parameters.AddWithValue("@BinID", row["BinID"].ToString());
                            command.Parameters.AddWithValue("@LotNo", row["LotNo"].ToString());
                            command.Parameters.AddWithValue("@CreatedBy", Name01);
                            j = command.ExecuteNonQuery();
                            if (j > 0)
                            {
                                ToPick = ToPick - Convert.ToDecimal(row["TakeUpQty"]);
                            }
                            else
                            {
                                message = "Something went wrong";
                                transaction.Rollback();
                                transaction.Dispose();
                                connection.Close();
                                return message;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                //Helper.TranLog("Sales Order", "Created new Sales Order:" + AdjustmentNo, connection, command, transaction);
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

        public string ResetSalesDetails(string SONo, string ItemID)
        {
            string message = "Information deleted successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF EXISTS(SELECT SONo FROM TblSalesDet WITH(READPAST) WHERE SONo=@SONo AND ItemID=@ItemID) " +
                                                           $" BEGIN " +
                                                           $"   DELETE TblSalesDet " +
                                                           $"       WHERE SONo=@SONo " +
                                                           $"           AND ItemID=@ItemID" +
                                                           $"   DELETE TblSalesDetLoc " +
                                                           $"       WHERE SONo=@SONo" +
                                                           $"           AND ItemID=@ItemID " +
                                                           $" END", connection, transaction);
                command.Parameters.AddWithValue("@ItemID", ItemID);
                command.Parameters.AddWithValue("@SONo", SONo);
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    transaction.Commit();
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

        public override void Delete(SalesOrderModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(SalesOrderModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(SalesOrderModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(SalesOrderModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                int i = 0;
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                int vat = 0;
                if (!entity.CashTran)
                {
                    command = Connection.setTransactionCommand($"SELECT VATType " +
                                                               $"   FROM TblSubsidiaryMain WITH(READPAST) " +
                                                               $"   WHERE SLID=@SLID", connection, transaction);
                    command.Parameters.AddWithValue("@SLID", entity.SLID);
                    vat = Convert.ToInt32(command.ExecuteScalar());
                }

                command = Connection.setTransactionCommand($"UPDATE TblSalesMain " +
                                                           $"   SET TermID=@TermID, InvoiceRefNo=@InvoiceRefNo, InvoiceRefDate=GETDATE(), Remarks=@Remarks, Status=@Status, " +
                                                           $"       ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                           $"   WHERE SONo=@SONo " +
                                                           $"UPDATE TblSalesDet " +
                                                           $"   SET Status=@Status, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                           $"   WHERE SONo=@SONo", connection, transaction);
                command.Parameters.AddWithValue("@SONo", entity.SoNo);
                command.Parameters.AddWithValue("@TermID", entity.TermID);
                command.Parameters.AddWithValue("@InvoiceRefNo", entity.InvoiceRefNo);
                command.Parameters.AddWithValue("@Remarks", entity.Remarks);
                command.Parameters.AddWithValue("@Status", 2);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                i = command.ExecuteNonQuery();
                if (i == 0)
                {
                    message = "Something went wrong.";
                    transaction.Rollback();
                    transaction.Dispose();
                    connection.Close();
                    return message;
                }
                else
                {
                    Helper.TranLog("Sales Order", "Posted Sales Order:" + entity.SoNo, connection, command, transaction);
                    transaction.Commit();
                }
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

        public string GenerateSalesOrderNo(SqlConnection connection, SqlCommand command, SqlTransaction transaction)
        {
            string id = "";
            try
            {
                command = Connection.setTransactionCommand("DECLARE @salesorderno varchar(10) = '' " +
                                                           "DECLARE @newsalesorderno varchar(10) = '' " +
                                                           "DECLARE @date varchar(4) = ((SELECT FORMAT(GETDATE(), 'yy'))+(SELECT FORMAT(GETDATE(),'MM'))) " +
                                                           "DECLARE @prefix varchar(10) = 'SO'+@date " +
                                                           "DECLARE @countable int = (SELECT COUNT(*) FROM TblCtrlNo WITH(READPAST)) " +
                                                           "SET @salesorderno = @prefix+'0001'; " +
                                                           "BEGIN " +
                                                           "   SET @salesorderno = (SELECT TOP 1 SONo FROM TblCtrlNo WITH(READPAST) WHERE CAST(SUBSTRING(SONo,1,6) AS varchar) = @prefix) " +
                                                           "   IF @salesorderno IS NULL " +
                                                           "       BEGIN " +
                                                           "           SET @salesorderno = @prefix+'0001'; " +
                                                           "           IF @countable > 0 " +
                                                           "           BEGIN " +
                                                           "               UPDATE TblCtrlNo " +
                                                           "                   SET SONo = @prefix+'0002' " +
                                                           "           END " +
                                                           "           ELSE " +
                                                           "           BEGIN " +
                                                           "               INSERT INTO TblCtrlNo(SONo) " +
                                                           "                   VALUES(@prefix+'0002') " +
                                                           "           END" +
                                                           "       END " +
                                                           "   ELSE " +
                                                           "       BEGIN " +
                                                           "           SET @newsalesorderno = (SELECT TOP 1 @prefix+ REPLICATE('0',4-LEN(SUBSTRING(@salesorderno,7,4)+1)) + CAST(SUBSTRING(@salesorderno,7,4)+1 AS varchar)) " +
                                                           "           UPDATE TblCtrlNo " +
                                                           "               SET SONo = @newsalesorderno " +
                                                           "       END " +
                                                           "END " +
                                                           "SELECT @salesorderno AS SONo", connection, transaction);
                id = Convert.ToString(command.ExecuteScalar() ?? "");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return id;
        }

        public SalesOrderModel GetUnfinishedOrder()
        {
            SalesOrderModel salesmodel = new SalesOrderModel();
            List<SalesOrderDetailModel> detailslist = new List<SalesOrderDetailModel>();
            try
            {
                connection.Open();
                command = Connection.setCommand($"SELECT a.ItemNo, a.PartNo, b.PartName, c.DescName, e.BrandName, b.Sku, d.UomName, a.FreeItem, a.Qty, a.ListPrice, " +
                                                $"      a.Discount, ((a.ListPrice - a.Discount) * a.Qty) AS NetPrice, a.FreeReason, a.AllowBelCost, a.ItemID " +
                                                $"  FROM TblSalesDet a WITH(READPAST) " +
                                                $"  LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                $"  LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                                                $"  LEFT JOIN TblPartsUomMF d WITH(READPAST) ON d.UomID = b.UomID " +
                                                $"  LEFT JOIN TblPartsBrandMF e WITH(READPAST) ON e.BrandID = b.BrandID " +
                                                $"  WHERE SONo = (SELECT TOP 1 SONo FROM TblSalesMain WITH(READPAST) " +
                                                $"                   WHERE Status=1 " +
                                                $"                      AND ModifiedBy=@CreatedBy " +
                                                $"                   ORDER BY CreatedDt DESC) " +
                                                $"  ORDER BY a.ItemNo", connection);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SalesOrderDetailModel details = new SalesOrderDetailModel();
                    details = new SalesOrderDetailModel
                    {
                        //ItemNo = reader.GetString(0),
                        PartNo = reader.GetString(1).TrimEnd(),
                        PartName = reader.GetString(2).TrimEnd(),
                        DescName = reader.GetString(3).TrimEnd(),
                        BrandName = reader.GetString(4).TrimEnd(),
                        Sku = reader.GetString(5).TrimEnd(),
                        UomName = reader.GetString(6).TrimEnd(),
                        FreeItem = reader.GetBoolean(7),
                        Qty = reader.GetDecimal(8),
                        ListPrice = Convert.ToDouble(reader.GetDecimal(9)),
                        Discount = Convert.ToDouble(reader.GetDecimal(10)),
                        NetPrice = reader.GetDecimal(11),
                        FreeReason = reader.GetString(12).TrimEnd(),
                        AllowBelCost = reader.GetBoolean(13),
                        ItemID = reader.GetString(14).TrimEnd(),
                    };
                    detailslist.Add(details);
                }
                reader.Close();

                command = Connection.setCommand($"SELECT TOP 1 SONo, CONVERT(varchar, ISNULL(SODate, '01/01/2024'), 23) AS SODate, CashTran, CustName, CustAdd, CustTin, " +
                                                $"      TermID, SalesmanID, Remarks " +
                                                $"  FROM TblSalesMain WITH(READPAST) " +
                                                $"  WHERE Status=1 " +
                                                $"      AND ModifiedBy=@CreatedBy " +
                                                $"  ORDER BY CreatedDt DESC", connection);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    salesmodel = new SalesOrderModel
                    {
                        SoNo = reader.GetString(0).TrimEnd(),
                        SoDate = reader.GetString(1),
                        CashTran = reader.GetBoolean(2),
                        CustName = reader.GetString(3).TrimEnd(),
                        CustAdd = reader.GetString(4).TrimEnd(),
                        CustTin = reader.GetString(5).TrimEnd(),
                        TermID = reader.GetString(6).TrimEnd(),
                        SalesmanID = reader.GetString(7).TrimEnd(),
                        Remarks = reader.GetString(8).TrimEnd(),
                        DetailsList = detailslist
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
            return salesmodel;
        }

        public SalesOrderDetailModel GetSuggestedPart(string PartNo)
        {
            SalesOrderDetailModel partDetail = new SalesOrderDetailModel();
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
                    partDetail = new SalesOrderDetailModel
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

        public List<string> CustomerDetailsRead(string id)
        {
            List<string> Details = new List<string>();
            try
            {
                connection.Open();
                command = Connection.setCommand($"SELECT RTRIM(a.NoStreet) + ', ' + RTRIM(c.CityName) + ', ' + RTRIM(b.ProvName) AS CustAdrs, RTRIM(d.TermName), a.TinNo " +
                                                $"  FROM TblSubsidiaryMain a WITH(READPAST) " +
                                                $"  LEFT JOIN TblProvinceMF b WITH(READPAST) ON b.ProvID = a.ProvID " +
                                                $"  LEFT JOIN TblCityMF c WITH(READPAST) ON c.CityID = a.CityID " +
                                                $"  LEFT JOIN TblTermsMF d WITH(READPAST) ON d.TermID = a.TermID " +
                                                $"  WHERE SLID=@SLID", connection);
                command.Parameters.AddWithValue("@SLID", id);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Details.Add(reader.GetString(0) ?? "");
                    Details.Add(reader.GetString(1) ?? "");
                    Details.Add(reader.GetString(2) ?? "");
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
            return Details;
        }

        public decimal GetBoh(string PartNo)
        {
            decimal boh = 0;
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT (SUM(BegBal)+SUM(Rcvd)+SUM(TakeUp)+SUM(TrfIn)+SUM(SReturns))-(SUM(Picked)+SUM(StockDrop)+ " +
                                                "       SUM(PReturns)+SUM(DefReturns)+SUM(TrfOut)) " +
                                                "   FROM TblInvLot WITH(READPAST) " +
                                                "   WHERE PartNo=@PartNo", connection);
                command.Parameters.AddWithValue("@PartNo", PartNo);
                boh = Convert.ToDecimal(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return boh;
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

        public bool AllowBelowCost(decimal netprice, string partno)
        {
            bool allow = false;
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT SUM(AveCost) " +
                                                "   FROM TblInvLot WITH(READPAST) " +
                                                "   WHERE PartNo=@PartNo", connection);
                command.Parameters.AddWithValue("@PartNo", partno);
                decimal avrg = Convert.ToDecimal(command.ExecuteScalar());
                if (netprice < avrg)
                {
                    allow = true;
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
            return allow;
        }

        public string CancelSO (string SONo)
        {
            string message = "Sales order cancelled successfully.";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF EXISTS(SELECT SONo FROM TblSalesMain WITH(READPAST) WHERE SONo=@SONo) " +
                                                           $" BEGIN " +
                                                           $"   UPDATE TblSalesMain " +
                                                           $"       SET Status=9, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                           $"       WHERE SONo=@SONo " +
                                                           $"   UPDATE TblSalesDet " +
                                                           $"       SET Status=9, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                           $"       WHERE SONo=@SONo " +
                                                           $" END", connection, transaction);
                command.Parameters.AddWithValue("@SONo", SONo);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    transaction.Commit();
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
    }
}
