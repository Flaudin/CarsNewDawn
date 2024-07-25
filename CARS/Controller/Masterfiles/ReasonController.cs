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
    internal class ReasonController : Universal<ReasonModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(ReasonModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT ReasonID FROM TblReasonMF WITH(READPAST) WHERE ReasonName=@ReasonName) " +
                                                           $"BEGIN " +
                                                           $"  INSERT INTO TblReasonMF(ReasonID, ReasonName, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                           $"      VALUES(@ReasonID, @ReasonName, @CreatedBy, GETDATE(), @CreatedBy, GETDATE())" +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@ReasonID", entity.ReasonID);
                command.Parameters.AddWithValue("@ReasonName", entity.ReasonName);
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
                Helper.TranLog("Reason Master", "Added a new reason:" + entity.ReasonID, connection, command, transaction);
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

        public override void Delete(ReasonModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(ReasonModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT ReasonID, RTRIM(ReasonName) AS ReasonName " +
                                                "   FROM TblReasonMF WITH(READPAST) " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@ReasonName,'') = '' THEN 1 ELSE 0 END) OR ReasonName LIKE '%' + @ReasonName + '%') " +
                                                "   ORDER BY ReasonName", connection);
                command.Parameters.AddWithValue("@ReasonName", entity.ReasonName);
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

        public override void Read(ReasonModel entity)
        {
            throw new NotImplementedException();
        }

        public string GenerateReasonID()
        {
            string id = "";
            try
            {
                connection.Open();
                command = Connection.setCommand("DECLARE @reasonid varchar(10) = '' " +
                                                "DECLARE @date varchar(4) = ((SELECT FORMAT(GETDATE(), 'yy'))+(SELECT FORMAT(GETDATE(),'MM'))) " +
                                                "DECLARE @prefix varchar(10) = 'RE'+@date " +
                                                "DECLARE @countable int = (SELECT COUNT(*) FROM TblReasonMF WITH(READPAST)) " +
                                                "SET @reasonid = @prefix+'0001'; " +
                                                "IF @countable > 0 " +
                                                "   BEGIN " +
                                                "   SET @reasonid = (SELECT TOP 1 ReasonID FROM TblReasonMF WITH(READPAST) WHERE CAST(SUBSTRING(ReasonID,1,6) AS varchar) = @prefix ORDER BY ReasonID DESC) " +
                                                "   IF @reasonid IS NULL " +
                                                "       BEGIN " +
                                                "           SET @reasonid = @prefix+'0001'; " +
                                                "       END " +
                                                "   ELSE " +
                                                "       BEGIN " +
                                                "           SET @reasonid = (SELECT TOP 1 @prefix+ REPLICATE('0',4-LEN(SUBSTRING(ReasonID,7,4)+1)) + CAST(SUBSTRING(ReasonID,7,4)+1 AS varchar) FROM TblReasonMF WITH(READPAST) WHERE CAST(SUBSTRING(ReasonID,1,6) AS varchar) = @prefix ORDER BY ReasonID DESC) " +
                                                "       END " +
                                                "   END " +
                                                "SELECT @reasonid AS ReasonID", connection);
                id = Convert.ToString(command.ExecuteScalar() ?? "");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return id;
        }

        public override string Update(ReasonModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT ReasonID FROM TblReasonMF WITH(READPAST) WHERE ReasonName=@ReasonName AND ReasonID!=@ReasonID) " +
                                                           $"BEGIN " +
                                                           $"   UPDATE TblReasonMF " +
                                                           $"       SET ReasonName=@ReasonName, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                           $"       WHERE ReasonID=@ReasonID " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@ReasonID", entity.ReasonID);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                command.Parameters.AddWithValue("@ReasonName", entity.ReasonName);
                int i = command.ExecuteNonQuery();
                if (i != 1)
                {
                    message = "The information entered is already present in the database.";
                    transaction.Rollback();
                    transaction.Dispose();
                    connection.Close();
                    return message;
                }
                Helper.TranLog("Reason Master", "Modified reason:" + entity.ReasonID, connection, command, transaction);
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
