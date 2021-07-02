using NewRestTest.viewmodel;
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
    public partial class ManageUsers : ContentPage
    {
        ManageUsersVM model;
        public ManageUsers()
        {
            InitializeComponent();
            model = new ManageUsersVM(Navigation);
            BindingContext = model;
        }
    }
}