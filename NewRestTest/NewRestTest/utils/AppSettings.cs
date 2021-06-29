using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace NewRestTest.utils
{
    public class AppSettings
    {
        public static int TIMEOUT = 9000;
        public static string APP_Platform = Device.RuntimePlatform;
        public static string APP_BASEADDRESS_URL = "https://fakestoreapi.com/";
        public static string PRODUCT_URL = "products";

        public static int USER_ID = 0;


        public static void MakeAnalyticsEvent(string e)
        {
           //// Analytics.TrackEvent(e);
        }

        public static void MakeToast(string message)
        {
            DependencyService.Get<IToast>().Show(message);
        }

        public static void MakeLog(string tag,string message)
        {
            Debug.WriteLine(tag,message);
        }
    }
}
