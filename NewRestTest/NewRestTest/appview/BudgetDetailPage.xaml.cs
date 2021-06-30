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
    public partial class BudgetDetailPage : ContentPage
    {
        BudgetDetailPageVM model;
        int BudgetID;
        public BudgetDetailPage(int BudgetID)
        {
            this.BudgetID = BudgetID;
            model = new BudgetDetailPageVM(BudgetID,Navigation);
            InitializeComponent();
            BindingContext = model;
        }



        /*private void MyCarousel_CurrentItemChanged_1(object sender, CurrentItemChangedEventArgs e)
        {

            model.CarouselItemChanged(((Label)MyCarousel.CurrentItem).Text);
            *//*if (MyCarousel.CurrentItem.Equals(""))
                AppSettings.MakeToast("Selected Fisrt Tab");
            else
                AppSettings.MakeToast("Selected Second Tab");*//*

        }*/

        protected override void OnAppearing()
        {
            if (Income.IsChecked)
                model.GetData(1);
            else
                model.GetData(2);
            base.OnAppearing();
        }  

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            
            if (Income.IsChecked)
            {
                model.GetData(1);
            }
            else
            {
                model.GetData(2);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            model.AddNewTransaction();
        }
    }
}