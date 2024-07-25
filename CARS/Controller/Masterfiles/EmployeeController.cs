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
    internal class EmployeeController : Universal<EmployeeModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(EmployeeModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT EmployeeID FROM TblEmployeeMF WITH(READPAST) WHERE EmployeeID=@EmployeeID OR EmployeeName=@EmployeeName) " +
                                                           $"BEGIN " +
                                                           $"  INSERT INTO TblEmployeeMF(EmployeeID, EmployeeName, LName, FName, MName, DateOfBirth, " +
                                                           $"           Gender, DateHired, EmploymentStatus, Remarks, DeptID, PosID, BsbAppUserName, " +
                                                           $"           CreatedBy, CreatedDt, ModifiedBy, ModifiedDt) " +
                                                           $"      VALUES(@EmployeeID, @EmployeeName, @LName, @FName, @MName, @DateOfBirth, " +
                                                           $"           @Gender, @DateHired, @EmploymentStatus, @Remarks, @DeptID, @PosID, " +
                                                           $"           @BsbAppUserName, @CreatedBy, GETDATE(), @CreatedBy, GETDATE())" +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@EmployeeID", entity.EmployeeID);
                command.Parameters.AddWithValue("@EmployeeName", entity.FName + " " + entity.MName + " " + entity.LName);
                command.Parameters.AddWithValue("@LName", entity.LName);
                command.Parameters.AddWithValue("@FName", entity.FName);
                command.Parameters.AddWithValue("@MName", entity.MName);
                command.Parameters.AddWithValue("@DateOfBirth", entity.DateOfBirth);
                command.Parameters.AddWithValue("@Gender", entity.Gender);
                command.Parameters.AddWithValue("@DateHired", entity.DateHired);
                command.Parameters.AddWithValue("@EmploymentStatus", entity.EmploymentStatus);
                command.Parameters.AddWithValue("@Remarks", entity.Remarks);
                command.Parameters.AddWithValue("@DeptID", entity.DeptID);
                command.Parameters.AddWithValue("@PosID", entity.PosID);
                command.Parameters.AddWithValue("@BsbAppUserName", entity.BsbUsername);
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
                Helper.TranLog("Employee Master", "Added a new Employee:" + entity.EmployeeID, connection, command, transaction);
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

        public override void Delete(EmployeeModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(EmployeeModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(EmployeeModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.EmployeeID, RTRIM(a.EmployeeName) AS EmployeeName, CONVERT(varchar, a.DateHired, 23) AS DateHired, " +
                                                "       RTRIM(b.DeptName) AS DeptName, RTRIM(c.PosName) AS PosName, " +
                                                "       CASE WHEN a.Gender = 1 THEN 'MALE' ELSE 'FEMALE' END AS Gender, " +
                                                "       CASE WHEN a.EmploymentStatus = 1 THEN 'CASUAL' " +
                                                "            WHEN a.EmploymentStatus = 2 THEN 'REGULAR' " +
                                                "            WHEN a.EmploymentStatus = 3 THEN 'CONSULTANT' " +
                                                "            WHEN a.EmploymentStatus = 4 THEN 'RESIGNED' " +
                                                "            WHEN a.EmploymentStatus = 5 THEN 'RETIRED' " +
                                                "            WHEN a.EmploymentStatus = 6 THEN 'TERMINATED' " +
                                                "            ELSE '' END AS EmploymentStatus, RTRIM(a.Remarks) AS Remarks, RTRIM(a.BsbAppUserName) AS BsbAppUserName, " +
                                                "       RTRIM(a.FName) AS FName, RTRIM(a.MName) AS MName, RTRIM(a.LName) AS LName, " +
                                                "       CONVERT(varchar, a.DateOfBirth, 23) AS DateOfBirth " +
                                                "   FROM TblEmployeeMF a WITH(READPAST) " +
                                                "   LEFT JOIN TblDepartmentMF b WITH(READPAST) ON b.DeptID = a.DeptID " +
                                                "   LEFT JOIN TblPositionMF c WITH(READPAST) ON c.PosID = a.PosID " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@EmployeeName,'') = '' THEN 1 ELSE 0 END) OR a.EmployeeName LIKE '%' + @EmployeeName + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@DeptID,'') = '' THEN 1 ELSE 0 END) OR a.DeptID=@DeptID) " +
                                                "       AND (1=(CASE WHEN ISNULL(@PosID,'') = '' THEN 1 ELSE 0 END) OR a.PosID=@PosID)", connection);
                command.Parameters.AddWithValue("@EmployeeName", entity.EmployeeName);
                command.Parameters.AddWithValue("@DeptID", entity.DeptID);
                command.Parameters.AddWithValue("@PosID", entity.PosID);
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

        public override string Update(EmployeeModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT EmployeeID FROM TblEmployeeMF WITH(READPAST) WHERE EmployeeName=@EmployeeName AND EmployeeID!=@EmployeeID) " +
                                                           $"BEGIN " +
                                                           $"  UPDATE TblEmployeeMF SET EmployeeName=@EmployeeName, LName=@LName, FName=@FName, MName=@MName, " +
                                                           $"           DateOfBirth=@DateOfBirth, Gender=@Gender, DateHired=@DateHired, EmploymentStatus=@EmploymentStatus, " +
                                                           $"           Remarks=@Remarks, DeptID=@DeptID, PosID=@PosID, BsbAppUserName=@BsbAppUserName, " +
                                                           $"           ModifiedBy=@CreatedBy, ModifiedDt=GETDATE(), DateInactive=CASE WHEN @EmploymentStatus > 3 THEN GETDATE() ELSE NULL END " +
                                                           $"      WHERE EmployeeID=@EmployeeID " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@EmployeeID", entity.EmployeeID);
                command.Parameters.AddWithValue("@EmployeeName", entity.FName + " " + entity.MName + " " + entity.LName);
                command.Parameters.AddWithValue("@LName", entity.LName);
                command.Parameters.AddWithValue("@FName", entity.FName);
                command.Parameters.AddWithValue("@MName", entity.MName);
                command.Parameters.AddWithValue("@DateOfBirth", entity.DateOfBirth);
                command.Parameters.AddWithValue("@Gender", entity.Gender);
                command.Parameters.AddWithValue("@DateHired", entity.DateHired);
                command.Parameters.AddWithValue("@EmploymentStatus", entity.EmploymentStatus);
                command.Parameters.AddWithValue("@Remarks", entity.Remarks);
                command.Parameters.AddWithValue("@DeptID", entity.DeptID);
                command.Parameters.AddWithValue("@PosID", entity.PosID);
                command.Parameters.AddWithValue("@BsbAppUserName", entity.BsbUsername);
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
                Helper.TranLog("Employee Master", "Modified Employee:" + entity.EmployeeID, connection, command, transaction);
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

        public SortedDictionary<string, string> GetDictionary(string Type)
        {
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();

            try
            {
                connection.Open();

                if (Type == "Dept")
                {
                    command = Connection.setCommand($"SELECT DeptName, DeptID " +
                                                    $"  FROM TblDepartmentMF WITH(READPAST)", connection);
                }
                else if (Type == "Position")
                {
                    command = Connection.setCommand($"SELECT PosName, PosID " +
                                                    $"  FROM TblPositionMF WITH(READPAST)", connection);
                }
                reader = command.ExecuteReader();

                dictionary.Add("", "");
                while (reader.Read())
                {
                    string dictionaryKey = reader.GetString(0).TrimEnd();
                    string dictionaryValue = reader.GetString(1).TrimEnd();

                    dictionary.Add(dictionaryKey, dictionaryValue);
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

        public SortedDictionary<string, int> GetStatuses()
        {
            SortedDictionary<string, int> dictionary = new SortedDictionary<string, int>();

            try
            {
                dictionary.Add("", 0);
                dictionary.Add("CASUAL", 1);
                dictionary.Add("REGULAR", 2);
                dictionary.Add("CONSULTANT", 3);
                dictionary.Add("RESIGNED", 4);
                dictionary.Add("RETIRED", 5);
                dictionary.Add("TERMINATED", 6);
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
    }
}
