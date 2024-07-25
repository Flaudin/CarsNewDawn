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
    internal class DescriptionController : Universal<DescriptionModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(DescriptionModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT DescID FROM TblPartsDescriptionMF WITH(READPAST) WHERE DescID=@DescID OR DescName=@DescName) " +
                                                           $"BEGIN " +
                                                           $"  INSERT INTO TblPartsDescriptionMF(DescID, DescName, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, IsActive) " +
                                                           $"      VALUES(@DescID, @DescName, @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), @IsActive)" +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@DescID", entity.DescID);
                command.Parameters.AddWithValue("@DescName", entity.DescName);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                int i = command.ExecuteNonQuery();
                if (i != 1)
                {
                    message = "The information entered is already present in the database.";
                    transaction.Rollback();
                    connection.Close();
                    return message;
                }
                Helper.TranLog("Description Master", "Added a new Description:" + entity.DescID, connection, command, transaction);
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

        public override void Delete(DescriptionModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(DescriptionModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(DescriptionModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT DescID, RTRIM(DescName) AS DescName, IsActive " +
                                                "   FROM TblPartsDescriptionMF WITH(READPAST) " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@DescName,'') = '' THEN 1 ELSE 0 END) OR DescName LIKE '%' + @DescName + '%') " +
                                                "   ORDER BY DescName ", connection);
                command.Parameters.AddWithValue("@DescName", entity.DescName);
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

        public override string Update(DescriptionModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT DescID FROM TblPartsDescriptionMF WITH(READPAST) WHERE DescName=@DescName AND DescID!=@DescID) " +
                                                           $"BEGIN " +
                                                           $"  UPDATE TblPartsDescriptionMF " +
                                                           $"      SET DescName=@DescName, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE(), IsActive=@IsActive " +
                                                           $"      WHERE DescID=@DescID " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@DescID", entity.DescID);
                command.Parameters.AddWithValue("@DescName", entity.DescName);
                command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                int i = command.ExecuteNonQuery();
                if (i != 1)
                {
                    message = "The information entered is already present in the database.";
                    transaction.Rollback();
                    connection.Close();
                    return message;
                }
                Helper.TranLog("Description Master", "Modified Description:" + entity.DescID, connection, command, transaction);
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
