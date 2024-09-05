using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class dbConnect
    {
        private SqlConnection conn;

        public dbConnect()
        {
            //Initialize the connection with the DB using the connection Query 
            conn = new SqlConnection("Data Source=DESKTOP-SE5084M\\SQLEXPRESS;Initial Catalog=CarRentalDB;Integrated Security=True");
        }

        // Open conneciton methode
        public void OpenConnection()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        //Close connection methode
        public void CloseConnection()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }

        // Get connection methode
        public SqlConnection GetConnection()
        {
            return conn;
        }
    }
}