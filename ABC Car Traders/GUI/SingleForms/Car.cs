using ABC_Car_Traders.Function;
using ABC_Car_Traders.Function.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Car_Traders.GUI.SingleForms
{
    public partial class Car : Form
    {
        public int CarID { get; set; }  // Property to hold the CarID

        public Car()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None; // Make the form borderless
            this.StartPosition = FormStartPosition.CenterScreen; // Center the form on the screen

        }

        public void SetCarDetails(Function.Admin.Car carDetails)
        {
            lblcarname.Text = $"{carDetails.Make} {carDetails.Model}";
            lbldiscripton.Text = carDetails.Description;
            lblprice.Text = carDetails.Price.ToString("C");

            // Combine the base directory with the image path
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Vehicles", carDetails.ImagePath);

            // Check if the image file exists
            if (File.Exists(imagePath))
            {
                imgcarimage.ImageLocation = imagePath; // Load the image from the correct path
            }
            else
            {
                // Provide a default image if the image doesn't exist
                string defaultImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Vehicles", "default-car.jpg");
                imgcarimage.ImageLocation = defaultImagePath;
            }

            CarID = carDetails.CarID; // Set the CarID for future use
        }


        private void btnaddtocartcar_Click(object sender, EventArgs e)
        {
            if (SessionManager.UserID != 0)
            {
                Cart cartFunctions = new Cart();
                cartFunctions.AddToCart(SessionManager.UserID, CarID, null, 1);
                ShowSuccessAlert("Item added to cart successfully!");
            }
            else
            {
                ShowErrorAlert("Please log in to add items to your cart.");
            }
        }

        private void btnordernow_Click(object sender, EventArgs e)
        {
            if (SessionManager.UserID != 0)
            {
                Function.Customer.Customer customers = new Function.Customer.Customer();
                int customerId = customers.GetCustomerIdByUserId(SessionManager.UserID);
                string address = customers.GetCustomerAddress(SessionManager.UserID);

                List<OrderDetail> orderDetails = new List<OrderDetail>
                {
                    new OrderDetail { CarID = CarID, Quantity = 1, Price = decimal.Parse(lblprice.Text, NumberStyles.Currency), Address = address }
                };

                Orders orderFunctions = new Orders();
                int orderId = orderFunctions.InsertOrder(customerId, orderDetails);

                if (orderId > 0)
                {
                    ShowSuccessAlert("Order placed successfully!");
                }
                else
                {
                    ShowErrorAlert("Failed to place the order.");
                }
            }
            else
            {
                ShowErrorAlert("Please log in to place an order.");
            }
        }

        private void ShowSuccessAlert(string message)
        {
            DisplayAlert(message, Color.FromArgb(94, 148, 255));
        }

        private void ShowErrorAlert(string message)
        {
            DisplayAlert(message, Color.FromArgb(255, 69, 58)); // Red color for error
        }

        private void DisplayAlert(string message, Color backgroundColor)
        {
            Label alertLabel = new Label
            {
                Text = message,
                BackColor = backgroundColor,
                ForeColor = Color.White,
                AutoSize = true,
                Padding = new Padding(10),
                Font = new Font("Segoe UI", 10),
                Location = new Point((this.ClientSize.Width - 200) / 2, 10),
                Anchor = AnchorStyles.Top
            };

            this.Controls.Add(alertLabel);
            alertLabel.BringToFront();

            Timer timer = new Timer
            {
                Interval = 50
            };

            int opacity = 255;

            timer.Tick += (s, e) =>
            {
                opacity -= 15;
                if (opacity <= 0)
                {
                    timer.Stop();
                    this.Controls.Remove(alertLabel);
                }
                else
                {
                    alertLabel.ForeColor = Color.FromArgb(opacity, alertLabel.ForeColor);
                    alertLabel.BackColor = Color.FromArgb(opacity, alertLabel.BackColor);
                }
            };

            timer.Start();
        }
    }
}
