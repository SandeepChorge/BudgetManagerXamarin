using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using NewRestTest.model;
using SQLite;

namespace NewRestTest.database
{
    public class MainDB
    {
        private SQLiteAsyncConnection conn;

        public SQLiteAsyncConnection Database
        {
            get
            {
                return conn;
            }
        }
        public string StatusMessage { get; set; }


        public MainDB(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<UserModel>().Wait();
            conn.CreateTableAsync<Budget>().Wait();
            conn.CreateTableAsync<Transaction>().Wait();
        }

        public async Task AddNewUserAsync(string name)
        {
            int result = 0;
            try
            {
                //basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                result = await conn.InsertAsync(new UserModel { Name = name });

                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            try
            {
                return await conn.Table<UserModel>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<UserModel>(); // TODO: return a list of people saved to the Person table in the database
        }

        public async Task<List<UserModel>> ValidateUser(string MobileNo, string Email)
        {
            try
            {
                var user = await conn.QueryAsync<List<UserModel>>("SELECT * FROM users WHERE Email = ? AND MobileNumber = ?", Email,MobileNo);
               if(user!=null && user.Count>0 )
                {
                    Console.WriteLine("a ");
                    return null;
                }
                else
                {

                }
                /* var apple = from s in conn.Table<UserModel>()
                             where (s.MobileNumber.Equals(MobileNo) &&
                             s.Email.Equals(Email))
                             select s;
                 return apple;*/
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return null; // TODO: return a list of people saved to the Person table in the database
        }

        public List<BudgetListModel> JustTry()
        {
            
            var res = conn.QueryAsync<BudgetListModel>(
                @"SELECT b.*,SUM(i.Amount) as TotalIncome,SUM(e.Amount) TotalExpense from
                   budgets b LEFT JOIN transactions i on (b.BudgetId = i.BudgetId AND i.Type = 1)
                    LEFT JOIN transactions e on (b.BudgetId = e.BudgetId AND e.Type = 2)
                      where b.BudgetId = 1");

           

            //IObservable<List<BudgetListModel>> l = res.Result;
            return res.Result;
            /*List<BudgetListModel> models = res.Result;
             * if (models != null )
              {
                  Debug.WriteLine("Query result is not null " + models.Count);
                  Debug.WriteLine("NAme "+models[0].Name);
                  Debug.WriteLine("TotalIncome " + models[0].TotalIncome);
                  Debug.WriteLine("TotalExpense " + models[0].TotalExpense);
                  Debug.WriteLine("TotalIncome " + models[0].DateAdded);

              }
              else
              {
                  Debug.WriteLine("Query result is null");
              }*/


        }
    }
}
