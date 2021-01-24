//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFForms.Suppliers
{
    public interface IWPFSuppliersDataConnection
    {
        public SuppliersDataContext GetDataContext(int SupplierID,out string error);
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public SuppliersDataContext GetEmptyDataContext(out string error);
        ModelNotifiedForSuppliers GetSuppliersByID(int SupplierID, out string error);
        public void SaveData(ModelNotifiedForSuppliers modelNotifiedForSuppliers, out string error);
        public void DeleteData(ModelNotifiedForSuppliers modelNotifiedForSuppliers, out string error);
        public void AddData(ModelNotifiedForSuppliers modelNotifiedForSuppliers, out string error);

    }
}

