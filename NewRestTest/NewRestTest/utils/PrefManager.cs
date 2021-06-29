using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace NewRestTest.utils
{
    public class PrefManager
    {
        public static void setIsLoggedIn(bool val)
        {
            Preferences.Set("isLoggedIn", val);
        }

        public static bool IsLoggedIn()
        {
            return Preferences.Get("isLoggedIn",false);
        }

        public static void setUserID(int userID)
        {
            Preferences.Set("userID", userID);
        }

        public static int getUserID()
        {
            return Preferences.Get("userID", 0);
        }
    }
}
