using NewRestTest.database;
using NewRestTest.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
namespace NewRestTest.viewmodel
{
    public class ManageUsersVM : MyBaseViewModel
    {
        MainDB dbh;
        INavigation navigation;
        IRepository<UserModel> usersRepo;

        public ObservableCollection<UserModel> users = new ObservableCollection<UserModel>();
        public ObservableCollection<UserModel> Users
        {
            get { return users; }

            set
            {
                if (users != value)
                {
                    users = value;
                    OnPropertyChanged("Users");
                }
            }
        }
        public ManageUsersVM(INavigation navigation)
        {
            dbh = App.getMainDatabase;
            this.navigation = navigation;
            usersRepo = new Repository<UserModel>(dbh.Database);
            GetUsers();
        }

        public async void GetUsers()
        {
            users = new ObservableCollection<UserModel>(await usersRepo.Get<UserModel>());
            OnPropertyChanged("Users");
        }

    }
}
