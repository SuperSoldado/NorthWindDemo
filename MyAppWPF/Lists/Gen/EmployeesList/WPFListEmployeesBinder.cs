//Track[0019] Template:WPF_List_Binder.html
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System;
using MyApp.WPFList.Employees;

namespace MyApp.WPFList.Employees
{    
    /* EXAMPLE: Click button code to open MASTER List (this list for example)
         * This exemplifies how bind a Master list (this list) to a detail form view (other form generated)
         * This is the caller click event to open MasterDetail page.
            private void OpenMasterDetail_Click(object sender, RoutedEventArgs e)
            {
                //Part 1: load config file
                configFile = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\GlobalConfiguration.json";
                globalConfigReader = new GlobalConfigReader();
                config = globalConfigReader.Load(configFile);

                //part2: create master and detail pages
                MyListPage master = new MyListPage(config.WPFConfig);//this list page object
                MyFormDetail detail = new MyFormDetail(config.WPFConfig);//Detail form (MUST be generated first)
                
                //Part3: link MasterPage.DetailPage to DetailObject
                master.DetailForm = detail;//this will connect master list to detail form
                
                //Part4:call master/detail container
                ContainerWindowMasterDetail win = new ContainerWindowMasterDetail(master, detail, "Master Detail Page");
                win.Show();
            }
        */
    
        /// <summary>
        /// Event triggered by mouse click in list. 
        /// Objective: on mouse click, load detail forms/lists when using MasterDetail
        /// Example: see example above
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public partial class ListWPFEmployees
        {

        /// <summary>
        /// Triggered by change in grid's row. 
        /// </summary>
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((DataGridEmployees.SelectedItem == null) || DataGridEmployees.SelectedItem.GetType() != typeof(ModelNotifiedForEmployees))
            {
                //New row on grid's bottom. By default do nothing when new row is included
                return;
            }

            ModelNotifiedForEmployees selectedItem = (ModelNotifiedForEmployees)DataGridEmployees.SelectedItem;
            LoadDetail(selectedItem);
        }        
        
        /// <summary>
        /// Detail List loading data from EmployeeTerritories table, using it's foreing key to 'Employees'
        /// </summary>
        public MyApp.WPFList.EmployeeTerritories.ListWPFEmployeeTerritories DetailListEmployeeTerritories { get; set; }
        
        /// <summary>
        /// Detail form loading data from EmployeeTerritories table, using it's foreing key to 'Employees'
        /// Note: You must implement inside 'LoadDetail' (code below) to find 'EmployeeTerritories' primary key.
        /// </summary>
        public MyApp.WPFForms.EmployeeTerritories.FormWPFEmployeeTerritories DetailFormEmployeeTerritories { get; set; }

        /// <summary>
        /// Detail List loading data from Orders table, using it's foreing key to 'Employees'
        /// </summary>
        public MyApp.WPFList.Orders.ListWPFOrders DetailListOrders { get; set; }
        
        /// <summary>
        /// Detail form loading data from Orders table, using it's foreing key to 'Employees'
        /// Note: You must implement inside 'LoadDetail' (code below) to find 'Orders' primary key.
        /// </summary>
        public MyApp.WPFForms.Orders.FormWPFOrders DetailFormOrders { get; set; }

        /// <summary>
        /// Detail List loading data from TagEmployee table, using it's foreing key to 'Employees'
        /// </summary>
        public MyApp.WPFList.TagEmployee.ListWPFTagEmployee DetailListTagEmployee { get; set; }
        
        /// <summary>
        /// Detail form loading data from TagEmployee table, using it's foreing key to 'Employees'
        /// Note: You must implement inside 'LoadDetail' (code below) to find 'TagEmployee' primary key.
        /// </summary>
        public MyApp.WPFForms.TagEmployee.FormWPFTagEmployee DetailFormTagEmployee { get; set; }



        /// <summary>
        /// Load Detail form/list in master detail. Triggered by user's change in Grid's Row.
        /// When row change, load "DetailForm" or "Detail List" (need to be configured)
        /// </summary>
        /// <param name="selectedItem"></param>
        private void LoadDetail(ModelNotifiedForEmployees selectedItem)
        {
            if (selectedItem == null)
            {
                return;
            }


if (DetailListEmployeeTerritories != null)
{
DetailListEmployeeTerritories.LoadGrid(x => x.EmployeeID == selectedItem.EmployeeID);
}
if (DetailListOrders != null)
{
DetailListOrders.LoadGrid(x => x.EmployeeID == selectedItem.EmployeeID);
}
if (DetailListTagEmployee != null)
{
DetailListTagEmployee.LoadGrid(x => x.EmployeeIDFK == selectedItem.EmployeeID);
}
/* Note: the detail form can load only ONE row from 'EmployeeTerritories'. It's necessary to inform DetailForm primary key here or create a custom Form.Load().
if (DetailForm{0} != null)
{
//DetailFormEmployeeTerritories.LoadForm(selectedItem.EmployeeID);
}
*/
/* Note: the detail form can load only ONE row from 'Orders'. It's necessary to inform DetailForm primary key here or create a custom Form.Load().
if (DetailForm{0} != null)
{
//DetailFormOrders.LoadForm(selectedItem.EmployeeID);
}
*/
/* Note: the detail form can load only ONE row from 'TagEmployee'. It's necessary to inform DetailForm primary key here or create a custom Form.Load().
if (DetailForm{0} != null)
{
//DetailFormTagEmployee.LoadForm(selectedItem.EmployeeID);
}
*/
        }

        }        
 }
