using NewRestTest.httpmanager;
using NewRestTest.model;
using NewRestTest.utils;
using NewRestTest.viewmodel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NewRestTest
{
    public partial class MainPage : ContentPage
    {
        MainPageVM _obj;
     
        public MainPage()
        {
            InitializeComponent();
            _obj = new MainPageVM();
            BindingContext = _obj;
        }

      /*  public void CallRestAPI(object sender, EventArgs e)
        {
            _obj.CallRestAPI();
        }*/


    }
}
