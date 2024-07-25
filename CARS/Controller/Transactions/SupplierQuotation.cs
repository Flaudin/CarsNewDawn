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
    internal class SupplierQuotationController : Universal<POMonitoring>
    {
        private static SqlConnection conn = Connection.GetConnection();
        private static SqlCommand cmd = null;
        private static SqlDataReader rd = null;
        private static SqlTransaction tr = null;


        public override string Create(POMonitoring entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(POMonitoring entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(POMonitoring entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(POMonitoring entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(POMonitoring entity)
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<string, string> getBrands()
        {
            SortedDictionary<string, string> Branddictionary = new SortedDictionary<string, string>();
            try
            {
                conn.Open();

                cmd = Connection.setCommand($"SELECT BrandID, BrandName FROM TblPartsBrandMF WITH (READPAST)", conn);
                rd = cmd.ExecuteReader();
                Branddictionary.Add("", "");

                while (rd.Read())
                {
                    BrandModel brands = new BrandModel();
                    brands.BrandID = rd.GetString(0).Trim();
                    brands.BrandName = rd.GetString(1).Trim();
                    Branddictionary.Add(brands.BrandID, brands.BrandName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
            return Branddictionary;
        }

        public SortedDictionary<string, string> getDescriptions()
        {
            SortedDictionary<string, string> Descdictionary = new SortedDictionary<string, string>();
            try
            {
                conn.Open();

                cmd = Connection.setCommand($"SELECT DescID, DescName FROM TblPartsDescriptionMF WITH (READPAST)", conn);
                rd = cmd.ExecuteReader();
                Descdictionary.Add("", "");

                while (rd.Read())
                {
                    DescriptionModel descs = new DescriptionModel();
                    descs.DescID = rd.GetString(0).Trim();
                    descs.DescName = rd.GetString(1).Trim();
                    Descdictionary.Add(descs.DescID, descs.DescName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
            return Descdictionary;
        }

        public SortedDictionary<string, string> getSupplier()
        {
            SortedDictionary<string, string> suppDictionary = new SortedDictionary<string, string>();
            try
            {
                conn.Open();

                cmd = Connection.setCommand("SELECT SLID, SLName FROM TblSubsidiaryMain WITH(READPAST) WHERE SLType = 'S'", conn);
                rd = cmd.ExecuteReader();
                suppDictionary.Add("", "");

                while (rd.Read())
                {
                    SupplierModel supp = new SupplierModel();
                    supp.SLID = rd.GetString(0).Trim();
                    supp.SLName = rd.GetString(1).Trim();
                    suppDictionary.Add(supp.SLID, supp.SLName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
            return suppDictionary;
        }

        public DataTable getParts()
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand($"SELECT a.PartNo, b.BrandName, c.DescName FROM TblPartsMainMF a WITH(READPAST) " +
                    $"                        LEFT JOIN TblPartsBrandMF b ON b.BrandID = a.BrandID " +
                    $"                        LEFT JOIN TblPartsDescriptionMF c ON c.DescID = a.DescID", conn);
                rd = cmd.ExecuteReader();
                dt.Load(rd);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public string getTermID(string slid)
        {
            string termId = "";
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT TermID FROM TblSubsidiaryMain WHERE SLID =@slid", conn);
                cmd.Parameters.AddWithValue("@slid", slid);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    termId = rd.GetString(0).TrimEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close() ;
            }
            return termId;
        }
        public string saveQuotation(SupplierQuotationModel quotationModel)
        {
            string msg = "Quotation Saved";
            string sqNo = "";
            try
            {
                conn.Open();
                tr = conn.BeginTransaction();
                cmd = Connection.setTransactionCommand(
                    " Declare @prefix varchar(10) = 'SQ'+RIGHT (YEAR(GETDATE()),2)+SUBSTRING(CONVERT(varchar,GETDATE(),23),6,2) " +
                        " Declare @code varchar(10) = (SELECT TOP 1 SuppQuotNo FROM TblCtrlNo WHERE CAST(SUBSTRING(SuppQuotNo,1,6) AS varchar) = @prefix ORDER BY SuppQuotNo DESC) " +
                        " IF @code IS NULL " +
                        " BEGIN " +
                        " SET @code = @prefix+'0001' " +
                        " IF (SELECT COUNT(*) FROM TblCtrlNo WITH(READPAST))>0 " +
                        " BEGIN UPDATE TblCtrlNo SET SuppQuotNo = @prefix+'0002' " +
                        " END" +
                        " ELSE " +
                        " BEGIN" +
                        " INSERT INTO TblCtrlNo(SuppQuotNo) " +
                        " VALUES(@prefix+'0002') " +
                        " END " +
                        " END " +
                        " ELSE " +
                        " BEGIN " +
                        " DECLARE @newcode varchar(10) = (SELECT @prefix+REPLICATE('0',4-LEN(SUBSTRING(@code,7,4)+1)) +  " +
                        " CAST(SUBSTRING(@code,7,4)+1 AS varchar)) " +
                        " UPDATE TblCtrlNo " +
                        " SET SuppQuotNo = @newcode" +
                        " END " +
                        " SELECT @code as Code  ", conn, tr);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    sqNo = rd.GetString(0).Trim();
                }
                rd.Close();
                cmd = Connection.setTransactionCommand("INSERT INTO TblSuppQuotMain(SuppQuotNo,SupplierID,QuotRefNo,QuotDate,TermID,Status,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt) " +
                                                       " VALUES(@code,@SupplierID,@QuotRefNo,GETDATE(),@TermID,@Status,@CreatedBy,GETDATE(),@ModiefiedBy,GETDATE())", conn, tr);
                cmd.Parameters.AddWithValue("@code", sqNo);
                cmd.Parameters.AddWithValue("@SupplierID", quotationModel.SuppID);
                cmd.Parameters.AddWithValue("@QuotRefNo", "");
                cmd.Parameters.AddWithValue("@TermID", quotationModel.TermID);
                cmd.Parameters.AddWithValue("@Status", quotationModel.Status);
                cmd.Parameters.AddWithValue("@CreatedBy", Name01);
                cmd.Parameters.AddWithValue("@ModiefiedBy", Name01);
                int i = cmd.ExecuteNonQuery();
                if(i != 1)
                {
                    msg = "The data entered is already been added";
                }if(quotationModel.supplierQuotationDets != null)
                {
                    int j = quotationModel.supplierQuotationDets.Count();
                    foreach(var item in quotationModel.supplierQuotationDets)
                    {
                        int k = 0;
                        cmd = Connection.setTransactionCommand(" " +
                            " INSERT INTO TblSuppQuotDet(SuppQuotNo,PartNo,Qty,ListPrice,Discount,UnitPrice,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt) " +
                            " VALUES(@code,@PartNo,@Qty,@ListPrice,@Discount,@UnitPrice,@CreatedBy,GETDATE(),@ModifiedBy,GETDATE())",conn,tr);
                        cmd.Parameters.AddWithValue("@code",sqNo);
                        cmd.Parameters.AddWithValue("@PartNo", item.PartNo);
                        cmd.Parameters.AddWithValue("@Qty", item.Qty);
                        cmd.Parameters.AddWithValue("@ListPrice", item.ListPrice);
                        cmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        cmd.Parameters.AddWithValue("@Discount", item.Discount);
                        cmd.Parameters.AddWithValue("@ModifiedBy", Name01);
                        cmd.Parameters.AddWithValue("@CreatedBy", Name01);
                        k = cmd.ExecuteNonQuery();
                        if(k > 0)
                        {
                            Helper.TranLog("Supplier Quotation", "Added quotations:" + item.PartNo, conn,cmd, tr);
                        }
                        else
                        {
                            msg = "Quotation is already been added";
                            tr.Rollback();
                            conn.Close();
                            return msg;
                        }
                    }
                }
                Helper.TranLog("Supplier Quotation", "Success " + quotationModel.SuppQuotNo, conn, cmd, tr); 
                    tr.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                tr.Rollback();
            }
            finally
            {
                conn.Close();
                tr.Dispose();
            }
            return msg;
        }
    }
}
