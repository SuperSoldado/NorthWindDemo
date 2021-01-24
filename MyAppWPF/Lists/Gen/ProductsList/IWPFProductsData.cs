//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFList.Products
{
    public interface IWPFProductsDataConnection
    {
        public ProductsDataContext GetDataContext(out string error);
        List<ModelNotifiedForProducts> GetAllProducts(out string error);
        public void SaveData(ModelNotifiedForProducts modelNotifiedForProducts, out string error);
        public void DeleteData(ModelNotifiedForProducts modelNotifiedForProducts, out string error);
        public void AddData(ModelNotifiedForProducts modelNotifiedForProducts, out string error);

        //test
        public List<ModelNotifiedForSuppliers> GetAll_Suppliers(out string error);
        //test
        public List<ModelNotifiedForCategories> GetAll_Categories(out string error);
    }
}

