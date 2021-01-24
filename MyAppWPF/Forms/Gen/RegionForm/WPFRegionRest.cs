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

namespace MyApp.WPFForms.Region
{
    public partial class WPFRegionRest : IWPFRegionDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFRegionRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }

        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public RegionDataContext GetEmptyDataContext(out string error)
        {
            RegionDataContext dataContext = new RegionDataContext();
            error = null;           
            dataContext.modelNotifiedForRegionMain = new ModelNotifiedForRegion();;
            return dataContext;
        }

        public RegionDataContext GetDataContext(int RegionID,out string error)
        {
            RegionDataContext dataContext = new RegionDataContext();
            error = null;
            dataContext.modelNotifiedForRegionMain = GetRegionByID(RegionID, out error);


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

        public ModelNotifiedForRegion GetRegionByID(int RegionID, out string error)
        {
            error = null;
            RegionGenericREST RegionGenericREST = new RegionGenericREST(wpfConfig);
            GetRegionView getRegionView = RegionGenericREST.GetByPK<GetRegionView>(RegionID, out error)[0];
            ModelNotifiedForRegion modelNotifiedForRegion = new ModelNotifiedForRegion();
            Cloner.CopyAllTo(typeof(GetRegionView), getRegionView, typeof(ModelNotifiedForRegion), modelNotifiedForRegion);
            return modelNotifiedForRegion;
        }
        

    }
}

