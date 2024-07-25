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
    internal class OemController : Universal<OemModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(OemModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT UniqueID FROM TblPartsOemMain WITH(READPAST) WHERE OemNo=@OemNo) " +
                                                           $"BEGIN " +
                                                           $"   INSERT INTO TblPartsOemMain(UniqueID, OemNo, MakeID, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, BOwn, IsActive) " +
                                                           $"       VALUES(@UniqueID, @OemNo, @MakeID, @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), 0, @IsActive) " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@UniqueID", entity.UniqueID);
                command.Parameters.AddWithValue("@OemNo", entity.OemNo);
                command.Parameters.AddWithValue("@MakeID", entity.MakeID);
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

                if (entity.DetailsList != null)
                {
                    int j = entity.DetailsList.Count();
                    foreach (var data in entity.DetailsList)
                    {
                        int k = 0;
                        command = Connection.setTransactionCommand($"INSERT INTO TblPartsOemParts(ParentID, PartNo, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, BOwn, IsActive) " +
                                                                   $"  VALUES(@ParentID, @PartNo, @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), 0, @IsActive)", connection, transaction);
                        command.Parameters.AddWithValue("@ParentID", entity.UniqueID);
                        command.Parameters.AddWithValue("@PartNo", data.PartNo);
                        command.Parameters.AddWithValue("@CreatedBy", Name01);
                        command.Parameters.AddWithValue("@IsActive", data.IsActive);
                        k = command.ExecuteNonQuery();
                        if (k > 0)
                        {
                            Helper.TranLog("OEM Masterfile", "Added new Part:" + data.PartNo + " to Oem:" + entity.OemNo, connection, command, transaction);
                        }
                        else
                        {
                            message = "Something went right, maybe? Are you sure? You lose your 50/50 next time.";
                            transaction.Rollback();
                            connection.Close();
                            return message;
                        }
                    }
                }
                Helper.TranLog("OEM Master", "Created new OEM:" + entity.OemNo, connection, command, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                message = ex.Message;
                transaction.Rollback();
                connection.Close();
                Console.WriteLine(ex.Message);
            }
            finally
            {
                transaction.Dispose();
                connection.Close();
            }
            return message;
        }

        public override void Delete(OemModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(OemModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(a.UniqueID) AS UniqueID, RTRIM(a.OemNo) AS OemNo, RTRIM(b.MakeName) AS MakeName, a.BOwn, a.IsActive, " +
                                                "       RTRIM(a.MakeID) AS MakeID " +
                                                "   FROM TblPartsOemMain a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsVehicleMakeMF b WITH(READPAST) ON b.MakeID = a.MakeID" +
                                                "   WHERE (1=(CASE WHEN ISNULL(@OemNo,'') = '' THEN 1 ELSE 0 END) OR a.OemNo LIKE '%' + @OemNo + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@MakeID,'') = '' THEN 1 ELSE 0 END) OR a.MakeID LIKE '%' + @MakeID + '%') " +
                                                "   ORDER BY a.OemNo ", connection);
                command.Parameters.AddWithValue("@OemNo", entity.OemNo);
                command.Parameters.AddWithValue("@MakeID", entity.MakeID);
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

        public DataTable dt(OemPartModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(PartNo) AS PartNo, IsActive AS IsActivePart, 0 AS IsNew " +
                                                "   FROM TblPartsOemParts WITH(READPAST) " +
                                                "   WHERE ParentID=@ParentID", connection);
                command.Parameters.AddWithValue("@ParentID", entity.ParentID);
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

        public DataTable dt(PartsModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(a.PartNo) AS PartNo, RTRIM(a.PartName) AS PartName, RTRIM(a.OtherName) AS OtherName, " +
                                                "       RTRIM(c.DescName) AS DescName, RTRIM(b.BrandName) AS BrandName  " +
                                                "   FROM TblPartsMainMF a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsBrandMF b WITH(READPAST) ON b.BrandID = a.BrandID " +
                                                "   LEFT JOIN TblPartsDescriptionMF c WITH(READPAST) ON c.DescID = a.DescID " +
                                                "   WHERE a.IsActive = 1", connection);
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

        public string GetPartImage(string PartNo)
        {
            string imageString = "";
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT PartImg " +
                                                "   FROM TblPartsMainMF WITH(READPAST) " +
                                                "   WHERE PartNo=@PartNo", connection);
                command.Parameters.AddWithValue("@PartNo", PartNo);
                imageString = command.ExecuteScalar().ToString() ?? "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return imageString;
        }

        public override void Read(OemModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(OemModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT UniqueID FROM TblPartsOemMain WITH(READPAST) WHERE OemNo=@OemNo AND UniqueID!=@UniqueID) " +
                                                           $"BEGIN " +
                                                           $"   UPDATE TblPartsOemMain " +
                                                           $"       SET OemNo=@OemNo, MakeID=@MakeID, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE(), IsActive=@IsActive " +
                                                           $"       WHERE UniqueID=@UniqueID " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@UniqueID", entity.UniqueID);
                command.Parameters.AddWithValue("@OemNo", entity.OemNo);
                command.Parameters.AddWithValue("@MakeID", entity.MakeID);
                command.Parameters.AddWithValue("@CreatedBy", Name01);
                command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                int i = command.ExecuteNonQuery();
                if (i != 1)
                {
                    message = "Something went right, maybe? Are you sure? You lose your 50/50 next time.";
                    transaction.Rollback();
                    transaction.Dispose();
                    connection.Close();
                    return message;
                }

                if (entity.DetailsList != null)
                {
                    int j = entity.DetailsList.Count();
                    foreach (var data in entity.DetailsList)
                    {
                        int k = 0;
                        if (data.IsNew)
                        {
                            command = Connection.setTransactionCommand($"INSERT INTO TblPartsOemParts(ParentID, PartNo, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, BOwn, IsActive) " +
                                                                       $"  VALUES(@ParentID, @PartNo, @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), 0, @IsActive)", connection, transaction);
                        }
                        else
                        {
                            command = Connection.setTransactionCommand($"UPDATE TblPartsOemParts " +
                                                                       $"  SET IsActive=@IsActive, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                                       $"  WHERE ParentID=@ParentID AND PartNo=@PartNo", connection, transaction);
                        }
                        command.Parameters.AddWithValue("@ParentID", entity.UniqueID);
                        command.Parameters.AddWithValue("@PartNo", data.PartNo);
                        command.Parameters.AddWithValue("@CreatedBy", Name01);
                        command.Parameters.AddWithValue("@IsActive", data.IsActive);
                        k = command.ExecuteNonQuery();
                        if (k > 0)
                        {
                            if (data.IsNew)
                            {
                                Helper.TranLog("OEM Masterfile", "Added new Part:" + data.PartNo + " to Oem:" + entity.OemNo, connection, command, transaction);
                            }
                            else
                            {
                                Helper.TranLog("OEM Masterfile", "Modified Part:" + data.PartNo + " of Oem:" + entity.OemNo, connection, command, transaction);
                            }
                        }
                        else
                        {
                            message = "Something went right, maybe? Are you sure? You lose your 50/50 next time.";
                            transaction.Rollback();
                            connection.Close();
                            return message;
                        }
                    }
                }
                Helper.TranLog("OEM Master", "Modified OEM:" + entity.OemNo, connection, command, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                message = ex.Message;
                transaction.Rollback();
                connection.Close();
                Console.WriteLine(ex.Message);
            }
            finally
            {
                transaction.Dispose();
                connection.Close();
            }
            return message;
        }

        public SortedDictionary<string, string> GetDictionary()
        {
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();

            try
            {
                connection.Open();

                command = Connection.setCommand($"SELECT MakeID, MakeName FROM TblPartsVehicleMakeMF WITH(READPAST)", connection);
                reader = command.ExecuteReader();

                dictionary.Add("", "");
                while (reader.Read())
                {
                    VehicleMakeModel model = new VehicleMakeModel();

                    model.MakeID = reader.GetString(0).TrimEnd();
                    model.MakeName = reader.GetString(1).TrimEnd();

                    dictionary.Add(model.MakeName, model.MakeID);
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
    }
}
