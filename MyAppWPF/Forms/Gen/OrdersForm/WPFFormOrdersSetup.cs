using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;
using System.Linq;

namespace MyApp.WPFForms.Orders
{
    public partial class FormWPFOrders
    {    
        public void Setup_SetLanguage(string language)
        {
            if (language == null)
            {
                return;
            }

            LabelsAndMessagesOrders labelsAndMessagesOrders = new LabelsAndMessagesOrders();
            WPFLanguage wPFLanguage = LanguageHelper.GetLanguageForForms(language, "Orders");
            LabelsAndMessagesOrders labelsAndMessages = new LabelsAndMessagesOrders();
            LanguageElement languageElement;

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrders.LabelOrderID).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrders.LabelOrderID = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrders.LabelCustomers_ContactName).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrders.LabelCustomers_ContactName = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrders.LabelEmployees_LastName).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrders.LabelEmployees_LastName = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrders.LabelOrderDate).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrders.LabelOrderDate = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrders.LabelRequiredDate).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrders.LabelRequiredDate = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrders.LabelShippedDate).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrders.LabelShippedDate = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrders.LabelShippers_CompanyName).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrders.LabelShippers_CompanyName = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrders.LabelFreight).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrders.LabelFreight = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrders.LabelShipName).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrders.LabelShipName = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrders.LabelShipAddress).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrders.LabelShipAddress = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrders.LabelShipCity).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrders.LabelShipCity = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrders.LabelShipRegion).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrders.LabelShipRegion = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrders.LabelShipPostalCode).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrders.LabelShipPostalCode = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBOrders.LabelShipCountry).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBOrders.LabelShipCountry = languageElement.TranslatedValue;
            }

            //Labels
            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsOrders.LabelBtnNew).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsOrders.LabelBtnNew = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsOrders.LabelBtnDelete).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsOrders.LabelBtnDelete = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsOrders.LabelBtnUpdate).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsOrders.LabelBtnUpdate = languageElement.TranslatedValue;
            }

            //Messages
            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesOrders.MessageBoxDeleteConfirm).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesOrders.MessageBoxDeleteConfirm = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesOrders.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesOrders.MessageBoxSaveError = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesOrders.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesOrders.MessageBoxSaveOK = languageElement.TranslatedValue;
            }
            
            OrdersDataContext.LabelsAndMessagesOrders = labelsAndMessages;
        }
    }
}
