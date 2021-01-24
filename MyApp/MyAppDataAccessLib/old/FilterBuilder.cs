using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkShop.Core.Util
{
    public static class FilterBuilder
    {
        /// <summary>
        /// Returns a list of itens to filter in query. This is a BASIC filter for simple queries. 
        /// </summary>
        /// <param name="columns">Column name separeted with ";". Ex: "col1;col2;...;"</param>
        /// <param name="propertiesValues">Filters to add. Ex.: myClass.Col1, myClass.Col2,...</param>
        /// <returns>List like " and col1 = 'ABC'"</returns>
        private static StringBuilder GetFilters(string columns, params object[] propertiesValues)
        {
            string[] columnList = columns.Split(';');
            StringBuilder listWhereClausule = new StringBuilder();

            if (columnList.Length != propertiesValues.Length)
            {
                throw new Exception("Number of columns differs from number os filters.");
            }

            for (int i = 0; i < propertiesValues.Length; i++)
            {
                string s = string.Empty;
                if (propertiesValues[i] is int)
                {
                    s = " and " + columnList[i] + " = " + propertiesValues[i].ToString();
                }
                if (propertiesValues[i] is string)
                {
                    s = " and " + columnList[i] + " = '" + propertiesValues[i] + "'";
                }
                if (propertiesValues[i] is decimal)
                {
                    s = " and " + columnList[i] + " = " + propertiesValues[i];
                }

                if (propertiesValues[i] is DateTime)
                {
                    s = " and " + columnList[i] + " = '" + ((DateTime)propertiesValues[i]).ToString("dd/mm/yyyy") + "'";
                }

                listWhereClausule.Append(s);
            }

            return listWhereClausule;
        }
    }
}
