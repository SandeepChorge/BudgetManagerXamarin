using NewRestTest.appview;
using NewRestTest.database;
using NewRestTest.model;
using NewRestTest.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NewRestTest.viewmodel
{
    public class LoginVM : MyBaseViewModel
    {

        MainDB dbh;
       // string _username,Username;
        public LoginVM()
        {
            dbh = App.getMainDatabase;
            Debug.WriteLine("Initializing LoginVM from Constructor");
            //SetProperty<String>(Username,_username,null, "Username");
        }

        string _username;
        public string Username
        {
            get { return _username; }

            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        string _password;
        public string Password
        {
            get { return _password; }

            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        string _loginResult;
        public string LoginResult
        {
            get { return _loginResult; }

            set
            {
                if (_loginResult != value)
                {
                    _loginResult = value;
                    
                    OnPropertyChanged("LoginResult");
                    Debug.WriteLine("USerName is " + Username + "\tPassword is " + Password);
                }
            }
        }

        public void printValues()
        {
            Debug.WriteLine("Reached in PrintValues");
            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password))
            {
                Debug.WriteLine("In IF");
                LoginResult = "Please Enter Valid Username and Password";
            }else if(_username.Equals("admin") && _password.Equals("admin"))
            {
                Debug.WriteLine("In ELSE IF");
                LoginResult = "Login Success";
            }
            else
            {
                Debug.WriteLine("In ELSE");
                LoginResult = "Login Failed";
            }
            

        }

        public async Task validateValuesAsync()
        {
            Debug.WriteLine("Reached in ValidateValuesAsync");
            IRepository<UserModel> userRepo = new Repository<UserModel>(dbh.Database);

            List<UserModel> alldata = await userRepo.Get<UserModel>(r => r.Email == Username && r.Password == Password, null);
            if (alldata != null && alldata.Count > 0 )
            {
                PrefManager.setUserID(alldata[0].Id); 
                AppSettings.MakeToast("Login Success");
                Debug.WriteLine("Username "+alldata[0].Name);
                LoginResult = "Login Success";
                PrefManager.setIsLoggedIn(true);
                Application.Current.MainPage = new DashboardPage();
            }
            else
            {
                AppSettings.MakeToast("Please enter valid Credentials");
                LoginResult = "Please enter valid Credentials";
                
            }
        }

        public void SignUp()
        {
            Application.Current.MainPage = new RegisterUser();
        }

    }
}
