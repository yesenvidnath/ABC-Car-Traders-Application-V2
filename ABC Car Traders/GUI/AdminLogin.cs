using ABC_Car_Traders.Function;
using ABC_Car_Traders.GUI.Alerts;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Car_Traders.GUI
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
            txtpassword.UseSystemPasswordChar = true;
        }

        private void btnloginnow_Click_1(object sender, EventArgs e)
        {
            // Get the username and password from the form
            string username = txtuname.Text;
            string password = txtpassword.Text;

            // Create an instance of the loginRegistration class
            loginRegistration loginReg = new loginRegistration();

            // Validate the login credentials
            var (userId, role) = loginReg.ValidateLogin(username, password);

            if (userId.HasValue)
            {
                CustomAlert.ShowAlert("Login successful!", 3000, "Success"); // Show success alert for 3 seconds

                // Start the session with username and role
                SessionManager.StartSession(userId.Value, role);

                // Close all other open forms, including Home form if it's open
                foreach (Form openForm in Application.OpenForms.Cast<Form>().ToList())
                {
                    if (openForm != this) // Don't close the current login form yet
                    {
                        openForm.Hide(); // Hide the form instead of closing it
                    }
                }

                // Check if the user is an admin
                if (role == "Admin")
                {
                    // Generate the secret key (combination of username, password, and current day name)
                    string secretKey = username + password + DateTime.Now.DayOfWeek.ToString();

                    // Prompt for the secret key
                    string inputKey = PromptForSecretKey();

                    if (inputKey == secretKey)
                    {
                        CustomAlert.ShowAlert("Admin access granted!", 3000, "Success");

                        // Redirect to the Admin Profile form
                        GUI.Admin.Profile adminProfileForm = new GUI.Admin.Profile();
                        this.Hide(); // Hide the login form
                        adminProfileForm.ShowDialog(); // Show the Admin Profile form as a modal dialog
                    }
                    else
                    {
                        CustomAlert.ShowAlert("Invalid secret key.", 3000, "Error");
                        SessionManager.EndSession(); // End the session if the secret key is invalid
                        foreach (Form openForm in Application.OpenForms.Cast<Form>().ToList())
                        {
                            openForm.Show(); // Re-show previously hidden forms if the secret key is invalid
                        }
                    }
                }
                else
                {
                    // Redirect to the Home form for non-admin users
                    Home homeForm = new Home();
                    this.Hide(); // Hide the login form
                    homeForm.ShowDialog(); // Show the Home form as a modal dialog
                }

                this.Close(); // Close the login form after everything is handled
            }
            else
            {
                CustomAlert.ShowAlert("Invalid username or password.", 3000, "Error"); // Show error alert for 3 seconds
            }
        }




        // Method to prompt the user for the secret key
        private string PromptForSecretKey()
        {
            using (var form = new Form())
            {
                form.Text = "Enter Secret Key";
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.BackColor = Color.FromArgb(45, 45, 48);
                form.ForeColor = Color.White;
                form.Width = 350;
                form.Height = 200;
                form.MaximizeBox = false;
                form.MinimizeBox = false;
                form.Font = new Font("Segoe UI", 10);
                form.ShowIcon = false;
                form.ShowInTaskbar = false;

                var label = new Label
                {
                    Left = 20,
                    Top = 20,
                    Text = "Enter Secret Key:",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };

                var textBox = new TextBox
                {
                    Left = 20,
                    Top = 50,
                    Width = 300,
                    UseSystemPasswordChar = true,  // Hide input for security
                    Font = new Font("Segoe UI", 10)
                };

                var buttonOk = new Button
                {
                    Text = "Submit",
                    Left = 120,
                    Width = 100,
                    Top = 90,
                    DialogResult = DialogResult.OK,
                    BackColor = Color.FromArgb(94, 148, 255),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };

                buttonOk.FlatAppearance.BorderSize = 0;

                form.Controls.Add(label);
                form.Controls.Add(textBox);
                form.Controls.Add(buttonOk);
                form.AcceptButton = buttonOk;

                return form.ShowDialog() == DialogResult.OK ? textBox.Text : string.Empty;
            }
        }


        private void togswtshowpassword_CheckedChanged(object sender, EventArgs e)
        {
            txtpassword.UseSystemPasswordChar = !togswtshowpassword.Checked;
        }


    }
}
