//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFList.Suppliers
{
    public interface IWPFSuppliersDataConnection
    {
        public SuppliersDataContext GetDataContext(out string error);
        List<ModelNotifiedForSuppliers> GetAllSuppliers(out string error);
        public void SaveData(ModelNotifiedForSuppliers modelNotifiedForSuppliers, out string error);
        public void DeleteData(ModelNotifiedForSuppliers modelNotifiedForSuppliers, out string error);
        public void AddData(ModelNotifiedForSuppliers modelNotifiedForSuppliers, out string error);

    }
}

