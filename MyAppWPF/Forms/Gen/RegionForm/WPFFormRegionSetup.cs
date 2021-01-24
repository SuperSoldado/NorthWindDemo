using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;
using System.Linq;

namespace MyApp.WPFForms.Region
{
    public partial class FormWPFRegion
    {    
        public void Setup_SetLanguage(string language)
        {
            if (language == null)
            {
                return;
            }

            LabelsAndMessagesRegion labelsAndMessagesRegion = new LabelsAndMessagesRegion();
            WPFLanguage wPFLanguage = LanguageHelper.GetLanguageForForms(language, "Region");
            LabelsAndMessagesRegion labelsAndMessages = new LabelsAndMessagesRegion();
            LanguageElement languageElement;

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBRegion.LabelRegionID).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBRegion.LabelRegionID = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBRegion.LabelRegionDescription).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBRegion.LabelRegionDescription = languageElement.TranslatedValue;
            }

            //Labels
            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsRegion.LabelBtnNew).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsRegion.LabelBtnNew = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsRegion.LabelBtnDelete).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsRegion.LabelBtnDelete = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsRegion.LabelBtnUpdate).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsRegion.LabelBtnUpdate = languageElement.TranslatedValue;
            }

            //Messages
            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesRegion.MessageBoxDeleteConfirm).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesRegion.MessageBoxDeleteConfirm = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesRegion.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesRegion.MessageBoxSaveError = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesRegion.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesRegion.MessageBoxSaveOK = languageElement.TranslatedValue;
            }
            
            RegionDataContext.LabelsAndMessagesRegion = labelsAndMessages;
        }
    }
}
