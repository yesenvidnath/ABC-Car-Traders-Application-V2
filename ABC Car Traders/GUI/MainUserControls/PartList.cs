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
    public partial class PartList : UserControl
    {
        public PartList()
        {
            InitializeComponent();
        }

        public string PartName
        {
            get { return lblpartname.Text; }
            set { lblpartname.Text = value; }
        }

        public string Description
        {
            get { return lblpartdicription.Text; }
            set { lblpartdicription.Text = value; }
        }

        public string Price
        {
            get { return lblpartprice.Text; }
            set { lblpartprice.Text = value; }
        }

        public string ImagePath
        {
            get { return imgparts.ImageLocation; }
            set { imgparts.ImageLocation = value; }
        }

        public Guna2Button OrderNowButton
        {
            get { return btnordernow; }
        }

        public Guna2CircleButton AddToCartButton
        {
            get { return btnaddtocart; }
        }

        public Guna2Button ViewMoreButton
        {
            get { return btnviwemore; }
        }

        public void SetPartInfo(int partID, string partName, string description, string price, string imagePath)
        {
            PartName = partName;
            Description = description;
            Price = price;

            // Combine the base directory with the correct path for the part images
            string fullImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Parts", imagePath);

            // Check if the image exists, if not, use a default image
            try
            {
                if (!File.Exists(fullImagePath) || string.IsNullOrEmpty(imagePath))
                {
                    // Set a default image if the part image is missing or invalid
                    fullImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Parts", "default-part.jpg");
                }

                // Set the ImageLocation property with the correct image path
                imgparts.ImageLocation = fullImagePath;
            }
            catch (Exception ex)
            {
                // Log error and fallback to the default image
                Console.WriteLine("Failed to load part image: " + ex.Message);
                imgparts.ImageLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Parts", "default-part.jpg");
            }

            // Assign the PartID to the buttons' Tag property
            btnordernow.Tag = partID;
            btnaddtocart.Tag = partID;
            btnviwemore.Tag = partID;
        }

    }
}
