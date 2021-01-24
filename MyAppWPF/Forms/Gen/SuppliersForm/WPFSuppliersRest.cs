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

namespace MyApp.WPFForms.Suppliers
{
    public partial class WPFSuppliersRest : IWPFSuppliersDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFSuppliersRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }

        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public SuppliersDataContext GetEmptyDataContext(out string error)
        {
            SuppliersDataContext dataContext = new SuppliersDataContext();
            error = null;           
            dataContext.modelNotifiedForSuppliersMain = new ModelNotifiedForSuppliers();;
            return dataContext;
        }

        public SuppliersDataContext GetDataContext(int SupplierID,out string error)
        {
            SuppliersDataContext dataContext = new SuppliersDataContext();
            error = null;
            dataContext.modelNotifiedForSuppliersMain = GetSuppliersByID(SupplierID, out error);


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

        public ModelNotifiedForSuppliers GetSuppliersByID(int SupplierID, out string error)
        {
            error = null;
            SuppliersGenericREST SuppliersGenericREST = new SuppliersGenericREST(wpfConfig);
            GetSuppliersView getSuppliersView = SuppliersGenericREST.GetByPK<GetSuppliersView>(SupplierID, out error)[0];
            ModelNotifiedForSuppliers modelNotifiedForSuppliers = new ModelNotifiedForSuppliers();
            Cloner.CopyAllTo(typeof(GetSuppliersView), getSuppliersView, typeof(ModelNotifiedForSuppliers), modelNotifiedForSuppliers);
            return modelNotifiedForSuppliers;
        }
        

    }
}

