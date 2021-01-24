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

namespace MyApp.WPFList.Suppliers
{
    public partial class WPFSuppliersRest : IWPFSuppliersDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFSuppliersRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }


        public SuppliersDataContext GetDataContext(out string error)
        {
            SuppliersDataContext dataContext = new SuppliersDataContext();
            error = null;
            dataContext.modelNotifiedForSuppliersMain = GetAllSuppliers(out error);


            return dataContext;
        }

        public void SaveData(ModelNotifiedForSuppliers modelNotifiedForSuppliers, out string error)
        {
            SuppliersGenericREST SuppliersGenericREST = new SuppliersGenericREST(wpfConfig);
            UpdateSuppliersView updateSuppliersView = new UpdateSuppliersView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForSuppliers), modelNotifiedForSuppliers, typeof(UpdateSuppliersView), updateSuppliersView);
            SuppliersGenericREST.Update(updateSuppliersView, out error);

        }
        
        public void AddData(ModelNotifiedForSuppliers modelNotifiedForSuppliers, out string error)
        {
            SuppliersGenericREST SuppliersGenericREST = new SuppliersGenericREST(wpfConfig);
            CreateSuppliersView createSuppliersView = new CreateSuppliersView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForSuppliers), modelNotifiedForSuppliers, typeof(CreateSuppliersView), createSuppliersView);
            SuppliersGenericREST.Insert(createSuppliersView, out error);
        }

        public void DeleteData(ModelNotifiedForSuppliers modelNotifiedForSuppliers, out string error)
        { 
            SuppliersGenericREST SuppliersGenericREST = new SuppliersGenericREST(wpfConfig);
            DeleteSuppliersView deleteSuppliersView = new DeleteSuppliersView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForSuppliers), modelNotifiedForSuppliers, typeof(DeleteSuppliersView), deleteSuppliersView);
            SuppliersGenericREST.Delete(deleteSuppliersView, out error);
        }

        public List<ModelNotifiedForSuppliers> GetAllSuppliers(out string error)
        {
            SuppliersGenericREST SuppliersGenericREST = new SuppliersGenericREST(wpfConfig);
            List<ModelNotifiedForSuppliers> modelNotifiedForSuppliers = SuppliersGenericREST.GetAll<ModelNotifiedForSuppliers>(100, 0, out error);

            if (!string.IsNullOrEmpty(error))
            {
                return null;
            }
    
            //Initializing row status
            foreach (var item in modelNotifiedForSuppliers)
            {
                item.ItemChanged = false;
                item.NewItem = false;
            }
    
            return modelNotifiedForSuppliers;
        }
        

    }
}

