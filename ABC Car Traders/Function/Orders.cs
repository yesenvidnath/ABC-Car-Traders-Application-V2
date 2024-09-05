using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WindowsFormsApp1;
using System.Data;

namespace ABC_Car_Traders.Function
{
    public class Orders
    {
        private dbConnect dbconnect = new dbConnect();


        // Insert a new order
        public int InsertOrder(int customerId, List<OrderDetail> orderDetails)
        {
            int orderId = -1;
            decimal totalAmount = CalculateTotalAmount(orderDetails);

            try
            {
                dbconnect.OpenConnection();

                SqlCommand cmd = new SqlCommand("usp_InsertOrder", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                cmd.Parameters.AddWithValue("@Status", "Pending");

                SqlParameter orderIdParam = new SqlParameter("@OrderID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(orderIdParam);

                cmd.ExecuteNonQuery();

                orderId = (int)orderIdParam.Value;

                if (orderId > 0)
                {
                    foreach (var detail in orderDetails)
                    {
                        InsertOrderDetail(orderId, detail.CarID, detail.PartID, detail.Quantity, detail.Price, detail.Address);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting order: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return orderId;
        }

        // Insert a new order detail
        private void InsertOrderDetail(int orderId, int? carId, int? partId, int quantity, decimal price, string address)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_InsertOrderDetail", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OrderID", orderId);
                cmd.Parameters.AddWithValue("@CarID", carId.HasValue ? (object)carId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@PartID", partId.HasValue ? (object)partId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Address", address ?? (object)DBNull.Value);  // Ensure address is not null

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting order detail: " + ex.Message);
            }
        }

        // Calculate the total amount for the order
        private decimal CalculateTotalAmount(List<OrderDetail> orderDetails)
        {
            decimal totalAmount = 0;

            foreach (var detail in orderDetails)
            {
                totalAmount += detail.Price * detail.Quantity;
            }

            return totalAmount;
        }



        // Method to get all orders for a specific customer
        public List<CustomerOrder> GetOrdersByCustomerId(int customerId)
        {
            List<CustomerOrder> orders = new List<CustomerOrder>();

            try
            {
                dbconnect.OpenConnection();

                SqlCommand cmd = new SqlCommand("usp_GetCustomerOrdersintotal", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CustomerOrder order = new CustomerOrder
                        {
                            OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                            OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                            TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                            Status = reader.GetString(reader.GetOrdinal("Status")),
                            Address = reader.GetString(reader.GetOrdinal("Address"))
                        };
                        orders.Add(order);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving orders: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return orders;
        }

        // Method to update order status to "Cancelled"
        public bool CancelOrder(int orderId)
        {
            string query = "UPDATE Orders SET Status = @Status WHERE OrderID = @OrderID";

            try
            {
                dbconnect.OpenConnection();
                SqlCommand cmd = new SqlCommand(query, dbconnect.GetConnection());
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                cmd.Parameters.AddWithValue("@Status", "Cancelled");

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating order status: " + ex.Message);
                return false;
            }
            finally
            {
                dbconnect.CloseConnection();
            }
        }



        // For the Admin Panel
        public List<CustomerOrder> GetAllOrders()
        {
            List<CustomerOrder> orders = new List<CustomerOrder>();

            try
            {
                dbconnect.OpenConnection();

                SqlCommand cmd = new SqlCommand("usp_GetAllOrders", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CustomerOrder order = new CustomerOrder
                        {
                            OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                            OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                            TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        };
                        orders.Add(order);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving all orders: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return orders;
        }


        // Method to update the order status
        public bool UpdateOrderStatus(int orderId, string newStatus)
        {
            try
            {
                dbconnect.OpenConnection();
                SqlCommand cmd = new SqlCommand("UPDATE Orders SET Status = @Status WHERE OrderID = @OrderID", dbconnect.GetConnection());
                cmd.Parameters.AddWithValue("@Status", newStatus);
                cmd.Parameters.AddWithValue("@OrderID", orderId);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating order status: " + ex.Message);
                return false;
            }
            finally
            {
                dbconnect.CloseConnection();
            }
        }

        // Method to get order details by order ID
        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            try
            {
                dbconnect.OpenConnection();

                // Use the stored procedure to get order details
                SqlCommand cmd = new SqlCommand("usp_GetOrderDetailsByOrderId", dbconnect.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@OrderID", orderId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OrderDetail detail = new OrderDetail
                        {
                            OrderDetailID = reader.GetInt32(reader.GetOrdinal("OrderDetailID")),
                            OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                            Quantity = reader.IsDBNull(reader.GetOrdinal("Quantity")) ? 0 : reader.GetInt32(reader.GetOrdinal("Quantity")),
                            Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price")),
                            ProductName = reader.IsDBNull(reader.GetOrdinal("ProductName")) ? "Unknown Product" : reader.GetString(reader.GetOrdinal("ProductName")),  // Handling NULL ProductName
                            Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? "No Address" : reader.GetString(reader.GetOrdinal("Address")) // Handling NULL Address
                        };
                        orderDetails.Add(detail);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting order details: " + ex.Message);
            }
            finally
            {
                dbconnect.CloseConnection();
            }

            return orderDetails;
        }

        // Orders.cs - Binary Search Method to Find Order by ID
        public CustomerOrder GetOrderByOrderId(int orderId)
        {
            List<CustomerOrder> allOrders = GetAllOrders(); // Get the list of all orders
            allOrders = allOrders.OrderBy(o => o.OrderID).ToList(); // Ensure the list is sorted by OrderID for binary search

            int low = 0;
            int high = allOrders.Count - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                if (allOrders[mid].OrderID == orderId)
                {
                    return allOrders[mid];  // Return the order if found
                }
                else if (allOrders[mid].OrderID < orderId)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }

            return null; // Return null if not found
        }



    }

    public class CustomerOrder
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
    }

    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public string ProductName { get; set; }
        public int? CarID { get; set; }
        public int? PartID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
    }
}
