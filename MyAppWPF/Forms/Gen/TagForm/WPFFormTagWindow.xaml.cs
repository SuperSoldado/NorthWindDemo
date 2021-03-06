using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using MyAppWPFLib;

    namespace MyApp.WPFForms.Tag
    {
    
    public partial class FormWPFTag : Page
    {    
        public TagDataContext TagDataContext { get; set; }
        public IWPFTagDataConnection dataConnection;
    

        /// <summary>
        /// Permits navigation into main frame
        /// </summary>
        private Frame FrameMainWindow { get; set; }

        /// <summary>
        /// Default configuration object. 
        /// </summary>
        private WPFConfig config {get; set;}

        /// <summary>
        /// Alternative contructor. Creates the Form and also load it's data using table '' Primary key.
        /// </summary>
        public FormWPFTag(WPFConfig config, int TagID, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            dataConnection = new WPFTagDB(config);
            string error = null;
            TagDataContext = dataConnection.GetDataContext(TagID, out error);
            DataContext = TagDataContext;
            InitializeComponent();
        }       

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="config">Default config object</param>
        public FormWPFTag(WPFConfig config)
        {
            this.config = config;
            dataConnection = new WPFTagDB(config);
            string error = null;
            TagDataContext = dataConnection.GetEmptyDataContext(out error);
            if (error != null)
            {
                MessageBox.Show(error);
            }
            DataContext = TagDataContext;
            InitializeComponent();
        }

        /// <summary>
        /// Load the form using table 'Tag' PrimaryKey
        /// </summary>
        public void LoadForm(int TagID)
        {            
            string error = null;
            TagDataContext = dataConnection.GetDataContext(TagID, out error);
            DataContext = TagDataContext;
        }

        public void LoadLanguages(string language)
        {
            if (language == null)
            {
                return;
            }

            LabelsAndMessagesTag labelsAndMessagesTag = new LabelsAndMessagesTag();
            WPFLanguage wPFLanguage = LanguageHelper.GetLanguageForForms(language, "Tag");
        }

        /// <summary>
        /// Update existing data or include new data
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {    
            string error = null;
            var itemToSave = TagDataContext.modelNotifiedForTagMain;
            if (btnUpdate.Tag == "btnSave")//todo este if nao funciona mais. pegar da "if new " da classe.
            {
                dataConnection.AddData(itemToSave, out error);
            }
            else
            {
                dataConnection.SaveData(itemToSave, out error);
            }
            
            if (error != null)
            {
                MessageBox.Show(error);
            }
            else
            {
                MessageBox.Show("OK");
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            PropertyInfo[] sourceListClassInfoProperties = typeof(ModelNotifiedForTag).GetProperties();
            foreach (PropertyInfo sourceProperty in sourceListClassInfoProperties)
            {
                sourceProperty.SetValue(TagDataContext.modelNotifiedForTagMain, null, null);
            }

            btnUpdate.Tag = "btnSave";
            btnNew.Visibility = Visibility.Hidden;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            btnNew.Visibility = Visibility.Visible;
            string messageBoxText = "Do you want to delete changes?";
            string caption = "Word Processor";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            string error = null;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    dataConnection.DeleteData(TagDataContext.modelNotifiedForTagMain, out error);
                    break;
                case MessageBoxResult.No:
                    return;
            }

            if (error != null)
            {
                MessageBox.Show(error);
            }
            else
            {
                btnNew_Click(null, null);
                MessageBox.Show("Deleted");
            }
        }

    

    }
    }

