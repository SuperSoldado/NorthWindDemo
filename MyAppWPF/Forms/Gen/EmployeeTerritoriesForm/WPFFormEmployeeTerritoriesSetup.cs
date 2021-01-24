using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;
using System.Linq;

namespace MyApp.WPFForms.EmployeeTerritories
{
    public partial class FormWPFEmployeeTerritories
    {    
        public void Setup_SetLanguage(string language)
        {
            if (language == null)
            {
                return;
            }

            LabelsAndMessagesEmployeeTerritories labelsAndMessagesEmployeeTerritories = new LabelsAndMessagesEmployeeTerritories();
            WPFLanguage wPFLanguage = LanguageHelper.GetLanguageForForms(language, "EmployeeTerritories");
            LabelsAndMessagesEmployeeTerritories labelsAndMessages = new LabelsAndMessagesEmployeeTerritories();
            LanguageElement languageElement;

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployeeTerritories.LabelEmployees_LastName).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployeeTerritories.LabelEmployees_LastName = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBEmployeeTerritories.LabelTerritories_TerritoryDescription).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBEmployeeTerritories.LabelTerritories_TerritoryDescription = languageElement.TranslatedValue;
            }

            //Labels
            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsEmployeeTerritories.LabelBtnNew).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsEmployeeTerritories.LabelBtnNew = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsEmployeeTerritories.LabelBtnDelete).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsEmployeeTerritories.LabelBtnDelete = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsEmployeeTerritories.LabelBtnUpdate).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsEmployeeTerritories.LabelBtnUpdate = languageElement.TranslatedValue;
            }

            //Messages
            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesEmployeeTerritories.MessageBoxDeleteConfirm).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesEmployeeTerritories.MessageBoxDeleteConfirm = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesEmployeeTerritories.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesEmployeeTerritories.MessageBoxSaveError = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesEmployeeTerritories.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesEmployeeTerritories.MessageBoxSaveOK = languageElement.TranslatedValue;
            }
            
            EmployeeTerritoriesDataContext.LabelsAndMessagesEmployeeTerritories = labelsAndMessages;
        }
    }
}
