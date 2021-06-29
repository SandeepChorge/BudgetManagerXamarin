using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewRestTest.utils
{
    public interface IMessageService
    {
        Task ShowAsync(string message);

      /*  Task<bool> AskAsync(string message);

        Task<bool> Custom1AskAsync(string message);

        Task<bool> Custom2AskAsync(string message);

        Task DoAsync(string message, Action afterHideCallback);*/
    }

    public class MessageService : IMessageService
    {
        public async Task ShowAsync(string message)
        {
            await App.Current.MainPage.DisplayAlert("Alert", message, "OK");
        }
    }
}
