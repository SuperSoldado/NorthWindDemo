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

namespace MyApp.WPFForms.Territories
{
    public partial class WPFTerritoriesRest : IWPFTerritoriesDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFTerritoriesRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }

        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public TerritoriesDataContext GetEmptyDataContext(out string error)
        {
            TerritoriesDataContext dataContext = new TerritoriesDataContext();
            error = null;           
            dataContext.modelNotifiedForTerritoriesMain = new ModelNotifiedForTerritories();;
            dataContext.modelNotifiedForRegion = GetAll_Region(out error);
            return dataContext;
        }

        public TerritoriesDataContext GetDataContext(string TerritoryID,out string error)
        {
            TerritoriesDataContext dataContext = new TerritoriesDataContext();
            error = null;
            dataContext.modelNotifiedForTerritoriesMain = GetTerritoriesByID(TerritoryID, out error);
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

        public ModelNotifiedForTerritories GetTerritoriesByID(string TerritoryID, out string error)
        {
            error = null;
            TerritoriesGenericREST TerritoriesGenericREST = new TerritoriesGenericREST(wpfConfig);
            GetTerritoriesView getTerritoriesView = TerritoriesGenericREST.GetByPK<GetTerritoriesView>(TerritoryID, out error)[0];
            ModelNotifiedForTerritories modelNotifiedForTerritories = new ModelNotifiedForTerritories();
            Cloner.CopyAllTo(typeof(GetTerritoriesView), getTerritoriesView, typeof(ModelNotifiedForTerritories), modelNotifiedForTerritories);
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

