using ABC_Car_Traders.Function;
using ABC_Car_Traders.Function.Admin;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using ABC_Car_Traders.Function.Customer;


namespace ABC_Car_Traders.GUI.Customer
{
    public partial class Profile : Form
    {
        private dbConnect dbconnect = new dbConnect();
        private Cart cartFunctions;
        public Profile()
        {
            InitializeComponent();
            LoadUserProfileImage();
            LoadOrderStatusChart();
            LoadSpendingByDateChart();
            LoadTotalOrdersByProductsChart();
            LoadCustomerPointsChart();
            cartFunctions = new Cart(); 
            LoadTotalCartItems();
            LoadCartItems();
            LoadCustomerOrders();
            LoadUserInfo();
        }


        // Nav Section Start 

        private void btnopencart_Click(object sender, EventArgs e)
        {

        }
        private void imguserprofile_Click_1(object sender, EventArgs e)
        {

        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            // Clear the session or log the user out
            SessionManager.EndSession();


            GUI.Home HomePge = new GUI.Home();
            HomePge.Show();
            this.Hide();
        }

        private void LoadUserProfileImage()
        {
            if (SessionManager.UserID != 0) // Check if user is logged in
            {
                int userId = SessionManager.UserID;

                // Initialize your HomeFunctions class or equivalent
                HomeFuntions homeFunctions = new HomeFuntions();
                homeFunctions.LoadUserProfileImage(userId, imguserprofile); // Load the user's profile image
            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            // Optionally, redirect to the login screen
            GUI.Home HomePge = new GUI.Home();
            HomePge.Show();
            this.Hide();
        }


        private void LoadTotalCartItems()
        {
            if (SessionManager.UserID != 0) // Check if user is logged in
            {
                int userId = SessionManager.UserID;
                int totalCartItems = 0;

                // Try to get the total cart items
                if (cartFunctions != null)
                {
                    totalCartItems = cartFunctions.GetTotalCartItems(userId);
                }

                // Display the total cart items or 0 if none exist
                if (bntntotalcactitems != null)
                {
                    bntntotalcactitems.Text = totalCartItems > 0 ? totalCartItems.ToString() : "0";
                }
            }
            else
            {
                if (bntntotalcactitems != null)
                {
                    bntntotalcactitems.Text = "0"; // Show 0 if the user is not logged in
                }
            }
        }



        // Nav Section End 


        // Tab 1

        private void LoadOrderStatusChart()
        {
            Function.Customer.Customer customerFunctions = new Function.Customer.Customer();
            int userId = SessionManager.UserID;
            int customerId = customerFunctions.GetCustomerIdByUserId(userId);

            // Fetch orders by status using the stored procedure
            Dictionary<string, int> orderStatusCounts = customerFunctions.GetOrdersByCustomer(customerId);

            // Clear the chart
            charTotOrders.Series.Clear();

            // Create a series for the chart
            var series = new System.Windows.Forms.DataVisualization.Charting.Series("Orders")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie
            };

            // Add data points to the series
            foreach (var status in orderStatusCounts)
            {
                series.Points.AddXY(status.Key, status.Value);
            }

            // Add the series to the chart
            charTotOrders.Series.Add(series);

            // Customize chart appearance if needed
            charTotOrders.Titles.Clear();
            charTotOrders.Titles.Add("Order Status Distribution");
        }


        private void LoadSpendingByDateChart()
        {
            Function.Customer.Customer customerFunctions = new Function.Customer.Customer();
            int userId = SessionManager.UserID;
            int customerId = customerFunctions.GetCustomerIdByUserId(userId);

            // Fetch spending by date using the stored procedure
            Dictionary<DateTime, decimal> spendingData = customerFunctions.GetSpendingByDate(customerId);

            // Clear the chart
            charspendingbasedondate.Series.Clear();

            // Create a series for the chart
            var series = new System.Windows.Forms.DataVisualization.Charting.Series("Spending")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut
            };

            // Add data points to the series
            foreach (var data in spendingData)
            {
                series.Points.AddXY(data.Key.ToString("MM/dd/yyyy"), data.Value);
            }

            // Add the series to the chart
            charspendingbasedondate.Series.Add(series);

            // Customize chart appearance if needed
            charspendingbasedondate.Titles.Clear();
            charspendingbasedondate.Titles.Add("Spending by Date");
        }


