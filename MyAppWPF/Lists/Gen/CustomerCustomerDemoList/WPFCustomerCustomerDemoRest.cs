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

namespace MyApp.WPFList.CustomerCustomerDemo
{
    public partial class WPFCustomerCustomerDemoRest : IWPFCustomerCustomerDemoDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFCustomerCustomerDemoRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }


        public CustomerCustomerDemoDataContext GetDataContext(out string error)
        {
            CustomerCustomerDemoDataContext dataContext = new CustomerCustomerDemoDataContext();
            error = null;
            dataContext.modelNotifiedForCustomerCustomerDemoMain = GetAllCustomerCustomerDemo(out error);
            dataContext.modelNotifiedForCustomers = GetAll_Customers(out error);
            dataContext.modelNotifiedForCustomerDemographics = GetAll_CustomerDemographics(out error);
            dataContext.PopulateComboBoxesItemSource();


            return dataContext;
        }

        public void SaveData(ModelNotifiedForCustomerCustomerDemo modelNotifiedForCustomerCustomerDemo, out string error)
        {
            CustomerCustomerDemoGenericREST CustomerCustomerDemoGenericREST = new CustomerCustomerDemoGenericREST(wpfConfig);
            UpdateCustomerCustomerDemoView updateCustomerCustomerDemoView = new UpdateCustomerCustomerDemoView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomerCustomerDemo), modelNotifiedForCustomerCustomerDemo, typeof(UpdateCustomerCustomerDemoView), updateCustomerCustomerDemoView);
            CustomerCustomerDemoGenericREST.Update(updateCustomerCustomerDemoView, out error);

        }
        
        public void AddData(ModelNotifiedForCustomerCustomerDemo modelNotifiedForCustomerCustomerDemo, out string error)
        {
            CustomerCustomerDemoGenericREST CustomerCustomerDemoGenericREST = new CustomerCustomerDemoGenericREST(wpfConfig);
            CreateCustomerCustomerDemoView createCustomerCustomerDemoView = new CreateCustomerCustomerDemoView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomerCustomerDemo), modelNotifiedForCustomerCustomerDemo, typeof(CreateCustomerCustomerDemoView), createCustomerCustomerDemoView);
            CustomerCustomerDemoGenericREST.Insert(createCustomerCustomerDemoView, out error);
        }

        public void DeleteData(ModelNotifiedForCustomerCustomerDemo modelNotifiedForCustomerCustomerDemo, out string error)
        { 
            CustomerCustomerDemoGenericREST CustomerCustomerDemoGenericREST = new CustomerCustomerDemoGenericREST(wpfConfig);
            DeleteCustomerCustomerDemoView deleteCustomerCustomerDemoView = new DeleteCustomerCustomerDemoView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomerCustomerDemo), modelNotifiedForCustomerCustomerDemo, typeof(DeleteCustomerCustomerDemoView), deleteCustomerCustomerDemoView);
            CustomerCustomerDemoGenericREST.Delete(deleteCustomerCustomerDemoView, out error);
        }

        public List<ModelNotifiedForCustomerCustomerDemo> GetAllCustomerCustomerDemo(out string error)
        {
            CustomerCustomerDemoGenericREST CustomerCustomerDemoGenericREST = new CustomerCustomerDemoGenericREST(wpfConfig);
            List<ModelNotifiedForCustomerCustomerDemo> modelNotifiedForCustomerCustomerDemo = CustomerCustomerDemoGenericREST.GetAll<ModelNotifiedForCustomerCustomerDemo>(100, 0, out error);

            if (!string.IsNullOrEmpty(error))
            {
                return null;
            }
    
            //Initializing row status
            foreach (var item in modelNotifiedForCustomerCustomerDemo)
            {
                item.ItemChanged = false;
                item.NewItem = false;
            }
    
            return modelNotifiedForCustomerCustomerDemo;
        }
        
        /// <summary>
        /// Get all data to fill combo box
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<ModelNotifiedForCustomers> GetAll_Customers(out string error)
        {
            CustomersGenericREST CustomersGenericREST = new CustomersGenericREST(wpfConfig);
            List<ModelNotifiedForCustomers> modelNotifiedForCustomers = CustomersGenericREST.GetAll<ModelNotifiedForCustomers>(100, 0, out error);
            return modelNotifiedForCustomers;
        }
        /// <summary>
        /// Get all data to fill combo box
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<ModelNotifiedForCustomerDemographics> GetAll_CustomerDemographics(out string error)
        {
            CustomerDemographicsGenericREST CustomerDemographicsGenericREST = new CustomerDemographicsGenericREST(wpfConfig);
            List<ModelNotifiedForCustomerDemographics> modelNotifiedForCustomerDemographics = CustomerDemographicsGenericREST.GetAll<ModelNotifiedForCustomerDemographics>(100, 0, out error);
            return modelNotifiedForCustomerDemographics;
        }

    }
}

