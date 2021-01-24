//Track[0002] Template:WPF_Shared_DataContext.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.CustomerDemographics
{
    public class CustomerDemographicsDataContext : INotifyPropertyChanged
    {
        public CustomerDemographicsDataContext()
        {
            this.LabelsAndMessagesCustomerDemographics = new LabelsAndMessagesCustomerDemographics();
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        //Track [0001]        
        
        
        //old. Remove later. DataContext.html holds the code This is the original.//public ModelNotifiedForCustomerDemographics modelNotifiedForCustomerDemographicsMain { get; set; }
        /// <summary>
        /// Contains Form's labels and messages
        /// </summary>        
        public LabelsAndMessagesCustomerDemographics LabelsAndMessagesCustomerDemographics { get; set; }

        /// <summary>
        /// Defines the main class holding Form's data
        /// </summary>
        public ModelNotifiedForCustomerDemographics modelNotifiedForCustomerDemographicsMain { get; set; }


        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}

