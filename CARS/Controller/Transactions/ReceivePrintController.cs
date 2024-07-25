using CARS.Model.Reports;
using CARS.Model.Transactions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CARS.Model.Transactions.ReceivePrintModel;

namespace CARS.Controller.Transactions
{
    internal class ReceivePrintController
    {
        private static SqlConnection conn = Connection.GetConnection();
        private static SqlCommand cmd = null;
        private static SqlDataReader rd = null;
        private static SqlTransaction tr = null;
        private static SqlDataAdapter dataAdapter = null;

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

        public ReceiveReportOrder GetReceiveOrder(string rrno)
        {
            ReceiveReportOrder receiveReportRecord = new ReceiveReportOrder();
            List<ReceiveReportParts> receiveParts = new List<ReceiveReportParts>();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    "SELECT TOP 1 b.Sku, a.PartNo, c.DescName, d.BrandName, a.Qty, b.UomID, a.UnitPrice, a.UnitPrice * a.Qty AS TotalPrice FROM TblReceivingDet a WITH(READPAST) " +
                    " LEFT JOIN TblPartsMainMF b ON b.PartNo = a.PartNo " +
                    " LEFT JOIN TblPartsDescriptionMF c ON c.DescID = b.DescID " +
                    " LEFT JOIN TblPartsBrandMF d ON d.BrandID = b.BrandID " +
                    " WHERE Status = '2'",
                    conn);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ReceiveReportParts part = new ReceiveReportParts();
                    part = new ReceiveReportParts
                    {
                        SKU = rd.GetString(0).TrimEnd(),
                        PartNo = rd.GetString(1).TrimEnd(),
                        Description = rd.GetString(2).TrimEnd(),
                        Brand = rd.GetString(3).TrimEnd(),
                        Qty = rd.GetDecimal(4),
                        Uom = rd.GetString(5).TrimEnd(),
                        UnitPrice = rd.GetDecimal(6),
                        TotalPrice = rd.GetDecimal(7),
                    };
                    receiveParts.Add(part);
                }
                rd.Close();
                cmd = Connection.setCommand(
                    "SELECT DISTINCT b.SLName, a.InvoiceNo, a.Remarks, CONVERT(varchar,a.CreatedDt)AS ReceiveDt, c.PONo, d.TermName FROM TblReceivingMain a WITH(READPAST) " +
                    " LEFT JOIN TblSubsidiaryMain b ON b.SLID = a.SupplierID " +
                    " LEFT JOIN TblReceivingDetPO c ON c.RRNo = a.RRNo " +
                    " LEFT JOIN TblTermsMF d ON d.TermID = a.TermID " +
                    " WHERE a.RRNo = @rrno",
                    conn);
                cmd.Parameters.AddWithValue("@rrno", rrno);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    receiveReportRecord = new ReceiveReportOrder
                    {
                        Supplier = rd.GetString(0).TrimEnd(),
                        InvoiceNo = rd.GetString(1).TrimEnd(),
                        Remarks = rd.GetString(2).TrimEnd(),
                        ReceiveDate = rd.GetString(3).TrimEnd(),
                        PONo = rd.GetString(4).TrimEnd(),
                        Terms = rd.GetString(5).TrimEnd(),
                        receiveReportParts = receiveParts
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
            return receiveReportRecord;
        }
    }
}
