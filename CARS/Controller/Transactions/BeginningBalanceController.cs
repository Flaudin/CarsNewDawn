using CARS.Functions;
using CARS.Model;
using CARS.Model.Masterfiles;
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
    internal class BeginningBalanceController : Universal<BeginningBalanceModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(BeginningBalanceModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                string BegBalNo = GenerateBeginningBalanceID(connection, command, transaction);
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT BegBalNo FROM TblInvBegbalMain WITH(READPAST) WHERE BegBalNo=@BegBalNo) " +
                                                           $"BEGIN " +
                                                           $"   INSERT INTO TblInvBegbalMain(BegBalNo, Remarks, Status, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                           $"       VALUES(@BegBalNo, @Remarks, @Status, @CreatedBy, GETDATE(), @CreatedBy, GETDATE()) " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@BegBalNo", BegBalNo);
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
                        int j = 0;
                        command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT UniqueID FROM TblInvBegbalDet WITH(READPAST) WHERE BegBalNo=@BegBalNo AND PartNo=@PartNo) " +
                                                                   $"BEGIN " +
                                                                   $"   INSERT INTO TblInvBegbalDet(BegBalNo, UniqueID, PartNo, Qty, UnitPrice, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                                   $"       VALUES(@BegBalNo, @UniqueID, @PartNo, @Qty, @UnitPrice, @CreatedBy, GETDATE(), @CreatedBy, GETDATE()) " +
                                                                   $"END", connection, transaction);
                        command.Parameters.AddWithValue("@BegBalNo", BegBalNo);
                        command.Parameters.AddWithValue("@UniqueID", data.UniqueID);
                        command.Parameters.AddWithValue("@PartNo", data.PartNo);
                        command.Parameters.AddWithValue("@Qty", data.Qty);
                        command.Parameters.AddWithValue("@UnitPrice", data.UnitPrice);
                        command.Parameters.AddWithValue("@CreatedBy", Name01);
                        j = command.ExecuteNonQuery();

                        int k = 0;
                        command = Connection.setTransactionCommand($"INSERT INTO TblInvLot(PartNo, CtrlNo, LotNo, RecdDt, Rcvd, BegBal, AveCost, UnitCost, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                                   $"   VALUES(@PartNo, @CtrlNo, @LotNo, GETDATE(), 0, @BegBal, @AveCost, @UnitCost, @CreatedBy, GETDATE(), @CreatedBy, GETDATE())", connection, transaction);
                        command.Parameters.AddWithValue("@PartNo", data.PartNo);
                        command.Parameters.AddWithValue("@CtrlNo", BegBalNo);
                        command.Parameters.AddWithValue("@LotNo", BegBalNo + data.PartNo);
                        command.Parameters.AddWithValue("@BegBal", data.Qty);
                        command.Parameters.AddWithValue("@AveCost", data.UnitPrice);
                        command.Parameters.AddWithValue("@UnitCost", data.UnitPrice);
                        command.Parameters.AddWithValue("@CreatedBy", Name01);
                        k = command.ExecuteNonQuery();
                        if (j > 0 && k > 0)
                        {
                            Helper.TranLog("BegBal Details", "Added new Part:" + data.PartNo + " for Beginning Balance No:" + BegBalNo, connection, command, transaction);
                            Helper.TranLog("Inventory Lot", "Added new Part:" + data.PartNo + " with Control No:" + BegBalNo, connection, command, transaction);

                            foreach (var loc in data.LocationsList)
                            {
                                int l = 0;
                                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT ParentID FROM TblInvBegbalDetLoc WITH(READPAST) " +
                                                                           $"               WHERE BegBalNo=@BegBalNo AND ParentID=@ParentID AND PartNo=@PartNo AND WhID=@WhID AND BinID=@BinID) " +
                                                                           $"BEGIN " +
                                                                           $"   INSERT INTO TblInvBegbalDetLoc(BegBalNo, ParentID, PartNo, WhID, BinID, Qty, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                                           $"       VALUES(@BegBalNo, @ParentID, @PartNo, @WhID, @BinID, @Qty, @CreatedBy, GETDATE(), @CreatedBy, GETDATE()) " +
                                                                           $"END", connection, transaction);
                                command.Parameters.AddWithValue("@BegBalNo", BegBalNo);
                                command.Parameters.AddWithValue("@ParentID", data.UniqueID);
                                command.Parameters.AddWithValue("@PartNo", data.PartNo);
                                command.Parameters.AddWithValue("@WhID", loc.WhID);
                                command.Parameters.AddWithValue("@BinID", loc.BinID);
                                command.Parameters.AddWithValue("@Qty", loc.Qty);
                                command.Parameters.AddWithValue("@CreatedBy", Name01);
                                l = command.ExecuteNonQuery();

                                int m = 0;
                                command = Connection.setTransactionCommand($"INSERT INTO TblInvLotLoc(PartNo, CtrlNo, LotNo, WhID, BinID, Rcvd, BegBal, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                                           $"       VALUES(@PartNo, @CtrlNo, @LotNo, @WhID, @BinID, 0, @BegBal, @CreatedBy, GETDATE(), @CreatedBy, GETDATE())", connection, transaction);
                                command.Parameters.AddWithValue("@PartNo", data.PartNo);
                                command.Parameters.AddWithValue("@CtrlNo", BegBalNo);
                                command.Parameters.AddWithValue("@LotNo", BegBalNo + data.PartNo);
                                command.Parameters.AddWithValue("@WhID", loc.WhID);
                                command.Parameters.AddWithValue("@BinID", loc.BinID);
                                command.Parameters.AddWithValue("@BegBal", loc.Qty);
                                command.Parameters.AddWithValue("@CreatedBy", Name01);
                                m = command.ExecuteNonQuery();
                                if (l > 0 && m > 0)
                                {
                                    Helper.TranLog("BegBal Location", "Added new Part:" + data.PartNo + " to Warehouse:" + loc.WhID + ", Bin:" + loc.BinID, connection, command, transaction);
                                    Helper.TranLog("Inventory LotLoc", "Added new Part:" + data.PartNo + " with Control No:" + BegBalNo, connection, command, transaction);
                                }
                                else
                                {
                                    message = "The beginning balance location entered is already present in the database.";
                                    transaction.Rollback();
                                    connection.Close();
                                    return message;
                                }
                            }
                        }
                        else
                        {
                            message = "The beginning balance details entered is already present in the database.";
                            transaction.Rollback();
                            connection.Close();
                            return message;
                        }
                    }
                }
                Helper.TranLog("Beginning Balance", "Created new Beginning Balance No:" + BegBalNo, connection, command, transaction);
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

        public override void Delete(BeginningBalanceModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(BeginningBalanceModel entity)
        {
            throw new NotImplementedException();
        }

        //Archive
        public DataTable BegBalDataTable (string begbalno, string fromdt, string todt)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT BegBalNo,RTRIM(Remarks) AS Remarks, CONVERT(varchar, CreatedDt, 23) AS CreatedDt " +
                                                "   FROM TblInvBegbalMain WITH(READPAST) " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@BegBalNo,'') = '' THEN 1 ELSE 0 END) OR BegBalNo LIKE '%' + @BegBalNo + '%') " +
                                                "       AND CreatedDt BETWEEN DATEADD(day, -1, @DateFrom) AND DATEADD(day, 1, @DateTo)", connection);
                command.Parameters.AddWithValue("@BegBalNo", begbalno);
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

        public DataTable PartsArchiveDataTable(string BegBalNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.UniqueID, RTRIM(a.PartNo) AS PartNo, RTRIM(b.PartName) AS PartName, RTRIM(b.OtherName) AS OtherName, " +
                                                "       RTRIM(c.DescName) AS DescName, RTRIM(d.BrandName) AS BrandName, RTRIM(e.UomName) AS UomName, a.UnitPrice, a.Qty " +
                                                "   FROM TblInvBegbalDet a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                "   LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                                                "   LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                                                "   LEFT JOIN TblPartsUomMF e WITH(READPAST) ON e.UomID = b.UomID " +
                                                "   WHERE a.BegBalNo=@BegBalNo", connection);
                command.Parameters.AddWithValue("@BegBalNo", BegBalNo);
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
        public DataTable LocationArchiveDataTable(string BegBalNo, string ParentID)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(b.BinName) AS BinName, RTRIM(c.WhName) AS WhName, RTRIM(c.WhLocation) AS WhLocation, a.Qty AS QtyLocation " +
                                                "   FROM TblInvBegbalDetLoc a WITH(READPAST) " +
                                                "   LEFT JOIN TblWHLocationMF b WITH(READPAST) ON b.BinID = a.BinID " +
                                                "   LEFT JOIN TblWarehouseMF c WITH(READPAST) ON c.WhID = b.WhID " +
                                                "   WHERE a.ParentID=@ParentID", connection);
                command.Parameters.AddWithValue("@ParentID", ParentID);
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

        public DataTable PartsDataTable()
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(a.PartNo) AS PartNo, RTRIM(a.PartName) AS PartName, RTRIM(a.OtherName) AS OtherName, RTRIM(b.DescName) AS DescName, " +
                                                "       RTRIM(c.BrandName) AS BrandName, RTRIM(d.UomName) AS UomName, a.ListPrice " +
                                                "   FROM TblPartsMainMF a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsDescriptionMF b WITH(READPAST) ON b.DescID = a.DescID " +
                                                "   LEFT JOIN TblPartsBrandMF c WITH(READPAST) ON c.BrandID = a.BrandID " +
                                                "   LEFT JOIN TblPartsUomMF d WITH(READPAST) ON d.UomID = a.UomID " +
                                                "   WHERE a.IsActive = 1", connection);
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

        public DataTable LocationDataTable(string PartNo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(a.BinName) AS BinName, RTRIM(b.WhName) AS WhName, a.BinID, a.WhID, RTRIM(b.WhLocation) AS WhLocation " +
                                                "   FROM TblWHLocationMF a WITH(READPAST) " +
                                                "   LEFT JOIN TblWarehouseMF b WITH(READPAST) ON b.WhID = a.WhID" +
                                                "   WHERE a.IsActive = 1 " +
                                                "       AND ISNULL(b.IsWebStore, 0) = 0 " +
                                                "       AND NOT EXISTS(SELECT c.BinID, c.WhID FROM TblInvLotLoc c WITH(READPAST) WHERE c.PartNo=@PartNo AND c.BinID = a.BinID AND c.WhID = a.WhID)", connection);
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

        public string GenerateBeginningBalanceID(SqlConnection connection, SqlCommand command, SqlTransaction transaction)
        {
            string id = "";
            try
            {
                command = Connection.setTransactionCommand("DECLARE @begbalno varchar(10) = '' " +
                                                           "DECLARE @newbegbalno varchar(10) = '' " +
                                                           "DECLARE @date varchar(4) = ((SELECT FORMAT(GETDATE(), 'yy'))+(SELECT FORMAT(GETDATE(),'MM'))) " +
                                                           "DECLARE @prefix varchar(10) = 'BB'+@date " +
                                                           "DECLARE @countable int = (SELECT COUNT(*) FROM TblCtrlNo WITH(READPAST)) " +
                                                           "SET @begbalno = @prefix+'0001'; " +
                                                           "BEGIN " +
                                                           "   SET @begbalno = (SELECT TOP 1 WhBegbalNo FROM TblCtrlNo WITH(READPAST) WHERE CAST(SUBSTRING(WhBegbalNo,1,6) AS varchar) = @prefix) " +
                                                           "   IF @begbalno IS NULL " +
                                                           "       BEGIN " +
                                                           "           SET @begbalno = @prefix+'0001'; " +
                                                           "           IF @countable > 0 " +
                                                           "           BEGIN " +
                                                           "               UPDATE TblCtrlNo " +
                                                           "                   SET WhBegbalNo = @prefix+'0002' " +
                                                           "           END " +
                                                           "           ELSE " +
                                                           "           BEGIN " +
                                                           "               INSERT INTO TblCtrlNo(WhBegbalNo) " +
                                                           "                   VALUES(@prefix+'0002') " +
                                                           "           END" +
                                                           "       END " +
                                                           "   ELSE " +
                                                           "       BEGIN " +
                                                           "           SET @newbegbalno = (SELECT TOP 1 @prefix+ REPLICATE('0',4-LEN(SUBSTRING(@begbalno,7,4)+1)) + CAST(SUBSTRING(@begbalno,7,4)+1 AS varchar)) " +
                                                           "           UPDATE TblCtrlNo " +
                                                           "               SET WhBegbalNo = @newbegbalno " +
                                                           "       END " +
                                                           "END " +
                                                           "SELECT @begbalno AS BegBalNo", connection, transaction);
                id = Convert.ToString(command.ExecuteScalar() ?? "");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return id;
        }

        public override void Read(BeginningBalanceModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(BeginningBalanceModel entity)
        {
            throw new NotImplementedException();
        }

        public List<BeginningBalancePrint> PrintBeginningBalance(string BeginningBalanceNo)
        {
            List<BeginningBalancePrint> beginningBalancePrint = new List<BeginningBalancePrint>();
            try
            {
                connection.Open();
                command = Connection.setCommand($"SELECT a.PartNo, c.WhName, d.BinName, b.Qty, a.UnitPrice " +
                                                $"  FROM TblInvBegbalDet a WITH(READPAST) " +
                                                $"  LEFT JOIN TblInvBegbalDetLoc b WITH(READPAST) ON b.ParentID = a.UniqueID " +
                                                $"  LEFT JOIN TblWarehouseMF c WITH(READPAST) ON c.WhID = b.WhID " +
                                                $"  LEFT JOIN TblWHLocationMF d WITH(READPAST) ON d.BinID = b.BinID " +
                                                $"  WHERE a.BegBalNo=@BegBalNo", connection);
                command.Parameters.AddWithValue("@BegBalNo", BeginningBalanceNo);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    BeginningBalancePrint details = new BeginningBalancePrint()
                    {
                        PartNo = reader.GetString(0).TrimEnd(),
                        WhName = reader.GetString(1).TrimEnd(),
                        BinName = reader.GetString(2).TrimEnd(),
                        Qty = reader.GetDecimal(3),
                        UnitPrice = reader.GetDecimal(4),
                    };
                    beginningBalancePrint.Add(details);
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
            return beginningBalancePrint;
        }
    }
}
