using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.Employees
{
    public class LabelsAndMessagesEmployees
    {    
        public LabelsFromDBEmployees LabelsFromDBEmployees { get; set; }
        public LanguageElementsControlsEmployees LanguageElementsControlsEmployees { get; set; }
        public LanguageElementsMessagesEmployees LanguageElementsMessagesEmployees { get; set; }
        public LabelsAndMessagesEmployees()
        {
            this.LabelsFromDBEmployees = new LabelsFromDBEmployees();
            this.LanguageElementsControlsEmployees = new LanguageElementsControlsEmployees();
            this.LanguageElementsMessagesEmployees = new LanguageElementsMessagesEmployees();
        }        
    }

    public class LabelsFromDBEmployees
    { 
        public LabelsFromDBEmployees()
        {
            this.LabelEmployeeID = "EmployeeID";
            this.LabelLastName = "LastName";
            this.LabelFirstName = "FirstName";
            this.LabelTitle = "Title";
            this.LabelTitleOfCourtesy = "TitleOfCourtesy";
            this.LabelBirthDate = "BirthDate";
            this.LabelHireDate = "HireDate";
            this.LabelAddress = "Address";
            this.LabelCity = "City";
            this.LabelRegion = "Region";
            this.LabelPostalCode = "PostalCode";
            this.LabelCountry = "Country";
            this.LabelHomePhone = "HomePhone";
            this.LabelExtension = "Extension";
            this.LabelPhoto = "Photo";
            this.LabelNotes = "Notes";
            this.LabelEmployees_LastName = "Employees_LastName";
            this.LabelPhotoPath = "PhotoPath";
        }
        public string LabelEmployeeID { get; set; }
        public string LabelLastName { get; set; }
        public string LabelFirstName { get; set; }
        public string LabelTitle { get; set; }
        public string LabelTitleOfCourtesy { get; set; }
        public string LabelBirthDate { get; set; }
        public string LabelHireDate { get; set; }
        public string LabelAddress { get; set; }
        public string LabelCity { get; set; }
        public string LabelRegion { get; set; }
        public string LabelPostalCode { get; set; }
        public string LabelCountry { get; set; }
        public string LabelHomePhone { get; set; }
        public string LabelExtension { get; set; }
        public string LabelPhoto { get; set; }
        public string LabelNotes { get; set; }
        public string LabelEmployees_LastName { get; set; }
        public string LabelPhotoPath { get; set; }
    }

    public class LanguageElementsControlsEmployees
    {
        public LanguageElementsControlsEmployees()
        {
            this.LabelBtnDelete = "btnDelete";
            this.LabelBtnNew = "btnNew";
            this.LabelBtnUpdate = "btnUpdate";            
        }
        public string LabelBtnDelete { get; set; }
        public string LabelBtnNew { get; set; }
        public string LabelBtnUpdate { get; set; }
    }

    public class LanguageElementsMessagesEmployees
    {
        public LanguageElementsMessagesEmployees()
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
