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

namespace MyApp.WPFList.Region
{
    public partial class WPFRegionRest : IWPFRegionDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFRegionRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }


        public RegionDataContext GetDataContext(out string error)
        {
            RegionDataContext dataContext = new RegionDataContext();
            error = null;
            dataContext.modelNotifiedForRegionMain = GetAllRegion(out error);


            return dataContext;
        }

        public void SaveData(ModelNotifiedForRegion modelNotifiedForRegion, out string error)
        {
            RegionGenericREST RegionGenericREST = new RegionGenericREST(wpfConfig);
            UpdateRegionView updateRegionView = new UpdateRegionView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForRegion), modelNotifiedForRegion, typeof(UpdateRegionView), updateRegionView);
            RegionGenericREST.Update(updateRegionView, out error);

        }
        
        public void AddData(ModelNotifiedForRegion modelNotifiedForRegion, out string error)
        {
            RegionGenericREST RegionGenericREST = new RegionGenericREST(wpfConfig);
            CreateRegionView createRegionView = new CreateRegionView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForRegion), modelNotifiedForRegion, typeof(CreateRegionView), createRegionView);
            RegionGenericREST.Insert(createRegionView, out error);
        }

        public void DeleteData(ModelNotifiedForRegion modelNotifiedForRegion, out string error)
        { 
            RegionGenericREST RegionGenericREST = new RegionGenericREST(wpfConfig);
            DeleteRegionView deleteRegionView = new DeleteRegionView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForRegion), modelNotifiedForRegion, typeof(DeleteRegionView), deleteRegionView);
            RegionGenericREST.Delete(deleteRegionView, out error);
        }

        public List<ModelNotifiedForRegion> GetAllRegion(out string error)
        {
            RegionGenericREST RegionGenericREST = new RegionGenericREST(wpfConfig);
            List<ModelNotifiedForRegion> modelNotifiedForRegion = RegionGenericREST.GetAll<ModelNotifiedForRegion>(100, 0, out error);

            if (!string.IsNullOrEmpty(error))
            {
                return null;
            }
    
            //Initializing row status
            foreach (var item in modelNotifiedForRegion)
            {
                item.ItemChanged = false;
                item.NewItem = false;
            }
    
            return modelNotifiedForRegion;
        }
        

    }
}

