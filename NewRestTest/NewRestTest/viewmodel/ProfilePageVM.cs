using NewRestTest.database;
using NewRestTest.model;
using NewRestTest.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NewRestTest.viewmodel
{
    public class ProfilePageVM : MyBaseViewModel
    {
        int UserId;

        public ProfilePageVM()
        {
            UserId = PrefManager.getUserID();
            GetData();
        }

        private async void GetData()
        {
            MainDB dbh = App.getMainDatabase;
            IRepository<UserModel> userRepo = new Repository<UserModel>(dbh.Database);
            UserModel userModel = await userRepo.Get(UserId.ToString());

            if (userModel != null)
            {
                Debug.WriteLine("User model is not null ");

                UserName = userModel.Name;
                MobileNumber = userModel.MobileNumber;
                EmailID = userModel.Email;
                CreatedOn = "";
            }
            else
            {
                UserName = "-";
                MobileNumber = "-";
                EmailID = "-";
                CreatedOn = "";
                Debug.WriteLine("USerMOdel is null" );
            }
        }

        string _username;
        public string UserName
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged("UserName");
                }
            }
        }

        string _mobilenumber;
        public string MobileNumber
        {
            get { return _mobilenumber; }
            set
            {
                if (_mobilenumber != value)
                {
                    _mobilenumber = value;
                    OnPropertyChanged("MobileNumber");
                }
            }
        }

        string _email;
        public string EmailID
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged("EmailID");
                }
            }
        }

        string _createdOn;
        public string CreatedOn
        {
            get { return _createdOn; }
            set
            {
                if (_createdOn != value)
                {
                    _createdOn = value;
                    OnPropertyChanged("CreatedOn");
                }
            }
        }

        //Text="Created On: 23th June 2021"



    }
}
