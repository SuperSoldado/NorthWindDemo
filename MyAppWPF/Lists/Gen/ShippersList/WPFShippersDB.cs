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
namespace MyApp.WPFList.Shippers
{
    public class WPFShippersDB : IWPFShippersDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFShippersDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public ShippersDataContext GetDataContext(out string error)
        {
            ShippersDataContext dataContext = new ShippersDataContext();            
            error=null;
            dataContext.modelNotifiedForShippersMain = GetAllShippers(out error);
    
            

            return dataContext;
        }

        

        //private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
            //Notify("IncludeFolders");
        //}


        
        public List<ModelNotifiedForShippers> GetAllShippers(out string error)
        {
            error = null;
            try
            {
                ShippersBsn bsn = new ShippersBsn(wpfConfig);
                List<ShippersInfo> dbItems = bsn.GetAll();
                List<ModelNotifiedForShippers> notifiedItems = new List<ModelNotifiedForShippers>();

                foreach (ShippersInfo dbItem in dbItems)
                {
                    ModelNotifiedForShippers itemToAdd = new ModelNotifiedForShippers();
                    Cloner.CopyAllTo(typeof(ShippersInfo), dbItem, typeof(ModelNotifiedForShippers), itemToAdd);
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
        
        
        public void SaveData(ModelNotifiedForShippers modelNotifiedForShippers, out string error)
        {
            ShippersBsn bsn = new ShippersBsn(wpfConfig);
            ShippersInfo dbItem = new ShippersInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForShippers), modelNotifiedForShippers, typeof(ShippersInfo), dbItem);
            
            bsn.UpdateOne(dbItem, out error);
        }

        public void AddData(ModelNotifiedForShippers modelNotifiedForShippers, out string error)
        {
            ShippersBsn bsn = new ShippersBsn(wpfConfig);
            ShippersInfo dbItem = new ShippersInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForShippers), modelNotifiedForShippers, typeof(ShippersInfo), dbItem);
            bsn.InsertOne(dbItem, out error);
            modelNotifiedForShippers.NewItem = false;
            Cloner.CopyAllTo(typeof(ShippersInfo), dbItem, typeof(ModelNotifiedForShippers), modelNotifiedForShippers);
        }
        
        public void DeleteData(ModelNotifiedForShippers modelNotifiedForShippers, out string error)
        {
            ShippersBsn bsn = new ShippersBsn(wpfConfig);
            ShippersInfo dbItem = new ShippersInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForShippers), modelNotifiedForShippers, typeof(ShippersInfo), dbItem);
            bsn.DeleteByID(dbItem, out error);
        }
    }
}

