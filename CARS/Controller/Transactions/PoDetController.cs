using CARS.Model;
using CARS.Model.Transactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Controller.Transactions
{
    internal class PoDetController : Universal<PoDetailsModel>
    {
        private static SqlConnection conn = Connection.GetConnection();
        private static SqlCommand cmd = null;
        private static SqlDataReader reader = null;

        private static List<dynamic> podetList = new List<dynamic>();
        public override string Create(PoDetailsModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(PoDetailsModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(PoDetailsModel entity)
        {
            throw new NotImplementedException();
        }

        public PoDetailsModel poDetails()
        {
            PoDetailsModel poDetailsModel = new PoDetailsModel();
            try 
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT * FROM TblPoDet WITH (READPAST)", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    poDetailsModel.PoNo = reader.GetString(1).TrimEnd();
                    poDetailsModel.PartNo = reader.GetString(2).TrimEnd();
                    poDetailsModel.DiscPrcnt = reader.GetInt32(3);
                    poDetailsModel.DiscAmt = reader.GetInt32(4);
                    poDetailsModel.UnitPrice = reader.GetInt32(5);
                    poDetailsModel.NetPrice = reader.GetInt32(6);
                    poDetailsModel.Qty = reader.GetInt32(7);
                    poDetailsModel.TotalAmt = reader.GetInt32(8);
                    poDetailsModel.DelivrdQty = reader.GetInt32(9);
                    poDetailsModel.PoDetStatus = reader.GetInt32(10);
                    poDetailsModel.CreatedBy = reader.GetString(11);
                    poDetailsModel.CreatedDt = reader.GetString(12);
                    poDetailsModel.ModifiedBy = reader.GetString(13);
                    poDetailsModel.ModifiedDt = reader.GetString(14);
                }
            }catch(Exception ex) { Console.WriteLine(ex.Message); }
            finally
            {
                conn.Close();
            }
            return poDetailsModel;
        }

        public override void Read(PoDetailsModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(PoDetailsModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
