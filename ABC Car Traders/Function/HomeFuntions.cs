using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using WindowsFormsApp1;
using System.Drawing;


namespace ABC_Car_Traders.Function
{
    public class HomeFuntions
    {

        private dbConnect dbconnect = new dbConnect();

        public void LoadUserProfileImage(int userId, PictureBox imgUserProfile)
        {
            try
            {
                dbconnect.OpenConnection();

                // Fetch the profile image name for the logged-in user
                string query = "SELECT ProfileImage FROM Users WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, dbconnect.GetConnection());
                cmd.Parameters.AddWithValue("@UserID", userId);

                string imageName = cmd.ExecuteScalar() as string;

                if (!string.IsNullOrEmpty(imageName))
                {
                    // Construct the full path to the image
                    string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Profiles", imageName);

                    // Print the image path to the console for debugging
                    Console.WriteLine("Image Path: " + imagePath);

                    // Check if the image file exists
                    if (File.Exists(imagePath))
                    {
                        // Use Image.FromFile to load the image
                        imgUserProfile.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        MessageBox.Show("Profile image not found at path: " + imagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Handle case where no image is found
                    MessageBox.Show("No profile image found for this user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading profile image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbconnect.CloseConnection();
            }
        }


        public void RedirectToProfile(Form homeForm)
        {
            GUI.Customer.Profile profileForm = new GUI.Customer.Profile();
            profileForm.Show();
            homeForm.Hide(); // Hide the current form
        }
    }
}
