using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.Region
{
    public class LabelsAndMessagesRegion
    {    
        public LabelsFromDBRegion LabelsFromDBRegion { get; set; }
        public LanguageElementsControlsRegion LanguageElementsControlsRegion { get; set; }
        public LanguageElementsMessagesRegion LanguageElementsMessagesRegion { get; set; }
        public LabelsAndMessagesRegion()
        {
            this.LabelsFromDBRegion = new LabelsFromDBRegion();
            this.LanguageElementsControlsRegion = new LanguageElementsControlsRegion();
            this.LanguageElementsMessagesRegion = new LanguageElementsMessagesRegion();
        }        
    }

    public class LabelsFromDBRegion
    { 
        public LabelsFromDBRegion()
        {
            this.LabelRegionID = "RegionID";
            this.LabelRegionDescription = "RegionDescription";
        }
        public string LabelRegionID { get; set; }
        public string LabelRegionDescription { get; set; }
    }

    public class LanguageElementsControlsRegion
    {
        public LanguageElementsControlsRegion()
        {
            this.LabelBtnDelete = "btnDelete";
            this.LabelBtnNew = "btnNew";
            this.LabelBtnUpdate = "btnUpdate";            
        }
        public string LabelBtnDelete { get; set; }
        public string LabelBtnNew { get; set; }
        public string LabelBtnUpdate { get; set; }
    }

    public class LanguageElementsMessagesRegion
    {
        public LanguageElementsMessagesRegion()
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
