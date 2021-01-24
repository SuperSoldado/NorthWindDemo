//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFForms.Customers
{
    public interface IWPFCustomersDataConnection
    {
        public CustomersDataContext GetDataContext(string CustomerID,out string error);
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public CustomersDataContext GetEmptyDataContext(out string error);
        ModelNotifiedForCustomers GetCustomersByID(string CustomerID, out string error);
        public void SaveData(ModelNotifiedForCustomers modelNotifiedForCustomers, out string error);
        public void DeleteData(ModelNotifiedForCustomers modelNotifiedForCustomers, out string error);
        public void AddData(ModelNotifiedForCustomers modelNotifiedForCustomers, out string error);

    }
}

