//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFForms.Region
{
    public interface IWPFRegionDataConnection
    {
        public RegionDataContext GetDataContext(int RegionID,out string error);
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public RegionDataContext GetEmptyDataContext(out string error);
        ModelNotifiedForRegion GetRegionByID(int RegionID, out string error);
        public void SaveData(ModelNotifiedForRegion modelNotifiedForRegion, out string error);
        public void DeleteData(ModelNotifiedForRegion modelNotifiedForRegion, out string error);
        public void AddData(ModelNotifiedForRegion modelNotifiedForRegion, out string error);

    }
}

