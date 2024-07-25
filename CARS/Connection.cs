using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS
{
    internal class Connection
    {
        public static DataTable dataTable;
        public static DataSet dataSet;
        public static SqlConnection connection;
        public static SqlDataAdapter adapter;
        public static SqlDataReader reader;
        public static SqlCommand command;

        private static string SERVER = "172.16.15.222,13740"; //CHQDWSQL08,13740 //172.16.15.222,13740
        //private static string PORT = "";
        //private static string USERNAME = "root";
        //private static string PASSWORD = "root";
        private static string DATABASE = "cars_am_new";

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = string.Format("Server={0}; Database={1};Integrated Security=True", SERVER, DATABASE);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return connection;
        }

        public static SqlCommand setCommand(String query, SqlConnection connection)
        {
            var sqlCommand = new SqlCommand(query, connection);

            return sqlCommand;
        }

        public static SqlCommand setTransactionCommand(String query, SqlConnection connection, SqlTransaction transaction)
        {
            var sqlCommand = new SqlCommand(query, connection, transaction);

            return sqlCommand;
        }

        public static DataTable GetDataTable(String query)
        {
            using (connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    dataTable = new DataTable();
                    using (adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.Fill(dataTable);
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
            }
            return dataTable;
        }

        public static DataSet GetDataSet(String query, String table)
        {
            using (connection = GetConnection())
            {
                connection.Open();

                dataSet = new DataSet();
                using (adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.Fill(dataSet, table);
                }
            }

            return dataSet;
        }
    }
}
