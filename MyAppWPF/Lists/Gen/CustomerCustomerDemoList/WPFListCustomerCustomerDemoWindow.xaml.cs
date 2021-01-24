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

    namespace MyApp.WPFList.CustomerCustomerDemo
    {
    
    public partial class ListWPFCustomerCustomerDemo : Page //Window
    {    
        public CustomerCustomerDemoDataContext CustomerCustomerDemoDataContext { get; set; }
        public IWPFCustomerCustomerDemoDataConnection dataConnection;
        public string CurrentLanguage { get; set; }
    
        //Track[0014] Template:WPF_List_Constructor.html

        /// <summary>
        /// Permits navigation into main frame
        /// </summary>
        private Frame FrameMainWindow { get; set; }
        
        private WPFConfig config {get; set;}
        public ListWPFCustomerCustomerDemo(WPFConfig config, bool loadGrid, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            if (config.connectionType == WPFConfig.ConnectionType.REST)
            {
                dataConnection = new WPFCustomerCustomerDemoRest(config);
            }
            else
            {
                dataConnection = new WPFCustomerCustomerDemoDB(config);
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
            ModelNotifiedForCustomerCustomerDemo itemSelected = (ModelNotifiedForCustomerCustomerDemo)DataGridCustomerCustomerDemo.SelectedItem;
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

            MyApp.WPFForms.CustomerCustomerDemo.FormWPFCustomerCustomerDemo page = new MyApp.WPFForms.CustomerCustomerDemo.FormWPFCustomerCustomerDemo(config);
            page.LoadForm(itemSelected.CustomerID,itemSelected.CustomerTypeID);
            page.Setup_SetLanguage(CurrentLanguage);
            ContainerWindowSimple win = new ContainerWindowSimple(page, "Form CustomerCustomerDemo");

            win.Show();
        }

        /// <summary>
        /// Set grid data. Convert List<T> into observable collection
        /// </summary>
        /// <param name="gridData"></param>
        public void SetGridData(List<ModelNotifiedForCustomerCustomerDemo> gridData)
        { 
            //Disable events during grid databind
            this.DataGridCustomerCustomerDemo.SelectionChanged -= OnSelectionChanged;
            
            this.CustomerCustomerDemoDataContext.GridData = new ObservableCollection<ModelNotifiedForCustomerCustomerDemo>(gridData);
            this.DataGridCustomerCustomerDemo.ItemsSource = this.CustomerCustomerDemoDataContext.GridData;
            //Enable events again
            this.DataGridCustomerCustomerDemo.SelectionChanged += OnSelectionChanged; 
        }

        public void LoadGrid(Func<ModelNotifiedForCustomerCustomerDemo, bool> filter = null)
        {
            this.DataGridCustomerCustomerDemo.ItemsSource = null;
            
            //Saving current language
            WPFMessageAndLabelForList currentLanguage = new WPFMessageAndLabelForList();
            if (this.CustomerCustomerDemoDataContext != null)
            { 
                currentLanguage = this.CustomerCustomerDemoDataContext.WPFMessageAndLabelForList;
            }
            string error = null;
            this.CustomerCustomerDemoDataContext = dataConnection.GetDataContext(out error);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }

            //Setting language messages
            this.CustomerCustomerDemoDataContext.WPFMessageAndLabelForList = currentLanguage;
            
            this.DataContext = CustomerCustomerDemoDataContext;            

            List<ModelNotifiedForCustomerCustomerDemo> filteredList;
            if (filter == null)
                filteredList = CustomerCustomerDemoDataContext.modelNotifiedForCustomerCustomerDemoMain;
            else
                filteredList = CustomerCustomerDemoDataContext.modelNotifiedForCustomerCustomerDemoMain.Where(filter).ToList();
            
            //Bind data
            SetGridData(filteredList);            

            //Load detail forms/lists in master/detail
            if (CustomerCustomerDemoDataContext.modelNotifiedForCustomerCustomerDemoMain.Count != 0)
            {
                this.LoadDetail(CustomerCustomerDemoDataContext.modelNotifiedForCustomerCustomerDemoMain[0]);
            }
        }        


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForCustomerCustomerDemo itemSelected = (ModelNotifiedForCustomerCustomerDemo)DataGridCustomerCustomerDemo.SelectedItem;
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
                MessageBox.Show(CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.MessageBoxSaveOK);
            }
            else
            {
                //MessageBox.Show(MessageBoxSaveError + error);
                MessageBox.Show(CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.MessageBoxSaveError + error);
            }
        }


        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            //MessageBoxResult result = MessageBox.Show(MessageBoxDeleteConfirm, MessageBoxDeleteConfirmCaption, button, icon);
            MessageBoxResult result = MessageBox.Show(CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirm, 
                CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirmCaption, button, icon);
            string error = null;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelNotifiedForCustomerCustomerDemo itemSelected = (ModelNotifiedForCustomerCustomerDemo)DataGridCustomerCustomerDemo.SelectedItem;
                    dataConnection.DeleteData(itemSelected, out error);
                    if (string.IsNullOrEmpty(error))
                    {
                        CustomerCustomerDemoDataContext.modelNotifiedForCustomerCustomerDemoMain.Remove(itemSelected);
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
                MessageBox.Show(CustomerCustomerDemoDataContext.WPFMessageAndLabelForList.MessageBoxDeleteOK);
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
                SetGridData(CustomerCustomerDemoDataContext.modelNotifiedForCustomerCustomerDemoMain);
                this.LoadDetail(CustomerCustomerDemoDataContext.modelNotifiedForCustomerCustomerDemoMain[0]);
                return;
            }
            List<ModelNotifiedForCustomerCustomerDemo> basicFilteredList = FilterGrid(filterValue);
            SetGridData(basicFilteredList);
            if (CustomerCustomerDemoDataContext.modelNotifiedForCustomerCustomerDemoMain.Count != 0)
            {
                this.LoadDetail(CustomerCustomerDemoDataContext.modelNotifiedForCustomerCustomerDemoMain[0]);
            }                
        }        

        private List<ModelNotifiedForCustomerCustomerDemo> FilterGrid(string filterValue)
        {
            filterValue = filterValue.ToLower();
            List<ModelNotifiedForCustomerCustomerDemo> filteredList = new List<ModelNotifiedForCustomerCustomerDemo>();
            foreach (ModelNotifiedForCustomerCustomerDemo item in CustomerCustomerDemoDataContext.modelNotifiedForCustomerCustomerDemoMain)
            {
                if (item.CustomerID.ToString().ToLower().Contains(filterValue))
{ 
filteredList.Add(item);
continue;
}

//Filter string values.
if (item.CustomerID != null)
{
    if (item.CustomerID.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.CustomerTypeID.ToString().ToLower().Contains(filterValue))
{ 
filteredList.Add(item);
continue;
}

if (item.CustomerTypeID != null)
{
    if (item.CustomerTypeID.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

//Filter FK values.
if (item.CustomerID != null)
{
    ModelNotifiedForCustomers comboItem = CustomerCustomerDemoDataContext.modelNotifiedForCustomers.Where(x => x.CustomerID == item.CustomerID).FirstOrDefault();
    if ((comboItem != null) && (comboItem.CompanyName != null) && (comboItem.CompanyName.ToLower().Contains(filterValue)))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.CustomerTypeID != null)
{
    ModelNotifiedForCustomerDemographics comboItem = CustomerCustomerDemoDataContext.modelNotifiedForCustomerDemographics.Where(x => x.CustomerTypeID == item.CustomerTypeID).FirstOrDefault();
    if ((comboItem != null) && (comboItem.CustomerTypeID != null) && (comboItem.CustomerTypeID.ToLower().Contains(filterValue)))
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

