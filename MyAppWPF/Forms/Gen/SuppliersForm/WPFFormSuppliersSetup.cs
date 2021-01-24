using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;
using System.Linq;

namespace MyApp.WPFForms.Suppliers
{
    public partial class FormWPFSuppliers
    {    
        public void Setup_SetLanguage(string language)
        {
            if (language == null)
            {
                return;
            }

            LabelsAndMessagesSuppliers labelsAndMessagesSuppliers = new LabelsAndMessagesSuppliers();
            WPFLanguage wPFLanguage = LanguageHelper.GetLanguageForForms(language, "Suppliers");
            LabelsAndMessagesSuppliers labelsAndMessages = new LabelsAndMessagesSuppliers();
            LanguageElement languageElement;

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBSuppliers.LabelSupplierID).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBSuppliers.LabelSupplierID = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBSuppliers.LabelCompanyName).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBSuppliers.LabelCompanyName = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBSuppliers.LabelContactName).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBSuppliers.LabelContactName = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBSuppliers.LabelContactTitle).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBSuppliers.LabelContactTitle = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBSuppliers.LabelAddress).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBSuppliers.LabelAddress = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBSuppliers.LabelCity).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBSuppliers.LabelCity = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBSuppliers.LabelRegion).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBSuppliers.LabelRegion = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBSuppliers.LabelPostalCode).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBSuppliers.LabelPostalCode = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBSuppliers.LabelCountry).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBSuppliers.LabelCountry = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBSuppliers.LabelPhone).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBSuppliers.LabelPhone = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBSuppliers.LabelFax).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBSuppliers.LabelFax = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBSuppliers.LabelHomePage).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBSuppliers.LabelHomePage = languageElement.TranslatedValue;
            }

            //Labels
            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsSuppliers.LabelBtnNew).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsSuppliers.LabelBtnNew = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsSuppliers.LabelBtnDelete).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsSuppliers.LabelBtnDelete = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsSuppliers.LabelBtnUpdate).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsSuppliers.LabelBtnUpdate = languageElement.TranslatedValue;
            }

            //Messages
            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesSuppliers.MessageBoxDeleteConfirm).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesSuppliers.MessageBoxDeleteConfirm = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesSuppliers.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesSuppliers.MessageBoxSaveError = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesSuppliers.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesSuppliers.MessageBoxSaveOK = languageElement.TranslatedValue;
            }
            
            SuppliersDataContext.LabelsAndMessagesSuppliers = labelsAndMessages;
        }
    }
}
