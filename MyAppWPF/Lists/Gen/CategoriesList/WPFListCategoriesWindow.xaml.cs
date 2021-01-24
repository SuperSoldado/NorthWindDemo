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

    namespace MyApp.WPFList.Categories
    {
    
    public partial class ListWPFCategories : Page //Window
    {    
        public CategoriesDataContext CategoriesDataContext { get; set; }
        public IWPFCategoriesDataConnection dataConnection;
        public string CurrentLanguage { get; set; }
    
        //Track[0014] Template:WPF_List_Constructor.html

        /// <summary>
        /// Permits navigation into main frame
        /// </summary>
        private Frame FrameMainWindow { get; set; }
        
        private WPFConfig config {get; set;}
        public ListWPFCategories(WPFConfig config, bool loadGrid, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            if (config.connectionType == WPFConfig.ConnectionType.REST)
            {
                dataConnection = new WPFCategoriesRest(config);
            }
            else
            {
                dataConnection = new WPFCategoriesDB(config);
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
            ModelNotifiedForCategories itemSelected = (ModelNotifiedForCategories)DataGridCategories.SelectedItem;
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

            MyApp.WPFForms.Categories.FormWPFCategories page = new MyApp.WPFForms.Categories.FormWPFCategories(config);
            page.LoadForm(itemSelected.CategoryID);
            page.Setup_SetLanguage(CurrentLanguage);
            ContainerWindowSimple win = new ContainerWindowSimple(page, "Form Categories");

            win.Show();
        }

        /// <summary>
        /// Set grid data. Convert List<T> into observable collection
        /// </summary>
        /// <param name="gridData"></param>
        public void SetGridData(List<ModelNotifiedForCategories> gridData)
        { 
            //Disable events during grid databind
            this.DataGridCategories.SelectionChanged -= OnSelectionChanged;
            
            this.CategoriesDataContext.GridData = new ObservableCollection<ModelNotifiedForCategories>(gridData);
            this.DataGridCategories.ItemsSource = this.CategoriesDataContext.GridData;
            //Enable events again
            this.DataGridCategories.SelectionChanged += OnSelectionChanged; 
        }

        public void LoadGrid(Func<ModelNotifiedForCategories, bool> filter = null)
        {
            this.DataGridCategories.ItemsSource = null;
            
            //Saving current language
            WPFMessageAndLabelForList currentLanguage = new WPFMessageAndLabelForList();
            if (this.CategoriesDataContext != null)
            { 
                currentLanguage = this.CategoriesDataContext.WPFMessageAndLabelForList;
            }
            string error = null;
            this.CategoriesDataContext = dataConnection.GetDataContext(out error);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }

            //Setting language messages
            this.CategoriesDataContext.WPFMessageAndLabelForList = currentLanguage;
            
            this.DataContext = CategoriesDataContext;            

            List<ModelNotifiedForCategories> filteredList;
            if (filter == null)
                filteredList = CategoriesDataContext.modelNotifiedForCategoriesMain;
            else
                filteredList = CategoriesDataContext.modelNotifiedForCategoriesMain.Where(filter).ToList();
            
            //Bind data
            SetGridData(filteredList);            

            //Load detail forms/lists in master/detail
            if (CategoriesDataContext.modelNotifiedForCategoriesMain.Count != 0)
            {
                this.LoadDetail(CategoriesDataContext.modelNotifiedForCategoriesMain[0]);
            }
        }        


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForCategories itemSelected = (ModelNotifiedForCategories)DataGridCategories.SelectedItem;
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
                MessageBox.Show(CategoriesDataContext.WPFMessageAndLabelForList.MessageBoxSaveOK);
            }
            else
            {
                //MessageBox.Show(MessageBoxSaveError + error);
                MessageBox.Show(CategoriesDataContext.WPFMessageAndLabelForList.MessageBoxSaveError + error);
            }
        }


        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            //MessageBoxResult result = MessageBox.Show(MessageBoxDeleteConfirm, MessageBoxDeleteConfirmCaption, button, icon);
            MessageBoxResult result = MessageBox.Show(CategoriesDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirm, 
                CategoriesDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirmCaption, button, icon);
            string error = null;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelNotifiedForCategories itemSelected = (ModelNotifiedForCategories)DataGridCategories.SelectedItem;
                    dataConnection.DeleteData(itemSelected, out error);
                    if (string.IsNullOrEmpty(error))
                    {
                        CategoriesDataContext.modelNotifiedForCategoriesMain.Remove(itemSelected);
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
                MessageBox.Show(CategoriesDataContext.WPFMessageAndLabelForList.MessageBoxDeleteOK);
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
                SetGridData(CategoriesDataContext.modelNotifiedForCategoriesMain);
                this.LoadDetail(CategoriesDataContext.modelNotifiedForCategoriesMain[0]);
                return;
            }
            List<ModelNotifiedForCategories> basicFilteredList = FilterGrid(filterValue);
            SetGridData(basicFilteredList);
            if (CategoriesDataContext.modelNotifiedForCategoriesMain.Count != 0)
            {
                this.LoadDetail(CategoriesDataContext.modelNotifiedForCategoriesMain[0]);
            }                
        }        

        private List<ModelNotifiedForCategories> FilterGrid(string filterValue)
        {
            filterValue = filterValue.ToLower();
            List<ModelNotifiedForCategories> filteredList = new List<ModelNotifiedForCategories>();
            foreach (ModelNotifiedForCategories item in CategoriesDataContext.modelNotifiedForCategoriesMain)
            {
                if (item.CategoryID.ToString().ToLower().Contains(filterValue))
{ 
filteredList.Add(item);
continue;
}

//Filter string values.
if (item.CategoryName != null)
{
    if (item.CategoryName.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.Description != null)
{
    if (item.Description.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}


            }
            return filteredList;
        }
    
        private void OnMouseUpBinary(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            ModelNotifiedForCategories itemSelected = (ModelNotifiedForCategories)DataGridCategories.SelectedItem;
            if (image.Tag.ToString().ToLower() == "Picture".ToLower())
            {
                
                frmDisplayBinaryData frmDisplayBinaryData = new frmDisplayBinaryData(itemSelected.Picture, GlobalEnums.MimeTypes.Image);
                frmDisplayBinaryData.ShowDialog();
                //frmDisplayBinaryData.Owner = ToDo. Without owner, alt tab can bug
                itemSelected.Picture = frmDisplayBinaryData.myViewModel.BinData;
                return;            
            }
            MessageBox.Show("Error: 'btnSaveBinAs' does not contains the tag reference to target binary column.");
        }

        private void btnAddPicture_Click(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForCategories itemSelected = (ModelNotifiedForCategories)DataGridCategories.SelectedItem;
            frmDisplayBinaryData frmDisplayBinaryData = new frmDisplayBinaryData(itemSelected.Picture, GlobalEnums.MimeTypes.Image);
            frmDisplayBinaryData.ShowDialog();
            //frmDisplayBinaryData.Owner = ToDo. Without owner, alt tab can bug
            itemSelected.Picture = frmDisplayBinaryData.myViewModel.BinData;
            itemSelected.BtnAddPictureVisibility = Visibility.Collapsed;
            itemSelected.BtnExcludePictureVisibility = Visibility.Visible;
        }

        private void btnExcludePicture_Click(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForCategories itemSelected = (ModelNotifiedForCategories)DataGridCategories.SelectedItem;
            itemSelected.Picture = null;
            itemSelected.BtnAddPictureVisibility = Visibility.Visible;
            itemSelected.BtnExcludePictureVisibility = Visibility.Collapsed;
        }


    }
    }

