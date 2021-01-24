//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFForms.Territories
{
    public interface IWPFTerritoriesDataConnection
    {
        public TerritoriesDataContext GetDataContext(string TerritoryID,out string error);
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public TerritoriesDataContext GetEmptyDataContext(out string error);
        ModelNotifiedForTerritories GetTerritoriesByID(string TerritoryID, out string error);
        public void SaveData(ModelNotifiedForTerritories modelNotifiedForTerritories, out string error);
        public void DeleteData(ModelNotifiedForTerritories modelNotifiedForTerritories, out string error);
        public void AddData(ModelNotifiedForTerritories modelNotifiedForTerritories, out string error);

        //test
        public List<ModelNotifiedForRegion> GetAll_Region(out string error);
    }
}

