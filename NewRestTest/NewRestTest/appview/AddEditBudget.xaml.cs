using NewRestTest.database;
using NewRestTest.model;
using NewRestTest.utils;
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
    public partial class AddEditBudget : ContentPage
    {
        MainDB dbh;
        int budgetID = 0;
        string TAG = "AddEditBudget";
        public AddEditBudget(int budgetID)
        {
            InitializeComponent();
            dbh = App.getMainDatabase;
            this.budgetID = budgetID;
            HandleViews();
        }


        private async void HandleViews()
        {
                if (budgetID == 0)
                {
                AddEditBdgtBtn.Text = "Add Budget";
                    HeaderLabel.Text = "Add Budget";
                }
                else
                {
                AddEditBdgtBtn.Text = "Edit Budget";
                HeaderLabel.Text = "Edit Budget";

                IRepository<Budget> budgetRepo = new Repository<Budget>(dbh.Database);

                    Budget budget = await budgetRepo.Get(budgetID.ToString());
                    if (budget != null)
                    {
                    AppSettings.MakeLog(TAG, "BudgetId  " + budgetID);

                        Name.Text = budget.Name.ToString();
                        //TransactionType.SelectedItem = transaction.Amount.ToString();
                    }
                }
            
        }

        private async void AddEditBdgtBtn_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Name.Text))
            {
                AppSettings.MakeToast("Please enter valid Budget Name");
                //Show message that data amount and message cannot be empty
            }
            else
            {
                    if (budgetID == 0)
                    {//For adding new Transaction
                        Budget newBudget = new Budget();
                        
                        newBudget.Name = Name.Text;
                        newBudget.UserId = PrefManager.getUserID();
                        newBudget.DateAdded = DateTime.Now.ToString();
                        newBudget.DateModified = DateTime.Now.ToString();
                        newBudget.Status = 1;

                        IRepository<Budget> budgetRepo = new Repository<Budget>(dbh.Database);

                        int result = await budgetRepo.Insert(newBudget);
                    AppSettings.MakeToast("Budget Added Successfully");
                    AppSettings.MakeLog(TAG, "Budget Added Status " + result + "\n On Date " + DateTime.Now.ToString());
                    }
                    else
                    {//For Updating existing Transaction
                        Budget newBudget = new Budget();
                        newBudget.Name = Name.Text;
                        newBudget.DateModified = "";
    
                        IRepository<Budget> budgetRepo = new Repository<Budget>(dbh.Database);

                        int result = await budgetRepo.Update(newBudget);
                    AppSettings.MakeToast("Budget Details Modified Successfully");
                }

                await Navigation.PopAsync();

            }

        }
    }
}