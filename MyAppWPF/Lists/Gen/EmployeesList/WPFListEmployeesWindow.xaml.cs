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

    namespace MyApp.WPFList.Employees
    {
    
    public partial class ListWPFEmployees : Page //Window
    {    
        public EmployeesDataContext EmployeesDataContext { get; set; }
        public IWPFEmployeesDataConnection dataConnection;
        public string CurrentLanguage { get; set; }
    
        //Track[0014] Template:WPF_List_Constructor.html

        /// <summary>
        /// Permits navigation into main frame
        /// </summary>
        private Frame FrameMainWindow { get; set; }
        
        private WPFConfig config {get; set;}
        public ListWPFEmployees(WPFConfig config, bool loadGrid, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            if (config.connectionType == WPFConfig.ConnectionType.REST)
            {
                dataConnection = new WPFEmployeesRest(config);
            }
            else
            {
                dataConnection = new WPFEmployeesDB(config);
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
            ModelNotifiedForEmployees itemSelected = (ModelNotifiedForEmployees)DataGridEmployees.SelectedItem;
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

            MyApp.WPFForms.Employees.FormWPFEmployees page = new MyApp.WPFForms.Employees.FormWPFEmployees(config);
            page.LoadForm(itemSelected.EmployeeID);
            page.Setup_SetLanguage(CurrentLanguage);
            ContainerWindowSimple win = new ContainerWindowSimple(page, "Form Employees");

            win.Show();
        }

        /// <summary>
        /// Set grid data. Convert List<T> into observable collection
        /// </summary>
        /// <param name="gridData"></param>
        public void SetGridData(List<ModelNotifiedForEmployees> gridData)
        { 
            //Disable events during grid databind
            this.DataGridEmployees.SelectionChanged -= OnSelectionChanged;
            
            this.EmployeesDataContext.GridData = new ObservableCollection<ModelNotifiedForEmployees>(gridData);
            this.DataGridEmployees.ItemsSource = this.EmployeesDataContext.GridData;
            //Enable events again
            this.DataGridEmployees.SelectionChanged += OnSelectionChanged; 
        }

        public void LoadGrid(Func<ModelNotifiedForEmployees, bool> filter = null)
        {
            this.DataGridEmployees.ItemsSource = null;
            
            //Saving current language
            WPFMessageAndLabelForList currentLanguage = new WPFMessageAndLabelForList();
            if (this.EmployeesDataContext != null)
            { 
                currentLanguage = this.EmployeesDataContext.WPFMessageAndLabelForList;
            }
            string error = null;
            this.EmployeesDataContext = dataConnection.GetDataContext(out error);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }

            //Setting language messages
            this.EmployeesDataContext.WPFMessageAndLabelForList = currentLanguage;
            
            this.DataContext = EmployeesDataContext;            

            List<ModelNotifiedForEmployees> filteredList;
            if (filter == null)
                filteredList = EmployeesDataContext.modelNotifiedForEmployeesMain;
            else
                filteredList = EmployeesDataContext.modelNotifiedForEmployeesMain.Where(filter).ToList();
            
            //Bind data
            SetGridData(filteredList);            

            //Load detail forms/lists in master/detail
            if (EmployeesDataContext.modelNotifiedForEmployeesMain.Count != 0)
            {
                this.LoadDetail(EmployeesDataContext.modelNotifiedForEmployeesMain[0]);
            }
        }        


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForEmployees itemSelected = (ModelNotifiedForEmployees)DataGridEmployees.SelectedItem;
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
                MessageBox.Show(EmployeesDataContext.WPFMessageAndLabelForList.MessageBoxSaveOK);
            }
            else
            {
                //MessageBox.Show(MessageBoxSaveError + error);
                MessageBox.Show(EmployeesDataContext.WPFMessageAndLabelForList.MessageBoxSaveError + error);
            }
        }


        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            //MessageBoxResult result = MessageBox.Show(MessageBoxDeleteConfirm, MessageBoxDeleteConfirmCaption, button, icon);
            MessageBoxResult result = MessageBox.Show(EmployeesDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirm, 
                EmployeesDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirmCaption, button, icon);
            string error = null;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelNotifiedForEmployees itemSelected = (ModelNotifiedForEmployees)DataGridEmployees.SelectedItem;
                    dataConnection.DeleteData(itemSelected, out error);
                    if (string.IsNullOrEmpty(error))
                    {
                        EmployeesDataContext.modelNotifiedForEmployeesMain.Remove(itemSelected);
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
                MessageBox.Show(EmployeesDataContext.WPFMessageAndLabelForList.MessageBoxDeleteOK);
                btnReload_Click(null, null);
            }
        }

        private List<ModelNotifiedForEmployees> FilterGridDate(List<ModelNotifiedForEmployees> itensToFilter)
        {
            List<ModelNotifiedForEmployees> filteredGrid = itensToFilter;
            string filterDateColumn;
            DateTime? beginDateInformed;
            DateTime? endDateInformed;
            if (WPFUtilities.ApplyDateTimeFilter(cbDateFilter, dtpFilterDateBegin, dtpFilterDateEnd, out filterDateColumn, out beginDateInformed, out endDateInformed))
            {
                filteredGrid = WPFUtilities.FilterDate<ModelNotifiedForEmployees>(filteredGrid, filterDateColumn, beginDateInformed, endDateInformed);
            }
            return filteredGrid;
        }

        public List<string> FilterDate
        {
            get
            {
                List<string> dates = new List<string>();
                var metaData = typeof(ModelNotifiedForEmployees).GetProperties();
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
                SetGridData(EmployeesDataContext.modelNotifiedForEmployeesMain);
                this.LoadDetail(EmployeesDataContext.modelNotifiedForEmployeesMain[0]);
                return;
            }
            List<ModelNotifiedForEmployees> basicFilteredList = FilterGrid(filterValue);
            basicFilteredList = FilterGridDate(basicFilteredList);                        
            SetGridData(basicFilteredList);
            if (EmployeesDataContext.modelNotifiedForEmployeesMain.Count != 0)
            {
                this.LoadDetail(EmployeesDataContext.modelNotifiedForEmployeesMain[0]);
            }                
        }        

        private List<ModelNotifiedForEmployees> FilterGrid(string filterValue)
        {
            filterValue = filterValue.ToLower();
            List<ModelNotifiedForEmployees> filteredList = new List<ModelNotifiedForEmployees>();
            foreach (ModelNotifiedForEmployees item in EmployeesDataContext.modelNotifiedForEmployeesMain)
            {
                if (item.EmployeeID.ToString().ToLower().Contains(filterValue))
{ 
filteredList.Add(item);
continue;
}

//Filter string values.
if (item.LastName != null)
{
    if (item.LastName.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.FirstName != null)
{
    if (item.FirstName.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.Title != null)
{
    if (item.Title.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.TitleOfCourtesy != null)
{
    if (item.TitleOfCourtesy.ToLower().Contains(filterValue))
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

if (item.HomePhone != null)
{
    if (item.HomePhone.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.Extension != null)
{
    if (item.Extension.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.Notes != null)
{
    if (item.Notes.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.PhotoPath != null)
{
    if (item.PhotoPath.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

//Filter FK values.
if (item.ReportsTo != null)
{
    ModelNotifiedForEmployees comboItem = EmployeesDataContext.modelNotifiedForEmployees.Where(x => x.EmployeeID == item.ReportsTo).FirstOrDefault();
    if ((comboItem != null) && (comboItem.LastName != null) && (comboItem.LastName.ToLower().Contains(filterValue)))
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
            ModelNotifiedForEmployees itemSelected = (ModelNotifiedForEmployees)DataGridEmployees.SelectedItem;
            if (image.Tag.ToString().ToLower() == "Photo".ToLower())
            {
                
                frmDisplayBinaryData frmDisplayBinaryData = new frmDisplayBinaryData(itemSelected.Photo, GlobalEnums.MimeTypes.Image);
                frmDisplayBinaryData.ShowDialog();
                //frmDisplayBinaryData.Owner = ToDo. Without owner, alt tab can bug
                itemSelected.Photo = frmDisplayBinaryData.myViewModel.BinData;
                return;            
            }
            MessageBox.Show("Error: 'btnSaveBinAs' does not contains the tag reference to target binary column.");
        }

        private void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForEmployees itemSelected = (ModelNotifiedForEmployees)DataGridEmployees.SelectedItem;
            frmDisplayBinaryData frmDisplayBinaryData = new frmDisplayBinaryData(itemSelected.Photo, GlobalEnums.MimeTypes.Image);
            frmDisplayBinaryData.ShowDialog();
            //frmDisplayBinaryData.Owner = ToDo. Without owner, alt tab can bug
            itemSelected.Photo = frmDisplayBinaryData.myViewModel.BinData;
            itemSelected.BtnAddPhotoVisibility = Visibility.Collapsed;
            itemSelected.BtnExcludePhotoVisibility = Visibility.Visible;
        }

        private void btnExcludePhoto_Click(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForEmployees itemSelected = (ModelNotifiedForEmployees)DataGridEmployees.SelectedItem;
            itemSelected.Photo = null;
            itemSelected.BtnAddPhotoVisibility = Visibility.Visible;
            itemSelected.BtnExcludePhotoVisibility = Visibility.Collapsed;
        }

        private void btnOpenSimpleList_Territories_Click(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForEmployees itemSelected = (ModelNotifiedForEmployees)DataGridEmployees.SelectedItem;
            Container_HelperWPFDataGrid container = new Container_HelperWPFDataGrid();
            container.Data = itemSelected.LookDownComboDataTerritories;

            List<ColumnParametertInGrid> columnsInGrid = new List<ColumnParametertInGrid>();
            ColumnParametertInGrid checkBoxColumn = new ColumnParametertInGrid();
            ColumnParametertInGrid displayColumn = new ColumnParametertInGrid();

            //Setting the checkbox field
            checkBoxColumn.FieldName = "Check_Status";
            checkBoxColumn.FieldHeader = "";
            checkBoxColumn.UserCanEdit = true;
            columnsInGrid.Add(checkBoxColumn);

            //Setting display field
            displayColumn.FieldName = "TerritoryDescription";
            displayColumn.FieldHeader = "TerritoryDescription";
            displayColumn.UserCanEdit = false;
            columnsInGrid.Add(displayColumn);
            container.ColumnsInGrid = columnsInGrid;

            //Setting the sortering: first the checked itens, then the display field
            //Example: "Col1;desc,Col2;asc"
            container.DefaultOrdering = "Check_Status;desc,TerritoryDescription;asc";

            frmPopUpSimpleList frmPopUpSimpleList = new frmPopUpSimpleList(container);
            frmPopUpSimpleList.Show();
            frmPopUpSimpleList.ApplyGridSetup();
        }

    }
    }

