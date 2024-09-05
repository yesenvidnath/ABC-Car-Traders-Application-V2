using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;

namespace ABC_Car_Traders.Function.Admin
{
    public class Reports
    {
        private dbConnect db = new dbConnect();


        private Users userManager = new Users();

        // Method to get all users for the report
        public List<User> GetAllUsersForReport()
        {
            return userManager.GetAllUsers(); // Assuming there's a GetAllUsers method in the Users class
        }


        public string GenerateUserReportHtml(List<User> users)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            // Add custom CSS for a professional, modern look
            htmlBuilder.AppendLine("<html><head><style>");
            htmlBuilder.AppendLine(@"
                body {
                    font-family: Arial, sans-serif;
                    background-color: #f9f9f9;
                    margin: 0;
                    padding: 20px;
                }
                h1 {
                    text-align: center;
                    color: #2c3e50;
                }
                table {
                    width: 100%;
                    margin: 0 auto;
                    border-collapse: collapse;
                    box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
                }
                th, td {
                    padding: 12px;
                    text-align: center;
                    border: 1px solid #ddd;
                }
                th {
                    background-color: #2980b9;
                    color: white;
                }
                tr:nth-child(even) {
                    background-color: #f2f2f2;
                }
                tr:hover {
                    background-color: #ddd;
                }
                img {
                    width: 60px;
                    height: 60px;
                    object-fit: cover;
                }
            ");
            htmlBuilder.AppendLine("</style></head><body>");

            // Start the HTML content
            htmlBuilder.AppendLine("<h1>User Report</h1>");
            htmlBuilder.AppendLine("<table>");
            htmlBuilder.AppendLine("<thead><tr><th>UserID</th><th>First Name</th><th>Last Name</th><th>Email</th><th>Phone</th><th>Username</th><th>Role</th><th>Address</th></tr></thead>");
            htmlBuilder.AppendLine("<tbody>");

            // Add user data into the table
            foreach (var user in users)
            {
                string imagePath = string.IsNullOrEmpty(user.ProfileImage) ? "No Image" : user.ProfileImage;
                htmlBuilder.AppendLine($"<tr><td>{user.UserID}</td><td>{user.FirstName}</td><td>{user.LastName}</td><td>{user.Email}</td><td>{user.Phone}</td><td>{user.Username}</td><td>{user.Role}</td><td>{user.Address}</td></tr>");
            }

            // Close the table and body
            htmlBuilder.AppendLine("</tbody></table>");
            htmlBuilder.AppendLine("</body></html>");

            return htmlBuilder.ToString();
        }



