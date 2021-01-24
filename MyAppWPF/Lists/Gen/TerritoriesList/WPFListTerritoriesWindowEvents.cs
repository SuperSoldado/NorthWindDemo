//Track[0017] Template:WPF_List_Main.html
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using System.Windows.Input;

namespace MyApp.WPFList.Territories
{
    public partial class ListWPFTerritories
    {
       /// <summary>
        /// DataGrid event. If included in DataGrid with ComboBox allows the control enters "editMode". 
        /// The user needs to click 2 times to open the combo. Without this event the user needs to click 3 times
        /// Example:<DataGrid DataGridCell.Selected="CustomEvent_DataGridCell_Selected" 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnDataGridCell_Selected(object sender, RoutedEventArgs e)
        {
            // Lookup for the source to be DataGridCell
            if (e.OriginalSource.GetType() == typeof(DataGridCell))
            {
                // Starts the Edit on the row;
                DataGrid grd = (DataGrid)sender;
                grd.BeginEdit(e);
            }
        }

        /// <summary>
        /// On Enter Key, it tabs to into next cell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var uiElement = e.OriginalSource as UIElement;
            if (e.Key == Key.Enter && uiElement != null)
            {
                e.Handled = true;
                uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        /// <summary>
        /// Triggered after the user change content in row, then change the selected row. 
        /// Prevent new row to be added when the last row (insert row) still being edited and is not saved.
        /// Removing this code will permit the user create multiple insert rows on the bottom of the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {            
            DataGrid dataGrid = ((DataGrid)sender);

            if (TerritoriesDataContext.GridData == null)
            {
                return;
            }

            var newItens = TerritoriesDataContext.GridData.ToList().Where(x => x.NewItem == true);
            if (newItens == null)
            {
                return;
            }

            int itensInInsertMode = newItens.Count();
            if (itensInInsertMode > 0)
            {
                e.Cancel = true;
            }
        }

    }
 }
