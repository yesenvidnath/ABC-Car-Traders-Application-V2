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
    public partial class SparePart : Form
    {
        public int PartID { get; set; }  // Property to hold the PartID

        public SparePart()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; // Make the form borderless
            this.StartPosition = FormStartPosition.CenterScreen; // Center the form on the screen
        }

        public void SetPartDetails(Function.Admin.Part partDetails)
        {
            lblpartname.Text = partDetails.PartName;
            lblpartdiscription.Text = partDetails.Description;
            lblpartprice.Text = partDetails.Price.ToString("C");

            // Combine the base directory with the correct path for the part images
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Parts", partDetails.ImagePath);

            // Check if the image exists, if not, use a default image
            if (!File.Exists(imagePath) || string.IsNullOrEmpty(partDetails.ImagePath))
            {
                // Set a default image if the part image is missing or invalid
                imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Parts", "default-part.jpg");
            }

            // Set the ImageLocation property with the correct image path
            imgimagepart.ImageLocation = imagePath;

            PartID = partDetails.PartID; // Set the PartID for future use
        }



        private void btnaddtocartpart_Click(object sender, EventArgs e)
        {
            if (SessionManager.UserID != 0)
            {
                Cart cartFunctions = new Cart();
                cartFunctions.AddToCart(SessionManager.UserID, null, PartID, 1);
                ShowSuccessAlert("Item added to cart successfully!");
            }
            else
            {
                ShowErrorAlert("Please log in to add items to your cart.");
            }
        }

        private void btnordernowpart_Click(object sender, EventArgs e)
        {
            if (SessionManager.UserID != 0)
            {
                Function.Customer.Customer customers = new Function.Customer.Customer();
                int customerId = customers.GetCustomerIdByUserId(SessionManager.UserID);
                string address = customers.GetCustomerAddress(SessionManager.UserID);

                List<OrderDetail> orderDetails = new List<OrderDetail>
            {
                new OrderDetail { PartID = PartID, Quantity = 1, Price = decimal.Parse(lblpartprice.Text, NumberStyles.Currency), Address = address }
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
