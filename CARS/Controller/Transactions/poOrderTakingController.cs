using CARS.Functions;
using CARS.Model;
using CARS.Model.Masterfiles;
using CARS.Model.Transactions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Controller.Transactions
{
    internal class poOrderTakingController : Universal<PartsModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection conn = Connection.GetConnection();
        private static SqlCommand cmd = null;
        private static SqlDataReader rd = null;
        private static SqlTransaction tr = null;
        private static MessageBox msgbx = null;
        public override string Create(PartsModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(PartsModel entity)
        {
            throw new NotImplementedException();
        }

        public DataTable fetchData(DataTable excel)
        {
            DataTable merger = new DataTable();
            merger.Columns.Add("PartNo");
            merger.Columns.Add("PartName");
            merger.Columns.Add("BrandName");
            merger.Columns.Add("DescName");
            merger.Columns.Add("Qty");

            List<string> partNumbers = excel.AsEnumerable().Select(row => row["PartNo"].ToString().TrimEnd()).Distinct().ToList();
            if(partNumbers.Count == 0)
            {
                return merger;
            }

            string partNumString = string.Join("','",partNumbers.Select(p => p.Replace("'","''")));
            DataTable fromDb = new DataTable();
            try
            {
                conn.Open();
                string query = $"SELECT a.PartNo, a.PartName, d.BrandName, b.DescName  " +
                    $" FROM TblPartsMainMF a WITH(READPAST) " +
                    $"   LEFT JOIN TblPartsDescriptionMF b WITH(READPAST) ON b.DescID = a.DescID  " +
                    $"   LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = a.BrandID  " +
                    $"   WHERE a.PartNo IN  ('{partNumString}')";
                Debug.WriteLine(query);
                cmd = Connection.setCommand(query,conn);
                rd = cmd.ExecuteReader();
                fromDb.Load(rd);
                foreach(DataRow row in excel.Rows)
                {
                    string partNo = row["PartNo"].ToString().TrimEnd();
                    int partQty = int.Parse(row["Qty"].ToString());

                    DataRow[] dbRow = fromDb.Select($"PartNo = '{partNo.Replace("'", "''")}'");
                    if(dbRow.Length > 0)
                    {
                        DataRow datRow = dbRow[0];

                        DataRow meregerRow = merger.NewRow();
                        meregerRow["PartNo"] = datRow["PartNo"].ToString();
                        meregerRow["PartName"] = datRow["PartName"].ToString();
                        meregerRow["BrandName"] = datRow["BrandName"].ToString();
                        meregerRow["DescName"] = datRow["DescName"].ToString();
                        meregerRow["Qty"] = partQty;

                        if (!IsDuplicate(merger, partNo))
                        {
                            merger.Rows.Add(meregerRow);
                        }
                    }
                }
                return merger;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return merger;
        }

        public bool IsDuplicate(DataTable dt, string partNo)
        {
            foreach(DataRow row in dt.Rows){
                if (row["PartNo"].ToString() == partNo)
                {
                    if (row["PartNo"].ToString() == partNo)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public DataTable getPartLists(PartsModel entity,string keyword)
        {
            DataTable parts = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(" SELECT RTRIM(a.PartNo) AS PartNoParts, RTRIM(a.PartName) AS PartName, RTRIM(b.BrandName) AS BrandName," +
                    " RTRIM(ISNULL(c.DescName,'')) as DescName, RTRIM(ISNULL(a.OtherName,'')) AS OtherName, RTRIM(ISNULL(a.Sku,'')) AS SKU, " +
                    " RTRIM(ISNULL(d.UomName,'')) AS UOM FROM TblPartsMainMF a WITH(READPAST) " +
                    " LEFT JOIN TblPartsBrandMF b WITH(READPAST) ON b.BrandID = a.BrandID " +
                    " LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = a.DescID " +
                    " LEFT JOIN TblPartsUomMF d WITH(READPAST) ON d.UomID = a.UomID" +
                    " WHERE (1=(CASE WHEN ISNULL(@brandID,'') = '' THEN 1 ELSE 0 END) OR b.BrandID =   @brandID ) " +
                    "  AND (1=(CASE WHEN ISNULL(@descID, '') = '' THEN 1 ELSE 0 END) OR c.DescID = @descID) " +
                    " AND (1=(CASE WHEN ISNULL(@keyword, '') = '' THEN 1 ELSE 0 END) OR a.PartName LIKE '%' + @keyword + '%' " +
                    " OR b.BrandName LIKE '%' + @keyword + '%' OR c.DescName LIKE '%' + @keyword + '%') " +
                    " AND a.IsActive = 1 ", conn);
                cmd.Parameters.AddWithValue("@brandID", entity.Brand);
                cmd.Parameters.AddWithValue("@descID", entity.Description);
                cmd.Parameters.AddWithValue("@keyword", keyword);
                rd = cmd.ExecuteReader();
                parts.Load(rd);
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

        public override DataTable dt(PartsModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(PartsModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(PartsModel entity)
        {
            throw new NotImplementedException();
        }

        public PartsModel suggestedParts(string partNo)
        {
            PartsModel partDetails = new PartsModel();
            try
            {
                conn.Open();
                cmd = Connection.setCommand($"SELECT a.PartNo, a.PartName, d.BrandName, b.DescName " +
                                                $"  FROM TblPartsMainMF a WITH(READPAST) " +
                                                $"  LEFT JOIN TblPartsDescriptionMF b WITH(READPAST) ON b.DescID = a.DescID " +
                                                $"  LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = a.BrandID " +
                                                $"  WHERE a.PartNo=@PartNo", conn);
                cmd.Parameters.AddWithValue("@PartNo", partNo);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    partDetails = new PartsModel
                    {
                        PartNo = rd.GetString(0).TrimEnd(),
                        PartName = rd.GetString(1).TrimEnd(),
                        Brand = rd.GetString(2).TrimEnd(),
                        Description = rd.GetString(3).TrimEnd(),
                    };
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
            return partDetails;
        }

        public PartsModel getPartsInfo(string partname)
        {
            PartsModel parts = null;
            try
            {
                parts = new PartsModel();
                conn.Open();
                cmd = Connection.setCommand("SELECT a.PartNo,a.PartName, a.Sku, a.PartImg, a.OtherName, a.PartApply, ISNULL(d.OemNo,'') AS OemNo, c.UomName," +
                    "                               a.BPartNo, a.ListPrice, a.PartImg, a.PPosition, a.PSize, a.PType, a.IUpack, a.MUpack " +
                    "                               FROM TblPartsMainMF a WITH(READPAST) " +
                    "                               LEFT JOIN TblPartsOemParts b WITH(READPAST) ON b.PartNo = a.PartNo " +
                    "                               LEFT JOIN TblPartsOemMain d WITH(READPAST) ON b.ParentID = d.UniqueID " +
                    "                               LEFT JOIN TblPartsUomMF c WITH(READPAST) ON c.UomID = a.UomID " +
                    "                               WHERE a.PartName = @partname", conn);
                cmd.Parameters.AddWithValue("@partname", partname);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    parts.PartNo = rd.GetString(rd.GetOrdinal("PartNo")).TrimEnd();
                    parts.PartName = rd.GetString(rd.GetOrdinal("PartName")).TrimEnd();
                    parts.Sku = rd.GetString(rd.GetOrdinal("Sku")).TrimEnd();
                    parts.OtherName = rd.GetString(rd.GetOrdinal("OtherName")).TrimEnd();
                    parts.PartApplication = rd.GetString(rd.GetOrdinal("PartApply")).TrimEnd();
                    parts.Oem = rd.GetString(rd.GetOrdinal("OemNo")).TrimEnd();
                    parts.Uom = rd.GetString(rd.GetOrdinal("UomName")).TrimEnd();
                    parts.BPartNo = rd.GetString(rd.GetOrdinal("BPartNo")).TrimEnd();
                    parts.Image = rd.GetString(rd.GetOrdinal("PartImg")).TrimEnd();
                    parts.PPosition = rd.GetString(rd.GetOrdinal("PPosition")).TrimEnd();
                    parts.PSize = rd.GetString(rd.GetOrdinal("PSize")).TrimEnd();
                    parts.Ptype = rd.GetString(rd.GetOrdinal("PType")).TrimEnd();
                    parts.ListPrice = rd.GetDecimal(rd.GetOrdinal("ListPrice"));
                    parts.MUpack = rd.GetDecimal(rd.GetOrdinal("MUpack"));
                    parts.IUpack = rd.GetDecimal(rd.GetOrdinal("IUpack"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return parts;
        }

        public SortedDictionary<string, string> getBrands()
        {
            SortedDictionary<string, string> Branddictionary = new SortedDictionary<string, string>();
            try
            {
                conn.Open();

                cmd = Connection.setCommand($"SELECT BrandName,BrandID FROM TblPartsBrandMF WITH (READPAST)", conn);
                rd = cmd.ExecuteReader();
                Branddictionary.Add("", "");

                while (rd.Read())
                {
                    BrandModel brands = new BrandModel();
                    brands.BrandID = rd.GetString(1).Trim();
                    brands.BrandName = rd.GetString(0).Trim();
                    Branddictionary.Add(brands.BrandName, brands.BrandID);
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

        public SortedDictionary<string,string> saveOrder(PoOrderTaking orderLists,string ctrlNoRes)
        {
            string msg = "Order saved successfully";
            string controlNo = "";
            SortedDictionary<string,string> dictionary = new SortedDictionary<string,string>();
            try
            {
                conn.Open();
            
                    tr = conn.BeginTransaction();
                    cmd.Transaction = tr;
                if (String.IsNullOrEmpty(ctrlNoRes))
                {
                    cmd = Connection.setTransactionCommand(
                                              "   Declare @prefix varchar(10) = 'OT'+RIGHT (YEAR(GETDATE()),2)+SUBSTRING(CONVERT(varchar,GETDATE(),23),6,2); " +
                                              "   Declare @code varchar(10) = (SELECT TOP 1 OrdrTakingNo FROM TblCtrlNo WHERE CAST(SUBSTRING(OrdrTakingNo,1,6) AS varchar) = @prefix ORDER BY OrdrTakingNo DESC) " +
                                              "   IF @code IS NULL " +
                                              "   BEGIN " +
                                              "   SET @code = @prefix+'0001' " +
                                              "   IF (SELECT COUNT(*) FROM TblCtrlNo WITH(READPAST))>0 " +
                                              "   BEGIN UPDATE TblCtrlNo SET OrdrTakingNo = @prefix+'0002' " +
                                              "   END " +
                                              "   ELSE " +
                                              "   BEGIN" +
                                              "   INSERT INTO TblCtrlNo(OrdrTakingNo) " +
                                              "   VALUES(@prefix+'0002') " +
                                              "   END " +
                                              "   END" +
                                              "   ELSE " +
                                              "   BEGIN " +
                                              "   DECLARE @newcode varchar(10) = (SELECT @prefix+REPLICATE('0',4-LEN(SUBSTRING(@code,7,4)+1)) + " +
                                              "   CAST(SUBSTRING(@code,7,4)+1 AS varchar)) " +
                                              "   UPDATE TblCtrlNo SET OrdrTakingNo = @newcode" +
                                              "   END " +
                                              "   SELECT @code as Code  ", conn, tr);
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        controlNo = rd.GetString(0).Trim();
                    }
                    rd.Close();
                    cmd = Connection.setTransactionCommand(
                                              "   INSERT INTO TblOrderListMain(CtrlNo, Remarks, Status, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                              "   VALUES (@code, @Remarks, @Status, @CreatedBy, GETDATE(), @CreatedBy, GETDATE())", conn, tr);
                    cmd.Parameters.AddWithValue("@code", controlNo);
                    cmd.Parameters.AddWithValue("@CreatedBy", Name01);
                    cmd.Parameters.AddWithValue("@Remarks", orderLists.Remarks);
                    cmd.Parameters.AddWithValue("@Status", orderLists.Status);
                    int i = cmd.ExecuteNonQuery();
                    if (i != 1)
                    {
                        msg = "The order entered is already present in the datbase";
                    }
                }
                else
                {
                    controlNo = ctrlNoRes;
                    cmd = Connection.setTransactionCommand(
                                              "  UPDATE TblOrderListMain SET ModifiedDt = GETDATE(), ModifiedBy = @CreatedBy, " +
                                              "  Remarks = @Remarks WHERE CtrlNo = @code", conn, tr);
                    cmd.Parameters.AddWithValue("@code", controlNo);
                    cmd.Parameters.AddWithValue("@CreatedBy", Name01);
                    cmd.Parameters.AddWithValue("@Remarks", orderLists.Remarks);
                    cmd.Parameters.AddWithValue("@Status", orderLists.Status);
                    int j = cmd.ExecuteNonQuery();
                    if (j > 0)
                    {
                        cmd = Connection.setTransactionCommand(
                                             "  DELETE TblOrderListDet WHERE CtrlNo = @code", conn, tr);
                        cmd.Parameters.AddWithValue("@code", controlNo);
                        int k = cmd.ExecuteNonQuery();
                    }
                }
                    if(orderLists.Orderlist != null)
                    {
                        int j = orderLists.Orderlist.Count();
                        foreach (var item in orderLists.Orderlist)
                        {
                            int k = 0;
                            cmd = Connection.setTransactionCommand(
                                //$"--IF EXISTS (SELECT CtrlNo FROM TblOrderListDet WHERE CtrlNo = @code) " +
                                $"BEGIN " +
                                //$"          --UPDATE TblOrderListDet SET PartNo = @PartNo " +
                                $"            INSERT INTO TblOrderListDet(CtrlNo,PartNo,Qty,ItmRemarks,Status, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                $"            VALUES(@code,@PartNo,@Qty,@ItmRemarks,@Status, @CreatedBy, GETDATE(), @CreatedBy, GETDATE()) " +
                                $"END", conn, tr);
                            cmd.Parameters.AddWithValue("@code", controlNo);
                            cmd.Parameters.AddWithValue("@PartNo", item.PartNo);
                            cmd.Parameters.AddWithValue("@Qty", item.Qty);
                            cmd.Parameters.AddWithValue("@ItmRemarks", item.ItmRemarks);
                            //cmd.Parameters.AddWithValue("@PONo", item.PONo);
                            cmd.Parameters.AddWithValue("@Status", item.Status);
                            cmd.Parameters.AddWithValue("@CreatedBy", Name01);
                            k = cmd.ExecuteNonQuery();
                            if (k > 0)
                            {
                                Helper.TranLog("Order List", "Added orders :" + item.CtrlNo, conn, cmd, tr);
                            }
                            else
                            {
                                msg = "The order is already been added.";
                                tr.Rollback();
                                conn.Close();
                                return dictionary;
                            }
                        }
                    }
                Helper.TranLog("Order Taking", "Added order :" + orderLists.CtrlNo, conn, cmd, tr);
                tr.Commit();
            }
            catch (Exception ex)
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
            dictionary.Add(msg, controlNo);
            return dictionary;
        }

        public DataTable getSavedOrder()
        {
            DataTable savedOrder = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT CtrlNo, CreatedDt FROM TblOrderListMain WHERE CreatedBy = @createdBy AND Status = 1", conn);
                cmd.Parameters.AddWithValue("@createdBy", Name01);
                rd = cmd.ExecuteReader();
                savedOrder.Load(rd);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return savedOrder;
        }

        public string PostOrderTaking(string ctrlNo, PoOrderTaking order)
        {
            string posted = "Posted Successfully";
            try
            {
                conn.Open();
                tr = conn.BeginTransaction();
                cmd.Transaction = tr;
                cmd = Connection.setTransactionCommand("UPDATE TblOrderListMain SET Status = @status, ModifiedBy = @modifiedby, ModifiedDt = GETDATE() WHERE CtrlNo = @ctrlno", conn, tr);
                cmd.Parameters.AddWithValue("@status",order.Status);
                cmd.Parameters.AddWithValue("@modifiedby", Name01);
                cmd.Parameters.AddWithValue("@ctrlno", ctrlNo);
                int i = cmd.ExecuteNonQuery();
                if(i > 0)
                {
                    if(order.Orderlist != null)
                    {
                        int j = order.Orderlist.Count();
                        foreach(var item in order.Orderlist)
                        {
                            int k = 0;
                            cmd = Connection.setTransactionCommand("UPDATE TblOrderListDet SET" +
                                " Status = @status, ModifiedBy = @modifiedby, ModifiedDt = GETDATE() WHERE CtrlNo = @ctrlno", conn, tr);
                            cmd.Parameters.AddWithValue("@status", item.Status);
                            cmd.Parameters.AddWithValue("@ctrlno", ctrlNo);
                            cmd.Parameters.AddWithValue("@modifiedby", Name01);
                            k = cmd.ExecuteNonQuery();
                            if (k > 0)
                            {
                                Helper.TranLog("Order List", "Added orders :" + item.CtrlNo, conn, cmd, tr);
                            }
                            else
                            {
                                posted = "The order is already been added.";
                                tr.Rollback();
                                conn.Close();
                                return posted;
                            }
                        }
                    }
                }
                Helper.TranLog("Order Taking", "Posted order :" + order.CtrlNo, conn, cmd, tr);
                tr.Commit();
            }
            catch(Exception ex)
            {
                posted = "Something went wrong";
                Console.WriteLine(ex);
                tr.Rollback();
            }
            finally
            {
                conn.Close();
                tr.Dispose();
            }
            return posted;
        }

        public string checkStatusPost(string ctrlno)
        {
            string status = "";
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT Status FROM TblOrderListMain WHERE CtrlNo = @ctrlno", conn);
                cmd.Parameters.AddWithValue("@ctrlno", ctrlno);
                status = cmd.ExecuteScalar().ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return status;
        }

       public DataTable addSavedOrder(string ctrlNo)
        {
            DataTable addOrder = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand(
                    "SELECT CAST(0 AS BIT) AS CheckOrder, a.PartNo, b.PartName, d.BrandName, c.DescName, a.Qty FROM TblOrderListDet a WITH(READPAST) " +
                    " LEFT JOIN TblPartsMainMF b WITH(READPAST) ON b.PartNo = a.PartNo " +
                    " LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = b.DescID " +
                    " LEFT JOIN TblPartsBrandMF d WITH(READPAST) ON d.BrandID = b.BrandID " +
                    " WHERE a.CreatedBy = @createdBy AND CtrlNo = @ctrlno",
                    conn);
                cmd.Parameters.AddWithValue("@createdBy",Name01);
                cmd.Parameters.AddWithValue("@ctrlno", ctrlNo);
                rd = cmd.ExecuteReader();
                addOrder.Load(rd);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return addOrder;
        }

        public string GetImage (string partNo)
        {
            string image = "";
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT PartImg FROM TblPartsMainMF WITH(READPAST) WHERE PartNo = @partno", conn);
                cmd.Parameters.AddWithValue("@partno", partNo);
                image = cmd.ExecuteScalar().ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close ();
            }
            return image;
        }
        
        public DataTable getPartno()
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open ();
                cmd = Connection.setCommand("SELECT RTRIM(PartNo) AS PartNo FROM TblPartsMainMF WITH(READPAST)", conn);
                rd = cmd.ExecuteReader();
                dt.Load(rd);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close () ;
            }
            return dt;
        }
        public DataTable PartsFormat()
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT a.PartNo, a.PartName, b.BrandName, c.DescName FROM TblPartsMainMF a WITH(READPAST) " +
                    " LEFT JOIN TblPartsBrandMF b ON b.BrandID = a.BrandID " +
                    " LEFT JOIN TblPartsDescriptionMF c ON c.DescID = a.DescID", conn);
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
    }
    }
