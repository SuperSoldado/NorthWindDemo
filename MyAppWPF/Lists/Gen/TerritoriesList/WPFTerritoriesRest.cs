//Track[0007] WPF_Shared_REST.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using RESTLib.Core;
using MyApp.TransferObjects.REST;

namespace MyApp.WPFList.Territories
{
    public partial class WPFTerritoriesRest : IWPFTerritoriesDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFTerritoriesRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }


        public TerritoriesDataContext GetDataContext(out string error)
        {
            TerritoriesDataContext dataContext = new TerritoriesDataContext();
            error = null;
            dataContext.modelNotifiedForTerritoriesMain = GetAllTerritories(out error);
            dataContext.modelNotifiedForRegion = GetAll_Region(out error);
            dataContext.PopulateComboBoxesItemSource();


            return dataContext;
        }

        public void SaveData(ModelNotifiedForTerritories modelNotifiedForTerritories, out string error)
        {
            TerritoriesGenericREST TerritoriesGenericREST = new TerritoriesGenericREST(wpfConfig);
            UpdateTerritoriesView updateTerritoriesView = new UpdateTerritoriesView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTerritories), modelNotifiedForTerritories, typeof(UpdateTerritoriesView), updateTerritoriesView);
            TerritoriesGenericREST.Update(updateTerritoriesView, out error);

        }
        
        public void AddData(ModelNotifiedForTerritories modelNotifiedForTerritories, out string error)
        {
            TerritoriesGenericREST TerritoriesGenericREST = new TerritoriesGenericREST(wpfConfig);
            CreateTerritoriesView createTerritoriesView = new CreateTerritoriesView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTerritories), modelNotifiedForTerritories, typeof(CreateTerritoriesView), createTerritoriesView);
            TerritoriesGenericREST.Insert(createTerritoriesView, out error);
        }

        public void DeleteData(ModelNotifiedForTerritories modelNotifiedForTerritories, out string error)
        { 
            TerritoriesGenericREST TerritoriesGenericREST = new TerritoriesGenericREST(wpfConfig);
            DeleteTerritoriesView deleteTerritoriesView = new DeleteTerritoriesView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTerritories), modelNotifiedForTerritories, typeof(DeleteTerritoriesView), deleteTerritoriesView);
            TerritoriesGenericREST.Delete(deleteTerritoriesView, out error);
        }

        public List<ModelNotifiedForTerritories> GetAllTerritories(out string error)
        {
            TerritoriesGenericREST TerritoriesGenericREST = new TerritoriesGenericREST(wpfConfig);
            List<ModelNotifiedForTerritories> modelNotifiedForTerritories = TerritoriesGenericREST.GetAll<ModelNotifiedForTerritories>(100, 0, out error);

            if (!string.IsNullOrEmpty(error))
            {
                return null;
            }
    
            //Initializing row status
            foreach (var item in modelNotifiedForTerritories)
            {
                item.ItemChanged = false;
                item.NewItem = false;
            }
    
            return modelNotifiedForTerritories;
        }
        
        /// <summary>
        /// Get all data to fill combo box
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<ModelNotifiedForRegion> GetAll_Region(out string error)
        {
            RegionGenericREST RegionGenericREST = new RegionGenericREST(wpfConfig);
            List<ModelNotifiedForRegion> modelNotifiedForRegion = RegionGenericREST.GetAll<ModelNotifiedForRegion>(100, 0, out error);
            return modelNotifiedForRegion;
        }

    }
}

