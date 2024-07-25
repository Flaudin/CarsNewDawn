using CARS.Model.Reports;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Controller.Reports
{
    internal class SalesReportController
    {
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public SalesReportModel GetSalesSummary (SalesReportFilter entity)
        {
            SalesReportModel SalesInfo = new SalesReportModel();
            List<SalesSummary> summarylist = new List<SalesSummary>();
            List<SalesRegister> registerlist = new List<SalesRegister>();
            List<SalesGroup> grouplist = new List<SalesGroup>();
            try
            {
                bool IsSO = true;
                if (entity.Type != "Sale Order")
                {
                    IsSO = false;
                }

                connection.Open();
                switch (entity.GroupBy)
                {
                    case "SALES SUMMARY":
                        if (IsSO)
                        {
                            command = Connection.setCommand($"SELECT a.SONo, CONVERT(varchar, ISNULL(e.SODate,''), 23) AS SODate, e.CustName, c.EmployeeName, " +
                                                            $"      d.TermName, SUM(a.NetPrice) AS NetPrice " +
                                                            $"  FROM TblSalesDet a WITH(READPAST) " +
                                                            $"  LEFT JOIN TblSalesMain e WITH(READPAST) ON e.SONo = a.SONo " +
                                                            $"  LEFT JOIN TblEmployeeMF c WITH(READPAST) ON c.EmployeeID = e.SalesmanID " +
                                                            $"  LEFT JOIN TblTermsMF d WITH(READPAST) ON d.TermID = e.TermID " +
                                                            $"  LEFT JOIN TblPartsMainMF f WITH(READPAST) ON f.PartNo = a.PartNo" +
                                                            $"  WHERE e.Status = 3 " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@CustName,'') = '' THEN 1 ELSE 0 END) OR e.CustName LIKE '%' + @CustName + '%') " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@SalesmanID,'') = '' THEN 1 ELSE 0 END) OR e.SalesmanID=@SalesmanID) " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@PartNo,'') = '' THEN 1 ELSE 0 END) OR a.PartNo LIKE '%' + @PartNo + '%') " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@DescID,'') = '' THEN 1 ELSE 0 END) OR f.DescID=@DescID) " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@BrandID,'') = '' THEN 1 ELSE 0 END) OR f.BrandID=@BrandID) " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@TermID,'') = '' THEN 1 ELSE 0 END) OR e.TermID=@TermID) " +
                                                            $"      AND e.SODate BETWEEN DATEADD(day, -1, @DateFrom) AND DATEADD(day, 1, @DateTo) " +
                                                            $"  GROUP BY a.SONo, e.SODate, e.CustName, c.EmployeeName, d.TermName " +
                                                            $"  ORDER BY a.SONo", connection);
                        }
                        else
                        {
                            command = Connection.setCommand($"SELECT e.InvoiceNo, CONVERT(varchar, ISNULL(e.SODate,''), 23) AS SODate, e.CustName, c.EmployeeName, " +
                                                            $"      d.TermName, SUM(a.NetPrice) AS NetPrice " +
                                                            $"  FROM TblSalesDet a WITH(READPAST) " +
                                                            $"  LEFT JOIN TblSalesMain e WITH(READPAST) ON e.SONo = a.SONo " +
                                                            $"  LEFT JOIN TblEmployeeMF c WITH(READPAST) ON c.EmployeeID = e.SalesmanID " +
                                                            $"  LEFT JOIN TblTermsMF d WITH(READPAST) ON d.TermID = e.TermID " +
                                                            $"  LEFT JOIN TblPartsMainMF f WITH(READPAST) ON f.PartNo = a.PartNo " +
                                                            $"  WHERE e.Status = 3 AND ISNULL(e.InvoiceNo,'') != '' " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@CustName,'') = '' THEN 1 ELSE 0 END) OR e.CustName LIKE '%' + @CustName + '%') " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@SalesmanID,'') = '' THEN 1 ELSE 0 END) OR e.SalesmanID=@SalesmanID) " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@PartNo,'') = '' THEN 1 ELSE 0 END) OR a.PartNo LIKE '%' + @PartNo + '%') " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@DescID,'') = '' THEN 1 ELSE 0 END) OR f.DescID=@DescID) " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@BrandID,'') = '' THEN 1 ELSE 0 END) OR f.BrandID=@BrandID) " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@TermID,'') = '' THEN 1 ELSE 0 END) OR e.TermID=@TermID) " +
                                                            $"      AND e.SODate BETWEEN DATEADD(day, -1, @DateFrom) AND DATEADD(day, 1, @DateTo) " +
                                                            $"  GROUP BY e.InvoiceNo, e.SODate, e.CustName, c.EmployeeName, d.TermName " +
                                                            $"  ORDER BY e.InvoiceNo", connection);
                        }
                        command.Parameters.AddWithValue("@CustName", entity.CustName);
                        command.Parameters.AddWithValue("@SalesmanID", entity.Salesman);
                        command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                        command.Parameters.AddWithValue("@DescID", entity.Desc);
                        command.Parameters.AddWithValue("@BrandID", entity.Brand);
                        command.Parameters.AddWithValue("@TermID", entity.Term);
                        command.Parameters.AddWithValue("@DateFrom", entity.DateFrom);
                        command.Parameters.AddWithValue("@DateTo", entity.DateTo);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            SalesSummary summary = new SalesSummary();
                            summary = new SalesSummary
                            {
                                SONo = reader.GetString(0).TrimEnd(),
                                SODate = reader.GetString(1).TrimEnd(),
                                Customer = reader.GetString(2).TrimEnd(),
                                Salesman = reader.GetString(3).TrimEnd(),
                                Term = reader.GetString(4).TrimEnd(),
                                TotalAmount = reader.GetDecimal(5),
                            };
                            summarylist.Add(summary);
                        }
                        reader.Close();
                        break;

                    default:
                        string order = "";
                        switch (entity.GroupBy)
                        {
                            case "SALESMAN":
                                order = "ORDER BY f.EmployeeName, e.CustName";
                                break;

                            case "PART NUMBER":
                                order = "ORDER BY a.PartNo, e.CustName";
                                break;

                            case "CUSTOMER":
                                order = "ORDER BY e.CustName, f.EmployeeName";
                                break;

                            case "BRAND":
                                order = "ORDER BY d.BrandName, f.EmployeeName";
                                break;
                        }
                        if (IsSO)
                        {
                            command = Connection.setCommand($"SELECT a.SONo, CONVERT(varchar, a.ItemNo) AS ItemNo, a.PartNo, ISNULL(c.DescName,'') AS DescName, a.ListPrice, a.Qty, a.NetPrice, a.Discount, " +
                                                            $"      SUM((a.ListPrice - a.Discount) * a.Qty) AS TotalPrice, d.BrandName, f.EmployeeName, e.CustName " +
                                                            $"  FROM TblSalesDet a WITH(READPAST) " +
                                                            $"  LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                            $"  LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                                                            $"  LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                                                            $"  LEFT JOIN TblSalesMain e WITH(READPAST) ON e.SONo = a.SONo " +
                                                            $"  LEFT JOIN TblEmployeeMF f WITH(READPAST) ON f.EmployeeID = e.SalesmanID " +
                                                            $"  WHERE e.Status = 3" +
                                                            $"      AND (1=(CASE WHEN ISNULL(@CustName,'') = '' THEN 1 ELSE 0 END) OR e.CustName LIKE '%' + @CustName + '%') " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@SalesmanID,'') = '' THEN 1 ELSE 0 END) OR e.SalesmanID=@SalesmanID) " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@PartNo,'') = '' THEN 1 ELSE 0 END) OR a.PartNo LIKE '%' + @PartNo + '%') " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@DescID,'') = '' THEN 1 ELSE 0 END) OR b.DescID=@DescID) " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@BrandID,'') = '' THEN 1 ELSE 0 END) OR b.BrandID=@BrandID) " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@TermID,'') = '' THEN 1 ELSE 0 END) OR e.TermID=@TermID) " +
                                                            $"      AND e.SODate BETWEEN DATEADD(day, -1, @DateFrom) AND DATEADD(day, 1, @DateTo) " +
                                                            $"  GROUP BY a.SONo, a.ItemNo, a.PartNo, c.DescName, a.ListPrice, a.Qty, a.NetPrice, a.Discount, " +
                                                            $"      d.BrandName, f.EmployeeName, e.CustName" +
                                                            $"  {order}", connection);
                        }
                        else
                        {
                            command = Connection.setCommand($"SELECT e.InvoiceNo, CONVERT(varchar, a.ItemNo) AS ItemNo, a.PartNo, ISNULL(c.DescName,'') AS DescName, a.ListPrice, a.Qty, a.NetPrice, a.Discount, " +
                                                            $"      SUM((a.ListPrice - a.Discount) * a.Qty) AS TotalPrice, d.BrandName, f.EmployeeName, e.CustName " +
                                                            $"  FROM TblSalesDet a WITH(READPAST) " +
                                                            $"  LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                            $"  LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                                                            $"  LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                                                            $"  LEFT JOIN TblSalesMain e WITH(READPAST) ON e.SONo = a.SONo " +
                                                            $"  LEFT JOIN TblEmployeeMF f WITH(READPAST) ON f.EmployeeID = e.SalesmanID " +
                                                            $"  WHERE e.Status = 3 AND ISNULL(e.InvoiceNo,'') != '' " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@CustName,'') = '' THEN 1 ELSE 0 END) OR e.CustName LIKE '%' + @CustName + '%') " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@SalesmanID,'') = '' THEN 1 ELSE 0 END) OR e.SalesmanID=@SalesmanID) " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@PartNo,'') = '' THEN 1 ELSE 0 END) OR a.PartNo LIKE '%' + @PartNo + '%') " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@DescID,'') = '' THEN 1 ELSE 0 END) OR b.DescID=@DescID) " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@BrandID,'') = '' THEN 1 ELSE 0 END) OR b.BrandID=@BrandID) " +
                                                            $"      AND (1=(CASE WHEN ISNULL(@TermID,'') = '' THEN 1 ELSE 0 END) OR e.TermID=@TermID) " +
                                                            $"      AND e.SODate BETWEEN DATEADD(day, -1, @DateFrom) AND DATEADD(day, 1, @DateTo) " +
                                                            $"  GROUP BY e.InvoiceNo, a.ItemNo, a.PartNo, c.DescName, a.ListPrice, a.Qty, a.NetPrice, a.Discount, " +
                                                            $"      d.BrandName, f.EmployeeName, e.CustName " +
                                                            $"  {order}", connection);
                        }
                        command.Parameters.AddWithValue("@Order", order);
                        command.Parameters.AddWithValue("@CustName", entity.CustName);
                        command.Parameters.AddWithValue("@SalesmanID", entity.Salesman);
                        command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                        command.Parameters.AddWithValue("@DescID", entity.Desc);
                        command.Parameters.AddWithValue("@BrandID", entity.Brand);
                        command.Parameters.AddWithValue("@TermID", entity.Term);
                        command.Parameters.AddWithValue("@DateFrom", entity.DateFrom);
                        command.Parameters.AddWithValue("@DateTo", entity.DateTo);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            SalesGroup group = new SalesGroup();
                            group = new SalesGroup
                            {
                                SONo = reader.GetString(0),
                                ItemNo = reader.GetString(1),
                                PartNo = reader.GetString(2).TrimEnd(),
                                PartDescription = reader.GetString(3).TrimEnd(),
                                ListPrice = reader.GetDecimal(4),
                                Qty = reader.GetDecimal(5),
                                NetPrice = reader.GetDecimal(6),
                                Discount = reader.GetDecimal(7),
                                TotalAmount = reader.GetDecimal(8),
                                Brand = reader.GetString(9).TrimEnd(),
                                Salesman = reader.GetString(10).TrimEnd(),
                                Customer = reader.GetString(11).TrimEnd(),
                            };
                            grouplist.Add(group);
                        }
                        reader.Close();
                        break;
                }
                SalesInfo = new SalesReportModel
                {
                    SummaryList = summarylist,
                    RegisterList = registerlist,
                    GroupList = grouplist,
                };
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
    }
}
