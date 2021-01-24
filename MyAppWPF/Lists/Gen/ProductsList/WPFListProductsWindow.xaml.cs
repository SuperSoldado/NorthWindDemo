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

    namespace MyApp.WPFList.Products
    {
    
    public partial class ListWPFProducts : Page //Window
    {    
        public ProductsDataContext ProductsDataContext { get; set; }
        public IWPFProductsDataConnection dataConnection;
        public string CurrentLanguage { get; set; }
    
        //Track[0014] Template:WPF_List_Constructor.html

        /// <summary>
        /// Permits navigation into main frame
        /// </summary>
        private Frame FrameMainWindow { get; set; }
        
        private WPFConfig config {get; set;}
        public ListWPFProducts(WPFConfig config, bool loadGrid, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            if (config.connectionType == WPFConfig.ConnectionType.REST)
            {
                dataConnection = new WPFProductsRest(config);
            }
            else
            {
                dataConnection = new WPFProductsDB(config);
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
            ModelNotifiedForProducts itemSelected = (ModelNotifiedForProducts)DataGridProducts.SelectedItem;
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

            MyApp.WPFForms.Products.FormWPFProducts page = new MyApp.WPFForms.Products.FormWPFProducts(config);
            page.LoadForm(itemSelected.ProductID);
            page.Setup_SetLanguage(CurrentLanguage);
            ContainerWindowSimple win = new ContainerWindowSimple(page, "Form Products");

            win.Show();
        }

        /// <summary>
        /// Set grid data. Convert List<T> into observable collection
        /// </summary>
        /// <param name="gridData"></param>
        public void SetGridData(List<ModelNotifiedForProducts> gridData)
        { 
            //Disable events during grid databind
            this.DataGridProducts.SelectionChanged -= OnSelectionChanged;
            
            this.ProductsDataContext.GridData = new ObservableCollection<ModelNotifiedForProducts>(gridData);
            this.DataGridProducts.ItemsSource = this.ProductsDataContext.GridData;
            //Enable events again
            this.DataGridProducts.SelectionChanged += OnSelectionChanged; 
        }

        public void LoadGrid(Func<ModelNotifiedForProducts, bool> filter = null)
        {
            this.DataGridProducts.ItemsSource = null;
            
            //Saving current language
            WPFMessageAndLabelForList currentLanguage = new WPFMessageAndLabelForList();
            if (this.ProductsDataContext != null)
            { 
                currentLanguage = this.ProductsDataContext.WPFMessageAndLabelForList;
            }
            string error = null;
            this.ProductsDataContext = dataConnection.GetDataContext(out error);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }

            //Setting language messages
            this.ProductsDataContext.WPFMessageAndLabelForList = currentLanguage;
            
            this.DataContext = ProductsDataContext;            

            List<ModelNotifiedForProducts> filteredList;
            if (filter == null)
                filteredList = ProductsDataContext.modelNotifiedForProductsMain;
            else
                filteredList = ProductsDataContext.modelNotifiedForProductsMain.Where(filter).ToList();
            
            //Bind data
            SetGridData(filteredList);            

            //Load detail forms/lists in master/detail
            if (ProductsDataContext.modelNotifiedForProductsMain.Count != 0)
            {
                this.LoadDetail(ProductsDataContext.modelNotifiedForProductsMain[0]);
            }
        }        


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForProducts itemSelected = (ModelNotifiedForProducts)DataGridProducts.SelectedItem;
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
                MessageBox.Show(ProductsDataContext.WPFMessageAndLabelForList.MessageBoxSaveOK);
            }
            else
            {
                //MessageBox.Show(MessageBoxSaveError + error);
                MessageBox.Show(ProductsDataContext.WPFMessageAndLabelForList.MessageBoxSaveError + error);
            }
        }


        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            //MessageBoxResult result = MessageBox.Show(MessageBoxDeleteConfirm, MessageBoxDeleteConfirmCaption, button, icon);
            MessageBoxResult result = MessageBox.Show(ProductsDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirm, 
                ProductsDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirmCaption, button, icon);
            string error = null;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelNotifiedForProducts itemSelected = (ModelNotifiedForProducts)DataGridProducts.SelectedItem;
                    dataConnection.DeleteData(itemSelected, out error);
                    if (string.IsNullOrEmpty(error))
                    {
                        ProductsDataContext.modelNotifiedForProductsMain.Remove(itemSelected);
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
                MessageBox.Show(ProductsDataContext.WPFMessageAndLabelForList.MessageBoxDeleteOK);
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
                SetGridData(ProductsDataContext.modelNotifiedForProductsMain);
                this.LoadDetail(ProductsDataContext.modelNotifiedForProductsMain[0]);
                return;
            }
            List<ModelNotifiedForProducts> basicFilteredList = FilterGrid(filterValue);
            SetGridData(basicFilteredList);
            if (ProductsDataContext.modelNotifiedForProductsMain.Count != 0)
            {
                this.LoadDetail(ProductsDataContext.modelNotifiedForProductsMain[0]);
            }                
        }        

        private List<ModelNotifiedForProducts> FilterGrid(string filterValue)
        {
            filterValue = filterValue.ToLower();
            List<ModelNotifiedForProducts> filteredList = new List<ModelNotifiedForProducts>();
            foreach (ModelNotifiedForProducts item in ProductsDataContext.modelNotifiedForProductsMain)
            {
                if (item.ProductID.ToString().ToLower().Contains(filterValue))
{ 
filteredList.Add(item);
continue;
}

//Filter string values.
if (item.ProductName != null)
{
    if (item.ProductName.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.QuantityPerUnit != null)
{
    if (item.QuantityPerUnit.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

//Filter FK values.
if (item.SupplierID != null)
{
    ModelNotifiedForSuppliers comboItem = ProductsDataContext.modelNotifiedForSuppliers.Where(x => x.SupplierID == item.SupplierID).FirstOrDefault();
    if ((comboItem != null) && (comboItem.CompanyName != null) && (comboItem.CompanyName.ToLower().Contains(filterValue)))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.CategoryID != null)
{
    ModelNotifiedForCategories comboItem = ProductsDataContext.modelNotifiedForCategories.Where(x => x.CategoryID == item.CategoryID).FirstOrDefault();
    if ((comboItem != null) && (comboItem.CategoryName != null) && (comboItem.CategoryName.ToLower().Contains(filterValue)))
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

