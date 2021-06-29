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
    public partial class DashboardPage : ContentPage
    {
        DashboardPageVM model;
        public DashboardPage()
        {
            InitializeComponent();
            model = new DashboardPageVM();
            BindingContext = model;
        }
    }
}