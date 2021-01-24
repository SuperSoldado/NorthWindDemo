using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.TagEmployee
{
    public class LabelsAndMessagesTagEmployee
    {    
        public LabelsFromDBTagEmployee LabelsFromDBTagEmployee { get; set; }
        public LanguageElementsControlsTagEmployee LanguageElementsControlsTagEmployee { get; set; }
        public LanguageElementsMessagesTagEmployee LanguageElementsMessagesTagEmployee { get; set; }
        public LabelsAndMessagesTagEmployee()
        {
            this.LabelsFromDBTagEmployee = new LabelsFromDBTagEmployee();
            this.LanguageElementsControlsTagEmployee = new LanguageElementsControlsTagEmployee();
            this.LanguageElementsMessagesTagEmployee = new LanguageElementsMessagesTagEmployee();
        }        
    }

    public class LabelsFromDBTagEmployee
    { 
        public LabelsFromDBTagEmployee()
        {
            this.LabelTagEmployeeID = "TagEmployeeID";
            this.LabelEmployees_LastName = "Employees_LastName";
            this.LabelTag_TextDesc = "Tag_TextDesc";
            this.LabelTagEmployeeTextDesc = "TagEmployeeTextDesc";
        }
        public string LabelTagEmployeeID { get; set; }
        public string LabelEmployees_LastName { get; set; }
        public string LabelTag_TextDesc { get; set; }
        public string LabelTagEmployeeTextDesc { get; set; }
    }

    public class LanguageElementsControlsTagEmployee
    {
        public LanguageElementsControlsTagEmployee()
        {
            this.LabelBtnDelete = "btnDelete";
            this.LabelBtnNew = "btnNew";
            this.LabelBtnUpdate = "btnUpdate";            
        }
        public string LabelBtnDelete { get; set; }
        public string LabelBtnNew { get; set; }
        public string LabelBtnUpdate { get; set; }
    }

    public class LanguageElementsMessagesTagEmployee
    {
        public LanguageElementsMessagesTagEmployee()
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
