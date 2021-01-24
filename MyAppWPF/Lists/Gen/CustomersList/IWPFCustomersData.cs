//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFList.Customers
{
    public interface IWPFCustomersDataConnection
    {
        public CustomersDataContext GetDataContext(out string error);
        List<ModelNotifiedForCustomers> GetAllCustomers(out string error);
        public void SaveData(ModelNotifiedForCustomers modelNotifiedForCustomers, out string error);
        public void DeleteData(ModelNotifiedForCustomers modelNotifiedForCustomers, out string error);
        public void AddData(ModelNotifiedForCustomers modelNotifiedForCustomers, out string error);

    }
}

