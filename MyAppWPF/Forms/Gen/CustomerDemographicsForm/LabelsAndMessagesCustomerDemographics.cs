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
    public class LabelsAndMessagesCustomerDemographics
    {    
        public LabelsFromDBCustomerDemographics LabelsFromDBCustomerDemographics { get; set; }
        public LanguageElementsControlsCustomerDemographics LanguageElementsControlsCustomerDemographics { get; set; }
        public LanguageElementsMessagesCustomerDemographics LanguageElementsMessagesCustomerDemographics { get; set; }
        public LabelsAndMessagesCustomerDemographics()
        {
            this.LabelsFromDBCustomerDemographics = new LabelsFromDBCustomerDemographics();
            this.LanguageElementsControlsCustomerDemographics = new LanguageElementsControlsCustomerDemographics();
            this.LanguageElementsMessagesCustomerDemographics = new LanguageElementsMessagesCustomerDemographics();
        }        
    }

    public class LabelsFromDBCustomerDemographics
    { 
        public LabelsFromDBCustomerDemographics()
        {
            this.LabelCustomerTypeID = "CustomerTypeID";
            this.LabelCustomerDesc = "CustomerDesc";
        }
        public string LabelCustomerTypeID { get; set; }
        public string LabelCustomerDesc { get; set; }
    }

    public class LanguageElementsControlsCustomerDemographics
    {
        public LanguageElementsControlsCustomerDemographics()
        {
            this.LabelBtnDelete = "btnDelete";
            this.LabelBtnNew = "btnNew";
            this.LabelBtnUpdate = "btnUpdate";            
        }
        public string LabelBtnDelete { get; set; }
        public string LabelBtnNew { get; set; }
        public string LabelBtnUpdate { get; set; }
    }

    public class LanguageElementsMessagesCustomerDemographics
    {
        public LanguageElementsMessagesCustomerDemographics()
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
