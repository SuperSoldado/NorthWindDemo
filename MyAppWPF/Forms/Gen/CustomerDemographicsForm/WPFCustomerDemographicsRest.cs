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

namespace MyApp.WPFForms.CustomerDemographics
{
    public partial class WPFCustomerDemographicsRest : IWPFCustomerDemographicsDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFCustomerDemographicsRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }

        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public CustomerDemographicsDataContext GetEmptyDataContext(out string error)
        {
            CustomerDemographicsDataContext dataContext = new CustomerDemographicsDataContext();
            error = null;           
            dataContext.modelNotifiedForCustomerDemographicsMain = new ModelNotifiedForCustomerDemographics();;
            return dataContext;
        }

        public CustomerDemographicsDataContext GetDataContext(string CustomerTypeID,out string error)
        {
            CustomerDemographicsDataContext dataContext = new CustomerDemographicsDataContext();
            error = null;
            dataContext.modelNotifiedForCustomerDemographicsMain = GetCustomerDemographicsByID(CustomerTypeID, out error);


            return dataContext;
        }

        public void SaveData(ModelNotifiedForCustomerDemographics modelNotifiedForCustomerDemographics, out string error)
        {
            CustomerDemographicsGenericREST CustomerDemographicsGenericREST = new CustomerDemographicsGenericREST(wpfConfig);
            UpdateCustomerDemographicsView updateCustomerDemographicsView = new UpdateCustomerDemographicsView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomerDemographics), modelNotifiedForCustomerDemographics, typeof(UpdateCustomerDemographicsView), updateCustomerDemographicsView);
            CustomerDemographicsGenericREST.Update(updateCustomerDemographicsView, out error);

        }
        
        public void AddData(ModelNotifiedForCustomerDemographics modelNotifiedForCustomerDemographics, out string error)
        {
            CustomerDemographicsGenericREST CustomerDemographicsGenericREST = new CustomerDemographicsGenericREST(wpfConfig);
            CreateCustomerDemographicsView createCustomerDemographicsView = new CreateCustomerDemographicsView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomerDemographics), modelNotifiedForCustomerDemographics, typeof(CreateCustomerDemographicsView), createCustomerDemographicsView);
            CustomerDemographicsGenericREST.Insert(createCustomerDemographicsView, out error);
        }

        public void DeleteData(ModelNotifiedForCustomerDemographics modelNotifiedForCustomerDemographics, out string error)
        { 
            CustomerDemographicsGenericREST CustomerDemographicsGenericREST = new CustomerDemographicsGenericREST(wpfConfig);
            DeleteCustomerDemographicsView deleteCustomerDemographicsView = new DeleteCustomerDemographicsView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCustomerDemographics), modelNotifiedForCustomerDemographics, typeof(DeleteCustomerDemographicsView), deleteCustomerDemographicsView);
            CustomerDemographicsGenericREST.Delete(deleteCustomerDemographicsView, out error);
        }

        public ModelNotifiedForCustomerDemographics GetCustomerDemographicsByID(string CustomerTypeID, out string error)
        {
            error = null;
            CustomerDemographicsGenericREST CustomerDemographicsGenericREST = new CustomerDemographicsGenericREST(wpfConfig);
            GetCustomerDemographicsView getCustomerDemographicsView = CustomerDemographicsGenericREST.GetByPK<GetCustomerDemographicsView>(CustomerTypeID, out error)[0];
            ModelNotifiedForCustomerDemographics modelNotifiedForCustomerDemographics = new ModelNotifiedForCustomerDemographics();
            Cloner.CopyAllTo(typeof(GetCustomerDemographicsView), getCustomerDemographicsView, typeof(ModelNotifiedForCustomerDemographics), modelNotifiedForCustomerDemographics);
            return modelNotifiedForCustomerDemographics;
        }
        

    }
}

