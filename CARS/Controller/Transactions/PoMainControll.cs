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
    internal class PoMainControll : Universal<PoMainModel>
    {
        private static SqlConnection conn = Connection.GetConnection();
        private static SqlCommand cmd = null;
        private SqlDataReader rd = null;

        private static List<dynamic> pomainList = new List<dynamic>();

        public override string Create(PoMainModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(PoMainModel entity)
        {
            throw new NotImplementedException();
        }

        public override DataTable dt(PoMainModel entity)
        {
            throw new NotImplementedException();
        }

        public PoMainModel poMain()
        {
            PoMainModel poMain = new PoMainModel();
            try
            {
                conn.Open();
                cmd = Connection.setCommand("SELECT * FROM TblPoMain WITH (READPAST)", conn);
                rd = cmd.ExecuteReader();
                while(rd.Read()) 
                {
                    poMain.PoNo = rd.GetString(1).TrimEnd();
                    poMain.PoDt = rd.GetString(2).TrimEnd();
                    poMain.SuppId = rd.GetString(3).TrimEnd();
                    poMain.TermId = rd.GetString(4).TrimEnd();
                    poMain.PoMainStatus = rd.GetInt32(5);
                    poMain.PoType = rd.GetInt32(6);
                    poMain.CreatedBy = rd.GetString(7).TrimEnd();
                    poMain.CreatedDt = rd.GetString(8).TrimEnd();
                    poMain.ModifiedBy = rd.GetString(9).TrimEnd();
                    poMain.ModifiedDt = rd.GetString(10).TrimEnd();
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            finally 
            {
                conn.Close();
            }
            return poMain;
        }

        public override void Read(PoMainModel entity)
        {
            throw new NotImplementedException();
        }

        public override string Update(PoMainModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
