using Guna.UI2.WinForms;
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

namespace ABC_Car_Traders.GUI.MainUserControls
{
    public partial class CarList : UserControl
    {
        public CarList()
        {
            InitializeComponent();
        }

        public string CarName
        {
            get { return lblcarname.Text; }
            set { lblcarname.Text = value; }
        }

        public string Description
        {
            get { return lbldescription.Text; }
            set { lbldescription.Text = value; }
        }

        public string Price
        {
            get { return lblprice.Text; }
            set { lblprice.Text = value; }
        }

        public string ImagePath
        {
            get { return imgcar.ImageLocation; }
            set
            {
                if (File.Exists(value))
                {
                    imgcar.ImageLocation = value; // Set the image if the file exists
                }
                else
                {
                    imgcar.ImageLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Vehicles", "no-image.jpg"); // Fallback to a default image
                }
            }
        }


        public Guna2Button OrderButton
        {
            get { return btnorder; }
        }

        // Change the return types to Guna2CircleButton for these properties
        public Guna2CircleButton AddToCartButton
        {
            get { return btnaddtocart; }
        }

        public Guna2Button ViewMoreButton
        {
            get { return btnviewmore; }
        }

        public void SetCarInfo(int carID, string carName, string description, string price, string imagePath)
        {
            CarName = carName;
            Description = description;
            Price = price;

            // Combine base path and validate
            string fullImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Vehicles", imagePath);

            // Try to load the car image
            try
            {
                if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(fullImagePath))
                {
                    // If the image exists and the path is valid
                    imgcar.ImageLocation = fullImagePath;
                }
                else
                {
                    // If image is not found, load a default image
                    imgcar.ImageLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Vehicles", "car.jpg");
                }
            }
            catch (Exception ex)
            {
                // Log error and use default image in case of an exception
                Console.WriteLine("Failed to load car image: " + ex.Message);
                imgcar.ImageLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Vehicles", "car.jpg");
            }

            // Set the CarID as a tag for future use
            btnorder.Tag = carID;
            btnaddtocart.Tag = carID;
            btnviewmore.Tag = carID;
        }



    }
}
