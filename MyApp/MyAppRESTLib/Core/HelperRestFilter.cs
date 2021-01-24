using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RESTLib.Core
{
    public class HelperRestFilter
    {
        public bool Ignore(List<DataFilterExpressionREST> Filters, Type sourceType, object sourceObject)
        {
            if (Filters == null)
            {
                return false;
            }
            PropertyInfo[] sourceListClassInfoProperties = sourceType.GetProperties();
            bool ignoreThisItem = true;
            foreach (PropertyInfo sourceProperty in sourceListClassInfoProperties)
            {
                
                foreach (DataFilterExpressionREST filter in Filters)
                {
                    //Property from json exis in class? If no, ignore and continue to next filer property
                    var targetProperty = sourceType.GetProperty(filter.FieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);                    
                    if (targetProperty != null)
                    {
                        continue;
                    }

                    //Filter property name from json match? If no, ignore
                    if (sourceProperty.Name.ToLower() != filter.FieldName.ToLower())
                    {
                        continue;
                    }

                    //Get property to filter value. If is null, ignore "if is null, is retrieved"
                    var propertyValue = sourceProperty.GetValue(sourceObject, null);
                    if (propertyValue == null)
                    {                        
                        continue;
                    }

                    string s = propertyValue.ToString().ToLower();
                    if (s.ToLower().Contains(filter.Filter.ToLower()))
                    {
                        ignoreThisItem = false;
                    }
                    else
                    {
                        return true;
                    }
                }
                
            }
            return ignoreThisItem;
        }
    }
}
