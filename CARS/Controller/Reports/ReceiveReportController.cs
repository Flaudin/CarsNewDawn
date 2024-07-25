using CARS.Controller.Transactions;
using CARS.Model;
using CARS.Model.Masterfiles;
using CARS.Model.Reports;
using CARS.Model.Transactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Controller.Reports
{
    internal class ReceiveReportController : Universal<ReceiveReportModel>
    {
        private static SqlConnection conn = Connection.GetConnection();
        private static SqlCommand cmd = null;
        private static SqlDataReader rd = null;
        private static SqlTransaction tr = null;

        

        public DataTable RRSelection()
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT RRNo, CreatedDt, Status FROM TblReceivingMain WHERE Status = '2'", conn);
                rd = cmd.ExecuteReader();
                dt.Load(rd);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable SupplierSelection()
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT * FROM TblSubsidiaryMain WHERE SLID LIKE '%SE%'", conn);
                rd = cmd.ExecuteReader();
                dt.Load(rd);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public SortedDictionary<string, string> getSupplier(string rrno)
        {
            SortedDictionary<string,string> dictionary = new SortedDictionary<string,string>();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT b.SLName,a.SupplierID FROM TblReceivingMain a WITH(READPAST) " +
                    " LEFT JOIN TblSubsidiaryMain b WITH(READPAST) ON b.SLID = a.SupplierID " +
                    " WHERE RRNo = @rrno", conn);
                cmd.Parameters.AddWithValue("@rrno", rrno);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                   SupplierModel model = new SupplierModel();
                    model.SLName = rd.GetString(0).TrimEnd();
                    model.SLID = rd.GetString(1).TrimEnd();

                    dictionary.Add(model.SLName, model.SLID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dictionary;
        }

        public ReceiveReportModel getReceiveSummary (string groupBy, string datefrom, string dateto)
        {
            ReceiveReportModel ReceiveInfo = new ReceiveReportModel();
            List<ReceiveSummary> summaryList = new List<ReceiveSummary>();
            List<ReceivingDetailsListing> receivingdetailsList = new List<ReceivingDetailsListing>();
            try
            {
                conn.Open();
                switch (groupBy)
                {
                    case "RR SUMMARY":
                        cmd = Connection.setCommand("SELECT DISTINCT a.RRNo, CONVERT(varchar, a.CreatedDt,23), b.SLName, a.InvoiceNo, CONVERT(varchar,a.InvoiceDt,23) AS InvoiceDt, e.TermName, " +
                            " CASE WHEN a.Status = 2 THEN 'SERVED' END AS Status, " +
                            " SUM(c.Qty * c.UnitPrice) AS Amount " +
                            " FROM TblReceivingMain a WITH(READPAST) " +
                            " LEFT JOIN TblSubsidiaryMain b WITH(READPAST) ON b.SLID = a.SupplierID " +
                            " LEFT JOIN TblReceivingDet c WITH(READPAST) ON c.RRNo = a.RRNo " +
                            " LEFT JOIN TblTermsMF e WITH(READPAST) ON e.TermID = a.TermID " +
                            " WHERE a.Status = 2 AND ISNULL(a.InvoiceNo,'') != '' AND  a.CreatedDt BETWEEN DATEADD(day, -1, @DateFrom) AND DATEADD(day, 1, @DateTo)" +
                            " GROUP BY a.RRNo, a.CreatedDt, b.SLName, a.InvoiceNo, a.InvoiceDt, e.TermName, a.Status", conn);
                        cmd.Parameters.AddWithValue("@DateFrom", datefrom);
                        cmd.Parameters.AddWithValue("@DateTo", dateto);
                        rd = cmd.ExecuteReader();
                        while(rd.Read())
                        {
                            ReceiveSummary summary = new ReceiveSummary();
                            summary = new ReceiveSummary
                            {
                                RRNo = rd.GetString(0).TrimEnd(),
                                RRDate = rd.GetString(1).TrimEnd(),
                                SupplierName = rd.GetString(2).TrimEnd(),
                                RefNo = rd.GetString(3).TrimEnd(),
                                RefDate = rd.GetString(4).TrimEnd(),
                                Terms = rd.GetString(5).TrimEnd(),
                                Status = rd.GetString(6).TrimEnd(),
                                Amount = rd.GetDecimal(7),
                            };
                            summaryList.Add(summary);
                        }
                        rd.Close();
                        break;

                    case "RR DETAILS LIST":
                        cmd = Connection.setCommand("SELECT DISTINCT CONVERT(varchar,a.CreatedDt,23), a.RRNo, b.SLName, d.Sku, c.PartNo, e.DescName, " +
                            " f.BrandName, c.Qty, c.UnitPrice, c.Qty * c.UnitPrice AS TotalAmt " +
                            " FROM TblReceivingMain a WITH(READPAST) " +
                            " LEFT JOIN TblSubsidiaryMain b WITH(READPAST) ON b.SLID = a.SupplierID " +
                            " LEFT JOIN TblReceivingDet c WITH(READPAST) ON c.RRNo = a.RRNo " +
                            " LEFT JOIN TblPartsMainMF d WITH(READPAST) ON d.PartNo = c.PartNo " +
                            " LEFT JOIN TblPartsDescriptionMF e WITH(READPAST) ON e.DescID = d.DescID " +
                            " LEFT JOIN TblPartsBrandMF f WITH(READPAST) ON f.BrandID = d.BrandID " +
                            " WHERE a.Status = 2 AND  a.CreatedDt BETWEEN DATEADD(day, -1, @DateFrom) AND DATEADD(day, 1, @DateTo)", conn);
                        cmd.Parameters.AddWithValue("@DateFrom", datefrom);
                        cmd.Parameters.AddWithValue("@DateTo", dateto);
                        rd = cmd.ExecuteReader() ;
                        while (rd.Read())
                        {
                            ReceivingDetailsListing details = new ReceivingDetailsListing();
                            details = new ReceivingDetailsListing
                            {
                                RRDate = rd.GetString(0).TrimEnd(),
                                RRNo = rd.GetString(1).TrimEnd(),
                                Supplier = rd.GetString(2).TrimEnd(),
                                SKU = rd.GetString(3).TrimEnd(),
                                PartNo = rd.GetString(4).TrimEnd(),
                                PartDesc = rd.GetString(5).TrimEnd(),
                                Brand = rd.GetString(6).TrimEnd(),
                                Qty = rd.GetDecimal(7),
                                UnitCost = rd.GetDecimal(8),
                                TotalAmt = rd.GetDecimal(9),
                            };
                            receivingdetailsList.Add(details);
                        }
                        break;
                }
                ReceiveInfo = new ReceiveReportModel
                {
                    SummaryList = summaryList,
                    ReceivingDetailsList = receivingdetailsList,
                    user = Name01
                };
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ReceiveInfo;
        }

        public ReceivePrintModel GetOwnerCompany()
        {
            ReceivePrintModel company = new ReceivePrintModel();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    "SELECT a.CompName, RTRIM(a.NoStreet) + ' ' + RTRIM(c.CityName) + ', ' + RTRIM(b.ProvName) AS Address, a.TelNo, a.TinNo, a.CompLogo " +
                    "   FROM TblCompanyProfile a WITH(READPAST) " +
                    "   LEFT JOIN TblProvinceMF b WITH(READPAST) ON b.ProvID = a.ProvID " +
                    "   LEFT JOIN TblCityMF c WITH(READPAST) ON c.CityID = a.CityID",
                    conn);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    company = new ReceivePrintModel
                    {
                        CompanyName = rd.GetString(0).TrimEnd(),
                        Address = rd.GetString(1).TrimEnd(),
                        TelNo = rd.GetString(2).TrimEnd(),
                        TinNo = rd.GetString(3).TrimEnd(),
                        CompLogo = rd.GetString(4).TrimEnd(),
                    };
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return company;
        }

        public override string Create(ReceiveReportModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(ReceiveReportModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(ReceiveReportModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(ReceiveReportModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(ReceiveReportModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
