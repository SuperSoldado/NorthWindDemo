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
namespace MyApp.WPFList.Territories
{
    public class WPFTerritoriesDB : IWPFTerritoriesDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFTerritoriesDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public TerritoriesDataContext GetDataContext(out string error)
        {
            TerritoriesDataContext dataContext = new TerritoriesDataContext();            
            error=null;
            dataContext.modelNotifiedForTerritoriesMain = GetAllTerritories(out error);
    
            dataContext.modelNotifiedForRegion = GetAll_Region(out error);
            dataContext.PopulateComboBoxesItemSource();
            

            return dataContext;
        }

        

        //private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
            //Notify("IncludeFolders");
        //}


        
        public List<ModelNotifiedForTerritories> GetAllTerritories(out string error)
        {
            error = null;
            try
            {
                TerritoriesBsn bsn = new TerritoriesBsn(wpfConfig);
                List<TerritoriesInfo> dbItems = bsn.GetAll();
                List<ModelNotifiedForTerritories> notifiedItems = new List<ModelNotifiedForTerritories>();

                foreach (TerritoriesInfo dbItem in dbItems)
                {
                    ModelNotifiedForTerritories itemToAdd = new ModelNotifiedForTerritories();
                    Cloner.CopyAllTo(typeof(TerritoriesInfo), dbItem, typeof(ModelNotifiedForTerritories), itemToAdd);
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
        
        /// <summary>
        /// Retrieve all data from Region table. Used to fill combo box.
        /// </summary>
        /// <returns>List of Region</returns>
        public List<ModelNotifiedForRegion> GetAll_Region(out string error)
        {
            error = null;
            RegionBsn bsn = new RegionBsn(wpfConfig);
            List<RegionInfo> dbItems = bsn.GetAll();
            List<ModelNotifiedForRegion> notifiedItems = new List<ModelNotifiedForRegion>();

            foreach (RegionInfo dbItem in dbItems)
            {
                ModelNotifiedForRegion itemToAdd = new ModelNotifiedForRegion();
                Cloner.CopyAllTo(typeof(RegionInfo), dbItem, typeof(ModelNotifiedForRegion), itemToAdd);
                notifiedItems.Add(itemToAdd);
            }
            return notifiedItems;
        }
        
        public void SaveData(ModelNotifiedForTerritories modelNotifiedForTerritories, out string error)
        {
            TerritoriesBsn bsn = new TerritoriesBsn(wpfConfig);
            TerritoriesInfo dbItem = new TerritoriesInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTerritories), modelNotifiedForTerritories, typeof(TerritoriesInfo), dbItem);
            
            bsn.UpdateOne(dbItem, out error);
        }

        public void AddData(ModelNotifiedForTerritories modelNotifiedForTerritories, out string error)
        {
            TerritoriesBsn bsn = new TerritoriesBsn(wpfConfig);
            TerritoriesInfo dbItem = new TerritoriesInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTerritories), modelNotifiedForTerritories, typeof(TerritoriesInfo), dbItem);
            bsn.InsertOne(dbItem, out error);
            modelNotifiedForTerritories.NewItem = false;
            Cloner.CopyAllTo(typeof(TerritoriesInfo), dbItem, typeof(ModelNotifiedForTerritories), modelNotifiedForTerritories);
        }
        
        public void DeleteData(ModelNotifiedForTerritories modelNotifiedForTerritories, out string error)
        {
            TerritoriesBsn bsn = new TerritoriesBsn(wpfConfig);
            TerritoriesInfo dbItem = new TerritoriesInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTerritories), modelNotifiedForTerritories, typeof(TerritoriesInfo), dbItem);
            bsn.DeleteByID(dbItem, out error);
        }
    }
}

