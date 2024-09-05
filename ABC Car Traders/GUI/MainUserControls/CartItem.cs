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
    public partial class CartItem : UserControl
    {
        public CartItem()
        {
            InitializeComponent();
        }

        public string ItemName
        {
            get { return lblitemname.Text; }
            set { lblitemname.Text = value; }
        }

        public string Price
        {
            get { return lblitemprice.Text; }
            set { lblitemprice.Text = value; }
        }

        public string Quantity
        {
            get { return NumericUpDownupitemscount.Value.ToString(); }
            set { NumericUpDownupitemscount.Value = int.Parse(value); }
        }

        public string ImagePath
        {
            get { return imgitemimage.ImageLocation; }
            set { imgitemimage.ImageLocation = value ?? "path_to_default_image.png"; } // Use a default image if null
        }

        public Guna.UI2.WinForms.Guna2CircleButton RemoveButton
        {
            get { return btnremoveitem; }
        }

        public Guna.UI2.WinForms.Guna2CircleButton UpdateButton
        {
            get { return btnupdatetheamount; }
        }

        public Guna.UI2.WinForms.Guna2NumericUpDown QuantitySelector
        {
            get { return NumericUpDownupitemscount; }
        }


    }
}
