using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.Categories
{
    public class LabelsAndMessagesCategories
    {    
        public LabelsFromDBCategories LabelsFromDBCategories { get; set; }
        public LanguageElementsControlsCategories LanguageElementsControlsCategories { get; set; }
        public LanguageElementsMessagesCategories LanguageElementsMessagesCategories { get; set; }
        public LabelsAndMessagesCategories()
        {
            this.LabelsFromDBCategories = new LabelsFromDBCategories();
            this.LanguageElementsControlsCategories = new LanguageElementsControlsCategories();
            this.LanguageElementsMessagesCategories = new LanguageElementsMessagesCategories();
        }        
    }

    public class LabelsFromDBCategories
    { 
        public LabelsFromDBCategories()
        {
            this.LabelCategoryID = "CategoryID";
            this.LabelCategoryName = "CategoryName";
            this.LabelDescription = "Description";
            this.LabelPicture = "Picture";
        }
        public string LabelCategoryID { get; set; }
        public string LabelCategoryName { get; set; }
        public string LabelDescription { get; set; }
        public string LabelPicture { get; set; }
    }

    public class LanguageElementsControlsCategories
    {
        public LanguageElementsControlsCategories()
        {
            this.LabelBtnDelete = "btnDelete";
            this.LabelBtnNew = "btnNew";
            this.LabelBtnUpdate = "btnUpdate";            
        }
        public string LabelBtnDelete { get; set; }
        public string LabelBtnNew { get; set; }
        public string LabelBtnUpdate { get; set; }
    }

    public class LanguageElementsMessagesCategories
    {
        public LanguageElementsMessagesCategories()
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
