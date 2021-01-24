//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFForms.Products
{
    public interface IWPFProductsDataConnection
    {
        public ProductsDataContext GetDataContext(int ProductID,out string error);
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public ProductsDataContext GetEmptyDataContext(out string error);
        ModelNotifiedForProducts GetProductsByID(int ProductID, out string error);
        public void SaveData(ModelNotifiedForProducts modelNotifiedForProducts, out string error);
        public void DeleteData(ModelNotifiedForProducts modelNotifiedForProducts, out string error);
        public void AddData(ModelNotifiedForProducts modelNotifiedForProducts, out string error);

        //test
        public List<ModelNotifiedForSuppliers> GetAll_Suppliers(out string error);
        //test
        public List<ModelNotifiedForCategories> GetAll_Categories(out string error);
    }
}

