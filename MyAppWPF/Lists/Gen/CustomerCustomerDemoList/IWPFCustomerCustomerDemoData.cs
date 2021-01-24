//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFList.CustomerCustomerDemo
{
    public interface IWPFCustomerCustomerDemoDataConnection
    {
        public CustomerCustomerDemoDataContext GetDataContext(out string error);
        List<ModelNotifiedForCustomerCustomerDemo> GetAllCustomerCustomerDemo(out string error);
        public void SaveData(ModelNotifiedForCustomerCustomerDemo modelNotifiedForCustomerCustomerDemo, out string error);
        public void DeleteData(ModelNotifiedForCustomerCustomerDemo modelNotifiedForCustomerCustomerDemo, out string error);
        public void AddData(ModelNotifiedForCustomerCustomerDemo modelNotifiedForCustomerCustomerDemo, out string error);

        //test
        public List<ModelNotifiedForCustomers> GetAll_Customers(out string error);
        //test
        public List<ModelNotifiedForCustomerDemographics> GetAll_CustomerDemographics(out string error);
    }
}

