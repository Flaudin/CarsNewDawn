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
    internal class WarehouseController : Universal<WarehouseModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;
        public override string Create(WarehouseModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT WhID FROM TblWarehouseMF WITH(READPAST) WHERE WhID=@WhID) " +
                                                           $"BEGIN " +
                                                           $"  INSERT INTO TblWarehouseMF(WhID, WhName, WhDesc, WhLocation, AreaSqm, StorageSqm, WhPriority, WhInCharge, " +
                                                           $"           CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, IsActive, IsWebStore) " +
                                                           $"       VALUES(@WhID, @WhName, @WhDesc, @WhLocation, @AreaSqm, @StorageSqm, @WhPriority, @WhInCharge, " +
                                                           $"           @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), @IsActive, @IsWebStore) " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@WhID", entity.WhID);
                command.Parameters.AddWithValue("@WhName", entity.WhName);
                command.Parameters.AddWithValue("@WhDesc", entity.WhDesc);
                command.Parameters.AddWithValue("@WhLocation", entity.WhLocation);
                command.Parameters.AddWithValue("@AreaSqm", entity.AreaSqm);
                command.Parameters.AddWithValue("@StorageSqm", entity.StorageSqm);
                command.Parameters.AddWithValue("@WhPriority", entity.WhPriority);
                if (entity.WhInCharge == "" || entity.WhInCharge == null)
                {
                    command.Parameters.AddWithValue("@WhInCharge", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@WhInCharge", entity.WhInCharge);
                }
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                command.Parameters.AddWithValue("@IsWebStore", entity.IsWebStore);
                int i = command.ExecuteNonQuery();
                if (i != 1)
                {
                    message = "The information entered is already present in the database.";
                    transaction.Rollback();
                    transaction.Dispose();
                    connection.Close();
                    return message;
                }
                Helper.TranLog("Warehouse Master", "Added a new Warehouse:" + entity.WhID, connection, command, transaction);
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

        public override void Delete(WarehouseModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(WarehouseModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(WarehouseModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.WhID, RTRIM(a.WhName) AS WhName, RTRIM(a.WhDesc) AS WhDesc, RTRIM(a.WhLocation) AS WhLocation, a.AreaSqm, a.StorageSqm, " +
                                                "       a.WhPriority, b.EmployeeName, a.IsActive, ISNULL(a.IsWebStore, 0) AS IsWebStore " +
                                                "   FROM TblWarehouseMF a WITH(READPAST) " +
                                                "   LEFT JOIN TblEmployeeMF b ON b.EmployeeID = a.WhInCharge " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@WhName,'') = '' THEN 1 ELSE 0 END) OR WhName LIKE '%' + @WhName + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@WhDesc,'') = '' THEN 1 ELSE 0 END) OR WhDesc LIKE '%' + @WhDesc + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@WhLocation,'') = '' THEN 1 ELSE 0 END) OR WhLocation LIKE '%' + @WhLocation + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@WhInCharge,'') = '' THEN 1 ELSE 0 END) OR WhInCharge=@WhInCharge) " +
                                                "   ORDER BY a.WhPriority ", connection);
                command.Parameters.AddWithValue("@WhName", entity.WhName);
                command.Parameters.AddWithValue("@WhDesc", entity.WhDesc);
                command.Parameters.AddWithValue("@WhLocation", entity.WhLocation);
                command.Parameters.AddWithValue("@WhInCharge", entity.WhInCharge);
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

        public SortedDictionary<string, string> GetDictionary()
        {
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();

            try
            {
                connection.Open();

                command = Connection.setCommand($"SELECT RTRIM(EmployeeName), EmployeeID " +
                                                $"  FROM TblEmployeeMF WITH(READPAST) " +
                                                $"  WHERE PosID!='auUlQWtNXY'", connection);
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

        public string GenerateWarehouseID()
        {
            string id = "";
            try
            {
                connection.Open();
                command = Connection.setCommand("DECLARE @warehouseno varchar(10) = '' " +
                                                "DECLARE @date varchar(4) = ((SELECT FORMAT(GETDATE(), 'yy'))+(SELECT FORMAT(GETDATE(),'MM'))) " +
                                                "DECLARE @prefix varchar(10) = 'WH'+@date " +
                                                "DECLARE @countable int = (SELECT COUNT(*) FROM TblWarehouseMF WITH(READPAST)) " +
                                                "SET @warehouseno = @prefix+'0001'; " +
                                                "IF @countable > 0 " +
                                                "   BEGIN " +
                                                "   SET @warehouseno = (SELECT TOP 1 WhID FROM TblWarehouseMF WITH(READPAST) WHERE CAST(SUBSTRING(WhID,1,6) AS varchar) = @prefix ORDER BY WhID DESC) " +
                                                "   IF @warehouseno IS NULL " +
                                                "       BEGIN " +
                                                "           SET @warehouseno = @prefix+'0001'; " +
                                                "       END " +
                                                "   ELSE " +
                                                "       BEGIN " +
                                                "           SET @warehouseno = (SELECT TOP 1 @prefix+ REPLICATE('0',4-LEN(SUBSTRING(WhID,7,4)+1)) + CAST(SUBSTRING(WhID,7,4)+1 AS varchar) FROM TblWarehouseMF WITH(READPAST) WHERE CAST(SUBSTRING(WhID,1,6) AS varchar) = @prefix ORDER BY WhID DESC) " +
                                                "       END " +
                                                "   END " +
                                                "SELECT @warehouseno AS WhID", connection);
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

        public override string Update(WarehouseModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT WhID FROM TblWarehouseMF WITH(READPAST) WHERE WhName=@WhName AND WhID!=@WhID) " +
                                                           $"BEGIN " +
                                                           $"  UPDATE TblWarehouseMF " +
                                                           $"       SET WhName=@WhName, WhDesc=@WhDesc, WhLocation=@WhLocation, AreaSqm=@AreaSqm, StorageSqm=@StorageSqm, " +
                                                           $"           WhPriority=@WhPriority, WhInCharge=@WhInCharge, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE(), IsActive=@IsActive " +
                                                           $"       WHERE WhID=@WhID " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@WhID", entity.WhID);
                command.Parameters.AddWithValue("@WhName", entity.WhName);
                command.Parameters.AddWithValue("@WhDesc", entity.WhDesc);
                command.Parameters.AddWithValue("@WhLocation", entity.WhLocation);
                command.Parameters.AddWithValue("@AreaSqm", entity.AreaSqm);
                command.Parameters.AddWithValue("@StorageSqm", entity.StorageSqm);
                command.Parameters.AddWithValue("@WhPriority", entity.WhPriority);
                command.Parameters.AddWithValue("@WhInCharge", entity.WhInCharge);
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
                Helper.TranLog("Warehouse Master", "Modified Warehouse:" + entity.WhID, connection, command, transaction);
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
