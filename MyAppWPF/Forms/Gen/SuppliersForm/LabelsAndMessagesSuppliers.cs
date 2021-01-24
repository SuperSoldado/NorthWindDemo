using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.Suppliers
{
    public class LabelsAndMessagesSuppliers
    {    
        public LabelsFromDBSuppliers LabelsFromDBSuppliers { get; set; }
        public LanguageElementsControlsSuppliers LanguageElementsControlsSuppliers { get; set; }
        public LanguageElementsMessagesSuppliers LanguageElementsMessagesSuppliers { get; set; }
        public LabelsAndMessagesSuppliers()
        {
            this.LabelsFromDBSuppliers = new LabelsFromDBSuppliers();
            this.LanguageElementsControlsSuppliers = new LanguageElementsControlsSuppliers();
            this.LanguageElementsMessagesSuppliers = new LanguageElementsMessagesSuppliers();
        }        
    }

    public class LabelsFromDBSuppliers
    { 
        public LabelsFromDBSuppliers()
        {
            this.LabelSupplierID = "SupplierID";
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
            this.LabelHomePage = "HomePage";
        }
        public string LabelSupplierID { get; set; }
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
        public string LabelHomePage { get; set; }
    }

    public class LanguageElementsControlsSuppliers
    {
        public LanguageElementsControlsSuppliers()
        {
            this.LabelBtnDelete = "btnDelete";
            this.LabelBtnNew = "btnNew";
            this.LabelBtnUpdate = "btnUpdate";            
        }
        public string LabelBtnDelete { get; set; }
        public string LabelBtnNew { get; set; }
        public string LabelBtnUpdate { get; set; }
    }

    public class LanguageElementsMessagesSuppliers
    {
        public LanguageElementsMessagesSuppliers()
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
