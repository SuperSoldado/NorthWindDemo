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

    namespace MyApp.WPFList.TagEmployee
    {
    
    public partial class ListWPFTagEmployee : Page //Window
    {    
        public TagEmployeeDataContext TagEmployeeDataContext { get; set; }
        public IWPFTagEmployeeDataConnection dataConnection;
        public string CurrentLanguage { get; set; }
    
        //Track[0014] Template:WPF_List_Constructor.html

        /// <summary>
        /// Permits navigation into main frame
        /// </summary>
        private Frame FrameMainWindow { get; set; }
        
        private WPFConfig config {get; set;}
        public ListWPFTagEmployee(WPFConfig config, bool loadGrid, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            if (config.connectionType == WPFConfig.ConnectionType.REST)
            {
                dataConnection = new WPFTagEmployeeRest(config);
            }
            else
            {
                dataConnection = new WPFTagEmployeeDB(config);
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
            ModelNotifiedForTagEmployee itemSelected = (ModelNotifiedForTagEmployee)DataGridTagEmployee.SelectedItem;
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

            MyApp.WPFForms.TagEmployee.FormWPFTagEmployee page = new MyApp.WPFForms.TagEmployee.FormWPFTagEmployee(config);
            page.LoadForm(itemSelected.TagEmployeeID);
            page.Setup_SetLanguage(CurrentLanguage);
            ContainerWindowSimple win = new ContainerWindowSimple(page, "Form TagEmployee");

            win.Show();
        }

        /// <summary>
        /// Set grid data. Convert List<T> into observable collection
        /// </summary>
        /// <param name="gridData"></param>
        public void SetGridData(List<ModelNotifiedForTagEmployee> gridData)
        { 
            //Disable events during grid databind
            this.DataGridTagEmployee.SelectionChanged -= OnSelectionChanged;
            
            this.TagEmployeeDataContext.GridData = new ObservableCollection<ModelNotifiedForTagEmployee>(gridData);
            this.DataGridTagEmployee.ItemsSource = this.TagEmployeeDataContext.GridData;
            //Enable events again
            this.DataGridTagEmployee.SelectionChanged += OnSelectionChanged; 
        }

        public void LoadGrid(Func<ModelNotifiedForTagEmployee, bool> filter = null)
        {
            this.DataGridTagEmployee.ItemsSource = null;
            
            //Saving current language
            WPFMessageAndLabelForList currentLanguage = new WPFMessageAndLabelForList();
            if (this.TagEmployeeDataContext != null)
            { 
                currentLanguage = this.TagEmployeeDataContext.WPFMessageAndLabelForList;
            }
            string error = null;
            this.TagEmployeeDataContext = dataConnection.GetDataContext(out error);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }

            //Setting language messages
            this.TagEmployeeDataContext.WPFMessageAndLabelForList = currentLanguage;
            
            this.DataContext = TagEmployeeDataContext;            

            List<ModelNotifiedForTagEmployee> filteredList;
            if (filter == null)
                filteredList = TagEmployeeDataContext.modelNotifiedForTagEmployeeMain;
            else
                filteredList = TagEmployeeDataContext.modelNotifiedForTagEmployeeMain.Where(filter).ToList();
            
            //Bind data
            SetGridData(filteredList);            

            //Load detail forms/lists in master/detail
            if (TagEmployeeDataContext.modelNotifiedForTagEmployeeMain.Count != 0)
            {
                this.LoadDetail(TagEmployeeDataContext.modelNotifiedForTagEmployeeMain[0]);
            }
        }        


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForTagEmployee itemSelected = (ModelNotifiedForTagEmployee)DataGridTagEmployee.SelectedItem;
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
                MessageBox.Show(TagEmployeeDataContext.WPFMessageAndLabelForList.MessageBoxSaveOK);
            }
            else
            {
                //MessageBox.Show(MessageBoxSaveError + error);
                MessageBox.Show(TagEmployeeDataContext.WPFMessageAndLabelForList.MessageBoxSaveError + error);
            }
        }


        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            //MessageBoxResult result = MessageBox.Show(MessageBoxDeleteConfirm, MessageBoxDeleteConfirmCaption, button, icon);
            MessageBoxResult result = MessageBox.Show(TagEmployeeDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirm, 
                TagEmployeeDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirmCaption, button, icon);
            string error = null;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelNotifiedForTagEmployee itemSelected = (ModelNotifiedForTagEmployee)DataGridTagEmployee.SelectedItem;
                    dataConnection.DeleteData(itemSelected, out error);
                    if (string.IsNullOrEmpty(error))
                    {
                        TagEmployeeDataContext.modelNotifiedForTagEmployeeMain.Remove(itemSelected);
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
                MessageBox.Show(TagEmployeeDataContext.WPFMessageAndLabelForList.MessageBoxDeleteOK);
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
                SetGridData(TagEmployeeDataContext.modelNotifiedForTagEmployeeMain);
                this.LoadDetail(TagEmployeeDataContext.modelNotifiedForTagEmployeeMain[0]);
                return;
            }
            List<ModelNotifiedForTagEmployee> basicFilteredList = FilterGrid(filterValue);
            SetGridData(basicFilteredList);
            if (TagEmployeeDataContext.modelNotifiedForTagEmployeeMain.Count != 0)
            {
                this.LoadDetail(TagEmployeeDataContext.modelNotifiedForTagEmployeeMain[0]);
            }                
        }        

        private List<ModelNotifiedForTagEmployee> FilterGrid(string filterValue)
        {
            filterValue = filterValue.ToLower();
            List<ModelNotifiedForTagEmployee> filteredList = new List<ModelNotifiedForTagEmployee>();
            foreach (ModelNotifiedForTagEmployee item in TagEmployeeDataContext.modelNotifiedForTagEmployeeMain)
            {
                if (item.TagEmployeeID.ToString().ToLower().Contains(filterValue))
{ 
filteredList.Add(item);
continue;
}

//Filter string values.
if (item.TagEmployeeTextDesc != null)
{
    if (item.TagEmployeeTextDesc.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

//Filter FK values.
if (item.EmployeeIDFK != null)
{
    ModelNotifiedForEmployees comboItem = TagEmployeeDataContext.modelNotifiedForEmployees.Where(x => x.EmployeeID == item.EmployeeIDFK).FirstOrDefault();
    if ((comboItem != null) && (comboItem.LastName != null) && (comboItem.LastName.ToLower().Contains(filterValue)))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.TagFK != null)
{
    ModelNotifiedForTag comboItem = TagEmployeeDataContext.modelNotifiedForTag.Where(x => x.TagID == item.TagFK).FirstOrDefault();
    if ((comboItem != null) && (comboItem.TextDesc != null) && (comboItem.TextDesc.ToLower().Contains(filterValue)))
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

