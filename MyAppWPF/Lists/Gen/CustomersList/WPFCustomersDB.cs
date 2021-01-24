//Track[0003] Template:WPF_Shared_DB.html
using System;
using System.Collections.Generic;
using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using MyApp.TransferObjects.REST;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace MyApp.WPFList.Customers
{
    public class WPFCustomersDB : IWPFCustomersDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFCustomersDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public CustomersDataContext GetDataContext(out string error)
        {
            CustomersDataContext dataContext = new CustomersDataContext();            
            error=null;
            dataContext.modelNotifiedForCustomersMain = GetAllCustomers(out error);
    
            

            return dataContext;
        }

        

        //private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
            //Notify("IncludeFolders");
        //}


        
        public List<ModelNotifiedForCustomers> GetAllCustomers(out string error)
        {
            error = null;
            try
            {
                CustomersBsn bsn = new CustomersBsn(wpfConfig);
                List<CustomersInfo> dbItems = bsn.GetAll();
                List<ModelNotifiedForCustomers> notifiedItems = new List<ModelNotifiedForCustomers>();

                foreach (CustomersInfo dbItem in dbItems)
                {
                    ModelNotifiedForCustomers itemToAdd = new ModelNotifiedForCustomers();
                    Cloner.CopyAllTo(typeof(CustomersInfo), dbItem, typeof(ModelNotifiedForCustomers), itemToAdd);
                    itemToAdd.ItemChanged = false;
                    itemToAdd.NewItem = false;
                    notifiedItems.Add(itemToAdd);
                }

                return notifiedItems;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return null;
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

