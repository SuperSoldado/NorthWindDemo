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

    namespace MyApp.WPFList.Region
    {
    
    public partial class ListWPFRegion : Page //Window
    {    
        public RegionDataContext RegionDataContext { get; set; }
        public IWPFRegionDataConnection dataConnection;
        public string CurrentLanguage { get; set; }
    
        //Track[0014] Template:WPF_List_Constructor.html

        /// <summary>
        /// Permits navigation into main frame
        /// </summary>
        private Frame FrameMainWindow { get; set; }
        
        private WPFConfig config {get; set;}
        public ListWPFRegion(WPFConfig config, bool loadGrid, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            if (config.connectionType == WPFConfig.ConnectionType.REST)
            {
                dataConnection = new WPFRegionRest(config);
            }
            else
            {
                dataConnection = new WPFRegionDB(config);
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
            ModelNotifiedForRegion itemSelected = (ModelNotifiedForRegion)DataGridRegion.SelectedItem;
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

            MyApp.WPFForms.Region.FormWPFRegion page = new MyApp.WPFForms.Region.FormWPFRegion(config);
            page.LoadForm(itemSelected.RegionID);
            page.Setup_SetLanguage(CurrentLanguage);
            ContainerWindowSimple win = new ContainerWindowSimple(page, "Form Region");

            win.Show();
        }

        /// <summary>
        /// Set grid data. Convert List<T> into observable collection
        /// </summary>
        /// <param name="gridData"></param>
        public void SetGridData(List<ModelNotifiedForRegion> gridData)
        { 
            //Disable events during grid databind
            this.DataGridRegion.SelectionChanged -= OnSelectionChanged;
            
            this.RegionDataContext.GridData = new ObservableCollection<ModelNotifiedForRegion>(gridData);
            this.DataGridRegion.ItemsSource = this.RegionDataContext.GridData;
            //Enable events again
            this.DataGridRegion.SelectionChanged += OnSelectionChanged; 
        }

        public void LoadGrid(Func<ModelNotifiedForRegion, bool> filter = null)
        {
            this.DataGridRegion.ItemsSource = null;
            
            //Saving current language
            WPFMessageAndLabelForList currentLanguage = new WPFMessageAndLabelForList();
            if (this.RegionDataContext != null)
            { 
                currentLanguage = this.RegionDataContext.WPFMessageAndLabelForList;
            }
            string error = null;
            this.RegionDataContext = dataConnection.GetDataContext(out error);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }

            //Setting language messages
            this.RegionDataContext.WPFMessageAndLabelForList = currentLanguage;
            
            this.DataContext = RegionDataContext;            

            List<ModelNotifiedForRegion> filteredList;
            if (filter == null)
                filteredList = RegionDataContext.modelNotifiedForRegionMain;
            else
                filteredList = RegionDataContext.modelNotifiedForRegionMain.Where(filter).ToList();
            
            //Bind data
            SetGridData(filteredList);            

            //Load detail forms/lists in master/detail
            if (RegionDataContext.modelNotifiedForRegionMain.Count != 0)
            {
                this.LoadDetail(RegionDataContext.modelNotifiedForRegionMain[0]);
            }
        }        


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForRegion itemSelected = (ModelNotifiedForRegion)DataGridRegion.SelectedItem;
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
                MessageBox.Show(RegionDataContext.WPFMessageAndLabelForList.MessageBoxSaveOK);
            }
            else
            {
                //MessageBox.Show(MessageBoxSaveError + error);
                MessageBox.Show(RegionDataContext.WPFMessageAndLabelForList.MessageBoxSaveError + error);
            }
        }


        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            //MessageBoxResult result = MessageBox.Show(MessageBoxDeleteConfirm, MessageBoxDeleteConfirmCaption, button, icon);
            MessageBoxResult result = MessageBox.Show(RegionDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirm, 
                RegionDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirmCaption, button, icon);
            string error = null;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelNotifiedForRegion itemSelected = (ModelNotifiedForRegion)DataGridRegion.SelectedItem;
                    dataConnection.DeleteData(itemSelected, out error);
                    if (string.IsNullOrEmpty(error))
                    {
                        RegionDataContext.modelNotifiedForRegionMain.Remove(itemSelected);
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
                MessageBox.Show(RegionDataContext.WPFMessageAndLabelForList.MessageBoxDeleteOK);
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
                SetGridData(RegionDataContext.modelNotifiedForRegionMain);
                this.LoadDetail(RegionDataContext.modelNotifiedForRegionMain[0]);
                return;
            }
            List<ModelNotifiedForRegion> basicFilteredList = FilterGrid(filterValue);
            SetGridData(basicFilteredList);
            if (RegionDataContext.modelNotifiedForRegionMain.Count != 0)
            {
                this.LoadDetail(RegionDataContext.modelNotifiedForRegionMain[0]);
            }                
        }        

        private List<ModelNotifiedForRegion> FilterGrid(string filterValue)
        {
            filterValue = filterValue.ToLower();
            List<ModelNotifiedForRegion> filteredList = new List<ModelNotifiedForRegion>();
            foreach (ModelNotifiedForRegion item in RegionDataContext.modelNotifiedForRegionMain)
            {
                if (item.RegionID.ToString().ToLower().Contains(filterValue))
{ 
filteredList.Add(item);
continue;
}

//Filter string values.
if (item.RegionDescription != null)
{
    if (item.RegionDescription.ToLower().Contains(filterValue))
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

