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
    internal class ProvinceController : Universal<ProvinceModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(ProvinceModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT uniqueid FROM TblProvinceMF WITH(READPAST) WHERE ProvID=@ProvID OR ProvName=@ProvName) " +
                                                           $"BEGIN " +
                                                           $"  INSERT INTO TblProvinceMF(uniqueid, region_code, ProvID, ProvName, RegionID, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, IsActive) " +
                                                           $"      VALUES(@uniqueid, @region_code, @ProvID, @ProvName, @RegionID, @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), @IsActive)" +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@uniqueid", entity.uniqueid);
                command.Parameters.AddWithValue("@region_code", "");
                command.Parameters.AddWithValue("@RegionID", entity.RegionID);
                command.Parameters.AddWithValue("@ProvID", entity.ProvID);
                command.Parameters.AddWithValue("@ProvName", entity.ProvName);
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
                Helper.TranLog("Province Master", "Added a new Province:" + entity.ProvID, connection, command, transaction);
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

        public override void Delete(ProvinceModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(ProvinceModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(ProvinceModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT uniqueid FROM TblProvinceMF WITH(READPAST) WHERE (ProvID=@ProvID OR ProvName=@ProvName) AND uniqueid!=@uniqueid) " +
                                                           $"BEGIN " +
                                                           $"  UPDATE TblProvinceMF SET region_code=@region_code, ProvID=@ProvID, ProvName=@ProvName, RegionID=@RegionID, ModifiedBy=@CreatedBy, " +
                                                           $"          ModifiedDt=GETDATE(), IsActive=@IsActive " +
                                                           $"      WHERE uniqueid=@uniqueid " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@uniqueid", entity.uniqueid);
                command.Parameters.AddWithValue("@region_code", entity.region_code);
                command.Parameters.AddWithValue("@RegionID", entity.RegionID);
                command.Parameters.AddWithValue("@ProvID", entity.ProvID);
                command.Parameters.AddWithValue("@ProvName", entity.ProvName);
                command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                int i = command.ExecuteNonQuery();
                if (i != 1)
                {
                    message = "The information entered is already present in the database.";
                    transaction.Rollback();
                    transaction.Dispose();
                    connection.Close();
                    return message;
                }
                Helper.TranLog("Province Master", "Modified Province:" + entity.ProvID, connection, command, transaction);
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

                command = Connection.setCommand($"SELECT RegionID, RegionName FROM TblRegionMF WITH(READPAST)", connection);
                reader = command.ExecuteReader();

                dictionary.Add("", "");
                while (reader.Read())
                {
                    RegionModel model = new RegionModel();

                    model.RegionID = reader.GetString(0).TrimEnd();
                    model.RegionName = reader.GetString(1).TrimEnd();

                    dictionary.Add(model.RegionName, model.RegionID);
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

        public override DataTable dt(ProvinceModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand($"SELECT a.uniqueid, a.region_code, a.ProvName, a.ProvID, RTRIM(b.RegionName) AS RegionID, a.IsActive " +
                                                $"   FROM TblProvinceMF a WITH(READPAST) " +
                                                $"   LEFT JOIN TblRegionMF b WITH(READPAST) ON b.RegionID = a.RegionID " +
                                                $"   WHERE (1=(CASE WHEN ISNULL(@RegionID,'') = '' THEN 1 ELSE 0 END) OR a.RegionID LIKE '%' + @RegionID + '%') " +
                                                $"       AND (1=(CASE WHEN ISNULL(@ProvName,'') = '' THEN 1 ELSE 0 END) OR a.ProvName LIKE '%' + @ProvName + '%') " +
                                                $"  ORDER BY a.ProvName ", connection);
                command.Parameters.AddWithValue("@RegionID", entity.RegionID);
                command.Parameters.AddWithValue("@ProvName", entity.ProvName);
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
