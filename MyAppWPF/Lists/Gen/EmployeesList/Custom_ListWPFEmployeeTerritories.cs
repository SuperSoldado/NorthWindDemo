//Track[0019] Template:WPF_List_Binder.html
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System;
using MyApp.WPFList.EmployeeTerritories;

namespace MyApp.WPFList.EmployeeTerritories
{
    /// <summary>
    /// Freddy: added this custom code
    /// </summary>
    public partial class ListWPFEmployeeTerritories
    {
        private void LoadDetail(ModelNotifiedForEmployeeTerritories selectedItem)
        {
            if (selectedItem == null)
            {
                return;
            }

            if (DetailListTerritories != null)
            {
                DetailListTerritories.LoadGrid(x => x.TerritoryID == selectedItem.TerritoryID);
            }
        }

        /// <summary>
        /// Detail List loading data from OrderDetails table, using it's foreing key to 'Orders'
        /// </summary>
        public MyApp.WPFList.Territories.ListWPFTerritories DetailListTerritories { get; set; }
    }
}
