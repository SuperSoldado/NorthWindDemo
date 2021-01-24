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

    namespace MyApp.WPFList.Tag
    {
    
    public partial class ListWPFTag : Page //Window
    {    
        public TagDataContext TagDataContext { get; set; }
        public IWPFTagDataConnection dataConnection;
        public string CurrentLanguage { get; set; }
    
        //Track[0014] Template:WPF_List_Constructor.html

        /// <summary>
        /// Permits navigation into main frame
        /// </summary>
        private Frame FrameMainWindow { get; set; }
        
        private WPFConfig config {get; set;}
        public ListWPFTag(WPFConfig config, bool loadGrid, Frame mainFrame)
        {
            this.config = config;
            this.FrameMainWindow = mainFrame;
            if (config.connectionType == WPFConfig.ConnectionType.REST)
            {
                dataConnection = new WPFTagRest(config);
            }
            else
            {
                dataConnection = new WPFTagDB(config);
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
            ModelNotifiedForTag itemSelected = (ModelNotifiedForTag)DataGridTag.SelectedItem;
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

            MyApp.WPFForms.Tag.FormWPFTag page = new MyApp.WPFForms.Tag.FormWPFTag(config);
            page.LoadForm(itemSelected.TagID);
            page.Setup_SetLanguage(CurrentLanguage);
            ContainerWindowSimple win = new ContainerWindowSimple(page, "Form Tag");

            win.Show();
        }

        /// <summary>
        /// Set grid data. Convert List<T> into observable collection
        /// </summary>
        /// <param name="gridData"></param>
        public void SetGridData(List<ModelNotifiedForTag> gridData)
        { 
            //Disable events during grid databind
            this.DataGridTag.SelectionChanged -= OnSelectionChanged;
            
            this.TagDataContext.GridData = new ObservableCollection<ModelNotifiedForTag>(gridData);
            this.DataGridTag.ItemsSource = this.TagDataContext.GridData;
            //Enable events again
            this.DataGridTag.SelectionChanged += OnSelectionChanged; 
        }

        public void LoadGrid(Func<ModelNotifiedForTag, bool> filter = null)
        {
            this.DataGridTag.ItemsSource = null;
            
            //Saving current language
            WPFMessageAndLabelForList currentLanguage = new WPFMessageAndLabelForList();
            if (this.TagDataContext != null)
            { 
                currentLanguage = this.TagDataContext.WPFMessageAndLabelForList;
            }
            string error = null;
            this.TagDataContext = dataConnection.GetDataContext(out error);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error);
                return;
            }

            //Setting language messages
            this.TagDataContext.WPFMessageAndLabelForList = currentLanguage;
            
            this.DataContext = TagDataContext;            

            List<ModelNotifiedForTag> filteredList;
            if (filter == null)
                filteredList = TagDataContext.modelNotifiedForTagMain;
            else
                filteredList = TagDataContext.modelNotifiedForTagMain.Where(filter).ToList();
            
            //Bind data
            SetGridData(filteredList);            

            //Load detail forms/lists in master/detail
            if (TagDataContext.modelNotifiedForTagMain.Count != 0)
            {
                this.LoadDetail(TagDataContext.modelNotifiedForTagMain[0]);
            }
        }        


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            ModelNotifiedForTag itemSelected = (ModelNotifiedForTag)DataGridTag.SelectedItem;
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
                MessageBox.Show(TagDataContext.WPFMessageAndLabelForList.MessageBoxSaveOK);
            }
            else
            {
                //MessageBox.Show(MessageBoxSaveError + error);
                MessageBox.Show(TagDataContext.WPFMessageAndLabelForList.MessageBoxSaveError + error);
            }
        }


        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            //MessageBoxResult result = MessageBox.Show(MessageBoxDeleteConfirm, MessageBoxDeleteConfirmCaption, button, icon);
            MessageBoxResult result = MessageBox.Show(TagDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirm, 
                TagDataContext.WPFMessageAndLabelForList.MessageBoxDeleteConfirmCaption, button, icon);
            string error = null;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ModelNotifiedForTag itemSelected = (ModelNotifiedForTag)DataGridTag.SelectedItem;
                    dataConnection.DeleteData(itemSelected, out error);
                    if (string.IsNullOrEmpty(error))
                    {
                        TagDataContext.modelNotifiedForTagMain.Remove(itemSelected);
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
                MessageBox.Show(TagDataContext.WPFMessageAndLabelForList.MessageBoxDeleteOK);
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
                SetGridData(TagDataContext.modelNotifiedForTagMain);
                this.LoadDetail(TagDataContext.modelNotifiedForTagMain[0]);
                return;
            }
            List<ModelNotifiedForTag> basicFilteredList = FilterGrid(filterValue);
            SetGridData(basicFilteredList);
            if (TagDataContext.modelNotifiedForTagMain.Count != 0)
            {
                this.LoadDetail(TagDataContext.modelNotifiedForTagMain[0]);
            }                
        }        

        private List<ModelNotifiedForTag> FilterGrid(string filterValue)
        {
            filterValue = filterValue.ToLower();
            List<ModelNotifiedForTag> filteredList = new List<ModelNotifiedForTag>();
            foreach (ModelNotifiedForTag item in TagDataContext.modelNotifiedForTagMain)
            {
                if (item.TagID.ToString().ToLower().Contains(filterValue))
{ 
filteredList.Add(item);
continue;
}

//Filter string values.
if (item.TextDesc != null)
{
    if (item.TextDesc.ToLower().Contains(filterValue))
    {
        filteredList.Add(item);
        continue;
    }
}

if (item.TagType != null)
{
    if (item.TagType.ToLower().Contains(filterValue))
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

