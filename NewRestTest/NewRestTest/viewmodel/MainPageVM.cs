using MvvmHelpers;
using MvvmHelpers.Commands;
using NewRestTest.httpmanager;
using NewRestTest.model;
using NewRestTest.utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NewRestTest.viewmodel
{
    public class MainPageVM : BaseViewModel
    {
        HttpManager httpManager;
        string result = "";
         public ObservableCollection<ProductModel> Models = new ObservableCollection<ProductModel>();

        public System.Windows.Input.ICommand IncreaseCount { get; }

        public ObservableCollection<ProductModel> ProductListDetails
        {
            get { return Models; }

            set
            {
                if (Models != value)
                {
                    Models = value;
                    OnPropertyChanged("ProductListDetails");
                }
            }
        }

        public string Result
        {
            get { return result; }

            set
            {
                if (result != value)
                {
                    result = value;
                    OnPropertyChanged("Result");
                }
            }
        }
        public MainPageVM()
        {
            IncreaseCount = new Command(OnClickedAPICall);
        }

        public async void OnClickedAPICall()
        {
            try
            {
                AppSettings.MakeAnalyticsEvent("Clicked Call RestAPI");
                httpManager = HttpManager.getInstance();
                result = await httpManager.makeGetRequest(AppSettings.PRODUCT_URL);

                if (result != null)
                {
                    

                    //re.Invo(this, new PropertyChangedEventArgs("Result"));

                    //OnPropertyChanged("Result");
                    httpManager.showMessage("REsult is " + result);

                    Models = JsonConvert.DeserializeObject<ObservableCollection<ProductModel>>(result);
                    OnPropertyChanged("ProductListDetails");
                    //List<ProductModel> productModels = JsonConvert.DeserializeObject<List<ProductModel>>(result);

                    /*  if (productModels !=null)
                      {
                          httpManager.showMessage("List size is " + productModels.Count);
                      }
                      else
                      {
                          httpManager.showMessage("List size is null");
                      }*/
                }
                else
                {
                    result = "REsult is null";
                    OnPropertyChanged("Result");
                    httpManager.showMessage("REsult is null");
                }
                /*
                if (!string.IsNullOrEmpty(txtMobileNumber.Text) && txtMobileNumber.Text.Length >= 10)
                {
                    await GenerateOTP();
                }*/
            }
            catch (Exception ex)
            {
                httpManager.showMessage("EXCEPTION __<" + ex.Message);
            }
        }
    }
}
