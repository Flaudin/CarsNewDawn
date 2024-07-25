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
    internal class InventoryManagementController : Universal<InventoryManagementModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(InventoryManagementModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(InventoryManagementModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(InventoryManagementModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(InventoryManagementModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(InventoryManagementModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                if (entity.ApplyAll)
                {
                    command = Connection.setTransactionCommand($"UPDATE TblPartsMainMF " +
                                                               $"   SET SGO=@SGO, ReorderPoint=@ReorderPoint, LeadTime=@LeadTime, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE()", connection, transaction);
                }
                else
                {
                    command = Connection.setTransactionCommand($"UPDATE TblPartsMainMF " +
                                                               $"   SET SGO=@SGO, ReorderPoint=@ReorderPoint, LeadTime=@LeadTime, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                               $"   WHERE PartNo=@PartNo", connection, transaction);
                }
                command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                command.Parameters.AddWithValue("@SGO", entity.SGO);
                command.Parameters.AddWithValue("@ReorderPoint", entity.ReorderPoint);
                command.Parameters.AddWithValue("@LeadTime", entity.LeadTime);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                int i = command.ExecuteNonQuery();
                if (i == 0)
                {
                    message = "Something went wrong.";
                    transaction.Rollback();
                    transaction.Dispose();
                    connection.Close();
                    return message;
                }

                //Helper.TranLog("Beginning Balance", "Created new Beginning Balance No:" + BegBalNo, connection, command, transaction);
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

        public DataTable PartsWithBegBalDataTable(InventoryManagementModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT DISTINCT RTRIM(a.PartNo) AS PartNo, RTRIM(b.PartName) AS PartName, RTRIM(b.OtherName) AS OtherName, " +
                                                "       RTRIM(ISNULL(e.DescName,'')) AS DescName, RTRIM(b.Sku) AS Sku, RTRIM(c.UomName) AS UomName, " +
                                                "       RTRIM(d.BrandName) AS BrandName, b.ListPrice, b.ReorderPoint, b.SGO, b.LeadTime " +
                                                "   FROM TblInvLot a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                "   LEFT JOIN TblPartsUomMF c WITH(READPAST) ON c.UomID = b.UomID " +
                                                "   LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                                                "   LEFT JOIN TblPartsDescriptionMF e WITH(READPAST) ON e.DescID = b.DescID " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@PartNo,'') = '' THEN 1 ELSE 0 END) OR a.PartNo LIKE '%' + @PartNo + '%') " +
                                                "       AND(1 = (CASE WHEN ISNULL(@PartName, '') = '' THEN 1 ELSE 0 END) OR b.PartName LIKE '%' + @PartName + '%') " +
                                                "       AND(1 = (CASE WHEN ISNULL(@OtherName, '') = '' THEN 1 ELSE 0 END) OR b.OtherName LIKE '%' + @OtherName + '%') " +
                                                "       AND(1 = (CASE WHEN ISNULL(@Sku, '') = '' THEN 1 ELSE 0 END) OR b.Sku LIKE '%' + @Sku + '%') " +
                                                "       AND(1 = (CASE WHEN ISNULL(@BrandID, '') = '' THEN 1 ELSE 0 END) OR b.BrandID=@BrandID) " +
                                                "       AND(1 = (CASE WHEN ISNULL(@UomID, '') = '' THEN 1 ELSE 0 END) OR b.UomID=@UomID) " +
                                                "       AND(1 = (CASE WHEN ISNULL(@DescID, '') = '' THEN 1 ELSE 0 END) OR b.DescID=@DescID)", connection);
                command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                command.Parameters.AddWithValue("@PartName", entity.PartName);
                command.Parameters.AddWithValue("@OtherName", entity.OtherName);
                command.Parameters.AddWithValue("@Sku", entity.Sku);
                command.Parameters.AddWithValue("@BrandID", entity.Brand);
                command.Parameters.AddWithValue("@UomID", entity.Uom);
                command.Parameters.AddWithValue("@DescID", entity.Description);
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

        public DataTable InventoryDataTable(string PartNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(b.WhName) AS WhName, RTRIM(c.BinName) AS BinName, a.BegBal, a.Rcvd, a.TakeUp, a.SReturns, " +
                                                "       a.Picked, a.StockDrop, a.PReturns, a.DefReturns, a.TrfIn, a.TrfOut " +
                                                "   FROM TblInvLotLoc a WITH(READPAST) " +
                                                "   LEFT JOIN TblWarehouseMF b WITH(READPAST) ON b.WhID = a.WhID " +
                                                "   LEFT JOIN TblWHLocationMF c WITH(READPAST) ON c.BinID = a.BinID " +
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

        public DataTable BegBalDataTable(string PartNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.BegBalNo, CONVERT(VARCHAR, a.CreatedDt, 23) AS CreatedDt,  RTRIM(a.PartNo) AS PartNo, " +
                                                "       RTRIM(d.BinName) AS BinName, RTRIM(c.WhName) AS WhName, b.Qty, a.UnitPrice " +
                                                "   FROM TblInvBegbalDet a WITH(READPAST) " +
                                                "   LEFT JOIN TblInvBegbalDetLoc b WITH(READPAST) ON b.ParentID = a.UniqueID " +
                                                "   LEFT JOIN TblWarehouseMF c WITH(READPAST) ON c.WhID = b.WhID " +
                                                "   LEFT JOIN TblWHLocationMF d WITH(READPAST) ON d.BinID = b.BinID " +
                                                "   WHERE a.PartNo = @PartNo", connection);
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

        public DataTable TakeUpDataTable(string PartNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.AdjNo, CONVERT(VARCHAR, a.CreatedDt, 23) AS CreatedDt, a.TakeUpQty, RTRIM(c.ReasonName) AS ReasonName, " +
                                                "       b.AveCost, (a.TakeUpQty * AveCost) AS Total " +
                                                "   FROM TblInvAdjustsDet a WITH(READPAST) " +
                                                "   LEFT JOIN TblInvLot b WITH(READPAST) ON b.LotNo = a.LotNo AND b.PartNo = a.PartNo " +
                                                "   LEFT JOIN TblReasonMF c WITH(READPAST) ON c.ReasonID = a.ReasonID " +
                                                "   WHERE a.PartNo=@PartNo", connection);
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
        
        public DataTable ReceivedDataTable(string PartNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.RRNo, CONVERT(VARCHAR, a.CreatedDt, 23) AS CreatedDt, SUM(b.Qty) AS Qty, SUM(b.UnitPrice) AS UnitPrice, " +
                                                "       SUM(b.VATAmt) AS VatAmt, SUM((b.UnitPrice - b.VATAmt) * b.Qty) AS TotalPrice " +
                                                "   FROM TblReceivingMain a WITH(READPAST) " +
                                                "   LEFT JOIN TblReceivingDet b WITH(READPAST) ON b.RRNo = a.RRNo " +
                                                "   WHERE b.PartNo=@PartNo AND b.Status = 2 " +
                                                "   GROUP BY a.RRNo, a.CreatedDt", connection);
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
        
        public DataTable DropDataTable(string PartNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.AdjNo, CONVERT(VARCHAR, a.CreatedDt, 23) AS CreatedDt, a.DropQty, RTRIM(c.ReasonName) AS ReasonName, " +
                                                "      b.AveCost, (a.DropQty * AveCost) AS Total " +
                                                "   FROM TblInvAdjustsDet a WITH(READPAST) " +
                                                "   LEFT JOIN TblInvLot b WITH(READPAST) ON b.LotNo = a.LotNo AND b.PartNo = a.PartNo " +
                                                "   LEFT JOIN TblReasonMF c WITH(READPAST) ON c.ReasonID = a.ReasonID " +
                                                "   WHERE a.PartNo = @PartNo " +
                                                "       AND a.DropQty != 0", connection);
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

        public DataTable SalesReturnGoodDataTable(string PartNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT DISTINCT a.SRNo, b.InvoiceNo, RTRIM(c.SLName) AS Customer, d.EmployeeName AS Supplier, a.GoodQty, " +
                                                "       e.NetPrice, (a.GoodQty * e.NetPrice) AS Total " +
                                                "   FROM TblSalesReturnDet a WITH(READPAST) " +
                                                "   LEFT JOIN TblSalesReturnMain b WITH(READPAST) ON b.SRNo = a.SRNo " +
                                                "   LEFT JOIN TblSubsidiaryMain c WITH(READPAST) ON c.SLID = b.SLID " +
                                                "   LEFT JOIN TblEmployeeMF d WITH(READPAST) ON d.EmployeeID = b.SalesmanID " +
                                                "   LEFT JOIN TblSalesDet e WITH(READPAST) ON e.ItemID = a.ItemID " +
                                                "   WHERE a.PartNo=@PartNo " +
                                                "       AND a.GoodQty != 0", connection);
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
        
        public DataTable SalesDataTable(string PartNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.Status AS MainStatus, a.SONo, CONVERT(varchar, a.SODate, 23) AS SODate, a.InvoiceNo, " +
                                                "       CONVERT(varchar, a.InvoiceDate, 23) AS InvoiceDate, a.InvoiceRefNo, RTRIM(a.CustName) AS CustName, " +
                                                "       RTRIM(d.SLName) AS SLName, RTRIM(b.TermName) AS TermName, " +
                                                "       SUM(c.Qty) AS Qty, SUM(c.ListPrice) as ListPrice, SUM(c.Discount) AS Discount, " +
                                                "       SUM((c.ListPrice - c.Discount) * c.Qty) AS TotalPrice" +
                                                "   FROM TblSalesMain a WITH(READPAST) " +
                                                "   LEFT JOIN TblTermsMF b WITH(READPAST) ON b.TermID = a.TermID " +
                                                "   LEFT JOIN TblSalesDet c WITH(READPAST) ON c.SONo = a.SONo " +
                                                "   LEFT JOIN TblSubsidiaryMain d WITH(READPAST) ON d.SLID = a.SLID " +
                                                "   WHERE a.Status IN(1, 2, 3) " +
                                                "       AND c.PartNo=@PartNo " +
                                                "   GROUP BY a.Status, a.SONo, a.SODate, a.InvoiceNo, a.InvoiceDate, a.InvoiceRefNo, a.CustName, d.SLName, b.TermName, a.CustAdd, a.CustTin ", connection);
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

        public DataTable SalesReturnDefectiveDataTable(string PartNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT DISTINCT a.SRNo, b.InvoiceNo, RTRIM(c.SLName) AS Customer, d.EmployeeName AS Supplier, " +
                                                "       a.DefectiveQty, e.NetPrice, (a.DefectiveQty * e.NetPrice) AS Total " +
                                                "   FROM TblSalesReturnDet a WITH(READPAST) " +
                                                "   LEFT JOIN TblSalesReturnMain b WITH(READPAST) ON b.SRNo = a.SRNo " +
                                                "   LEFT JOIN TblSubsidiaryMain c WITH(READPAST) ON c.SLID = b.SLID " +
                                                "   LEFT JOIN TblEmployeeMF d WITH(READPAST) ON d.EmployeeID = b.SalesmanID " +
                                                "   LEFT JOIN TblSalesDet e WITH(READPAST) ON e.ItemID = a.ItemID " +
                                                "   WHERE a.PartNo=@PartNo " +
                                                "       AND a.DefectiveQty != 0", connection);
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

        public DataTable ListPriceDataTable(string PartNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT ControlNo, CASE WHEN ModifiedDt IS NULL THEN CONVERT(varchar, CreatedDt, 23) ELSE CONVERT(varchar, ModifiedDt, 23) END AS PriceDate, " +
                                                "       CONVERT(varchar, CreatedDt, 23) AS CreatedDt, CONVERT(varchar, ModifiedDt, 23) AS ModifiedDt, " +
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

        public DataTable PurchaseReturnDataTable(string PartNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.PurchRetNo, CONVERT(VARCHAR, a.CreatedDt, 23) AS CreatedDt, a.RRNo, b.Qty, c.ListPrice, " +
                                                "       RTRIM(d.ReasonName) AS ReasonName " +
                                                "   FROM TblPurchaseReturnMain a WITH(READPAST) " +
                                                "   LEFT JOIN TblPurchaseReturnDet b WITH(READPAST) ON b.PurchRetNo = a.PurchRetNo " +
                                                "   LEFT JOIN TblPartsMainMF c WITH(READPAST) ON c.PartNo = b.PartNo " +
                                                "   LEFT JOIn TblReasonMF d WITH(READPAST) ON d.ReasonID = a.ReasonID " +
                                                "   WHERE b.PartNo=@PartNo", connection);
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

        public List<decimal> InventoryMovement(string PartNo)
        {
            List<decimal> listmovement = new List<decimal>();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT SUM(BegBal), SUM(Rcvd), SUM(TakeUp), SUM(SReturns), SUM(Picked), SUM(StockDrop), " +
                                                "       SUM(PReturns), SUM(DefReturns), SUM(TrfIn), SUM(TrfOut) " +
                                                "   FROM TblInvLot WITH(READPAST) " +
                                                "   WHERE PartNo=@PartNo", connection);
                command.Parameters.AddWithValue("@PartNo", PartNo);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listmovement.Add(Convert.ToDecimal(reader[0]));
                    listmovement.Add(Convert.ToDecimal(reader[1]));
                    listmovement.Add(Convert.ToDecimal(reader[2]));
                    listmovement.Add(Convert.ToDecimal(reader[3]));
                    listmovement.Add(Convert.ToDecimal(reader[4]));
                    listmovement.Add(Convert.ToDecimal(reader[5]));
                    listmovement.Add(Convert.ToDecimal(reader[6]));
                    listmovement.Add(Convert.ToDecimal(reader[7]));
                    listmovement.Add(Convert.ToDecimal(reader[8]));
                    listmovement.Add(Convert.ToDecimal(reader[9]));
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
            return listmovement;
        }

        public List<decimal> Setup(string PartNo)
        {
            List<decimal> setup = new List<decimal>();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT ISNULL(SGO,0.0000), ISNULL(ReorderPoint,0.0000), ISNULL(LeadTime,0.0) " +
                                                "   FROM TblPartsMainMF WITH(READPAST) " +
                                                "   WHERE PartNo=@PartNo", connection);
                command.Parameters.AddWithValue("@PartNo", PartNo);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    setup.Add(Convert.ToDecimal(reader[0]));
                    setup.Add(Convert.ToDecimal(reader[1]));
                    setup.Add(Convert.ToDecimal(reader[2]));
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
            return setup;
        }

        public List<decimal> SalesQty(string PartNo)
        {
            List<decimal> quantities = new List<decimal>();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) THEN Qty ELSE 0 END),0) AS Month1, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 1 THEN Qty ELSE 0 END),0) AS Month2, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 2 THEN Qty ELSE 0 END),0) AS Month3, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 3 THEN Qty ELSE 0 END),0) AS Month4, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 4 THEN Qty ELSE 0 END),0) AS Month5, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 5 THEN Qty ELSE 0 END),0) AS Month6, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 6 THEN Qty ELSE 0 END),0) AS Month7, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 7 THEN Qty ELSE 0 END),0) AS Month8, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 8 THEN Qty ELSE 0 END),0) AS Month9, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 9 THEN Qty ELSE 0 END),0) AS Month10, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 10 THEN Qty ELSE 0 END),0) AS Month11, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 11 THEN Qty ELSE 0 END),0) AS Month1 " +
                                                "   FROM TblSalesDet " +
                                                "   WHERE CreatedDt >= DATEADD(MONTH, -12, GETDATE()) " +
                                                "       AND PartNo=@PartNo", connection);
                command.Parameters.AddWithValue("@PartNo", PartNo);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    quantities.Add(Convert.ToDecimal(reader[0]));
                    quantities.Add(Convert.ToDecimal(reader[1]));
                    quantities.Add(Convert.ToDecimal(reader[2]));
                    quantities.Add(Convert.ToDecimal(reader[3]));
                    quantities.Add(Convert.ToDecimal(reader[4]));
                    quantities.Add(Convert.ToDecimal(reader[5]));
                    quantities.Add(Convert.ToDecimal(reader[6]));
                    quantities.Add(Convert.ToDecimal(reader[7]));
                    quantities.Add(Convert.ToDecimal(reader[8]));
                    quantities.Add(Convert.ToDecimal(reader[9]));
                    quantities.Add(Convert.ToDecimal(reader[10]));
                    quantities.Add(Convert.ToDecimal(reader[11]));
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
            return quantities;
        }

        public List<decimal> SalesValue(string PartNo)
        {
            List<decimal> values = new List<decimal>();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) THEN NetPrice ELSE 0 END),0) AS Month1, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 1 THEN NetPrice ELSE 0 END),0) AS Month2, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 2 THEN NetPrice ELSE 0 END),0) AS Month3, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 3 THEN NetPrice ELSE 0 END),0) AS Month4, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 4 THEN NetPrice ELSE 0 END),0) AS Month5, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 5 THEN NetPrice ELSE 0 END),0) AS Month6, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 6 THEN NetPrice ELSE 0 END),0) AS Month7, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 7 THEN NetPrice ELSE 0 END),0) AS Month8, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 8 THEN NetPrice ELSE 0 END),0) AS Month9, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 9 THEN NetPrice ELSE 0 END),0) AS Month10, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 10 THEN NetPrice ELSE 0 END),0) AS Month11, " +
                                                "       ISNULL(SUM(CASE WHEN YEAR(CreatedDt) = YEAR(GETDATE()) AND MONTH(CreatedDt) = MONTH(GETDATE()) - 11 THEN NetPrice ELSE 0 END),0) AS Month1 " +
                                                "   FROM TblSalesDet " +
                                                "   WHERE CreatedDt >= DATEADD(MONTH, -12, GETDATE()) " +
                                                "       AND PartNo=@PartNo", connection);
                command.Parameters.AddWithValue("@PartNo", PartNo);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    values.Add(Convert.ToDecimal(reader[0]));
                    values.Add(Convert.ToDecimal(reader[1]));
                    values.Add(Convert.ToDecimal(reader[2]));
                    values.Add(Convert.ToDecimal(reader[3]));
                    values.Add(Convert.ToDecimal(reader[4]));
                    values.Add(Convert.ToDecimal(reader[5]));
                    values.Add(Convert.ToDecimal(reader[6]));
                    values.Add(Convert.ToDecimal(reader[7]));
                    values.Add(Convert.ToDecimal(reader[8]));
                    values.Add(Convert.ToDecimal(reader[9]));
                    values.Add(Convert.ToDecimal(reader[10]));
                    values.Add(Convert.ToDecimal(reader[11]));
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
            return values;
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
                else if (Type == "Description")
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
