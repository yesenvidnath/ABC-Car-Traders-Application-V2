using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace ABC_Car_Traders.Function
{
    public class loginRegistration
    {

        private dbConnect dbconnect = new dbConnect();

        public bool RegisterUser(string firstName, string lastName, string email, string phone, string address, string username, string password, string role, string imageName)
        {
            try
            {
                dbconnect.OpenConnection();

                SqlCommand cmd = new SqlCommand("RegisterUser", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@ProfileImage", imageName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", address);

                SqlParameter userIdParam = new SqlParameter("@UserID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(userIdParam);

                cmd.ExecuteNonQuery();

                int newUserId = (int)userIdParam.Value;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the exception
                return false;
            }
            finally
            {
                dbconnect.CloseConnection();
            }
        }

        public (int? UserID, string Role) ValidateLogin(string username, string password)
        {
            try
            {
                dbconnect.OpenConnection();

                // Hash the password before checking
                string hashedPassword = HashPassword(password);

                // Optionally log the hashed password to verify
                //Console.WriteLine("Hashed Password: " + hashedPassword);

                // Direct SQL query to validate username and hashed password
                string query = "SELECT UserID, Role FROM Users WHERE Username = @Username AND Password = @Password";

                SqlCommand cmd = new SqlCommand(query, dbconnect.GetConnection());
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int? userId = reader["UserID"] as int?;
                    string role = reader["Role"] as string;

                    return (userId, role);
                }
                else
                {
                    return (null, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the exception
                return (null, null);
            }
            finally
            {
                dbconnect.CloseConnection();
            }
        }


        public string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                // Truncate the hash to 32 characters
                return builder.ToString().Substring(0, 32);
            }
        }


    }
}
