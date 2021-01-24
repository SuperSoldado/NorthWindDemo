//Track[0003] Template:WPF_Shared_DB.html
using System;
using System.Collections.Generic;
using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using MyApp.TransferObjects.REST;
namespace MyApp.WPFForms.Shippers
{
    public class WPFShippersDB : IWPFShippersDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFShippersDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public ShippersDataContext GetDataContext(int ShipperID,out string error)
        {
            ShippersDataContext dataContext = new ShippersDataContext();            
            error=null;
            dataContext.modelNotifiedForShippersMain = GetShippersByID(ShipperID, out error);
    
            

            return dataContext;
        }

        
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public ShippersDataContext GetEmptyDataContext(out string error)
        {
            ShippersDataContext dataContext = new ShippersDataContext();
            error=null;
            dataContext.modelNotifiedForShippersMain = new ModelNotifiedForShippers();
    

            return dataContext;
        }



        public ModelNotifiedForShippers GetShippersByID(int ShipperID, out string error)
        {
            error = null;
            ShippersBsn bsn = new ShippersBsn(wpfConfig);
            ShippersInfo dbItem = bsn.GetValueByID(ShipperID);
            ModelNotifiedForShippers item = new ModelNotifiedForShippers();
            Cloner.CopyAllTo(typeof(ShippersInfo), dbItem, typeof(ModelNotifiedForShippers), item);
            return item;
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

