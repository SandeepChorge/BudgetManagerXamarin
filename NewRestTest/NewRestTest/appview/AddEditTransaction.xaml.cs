using NewRestTest.database;
using NewRestTest.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewRestTest.appview
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditTransaction : ContentPage
    {
        ManageTransactionModel manageTransactionModel;
        MainDB dbh;
       /* List<TransactionType> TransactionTypes = new List<TransactionType>() {
        new model.TransactionType("Income"),
        new model.TransactionType("Expense")};*/

        public AddEditTransaction()
        {
            InitializeComponent();
            //BindingContext = this;

        }
        public AddEditTransaction(ManageTransactionModel manageTransactionModel)
        {
            InitializeComponent();
            dbh = App.getMainDatabase;
            this.manageTransactionModel = manageTransactionModel;
            HandleViews();
        }

        private async void HandleViews()
        {
            if (manageTransactionModel != null)
            {
                if(manageTransactionModel.Type == 1)
                {
                    AddEditTranBtn.Text = "Add Transaction";
                    HeaderLabel.Text = "Add Transaction";
                }
                else
                {
                    AddEditTranBtn.Text = "Edit Transaction";
                    HeaderLabel.Text = "Edit Transaction";

                    IRepository<Transaction> tranRepo = new Repository<Transaction>(dbh.Database);

                    Transaction transaction = await tranRepo.Get(manageTransactionModel.TransactionId.ToString());
                    if (transaction != null)
                    {
                        Debug.WriteLine("TransactionId  " + transaction.TransactionId+" AMT "+transaction.Amount);

                        TransactionAmt.Text = transaction.Amount.ToString();
                        Message.Text = transaction.Message.ToString();
                        //TransactionType.SelectedItem = transaction.Amount.ToString();
                    }
                }
            }
        }

        private async void AddEditTransaction_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(TransactionAmt.Text) ||
                String.IsNullOrEmpty(Message.Text))
            {
                Debug.WriteLine("Please enter valid details");
                //Show message that data amount and message cannot be empty
            }
            else
            {
                if (manageTransactionModel != null)
                {
                    if (manageTransactionModel.Type == 1)
                    {//For adding new Transaction
                        Transaction newTransaction = new Transaction();
                        newTransaction.BudgetId = manageTransactionModel.BudgetId;
                        newTransaction.Amount = double.Parse(TransactionAmt.Text);
                        newTransaction.Message = Message.Text;
                        newTransaction.DateAdded = DateTime.Now.ToString();
                        newTransaction.DateModified = DateTime.Now.ToString();
                        newTransaction.Type = TransactionType.SelectedIndex == 1 ? 1 : 2;
                        newTransaction.Status = 1;

                        IRepository<Transaction> tranRepo = new Repository<Transaction>(dbh.Database);

                        int result = await tranRepo.Insert(newTransaction);
                        Debug.WriteLine("Transaction Added Status " + result+"\n On Date "+ DateTime.Now.ToString());
                    }
                    else
                    {//For Updating existing Transaction
                        Transaction newTransaction = new Transaction();
                        newTransaction.BudgetId = manageTransactionModel.BudgetId;
                        newTransaction.TransactionId = manageTransactionModel.TransactionId;
                        newTransaction.Amount = double.Parse(TransactionAmt.Text);
                        newTransaction.Message = Message.Text;
                        newTransaction.DateModified = "";
                        newTransaction.Type = TransactionType.SelectedIndex == 1 ? 1 : 2;
                        newTransaction.Status = 1;

                        IRepository<Transaction> tranRepo = new Repository<Transaction>(dbh.Database);

                        int result = await tranRepo.Update(newTransaction);
                    }
                }
            }

        }
    }
}