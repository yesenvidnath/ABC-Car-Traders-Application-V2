using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace ABC_Car_Traders.Function.Admin
{
    public class Users
    {
        private dbConnect dbconnect = new dbConnect();

        public User GetUserInfoById(int userId)
        {
            User userInfo = null;

            try
            {
                dbconnect.OpenConnection();

                SqlCommand cmd = new SqlCommand(
                    "SELECT UserID, FirstName, LastName, Email, Phone, Username, Password, Role, ProfileImage, Address " +
                    "FROM Users WHERE UserID = @UserID", dbconnect.GetConnection());
                cmd.Parameters.AddWithValue("@UserID", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    userInfo = new User
                    {
                        UserID = (int)reader["UserID"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Role = reader["Role"].ToString(),
                        ProfileImage = reader["ProfileImage"].ToString(),
                        Address = reader["Address"].ToString()
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching user info: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return userInfo;
        }


        public bool UpdateUserInfo(User user)
        {
            try
            {
                dbconnect.OpenConnection();

                string query = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, " +
                               "Username = @Username, Password = @Password, ProfileImage = @ProfileImage, Address = @Address " +
                               "WHERE UserID = @UserID";

                SqlCommand cmd = new SqlCommand(query, dbconnect.GetConnection());
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password); 
                cmd.Parameters.AddWithValue("@ProfileImage", user.ProfileImage);
                cmd.Parameters.AddWithValue("@Address", user.Address);
                cmd.Parameters.AddWithValue("@UserID", user.UserID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating user info: " + ex.Message);
                return false;
            }
            finally
            {
                dbconnect.CloseConnection();
            }
        }


        public bool DeleteUser(int userId)
        {
            SqlTransaction transaction = null;
            try
            {
                dbconnect.OpenConnection();
                transaction = dbconnect.GetConnection().BeginTransaction();

                // Delete from Customers table first (if exists)
                SqlCommand cmdDeleteCustomer = new SqlCommand("DELETE FROM Customers WHERE UserID = @UserID", dbconnect.GetConnection(), transaction);
                cmdDeleteCustomer.Parameters.AddWithValue("@UserID", userId);
                cmdDeleteCustomer.ExecuteNonQuery();

                // Delete from Users table
                SqlCommand cmdDeleteUser = new SqlCommand("DELETE FROM Users WHERE UserID = @UserID", dbconnect.GetConnection(), transaction);
                cmdDeleteUser.Parameters.AddWithValue("@UserID", userId);
                int result = cmdDeleteUser.ExecuteNonQuery();

                transaction.Commit();
                return result > 0;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                Console.WriteLine("Error deleting user: " + ex.Message);
                return false;
            }
            finally
            {
                dbconnect.CloseConnection();
            }
        }


        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            try
            {
                dbconnect.OpenConnection();
                // Fetch all the necessary fields from the Users table, including handling null values
                SqlCommand cmd = new SqlCommand("SELECT UserID, FirstName, LastName, Email, Phone, Username, Role, ProfileImage, Address FROM Users", dbconnect.GetConnection());
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User
                    {
                        UserID = reader.IsDBNull(reader.GetOrdinal("UserID")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserID")),
                        FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? "N/A" : reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? "N/A" : reader.GetString(reader.GetOrdinal("LastName")),
                        Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "N/A" : reader.GetString(reader.GetOrdinal("Email")),
                        Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? "N/A" : reader.GetString(reader.GetOrdinal("Phone")),
                        Username = reader.IsDBNull(reader.GetOrdinal("Username")) ? "N/A" : reader.GetString(reader.GetOrdinal("Username")),
                        Role = reader.IsDBNull(reader.GetOrdinal("Role")) ? "N/A" : reader.GetString(reader.GetOrdinal("Role")),
                        ProfileImage = reader.IsDBNull(reader.GetOrdinal("ProfileImage")) ? null : reader.GetString(reader.GetOrdinal("ProfileImage")), // Set null if no profile image
                        Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? "N/A" : reader.GetString(reader.GetOrdinal("Address"))
                    };
                    users.Add(user);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching users: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return users;

        }

    }

    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public string ProfileImage { get; set; }

    }
}
