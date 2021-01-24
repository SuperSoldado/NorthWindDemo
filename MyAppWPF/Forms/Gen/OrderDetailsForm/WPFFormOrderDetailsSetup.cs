using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;
using System.Linq;

namespace MyApp.WPFForms.OrderDetails
{
    public partial class FormWPFOrderDetails
    {    
        public void Setup_SetLanguage(string language)
        {
            if (language == null)
            {
                return;
            }

            LabelsAndMessagesOrderDetails labelsAndMessagesOrderDetails = new LabelsAndMessagesOrderDetails();
            WPFLanguage wPFLanguage = LanguageHelper.GetLanguageForForms(language, "OrderDetails");
            LabelsAndMessagesOrderDetails labelsAndMessages = new LabelsAndMessagesOrderDetails();
            LanguageElement languageElement;

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrderDetails.LabelOrders_ShipName).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrderDetails.LabelOrders_ShipName = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrderDetails.LabelProducts_ProductName).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrderDetails.LabelProducts_ProductName = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrderDetails.LabelUnitPrice).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrderDetails.LabelUnitPrice = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrderDetails.LabelQuantity).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrderDetails.LabelQuantity = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrderDetails.LabelDiscount).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrderDetails.LabelDiscount = languageElement.TranslatedValue;
            }

            //Labels
            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsOrderDetails.LabelBtnNew).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsOrderDetails.LabelBtnNew = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsOrderDetails.LabelBtnDelete).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsOrderDetails.LabelBtnDelete = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsOrderDetails.LabelBtnUpdate).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsOrderDetails.LabelBtnUpdate = languageElement.TranslatedValue;
            }

            //Messages
            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesOrderDetails.MessageBoxDeleteConfirm).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesOrderDetails.MessageBoxDeleteConfirm = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesOrderDetails.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesOrderDetails.MessageBoxSaveError = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesOrderDetails.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesOrderDetails.MessageBoxSaveOK = languageElement.TranslatedValue;
            }
            
            OrderDetailsDataContext.LabelsAndMessagesOrderDetails = labelsAndMessages;
        }
    }
}
