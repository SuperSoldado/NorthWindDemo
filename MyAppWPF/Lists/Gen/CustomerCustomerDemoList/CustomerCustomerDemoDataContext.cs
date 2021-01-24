//Track[0002] Template:WPF_Shared_DataContext.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFList.CustomerCustomerDemo
{
    public class CustomerCustomerDemoDataContext : INotifyPropertyChanged
    {
        public CustomerCustomerDemoDataContext()
        {
            this.WPFMessageAndLabelForList = new WPFMessageAndLabelForList();
        }

        public WPFMessageAndLabelForList WPFMessageAndLabelForList { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;
        //Track [0001]        
        /// <summary>
        /// Populates combo box item source
        /// </summary>
        public void PopulateComboBoxesItemSource()
        {
            modelNotifiedForCustomers_ComboItemSource = new ObservableCollection<ModelNotifiedForCustomers>(modelNotifiedForCustomers);
            modelNotifiedForCustomerDemographics_ComboItemSource = new ObservableCollection<ModelNotifiedForCustomerDemographics>(modelNotifiedForCustomerDemographics);
        }
        
        /// <summary>
        /// All itens. Used later to populate combo box
        /// </summary>
        public List<ModelNotifiedForCustomers> modelNotifiedForCustomers {get; set;}
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        private ObservableCollection<ModelNotifiedForCustomers> _modelNotifiedForCustomers_ComboItemSource { get; set; }
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        public ObservableCollection<ModelNotifiedForCustomers> modelNotifiedForCustomers_ComboItemSource
        {
            get
            {
                return _modelNotifiedForCustomers_ComboItemSource;
            }
            set
            {
                if (value != _modelNotifiedForCustomers_ComboItemSource)
                {
                    _modelNotifiedForCustomers_ComboItemSource = value;
                    RaiseProperChanged("modelNotifiedForCustomers_ComboItemSource");
                }
            }
        }
        /// <summary>
        /// All itens. Used later to populate combo box
        /// </summary>
        public List<ModelNotifiedForCustomerDemographics> modelNotifiedForCustomerDemographics {get; set;}
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        private ObservableCollection<ModelNotifiedForCustomerDemographics> _modelNotifiedForCustomerDemographics_ComboItemSource { get; set; }
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        public ObservableCollection<ModelNotifiedForCustomerDemographics> modelNotifiedForCustomerDemographics_ComboItemSource
        {
            get
            {
                return _modelNotifiedForCustomerDemographics_ComboItemSource;
            }
            set
            {
                if (value != _modelNotifiedForCustomerDemographics_ComboItemSource)
                {
                    _modelNotifiedForCustomerDemographics_ComboItemSource = value;
                    RaiseProperChanged("modelNotifiedForCustomerDemographics_ComboItemSource");
                }
            }
        }
        
        //old Remove later. DataContext.html holds the code. This is the original.// public ObservableCollection<ModelNotifiedForCustomerCustomerDemo> modelNotifiedForCustomerCustomerDemo { get; set; }

        /// <summary>
        /// Defines the main class holding Grid's data. This data is filtered/copied to "GridData" and binded to grid.
        /// </summary>
        public List<ModelNotifiedForCustomerCustomerDemo> modelNotifiedForCustomerCustomerDemoMain { get; set; }

        /// <summary>
        /// Holds the data binded to Grid. 
        /// </summary>
        public ObservableCollection<ModelNotifiedForCustomerCustomerDemo> GridData { get; set; }

        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}

