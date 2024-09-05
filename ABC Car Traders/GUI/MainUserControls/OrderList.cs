using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Car_Traders.GUI.MainUserControls
{
    public partial class OrderList : UserControl
    {
        public OrderList()
        {
            InitializeComponent();
        }

        public string OrderID
        {
            get { return lblorderID.Text; }
            set { lblorderID.Text = value; } // This will cause infinite recursion
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

        public Guna.UI2.WinForms.Guna2Button CancelOrderButton
        {
            get { return btncancelorder; }
        }


        private void OrderList_Load(object sender, EventArgs e)
        {

        }
    }
}
