using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.EmployeeTerritories
{
    public class LabelsAndMessagesEmployeeTerritories
    {    
        public LabelsFromDBEmployeeTerritories LabelsFromDBEmployeeTerritories { get; set; }
        public LanguageElementsControlsEmployeeTerritories LanguageElementsControlsEmployeeTerritories { get; set; }
        public LanguageElementsMessagesEmployeeTerritories LanguageElementsMessagesEmployeeTerritories { get; set; }
        public LabelsAndMessagesEmployeeTerritories()
        {
            this.LabelsFromDBEmployeeTerritories = new LabelsFromDBEmployeeTerritories();
            this.LanguageElementsControlsEmployeeTerritories = new LanguageElementsControlsEmployeeTerritories();
            this.LanguageElementsMessagesEmployeeTerritories = new LanguageElementsMessagesEmployeeTerritories();
        }        
    }

    public class LabelsFromDBEmployeeTerritories
    { 
        public LabelsFromDBEmployeeTerritories()
        {
            this.LabelEmployees_LastName = "Employees_LastName";
            this.LabelTerritories_TerritoryDescription = "Territories_TerritoryDescription";
        }
        public string LabelEmployees_LastName { get; set; }
        public string LabelTerritories_TerritoryDescription { get; set; }
    }

    public class LanguageElementsControlsEmployeeTerritories
    {
        public LanguageElementsControlsEmployeeTerritories()
        {
            this.LabelBtnDelete = "btnDelete";
            this.LabelBtnNew = "btnNew";
            this.LabelBtnUpdate = "btnUpdate";            
        }
        public string LabelBtnDelete { get; set; }
        public string LabelBtnNew { get; set; }
        public string LabelBtnUpdate { get; set; }
    }

    public class LanguageElementsMessagesEmployeeTerritories
    {
        public LanguageElementsMessagesEmployeeTerritories()
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
