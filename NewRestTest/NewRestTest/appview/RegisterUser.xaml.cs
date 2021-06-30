using NewRestTest.database;
using NewRestTest.model;
using NewRestTest.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewRestTest.appview
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterUser : ContentPage
	{

        MainDB dbh;
       
        public RegisterUser()
		{
			InitializeComponent ();
            dbh = App.getMainDatabase;
           
            BindingContext = this;
            
		}

		string _Selection;
        public string Selection
        {
            get { return _Selection; }
            set
            {
                if (_Selection != value)
                {
                    _Selection = value;
                    OnPropertyChanged("Selection");
                }
            }
        }


        private async void ValidateUserAsync(object sender, EventArgs e)
        {

            Debug.WriteLine("Selection is " + Selection);
            if (ValidateRegistrationFields())
            {
                IRepository<UserModel> userRepo = new Repository<UserModel>(dbh.Database);

                List<UserModel> alldata = await userRepo.Get<UserModel>(r => r.Email == Email.Text || r.MobileNumber == MobileNumber.Text, null);
                if (alldata != null && alldata.Count > 0)
                {
                    AppSettings.MakeToast("Email or phone number already exists with different username " + alldata[0].Name);
                }
                else
                {

                    Debug.WriteLine("Name: " + Name.Text + "\t" +
                       "MobileNumber: " + MobileNumber.Text + "\t" +
                       "Email: " + Email.Text + "\t" +
                       "Password: " + Password.Text + "\t" +
                       "Gender: " + Selection
                       );

                    UserModel user = new UserModel
                    {
                        Name = Name.Text,
                        MobileNumber = MobileNumber.Text,
                        Email = Email.Text,
                        Password = Password.Text,
                        Gender = 1
                    };

                    //IRepository<UserModel> userRepo = new Repository<UserModel>(dbh.Database);
                    int val = await userRepo.Insert(user);
                    AppSettings.MakeToast("User added successfully");
                    //Application.Current.MainPage = new NavigationPage(new LoginPage());
                    Debug.WriteLine("Insert Result is " + val);
                    Application.Current.MainPage = new LoginPage();
                }
            }
        }

        //Removed this method
        private void LoginPageRedirect(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());

            
        }

        private Boolean ValidateRegistrationFields()
        {
            Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if (String.IsNullOrEmpty(Name.Text) || String.IsNullOrEmpty(MobileNumber.Text) ||
                String.IsNullOrEmpty(Email.Text) || String.IsNullOrEmpty(Password.Text) ||
               String.IsNullOrEmpty(Selection) || Selection.Contains("-1"))
            {
                AppSettings.MakeToast("Please Select/ Enter all the fields before submitting");
                return false;
            }else if (!EmailRegex.IsMatch(Email.Text))
            {
                AppSettings.MakeToast("Please Enter a Valid Email Address");
                return false;
            }
            else if (MobileNumber.Text.Length<10)
            {
                AppSettings.MakeToast("Please Enter a Valid Mobile Number");
                return false;
            }
            return true;
        }
    }
}