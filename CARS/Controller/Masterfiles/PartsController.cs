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

namespace CARS.Controllers.Masterfiles
{
    internal class PartsController : Universal<PartsModel>
    {
        private static DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
        private static SqlConnection connection = Connection.GetConnection();
        private static SqlTransaction transaction = null;
        private static SqlCommand command = null;
        private static SqlDataReader reader = null;

        private static List<string> Searchable = new List<string>();

        public override string Create(PartsModel entity)
        {
            //Dictionary<string, dynamic> fields = new Dictionary<string, dynamic>();
            string message = "Information saved successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT PartNo FROM TblPartsMainMF WITH(READPAST) WHERE PartNo=@PartNo OR PartName=@PartName) " +
                                                           $"BEGIN " +
                                                           $"   INSERT INTO TblPartsMainMF(PartNo, BPartNo, PartName, UomID, BrandID, OtherName, Sku, IUpack, MUpack, PPosition, PSize, " +
                                                           $"           PType, ListPrice, BListPrice, PartApply, DescID, PartImg, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, IsActive) " +
                                                           $"       VALUES(@PartNo, @BPartNo, @PartName, @UomID, @BrandID, @OtherName, @Sku, @IUpack, @MUpack, @PPosition, @PSize, " +
                                                           $"           @PType, @ListPrice, @BListPrice, @PartApply, @DescID, @PartImg, @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), @IsActive) " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                command.Parameters.AddWithValue("@BPartNo", entity.BPartNo);
                command.Parameters.AddWithValue("@PartName", entity.PartName);
                command.Parameters.AddWithValue("@UomID", entity.Uom);
                command.Parameters.AddWithValue("@BrandID", entity.Brand);
                command.Parameters.AddWithValue("@OtherName", entity.OtherName);
                command.Parameters.AddWithValue("@Sku", entity.Sku);
                command.Parameters.AddWithValue("@IUpack", entity.IUpack);
                command.Parameters.AddWithValue("@MUpack", entity.MUpack);
                command.Parameters.AddWithValue("@PPosition", entity.PPosition);
                command.Parameters.AddWithValue("@PSize", entity.PSize);
                command.Parameters.AddWithValue("@PType", entity.Ptype);
                command.Parameters.AddWithValue("@ListPrice", entity.ListPrice);
                command.Parameters.AddWithValue("@BListPrice", entity.BListPrice);
                command.Parameters.AddWithValue("@PartApply", entity.PartApplication);
                command.Parameters.AddWithValue("@DescID", entity.Description);
                command.Parameters.AddWithValue("@PartImg", entity.Image);
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

                if (entity.AlternateList != null)
                {
                    foreach (var data in entity.AlternateList)
                    {
                        int k = 0;
                        command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT AltPartNo FROM TblPartsAlternatePart WITH(READPAST) WHERE PartNo=@PartNo AND AltPartNo=@AltPartNo) " +
                                                                   $"BEGIN " +
                                                                   $"   INSERT INTO TblPartsAlternatePart(PartNo, AltPartNo, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, BOwn, IsActive) " +
                                                                   $"       VALUES(@PartNo, @AltPartNo, @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), @BOwn, @IsActive) " +
                                                                   $"END", connection, transaction);
                        command.Parameters.AddWithValue("@PartNo", data.PartNo);
                        command.Parameters.AddWithValue("@AltPartNo", data.AltPartNo);
                        command.Parameters.AddWithValue("@CreatedBy", Name01);
                        command.Parameters.AddWithValue("@BOwn", data.BOwn);
                        command.Parameters.AddWithValue("@IsActive", data.IsActive);
                        k = command.ExecuteNonQuery();
                        if (k > 0)
                        {
                            Helper.TranLog("Parts Library", "Added new Alternate Part:" + data.AltPartNo + " to Part:" + entity.PartNo, connection, command, transaction);
                        }
                        else
                        {
                            message = "The alternate part entered is already present in the database.";
                            transaction.Rollback();
                            connection.Close();
                            return message;
                        }
                    }
                }
                Helper.TranLog("Parts Library", "Created new part:" + entity.PartNo, connection, command, transaction);
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

        public override void Read(PartsModel entity)
        {
            throw new NotImplementedException();
        }

