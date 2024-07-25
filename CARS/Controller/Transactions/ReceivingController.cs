using CARS.Functions;
using CARS.Model;
using CARS.Model.Masterfiles;
using CARS.Model.Transactions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Controller.Transactions
{
    internal class ReceivingController : Universal<ReceivingModel>
    {
        private static SqlConnection conn = Connection.GetConnection();
        private static SqlCommand cmd = null;
        private static SqlDataReader rd = null;
        private static SqlTransaction tr = null;
        private static SqlDataAdapter dataAdapter = null;


        public override string Create(ReceivingModel entity)
        {
            string msg = "Information saved successfully";
            string rrNO = "";
            try
            {
                conn.Open();
                cmd = Connection.setTransactionCommand(
                    "  Declare @prefix varchar(10) = 'RR'+RIGHT (YEAR(GETDATE()),2)+SUBSTRING(CONVERT(varchar,GETDATE(),23),6,2) " +
                    "  Declare @code varchar(10) = (SELECT TOP 1 RRNo FROM TblCtrlNo WHERE CAST(SUBSTRING(RRNo,1,6) AS varchar) = @prefix ORDER BY RRNo DESC) " +
                    "  IF @code IS NULL " +
                    "  BEGIN  " +
                    "  SET @code = @prefix+'0001' " +
                    "  IF (SELECT COUNT(*) FROM TblCtrlNo WITH(READPAST))> 0 " +
                    "  BEGIN UPDATE TblCtrlNo SET RRNo = @prefix+'0002' " +
                    "  END " +
                    "  ELSE " +
                    "  BEGIN" +
                    "  INSERT INTO TblCtrlNo(RRNo) " +
                    "  VALUES(@prefix+'0002') " +
                    "  END " +
                    "  END" +
                    "  ELSE " +
                    "  BEGIN " +
                    "  DECLARE @newcode varchar(10) = (SELECT @prefix+REPLICATE('0',4-LEN(SUBSTRING(@code,7,4)+1)) + " +
                    "  CAST(SUBSTRING(@code,7,4)+1 AS varchar)) " +
                    "  UPDATE TblCtrlNo SET RRNo = @newcode" +
                    "  END " +
                    "  SELECT @code as Code",
                    conn, tr);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    rrNO = rd.GetString(0);
                }
                rd.Close();
                cmd = Connection.setTransactionCommand(
                    "INSERT INTO TblReceivingMain(RRNo,PONo,SupplierID,TermID,InvoiceNo,InvoiceDt,ReceivedBy,Remarks,Status,ReasonID,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt) " +
                    "VALUES (@rrno,@pono,@supplierid,@termid,@invoiceno,@invoicedt,@receivedby,@remarks,@status,@reason,@createdby,GETDATE(),@modifiedby,GETDATE())",
                    conn,tr);
                cmd.Parameters.AddWithValue("@rrno",rrNO);
                cmd.Parameters.AddWithValue("@pono",entity.PONo);
                cmd.Parameters.AddWithValue("@supplierid",entity.SupplierID);
                cmd.Parameters.AddWithValue("@termid",entity.TermId);
                cmd.Parameters.AddWithValue("@invoiceno",entity.InvoiceNo);
                cmd.Parameters.AddWithValue("@invoicedt",entity.InvoiceDt);
                cmd.Parameters.AddWithValue("@receivedby",entity.ReceivedBy);
                cmd.Parameters.AddWithValue("@remarks",entity.Remarks);
                cmd.Parameters.AddWithValue("@status",entity.Status);
                cmd.Parameters.AddWithValue("@reason",entity.ReasonId);
                cmd.Parameters.AddWithValue("@createdby",Name01);
                cmd.Parameters.AddWithValue("@modifiedby",Name01);
                int i = cmd.ExecuteNonQuery();
                if (i != 1)
                {
                    msg = "The information has already been added.";
                }
                if (entity.receivingDets != null)
                {
                    int j = entity.receivingDets.Count();
                    foreach (var item in entity.receivingDets)
                    {
                        int k = 0;
                        cmd = Connection.setTransactionCommand(
                            " INSERT INTO TblReceivingDet(RRNo,PartNo,Qty,UnitPrice,VATAmt,Status,FreeItem,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt)" +
                            " VALUES(@rrno,@partno,@qty,@unitprice,@vatamt,@status,@freeitm,@createdby,GETDATE(),@modifiedby,GETDATE())",
                            conn, tr);
                        cmd.Parameters.AddWithValue("@rrno", rrNO);
                        cmd.Parameters.AddWithValue("@partno", item.PartNo);
                        cmd.Parameters.AddWithValue("@qty", item.Qty);
                        cmd.Parameters.AddWithValue("@unitprice", item.UnitPrice);
                        cmd.Parameters.AddWithValue("@vatamt", item.VATAmt);
                        cmd.Parameters.AddWithValue("@status", item.Status);
                        cmd.Parameters.AddWithValue("@freeitm", item.Freeitem);
                        cmd.Parameters.AddWithValue("@createdby", Name01);
                        cmd.Parameters.AddWithValue("@modifiedby", Name01);
                        k = cmd.ExecuteNonQuery();
                        if (k > 0)
                        {
                            Helper.TranLog("Receiving", " Saving Receive" + entity.PONo, conn, cmd, tr);
                        }
                        else
                        {
                            msg = "Received save successfully";
                            tr.Rollback();
                            conn.Open();
                            return msg;
                        }
                    }
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
           // throw new NotImplementedException();
        }

        public override void Delete(ReceivingModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(ReceivingModel entity)
        {
            throw new NotImplementedException();
        }

        public DataTable PODisplay(string slid, string termid, string rrno)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                if (String.IsNullOrEmpty(rrno) || rrno == "NEW RECORD")
                {
                    cmd = Connection.setCommand(
                                        " SELECT a.PONo, a.CreatedDt, a.CreatedBy, a.Status FROM TblPOMain a WITH(READPAST)" +
                                        " LEFT JOIN TblSubsidiaryMain b WITH(READPAST) ON b.SLID = a.SupplierID " +
                                        " WHERE b.SLID = @slid AND a.TermID = @termid AND ISNULL((SELECT COUNT(PartNo) FROM TblPODet WHERE PONo = a.PONo),0) >  ISNULL((SELECT COUNT(PartNo) FROM TblReceivingDetPO WHERE PONo = a.PONo),0)",
                                        conn);
                    cmd.Parameters.AddWithValue("@slid", slid);
                    cmd.Parameters.AddWithValue("@termid", termid);
                } else
                {
                    cmd = Connection.setCommand(
                                        "SELECT DISTINCT a.PONo, a.CreatedDt, a.CreatedBy, a.Status FROM TblPOMain a WITH(READPAST) " +
                                        "LEFT JOIN TblSubsidiaryMain b WITH(READPAST) ON b.SLID = a.SupplierID " +
                                        "WHERE b.SLID = @slid AND a.TermID = @termid AND ISNULL((SELECT COUNT(PartNo) FROM TblPODet WHERE PONo = a.PONo), 0) > ISNULL((SELECT COUNT(PartNo) FROM TblReceivingDetPO WHERE PONo = a.PONo),0) " +
                                        "UNION " +
                                        "SELECT DISTINCT z.PONo, x.CreatedDt,x.CreatedBy,1 " +
                                        "FROM TblReceivingDetPO z WITH(READPAST) " +
                                        "LEFT JOIN TblReceivingMain y WITH(READPAST) ON y.RRNo =  z.RRNo " +
                                        "LEFT JOIN TblPOMain x WITH(READPAST) ON x.PONo = z.PONo " +
                                        "WHERE y.SupplierID = @slid AND y.TermID = @termid AND y.Status != 2 AND z.RRNo = @rrno",
                                        conn);
                    cmd.Parameters.AddWithValue("@slid", slid);
                    cmd.Parameters.AddWithValue("@termid", termid);
                    cmd.Parameters.AddWithValue("@rrno", rrno);
                }
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

        public DataTable ReceivingItems(string PONo,string rrno) 
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                if (String.IsNullOrEmpty(rrno) || rrno == "NEW RECORD")
                {
                    cmd = Connection.setCommand(
                    " SELECT a.PONo, c.SLName, d.TermName, e.Sku, a.PartNo, f.DescName, g.BrandName, e.UomID, 0.00 AS Discount, a.NetPrice,0 as TtlRecQuantity, a.Qty, 0 AS FreeItem, 0 AS TotalPrice " +
                    " FROM TblPODet a WITH(READPAST) " +
                    " LEFT JOIN TblPOMain b ON b.PONo = a.PONo " +
                    " LEFT JOIN TblSubsidiaryMain c ON c.SLID = b.SupplierID " +
                    " LEFT JOIN TblTermsMF d ON d.TermID = b.TermID " +
                    " LEFT JOIN TblPartsMainMF e ON e.PartNo = a.PartNo " +
                    " LEFT JOIN TblPartsDescriptionMF f ON f.DescID = e.DescID" +
                    " LEFT JOIN TblPartsBrandMF g ON g.BrandID = e.BrandID  " +
                    " WHERE a.PONo = @PONo AND a.PartNo NOT IN (SELECT PartNo FROM TblReceivingDetPO WHERE PONo = a.PONo) ",
                    conn);
                    cmd.Parameters.AddWithValue("@PONo", PONo);
                }
                else
                {
                    cmd = Connection.setCommand(
                    " SELECT a.PONo, c.SLName, d.TermName, e.Sku, a.PartNo, f.DescName, g.BrandName, e.UomID, 0.00 AS Discount, a.NetPrice,0 as TtlRecQuantity, a.Qty, 0 AS FreeItem, 0 AS TotalPrice " +
                    "FROM TblPODet a WITH(READPAST) " +
                    "LEFT JOIN TblPOMain b ON b.PONo = a.PONo " +
                    "LEFT JOIN TblSubsidiaryMain c ON c.SLID = b.SupplierID " +
                    "LEFT JOIN TblTermsMF d ON d.TermID = b.TermID " +
                    "LEFT JOIN TblPartsMainMF e ON e.PartNo = a.PartNo " +
                    "LEFT JOIN TblPartsDescriptionMF f ON f.DescID = e.DescID " +
                    "LEFT JOIN TblPartsBrandMF g ON g.BrandID = e.BrandID " +
                    "WHERE a.PONo = @PONo AND a.PartNo IN(SELECT PartNo FROM TblReceivingDetPO WHERE PONo = a.PONo AND RRNo = @rrno) " +
                    "UNION " +
                    "SELECT a.PONo, c.SLName, d.TermName, e.Sku, a.PartNo, f.DescName, g.BrandName, e.UomID, 0.00 AS Discount, a.NetPrice, 0 as TtlRecQuantity, a.Qty, 0 AS FreeItem, 0 AS TotalPrice " +
                    "FROM TblPODet a WITH(READPAST) " +
                    "LEFT JOIN TblPOMain b ON b.PONo = a.PONo " +
                    "LEFT JOIN TblSubsidiaryMain c ON c.SLID = b.SupplierID " +
                    "LEFT JOIN TblTermsMF d ON d.TermID = b.TermID " +
                    "LEFT JOIN TblPartsMainMF e ON e.PartNo = a.PartNo " +
                    "LEFT JOIN TblPartsDescriptionMF f ON f.DescID = e.DescID " +
                    "LEFT JOIN TblPartsBrandMF g ON g.BrandID = e.BrandID " +
                    "WHERE a.PONo = @PONo AND a.PartNo NOT IN(SELECT PartNo FROM TblReceivingDetPO WHERE PONo = @PONo AND RRNo != @rrno) ",
                    conn);
                    cmd.Parameters.AddWithValue("@PONo", PONo);
                    cmd.Parameters.AddWithValue("@rrno", rrno);
                }
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

        public DataTable ReceivingItemRR(string rrno)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    "  SELECT '' as PONo, '' as SLName, '' as TermName,b.Sku, b.PartNo, c.DescName, d.BrandName, b.UomID, 0.00 AS Discount, a.UnitPrice as NetPrice,Qty as TtlRecQuantity," +
                    " ISNULL((SELECT SUM(A.Qty) FROM TblPODet A WHERE PONo IN (SELECT PONo FROM TblReceivingDetPO WHERE RRNo = @rrno AND PartNo = a.PartNo) AND PartNo = a.PartNo),0) as Qty, " +
                    " ISNULL(a.FreeItem,0) AS FreeItem, a.UnitPrice * a.Qty AS TotalPrice " +
                    "  FROM TblReceivingDet a WITH(READPAST)   LEFT JOIN TblPartsMainMF b ON b.PartNo = a.PartNo    LEFT JOIN TblPartsDescriptionMF c ON c.DescID = b.DescID  " +
                    "  LEFT JOIN TblPartsBrandMF d ON d.BrandID = b.BrandID  WHERE a.RRNo = @rrno",
                    conn);
                cmd.Parameters.AddWithValue("@rrno", rrno);
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

        public DataTable ReceivingParts(string partno)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    " SELECT TOP 1 a.Sku, a.PartNo, b.DescName, c.BrandName, ISNULL(g.UomName,'') as UomID,0.00 AS Discount, a.ListPrice AS NetPrice,0 as TtlRecQuantity, 0 AS Qty," +
                    " 0 AS FreeItem, 0.00 AS TotalPrice " +
                    " FROM TblPartsMainMF a WITH(READPAST) " +
                    " LEFT JOIN TblPartsDescriptionMF b WITH(READPAST) ON b.DescID = a.DescID " +
                    " LEFT JOIN TblPartsBrandMF c WITH(READPAST) ON c.BrandID = a.BrandID " +
                    " LEFT JOIN TblInvLotLoc d WITH(READPAST) ON d.PartNo = a.PartNo " +
                    " LEFT JOIN TblWarehouseMF e WITH(READPAST) ON e.WhID = d.WhID " +
                    " LEFT JOIN TblWHLocationMF f WITH(READPAST) ON f.BinID = d.BinID" +
                    " LEFT JOIN TblPartsUomMF g WITH(READPAST) ON g.UomID = a.UomID " +
                    " WHERE a.PartNo = @partno " +
                    " ORDER BY d.ModifiedDt DESC",
                    conn);
                cmd.Parameters.AddWithValue("@partno", partno);
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

        public override void Read(ReceivingModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(ReceivingModel entity)
        {
            throw new NotImplementedException();
        }

        public DataTable PartsSelection(PartsModel entity, string keyword,bool isBSBItem,bool isCriticalITem)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(" SELECT RTRIM(a.PartNo) AS PartNoParts, RTRIM(b.BrandName) AS BrandName, " +
                    "                         RTRIM(c.DescName) as DescName FROM TblPartsMainMF a WITH(READPAST) " +
                    "                         LEFT JOIN TblPartsBrandMF b WITH(READPAST) ON b.BrandID = a.BrandID " +
                    "                         LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = a.DescID " +
                    "                         WHERE (1=(CASE WHEN ISNULL(@brandID,'') = '' THEN 1 ELSE 0 END) OR b.BrandID =  @brandID OR b.BrandName LIKE '%'+@brandID+'%')  " +
                    "                         AND (1=(CASE WHEN ISNULL(@descID, '') = '' THEN 1 ELSE 0 END) OR c.DescID = @descID OR c.DescName LIKE '%'+@descID+'%')  " +
                    "                         AND (1=(CASE WHEN ISNULL(@isBSBItem, 0) = 0 THEN 1 ELSE 0 END) OR ISNULL(a.BPartNo,'') <> '') " +
                    "                         AND (1=(CASE WHEN ISNULL(@keyword, '') = '' THEN 1 ELSE 0 END) OR a.PartName LIKE '%' + @keyword + '%' " +
                    "                         OR b.BrandName LIKE '%' + @keyword + '%' OR c.DescName LIKE '%' + @keyword + '%') " +
                    "                         AND a.IsActive = 1 ", conn);
                cmd.Parameters.AddWithValue("@brandID", entity.Brand);
                cmd.Parameters.AddWithValue("@descID", entity.Description);
                cmd.Parameters.AddWithValue("@keyword", keyword);
                cmd.Parameters.AddWithValue("@isBSBItem", isBSBItem);
                rd = cmd.ExecuteReader();
                dt.Load(rd);
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

       

        public DataTable LocationDisplayWRR(string partNo,string rrno,int i)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                if(i == 1)
                {
                    cmd = Connection.setCommand(
                    "  SELECT DISTINCT RTRIM(b.BinName) AS BinName, RTRIM(c.WhName) AS WhName, '' AS LotNo,a.Qty, b.BinID, b.WhID, a.PartNo " +
                    "    FROM TblReceivingDetLoc a WITH(READPAST) " +
                    "    LEFT JOIN TblWHLocationMF b WITH(READPAST) ON b.BinID = a.BinID  " +
                    "    LEFT JOIN TblWarehouseMF c WITH(READPAST) ON c.WhID = a.WhID " +
                    "    LEFT JOIN TblReceivingDet e WITH(READPAST) ON e.RRNo = a.RRNo" +
                    "    WHERE a.PartNo = @partno AND a.RRNo = @rrno ",
                    conn);
                    cmd.Parameters.AddWithValue("@partno", partNo);
                    cmd.Parameters.AddWithValue("@rrno", rrno);
                } else
                {
                    cmd = Connection.setCommand(
                    "  SELECT DISTINCT RTRIM(b.BinName) AS BinName, RTRIM(c.WhName) AS WhName, '' AS LotNo,a.Qty, b.BinID, b.WhID, a.PartNo " +
                    "    FROM TblReceivingDetLoc a WITH(READPAST) " +
                    "    LEFT JOIN TblWHLocationMF b WITH(READPAST) ON b.BinID = a.BinID  " +
                    "    LEFT JOIN TblWarehouseMF c WITH(READPAST) ON c.WhID = a.WhID " +
                    "    LEFT JOIN TblReceivingDet e WITH(READPAST) ON e.RRNo = a.RRNo" +
                    "    WHERE a.RRNo = @rrno ",
                    conn);
                    cmd.Parameters.AddWithValue("@rrno", rrno);
                }
                
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


        public DataTable SearchLocation(string partno,string keyword,string whereclause)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    "   SELECT DISTINCT RTRIM(a.BinName) AS BinName, RTRIM(c.WhName) AS WhName, '' AS LotNo, a.BinID, a.WhID " +
                    "   FROM TblWHLocationMF a WITH(READPAST) " +
                    "   LEFT JOIN TblWarehouseMF c WITH(READPAST) ON c.WhID = a.WhID  " +
                    "   WHERE ISNULL(c.IsWebStore,0) <> 1 AND a.BinID NOT IN (@whereclause) AND a.BinID NOT IN(SELECT DISTINCT BinID FROM TblInvLotLoc WHERE PartNo = @partno)  AND  (a.BinName LIKE '%'+@keyword+'%' OR a.BinID LIKE '%'+@keyword+'%' OR c.WhName LIKE '%'+@keyword+'%') ",
                    conn);
                cmd.Parameters.AddWithValue("@whereclause", whereclause);
                cmd.Parameters.AddWithValue("@keyword", keyword.Trim());
                cmd.Parameters.AddWithValue("@partno", partno.Trim());
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


        //ginagamit pag new record
        public DataTable LocationDisplay(string partNo)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    " SELECT DISTINCT RTRIM(b.BinName) AS BinName, RTRIM(c.WhName) AS WhName, '' AS LotNo, 0 as Qty, b.BinID, b.WhID, a.PartNo " +
                    " FROM TblInvLotLoc a WITH(READPAST) " +
                    " LEFT JOIN TblWHLocationMF b WITH(READPAST) ON b.BinID = a.BinID " +
                    " LEFT JOIN TblWarehouseMF c WITH(READPAST) ON c.WhID = a.WhID " +
                    " WHERE a.PartNo = @partNo",
                    conn);
                cmd.Parameters.AddWithValue("@partNo", partNo);
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

        public SortedDictionary<string, string> getSupplier(bool isRushPo)
        {
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();
            try
            {
                dictionary.Add("", "");
                conn.Open();
                if (isRushPo)
                {
                    cmd = Connection.setCommand("SELECT DISTINCT SLID, SLName FROM TblSubsidiaryMain A WITH(READPAST) WHERE SLID LIKE 'SE%' ORDER BY A.SLName ASC",
                    conn);
                } else
                {
                    cmd = Connection.setCommand(
                                        "SELECT DISTINCT SLID, SLName FROM TblPOMain A WITH(READPAST) LEFT JOIN TblSubsidiaryMain B WITH(READPAST) ON A.SupplierID = B.SLID " +
                                        " WHERE SLID LIKE 'SE%'  AND (SELECT COUNT(*) FROM TblPODet WHERE PONo = A.PONo) > ISNULL((SELECT COUNT(*) FROM TblReceivingDetPO WHERE PONo = A.PONo),0)   " +
                                        " ORDER BY B.SLName ASC",
                                        conn);
                }
                
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    SupplierModel supplier = new SupplierModel();
                    supplier.SLID = rd.GetString(0).TrimEnd();
                    supplier.SLName = rd.GetString(1).TrimEnd();

                    dictionary.Add(supplier.SLID, supplier.SLName);
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
            return dictionary;
        }



        public ReceivingModel getRRDetails(string rrno)
        {
            ReceivingModel result = new ReceivingModel();
            try
            {
                conn.Open();

                cmd = Connection.setCommand($"SELECT A.RRNo,A.SupplierID,ISNULL(B.SLName,'') as SLName,A.TermID,ISNULL(C.TermName,'') as TermName,ISNULL(InvoiceNo,'') as InvoiceNo, " +
                    $" ISNULL(CONVERT(varchar, InvoiceDt, 23), '') as InvoiceDt, ISNULL(Remarks, '') as Remarks,ISNULL(CONVERT(varchar,A.CreatedDt,23),'') as CreatedDt, " +
                    $" CASE WHEN ISNULL((SELECT COUNT(*) FROM TblReceivingDetPO WHERE RRNo = @rrno),0) > 0 THEN 0 ELSE 1 END as isRushORder " +
                    $"FROM TblReceivingMain A " +
                    $"LEFT JOIN TblSubsidiaryMain B WITH(READPAST) ON B.SLID = A.SupplierID " +
                    $"LEFT JOIN TblTermsMF C WITH(READPAST) ON C.TermID = A.TermID " +
                    $"WHERE RRNo = @rrno ", conn);
                cmd.Parameters.AddWithValue("@rrno", rrno);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    result.RRNo = rd.GetString(0).TrimEnd();
                    result.SupplierID = rd.GetString(1).TrimEnd();
                    result.TermId = rd.GetString(3).TrimEnd();
                    result.InvoiceNo = rd.GetString(5).TrimEnd();
                    result.InvoiceDt = rd.GetString(6).TrimEnd();
                    result.Remarks = rd.GetString(7).TrimEnd();
                    result.CreatedDt = rd.GetString(8).TrimEnd();
                    result.RushOrder = rd.GetInt32(9);
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
            return result;
        }


        public SortedDictionary<string, string> getTermsSupplier(string slid)
        {
            SortedDictionary<string, string> Termsdictionary = new SortedDictionary<string, string>();
            try
            {
                conn.Open();

                cmd = Connection.setCommand($"SELECT  A.TermID, TermName FROM TblPOMain A WITH(READPAST) LEFT JOIN TblTermsMF B WITH(READPAST) ON A.TermID = B.TermID WHERE A.SupplierID = @slid", conn);
                cmd.Parameters.AddWithValue("@slid", slid);
                rd = cmd.ExecuteReader();
                Termsdictionary.Add("", "");
                while (rd.Read())
                {
                    TermsModel terms = new TermsModel();
                    terms.TermID = rd.GetString(0).Trim();
                    terms.TermName = rd.GetString(1).Trim();
                    Termsdictionary.Add(terms.TermID, terms.TermName);
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
            return Termsdictionary;
        }


        public SortedDictionary<string, string> getTermsIfSelectedRR(string rrno,bool isRushOrder)
        {
            SortedDictionary<string, string> Termsdictionary = new SortedDictionary<string, string>();
            try
            {
                conn.Open();
                if (isRushOrder)
                {
                    cmd = Connection.setCommand($"SELECT A.TermID,A.TermName FROM TblTermsMF A WITH(READPAST)", conn);
                }
                else
                {
                    cmd = Connection.setCommand($"SELECT A.TermID,B.TermName FROM TblReceivingMain A WITH(READPAST) LEFT JOIN TblTermsMF B WITH(READPAST) ON A.TermID = B.TermID " +
                                                $"WHERE A.RRNo = @rrno", conn);
                    cmd.Parameters.AddWithValue("@rrno", rrno);
                }
                rd = cmd.ExecuteReader();
                Termsdictionary.Add("", "");
                while (rd.Read())
                {
                    TermsModel terms = new TermsModel();
                    terms.TermID = rd.GetString(0).Trim();
                    terms.TermName = rd.GetString(1).Trim();
                    Termsdictionary.Add(terms.TermID, terms.TermName);
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
            return Termsdictionary;
        }

        public string getTermID(string slid)
        {
            string termID = "";
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT TermID FROM TblSubsidiaryMain WHERE SLID =@slid", conn);
                cmd.Parameters.AddWithValue("@slid", slid);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    termID = rd.GetString(0).TrimEnd();
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
            return termID;
        }
    
        public string getTemporaryEmployee()
        {
            string employeeId = "";
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    "SELECT EmployeeID FROM TblEmployeeMF WITH(READPAST) WHERE EmployeeID = 'EE24040001'",
                    conn);
                employeeId = cmd.ExecuteScalar().ToString();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { conn.Close(); }
            return employeeId;
        }
        
        public string getTemporaryReason()
        {
            string reason = "";
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    "SELECT TOP 1 ReasonID FROM TblReasonMF WITH(READPAST)",
                    conn);
                reason = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { conn.Close(); }
            return reason;
        }
    
        public SortedDictionary<string,string> CreateAll(ReceivingMF recmodel, string rrno,bool isrushorder)
        {
            string msg = "Information saved successfully";
            string rrNO = "";
            SortedDictionary<string,string> dictionary = new SortedDictionary<string, string>();
            bool fromedit = false;
            try
            {
                conn.Open();
                tr = conn.BeginTransaction();
                cmd.Transaction = tr;
                if (String.IsNullOrEmpty(rrno))
                {
                    cmd = Connection.setTransactionCommand(
                       "  Declare @prefix varchar(10) = 'RR'+RIGHT (YEAR(GETDATE()),2)+SUBSTRING(CONVERT(varchar,GETDATE(),23),6,2) " +
                       "  Declare @code varchar(10) = (SELECT TOP 1 RRNo FROM TblCtrlNo WHERE CAST(SUBSTRING(RRNo,1,6) AS varchar) = @prefix ORDER BY RRNo DESC) " +
                       "  IF @code IS NULL " +
                       "  BEGIN  " +
                       "  SET @code = @prefix+'0001' " +
                       "  IF (SELECT COUNT(*) FROM TblCtrlNo WITH(READPAST))> 0 " +
                       "  BEGIN UPDATE TblCtrlNo SET RRNo = @prefix+'0002' " +
                       "  END " +
                       "  ELSE " +
                       "  BEGIN" +
                       "  INSERT INTO TblCtrlNo(RRNo) " +
                       "  VALUES(@prefix+'0002') " +
                       "  END " +
                       "  END" +
                       "  ELSE " +
                       "  BEGIN " +
                       "  DECLARE @newcode varchar(10) = (SELECT @prefix+REPLICATE('0',4-LEN(SUBSTRING(@code,7,4)+1)) + " +
                       "  CAST(SUBSTRING(@code,7,4)+1 AS varchar)) " +
                       "  UPDATE TblCtrlNo SET RRNo = @newcode" +
                       "  END " +
                       "  SELECT @code as Code",
                       conn, tr);
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        rrNO = rd.GetString(0);
                    }
                    rd.Close();
                }
                else
                {
                    fromedit = true;
                    rrNO = rrno;
                }



                if (recmodel.receivingModels != null)
                {
                    int ikl = 0;
                    foreach (var item in recmodel.receivingModels)
                    {
                        if (ikl == 0)
                        {
                            if (fromedit)
                            {
                                cmd = Connection.setTransactionCommand(
                                                            "DELETE FROM TblReceivingDet WHERE RRNo = @rrno; " +
                                                            "DELETE FROM TblReceivingDetLoc WHERE RRNo = @rrno; " +
                                                            "DELETE FROM TblReceivingDetPO WHERE RRNo = @rrno",
                                                            conn, tr);
                                cmd.Parameters.AddWithValue("@rrno", rrNO);
                                int deleteall = cmd.ExecuteNonQuery();
                                cmd = Connection.setTransactionCommand(
                                                            "UPDATE TblReceivingMain " +
                                                            "SET InvoiceDt = @invoicedt," +
                                                            "InvoiceNo = @invoiceno," +
                                                            "ReceivedBy = @receivedby," +
                                                            "Remarks = @remarks," +
                                                            "ReasonID = null," +
                                                            "ModifiedBy = @modifiedby," +
                                                            "ModifiedDt = GETDATE()" +
                                                            "WHERE RRNo = @rrno",
                                                            conn, tr);
                                cmd.Parameters.AddWithValue("@rrno", rrNO);
                                cmd.Parameters.AddWithValue("@invoiceno", item.InvoiceNo);
                                cmd.Parameters.AddWithValue("@invoicedt", item.InvoiceDt);
                                cmd.Parameters.AddWithValue("@receivedby", item.ReceivedBy);
                                cmd.Parameters.AddWithValue("@remarks", item.Remarks);
                                cmd.Parameters.AddWithValue("@modifiedby", Name01);
                                int i = cmd.ExecuteNonQuery();
                                if (i != 1)
                                {
                                    msg = "Failed to save receiving main details.";
                                }
                            } else
                            {
                                cmd = Connection.setTransactionCommand(
                                                            "INSERT INTO TblReceivingMain(RRNo,PONo,SupplierID,TermID,InvoiceNo,InvoiceDt,ReceivedBy,Remarks,Status,ReasonID,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt) " +
                                                            "VALUES (@rrno,null,@supplierid,@termid,@invoiceno,@invoicedt,@receivedby,@remarks,@status,@reason,@createdby,GETDATE(),@modifiedby,GETDATE())",
                                                            conn, tr);
                                cmd.Parameters.AddWithValue("@rrno", rrNO);
                                cmd.Parameters.AddWithValue("@supplierid", item.SupplierID);
                                cmd.Parameters.AddWithValue("@termid", item.TermId);
                                cmd.Parameters.AddWithValue("@invoiceno", item.InvoiceNo);
                                cmd.Parameters.AddWithValue("@invoicedt", item.InvoiceDt);
                                cmd.Parameters.AddWithValue("@receivedby", item.ReceivedBy);
                                cmd.Parameters.AddWithValue("@remarks", item.Remarks);
                                cmd.Parameters.AddWithValue("@status", item.Status);
                                cmd.Parameters.AddWithValue("@reason", item.ReasonId);
                                cmd.Parameters.AddWithValue("@createdby", Name01);
                                cmd.Parameters.AddWithValue("@modifiedby", Name01);
                                int i = cmd.ExecuteNonQuery();
                                if (i != 1)
                                {
                                    msg = "Failed to save receiving main details.";
                                }
                            }
                        }


                       if(item.receivingDets != null)
                        {
                            int j = item.receivingDets.Count();
                            foreach (var itm in item.receivingDets)
                            {
                                int k = 0;
                                if (ikl == 0)
                                {
                                        cmd = Connection.setTransactionCommand(" INSERT INTO TblReceivingDet(RRNo,PartNo,Qty,UnitPrice,VATAmt,Status,FreeItem,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt)" +
                                                                        " VALUES(@rrno,@partno,@qty,@unitprice,@vatamt,@status,@freeitm,@createdby,GETDATE(),@modifiedby,GETDATE())",
                                        conn, tr);
                                        cmd.Parameters.AddWithValue("@rrno", rrNO);
                                        cmd.Parameters.AddWithValue("@partno", itm.PartNo);
                                        cmd.Parameters.AddWithValue("@qty", itm.Qty);
                                        cmd.Parameters.AddWithValue("@unitprice", itm.UnitPrice);
                                        cmd.Parameters.AddWithValue("@vatamt", itm.VATAmt);
                                        cmd.Parameters.AddWithValue("@status", itm.Status);
                                        cmd.Parameters.AddWithValue("@freeitm", itm.Freeitem);
                                        cmd.Parameters.AddWithValue("@createdby", Name01);
                                        cmd.Parameters.AddWithValue("@modifiedby", Name01);
                                        k = cmd.ExecuteNonQuery();
                                } else if (isrushorder)
                                {
                                    cmd = Connection.setTransactionCommand(" INSERT INTO TblReceivingDet(RRNo,PartNo,Qty,UnitPrice,VATAmt,Status,FreeItem,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt)" +
                                                                        " VALUES(@rrno,@partno,@qty,@unitprice,@vatamt,@status,@freeitm,@createdby,GETDATE(),@modifiedby,GETDATE())",
                                        conn, tr);
                                    cmd.Parameters.AddWithValue("@rrno", rrNO);
                                    cmd.Parameters.AddWithValue("@partno", itm.PartNo);
                                    cmd.Parameters.AddWithValue("@qty", itm.Qty);
                                    cmd.Parameters.AddWithValue("@unitprice", itm.UnitPrice);
                                    cmd.Parameters.AddWithValue("@vatamt", itm.VATAmt);
                                    cmd.Parameters.AddWithValue("@status", itm.Status);
                                    cmd.Parameters.AddWithValue("@freeitm", itm.Freeitem);
                                    cmd.Parameters.AddWithValue("@createdby", Name01);
                                    cmd.Parameters.AddWithValue("@modifiedby", Name01);
                                    k = cmd.ExecuteNonQuery();
                                }


                                if (!isrushorder)
                                {
                                    if (itm.Qty == 0) //ipasok pa rin sa receivingdetpo
                                    {
                                        cmd = Connection.setTransactionCommand(
                                                " DECLARE @countitem int = ISNULL((SELECT COUNT(*) FROM TblPODet WHERE PONo = @pono AND PartNo = @partno),0)" +
                                                " IF @countitem > 0 BEGIN " +
                                                " INSERT INTO TblReceivingDetPO(RRNo,PartNo,Qty,PONo,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt) VALUES (@rrno,@partno,@qty,@pono,@createdby,GETDATE(),@createdby,GETDATE())  END",
                                                conn, tr);
                                        cmd.Parameters.AddWithValue("@rrno", rrNO);
                                        cmd.Parameters.AddWithValue("@partno", itm.PartNo);
                                        cmd.Parameters.AddWithValue("@qty", itm.Qty);
                                        cmd.Parameters.AddWithValue("@pono", item.PONo);
                                        cmd.Parameters.AddWithValue("@createdby", Name01);
                                        cmd.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        //get ko yung partno per po tapos cocompare ko if magagamit lahat ng quantity
                                        cmd = Connection.setTransactionCommand(
                                        "SELECT PONo,PartNo,Qty,ISNULL((SELECT ISNULL(SUM(Qty),0) FROM TblReceivingDetPO WHERE PONo != @pono AND RRNo = @rrno AND PartNo = @partno),0) as Qty1 FROM TblPODet WHERE PONo = @pono AND PartNo = @partno",
                                        conn, tr);
                                        cmd.Parameters.AddWithValue("@pono", item.PONo);
                                        cmd.Parameters.AddWithValue("@partno", itm.PartNo);
                                        cmd.Parameters.AddWithValue("@rrno", rrNO);
                                        rd = cmd.ExecuteReader();

                                        List<POItemsForReceiving> forcheckingpart = new List<POItemsForReceiving>();
                                        while (rd.Read())
                                        {
                                            POItemsForReceiving poitems = new POItemsForReceiving
                                            {
                                                PartNo = rd.GetString(1),
                                                Qty = rd.GetDecimal(2),
                                                PONo = rd.GetString(0),
                                                QtyAlreadyInserted = rd.GetDecimal(3),
                                            };
                                            forcheckingpart.Add(poitems);
                                        }
                                        rd.Close();

                                        foreach (var partno in forcheckingpart)
                                        {
                                            decimal qtytobeinserted = itm.Qty;

                                            if (itm.Qty == partno.Qty + partno.QtyAlreadyInserted)
                                            {
                                                qtytobeinserted = partno.Qty;
                                            }
                                            else if (itm.Qty < partno.Qty)
                                            {
                                                qtytobeinserted = itm.Qty - partno.QtyAlreadyInserted;
                                            }
                                            else if (partno.Qty + partno.QtyAlreadyInserted > itm.Qty)
                                            {
                                                qtytobeinserted = itm.Qty - partno.QtyAlreadyInserted;
                                            }
                                            else
                                            {
                                                qtytobeinserted = partno.Qty;
                                            }
                                            if (partno.PartNo.Trim() == itm.PartNo.Trim() && partno.PONo.Trim() == item.PONo.Trim() && itm.Qty != partno.QtyAlreadyInserted)
                                            {
                                                int m = 0; //dito need ipasok kahit zero kasi ibig sabihin wala talaga siyang nakuha for that PartNo
                                                cmd = Connection.setTransactionCommand(
                                                    " DECLARE @countitem int = ISNULL((SELECT COUNT(*) FROM TblPODet WHERE PONo = @pono AND PartNo = @partno),0)" +
                                                    " IF @countitem > 0 BEGIN " +
                                                    " INSERT INTO TblReceivingDetPO(RRNo,PartNo,Qty,PONo,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt) VALUES (@rrno,@partno,@qty,@pono,@createdby,GETDATE(),@createdby,GETDATE())  END",
                                                    conn, tr);
                                                cmd.Parameters.AddWithValue("@rrno", rrNO);
                                                cmd.Parameters.AddWithValue("@partno", itm.PartNo);
                                                cmd.Parameters.AddWithValue("@qty", qtytobeinserted);
                                                cmd.Parameters.AddWithValue("@pono", item.PONo);
                                                cmd.Parameters.AddWithValue("@createdby", Name01);
                                                m = cmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        ikl++;
                    }
                }
                if(recmodel.receivingLoc != null)
                {
                    foreach(var item in recmodel.receivingLoc)
                        {
                            string lotNo = !string.IsNullOrEmpty(item.LotNo) ? item.LotNo : rrNO+item.PartNo;
                            int l = 0;
                            cmd = Connection.setTransactionCommand(
                                       " INSERT INTO TblReceivingDetLoc(RRNo,UniqueID,PartNo,Qty,LotNo,WhID,BinID,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt)" +
                                       " VALUES(@rrno,@unique,@partno,@qty,@lotno,@whid,@binid,@createdby,GETDATE(),@modifiedby,GETDATE())",
                                       conn, tr);
                            cmd.Parameters.AddWithValue("@rrno", rrNO);
                            cmd.Parameters.AddWithValue("@partno", item.PartNo);
                            cmd.Parameters.AddWithValue("@qty", item.Qty);
                            cmd.Parameters.AddWithValue("@lotno", lotNo);
                            cmd.Parameters.AddWithValue("@whid", item.WhID);
                            cmd.Parameters.AddWithValue("@binid", item.BinID);
                            cmd.Parameters.AddWithValue("@unique", item.UniqueID);
                            cmd.Parameters.AddWithValue("@createdby", Name01);
                            cmd.Parameters.AddWithValue("@modifiedby", Name01);
                            l = cmd.ExecuteNonQuery();
                            if (l > 0)
                            {
                                Helper.TranLog("Receiving", " Saving Receive" + item.PartNo, conn, cmd, tr);
                            }
                            else
                            {
                                msg = "Something went wrong";
                                tr.Rollback();
                                return dictionary;
                            }
                        }
                    }tr.Commit();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                msg = ex.Message;
            }
            finally 
            { 
                conn.Close(); 
            }
            dictionary.Add(msg, rrNO);
            return dictionary;
        }

        public SortedDictionary<string, string> UpdatingReceive(ReceivingMF recmodel,string rrno)
        {
            string msg = "Information saved successfully";
            string rrNO = "";
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();
            try
            {
                conn.Open();
                tr = conn.BeginTransaction();
                cmd.Transaction = tr;
                if (recmodel.receivingModels != null)
                {
                    int ikl = 0;
                    foreach (var item in recmodel.receivingModels)
                    {
                        if (ikl == 0)
                        {
                            cmd = Connection.setTransactionCommand(
                            "UPDATE TblReceivingMain SET InvoiceNo = @invoiceno,InvoiceDt = @invoicedt, " +
                            " ReceivedBy = @receivedby, Remarks = @remarks, Status =@status, " +
                            " ReasonID = @reason, CreatedBy = @createdby, CreatedDt = GETDATE(), ModifiedBy = @modifiedby, ModifiedDt = GETDATE() " +
                            " WHERE RRNo=@rrno",
                            conn, tr);
                            cmd.Parameters.AddWithValue("@rrno", rrno);
                            cmd.Parameters.AddWithValue("@invoiceno", item.InvoiceNo);
                            cmd.Parameters.AddWithValue("@invoicedt", item.InvoiceDt);
                            cmd.Parameters.AddWithValue("@receivedby", item.ReceivedBy);
                            cmd.Parameters.AddWithValue("@remarks", item.Remarks);
                            cmd.Parameters.AddWithValue("@status", item.Status);
                            cmd.Parameters.AddWithValue("@reason", item.ReasonId);
                            cmd.Parameters.AddWithValue("@createdby", Name01);
                            cmd.Parameters.AddWithValue("@modifiedby", Name01);
                            int i = cmd.ExecuteNonQuery();
                            if (i != 1)
                            {
                                msg = "The information has already been added.";
                            }
                            if (item.receivingDets != null)
                            {
                                int j = item.receivingDets.Count();
                                foreach (var itm in item.receivingDets)
                                {
                                    int k = 0;
                                    cmd = Connection.setTransactionCommand(
                                        "UPDATE TblReceivingDet SET PartNo = @partno, Qty = @qty, UnitPrice = @unitprice, " +
                                        " VATAmt = @vatamt, Status = @status, FreeItem = @freeitm, " +
                                        " CreatedBy = @createdby, CreatedDt = GETDATE(), ModifiedBy = @modifiedby, ModifiedDt = GETDATE() " +
                                        " WHERE RRNo = @rrno AND PartNo = @partno",
                                        conn, tr);
                                    cmd.Parameters.AddWithValue("@rrno", rrno);
                                    cmd.Parameters.AddWithValue("@partno", itm.PartNo);
                                    cmd.Parameters.AddWithValue("@qty", itm.Qty);
                                    cmd.Parameters.AddWithValue("@unitprice", itm.UnitPrice);
                                    cmd.Parameters.AddWithValue("@vatamt", itm.VATAmt);
                                    cmd.Parameters.AddWithValue("@status", itm.Status);
                                    cmd.Parameters.AddWithValue("@freeitm", itm.Freeitem);
                                    cmd.Parameters.AddWithValue("@createdby", Name01);
                                    cmd.Parameters.AddWithValue("@modifiedby", Name01);
                                    k = cmd.ExecuteNonQuery();
                                    int m = 0;
                                    if (k > 0)
                                        cmd = Connection.setTransactionCommand(
                                            " UPDATE TblReceivingDetPO SET PartNo = @partno, Qty = @qty, PONo = @pono, " +
                                            " CreatedBy = @createdby, CreatedDt = GETDATE(), " +
                                            " ModifiedBy = @createdby,ModifiedDt = GETDATE() " +
                                            " WHERE RRNo = @rrno AND PartNo = @partno",
                                            conn, tr);
                                    cmd.Parameters.AddWithValue("@rrno", rrno);
                                    cmd.Parameters.AddWithValue("@partno", itm.PartNo);
                                    cmd.Parameters.AddWithValue("@qty", itm.Qty);
                                    cmd.Parameters.AddWithValue("@pono", item.PONo);
                                    cmd.Parameters.AddWithValue("@createdby", Name01);
                                    m = cmd.ExecuteNonQuery();
                                    if (m > 0)
                                    {
                                        Helper.TranLog("Receiving", " Saving Receive" + itm.PartNo, conn, cmd, tr);
                                    }
                                    else
                                    {
                                        msg = "Something went wrong";
                                        tr.Rollback();
                                        return dictionary;
                                    }
                                }
                            }
                        }
                        ikl++;
                    }
                }
                if (recmodel.receivingLoc != null)
                {
                    foreach (var item in recmodel.receivingLoc)
                    {
                        int l = 0;
                        cmd = Connection.setTransactionCommand(
                                   " UPDATE TblReceivingDetLoc SET UniqueID = @unique, PartNo = @partno, Qty = @qty,LotNo = @lotno, WhID = @whid, " +
                                   " BinID = @binid, CreatedBy = @createdby, CreatedDt = GETDATE(), ModifiedBy = @modifiedby, ModifiedDt = GETDATE() " +
                                   " WHERE RRNo = @rrno",
                                   conn, tr);
                        cmd.Parameters.AddWithValue("@rrno", rrno);
                        cmd.Parameters.AddWithValue("@partno", item.PartNo);
                        cmd.Parameters.AddWithValue("@qty", item.Qty);
                        cmd.Parameters.AddWithValue("@lotno", item.LotNo);
                        cmd.Parameters.AddWithValue("@whid", item.WhID);
                        cmd.Parameters.AddWithValue("@binid", item.BinID);
                        cmd.Parameters.AddWithValue("@unique", item.UniqueID);
                        cmd.Parameters.AddWithValue("@createdby", Name01);
                        cmd.Parameters.AddWithValue("@modifiedby", Name01);
                        l = cmd.ExecuteNonQuery();
                        if (l > 0)
                        {
                            Helper.TranLog("Receiving", " Saving Receive" + item.PartNo, conn, cmd, tr);
                        }
                        else
                        {
                            msg = "Something went wrong";
                            tr.Rollback();
                            return dictionary;
                        }
                    }
            }tr.Commit();
                }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dictionary;
        }

        public string POReceiving(ReceivingMF recmodel,string rrno)
        {
            
            string msg = "Information posted successfully";
            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                tr = conn.BeginTransaction();
                cmd.Transaction = tr;
                cmd = Connection.setTransactionCommand(
                    "UPDATE TblReceivingMain SET Status = '2' WHERE RRNo = @rrno",
                    conn,tr);
                cmd.Parameters.AddWithValue("@rrno", rrno);
                int i = cmd.ExecuteNonQuery();
                if( i > 0)
                {
                    cmd = Connection.setTransactionCommand(
                        "UPDATE TblReceivingDet SET Status = '2' WHERE RRNo = @rrno",
                        conn,tr);
                    cmd.Parameters.AddWithValue("@rrno", rrno);
                    int j = cmd.ExecuteNonQuery();
                    if (j > 0)
                    {
                        cmd = Connection.setTransactionCommand(
                            "SELECT PartNo, LotNo, WhID, BinID,Qty,CONVERT(varchar,CreatedDt,23) as CreatedDt FROM TblReceivingDetLoc WITH(READPAST) WHERE RRNo = @rrno",
                            conn,tr);
                        cmd.Parameters.AddWithValue("@rrno", rrno);
                        rd = cmd.ExecuteReader();

                        List<RecItemLoc> recItemLoc2 = new List<RecItemLoc>();
                        while (rd.Read())
                        {
                            RecItemLoc recItemLoc = new RecItemLoc
                            {
                                PartNo = rd.GetString(0),
                                LotNo = rd.GetString(1),
                                WhID = rd.GetString(2),
                                BinID = rd.GetString(3),
                                Qty = rd.GetDecimal(4),
                                ReceivedDate = rd.GetString(5),
                                PONo = ""
                            };
                            recItemLoc2.Add(recItemLoc);
                        }
                        rd.Close();
                        recmodel.receivingLoc = recItemLoc2;
                        if (recmodel.receivingLoc != null)
                        {
                            foreach (var item in recmodel.receivingLoc)
                            {
                                Helper.TranLog("Receiving", " Post Receive" + item.PartNo, conn, cmd, tr);
                                int k2 = 0;
                                cmd = Connection.setTransactionCommand(
                                " IF EXISTS(SELECT PartNo FROM TblInvLot WITH(READPAST) WHERE PartNo = @partno AND LotNo = @lotno) " +
                                "   BEGIN " +
                                "       UPDATE TblInvLot SET Rcvd = ISNULL(Rcvd,0) + @qtyrcvd " +
                                "       WHERE PartNo = @partno " +
                                "           AND LotNo = @lotno " +
                                "   END  " +
                                " ELSE " +
                                " BEGIN" +
                                " INSERT INTO TblInvLot (PartNo,CtrlNo,RecdDt,LotNo," +
                                "       Rcvd,PReturns,Picked,Issued,SReturns,DefReturns,BegBal,TakeUp,StockDrop,TrfIn,TrfOut,UnitCost,AveCost," +
                                "       CreatedBy,CreatedDt,ModifiedBy,ModifiedDt)" +
                                "   VALUES (@partno,@rrno,@receiveddate,@lotno, " +
                                "       @qtyrcvd,0,0,0,0,0,0,0,0,0,0,0,0, " +
                                "       @createdby,GETDATE(),@modifiedby,GETDATE()) " +
                                " END",
                                conn, tr);
                                cmd.Parameters.AddWithValue("@partno", item.PartNo);
                                cmd.Parameters.AddWithValue("@lotno", item.LotNo);
                                cmd.Parameters.AddWithValue("@rrno", rrno);
                                cmd.Parameters.AddWithValue("@receiveddate", item.ReceivedDate);
                                cmd.Parameters.AddWithValue("@qtyrcvd", item.Qty);
                                cmd.Parameters.AddWithValue("@createdby", Name01);
                                cmd.Parameters.AddWithValue("@modifiedby", Name01);
                                k2 = cmd.ExecuteNonQuery();
                                if (k2 > 0)
                                {
                                    int k = 0;
                                    cmd = Connection.setTransactionCommand(
                                    " IF EXISTS(SELECT PartNo FROM TblInvLotLoc WITH(READPAST) WHERE PartNo = @partno AND WhID = @whid " +
                                    " AND BinID = @binid AND LotNo = @lotno) " +
                                    "   BEGIN " +
                                    "       UPDATE TblInvLotLoc SET Rcvd = ISNULL(Rcvd,0) + @qtyrcvd " +
                                    "       WHERE PartNo = @partno " +
                                    "           AND LotNo = @lotno " +
                                    "           AND WhID = @whid " +
                                    "           AND BinID = @binid " +
                                    "   END  " +
                                    " ELSE " +
                                    " BEGIN" +
                                    " INSERT INTO TblInvLotLoc (PartNo,CtrlNo,LotNo,WhID,BinID," +
                                    "       Rcvd,PReturns,Picked,Issued,SReturns,DefReturns,BegBal,TakeUp,StockDrop,TrfIn,TrfOut," +
                                    "       CreatedBy,CreatedDt,ModifiedBy,ModifiedDt)" +
                                    "   VALUES (@partno,@rrno,@lotno,@whid,@binid, " +
                                    "       @qtyrcvd,0,0,0,0,0,0,0,0,0,0, " +
                                    "       @createdby,GETDATE(),@modifiedby,GETDATE()) " +
                                    " END",
                                    conn, tr);
                                    cmd.Parameters.AddWithValue("@partno", item.PartNo);
                                    cmd.Parameters.AddWithValue("@qtyrcvd", item.Qty);
                                    cmd.Parameters.AddWithValue("@whid", item.WhID);
                                    cmd.Parameters.AddWithValue("@binid", item.BinID);
                                    cmd.Parameters.AddWithValue("@lotno", item.LotNo);
                                    cmd.Parameters.AddWithValue("@rrno", rrno);
                                    cmd.Parameters.AddWithValue("@createdby", Name01);
                                    cmd.Parameters.AddWithValue("@modifiedby", Name01);
                                    k = cmd.ExecuteNonQuery();

                                }
                                else
                                {
                                    msg = "Something went wrong";
                                    tr.Rollback();
                                    conn.Open();
                                    return msg;
                                }
                            }
                        }
                    }
                    else
                    {
                        msg = "Something went wrong";
                        conn.Open();
                        return msg;
                    }
                }
                else
                {
                    msg = "Something went wrong";
                    conn.Open();
                    return msg;
                }
                tr.Commit();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Console.WriteLine(ex.Message);
            }
            finally
            { 
                conn.Close(); 
                tr.Dispose();
            }
            return msg;
        }

        public DataTable RRSelection(string txtsearch)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    "SELECT a.RRNo, (SELECT SUM(Qty) FROM TblReceivingDetLoc WHERE RRNo = a.RRNo) as Qty, a.SupplierID, ISNULL(c.SLName,'') as SLName, a.TermID,d.TermName as Term, " +
                    " a.Status, a.CreatedBy, a.CreatedDt FROM TblReceivingMain a WITH(READPAST) " +
                    " LEFT JOIN TblSubsidiaryMain c ON a.SupplierID = c.SLID " +
                    " LEFT JOIN TblTermsMF d ON a.TermID = d.TermID" +
                    " WHERE a.Status = '1' " +
                    " AND (1=(CASE WHEN ISNULL(@keyword,'') = '' THEN 1 ELSE 0 END) " +
                    " OR a.RRNo LIKE '%'+@keyword+'%' " +
                    " OR c.SLName LIKE '%' + @keyword + '%' OR c.SLID LIKE '%'+@keyword+'%')",
                    conn);
                cmd.Parameters.AddWithValue("@keyword", txtsearch);
                rd = cmd.ExecuteReader();
                dt.Load(rd);
            }
            catch(Exception ex)
            {
                Console.WriteLine (ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public string SelectedRR(string rrno)
        {
            string rrnos = "";
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    "SELECT RRNo FROM TblReceivingMain WITH(READPAST) WHERE RRNo = @rrno",
                    conn);
                cmd.Parameters.AddWithValue("@rrno", rrno);
                rrnos = cmd.ExecuteScalar().ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine (ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return rrnos;
        }

        public List<string> SelectedRRForPO(string rrno)
        {
            List<string> pono = new List<string>();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    " SELECT DISTINCT  a.PONo FROM TblPOMain a  WITH(READPAST) " +
                    " LEFT JOIN TblReceivingDetPO b WITH(READPAST) ON b.PONo = a.PONo " +
                    " WHERE b.RRNo = @rrno",
                    conn);
                cmd.Parameters.AddWithValue("@rrno", rrno);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    pono.Add(rd[0].ToString());
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
            return pono;
        }

        public DataTable recpodisp()
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT a.PONo, a.CreatedDt, a.CreatedBy, a.Status FROM TblPOMain a WITH(READPAST) " +
                    " LEFT JOIN TblSubsidiaryMain b WITH(READPAST) ON b.SLID = a.SupplierID " +
                    " WHERE a.Status = '1'", conn);
                rd = cmd.ExecuteReader();
                dt.Load(rd);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close ();
            }
            return dt;
        }
        
        
    }
}
