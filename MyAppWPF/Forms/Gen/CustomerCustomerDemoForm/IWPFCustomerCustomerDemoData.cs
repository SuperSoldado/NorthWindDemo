//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFForms.CustomerCustomerDemo
{
    public interface IWPFCustomerCustomerDemoDataConnection
    {
        public CustomerCustomerDemoDataContext GetDataContext(string CustomerID,string CustomerTypeID,out string error);
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public CustomerCustomerDemoDataContext GetEmptyDataContext(out string error);
        ModelNotifiedForCustomerCustomerDemo GetCustomerCustomerDemoByID(string CustomerID,string CustomerTypeID, out string error);
        public void SaveData(ModelNotifiedForCustomerCustomerDemo modelNotifiedForCustomerCustomerDemo, out string error);
        public void DeleteData(ModelNotifiedForCustomerCustomerDemo modelNotifiedForCustomerCustomerDemo, out string error);
        public void AddData(ModelNotifiedForCustomerCustomerDemo modelNotifiedForCustomerCustomerDemo, out string error);

        //test
        public List<ModelNotifiedForCustomers> GetAll_Customers(out string error);
        //test
        public List<ModelNotifiedForCustomerDemographics> GetAll_CustomerDemographics(out string error);
    }
}

