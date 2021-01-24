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

namespace MyApp.WPFForms.Customers
{
    public partial class WPFCustomersRest : IWPFCustomersDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFCustomersRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }

        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public CustomersDataContext GetEmptyDataContext(out string error)
        {
            CustomersDataContext dataContext = new CustomersDataContext();
            error = null;           
            dataContext.modelNotifiedForCustomersMain = new ModelNotifiedForCustomers();;
            return dataContext;
        }

        public CustomersDataContext GetDataContext(string CustomerID,out string error)
        {
            CustomersDataContext dataContext = new CustomersDataContext();
            error = null;
            dataContext.modelNotifiedForCustomersMain = GetCustomersByID(CustomerID, out error);


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

        public ModelNotifiedForCustomers GetCustomersByID(string CustomerID, out string error)
        {
            error = null;
            CustomersGenericREST CustomersGenericREST = new CustomersGenericREST(wpfConfig);
            GetCustomersView getCustomersView = CustomersGenericREST.GetByPK<GetCustomersView>(CustomerID, out error)[0];
            ModelNotifiedForCustomers modelNotifiedForCustomers = new ModelNotifiedForCustomers();
            Cloner.CopyAllTo(typeof(GetCustomersView), getCustomersView, typeof(ModelNotifiedForCustomers), modelNotifiedForCustomers);
            return modelNotifiedForCustomers;
        }
        

    }
}

