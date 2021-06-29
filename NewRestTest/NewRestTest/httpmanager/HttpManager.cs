using NewRestTest.utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NewRestTest.httpmanager
{
    public class HttpManager
    {

        private static HttpManager objHttpManager = null;
        private readonly IMessageService _messageService;
        public HttpManager()
        {
            this._messageService = DependencyService.Get<IMessageService>();
        }
        public static HttpManager getInstance()
        {
            if (objHttpManager == null)
            {
                objHttpManager = new HttpManager();
            }

            return objHttpManager;
        }

        public async Task<string> makePostRequest(String url, Object obj)
        {
            string strResponse = null;
            HttpClientHandler handler = new HttpClientHandler();
            using (var client = new HttpClient(handler, false))
            {
                client.Timeout = TimeSpan.FromSeconds(AppSettings.TIMEOUT);
                //client.BaseAddress = new Uri(AppSettings.APP_BASEADDRESS_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("languageCode", "EN");
                client.DefaultRequestHeaders.Add("Platform", AppSettings.APP_Platform);
                client.DefaultRequestHeaders.Add("AppVersion", "1.0");
              //  client.DefaultRequestHeaders.Add("UserId", AppSettings.UserID);

                using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, AppSettings.APP_BASEADDRESS_URL + url))
                {
                    req.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                    Debug.WriteLine(" --- request content : {0}", JsonConvert.SerializeObject(obj).ToString());
                    try
                    {
                        var json = JsonConvert.SerializeObject(obj);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        var uri = AppSettings.APP_BASEADDRESS_URL + url;
                        Debug.WriteLine("@@@@@@@@@@@@@@@@@" + uri.ToString());
                        var response = await client.PostAsync(uri, content);
                        strResponse = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            Debug.WriteLine("@@@@@@@@@@@@@@@@@" + strResponse);
                            client.Dispose();
                        }
                        else
                        {
                            client.Dispose();
                            await this._messageService.ShowAsync(response.ReasonPhrase.Trim().ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        client.Dispose();
                        Console.WriteLine(e.Message);
                        //await this._messageService.ShowAsync("Please check internet connection.");
                    }
                }
            }

            return strResponse;
        }

        public async Task<string> makeGetRequest(String url)
        {
            string strResponse = null;
            HttpClientHandler handler = new HttpClientHandler();
            using (var client = new HttpClient(handler, false))
            {
                client.Timeout = TimeSpan.FromSeconds(AppSettings.TIMEOUT);
                //client.BaseAddress = new Uri(AppSettings.APP_BASEADDRESS_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("languageCode", "EN");
                client.DefaultRequestHeaders.Add("Platform", AppSettings.APP_Platform);
                client.DefaultRequestHeaders.Add("AppVersion", "1.0");
                //  client.DefaultRequestHeaders.Add("UserId", AppSettings.UserID);

                using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, AppSettings.APP_BASEADDRESS_URL + url))
                {
                   // req.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                 //   Debug.WriteLine(" --- request content : {0}", JsonConvert.SerializeObject(obj).ToString());
                    try
                    {
                        var uri = AppSettings.APP_BASEADDRESS_URL + url;
                        Debug.WriteLine("@@@@@@@@@@@@@@@@@" + uri.ToString());
                        var response = await client.GetAsync(uri);
                        strResponse = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            Debug.WriteLine("@@@@@@@@@@@@@@@@@" + strResponse);
                            client.Dispose();
                        }
                        else
                        {
                            client.Dispose();
                            await this._messageService.ShowAsync(response.ReasonPhrase.Trim().ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        client.Dispose();
                        Console.WriteLine(e.Message);
                        //await this._messageService.ShowAsync("Please check internet connection.");
                    }
                }
            }

            return strResponse;
        }

        public void showMessage(string msg)
        {
            try
            {
                Debug.WriteLine("--> " + msg);
                //await this._messageService.ShowAsync(msg);
            }
            catch( Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
