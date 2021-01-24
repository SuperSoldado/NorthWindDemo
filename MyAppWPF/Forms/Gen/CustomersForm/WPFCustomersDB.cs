//Track[0003] Template:WPF_Shared_DB.html
using System;
using System.Collections.Generic;
using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using MyApp.TransferObjects.REST;
namespace MyApp.WPFForms.Customers
{
    public class WPFCustomersDB : IWPFCustomersDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFCustomersDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public CustomersDataContext GetDataContext(string CustomerID,out string error)
        {
            CustomersDataContext dataContext = new CustomersDataContext();            
            error=null;
            dataContext.modelNotifiedForCustomersMain = GetCustomersByID(CustomerID, out error);
    
            

            return dataContext;
        }

        
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public CustomersDataContext GetEmptyDataContext(out string error)
        {
            CustomersDataContext dataContext = new CustomersDataContext();
            error=null;
            dataContext.modelNotifiedForCustomersMain = new ModelNotifiedForCustomers();
    

            return dataContext;
        }



        public ModelNotifiedForCustomers GetCustomersByID(string CustomerID, out string error)
        {
            error = null;
            CustomersBsn bsn = new CustomersBsn(wpfConfig);
            CustomersInfo dbItem = bsn.GetValueByID(CustomerID);
            ModelNotifiedForCustomers item = new ModelNotifiedForCustomers();
            Cloner.CopyAllTo(typeof(CustomersInfo), dbItem, typeof(ModelNotifiedForCustomers), item);
            return item;
        }
        
        
        
        public void SaveData(ModelNotifiedForCustomers modelNotifiedForCustomers, out string error)
        {
            CustomersBsn bsn = new CustomersBsn(wpfConfig);
            CustomersInfo dbItem = new CustomersInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomers), modelNotifiedForCustomers, typeof(CustomersInfo), dbItem);
            
            bsn.UpdateOne(dbItem, out error);
        }

        public void AddData(ModelNotifiedForCustomers modelNotifiedForCustomers, out string error)
        {
            CustomersBsn bsn = new CustomersBsn(wpfConfig);
            CustomersInfo dbItem = new CustomersInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomers), modelNotifiedForCustomers, typeof(CustomersInfo), dbItem);
            bsn.InsertOne(dbItem, out error);
            modelNotifiedForCustomers.NewItem = false;
            Cloner.CopyAllTo(typeof(CustomersInfo), dbItem, typeof(ModelNotifiedForCustomers), modelNotifiedForCustomers);
        }
        
        public void DeleteData(ModelNotifiedForCustomers modelNotifiedForCustomers, out string error)
        {
            CustomersBsn bsn = new CustomersBsn(wpfConfig);
            CustomersInfo dbItem = new CustomersInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomers), modelNotifiedForCustomers, typeof(CustomersInfo), dbItem);
            bsn.DeleteByID(dbItem, out error);
        }
    }
}

