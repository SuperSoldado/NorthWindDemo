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

namespace MyApp.WPFList.Customers
{
    public partial class WPFCustomersRest : IWPFCustomersDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFCustomersRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }


        public CustomersDataContext GetDataContext(out string error)
        {
            CustomersDataContext dataContext = new CustomersDataContext();
            error = null;
            dataContext.modelNotifiedForCustomersMain = GetAllCustomers(out error);


            return dataContext;
        }

        public void SaveData(ModelNotifiedForCustomers modelNotifiedForCustomers, out string error)
        {
            CustomersGenericREST CustomersGenericREST = new CustomersGenericREST(wpfConfig);
            UpdateCustomersView updateCustomersView = new UpdateCustomersView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomers), modelNotifiedForCustomers, typeof(UpdateCustomersView), updateCustomersView);
            CustomersGenericREST.Update(updateCustomersView, out error);

        }
        
        public void AddData(ModelNotifiedForCustomers modelNotifiedForCustomers, out string error)
        {
            CustomersGenericREST CustomersGenericREST = new CustomersGenericREST(wpfConfig);
            CreateCustomersView createCustomersView = new CreateCustomersView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomers), modelNotifiedForCustomers, typeof(CreateCustomersView), createCustomersView);
            CustomersGenericREST.Insert(createCustomersView, out error);
        }

        public void DeleteData(ModelNotifiedForCustomers modelNotifiedForCustomers, out string error)
        { 
            CustomersGenericREST CustomersGenericREST = new CustomersGenericREST(wpfConfig);
            DeleteCustomersView deleteCustomersView = new DeleteCustomersView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomers), modelNotifiedForCustomers, typeof(DeleteCustomersView), deleteCustomersView);
            CustomersGenericREST.Delete(deleteCustomersView, out error);
        }

        public List<ModelNotifiedForCustomers> GetAllCustomers(out string error)
        {
            CustomersGenericREST CustomersGenericREST = new CustomersGenericREST(wpfConfig);
            List<ModelNotifiedForCustomers> modelNotifiedForCustomers = CustomersGenericREST.GetAll<ModelNotifiedForCustomers>(100, 0, out error);

            if (!string.IsNullOrEmpty(error))
            {
                return null;
            }
    
            //Initializing row status
            foreach (var item in modelNotifiedForCustomers)
            {
                item.ItemChanged = false;
                item.NewItem = false;
            }
    
            return modelNotifiedForCustomers;
        }
        

    }
}

