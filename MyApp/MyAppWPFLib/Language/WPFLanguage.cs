using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using System.IO;

/// <summary>
/// Note: when change this class, change also in "Codetomat". This class is duplicated because they stay in different projects.
/// </summary>
namespace MyAppWPFLib
{
    /// <summary>
    /// Element (ex.: column) to be translated
    /// </summary>
    public class LanguageElement
    {
        public LanguageElement()
        {
            this.TranslationType = Translation_Type.Unknown;
        }

        /// <summary>
        /// Original value auto generated
        /// </summary>
        public string OriginalValue { get; set; }

        /// <summary>
        /// Translated value
        /// </summary>
        public string TranslatedValue { get; set; }

        public Translation_Type TranslationType{ get; set; }

        /// <summary>
        /// Defines the tipe of translation
        /// </summary>
        public enum Translation_Type { 
            /// <summary>
            /// Initialize the class. Should not be used
            /// </summary>
            Unknown,

            /// <summary>
            /// The element translated is a control
            /// </summary>
            Control, 
            
            /// <summary>
            /// The element translated is a message
            /// </summary>
            Message, 
            
            /// <summary>
            /// The element translated is from Database
            /// </summary>
            DBColumn
        }
    }

    /// <summary>
    /// Main language class
    /// </summary>
    public class WPFLanguage
    {
        public string GetDefaultMessage(LanguageMessages LanguageMessages)
        {
            switch (LanguageMessages)
            {
                case LanguageMessages.MessageBoxSaveError:
                    return "xErrorx";
                case LanguageMessages.MessageBoxSaveOK:
                    return "xSavedx";
                case LanguageMessages.MessageBoxDeleteConfirm:
                    return "xAre you sure?x";
                case LanguageMessages.MessageBoxDeleteConfirmCaption:
                    return "xConfirmx";
                case LanguageMessages.MessageBoxDeleteOK:
                    return "xDeletedx";
                default:
                    throw new Exception("no default message.");
            }
        }
        /// <summary>
        /// Controls names. Used for identify the translation for controls based on it's name
        /// </summary>
        public enum LanguageControls { 
            //FilterControls
            groupVisibility, groupSearch,  List_btnFilter, List_btnReload, cbDateFilter,
            
            //Grid controls
            List_btnSave, List_btnDelete, List_btnOpenForm}
        public enum LanguageMessages { 
            MessageBoxSaveError, MessageBoxSaveOK, 
            MessageBoxDeleteConfirm, MessageBoxDeleteConfirmCaption, MessageBoxDeleteOK}
        public string Language { get; set; }
        /// <summary>
        /// Contains default translations from columns in database.
        /// </summary>        
        public List<LanguageElement> LanguageElementsFromDB { get; set; }
        
        /// <summary>
        /// Contains default translations for controls (labels, buttons).
        /// </summary>
        public List<LanguageElement> LanguageElementsDefaultControls { get; set; }
        
        /// <summary>
        /// Contains default translations for Messages (dialogs)
        /// </summary>
        public List<LanguageElement> LanguageElementsDefaultMessages { get; set; }

        public WPFLanguage()
        {
            LanguageElementsFromDB = new List<LanguageElement>();
            LanguageElementsDefaultControls = new List<LanguageElement>();
            LanguageElementsDefaultMessages = new List<LanguageElement>();
        }
    }

    /// <summary>
    /// Container class to write and read
    /// </summary>
    public class WPFLanguageContainer
    {
        public WPFLanguage ReadFile(string fileNameAndFullPathToRead)
        {
            string text = File.ReadAllText(fileNameAndFullPathToRead);
            WPFLanguage wPFLanguage = JsonConvert.DeserializeObject<WPFLanguage>(text);
            return wPFLanguage;
        }

        public void Save(WPFLanguage wPFLanguage, string fileNameAndFullPathToSave)
        {
            string jsonData = JsonConvert.SerializeObject(wPFLanguage, Formatting.Indented);
            File.WriteAllText(fileNameAndFullPathToSave, jsonData);
        }
    }
}
