using CARS.Functions;
using CARS.Model;
using CARS.Model.Masterfiles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Controller.Masterfiles
{
    internal class CityController : Universal<CityModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(CityModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT uniqueid FROM TblCityMF WITH(READPAST) WHERE CityID=@CityID OR CityName=@CityName OR zip_code=@zip_code) " +
                                                           $"BEGIN " +
                                                           $"  INSERT INTO TblCityMF(uniqueid, CityID, CityName, zip_code, ProvID, within_gma, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, IsActive) " +
                                                           $"      VALUES(@uniqueid, @CityID, @CityName, @zip_code, @ProvID, @within_gma, @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), @IsActive)" +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@uniqueid", entity.uniqueid);
                command.Parameters.AddWithValue("@CityID", entity.CityID);
                command.Parameters.AddWithValue("@CityName", entity.CityName);
                command.Parameters.AddWithValue("@zip_code", entity.zip_code);
                command.Parameters.AddWithValue("@ProvID", entity.ProvID);
                command.Parameters.AddWithValue("@within_gma", entity.with_gma);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                int i = command.ExecuteNonQuery();
                if (i != 1)
                {
                    message = "The information entered is already present in the database.";
                    transaction.Rollback();
                    transaction.Dispose();
                    connection.Close();
                    return message;
                }
                Helper.TranLog("City Master", "Added a new City:" + entity.CityID, connection, command, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                message = ex.Message;
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

        public override void Delete(CityModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(CityModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(CityModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT uniqueid FROM TblCityMF WITH(READPAST) WHERE (CityID=@CityID OR CityName=@CityName) AND uniqueid!=@uniqueid) " +
                                                           $"BEGIN " +
                                                           $"  UPDATE TblCityMF SET CityID=@CityID, CityName=@CityName, zip_code=@zip_code, ProvID=@ProvID, within_gma=@within_gma, ModifiedBy=@CreatedBy, " +
                                                           $"          ModifiedDt=GETDATE(), IsActive=@IsActive " +
                                                           $"      WHERE uniqueid=@uniqueid " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@uniqueid", entity.uniqueid);
                command.Parameters.AddWithValue("@CityID", entity.CityID);
                command.Parameters.AddWithValue("@CityName", entity.CityName);
                command.Parameters.AddWithValue("@zip_code", entity.zip_code);
                command.Parameters.AddWithValue("@ProvID", entity.ProvID);
                command.Parameters.AddWithValue("@within_gma", entity.with_gma);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                int i = command.ExecuteNonQuery();
                if (i != 1)
                {
                    message = "The information entered is already present in the database.";
                    transaction.Rollback();
                    transaction.Dispose();
                    connection.Close();
                    return message;
                }
                Helper.TranLog("City Master", "Modified City:" + entity.CityID, connection, command, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                message = ex.Message;
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

        public SortedDictionary<string, string> GetDictionary()
        {
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();

            try
            {
                connection.Open();

                command = Connection.setCommand($"SELECT ProvID, ProvName FROM TblProvinceMF WITH(READPAST)", connection);
                reader = command.ExecuteReader();

                dictionary.Add("", "");
                while (reader.Read())
                {
                    ProvinceModel model = new ProvinceModel();

                    model.ProvID = reader.GetString(reader.GetOrdinal("ProvId")).TrimEnd();
                    model.ProvName = reader.GetString(reader.GetOrdinal("ProvName")).TrimEnd();

                    dictionary.Add(model.ProvName, model.ProvID);
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

        public override DataTable dt(CityModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.uniqueid, RTRIM(a.CityName) AS CityName, a.CityID, a.zip_code, RTRIM(b.ProvName) AS ProvID, a.IsActive, a.within_gma " +
                                                "   FROM TblCityMF a WITH(READPAST) " +
                                                "   LEFT JOIN TblProvinceMF b WITH(READPAST) ON b.ProvID = a.ProvID" +
                                                "   WHERE (1=(CASE WHEN ISNULL(@ProvID,'') = '' THEN 1 ELSE 0 END) OR a.ProvID LIKE '%' + @ProvID + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@CityName,'') = '' THEN 1 ELSE 0 END) OR a.CityName LIKE '%' + @CityName + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@zip_code,'') = '' THEN 1 ELSE 0 END) OR a.zip_code LIKE '%' + @zip_code + '%') " +
                                                "   ORDER BY a.CityName ", connection);
                command.Parameters.AddWithValue("@CityName", entity.CityName);
                command.Parameters.AddWithValue("@zip_code", entity.zip_code);
                command.Parameters.AddWithValue("@ProvID", entity.ProvID);
                reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
    }
}
