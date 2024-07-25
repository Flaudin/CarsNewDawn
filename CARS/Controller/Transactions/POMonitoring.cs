using CARS.Functions;
using CARS.Model;
using CARS.Model.Transactions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Controller.Transactions
{
    internal class POMonitoring : Universal<POMonitoringModel> { 
                 private static SqlConnection conn = Connection.GetConnection();
                 private static SqlCommand cmd = null;
                 private static SqlDataReader rd = null;
                 private static SqlTransaction tr = null;
                 private static SqlDataAdapter dataAdapter = null;

        public POMonitoringModel ConsolodatedOrders()
        {
            POMonitoringModel pOMonitoringModel = new POMonitoringModel();
            try
            {
                conn.Open();
                cmd = Connection.setCommand($"SELECT a.OrdrNo, b.SLName, a.CreatedDt, a.CreatedBy FROM TblOrderMain a WITH(READPAST) " +
                                            " LEFT JOIN TblSubsidiaryMain b ON b.SLID = a.SupplierID", conn);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    pOMonitoringModel.OrderNo = rd.GetString(1).TrimEnd();
                    pOMonitoringModel.SupplierName = rd.GetString(2).TrimEnd();
                    pOMonitoringModel.CreatedDt = rd.GetString(3).TrimEnd();
                    pOMonitoringModel.CreatedBy = rd.GetString(4).TrimEnd();
                }
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);
            } 
            finally
            {
                conn.Close();
            }
            return pOMonitoringModel;
        }

        public override string Create(POMonitoringModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(POMonitoringModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(POMonitoringModel entity)
        {
            throw new NotImplementedException();
        }

        public DataTable PoOrderDet()
        {
            //PoMainModel poMainModel = new PoMainModel();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    " SELECT a.PONo, a.CreatedDt, a.CreatedBy, f.SLName, g.TermName, a.Status" +
                    " FROM TblPOMain a WITH(READPAST) " +
                    " LEFT JOIN TblPODet b ON b.PONo = a.PONo " +
                    " LEFT JOIN TblSubsidiaryMain f ON f.SLID = a.SupplierID " +
                    " LEFT JOIN TblTermsMF g ON g.TermID = a.TermID " +
                    " GROUP BY a.PONo, a.CreatedDt, a.CreatedBy, f.SLName, g.TermName, a.Status", conn);
                rd = cmd.ExecuteReader();
                dt.Load(rd);
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public string ClosePO(string pono)
        {
            string msg = "Successfully close PO";
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    "UPDATE TblPOMain SET Status = '8' WHERE PONo = @pono",
                    conn);
                cmd.Parameters.AddWithValue("@pono", pono);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                msg = ex.Message;
            }
            finally { conn.Close(); }
            return msg;
        }

        public string CancelPO(string pono)
        {
            string msg = "Successfully cancel PO";
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    "UPDATE TblPOMain SET Status = '9' WHERE PONo = @pono",
                    conn);
                cmd.Parameters.AddWithValue("@pono", pono);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                msg = ex.Message;
            }
            finally { conn.Close(); }
            return msg;
        }

        public DataTable PoOrderItemDet(string poNo)
        {
            PoMainModel poMainModel = new PoMainModel();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    " SELECT a.PartNo, c.DescName, d.BrandName, ISNULL(e.UomName,'') as UomID, a.UnitPrice, a.Discount, a.NetPrice, a.Qty, a.DeliveredQty " +
                    " FROM TblPODet a WITH(READPAST) " +
                    " LEFT JOIN TblPartsMainMF b ON b.PartNo = a.PartNo " +
                    " LEFT JOIN TblPartsDescriptionMF c ON c.DescID = b.DescID " +
                    " LEFT JOIN TblPartsBrandMF d ON d.BrandID = b.BrandID " +
                    " LEFT JOIN TblPartsUomMF e ON e.UomID = b.UomID" +
                    " WHERE a.PONo = @poNo", conn);
                cmd.Parameters.AddWithValue("@poNo", poNo);
                rd = cmd.ExecuteReader() ;
                if(rd != null)
                {
                dt.Load(rd);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable createPoDt(POMonitoringModel poMonitoringModel)
        {
            POMonitoringModel pOMonitoringModel = new POMonitoringModel();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand($"SELECT a.OrdrNo, b.SLName, a.CreatedDt, a.CreatedBy, a.SupplierID FROM TblOrderMain a WITH(READPAST) " +
                                            " LEFT JOIN TblSubsidiaryMain b ON b.SLID = a.SupplierID ORDER BY a.CreatedDt ASC", conn);
                rd = cmd.ExecuteReader();
                dt.Load(rd);
                while (rd.Read())
                {
                    pOMonitoringModel.OrderNo = rd.GetString(1).TrimEnd();
                    pOMonitoringModel.SupplierName = rd.GetString(2).TrimEnd();
                    pOMonitoringModel.CreatedDt = rd.GetString(3).TrimEnd();
                    pOMonitoringModel.CreatedBy = rd.GetString(4).TrimEnd();
                    pOMonitoringModel.SuppId = rd.GetString(5).TrimEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable getMonitoringDets(string ControlNo)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand($"SELECT a.OrdrNo, c.PartNo, c.OrdrQty, c.UnitPrice FROM TblOrderMain a WITH(READPAST) " +
                    $"                        LEFT JOIN TblSubsidiaryMain b ON b.SLID = a.SupplierID " +
                    $"                        LEFT JOIN TblOrderDet c ON c.OrdrNo = a.OrdrNo WHERE a.OrdrNo = @ordrNo", conn);
                cmd.Parameters.AddWithValue("@ordrNo", ControlNo);
                rd = cmd.ExecuteReader();
                dt.Load(rd);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close() ;
            }
            return dt;
        }

        public List<string> getSlid(string suppName)
        {
            List<string> suppID = new List<string>();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT SLID, TermID FROM TblSubsidiaryMain WHERE SLName = @suppName", conn);
                cmd.Parameters.AddWithValue("@suppName", suppName);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    suppID.Add(rd.GetString(0));
                    suppID.Add(rd.GetString(1));
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
            finally
            {
                conn.Close() ;
            }
              return suppID;
        }

        public PoDet getOrderData(string slid,string partno)
        {
            POMonitoringModel pOMonitoringModel = new POMonitoringModel();
            PoDet poDet = new PoDet();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT a.UnitPrice, a.Qty, a.Discount, b.TermID, a.ListPrice FROM TblSuppQuotDet a " +
                    "                        LEFT JOIN TblSuppQuotMain b ON b.SuppQuotNo = a.SuppQuotNo " +
                    "                        WHERE PartNo = @partno AND b.SupplierID = @slid", conn);
                cmd.Parameters.AddWithValue("@partno", partno);
                cmd.Parameters.AddWithValue("@slid", slid);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    poDet = new PoDet();
                    poDet.UnitPrice = rd.GetDecimal(0);
                    poDet.Qty = rd.GetDecimal(1);
                    poDet.Discount = rd.GetDecimal(2);
                    poDet.NetPrice = rd.GetDecimal(4);
                    poDet.Status = 1;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine (ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return poDet;
        }

        public List<decimal> getPricesVal(string slid,string partNo)
        {
            List<decimal> priceVal = new List<decimal>();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT a.Discount, a.ListPrice FROM TblSuppQuotDet a WITH(READPAST) " +
                    " LEFT JOIN TblSuppQuotMain b ON b.SuppQuotNo = a.SuppQuotNo " +
                    " WHERE b.SupplierID = @slid AND a.PartNo = @partno", conn);
                cmd.Parameters.AddWithValue("@slid", slid);
                cmd.Parameters.AddWithValue("@partno", partNo);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    priceVal.Add(rd.GetDecimal(0));
                    priceVal.Add(rd.GetDecimal(1));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return priceVal;
        }

        //public string savePO(CreatePo createPoModel)
        //{
        //    string msg = "PO Generated Success";
        //    string poNo = "";
        //    try
        //    {
        //        conn.Open();
        //        tr = conn.BeginTransaction();
        //        cmd = Connection.setTransactionCommand(
        //            " Declare @prefix varchar(10) = 'PO'+RIGHT (YEAR(GETDATE()),2)+SUBSTRING(CONVERT(varchar,GETDATE(),23),6,2) " +
        //                " Declare @code varchar(10) = (SELECT TOP 1 PoNo FROM TblCtrlNo WHERE CAST(SUBSTRING(PoNo,1,6) AS varchar) = @prefix ORDER BY PoNo DESC) " +
        //                " IF @code IS NULL " +
        //                " BEGIN " +
        //                " SET @code = @prefix+'0001' " +
        //                " IF (SELECT COUNT(*) FROM TblCtrlNo WITH(READPAST)) > 0 " +
        //                " BEGIN " +
        //                "   UPDATE TblCtrlNo SET PoNo = @prefix+'0002' " +
        //                " END " +
        //                " ELSE " +
        //                " BEGIN " +
        //                "   INSERT INTO TblCtrlNo(PoNo) " +
        //                " VALUES(@prefix+'0002') " +
        //                " END  " +
        //                " END  " +
        //                " ELSE " +
        //                " BEGIN DECLARE @newcode varchar(10) = (SELECT @prefix+REPLICATE('0',4-LEN(SUBSTRING(@code,7,4)+1)) +  " +
        //                " CAST(SUBSTRING(@code,7,4)+1 AS varchar)) " +
        //                " UPDATE TblCtrlNo " +
        //                " SET PoNo = @newcode" +
        //                " END " +
        //                " SELECT @code as Code  ", conn, tr);
        //        rd = cmd.ExecuteReader();
        //        while (rd.Read())
        //        {
        //            poNo = rd.GetString(0);
        //        }
        //        rd.Close();
        //        cmd = Connection.setTransactionCommand("INSERT INTO TblPOMain(PONo,SupplierID,TermID,Status,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt) " +
        //            "                                   VALUES(@code,@SupplierID,@TermID,@Status,@CreatedBy,GETDATE(),@ModiefiedBy,GETDATE())", conn, tr);
        //        cmd.Parameters.AddWithValue("@code",poNo.ToString());
        //        cmd.Parameters.AddWithValue("@SupplierID",createPoModel.SupplierID);
        //        cmd.Parameters.AddWithValue("@TermID",    createPoModel.TermID);
        //        cmd.Parameters.AddWithValue("@Status",createPoModel.Status);
        //        cmd.Parameters.AddWithValue("@CreatedBy",Name01);
        //        cmd.Parameters.AddWithValue("@ModiefiedBy", Name01);
        //        int i = cmd.ExecuteNonQuery();
        //        if(i != 1)
        //        {
        //            msg = "Already been added";
        //            tr.Rollback();
        //            conn.Close();
        //            return msg;
        //        }
        //        if(createPoModel.poDetails != null)
        //        {
        //            int j = createPoModel.poDetails.Count();
        //            foreach (var item in createPoModel.poDetails)
        //            {
        //                int k = 0;
        //                cmd = Connection.setTransactionCommand(
        //                    "INSERT INTO TblPODet(PONo,PartNo,Qty,UnitPrice,Discount,NetPrice,Status,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt) " +
        //                    "VALUES (@code,@partno,@qty,@unitprice,@discount,@netprice,@status,@createdby,GETDATE(),@modifiedby,GETDATE())",conn, tr);
        //                cmd.Parameters.AddWithValue("@code",poNo);
        //                cmd.Parameters.AddWithValue("@partno",item.PartNo);
        //                cmd.Parameters.AddWithValue("@qty",item.Qty);
        //                cmd.Parameters.AddWithValue("@unitprice",item.UnitPrice);
        //                cmd.Parameters.AddWithValue("@discount",item.Discount);
        //                cmd.Parameters.AddWithValue("@netprice",item.NetPrice);
        //                cmd.Parameters.AddWithValue("@status",item.Status);
        //                cmd.Parameters.AddWithValue("@createdby",Name01);
        //                cmd.Parameters.AddWithValue("@modifiedby",Name01);
        //                k = cmd.ExecuteNonQuery();
        //                if(k > 0)
        //                {
        //                    Helper.TranLog("PO Generation", "Generated PO success:" + item.PartNo, conn, cmd, tr);
        //                }
        //                else
        //                {
        //                    msg = "PO is alredy been generated";
        //                    tr.Rollback();
        //                    conn.Close();
        //                    return msg;
        //                }
        //            }
        //        }
        //        Helper.TranLog("PO Generation", "Success" + createPoModel.PONo, conn, cmd, tr);
        //        tr.Commit();
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        msg = ex.Message;
        //        tr.Rollback();
        //    }
        //    finally
        //    {
        //        conn.Close();
        //        tr.Dispose();
        //    }
        //    return msg;
        //}

        public async Task<ResponseModel> sendToBSB(CreatePo poModel)
        {
               ResponseModel resp = new ResponseModel();
            using (             HttpClient client = new HttpClient())
                    {
                var data = new SendToBsb
                {
                    CustId = "GS-CASH",
                    Username = "gstestuser1",
                    Transcode = "",
                    Area = "",
                    PoName = "",
                    Term = poModel.TermID,
                    PoItemsList = poModel.poDetails
                };
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                Console.WriteLine(data.ToString());
                Console.WriteLine(json.ToString());
                try
                {
                    var response = await client.PostAsync("http://172.16.15.10:96/api/cars/sendpo", content).ConfigureAwait(true);
                    //response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    resp = JsonConvert.DeserializeObject<ResponseModel>(responseBody);
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return resp;
            }
        }

        public SortedDictionary<string, string> savePO(CreatePo createPoModel)
        {
            string msg = "PO Generated Success";
            string poNo = "";
            SortedDictionary<string,string> dictionary = new SortedDictionary<string,string>();
            try
            {
                conn.Open();
                tr = conn.BeginTransaction();
                cmd = Connection.setTransactionCommand(
                    " Declare @prefix varchar(10) = 'PO'+RIGHT (YEAR(GETDATE()),2)+SUBSTRING(CONVERT(varchar,GETDATE(),23),6,2) " +
                        " Declare @code varchar(10) = (SELECT TOP 1 PoNo FROM TblCtrlNo WHERE CAST(SUBSTRING(PoNo,1,6) AS varchar) = @prefix ORDER BY PoNo DESC) " +
                        " IF @code IS NULL " +
                        " BEGIN " +
                        " SET @code = @prefix+'0001' " +
                        " IF (SELECT COUNT(*) FROM TblCtrlNo WITH(READPAST)) > 0 " +
                        " BEGIN " +
                        "   UPDATE TblCtrlNo SET PoNo = @prefix+'0002' " +
                        " END " +
                        " ELSE " +
                        " BEGIN " +
                        "   INSERT INTO TblCtrlNo(PoNo) " +
                        " VALUES(@prefix+'0002') " +
                        " END  " +
                        " END  " +
                        " ELSE " +
                        " BEGIN DECLARE @newcode varchar(10) = (SELECT @prefix+REPLICATE('0',4-LEN(SUBSTRING(@code,7,4)+1)) +  " +
                        " CAST(SUBSTRING(@code,7,4)+1 AS varchar)) " +
                        " UPDATE TblCtrlNo " +
                        " SET PoNo = @newcode" +
                        " END " +
                        " SELECT @code as Code  ", conn, tr);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    poNo = rd.GetString(0);
                }
                rd.Close();
                cmd = Connection.setTransactionCommand("INSERT INTO TblPOMain(PONo,SupplierID,TermID,Status,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt) " +
                    "                                   VALUES(@code,@SupplierID,@TermID,@Status,@CreatedBy,GETDATE(),@ModiefiedBy,GETDATE())", conn, tr);
                cmd.Parameters.AddWithValue("@code", poNo.ToString());
                cmd.Parameters.AddWithValue("@SupplierID", createPoModel.SupplierID);
                cmd.Parameters.AddWithValue("@TermID", createPoModel.TermID);
                cmd.Parameters.AddWithValue("@Status", createPoModel.Status);
                cmd.Parameters.AddWithValue("@CreatedBy", Name01);
                cmd.Parameters.AddWithValue("@ModiefiedBy", Name01);
                int i = cmd.ExecuteNonQuery();
                if (i != 1)
                {
                    msg = "Already been added";
                    tr.Rollback();
                    conn.Close();
                    return dictionary;
                }
                if (createPoModel.poDetails != null)
                {
                    int j = createPoModel.poDetails.Count();
                    foreach (var item in createPoModel.poDetails)
                    {
                        int k = 0;
                        cmd = Connection.setTransactionCommand(
                            "INSERT INTO TblPODet(PONo,PartNo,Qty,UnitPrice,Discount,NetPrice,Status,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt) " +
                            "VALUES (@code,@partno,@qty,@unitprice,@discount,@netprice,@status,@createdby,GETDATE(),@modifiedby,GETDATE())", conn, tr);
                        cmd.Parameters.AddWithValue("@code", poNo);
                        cmd.Parameters.AddWithValue("@partno", item.PartNo);
                        cmd.Parameters.AddWithValue("@qty", item.Qty);
                        cmd.Parameters.AddWithValue("@unitprice", item.UnitPrice);
                        cmd.Parameters.AddWithValue("@discount", item.Discount);
                        cmd.Parameters.AddWithValue("@netprice", item.NetPrice);
                        cmd.Parameters.AddWithValue("@status", item.Status);
                        cmd.Parameters.AddWithValue("@createdby", Name01);
                        cmd.Parameters.AddWithValue("@modifiedby", Name01);
                        k = cmd.ExecuteNonQuery();
                        if (k > 0)
                        {
                            Helper.TranLog("PO Generation", "Generated PO success:" + item.PartNo, conn, cmd, tr);
                        }
                        else
                        {
                            msg = "PO is alredy been generated";
                            tr.Rollback();
                            conn.Close();
                            return dictionary;
                        }
                    }
                }
                Helper.TranLog("PO Generation", "Success" + createPoModel.PONo, conn, cmd, tr);
                tr.Commit();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            dictionary.Add(msg, poNo);
            return dictionary;
        }

        public override void Read(POMonitoringModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(POMonitoringModel entity)
        {
            throw new NotImplementedException();
        }

        public string getCompanyName()
        {
            string companyName = "";
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT CompName FROM TblCompanyProfile", conn);
                companyName = cmd.ExecuteScalar().ToString();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return companyName;
        }

        public string getSubheader()
        {
            string subhead = "";
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT NoStreet FROM TblCompanyProfile", conn);
                subhead = cmd.ExecuteScalar().ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return subhead;
        }

        public string getTin()
        {
            string tinNo = "";
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT TinNo FROM TblCompanyProfile", conn);
                tinNo = cmd.ExecuteScalar().ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine (ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return tinNo;
        }

        public string getCompanyImage()
        {
            string companyImage = "";
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT CompLogo FROM TblCompanyProfile", conn);
                companyImage = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return companyImage;
        }

        public CreatePo poPrinting(string poNo)
        {
            CreatePo poInfo = new CreatePo();
            List<PritingPO> poPrinting = new List<PritingPO>();
            PritingPO forPrinting = new PritingPO();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    " SELECT a.Qty, e.UomName, c.DescName, d.BrandName, a.UnitPrice, a.Qty * a.UnitPrice AS Amount FROM TblPODet a WITH(READPAST) " +
                    " LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                    " LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                    " LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                    " LEFT JOIN TblPartsUomMF e WITH(READPAST) ON e.UomID = b.UomID " +
                    " WHERE a.PONo = @poNo", conn);
                cmd.Parameters.AddWithValue("@poNo", poNo);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    forPrinting = new PritingPO
                    {
                        Qty = rd.GetDecimal(0),
                        Unit = rd.GetString(1).TrimEnd(),
                        Description = rd.GetString(2).TrimEnd(),
                        Brand = rd.GetString(3).TrimEnd(),
                        UnitPrice = rd.GetDecimal(4),
                        Amount = rd.GetDecimal(5),
                    };
                    poPrinting.Add(forPrinting);
                }
                poInfo = new CreatePo
                {
                    printingPO = poPrinting,
                };  
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return poInfo;
        }

    }
}
