using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WindowsFormsApp1;
using ABC_Car_Traders.Function.Customer;
using System.Data;
using ABC_Car_Traders.GUI.MainUserControls;

namespace ABC_Car_Traders.Function
{
    public class Cart
    {
        private dbConnect dbconnect = new dbConnect();
        private Customer.Customer customers = new Customer.Customer();


        public int GetTotalCartItems_Profile(int userId)
        {
            try
            {
                dbconnect.OpenConnection();

                // Get the CustomerID using the userId
                Function.Customer.Customer customerFunctions = new Function.Customer.Customer();
                int customerId = customerFunctions.GetCustomerIdByUserId(userId);

                // Query to get the total count of cart items for the customer
                string query = "SELECT SUM(Quantity) FROM CartItems WHERE CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, dbconnect.GetConnection());
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                object result = cmd.ExecuteScalar();
                int totalItems = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                return totalItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting total cart items: " + ex.Message);
                return 0; // Return 0 if there's an error
            }
            finally
            {
                dbconnect.CloseConnection();
            }
        }


        // Get total Cart Items
        public int GetTotalCartItems(int userId)
        {
            try
            {
                dbconnect.OpenConnection();

                // Get the CustomerID using the userId
                Customer.Customer customers = new Customer.Customer();
                int customerId = customers.GetCustomerIdByUserId(userId);

                // Query to get the total count of cart items for the customer
                string query = "SELECT SUM(Quantity) FROM CartItems WHERE CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, dbconnect.GetConnection());
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                object result = cmd.ExecuteScalar();
                int totalItems = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                return totalItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting total cart items: " + ex.Message);
                return 0; // Return 0 if there's an error
            }
            finally
            {
                dbconnect.CloseConnection();
            }
        }


        // The add to cart funtonality
        public void AddToCart(int userId, int? carId, int? partId, int quantity)
        {
            // Get the CustomerID by UserID
            int customerId = customers.GetCustomerIdByUserId(userId);

            dbconnect.OpenConnection();

            SqlCommand cmd = new SqlCommand("usp_AddToCart", dbconnect.GetConnection())
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CustomerID", customerId);
            cmd.Parameters.AddWithValue("@CarID", carId.HasValue ? (object)carId.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@PartID", partId.HasValue ? (object)partId.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@Quantity", quantity);

            cmd.ExecuteNonQuery();

            dbconnect.CloseConnection();
        }


        public List<CartItem> GetCartItems(int customerId)
        {
            List<CartItem> cartItems = new List<CartItem>();

            try
            {
                dbconnect.OpenConnection();

                string query = "usp_GetCartItemsByUser"; // Assuming this is your stored procedure name
                SqlCommand cmd = new SqlCommand(query, dbconnect.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", customerId); // Use CustomerID

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CartItem item = new CartItem
                    {
                        CartItemID = reader.GetInt32(reader.GetOrdinal("CartItemID")),
                        ItemName = reader.IsDBNull(reader.GetOrdinal("ItemName")) ? "Unknown Item" : reader.GetString(reader.GetOrdinal("ItemName")),
                        ItemDescription = reader.IsDBNull(reader.GetOrdinal("ItemDescription")) ? "No Description" : reader.GetString(reader.GetOrdinal("ItemDescription")),
                        Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price")),
                        ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath")),
                        Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                    };
                    cartItems.Add(item);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting cart items: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return cartItems;
        }

        public void RemoveFromCart(int cartItemId)
        {
            try
            {
                dbconnect.OpenConnection();
                SqlCommand cmd = new SqlCommand("DELETE FROM CartItems WHERE CartItemID = @CartItemID", dbconnect.GetConnection());
                cmd.Parameters.AddWithValue("@CartItemID", cartItemId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error removing item from cart: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }
        }

        public void UpdateCartItemQuantity(int cartItemId, int newQuantity)
        {
            try
            {
                dbconnect.OpenConnection();

                string query = "UPDATE CartItems SET Quantity = @Quantity WHERE CartItemID = @CartItemID";
                SqlCommand cmd = new SqlCommand(query, dbconnect.GetConnection());
                cmd.Parameters.AddWithValue("@Quantity", newQuantity);
                cmd.Parameters.AddWithValue("@CartItemID", cartItemId);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating cart item quantity: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }
        }

        public void ClearCart(int customerId)
        {
            try
            {
                dbconnect.OpenConnection();

                string query = "DELETE FROM CartItems WHERE CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, dbconnect.GetConnection());
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error clearing cart: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }
        }
    }

    public class CartItem
    {
        public int CartItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; }
        public int? CarID { get; set; }
        public int? PartID { get; set; }
    }
}
