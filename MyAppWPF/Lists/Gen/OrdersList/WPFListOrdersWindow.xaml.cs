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

    namespace MyApp.WPFList.Orders
    {
    
    public partial class ListWPFOrders : Page //Window
    {    
        public OrdersDataContext OrdersDataContext { get; set; }
        public IWPFOrdersDataConnection dataConnection;
        public string CurrentLanguage { get; set; }
    
        //Track[0014] Template:WPF_List_Constructor.html

        /// <summary>
        /// Permits navigation into main frame
        /// </summary>
        private Frame FrameMainWindow { get; set; }
        
        private WPFConfig config {get; set;}
        public ListWPFOrders(WPFConfig config, bool loadGrid, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            if (config.connectionType == WPFConfig.ConnectionType.REST)
            {
                dataConnection = new WPFOrdersRest(config);
            }
            else
            {
                dataConnection = new WPFOrdersDB(config);
            } 
            InitializeComponent();
            if(loadGrid)
            {
                LoadGrid();
            }            
            txtFilter.KeyDown += new KeyEventHandler(btnFilterKeyDown);            
            SetDateFilters();
        }       
        private void OpenFormClick(object sender, RoutedEventArgs e)
        {            
            ModelNotifiedForOrders itemSelected = (ModelNotifiedForOrders)DataGridOrders.SelectedItem;
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

            MyApp.WPFForms.Orders.FormWPFOrders page = new MyApp.WPFForms.Orders.FormWPFOrders(config);
            page.LoadForm(itemSelected.OrderID);
            page.Setup_SetLanguage(CurrentLanguage);
            ContainerWindowSimple win = new ContainerWindowSimple(page, "Form Orders");

            win.Show();
        }

        /// <summary>
        /// Set grid data. Convert List<T> into observable collection
        /// </summary>
        /// <param name="gridData"></param>
        public void SetGridData(List<ModelNotifiedForOrders> gridData)
        { 
            //Disable events during grid databind
            this.DataGridOrders.SelectionChanged -= OnSelectionChanged;
            
            this.OrdersDataContext.GridData = new ObservableCollection<ModelNotifiedForOrders>(gridData);
            this.DataGridOrders.ItemsSource = this.OrdersDataContext.GridData;
            //Enable events again
            this.DataGridOrders.SelectionChanged += OnSelectionChanged; 
        }

        public void LoadGrid(Func<ModelNotifiedForOrders, bool> filter = null)
        {
            this.DataGridOrders.ItemsSource = null;
            
            //Saving current language
            WPFMessageAndLabelForList currentLanguage = new WPFMessageAndLabelForList();
            if (this.OrdersDataContext != null)
            { 
                currentLanguage = this.OrdersDataContext.WPFMessageAndLabelForList;
            }
            string error = null;
            this.OrdersDataContext = dataConnection.GetDataContext(out error);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }

            //Setting language messages
            this.OrdersDataContext.WPFMessageAndLabelForList = currentLanguage;
            
            this.DataContext = OrdersDataContext;            

            List<ModelNotifiedForOrders> filteredList;
            if (filter == null)
                filteredList = OrdersDataContext.modelNotifiedForOrdersMain;
            else
                filteredList = OrdersDataContext.modelNotifiedForOrdersMain.Where(filter).ToList();
            
            //Bind data
            SetGridData(filteredList);            

            //Load detail forms/lists in master/detail
            if (OrdersDataContext.modelNotifiedForOrdersMain.Count != 0)
            {
                this.LoadDetail(OrdersDataContext.modelNotifiedForOrdersMain[0]);
            }
        }        


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForOrders itemSelected = (ModelNotifiedForOrders)DataGridOrders.SelectedItem;
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
                MessageBox.Show(OrdersDataContext.WPFMessageAndLabelForList.MessageBoxSaveOK);
            }
            else
            {
                //MessageBox.Show(MessageBoxSaveError + error);
                MessageBox.Show(OrdersDataContext.WPFMessageAndLabelForList.MessageBoxSaveError + error);
            }
        }


        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            //MessageBoxResult result = MessageBox.Show(MessageBoxDeleteConfirm, MessageBoxDeleteConfirmCaption, button, icon);
            MessageBoxResult result = MessageBox.Show(OrdersDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirm, 
                OrdersDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirmCaption, button, icon);
            string error = null;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelNotifiedForOrders itemSelected = (ModelNotifiedForOrders)DataGridOrders.SelectedItem;
                    dataConnection.DeleteData(itemSelected, out error);
                    if (string.IsNullOrEmpty(error))
                    {
                        OrdersDataContext.modelNotifiedForOrdersMain.Remove(itemSelected);
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
                MessageBox.Show(OrdersDataContext.WPFMessageAndLabelForList.MessageBoxDeleteOK);
                btnReload_Click(null, null);
            }
        }

        private List<ModelNotifiedForOrders> FilterGridDate(List<ModelNotifiedForOrders> itensToFilter)
        {
            List<ModelNotifiedForOrders> filteredGrid = itensToFilter;
            string filterDateColumn;
            DateTime? beginDateInformed;
            DateTime? endDateInformed;
            if (WPFUtilities.ApplyDateTimeFilter(cbDateFilter, dtpFilterDateBegin, dtpFilterDateEnd, out filterDateColumn, out beginDateInformed, out endDateInformed))
            {
                filteredGrid = WPFUtilities.FilterDate<ModelNotifiedForOrders>(filteredGrid, filterDateColumn, beginDateInformed, endDateInformed);
            }
            return filteredGrid;
        }

        public List<string> FilterDate
        {
            get
            {
                List<string> dates = new List<string>();
                var metaData = typeof(ModelNotifiedForOrders).GetProperties();
                foreach (var item in metaData)
                {
                    if (item.PropertyType.ToString().Contains("DateTime"))
                        dates.Add(item.Name);
                }
                return dates;
            }
            set { _FilterDate = value; }
        }

        private void SetDateFilters()
        {
            List<string> dateFields = FilterDate;
            cbDateFilter.ItemsSource = dateFields;
        }
        private List<string> _FilterDate;
        public String SelectedDate { get; set; }        
        
        
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
                SetGridData(OrdersDataContext.modelNotifiedForOrdersMain);
                this.LoadDetail(OrdersDataContext.modelNotifiedForOrdersMain[0]);
                return;
            }
            List<ModelNotifiedForOrders> basicFilteredList = FilterGrid(filterValue);
            basicFilteredList = FilterGridDate(basicFilteredList);                        
            SetGridData(basicFilteredList);
            if (OrdersDataContext.modelNotifiedForOrdersMain.Count != 0)
            {
                this.LoadDetail(OrdersDataContext.modelNotifiedForOrdersMain[0]);
            }                
        }        

        private List<ModelNotifiedForOrders> FilterGrid(string filterValue)
        {
            filterValue = filterValue.ToLower();
            List<ModelNotifiedForOrders> filteredList = new List<ModelNotifiedForOrders>();
            foreach (ModelNotifiedForOrders item in OrdersDataContext.modelNotifiedForOrdersMain)
            {
                if (item.OrderID.ToString().ToLower().Contains(filterValue))
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

if (item.ShipName != null)
{
    if (item.ShipName.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.ShipAddress != null)
{
    if (item.ShipAddress.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.ShipCity != null)
{
    if (item.ShipCity.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.ShipRegion != null)
{
    if (item.ShipRegion.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.ShipPostalCode != null)
{
    if (item.ShipPostalCode.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.ShipCountry != null)
{
    if (item.ShipCountry.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

//Filter FK values.
if (item.CustomerID != null)
{
    ModelNotifiedForCustomers comboItem = OrdersDataContext.modelNotifiedForCustomers.Where(x => x.CustomerID == item.CustomerID).FirstOrDefault();
    if ((comboItem != null) && (comboItem.ContactName != null) && (comboItem.ContactName.ToLower().Contains(filterValue)))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.EmployeeID != null)
{
    ModelNotifiedForEmployees comboItem = OrdersDataContext.modelNotifiedForEmployees.Where(x => x.EmployeeID == item.EmployeeID).FirstOrDefault();
    if ((comboItem != null) && (comboItem.LastName != null) && (comboItem.LastName.ToLower().Contains(filterValue)))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.ShipVia != null)
{
    ModelNotifiedForShippers comboItem = OrdersDataContext.modelNotifiedForShippers.Where(x => x.ShipperID == item.ShipVia).FirstOrDefault();
    if ((comboItem != null) && (comboItem.CompanyName != null) && (comboItem.CompanyName.ToLower().Contains(filterValue)))
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

