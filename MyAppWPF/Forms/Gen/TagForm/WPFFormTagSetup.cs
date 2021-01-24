using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;
using System.Linq;

namespace MyApp.WPFForms.Tag
{
    public partial class FormWPFTag
    {    
        public void Setup_SetLanguage(string language)
        {
            if (language == null)
            {
                return;
            }

            LabelsAndMessagesTag labelsAndMessagesTag = new LabelsAndMessagesTag();
            WPFLanguage wPFLanguage = LanguageHelper.GetLanguageForForms(language, "Tag");
            LabelsAndMessagesTag labelsAndMessages = new LabelsAndMessagesTag();
            LanguageElement languageElement;

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBTag.LabelTagID).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBTag.LabelTagID = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBTag.LabelTextDesc).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBTag.LabelTextDesc = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsFromDB.Where(x => x.OriginalValue == labelsAndMessages.LabelsFromDBTag.LabelTagType).FirstOrDefault();            
            if (languageElement != null)
            {
                labelsAndMessages.LabelsFromDBTag.LabelTagType = languageElement.TranslatedValue;
            }

            //Labels
            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsTag.LabelBtnNew).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsTag.LabelBtnNew = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsTag.LabelBtnDelete).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsTag.LabelBtnDelete = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultControls.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsControlsTag.LabelBtnUpdate).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsControlsTag.LabelBtnUpdate = languageElement.TranslatedValue;
            }

            //Messages
            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesTag.MessageBoxDeleteConfirm).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesTag.MessageBoxDeleteConfirm = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesTag.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesTag.MessageBoxSaveError = languageElement.TranslatedValue;
            }

            languageElement = wPFLanguage.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == labelsAndMessages.LanguageElementsMessagesTag.MessageBoxSaveOK).FirstOrDefault();
            if (languageElement != null)
            {
                labelsAndMessages.LanguageElementsMessagesTag.MessageBoxSaveOK = languageElement.TranslatedValue;
            }
            
            TagDataContext.LabelsAndMessagesTag = labelsAndMessages;
        }
    }
}
