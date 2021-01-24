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
namespace MyApp.WPFList.Region
{
    public class WPFRegionDB : IWPFRegionDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFRegionDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public RegionDataContext GetDataContext(out string error)
        {
            RegionDataContext dataContext = new RegionDataContext();            
            error=null;
            dataContext.modelNotifiedForRegionMain = GetAllRegion(out error);
    
            

            return dataContext;
        }

        

        //private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
            //Notify("IncludeFolders");
        //}


        
        public List<ModelNotifiedForRegion> GetAllRegion(out string error)
        {
            error = null;
            try
            {
                RegionBsn bsn = new RegionBsn(wpfConfig);
                List<RegionInfo> dbItems = bsn.GetAll();
                List<ModelNotifiedForRegion> notifiedItems = new List<ModelNotifiedForRegion>();

                foreach (RegionInfo dbItem in dbItems)
                {
                    ModelNotifiedForRegion itemToAdd = new ModelNotifiedForRegion();
                    Cloner.CopyAllTo(typeof(RegionInfo), dbItem, typeof(ModelNotifiedForRegion), itemToAdd);
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

