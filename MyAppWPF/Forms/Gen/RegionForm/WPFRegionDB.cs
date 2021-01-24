//Track[0003] Template:WPF_Shared_DB.html
using System;
using System.Collections.Generic;
using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using MyApp.TransferObjects.REST;
namespace MyApp.WPFForms.Region
{
    public class WPFRegionDB : IWPFRegionDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFRegionDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public RegionDataContext GetDataContext(int RegionID,out string error)
        {
            RegionDataContext dataContext = new RegionDataContext();            
            error=null;
            dataContext.modelNotifiedForRegionMain = GetRegionByID(RegionID, out error);
    
            

            return dataContext;
        }

        
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public RegionDataContext GetEmptyDataContext(out string error)
        {
            RegionDataContext dataContext = new RegionDataContext();
            error=null;
            dataContext.modelNotifiedForRegionMain = new ModelNotifiedForRegion();
    

            return dataContext;
        }



        public ModelNotifiedForRegion GetRegionByID(int RegionID, out string error)
        {
            error = null;
            RegionBsn bsn = new RegionBsn(wpfConfig);
            RegionInfo dbItem = bsn.GetValueByID(RegionID);
            ModelNotifiedForRegion item = new ModelNotifiedForRegion();
            Cloner.CopyAllTo(typeof(RegionInfo), dbItem, typeof(ModelNotifiedForRegion), item);
            return item;
        }
        
        
        
        public void SaveData(ModelNotifiedForRegion modelNotifiedForRegion, out string error)
        {
            RegionBsn bsn = new RegionBsn(wpfConfig);
            RegionInfo dbItem = new RegionInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForRegion), modelNotifiedForRegion, typeof(RegionInfo), dbItem);
            
            bsn.UpdateOne(dbItem, out error);
        }

        public void AddData(ModelNotifiedForRegion modelNotifiedForRegion, out string error)
        {
            RegionBsn bsn = new RegionBsn(wpfConfig);
            RegionInfo dbItem = new RegionInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForRegion), modelNotifiedForRegion, typeof(RegionInfo), dbItem);
            bsn.InsertOne(dbItem, out error);
            modelNotifiedForRegion.NewItem = false;
            Cloner.CopyAllTo(typeof(RegionInfo), dbItem, typeof(ModelNotifiedForRegion), modelNotifiedForRegion);
        }
        
        public void DeleteData(ModelNotifiedForRegion modelNotifiedForRegion, out string error)
        {
            RegionBsn bsn = new RegionBsn(wpfConfig);
            RegionInfo dbItem = new RegionInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForRegion), modelNotifiedForRegion, typeof(RegionInfo), dbItem);
            bsn.DeleteByID(dbItem, out error);
        }
    }
}

