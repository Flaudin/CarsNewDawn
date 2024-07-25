using CARS.Functions;
using CARS.Model;
using CARS.Model.Masterfiles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Controller.Masterfiles
{
    internal class CustomerController : Universal<CustomerModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection conn = Connection.GetConnection();
        private static SqlCommand cmd = null;
        private static SqlDataReader rd = null;
        private static SqlTransaction tr = null;
        private static MessageBox msgbx = null;

        public override string Create(CustomerModel entity)
        {
            string msg = "Information saved successfully";
            string slid = "";
            try
            {
                conn.Open();
                tr = conn.BeginTransaction();
                cmd.Transaction = tr;
                cmd = Connection.setTransactionCommand(
                    " Declare @prefix varchar(10) = 'CE'+RIGHT (YEAR(GETDATE()),2)+SUBSTRING(CONVERT(varchar,GETDATE(),23),6,2); " +
                    "         Declare @code varchar(10) = (SELECT TOP 1 SLID FROM TblSubsidiaryMain WHERE CAST(SUBSTRING(SLID,1,6) AS varchar) = @prefix ORDER BY SLID DESC) " +
                    "         IF @code IS NULL BEGIN SET @code = @prefix+'0001' END " +
                    "         ELSE " +
                    "         BEGIN SET @code = (SELECT @prefix+REPLICATE('0',4-LEN(SUBSTRING(@code,7,4)+1)) +" +
                    "         CAST(SUBSTRING(@code,7,4)+1 AS varchar)) END " +
                    "         SELECT @code as Code ", conn, tr);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    slid = rd.GetString(0);
                }
                rd.Close();
                cmd = Connection.setTransactionCommand(
                                            $"  INSERT INTO TblSubsidiaryMain(SLID, SLName, RegName, TinNo, VATType, NoStreet, CityID, ProvID, " +
                                            $"      RegionID, Zip, TelNo, CellNo, Website, EmailAdd, SLType, SupplierType, TermID, IsActive, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt,BldgNo,Brgy,BusinessName) " +
                                            $"  VALUES(@SLID, @SLName, @RegName, @TinNo, @VATType, @NoStreet, @CityID, " +
                                            $"      (SELECT ProvID FROM TblProvinceMF WITH(READPAST) WHERE ProvName = @ProvID), " +
                                            $"      (SELECT RegionID FROM TblRegionMF WITH(READPAST) WHERE RegionName = @RegionID), @Zip, @TelNo, @CellNo, @Website, @EmailAdd, @SLType, @SupplierType, @TermId, 1, @CreatedBy ,GETDATE(), @ModifiedBy, " +
                                            $"  GETDATE(),@BldgNo,@Brgy,@BusinessName)"
                                            , conn,tr);
                cmd.Parameters.AddWithValue("@SLID", slid.ToString());
                cmd.Parameters.AddWithValue("@SLName", entity.SLName);
                cmd.Parameters.AddWithValue("@RegName", entity.RegName);
                cmd.Parameters.AddWithValue("@TinNo", entity.TinNo);
                cmd.Parameters.AddWithValue("@VATType", entity.VATType);
                cmd.Parameters.AddWithValue("@NoStreet", entity.NoStreet);
                cmd.Parameters.AddWithValue("@CityID", entity.CityID);
                cmd.Parameters.AddWithValue("@ProvID", entity.ProvID);
                cmd.Parameters.AddWithValue("@RegionID", entity.RegionID);
                cmd.Parameters.AddWithValue("@Zip", entity.Zip);
                cmd.Parameters.AddWithValue("@TelNo", entity.TelNo);
                cmd.Parameters.AddWithValue("@CellNo", entity.CelNo);
                cmd.Parameters.AddWithValue("@Website", entity.Website);
                cmd.Parameters.AddWithValue("@EmailAdd", entity.EmailAdd);
                cmd.Parameters.AddWithValue("@SLType", entity.SLType);
                cmd.Parameters.AddWithValue("@SupplierType", entity.SupplierType);
                cmd.Parameters.AddWithValue("@TermID", entity.TermID);
                cmd.Parameters.AddWithValue("@BldgNo", entity.FlrNo);
                cmd.Parameters.AddWithValue("@Brgy", entity.Brgy);
                cmd.Parameters.AddWithValue("@BusinessName", entity.BusinessName);
                cmd.Parameters.AddWithValue("@CreatedBy", Name01);
                cmd.Parameters.AddWithValue("@ModifiedBy", Name01);
                int i = cmd.ExecuteNonQuery();
                if (i != 1)
                {
                    msg = "The information entered is already present in the database.";
                }
                if (entity.ContactList != null)
                {
                    int j = entity.ContactList.Count();
                    foreach (var data in entity.ContactList)
                    {
                        int k = 0;
                        cmd = Connection.setTransactionCommand(
                            $" IF EXISTS (SELECT UniqueID FROM TblSubsidiaryContacts WHERE UniqueID = @UniqueID) " +
                            $" BEGIN " +
                            $"      UPDATE TblSubsidiaryContacts SET ContactPerson = @ContactPerson," +
                            $"      ContactNo = @ContactNo,EmailAdd = @EmailAdd,Remarks = @Remarks, " +
                            $"      IsActive=@IsActive" +
                            $"      WHERE UniqueID = @UniqueID " +
                            $" END " +
                            $" ELSE " +
                            $" BEGIN " +
                            $"      INSERT INTO TblSubsidiaryContacts(SLID,UniqueID,ContactPerson,ContactNo,EmailAdd,Remarks, IsActive) " +
                            $"      VALUES(@SLID,@UniqueID,@ContactPerson,@ContactNo,@EmailAdd,@Remarks , 1) " +
                            $" END", conn, tr);
                        cmd.Parameters.AddWithValue("@SLID", slid);
                        cmd.Parameters.AddWithValue("@UniqueID", data.UniqueID);
                        cmd.Parameters.AddWithValue("@ContactPerson", data.ContactPerson);
                        cmd.Parameters.AddWithValue("@ContactNo", data.ContactNo);
                        cmd.Parameters.AddWithValue("@EmailAdd", data.EmailAdd);
                        cmd.Parameters.AddWithValue("@Remarks", data.Remarks);
                        cmd.Parameters.AddWithValue("@IsActive", data.IsActive);
                        k = cmd.ExecuteNonQuery();
                        if (k > 0)
                        {
                            Helper.TranLog("customer Contact", "Added new Contact :" + data.ContactPerson, conn, cmd, tr);
                        }
                        else
                        {
                            msg = "The data's entered is already been addedd.";
                            tr.Rollback();
                            conn.Close();
                            return msg;
                        }
                    }
                }
                Helper.TranLog("Customer Library", "Created new customer:" + entity.SLID, conn, cmd, tr);
                tr.Commit();
            }
            catch(Exception e) 
            { 
                msg = e.Message;
                tr.Rollback();
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
                tr.Dispose();
            }
            return msg;
        }

        public override void Delete(CustomerModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(CustomerModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT a.SLID, a.VATType, CASE WHEN a.VATType = 0 THEN 'VAT INCLUSIVE' WHEN a.VATType = 1 " +
                    " THEN 'VAT EXCLUSIVE' WHEN a.VATType = 2 THEN 'Zero Rated' WHEN a.VATType = 3 THEN 'Non VAT' WHEN a.VATType = 4 THEN 'VAT Exempt'" +
                    " END AS VATName, " +
                    " a.RegName, a.SLName, a.TinNo,  b.TermName " +
                    "      FROM TblSubsidiaryMain a WITH(READPAST) LEFT JOIN TblTermsMF b WITH(READPAST) ON b.TermID = a.TermID " +
                    "      WHERE a.SLType = 'C' " +
                    "       AND (1=(CASE WHEN ISNULL(@SLName,'') = '' THEN 1 ELSE 0 END) OR a.SLName LIKE '%' + @SLName + '%') " +
                    "       AND (1=(CASE WHEN ISNULL(@RegName,'') = '' THEN 1 ELSE 0 END) OR a.RegName LIKE '%' + @RegName + '%') " +
                    "       AND (1=(CASE WHEN ISNULL(@TinNo,'') = '' THEN 1 ELSE 0 END) OR a.TinNo LIKE '%' + @TinNo + '%') " +
                    "       AND (1=(CASE WHEN ISNULL(@VATType,0) = 5 THEN 1 ELSE 0 END) OR a.VATType  = @VATType ) " +
                    "       AND (1=(CASE WHEN ISNULL(@TermID,'') = '' THEN 1 ELSE 0 END) OR a.TermID LIKE '%' + @TermID + '%') ", conn);
                cmd.Parameters.AddWithValue("@SLName", entity.SLName);
                cmd.Parameters.AddWithValue("@RegName", entity.RegName);
                cmd.Parameters.AddWithValue("@TinNo", entity.TinNo);
                cmd.Parameters.AddWithValue("@TermID", entity.TermID);
                cmd.Parameters.AddWithValue("@VATType", entity.VATType);
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

       

        //Select b.termname as termname from tblsubsidiarymain a with (readpast) left join tlbtermMF b with (readpast) on b.termid = a.termid 

        public DataTable getCustomerTable(CustomerModel entity,string keyword)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT SLID, VATType, CASE WHEN VATType = 0 THEN 'VAT' WHEN VATType = 1 " +
                    " THEN 'NON-VAT' WHEN VATType = 2 THEN 'Zero Rated' WHEN VATType = 3 THEN 'VAT Exempt'  END AS VATName, RegName, SLName, TinNo, TermID   " +
                    "      FROM TblSubsidiaryMain WITH(READPAST) " +
                    "      WHERE SLType = 'C' " +
                    "       AND (1=(CASE WHEN ISNULL(@SLName,'') = '' THEN 1 ELSE 0 END) OR SLName LIKE '%' + @SLName + '%') " +
                    "       AND (1=(CASE WHEN ISNULL(@RegName,'') = '' THEN 1 ELSE 0 END) OR RegName LIKE '%' + @RegName + '%') " +
                    "       AND (1=(CASE WHEN ISNULL(@TinNo,'') = '' THEN 1 ELSE 0 END) OR TinNo LIKE '%' + @TinNo + '%') " +
                    "       AND (1=(CASE WHEN ISNULL(@VATType,0) = 5 THEN 1 ELSE 0 END) OR VATType  = @VATType ) " +
                    "       AND (1=(CASE WHEN ISNULL(@TermID,'') = '' THEN 1 ELSE 0 END) OR TermID LIKE '%' + @TermID + '%') ", conn);
                cmd.Parameters.AddWithValue("@SLName", entity.SLName);
                cmd.Parameters.AddWithValue("@RegName", entity.RegName);
                cmd.Parameters.AddWithValue("@TinNo", entity.TinNo);
                cmd.Parameters.AddWithValue("@TermID", entity.TermID);
                cmd.Parameters.AddWithValue("@VATType", entity.VATType);
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

        public override void Read(CustomerModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(CustomerModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                tr = conn.BeginTransaction();
                cmd.Transaction = tr;
                cmd = Connection.setTransactionCommand($"UPDATE TblSubsidiaryMain SET SLName=@SLName, RegName=@RegName, TinNo=@TinNo, VATType=@VATType, NoStreet=@NoStreet, " +
                                                $"      CityID=@CityID, ProvID= (SELECT ProvID FROM TblProvinceMF WITH(READPAST) WHERE ProvName = @ProvID), " +
                                                $"      RegionID=(SELECT RegionID FROM TblRegionMF WITH(READPAST) WHERE RegionName = @RegionID), " +
                                                $"      Zip=@Zip, TelNo=@TelNo, CellNo=@CellNo, Website=@Website, " +
                                                $"      EmailAdd=@EmailAdd, SLType=@SLType, SupplierType=@SupplierType, TermID=@TermID, ModifiedBy=@ModifiedBy, " +
                                                $"      BldgNo = @BldgNo, Brgy = @Brgy, BusinessName = @BusinessName, " +
                                                $"      ModifiedDt=GETDATE(), IsActive=@IsActive" +
                                                $"  WHERE SLID=@SLID", conn, tr);
                cmd.Parameters.AddWithValue("@SLID", entity.SLID);
                cmd.Parameters.AddWithValue("@SLName", entity.SLName);
                cmd.Parameters.AddWithValue("@RegName", entity.RegName);
                cmd.Parameters.AddWithValue("@TinNo", entity.TinNo);
                cmd.Parameters.AddWithValue("@VATType", entity.VATType);
                cmd.Parameters.AddWithValue("@NoStreet", entity.NoStreet);
                cmd.Parameters.AddWithValue("@CityID", entity.CityID);
                cmd.Parameters.AddWithValue("@ProvID", entity.ProvID);
                cmd.Parameters.AddWithValue("@RegionID", entity.RegionID);
                cmd.Parameters.AddWithValue("@Zip", entity.Zip);
                cmd.Parameters.AddWithValue("@TelNo", entity.TelNo);
                cmd.Parameters.AddWithValue("@CellNo", entity.CelNo);
                cmd.Parameters.AddWithValue("@Website", entity.Website);
                cmd.Parameters.AddWithValue("@EmailAdd", entity.EmailAdd);
                cmd.Parameters.AddWithValue("@SLType", entity.SLType);
                cmd.Parameters.AddWithValue("@SupplierType", entity.SupplierType);
                cmd.Parameters.AddWithValue("@TermID", entity.TermID);
                cmd.Parameters.AddWithValue("@BldgNo", entity.FlrNo);
                cmd.Parameters.AddWithValue("@Brgy", entity.Brgy);
                cmd.Parameters.AddWithValue("@BusinessName", entity.BusinessName);
                cmd.Parameters.AddWithValue("@ModifiedBy", Name01);
                cmd.Parameters.AddWithValue("@IsActive", 1);
                int i = cmd.ExecuteNonQuery();
                if (i != 1)
                {
                    message = "The information entered is already present in the database.";
                }
                if (entity.ContactList != null)
                {
                    int j = entity.ContactList.Count();
                    foreach (var data in entity.ContactList)
                    {
                        int k = 0;
                        cmd = Connection.setTransactionCommand(
                            $" IF EXISTS(SELECT UniqueID FROM TblSubsidiaryContacts WHERE UniqueID = @UniqueID) " +
                            $" BEGIN" +
                            $" UPDATE TblSubsidiaryContacts SET ContactPerson=@ContactPerson, ContactNo=@ContactNo, EmailAdd=@EmailAdd, Remarks=@Remarks, " +
                            $" IsActive = @IsActive" +
                            $" WHERE UniqueID=@UniqueID  " +
                            $" AND SLID=@SLID " +
                            $" END  " +
                            $" ELSE " +
                            $" BEGIN" +
                            $" INSERT INTO TblSubsidiaryContacts(SLID,UniqueID,ContactPerson,ContactNo,EmailAdd,Remarks,IsActive) " +
                            $" VALUES(@SLID,@UniqueID,@ContactPerson,@ContactNo,@EmailAdd,@Remarks,@IsActive) " +
                            $" END", conn, tr);
                        cmd.Parameters.AddWithValue("@SLID", data.SLID);
                        cmd.Parameters.AddWithValue("@UniqueID", data.UniqueID);
                        cmd.Parameters.AddWithValue("@ContactPerson", data.ContactPerson);
                        cmd.Parameters.AddWithValue("@ContactNo", data.ContactNo);
                        cmd.Parameters.AddWithValue("@EmailAdd", data.EmailAdd);
                        cmd.Parameters.AddWithValue("@Remarks", data.Remarks);
                        cmd.Parameters.AddWithValue("@IsActive", data.IsActive);
                        k = cmd.ExecuteNonQuery();
                        if (k > 0)
                        {
                            Helper.TranLog("Customer Contact", "Updated Contact :" + data.ContactPerson, conn, cmd, tr);
                        }
                        else
                        {
                            message = "The data's entered is already been added.";
                            tr.Rollback();
                            conn.Close();
                            return message;
                        }
                    }
                }
                Helper.TranLog("Customer Library", "Updated customer:" + entity.SLID, conn, cmd, tr);
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
            }
            return message;
        }

        public CustomerModel get(string code)
        {
            CustomerModel customer = null;
            try
            {
                customer = new CustomerModel();
                conn.Open();
                cmd = Connection.setCommand($"  SELECT SLID, SLName, RegName, TinNo, VATType, " +
                                                $"      NoStreet, CityID, ProvID, RegionID, Zip, Brgy, " +
                                                $"      BusinessName, BldgNo, TelNo, " +
                                                $"      CellNo, Website, EmailAdd, SLType, SupplierType, " +
                                                $"      TermID, IsActive" +
                                                $"  FROM TblSubsidiaryMain WITH (READPAST) " +
                                                $"  WHERE SLID = @code ",conn);

                cmd.Parameters.AddWithValue("code", code);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    customer.SLID = rd.GetString(rd.GetOrdinal("SLID"));
                    customer.SLName = rd.GetString(rd.GetOrdinal("SLName")).TrimEnd();
                    customer.RegName = rd.GetString(rd.GetOrdinal("RegName")).TrimEnd();
                    customer.TinNo = rd.GetString(rd.GetOrdinal("TinNo")).TrimEnd();
                    customer.VATType = rd.GetDecimal(rd.GetOrdinal("VATType"));
                    customer.NoStreet = rd.GetString(rd.GetOrdinal("NoStreet"));
                    customer.CityID = rd.GetString(rd.GetOrdinal("CityID")).TrimEnd();
                    customer.ProvID = rd.GetString(rd.GetOrdinal("ProvID")).TrimEnd();
                    customer.RegionID = rd.GetString(rd.GetOrdinal("RegionID")).TrimEnd();
                    customer.Zip = rd.GetString(rd.GetOrdinal("Zip"));
                    customer.Brgy = rd.GetString(rd.GetOrdinal("Brgy"));
                    customer.FlrNo = rd.GetString(rd.GetOrdinal("BldgNo"));
                    customer.BusinessName = rd.GetString(rd.GetOrdinal("BusinessName"));
                    customer.TelNo = rd.GetString(rd.GetOrdinal("TelNo"));
                    customer.CelNo = rd.GetString(rd.GetOrdinal("CellNo"));
                    customer.Website = rd.GetString(rd.GetOrdinal("Website")).TrimEnd();
                    customer.EmailAdd = rd.GetString(rd.GetOrdinal("EmailAdd")).TrimEnd();
                    customer.SLType = rd.GetString(rd.GetOrdinal("SLType"));
                    customer.SupplierType = rd.GetString(rd.GetOrdinal("SupplierType"));
                    customer.TermID = rd.GetString(rd.GetOrdinal("TermID")).TrimEnd();
                    customer.IsActive = rd.GetBoolean(rd.GetOrdinal("IsActive"));
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }
            return customer;
        }

        public SortedDictionary<string, string> getTerms()
        {
            SortedDictionary<string, string> Termsdictionary = new SortedDictionary<string, string>();
            try
            {
                conn.Open();

                cmd = Connection.setCommand($"SELECT TermID, TermName FROM TblTermsMF WITH (READPAST)", conn);
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
        public SortedDictionary<string, string> GetProvince()
        {
            SortedDictionary<string, string> provincedictionary = new SortedDictionary<string, string>();
            try
            {
                conn.Open();
                cmd = Connection.setCommand($"SELECT ProvName,ProvID FROM TblProvinceMF WITH (READPAST)", conn);
                //cmd.Parameters.AddWithValue("@regionID", regionID);
                rd = cmd.ExecuteReader();
                provincedictionary.Add("", "");
                while (rd.Read())
                {
                    ProvinceModel province = new ProvinceModel();
                    province.ProvID = rd.GetString(1).TrimEnd();
                    province.ProvName = rd.GetString(0).TrimEnd();

                    provincedictionary.Add(province.ProvName, province.ProvID);
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
            return provincedictionary;
        }

        public SortedDictionary<string, string> GetRegion()
        {
            SortedDictionary<string, string> regiondictionary = new SortedDictionary<string, string>();
            try
            {
                conn.Open();
                cmd = Connection.setCommand($"SELECT RegionName, RegionID FROM TblRegionMF WITH (READPAST)", conn);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    RegionModel region = new RegionModel();
                    region.RegionID = rd.GetString(1).TrimEnd();
                    region.RegionName = rd.GetString(0).TrimEnd();

                    regiondictionary.Add(region.RegionID, region.RegionName);
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
            return regiondictionary;
        }
        public SortedDictionary<string, string> GetCity()
        {
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();
            try
            {
                conn.Open();
                cmd = Connection.setCommand($"SELECT CityName, CityID FROM TblCityMF WITH (READPAST)", conn);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    CityModel city = new CityModel();
                    city.CityID = rd.GetString(1).TrimEnd();
                    city.CityName = rd.GetString(0).TrimEnd();

                    dictionary.Add(city.CityName, city.CityID);
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

        public string getSelectedCustomerType(string slid)
        {
            string i = "0";
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT SLType FROM TblSubsidiaryMain WHERE SLID = @SLID", conn);
                cmd.Parameters.AddWithValue("@SLID", slid);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    i = rd.GetString(1);
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
            return i;
        }


        public List<CustomerContactModel> getContacts(string slid)
        {
            List<CustomerContactModel> contacts = new List<CustomerContactModel>();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT UniqueID,ContactPerson,ContactNo,EmailAdd,Remarks,IsActive FROM TblSubsidiaryContacts " +
                    " WHERE SLID = @SLID", conn);
                cmd.Parameters.AddWithValue("@SLID", slid);
                rd = cmd.ExecuteReader() ;
                while (rd.Read())
                {
                   CustomerContactModel contactModel = new CustomerContactModel();
                    contactModel.UniqueID = rd.GetString(0).TrimEnd();
                    contactModel.ContactPerson = rd.GetString(1).TrimEnd();
                    contactModel.ContactNo = rd.GetString(2).TrimEnd();
                    contactModel.EmailAdd = rd.GetString(3).TrimEnd();
                    contactModel.Remarks = rd.GetString(4).TrimEnd();
                    contactModel.IsActive = rd.GetBoolean(5);
                    contacts.Add(contactModel);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine (e.Message);
            }
            finally
            {
                conn.Close();
            }
            return contacts;
        }

        public DataTable dt(CustomerContactModel customerContactModel,string slid)
        {
            DataTable contacts = new DataTable();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT UniqueID,ContactPerson,ContactNo,EmailAdd,Remarks FROM TblSubsidiaryContacts " +
                    " WHERE SLID = @SLID", conn);
                cmd.Parameters.AddWithValue("@SLID", slid);
                rd = cmd.ExecuteReader();
                contacts.Load(rd);
                //while (rd.Read())
                //{
                    //CustomerContactModel contactModel = new CustomerContactModel();
                    //contactModel.UniqueID = rd.GetString(0).TrimEnd();
                    //contactModel.ContactPerson = rd.GetString(1).TrimEnd();
                    //contactModel.ContactNo = rd.GetString(2).TrimEnd();
                    //contactModel.EmailAdd = rd.GetString(3).TrimEnd();
                    //contactModel.Remarks = rd.GetString(4).TrimEnd();
                    //contacts.Add(contactModel);
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return contacts;
        }

        public SortedDictionary<string, string> getSelectedProvince(string selectedReg)
        {
            SortedDictionary<string, string> selectedProvince = new SortedDictionary<string, string>();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT ProvName, ProvID FROM TblProvinceMF WITH (READPAST) WHERE RegionID = @regid AND ISNULL(RegionID,'') <> '' AND IsActive = 1 ORDER BY ProvName ASC", conn);
                cmd.Parameters.AddWithValue("regid", selectedReg);
                rd = cmd.ExecuteReader();
                selectedProvince.Add("", "");
                while (rd.Read())
                {
                    ProvinceModel selectedProvinceModel = new ProvinceModel();
                    selectedProvinceModel.ProvID = rd.GetString(0).Trim();
                    selectedProvinceModel.ProvName = rd.GetString(1).Trim();

                    selectedProvince.Add(selectedProvinceModel.ProvID, selectedProvinceModel.ProvName);
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
            return selectedProvince;
        }

        public Dictionary<string,string> getSelectedProvice(string selectedCity)
        {
            Dictionary<string,string> dictionary = new Dictionary<string, string>();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT a.ProvName, c.RegionName FROM TblProvinceMF a WITH(READPAST) " +
                    " LEFT JOIN TblCityMF b WITH(READPAST) ON b.ProvID = a.ProvID " +
                    " LEFT JOIN TblRegionMF c WITH(READPAST) ON c.RegionID = a.RegionID" +
                    " WHERE b.CityName = @cityname", conn);
                cmd.Parameters.AddWithValue("@cityname", selectedCity);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ProvinceModel model = new ProvinceModel();
                    model.selectedCity = rd.GetString(0).Trim();
                    model.selectedRegion = rd.GetString(1).Trim();

                    dictionary.Add(model.selectedCity, model.selectedRegion);
                }
            }
            catch(Exception ex) {  Console.WriteLine(ex.Message); }
            finally { conn.Close(); }
            return dictionary;
        }
        public SortedDictionary<string, string> getSelectedCity(string provid)
        {
            SortedDictionary<string, string> selectedCity = new SortedDictionary<string, string>();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT  CityName,CityID FROM TblCityMF WITH (READPAST) WHERE ProvID = @provid AND ISNULL(ProvID,'') <> '' AND IsActive = 1 ORDER BY CityName ASC", conn);
                cmd.Parameters.AddWithValue("provid", provid);
                rd = cmd.ExecuteReader();
                //selectedCity.Add("", "");
                while (rd.Read())
                {
                    CityModel selectedCityModel = new CityModel();
                    selectedCityModel.CityID = rd.GetString(1).Trim();
                    selectedCityModel.CityName = rd.GetString(0).Trim();

                    selectedCity.Add(selectedCityModel.CityID, selectedCityModel.CityName);
                }
                selectedCity.OrderBy(e => e.Value).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return selectedCity;
        }
    }
}
