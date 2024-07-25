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
    internal class VehicleMakeController : Universal<VehicleMakeModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(VehicleMakeModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT MakeID FROM TblPartsVehicleMakeMF WITH(READPAST) WHERE MakeID=@MakeID OR MakeName=@MakeName) " +
                                                           $"BEGIN " +
                                                           $"   INSERT INTO TblPartsVehicleMakeMF(MakeID, MakeName, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, BOwn, IsActive) " +
                                                           $"       VALUES(@MakeID, @MakeName, @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), 0, @IsActive) " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@MakeID", entity.MakeID);
                command.Parameters.AddWithValue("@MakeName", entity.MakeName);
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
                Helper.TranLog("VehicleMake Master", "Created new Vehicle Make:" + entity.MakeID, connection, command, transaction);
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

        public override void Delete(VehicleMakeModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(VehicleMakeModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand($"SELECT RTRIM(MakeID) AS MakeID, RTRIM(MakeName) AS MakeName, BOwn, IsActive " +
                                                $"   FROM TblPartsVehicleMakeMF WITH(READPAST) " +
                                                $"   WHERE (1=(CASE WHEN ISNULL(@MakeName,'') = '' THEN 1 ELSE 0 END) OR MakeName LIKE '%' + @MakeName + '%') " +
                                                $"  ORDER BY MakeName ", connection);
                command.Parameters.AddWithValue("@MakeName", entity.MakeName);
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

        public override void Read(VehicleMakeModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(VehicleMakeModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT MakeID FROM TblPartsVehicleMakeMF WITH(READPAST) WHERE MakeName=@MakeName AND MakeID!=@MakeID) " +
                                                           $"BEGIN " +
                                                           $"   UPDATE TblPartsVehicleMakeMF " +
                                                           $"       SET MakeName=@MakeName, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE(), IsActive=@IsActive " +
                                                           $"       WHERE MakeID=@MakeID " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@MakeID", entity.MakeID);
                command.Parameters.AddWithValue("@MakeName", entity.MakeName);
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
                Helper.TranLog("VehicleMake Master", "Modified VehicleMake:" + entity.MakeID, connection, command, transaction);
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
