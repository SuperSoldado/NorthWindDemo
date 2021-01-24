//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFForms.Categories
{
    public interface IWPFCategoriesDataConnection
    {
        public CategoriesDataContext GetDataContext(int CategoryID,out string error);
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public CategoriesDataContext GetEmptyDataContext(out string error);
        ModelNotifiedForCategories GetCategoriesByID(int CategoryID, out string error);
        public void SaveData(ModelNotifiedForCategories modelNotifiedForCategories, out string error);
        public void DeleteData(ModelNotifiedForCategories modelNotifiedForCategories, out string error);
        public void AddData(ModelNotifiedForCategories modelNotifiedForCategories, out string error);

    }
}

