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

    namespace MyApp.WPFList.EmployeeTerritories
    {
    
    public partial class ListWPFEmployeeTerritories : Page //Window
    {    
        public EmployeeTerritoriesDataContext EmployeeTerritoriesDataContext { get; set; }
        public IWPFEmployeeTerritoriesDataConnection dataConnection;
        public string CurrentLanguage { get; set; }
    
        //Track[0014] Template:WPF_List_Constructor.html

        /// <summary>
        /// Permits navigation into main frame
        /// </summary>
        private Frame FrameMainWindow { get; set; }
        
        private WPFConfig config {get; set;}
        public ListWPFEmployeeTerritories(WPFConfig config, bool loadGrid, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            if (config.connectionType == WPFConfig.ConnectionType.REST)
            {
                dataConnection = new WPFEmployeeTerritoriesRest(config);
            }
            else
            {
                dataConnection = new WPFEmployeeTerritoriesDB(config);
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
            ModelNotifiedForEmployeeTerritories itemSelected = (ModelNotifiedForEmployeeTerritories)DataGridEmployeeTerritories.SelectedItem;
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

            MyApp.WPFForms.EmployeeTerritories.FormWPFEmployeeTerritories page = new MyApp.WPFForms.EmployeeTerritories.FormWPFEmployeeTerritories(config);
            page.LoadForm(itemSelected.EmployeeID,itemSelected.TerritoryID);
            page.Setup_SetLanguage(CurrentLanguage);
            ContainerWindowSimple win = new ContainerWindowSimple(page, "Form EmployeeTerritories");

            win.Show();
        }

        /// <summary>
        /// Set grid data. Convert List<T> into observable collection
        /// </summary>
        /// <param name="gridData"></param>
        public void SetGridData(List<ModelNotifiedForEmployeeTerritories> gridData)
        { 
            //Disable events during grid databind
            this.DataGridEmployeeTerritories.SelectionChanged -= OnSelectionChanged;
            
            this.EmployeeTerritoriesDataContext.GridData = new ObservableCollection<ModelNotifiedForEmployeeTerritories>(gridData);
            this.DataGridEmployeeTerritories.ItemsSource = this.EmployeeTerritoriesDataContext.GridData;
            //Enable events again
            this.DataGridEmployeeTerritories.SelectionChanged += OnSelectionChanged; 
        }

        public void LoadGrid(Func<ModelNotifiedForEmployeeTerritories, bool> filter = null)
        {
            this.DataGridEmployeeTerritories.ItemsSource = null;
            
            //Saving current language
            WPFMessageAndLabelForList currentLanguage = new WPFMessageAndLabelForList();
            if (this.EmployeeTerritoriesDataContext != null)
            { 
                currentLanguage = this.EmployeeTerritoriesDataContext.WPFMessageAndLabelForList;
            }
            string error = null;
            this.EmployeeTerritoriesDataContext = dataConnection.GetDataContext(out error);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }

            //Setting language messages
            this.EmployeeTerritoriesDataContext.WPFMessageAndLabelForList = currentLanguage;
            
            this.DataContext = EmployeeTerritoriesDataContext;            

            List<ModelNotifiedForEmployeeTerritories> filteredList;
            if (filter == null)
                filteredList = EmployeeTerritoriesDataContext.modelNotifiedForEmployeeTerritoriesMain;
            else
                filteredList = EmployeeTerritoriesDataContext.modelNotifiedForEmployeeTerritoriesMain.Where(filter).ToList();
            
            //Bind data
            SetGridData(filteredList);            

            //Load detail forms/lists in master/detail
            if (EmployeeTerritoriesDataContext.modelNotifiedForEmployeeTerritoriesMain.Count != 0)
            {
                this.LoadDetail(EmployeeTerritoriesDataContext.modelNotifiedForEmployeeTerritoriesMain[0]);
            }
        }        


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForEmployeeTerritories itemSelected = (ModelNotifiedForEmployeeTerritories)DataGridEmployeeTerritories.SelectedItem;
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
                MessageBox.Show(EmployeeTerritoriesDataContext.WPFMessageAndLabelForList.MessageBoxSaveOK);
            }
            else
            {
                //MessageBox.Show(MessageBoxSaveError + error);
                MessageBox.Show(EmployeeTerritoriesDataContext.WPFMessageAndLabelForList.MessageBoxSaveError + error);
            }
        }


        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            //MessageBoxResult result = MessageBox.Show(MessageBoxDeleteConfirm, MessageBoxDeleteConfirmCaption, button, icon);
            MessageBoxResult result = MessageBox.Show(EmployeeTerritoriesDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirm, 
                EmployeeTerritoriesDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirmCaption, button, icon);
            string error = null;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelNotifiedForEmployeeTerritories itemSelected = (ModelNotifiedForEmployeeTerritories)DataGridEmployeeTerritories.SelectedItem;
                    dataConnection.DeleteData(itemSelected, out error);
                    if (string.IsNullOrEmpty(error))
                    {
                        EmployeeTerritoriesDataContext.modelNotifiedForEmployeeTerritoriesMain.Remove(itemSelected);
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
                MessageBox.Show(EmployeeTerritoriesDataContext.WPFMessageAndLabelForList.MessageBoxDeleteOK);
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
                SetGridData(EmployeeTerritoriesDataContext.modelNotifiedForEmployeeTerritoriesMain);
                this.LoadDetail(EmployeeTerritoriesDataContext.modelNotifiedForEmployeeTerritoriesMain[0]);
                return;
            }
            List<ModelNotifiedForEmployeeTerritories> basicFilteredList = FilterGrid(filterValue);
            SetGridData(basicFilteredList);
            if (EmployeeTerritoriesDataContext.modelNotifiedForEmployeeTerritoriesMain.Count != 0)
            {
                this.LoadDetail(EmployeeTerritoriesDataContext.modelNotifiedForEmployeeTerritoriesMain[0]);
            }                
        }        

        private List<ModelNotifiedForEmployeeTerritories> FilterGrid(string filterValue)
        {
            filterValue = filterValue.ToLower();
            List<ModelNotifiedForEmployeeTerritories> filteredList = new List<ModelNotifiedForEmployeeTerritories>();
            foreach (ModelNotifiedForEmployeeTerritories item in EmployeeTerritoriesDataContext.modelNotifiedForEmployeeTerritoriesMain)
            {
                if (item.EmployeeID.ToString().ToLower().Contains(filterValue))
{ 
filteredList.Add(item);
continue;
}

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

//Filter FK values.
if (item.EmployeeID != null)
{
    ModelNotifiedForEmployees comboItem = EmployeeTerritoriesDataContext.modelNotifiedForEmployees.Where(x => x.EmployeeID == item.EmployeeID).FirstOrDefault();
    if ((comboItem != null) && (comboItem.LastName != null) && (comboItem.LastName.ToLower().Contains(filterValue)))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.TerritoryID != null)
{
    ModelNotifiedForTerritories comboItem = EmployeeTerritoriesDataContext.modelNotifiedForTerritories.Where(x => x.TerritoryID == item.TerritoryID).FirstOrDefault();
    if ((comboItem != null) && (comboItem.TerritoryDescription != null) && (comboItem.TerritoryDescription.ToLower().Contains(filterValue)))
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

