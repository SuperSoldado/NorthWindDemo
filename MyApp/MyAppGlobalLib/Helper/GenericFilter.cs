using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MyAppGlobalLib.Helper
{
    /// <summary>
    /// Give me 
    /// * IEnumerable SomeDataToFilter 
    /// * List<string> fields to search (case insensitive)
    /// * string "someFilter" (case insensitive)
    /// and I'll give the filtered list
    /// </summary>
    public class GenericFilter
    {
        private bool IsInFilteredList(object itemObjectToTest, string columnToTest, string valueInFilter)
        {
            Type dataType = itemObjectToTest.GetType();
            PropertyInfo[] sourceListClassInfoProperties = dataType.GetProperties();
            var targetProperty = dataType.GetProperty(columnToTest, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (targetProperty == null)
            {
                return false;
            }

            var content = targetProperty.GetValue(itemObjectToTest, null);
            string contentCleaned = content.ToString().Trim().ToLower();
            string filterCleaned = valueInFilter.ToLower().Trim();
            if (contentCleaned.Contains(filterCleaned))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Filter one list
        /// </summary>
        /// <param name="fullList">Data to filter</param>
        /// <param name="columnsToFilter">Properties (getters) to search</param>
        /// <param name="filterExpression">some string to filter</param>
        /// <returns>Filtered data</returns>
        public List<object> GetFilterdList(IEnumerable<object> fullList, List<string> columnsToFilter, string filterExpression)
        {
            List<object> filteredList = new List<object>();
            foreach (string column in columnsToFilter)
            {
                foreach (object item in fullList)
                {
                    bool isInFilteredList = IsInFilteredList(item, column, filterExpression);
                    if (isInFilteredList)
                    {
                        filteredList.Add(item);
                    }
                }
            }
            return filteredList;
        }
    }
}
