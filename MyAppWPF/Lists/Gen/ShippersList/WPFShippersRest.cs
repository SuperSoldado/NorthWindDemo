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

namespace MyApp.WPFList.Shippers
{
    public partial class WPFShippersRest : IWPFShippersDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFShippersRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }


        public ShippersDataContext GetDataContext(out string error)
        {
            ShippersDataContext dataContext = new ShippersDataContext();
            error = null;
            dataContext.modelNotifiedForShippersMain = GetAllShippers(out error);


            return dataContext;
        }

        public void SaveData(ModelNotifiedForShippers modelNotifiedForShippers, out string error)
        {
            ShippersGenericREST ShippersGenericREST = new ShippersGenericREST(wpfConfig);
            UpdateShippersView updateShippersView = new UpdateShippersView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForShippers), modelNotifiedForShippers, typeof(UpdateShippersView), updateShippersView);
            ShippersGenericREST.Update(updateShippersView, out error);

        }
        
        public void AddData(ModelNotifiedForShippers modelNotifiedForShippers, out string error)
        {
            ShippersGenericREST ShippersGenericREST = new ShippersGenericREST(wpfConfig);
            CreateShippersView createShippersView = new CreateShippersView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForShippers), modelNotifiedForShippers, typeof(CreateShippersView), createShippersView);
            ShippersGenericREST.Insert(createShippersView, out error);
        }

        public void DeleteData(ModelNotifiedForShippers modelNotifiedForShippers, out string error)
        { 
            ShippersGenericREST ShippersGenericREST = new ShippersGenericREST(wpfConfig);
            DeleteShippersView deleteShippersView = new DeleteShippersView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForShippers), modelNotifiedForShippers, typeof(DeleteShippersView), deleteShippersView);
            ShippersGenericREST.Delete(deleteShippersView, out error);
        }

        public List<ModelNotifiedForShippers> GetAllShippers(out string error)
        {
            ShippersGenericREST ShippersGenericREST = new ShippersGenericREST(wpfConfig);
            List<ModelNotifiedForShippers> modelNotifiedForShippers = ShippersGenericREST.GetAll<ModelNotifiedForShippers>(100, 0, out error);

            if (!string.IsNullOrEmpty(error))
            {
                return null;
            }
    
            //Initializing row status
            foreach (var item in modelNotifiedForShippers)
            {
                item.ItemChanged = false;
                item.NewItem = false;
            }
    
            return modelNotifiedForShippers;
        }
        

    }
}

