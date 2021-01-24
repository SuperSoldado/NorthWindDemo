    //Track[0015] Template:WPF_List_Main.html
using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using MyAppWPF.Containers;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;
using MyAppWPFLib;

    namespace MyApp.WPFList.CustomerDemographics
    {
    
    public partial class ListWPFCustomerDemographics : Page //Window
    {    
        public CustomerDemographicsDataContext CustomerDemographicsDataContext { get; set; }
        public IWPFCustomerDemographicsDataConnection dataConnection;
        public string CurrentLanguage { get; set; }
    
        //Track[0014] Template:WPF_List_Constructor.html

        /// <summary>
        /// Permits navigation into main frame
        /// </summary>
        private Frame FrameMainWindow { get; set; }
        
        private WPFConfig config {get; set;}
        public ListWPFCustomerDemographics(WPFConfig config, bool loadGrid, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            if (config.connectionType == WPFConfig.ConnectionType.REST)
            {
                dataConnection = new WPFCustomerDemographicsRest(config);
            }
            else
            {
                dataConnection = new WPFCustomerDemographicsDB(config);
            } 
            InitializeComponent();
            if(loadGrid)
            {
                LoadGrid();
            }            
            txtFilter.KeyDown += new KeyEventHandler(btnFilterKeyDown);            
        }       
        private void OpenFormClick(object sender, RoutedEventArgs e)
        {            
            ModelNotifiedForCustomerDemographics itemSelected = (ModelNotifiedForCustomerDemographics)DataGridCustomerDemographics.SelectedItem;
            if (itemSelected == null)
            {
                return;
            }
            
            //Uncomment this line to allow navigation 
            //this.FrameMainWindow.Navigate(this.DetailListGeoCities);

            /* COMPILE ERROR WARNING!!! If this line crash: 
             * a) Generate the FORM version of this list.
             * b) Re-generate the LIST without the "OpenForm" feature.#
             * c) Remove this chunk of code*/

            MyApp.WPFForms.CustomerDemographics.FormWPFCustomerDemographics page = new MyApp.WPFForms.CustomerDemographics.FormWPFCustomerDemographics(config);
            page.LoadForm(itemSelected.CustomerTypeID);
            page.Setup_SetLanguage(CurrentLanguage);
            ContainerWindowSimple win = new ContainerWindowSimple(page, "Form CustomerDemographics");

            win.Show();
        }

        /// <summary>
        /// Set grid data. Convert List<T> into observable collection
        /// </summary>
        /// <param name="gridData"></param>
        public void SetGridData(List<ModelNotifiedForCustomerDemographics> gridData)
        { 
            //Disable events during grid databind
            this.DataGridCustomerDemographics.SelectionChanged -= OnSelectionChanged;
            
            this.CustomerDemographicsDataContext.GridData = new ObservableCollection<ModelNotifiedForCustomerDemographics>(gridData);
            this.DataGridCustomerDemographics.ItemsSource = this.CustomerDemographicsDataContext.GridData;
            //Enable events again
            this.DataGridCustomerDemographics.SelectionChanged += OnSelectionChanged; 
        }

        public void LoadGrid(Func<ModelNotifiedForCustomerDemographics, bool> filter = null)
        {
            this.DataGridCustomerDemographics.ItemsSource = null;
            
            //Saving current language
            WPFMessageAndLabelForList currentLanguage = new WPFMessageAndLabelForList();
            if (this.CustomerDemographicsDataContext != null)
            { 
                currentLanguage = this.CustomerDemographicsDataContext.WPFMessageAndLabelForList;
            }
            string error = null;
            this.CustomerDemographicsDataContext = dataConnection.GetDataContext(out error);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }

            //Setting language messages
            this.CustomerDemographicsDataContext.WPFMessageAndLabelForList = currentLanguage;
            
            this.DataContext = CustomerDemographicsDataContext;            

            List<ModelNotifiedForCustomerDemographics> filteredList;
            if (filter == null)
                filteredList = CustomerDemographicsDataContext.modelNotifiedForCustomerDemographicsMain;
            else
                filteredList = CustomerDemographicsDataContext.modelNotifiedForCustomerDemographicsMain.Where(filter).ToList();
            
            //Bind data
            SetGridData(filteredList);            

            //Load detail forms/lists in master/detail
            if (CustomerDemographicsDataContext.modelNotifiedForCustomerDemographicsMain.Count != 0)
            {
                this.LoadDetail(CustomerDemographicsDataContext.modelNotifiedForCustomerDemographicsMain[0]);
            }
        }        


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForCustomerDemographics itemSelected = (ModelNotifiedForCustomerDemographics)DataGridCustomerDemographics.SelectedItem;
            if (itemSelected == null)
            {
                return;
            }
                
            string error = null;
            if (itemSelected.NewItem)
            {
                dataConnection.AddData(itemSelected, out error);

                if (error == null)
                {
                    btnReload_Click(null, null);
                }
            }
            else
            {
                dataConnection.SaveData(itemSelected, out error);    
            }

            if (error == null)
            {
                //MessageBox.Show(MessageBoxSaveOK);
                MessageBox.Show(CustomerDemographicsDataContext.WPFMessageAndLabelForList.MessageBoxSaveOK);
            }
            else
            {
                //MessageBox.Show(MessageBoxSaveError + error);
                MessageBox.Show(CustomerDemographicsDataContext.WPFMessageAndLabelForList.MessageBoxSaveError + error);
            }
        }


        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            //MessageBoxResult result = MessageBox.Show(MessageBoxDeleteConfirm, MessageBoxDeleteConfirmCaption, button, icon);
            MessageBoxResult result = MessageBox.Show(CustomerDemographicsDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirm, 
                CustomerDemographicsDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirmCaption, button, icon);
            string error = null;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelNotifiedForCustomerDemographics itemSelected = (ModelNotifiedForCustomerDemographics)DataGridCustomerDemographics.SelectedItem;
                    dataConnection.DeleteData(itemSelected, out error);
                    if (string.IsNullOrEmpty(error))
                    {
                        CustomerDemographicsDataContext.modelNotifiedForCustomerDemographicsMain.Remove(itemSelected);
                    }
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
                //MessageBox.Show(MessageBoxDeleteOK);
                MessageBox.Show(CustomerDemographicsDataContext.WPFMessageAndLabelForList.MessageBoxDeleteOK);
                btnReload_Click(null, null);
            }
        }

        
        public void btnReload_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }
        
        public void btnFilterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnFilter_Click(btnFilter, null);
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            string button = (sender as Button).Name.ToString();
            string filterValue = txtFilter.Text;
            if (button == "btnClearFilter")
            {                
                txtFilter.Text = "";
                SetGridData(CustomerDemographicsDataContext.modelNotifiedForCustomerDemographicsMain);
                this.LoadDetail(CustomerDemographicsDataContext.modelNotifiedForCustomerDemographicsMain[0]);
                return;
            }
            List<ModelNotifiedForCustomerDemographics> basicFilteredList = FilterGrid(filterValue);
            SetGridData(basicFilteredList);
            if (CustomerDemographicsDataContext.modelNotifiedForCustomerDemographicsMain.Count != 0)
            {
                this.LoadDetail(CustomerDemographicsDataContext.modelNotifiedForCustomerDemographicsMain[0]);
            }                
        }        

        private List<ModelNotifiedForCustomerDemographics> FilterGrid(string filterValue)
        {
            filterValue = filterValue.ToLower();
            List<ModelNotifiedForCustomerDemographics> filteredList = new List<ModelNotifiedForCustomerDemographics>();
            foreach (ModelNotifiedForCustomerDemographics item in CustomerDemographicsDataContext.modelNotifiedForCustomerDemographicsMain)
            {
                if (item.CustomerTypeID.ToString().ToLower().Contains(filterValue))
{ 
filteredList.Add(item);
continue;
}

//Filter string values.
if (item.CustomerTypeID != null)
{
    if (item.CustomerTypeID.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.CustomerDesc != null)
{
    if (item.CustomerDesc.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}


            }
            return filteredList;
        }
    



    }
    }

