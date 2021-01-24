//Track[0003] Template:WPF_Shared_DB.html
using System;
using System.Collections.Generic;
using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using MyApp.TransferObjects.REST;
namespace MyApp.WPFForms.CustomerCustomerDemo
{
    public class WPFCustomerCustomerDemoDB : IWPFCustomerCustomerDemoDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFCustomerCustomerDemoDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public CustomerCustomerDemoDataContext GetDataContext(string CustomerID,string CustomerTypeID,out string error)
        {
            CustomerCustomerDemoDataContext dataContext = new CustomerCustomerDemoDataContext();            
            error=null;
            dataContext.modelNotifiedForCustomerCustomerDemoMain = GetCustomerCustomerDemoByID(CustomerID,CustomerTypeID, out error);
    
            dataContext.modelNotifiedForCustomers = GetAll_Customers(out error);
            dataContext.modelNotifiedForCustomerDemographics = GetAll_CustomerDemographics(out error);
            dataContext.PopulateComboBoxesItemSource();
            

            return dataContext;
        }

        
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public CustomerCustomerDemoDataContext GetEmptyDataContext(out string error)
        {
            CustomerCustomerDemoDataContext dataContext = new CustomerCustomerDemoDataContext();
            error=null;
            dataContext.modelNotifiedForCustomerCustomerDemoMain = new ModelNotifiedForCustomerCustomerDemo();
    

            return dataContext;
        }



        public ModelNotifiedForCustomerCustomerDemo GetCustomerCustomerDemoByID(string CustomerID,string CustomerTypeID, out string error)
        {
            error = null;
            CustomerCustomerDemoBsn bsn = new CustomerCustomerDemoBsn(wpfConfig);
            CustomerCustomerDemoInfo dbItem = bsn.GetValueByID(CustomerID,CustomerTypeID);
            ModelNotifiedForCustomerCustomerDemo item = new ModelNotifiedForCustomerCustomerDemo();
            Cloner.CopyAllTo(typeof(CustomerCustomerDemoInfo), dbItem, typeof(ModelNotifiedForCustomerCustomerDemo), item);
            return item;
        }
        
        
        /// <summary>
        /// Retrieve all data from Customers table. Used to fill combo box.
        /// </summary>
        /// <returns>List of Customers</returns>
        public List<ModelNotifiedForCustomers> GetAll_Customers(out string error)
        {
            error = null;
            CustomersBsn bsn = new CustomersBsn(wpfConfig);
            List<CustomersInfo> dbItems = bsn.GetAll();
            List<ModelNotifiedForCustomers> notifiedItems = new List<ModelNotifiedForCustomers>();

            foreach (CustomersInfo dbItem in dbItems)
            {
                ModelNotifiedForCustomers itemToAdd = new ModelNotifiedForCustomers();
                Cloner.CopyAllTo(typeof(CustomersInfo), dbItem, typeof(ModelNotifiedForCustomers), itemToAdd);
                notifiedItems.Add(itemToAdd);
            }
            return notifiedItems;
        }
        /// <summary>
        /// Retrieve all data from CustomerDemographics table. Used to fill combo box.
        /// </summary>
        /// <returns>List of CustomerDemographics</returns>
        public List<ModelNotifiedForCustomerDemographics> GetAll_CustomerDemographics(out string error)
        {
            error = null;
            CustomerDemographicsBsn bsn = new CustomerDemographicsBsn(wpfConfig);
            List<CustomerDemographicsInfo> dbItems = bsn.GetAll();
            List<ModelNotifiedForCustomerDemographics> notifiedItems = new List<ModelNotifiedForCustomerDemographics>();

            foreach (CustomerDemographicsInfo dbItem in dbItems)
            {
                ModelNotifiedForCustomerDemographics itemToAdd = new ModelNotifiedForCustomerDemographics();
                Cloner.CopyAllTo(typeof(CustomerDemographicsInfo), dbItem, typeof(ModelNotifiedForCustomerDemographics), itemToAdd);
                notifiedItems.Add(itemToAdd);
            }
            return notifiedItems;
        }
        
        public void SaveData(ModelNotifiedForCustomerCustomerDemo modelNotifiedForCustomerCustomerDemo, out string error)
        {
            CustomerCustomerDemoBsn bsn = new CustomerCustomerDemoBsn(wpfConfig);
            CustomerCustomerDemoInfo dbItem = new CustomerCustomerDemoInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomerCustomerDemo), modelNotifiedForCustomerCustomerDemo, typeof(CustomerCustomerDemoInfo), dbItem);
            
            bsn.UpdateOne(dbItem, out error);
        }

        public void AddData(ModelNotifiedForCustomerCustomerDemo modelNotifiedForCustomerCustomerDemo, out string error)
        {
            CustomerCustomerDemoBsn bsn = new CustomerCustomerDemoBsn(wpfConfig);
            CustomerCustomerDemoInfo dbItem = new CustomerCustomerDemoInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomerCustomerDemo), modelNotifiedForCustomerCustomerDemo, typeof(CustomerCustomerDemoInfo), dbItem);
            bsn.InsertOne(dbItem, out error);
            modelNotifiedForCustomerCustomerDemo.NewItem = false;
            Cloner.CopyAllTo(typeof(CustomerCustomerDemoInfo), dbItem, typeof(ModelNotifiedForCustomerCustomerDemo), modelNotifiedForCustomerCustomerDemo);
        }
        
        public void DeleteData(ModelNotifiedForCustomerCustomerDemo modelNotifiedForCustomerCustomerDemo, out string error)
        {
            CustomerCustomerDemoBsn bsn = new CustomerCustomerDemoBsn(wpfConfig);
            CustomerCustomerDemoInfo dbItem = new CustomerCustomerDemoInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomerCustomerDemo), modelNotifiedForCustomerCustomerDemo, typeof(CustomerCustomerDemoInfo), dbItem);
            bsn.DeleteByID(dbItem, out error);
        }
    }
}

