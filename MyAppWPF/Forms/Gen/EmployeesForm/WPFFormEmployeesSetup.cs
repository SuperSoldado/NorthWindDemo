using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;
using System.Linq;

namespace MyApp.WPFForms.Employees
{
    public partial class FormWPFEmployees
    {    
        public void Setup_SetLanguage(string language)
        {
            if (language == null)
            {
                return;
            }

            LabelsAndMessagesEmployees labelsAndMessagesEmployees = new LabelsAndMessagesEmployees();
            WPFLanguage wPFLanguage = LanguageHelper.GetLanguageForForms(language, "Employees");
            LabelsAndMessagesEmployees labelsAndMessages = new LabelsAndMessagesEmployees();
            LanguageElement languageElement;

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelEmployeeID).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelEmployeeID = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelLastName).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelLastName = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelFirstName).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelFirstName = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelTitle).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelTitle = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelTitleOfCourtesy).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelTitleOfCourtesy = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelBirthDate).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelBirthDate = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelHireDate).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelHireDate = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelAddress).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelAddress = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelCity).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelCity = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelRegion).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelRegion = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelPostalCode).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelPostalCode = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelCountry).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelCountry = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelHomePhone).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelHomePhone = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelExtension).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelExtension = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelPhoto).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelPhoto = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelNotes).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelNotes = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelEmployees_LastName).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelEmployees_LastName = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployees.LabelPhotoPath).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployees.LabelPhotoPath = languageElement.TranslatedValue;
            }

            //Labels
            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsEmployees.LabelBtnNew).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsEmployees.LabelBtnNew = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsEmployees.LabelBtnDelete).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsEmployees.LabelBtnDelete = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsEmployees.LabelBtnUpdate).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsEmployees.LabelBtnUpdate = languageElement.TranslatedValue;
            }

            //Messages
            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesEmployees.MessageBoxDeleteConfirm).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesEmployees.MessageBoxDeleteConfirm = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesEmployees.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesEmployees.MessageBoxSaveError = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesEmployees.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesEmployees.MessageBoxSaveOK = languageElement.TranslatedValue;
            }
            
            EmployeesDataContext.LabelsAndMessagesEmployees = labelsAndMessages;
        }
    }
}
