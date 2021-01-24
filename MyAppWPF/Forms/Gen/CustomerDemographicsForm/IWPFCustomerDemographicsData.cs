//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFForms.CustomerDemographics
{
    public interface IWPFCustomerDemographicsDataConnection
    {
        public CustomerDemographicsDataContext GetDataContext(string CustomerTypeID,out string error);
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public CustomerDemographicsDataContext GetEmptyDataContext(out string error);
        ModelNotifiedForCustomerDemographics GetCustomerDemographicsByID(string CustomerTypeID, out string error);
        public void SaveData(ModelNotifiedForCustomerDemographics modelNotifiedForCustomerDemographics, out string error);
        public void DeleteData(ModelNotifiedForCustomerDemographics modelNotifiedForCustomerDemographics, out string error);
        public void AddData(ModelNotifiedForCustomerDemographics modelNotifiedForCustomerDemographics, out string error);

    }
}

