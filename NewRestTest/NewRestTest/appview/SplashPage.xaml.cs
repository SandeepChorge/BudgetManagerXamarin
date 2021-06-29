using NewRestTest.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewRestTest.appview
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SplashPage : ContentPage
	{
		public SplashPage ()
		{
			InitializeComponent ();
			AppStart();
		}

        private async void AppStart()
        {
            await Task.Delay(1000);
			if (PrefManager.IsLoggedIn())
			{
				Application.Current.MainPage = new NavigationPage(new DashboardPage());
            }
            else
            {
				Application.Current.MainPage = new LoginPage();
			}
		}
    }
}