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

    namespace MyApp.WPFList.OrderDetails
    {
    
    public partial class ListWPFOrderDetails : Page //Window
    {    
        public OrderDetailsDataContext OrderDetailsDataContext { get; set; }
        public IWPFOrderDetailsDataConnection dataConnection;
        public string CurrentLanguage { get; set; }
    
        //Track[0014] Template:WPF_List_Constructor.html

        /// <summary>
        /// Permits navigation into main frame
        /// </summary>
        private Frame FrameMainWindow { get; set; }
        
        private WPFConfig config {get; set;}
        public ListWPFOrderDetails(WPFConfig config, bool loadGrid, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            if (config.connectionType == WPFConfig.ConnectionType.REST)
            {
                dataConnection = new WPFOrderDetailsRest(config);
            }
            else
            {
                dataConnection = new WPFOrderDetailsDB(config);
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
            ModelNotifiedForOrderDetails itemSelected = (ModelNotifiedForOrderDetails)DataGridOrderDetails.SelectedItem;
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

            MyApp.WPFForms.OrderDetails.FormWPFOrderDetails page = new MyApp.WPFForms.OrderDetails.FormWPFOrderDetails(config);
            page.LoadForm(itemSelected.OrderID,itemSelected.ProductID);
            page.Setup_SetLanguage(CurrentLanguage);
            ContainerWindowSimple win = new ContainerWindowSimple(page, "Form OrderDetails");

            win.Show();
        }

        /// <summary>
        /// Set grid data. Convert List<T> into observable collection
        /// </summary>
        /// <param name="gridData"></param>
        public void SetGridData(List<ModelNotifiedForOrderDetails> gridData)
        { 
            //Disable events during grid databind
            this.DataGridOrderDetails.SelectionChanged -= OnSelectionChanged;
            
            this.OrderDetailsDataContext.GridData = new ObservableCollection<ModelNotifiedForOrderDetails>(gridData);
            this.DataGridOrderDetails.ItemsSource = this.OrderDetailsDataContext.GridData;
            //Enable events again
            this.DataGridOrderDetails.SelectionChanged += OnSelectionChanged; 
        }

        public void LoadGrid(Func<ModelNotifiedForOrderDetails, bool> filter = null)
        {
            this.DataGridOrderDetails.ItemsSource = null;
            
            //Saving current language
            WPFMessageAndLabelForList currentLanguage = new WPFMessageAndLabelForList();
            if (this.OrderDetailsDataContext != null)
            { 
                currentLanguage = this.OrderDetailsDataContext.WPFMessageAndLabelForList;
            }
            string error = null;
            this.OrderDetailsDataContext = dataConnection.GetDataContext(out error);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }

            //Setting language messages
            this.OrderDetailsDataContext.WPFMessageAndLabelForList = currentLanguage;
            
            this.DataContext = OrderDetailsDataContext;            

            List<ModelNotifiedForOrderDetails> filteredList;
            if (filter == null)
                filteredList = OrderDetailsDataContext.modelNotifiedForOrderDetailsMain;
            else
                filteredList = OrderDetailsDataContext.modelNotifiedForOrderDetailsMain.Where(filter).ToList();
            
            //Bind data
            SetGridData(filteredList);            

            //Load detail forms/lists in master/detail
            if (OrderDetailsDataContext.modelNotifiedForOrderDetailsMain.Count != 0)
            {
                this.LoadDetail(OrderDetailsDataContext.modelNotifiedForOrderDetailsMain[0]);
            }
        }        


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForOrderDetails itemSelected = (ModelNotifiedForOrderDetails)DataGridOrderDetails.SelectedItem;
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
                MessageBox.Show(OrderDetailsDataContext.WPFMessageAndLabelForList.MessageBoxSaveOK);
            }
            else
            {
                //MessageBox.Show(MessageBoxSaveError + error);
                MessageBox.Show(OrderDetailsDataContext.WPFMessageAndLabelForList.MessageBoxSaveError + error);
            }
        }


        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            //MessageBoxResult result = MessageBox.Show(MessageBoxDeleteConfirm, MessageBoxDeleteConfirmCaption, button, icon);
            MessageBoxResult result = MessageBox.Show(OrderDetailsDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirm, 
                OrderDetailsDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirmCaption, button, icon);
            string error = null;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelNotifiedForOrderDetails itemSelected = (ModelNotifiedForOrderDetails)DataGridOrderDetails.SelectedItem;
                    dataConnection.DeleteData(itemSelected, out error);
                    if (string.IsNullOrEmpty(error))
                    {
                        OrderDetailsDataContext.modelNotifiedForOrderDetailsMain.Remove(itemSelected);
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
                MessageBox.Show(OrderDetailsDataContext.WPFMessageAndLabelForList.MessageBoxDeleteOK);
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
                SetGridData(OrderDetailsDataContext.modelNotifiedForOrderDetailsMain);
                this.LoadDetail(OrderDetailsDataContext.modelNotifiedForOrderDetailsMain[0]);
                return;
            }
            List<ModelNotifiedForOrderDetails> basicFilteredList = FilterGrid(filterValue);
            SetGridData(basicFilteredList);
            if (OrderDetailsDataContext.modelNotifiedForOrderDetailsMain.Count != 0)
            {
                this.LoadDetail(OrderDetailsDataContext.modelNotifiedForOrderDetailsMain[0]);
            }                
        }        

        private List<ModelNotifiedForOrderDetails> FilterGrid(string filterValue)
        {
            filterValue = filterValue.ToLower();
            List<ModelNotifiedForOrderDetails> filteredList = new List<ModelNotifiedForOrderDetails>();
            foreach (ModelNotifiedForOrderDetails item in OrderDetailsDataContext.modelNotifiedForOrderDetailsMain)
            {
                if (item.OrderID.ToString().ToLower().Contains(filterValue))
{ 
filteredList.Add(item);
continue;
}

if (item.ProductID.ToString().ToLower().Contains(filterValue))
{ 
filteredList.Add(item);
continue;
}

//Filter FK values.
if (item.OrderID != null)
{
    ModelNotifiedForOrders comboItem = OrderDetailsDataContext.modelNotifiedForOrders.Where(x => x.OrderID == item.OrderID).FirstOrDefault();
    if ((comboItem != null) && (comboItem.ShipName != null) && (comboItem.ShipName.ToLower().Contains(filterValue)))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.ProductID != null)
{
    ModelNotifiedForProducts comboItem = OrderDetailsDataContext.modelNotifiedForProducts.Where(x => x.ProductID == item.ProductID).FirstOrDefault();
    if ((comboItem != null) && (comboItem.ProductName != null) && (comboItem.ProductName.ToLower().Contains(filterValue)))
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

