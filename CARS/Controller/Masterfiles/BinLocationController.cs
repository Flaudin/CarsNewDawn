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
    internal class BinLocationController : Universal<BinLocationModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(BinLocationModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT BinID FROM TblWHLocationMF WITH(READPAST) WHERE BinID=@BinID) " +
                                                           $"BEGIN " +
                                                           $"  INSERT INTO TblWHLocationMF(BinID, BinName, WhID, BinDesc, BinLength, BinHeight, BinWidth, BinArea, " +
                                                           $"      CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, IsActive) " +
                                                           $"  VALUES(@BinID, @BinName, @WhID, @BinDesc, @BinLength, @BinHeight, @BinWidth, @BinArea, " +
                                                           $"      @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), @IsActive) " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@WhID", entity.WhID);
                command.Parameters.AddWithValue("@BinID", entity.BinID);
                command.Parameters.AddWithValue("@BinName", entity.BinName);
                command.Parameters.AddWithValue("@BinDesc", entity.BinDesc);
                command.Parameters.AddWithValue("@BinLength", entity.BinLength);
                command.Parameters.AddWithValue("@BinHeight", entity.BinHeigth);
                command.Parameters.AddWithValue("@BinWidth", entity.BinWidth);
                command.Parameters.AddWithValue("@BinArea", entity.BinArea);
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
                Helper.TranLog("Bin Location Master", "Added a new Bin: " + entity.BinID + " to Warehouse:" + entity.WhID, connection, command, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                //message = ex.Message;
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

        public override void Delete(BinLocationModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(BinLocationModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(BinLocationModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT a.BinID, a.WhID, RTRIM(b.WhName) AS WhName, RTRIM(a.BinName) AS BinName, RTRIM(a.BinDesc) AS BinDesc, a.BinLength, " +
                                                "       a.BinHeight, a.BinWidth, a.BinArea, a.IsActive " +
                                                "   FROM TblWHLocationMF a WITH(READPAST) " +
                                                "   LEFT JOIN TblWarehouseMF b WITH(READPAST) ON b.WhID = a.WhID " +
                                                "   WHERE (1=(CASE WHEN ISNULL(@WhID,'') = '' THEN 1 ELSE 0 END) OR a.WhID=@WhID) " +
                                                "       AND (1=(CASE WHEN ISNULL(@BinName,'') = '' THEN 1 ELSE 0 END) OR a.BinName LIKE '%' + @BinName + '%') " +
                                                "   ORDER BY b.WhPriority, a.BinName", connection);
                command.Parameters.AddWithValue("@WhID", entity.WhID);
                command.Parameters.AddWithValue("@BinName", entity.BinName);
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

                command = Connection.setCommand($"SELECT RTRIM(WhName), WhID " +
                                                $"  FROM TblWarehouseMF WITH(READPAST)", connection);
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

        public string GenerateBinID()
        {
            string id = "";
            try
            {
                connection.Open();
                command = Connection.setCommand("DECLARE @binno varchar(10) = '' " +
                                                "DECLARE @date varchar(4) = ((SELECT FORMAT(GETDATE(), 'yy'))+(SELECT FORMAT(GETDATE(),'MM'))) " +
                                                "DECLARE @prefix varchar(10) = 'BE'+@date " +
                                                "DECLARE @countable int = (SELECT COUNT(*) FROM TblWHLocationMF WITH(READPAST)) " +
                                                "SET @binno = @prefix+'0001'; " +
                                                "IF @countable > 0 " +
                                                "   BEGIN " +
                                                "   SET @binno = (SELECT TOP 1 BinID FROM TblWHLocationMF WITH(READPAST) WHERE CAST(SUBSTRING(BinID,1,6) AS varchar) = @prefix ORDER BY BinID DESC) " +
                                                "   IF @binno IS NULL " +
                                                "       BEGIN " +
                                                "           SET @binno = @prefix+'0001'; " +
                                                "       END " +
                                                "   ELSE " +
                                                "       BEGIN " +
                                                "           SET @binno = (SELECT TOP 1 @prefix+ REPLICATE('0',4-LEN(SUBSTRING(BinID,7,4)+1)) + CAST(SUBSTRING(BinID,7,4)+1 AS varchar) FROM TblWHLocationMF WITH(READPAST) WHERE CAST(SUBSTRING(BinID,1,6) AS varchar) = @prefix ORDER BY BinID DESC) " +
                                                "       END " +
                                                "   END " +
                                                "SELECT @binno AS BinID", connection);
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

        public override string Update(BinLocationModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT BinID FROM TblWHLocationMF WITH(READPAST) WHERE BinName=@BinName AND BinID!=@BinID) " +
                                                           $"BEGIN " +
                                                           $"  UPDATE TblWHLocationMF " +
                                                           $"       SET BinName=@BinName, BinDesc=@BinDesc, BinLength=@BinLength, BinHeight=@BinHeight, BinWidth=@BinWidth, " +
                                                           $"           BinArea=@BinArea, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE(), IsActive=@IsActive " +
                                                           $"       WHERE BinID=@BinID " +
                                                           $"           AND WhID=@WhID " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@WhID", entity.WhID);
                command.Parameters.AddWithValue("@BinID", entity.BinID);
                command.Parameters.AddWithValue("@BinName", entity.BinName);
                command.Parameters.AddWithValue("@BinDesc", entity.BinDesc);
                command.Parameters.AddWithValue("@BinLength", entity.BinLength);
                command.Parameters.AddWithValue("@BinHeight", entity.BinHeigth);
                command.Parameters.AddWithValue("@BinWidth", entity.BinWidth);
                command.Parameters.AddWithValue("@BinArea", entity.BinArea);
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
                Helper.TranLog("Bin Location Master", "Modified Bin: " + entity.BinID + " of Warehouse:" + entity.WhID, connection, command, transaction);
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

        public bool CheckWarehouse(string WarehouseID)
        {
            bool IsWebStore = false;

            try
            {
                connection.Open();

                command = Connection.setCommand($"SELECT IsWebStore " +
                                                $"  FROM TblWarehouseMF WITH(READPAST) " +
                                                $"  WHERE WhID=@WhID", connection);
                command.Parameters.AddWithValue("@WhID", WarehouseID);
                IsWebStore = Convert.ToBoolean(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsWebStore;
        }
    }
}
