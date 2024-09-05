using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABC_Car_Traders.Function;
using ABC_Car_Traders.Function.Admin;
using ABC_Car_Traders.Function.Customer;
using ABC_Car_Traders.GUI.MainUserControls;
using Guna.UI2.WinForms;

namespace ABC_Car_Traders.GUI
{
    public partial class Home : Form
    {
        private HomeFuntions homeFunctions;
        private Cart cartFunctions;

        // Implimenting the dropdown

        public Home()
        {
            InitializeComponent();
            cartFunctions = new Cart(); // Initialize cartFunctions first
            LoadBrands();
            LoadMixedItems();
            DockNavControl();
            LoadUserProfile();
            LoadTotalCartItems(); 
            UpdateLoginLogoutButton();

        }

        // Nav Bar funtonalities Start 

        private void DockNavControl()
        {
            Nav navControl = new Nav(); 
            navControl.Dock = DockStyle.Top;
            this.Controls.Add(navControl);
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
                bntntotalcactitems.Text = totalCartItems > 0 ? totalCartItems.ToString() : "0";
            }
            else
            {
                bntntotalcactitems.Text = "0"; // Show 0 if the user is not logged in
            }
        }


        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            if (SessionManager.UserID != 0)
            {
                // Create an instance of the CartItems form
                GUI.SingleForms.CartItems cartItemsForm = new GUI.SingleForms.CartItems();
                // Load the cart items for the logged-in user
                cartItemsForm.LoadCartItems();
                // Show the cart items form
                cartItemsForm.Show();
            }
            else
            {
                // Display an alert if the user is not logged in
                ShowAlert("You need to log in to view your cart items.", Color.FromArgb(255, 69, 58));
            }
        }

        private void UpdateLoginLogoutButton()
        {
            if (SessionManager.UserID != 0) // Check if user is logged in
            {
                btnloginlogout.Text = "Logout";
                btnloginlogout.Click += BtnLogout_Click;
            }
            else
            {
                btnloginlogout.Text = "Login Now";
                btnloginlogout.Click += BtnLoginNow_Click;
            }
        }

        // Login logut button
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            // Clear the session or log the user out
            SessionManager.EndSession();

            // Update button text to "Login Now"
            btnloginlogout.Text = "Login Now";

            // Optionally, redirect to the login screen
            GUI.Login loginForm = new GUI.Login();
            loginForm.Show();
            this.Hide();
        }

        private void BtnLoginNow_Click(object sender, EventArgs e)
        {
            // Redirect to the login screen
            GUI.Login loginForm = new GUI.Login();
            loginForm.Show();
            this.Hide();
        }

        private void LoadUserProfile()
        {
            // Ensure homeFunctions is initialized before use
            if (homeFunctions == null)
            {
                homeFunctions = new HomeFuntions(); 
            }

            // Check if the user is logged in by checking if UserID is set
            if (SessionManager.UserID != 0)
            {
                int userId = SessionManager.UserID;
                homeFunctions.LoadUserProfileImage(userId, imguserprofile); 
            }
            // If the user is not logged in, do nothing and keep the image as it is
        }

        private void imguserprofile_Click_1(object sender, EventArgs e)
        {
            // Ensure homeFunctions is initialized before use
            if (homeFunctions == null)
            {
                homeFunctions = new HomeFuntions();
            }

            if (SessionManager.UserID != 0)
            {
                homeFunctions.RedirectToProfile(this); 
            }
        }

        // Nav Bar funtonalities End

        private void btnshowparts_Click(object sender, EventArgs e)
        {
            LoadParts();
        }

        private void btnshowvehicles_Click(object sender, EventArgs e)
        {
            LoadCars();
        }

        // Load Brands Start 
        private void LoadBrands()
        {
            Brands brandManager = new Brands();
            List<Brand> brands = brandManager.GetAllVehicles();

            flpbrands.Controls.Clear();
            flpbrands.HorizontalScroll.Visible = false;

            foreach (var brand in brands)
            {
                BrandList brandControl = new BrandList();

                // Construct the correct image path for the brand
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Brands", brand.ImagePath);

                // Check if the image path exists, otherwise set a default image
                if (File.Exists(imagePath))
                {
                    brandControl.SetBrandInfo(brand.BrandName, imagePath); // Use the valid image path
                }
                else
                {
                    // Optional: Provide a default image in case the image doesn't exist
                    string defaultImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Brands", "default.jpg");
                    brandControl.SetBrandInfo(brand.BrandName, defaultImagePath);
                }

                brandControl.Tag = brand.BrandID;
                brandControl.Click += BrandControl_Click;

                flpbrands.Controls.Add(brandControl);
            }
        }


        private void BrandControl_Click(object sender, EventArgs e)
        {
            BrandList clickedBrand = sender as BrandList;
            if (clickedBrand != null)
            {
                int brandID = (int)clickedBrand.Tag;
            }
        }

        // Load Brands End



        // Load Mix Values Start

        private void LoadMixedItems()
        {
            Cars carManager = new Cars();
            List<Car> cars = carManager.GetAllVehicles();

            CarParts partManager = new CarParts();
            List<Part> parts = partManager.GetAllParts();

            flpproducts.Controls.Clear();
            flpproducts.HorizontalScroll.Visible = false;

            int carIndex = 0;
            int partIndex = 0;

            // Loop through both lists and add items to the flow panel
            while (carIndex < cars.Count || partIndex < parts.Count)
            {
                // Add two cars
                for (int i = 0; i < 2 && carIndex < cars.Count; i++, carIndex++)
                {
                    CarList carControl = new CarList();

                    carControl.SetCarInfo(
                        cars[carIndex].CarID,
                        $"{cars[carIndex].Make} {cars[carIndex].Model} ({cars[carIndex].Year})",
                        cars[carIndex].Description,
                        cars[carIndex].Price.ToString("C"),
                        cars[carIndex].ImagePath
                    );

                    carControl.OrderButton.Click += CarOrderNowButton_Click;
                    carControl.AddToCartButton.Click += CarAddToCartButton_Click;
                    carControl.ViewMoreButton.Click += CarViewMoreButton_Click;

                    flpproducts.Controls.Add(carControl);
                }

                // Add two parts
                for (int i = 0; i < 2 && partIndex < parts.Count; i++, partIndex++)
                {
                    PartList partControl = new PartList();

                    partControl.SetPartInfo(
                        parts[partIndex].PartID,
                        parts[partIndex].PartName,
                        parts[partIndex].Description,
                        parts[partIndex].Price.ToString("C"),
                        parts[partIndex].ImagePath
                    );

                    partControl.OrderNowButton.Click += PartOrderNowButton_Click;
                    partControl.AddToCartButton.Click += PartAddToCartButton_Click;
                    partControl.ViewMoreButton.Click += PartViewMoreButton_Click;

                    flpproducts.Controls.Add(partControl);
                }
            }
        }


        // Load Mix Values End


        // Load Cars Start 
        private void LoadCars()
        {
            Cars carManager = new Cars();
            List<Car> cars = carManager.GetAllVehicles();

            flpproducts.Controls.Clear();
            flpproducts.HorizontalScroll.Visible = false;

            foreach (var car in cars)
            {
                CarList carControl = new CarList();

                // Combine the base directory with the image path
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Vehicles", car.ImagePath);

                // Log the image path for debugging
                Console.WriteLine($"Car Image Path: {imagePath}");

                // Set the car info, with fallback for image handled in CarList
                carControl.SetCarInfo(
                    car.CarID,
                    $"{car.Make} {car.Model} ({car.Year})",
                    car.Description,
                    car.Price.ToString("C"),
                    imagePath
                );

                // Assign button event handlers
                carControl.OrderButton.Click += CarOrderNowButton_Click;
                carControl.AddToCartButton.Click += CarAddToCartButton_Click;
                carControl.ViewMoreButton.Click += CarViewMoreButton_Click;

                flpproducts.Controls.Add(carControl);
            }
        }



        private void CarOrderNowButton_Click(object sender, EventArgs e)
        {

            if (SessionManager.UserID != 0) // Check if the user is logged in
            {
                int carID = (int)((Guna2Button)sender).Tag;

                Function.Customer.Customer customers = new Function.Customer.Customer();
                int customerId = customers.GetCustomerIdByUserId(SessionManager.UserID);
                string address = customers.GetCustomerAddress(SessionManager.UserID);

                List<OrderDetail> orderDetails = new List<OrderDetail>
                {
                    new OrderDetail { CarID = carID, Quantity = 1, Price = GetCarPrice(carID), Address = address }
                };

                Orders orderFunctions = new Orders();
                int orderId = orderFunctions.InsertOrder(customerId, orderDetails);

                if (orderId > 0)
                {
                    ShowSuccessAlert("Order placed successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to place the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please log in to place an order.", "Not Logged In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void CarAddToCartButton_Click(object sender, EventArgs e)
        {
            if (SessionManager.UserID != 0) // Check if the user is logged in
            {
                int carID = (int)((Guna2CircleButton)sender).Tag;
                Cart cartFunctions = new Cart();
                cartFunctions.AddToCart(SessionManager.UserID, carID, null, 1);
                LoadTotalCartItems(); // Live update the cart item count

                // Display a success alert
                ShowSuccessAlert("Item added to cart successfully!");
            }
            else
            {
                MessageBox.Show("Please log in to add items to your cart.", "Not Logged In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CarViewMoreButton_Click(object sender, EventArgs e)
        {
            int carID = (int)((Guna2Button)sender).Tag;
            Cars carManager = new Cars();
            Car carDetails = carManager.GetCarDetails(carID);

            if (carDetails != null)
            {
                GUI.SingleForms.Car carForm = new GUI.SingleForms.Car();
                carForm.SetCarDetails(carDetails);
                carForm.ShowDialog(); // Show as a borderless dialog in the center
            }
        }

        // Load Cars End 


        // Load Car Parts Start

        private void LoadParts()
        {
            CarParts partManager = new CarParts();
            List<Part> parts = partManager.GetAllParts();

            flpproducts.Controls.Clear();
            flpproducts.HorizontalScroll.Visible = false;

            foreach (var part in parts)
            {
                PartList partControl = new PartList();

                // Construct the correct image path for the part
                string imageFileName = part.ImagePath;
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Parts", imageFileName + ".jpeg");

                // Check if the image exists, otherwise load a default image
                if (!File.Exists(imagePath) || string.IsNullOrEmpty(imageFileName))
                {
                    imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Parts", "default-part.jpeg");
                }

                // Debugging: Log or display the image path
                Console.WriteLine($"Loading image from path: {imagePath}");
                MessageBox.Show($"Loading image from path: {imagePath}");

                // Assign the image path to the control
                partControl.SetPartInfo(
                    part.PartID,
                    part.PartName,
                    part.Description,
                    part.Price.ToString("C"),
                    imagePath // Pass the constructed image path
                );

                partControl.OrderNowButton.Click += PartOrderNowButton_Click;
                partControl.AddToCartButton.Click += PartAddToCartButton_Click;
                partControl.ViewMoreButton.Click += PartViewMoreButton_Click;

                flpproducts.Controls.Add(partControl);
            }
        }



        private void PartOrderNowButton_Click(object sender, EventArgs e)
        {
            if (SessionManager.UserID != 0) // Check if the user is logged in
            {
                int partID = (int)((Guna2Button)sender).Tag;

                Function.Customer.Customer customers = new Function.Customer.Customer();
                int customerId = customers.GetCustomerIdByUserId(SessionManager.UserID);
                string address = customers.GetCustomerAddress(SessionManager.UserID);

                List<OrderDetail> orderDetails = new List<OrderDetail>
                {
                    new OrderDetail { PartID = partID, Quantity = 1, Price = GetPartPrice(partID), Address = address }
                };

                Orders orderFunctions = new Orders();
                int orderId = orderFunctions.InsertOrder(customerId, orderDetails);

                if (orderId > 0)
                {
                    ShowSuccessAlert("Order placed successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to place the order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please log in to place an order.", "Not Logged In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void PartAddToCartButton_Click(object sender, EventArgs e)
        {
            if (SessionManager.UserID != 0) // Check if the user is logged in
            {
                int partID = (int)((Guna2CircleButton)sender).Tag;
                Cart cartFunctions = new Cart();
                cartFunctions.AddToCart(SessionManager.UserID, null, partID, 1);
                LoadTotalCartItems(); // Live update the cart item count

                // Display a success alert
                ShowSuccessAlert("Item added to cart successfully!");
            }
            else
            {
                MessageBox.Show("Please log in to add items to your cart.", "Not Logged In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void PartViewMoreButton_Click(object sender, EventArgs e)
        {
            int partID = (int)((Guna2Button)sender).Tag;
            CarParts partManager = new CarParts();
            Part partDetails = partManager.GetPartDetails(partID);

            if (partDetails != null)
            {
                GUI.SingleForms.SparePart partForm = new GUI.SingleForms.SparePart();
                partForm.SetPartDetails(partDetails);
                partForm.ShowDialog(); // Show as a borderless dialog in the center
            }
        }

        // Load Car Parts End


        // Helper methods to get car or part prices
        private decimal GetCarPrice(int carId)
        {
            // Implement the logic to get the car price
            return 10000; // Example value
        }

        private decimal GetPartPrice(int partId)
        {
            // Implement the logic to get the part price
            return 100; // Example value
        }

        // Alerts
        private void ShowSuccessAlert(string message)
        {
            Label alertLabel = new Label
            {
                Text = message,
                BackColor = Color.FromArgb(94, 148, 255), // Background color for the alert
                ForeColor = Color.White,
                AutoSize = true,
                Padding = new Padding(10),
                Font = new Font("Segoe UI", 10),
                Location = new Point((this.ClientSize.Width - 200) / 2, 10), // Center the label at the top
                Anchor = AnchorStyles.Top
            };

            this.Controls.Add(alertLabel);
            alertLabel.BringToFront();

            // Timer to fade out the label
            Timer timer = new Timer
            {
                Interval = 50 // Control the speed of the fade
            };

            int opacity = 255; // Start with full opacity

            timer.Tick += (s, e) =>
            {
                opacity -= 15; // Reduce opacity by 15 each tick
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
    }

}
