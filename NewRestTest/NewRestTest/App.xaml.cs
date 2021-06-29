using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using NewRestTest.appview;
using NewRestTest.utils;
using NewRestTest.database;
using NewRestTest.model;

namespace NewRestTest
{
    public partial class App : Application
    {

        static string dbPath => FileAccessHelper.GetLocalFilePath("newresttest.db3");
        public static MainDB dbRepo;
        public App()
        {
            InitializeComponent();
            //dbRepo = new MainDB(dbPath);
            //MainPage = new ProfilePage();
            MainPage = new ViewAllBudgets();

            /*  ManageTransactionModel manageTransactionModel = new ManageTransactionModel() {
                  BudgetId = 1, Type = 2,TransactionId = 1
          };
              MainPage = new AddEditTransaction(manageTransactionModel);*/
        }

        public static MainDB getMainDatabase
        {
            get
            {
                if (dbRepo == null)
                {
                    dbRepo = new MainDB(dbPath);
                }
                return dbRepo;
            }
        }

        protected override void OnStart()
        {
            /*AppCenter.Start("android=122ddefb-d95a-4a53-a038-b2e0a14e6283;",
                  typeof(Analytics), typeof(Crashes));*/
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
