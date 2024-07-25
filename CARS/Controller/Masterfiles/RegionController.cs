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
    internal class RegionController : Universal<RegionModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(RegionModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT uniqueid FROM TblRegionMF WITH(READPAST) WHERE RegionID=@RegionID OR RegionName=@RegionName) " +
                                                           $"BEGIN " +
                                                           $"  INSERT INTO TblRegionMF(uniqueid, RegionID, RegionName, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, IsActive) " +
                                                           $"      VALUES(@uniqueid, @RegionID, @RegionName, @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), @IsActive)" +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@uniqueid", entity.uniqueid);
                command.Parameters.AddWithValue("@RegionID", entity.RegionID);
                command.Parameters.AddWithValue("@RegionName", entity.RegionName);
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
                Helper.TranLog("Region Master", "Added a new Region:" + entity.RegionID, connection, command, transaction);
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

        public override void Delete(RegionModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(RegionModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(RegionModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT uniqueid, RTRIM(RegionName) AS RegionName, RegionID, IsActive " +
                                                "   FROM TblRegionMF WITH(READPAST) " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@RegionName,'') = '' THEN 1 ELSE 0 END) OR RegionName LIKE '%' + @RegionName + '%') " +
                                                "   ORDER BY RegionName", connection);
                command.Parameters.AddWithValue("@RegionName", entity.RegionName);
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

        public override string Update(RegionModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT uniqueid FROM TblRegionMF WITH(READPAST) WHERE (RegionID=@RegionID OR RegionName=@RegionName) AND uniqueid!=@uniqueid) " +
                                                           $"BEGIN " +
                                                           $"  UPDATE TblRegionMF SET RegionID=@RegionID, RegionName=@RegionName, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE(), IsActive=@IsActive " +
                                                           $"      WHERE uniqueid=@uniqueid " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@uniqueid", entity.uniqueid);
                command.Parameters.AddWithValue("@RegionID", entity.RegionID);
                command.Parameters.AddWithValue("@RegionName", entity.RegionName);
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
                Helper.TranLog("Region Master", "Modified Region:" + entity.RegionID, connection, command, transaction);
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
    }
}
