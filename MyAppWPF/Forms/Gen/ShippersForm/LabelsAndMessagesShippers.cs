using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.Shippers
{
    public class LabelsAndMessagesShippers
    {    
        public LabelsFromDBShippers LabelsFromDBShippers { get; set; }
        public LanguageElementsControlsShippers LanguageElementsControlsShippers { get; set; }
        public LanguageElementsMessagesShippers LanguageElementsMessagesShippers { get; set; }
        public LabelsAndMessagesShippers()
        {
            this.LabelsFromDBShippers = new LabelsFromDBShippers();
            this.LanguageElementsControlsShippers = new LanguageElementsControlsShippers();
            this.LanguageElementsMessagesShippers = new LanguageElementsMessagesShippers();
        }        
    }

    public class LabelsFromDBShippers
    { 
        public LabelsFromDBShippers()
        {
            this.LabelShipperID = "ShipperID";
            this.LabelCompanyName = "CompanyName";
            this.LabelPhone = "Phone";
        }
        public string LabelShipperID { get; set; }
        public string LabelCompanyName { get; set; }
        public string LabelPhone { get; set; }
    }

    public class LanguageElementsControlsShippers
    {
        public LanguageElementsControlsShippers()
        {
            this.LabelBtnDelete = "btnDelete";
            this.LabelBtnNew = "btnNew";
            this.LabelBtnUpdate = "btnUpdate";            
        }
        public string LabelBtnDelete { get; set; }
        public string LabelBtnNew { get; set; }
        public string LabelBtnUpdate { get; set; }
    }

    public class LanguageElementsMessagesShippers
    {
        public LanguageElementsMessagesShippers()
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
