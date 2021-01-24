using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.Customers
{
    public class LabelsAndMessagesCustomers
    {    
        public LabelsFromDBCustomers LabelsFromDBCustomers { get; set; }
        public LanguageElementsControlsCustomers LanguageElementsControlsCustomers { get; set; }
        public LanguageElementsMessagesCustomers LanguageElementsMessagesCustomers { get; set; }
        public LabelsAndMessagesCustomers()
        {
            this.LabelsFromDBCustomers = new LabelsFromDBCustomers();
            this.LanguageElementsControlsCustomers = new LanguageElementsControlsCustomers();
            this.LanguageElementsMessagesCustomers = new LanguageElementsMessagesCustomers();
        }        
    }

    public class LabelsFromDBCustomers
    { 
        public LabelsFromDBCustomers()
        {
            this.LabelCustomerID = "CustomerID";
            this.LabelCompanyName = "CompanyName";
            this.LabelContactName = "ContactName";
            this.LabelContactTitle = "ContactTitle";
            this.LabelAddress = "Address";
            this.LabelCity = "City";
            this.LabelRegion = "Region";
            this.LabelPostalCode = "PostalCode";
            this.LabelCountry = "Country";
            this.LabelPhone = "Phone";
            this.LabelFax = "Fax";
        }
        public string LabelCustomerID { get; set; }
        public string LabelCompanyName { get; set; }
        public string LabelContactName { get; set; }
        public string LabelContactTitle { get; set; }
        public string LabelAddress { get; set; }
        public string LabelCity { get; set; }
        public string LabelRegion { get; set; }
        public string LabelPostalCode { get; set; }
        public string LabelCountry { get; set; }
        public string LabelPhone { get; set; }
        public string LabelFax { get; set; }
    }

    public class LanguageElementsControlsCustomers
    {
        public LanguageElementsControlsCustomers()
        {
            this.LabelBtnDelete = "btnDelete";
            this.LabelBtnNew = "btnNew";
            this.LabelBtnUpdate = "btnUpdate";            
        }
        public string LabelBtnDelete { get; set; }
        public string LabelBtnNew { get; set; }
        public string LabelBtnUpdate { get; set; }
    }

    public class LanguageElementsMessagesCustomers
    {
        public LanguageElementsMessagesCustomers()
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
