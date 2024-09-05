using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC_Car_Traders
{
    public static class SessionManager
    {
        // Property to store the logged-in user's ID
        public static int UserID { get; private set; }

        // Property to store the logged-in user's Role
        public static string Role { get; private set; }

        // Method to set the session when the user logs in
        public static void StartSession(int userId, string role)
        {
            UserID = userId;
            Role = role;
        }

        // Method to clear the session when the user logs out
        public static void EndSession()
        {
            UserID = 0;
            Role = null;
        }

        // Method to check if the user is logged in
        public static bool IsLoggedIn()
        {
            return UserID != 0;
        }
    }
}
