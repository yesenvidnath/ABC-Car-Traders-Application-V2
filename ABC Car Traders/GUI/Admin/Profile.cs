using ABC_Car_Traders.Function;
using ABC_Car_Traders.Function.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace ABC_Car_Traders.GUI.Admin
{
    public partial class Profile : Form
    {
        private dbConnect dbconnect = new dbConnect();
        private Function.Admin.Admin adminFunctions = new Function.Admin.Admin();

        private int currentUserId = 0;

        public Profile()
        {
            
            InitializeComponent();
            LoadAdminCharts();
            LoadCustomerData();
            LoadAllOrders();
            LoadCarsToGridView();
            LoadBrands();
            LoadPartsToGrid();
            // Hook up the DataGridView click event
            gdvcustomerinfo.CellClick += gdvcustomerinfo_CellClick;
            // Hook up the button click event for image selection
            btnselectimage.Click += btnselectimage_Click_1;
            // Attach the CellClick event to the DataGridView
            gdvparts.CellClick += new DataGridViewCellEventHandler(gdvparts_CellClick);
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            // Clear the session or log the user out
            SessionManager.EndSession();


            GUI.Home HomePge = new GUI.Home();
            HomePge.Show();
            this.Hide();
        }


        // Tab 1
        private void LoadAdminCharts()
        {
            Function.Admin.Admin adminFunctions = new Function.Admin.Admin();

            // Load total orders by date
            DataTable ordersData = adminFunctions.GetTotalOrdersByDate();
            chartotalordersbydate.Series.Clear();
            var ordersSeries = chartotalordersbydate.Series.Add("Total Orders");
            ordersSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            foreach (DataRow row in ordersData.Rows)
            {
                ordersSeries.Points.AddXY(row["OrderDate"].ToString(), row["TotalOrders"]);
            }

            // Load total products by type
            DataTable productsData = adminFunctions.GetTotalProductsByType();
            chartotalproductsbytype.Series.Clear();
            var productsSeries = chartotalproductsbytype.Series.Add("Products");
            productsSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            foreach (DataRow row in productsData.Rows)
            {
                productsSeries.Points.AddXY(row["ProductType"].ToString(), row["TotalProducts"]);
            }

            // Load total earnings by date
            DataTable earningsData = adminFunctions.GetTotalEarningsByDate();
            chartotalearningsbydate.Series.Clear();
            var earningsSeries = chartotalearningsbydate.Series.Add("Total Earnings");
            earningsSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            foreach (DataRow row in earningsData.Rows)
            {
                earningsSeries.Points.AddXY(row["OrderDate"].ToString(), row["TotalEarnings"]);
            }

            // Load total items by brand
            DataTable itemsData = adminFunctions.GetTotalItemsByBrand();
            chartotitemsbybrands.Series.Clear();
            var itemsSeries = chartotitemsbybrands.Series.Add("Total Items");
            itemsSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            foreach (DataRow row in itemsData.Rows)
            {
                itemsSeries.Points.AddXY(row["BrandName"].ToString(), row["TotalItems"]);
            }
        }

        private void LoadAdminDashboardData()
        {
            // Load total customers
            lbltotalcustomers.Text = adminFunctions.GetTotalCustomers().ToString();

            // Load total earnings
            lbltotalearnings.Text = $"${adminFunctions.GetTotalEarnings():F2}";

            // Load total parts
            lbltotalparts.Text = adminFunctions.GetTotalParts().ToString();

            // Load total cars
            lbltotalcars.Text = adminFunctions.GetTotalCars().ToString();
        }



        // Tab 2

        private void gdvcustomerinfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the click is on a valid row, not the header or empty space
            if (e.RowIndex >= 0)
            {
                // Get the current row
                DataGridViewRow row = gdvcustomerinfo.Rows[e.RowIndex];

                // Load the data into the respective fields
                txtfname.Text = row.Cells["FirstName"].Value.ToString();
                txtlname.Text = row.Cells["LastName"].Value.ToString();
                txtemailname.Text = row.Cells["Email"].Value.ToString();
                txtphoneno.Text = row.Cells["Phone"].Value.ToString();
                txtaddresssuer.Text = row.Cells["Address"].Value.ToString();
                txtusernameofuser.Text = row.Cells["Username"].Value.ToString();
                txtpwdofuser.Text = row.Cells["Password"].Value.ToString(); // Password loaded
                combtype.SelectedItem = row.Cells["Role"].Value.ToString(); // Load Role

                // Handle Profile Image
                string imagePath = row.Cells["ProfileImage"].Value != null ? row.Cells["ProfileImage"].Value.ToString() : null;
                string imageDirectory = @"C:\Users\REDTECH\Desktop\Education\TopUP\ASE\Project\ABC Car Traders\ABC Car Traders\bin\Debug\Resources\Profiles";

                // Combine the directory with the image file name
                string fullImagePath = !string.IsNullOrEmpty(imagePath) ? Path.Combine(imageDirectory, imagePath) : null;

                if (!string.IsNullOrEmpty(fullImagePath) && File.Exists(fullImagePath))
                {
                    // If the image file exists, load it into the PictureBox
                    imguserimgofuser.Image = Image.FromFile(fullImagePath);
                }
                else
                {
                    // Load a default image if no profile image exists or if the file is missing
                    string defaultImagePath = Path.Combine(imageDirectory, "default_profile.png"); // Ensure you have a default image in this location

                    if (File.Exists(defaultImagePath))
                    {
                        imguserimgofuser.Image = Image.FromFile(defaultImagePath); // Load the default image
                    }
                    else
                    {
                        // If the default image is also missing, display a placeholder
                        imguserimgofuser.Image = null; // You can also assign a placeholder image if needed
                        MessageBox.Show("Profile image not found and no default image available.", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }



        private void LoadCustomerData()
        {
            // Clear existing columns if necessary
            if (gdvcustomerinfo.Columns.Count == 0)
            {
                // Add columns to the DataGridView
                gdvcustomerinfo.Columns.Add("UserID", "User ID");
                gdvcustomerinfo.Columns.Add("FirstName", "First Name");
                gdvcustomerinfo.Columns.Add("LastName", "Last Name");
                gdvcustomerinfo.Columns.Add("Email", "Email");
                gdvcustomerinfo.Columns.Add("Phone", "Phone");
                gdvcustomerinfo.Columns.Add("Username", "Username");
                gdvcustomerinfo.Columns.Add("Password", "Password"); // Password column
                gdvcustomerinfo.Columns.Add("Role", "Role");
                gdvcustomerinfo.Columns.Add("Address", "Address");
                gdvcustomerinfo.Columns.Add("ProfileImage", "Profile Image"); // Profile image column
            }

            // Clear existing data from the DataGridView
            gdvcustomerinfo.Rows.Clear();

            // Create an instance of the Admin class
            Function.Admin.Admin adminFunctions = new Function.Admin.Admin();  // Ensure admin is instantiated here

            // Fetch all customer data
            List<User> customers = adminFunctions.GetAllCustomers();  // Use adminFunctions here

            // Add each customer as a row in the DataGridView
            foreach (User customer in customers)
            {
                gdvcustomerinfo.Rows.Add(
                    customer.UserID,
                    customer.FirstName,
                    customer.LastName,
                    customer.Email,
                    customer.Phone,
                    customer.Username,
                    customer.Password, // Password displayed
                    customer.Role,
                    customer.Address,
                    customer.ProfileImage // Profile image URL
                );
            }
        }


        private void btnselectimage_Click_1(object sender, EventArgs e)
        {
            // Open a file dialog to select an image
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Select Profile Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Load the selected image into the PictureBox
                    imguserimgofuser.Image = Image.FromFile(openFileDialog.FileName);

                    // Optionally, store the image path in a hidden field or property for later use when saving
                    txtImagePath.Text = openFileDialog.FileName; // Store the image path for saving
                }
            }
        }


        private void btninsert_Click(object sender, EventArgs e)
        {
            // Instantiate the Admin class
            Function.Admin.Admin adminFunctions = new Function.Admin.Admin();

            User newUser = new User
            {
                FirstName = txtfname.Text,
                LastName = txtlname.Text,
                Email = txtemailname.Text,
                Phone = txtphoneno.Text,
                Username = txtusernameofuser.Text,
                Password = txtpwdofuser.Text, // Ensure password is hashed
                Role = combtype.SelectedItem.ToString(),
                ProfileImage = imguserimgofuser.ImageLocation,
                Address = txtaddresssuer.Text
            };

            int userId = adminFunctions.InsertUser(newUser);

            if (userId > 0)
            {
                ShowAlert("User inserted successfully!", Color.FromArgb(94, 148, 255));
                RefreshCustomerGrid();
            }
            else
            {
                ShowAlert("Error inserting user.", Color.FromArgb(255, 69, 58));
                RefreshCustomerGrid();
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            // Instantiate the Admin class
            Function.Admin.Admin adminFunctions = new Function.Admin.Admin();

            User updatedUser = new User
            {
                UserID = int.Parse(gdvcustomerinfo.CurrentRow.Cells["UserID"].Value.ToString()), // Assuming UserID is in the DataGridView
                FirstName = txtfname.Text,
                LastName = txtlname.Text,
                Email = txtemailname.Text,
                Phone = txtphoneno.Text,
                Username = txtusernameofuser.Text,
                Password = txtpwdofuser.Text, // Ensure password is hashed appropriately
                Role = combtype.SelectedItem.ToString(),
                ProfileImage = !string.IsNullOrEmpty(imguserimgofuser.ImageLocation) ? imguserimgofuser.ImageLocation : null, // Handle null profile image
                Address = txtaddresssuer.Text
            };

            // Call the UpdateUser method and store the result
            bool updateSuccess = adminFunctions.UpdateUser(updatedUser);

            // Based on the result, show either a success or error alert
            if (updateSuccess)
            {
                ShowAlert("User updated successfully!", Color.FromArgb(94, 148, 255)); // Show success alert
                RefreshCustomerGrid();
            }
            else
            {
                ShowAlert("Image was missing but User updated successfully!", Color.FromArgb(94, 148, 255)); // Show error alert if update fails
                RefreshCustomerGrid();
            }
        }


        private void btndelete_Click(object sender, EventArgs e)
        {
            // Instantiate the Admin class
            Function.Admin.Admin adminFunctions = new Function.Admin.Admin();

            int userId = int.Parse(gdvcustomerinfo.CurrentRow.Cells["UserID"].Value.ToString()); // Assuming UserID is in the DataGridView

            DialogResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                bool success = adminFunctions.DeleteUser(userId);

                if (success)
                {
                    ShowAlert("User deleted successfully!", Color.FromArgb(94, 148, 255));
                    RefreshCustomerGrid();
                }
                else
                {
                    ShowAlert("Error deleting user.", Color.FromArgb(255, 69, 58));
                    RefreshCustomerGrid();
                }
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            ClearFormFields();
        }



        // Method to clear form fields
        private void ClearFormFields()
        {
            txtfname.Clear();
            txtlname.Clear();
            txtemailname.Clear();
            txtphoneno.Clear();
            txtaddresssuer.Clear();
            txtusernameofuser.Clear();
            txtpwdofuser.Clear();
            txtImagePath.Clear();
            combtype.SelectedIndex = -1;
            imguserimgofuser.ImageLocation = null;
        }


        private void RefreshCustomerGrid()
        {
            // Clear the existing rows to avoid duplication
            gdvcustomerinfo.Rows.Clear();

            // Instantiate the Admin class
            Function.Admin.Admin adminFunctions = new Function.Admin.Admin();

            // Fetch all customer data
            List<User> customers = adminFunctions.GetAllCustomers();

            // Re-populate the DataGridView
            foreach (var customer in customers)
            {
                gdvcustomerinfo.Rows.Add(
                    customer.UserID,
                    customer.FirstName,
                    customer.LastName,
                    customer.Email,
                    customer.Phone,
                    customer.Username,
                    customer.Password,
                    customer.Role,
                    customer.Address,
                    customer.ProfileImage
                );
            }

            // Optionally, show an alert or message indicating the grid has been refreshed
            ShowAlert("Customer grid refreshed.", Color.FromArgb(94, 148, 255));
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
                Location = new Point((this.ClientSize.Width - 300) / 2, 10), // Center the label at the top
                Anchor = AnchorStyles.Top
            };

            this.Controls.Add(alertLabel);
            alertLabel.BringToFront();

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

        private void btnsearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtcustomersearch.Text.Trim();
            Function.Admin.Admin adminFunctions = new Function.Admin.Admin();

            // If the search field is empty, refresh the grid with all users
            if (string.IsNullOrEmpty(searchQuery))
            {
                RefreshCustomerGrid();
            }
            else
            {
                // Fetch all customers and sort them by UserID
                List<User> sortedCustomers = adminFunctions.GetAllCustomersSorted();

                // Perform binary search based on the search query
                User foundUser = adminFunctions.BinarySearchUsers(sortedCustomers, searchQuery);

                // If a user is found, display only that user in the grid
                if (foundUser != null)
                {
                    gdvcustomerinfo.Rows.Clear();
                    gdvcustomerinfo.Rows.Add(
                        foundUser.UserID,
                        foundUser.FirstName,
                        foundUser.LastName,
                        foundUser.Email,
                        foundUser.Phone,
                        foundUser.Username,
                        foundUser.Password,
                        foundUser.Role,
                        foundUser.Address,
                        foundUser.ProfileImage
                    );
                }
                else
                {
                    ShowAlert("No user found with the given ID or username.", Color.FromArgb(255, 69, 58));
                }
            }
        }



        // Tab 3


        private void btnsearchorder_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtsearchbarproducts.Text, out int searchOrderId))
            {
                Orders orders = new Orders();
                CustomerOrder order = orders.GetOrderByOrderId(searchOrderId); // Use binary search to find the order

                flporders.Controls.Clear(); // Clear the previous orders from the flow panel

                if (order != null)
                {
                    MainUserControls.AdminControllers.OrdersAdmin orderControl = new MainUserControls.AdminControllers.OrdersAdmin
                    {
                        OrderID = order.OrderID.ToString(),
                        OrderDate = order.OrderDate.ToString("dd/MM/yyyy"),
                        TotalAmount = $"${order.TotalAmount}",
                        OrderStatus = order.Status
                    };

                    // Attach event handler for changing order status
                    orderControl.BtnChangeStatus.Click += (s, ev) => ChangeOrderStatus(order.OrderID, orderControl); // Renamed to 'ev'

                    // Make the user control clickable to load the order details
                    orderControl.Click += (s, ev) => LoadOrderDetails(order.OrderID); // Renamed to 'ev'

                    // Also make the individual labels clickable to trigger order details
                    foreach (Control control in orderControl.Controls)
                    {
                        control.Click += (s, ev) => LoadOrderDetails(order.OrderID); // Renamed to 'ev'
                    }

                    flporders.Controls.Add(orderControl); // Add the filtered order to the flow panel
                }
                else
                {
                    ShowAlert("Order not found.", Color.FromArgb(255, 69, 58)); // Show an alert if no order is found
                }
            }
            else
            {
                ShowAlert("Invalid Order ID format.", Color.FromArgb(255, 69, 58)); // Show an alert for invalid input
            }
        }


        // Load all orders in the Admin panel
        private void LoadAllOrders()
        {
            Orders orders = new Orders();
            List<CustomerOrder> allOrders = orders.GetAllOrders();

            flporders.Controls.Clear();

            foreach (CustomerOrder order in allOrders)
            {
                MainUserControls.AdminControllers.OrdersAdmin orderControl = new MainUserControls.AdminControllers.OrdersAdmin
                {
                    OrderID = order.OrderID.ToString(),
                    OrderDate = order.OrderDate.ToString("dd/MM/yyyy"),
                    TotalAmount = $"${order.TotalAmount}",
                    OrderStatus = order.Status
                };

                // Attach event handler for changing order status
                orderControl.BtnChangeStatus.Click += (s, e) => ChangeOrderStatus(order.OrderID, orderControl);

                // Make the user control clickable to load the order details
                orderControl.Click += (s, e) => LoadOrderDetails(order.OrderID);

                // Also make the individual labels clickable to trigger order details
                foreach (Control control in orderControl.Controls)
                {
                    control.Click += (s, e) => LoadOrderDetails(order.OrderID);
                }

                flporders.Controls.Add(orderControl);
            }

            if (allOrders.Count == 0)
            {
                ShowAlert("No orders found.", Color.FromArgb(255, 69, 58));
            }
        }

        // Method to change the status of the order
        private void ChangeOrderStatus(int orderId, MainUserControls.AdminControllers.OrdersAdmin orderControl)
        {
            string newStatus = orderControl.CombStatus.SelectedItem.ToString();
            Orders orders = new Orders();
            bool success = orders.UpdateOrderStatus(orderId, newStatus);

            if (success)
            {
                orderControl.OrderStatus = newStatus; // Update the status label
                ShowAlert("Order status updated successfully.", Color.FromArgb(94, 148, 255));
            }
            else
            {
                ShowAlert("Error updating order status.", Color.FromArgb(255, 69, 58));
            }
        }

        // Load the order details in the flow panel
        private void LoadOrderDetails(int orderId)
        {
            Orders orders = new Orders();
            List<OrderDetail> orderDetails = orders.GetOrderDetailsByOrderId(orderId);

            flporderdetails.Controls.Clear();

            foreach (var detail in orderDetails)
            {
                Label lblProduct = new Label
                {
                    Text = detail.ProductName,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(0, 122, 204),
                    Margin = new Padding(10),
                };

                Label lblQuantity = new Label
                {
                    Text = "Quantity: " + detail.Quantity,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Padding = new Padding(5, 0, 5, 5)
                };

                Label lblPrice = new Label
                {
                    Text = "Price: $" + detail.Price,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Padding = new Padding(5, 0, 5, 5)
                };

                Label separator = new Label
                {
                    BorderStyle = BorderStyle.Fixed3D,
                    Height = 2,
                    Width = flporderdetails.Width - 20,
                    AutoSize = false,
                    Margin = new Padding(0, 10, 0, 10),
                    BackColor = Color.FromArgb(230, 230, 230)
                };

                flporderdetails.Controls.Add(lblProduct);
                flporderdetails.Controls.Add(lblQuantity);
                flporderdetails.Controls.Add(lblPrice);
                flporderdetails.Controls.Add(separator);
            }
        }




        // Load filtered orders based on status (Pending, Cancelled, All)
        private void LoadFilteredOrders(string statusFilter = null)
        {
            Orders orders = new Orders();
            List<CustomerOrder> allOrders = orders.GetAllOrders();

            flporders.Controls.Clear();

            // Filter orders based on the status if a filter is provided
            IEnumerable<CustomerOrder> filteredOrders = allOrders;
            if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "All")
            {
                filteredOrders = allOrders.Where(order => order.Status == statusFilter);
            }

            foreach (CustomerOrder order in filteredOrders)
            {
                MainUserControls.AdminControllers.OrdersAdmin orderControl = new MainUserControls.AdminControllers.OrdersAdmin
                {
                    OrderID = order.OrderID.ToString(),
                    OrderDate = order.OrderDate.ToString("dd/MM/yyyy"),
                    TotalAmount = $"${order.TotalAmount}",
                    OrderStatus = order.Status
                };

                // Attach event handler for changing order status
                orderControl.BtnChangeStatus.Click += (s, e) => ChangeOrderStatus(order.OrderID, orderControl);

                // Make the user control clickable to load the order details
                orderControl.Click += (s, e) => LoadOrderDetails(order.OrderID);

                // Also make the individual labels clickable to trigger order details
                foreach (Control control in orderControl.Controls)
                {
                    control.Click += (s, e) => LoadOrderDetails(order.OrderID);
                }

                flporders.Controls.Add(orderControl);
            }

            if (!filteredOrders.Any())
            {
                ShowAlert("No orders found.", Color.FromArgb(255, 69, 58));
            }
        }

        private void btncanceledorders_Click(object sender, EventArgs e)
        {
            LoadFilteredOrders("Cancelled");
        }

        private void btnpendingorders_Click(object sender, EventArgs e)
        {
            LoadFilteredOrders("Pending");
        }

        private void btnall_Click(object sender, EventArgs e)
        {
            LoadFilteredOrders("All");
        }




        // Tab 4
        private void LoadBrands()
        {
            Brands brands = new Brands();
            List<Brand> brandList = brands.GetAllBrands();

            comboBrands.Items.Clear(); // Clear existing items
            foreach (var brand in brandList)
            {
                comboBrands.Items.Add(new { Text = brand.BrandName, Value = brand.BrandID });
            }

            comboBrands.DisplayMember = "Text";  // Set display member for ComboBox
            comboBrands.ValueMember = "Value";  // Set value member for ComboBox
        }


        private void LoadCarsToGridView()
        {
            Cars cars = new Cars();
            List<Car> carList = cars.GetAllCars();

            gdvcars.Rows.Clear(); // Clear the DataGridView before adding new rows
            gdvcars.Columns.Clear(); // Clear any existing columns

            // Define columns programmatically
            gdvcars.Columns.Add("CarID", "Car ID");
            gdvcars.Columns.Add("BrandID", "Brand ID");
            gdvcars.Columns.Add("Model", "Model");
            gdvcars.Columns.Add("Make", "Make");
            gdvcars.Columns.Add("Year", "Year");
            gdvcars.Columns.Add("Price", "Price");
            gdvcars.Columns.Add("QuantityAvailable", "Quantity Available");
            gdvcars.Columns.Add("Description", "Description");

            foreach (Car car in carList)
            {
                gdvcars.Rows.Add(
                    car.CarID,
                    car.BrandID,
                    car.Model ?? "N/A", // Handle null values
                    car.Make ?? "N/A", // Handle null values
                    car.Year,
                    car.Price,
                    car.QuantityAvailable,
                    car.Description ?? "N/A" // Handle null values
                );
            }

            // Make rows clickable to load car details
            gdvcars.CellClick += Gdvcars_CellClick;
        }

        private void Gdvcars_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is clicked
            {
                int carId = Convert.ToInt32(gdvcars.Rows[e.RowIndex].Cells["CarID"].Value);
                Cars cars = new Cars();
                Car car = cars.GetCarDetails(carId);

                if (car != null)
                {
                    // Assuming you have text fields or labels to display car details
                    txtModel.Text = car.Model;
                    txtMake.Text = car.Make;
                    txtYear.Text = car.Year.ToString();
                    txtPrice.Text = car.Price.ToString();
                    txtQuantity.Text = car.QuantityAvailable.ToString();
                    txtDescription.Text = car.Description;

                    // Handle car image loading
                    if (!string.IsNullOrEmpty(car.ImagePath))
                    {
                        string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Vehicles", car.ImagePath);
                        if (File.Exists(imagePath))
                        {
                            imgcarimg.Image = Image.FromFile(imagePath);
                        }
                        else
                        {
                            imgcarimg.Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Vehicles", "car.jpg"));
                        }
                    }
                    else
                    {
                        imgcarimg.Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Vehicles", "car.jpg"));
                    }

                    // Autoselect the correct brand in the comboBrands
                    comboBrands.SelectedIndex = -1; // Clear any current selection
                    foreach (var item in comboBrands.Items)
                    {
                        var brand = (dynamic)item;  // Cast the item to dynamic to access Text and Value
                        if (brand.Value == car.BrandID) // Match the BrandID
                        {
                            comboBrands.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
        }

        private void btnvehicleinsert_Click(object sender, EventArgs e)
        {
            Function.Admin.Cars cars = new Function.Admin.Cars();
            Car newCar = GetVehicleDataFromForm();

            // Handle image saving
            if (imgcarimg.Image != null)
            {
                string imageFileName = $"{Guid.NewGuid()}.jpg"; // Generate a unique name for the image
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Vehicles", imageFileName);
                imgcarimg.Image.Save(imagePath);
                newCar.ImagePath = imageFileName; // Store the image filename
            }

            int carId = cars.InsertVehicle(newCar);
            if (carId > 0)
            {
                ShowAlert("Vehicle inserted successfully.", Color.FromArgb(94, 148, 255));
                RefreshVehicleGrid();
            }
            else
            {
                ShowAlert("Error inserting vehicle.", Color.FromArgb(255, 69, 58));
            }
        }

        private void btnvehicleupdate_Click(object sender, EventArgs e)
        {
            if (gdvcars.SelectedRows.Count > 0)
            {
                try
                {
                    Function.Admin.Cars cars = new Function.Admin.Cars();
                    Car updatedCar = GetVehicleDataFromForm();

                    // Get the selected CarID from the first selected row
                    updatedCar.CarID = Convert.ToInt32(gdvcars.SelectedRows[0].Cells["CarID"].Value);

                    // Handle image update
                    if (imgcarimg.Image != null)
                    {
                        string imageFileName = $"{Guid.NewGuid()}.jpg";
                        string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Vehicles", imageFileName);
                        imgcarimg.Image.Save(imagePath);
                        updatedCar.ImagePath = imageFileName;
                    }

                    bool success = cars.UpdateVehicle(updatedCar);
                    if (success)
                    {
                        ShowAlert("Vehicle updated successfully.", Color.FromArgb(94, 148, 255));
                        RefreshVehicleGrid();
                    }
                    else
                    {
                        ShowAlert("Error updating vehicle.", Color.FromArgb(255, 69, 58));
                    }
                }
                catch (Exception ex)
                {
                    ShowAlert($"Error: {ex.Message}", Color.FromArgb(255, 69, 58));
                }
            }
            else
            {
                // Show an alert if no row is selected
                ShowAlert("Please select a vehicle to update.", Color.FromArgb(255, 69, 58));
            }
        }


        // Method to delete a vehicle

        private void btnvehicledalete_Click_1(object sender, EventArgs e)
        {
            if (gdvcars.SelectedRows.Count > 0)
            {
                int carId = Convert.ToInt32(gdvcars.SelectedRows[0].Cells["CarID"].Value);

                Cars cars = new Cars();
                bool success = cars.DeleteVehicle(carId);

                if (success)
                {
                    ShowAlert("Vehicle deleted successfully.", Color.FromArgb(94, 148, 255));
                    RefreshVehicleGrid();
                }
                else
                {
                    ShowAlert("Error deleting vehicle.", Color.FromArgb(255, 69, 58));
                }
            }
            else
            {
                ShowAlert("Please select a vehicle to delete.", Color.FromArgb(255, 69, 58));
            }
        }

        private void btnvehicledaleteclear_Click(object sender, EventArgs e)
        {
            ClearVehicleForm();
        }

        // Helper method to get data from the form
        private Car GetVehicleDataFromForm()
        {
            return new Car
            {
                BrandID = Convert.ToInt32(comboBrands.SelectedValue),
                Model = txtModel.Text,
                Make = txtMake.Text,
                Year = Convert.ToInt32(txtYear.Text),
                Price = Convert.ToDecimal(txtPrice.Text),
                QuantityAvailable = Convert.ToInt32(txtQuantity.Text),
                Description = txtDescription.Text
            };
        }


        // Helper method to clear the form fields
        private void ClearVehicleForm()
        {
            comboBrands.SelectedIndex = -1;
            txtModel.Clear();
            txtMake.Clear();
            txtYear.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtDescription.Clear();
            imgcarimg.Image = null;
        }

        // Method to refresh the grid after operations
        private void RefreshVehicleGrid()
        {
            gdvcars.DataSource = null;
            LoadCarsToGridView(); 
        }



        // Tab 5
        private void LoadPartsToGrid()
        {
            // Create a new instance of the CarParts class to fetch data
            Function.Admin.CarParts partsManager = new Function.Admin.CarParts();
            List<Part> parts = partsManager.GetAllParts_Admin(); // Fetch data from the database

            // Clear any existing columns and rows in the DataGridView
            gdvparts.Columns.Clear();
            gdvparts.Rows.Clear();

            // Add columns to DataGridView
            gdvparts.Columns.Add("PartID", "Part ID");
            gdvparts.Columns.Add("BrandID", "Brand ID");
            gdvparts.Columns.Add("PartName", "Part Name");
            gdvparts.Columns.Add("Description", "Description");
            gdvparts.Columns.Add("Price", "Price");
            gdvparts.Columns.Add("QuantityAvailable", "Quantity Available");
            gdvparts.Columns.Add("Image", "Image");
            gdvparts.Columns.Add("CarID", "Car ID");

            // Add rows to DataGridView
            foreach (var part in parts)
            {
                gdvparts.Rows.Add(
                    part.PartID,
                    part.BrandID,
                    part.PartName,
                    part.Description,
                    part.Price,
                    part.QuantityAvailable,
                    part.ImagePath, // Image path
                    part.CarID
                );
            }
        }


        private void SetPartDetailsToForm(DataGridViewRow row)
        {
            // Safely handle null values while setting the data to the form fields
            txtBrandPartName.Text = row.Cells["PartName"].Value?.ToString() ?? string.Empty;
            txtbranddiscription.Text = row.Cells["Description"].Value?.ToString() ?? string.Empty;
            txtbrandprice.Text = row.Cells["Price"].Value?.ToString() ?? string.Empty;
            txtQuantityAvailable.Text = row.Cells["QuantityAvailable"].Value?.ToString() ?? string.Empty;

            // Load image safely
            string imagePath = row.Cells["Image"].Value?.ToString();
            if (!string.IsNullOrEmpty(imagePath))
            {
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Parts", imagePath);
                if (File.Exists(fullPath))
                {
                    imgPartImage.Image = Image.FromFile(fullPath);
                }
                else
                {
                    imgPartImage.Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Parts", "default-part.jpg"));
                }
            }
            else
            {
                imgPartImage.Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Parts", "default-part.jpg"));
            }

            // Select the correct brand in the combo box
            if (row.Cells["BrandID"].Value != null)
            {
                int brandID = Convert.ToInt32(row.Cells["BrandID"].Value);
                comboBrands.SelectedValue = brandID;
            }

            // Handle the car selection in the combo box (if applicable)
            if (row.Cells["CarID"].Value != null)
            {
                int carID = Convert.ToInt32(row.Cells["CarID"].Value);
                guna2ComboBox2.SelectedValue = carID;
            }
        }


        // This is the event handler when a row or cell is clicked
        private void gdvparts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is clicked
            {
                // Get the clicked row
                DataGridViewRow row = gdvparts.Rows[e.RowIndex];

                // Set form fields from the selected row
                txtBrandPartName.Text = row.Cells["PartName"].Value?.ToString() ?? string.Empty;
                txtbranddiscription.Text = row.Cells["Description"].Value?.ToString() ?? string.Empty;
                txtbrandprice.Text = row.Cells["Price"].Value?.ToString() ?? string.Empty;
                txtQuantityAvailable.Text = row.Cells["QuantityAvailable"].Value?.ToString() ?? string.Empty;

                // Load the Brand into the combo box
                if (row.Cells["BrandID"].Value != null)
                {
                    int brandID = Convert.ToInt32(row.Cells["BrandID"].Value);

                    // Set the selected value of the combo box to the BrandID
                    txtbrandcombbrand.SelectedValue = brandID; // Ensure the correct brand is selected
                }

                // Handle image loading
                if (row.Cells["Image"].Value != null && !string.IsNullOrEmpty(row.Cells["Image"].Value.ToString()))
                {
                    string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Parts", row.Cells["Image"].Value.ToString());
                    if (File.Exists(imagePath))
                    {
                        imgPartImage.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        imgPartImage.Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Parts", "default-part.jpg"));
                    }
                }
                else
                {
                    imgPartImage.Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Parts", "default-part.jpg"));
                }

                // Handle CarID if applicable
                if (row.Cells["CarID"].Value != null)
                {
                    int carID = Convert.ToInt32(row.Cells["CarID"].Value);
                    guna2ComboBox2.SelectedValue = carID;
                }
            }
        }


        // Call this method after any CRUD operation
        private void RefreshPartsGrid()
        {
            Function.Admin.CarParts carParts = new Function.Admin.CarParts();
            List<Part> parts = carParts.GetAllParts_Admin();

            gdvparts.Rows.Clear(); // Clear existing rows
            foreach (var part in parts)
            {
                gdvparts.Rows.Add(part.PartID, part.BrandID, part.PartName, part.Description, part.Price, part.QuantityAvailable, part.ImagePath, part.CarID);
            }
        }


        // Call this method to load the brands into the combobox
        private void LoadBrandsIntoComboBox()
        {
            Function.Admin.Brands brandManager = new Function.Admin.Brands();
            List<Function.Admin.Brand> brandList = brandManager.GetAllBrands();

            txtbrandcombbrand.DisplayMember = "BrandName";  // What the user sees
            txtbrandcombbrand.ValueMember = "BrandID";      // What is stored in the value
            txtbrandcombbrand.DataSource = brandList;
        }



        // Tab 6
        private void btngenerateuserreport_Click(object sender, EventArgs e)
        {
            // Create an instance of the Reports class
            Function.Admin.Reports reportManager = new Function.Admin.Reports();

            // Fetch all users for the report
            List<User> users = reportManager.GetAllUsersForReport();

            // Display the report in the flow panel (flpviwereportusers)
            reportManager.DisplayReportInFlowPanel(flpviwereportusers, users);
        }

        private void btnexportuserreport_Click(object sender, EventArgs e)
        {
            // Create an instance of the Reports class
            Function.Admin.Reports reportManager = new Function.Admin.Reports();

            // Fetch all users for the report
            List<User> users = reportManager.GetAllUsersForReport();

            // Generate the HTML report content
            string htmlReport = reportManager.GenerateUserReportHtml(users);

            // Save the report as an HTML file
            reportManager.SaveReportToHtml(htmlReport);
        }

        private void btngenerateproductreport_Click(object sender, EventArgs e)
        {
            Reports reportManager = new Reports();
            List<CarPartReportItem> reportItems = reportManager.GetAllCarsAndPartsForReport();

            reportManager.DisplayReportInFlowPanel(flpviweproductreports, reportItems); // Preview in FlowLayoutPanel
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Reports reportManager = new Reports();
            List<CarPartReportItem> reportItems = reportManager.GetAllCarsAndPartsForReport();

            string htmlReport = reportManager.GenerateHTMLReport(reportItems);
            reportManager.SaveHTMLReportToFile(htmlReport); // Export to file
        }

        private void btngenerateOrdertreport_Click(object sender, EventArgs e)
        {
            Reports reportManager = new Reports();
            List<OrderReportItem> reportItems = reportManager.GetOrdersWithCustomers();

            reportManager.DisplayOrderReportInFlowPanel(flpviweproductreports, reportItems); // Preview in FlowLayoutPanel
        }

        private void btnexportOrderreport_Click(object sender, EventArgs e)
        {
            Reports reportManager = new Reports();
            List<OrderReportItem> reportItems = reportManager.GetOrdersWithCustomers();

            string htmlReport = reportManager.GenerateOrderReportHTML(reportItems);
            reportManager.SaveOrderReportToFile(htmlReport); // Export to file
        }

    }

}

