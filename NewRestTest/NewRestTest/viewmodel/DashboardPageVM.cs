using Android.Content.Res;
using MvvmHelpers.Commands;
using NewRestTest.appview;
using NewRestTest.model;
using NewRestTest.utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NewRestTest.viewmodel
{
    public class DashboardPageVM : MyBaseViewModel
    {
        INavigation navigation;
        public ICommand RBudget { private set; get; }
        public ICommand RProfile { private set; get; }
        public ICommand RAddBudget { private set; get; }
        public ICommand ManageUsers { private set; get; }
        public ICommand AddUser { private set; get; }
        
        List<CarouselItem> carouselContents = new List<CarouselItem>();
        public List<CarouselItem> CarouselContents
        {
            get { return carouselContents; }

            set
            {
                if (carouselContents != value)
                {
                    carouselContents = value;
                    OnPropertyChanged("CarouselContents");
                }
            }
        }
        public String UserName
        {
            get { return PrefManager.getUserName(); }
        }
        public DashboardPageVM(INavigation navigation)
        {
            OnPropertyChanged("UserName");
            RBudget = new MvvmHelpers.Commands.Command(MakeC);
            RProfile = new MvvmHelpers.Commands.Command(RedirectProfilePage);
            RAddBudget = new MvvmHelpers.Commands.Command(AddBudget);
            ManageUsers = new MvvmHelpers.Commands.Command(ManageUsersAcc);
            AddUser = new MvvmHelpers.Commands.Command(AddUserAcc);
            GetCarouselList();
            this.navigation = navigation;
        }

        public async void MakeC()
        {
            await navigation.PushAsync(new ViewAllBudgets());

        }
      
        public async void RedirectProfilePage()
        {
            await navigation.PushAsync(new ProfilePage());
        }

        public async void AddBudget()
        {
            await navigation.PushAsync(new AddEditBudget(0));
        }

        public async void ManageUsersAcc()
        {
            await navigation.PushAsync(new ManageUsers());
        }

        public async void AddUserAcc()
        {
            await navigation.PushAsync(new RegisterUser(1));
        }
        
        public void GetCarouselList()
        {
            carouselContents.Add(new CarouselItem { Item = "\"Add and Manage Users\"" });
            carouselContents.Add(new CarouselItem { Item = "\"Manage your Budgets\"" });
            carouselContents.Add(new CarouselItem { Item = "\"Track your income and Expense Transactions\"" });

            OnPropertyChanged("CarouselContents");
        }
    }
}
