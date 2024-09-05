using ABC_Car_Traders.Function;
using ABC_Car_Traders.Function.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Car_Traders.GUI.SingleForms
{
    public partial class CartItems : Form
    {
        public CartItems()
        {
            InitializeComponent();
        }

        public void LoadCartItems()
        {
            int userId = SessionManager.UserID;
            Function.Customer.Customer customers = new Function.Customer.Customer();
            int customerId = customers.GetCustomerIdByUserId(userId);

            //Console.WriteLine("Loading cart items for CustomerID: " + customerId); // Debug

            Cart cart = new Cart();
            List<CartItem> cartItems = cart.GetCartItems(customerId); // Fetch cart items using CustomerID

            //Console.WriteLine("Number of items found: " + cartItems.Count); // Debug

            flpcartitems.Controls.Clear();
            foreach (CartItem item in cartItems)
            {
                MainUserControls.CartItem cartItemControl = new MainUserControls.CartItem();
                cartItemControl.ItemName = item.ItemName;
                cartItemControl.Price = $"${item.Price}";
                cartItemControl.Quantity = item.Quantity.ToString();
                cartItemControl.ImagePath = item.ImagePath;

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

            LoadCartItems(); // Refresh the cart items after removal
            UpdateCartItemCount(); // Update the cart item count in the navbar
        }

        private void UpdateItemQuantity(int cartItemId, int newQuantity)
        {
            Cart cart = new Cart();
            cart.UpdateCartItemQuantity(cartItemId, newQuantity);

            LoadCartItems(); // Refresh the cart items after updating
        }

        private void UpdateCartItemCount()
        {
            // Logic to update the cart item count in the navbar
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
                // Optionally clear the cart after placing the order
                cart.ClearCart(customerId);
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
    }
}
