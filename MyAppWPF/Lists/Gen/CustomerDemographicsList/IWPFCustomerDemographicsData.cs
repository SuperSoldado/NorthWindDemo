//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFList.CustomerDemographics
{
    public interface IWPFCustomerDemographicsDataConnection
    {
        public CustomerDemographicsDataContext GetDataContext(out string error);
        List<ModelNotifiedForCustomerDemographics> GetAllCustomerDemographics(out string error);
        public void SaveData(ModelNotifiedForCustomerDemographics modelNotifiedForCustomerDemographics, out string error);
        public void DeleteData(ModelNotifiedForCustomerDemographics modelNotifiedForCustomerDemographics, out string error);
        public void AddData(ModelNotifiedForCustomerDemographics modelNotifiedForCustomerDemographics, out string error);

    }
}

