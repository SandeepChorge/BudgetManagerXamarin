using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewRestTest.model
{
    public class 
        BudgetModels
    {
    }


    [Table("budgets")]
    public class Budget
    {
        [PrimaryKey, AutoIncrement]
        public int BudgetId { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }

        public int Status { get; set; }

        public string DateAdded { get; set; }

        public string DateModified { get; set; }
    }


    [Table("transactions")]
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int TransactionId { get; set; }
        public int BudgetId { get; set; }
        public double Amount { get; set; }
        public string Message { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public string DateAdded { get; set; }
        public string DateModified { get; set; }
    }

    public class ManageTransactionModel{
        public int BudgetId { get; set; }

        //1-Add , 2-Edit
        public int Type { get; set; }

        public int TransactionId { get; set; }
    }

    public class TransactionType
    {
        public TransactionType(string Type)
        {
            this.Type = Type;
        }
        string Type;
    }

    public class BudgetListModel
    {
        
        public int BudgetId { get; set; }
       
        public string Name { get; set; }

        public int Status { get; set; }

        public string DateAdded { get; set; }

        public string DateModified { get; set; }

        public double TotalIncome { get; set; }

        public double TotalExpense { get; set; }

    }
}
