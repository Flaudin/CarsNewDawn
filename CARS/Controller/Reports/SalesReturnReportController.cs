using CARS.Model.Reports;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Controller.Reports
{
    internal class SalesReturnReportController
    {
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;
        private static SqlTransaction transaction = null;

        //string GroupBy, string Category
        public SalesReturnReportModel GetSalesReturnSummary(SalesReturnReportFilter entity)
        {
            SalesReturnReportModel SalesInfo = new SalesReturnReportModel();
            List<SalesReturnSummary> summarylist = new List<SalesReturnSummary>();
            List<SalesReturnRegister> registerlist = new List<SalesReturnRegister>();
            List<SalesReturnGroup> grouplist = new List<SalesReturnGroup>();
            try
            {
                connection.Open();
                switch (entity.GroupBy)
                {
                    case "SALES SUMMARY":
                        command = Connection.setCommand($"SELECT a.SRNo, CONVERT(varchar, ISNULL(e.SRDate,''), 23) AS SRDate, e.SONo, " +
                                                        $"      CONVERT(varchar, ISNULL(g.SODate,''), 23) AS SODate,  g.CustName, " +
                                                        $"      c.EmployeeName, d.TermName, a.GoodQty, a.DefectiveQty, " +
                                                        $"      (f.ListPrice - f.Discount) * (a.GoodQty + a.DefectiveQty) AS NetPrice " +
                                                        $"  FROM TblSalesReturnDet a WITH(READPAST) " +
                                                        $"  LEFT JOIN TblSalesReturnMain e WITH(READPAST) ON e.SRNo = a.SRNo " +
                                                        $"  LEFT JOIN TblEmployeeMF c WITH(READPAST) ON c.EmployeeID = e.SalesmanID " +
                                                        $"  LEFT JOIN TblSalesDet f WITH(READPAST) ON f.SONo = e.SONo " +
                                                        $"  LEFT JOIN TblSalesMain g WITH(READPAST) ON g.SONo = f.SONo " +
                                                        $"  LEFT JOIN TblTermsMF d WITH(READPAST) ON d.TermID = g.TermID " +
                                                        $"  WHERE (1=(CASE WHEN ISNULL(@CustName,'') = '' THEN 1 ELSE 0 END) OR g.CustName LIKE '%' + @CustName + '%') " +
                                                        $"      AND (1=(CASE WHEN ISNULL(@SalesmanID,'') = '' THEN 1 ELSE 0 END) OR e.SalesmanID=@SalesmanID) " +
                                                        $"      AND (1=(CASE WHEN ISNULL(@PartNo,'') = '' THEN 1 ELSE 0 END) OR a.PartNo LIKE '%' + @PartNo + '%') " +
                                                        //$"      AND (1=(CASE WHEN ISNULL(@DescID,'') = '' THEN 1 ELSE 0 END) OR f.DescID=@DescID) " +
                                                        //$"      AND (1=(CASE WHEN ISNULL(@BrandID,'') = '' THEN 1 ELSE 0 END) OR f.BrandID=@BrandID) " +
                                                        $"      AND (1=(CASE WHEN ISNULL(@TermID,'') = '' THEN 1 ELSE 0 END) OR g.TermID=@TermID) " +
                                                        $"      AND g.SODate BETWEEN DATEADD(day, -1, @SODateFrom) AND DATEADD(day, 1, @SODateTo) " +
                                                        $"      AND e.SRDate BETWEEN DATEADD(day, -1, @SRDateFrom) AND DATEADD(day, 1, @SRDateTo) " +
                                                        $"  ORDER BY a.SRNo", connection);
                        command.Parameters.AddWithValue("@CustName", entity.CustName);
                        command.Parameters.AddWithValue("@SalesmanID", entity.Salesman);
                        command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                        //command.Parameters.AddWithValue("@DescID", entity.Desc);
                        //command.Parameters.AddWithValue("@BrandID", entity.Brand);
                        command.Parameters.AddWithValue("@TermID", entity.Term);
                        command.Parameters.AddWithValue("@SODateFrom", entity.SODateFrom);
                        command.Parameters.AddWithValue("@SODateTo", entity.SODateTo);
                        command.Parameters.AddWithValue("@SRDateFrom", entity.SRDateFrom);
                        command.Parameters.AddWithValue("@SRDateTo", entity.SRDateTo);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            SalesReturnSummary summary = new SalesReturnSummary();
                            summary = new SalesReturnSummary
                            {
                                SRNo = reader.GetString(0).TrimEnd(),
                                SRDate = reader.GetString(1).TrimEnd(),
                                SONo = reader.GetString(2).TrimEnd(),
                                SODate = reader.GetString(3).TrimEnd(),
                                Customer = reader.GetString(4).TrimEnd(),
                                Salesman = reader.GetString(5).TrimEnd(),
                                Term = reader.GetString(6).TrimEnd(),
                                GoodQty = reader.GetDecimal(7),
                                DefectiveQty = reader.GetDecimal(8),
                                NetPrice = reader.GetDecimal(9),
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
                                order = "ORDER BY h.EmployeeName, g.CustName";
                                break;

                            case "PART NUMBER":
                                order = "ORDER BY a.PartNo, g.CustName";
                                break;

                            case "CUSTOMER":
                                order = "ORDER BY g.CustName, h.EmployeeName";
                                break;

                            case "BRAND":
                                order = "ORDER BY d.BrandName, h.EmployeeName";
                                break;
                        }
                        command = Connection.setCommand($"SELECT CONVERT(varchar, a.ItemNo) AS ItemNo,  a.SRNo, CONVERT(varchar, ISNULL(e.SRDate,''), 23) AS SODate, e.SONo, " +
                                                        $"      CONVERT(varchar, g.SODate, 23) AS SODate,  a.PartNo, c.DescName, d.BrandName, a.GoodQty, " +
                                                        $"      a.DefectiveQty, (f.ListPrice - f.Discount) * (a.GoodQty + a.DefectiveQty) AS NetPrice, " +
                                                        $"      h.EmployeeName, g.CustName " +
                                                        $"  FROM TblSalesReturnDet a WITH(READPAST) " +
                                                        $"  LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                                                        $"  LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                                                        $"  LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                                                        $"  LEFT JOIN TblSalesReturnMain e WITH(READPAST) ON e.SRNo = a.SRNo " +
                                                        $"  LEFT JOIN TblSalesDet f WITH(READPAST) ON f.SONo = e.SONo " +
                                                        $"  LEFT JOIN TblSalesMain g WITH(READPAST) ON g.SONo = f.SONo " +
                                                        $"  LEFT JOIN TblEmployeeMF h WITH(READPAST) ON h.EmployeeID = e.SalesmanID " +
                                                        $"  WHERE (1=(CASE WHEN ISNULL(@CustName,'') = '' THEN 1 ELSE 0 END) OR g.CustName LIKE '%' + @CustName + '%') " +
                                                        $"      AND (1=(CASE WHEN ISNULL(@SalesmanID,'') = '' THEN 1 ELSE 0 END) OR e.SalesmanID=@SalesmanID) " +
                                                        $"      AND (1=(CASE WHEN ISNULL(@PartNo,'') = '' THEN 1 ELSE 0 END) OR a.PartNo LIKE '%' + @PartNo + '%') " +
                                                        $"      AND (1=(CASE WHEN ISNULL(@DescID,'') = '' THEN 1 ELSE 0 END) OR b.DescID=@DescID) " +
                                                        $"      AND (1=(CASE WHEN ISNULL(@BrandID,'') = '' THEN 1 ELSE 0 END) OR b.BrandID=@BrandID) " +
                                                        $"      AND (1=(CASE WHEN ISNULL(@TermID,'') = '' THEN 1 ELSE 0 END) OR g.TermID=@TermID) " +
                                                        $"      AND g.SODate BETWEEN DATEADD(day, -1, @SODateFrom) AND DATEADD(day, 1, @SODateTo) " +
                                                        $"      AND e.SRDate BETWEEN DATEADD(day, -1, @SRDateFrom) AND DATEADD(day, 1, @SRDateTo) " +
                                                        $"  {order}", connection);
                        command.Parameters.AddWithValue("@Order", order);
                        command.Parameters.AddWithValue("@CustName", entity.CustName);
                        command.Parameters.AddWithValue("@SalesmanID", entity.Salesman);
                        command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                        command.Parameters.AddWithValue("@DescID", entity.Desc);
                        command.Parameters.AddWithValue("@BrandID", entity.Brand);
                        command.Parameters.AddWithValue("@TermID", entity.Term);
                        command.Parameters.AddWithValue("@SODateFrom", entity.SODateFrom);
                        command.Parameters.AddWithValue("@SODateTo", entity.SODateTo);
                        command.Parameters.AddWithValue("@SRDateFrom", entity.SRDateFrom);
                        command.Parameters.AddWithValue("@SRDateTo", entity.SRDateTo);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            SalesReturnGroup group = new SalesReturnGroup();
                            group = new SalesReturnGroup
                            {
                                ItemNo = reader.GetString(0),
                                SRNo = reader.GetString(1),
                                SRDate = reader.GetString(2).TrimEnd(),
                                SONo = reader.GetString(3).TrimEnd(),
                                SODate = reader.GetString(4).TrimEnd(),
                                PartNo = reader.GetString(5).TrimEnd(),
                                PartDescription = reader.GetString(6).TrimEnd(),
                                Brand = reader.GetString(7).TrimEnd(),
                                GoodQty = reader.GetDecimal(8),
                                DefectiveQty = reader.GetDecimal(9),
                                NetPrice = reader.GetDecimal(10),
                                Salesman = reader.GetString(11).TrimEnd(),
                                Customer = reader.GetString(12).TrimEnd(),
                            };
                            grouplist.Add(group);
                        }
                        reader.Close();
                        break;

                    //case "PART NUMBER":
                    //    command = Connection.setCommand($"SELECT CONVERT(varchar, a.ItemNo) AS ItemNo,  a.SRNo, CONVERT(varchar, ISNULL(e.SRDate,''), 23) AS SODate, e.SONo, " +
                    //                                    $"      CONVERT(varchar, g.SODate, 23) AS SODate,  a.PartNo, c.DescName, d.BrandName, a.GoodQty, " +
                    //                                    $"      a.DefectiveQty, (f.ListPrice - f.Discount) * (a.GoodQty + a.DefectiveQty) AS NetPrice, " +
                    //                                    $"      h.EmployeeName, g.CustName " +
                    //                                    $"  FROM TblSalesReturnDet a WITH(READPAST) " +
                    //                                    $"  LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                    //                                    $"  LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                    //                                    $"  LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                    //                                    $"  LEFT JOIN TblSalesReturnMain e WITH(READPAST) ON e.SRNo = a.SRNo " +
                    //                                    $"  LEFT JOIN TblSalesDet f WITH(READPAST) ON f.SONo = e.SONo " +
                    //                                    $"  LEFT JOIN TblSalesMain g WITH(READPAST) ON g.SONo = f.SONo " +
                    //                                    $"  LEFT JOIN TblEmployeeMF h WITH(READPAST) ON h.EmployeeID = e.SalesmanID " +
                    //                                    $"  WHERE (1=(CASE WHEN ISNULL(@CustName,'') = '' THEN 1 ELSE 0 END) OR g.CustName LIKE '%' + @CustName + '%') " +
                    //                                    $"      AND (1=(CASE WHEN ISNULL(@SalesmanID,'') = '' THEN 1 ELSE 0 END) OR e.SalesmanID=@SalesmanID) " +
                    //                                    $"      AND (1=(CASE WHEN ISNULL(@PartNo,'') = '' THEN 1 ELSE 0 END) OR a.PartNo LIKE '%' + @PartNo + '%') " +
                    //                                    $"      AND (1=(CASE WHEN ISNULL(@DescID,'') = '' THEN 1 ELSE 0 END) OR b.DescID=@DescID) " +
                    //                                    $"      AND (1=(CASE WHEN ISNULL(@BrandID,'') = '' THEN 1 ELSE 0 END) OR b.BrandID=@BrandID) " +
                    //                                    $"      AND (1=(CASE WHEN ISNULL(@TermID,'') = '' THEN 1 ELSE 0 END) OR g.TermID=@TermID) " +
                    //                                    $"      AND g.SODate BETWEEN DATEADD(day, -1, @SODateFrom) AND DATEADD(day, 1, @SODateTo) " +
                    //                                    $"      AND e.SRDate BETWEEN DATEADD(day, -1, @SRDateFrom) AND DATEADD(day, 1, @SRDateTo) " +
                    //                                    $"  ORDER BY a.PartNo, g.CustName", connection);
                    //    command.Parameters.AddWithValue("@CustName", entity.CustName);
                    //    command.Parameters.AddWithValue("@SalesmanID", entity.Salesman);
                    //    command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                    //    command.Parameters.AddWithValue("@DescID", entity.Desc);
                    //    command.Parameters.AddWithValue("@BrandID", entity.Brand);
                    //    command.Parameters.AddWithValue("@TermID", entity.Term);
                    //    command.Parameters.AddWithValue("@SODateFrom", entity.SODateFrom);
                    //    command.Parameters.AddWithValue("@SODateTo", entity.SODateTo);
                    //    command.Parameters.AddWithValue("@SRDateFrom", entity.SRDateFrom);
                    //    command.Parameters.AddWithValue("@SRDateTo", entity.SRDateTo);
                    //    reader = command.ExecuteReader();
                    //    while (reader.Read())
                    //    {
                    //        SalesReturnGroup group = new SalesReturnGroup();
                    //        group = new SalesReturnGroup
                    //        {
                    //            ItemNo = reader.GetString(0),
                    //            SRNo = reader.GetString(1),
                    //            SRDate = reader.GetString(2).TrimEnd(),
                    //            SONo = reader.GetString(3).TrimEnd(),
                    //            SODate = reader.GetString(4).TrimEnd(),
                    //            PartNo = reader.GetString(5).TrimEnd(),
                    //            PartDescription = reader.GetString(6).TrimEnd(),
                    //            Brand = reader.GetString(7).TrimEnd(),
                    //            GoodQty = reader.GetDecimal(8),
                    //            DefectiveQty = reader.GetDecimal(9),
                    //            NetPrice = reader.GetDecimal(10),
                    //            Salesman = reader.GetString(11).TrimEnd(),
                    //            Customer = reader.GetString(12).TrimEnd(),
                    //        };
                    //        grouplist.Add(group);
                    //    }
                    //    reader.Close();
                    //    break;

                    //case "CUSTOMER":
                    //    command = Connection.setCommand($"SELECT CONVERT(varchar, a.ItemNo) AS ItemNo,  a.SRNo, CONVERT(varchar, ISNULL(e.SRDate,''), 23) AS SODate, e.SONo, " +
                    //                                    $"      CONVERT(varchar, g.SODate, 23) AS SODate,  a.PartNo, c.DescName, d.BrandName, a.GoodQty, " +
                    //                                    $"      a.DefectiveQty, (f.ListPrice - f.Discount) * (a.GoodQty + a.DefectiveQty) AS NetPrice, " +
                    //                                    $"      h.EmployeeName, g.CustName " +
                    //                                    $"  FROM TblSalesReturnDet a WITH(READPAST) " +
                    //                                    $"  LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                    //                                    $"  LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                    //                                    $"  LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                    //                                    $"  LEFT JOIN TblSalesReturnMain e WITH(READPAST) ON e.SRNo = a.SRNo " +
                    //                                    $"  LEFT JOIN TblSalesDet f WITH(READPAST) ON f.SONo = e.SONo " +
                    //                                    $"  LEFT JOIN TblSalesMain g WITH(READPAST) ON g.SONo = f.SONo " +
                    //                                    $"  LEFT JOIN TblEmployeeMF h WITH(READPAST) ON h.EmployeeID = e.SalesmanID " +
                    //                                    $"  WHERE (1=(CASE WHEN ISNULL(@CustName,'') = '' THEN 1 ELSE 0 END) OR g.CustName LIKE '%' + @CustName + '%') " +
                    //                                    $"      AND (1=(CASE WHEN ISNULL(@SalesmanID,'') = '' THEN 1 ELSE 0 END) OR e.SalesmanID=@SalesmanID) " +
                    //                                    $"      AND (1=(CASE WHEN ISNULL(@PartNo,'') = '' THEN 1 ELSE 0 END) OR a.PartNo LIKE '%' + @PartNo + '%') " +
                    //                                    $"      AND (1=(CASE WHEN ISNULL(@DescID,'') = '' THEN 1 ELSE 0 END) OR b.DescID=@DescID) " +
                    //                                    $"      AND (1=(CASE WHEN ISNULL(@BrandID,'') = '' THEN 1 ELSE 0 END) OR b.BrandID=@BrandID) " +
                    //                                    $"      AND (1=(CASE WHEN ISNULL(@TermID,'') = '' THEN 1 ELSE 0 END) OR g.TermID=@TermID) " +
                    //                                    $"      AND g.SODate BETWEEN DATEADD(day, -1, @SODateFrom) AND DATEADD(day, 1, @SODateTo) " +
                    //                                    $"      AND e.SRDate BETWEEN DATEADD(day, -1, @SRDateFrom) AND DATEADD(day, 1, @SRDateTo) " +
                    //                                    $"  ORDER BY g.CustName, h.EmployeeName", connection);
                    //    command.Parameters.AddWithValue("@CustName", entity.CustName);
                    //    command.Parameters.AddWithValue("@SalesmanID", entity.Salesman);
                    //    command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                    //    command.Parameters.AddWithValue("@DescID", entity.Desc);
                    //    command.Parameters.AddWithValue("@BrandID", entity.Brand);
                    //    command.Parameters.AddWithValue("@TermID", entity.Term);
                    //    command.Parameters.AddWithValue("@SODateFrom", entity.SODateFrom);
                    //    command.Parameters.AddWithValue("@SODateTo", entity.SODateTo);
                    //    command.Parameters.AddWithValue("@SRDateFrom", entity.SRDateFrom);
                    //    command.Parameters.AddWithValue("@SRDateTo", entity.SRDateTo);
                    //    reader = command.ExecuteReader();
                    //    while (reader.Read())
                    //    {
                    //        SalesReturnGroup group = new SalesReturnGroup();
                    //        group = new SalesReturnGroup
                    //        {
                    //            ItemNo = reader.GetString(0),
                    //            SRNo = reader.GetString(1),
                    //            SRDate = reader.GetString(2).TrimEnd(),
                    //            SONo = reader.GetString(3).TrimEnd(),
                    //            SODate = reader.GetString(4).TrimEnd(),
                    //            PartNo = reader.GetString(5).TrimEnd(),
                    //            PartDescription = reader.GetString(6).TrimEnd(),
                    //            Brand = reader.GetString(7).TrimEnd(),
                    //            GoodQty = reader.GetDecimal(8),
                    //            DefectiveQty = reader.GetDecimal(9),
                    //            NetPrice = reader.GetDecimal(10),
                    //            Salesman = reader.GetString(11).TrimEnd(),
                    //            Customer = reader.GetString(12).TrimEnd(),
                    //        };
                    //        grouplist.Add(group);
                    //    }
                    //    reader.Close();
                    //    break;

                    //case "BRAND":
                    //    command = Connection.setCommand($"SELECT CONVERT(varchar, a.ItemNo) AS ItemNo,  a.SRNo, CONVERT(varchar, ISNULL(e.SRDate,''), 23) AS SODate, e.SONo, " +
                    //                                    $"      CONVERT(varchar, g.SODate, 23) AS SODate,  a.PartNo, c.DescName, d.BrandName, a.GoodQty, " +
                    //                                    $"      a.DefectiveQty, (f.ListPrice - f.Discount) * (a.GoodQty + a.DefectiveQty) AS NetPrice, " +
                    //                                    $"      h.EmployeeName, g.CustName " +
                    //                                    $"  FROM TblSalesReturnDet a WITH(READPAST) " +
                    //                                    $"  LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                    //                                    $"  LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                    //                                    $"  LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                    //                                    $"  LEFT JOIN TblSalesReturnMain e WITH(READPAST) ON e.SRNo = a.SRNo " +
                    //                                    $"  LEFT JOIN TblSalesDet f WITH(READPAST) ON f.SONo = e.SONo " +
                    //                                    $"  LEFT JOIN TblSalesMain g WITH(READPAST) ON g.SONo = f.SONo " +
                    //                                    $"  LEFT JOIN TblEmployeeMF h WITH(READPAST) ON h.EmployeeID = e.SalesmanID " +
                    //                                    $"  WHERE (1=(CASE WHEN ISNULL(@CustName,'') = '' THEN 1 ELSE 0 END) OR g.CustName LIKE '%' + @CustName + '%') " +
                    //                                    $"      AND (1=(CASE WHEN ISNULL(@SalesmanID,'') = '' THEN 1 ELSE 0 END) OR e.SalesmanID=@SalesmanID) " +
                    //                                    $"      AND (1=(CASE WHEN ISNULL(@PartNo,'') = '' THEN 1 ELSE 0 END) OR a.PartNo LIKE '%' + @PartNo + '%') " +
                    //                                    $"      AND (1=(CASE WHEN ISNULL(@DescID,'') = '' THEN 1 ELSE 0 END) OR b.DescID=@DescID) " +
                    //                                    $"      AND (1=(CASE WHEN ISNULL(@BrandID,'') = '' THEN 1 ELSE 0 END) OR b.BrandID=@BrandID) " +
                    //                                    $"      AND (1=(CASE WHEN ISNULL(@TermID,'') = '' THEN 1 ELSE 0 END) OR g.TermID=@TermID) " +
                    //                                    $"      AND g.SODate BETWEEN DATEADD(day, -1, @SODateFrom) AND DATEADD(day, 1, @SODateTo) " +
                    //                                    $"      AND e.SRDate BETWEEN DATEADD(day, -1, @SRDateFrom) AND DATEADD(day, 1, @SRDateTo) " +
                    //                                    $"  ORDER BY d.BrandName, h.EmployeeName", connection);
                    //    command.Parameters.AddWithValue("@CustName", entity.CustName);
                    //    command.Parameters.AddWithValue("@SalesmanID", entity.Salesman);
                    //    command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                    //    command.Parameters.AddWithValue("@DescID", entity.Desc);
                    //    command.Parameters.AddWithValue("@BrandID", entity.Brand);
                    //    command.Parameters.AddWithValue("@TermID", entity.Term);
                    //    command.Parameters.AddWithValue("@SODateFrom", entity.SODateFrom);
                    //    command.Parameters.AddWithValue("@SODateTo", entity.SODateTo);
                    //    command.Parameters.AddWithValue("@SRDateFrom", entity.SRDateFrom);
                    //    command.Parameters.AddWithValue("@SRDateTo", entity.SRDateTo);
                    //    reader = command.ExecuteReader();
                    //    while (reader.Read())
                    //    {
                    //        SalesReturnGroup group = new SalesReturnGroup();
                    //        group = new SalesReturnGroup
                    //        {
                    //            ItemNo = reader.GetString(0),
                    //            SRNo = reader.GetString(1),
                    //            SRDate = reader.GetString(2).TrimEnd(),
                    //            SONo = reader.GetString(3).TrimEnd(),
                    //            SODate = reader.GetString(4).TrimEnd(),
                    //            PartNo = reader.GetString(5).TrimEnd(),
                    //            PartDescription = reader.GetString(6).TrimEnd(),
                    //            Brand = reader.GetString(7).TrimEnd(),
                    //            GoodQty = reader.GetDecimal(8),
                    //            DefectiveQty = reader.GetDecimal(9),
                    //            NetPrice = reader.GetDecimal(10),
                    //            Salesman = reader.GetString(11).TrimEnd(),
                    //            Customer = reader.GetString(12).TrimEnd(),
                    //        };
                    //        grouplist.Add(group);
                    //    }
                    //    reader.Close();
                    //    break;
                }
                SalesInfo = new SalesReturnReportModel
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
