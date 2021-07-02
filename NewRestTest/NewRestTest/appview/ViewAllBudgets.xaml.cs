using NewRestTest.model;
using NewRestTest.utils;
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

        protected override void OnAppearing()
        {
            if (model != null)
            {
                model.AddTempData();
            }
        }

            private void Button_Clicked(object sender, EventArgs e)
        {
            model.AddTempData();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var myListView = (ListView)sender;
            BudgetListModel b = (BudgetListModel)myListView.SelectedItem;
            //AppSettings.MakeLog("ERR", "->" + e.GetType());
            //AppSettings.MakeLog("ERR", "->" +e.GetType() );
            // Budget b = e.SelectedItem as Budget;
            Navigation.PushAsync(new BudgetDetailPage(b.BudgetId));
        }
    }
}