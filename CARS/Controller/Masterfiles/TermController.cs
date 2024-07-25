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
    internal class TermController : Universal<TermsModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(TermsModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT uniqueid FROM TblTermsMF WITH(READPAST) WHERE TermID=@TermID OR TermName=@TermName) " +
                                                           $"BEGIN " +
                                                           $"  INSERT INTO TblTermsMF(uniqueid, TermID, TermName, TermDays, term_code2, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, IsActive) " +
                                                           $"      VALUES(@uniqueid, @TermID, @TermName, @TermDays, @term_code2, @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), @IsActive)" +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@uniqueid", entity.uniqueid);
                command.Parameters.AddWithValue("@TermID", entity.TermID);
                command.Parameters.AddWithValue("@TermName", entity.TermName);
                command.Parameters.AddWithValue("@TermDays", entity.TermDays);
                command.Parameters.AddWithValue("@term_code2", "");
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
                Helper.TranLog("Term Master", "Added a new Term:" + entity.TermID, connection, command, transaction);
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

        public override void Delete(TermsModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(TermsModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(TermsModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT uniqueid, term_code2, RTRIM(TermName) AS TermName, TermID, TermDays, IsActive " +
                                                "   FROM TblTermsMF WITH(READPAST)" +
                                                "   WHERE (1=(CASE WHEN ISNULL(@TermName,'') = '' THEN 1 ELSE 0 END) OR TermName LIKE '%' + @TermName + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@TermDays,0) = 0 THEN 1 ELSE 0 END) OR TermDays=@TermDays) " +
                                                "   ORDER BY TermName", connection);
                command.Parameters.AddWithValue("@TermName", entity.TermName);
                command.Parameters.AddWithValue("@TermDays", entity.TermDays);
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

        public override string Update(TermsModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT uniqueid FROM TblTermsMF WITH(READPAST) WHERE (TermID=@TermID OR TermName=@TermName) AND uniqueid!=@uniqueid) " +
                                                           $"BEGIN " +
                                                           $"  UPDATE TblTermsMF SET TermID=@TermID, TermName=@TermName, TermDays=@TermDays, term_code2=@term_code2, ModifiedBy=@CreatedBy, " +
                                                           $"          ModifiedDt=GETDATE(), IsActive=@IsActive " +
                                                           $"      WHERE uniqueid=@uniqueid " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@uniqueid", entity.uniqueid);
                command.Parameters.AddWithValue("@TermID", entity.TermID);
                command.Parameters.AddWithValue("@TermName", entity.TermName);
                command.Parameters.AddWithValue("@TermDays", entity.TermDays);
                command.Parameters.AddWithValue("@term_code2", "");
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
                Helper.TranLog("Term Master", "Modified Term:" + entity.TermID, connection, command, transaction);
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
