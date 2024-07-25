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
using System.Windows.Forms;

namespace CARS.Controller.Transactions
{
    internal class PoOrderConsolidationController : Universal<PoOrderConsolidationModel>
    {
        private static SqlConnection conn = Connection.GetConnection();
        private static SqlCommand cmd = null;
        private static SqlDataReader rd = null;
        private static SqlTransaction tr = null;
        private static MessageBox msgbx = null;

        public override string Create(PoOrderConsolidationModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(PoOrderConsolidationModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(PoOrderConsolidationModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(PoOrderConsolidationModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(PoOrderConsolidationModel entity)
        {
            throw new NotImplementedException();
        }

        public DataTable getOrders(OrderList entity,string keyword)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT Cast(0 as bit) as CheckOrder,a.CtrlNo, a.CreatedBy, a.ModifiedDt, SUM(b.Qty) AS TotalQty, Count(b.PartNo) AS TotalItem " +
                    "    FROM TblOrderListMain a WITH(READPAST) " +
                    "    LEFT JOIN TblOrderListDet b ON b.CtrlNo = a.CtrlNo " +
                    "    WHERE a.Status = 2 " +
                    "    GROUP BY a.CtrlNo, a.CreatedBy, a.ModifiedDt", conn);
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

        public DataTable orderBreakdown(string ctrlno)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT a.PartNo, a.Qty,  c.DescName, d.BrandName, a.CtrlNo FROM TblOrderListDet a WITH(READPAST) " +
                    "                        LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                    "                        LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                    "                        LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID WHERE CtrlNo =@ctrlno", conn);
                cmd.Parameters.AddWithValue("@ctrlno", ctrlno);
                rd = cmd.ExecuteReader();
                dt.Load(rd);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable orderConsolidation(string partno,string ctrlno)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT a.CreatedBy, a.CreatedDt, a.CtrlNo, a.PartNo, a.Qty, c.DescName, d.BrandName FROM TblOrderListDet a WITH(READPAST) " +
                    "                        LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                    "                        LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                    "                        LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID WHERE a.PartNo = @partno AND a.CtrlNo = @ctrlno", conn);
                cmd.Parameters.AddWithValue("@partno", partno);
                cmd.Parameters.AddWithValue("@ctrlno", ctrlno);
                rd = cmd.ExecuteReader();
                dt.Load(rd);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
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
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
            return suppDictionary;
        }

