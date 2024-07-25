using CARS.Functions;
using CARS.Model;
using CARS.Model.Masterfiles;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARS.Controller.Masterfiles
{
    internal class SupplierController : Universal<SupplierModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;
        private static SqlTransaction transaction = null;


        public override string Create(SupplierModel entity)
        {
            string message = "Information saved successfully";
            string slid = "";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand(" Declare @prefix varchar(10) = 'SE'+RIGHT (YEAR(GETDATE()),2)+SUBSTRING(CONVERT(varchar,GETDATE(),23),6,2); " +
                    "                                        Declare @code varchar(10) = (SELECT TOP 1 SLID FROM TblSubsidiaryMain WHERE CAST(SUBSTRING(SLID,1,6) AS varchar) = @prefix ORDER BY SLID DESC) " +
                    "                                        IF @code IS NULL BEGIN SET @code = @prefix+'0001' END " +
                    "                                        ELSE " +
                    "                                        BEGIN SET @code = (SELECT @prefix+REPLICATE('0',4-LEN(SUBSTRING(@code,7,4)+1)) +" +
                    "                                        CAST(SUBSTRING(@code,7,4)+1 AS varchar)) END " +
                    "                                        SELECT @code as Code ", connection, transaction);
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    slid = reader.GetString(0);
                }
                reader.Close();
                command = Connection.setTransactionCommand(
                                            $"  INSERT INTO TblSubsidiaryMain(SLID, SLName, RegName, TinNo, VATType, NoStreet, CityID, ProvID, " +
                                            $"      RegionID, Zip, TelNo, CellNo, Website, EmailAdd, SLType, SupplierType, TermID, IsActive, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt,BldgNo,Brgy,BusinessName) " +
                                            $"  VALUES(@SLID, @SLName, @RegName, @TinNo, @VATType, @NoStreet, @CityID, " +
                                            $"   (SELECT ProvID FROM TblProvinceMF WITH(READPAST) WHERE ProvName = @ProvID), " +
                                            $"   (SELECT RegionID FROM TblRegionMF WITH(READPAST) WHERE RegionName = @RegionID), " +
                                            $"  @Zip, @TelNo, @CellNo, @Website, @EmailAdd, @SLType, @SupplierType, @TermId, 1, @CreatedBy ,GETDATE(), @ModifiedBy, GETDATE(),@BldgNo,@Brgy,@BusinessName) "
                                            , connection,transaction);
                command.Parameters.AddWithValue("@SLID", slid.ToString());
                command.Parameters.AddWithValue("@SLName", entity.SLName);
                command.Parameters.AddWithValue("@RegName", entity.RegName);
                command.Parameters.AddWithValue("@TinNo", entity.TinNo);
                command.Parameters.AddWithValue("@VATType", entity.VATType);
                command.Parameters.AddWithValue("@NoStreet", entity.NoStreet);
                command.Parameters.AddWithValue("@CityID", entity.CityID);
                command.Parameters.AddWithValue("@ProvID", entity.ProvID);
                command.Parameters.AddWithValue("@RegionID", entity.RegionID);
                command.Parameters.AddWithValue("@Zip", entity.Zip);
                command.Parameters.AddWithValue("@TelNo", entity.TelNo);
                command.Parameters.AddWithValue("@CellNo", entity.CelNo);
                command.Parameters.AddWithValue("@Website", entity.Website);
                command.Parameters.AddWithValue("@EmailAdd", entity.EmailAdd);
                command.Parameters.AddWithValue("@SLType", entity.SLType);
                command.Parameters.AddWithValue("@SupplierType", entity.SupplierType);
                command.Parameters.AddWithValue("@TermID", entity.TermID);
                command.Parameters.AddWithValue("@BldgNo", entity.FlrNo);
                command.Parameters.AddWithValue("@Brgy", entity.Brgy);
                command.Parameters.AddWithValue("@BusinessName", entity.BusinessName);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                command.Parameters.AddWithValue("@ModifiedBy", Name01);
                int i = command.ExecuteNonQuery();
                if (i != 1)
                {
                    message = "The information entered is already present in the database.";
                }
                if(entity.ContactList != null)
                {
                    int j = entity.ContactList.Count();
                    foreach(var data in entity.ContactList)
                    {
                        int k = 0;
                        command = Connection.setTransactionCommand(
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
                            $" END", connection, transaction);
                        command.Parameters.AddWithValue("@SLID", slid);
                        command.Parameters.AddWithValue("@UniqueID", data.UniqueID);
                        command.Parameters.AddWithValue("@ContactPerson", data.ContactPerson);
                        command.Parameters.AddWithValue("@ContactNo", data.ContactNo);
                        command.Parameters.AddWithValue("@EmailAdd", data.EmailAdd);
                        command.Parameters.AddWithValue("@Remarks", data.Remarks);
                        command.Parameters.AddWithValue(@"IsActive", data.IsActive);
                        k = command.ExecuteNonQuery();
                        if (k > 0)
                        {
                            Helper.TranLog("Supplier Contact", "Added new Contact :" + data.ContactPerson, connection, command, transaction);
                        }
                        else
                        {
                            message = "The data's entered is already been addedd.";
                            transaction.Rollback();
                            connection.Close();
                            return message;
                        }
                    }
                }
                Helper.TranLog("Supplier Library", "Created new supplier:" + entity.SLID, connection, command, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                message = "Something went wrong.";
                transaction.Rollback();
                Console.WriteLine(ex.Message);
            }
            finally
            {
                transaction.Dispose();
                connection.Close();
            }
            return message;
        }

        public void Create(SupplierContactModel entity, bool IsContact)
        {
            if (IsContact)
            {
                command = Connection.setCommand($"INSERT INTO TblSubsidiaryContacts(SLID, UniqueID, ContactPerson, ContactNo, EmailAdd, Remarks, " +
                                                $"      CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, IsActive) " +
                                                $"  VALUES(@SLID, @UniqueID, @ContactPerson, @ContactNo, @EmailAdd, @Remarks, " +
                                                $"      true)", connection);
                command.Parameters.AddWithValue("@SLID", entity.SLID);
                command.Parameters.AddWithValue("@UniqueID", entity.UniqueID);
                command.Parameters.AddWithValue("@ContactPerson", entity.ContactPerson);
                command.Parameters.AddWithValue("@ContactNo", entity.ContactNo);
                command.Parameters.AddWithValue("@EmailAdd", entity.EmailAdd);
                command.Parameters.AddWithValue("@Remarks", entity.Remarks);
                //VALUES @CreatedBy, GETDATE, @CreatedBy, GETDATE(),
            }
            throw new NotImplementedException();
        }

        public override void Delete(SupplierModel entity)
        {
            throw new NotImplementedException();
        }

        public SupplierModel Read(string code)
        {
            SupplierModel supplier = null;

            try
            {
                supplier = new SupplierModel();
                connection.Open();
                command = Connection.setCommand($"SELECT SLID, SLName, RegName, TinNo, VATType, " +
                                                $"      NoStreet, CityID, ProvID, RegionID, Zip, Brgy, " +
                                                $"      BusinessName, BldgNo, TelNo, " +
                                                $"      CellNo, Website, EmailAdd, SLType, SupplierType, " +
                                                $"      TermID, IsActive" +
                                                $"  FROM TblSubsidiaryMain WITH (READPAST) " +
                                                $"  WHERE SLID = @code", connection);
                //command = Connection.setCommand($"SELECT a.SLID, a.SLName, a.RegName, a.TinNo, a.VATType, " +
                //                                $"      a.NoStreet, a.CityID, b.ProvName, a.RegionID, a.Zip, a.TelNo, " +
                //                                $"      a.CellNo, a.Website, a.EmailAdd, a.SLType, a.SupplierType, " +
                //                                $"      a.TermID, a.IsActive " +
                //                                $"  FROM TblSubsidiaryMain a WITH(READPAST) " +
                //                                $"  LEFT JOIN TblProvinceMF b WITH(READPAST) ON b.ProvID = a.ProvID" +
                //                                $"  WHERE a.SLID = @code", connection);
                command.Parameters.AddWithValue("@code", code);
                reader = command.ExecuteReader();

                while (reader.Read()) 
                {
                    supplier.SLID = reader.GetString(reader.GetOrdinal("SLID"));
                    supplier.SLName = reader.GetString(reader.GetOrdinal("SLName")).TrimEnd();
                    supplier.RegName = reader.GetString(reader.GetOrdinal("RegName")).TrimEnd();
                    supplier.TinNo = reader.GetString(reader.GetOrdinal("TinNo")).TrimEnd();
                    supplier.VATType = reader.GetDecimal(reader.GetOrdinal("VATType"));
                    supplier.NoStreet = reader.GetString(reader.GetOrdinal("NoStreet"));
                    supplier.CityID = reader.GetString(reader.GetOrdinal("CityID")).TrimEnd();
                    supplier.ProvID = reader.GetString(reader.GetOrdinal("ProvID")).TrimEnd();
                    supplier.RegionID = reader.GetString(reader.GetOrdinal("RegionID")).TrimEnd();
                    supplier.Zip = reader.GetString(reader.GetOrdinal("Zip"));
                    supplier.Brgy = reader.GetString(reader.GetOrdinal("Brgy"));
                    supplier.FlrNo = reader.GetString(reader.GetOrdinal("BldgNo"));
                    supplier.BusinessName = reader.GetString(reader.GetOrdinal("BusinessName"));
                    supplier.TelNo = reader.GetString(reader.GetOrdinal("TelNo"));
                    supplier.CelNo = reader.GetString(reader.GetOrdinal("CellNo"));
                    supplier.Website = reader.GetString(reader.GetOrdinal("Website")).TrimEnd();
                    supplier.EmailAdd = reader.GetString(reader.GetOrdinal("EmailAdd")).TrimEnd();
                    supplier.SLType = reader.GetString(reader.GetOrdinal("SLType"));
                    supplier.SupplierType = reader.GetString(reader.GetOrdinal("SupplierType"));
                    supplier.TermID = reader.GetString(reader.GetOrdinal("TermID")).TrimEnd();
                    supplier.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                }

                return supplier;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            } finally
            {
                connection.Close();
            }

            return supplier;
        }

        public override void Read(SupplierModel entity)
        {
            try
            {
                connection.Open();
                Connection.GetDataTable("SELECT * FROM TblSubsidiaryMain WITH(READPAST) WHERE SLType=1");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void Read(SupplierModel entity, bool IsContact)
        {
            if (IsContact)
            {
                try
                {
                    connection.Open();
                    Connection.GetDataTable("SELECT * FROM TblSubsidiaryContacts WITH(READPAST) WHERE SLID = @id");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            throw new NotImplementedException();
        }

        public override DataTable dt(SupplierModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand(
                    " SELECT a.SLID, a.SupplierType, a.VATType, " +
                    " CASE WHEN a.VATType = 0 THEN 'VAT INCLUSIVE' " +
                    " WHEN a.VATType = 1    " +
                    " THEN 'VAT EXCLUSIVE' " +
                    " WHEN a.VATType = 2 THEN 'Zero Rated' " +
                    " WHEN a.VATType = 3 THEN 'Non Vat' " +
                    " WHEN a.VATType = 4 THEN 'VAT Exempt' " +
                    " END AS VATName, a.RegName, a.SLName, a.TinNo, b.TermName," +
                    " CASE WHEN a.SupplierType = 'I' THEN 'Imported' " +
                    " WHEN a.SupplierType = 'L' THEN 'Local' " +
                    " END AS SupplierName " +
                    " FROM TblSubsidiaryMain a WITH(READPAST) " +
                    " LEFT JOIN TblTermsMF b WITH(READPAST) ON b.TermID = a.TermID " +
                    " WHERE a.SLType = 'S' " +
                    "       AND (1=(CASE WHEN ISNULL(@SLName,'') = '' THEN 1 ELSE 0 END) OR a.SLName LIKE '%' + @SLName + '%') " +
                    "       AND (1=(CASE WHEN ISNULL(@RegName,'') = '' THEN 1 ELSE 0 END) OR a.RegName LIKE '%' + @RegName + '%') " +
                    "       AND (1=(CASE WHEN ISNULL(@SupplierType,'') = '' OR ISNULL(@SupplierType,'') = 'ALL'  THEN 1 ELSE 0 END) OR a.SupplierType LIKE '%' + @SupplierType + '%') " +
                    "       AND (1=(CASE WHEN ISNULL(@TinNo,'') = '' THEN 1 ELSE 0 END) OR a.TinNo LIKE '%' + @TinNo + '%') " +
                    "       AND (1=(CASE WHEN ISNULL(@VATType,0) = 5 THEN 1 ELSE 0 END) OR a.VATType  = @VATType ) " +
                    "       AND (1=(CASE WHEN ISNULL(@TermID,'') = '' THEN 1 ELSE 0 END) OR a.TermID LIKE '%' + @TermID + '%') ", connection);
                command.Parameters.AddWithValue("@SLName", entity.SLName);
                command.Parameters.AddWithValue("@RegName", entity.RegName);
                command.Parameters.AddWithValue("@TinNo", entity.TinNo);
                command.Parameters.AddWithValue("@TermID", entity.TermID);
                command.Parameters.AddWithValue("@SupplierType", entity.SupplierType);
                command.Parameters.AddWithValue("@VATType", entity.VATType);
                reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close() ;
            }
            return dt;
        }

        public DataTable dtC(string slid)
        {
            DataTable dt = new DataTable();
            try 
            {
                connection.Open();
                command = Connection.setCommand("SELECT * FROM TblSubsidiaryContacts WHERE SLID = @SLID", connection);
                command.Parameters.AddWithValue("@SLID", slid);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            } 
            finally 
            {
                connection.Close();
            }
            return dt;
        }

        public override string Update(SupplierModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"UPDATE TblSubsidiaryMain SET SLName=@SLName, RegName=@RegName, TinNo=@TinNo, VATType=@VATType, NoStreet=@NoStreet, " +
                                                $"      CityID=@CityID, ProvID=(SELECT ProvID FROM TblProvinceMF WITH(READPAST) WHERE ProvName = @ProvID), " +
                                                $"      RegionID=(SELECT RegionID FROM TblRegionMF WITH(READPAST) WHERE RegionName = @RegionID), " +
                                                $"      Zip=@Zip, TelNo=@TelNo, CellNo=@CellNo, Website=@Website, " +
                                                $"      EmailAdd=@EmailAdd, SLType=@SLType, SupplierType=@SupplierType, TermID=@TermID, ModifiedBy=@ModifiedBy, " +
                                                $"      BldgNo = @BldgNo, Brgy = @Brgy, BusinessName = @BusinessName, " +
                                                $"      ModifiedDt=GETDATE(), IsActive=@IsActive" +
                                                $"  WHERE SLID=@SLID", connection,transaction);
                command.Parameters.AddWithValue("@SLID", entity.SLID);
                command.Parameters.AddWithValue("@SLName", entity.SLName);
                command.Parameters.AddWithValue("@RegName", entity.RegName);
                command.Parameters.AddWithValue("@TinNo", entity.TinNo);
                command.Parameters.AddWithValue("@VATType", entity.VATType);
                command.Parameters.AddWithValue("@NoStreet", entity.NoStreet);
                command.Parameters.AddWithValue("@CityID", entity.CityID);
                command.Parameters.AddWithValue("@ProvID", entity.ProvID);
                command.Parameters.AddWithValue("@RegionID", entity.RegionID);
                command.Parameters.AddWithValue("@Zip", entity.Zip);
                command.Parameters.AddWithValue("@TelNo", entity.TelNo);
                command.Parameters.AddWithValue("@CellNo", entity.CelNo);
                command.Parameters.AddWithValue("@Website", entity.Website);
                command.Parameters.AddWithValue("@EmailAdd", entity.EmailAdd);
                command.Parameters.AddWithValue("@SLType", entity.SLType);
                command.Parameters.AddWithValue("@SupplierType", entity.SupplierType);
                command.Parameters.AddWithValue("@TermID", entity.TermID);
                command.Parameters.AddWithValue("@BldgNo", entity.FlrNo);
                command.Parameters.AddWithValue("@Brgy", entity.Brgy);
                command.Parameters.AddWithValue("@BusinessName", entity.BusinessName);
                command.Parameters.AddWithValue("@ModifiedBy", Name01);
                command.Parameters.AddWithValue("@IsActive", 1);
                int i = command.ExecuteNonQuery();
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
                        command = Connection.setTransactionCommand(
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
                            $" END", connection, transaction);
                        command.Parameters.AddWithValue("@SLID", data.SLID);
                        command.Parameters.AddWithValue("@UniqueID", data.UniqueID);
                        command.Parameters.AddWithValue("@ContactPerson", data.ContactPerson);
                        command.Parameters.AddWithValue("@ContactNo", data.ContactNo);
                        command.Parameters.AddWithValue("@EmailAdd", data.EmailAdd);
                        command.Parameters.AddWithValue("@Remarks", data.Remarks);
                        command.Parameters.AddWithValue("@IsActive", data.IsActive);
                        k = command.ExecuteNonQuery();
                        if (k > 0)
                        {
                            Helper.TranLog("Supplier Contact", "Updated Contact :" + data.ContactPerson, connection, command, transaction);
                        }
                        else
                        {
                            message = "The data's entered is already been addedd.";
                            transaction.Rollback();
                            connection.Close();
                            return message;
                        }
                    }
                }
                Helper.TranLog("Supplier Library", "Updated supplier:" + entity.SLID, connection, command, transaction);
                transaction.Commit();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
            }
            finally 
            { 
                connection.Close(); 
            }
            return message;
        }

        public void Update(SupplierModel entity, bool IsContact)
        {
            if (IsContact)
            {
                command = Connection.setCommand($"UPDATE TblSubsidiaryContacts SET ContactPerson=@ContactPerson, ContactNo=@ContactNo, EmailAdd=@EmailAdd, Remarks=@Remarks, " +
                                                $"      ModifiedBy=@ModifiedBy, ModifiedDt=GETDATE(), IsActive" +
                                                $"  WHERE UniqueID=@UniqueID " +
                                                $"      AND SLID=@SLID", connection);
            }
            throw new NotImplementedException();
        }

        public SortedDictionary<string, string> GetCity()
        {

            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();
            try
            {
                connection.Open();
                command =  Connection.setCommand($"SELECT  CityName, CityID FROM TblCityMF WITH (READPAST)", connection);
                reader = command.ExecuteReader();
                dictionary.Add("", "");
                while (reader.Read())
                {
                    CityModel city = new CityModel();
                    city.CityID = reader.GetString(1).TrimEnd();
                    city.CityName = reader.GetString(0).TrimEnd();

                    dictionary.Add(city.CityName, city.CityID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dictionary;
        }

       

        public SortedDictionary<string,string> getTerms()
        {
            SortedDictionary<string, string> Termsdictionary = new SortedDictionary<string, string>();
            try
            {
                connection.Open();
                command = Connection.setCommand($"SELECT TermID, TermName FROM TblTermsMF WITH (READPAST)", connection);
                reader = command.ExecuteReader();
                Termsdictionary.Add("", "");
                while (reader.Read())
                {
                    TermsModel terms = new TermsModel();
                    terms.TermID = reader.GetString(0).Trim();
                    terms.TermName = reader.GetString(1).Trim();
                    Termsdictionary.Add(terms.TermID, terms.TermName);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
            return Termsdictionary;
        }
        public SortedDictionary<string, string> GetProvince()
        {
            SortedDictionary<string, string> provincedictionary = new SortedDictionary<string, string>();
            try
            {
                connection.Open();
                command = Connection.setCommand($"SELECT ProvName, ProvID FROM TblProvinceMF WITH (READPAST)", connection);
                reader = command.ExecuteReader();
                provincedictionary.Add("", "");
                while (reader.Read())
                {
                    ProvinceModel province = new ProvinceModel();
                    province.ProvID = reader.GetString(1).TrimEnd();
                    province.ProvName = reader.GetString(0).TrimEnd();
                    provincedictionary.Add(province.ProvID, province.ProvName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return provincedictionary;
        }

        public SortedDictionary<string, string> GetRegion()
        {
            SortedDictionary<string, string> regiondictionary = new SortedDictionary<string, string>();
            try
            {
                connection.Open();
                command = Connection.setCommand($"SELECT RegionID, RegionName FROM TblRegionMF WITH (READPAST)", connection);
                reader = command.ExecuteReader();
                regiondictionary.Add("", "");
                while (reader.Read())
                {
                    RegionModel region = new RegionModel();
                    region.RegionID = reader.GetString(0).TrimEnd();
                    region.RegionName = reader.GetString(1).TrimEnd();

                    regiondictionary.Add(region.RegionID, region.RegionName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return regiondictionary;
        }

        public string getSelectedCustomerType(string slid)
        {
                string i = "0";
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT SLType FROM TblSubsidiaryMain WHERE SLID = @SLID", connection);
                command.Parameters.AddWithValue("@SLID", slid);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    i = reader.GetString(1);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close ();
            }
            return i;
        }
        
        public List<SupplierContactModel> getContacts(string slid)
        {
            List<SupplierContactModel> contacts = new List<SupplierContactModel>();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT UniqueID,ContactPerson,ContactNo,EmailAdd,Remarks, IsActive FROM TblSubsidiaryContacts " +
                    " WHERE SLID = @SLID", connection);

                command.Parameters.AddWithValue("@SLID", slid);
                //command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SupplierContactModel supplierContactModel = new SupplierContactModel();
                    supplierContactModel.UniqueID = reader.GetString(0);
                    supplierContactModel.ContactPerson = reader.GetString(1);
                    supplierContactModel.ContactNo = reader.GetString(2);
                    supplierContactModel.EmailAdd = reader.GetString(3);
                    supplierContactModel.Remarks = reader.GetString(4);
                    supplierContactModel.IsActive = reader.GetBoolean(5);
                    contacts.Add(supplierContactModel);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine (ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return contacts;
        }

        public SortedDictionary<string,string> getSelectedProvince(string selectedReg)
        {
            SortedDictionary<string,string> selectedProvince = new SortedDictionary<string,string>();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT ProvName, ProvID FROM TblProvinceMF WITH (READPAST) WHERE RegionID = @regid AND ISNULL(RegionID,'') <> '' AND IsActive = 1 ORDER BY ProvName ASC", connection);
                command.Parameters.AddWithValue("regid", selectedReg);
                reader = command.ExecuteReader();
                selectedProvince.Add("", "");
                while (reader.Read())
                {
                    ProvinceModel selectedProvinceModel = new ProvinceModel();
                    selectedProvinceModel.ProvID = reader.GetString(0).Trim();
                    selectedProvinceModel.ProvName = reader.GetString(1).Trim();

                    selectedProvince.Add(selectedProvinceModel.ProvID, selectedProvinceModel.ProvName);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return selectedProvince;
        }

        public SortedDictionary<string,string> getSelectedCity(string provid)
        {
            SortedDictionary<string,string> selectedCity = new SortedDictionary<string,string>();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT  CityName,CityID FROM TblCityMF WITH (READPAST) WHERE ProvID = @provid AND ISNULL(ProvID,'') <> '' AND IsActive = 1 ORDER BY CityName ASC", connection);
                command.Parameters.AddWithValue("provid", provid);
                reader = command.ExecuteReader();
                //selectedCity.Add("", "");
                while (reader.Read())
                {
                    CityModel selectedCityModel = new CityModel();
                    selectedCityModel.CityID = reader.GetString(1).Trim();
                    selectedCityModel.CityName = reader.GetString(0).Trim();

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
                connection.Close();
            }
            return selectedCity;
        }

        public Dictionary<string, string> getSelectedProvice(string selectedCity)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.ProvName, c.RegionName FROM TblProvinceMF a WITH(READPAST) " +
                    " LEFT JOIN TblCityMF b WITH(READPAST) ON b.ProvID = a.ProvID " +
                    " LEFT JOIN TblRegionMF c WITH(READPAST) ON c.RegionID = a.RegionID" +
                    " WHERE b.CityName = @cityname", connection);
                command.Parameters.AddWithValue("@cityname", selectedCity);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProvinceModel model = new ProvinceModel();
                    model.selectedCity = reader.GetString(0).Trim();
                    model.selectedRegion = reader.GetString(1).Trim();

                    dictionary.Add(model.selectedCity, model.selectedRegion);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { connection.Close(); }
            return dictionary;
        }

    }
}
