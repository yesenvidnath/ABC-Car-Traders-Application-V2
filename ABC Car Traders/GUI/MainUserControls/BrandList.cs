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
    public partial class BrandList : UserControl
    {
        public BrandList()
        {
            InitializeComponent();
        }

        public void SetBrandInfo(string brandName, string imagePath)
        {
            lblbrandname.Text = brandName;

            if (imagePath == "No Image")
            {
                imgbrand.Image = CreateNoImageBitmap(); // Create a "No Image" bitmap
            }
            else
            {
                imgbrand.ImageLocation = imagePath;
            }
        }

        private Bitmap CreateNoImageBitmap()
        {
            Bitmap bmp = new Bitmap(200, 200); // Adjust size as needed
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Gray); // Background color
                using (Font font = new Font("Arial", 20))
                {
                    g.DrawString("No Image", font, Brushes.White, new PointF(20, 80)); // Adjust position as needed
                }
            }
            return bmp;
        }


    }
}
