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

    namespace MyApp.WPFForms.Employees
    {
    
    public partial class FormWPFEmployees : Page
    {    
        public EmployeesDataContext EmployeesDataContext { get; set; }
        public IWPFEmployeesDataConnection dataConnection;
    

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
        public FormWPFEmployees(WPFConfig config, int EmployeeID, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            dataConnection = new WPFEmployeesDB(config);
            string error = null;
            EmployeesDataContext = dataConnection.GetDataContext(EmployeeID, out error);
            DataContext = EmployeesDataContext;
            InitializeComponent();
        }       

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="config">Default config object</param>
        public FormWPFEmployees(WPFConfig config)
        {
            this.config = config;
            dataConnection = new WPFEmployeesDB(config);
            string error = null;
            EmployeesDataContext = dataConnection.GetEmptyDataContext(out error);
            if (error != null)
            {
                MessageBox.Show(error);
            }
            DataContext = EmployeesDataContext;
            InitializeComponent();
        }

        /// <summary>
        /// Load the form using table 'Employees' PrimaryKey
        /// </summary>
        public void LoadForm(int EmployeeID)
        {            
            string error = null;
            EmployeesDataContext = dataConnection.GetDataContext(EmployeeID, out error);
            DataContext = EmployeesDataContext;
        }

        public void LoadLanguages(string language)
        {
            if (language == null)
            {
                return;
            }

            LabelsAndMessagesEmployees labelsAndMessagesEmployees = new LabelsAndMessagesEmployees();
            WPFLanguage wPFLanguage = LanguageHelper.GetLanguageForForms(language, "Employees");
        }

        /// <summary>
        /// Update existing data or include new data
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {    
            string error = null;
            var itemToSave = EmployeesDataContext.modelNotifiedForEmployeesMain;
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
            PropertyInfo[] sourceListClassInfoProperties = typeof(ModelNotifiedForEmployees).GetProperties();
            foreach (PropertyInfo sourceProperty in sourceListClassInfoProperties)
            {
                sourceProperty.SetValue(EmployeesDataContext.modelNotifiedForEmployeesMain, null, null);
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
                    dataConnection.DeleteData(EmployeesDataContext.modelNotifiedForEmployeesMain, out error);
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
            if (image.Tag.ToString().ToLower() == "Photo".ToLower())
            {
               frmDisplayBinaryData frmDisplayBinaryData = new frmDisplayBinaryData(EmployeesDataContext.modelNotifiedForEmployeesMain.Photo, GlobalEnums.MimeTypes.Image);
               frmDisplayBinaryData.ShowDialog();
               //frmDisplayBinaryData.Owner = ToDo. Without owner, alt tab can bug
               EmployeesDataContext.modelNotifiedForEmployeesMain.Photo = frmDisplayBinaryData.myViewModel.BinData;
               return;            
            }
            MessageBox.Show("Error: 'btnSaveBinAs' does not contains the tag reference to target binary column.");
        }
    
        private void btnOpenSimpleList_Territories_Click(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForEmployees itemSelected = EmployeesDataContext.modelNotifiedForEmployeesMain;
            Container_HelperWPFDataGrid container = new Container_HelperWPFDataGrid();
            container.Data = itemSelected.LookDownComboDataTerritories;

            List<ColumnParametertInGrid> columnsInGrid = new List<ColumnParametertInGrid>();
            ColumnParametertInGrid checkBoxColumn = new ColumnParametertInGrid();
            ColumnParametertInGrid displayColumn = new ColumnParametertInGrid();

            //Setting the checkbox field
            checkBoxColumn.FieldName = "Check_Status";
            checkBoxColumn.FieldHeader = "";
            checkBoxColumn.UserCanEdit = true;
            columnsInGrid.Add(checkBoxColumn);

            //Setting display field
            displayColumn.FieldName = "TerritoryDescription";
            displayColumn.FieldHeader = "TerritoryDescription";
            displayColumn.UserCanEdit = false;
            columnsInGrid.Add(displayColumn);
            container.ColumnsInGrid = columnsInGrid;

            //Setting the sortering: first the checked itens, then the display field
            //Example: "Col1;desc,Col2;asc"
            container.DefaultOrdering = "Check_Status;desc,TerritoryDescription;asc";

            frmPopUpSimpleList frmPopUpSimpleList = new frmPopUpSimpleList(container);
            frmPopUpSimpleList.Show();
            frmPopUpSimpleList.ApplyGridSetup();
        }

    }
    }

