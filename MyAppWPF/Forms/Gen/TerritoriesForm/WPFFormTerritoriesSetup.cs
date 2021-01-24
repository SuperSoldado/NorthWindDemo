using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;
using System.Linq;

namespace MyApp.WPFForms.Territories
{
    public partial class FormWPFTerritories
    {    
        public void Setup_SetLanguage(string language)
        {
            if (language == null)
            {
                return;
            }

            LabelsAndMessagesTerritories labelsAndMessagesTerritories = new LabelsAndMessagesTerritories();
            WPFLanguage wPFLanguage = LanguageHelper.GetLanguageForForms(language, "Territories");
            LabelsAndMessagesTerritories labelsAndMessages = new LabelsAndMessagesTerritories();
            LanguageElement languageElement;

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBTerritories.LabelTerritoryID).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBTerritories.LabelTerritoryID = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBTerritories.LabelTerritoryDescription).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBTerritories.LabelTerritoryDescription = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBTerritories.LabelRegion_RegionDescription).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBTerritories.LabelRegion_RegionDescription = languageElement.TranslatedValue;
            }

            //Labels
            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsTerritories.LabelBtnNew).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsTerritories.LabelBtnNew = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsTerritories.LabelBtnDelete).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsTerritories.LabelBtnDelete = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsTerritories.LabelBtnUpdate).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsTerritories.LabelBtnUpdate = languageElement.TranslatedValue;
            }

            //Messages
            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesTerritories.MessageBoxDeleteConfirm).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesTerritories.MessageBoxDeleteConfirm = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesTerritories.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesTerritories.MessageBoxSaveError = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesTerritories.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesTerritories.MessageBoxSaveOK = languageElement.TranslatedValue;
            }
            
            TerritoriesDataContext.LabelsAndMessagesTerritories = labelsAndMessages;
        }
    }
}
