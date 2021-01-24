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

    namespace MyApp.WPFForms.Categories
    {
    
    public partial class FormWPFCategories : Page
    {    
        public CategoriesDataContext CategoriesDataContext { get; set; }
        public IWPFCategoriesDataConnection dataConnection;
    

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
        public FormWPFCategories(WPFConfig config, int CategoryID, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            dataConnection = new WPFCategoriesDB(config);
            string error = null;
            CategoriesDataContext = dataConnection.GetDataContext(CategoryID, out error);
            DataContext = CategoriesDataContext;
            InitializeComponent();
        }       

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="config">Default config object</param>
        public FormWPFCategories(WPFConfig config)
        {
            this.config = config;
            dataConnection = new WPFCategoriesDB(config);
            string error = null;
            CategoriesDataContext = dataConnection.GetEmptyDataContext(out error);
            if (error != null)
            {
                MessageBox.Show(error);
            }
            DataContext = CategoriesDataContext;
            InitializeComponent();
        }

        /// <summary>
        /// Load the form using table 'Categories' PrimaryKey
        /// </summary>
        public void LoadForm(int CategoryID)
        {            
            string error = null;
            CategoriesDataContext = dataConnection.GetDataContext(CategoryID, out error);
            DataContext = CategoriesDataContext;
        }

        public void LoadLanguages(string language)
        {
            if (language == null)
            {
                return;
            }

            LabelsAndMessagesCategories labelsAndMessagesCategories = new LabelsAndMessagesCategories();
            WPFLanguage wPFLanguage = LanguageHelper.GetLanguageForForms(language, "Categories");
        }

        /// <summary>
        /// Update existing data or include new data
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {    
            string error = null;
            var itemToSave = CategoriesDataContext.modelNotifiedForCategoriesMain;
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
            PropertyInfo[] sourceListClassInfoProperties = typeof(ModelNotifiedForCategories).GetProperties();
            foreach (PropertyInfo sourceProperty in sourceListClassInfoProperties)
            {
                sourceProperty.SetValue(CategoriesDataContext.modelNotifiedForCategoriesMain, null, null);
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
                    dataConnection.DeleteData(CategoriesDataContext.modelNotifiedForCategoriesMain, out error);
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

        private void OnMouseDownBinary(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            if (image.Tag.ToString().ToLower() == "Picture".ToLower())
            {
               frmDisplayBinaryData frmDisplayBinaryData = new frmDisplayBinaryData(CategoriesDataContext.modelNotifiedForCategoriesMain.Picture, GlobalEnums.MimeTypes.Image);
               frmDisplayBinaryData.ShowDialog();
               //frmDisplayBinaryData.Owner = ToDo. Without owner, alt tab can bug
               CategoriesDataContext.modelNotifiedForCategoriesMain.Picture = frmDisplayBinaryData.myViewModel.BinData;
               return;            
            }
            MessageBox.Show("Error: 'btnSaveBinAs' does not contains the tag reference to target binary column.");
        }
    

    }
    }

