using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace ABC_Car_Traders.Function.Admin
{
    public class CarParts
    {
        private dbConnect db = new dbConnect();

        public List<Part> GetAllParts()
        {
            List<Part> parts = new List<Part>();

            try
            {
                db.OpenConnection();
                // Ensure the table name here matches the one in your database
                string query = "SELECT PartID, CarID, PartName, Description, Price, Image FROM CarParts"; // Update to the correct table name
                SqlCommand cmd = new SqlCommand(query, db.GetConnection());
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Part part = new Part
                    {
                        PartID = reader.GetInt32(0),
                        CarID = reader.GetInt32(1),
                        PartName = reader.GetString(2),
                        Description = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Price = reader.GetDecimal(4),
                        ImagePath = reader.IsDBNull(5) ? "No Image" : reader.GetString(5)
                    };
                    parts.Add(part);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            return parts;
        }


        public Part GetPartDetails(int partId)
        {
            Part part = null;

            try
            {
                db.OpenConnection();

                string query = "SELECT * FROM CarParts WHERE PartID = @PartID";
                SqlCommand cmd = new SqlCommand(query, db.GetConnection());
                cmd.Parameters.AddWithValue("@PartID", partId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    part = new Part
                    {
                        PartID = reader.GetInt32(reader.GetOrdinal("PartID")),
                        PartName = reader.GetString(reader.GetOrdinal("PartName")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                        QuantityAvailable = reader.GetInt32(reader.GetOrdinal("QuantityAvailable")),
                        ImagePath = reader.GetString(reader.GetOrdinal("Image"))
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting part details: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            return part;
        }


        public List<Part> GetAllParts_Admin()
        {
            List<Part> parts = new List<Part>();

            try
            {
                db.OpenConnection();
                string query = "SELECT PartID, BrandID, PartName, Description, Price, QuantityAvailable, Image, CarID FROM CarParts";
                SqlCommand cmd = new SqlCommand(query, db.GetConnection());
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Part part = new Part
                    {
                        PartID = reader.GetInt32(0),
                        BrandID = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),  // Handle nullable BrandID
                        PartName = reader.GetString(2),
                        Description = reader.GetString(3),
                        Price = reader.GetDecimal(4),
                        QuantityAvailable = reader.GetInt32(5),
                        ImagePath = reader.IsDBNull(6) ? null : reader.GetString(6), // Handle nullable Image
                        CarID = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7) // Handle nullable CarID
                    };
                    parts.Add(part);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching parts: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            return parts;
        }

        public int InsertPart(CarPart part)
        {
            int partID = -1;

            try
            {
                db.OpenConnection();
                string query = "INSERT INTO CarParts (BrandID, PartName, Description, Price, QuantityAvailable, Image, CarID) " +
                               "OUTPUT INSERTED.PartID " +
                               "VALUES (@BrandID, @PartName, @Description, @Price, @QuantityAvailable, @Image, @CarID)";

                SqlCommand cmd = new SqlCommand(query, db.GetConnection());
                cmd.Parameters.AddWithValue("@BrandID", part.BrandID);
                cmd.Parameters.AddWithValue("@PartName", part.PartName);
                cmd.Parameters.AddWithValue("@Description", part.Description);
                cmd.Parameters.AddWithValue("@Price", part.Price);
                cmd.Parameters.AddWithValue("@QuantityAvailable", part.QuantityAvailable);
                cmd.Parameters.AddWithValue("@Image", part.ImagePath);
                cmd.Parameters.AddWithValue("@CarID", (object)part.CarID ?? DBNull.Value);  // Handle null CarID

                partID = (int)cmd.ExecuteScalar();  // Get the inserted PartID
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting part: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            return partID;
        }


        public bool UpdatePart(CarPart part)
        {
            bool isUpdated = false;

            try
            {
                db.OpenConnection();
                string query = "UPDATE CarParts SET BrandID = @BrandID, PartName = @PartName, Description = @Description, " +
                               "Price = @Price, QuantityAvailable = @QuantityAvailable, Image = @Image, CarID = @CarID " +
                               "WHERE PartID = @PartID";

                SqlCommand cmd = new SqlCommand(query, db.GetConnection());
                cmd.Parameters.AddWithValue("@PartID", part.PartID);
                cmd.Parameters.AddWithValue("@BrandID", part.BrandID);
                cmd.Parameters.AddWithValue("@PartName", part.PartName);
                cmd.Parameters.AddWithValue("@Description", part.Description);
                cmd.Parameters.AddWithValue("@Price", part.Price);
                cmd.Parameters.AddWithValue("@QuantityAvailable", part.QuantityAvailable);
                cmd.Parameters.AddWithValue("@Image", part.ImagePath);
                cmd.Parameters.AddWithValue("@CarID", (object)part.CarID ?? DBNull.Value);

                int rowsAffected = cmd.ExecuteNonQuery();
                isUpdated = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating part: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            return isUpdated;
        }


        public bool DeletePart(int partID)
        {
            bool isDeleted = false;

            try
            {
                db.OpenConnection();
                string query = "DELETE FROM CarParts WHERE PartID = @PartID";

                SqlCommand cmd = new SqlCommand(query, db.GetConnection());
                cmd.Parameters.AddWithValue("@PartID", partID);

                int rowsAffected = cmd.ExecuteNonQuery();
                isDeleted = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting part: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            return isDeleted;
        }




    }


    public class CarPart
    {
        public int PartID { get; set; }
        public int BrandID { get; set; }
        public string PartName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public string ImagePath { get; set; }
        public int? CarID { get; set; }  // Nullable as some parts may not be tied to a car
    }



    public class Part
    {
        public int PartID { get; set; }
        public int? BrandID { get; set; }  // Update this to make BrandID nullable
        public string PartName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public string ImagePath { get; set; }
        public int? CarID { get; set; }  // Make CarID nullable since it can be null
    }

}
