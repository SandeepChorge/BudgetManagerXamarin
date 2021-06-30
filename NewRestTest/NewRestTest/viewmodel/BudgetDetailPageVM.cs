using NewRestTest.appview;
using NewRestTest.database;
using NewRestTest.model;
using NewRestTest.utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace NewRestTest.viewmodel
{
    public class BudgetDetailPageVM : MyBaseViewModel
    {

        MainDB dbh;
        INavigation navigation;
        int BudgetID;
        IRepository<Transaction> transactionRepo;

        public ObservableCollection<Transaction> transactions = new ObservableCollection<Transaction>();
        public ObservableCollection<Transaction> Transactions
        {
            get { return transactions; }

            set
            {
                if (transactions != value)
                {
                    transactions = value;
                    OnPropertyChanged("Transactions");
                }
            }
        }

        private String budgetName;
        public String BudgetName
        {
            get { return budgetName; }
            set
            {
                if (budgetName != value)
                {
                    budgetName = value;
                    OnPropertyChanged("BudgetName");
                }
            }
        }

        private String totalIncome;
        public String TotalIncome
        {
            get { return totalIncome; }
            set
            {
                if (totalIncome != value)
                {
                    totalIncome = value;
                    OnPropertyChanged("TotalIncome");
                }
            }
        }

        private String totalExpense;
        public String TotalExpense
        {
            get { return totalExpense; }
            set
            {
                if (totalExpense != value)
                {
                    totalExpense = value;
                    OnPropertyChanged("TotalExpense");
                }
            }
        }

        public BudgetDetailPageVM(int BudgetID, INavigation navigation)
        {
            this.navigation = navigation;
            this.BudgetID = BudgetID;
            dbh = App.getMainDatabase;
            transactionRepo = new Repository<Transaction>(dbh.Database);
            //GetData(1);

        }

        public async void GetData(int Type)
        {
            GetBudgetModelDetails();
            transactions = new ObservableCollection<Transaction>(
                await transactionRepo.Get<Transaction>(t => t.BudgetId == BudgetID &&  t.Type == Type && t.Status == 1));
            OnPropertyChanged("Transactions");
            AppSettings.MakeToast("Count For type "+Type+" is " + transactions.Count);
        }

        private async void GetBudgetModelDetails()
        {
            AppSettings.MakeLog("Budget id detail is  ", "->" + BudgetID);
            List<BudgetListModel> budgetModel = await dbh.Database.QueryAsync<BudgetListModel>(
               @"SELECT b.*,SUM(i.Amount) as TotalIncome,SUM(e.Amount) TotalExpense from
                   budgets b LEFT JOIN transactions i on (b.BudgetId = i.BudgetId AND i.Type = 1)
                    LEFT JOIN transactions e on (b.BudgetId = e.BudgetId AND e.Type = 2)
                      where b.BudgetId = ? AND b.UserId = ? AND b.Status = 1
                      group by b.BudgetId", new string[2] { BudgetID.ToString(),PrefManager.getUserID().ToString() });
            if (budgetModel != null && budgetModel.Count>0)
            {
                AppSettings.MakeLog("Budget detail is  ", "-> is not  null "+ budgetModel[0].Name);
                budgetName = budgetModel[0].Name;
                totalIncome = budgetModel[0].TotalIncome.ToString();
                totalExpense = budgetModel[0].TotalExpense.ToString();
                OnPropertyChanged("BudgetName");
                OnPropertyChanged("TotalIncome");
                OnPropertyChanged("TotalExpense");
            }
            else
            {
                AppSettings.MakeLog("Budget detail is  ", "-> is null");
            }
        }

        public void CarouselItemChanged(string selectedItem)
        {
            //AppSettings.MakeToast("Current item " + selectedItem);
            if (selectedItem.Equals("Income"))
            {
                AppSettings.MakeLog("BudgetDetailVM", "Filtering with Type 1");
                /*                Transactions = new ObservableCollection<Transaction>(transactions.Where((tran) => tran.Type == 1));
                                AppSettings.MakeToast("Count is " + Transactions.Count);
                                OnPropertyChanged("Transactions");*/
                GetData(1);
            }
            else
            {
                AppSettings.MakeLog("BudgetDetailVM", "Filtering with Type 2");
                /*Transactions = new ObservableCollection<Transaction>(transactions.Where((tran) => tran.Type == 2));
                AppSettings.MakeToast("Count is " + Transactions.Count);
                OnPropertyChanged("Transactions");*/
                GetData(2);
            }
        }

        public async void AddNewTransaction()
        {
            ManageTransactionModel manageTransactionModel = new ManageTransactionModel
            {
                BudgetId = BudgetID,
                TransactionId = 0,
                Type = 1
            };
            await navigation.PushAsync(new AddEditTransaction(manageTransactionModel));
        }

    }
}
