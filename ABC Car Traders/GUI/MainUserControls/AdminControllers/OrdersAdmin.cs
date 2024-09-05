using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Car_Traders.GUI.MainUserControls.AdminControllers
{
    public partial class OrdersAdmin : UserControl
    {
        public OrdersAdmin()
        {
            InitializeComponent();

            // Make the entire user control clickable
            this.Click += OrdersAdmin_Click;
            // If only specific parts should be clickable, attach event handlers to them
            lblorderID.Click += OrdersAdmin_Click;
            lblorderdate.Click += OrdersAdmin_Click;
            lbladdress.Click += OrdersAdmin_Click;
            lbltotamount.Click += OrdersAdmin_Click;
            lblstatus.Click += OrdersAdmin_Click;
        }

        // Trigger an event when the user control is clicked
        public event EventHandler OrderClicked;

        private void OrdersAdmin_Click(object sender, EventArgs e)
        {
            if (OrderClicked != null)
            {
                OrderClicked(this, e);
            }
        }


        // Property for BtnChangeStatus (Assuming it's a Guna2Button or similar)
        public Guna.UI2.WinForms.Guna2Button BtnChangeStatus
        {
            get { return btnchangestatus; } // Make sure `btnchangestatus` exists in the designer
        }

        // Property for ComboBox (combstatus)
        public Guna.UI2.WinForms.Guna2ComboBox CombStatus
        {
            get { return combstatus; } // Make sure `combstatus` exists in the designer
        }

        public string OrderID
        {
            get { return lblorderID.Text; }
            set { lblorderID.Text = value; }
        }

        public string OrderDate
        {
            get { return lblorderdate.Text; }
            set { lblorderdate.Text = value; }
        }

        public string Address
        {
            get { return lbladdress.Text; }
            set { lbladdress.Text = value; }
        }

        public string TotalAmount
        {
            get { return lbltotamount.Text; }
            set { lbltotamount.Text = value; }
        }

        public string OrderStatus
        {
            get { return lblstatus.Text; }
            set { lblstatus.Text = value; }
        }

        public void SetStatus(string status)
        {
            combstatus.SelectedItem = status;
        }

        public string GetStatus()
        {
            return combstatus.SelectedItem.ToString();
        }

    }
}
