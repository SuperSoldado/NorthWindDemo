using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.Territories
{
    public class LabelsAndMessagesTerritories
    {    
        public LabelsFromDBTerritories LabelsFromDBTerritories { get; set; }
        public LanguageElementsControlsTerritories LanguageElementsControlsTerritories { get; set; }
        public LanguageElementsMessagesTerritories LanguageElementsMessagesTerritories { get; set; }
        public LabelsAndMessagesTerritories()
        {
            this.LabelsFromDBTerritories = new LabelsFromDBTerritories();
            this.LanguageElementsControlsTerritories = new LanguageElementsControlsTerritories();
            this.LanguageElementsMessagesTerritories = new LanguageElementsMessagesTerritories();
        }        
    }

    public class LabelsFromDBTerritories
    { 
        public LabelsFromDBTerritories()
        {
            this.LabelTerritoryID = "TerritoryID";
            this.LabelTerritoryDescription = "TerritoryDescription";
            this.LabelRegion_RegionDescription = "Region_RegionDescription";
        }
        public string LabelTerritoryID { get; set; }
        public string LabelTerritoryDescription { get; set; }
        public string LabelRegion_RegionDescription { get; set; }
    }

    public class LanguageElementsControlsTerritories
    {
        public LanguageElementsControlsTerritories()
        {
            this.LabelBtnDelete = "btnDelete";
            this.LabelBtnNew = "btnNew";
            this.LabelBtnUpdate = "btnUpdate";            
        }
        public string LabelBtnDelete { get; set; }
        public string LabelBtnNew { get; set; }
        public string LabelBtnUpdate { get; set; }
    }

    public class LanguageElementsMessagesTerritories
    {
        public LanguageElementsMessagesTerritories()
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
