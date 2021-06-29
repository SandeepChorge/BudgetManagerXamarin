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
            if (values[0] != null && values[1] != null && (double)values[1] > 0 && (double)values[0] > 0)
            {
                res = ((((double)values[1]) / ((double)values[0])) * 100) + " % of Savings Exhausted";
            }
            else
            {
                res = "No Savings yet";
                //$"{values[0]} {values[1]} "$"{values[0]} {values[1]} "
            }
            return res;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
