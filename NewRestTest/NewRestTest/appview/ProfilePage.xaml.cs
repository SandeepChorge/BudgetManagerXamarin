using NewRestTest.database;
using NewRestTest.model;
using NewRestTest.utils;
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
    public partial class ProfilePage : ContentPage
    {

        private ProfilePageVM model;
        public ProfilePage()
        {
            InitializeComponent();
            model = new ProfilePageVM();
            BindingContext = model;
        }

        private void LogoutTapped(object sender, EventArgs e)
        {
            PrefManager.setIsLoggedIn(false);
            Application.Current.MainPage = new LoginPage();
            /* MainDB dbh = App.getMainDatabase;
             UserModel user = new UserModel
             {
                 Name = "Monkey D. Luffy",
                 MobileNumber = "9876789098",
                 Email = "PirateKing@FamilyOfD.com",
                 Password = "pirate",
                 Gender = 1
             };

             IRepository<UserModel> userRepo = new Repository<UserModel>(dbh.Database);
             int val = await userRepo.Insert(user);

             Debug.WriteLine("Insert Result is " + val);*/
        }
    }
}