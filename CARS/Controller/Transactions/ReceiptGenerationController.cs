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
    internal class ReceiptGenerationController : Universal<ReceiptGenerationModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(ReceiptGenerationModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(ReceiptGenerationModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(ReceiptGenerationModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.SONo, CONVERT(varchar, a.SODate, 23) AS SODate, a.InvoiceRefNo, RTRIM(a.CustName) AS CustName, " +
                                                "       RTRIM(a.CustAdd) AS CustAdd, a.CustTin, d.EmployeeName AS Salesman, RTRIM(b.TermName) AS TermName, " +
                                                "       RTRIM(a.Remarks) AS Remarks, SUM((c.ListPrice - c.Discount) * Qty) AS NetPrice " +
                                                "   FROM TblSalesMain a WITH(READPAST) " +
                                                "   LEFT JOIN TblTermsMF b WITH(READPAST) ON b.TermID = a.TermID " +
                                                "   LEFT JOIN TblSalesDet c WITH(READPAST) ON c.SONo = a.SONo " +
                                                "   LEFT JOIN TblEmployeeMF d WITH(READPAST) ON d.EmployeeID = a.SalesmanID " +
                                                "   WHERE a.Status IN (2) " +
                                                "       AND (1=(CASE WHEN ISNULL(@SONo,'') = '' THEN 1 ELSE 0 END) OR a.SONo LIKE '%' + @SONo + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@CustName,'') = '' THEN 1 ELSE 0 END) OR a.CustName LIKE '%' + @CustName + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@TermID,'') = '' THEN 1 ELSE 0 END) OR a.TermID LIKE '%' + @TermID + '%') " +
                                                "       AND a.SODate BETWEEN DATEADD(day, -1, @DateFrom) AND DATEADD(day, 1, @DateTo) " +
                                                "   GROUP BY a.SONo, a.SODate,a.InvoiceRefNo, a.CustName, a.CustAdd, a.CustTin, d.EmployeeName, b.TermName, a.Remarks", connection);
                command.Parameters.AddWithValue("@SONo", entity.SONo);
                command.Parameters.AddWithValue("@CustName", entity.CustomerName);
                command.Parameters.AddWithValue("@TermID", entity.CreditTerm);
                command.Parameters.AddWithValue("@DateFrom", entity.DateFrom);
                command.Parameters.AddWithValue("@DateTo", entity.DateTo);
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

        public override void Read(ReceiptGenerationModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(ReceiptGenerationModel entity)
        {
            throw new NotImplementedException();
        }

        public DataTable PartTable(string SONo)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.ItemNo, RTRIM(a.PartNo) AS PartNo, RTRIM(b.PartName) AS PartName, RTRIM(d.DescName) AS DescName, " +
                                                "       RTRIM(e.BrandName) AS BrandName, RTRIM(b.Sku) AS Sku, c.UomName, a.FreeItem, a.Qty, a.ListPrice AS SRP, " +
                                                "       a.Discount, a.NetPrice AS NetPricePart, a.NetPrice * a.Qty AS TotalAmount " +
                                                "   FROM TblSalesDet a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                "   LEFT JOIN TblPartsUomMF c WITH(READPAST) ON c.UomID = b.UomID " +
                                                "   LEFT JOIN TblPartsDescriptionMF d WITH(READPAST) ON d.DescID = b.DescID " +
                                                "   LEFT JOIN TblPartsBrandMF e WITH(READPAST) ON e.BrandID = b.BrandID " +
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

        public SalesOrderReportCustomer GetCustomerSalesOrder(string SONo)
        {
            SalesOrderReportCustomer SalesInfo = new SalesOrderReportCustomer();
            List<SalesOrderReportParts> partslist = new List<SalesOrderReportParts>();
            try
            {
                connection.Open();
                command = Connection.setCommand($"SELECT CONVERT(varchar,a.ItemNo), a.PartNo, b.PartName, a.Qty, c.UomName, a.NetPrice, a.NetPrice * a.Qty AS TotalAmount, a.VATAmt " +
                                                $"  FROM TblSalesDet a WITH(READPAST) " +
                                                $"  LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                $"  LEFT JOIN TblPartsUomMF c WITH(READPAST) ON c.UomID = b.UomID " +
                                                $"  WHERE a.SONo=@SONo " +
                                                $"  ORDER BY ItemNo", connection);
                command.Parameters.AddWithValue("@SONo", SONo);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SalesOrderReportParts part = new SalesOrderReportParts();
                    part = new SalesOrderReportParts
                    {
                        ItemNo = reader.GetString(0),
                        PartNo = reader.GetString(1).TrimEnd(),
                        PartName = reader.GetString(2).TrimEnd(),
                        Qty = reader.GetDecimal(3),
                        UomName = reader.GetString(4).TrimEnd(),
                        NetPrice = reader.GetDecimal(5),
                        TotalAmount = reader.GetDecimal(6),
                        VATAmt = reader.GetDecimal(7),
                    };
                    partslist.Add(part);
                }
                reader.Close();

                command = Connection.setCommand($"SELECT a.InvoiceNo, a.CustName, " +
                                                $"      CASE WHEN ISNULL(a.SLID,'') != '' THEN RTRIM(b.NoStreet) + ' ' + RTRIM(d.CityName) + ', ' + RTRIM(c.ProvName) ELSE a.CustAdd END AS Address, " +
                                                $"      CASE WHEN ISNULL(a.SLID,'') != '' THEN b.TinNo ELSE a.CustTin END AS TinNo, " +
                                                $"      CASE WHEN ISNULL(a.SLID,'') != '' THEN b.RegName ELSE a.CustName END AS RegName, e.TermName" +
                                                $"  FROM TblSalesMain a WITH(READPAST) " +
                                                $"  LEFT JOIN TblSubsidiaryMain b WITH(READPAST) ON b.SLID = a.SLID " +
                                                $"  LEFT JOIN TblProvinceMF c WITH(READPAST) ON c.ProvID = b.ProvID " +
                                                $"  LEFT JOIN TblCityMF d WITH(READPAST) ON d.CityID = b.CityID " +
                                                $"  LEFT JOIN TblTermsMF e WITH(READPAST) ON e.TermID = a.TermID " +
                                                $"  WHERE a.SONo=@SONo", connection);
                command.Parameters.AddWithValue("@SONo", SONo);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SalesInfo = new SalesOrderReportCustomer
                    {
                        InvoiceNo = reader.GetString(0).TrimEnd(),
                        CustName = reader.GetString(1).TrimEnd(),
                        Address = reader.GetString(2).TrimEnd(),
                        TinNo = reader.GetString(3).TrimEnd(),
                        RegName = reader.GetString(4).TrimEnd(),
                        TermName = reader.GetString(5).TrimEnd(),
                        PartsList = partslist,
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
            return SalesInfo;
        }

        public bool GenerateInvoiceNo(string SONo)
        {
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand("SELECT ISNULL(RTRIM(InvoiceNo),'') AS InvoiceNo " +
                                                           "    FROM TblSalesMain WITH(READPAST) " +
                                                           "    WHERE SONo=@SONo", connection, transaction);
                command.Parameters.AddWithValue("@SONo", SONo);
                if (Convert.ToString(command.ExecuteScalar() ?? "") != "")
                {
                    transaction.Rollback();
                    transaction.Dispose();
                    connection.Close();
                    return true;
                }
                else
                {
                    command = Connection.setTransactionCommand("DECLARE @invoiceno varchar(20) = '' " +
                                                               "DECLARE @newinvoiceno varchar(20) = '' " +
                                                               "DECLARE @date varchar(4) = ((SELECT FORMAT(GETDATE(), 'yy'))+(SELECT FORMAT(GETDATE(),'MM'))) " +
                                                               "DECLARE @prefix varchar(20) = 'IN'+@date " +
                                                               "DECLARE @countable int = (SELECT COUNT(*) FROM TblCtrlNo WITH(READPAST)) " +
                                                               "SET @invoiceno = @prefix+'0000000000001'; " +
                                                               "BEGIN " +
                                                               "   SET @invoiceno = (SELECT TOP 1 InvoiceNo FROM TblCtrlNo WITH(READPAST) WHERE CAST(SUBSTRING(InvoiceNo,1,6) AS varchar) = @prefix) " +
                                                               "   IF @invoiceno IS NULL " +
                                                               "       BEGIN " +
                                                               "           SET @invoiceno = @prefix+'0000000000001'; " +
                                                               "           IF @countable > 0 " +
                                                               "           BEGIN " +
                                                               "               UPDATE TblCtrlNo " +
                                                               "                   SET InvoiceNo = @prefix+'0000000000002' " +
                                                               "           END " +
                                                               "           ELSE " +
                                                               "           BEGIN " +
                                                               "               INSERT INTO TblCtrlNo(InvoiceNo) " +
                                                               "                   VALUES(@prefix+'0000000000002') " +
                                                               "           END" +
                                                               "       END " +
                                                               "   ELSE " +
                                                               "       BEGIN " +
                                                               "           SET @newinvoiceno = (SELECT TOP 1 @prefix+ REPLICATE('0',13-LEN(SUBSTRING(@invoiceno,7,13)+1)) + CAST(SUBSTRING(@invoiceno,7,13)+1 AS varchar)) " +
                                                               "           UPDATE TblCtrlNo " +
                                                               "               SET InvoiceNo = @newinvoiceno " +
                                                               "       END " +
                                                               "END " +
                                                               "SELECT @invoiceno AS InvoiceNo", connection, transaction);
                    string id = Convert.ToString(command.ExecuteScalar() ?? "");

                    command = Connection.setTransactionCommand("UPDATE TblSalesMain " +
                                                               "   SET InvoiceNo=@InvoiceNo, InvoiceDate=GETDATE(), Status=3 " +
                                                               "   WHERE SONo=@SONo", connection, transaction);
                    command.Parameters.AddWithValue("@InvoiceNo", id);
                    command.Parameters.AddWithValue("@SONo", SONo);
                    int i = command.ExecuteNonQuery();
                    if (i == 0)
                    {
                        transaction.Rollback();
                        transaction.Dispose();
                        connection.Close();
                        return false;
                    }

                    command = Connection.setTransactionCommand("UPDATE TblSalesDet " +
                                                               "   SET Status=3, ModifiedBy=@User, ModifiedDt=GETDATE() " +
                                                               "   WHERE SONo=@SONo", connection, transaction);
                    command.Parameters.AddWithValue("@SONo", SONo);
                    command.Parameters.AddWithValue("@User", Name01);
                    int j = command.ExecuteNonQuery();
                    if (j == 0)
                    {
                        transaction.Rollback();
                        transaction.Dispose();
                        connection.Close();
                        return false;
                    }
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                transaction.Dispose();
                connection.Close();
            }
            return true;
        }
    }
}
