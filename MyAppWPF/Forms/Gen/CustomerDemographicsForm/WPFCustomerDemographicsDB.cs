//Track[0003] Template:WPF_Shared_DB.html
using System;
using System.Collections.Generic;
using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using MyApp.TransferObjects.REST;
namespace MyApp.WPFForms.CustomerDemographics
{
    public class WPFCustomerDemographicsDB : IWPFCustomerDemographicsDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFCustomerDemographicsDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public CustomerDemographicsDataContext GetDataContext(string CustomerTypeID,out string error)
        {
            CustomerDemographicsDataContext dataContext = new CustomerDemographicsDataContext();            
            error=null;
            dataContext.modelNotifiedForCustomerDemographicsMain = GetCustomerDemographicsByID(CustomerTypeID, out error);
    
            

            return dataContext;
        }

        
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public CustomerDemographicsDataContext GetEmptyDataContext(out string error)
        {
            CustomerDemographicsDataContext dataContext = new CustomerDemographicsDataContext();
            error=null;
            dataContext.modelNotifiedForCustomerDemographicsMain = new ModelNotifiedForCustomerDemographics();
    

            return dataContext;
        }



        public ModelNotifiedForCustomerDemographics GetCustomerDemographicsByID(string CustomerTypeID, out string error)
        {
            error = null;
            CustomerDemographicsBsn bsn = new CustomerDemographicsBsn(wpfConfig);
            CustomerDemographicsInfo dbItem = bsn.GetValueByID(CustomerTypeID);
            ModelNotifiedForCustomerDemographics item = new ModelNotifiedForCustomerDemographics();
            Cloner.CopyAllTo(typeof(CustomerDemographicsInfo), dbItem, typeof(ModelNotifiedForCustomerDemographics), item);
            return item;
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

