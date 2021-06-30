using NewRestTest.utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace NewRestTest.converter
{
    public class SavingsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var res = "";
            double income = 0;
            double expense = 0;
            int percent = 0;
            if (values[0] != null && values[1] != null)
            {
                income = (double)values[0];
                expense = (double)values[1];              
            }

            AppSettings.MakeLog("INCOME " + income, "EXPENSE " + expense);
            if (income == 0 && expense == 0)
                res = "No Savings yet";
            else if (income > 0 && expense < 0)
                res = "Yay! 100% Savings";
            else if ((income < 0 && expense > 0) || (expense>income && income <=0))
                res = "No Savings yet";
            else if(expense > income)
            {
                percent = (int)((income / expense) * 100);
                res = "You have exceeded "+percent+" % of your Savings";
            }
            else
            {
                percent = (int)((expense / income) * 100);
                res = percent + " % of Savings Exhausted";
            }
            return res;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
