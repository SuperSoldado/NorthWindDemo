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

    namespace MyApp.WPFList.Shippers
    {
    
    public partial class ListWPFShippers : Page //Window
    {    
        public ShippersDataContext ShippersDataContext { get; set; }
        public IWPFShippersDataConnection dataConnection;
        public string CurrentLanguage { get; set; }
    
        //Track[0014] Template:WPF_List_Constructor.html

        /// <summary>
        /// Permits navigation into main frame
        /// </summary>
        private Frame FrameMainWindow { get; set; }
        
        private WPFConfig config {get; set;}
        public ListWPFShippers(WPFConfig config, bool loadGrid, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            if (config.connectionType == WPFConfig.ConnectionType.REST)
            {
                dataConnection = new WPFShippersRest(config);
            }
            else
            {
                dataConnection = new WPFShippersDB(config);
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
            ModelNotifiedForShippers itemSelected = (ModelNotifiedForShippers)DataGridShippers.SelectedItem;
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

            MyApp.WPFForms.Shippers.FormWPFShippers page = new MyApp.WPFForms.Shippers.FormWPFShippers(config);
            page.LoadForm(itemSelected.ShipperID);
            page.Setup_SetLanguage(CurrentLanguage);
            ContainerWindowSimple win = new ContainerWindowSimple(page, "Form Shippers");

            win.Show();
        }

        /// <summary>
        /// Set grid data. Convert List<T> into observable collection
        /// </summary>
        /// <param name="gridData"></param>
        public void SetGridData(List<ModelNotifiedForShippers> gridData)
        { 
            //Disable events during grid databind
            this.DataGridShippers.SelectionChanged -= OnSelectionChanged;
            
            this.ShippersDataContext.GridData = new ObservableCollection<ModelNotifiedForShippers>(gridData);
            this.DataGridShippers.ItemsSource = this.ShippersDataContext.GridData;
            //Enable events again
            this.DataGridShippers.SelectionChanged += OnSelectionChanged; 
        }

        public void LoadGrid(Func<ModelNotifiedForShippers, bool> filter = null)
        {
            this.DataGridShippers.ItemsSource = null;
            
            //Saving current language
            WPFMessageAndLabelForList currentLanguage = new WPFMessageAndLabelForList();
            if (this.ShippersDataContext != null)
            { 
                currentLanguage = this.ShippersDataContext.WPFMessageAndLabelForList;
            }
            string error = null;
            this.ShippersDataContext = dataConnection.GetDataContext(out error);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }

            //Setting language messages
            this.ShippersDataContext.WPFMessageAndLabelForList = currentLanguage;
            
            this.DataContext = ShippersDataContext;            

            List<ModelNotifiedForShippers> filteredList;
            if (filter == null)
                filteredList = ShippersDataContext.modelNotifiedForShippersMain;
            else
                filteredList = ShippersDataContext.modelNotifiedForShippersMain.Where(filter).ToList();
            
            //Bind data
            SetGridData(filteredList);            

            //Load detail forms/lists in master/detail
            if (ShippersDataContext.modelNotifiedForShippersMain.Count != 0)
            {
                this.LoadDetail(ShippersDataContext.modelNotifiedForShippersMain[0]);
            }
        }        


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForShippers itemSelected = (ModelNotifiedForShippers)DataGridShippers.SelectedItem;
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
                MessageBox.Show(ShippersDataContext.WPFMessageAndLabelForList.MessageBoxSaveOK);
            }
            else
            {
                //MessageBox.Show(MessageBoxSaveError + error);
                MessageBox.Show(ShippersDataContext.WPFMessageAndLabelForList.MessageBoxSaveError + error);
            }
        }


        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            //MessageBoxResult result = MessageBox.Show(MessageBoxDeleteConfirm, MessageBoxDeleteConfirmCaption, button, icon);
            MessageBoxResult result = MessageBox.Show(ShippersDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirm, 
                ShippersDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirmCaption, button, icon);
            string error = null;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelNotifiedForShippers itemSelected = (ModelNotifiedForShippers)DataGridShippers.SelectedItem;
                    dataConnection.DeleteData(itemSelected, out error);
                    if (string.IsNullOrEmpty(error))
                    {
                        ShippersDataContext.modelNotifiedForShippersMain.Remove(itemSelected);
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
                MessageBox.Show(ShippersDataContext.WPFMessageAndLabelForList.MessageBoxDeleteOK);
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
                SetGridData(ShippersDataContext.modelNotifiedForShippersMain);
                this.LoadDetail(ShippersDataContext.modelNotifiedForShippersMain[0]);
                return;
            }
            List<ModelNotifiedForShippers> basicFilteredList = FilterGrid(filterValue);
            SetGridData(basicFilteredList);
            if (ShippersDataContext.modelNotifiedForShippersMain.Count != 0)
            {
                this.LoadDetail(ShippersDataContext.modelNotifiedForShippersMain[0]);
            }                
        }        

        private List<ModelNotifiedForShippers> FilterGrid(string filterValue)
        {
            filterValue = filterValue.ToLower();
            List<ModelNotifiedForShippers> filteredList = new List<ModelNotifiedForShippers>();
            foreach (ModelNotifiedForShippers item in ShippersDataContext.modelNotifiedForShippersMain)
            {
                if (item.ShipperID.ToString().ToLower().Contains(filterValue))
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

if (item.Phone != null)
{
    if (item.Phone.ToLower().Contains(filterValue))
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

