using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CARS.Functions;
using CARS.Model;
using CARS.Model.Masterfiles;
using CARS.Model.Utilities;

namespace CARS.Controller.Utilities
{
    internal class CompanyController : Universal<CompanyModel>
    {
        private static SqlConnection conn = Connection.GetConnection();
        private static SqlCommand cmd = null;
        private static SqlDataReader rd = null;
        public const string cmpCode = "CC24000001";

        private static List<dynamic> companyList = new List<dynamic>();

        public override string Create(CompanyModel entity)
        {

            throw new NotImplementedException();
        }

        public override void Delete(CompanyModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(CompanyModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(CompanyModel entity)
        {  
            
        }

        public CompanyModel companyMods()
        {
            CompanyModel compModel = new CompanyModel();
            try {
                //if()
                conn.Open();
                cmd = Connection.setCommand("SELECT * FROM TblCompanyProfile WITH (READPAST)",conn);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    compModel.CompName = rd.GetString(1).TrimEnd();
                    compModel.RegName = rd.GetString(2).TrimEnd();
                    compModel.NoStreet = rd.GetString(5).TrimEnd();
                    compModel.CityID = rd.GetString(6).TrimEnd();
                    compModel.ProvID = rd.GetString(7).TrimEnd();
                    compModel.RegionID = rd.GetString(8).TrimEnd();
                    compModel.TelNo = rd.GetString(10).TrimEnd();
                    compModel.Web = rd.GetString(11).TrimEnd();
                    compModel.EmailAdd = rd.GetString(12).TrimEnd();
                    compModel.CompLogo = rd.GetString(18).TrimEnd();
                    compModel.VatType = rd.GetDecimal(rd.GetOrdinal("VATType"));
                }
                } 
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                Helper.Confirmator("Something went wrong.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            { 
                conn.Close();
            }
                return compModel;
        }

        public override string Update(CompanyModel entity)
        {
            string msg = "Information has been updated successfully!";
            try {
                conn.Open(); 
                cmd = Connection.setCommand($"IF EXISTS(SELECT TOP 1 CompID FROM TblCompanyProfile WITH(READPAST)) " +
                                                $"BEGIN " +
                                                $" UPDATE TblCompanyProfile SET CompName = @name, RegName = @regname, TinNo = @tin, NoStreet = @street, CityID = @city, ProvID = @province, " +
                                                $" RegionID = @region, TelNo = @telpho, CellNo = @cellno, EmailAdd = @email, CompLogo = @logo,VATType = @vat, ModifiedDt=GETDATE()  " +
                                                $"END " +
                                                $"ELSE " +
                                                $"BEGIN " +
                                                $"INSERT INTO TblCompanyProfile(CompName, RegName, TinNo, NoStreet, CityID, ProvID, RegionID, TelNo, CellNo, EmailAdd, CompLogo, VATType) " +
                                                $" VALUES ( @name, @regName, @tin, @street, @city, @province, @region, @telpho, @cellno, @email, @logo, @vat) " +
                                                $"END" , conn);


                cmd.Parameters.AddWithValue("@name", entity.CompName);
                cmd.Parameters.AddWithValue("@regName", entity.RegName);
                cmd.Parameters.AddWithValue("@street", entity.NoStreet);
                cmd.Parameters.AddWithValue("@city", entity.CityID);
                cmd.Parameters.AddWithValue("@province", entity.ProvID);
                cmd.Parameters.AddWithValue("@region", entity.RegionID);
                cmd.Parameters.AddWithValue("@tin", entity.TinNo);
                cmd.Parameters.AddWithValue("@telpho", entity.TelNo);
                cmd.Parameters.AddWithValue("@cellno",entity.CellNo);
                cmd.Parameters.AddWithValue("@web", entity.Web);
                cmd.Parameters.AddWithValue("@email", entity.EmailAdd);
                cmd.Parameters.AddWithValue("@vat", entity.VatType);
                cmd.Parameters.AddWithValue("@logo", entity.CompLogo);
                //cmd.ExecuteNonQuery();
                int i = cmd.ExecuteNonQuery();
                if (i != 1)
                {
                    msg = "The information entered is already exists";
                }
                
            }catch(Exception ex) 
            {
                msg = "Something went wrong!";
                Console.WriteLine(ex);
            } 
            finally 
            { 
                conn.Close(); 
            }
            return msg;
        }
    
        public SortedDictionary<string,string> getCity()
        {
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();
            try
            {
                conn.Open();
                cmd = Connection.setCommand($"SELECT CityID, CityName FROM TblCityMF WITH (READPAST)", conn);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    CityModel city = new CityModel();
                    city.CityID = rd.GetString(0).TrimEnd();
                    city.CityName = rd.GetString(1).TrimEnd();

                    dictionary.Add(city.CityID, city.CityName);
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

        public SortedDictionary<string, string> GetProvince()
        {
            SortedDictionary<string, string> provincedictionary = new SortedDictionary<string, string>();
            try
            {
                conn.Open();
                cmd = Connection.setCommand($"SELECT ProvID, ProvName FROM TblProvinceMF WITH (READPAST)", conn);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ProvinceModel province = new ProvinceModel();
                    province.ProvID = rd.GetString(0).TrimEnd();
                    province.ProvName = rd.GetString(1).TrimEnd();

                    provincedictionary.Add(province.ProvID, province.ProvName);
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
                cmd = Connection.setCommand($"SELECT RegionID, RegionName FROM TblRegionMF WITH (READPAST)", conn);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    RegionModel region = new RegionModel();
                    region.RegionID = rd.GetString(0).TrimEnd();
                    region.RegionName = rd.GetString(1).TrimEnd();

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
    }
}

//"UPDATE TblCompanyProfile SET CompName = @name,"