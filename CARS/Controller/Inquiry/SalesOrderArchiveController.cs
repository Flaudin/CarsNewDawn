using CARS.Model;
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
    internal class SalesOrderArchiveController : Universal<SalesOrderArchiveModel>
    {
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(SalesOrderArchiveModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(SalesOrderArchiveModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(SalesOrderArchiveModel entity)
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
                command.Parameters.AddWithValue("@SONo", entity.SONo);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    if (entity.DetailsList != null)
                    {
                        foreach (var data in entity.DetailsList)
                        {
                            DataTable LocationTable = new DataTable();
                            command = Connection.setTransactionCommand($"SELECT LotNo, WhID, BinID, Qty " +
                                                                       $"   FROM TblSalesDetLoc WITH(READPAST) " +
                                                                       $"   WHERE SONo=@SONo " +
                                                                       $"       AND PartNo=@PartNo " +
                                                                       $"       AND ItemID=@ItemID", connection, transaction);
                            command.Parameters.AddWithValue("@SONo", entity.SONo);
                            command.Parameters.AddWithValue("@PartNo", data.PartNo);
                            command.Parameters.AddWithValue("@ItemID", data.ItemID);
                            reader = command.ExecuteReader();
                            LocationTable.Load(reader);
                            foreach (DataRow row in LocationTable.Rows)
                            {
                                command = Connection.setTransactionCommand($"UPDATE TblInvLot " +
                                                                           $"   SET Picked = Picked - @QtyTotal " +
                                                                           $"   WHERE PartNo=@PartNo " +
                                                                           $"       AND LotNo=@LotNo " +
                                                                           $"UPDATE TblInvLotLoc " +
                                                                           $"   SET Picked = Picked - @Qty " +
                                                                           $"   WHERE PartNo=@PartNo " +
                                                                           $"       AND LotNo=@LotNo " +
                                                                           $"       AND WhID=@WhID " +
                                                                           $"       AND BinID=@BinID ", connection, transaction);
                                command.Parameters.AddWithValue("@PartNo", data.PartNo);
                                command.Parameters.AddWithValue("@LotNo", row["LotNo"].ToString().TrimEnd());
                                command.Parameters.AddWithValue("@WhID", row["WhID"].ToString().TrimEnd());
                                command.Parameters.AddWithValue("@BinID", row["BinID"].ToString().TrimEnd());
                                command.Parameters.AddWithValue("@QtyTotal", data.Qty);
                                command.Parameters.AddWithValue("@Qty", Convert.ToDecimal(row["Qty"]));
                                i = command.ExecuteNonQuery();
                                if (i == 0)
                                {
                                    message = "Something went wrong.";
                                    transaction.Rollback();
                                    transaction.Dispose();
                                    connection.Close();
                                    return message;
                                }
                            }
                        }
                    }
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

        public override void Delete(SalesOrderArchiveModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(SalesOrderArchiveModel entity)
        {
            throw new NotImplementedException();
        }

        public DataTable SearchSalesArchive(string customer, string invoice, string order, string reference, string dateFrom, string dateTo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.Status AS MainStatus, a.SONo, CONVERT(varchar, a.SODate, 23) AS SODate, a.InvoiceNo, CONVERT(varchar, a.InvoiceDate, 23) AS InvoiceDate, " +
                                                "       a.InvoiceRefNo, RTRIM(a.CustName) AS CustName, RTRIM(d.SLName) AS SLName, RTRIM(b.TermName) AS TermName, " +
                                                "       SUM((c.ListPrice - c.Discount) * Qty) AS TotalPrice, RTRIM(a.CustAdd) AS CustAdd, a.CustTin " +
                                                "   FROM TblSalesMain a WITH(READPAST) " +
                                                "   LEFT JOIN TblTermsMF b WITH(READPAST) ON b.TermID = a.TermID " +
                                                "   LEFT JOIN TblSalesDet c WITH(READPAST) ON c.SONo = a.SONo " +
                                                "   LEFT JOIN TblSubsidiaryMain d WITH(READPAST) ON d.SLID = a.SLID " +
                                                "   WHERE a.Status != 1 " +
                                                "       AND (1=(CASE WHEN ISNULL(@CustName,'') = '' THEN 1 ELSE 0 END) OR a.CustName LIKE '%' + @CustName + '%' ) " +
                                                "       AND (1=(CASE WHEN ISNULL(@SONo,'') = '' THEN 1 ELSE 0 END) OR a.SONo LIKE '%' + @SONo + '%' ) " +
                                                "       AND (1=(CASE WHEN ISNULL(@InvoiceNo, '') = '' THEN 1 ELSE 0 END) OR a.InvoiceNo LIKE '%' + @InvoiceNo + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@InvoiceRefNo, '') = '' THEN 1 ELSE 0 END) OR a.InvoiceRefNo LIKE '%' + @InvoiceRefNo + '%') " +
                                                "       AND a.SODate BETWEEN DATEADD(day, -1, @DateFrom) AND DATEADD(day, 1, @DateTo) " +
                                                "   GROUP BY a.Status, a.SONo, a.SODate, a.InvoiceNo, a.InvoiceDate, a.InvoiceRefNo, a.CustName, d.SLName, b.TermName, a.CustAdd, a.CustTin ", connection);
                command.Parameters.AddWithValue("@CustName", customer);
                command.Parameters.AddWithValue("@SONo", order);
                command.Parameters.AddWithValue("@InvoiceNo", invoice);
                command.Parameters.AddWithValue("@InvoiceRefNo", reference);
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

        public DataTable SalesDetailsArchive(string SONo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.ItemNo, RTRIM(a.PartNo) AS PartNo, RTRIM(b.PartName) AS PartName, a.Qty, c.UomName, a.ListPrice, " +
                                                "       a.Discount, a.NetPrice * a.Qty AS TotalAmount, a.ItemID, a.FreeItem, a.AllowBelCost " +
                                                "   FROM TblSalesDet a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                "   LEFT JOIN TblPartsUomMF c WITH(READPAST) ON c.UomID = b.UomID " +
                                                "   WHERE a.SONo=@SONo " +
                                                "   ORDER BY ItemNo", connection);
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

        public DataTable SalesLocationArchive(string ItemID)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT c.BinName, b.WhName, a.LotNo, a.Qty AS LocationQty " +
                                                "   FROM TblSalesDetLoc a WITH(READPAST) " +
                                                "   LEFT JOIN TblWarehouseMF b WITH(READPAST) ON b.WhID = a.WhID " +
                                                "   LEFT JOIN TblWHLocationMF c WITH(READPAST) ON c.BinID = a.BinID " +
                                                "   WHERE a.ItemID=@ItemID", connection);
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
