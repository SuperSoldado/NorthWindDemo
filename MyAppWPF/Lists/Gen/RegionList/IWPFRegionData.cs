//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFList.Region
{
    public interface IWPFRegionDataConnection
    {
        public RegionDataContext GetDataContext(out string error);
        List<ModelNotifiedForRegion> GetAllRegion(out string error);
        public void SaveData(ModelNotifiedForRegion modelNotifiedForRegion, out string error);
        public void DeleteData(ModelNotifiedForRegion modelNotifiedForRegion, out string error);
        public void AddData(ModelNotifiedForRegion modelNotifiedForRegion, out string error);

    }
}

