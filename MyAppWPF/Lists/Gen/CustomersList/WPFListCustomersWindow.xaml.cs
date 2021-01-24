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

    namespace MyApp.WPFList.Customers
    {
    
    public partial class ListWPFCustomers : Page //Window
    {    
        public CustomersDataContext CustomersDataContext { get; set; }
        public IWPFCustomersDataConnection dataConnection;
        public string CurrentLanguage { get; set; }
    
        //Track[0014] Template:WPF_List_Constructor.html

        /// <summary>
        /// Permits navigation into main frame
        /// </summary>
        private Frame FrameMainWindow { get; set; }
        
        private WPFConfig config {get; set;}
        public ListWPFCustomers(WPFConfig config, bool loadGrid, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            if (config.connectionType == WPFConfig.ConnectionType.REST)
            {
                dataConnection = new WPFCustomersRest(config);
            }
            else
            {
                dataConnection = new WPFCustomersDB(config);
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
            ModelNotifiedForCustomers itemSelected = (ModelNotifiedForCustomers)DataGridCustomers.SelectedItem;
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

            MyApp.WPFForms.Customers.FormWPFCustomers page = new MyApp.WPFForms.Customers.FormWPFCustomers(config);
            page.LoadForm(itemSelected.CustomerID);
            page.Setup_SetLanguage(CurrentLanguage);
            ContainerWindowSimple win = new ContainerWindowSimple(page, "Form Customers");

            win.Show();
        }

        /// <summary>
        /// Set grid data. Convert List<T> into observable collection
        /// </summary>
        /// <param name="gridData"></param>
        public void SetGridData(List<ModelNotifiedForCustomers> gridData)
        { 
            //Disable events during grid databind
            this.DataGridCustomers.SelectionChanged -= OnSelectionChanged;
            
            this.CustomersDataContext.GridData = new ObservableCollection<ModelNotifiedForCustomers>(gridData);
            this.DataGridCustomers.ItemsSource = this.CustomersDataContext.GridData;
            //Enable events again
            this.DataGridCustomers.SelectionChanged += OnSelectionChanged; 
        }

        public void LoadGrid(Func<ModelNotifiedForCustomers, bool> filter = null)
        {
            this.DataGridCustomers.ItemsSource = null;
            
            //Saving current language
            WPFMessageAndLabelForList currentLanguage = new WPFMessageAndLabelForList();
            if (this.CustomersDataContext != null)
            { 
                currentLanguage = this.CustomersDataContext.WPFMessageAndLabelForList;
            }
            string error = null;
            this.CustomersDataContext = dataConnection.GetDataContext(out error);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }

            //Setting language messages
            this.CustomersDataContext.WPFMessageAndLabelForList = currentLanguage;
            
            this.DataContext = CustomersDataContext;            

            List<ModelNotifiedForCustomers> filteredList;
            if (filter == null)
                filteredList = CustomersDataContext.modelNotifiedForCustomersMain;
            else
                filteredList = CustomersDataContext.modelNotifiedForCustomersMain.Where(filter).ToList();
            
            //Bind data
            SetGridData(filteredList);            

            //Load detail forms/lists in master/detail
            if (CustomersDataContext.modelNotifiedForCustomersMain.Count != 0)
            {
                this.LoadDetail(CustomersDataContext.modelNotifiedForCustomersMain[0]);
            }
        }        


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForCustomers itemSelected = (ModelNotifiedForCustomers)DataGridCustomers.SelectedItem;
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
                MessageBox.Show(CustomersDataContext.WPFMessageAndLabelForList.MessageBoxSaveOK);
            }
            else
            {
                //MessageBox.Show(MessageBoxSaveError + error);
                MessageBox.Show(CustomersDataContext.WPFMessageAndLabelForList.MessageBoxSaveError + error);
            }
        }


        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            //MessageBoxResult result = MessageBox.Show(MessageBoxDeleteConfirm, MessageBoxDeleteConfirmCaption, button, icon);
            MessageBoxResult result = MessageBox.Show(CustomersDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirm, 
                CustomersDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirmCaption, button, icon);
            string error = null;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelNotifiedForCustomers itemSelected = (ModelNotifiedForCustomers)DataGridCustomers.SelectedItem;
                    dataConnection.DeleteData(itemSelected, out error);
                    if (string.IsNullOrEmpty(error))
                    {
                        CustomersDataContext.modelNotifiedForCustomersMain.Remove(itemSelected);
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
                MessageBox.Show(CustomersDataContext.WPFMessageAndLabelForList.MessageBoxDeleteOK);
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
                SetGridData(CustomersDataContext.modelNotifiedForCustomersMain);
                this.LoadDetail(CustomersDataContext.modelNotifiedForCustomersMain[0]);
                return;
            }
            List<ModelNotifiedForCustomers> basicFilteredList = FilterGrid(filterValue);
            SetGridData(basicFilteredList);
            if (CustomersDataContext.modelNotifiedForCustomersMain.Count != 0)
            {
                this.LoadDetail(CustomersDataContext.modelNotifiedForCustomersMain[0]);
            }                
        }        

        private List<ModelNotifiedForCustomers> FilterGrid(string filterValue)
        {
            filterValue = filterValue.ToLower();
            List<ModelNotifiedForCustomers> filteredList = new List<ModelNotifiedForCustomers>();
            foreach (ModelNotifiedForCustomers item in CustomersDataContext.modelNotifiedForCustomersMain)
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

if (item.CompanyName != null)
{
    if (item.CompanyName.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.ContactName != null)
{
    if (item.ContactName.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.ContactTitle != null)
{
    if (item.ContactTitle.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.Address != null)
{
    if (item.Address.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.City != null)
{
    if (item.City.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.Region != null)
{
    if (item.Region.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.PostalCode != null)
{
    if (item.PostalCode.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.Country != null)
{
    if (item.Country.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.Phone != null)
{
    if (item.Phone.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.Fax != null)
{
    if (item.Fax.ToLower().Contains(filterValue))
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