        private void LoadTotalOrdersByProductsChart()
        {
            Function.Customer.Customer customerFunctions = new Function.Customer.Customer();
            int userId = SessionManager.UserID;
            int customerId = customerFunctions.GetCustomerIdByUserId(userId);

            Dictionary<string, int> productOrderCounts = customerFunctions.GetTotalOrdersByProducts(customerId);

            // Clear the chart
            chartotalordersbyproducts.Series.Clear();

            // Create a series for the chart
            var series = new System.Windows.Forms.DataVisualization.Charting.Series("Total Orders")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar
            };

            // Add data points to the series
            foreach (var product in productOrderCounts)
            {
                series.Points.AddXY(product.Key, product.Value);
            }

            // Add the series to the chart
            chartotalordersbyproducts.Series.Add(series);

            // Customize chart appearance if needed
            chartotalordersbyproducts.Titles.Clear();
            chartotalordersbyproducts.Titles.Add("Total Orders by Products");
        }


        private void LoadCustomerPointsChart()
        {
            Function.Customer.Customer customerFunctions = new Function.Customer.Customer();
            int userId = SessionManager.UserID;
            int customerId = customerFunctions.GetCustomerIdByUserId(userId);

            Dictionary<DateTime, int> pointsData = customerFunctions.GetCustomerPointsByDate(customerId);

            // Clear the chart
            charcustomerpointsbasedondates.Series.Clear();

            // Create a series for the chart
            var series = new System.Windows.Forms.DataVisualization.Charting.Series("Points")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column
            };

            // Add data points to the series
            foreach (var point in pointsData)
            {
                series.Points.AddXY(point.Key.ToString("MM/dd/yyyy"), point.Value);
            }

            // Add the series to the chart
            charcustomerpointsbasedondates.Series.Add(series);

            // Customize chart appearance if needed
            charcustomerpointsbasedondates.Titles.Clear();
            charcustomerpointsbasedondates.Titles.Add("Customer Points Based on Dates");
        }



        // Tab 2
        private void LoadCartItems()
        {
            int userId = SessionManager.UserID;
            Function.Customer.Customer customerFunctions = new Function.Customer.Customer();
            int customerId = customerFunctions.GetCustomerIdByUserId(userId);

            Cart cart = new Cart();
            List<CartItem> cartItems = cart.GetCartItems(customerId);

            flpcartitems.Controls.Clear();

            foreach (CartItem item in cartItems)
            {
                MainUserControls.CartItem cartItemControl = new MainUserControls.CartItem
                {
                    ItemName = item.ItemName,
                    Price = $"${item.Price}",
                    Quantity = item.Quantity.ToString(),
                    ImagePath = item.ImagePath
                };

                cartItemControl.RemoveButton.Click += (s, e) => RemoveItemFromCart(item.CartItemID);
                cartItemControl.UpdateButton.Click += (s, e) => UpdateItemQuantity(item.CartItemID, (int)cartItemControl.QuantitySelector.Value);

                flpcartitems.Controls.Add(cartItemControl);
            }

            if (cartItems.Count == 0)
            {
                ShowAlert("Your cart is empty.", Color.FromArgb(255, 69, 58));
            }
        }

        private void RemoveItemFromCart(int cartItemId)
        {
            Cart cart = new Cart();
            cart.RemoveFromCart(cartItemId);

            // Reload the cart items after removal
            LoadCartItems();
        }

        private void UpdateItemQuantity(int cartItemId, int newQuantity)
        {
            Cart cart = new Cart();
            cart.UpdateCartItemQuantity(cartItemId, newQuantity);

            // Optionally show an alert that the quantity has been updated
            ShowAlert("Item quantity updated.", Color.FromArgb(94, 148, 255));

            // Reload the cart items after updating the quantity
            LoadCartItems();
        }

        private void PlaceOrderForAllItems()
        {
            int userId = SessionManager.UserID;
            Function.Customer.Customer customers = new Function.Customer.Customer();
            int customerId = customers.GetCustomerIdByUserId(userId);
            string address = customers.GetCustomerAddress(userId);  // Assuming you have a method to get the user's address

            Cart cart = new Cart();
            List<CartItem> cartItems = cart.GetCartItems(customerId);

            if (cartItems.Count == 0)
            {
                ShowAlert("Your cart is empty, nothing to order.", Color.FromArgb(255, 69, 58));
                return;
            }

            List<OrderDetail> orderDetails = new List<OrderDetail>();

            foreach (var item in cartItems)
            {
                orderDetails.Add(new OrderDetail
                {
                    CarID = item.CarID,
                    PartID = item.PartID,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Address = address
                });
            }

            Orders orderFunctions = new Orders();
            int orderId = orderFunctions.InsertOrder(customerId, orderDetails);

            if (orderId > 0)
            {
                ShowAlert("Order placed successfully!", Color.FromArgb(94, 148, 255));
                cart.ClearCart(customerId);
                LoadCartItems(); // Reload the cart items after placing the order
            }
            else
            {
                ShowAlert("Failed to place the order. Please try again.", Color.FromArgb(255, 69, 58));
            }
        }


        private void btnorderall_Click(object sender, EventArgs e)
        {
            if (SessionManager.UserID == 0)
            {
                ShowAlert("You need to log in to place an order.", Color.FromArgb(255, 69, 58));
                return;
            }

            // Confirm the order
            DialogResult result = MessageBox.Show("Are you sure you want to place the order for all items in the cart?",
                                                  "Confirm Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                PlaceOrderForAllItems();
            }
        }

        private void ShowAlert(string message, Color backgroundColor)
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



        // Tab 3
        private void LoadCustomerOrders()
        {
            Orders orders = new Orders();
            int userId = SessionManager.UserID;
            Function.Customer.Customer customerFunctions = new Function.Customer.Customer();
            int customerId = customerFunctions.GetCustomerIdByUserId(userId);

            List<CustomerOrder> customerOrders = orders.GetOrdersByCustomerId(customerId);

            flporders.Controls.Clear();
            foreach (CustomerOrder order in customerOrders)
            {
                MainUserControls.OrderList orderControl = new MainUserControls.OrderList
                {
                    OrderID = order.OrderID.ToString(),
                    OrderDate = order.OrderDate.ToString("dd/MM/yyyy"),
                    TotalAmount = $"${order.TotalAmount}",
                    OrderStatus = order.Status,
                    Address = order.Address
                };

                // Attach event handler for canceling order
                orderControl.CancelOrderButton.Click += (s, e) => CancelCustomerOrder(order.OrderID, orderControl);

                flporders.Controls.Add(orderControl);
            }

            if (customerOrders.Count == 0)
            {
                ShowAlert("You have no orders.", Color.FromArgb(255, 69, 58));
            }
        }

        private void CancelCustomerOrder(int orderId, MainUserControls.OrderList orderControl)
        {
            Orders orders = new Orders();
            bool success = orders.CancelOrder(orderId);

            if (success)
            {
                orderControl.OrderStatus = "Cancelled"; // Update the order status label
                orderControl.CancelOrderButton.Enabled = false; // Disable the cancel button
                ShowAlert("Order has been cancelled successfully.", Color.FromArgb(94, 148, 255));
            }
            else
            {
                ShowAlert("An error occurred while cancelling the order. Please try again.", Color.FromArgb(255, 69, 58));
            }
        }


        // Tab 4
        private void LoadUserInfo()
        {
            int userId = SessionManager.UserID;

            if (userId > 0)
            {
                Users userFunctions = new Users();
                User userInfo = userFunctions.GetUserInfoById(userId);

                if (userInfo != null)
                {
                    txtfirstname.Text = userInfo.FirstName;
                    txtlastname.Text = userInfo.LastName;
                    txtemail.Text = userInfo.Email;
                    txtphone.Text = userInfo.Phone;
                    txtusername.Text = userInfo.Username;
                    txtwpd.Text = userInfo.Password; 

                    if (!string.IsNullOrEmpty(userInfo.ProfileImage))
                    {
                        imgprofilepic.Image = Image.FromFile(Path.Combine("Resources/Profiles", userInfo.ProfileImage));
                    }

                    txtaddress.Text = userInfo.Address;
                }
                else
                {
                    MessageBox.Show("Unable to retrieve user information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnselectimg_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Select a Profile Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedImagePath = openFileDialog.FileName;

                    // Display the selected image in the PictureBox
                    imgprofilepic.Image = Image.FromFile(selectedImagePath);

                    // Optionally store the selected image path for later use when saving
                    txtProfileImagePath.Text = selectedImagePath;
                }
            }
        }

        private void btnsavenow_Click(object sender, EventArgs e)
        {
            Users userFunctions = new Users();
            int userId = SessionManager.UserID;

            User updatedUser = new User
            {
                UserID = userId,
                FirstName = txtfirstname.Text,
                LastName = txtlastname.Text,
                Email = txtemail.Text,
                Phone = txtphone.Text,
                Username = txtusername.Text,
                Password = txtwpd.Text, 
                ProfileImage = Path.GetFileName(txtProfileImagePath.Text), // Save the image name
                Address = txtaddress.Text
            };

            bool success = userFunctions.UpdateUserInfo(updatedUser);

            if (success)
            {
                MessageBox.Show("Profile updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to update profile. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
