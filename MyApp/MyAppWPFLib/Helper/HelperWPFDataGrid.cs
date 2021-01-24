using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MyAppWPFLib
{
    /// <summary>
    /// Defines a column parameter used in GridHelper
    /// </summary>
    public class ColumnParametertInGrid
    {
        /// <summary>
        /// Field name is the field in "Data" parameter.
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// Header in Grid's column
        /// </summary>
        public string FieldHeader { get; set; }

        public bool UserCanEdit { get; set; }
    }

    /// <summary>
    /// Parameters used to use in helper
    /// </summary>
    public class Container_HelperWPFDataGrid
    {
        /// <summary>
        /// Data in grid
        /// </summary>
        public IEnumerable<object> Data { get; set; }

        /// <summary>
        /// Default ordering when open the form. Example: col1;asc,col2;desc,col3;asc
        /// Check_Status;desc,TerritoryDescription;asc
        /// </summary>
        public string DefaultOrdering { get; set; }

        /// <summary>
        /// Defines the order and visiblility
        /// </summary>
        public List<ColumnParametertInGrid> ColumnsInGrid { get; set; }
    }

    /// <summary>
    /// Generic gelper for WPFDataGrid (sorting, show/Hide columns)
    /// </summary>
    public static class HelperWPFDataGrid
    {
        private static DataGridColumn GetGridColumn(string columnName, Container_HelperWPFDataGrid container, DataGrid dataGrid)
        {
            var parameter = container.ColumnsInGrid.Where(x => x.FieldName == columnName).FirstOrDefault();
            foreach (DataGridColumn columnInGrid in dataGrid.Columns)
            {
                string gridColumnName = columnInGrid.Header.ToString();
                if (parameter.FieldName == gridColumnName)
                {
                    return columnInGrid;
                }
            }

            return null;
        }

        /// <summary>
        /// Send grid and columns and it will sort by default. Example (column): "col1;asc,col2;desc,col3;asc"
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="columns"></param>
        public static void SortGrid(string columnsToSort, Container_HelperWPFDataGrid container, DataGrid dataGrid)
        {
            foreach (var col in dataGrid.Columns)
            {
                col.SortDirection = null;
            }

            var colToSort = columnsToSort.Split(',');

            // Clear current sort descriptions
            dataGrid.Items.SortDescriptions.Clear();

            foreach (string column in colToSort)
            {
                var aux = column.Split(";");
                string fieldName = aux[0];
                string sortDirectionFromParam = aux[1];
                ListSortDirection sortDirection = ListSortDirection.Ascending;
                if (sortDirectionFromParam.ToLower() == "asc")
                {
                    sortDirection = ListSortDirection.Ascending;
                }
                else
                {
                    sortDirection = ListSortDirection.Descending;
                }

                DataGridColumn dataGridColumn = GetGridColumn(fieldName, container, dataGrid);

                var sortDescription = new SortDescription(dataGridColumn.SortMemberPath, sortDirection);
                // Add the new sort description
                dataGrid.Items.SortDescriptions.Add(sortDescription);
            }

            // Refresh items to display sort
            dataGrid.Items.Refresh();
        }

        /// <summary>
        /// Send grid and columns and it will sort by default. Example (column): "col1;asc,col2;desc,col3;asc"
        /// </summary>
        public static void ShowOrHideColumns(Container_HelperWPFDataGrid container, DataGrid dataGrid)
        {
            foreach (DataGridColumn columnInGrid in dataGrid.Columns)
            {
                string gridColumnName = columnInGrid.Header.ToString();
                bool found = false;

                foreach (ColumnParametertInGrid setupColumn in container.ColumnsInGrid)
                {
                    if (gridColumnName == setupColumn.FieldName)
                    {
                        columnInGrid.IsReadOnly = !setupColumn.UserCanEdit;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    columnInGrid.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Changee the order of displayed columns
        /// </summary>
        /// <param name="container"></param>
        /// <param name="dataGrid"></param>
        public static void ChangeColumnOrder(Container_HelperWPFDataGrid container, DataGrid dataGrid)
        {
            int columnIndex = 0;
            foreach (ColumnParametertInGrid setupColumn in container.ColumnsInGrid)
            {
                foreach (DataGridColumn columnInGrid in dataGrid.Columns)
                {
                    string gridColumnName = columnInGrid.Header.ToString();
                    if (gridColumnName == setupColumn.FieldName)
                    {
                        columnInGrid.DisplayIndex = columnIndex;
                        columnIndex++;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Change column headers
        /// </summary>
        /// <param name="container"></param>
        /// <param name="dataGrid"></param>
        public static void SetColumnHeaders(Container_HelperWPFDataGrid container, DataGrid dataGrid)
        {
            foreach (DataGridColumn columnInGrid in dataGrid.Columns)
            {
                string gridColumnName = columnInGrid.Header.ToString();
                bool found = false;

                foreach (ColumnParametertInGrid setupColumn in container.ColumnsInGrid)
                {
                    if (gridColumnName == setupColumn.FieldName)
                    {
                        found = true;
                        columnInGrid.Header = setupColumn.FieldHeader;
                        break;
                    }
                }
            }
        }
    }
}
