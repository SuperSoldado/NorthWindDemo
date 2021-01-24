using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;
using System.Linq;

namespace MyApp.WPFForms.Customers
{
    public partial class FormWPFCustomers
    {    
        public void Setup_SetLanguage(string language)
        {
            if (language == null)
            {
                return;
            }

            LabelsAndMessagesCustomers labelsAndMessagesCustomers = new LabelsAndMessagesCustomers();
            WPFLanguage wPFLanguage = LanguageHelper.GetLanguageForForms(language, "Customers");
            LabelsAndMessagesCustomers labelsAndMessages = new LabelsAndMessagesCustomers();
            LanguageElement languageElement;

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBCustomers.LabelCustomerID).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBCustomers.LabelCustomerID = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBCustomers.LabelCompanyName).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBCustomers.LabelCompanyName = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBCustomers.LabelContactName).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBCustomers.LabelContactName = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBCustomers.LabelContactTitle).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBCustomers.LabelContactTitle = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBCustomers.LabelAddress).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBCustomers.LabelAddress = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBCustomers.LabelCity).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBCustomers.LabelCity = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBCustomers.LabelRegion).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBCustomers.LabelRegion = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBCustomers.LabelPostalCode).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBCustomers.LabelPostalCode = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBCustomers.LabelCountry).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBCustomers.LabelCountry = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBCustomers.LabelPhone).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBCustomers.LabelPhone = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBCustomers.LabelFax).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBCustomers.LabelFax = languageElement.TranslatedValue;
            }

            //Labels
            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsCustomers.LabelBtnNew).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsCustomers.LabelBtnNew = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsCustomers.LabelBtnDelete).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsCustomers.LabelBtnDelete = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsCustomers.LabelBtnUpdate).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsCustomers.LabelBtnUpdate = languageElement.TranslatedValue;
            }

            //Messages
            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesCustomers.MessageBoxDeleteConfirm).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesCustomers.MessageBoxDeleteConfirm = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesCustomers.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesCustomers.MessageBoxSaveError = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesCustomers.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesCustomers.MessageBoxSaveOK = languageElement.TranslatedValue;
            }
            
            CustomersDataContext.LabelsAndMessagesCustomers = labelsAndMessages;
        }
    }
}