        public DataTable getPartLists(PartsModel entity, string keyword,bool isbsbpartno,bool iscritical)
        {
            DataTable parts = new DataTable();
            try
            {
                conn.Open();
                if (isbsbpartno)
                {
                    cmd = Connection.setCommand(" SELECT RTRIM(a.PartNo) AS PartNoParts, RTRIM(a.PartName) AS PartName, RTRIM(b.BrandName) AS BrandName," +
                        " RTRIM(ISNULL(c.DescName,'')) as DescName,(SELECT SUM(BegBal) FROM TblInvLot WHERE PartNo = a.PartNo) AS BegBal FROM TblPartsMainMF a WITH(READPAST) " +
                        " LEFT JOIN TblPartsBrandMF b WITH(READPAST) ON b.BrandID = a.BrandID  LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = a.DescID " +
                        " WHERE (1=(CASE WHEN ISNULL(@brandID,'') = '' THEN 1 ELSE 0 END) OR b.BrandID =   @brandID ) " +
                        "  AND (1=(CASE WHEN ISNULL(@descID, '') = '' THEN 1 ELSE 0 END) OR c.DescID = @descID) " +
                        " AND (1=(CASE WHEN ISNULL(@keyword, '') = '' THEN 1 ELSE 0 END) OR a.PartName LIKE '%' + @keyword + '%' " +
                        " OR b.BrandName LIKE '%' + @keyword + '%' OR c.DescName LIKE '%' + @keyword + '%') " +
                        " AND a.IsActive = 1  AND ISNULL(BPartNo,'') <> '' ", conn);
                    cmd.Parameters.AddWithValue("@brandID", entity.Brand);
                    cmd.Parameters.AddWithValue("@descID", entity.Description);
                    cmd.Parameters.AddWithValue("@keyword", keyword);
                    rd = cmd.ExecuteReader();
                    parts.Load(rd);
                }else if (iscritical)
                {
                    cmd = Connection.setCommand(" SELECT RTRIM(a.PartNo) AS PartNoParts, RTRIM(a.PartName) AS PartName, RTRIM(b.BrandName) AS BrandName," +
                    " RTRIM(ISNULL(c.DescName,'')) as DescName,(SELECT SUM(BegBal) FROM TblInvLot WHERE PartNo = a.PartNo) AS BegBal FROM TblPartsMainMF a WITH(READPAST) " +
                    " LEFT JOIN TblPartsBrandMF b WITH(READPAST) ON b.BrandID = a.BrandID  " +
                    " LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = a.DescID " +
                    " LEFT JOIN TblInvLot d WITH(READPAST) ON d.PartNo = a.PartNo" +
                    " WHERE (1=(CASE WHEN ISNULL(@brandID,'') = '' THEN 1 ELSE 0 END) OR b.BrandID =   @brandID ) " +
                    "  AND (1=(CASE WHEN ISNULL(@descID, '') = '' THEN 1 ELSE 0 END) OR c.DescID = @descID) " +
                    " AND (1=(CASE WHEN ISNULL(@keyword, '') = '' THEN 1 ELSE 0 END) OR a.PartName LIKE '%' + @keyword + '%' " +
                    " OR b.BrandName LIKE '%' + @keyword + '%' OR c.DescName LIKE '%' + @keyword + '%') " +
                    " AND a.IsActive = 1 AND d.Rcvd +  d.SReturns +  d.BegBal +  d.TakeUp +  d.TrfIn -  " +
                    " d.PReturns -  d.Picked -  d.DefReturns -  d.StockDrop -  d.TrfOut  < a.ReorderPoint ", conn);
                    cmd.Parameters.AddWithValue("@brandID", entity.Brand);
                    cmd.Parameters.AddWithValue("@descID", entity.Description);
                    cmd.Parameters.AddWithValue("@keyword", keyword);
                    rd = cmd.ExecuteReader();
                    parts.Load(rd);
                }
                else
                {
                    cmd = Connection.setCommand(" SELECT RTRIM(a.PartNo) AS PartNoParts, RTRIM(a.PartName) AS PartName, RTRIM(b.BrandName) AS BrandName," +
                    " RTRIM(ISNULL(c.DescName,'')) as DescName,(SELECT SUM(BegBal) FROM TblInvLot WHERE PartNo = a.PartNo) AS BegBal FROM TblPartsMainMF a WITH(READPAST) " +
                    " LEFT JOIN TblPartsBrandMF b WITH(READPAST) ON b.BrandID = a.BrandID  LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = a.DescID " +
                    " WHERE (1=(CASE WHEN ISNULL(@brandID,'') = '' THEN 1 ELSE 0 END) OR b.BrandID =   @brandID ) " +
                    "  AND (1=(CASE WHEN ISNULL(@descID, '') = '' THEN 1 ELSE 0 END) OR c.DescID = @descID) " +
                    " AND (1=(CASE WHEN ISNULL(@keyword, '') = '' THEN 1 ELSE 0 END) OR a.PartName LIKE '%' + @keyword + '%' " +
                    " OR b.BrandName LIKE '%' + @keyword + '%' OR c.DescName LIKE '%' + @keyword + '%') " +
                    " AND a.IsActive = 1 AND (SELECT SUM(BegBal) FROM TblInvLot WHERE PartNo = a.PartNo) != 0", conn);
                    cmd.Parameters.AddWithValue("@brandID", entity.Brand);
                    cmd.Parameters.AddWithValue("@descID", entity.Description);
                    cmd.Parameters.AddWithValue("@keyword", keyword);
                    rd = cmd.ExecuteReader();
                    parts.Load(rd);
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
            return parts;
        }

        public DataTable inventoryMovement(string partNo)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT RTRIM(a.Rcvd) AS Rcvd, RTRIM(a.BegBal) AS BegBal, RTRIM(a.TakeUp) AS TakeUp, " +
                                            "       RTRIM(a.UnitCost) AS UnitCost, RTRIM(a.AveCost) AS AveCost, " +
                                            "       RTRIM(a.Picked) AS Picked, RTRIM(b.StockDrop) AS StockDrop, " +
                                            "       b.Rcvd + b.SReturns + b.BegBal + b.TakeUp + b.TrfIn - b.PReturns - b.Picked - b.DefReturns - b.StockDrop - b.TrfOut AS BOh, " +
                                            "       RTRIM(b.CreatedDt) AS CreatedDt " +
                                            "   FROM TblInvLot a WITH(READPAST) "+
                                            "   LEFT JOIN TblInvLotLoc b ON b.CtrlNo = a.CtrlNo " +
                                            "   LEFT JOIN TblWarehouseMF c ON c.WhID = b.WhID " +
                                            "   WHERE a.PartNo = @partNo", conn);
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
    
        public decimal getUnitPrice(string partNo)
        {
            decimal unitPrice = 0;
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT UnitPrice FROM TblSuppQuotDet WHERE PartNo = @partno", conn);
                cmd.Parameters.AddWithValue("@partno", partNo);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                unitPrice = rd.GetDecimal(0);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return unitPrice;
        }

        public decimal getQuotation(string partNo, string suppName)
        {
            decimal quotation = 0;
            try
            {
                conn.Open();
                cmd = Connection.setCommand(" SELECT a.UnitPrice FROM TblSuppQuotDet a " +
                                            " LEFT JOIN TblSuppQuotMain b ON b.SuppQuotNo = a.SuppQuotNo " +
                                            " WHERE PartNo = @partno AND b.SupplierID = @suppName ", conn);
                cmd.Parameters.AddWithValue("@partno", partNo);
                cmd.Parameters.AddWithValue("@suppName", suppName);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    quotation = rd.GetDecimal(0);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally 
            { 
                conn.Close(); 
            }
            return quotation;
        }

        public Dictionary <string,string> saveConsolidate(OrderConsolidate order)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();//"Consolidation Saved";
            string msg = "Consolidation saved successfully";
            string orderNo = "";
            try
            {
                conn.Open();
                tr = conn.BeginTransaction();
                cmd.Transaction = tr;
                cmd = Connection.setTransactionCommand(
                                          " Declare @prefix varchar(10) = 'OC'+RIGHT (YEAR(GETDATE()),2)+SUBSTRING(CONVERT(varchar,GETDATE(),23),6,2) " +
                                          " Declare @code varchar(10) = (SELECT TOP 1 OrdrConsoNo FROM TblCtrlNo WHERE CAST(SUBSTRING(OrdrConsoNo,1,6) AS varchar) = @prefix ORDER BY OrdrConsoNo DESC) " +
                                          " IF @code IS NULL " +
                                          " BEGIN " +
                                          " SET @code = @prefix+'0001' " +
                                          " IF(SELECT COUNT(*) FROM TblCtrlNo WITH(READPAST)) > 0 " +
                                          " BEGIN UPDATE TblCtrlNo SET OrdrConsoNo = @prefix+'0002' " +
                                          " END " +
                                          " ELSE " +
                                          " BEGIN " +
                                          " INSERT INTO TblCtrlNo(OrdrConsoNo) VALUES(@prefix+'0002') " +
                                          " END " +
                                          " END " +
                                          " ELSE " +
                                          " BEGIN " +
                                          " DECLARE @newcode varchar(10) = (SELECT @prefix+REPLICATE('0',4-LEN(SUBSTRING(@code,7,4)+1)) +  " +
                                          " CAST(SUBSTRING(@code,7,4)+1 AS varchar)) " +
                                          " UPDATE TblCtrlNo " +
                                          " SET OrdrConsoNo = @newcode" +
                                          " END " +
                                          " SELECT @code as Code  ", conn, tr);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    orderNo = rd.GetString(0).Trim();
                }
                rd.Close();
                cmd = Connection.setTransactionCommand(" INSERT INTO TblOrderMain(OrdrNo,SupplierID,Status,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt) " +
                                                       " VALUES(@code,@SupplierID,@Status, @CreatedBy,GETDATE(), @ModifiedBy, GETDATE()) ", conn, tr);
                cmd.Parameters.AddWithValue("@code", orderNo);
                cmd.Parameters.AddWithValue("@Status", 1);
                cmd.Parameters.AddWithValue("@SupplierID", order.SupplierID);
                cmd.Parameters.AddWithValue("@CreatedBy", Name01);
                cmd.Parameters.AddWithValue("@ModifiedBy", Name01);
                int i = cmd.ExecuteNonQuery();
                if(i != 1)
                {
                    msg = "The order entered is already been ordered";
                }if(order.orderDetails != null)
                {
                    int j = order.orderDetails.Count();
                    foreach(var item in order.orderDetails)
                    {
                        int k = 0;
                        cmd = Connection.setTransactionCommand("" +
                            " INSERT INTO TblOrderDet(OrdrNo,PartNo,Status,OrdrQty,UnitPrice,SupplierID,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt) " +
                            " VALUES(@code,@PartNo,@Status,@OrdrQty,@UnitPrice,@SupplierID,@CreatedBy,GETDATE(),@ModifiedBy,GETDATE())", conn, tr);
                        cmd.Parameters.AddWithValue("@code", orderNo);
                        cmd.Parameters.AddWithValue("@PartNo", item.PartNo);
                        cmd.Parameters.AddWithValue("@Status", 1);
                        cmd.Parameters.AddWithValue("@OrdrQty", item.OrdrQty);
                        cmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        cmd.Parameters.AddWithValue("@SupplierID", item.SupplierID);
                        cmd.Parameters.AddWithValue("@CreatedBy", Name01);
                        cmd.Parameters.AddWithValue("@ModifiedBy", Name01);
                        k = cmd.ExecuteNonQuery();
                        if (k > 0)
                        {
                            Helper.TranLog("Order Consolidate", "Added orders :" + item.OrderNo, conn, cmd, tr);
                        }
                        else
                        {
                            msg = "The order is already been added";
                            tr.Rollback();
                            conn.Close();
                            return dictionary;
                        }
                    }
                }
                Helper.TranLog("Order Consolidation", "Added order" + order.OrderNo, conn, cmd, tr);
                tr.Commit();
            }
            catch(Exception ex)
            {
                msg = "Something went wrong";
                Console.WriteLine(ex);
                tr.Rollback();
            }
            finally 
            { 
                conn.Close();
                tr.Dispose();
            }
            dictionary.Add(msg, orderNo);
            return dictionary;
        }

