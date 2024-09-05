using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using System.Drawing;
using System.IO;

namespace ABC_Car_Traders.Function.Admin
{
    public class Brands
    {
        private dbConnect db = new dbConnect();

        public List<Brand> GetAllVehicles()
        {
            List<Brand> brands = new List<Brand>();

            try
            {
                db.OpenConnection();
                string query = "SELECT BrandID, BrandName, Description, Image FROM Brands";
                SqlCommand cmd = new SqlCommand(query, db.GetConnection());
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Brand brand = new Brand
                    {
                        BrandID = reader.GetInt32(0),
                        BrandName = reader.GetString(1),
                        Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                        ImagePath = reader.IsDBNull(3) ? "No Image" : reader.GetString(3)
                    };
                    brands.Add(brand);
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

            return brands;
        }


        public static Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                return Image.FromFile("Path_to_No_Image"); // Specify the path to your default "No Image" picture
            }

            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }


        // Method to get all brand names from the database
        public List<Brand> GetAllBrands()
        {
            List<Brand> brands = new List<Brand>();

            try
            {
                db.OpenConnection();

                string query = "SELECT BrandID, BrandName FROM Brands";
                SqlCommand cmd = new SqlCommand(query, db.GetConnection());

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Brand brand = new Brand
                    {
                        BrandID = reader.GetInt32(reader.GetOrdinal("BrandID")),
                        BrandName = reader.GetString(reader.GetOrdinal("BrandName"))
                    };
                    brands.Add(brand);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching brands: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            return brands;
        }

    }

    public class Brand
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}
