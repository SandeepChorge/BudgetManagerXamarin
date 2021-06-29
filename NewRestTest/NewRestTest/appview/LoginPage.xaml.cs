using NewRestTest.viewmodel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewRestTest.appview
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        LoginVM obj;
        public LoginPage()
        {
            InitializeComponent();
            obj = new LoginVM();
            BindingContext = obj;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            // Debug.WriteLine("--> Button is clicked " );
            //obj.printValues();
            await obj.validateValuesAsync();
        }

        private void Signup(object sender, EventArgs e)
        {
            obj.SignUp();
        }
    }
}