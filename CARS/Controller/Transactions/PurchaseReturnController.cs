using CARS.Model;
using CARS.Model.Masterfiles;
using CARS.Model.Transactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CARS.Controller.Transactions
{
    internal class PurchaseReturnController : Universal<PurchaseReturnModel>
    {
        private static SqlConnection conn = Connection.GetConnection();
        private static SqlCommand cmd = null;
        private static SqlDataReader rd = null;
        private static SqlTransaction tr = null;
        private static SqlDataAdapter dataAdapter = null;
        public override string Create(PurchaseReturnModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(PurchaseReturnModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(PurchaseReturnModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(PurchaseReturnModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(PurchaseReturnModel entity)
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<string, string> getSuppliers()
        {
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();
            try
            {
                dictionary.Add("", "");
                conn.Open();
                cmd = Connection.setCommand(
                    "SELECT SLName, SLID FROM TblSubsidiaryMain WITH(READPAST) WHERE SLID LIKE 'SE%' ORDER BY SLName ASC",
                    conn);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    SupplierModel supplier = new SupplierModel();
                    supplier.SLID = rd.GetString(0).TrimEnd();
                    supplier.SLName = rd.GetString(1).TrimEnd();

                    dictionary.Add(supplier.SLID, supplier.SLName);
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

        public DataTable ReceivingListDisplay(string slid)
        {
            DataTable ret = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    "SELECT a.RRNo, b.EmployeeName, a.CreatedDt, a.Status " +
                    " FROM TblReceivingMain a WITH(READPAST) " +
                    " LEFT JOIN TblEmployeeMF b WITH(READPAST) ON b.EmployeeID = a.ReceivedBy " +
                    " WHERE Status = 2 AND SupplierID = @suppid",
                    conn);
                cmd.Parameters.AddWithValue("@suppid", slid);
                rd = cmd.ExecuteReader();
                ret.Load(rd);
            }
            catch(Exception ex) 
            {
            Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ret;
        }

        public DataTable ReceivingDetails(string rrno)
        {
            DataTable ret = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    "SELECT a.PartNo, c.DescName, d.BrandName, " +
                    "(SELECT SUM(Qty) FROM TblReceivingDetLoc WHERE RRNo = a.RRNo AND PartNo = a.PartNo)AS TTLQtyRcvd, a.UnitPrice, " +
                    "a.RRNo FROM TblReceivingDet a WITH(READPAST) " +
                    " LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                    " LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                    " LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                    " WHERE a.RRNo = @rrno",
                    conn);
                cmd.Parameters.AddWithValue("@rrno", rrno);
                rd = cmd.ExecuteReader();
                ret.Load(rd);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ret;
        }

        public DataTable LocationSelectionDisplay(string partNo,string rrno)
        {
            DataTable ret = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    "SELECT b.BinName, c.WhName ,a.LotNo, a.Qty FROM TblReceivingDetLoc a WITH(READPAST) " +
                    " LEFT JOIN TblWHLocationMF b WITH(READPAST) ON b.BinID = a.BinID " +
                    " LEFT JOIN TblWarehouseMF c WITH(READPAST) ON c.WhID = a.WhID " +
                    " WHERE a.RRNo = @rrno AND a.PartNo = @partno",
                    conn);
                cmd.Parameters.AddWithValue("@rrno", rrno);
                cmd.Parameters.AddWithValue("@partno", partNo);
                rd = cmd.ExecuteReader();
                ret.Load(rd);

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ret;
        }

        public DataTable PartsSelection(string rrno)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    " SELECT a.PartNo, d.PartName, d.OtherName, b.DescName,e.BrandName,d.Sku, c.UomName, a.UnitPrice, " +
                    " (SELECT SUM(Qty) - ISNULL((SELECT SUM(Qty) FROM TblPurchaseReturnDetLoc Z " +
                    " LEFT JOIN TblPurchaseReturnMain A WITH(READPAST) ON A.PurchRetNo = Z.PurchRetNo " +
                    " WHERE Z.PartNo = a.PartNo and A.RRNo = a.RRNo),0) " +
                    " FROM TblReceivingDetLoc WITH(READPAST) WHERE RRNo = a.RRNo AND PartNo = a.PartNo) AS TTLQtyRcvd " +
                    " FROM TblReceivingDet a WITH(READPAST) " +
                    " LEFT JOIN TblPartsMainMF d WITH(READPAST) ON d.PartNo = a.PartNo " +
                    " LEFT JOIN TblPartsDescriptionMF b WITH(READPAST) ON b.DescID = d.DescID " +
                    " LEFT JOIN TblPartsUomMF c WITH(READPAST) ON c.UomID = d.UomID " +
                    " LEFT JOIN TblPartsBrandMF e WITH(READPAST) ON e.BrandID = d.BrandID " +
                    " WHERE a.RRNo = @rrno AND (SELECT SUM(Qty) - ISNULL((SELECT SUM(Qty) FROM TblPurchaseReturnDetLoc Z " +
                    " LEFT JOIN TblPurchaseReturnMain A WITH(READPAST) ON A.PurchRetNo = Z.PurchRetNo\r\nWHERE Z.PartNo = a.PartNo and A.RRNo = a.RRNo),0)\r\nFROM TblReceivingDetLoc WITH(READPAST)\r\nWHERE RRNo = a.RRNo AND PartNo = a.PartNo) > 0\r\nORDER BY TTLQtyRcvd desc",
                    conn);
                cmd.Parameters.AddWithValue("@rrno", rrno);
                rd = cmd.ExecuteReader();
                dt.Load(rd);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable ReasonDisplay()
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT ReasonName FROM TblReasonMF", conn);
                rd = cmd.ExecuteReader();
                dt.Load(rd);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public string GetReasonID(string reasonName)
        {
            string reasonId = "";
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT ReasonID FROM TblReasonMF WHERE ReasonName = @reasonName", conn);
                cmd.Parameters.AddWithValue("@reasonName", reasonName);
                reasonId = cmd.ExecuteScalar().ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return reasonId;
        }

        //string lotno = "";
        //string binid = "";
        //string whid = "";
        public string SavePurchaseReturn(PurchaseReturnModel purchaseReturn)
        {
            string msg = "Save Purchase Return Successfully";
            string purchRetNo = "";
            try
            {
                conn.Open();
                tr = conn.BeginTransaction();
                cmd.Transaction = tr;
                cmd = Connection.setTransactionCommand(
                    "  Declare @prefix varchar(10) = 'PR'+RIGHT (YEAR(GETDATE()),2)+SUBSTRING(CONVERT(varchar,GETDATE(),23),6,2) " +
                    "  Declare @code varchar(10) = (SELECT TOP 1 PurchRetNo FROM TblCtrlNo WHERE CAST(SUBSTRING(PurchRetNo,1,6) AS varchar) = @prefix ORDER BY PurchRetNo DESC) " +
                    "  IF @code IS NULL " +
                    "  BEGIN  " +
                    "  SET @code = @prefix+'0001' " +
                    "  IF (SELECT COUNT(*) FROM TblCtrlNo WITH(READPAST))> 0 " +
                    "  BEGIN UPDATE TblCtrlNo SET PurchRetNo = @prefix+'0002' " +
                    "  END " +
                    "  ELSE " +
                    "  BEGIN" +
                    "  INSERT INTO TblCtrlNo(PurchRetNo) " +
                    "  VALUES(@prefix+'0002') " +
                    "  END " +
                    "  END" +
                    "  ELSE " +
                    "  BEGIN " +
                    "  DECLARE @newcode varchar(10) = (SELECT @prefix+REPLICATE('0',4-LEN(SUBSTRING(@code,7,4)+1)) + " +
                    "  CAST(SUBSTRING(@code,7,4)+1 AS varchar)) " +
                    "  UPDATE TblCtrlNo SET PurchRetNo = @newcode" +
                    "  END " +
                    "  SELECT @code as Code",
                    conn, tr);
                purchRetNo = cmd.ExecuteScalar().ToString();
                int i = 0;
                cmd = Connection.setTransactionCommand(
                    "INSERT INTO TblPurchaseReturnMain(PurchRetNo,RRNo,ReasonID,SupplierID,Remarks,Status,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt)" +
                    "VALUES(@purchretNo,@rrno,@reasonId,@suppId,@remarks,@status,@createby,GETDATE(),@modifiedby,GETDATE())",
                    conn, tr) ;
                cmd.Parameters.AddWithValue("@purchretNo", purchRetNo);
                cmd.Parameters.AddWithValue("@rrno", purchaseReturn.purchaseReturnMains.RRNo);
                cmd.Parameters.AddWithValue("@reasonId", purchaseReturn.purchaseReturnMains.ReasonID);
                cmd.Parameters.AddWithValue("@suppId", purchaseReturn.purchaseReturnMains.SupplierID);
                cmd.Parameters.AddWithValue("@remarks", purchaseReturn.purchaseReturnMains.Remarks);
                cmd.Parameters.AddWithValue("@status", purchaseReturn.purchaseReturnMains.Status);
                cmd.Parameters.AddWithValue("@createby", Name01);
                cmd.Parameters.AddWithValue("@modifiedby", Name01);
                i = cmd.ExecuteNonQuery();
                if(i > 0)
                {
                    foreach(var itm in purchaseReturn.purchaseReturnDets)
                    {
                        cmd = Connection.setTransactionCommand(
                            "INSERT INTO TblPurchaseReturnDet(PurchRetNo,PartNo,Qty,Status,ReasonID,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt)" +
                            " VALUES(@purchretNo,@partno,@qty,@status,@reasonId,@createby,GETDATE(),@modifiedby,GETDATE())",
                            conn, tr);
                        cmd.Parameters.AddWithValue("@purchretNo", purchRetNo);
                        cmd.Parameters.AddWithValue("@partno", itm.PartNo);
                        cmd.Parameters.AddWithValue("@qty", itm.Qty);
                        cmd.Parameters.AddWithValue("@status", itm.Status);
                        cmd.Parameters.AddWithValue("@reasonId", itm.ReasonID);
                        cmd.Parameters.AddWithValue("@createby", Name01);
                        cmd.Parameters.AddWithValue("@modifiedby", Name01);
                        int j = cmd.ExecuteNonQuery();
                        if (j != 1)
                        {
                            msg = "The information has already been added.";
                        }
                        if(j > 0)
                        {
                            decimal prtotalQty = itm.Qty;
                            cmd.Transaction = tr;
                            cmd = Connection.setTransactionCommand(
                                "SELECT a.PartNo, a.LotNo, a.BinID, a.WhID, a.Qty, c.WhPriority FROM TblReceivingDetLoc a WITH(READPAST) " +
                                " LEFT JOIN TblWHLocationMF b WITH(READPAST) ON b.BinID = a.BinID " +
                                " LEFT JOIN TblWarehouseMF c WITH(READPAST) ON c.WhID = a.WhID " +
                                " WHERE RRNo = @rrno AND PartNo = @partno ORDER BY c.WhPriority ASC",
                                conn, tr);
                            cmd.Parameters.AddWithValue("@rrno", purchaseReturn.purchaseReturnMains.RRNo);
                            cmd.Parameters.AddWithValue("@partno", itm.PartNo);
                            rd = cmd.ExecuteReader();
                            List<PurhcaseReturnLocatorGetter> locationGetter = new List<PurhcaseReturnLocatorGetter>();
                            List<PurhcaseReturnLocatorGetter> invReturn = new List<PurhcaseReturnLocatorGetter>();
                            while (rd.Read())
                            {
                                PurhcaseReturnLocatorGetter purchaseRetLoc = new PurhcaseReturnLocatorGetter
                                {
                                    LotNo = rd.GetString(1).TrimEnd(),
                                    WhID = rd.GetString(3).TrimEnd(),
                                    BinID = rd.GetString(2).TrimEnd(),
                                    Qty = rd.GetDecimal(4),
                                };
                                locationGetter.Add(purchaseRetLoc);
                            }
                            rd.Close();
                            decimal insrtqty = 0;
                            foreach (var itms in locationGetter)
                            {
                                if (prtotalQty != 0)
                                {
                                    if (prtotalQty > itms.Qty)
                                    {
                                        insrtqty = itms.Qty;
                                    }
                                    else
                                    {
                                        insrtqty = prtotalQty;
                                    }
                                    prtotalQty -= insrtqty;
                                    cmd = Connection.setTransactionCommand(
                                        " INSERT INTO TblPurchaseReturnDetLoc(PurchRetNo,PartNo,Qty,LotNo,WhID,BinID,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt)" +
                                        " VALUES(@purchretNo,@partno,@qty,@lotno,@whid,@binid,@createby,GETDATE(),@modifiedby,GETDATE())",
                                        conn, tr);
                                    cmd.Parameters.AddWithValue("@purchretNo", purchRetNo);
                                    cmd.Parameters.AddWithValue("@partno", itm.PartNo);
                                    cmd.Parameters.AddWithValue("@qty", insrtqty);
                                    cmd.Parameters.AddWithValue("@lotno", itms.LotNo);
                                    cmd.Parameters.AddWithValue("@whid", itms.WhID);
                                    cmd.Parameters.AddWithValue("@binid", itms.BinID);
                                    cmd.Parameters.AddWithValue("@createby", Name01);
                                    cmd.Parameters.AddWithValue("@modifiedby", Name01);
                                    int k = cmd.ExecuteNonQuery();
                                    if (k > 0)
                                    {
                                        cmd = Connection.setTransactionCommand(" SELECT Rcvd + SReturns + BegBal + TakeUp + TrfIn - PReturns - Picked - DefReturns - StockDrop - TrfOut AS BOH, " +
                                                                    " Rcvd, PartNo, BinID, LotNo FROM TblInvLotLoc WITH(READPAST) WHERE PartNo = @partno AND BinID = @binid AND LotNo = @lotno", conn,tr);
                                        cmd.Parameters.AddWithValue("@partno",itm.PartNo);
                                        cmd.Parameters.AddWithValue("@binid", itms.BinID);
                                        cmd.Parameters.AddWithValue("@lotno", itms.LotNo);
                                        rd = cmd.ExecuteReader();
                                        while (rd.Read())
                                        {
                                            PurhcaseReturnLocatorGetter placeholder = new PurhcaseReturnLocatorGetter
                                            {
                                                Boh = rd.GetDecimal(0),
                                                RcvdQty = rd.GetDecimal(1),
                                                PartNo = rd.GetString(2).TrimEnd(),
                                                BinID = rd.GetString(3).TrimEnd(),
                                                LotNo = rd.GetString(4).TrimEnd(),
                                            };
                                            invReturn.Add(placeholder);
                                        }
                                        rd.Close();
                                        }
                                        foreach(var item in invReturn)
                                        {
                                        if (itms.LotNo == item.LotNo && itms.BinID == item.BinID)
                                        {
                                            if (item.RcvdQty >= insrtqty && item.Boh >= insrtqty)
                                            {
                                                cmd = Connection.setTransactionCommand(
                                                    "UPDATE TblInvLotLoc SET PReturns = PReturns + @preturns WHERE PartNo = @partno AND BinID = @binid AND LotNo = @lotno",
                                                    conn, tr);
                                                cmd.Parameters.AddWithValue("@preturns", insrtqty);
                                                cmd.Parameters.AddWithValue("@partno", item.PartNo);
                                                cmd.Parameters.AddWithValue("@binid", item.BinID);
                                                cmd.Parameters.AddWithValue("@lotno", item.LotNo);
                                                int l = cmd.ExecuteNonQuery();
                                                if (l > 0)
                                                {
                                                    cmd = Connection.setTransactionCommand(
                                                        "UPDATE TblInvLot SET PReturns = PReturns + @preturns WHERE PartNo = @partno AND LotNo = @lotno",
                                                        conn, tr);
                                                    cmd.Parameters.AddWithValue("@preturns", itms.Qty);
                                                    cmd.Parameters.AddWithValue("@partno", item.PartNo);
                                                    cmd.Parameters.AddWithValue("@lotno", item.LotNo);
                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                            else
                                            {
                                                msg = "ReceiveQty is less than Qty " + itm.PartNo;
                                                tr.Rollback();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    tr.Commit();
                }
                
                else
                {
                    msg = "Something went wrong";
                    tr.Rollback();
                }
            }
            catch(Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return msg;
        }
    }
}
