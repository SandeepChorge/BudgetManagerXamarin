using Android.Content.Res;
using MvvmHelpers.Commands;
using NewRestTest.appview;
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
        

        public DashboardPageVM(INavigation navigation)
        {
            RBudget = new MvvmHelpers.Commands.Command(MakeC);
            RProfile = new MvvmHelpers.Commands.Command(RedirectProfilePage);
            RAddBudget = new MvvmHelpers.Commands.Command(AddBudget);
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
    }
}
