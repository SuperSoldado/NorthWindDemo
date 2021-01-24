using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.CustomerCustomerDemo
{
    public class LabelsAndMessagesCustomerCustomerDemo
    {    
        public LabelsFromDBCustomerCustomerDemo LabelsFromDBCustomerCustomerDemo { get; set; }
        public LanguageElementsControlsCustomerCustomerDemo LanguageElementsControlsCustomerCustomerDemo { get; set; }
        public LanguageElementsMessagesCustomerCustomerDemo LanguageElementsMessagesCustomerCustomerDemo { get; set; }
        public LabelsAndMessagesCustomerCustomerDemo()
        {
            this.LabelsFromDBCustomerCustomerDemo = new LabelsFromDBCustomerCustomerDemo();
            this.LanguageElementsControlsCustomerCustomerDemo = new LanguageElementsControlsCustomerCustomerDemo();
            this.LanguageElementsMessagesCustomerCustomerDemo = new LanguageElementsMessagesCustomerCustomerDemo();
        }        
    }

    public class LabelsFromDBCustomerCustomerDemo
    { 
        public LabelsFromDBCustomerCustomerDemo()
        {
            this.LabelCustomers_CompanyName = "Customers_CompanyName";
            this.LabelCustomerDemographics_CustomerTypeID = "CustomerDemographics_CustomerTypeID";
        }
        public string LabelCustomers_CompanyName { get; set; }
        public string LabelCustomerDemographics_CustomerTypeID { get; set; }
    }

    public class LanguageElementsControlsCustomerCustomerDemo
    {
        public LanguageElementsControlsCustomerCustomerDemo()
        {
            this.LabelBtnDelete = "btnDelete";
            this.LabelBtnNew = "btnNew";
            this.LabelBtnUpdate = "btnUpdate";            
        }
        public string LabelBtnDelete { get; set; }
        public string LabelBtnNew { get; set; }
        public string LabelBtnUpdate { get; set; }
    }

    public class LanguageElementsMessagesCustomerCustomerDemo
    {
        public LanguageElementsMessagesCustomerCustomerDemo()
        {
            this.MessageBoxSaveOK = "Save";
            this.MessageBoxSaveError = "Error:";
            this.MessageBoxDeleteConfirm = "Are you sure?";
            this.MessageBoxDeleteOK = "Deleted";
        }

        public string MessageBoxSaveOK { get; set; }
        public string MessageBoxSaveError { get; set; }
        public string MessageBoxDeleteConfirm { get; set; }
        public string MessageBoxDeleteOK { get; set; }
    }
}
