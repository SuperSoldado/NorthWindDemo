using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace MyAppWPFLib
{
    public static class WPFUtilities
    {
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(src, null);
        }
        public static List<T> FilterDate<T>(List<T> list, string filterDateColumn, DateTime? beginDate, DateTime? endDate)
        {
            if (string.IsNullOrEmpty(filterDateColumn))
            {
                return list;
            }

            if ((beginDate == null) && (endDate == null))
            {
                return list;
            }

            List<T> filtereList = new List<T>();
            foreach (T item in list)
            {
                object o;
                try
                {
                    //try to get value from "class.property". 
                    o = GetPropValue(item, filterDateColumn);
                }
                catch (Exception ex)
                {
                    //If the property does not exist in class, i will abort the operation and filter nothing
                    return list;
                }
                if (o == null)
                {
                    continue;
                }

                DateTime dateTocheck;
                if (!DateTime.TryParse(o.ToString(), out dateTocheck))
                {
                    continue;
                }
                
                if (beginDate != null && endDate != null)
                {
                    if (dateTocheck >= beginDate && dateTocheck <= endDate)
                    {
                        filtereList.Add(item);
                    }
                }
                else if (beginDate != null)
                {
                    if (dateTocheck >= beginDate)
                    {
                        filtereList.Add(item);
                    }
                }
                else
                {
                    if (dateTocheck <= endDate)
                    {
                        filtereList.Add(item);
                    }
                }
            }

            return filtereList;
        }

        public static bool ApplyDateTimeFilter(ComboBox comboBoxDateTimeColumns, DatePicker textBoxBeginDateFilter, DatePicker textBoxEndDateFilter, out string filterDateColumn, out DateTime? beginDateInformed, out DateTime? endDateInformed)
        {
            filterDateColumn = null;
            beginDateInformed = null;
            endDateInformed = null;
            if ((comboBoxDateTimeColumns.SelectedItem ==null) || (string.IsNullOrEmpty(comboBoxDateTimeColumns.SelectedItem.ToString())))
            {
                return false;
            }

            DateTime beginDate;
            DateTime endDate;
            if (DateTime.TryParse(textBoxBeginDateFilter.Text, out beginDate))
            {
                beginDateInformed = beginDate;
            }
            if (DateTime.TryParse(textBoxEndDateFilter.Text, out endDate))
            {
                endDateInformed = endDate;
            }
            filterDateColumn = comboBoxDateTimeColumns.SelectedItem.ToString();
            return true;
        }
    }
}
