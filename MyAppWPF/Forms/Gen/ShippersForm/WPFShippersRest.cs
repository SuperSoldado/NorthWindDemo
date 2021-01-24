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

namespace MyApp.WPFForms.Shippers
{
    public partial class WPFShippersRest : IWPFShippersDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFShippersRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }

        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public ShippersDataContext GetEmptyDataContext(out string error)
        {
            ShippersDataContext dataContext = new ShippersDataContext();
            error = null;           
            dataContext.modelNotifiedForShippersMain = new ModelNotifiedForShippers();;
            return dataContext;
        }

        public ShippersDataContext GetDataContext(int ShipperID,out string error)
        {
            ShippersDataContext dataContext = new ShippersDataContext();
            error = null;
            dataContext.modelNotifiedForShippersMain = GetShippersByID(ShipperID, out error);


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

        public ModelNotifiedForShippers GetShippersByID(int ShipperID, out string error)
        {
            error = null;
            ShippersGenericREST ShippersGenericREST = new ShippersGenericREST(wpfConfig);
            GetShippersView getShippersView = ShippersGenericREST.GetByPK<GetShippersView>(ShipperID, out error)[0];
            ModelNotifiedForShippers modelNotifiedForShippers = new ModelNotifiedForShippers();
            Cloner.CopyAllTo(typeof(GetShippersView), getShippersView, typeof(ModelNotifiedForShippers), modelNotifiedForShippers);
            return modelNotifiedForShippers;
        }
        

    }
}

