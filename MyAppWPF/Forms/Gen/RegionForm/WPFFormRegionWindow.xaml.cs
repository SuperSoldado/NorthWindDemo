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

    namespace MyApp.WPFForms.Region
    {
    
    public partial class FormWPFRegion : Page
    {    
        public RegionDataContext RegionDataContext { get; set; }
        public IWPFRegionDataConnection dataConnection;
    

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
        public FormWPFRegion(WPFConfig config, int RegionID, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            dataConnection = new WPFRegionDB(config);
            string error = null;
            RegionDataContext = dataConnection.GetDataContext(RegionID, out error);
            DataContext = RegionDataContext;
            InitializeComponent();
        }       

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="config">Default config object</param>
        public FormWPFRegion(WPFConfig config)
        {
            this.config = config;
            dataConnection = new WPFRegionDB(config);
            string error = null;
            RegionDataContext = dataConnection.GetEmptyDataContext(out error);
            if (error != null)
            {
                MessageBox.Show(error);
            }
            DataContext = RegionDataContext;
            InitializeComponent();
        }

        /// <summary>
        /// Load the form using table 'Region' PrimaryKey
        /// </summary>
        public void LoadForm(int RegionID)
        {            
            string error = null;
            RegionDataContext = dataConnection.GetDataContext(RegionID, out error);
            DataContext = RegionDataContext;
        }

        public void LoadLanguages(string language)
        {
            if (language == null)
            {
                return;
            }

            LabelsAndMessagesRegion labelsAndMessagesRegion = new LabelsAndMessagesRegion();
            WPFLanguage wPFLanguage = LanguageHelper.GetLanguageForForms(language, "Region");
        }

        /// <summary>
        /// Update existing data or include new data
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {    
            string error = null;
            var itemToSave = RegionDataContext.modelNotifiedForRegionMain;
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
            PropertyInfo[] sourceListClassInfoProperties = typeof(ModelNotifiedForRegion).GetProperties();
            foreach (PropertyInfo sourceProperty in sourceListClassInfoProperties)
            {
                sourceProperty.SetValue(RegionDataContext.modelNotifiedForRegionMain, null, null);
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
                    dataConnection.DeleteData(RegionDataContext.modelNotifiedForRegionMain, out error);
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

