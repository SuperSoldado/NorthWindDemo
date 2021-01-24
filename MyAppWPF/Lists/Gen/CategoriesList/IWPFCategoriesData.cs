//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFList.Categories
{
    public interface IWPFCategoriesDataConnection
    {
        public CategoriesDataContext GetDataContext(out string error);
        List<ModelNotifiedForCategories> GetAllCategories(out string error);
        public void SaveData(ModelNotifiedForCategories modelNotifiedForCategories, out string error);
        public void DeleteData(ModelNotifiedForCategories modelNotifiedForCategories, out string error);
        public void AddData(ModelNotifiedForCategories modelNotifiedForCategories, out string error);

    }
}