        public string post(string ctrlNo)
        {
            string msg = "Post Successfully";
            try
            {
            conn.Open();
            cmd = Connection.setCommand(" UPDATE TblOrderMain SET Status = 2, ModifiedDt = GETDATE() WHERE OrdrNo = @OrdrNo", conn);
            cmd.Parameters.AddWithValue("@OrdrNo", ctrlNo);
            int i = cmd.ExecuteNonQuery();
                if(i > 0)
                {
                    cmd = Connection.setCommand(" UPDATE TblOrderDet SET Status = 2, ModifiedDt = GETDATE() WHERE OrdrNo = @OrdrNo", conn);
                    cmd.Parameters.AddWithValue("@OrdrNo", ctrlNo);
                    int h = cmd.ExecuteNonQuery();
                    if (h > 0)
                    {
                        msg = "Post Successfully";
                    }
                }
            }
            catch(Exception ex)
            {
                msg = "Something went wrong";
                Console.WriteLine(ex);
                tr.Rollback();
            }
            finally 
            { 
                conn.Close();
                tr.Dispose();
            }
        return msg;
        }

        public DataTable getSupplierQuotation(string partNo)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT CAST(0 as bit) as isChecked, c.SLName, a.PartNo, a.UnitPrice, b.SupplierID,0 as SupplierQuotation FROM TblSuppQuotDet a WITH(READPAST) " +
                    "                        LEFT JOIN TblSuppQuotMain b ON b.SuppQuotNo = a.SuppQuotNo " +
                    "                        LEFT JOIN TblSubsidiaryMain c ON c.SLID = b.SupplierID " +
                    "                        WHERE a.PartNo = @partno", conn);
                cmd.Parameters.AddWithValue("@partno", partNo);
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

        public string checkSuppQuot(DataTable dt)
        {
            string msg = "";
            string a = "";
            try
            {
                conn.Open();
                foreach(DataRow itm in dt.Rows)
                {
                    cmd = Connection.setCommand("SELECT SuppQuotNo FROM TblSuppQuotDet  " +
                        "                        WHERE PartNo = @partno", conn);
                    cmd.Parameters.AddWithValue("@partno", itm["PartNo"].ToString());
                    object result = cmd.ExecuteScalar();
                    if(result != null && result != DBNull.Value)
                    {
                        a = cmd.ExecuteScalar().ToString();
                    }
                    if (String.IsNullOrEmpty(a))
                    {
                        msg = itm["PartNo"].ToString().Trim() + " has no supplier quotation";
                    }

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
            return msg;
        }

    }
}


