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
namespace MyApp.WPFList.Suppliers
{
    public class WPFSuppliersDB : IWPFSuppliersDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFSuppliersDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public SuppliersDataContext GetDataContext(out string error)
        {
            SuppliersDataContext dataContext = new SuppliersDataContext();            
            error=null;
            dataContext.modelNotifiedForSuppliersMain = GetAllSuppliers(out error);
    
            

            return dataContext;
        }

        

        //private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
            //Notify("IncludeFolders");
        //}


        
        public List<ModelNotifiedForSuppliers> GetAllSuppliers(out string error)
        {
            error = null;
            try
            {
                SuppliersBsn bsn = new SuppliersBsn(wpfConfig);
                List<SuppliersInfo> dbItems = bsn.GetAll();
                List<ModelNotifiedForSuppliers> notifiedItems = new List<ModelNotifiedForSuppliers>();

                foreach (SuppliersInfo dbItem in dbItems)
                {
                    ModelNotifiedForSuppliers itemToAdd = new ModelNotifiedForSuppliers();
                    Cloner.CopyAllTo(typeof(SuppliersInfo), dbItem, typeof(ModelNotifiedForSuppliers), itemToAdd);
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
        
        
        public void SaveData(ModelNotifiedForSuppliers modelNotifiedForSuppliers, out string error)
        {
            SuppliersBsn bsn = new SuppliersBsn(wpfConfig);
            SuppliersInfo dbItem = new SuppliersInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForSuppliers), modelNotifiedForSuppliers, typeof(SuppliersInfo), dbItem);
            
            bsn.UpdateOne(dbItem, out error);
        }

        public void AddData(ModelNotifiedForSuppliers modelNotifiedForSuppliers, out string error)
        {
            SuppliersBsn bsn = new SuppliersBsn(wpfConfig);
            SuppliersInfo dbItem = new SuppliersInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForSuppliers), modelNotifiedForSuppliers, typeof(SuppliersInfo), dbItem);
            bsn.InsertOne(dbItem, out error);
            modelNotifiedForSuppliers.NewItem = false;
            Cloner.CopyAllTo(typeof(SuppliersInfo), dbItem, typeof(ModelNotifiedForSuppliers), modelNotifiedForSuppliers);
        }
        
        public void DeleteData(ModelNotifiedForSuppliers modelNotifiedForSuppliers, out string error)
        {
            SuppliersBsn bsn = new SuppliersBsn(wpfConfig);
            SuppliersInfo dbItem = new SuppliersInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForSuppliers), modelNotifiedForSuppliers, typeof(SuppliersInfo), dbItem);
            bsn.DeleteByID(dbItem, out error);
        }
    }
}

