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

    namespace MyApp.WPFForms.EmployeeTerritories
    {
    
    public partial class FormWPFEmployeeTerritories : Page
    {    
        public EmployeeTerritoriesDataContext EmployeeTerritoriesDataContext { get; set; }
        public IWPFEmployeeTerritoriesDataConnection dataConnection;
    

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
        public FormWPFEmployeeTerritories(WPFConfig config, int EmployeeID,string TerritoryID, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            dataConnection = new WPFEmployeeTerritoriesDB(config);
            string error = null;
            EmployeeTerritoriesDataContext = dataConnection.GetDataContext(EmployeeID,TerritoryID, out error);
            DataContext = EmployeeTerritoriesDataContext;
            InitializeComponent();
        }       

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="config">Default config object</param>
        public FormWPFEmployeeTerritories(WPFConfig config)
        {
            this.config = config;
            dataConnection = new WPFEmployeeTerritoriesDB(config);
            string error = null;
            EmployeeTerritoriesDataContext = dataConnection.GetEmptyDataContext(out error);
            if (error != null)
            {
                MessageBox.Show(error);
            }
            DataContext = EmployeeTerritoriesDataContext;
            InitializeComponent();
        }

        /// <summary>
        /// Load the form using table 'EmployeeTerritories' PrimaryKey
        /// </summary>
        public void LoadForm(int EmployeeID,string TerritoryID)
        {            
            string error = null;
            EmployeeTerritoriesDataContext = dataConnection.GetDataContext(EmployeeID,TerritoryID, out error);
            DataContext = EmployeeTerritoriesDataContext;
        }

        public void LoadLanguages(string language)
        {
            if (language == null)
            {
                return;
            }

            LabelsAndMessagesEmployeeTerritories labelsAndMessagesEmployeeTerritories = new LabelsAndMessagesEmployeeTerritories();
            WPFLanguage wPFLanguage = LanguageHelper.GetLanguageForForms(language, "EmployeeTerritories");
        }

        /// <summary>
        /// Update existing data or include new data
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {    
            string error = null;
            var itemToSave = EmployeeTerritoriesDataContext.modelNotifiedForEmployeeTerritoriesMain;
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
            PropertyInfo[] sourceListClassInfoProperties = typeof(ModelNotifiedForEmployeeTerritories).GetProperties();
            foreach (PropertyInfo sourceProperty in sourceListClassInfoProperties)
            {
                sourceProperty.SetValue(EmployeeTerritoriesDataContext.modelNotifiedForEmployeeTerritoriesMain, null, null);
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
                    dataConnection.DeleteData(EmployeeTerritoriesDataContext.modelNotifiedForEmployeeTerritoriesMain, out error);
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