        public void SaveReportToHtml(string htmlContent)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "HTML files (*.html)|*userReport.html";
                saveFileDialog.DefaultExt = "UserReport";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, htmlContent);
                    MessageBox.Show("Report saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        public void DisplayReportInFlowPanel(FlowLayoutPanel flowPanel, List<User> users)
        {
            flowPanel.Controls.Clear();

            // Create a title label
            Label titleLabel = new Label
            {
                Text = "User Report Preview",
                Font = new Font("Arial", 14, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(10),
                ForeColor = Color.Black, // For clearer title text
            };
            flowPanel.Controls.Add(titleLabel);

            // Create a table with proper column definitions
            TableLayoutPanel table = new TableLayoutPanel
            {
                ColumnCount = 8, // Adjust to match the fields
                AutoSize = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                Padding = new Padding(5) // Add padding to make the layout cleaner
            };

            // Add table headers with proper alignment and font styles
            table.Controls.Add(new Label { Text = "UserID", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5), TextAlign = ContentAlignment.MiddleCenter });
            table.Controls.Add(new Label { Text = "FirstName", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5), TextAlign = ContentAlignment.MiddleCenter });
            table.Controls.Add(new Label { Text = "LastName", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5), TextAlign = ContentAlignment.MiddleCenter });
            table.Controls.Add(new Label { Text = "Email", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5), TextAlign = ContentAlignment.MiddleCenter });
            table.Controls.Add(new Label { Text = "Phone", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5), TextAlign = ContentAlignment.MiddleCenter });
            table.Controls.Add(new Label { Text = "Username", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5), TextAlign = ContentAlignment.MiddleCenter });
            table.Controls.Add(new Label { Text = "Role", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5), TextAlign = ContentAlignment.MiddleCenter });
            table.Controls.Add(new Label { Text = "Address", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5), TextAlign = ContentAlignment.MiddleCenter });

            // Add user data rows with proper spacing and centering
            foreach (var user in users)
            {
                table.Controls.Add(new Label { Text = user.UserID.ToString(), Padding = new Padding(5), TextAlign = ContentAlignment.MiddleCenter });
                table.Controls.Add(new Label { Text = user.FirstName, Padding = new Padding(5), TextAlign = ContentAlignment.MiddleCenter });
                table.Controls.Add(new Label { Text = user.LastName, Padding = new Padding(5), TextAlign = ContentAlignment.MiddleCenter });
                table.Controls.Add(new Label { Text = user.Email, Padding = new Padding(5), TextAlign = ContentAlignment.MiddleCenter });
                table.Controls.Add(new Label { Text = user.Phone, Padding = new Padding(5), TextAlign = ContentAlignment.MiddleCenter });
                table.Controls.Add(new Label { Text = user.Username, Padding = new Padding(5), TextAlign = ContentAlignment.MiddleCenter });
                table.Controls.Add(new Label { Text = user.Role, Padding = new Padding(5), TextAlign = ContentAlignment.MiddleCenter });
                table.Controls.Add(new Label { Text = user.Address, Padding = new Padding(5), TextAlign = ContentAlignment.MiddleCenter });
            }

            // Add the table to the flow panel
            flowPanel.Controls.Add(table);
        }









        public List<CarPartReportItem> GetAllCarsAndPartsForReport()
        {
            List<CarPartReportItem> reportItems = new List<CarPartReportItem>();

            try
            {
                db.OpenConnection();
                // Query to get all cars
                string query = @"
            SELECT 'Car' AS ItemType, c.CarID AS ItemID, b.BrandName, c.Model AS ItemName, c.Description, c.Price, c.QuantityAvailable 
            FROM Cars c
            INNER JOIN Brands b ON c.BrandID = b.BrandID
            UNION ALL
            SELECT 'Part' AS ItemType, p.PartID AS ItemID, b.BrandName, p.PartName AS ItemName, p.Description, p.Price, p.QuantityAvailable 
            FROM CarParts p
            INNER JOIN Brands b ON p.BrandID = b.BrandID";

                SqlCommand cmd = new SqlCommand(query, db.GetConnection());
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CarPartReportItem item = new CarPartReportItem
                    {
                        ItemType = reader.GetString(0), // Car or Part
                        ItemID = reader.GetInt32(1),
                        BrandName = reader.GetString(2),
                        ItemName = reader.GetString(3),
                        Description = reader.IsDBNull(4) ? "No Description" : reader.GetString(4),
                        Price = reader.GetDecimal(5),
                        QuantityAvailable = reader.GetInt32(6)
                    };
                    reportItems.Add(item);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching cars and parts: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            return reportItems;
        }


        public string GenerateHTMLReport(List<CarPartReportItem> reportItems)
        {
            StringBuilder html = new StringBuilder();

            // Add HTML header with CSS styling
            html.Append("<html><head>");
            html.Append("<style>");
            html.Append("body { font-family: Arial, sans-serif; margin: 20px; }");
            html.Append("h2 { text-align: center; color: #333; }");
            html.Append("table { width: 100%; border-collapse: collapse; margin: 20px 0; }");
            html.Append("th, td { border: 1px solid #ddd; padding: 8px; text-align: center; }");
            html.Append("th { background-color: #4CAF50; color: white; }");
            html.Append("tr:nth-child(even) { background-color: #f2f2f2; }");
            html.Append("tr:hover { background-color: #ddd; }");
            html.Append("</style>");
            html.Append("</head><body>");

            // Add title
            html.Append("<h2>Cars and Car Parts Report</h2>");

            // Add table structure
            html.Append("<table>");
            html.Append("<tr><th>Type</th><th>ID</th><th>Brand</th><th>Item Name</th><th>Description</th><th>Price</th><th>Quantity Available</th></tr>");

            // Populate rows with data
            foreach (var item in reportItems)
            {
                html.Append("<tr>");
                html.Append($"<td>{item.ItemType}</td>");
                html.Append($"<td>{item.ItemID}</td>");
                html.Append($"<td>{item.BrandName}</td>");
                html.Append($"<td>{item.ItemName}</td>");
                html.Append($"<td>{item.Description}</td>");
                html.Append($"<td>{item.Price:C}</td>");
                html.Append($"<td>{item.QuantityAvailable}</td>");
                html.Append("</tr>");
            }

            html.Append("</table>");
            html.Append("</body></html>");

            return html.ToString();
        }




        public void SaveHTMLReportToFile(string htmlContent)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "HTML Files|*.html";
                sfd.Title = "Save the HTML Report";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, htmlContent);
                }
            }
        }


        public void DisplayReportInFlowPanel(FlowLayoutPanel flowPanel, List<CarPartReportItem> reportItems)
        {
            flowPanel.Controls.Clear();

            // Title Label
            Label titleLabel = new Label
            {
                Text = "Cars and Car Parts Report Preview",
                Font = new Font("Arial", 14, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.FromArgb(50, 50, 50),
                Padding = new Padding(10),
                Margin = new Padding(0, 20, 0, 10)
            };
            flowPanel.Controls.Add(titleLabel);

            TableLayoutPanel table = new TableLayoutPanel
            {
                ColumnCount = 7,
                AutoSize = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };

            // Add table headers
            table.Controls.Add(new Label { Text = "Type", Font = new Font("Arial", 12, FontStyle.Bold), BackColor = Color.FromArgb(76, 175, 80), ForeColor = Color.White, Padding = new Padding(5) });
            table.Controls.Add(new Label { Text = "ID", Font = new Font("Arial", 12, FontStyle.Bold), BackColor = Color.FromArgb(76, 175, 80), ForeColor = Color.White, Padding = new Padding(5) });
            table.Controls.Add(new Label { Text = "Brand", Font = new Font("Arial", 12, FontStyle.Bold), BackColor = Color.FromArgb(76, 175, 80), ForeColor = Color.White, Padding = new Padding(5) });
            table.Controls.Add(new Label { Text = "Item Name", Font = new Font("Arial", 12, FontStyle.Bold), BackColor = Color.FromArgb(76, 175, 80), ForeColor = Color.White, Padding = new Padding(5) });
            table.Controls.Add(new Label { Text = "Description", Font = new Font("Arial", 12, FontStyle.Bold), BackColor = Color.FromArgb(76, 175, 80), ForeColor = Color.White, Padding = new Padding(5) });
            table.Controls.Add(new Label { Text = "Price", Font = new Font("Arial", 12, FontStyle.Bold), BackColor = Color.FromArgb(76, 175, 80), ForeColor = Color.White, Padding = new Padding(5) });
            table.Controls.Add(new Label { Text = "Quantity Available", Font = new Font("Arial", 12, FontStyle.Bold), BackColor = Color.FromArgb(76, 175, 80), ForeColor = Color.White, Padding = new Padding(5) });

            // Add data rows with alternating background color
            bool isAlternate = false;
            foreach (var item in reportItems)
            {
                Color rowColor = isAlternate ? Color.FromArgb(242, 242, 242) : Color.White;
                isAlternate = !isAlternate;

                table.Controls.Add(new Label { Text = item.ItemType, BackColor = rowColor, Padding = new Padding(5), Font = new Font("Arial", 10) });
                table.Controls.Add(new Label { Text = item.ItemID.ToString(), BackColor = rowColor, Padding = new Padding(5), Font = new Font("Arial", 10) });
                table.Controls.Add(new Label { Text = item.BrandName, BackColor = rowColor, Padding = new Padding(5), Font = new Font("Arial", 10) });
                table.Controls.Add(new Label { Text = item.ItemName, BackColor = rowColor, Padding = new Padding(5), Font = new Font("Arial", 10) });
                table.Controls.Add(new Label { Text = item.Description, BackColor = rowColor, Padding = new Padding(5), Font = new Font("Arial", 10) });
                table.Controls.Add(new Label { Text = item.Price.ToString("C"), BackColor = rowColor, Padding = new Padding(5), Font = new Font("Arial", 10) });
                table.Controls.Add(new Label { Text = item.QuantityAvailable.ToString(), BackColor = rowColor, Padding = new Padding(5), Font = new Font("Arial", 10) });
            }

            flowPanel.Controls.Add(table);
        }


        public List<OrderReportItem> GetOrdersWithCustomers()
        {
            List<OrderReportItem> ordersWithCustomers = new List<OrderReportItem>();

            try
            {
                db.OpenConnection();

                // Assuming you are using a stored procedure to get the data
                SqlCommand cmd = new SqlCommand("usp_GetOrdersWithCustomers", db.GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    OrderReportItem item = new OrderReportItem
                    {
                        OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                        CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                        OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                        TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                        Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? "N/A" : reader.GetString(reader.GetOrdinal("Status")),
                        FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? "N/A" : reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? "N/A" : reader.GetString(reader.GetOrdinal("LastName")),
                        Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "N/A" : reader.GetString(reader.GetOrdinal("Email")),
                        Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? "N/A" : reader.GetString(reader.GetOrdinal("Phone")),
                        Username = reader.IsDBNull(reader.GetOrdinal("Username")) ? "N/A" : reader.GetString(reader.GetOrdinal("Username")),
                        UserAddress = reader.IsDBNull(reader.GetOrdinal("Address")) ? "N/A" : reader.GetString(reader.GetOrdinal("Address"))
                    };

                    ordersWithCustomers.Add(item);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching orders with customers: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }

            return ordersWithCustomers;
        }



        public string GenerateOrderReportHTML(List<OrderReportItem> reportItems)
        {
            StringBuilder html = new StringBuilder();

            // Add HTML structure and CSS
            html.Append("<html><head>");
            html.Append("<style>");
            html.Append("body { font-family: Arial, sans-serif; margin: 20px; }");
            html.Append("h2 { text-align: center; color: #333; }");
            html.Append("table { width: 100%; border-collapse: collapse; margin: 20px 0; }");
            html.Append("th, td { border: 1px solid #ddd; padding: 8px; text-align: center; }");
            html.Append("th { background-color: #4CAF50; color: white; }");
            html.Append("tr:nth-child(even) { background-color: #f2f2f2; }");
            html.Append("tr:hover { background-color: #ddd; }");
            html.Append("</style>");
            html.Append("</head><body>");

            // Add report title
            html.Append("<h2>Order and Customer Report</h2>");

            // Create table structure
            html.Append("<table>");
            html.Append("<tr><th>OrderID</th><th>Customer Name</th><th>Email</th><th>Phone</th><th>Order Date</th><th>Total Amount</th><th>Status</th><th>Product</th><th>Quantity</th><th>Price</th><th>Delivery Address</th></tr>");

            // Populate the table with data
            foreach (var item in reportItems)
            {
                html.Append("<tr>");
                html.Append($"<td>{item.OrderID}</td>");
                html.Append($"<td>{item.FirstName} {item.LastName}</td>");
                html.Append($"<td>{item.Email}</td>");
                html.Append($"<td>{item.Phone}</td>");
                html.Append($"<td>{item.OrderDate.ToString("dd/MM/yyyy")}</td>");
                html.Append($"<td>{item.TotalAmount:C}</td>");
                html.Append($"<td>{item.Status}</td>");

                string product = item.CarID.HasValue ? $"Car (ID: {item.CarID})" : $"Part (ID: {item.PartID})";
                html.Append($"<td>{product}</td>");

                html.Append($"<td>{item.Quantity}</td>");
                html.Append($"<td>{item.Price:C}</td>");
                html.Append($"<td>{item.Address}</td>");
                html.Append("</tr>");
            }

            html.Append("</table>");
            html.Append("</body></html>");

            return html.ToString();
        }


        public void DisplayOrderReportInFlowPanel(FlowLayoutPanel flowPanel, List<OrderReportItem> reportItems)
        {
            flowPanel.Controls.Clear();

            // Create a title label
            Label titleLabel = new Label
            {
                Text = "Order and Customer Report Preview",
                Font = new Font("Arial", 14, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(10)
            };
            flowPanel.Controls.Add(titleLabel);

            // Create table structure
            TableLayoutPanel table = new TableLayoutPanel
            {
                ColumnCount = 11,
                AutoSize = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };

            // Add headers
            table.Controls.Add(new Label { Text = "OrderID", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5) });
            table.Controls.Add(new Label { Text = "Customer Name", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5) });
            table.Controls.Add(new Label { Text = "Email", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5) });
            table.Controls.Add(new Label { Text = "Phone", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5) });
            table.Controls.Add(new Label { Text = "Order Date", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5) });
            table.Controls.Add(new Label { Text = "Total Amount", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5) });
            table.Controls.Add(new Label { Text = "Status", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5) });
            table.Controls.Add(new Label { Text = "Product", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5) });
            table.Controls.Add(new Label { Text = "Quantity", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5) });
            table.Controls.Add(new Label { Text = "Price", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5) });
            table.Controls.Add(new Label { Text = "Delivery Address", Font = new Font("Arial", 12, FontStyle.Bold), Padding = new Padding(5) });

            // Add report rows
            foreach (var item in reportItems)
            {
                table.Controls.Add(new Label { Text = item.OrderID.ToString(), Padding = new Padding(5) });
                table.Controls.Add(new Label { Text = $"{item.FirstName} {item.LastName}", Padding = new Padding(5) });
                table.Controls.Add(new Label { Text = item.Email, Padding = new Padding(5) });
                table.Controls.Add(new Label { Text = item.Phone, Padding = new Padding(5) });
                table.Controls.Add(new Label { Text = item.OrderDate.ToString("dd/MM/yyyy"), Padding = new Padding(5) });
                table.Controls.Add(new Label { Text = item.TotalAmount.ToString("C"), Padding = new Padding(5) });
                table.Controls.Add(new Label { Text = item.Status, Padding = new Padding(5) });

                string product = item.CarID.HasValue ? $"Car (ID: {item.CarID})" : $"Part (ID: {item.PartID})";
                table.Controls.Add(new Label { Text = product, Padding = new Padding(5) });

                table.Controls.Add(new Label { Text = item.Quantity.ToString(), Padding = new Padding(5) });
                table.Controls.Add(new Label { Text = item.Price.ToString("C"), Padding = new Padding(5) });
                table.Controls.Add(new Label { Text = item.Address, Padding = new Padding(5) });
            }

            flowPanel.Controls.Add(table);
        }



        public void SaveOrderReportToFile(string htmlReport)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "HTML Files (*.html)|*.html",
                Title = "Save Report as HTML"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, htmlReport);
                MessageBox.Show("Report saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


    }

    public class CarPartReportItem
    {
        public string ItemType { get; set; } // Car or Part
        public int ItemID { get; set; }
        public string BrandName { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
    }

    public class OrderReportItem
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public int OrderDetailID { get; set; }
        public int? CarID { get; set; } // Nullable because there might be a part instead of a car
        public int? PartID { get; set; } // Nullable because there might be a car instead of a part
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string UserAddress { get; set; }
    }


}
