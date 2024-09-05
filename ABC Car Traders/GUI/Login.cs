using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ABC_Car_Traders.Function;
using ABC_Car_Traders.GUI.Alerts;
using System.Security.Cryptography;


namespace ABC_Car_Traders.GUI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            ShowLoginPanel();
            txtpassword.UseSystemPasswordChar = true;

        }

        // Button handling bases on the button click Start 

        private void ShowLoginPanel()
        {
            // Display login panel and hide registration panel
            pnllogin.Visible = true;
            pnlregistrationshandle.Visible = false;

            // Change button colors
            btnenablelogin.BackColor = Color.FromArgb(94, 148, 255); // Highlight the login button
            btnenableregister.BackColor = SystemColors.Control; // Reset the registration button color
        }

        private void ShowRegistrationPanel()
        {
            // Display registration panel and hide login panel
            pnllogin.Visible = false;
            pnlregistrationshandle.Visible = true;

            // Change button colors
            btnenablelogin.BackColor = SystemColors.Control; // Reset the login button color
            btnenableregister.BackColor = Color.FromArgb(94, 148, 255); // Highlight the registration button
        }

        private void btnenablelogin_Click_1(object sender, EventArgs e)
        {
            ShowLoginPanel();
        }

        private void btnenableregister_Click_1(object sender, EventArgs e)
        {
            ShowRegistrationPanel();
        }

        // Button handling bases on the button click End 



        // Registration Start
        private void btnselectimg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Select a Profile Image"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedImagePath = openFileDialog.FileName;
                imgprofilepic.ImageLocation = selectedImagePath;

                // Convert the selected image to a byte array
                byte[] profileImage = File.ReadAllBytes(selectedImagePath);

                // Store the byte array for registration use
                // For example, store it in a class-level variable
                // profileImageData = profileImage;
            }
        }

        private void btnregisternow_Click(object sender, EventArgs e)
        {
            // Gather input data from the form
            string firstName = txtfirstname.Text;
            string lastName = txtlastname.Text;
            string email = txtemail.Text;
            string phone = txtphone.Text;
            string address = txtaddress.Text;
            string username = txtusername.Text;
            string password = txtwpd.Text;
            string role = combtype.SelectedItem.ToString();

            // Create an instance of the loginRegistration class
            loginRegistration reg = new loginRegistration();

            // Hash the password using the method from loginRegistration class
            string hashedPassword = reg.HashPassword(password);

            string imageName = null;

            if (imgprofilepic.ImageLocation != null)
            {
                // Define the directory within your project
                string imagesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Profiles");

                // Ensure the directory exists
                Directory.CreateDirectory(imagesDirectory); // This will create the directory if it doesn't exist

                // Generate a unique filename for the image
                imageName = Guid.NewGuid().ToString() + Path.GetExtension(imgprofilepic.ImageLocation);

                // Full path where the image will be saved
                string imagePath = Path.Combine(imagesDirectory, imageName);

                // Copy the image to the target directory
                File.Copy(imgprofilepic.ImageLocation, imagePath, true);
            }

            // Call the RegisterUser method with the hashed password and image name
            bool isRegistered = reg.RegisterUser(firstName, lastName, email, phone, address, username, hashedPassword, role, imageName);

            // Provide feedback to the user
            if (isRegistered)
            {
                // Clear the fields
                ClearRegistrationFields();

                // Open the login panel
                ShowLoginPanel();
            }
            else
            {
                MessageBox.Show("Registration failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to clear registration fields
        private void ClearRegistrationFields()
        {
            txtfirstname.Clear();
            txtlastname.Clear();
            txtemail.Clear();
            txtphone.Clear();
            txtaddress.Clear();
            txtusername.Clear();
            txtwpd.Clear();
            combtype.SelectedIndex = -1;
            imgprofilepic.Image = null; // Clear the selected image
        }

        // Registration END 



        // Login Methode Start

        private void btnloginnow_Click(object sender, EventArgs e)
        {
            // Get the username and password from the formt
            string username = txtuname.Text;
            string password = txtpassword.Text;

            // Create an instance of the loginRegistration class
            loginRegistration loginReg = new loginRegistration();

            // Validate the login credentials
            var (userId, role) = loginReg.ValidateLogin(username, password);

            if (userId.HasValue)
            {
                CustomAlert.ShowAlert("Login successful!", 3000, "Success"); // Show success alert for 3 seconds

                // Start the session
                SessionManager.StartSession(userId.Value, role);

                // Redirect to the Home form
                Home homeForm = new Home();
                homeForm.Show();
                this.Hide(); 
            }
            else
            {
                CustomAlert.ShowAlert("Invalid username or password.", 3000, "Error"); // Show error alert for 3 seconds
            }
        }

        private void togswtshowpassword_CheckedChanged(object sender, EventArgs e)
        {
            txtpassword.UseSystemPasswordChar = !togswtshowpassword.Checked;
        }

        // Login Methode End

    }
}
