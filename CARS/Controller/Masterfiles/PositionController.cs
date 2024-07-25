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
    internal class PositionController : Universal<PositionModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(PositionModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT PosID FROM TblPositionMF WITH(READPAST) WHERE PosID=@PosID OR PosName=@PosName) " +
                                                           $"BEGIN " +
                                                           $"  INSERT INTO TblPositionMF(PosID, PosName, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, IsActive) " +
                                                           $"      VALUES(@PosID, @PosName, @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), @IsActive)" +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@PosID", entity.PosID);
                command.Parameters.AddWithValue("@PosName", entity.PosName);
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
                Helper.TranLog("Position Master", "Added a new Position:" + entity.PosID, connection, command, transaction);
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

        public override void Delete(PositionModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(PositionModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(PosID) AS PosID, RTRIM(PosName) AS PosName, IsActive " +
                                                "   FROM TblPositionMF WITH(READPAST) " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@PosName,'') = '' THEN 1 ELSE 0 END) OR PosName LIKE '%' + @PosName + '%')", connection);
                command.Parameters.AddWithValue("@PosName", entity.PosName);
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

        public override void Read(PositionModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(PositionModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT PosID FROM TblPositionMF WITH(READPAST) WHERE PosName=@PosName AND PosID!=@PosID) " +
                                                           $"BEGIN " +
                                                           $"  UPDATE TblPositionMF SET PosName=@PosName, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE(), IsActive=@IsActive " +
                                                           $"      WHERE PosID=@PosID " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@PosID", entity.PosID);
                command.Parameters.AddWithValue("@PosName", entity.PosName);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                command.ExecuteNonQuery();
                int i = command.ExecuteNonQuery();
                if (i != 1)
                {
                    message = "The information entered is already present in the database.";
                    transaction.Rollback();
                    transaction.Dispose();
                    connection.Close();
                    return message;
                }
                Helper.TranLog("Position Master", "Modified Position:" + entity.PosID, connection, command, transaction);
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
