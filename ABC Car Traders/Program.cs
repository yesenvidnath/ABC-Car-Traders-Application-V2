using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABC_Car_Traders.GUI;
using System.Runtime.InteropServices;

namespace ABC_Car_Traders
{
    internal static class Program
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            // Set up a timer to check for the key combination
            Timer checkKeysTimer = new Timer();
            checkKeysTimer.Interval = 100; // Check every 100ms
            checkKeysTimer.Tick += CheckForAdminShortcut;
            checkKeysTimer.Start();

            // Instantiate the main form and apply the font to all its controls
            Home mainForm = new Home();
            ApplyFontToAllForms(mainForm, new Font("Poppins", 10F, FontStyle.Regular));

            // Check if the user is already logged in
            if (SessionManager.IsLoggedIn())
            {
                Application.Run(new Home());
            }
            else
            {
                Application.Run(new Home());
            }
        }

        // Open the secret Admin Panel 
        private static void CheckForAdminShortcut(object sender, EventArgs e)
        {
            // Check if Ctrl + Shift + A is pressed
            if (GetAsyncKeyState(Keys.ControlKey) < 0 && GetAsyncKeyState(Keys.ShiftKey) < 0 && GetAsyncKeyState(Keys.A) < 0)
            {
                // Stop the timer to prevent opening multiple forms
                ((Timer)sender).Stop();

                // Open the AdminLogin form
                OpenAdminLogin();
            }
        }

        private static void OpenAdminLogin()
        {
            // Create and show the AdminLogin form
            AdminLogin adminLoginForm = new AdminLogin();
            adminLoginForm.ShowDialog(); // Show the form as a dialog

            // Resume the timer after closing the form
            Timer checkKeysTimer = new Timer();
            checkKeysTimer.Interval = 100;
            checkKeysTimer.Tick += CheckForAdminShortcut;
            checkKeysTimer.Start();
        }


        static void ApplyFontToAllForms(Form form, Font font)
        {
            form.Font = font;
            foreach (Form childForm in form.OwnedForms)
            {
                ApplyFontToAllForms(childForm, font);
            }
            ApplyFontToControls(form.Controls, font);
        }

        static void ApplyFontToControls(Control.ControlCollection controls, Font font)
        {
            foreach (Control control in controls)
            {
                control.Font = font;
                if (control.HasChildren)
                {
                    ApplyFontToControls(control.Controls, font);
                }
            }
        }

    }
}
