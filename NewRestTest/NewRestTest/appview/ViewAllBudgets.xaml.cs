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
    public partial class ViewAllBudgets : ContentPage
    {
        ViewAllBudgetsVM model;
        public ViewAllBudgets()
        {
            InitializeComponent();
            model = new ViewAllBudgetsVM();
            BindingContext = model;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            model.AddTempData();
        }
    }
}