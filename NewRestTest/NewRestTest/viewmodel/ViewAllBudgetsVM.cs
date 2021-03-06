using NewRestTest.database;
using NewRestTest.model;
using NewRestTest.utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace NewRestTest.viewmodel
{
    public class ViewAllBudgetsVM : MyBaseViewModel
    {
        MainDB dbh;
        //BudgetListModel
        public ObservableCollection<BudgetListModel> Models = new ObservableCollection<BudgetListModel>();
        public ObservableCollection<BudgetListModel> Budgets
        {
            get { return Models; }

            set
            {
                if (Models != value)
                {
                    Models = value;
                    OnPropertyChanged("Budgets");
                }
            }
        }

        public async void AddTempData()
        {
           /* IRepository<Budget> userRepo = new Repository<Budget>(dbh.Database);

            List<Budget> alldata = await userRepo.Get<Budget>();

            foreach(Budget b in alldata){
                Debug.WriteLine("-- "+b.Name+"\ts "+b.Status+"\tu "+b.UserId+"\tid "+b.BudgetId);
            }*/
            


            //addTempData();

            Models = new ObservableCollection<BudgetListModel>(await dbh.Database.QueryAsync<BudgetListModel>(
               @"SELECT b.*,
COALESCE((SELECT SUM(Amount) from transactions where b.BudgetId = BudgetId AND Type = 1 AND Status = 1),0) as TotalIncome,
COALESCE((SELECT SUM(Amount) from transactions where b.BudgetId = BudgetId AND Type = 2 AND Status = 1),0) TotalExpense 
from budgets b where b.UserId = ? AND b.Status = 1
                      ", new string[1] { PrefManager.getUserID().ToString()}));
            Debug.WriteLine("Models count "+Models.Count);
            OnPropertyChanged("Budgets");
        }

        internal void FindIndex(object myItem)
        {
            //Debug.WriteLine("Models count " + Models.IndexOf(myItem));
        }

        public  void addTempData()
        {
          /*  Transaction newTransaction = new Transaction();
            newTransaction.BudgetId = 1;
            newTransaction.Amount = 100;
            newTransaction.Message = "Temp Income data ";
            newTransaction.DateAdded = DateTime.Now.ToString();
            newTransaction.DateModified = DateTime.Now.ToString();
            newTransaction.Type = 1;
            newTransaction.Status = 1;

            IRepository<Transaction> tranRepo = new Repository<Transaction>(dbh.Database);

            await tranRepo.Insert(newTransaction);
            await tranRepo.Insert(newTransaction);

            newTransaction.Type = 2;
            newTransaction.Amount = 50;
            newTransaction.Message = "Temp Expense data ";

            await tranRepo.Insert(newTransaction);
            await tranRepo.Insert(newTransaction);*/

        }

        public ViewAllBudgetsVM()
        {
            dbh = App.getMainDatabase;
            //AddTempData();
            //IRepository<BudgetListModel> userRepo = new Repository<BudgetListModel>(dbh.Database);
        }
    }
}
