using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1;

namespace ABC_Car_Traders.Function.Admin
{
    public class Admin
    {
        private dbConnect dbconnect = new dbConnect();

        // Method to get total orders by date
        public DataTable GetTotalOrdersByDate()
        {
            DataTable dt = new DataTable();

            try
            {
                dbconnect.OpenConnection();
                SqlCommand cmd = new SqlCommand("usp_GetTotalOrdersByDate", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching total orders by date: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return dt;
        }

        // Method to get total products by type
        public DataTable GetTotalProductsByType()
        {
            DataTable dt = new DataTable();

            try
            {
                dbconnect.OpenConnection();
                SqlCommand cmd = new SqlCommand("usp_GetTotalProductsByType", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching total products by type: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return dt;
        }

        // Method to get total earnings by date
        public DataTable GetTotalEarningsByDate()
        {
            DataTable dt = new DataTable();

            try
            {
                dbconnect.OpenConnection();
                SqlCommand cmd = new SqlCommand("usp_GetTotalEarningsByDate", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching total earnings by date: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return dt;
        }


        // Method to get total items by brand
        public DataTable GetTotalItemsByBrand()
        {
            DataTable dt = new DataTable();

            try
            {
                dbconnect.OpenConnection();
                SqlCommand cmd = new SqlCommand("usp_GetTotalItemsByBrand", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching total items by brand: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return dt;
        }




        // Method to get the total number of customers
        public int GetTotalCustomers()
        {
            int totalCustomers = 0;
            try
            {
                dbconnect.OpenConnection();
                SqlCommand cmd = new SqlCommand("usp_GetTotalCustomers", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                totalCustomers = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching total customers: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }
            return totalCustomers;
        }

        // Method to get the total earnings
        public decimal GetTotalEarnings()
        {
            decimal totalEarnings = 0;
            try
            {
                dbconnect.OpenConnection();
                SqlCommand cmd = new SqlCommand("usp_GetTotalEarnings", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                totalEarnings = (decimal)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching total earnings: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }
            return totalEarnings;
        }

        // Method to get the total number of parts
        public int GetTotalParts()
        {
            int totalParts = 0;
            try
            {
                dbconnect.OpenConnection();
                SqlCommand cmd = new SqlCommand("usp_GetTotalParts", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                totalParts = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching total parts: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }
            return totalParts;
        }


        // Method to get the total number of cars
        public int GetTotalCars()
        {
            int totalCars = 0;
            try
            {
                dbconnect.OpenConnection();
                SqlCommand cmd = new SqlCommand("usp_GetTotalCars", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                totalCars = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching total cars: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }
            return totalCars;
        }


        public List<User> GetAllCustomers()
        {
            List<User> customers = new List<User>();

            try
            {
                dbconnect.OpenConnection();

                // Query to get users with Role = 'Customer' and include Password and profileImage
                string query = "SELECT UserID, FirstName, LastName, Email, Phone, Username, Password, Role, profileImage, Address FROM Users WHERE Role = 'Customer'";
                SqlCommand cmd = new SqlCommand(query, dbconnect.GetConnection());

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User
                    {
                        UserID = (int)reader["UserID"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),  // Include Password
                        Role = reader["Role"].ToString(),
                        Address = reader["Address"].ToString(),
                        ProfileImage = reader["profileImage"] != DBNull.Value ? reader["profileImage"].ToString() : null  // Include profileImage
                    };
                    customers.Add(user);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching customer info: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return customers;
        }

        public int InsertUser(User user)
        {
            int userId = 0;

            try
            {
                dbconnect.OpenConnection();
                SqlCommand cmd = new SqlCommand("RegisterUser", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password); // Assuming this is hashed
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@ProfileImage", user.ProfileImage);
                cmd.Parameters.AddWithValue("@Address", user.Address);

                SqlParameter userIdParam = new SqlParameter("@UserID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(userIdParam);

                cmd.ExecuteNonQuery();

                userId = (int)userIdParam.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting user: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return userId;
        }


        public bool UpdateUser(User user)
        {
            try
            {
                dbconnect.OpenConnection();

                SqlCommand cmd = new SqlCommand("UpdateUser", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserID", user.UserID);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@Address", user.Address);

                // Handle ProfileImage, pass DBNull.Value if null
                cmd.Parameters.AddWithValue("@ProfileImage", user.ProfileImage ?? (object)DBNull.Value);

                // Execute the update query and check the affected rows
                int rowsAffected = cmd.ExecuteNonQuery();

                // Return true if rows were affected, false otherwise
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating user: " + ex.Message);
                return false; // Return false if an exception occurs
            }
            finally
            {
                dbconnect.CloseConnection();
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                dbconnect.OpenConnection();
                SqlCommand cmd = new SqlCommand("DeleteUser", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserID", userId);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting user: " + ex.Message);
                return false;
            }
            finally
            {
                dbconnect.CloseConnection();
            }
        }




        // Get all customers sorted by UserID (assuming IDs are numeric)
        public List<User> GetAllCustomersSorted()
        {
            List<User> customers = GetAllCustomers();
            customers = customers.OrderBy(c => c.UserID).ToList(); // Sorting by UserID
            return customers;
        }

        // Binary Search method to find a user by ID or Username
        public User BinarySearchUsers(List<User> sortedUsers, string searchQuery)
        {
            int left = 0;
            int right = sortedUsers.Count - 1;
            int userId;
            bool isNumeric = int.TryParse(searchQuery, out userId);

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                User currentUser = sortedUsers[mid];

                if (isNumeric)
                {
                    // Search by UserID
                    if (currentUser.UserID == userId)
                    {
                        return currentUser;
                    }
                    else if (currentUser.UserID < userId)
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                }
                else
                {
                    // Search by Username (assuming case-insensitive comparison)
                    int comparison = string.Compare(currentUser.Username, searchQuery, StringComparison.OrdinalIgnoreCase);
                    if (comparison == 0)
                    {
                        return currentUser;
                    }
                    else if (comparison < 0)
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                }
            }

            return null; // Return null if not found
        }



    }


}