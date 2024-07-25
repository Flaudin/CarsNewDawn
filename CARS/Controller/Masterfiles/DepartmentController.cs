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
    internal class DepartmentController : Universal<DepartmentModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(DepartmentModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT DeptID FROM TblDepartmentMF WITH(READPAST) WHERE DeptID=@DeptID OR DeptName=@DeptName) " +
                                                           $"BEGIN " +
                                                           $"  INSERT INTO TblDepartmentMF(DeptID, DeptName, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, IsActive) " +
                                                           $"      VALUES(@DeptID, @DeptName, @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), @IsActive)" +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@DeptID", entity.DeptID);
                command.Parameters.AddWithValue("@DeptName", entity.DeptName);
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
                Helper.TranLog("Department Master", "Added a new Department:" + entity.DeptID, connection, command, transaction);
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

        public override void Delete(DepartmentModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(DepartmentModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(DeptID) AS DeptID, RTRIM(DeptName) AS DeptName, IsActive " +
                                                "   FROM TblDepartmentMF WITH(READPAST) " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@DeptName,'') = '' THEN 1 ELSE 0 END) OR DeptName LIKE '%' + @DeptName + '%') " +
                                                "   ORDER BY DeptName ", connection);
                command.Parameters.AddWithValue("@DeptName", entity.DeptName);
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

        public override void Read(DepartmentModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(DepartmentModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT DeptID FROM TblDepartmentMF WITH(READPAST) WHERE DeptName=@DeptName AND DeptID!=@DeptID) " +
                                                           $"BEGIN " +
                                                           $"  UPDATE TblDepartmentMF SET DeptName=@DeptName, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE(), IsActive=@IsActive " +
                                                           $"      WHERE DeptID=@DeptID " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@DeptID", entity.DeptID);
                command.Parameters.AddWithValue("@DeptName", entity.DeptName);
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
                Helper.TranLog("Department Master", "Modified Department:" + entity.DeptID, connection, command, transaction);
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
