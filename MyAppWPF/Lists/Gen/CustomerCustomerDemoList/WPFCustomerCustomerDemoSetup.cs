using MyAppGlobalLib;
using MyAppWPFLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using static MyAppGlobalLib.GlobalEnums;

namespace MyApp.WPFList.CustomerCustomerDemo
{
    /// <summary>
    /// Setup for List. All this methods change the basic behaviour of the list. 
    /// Example: customizing visibilitiy of filters and columns
    /// </summary>
    public partial class ListWPFCustomerCustomerDemo : Page
    {
        public void Setup_SetLanguage(string language)
        {           
            CurrentLanguage = language;
            WPFLanguage wPFLanguage = LanguageHelper.GetLanguageForList(language, "CustomerCustomerDemo");
            Setup_SetLanguage(wPFLanguage);
            Setup_Controls(wPFLanguage);            
            Setup_Messages(wPFLanguage);
        }

        private void Setup_Controls(WPFLanguage language)
        {
            string languageElementID = null;
            LanguageElement languageElement;

            languageElementID = WPFLanguage.LanguageControls.List_btnOpenForm.ToString();
            languageElement = language.LanguageElementsDefaultControls.Where(x => x.OriginalValue == languageElementID).FirstOrDefault();
            if (languageElement != null)
            {
                CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.LabelBtnOpenForm = languageElement.TranslatedValue;
            }

            languageElementID = WPFLanguage.LanguageControls.List_btnDelete.ToString();
            languageElement = language.LanguageElementsDefaultControls.Where(x => x.OriginalValue == languageElementID).FirstOrDefault();
            if (languageElement != null)
            {
                CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.LabelBtnDelete = languageElement.TranslatedValue;
            }

            languageElementID = WPFLanguage.LanguageControls.List_btnSave.ToString();
            languageElement = language.LanguageElementsDefaultControls.Where(x => x.OriginalValue == languageElementID).FirstOrDefault();
            if (languageElement != null)
            {
                CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.LabelBtnSave = languageElement.TranslatedValue;
            }

            languageElementID = WPFLanguage.LanguageControls.List_btnReload.ToString();
            languageElement = language.LanguageElementsDefaultControls.Where(x => x.OriginalValue == languageElementID).FirstOrDefault();
            if (languageElement != null)
            {
                CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.LabelBtnReload = languageElement.TranslatedValue;
            }

            languageElementID = WPFLanguage.LanguageControls.List_btnFilter.ToString();
            languageElement = language.LanguageElementsDefaultControls.Where(x => x.OriginalValue == languageElementID).FirstOrDefault();
            if (languageElement != null)
            {
                CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.LabelBtnFilter = languageElement.TranslatedValue;
            }

            languageElementID = WPFLanguage.LanguageControls.groupVisibility.ToString();
            languageElement = language.LanguageElementsDefaultControls.Where(x => x.OriginalValue == languageElementID).FirstOrDefault();
            if (languageElement != null)
            {
                CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.LabelGroupVisibility = languageElement.TranslatedValue;
            }

            languageElementID = WPFLanguage.LanguageControls.groupSearch.ToString();
            languageElement = language.LanguageElementsDefaultControls.Where(x => x.OriginalValue == languageElementID).FirstOrDefault();
            if (languageElement != null)
            {
                CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.LabelGroupSearch = languageElement.TranslatedValue;
            }
        }

        private void Setup_Messages(WPFLanguage language)
        {
            LanguageElement languageElement;
            string id;
            
            id = WPFLanguage.LanguageMessages.MessageBoxDeleteConfirm.ToString();
            languageElement = language.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == id).FirstOrDefault();
            if (languageElement != null)
            {
                //MessageBoxDeleteConfirm = languageElement.TranslatedValue;
                CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirm = languageElement.TranslatedValue;
            }

            id = WPFLanguage.LanguageMessages.MessageBoxDeleteConfirmCaption.ToString();
            languageElement = language.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == id).FirstOrDefault();
            if (languageElement != null)
            {
                //MessageBoxDeleteConfirmCaption = languageElement.TranslatedValue;
                CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirmCaption = languageElement.TranslatedValue; 
            }

            id = WPFLanguage.LanguageMessages.MessageBoxDeleteOK.ToString();
            languageElement = language.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == id).FirstOrDefault();
            if (languageElement != null)
            {
                //MessageBoxDeleteOK = languageElement.TranslatedValue;
                CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.MessageBoxDeleteOK = languageElement.TranslatedValue;
            }            

            id = WPFLanguage.LanguageMessages.MessageBoxSaveError.ToString();
            languageElement = language.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == id).FirstOrDefault();
            if (languageElement != null)
            {
                //MessageBoxSaveError = languageElement.TranslatedValue;
                CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.MessageBoxSaveError = languageElement.TranslatedValue;
            }

            id = WPFLanguage.LanguageMessages.MessageBoxSaveOK.ToString();
            languageElement = language.LanguageElementsDefaultMessages.Where(x => x.OriginalValue == id).FirstOrDefault();
            if (languageElement != null)
            {
                //MessageBoxSaveOK = languageElement.TranslatedValue;
                CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.MessageBoxSaveOK = languageElement.TranslatedValue;
            }
        }        

        public void Setup_SetLanguage(WPFLanguage language)
        {
            if (language == null)
            {
                return;
            }

            foreach (var gridColumn in DataGridCustomerCustomerDemo.Columns)
            {
                if (gridColumn.Header == null)
                {
                    continue;
                }

                //The replace is done because FK values in Grid are 'MyTable.MyColumn' but in json are 'MyTable_MyColumn'
                string actualHeaderValue = gridColumn.Header.ToString().Replace(".", "_");
                LanguageElement languageElement  =  language.LanguageElementsFromDB.Where(x => x.OriginalValue.ToLower() == actualHeaderValue.ToLower()).FirstOrDefault();
                if (languageElement == null)
                {
                    continue;
                }

                gridColumn.Header = languageElement.TranslatedValue;
            }
        }

        /// <summary>
        /// Hides/Shows "Filter" group box.
        /// </summary>
        /// <param name="filterGroupBoxIsVisible"></param>
        public void Setup_FilterGroupBoxIsVisible(bool filterGroupBoxIsVisible)
        {
            GridLengthConverter gridLengthConverter = new GridLengthConverter();
            if (filterGroupBoxIsVisible)
            {
                groupVisibility.Visibility = Visibility.Visible;
                mainGrid.RowDefinitions[0].Height = (GridLength)gridLengthConverter.ConvertFrom("90");
            }
            else
            {
                mainGrid.RowDefinitions[0].Height = (GridLength)gridLengthConverter.ConvertFrom("0");
                groupVisibility.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Set the maxWidth of all columns
        /// </summary>
        /// <param name="maxWidth"></param>
        public void Setup_SetColumnsMaxLenght(int maxWidth)
        {
            foreach (var item in DataGridCustomerCustomerDemo.Columns)
            {
                item.MaxWidth = maxWidth;
            }
        }
    }
}
