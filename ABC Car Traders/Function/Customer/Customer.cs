using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WindowsFormsApp1;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace ABC_Car_Traders.Function.Customer
{
    public class Customer
    {
        private dbConnect dbconnect = new dbConnect();

        public int GetCustomerIdByUserId(int userId)
        {
            int customerId = 0;

            try
            {
                dbconnect.OpenConnection();

                // Query to get the CustomerID using the UserID
                string query = "SELECT CustomerID FROM Customers WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, dbconnect.GetConnection());
                cmd.Parameters.AddWithValue("@UserID", userId);

                // Execute the query and get the CustomerID
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    customerId = Convert.ToInt32(reader["CustomerID"]);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting CustomerID: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return customerId;
        }


        public string GetCustomerAddress(int userId)
        {
            string address = string.Empty;

            try
            {
                dbconnect.OpenConnection();

                string query = "SELECT Address FROM Users WHERE UserID = @UserID"; 
                SqlCommand cmd = new SqlCommand(query, dbconnect.GetConnection());
                cmd.Parameters.AddWithValue("@UserID", userId);

                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    address = result.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting customer address: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return address;
        }


        // For the customer profile 
        public Dictionary<string, int> GetOrdersByCustomer(int customerId)
        {
            Dictionary<string, int> ordersData = new Dictionary<string, int>();

            try
            {
                dbconnect.OpenConnection();

                SqlCommand cmd = new SqlCommand("usp_GetOrdersByCustomer", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string status = reader.GetString(reader.GetOrdinal("Status"));
                    int orderCount = reader.GetInt32(reader.GetOrdinal("OrderCount"));
                    ordersData[status] = orderCount;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching orders data: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return ordersData;
        }


        // Fetch spending data by date for the customer
        public Dictionary<DateTime, decimal> GetSpendingByDate(int customerId)
        {
            Dictionary<DateTime, decimal> spendingData = new Dictionary<DateTime, decimal>();

            try
            {
                dbconnect.OpenConnection();

                SqlCommand cmd = new SqlCommand("usp_GetSpendingByDate", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime orderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate"));
                    decimal totalSpent = reader.GetDecimal(reader.GetOrdinal("TotalSpent"));
                    spendingData[orderDate] = totalSpent;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching spending by date: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return spendingData;
        }


        // Fetch spending data by products for the customer
        public Dictionary<string, decimal> GetSpendingByProducts(int customerId)
        {
            Dictionary<string, decimal> spendingData = new Dictionary<string, decimal>();

            try
            {
                dbconnect.OpenConnection();

                SqlCommand cmd = new SqlCommand("usp_GetSpendingByProducts", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string productName = reader.GetString(reader.GetOrdinal("ProductName"));
                    decimal totalSpent = reader.GetDecimal(reader.GetOrdinal("TotalSpent"));
                    spendingData[productName] = totalSpent;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching spending by products: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return spendingData;
        }

        // fetches this data and populates chartotalordersbyproducts
        public Dictionary<string, int> GetTotalOrdersByProducts(int customerId)
        {
            Dictionary<string, int> productOrderCounts = new Dictionary<string, int>();

            try
            {
                dbconnect.OpenConnection();

                SqlCommand cmd = new SqlCommand("usp_GetTotalOrdersByProducts", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string productName = reader["ProductName"].ToString();
                    int totalOrders = (int)reader["TotalOrders"];
                    productOrderCounts[productName] = totalOrders;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching total orders by products: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return productOrderCounts;
        }

        //execute the stored procedure and fetch the data from Points
        public Dictionary<DateTime, int> GetCustomerPointsByDate(int customerId)
        {
            Dictionary<DateTime, int> pointsData = new Dictionary<DateTime, int>();

            try
            {
                dbconnect.OpenConnection();
                SqlCommand cmd = new SqlCommand("usp_GetCustomerPointsByDate", dbconnect.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime date = (DateTime)reader["Date"];
                    int totalPoints = (int)reader["TotalPoints"];
                    pointsData[date] = totalPoints;
                    Console.WriteLine($"Date: {date}, Points: {totalPoints}"); // Debugging output
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching customer points data: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return pointsData;
        }



    }
}
