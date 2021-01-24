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
namespace MyApp.WPFList.CustomerDemographics
{
    public class WPFCustomerDemographicsDB : IWPFCustomerDemographicsDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFCustomerDemographicsDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public CustomerDemographicsDataContext GetDataContext(out string error)
        {
            CustomerDemographicsDataContext dataContext = new CustomerDemographicsDataContext();            
            error=null;
            dataContext.modelNotifiedForCustomerDemographicsMain = GetAllCustomerDemographics(out error);
    
            

            return dataContext;
        }

        

        //private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
            //Notify("IncludeFolders");
        //}


        
        public List<ModelNotifiedForCustomerDemographics> GetAllCustomerDemographics(out string error)
        {
            error = null;
            try
            {
                CustomerDemographicsBsn bsn = new CustomerDemographicsBsn(wpfConfig);
                List<CustomerDemographicsInfo> dbItems = bsn.GetAll();
                List<ModelNotifiedForCustomerDemographics> notifiedItems = new List<ModelNotifiedForCustomerDemographics>();

                foreach (CustomerDemographicsInfo dbItem in dbItems)
                {
                    ModelNotifiedForCustomerDemographics itemToAdd = new ModelNotifiedForCustomerDemographics();
                    Cloner.CopyAllTo(typeof(CustomerDemographicsInfo), dbItem, typeof(ModelNotifiedForCustomerDemographics), itemToAdd);
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
        
        
        public void SaveData(ModelNotifiedForCustomerDemographics modelNotifiedForCustomerDemographics, out string error)
        {
            CustomerDemographicsBsn bsn = new CustomerDemographicsBsn(wpfConfig);
            CustomerDemographicsInfo dbItem = new CustomerDemographicsInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomerDemographics), modelNotifiedForCustomerDemographics, typeof(CustomerDemographicsInfo), dbItem);
            
            bsn.UpdateOne(dbItem, out error);
        }

        public void AddData(ModelNotifiedForCustomerDemographics modelNotifiedForCustomerDemographics, out string error)
        {
            CustomerDemographicsBsn bsn = new CustomerDemographicsBsn(wpfConfig);
            CustomerDemographicsInfo dbItem = new CustomerDemographicsInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomerDemographics), modelNotifiedForCustomerDemographics, typeof(CustomerDemographicsInfo), dbItem);
            bsn.InsertOne(dbItem, out error);
            modelNotifiedForCustomerDemographics.NewItem = false;
            Cloner.CopyAllTo(typeof(CustomerDemographicsInfo), dbItem, typeof(ModelNotifiedForCustomerDemographics), modelNotifiedForCustomerDemographics);
        }
        
        public void DeleteData(ModelNotifiedForCustomerDemographics modelNotifiedForCustomerDemographics, out string error)
        {
            CustomerDemographicsBsn bsn = new CustomerDemographicsBsn(wpfConfig);
            CustomerDemographicsInfo dbItem = new CustomerDemographicsInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomerDemographics), modelNotifiedForCustomerDemographics, typeof(CustomerDemographicsInfo), dbItem);
            bsn.DeleteByID(dbItem, out error);
        }
    }
}

