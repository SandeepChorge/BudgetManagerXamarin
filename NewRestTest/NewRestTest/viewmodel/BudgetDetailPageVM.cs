using NewRestTest.appview;
using NewRestTest.database;
using NewRestTest.model;
using NewRestTest.utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NewRestTest.viewmodel
{
    public class BudgetDetailPageVM : MyBaseViewModel
    {

        MainDB dbh;
        INavigation navigation;
        int BudgetID;
        IRepository<Transaction> transactionRepo;

        int SelectedTransactionType= 1;

        public ICommand EditBudgetCommand { private set; get; }


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

        private String remaining;
        public String Remaining
        {
            get { return remaining; }
            set
            {
                if (remaining != value)
                {
                    remaining = value;
                    OnPropertyChanged("Remaining");
                }
            }
        }

        
        public BudgetDetailPageVM(int BudgetID, INavigation navigation)
        {
            dbh = App.getMainDatabase;
            this.navigation = navigation;
            this.BudgetID = BudgetID;
            EditBudgetCommand = new MvvmHelpers.Commands.Command(EditBudget);

            transactionRepo = new Repository<Transaction>(dbh.Database);
           

        }

     
        public async void EditBudget()
        {
            await navigation.PushAsync(new AddEditBudget(BudgetID));
        }
        public async void GetData(int Type)
        {
            SelectedTransactionType = Type;
            GetBudgetModelDetails();
            transactions = new ObservableCollection<Transaction>(
                await transactionRepo.Get<Transaction>(t => t.BudgetId == BudgetID &&  t.Type == Type && t.Status == 1));
            OnPropertyChanged("Transactions");
            //AppSettings.MakeToast("Count For type "+Type+" is " + transactions.Count);
        }

        internal async void DeleteTransaction(Transaction tran)
        {
            IRepository<Transaction> tranRepo = new Repository<Transaction>(dbh.Database);

            tran.Status = 0;
            int result = await tranRepo.Update(tran);
            AppSettings.MakeLog("BudgetDetailVM", "Result of transactio delete is " + result);
            GetData(SelectedTransactionType);
            AppSettings.MakeToast("Transaction Deleted Successfully");
            
        }

        private async void GetBudgetModelDetails()
        {
            //AppSettings.MakeLog("Budget id detail is  ", "->" + BudgetID);
            List<BudgetListModel> budgetModel = await dbh.Database.QueryAsync<BudgetListModel>(
               @"SELECT b.*,
COALESCE((SELECT SUM(Amount) from transactions where b.BudgetId = BudgetId AND Type = 1 AND Status = 1),0) as TotalIncome,
COALESCE((SELECT SUM(Amount) from transactions where b.BudgetId = BudgetId AND Type = 2 AND Status = 1),0) TotalExpense 
from budgets b where b.BudgetId = ? AND b.UserId = ? AND b.Status = 1", new string[2] { BudgetID.ToString(),PrefManager.getUserID().ToString() });
            if (budgetModel != null && budgetModel.Count>0)
            {
                //AppSettings.MakeLog("Budget detail is  ", "-> is not  null "+ budgetModel[0].Name);
                budgetName = budgetModel[0].Name;
                totalIncome = budgetModel[0].TotalIncome.ToString();
                totalExpense = budgetModel[0].TotalExpense.ToString();
                remaining = (budgetModel[0].TotalIncome - budgetModel[0].TotalExpense).ToString();
                OnPropertyChanged("BudgetName");
                OnPropertyChanged("TotalIncome");
                OnPropertyChanged("TotalExpense");
                OnPropertyChanged("Remaining");
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

        public async void AddNewTransaction(ManageTransactionModel manageTransactionModel)
        {
            await navigation.PushAsync(new AddEditTransaction(manageTransactionModel));
        }

    }
}
