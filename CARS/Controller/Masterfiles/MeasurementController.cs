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
    internal class MeasurementController : Universal<MeasurementModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        public override string Create(MeasurementModel entity)
        {
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT uniqueid FROM TblPartsUomMF WITH(READPAST) WHERE UomID=@UomID OR UomName=@UomName) " +
                                                           $"BEGIN " +
                                                           $"  INSERT INTO TblPartsUomMF(uniqueid, UomID, UomName, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, IsActive) " +
                                                           $"      VALUES(@uniqueid, @UomID, @UomName, @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), @IsActive)" +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@uniqueid", entity.uniqueid);
                command.Parameters.AddWithValue("@UomID", entity.UomID);
                command.Parameters.AddWithValue("@UomName", entity.UomName);
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
                Helper.TranLog("Measurement Master", "Added a new Measurement:" + entity.UomID, connection, command, transaction);
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

        public override void Delete(MeasurementModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Read(MeasurementModel entity)
        {
            throw new NotImplementedException();
        }

        public void Read(PartsModel entity, string selection, string search)
        {
            switch (selection)
            {
                case "BrandID":
                    command = Connection.setCommand($"SELECT * FROM TblPartsUomMF WITH(READPAST) WHERE {selection} = @param", connection);
                    command.Parameters.AddWithValue("@param", entity.Description);
                    command.Prepare();
                    break;

                case "BrandName":
                    break;

                default:
                    command = Connection.setCommand($"SELECT * FROM TblPartsBrandMF WITH(READPAST)", connection);
                    break;
            }
            throw new NotImplementedException();
        }

        public override DataTable dt(MeasurementModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT uniqueid, UomID, RTRIM(UomName) AS UomName, IsActive " +
                                                "   FROM TblPartsUomMF WITH(READPAST)" +
                                                "   WHERE (1=(CASE WHEN ISNULL(@UomName,'') = '' THEN 1 ELSE 0 END) OR UomName LIKE '%' + @UomName + '%') " +
                                                "   ORDER BY UomName", connection);
                command.Parameters.AddWithValue("@UomName", entity.UomName);
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

        public override string Update(MeasurementModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT uniqueid FROM TblPartsUomMF WITH(READPAST) WHERE (UomID=@UomID OR UomName=@UomName) AND uniqueid!=@uniqueid) " +
                                                           $"BEGIN " +
                                                           $"  UPDATE TblPartsUomMF SET UomID=@UomID, UomName=@UomName, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE(), IsActive=@IsActive " +
                                                           $"      WHERE uniqueid=@uniqueid " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@uniqueid", entity.uniqueid);
                command.Parameters.AddWithValue("@UomID", entity.UomID);
                command.Parameters.AddWithValue("@UomName", entity.UomName);
                command.Parameters.AddWithValue("@IsActive", entity.IsActive);
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
                Helper.TranLog("Measurement Master", "Modified Measurement:" + entity.UomID, connection, command, transaction);
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
