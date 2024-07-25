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
    internal class BrandController : Universal<BrandModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;
        private static SqlDataAdapter adapter = null;

        public override string Create(BrandModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT uniqueid FROM TblPartsBrandMF WITH(READPAST) WHERE BrandID=@BrandID OR BrandName=@BrandName) " +
                                                           $"BEGIN " +
                                                           $"  INSERT INTO TblPartsBrandMF(uniqueid, BrandID, BrandName, BrandType, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, IsActive) " +
                                                           $"      VALUES(@uniqueid, @BrandID, @BrandName, @BrandType, @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), @IsActive)" +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@uniqueid", entity.uniqueid);
                command.Parameters.AddWithValue("@BrandID", entity.BrandID);
                command.Parameters.AddWithValue("@BrandName", entity.BrandName);
                command.Parameters.AddWithValue("@BrandType", entity.BrandType);
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
                Helper.TranLog("Brand Master", "Added a new Brand:" + entity.BrandID, connection, command, transaction);
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

        public override void Delete(BrandModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(BrandModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(BrandModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT uniqueid, RTRIM(BrandName) AS BrandName, BrandID, BrandType, IsActive " +
                                                "   FROM TblPartsBrandMF WITH(READPAST) " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@BrandName,'') = '' THEN 1 ELSE 0 END) OR BrandName LIKE '%' + @BrandName + '%') " +
                                                "   ORDER BY BrandName ", connection);
                command.Parameters.AddWithValue("@BrandName", entity.BrandName);
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

        public override string Update(BrandModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT uniqueid FROM TblPartsBrandMF WITH(READPAST) WHERE (BrandID=@BrandID OR BrandName=@BrandName) AND uniqueid!=@uniqueid) " +
                                                           $"BEGIN " + 
                                                           $"  UPDATE TblPartsBrandMF SET BrandName=@BrandName, BrandType=@BrandType, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE(), IsActive=@IsActive " +
                                                           $"      WHERE uniqueid=@uniqueid " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@uniqueid", entity.uniqueid);
                command.Parameters.AddWithValue("@BrandID", entity.BrandID);
                command.Parameters.AddWithValue("@BrandName", entity.BrandName);
                command.Parameters.AddWithValue("@BrandType", entity.BrandType);
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
                Helper.TranLog("Brand Master", "Modified Brand:" + entity.BrandID, connection, command, transaction);
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
