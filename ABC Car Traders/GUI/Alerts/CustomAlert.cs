using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Car_Traders.GUI.Alerts
{
    public partial class CustomAlert : Form
    {
        public CustomAlert()
        {
            InitializeComponent();
        }

        private Timer timer;
        private Label messageLabel;
        private Button closeButton;

        public CustomAlert(string message, int duration = 5000, string issueType = null)
        {
            // Set up the form
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightGray;
            this.Size = new Size(350, 100);

            // Create and configure the label
            messageLabel = new Label();
            messageLabel.Text = message;
            messageLabel.Dock = DockStyle.Fill;
            messageLabel.TextAlign = ContentAlignment.MiddleCenter;
            messageLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            this.Controls.Add(messageLabel);

            // Add specific issue type if provided
            if (!string.IsNullOrEmpty(issueType))
            {
                messageLabel.Text = $"{issueType}: {message}";
            }

            // Create and configure the close button
            closeButton = new Button();
            closeButton.Text = "X";
            closeButton.Font = new Font("Arial", 10, FontStyle.Bold);
            closeButton.Size = new Size(30, 30);
            closeButton.Location = new Point(this.Width - 40, 10);
            closeButton.Click += CloseButton_Click;
            this.Controls.Add(closeButton);

            // Set up the timer
            timer = new Timer();
            timer.Interval = duration; // Duration in milliseconds
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            this.Close();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Static method to show the alert
        public static void ShowAlert(string message, int duration = 5000, string issueType = null)
        {
            CustomAlert alert = new CustomAlert(message, duration, issueType);
            alert.ShowDialog();
        }

    }
}
