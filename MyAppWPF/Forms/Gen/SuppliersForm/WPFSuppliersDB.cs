//Track[0003] Template:WPF_Shared_DB.html
using System;
using System.Collections.Generic;
using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using MyApp.TransferObjects.REST;
namespace MyApp.WPFForms.Suppliers
{
    public class WPFSuppliersDB : IWPFSuppliersDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFSuppliersDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public SuppliersDataContext GetDataContext(int SupplierID,out string error)
        {
            SuppliersDataContext dataContext = new SuppliersDataContext();            
            error=null;
            dataContext.modelNotifiedForSuppliersMain = GetSuppliersByID(SupplierID, out error);
    
            

            return dataContext;
        }

        
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public SuppliersDataContext GetEmptyDataContext(out string error)
        {
            SuppliersDataContext dataContext = new SuppliersDataContext();
            error=null;
            dataContext.modelNotifiedForSuppliersMain = new ModelNotifiedForSuppliers();
    

            return dataContext;
        }



        public ModelNotifiedForSuppliers GetSuppliersByID(int SupplierID, out string error)
        {
            error = null;
            SuppliersBsn bsn = new SuppliersBsn(wpfConfig);
            SuppliersInfo dbItem = bsn.GetValueByID(SupplierID);
            ModelNotifiedForSuppliers item = new ModelNotifiedForSuppliers();
            Cloner.CopyAllTo(typeof(SuppliersInfo), dbItem, typeof(ModelNotifiedForSuppliers), item);
            return item;
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

