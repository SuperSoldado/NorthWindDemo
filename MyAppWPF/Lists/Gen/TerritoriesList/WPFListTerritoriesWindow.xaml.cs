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

    namespace MyApp.WPFList.Territories
    {
    
    public partial class ListWPFTerritories : Page //Window
    {    
        public TerritoriesDataContext TerritoriesDataContext { get; set; }
        public IWPFTerritoriesDataConnection dataConnection;
        public string CurrentLanguage { get; set; }
    
        //Track[0014] Template:WPF_List_Constructor.html

        /// <summary>
        /// Permits navigation into main frame
        /// </summary>
        private Frame FrameMainWindow { get; set; }
        
        private WPFConfig config {get; set;}
        public ListWPFTerritories(WPFConfig config, bool loadGrid, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            if (config.connectionType == WPFConfig.ConnectionType.REST)
            {
                dataConnection = new WPFTerritoriesRest(config);
            }
            else
            {
                dataConnection = new WPFTerritoriesDB(config);
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
            ModelNotifiedForTerritories itemSelected = (ModelNotifiedForTerritories)DataGridTerritories.SelectedItem;
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

            MyApp.WPFForms.Territories.FormWPFTerritories page = new MyApp.WPFForms.Territories.FormWPFTerritories(config);
            page.LoadForm(itemSelected.TerritoryID);
            page.Setup_SetLanguage(CurrentLanguage);
            ContainerWindowSimple win = new ContainerWindowSimple(page, "Form Territories");

            win.Show();
        }

        /// <summary>
        /// Set grid data. Convert List<T> into observable collection
        /// </summary>
        /// <param name="gridData"></param>
        public void SetGridData(List<ModelNotifiedForTerritories> gridData)
        { 
            //Disable events during grid databind
            this.DataGridTerritories.SelectionChanged -= OnSelectionChanged;
            
            this.TerritoriesDataContext.GridData = new ObservableCollection<ModelNotifiedForTerritories>(gridData);
            this.DataGridTerritories.ItemsSource = this.TerritoriesDataContext.GridData;
            //Enable events again
            this.DataGridTerritories.SelectionChanged += OnSelectionChanged; 
        }

        public void LoadGrid(Func<ModelNotifiedForTerritories, bool> filter = null)
        {
            this.DataGridTerritories.ItemsSource = null;
            
            //Saving current language
            WPFMessageAndLabelForList currentLanguage = new WPFMessageAndLabelForList();
            if (this.TerritoriesDataContext != null)
            { 
                currentLanguage = this.TerritoriesDataContext.WPFMessageAndLabelForList;
            }
            string error = null;
            this.TerritoriesDataContext = dataConnection.GetDataContext(out error);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }

            //Setting language messages
            this.TerritoriesDataContext.WPFMessageAndLabelForList = currentLanguage;
            
            this.DataContext = TerritoriesDataContext;            

            List<ModelNotifiedForTerritories> filteredList;
            if (filter == null)
                filteredList = TerritoriesDataContext.modelNotifiedForTerritoriesMain;
            else
                filteredList = TerritoriesDataContext.modelNotifiedForTerritoriesMain.Where(filter).ToList();
            
            //Bind data
            SetGridData(filteredList);            

            //Load detail forms/lists in master/detail
            if (TerritoriesDataContext.modelNotifiedForTerritoriesMain.Count != 0)
            {
                this.LoadDetail(TerritoriesDataContext.modelNotifiedForTerritoriesMain[0]);
            }
        }        


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForTerritories itemSelected = (ModelNotifiedForTerritories)DataGridTerritories.SelectedItem;
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
                MessageBox.Show(TerritoriesDataContext.WPFMessageAndLabelForList.MessageBoxSaveOK);
            }
            else
            {
                //MessageBox.Show(MessageBoxSaveError + error);
                MessageBox.Show(TerritoriesDataContext.WPFMessageAndLabelForList.MessageBoxSaveError + error);
            }
        }


        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            //MessageBoxResult result = MessageBox.Show(MessageBoxDeleteConfirm, MessageBoxDeleteConfirmCaption, button, icon);
            MessageBoxResult result = MessageBox.Show(TerritoriesDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirm, 
                TerritoriesDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirmCaption, button, icon);
            string error = null;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelNotifiedForTerritories itemSelected = (ModelNotifiedForTerritories)DataGridTerritories.SelectedItem;
                    dataConnection.DeleteData(itemSelected, out error);
                    if (string.IsNullOrEmpty(error))
                    {
                        TerritoriesDataContext.modelNotifiedForTerritoriesMain.Remove(itemSelected);
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
                MessageBox.Show(TerritoriesDataContext.WPFMessageAndLabelForList.MessageBoxDeleteOK);
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
                SetGridData(TerritoriesDataContext.modelNotifiedForTerritoriesMain);
                this.LoadDetail(TerritoriesDataContext.modelNotifiedForTerritoriesMain[0]);
                return;
            }
            List<ModelNotifiedForTerritories> basicFilteredList = FilterGrid(filterValue);
            SetGridData(basicFilteredList);
            if (TerritoriesDataContext.modelNotifiedForTerritoriesMain.Count != 0)
            {
                this.LoadDetail(TerritoriesDataContext.modelNotifiedForTerritoriesMain[0]);
            }                
        }        

        private List<ModelNotifiedForTerritories> FilterGrid(string filterValue)
        {
            filterValue = filterValue.ToLower();
            List<ModelNotifiedForTerritories> filteredList = new List<ModelNotifiedForTerritories>();
            foreach (ModelNotifiedForTerritories item in TerritoriesDataContext.modelNotifiedForTerritoriesMain)
            {
                if (item.TerritoryID.ToString().ToLower().Contains(filterValue))
{ 
filteredList.Add(item);
continue;
}

//Filter string values.
if (item.TerritoryID != null)
{
    if (item.TerritoryID.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.TerritoryDescription != null)
{
    if (item.TerritoryDescription.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

//Filter FK values.
if (item.RegionID != null)
{
    ModelNotifiedForRegion comboItem = TerritoriesDataContext.modelNotifiedForRegion.Where(x => x.RegionID == item.RegionID).FirstOrDefault();
    if ((comboItem != null) && (comboItem.RegionDescription != null) && (comboItem.RegionDescription.ToLower().Contains(filterValue)))
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

