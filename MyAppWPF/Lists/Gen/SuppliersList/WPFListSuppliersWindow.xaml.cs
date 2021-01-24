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

    namespace MyApp.WPFList.Suppliers
    {
    
    public partial class ListWPFSuppliers : Page //Window
    {    
        public SuppliersDataContext SuppliersDataContext { get; set; }
        public IWPFSuppliersDataConnection dataConnection;
        public string CurrentLanguage { get; set; }
    
        //Track[0014] Template:WPF_List_Constructor.html

        /// <summary>
        /// Permits navigation into main frame
        /// </summary>
        private Frame FrameMainWindow { get; set; }
        
        private WPFConfig config {get; set;}
        public ListWPFSuppliers(WPFConfig config, bool loadGrid, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            if (config.connectionType == WPFConfig.ConnectionType.REST)
            {
                dataConnection = new WPFSuppliersRest(config);
            }
            else
            {
                dataConnection = new WPFSuppliersDB(config);
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
            ModelNotifiedForSuppliers itemSelected = (ModelNotifiedForSuppliers)DataGridSuppliers.SelectedItem;
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

            MyApp.WPFForms.Suppliers.FormWPFSuppliers page = new MyApp.WPFForms.Suppliers.FormWPFSuppliers(config);
            page.LoadForm(itemSelected.SupplierID);
            page.Setup_SetLanguage(CurrentLanguage);
            ContainerWindowSimple win = new ContainerWindowSimple(page, "Form Suppliers");

            win.Show();
        }

        /// <summary>
        /// Set grid data. Convert List<T> into observable collection
        /// </summary>
        /// <param name="gridData"></param>
        public void SetGridData(List<ModelNotifiedForSuppliers> gridData)
        { 
            //Disable events during grid databind
            this.DataGridSuppliers.SelectionChanged -= OnSelectionChanged;
            
            this.SuppliersDataContext.GridData = new ObservableCollection<ModelNotifiedForSuppliers>(gridData);
            this.DataGridSuppliers.ItemsSource = this.SuppliersDataContext.GridData;
            //Enable events again
            this.DataGridSuppliers.SelectionChanged += OnSelectionChanged; 
        }

        public void LoadGrid(Func<ModelNotifiedForSuppliers, bool> filter = null)
        {
            this.DataGridSuppliers.ItemsSource = null;
            
            //Saving current language
            WPFMessageAndLabelForList currentLanguage = new WPFMessageAndLabelForList();
            if (this.SuppliersDataContext != null)
            { 
                currentLanguage = this.SuppliersDataContext.WPFMessageAndLabelForList;
            }
            string error = null;
            this.SuppliersDataContext = dataConnection.GetDataContext(out error);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }

            //Setting language messages
            this.SuppliersDataContext.WPFMessageAndLabelForList = currentLanguage;
            
            this.DataContext = SuppliersDataContext;            

            List<ModelNotifiedForSuppliers> filteredList;
            if (filter == null)
                filteredList = SuppliersDataContext.modelNotifiedForSuppliersMain;
            else
                filteredList = SuppliersDataContext.modelNotifiedForSuppliersMain.Where(filter).ToList();
            
            //Bind data
            SetGridData(filteredList);            

            //Load detail forms/lists in master/detail
            if (SuppliersDataContext.modelNotifiedForSuppliersMain.Count != 0)
            {
                this.LoadDetail(SuppliersDataContext.modelNotifiedForSuppliersMain[0]);
            }
        }        


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForSuppliers itemSelected = (ModelNotifiedForSuppliers)DataGridSuppliers.SelectedItem;
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
                MessageBox.Show(SuppliersDataContext.WPFMessageAndLabelForList.MessageBoxSaveOK);
            }
            else
            {
                //MessageBox.Show(MessageBoxSaveError + error);
                MessageBox.Show(SuppliersDataContext.WPFMessageAndLabelForList.MessageBoxSaveError + error);
            }
        }


        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            //MessageBoxResult result = MessageBox.Show(MessageBoxDeleteConfirm, MessageBoxDeleteConfirmCaption, button, icon);
            MessageBoxResult result = MessageBox.Show(SuppliersDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirm, 
                SuppliersDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirmCaption, button, icon);
            string error = null;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelNotifiedForSuppliers itemSelected = (ModelNotifiedForSuppliers)DataGridSuppliers.SelectedItem;
                    dataConnection.DeleteData(itemSelected, out error);
                    if (string.IsNullOrEmpty(error))
                    {
                        SuppliersDataContext.modelNotifiedForSuppliersMain.Remove(itemSelected);
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
                MessageBox.Show(SuppliersDataContext.WPFMessageAndLabelForList.MessageBoxDeleteOK);
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
                SetGridData(SuppliersDataContext.modelNotifiedForSuppliersMain);
                this.LoadDetail(SuppliersDataContext.modelNotifiedForSuppliersMain[0]);
                return;
            }
            List<ModelNotifiedForSuppliers> basicFilteredList = FilterGrid(filterValue);
            SetGridData(basicFilteredList);
            if (SuppliersDataContext.modelNotifiedForSuppliersMain.Count != 0)
            {
                this.LoadDetail(SuppliersDataContext.modelNotifiedForSuppliersMain[0]);
            }                
        }        

        private List<ModelNotifiedForSuppliers> FilterGrid(string filterValue)
        {
            filterValue = filterValue.ToLower();
            List<ModelNotifiedForSuppliers> filteredList = new List<ModelNotifiedForSuppliers>();
            foreach (ModelNotifiedForSuppliers item in SuppliersDataContext.modelNotifiedForSuppliersMain)
            {
                if (item.SupplierID.ToString().ToLower().Contains(filterValue))
{ 
filteredList.Add(item);
continue;
}

//Filter string values.
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

if (item.HomePage != null)
{
    if (item.HomePage.ToLower().Contains(filterValue))
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

