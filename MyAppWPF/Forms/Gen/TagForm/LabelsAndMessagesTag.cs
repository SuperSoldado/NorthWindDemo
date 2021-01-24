using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.Tag
{
    public class LabelsAndMessagesTag
    {    
        public LabelsFromDBTag LabelsFromDBTag { get; set; }
        public LanguageElementsControlsTag LanguageElementsControlsTag { get; set; }
        public LanguageElementsMessagesTag LanguageElementsMessagesTag { get; set; }
        public LabelsAndMessagesTag()
        {
            this.LabelsFromDBTag = new LabelsFromDBTag();
            this.LanguageElementsControlsTag = new LanguageElementsControlsTag();
            this.LanguageElementsMessagesTag = new LanguageElementsMessagesTag();
        }        
    }

    public class LabelsFromDBTag
    { 
        public LabelsFromDBTag()
        {
            this.LabelTagID = "TagID";
            this.LabelTextDesc = "TextDesc";
            this.LabelTagType = "TagType";
        }
        public string LabelTagID { get; set; }
        public string LabelTextDesc { get; set; }
        public string LabelTagType { get; set; }
    }

    public class LanguageElementsControlsTag
    {
        public LanguageElementsControlsTag()
        {
            this.LabelBtnDelete = "btnDelete";
            this.LabelBtnNew = "btnNew";
            this.LabelBtnUpdate = "btnUpdate";            
        }
        public string LabelBtnDelete { get; set; }
        public string LabelBtnNew { get; set; }
        public string LabelBtnUpdate { get; set; }
    }

    public class LanguageElementsMessagesTag
    {
        public LanguageElementsMessagesTag()
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
