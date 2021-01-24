using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using System.Linq;
using System.ComponentModel;
using MyAppGlobalLib.Helper;
using System.ComponentModel;

namespace MyAppWPFLib
{
    /// <summary>
    /// Interaction logic for frmPopUpSimpleList.xaml
    /// </summary>
    public partial class frmPopUpSimpleList : Window
    {
        public Container_HelperWPFDataGrid Container { get; set; }

        public frmPopUpSimpleList()
        {
            InitializeComponent();
        }

        public frmPopUpSimpleList(Container_HelperWPFDataGrid container)
        {
            this.Container = container;
            InitializeComponent();
            MyGrid.ItemsSource = container.Data;
            
            //possible todo: try to figure out initial form position in screen and size
            this.Width = 300;
            this.Height = 300;
            Closing += OnWindowClosing;
        }

        /// <summary>
        /// Handle closing logic, set e.Cancel as needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            //This will force edited data in grid being "commited" (in memory) before the form is closed
            //Without this code, if user "check" one checkbox and immediately close, the change is not considered.
            MyGrid.CommitEdit(DataGridEditingUnit.Row, true);            
        }

        public void ApplyGridSetup()
        {
            HelperWPFDataGrid.ShowOrHideColumns(Container, MyGrid);
            HelperWPFDataGrid.SortGrid(Container.DefaultOrdering, Container, MyGrid);
            HelperWPFDataGrid.ChangeColumnOrder(Container, MyGrid);
            HelperWPFDataGrid.SetColumnHeaders(Container, MyGrid);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private List<string> GetColumnsToFilter()
        {
            List<string> columnsToFilter = new List<string>();
            foreach (var item in this.Container.ColumnsInGrid)
            {
                if (item.FieldName == "Check_Status")
                {
                    continue;
                }
                columnsToFilter.Add(item.FieldName);
            }
            return columnsToFilter;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterExpression = txtFilter.Text;
            if (string.IsNullOrEmpty(filterExpression))
            {
                MyGrid.ItemsSource = Container.Data;
                MyGrid.Items.Refresh();
                ApplyGridSetup();
            }
            else
            {
                if (filterExpression.Length > 1)
                {
                    List<string> columnsToFilter = GetColumnsToFilter();
                    GenericFilter genericFilter = new GenericFilter();
                    List<object> filteredList = genericFilter.GetFilterdList(Container.Data, columnsToFilter, filterExpression);
                    MyGrid.ItemsSource = filteredList;
                    MyGrid.Items.Refresh();
                    ApplyGridSetup();
                }
            }
        }
    }
}
