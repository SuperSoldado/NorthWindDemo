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

namespace MyApp.WPFList.CustomerDemographics
{
    public partial class WPFCustomerDemographicsRest : IWPFCustomerDemographicsDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFCustomerDemographicsRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }


        public CustomerDemographicsDataContext GetDataContext(out string error)
        {
            CustomerDemographicsDataContext dataContext = new CustomerDemographicsDataContext();
            error = null;
            dataContext.modelNotifiedForCustomerDemographicsMain = GetAllCustomerDemographics(out error);


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

        public List<ModelNotifiedForCustomerDemographics> GetAllCustomerDemographics(out string error)
        {
            CustomerDemographicsGenericREST CustomerDemographicsGenericREST = new CustomerDemographicsGenericREST(wpfConfig);
            List<ModelNotifiedForCustomerDemographics> modelNotifiedForCustomerDemographics = CustomerDemographicsGenericREST.GetAll<ModelNotifiedForCustomerDemographics>(100, 0, out error);

            if (!string.IsNullOrEmpty(error))
            {
                return null;
            }
    
            //Initializing row status
            foreach (var item in modelNotifiedForCustomerDemographics)
            {
                item.ItemChanged = false;
                item.NewItem = false;
            }
    
            return modelNotifiedForCustomerDemographics;
        }
        

    }
}

