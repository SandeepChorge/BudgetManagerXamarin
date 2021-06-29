using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NewRestTest.model
{

    public class ProductModel 
        {
            public int id { get; set; }
            public string title { get; set; }
            public double price { get; set; }
            public string description { get; set; }
            public string category { get; set; }
            public string image { get; set; }

            public ObservableCollection<ProductModel> productModels { get; set; }
        }
   
}