        public PartsModel Read(string id)
        {
            PartsModel parts = null;
            try
            {
                parts = new PartsModel();
                connection.Open();
                command = Connection.setCommand($"SELECT ISNULL(BPartNo,'') AS BPartNo, PartName, UomID, BrandID, OtherName, Sku, IUpack, MUpack, PPosition, PSize, " +
                                                $"      PType, ListPrice, BListPrice, PartApply, DescID, ISNULL(PartImg, '') AS PartImg, IsActive " +
                                                $"  FROM TblPartsMainMF WITH(READPAST) " +
                                                $"  WHERE PartNo = @PartNo", connection);
                command.Parameters.AddWithValue("@PartNo", id);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    parts.BPartNo = reader.GetString(reader.GetOrdinal("BPartNo")).TrimEnd();
                    parts.PartName = reader.GetString(reader.GetOrdinal("PartName")).TrimEnd();
                    parts.Uom = reader.GetString(reader.GetOrdinal("UomID")).TrimEnd();
                    parts.Brand = reader.GetString(reader.GetOrdinal("BrandID")).TrimEnd();
                    parts.OtherName = reader.GetString(reader.GetOrdinal("OtherName")).TrimEnd();
                    parts.Sku = reader.GetString(reader.GetOrdinal("Sku")).TrimEnd();
                    parts.IUpack = reader.GetDecimal(reader.GetOrdinal("IUpack"));
                    parts.MUpack = reader.GetDecimal(reader.GetOrdinal("MUpack"));
                    parts.PPosition = reader.GetString(reader.GetOrdinal("PPosition")).TrimEnd();
                    parts.PSize = reader.GetString(reader.GetOrdinal("PSize")).TrimEnd();
                    parts.Ptype = reader.GetString(reader.GetOrdinal("Ptype")).TrimEnd();
                    parts.ListPrice = reader.GetDecimal(reader.GetOrdinal("ListPrice"));
                    parts.BListPrice = reader.GetDecimal(reader.GetOrdinal("BListPrice"));
                    parts.PartApplication = reader.GetString(reader.GetOrdinal("PartApply")).TrimEnd();
                    parts.Description = reader.GetString(reader.GetOrdinal("DescID")).TrimEnd();
                    parts.Image = reader.GetString(reader.GetOrdinal("PartImg"));
                    parts.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
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

            return parts;
        }

        public SortedDictionary<string, string> GetDictionary(string Type)
        {
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();

            try
            {
                connection.Open();

                if (Type == "Uom")
                {
                    command = Connection.setCommand($"SELECT UomName, UomID " +
                                                    $"  FROM TblPartsUomMF WITH(READPAST)", connection);
                }
                else if (Type == "Brand")
                {
                    command = Connection.setCommand($"SELECT BrandName, BrandID " +
                                                    $"  FROM TblPartsBrandMF WITH(READPAST)", connection);
                }
                else
                {
                    command = Connection.setCommand($"SELECT (RTRIM(DescName) + ' (' + RTRIM(DescSku) + ')') AS DescName, DescID " +
                                                    $"  FROM TblPartsDescriptionMF WITH(READPAST)", connection);
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

        public string GeneratePartID()
        {
            string id = "";
            try
            {
                connection.Open();
                command = Connection.setCommand("DECLARE @partno varchar(10) = '' " +
                                                "DECLARE @date varchar(4) = ((SELECT FORMAT(GETDATE(), 'yy'))+(SELECT FORMAT(GETDATE(),'MM'))) " +
                                                "DECLARE @prefix varchar(10) = 'PE'+@date " +
                                                "DECLARE @countable int = (SELECT COUNT(*) FROM TblPartsMainMF WITH(READPAST)) " +
                                                "SET @partno = @prefix+'0001'; " +
                                                "IF @countable > 0 " +
                                                "   BEGIN " +
                                                "   SET @partno = (SELECT TOP 1 PartNo FROM TblPartsMainMF WITH(READPAST) WHERE CAST(SUBSTRING(PartNo,1,6) AS varchar) = @prefix ORDER BY PartNo DESC) " +
                                                "   IF @partno IS NULL " +
                                                "       BEGIN " +
                                                "           SET @partno = @prefix+'0001'; " +
                                                "       END " +
                                                "   ELSE " +
                                                "       BEGIN " +
                                                "           SET @partno = (SELECT TOP 1 @prefix+ REPLICATE('0',4-LEN(SUBSTRING(PartNo,7,4)+1)) + CAST(SUBSTRING(PartNo,7,4)+1 AS varchar) FROM TblPartsMainMF WITH(READPAST) WHERE CAST(SUBSTRING(PartNo,1,6) AS varchar) = @prefix ORDER BY PartNo DESC) " +
                                                "       END " +
                                                "   END " +
                                                "SELECT @partno AS PartNo", connection);
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

        public override string Update(PartsModel entity)
        {
            string message = "Information updated successfully";
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command = Connection.setTransactionCommand($"IF (((SELECT PartNo FROM TblPartsMainMF WITH(READPAST) WHERE PartName=@PartName) = @PartNo) OR ((SELECT PartNo FROM TblPartsMainMF WITH(READPAST) WHERE PartName=@PartName) IS NULL)) " +
                                                           $"BEGIN " +
                                                           $"   UPDATE TblPartsMainMF " +
                                                           $"       SET BPartNo=@BPartNo, PartName=@PartName, UomID=@UomID, BrandID=@BrandID, OtherName=@OtherName, Sku=@Sku, IUpack=@IUpack, " +
                                                           $"           MUpack=@MUpack, PPosition=@PPosition, PSize=@PSize, PType=@PType, BListPrice=@BListPrice, " +
                                                           $"           PartApply=@PartApply, DescID=@DescID, PartImg = CASE WHEN @BPartNo != '' THEN PartImg ELSE @PartImg END, " +
                                                           $"           ModifiedBy=@CreatedBy,ModifiedDt=GETDATE(), IsActive=@IsActive " +
                                                           $"       WHERE PartNo=@PartNo " +
                                                           $"END", connection, transaction);
                command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                command.Parameters.AddWithValue("@BPartNo", entity.BPartNo);
                command.Parameters.AddWithValue("@PartName", entity.PartName);
                if (entity.Uom == null)
                {
                    command.Parameters.AddWithValue("@UomID", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@UomID", entity.Uom);
                }
                if (entity.Brand == null)
                {
                    command.Parameters.AddWithValue("@BrandID", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@BrandID", entity.Brand);
                }
                command.Parameters.AddWithValue("@OtherName", entity.OtherName);
                command.Parameters.AddWithValue("@Sku", entity.Sku);
                command.Parameters.AddWithValue("@IUpack", entity.IUpack);
                command.Parameters.AddWithValue("@MUpack", entity.MUpack);
                command.Parameters.AddWithValue("@PPosition", entity.PPosition);
                command.Parameters.AddWithValue("@PSize", entity.PSize);
                command.Parameters.AddWithValue("@PType", entity.Ptype);
                command.Parameters.AddWithValue("@BListPrice", entity.BListPrice);
                command.Parameters.AddWithValue("@PartApply", entity.PartApplication);
                if (entity.Description == null)
                {
                    command.Parameters.AddWithValue("@DescID", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@DescID", entity.Description);
                }
                command.Parameters.AddWithValue("@PartImg", entity.Image);
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

                if (entity.AlternateList != null)
                {
                    foreach (var data in entity.AlternateList)
                    {
                        int k = 0;
                        if (data.IsNew)
                        {
                            command = Connection.setTransactionCommand($"IF NOT EXISTS(SELECT AltPartNo FROM TblPartsAlternatePart WITH(READPAST) WHERE PartNo=@PartNo AND AltPartNo=@AltPartNo) " +
                                                                       $"BEGIN " +
                                                                       $"   INSERT INTO TblPartsAlternatePart(PartNo, AltPartNo, CreatedBy, CreatedDt, ModifiedBy, ModifiedDt, BOwn, IsActive) " +
                                                                       $"       VALUES(@PartNo, @AltPartNo, @CreatedBy, GETDATE(), @CreatedBy, GETDATE(), @BOwn, @IsActive) " +
                                                                       $"END", connection, transaction);
                        }
                        else
                        {
                            command = Connection.setTransactionCommand($"UPDATE TblPartsAlternatePart " +
                                                                       $"  SET BOwn=@BOwn, IsActive=@IsActive, ModifiedBy=@CreatedBy, ModifiedDt=GETDATE() " +
                                                                       $"  WHERE PartNo=@PartNo AND AltPartNo=@AltPartNo", connection, transaction);
                        }
                        command.Parameters.AddWithValue("@PartNo", data.PartNo);
                        command.Parameters.AddWithValue("@AltPartNo", data.AltPartNo);
                        command.Parameters.AddWithValue("@CreatedBy", Name01);
                        command.Parameters.AddWithValue("@BOwn", data.BOwn);
                        command.Parameters.AddWithValue("@IsActive", data.IsActive);
                        k = command.ExecuteNonQuery();
                        if (k > 0)
                        {
                            if (data.IsNew)
                            {
                                Helper.TranLog("Parts Library", "Added new Alternate Part:" + data.AltPartNo + " to Part:" + entity.PartNo, connection, command, transaction);
                            }
                            else
                            {
                                Helper.TranLog("Parts Library", "Modified Alternate Part:" + data.AltPartNo + " of Part:" + entity.PartNo, connection, command, transaction);
                            }
                        }
                        else
                        {
                            if (data.IsNew)
                            {
                                message = "The alternate part entered is already present in the database.";
                            }
                            else
                            {
                                message = "Something went right, maybe? Are you sure? You lose your 50/50 next time.";
                            }
                            transaction.Rollback();
                            connection.Close();
                            return message;
                        }
                    }
                }
                Helper.TranLog("Parts Library", "Modified part:" + entity.PartNo, connection, command, transaction);
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

        public override void Delete(PartsModel entity)
        {
            throw new NotImplementedException();
        }

        public List<string> GetList()
        {
            List<string> list = new List<string>();

            try
            {
                connection.Open();

                command = Connection.setCommand($"SELECT * FROM TblPartsBrandMF WITH(READPAST)", connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BrandModel model = new BrandModel();

                    model.BrandName = reader.GetString(2);

                    list.Add(model.BrandName);
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

            return list;
        }

        public override DataTable dt(PartsModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(a.PartNo) AS PartNo, RTRIM(a.PartName) AS PartName, RTRIM(a.OtherName) AS OtherName, " +
                                                "       RTRIM(ISNULL(d.DescName,'')) AS DescName, RTRIM(a.Sku) AS Sku, RTRIM(b.UomName) AS UomName, " +
                                                "       RTRIM(c.BrandName) AS BrandName, RTRIM(a.OemNo) AS OemNo, RTRIM(a.PartApply) AS PartApply, " +
                                                "       a.ListPrice, a.IsActive " +
                                                "   FROM TblPartsMainMF a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsUomMF b WITH(READPAST) ON b.UomID = a.UomID " +
                                                "   LEFT JOIN TblPartsBrandMF c WITH(READPAST) ON c.BrandID = a.BrandID " +
                                                "   LEFT JOIN TblPartsDescriptionMF d WITH(READPAST) ON d.DescID = a.DescID" +
                                                "   WHERE (1=(CASE WHEN ISNULL(@PartNo,'') = '' THEN 1 ELSE 0 END) OR a.PartNo LIKE '%' + @PartNo + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@PartName,'') = '' THEN 1 ELSE 0 END) OR a.PartName LIKE '%' + @PartName + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@OtherName,'') = '' THEN 1 ELSE 0 END) OR a.OtherName LIKE '%' + @OtherName + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@BrandID,'') = '' THEN 1 ELSE 0 END) OR a.BrandID=@BrandID) " +
                                                "       AND (1=(CASE WHEN ISNULL(@UomID,'') = '' THEN 1 ELSE 0 END) OR a.UomID=@UomID) " +
                                                "       AND (1=(CASE WHEN ISNULL(@Desc,'') = '' THEN 1 ELSE 0 END) OR a.DescID LIKE '%' + @Desc + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@Sku,'') = '' THEN 1 ELSE 0 END) OR a.Sku LIKE '%' + @Sku + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@PartApply,'') = '' THEN 1 ELSE 0 END) OR a.PartApply LIKE '%' + @PartApply + '%') " +
                                                "       AND (1=(CASE WHEN ISNULL(@ListPrice,0) = 0 THEN 1 ELSE 0 END) OR a.ListPrice=@ListPrice) ", connection);
                command.Parameters.AddWithValue("@PartNo", entity.PartNo);
                command.Parameters.AddWithValue("@PartName", entity.PartName);
                command.Parameters.AddWithValue("@OtherName", entity.OtherName);
                command.Parameters.AddWithValue("@BrandID", entity.Brand);
                command.Parameters.AddWithValue("@UomID", entity.Uom);
                command.Parameters.AddWithValue("@Desc", entity.Description);
                command.Parameters.AddWithValue("@Sku", entity.Sku);
                command.Parameters.AddWithValue("@PartApply", entity.PartApplication);
                command.Parameters.AddWithValue("@ListPrice", entity.ListPrice);
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

        public DataTable dt(AlternatePartsModel entity)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT AltPartNo, BOwn, IsActive " +
                                                "   FROM TblPartsAlternatePart WITH(READPAST) " +
                                                "   WHERE PartNo=@PartNo ", connection);
                command.Parameters.AddWithValue("@PartNo", entity.PartNo);
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

        public DataTable dt(string Part)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                command = Connection.setCommand("SELECT RTRIM(b.OemNo) AS OemNo, RTRIM(c.MakeName) AS MakeName, a.BOwn AS BOwnOem, a.IsActive AS IsActiveOem " +
                                                "   FROM TblPartsOemParts a WITH(READPAST) " +
                                                "   LEFT JOIN TblPartsOemMain b ON b.UniqueID = a.ParentID " +
                                                "   LEFT JOIN TblPartsVehicleMakeMF c ON c.MakeID = b.MakeID " +
                                                "   WHERE PartNo = @PartNo", connection);
                command.Parameters.AddWithValue("@PartNo", Part);
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

        //public override Task<IAsyncResult> Asyncher()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
