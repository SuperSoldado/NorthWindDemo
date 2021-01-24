//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFList.Territories
{
    public interface IWPFTerritoriesDataConnection
    {
        public TerritoriesDataContext GetDataContext(out string error);
        List<ModelNotifiedForTerritories> GetAllTerritories(out string error);
        public void SaveData(ModelNotifiedForTerritories modelNotifiedForTerritories, out string error);
        public void DeleteData(ModelNotifiedForTerritories modelNotifiedForTerritories, out string error);
        public void AddData(ModelNotifiedForTerritories modelNotifiedForTerritories, out string error);

        //test
        public List<ModelNotifiedForRegion> GetAll_Region(out string error);
    }
}

