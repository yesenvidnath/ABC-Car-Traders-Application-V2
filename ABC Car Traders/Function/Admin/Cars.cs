using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; // Added to handle images
using System.IO; // Added to handle file streams
using System.Data.SqlClient;
using WindowsFormsApp1;
using System.Windows.Forms;
using System.Data;

namespace ABC_Car_Traders.Function.Admin
{
    public class Cars
    {
        private dbConnect db = new dbConnect();

        public List<Car> GetAllVehicles()
        {
            List<Car> cars = new List<Car>();

            try
            {
                db.OpenConnection();
                string query = "SELECT CarID, BrandID, Model, Make, Year, Price, QuantityAvailable, Image, Description FROM Cars";
                SqlCommand cmd = new SqlCommand(query, db.GetConnection());
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Car car = new Car
                    {
                        CarID = reader.GetInt32(0),
                        BrandID = reader.GetInt32(1),
                        Model = reader.GetString(2),
                        Make = reader.GetString(3),
                        Year = reader.GetInt32(4),
                        Price = reader.GetDecimal(5),
                        QuantityAvailable = reader.GetInt32(6),
                        ImagePath = reader.IsDBNull(7) ? "No Image" : reader.GetString(7), // Assuming Image is stored as a string path
                        Description = reader.IsDBNull(8) ? null : reader.GetString(8)
                    };
                    cars.Add(car);
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

            return cars;
        }

        public Car GetCarDetails(int carId)
        {
            Car car = null;

            try
            {
                db.OpenConnection();

                string query = "SELECT * FROM Cars WHERE CarID = @CarID";
                SqlCommand cmd = new SqlCommand(query, db.GetConnection());
                cmd.Parameters.AddWithValue("@CarID", carId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    car = new Car
                    {
                        CarID = reader.GetInt32(reader.GetOrdinal("CarID")),
                        BrandID = reader.GetInt32(reader.GetOrdinal("BrandID")),
                        Model = reader.IsDBNull(reader.GetOrdinal("Model")) ? string.Empty : reader.GetString(reader.GetOrdinal("Model")),
                        Make = reader.IsDBNull(reader.GetOrdinal("Make")) ? string.Empty : reader.GetString(reader.GetOrdinal("Make")),
                        Year = reader.IsDBNull(reader.GetOrdinal("Year")) ? 0 : reader.GetInt32(reader.GetOrdinal("Year")),
                        Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price")),
                        QuantityAvailable = reader.IsDBNull(reader.GetOrdinal("QuantityAvailable")) ? 0 : reader.GetInt32(reader.GetOrdinal("QuantityAvailable")),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description")),
                        ImagePath = reader.IsDBNull(reader.GetOrdinal("Image")) ? string.Empty : reader.GetString(reader.GetOrdinal("Image"))
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting car details: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            return car;
        }

        // Method to fetch all cars data
        public DataTable GetAllCarsForGrid()
        {
            DataTable carTable = new DataTable();

            try
            {
                db.OpenConnection();
                string query = "SELECT CarID, BrandID, Model, Make, Year, Price, QuantityAvailable, Image, Description FROM Cars";
                SqlCommand cmd = new SqlCommand(query, db.GetConnection());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(carTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching cars: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            return carTable;
        }


        public List<Car> GetAllCars()
        {
            List<Car> cars = new List<Car>();

            try
            {
                db.OpenConnection();
                string query = "SELECT * FROM Cars";
                SqlCommand cmd = new SqlCommand(query, db.GetConnection());

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Car car = new Car
                    {
                        CarID = reader.GetInt32(reader.GetOrdinal("CarID")),
                        BrandID = reader.GetInt32(reader.GetOrdinal("BrandID")),
                        Model = reader.IsDBNull(reader.GetOrdinal("Model")) ? string.Empty : reader.GetString(reader.GetOrdinal("Model")),
                        Make = reader.IsDBNull(reader.GetOrdinal("Make")) ? string.Empty : reader.GetString(reader.GetOrdinal("Make")),
                        Year = reader.IsDBNull(reader.GetOrdinal("Year")) ? 0 : reader.GetInt32(reader.GetOrdinal("Year")),
                        Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price")),
                        QuantityAvailable = reader.IsDBNull(reader.GetOrdinal("QuantityAvailable")) ? 0 : reader.GetInt32(reader.GetOrdinal("QuantityAvailable")),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description")),
                        ImagePath = reader.IsDBNull(reader.GetOrdinal("Image")) ? string.Empty : reader.GetString(reader.GetOrdinal("Image"))
                    };
                    cars.Add(car);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching car data: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            return cars;
        }


        // Insert a new vehicle
        public int InsertVehicle(Car car)
        {
            int carId = -1;

            try
            {
                db.OpenConnection();

                string query = "INSERT INTO Cars (BrandID, Model, Make, Year, Price, QuantityAvailable, Image, Description) " +
                               "OUTPUT INSERTED.CarID " +
                               "VALUES (@BrandID, @Model, @Make, @Year, @Price, @QuantityAvailable, @Image, @Description)";

                SqlCommand cmd = new SqlCommand(query, db.GetConnection());
                cmd.Parameters.AddWithValue("@BrandID", car.BrandID);
                cmd.Parameters.AddWithValue("@Model", car.Model);
                cmd.Parameters.AddWithValue("@Make", car.Make);
                cmd.Parameters.AddWithValue("@Year", car.Year);
                cmd.Parameters.AddWithValue("@Price", car.Price);
                cmd.Parameters.AddWithValue("@QuantityAvailable", car.QuantityAvailable);
                cmd.Parameters.AddWithValue("@Image", car.ImagePath);
                cmd.Parameters.AddWithValue("@Description", car.Description);

                carId = (int)cmd.ExecuteScalar(); // Get the inserted CarID
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting vehicle: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            return carId;
        }


        // Update an existing vehicle
        public bool UpdateVehicle(Car car)
        {
            bool isUpdated = false;

            try
            {
                db.OpenConnection();

                string query = "UPDATE Cars SET BrandID = @BrandID, Model = @Model, Make = @Make, Year = @Year, Price = @Price, " +
                               "QuantityAvailable = @QuantityAvailable, Image = @Image, Description = @Description " +
                               "WHERE CarID = @CarID";

                SqlCommand cmd = new SqlCommand(query, db.GetConnection());
                cmd.Parameters.AddWithValue("@CarID", car.CarID);
                cmd.Parameters.AddWithValue("@BrandID", car.BrandID);
                cmd.Parameters.AddWithValue("@Model", car.Model);
                cmd.Parameters.AddWithValue("@Make", car.Make);
                cmd.Parameters.AddWithValue("@Year", car.Year);
                cmd.Parameters.AddWithValue("@Price", car.Price);
                cmd.Parameters.AddWithValue("@QuantityAvailable", car.QuantityAvailable);
                cmd.Parameters.AddWithValue("@Image", car.ImagePath);
                cmd.Parameters.AddWithValue("@Description", car.Description);

                int rowsAffected = cmd.ExecuteNonQuery();
                isUpdated = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating vehicle: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            return isUpdated;
        }

        // Delete a vehicle
        public bool DeleteVehicle(int carId)
        {
            bool isDeleted = false;

            try
            {
                db.OpenConnection();

                string query = "DELETE FROM Cars WHERE CarID = @CarID";

                SqlCommand cmd = new SqlCommand(query, db.GetConnection());
                cmd.Parameters.AddWithValue("@CarID", carId);

                int rowsAffected = cmd.ExecuteNonQuery();
                isDeleted = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting vehicle: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            return isDeleted;
        }


    }

    public class Car
    {
        public int CarID { get; set; }
        public int BrandID { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public string ImagePath { get; set; } // Change from byte[] to string
        public string Description { get; set; }
    }

}
