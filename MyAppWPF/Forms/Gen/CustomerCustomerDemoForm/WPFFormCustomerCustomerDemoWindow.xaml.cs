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

    namespace MyApp.WPFForms.CustomerCustomerDemo
    {
    
    public partial class FormWPFCustomerCustomerDemo : Page
    {    
        public CustomerCustomerDemoDataContext CustomerCustomerDemoDataContext { get; set; }
        public IWPFCustomerCustomerDemoDataConnection dataConnection;
    

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
        public FormWPFCustomerCustomerDemo(WPFConfig config, string CustomerID,string CustomerTypeID, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            dataConnection = new WPFCustomerCustomerDemoDB(config);
            string error = null;
            CustomerCustomerDemoDataContext = dataConnection.GetDataContext(CustomerID,CustomerTypeID, out error);
            DataContext = CustomerCustomerDemoDataContext;
            InitializeComponent();
        }       

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="config">Default config object</param>
        public FormWPFCustomerCustomerDemo(WPFConfig config)
        {
            this.config = config;
            dataConnection = new WPFCustomerCustomerDemoDB(config);
            string error = null;
            CustomerCustomerDemoDataContext = dataConnection.GetEmptyDataContext(out error);
            if (error != null)
            {
                MessageBox.Show(error);
            }
            DataContext = CustomerCustomerDemoDataContext;
            InitializeComponent();
        }

        /// <summary>
        /// Load the form using table 'CustomerCustomerDemo' PrimaryKey
        /// </summary>
        public void LoadForm(string CustomerID,string CustomerTypeID)
        {            
            string error = null;
            CustomerCustomerDemoDataContext = dataConnection.GetDataContext(CustomerID,CustomerTypeID, out error);
            DataContext = CustomerCustomerDemoDataContext;
        }

        public void LoadLanguages(string language)
        {
            if (language == null)
            {
                return;
            }

            LabelsAndMessagesCustomerCustomerDemo labelsAndMessagesCustomerCustomerDemo = new LabelsAndMessagesCustomerCustomerDemo();
            WPFLanguage wPFLanguage = LanguageHelper.GetLanguageForForms(language, "CustomerCustomerDemo");
        }

        /// <summary>
        /// Update existing data or include new data
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {    
            string error = null;
            var itemToSave = CustomerCustomerDemoDataContext.modelNotifiedForCustomerCustomerDemoMain;
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
            PropertyInfo[] sourceListClassInfoProperties = typeof(ModelNotifiedForCustomerCustomerDemo).GetProperties();
            foreach (PropertyInfo sourceProperty in sourceListClassInfoProperties)
            {
                sourceProperty.SetValue(CustomerCustomerDemoDataContext.modelNotifiedForCustomerCustomerDemoMain, null, null);
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
                    dataConnection.DeleteData(CustomerCustomerDemoDataContext.modelNotifiedForCustomerCustomerDemoMain, out error);
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

