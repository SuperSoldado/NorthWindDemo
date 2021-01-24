using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MyAppGlobalLib.Helpers
{
    /// <summary>
    /// Copy all properties from one class to another class
    /// </summary>
    public static class Cloner
    {
        /// <summary>
        /// Copy all properties from "source" to "target". Example
        /// Cloner.CopyAllTo(typeof(Product), product, typeof(ProductWebApiControllerResponse), response);
        /// </summary>
        /// <param name="sourceType">Source type</param>
        /// <param name="sourceObject">Source object</param>
        /// <param name="targetType">Target type</param>
        /// <param name="targetObject">Target object</param>
        public static void CopyAllTo(Type sourceType, object sourceObject, Type targetType, object targetObject)
        {
            PropertyInfo[] sourceListClassInfoProperties = sourceType.GetProperties();
            foreach (PropertyInfo sourceProperty in sourceListClassInfoProperties)
            {
                var targetProperty = targetType.GetProperty(sourceProperty.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (targetProperty == null)
                {
                    continue;
                }

                targetProperty.SetValue(targetObject, sourceProperty.GetValue(sourceObject, null), null);
            }

            foreach (var sourceField in sourceType.GetFields())
            {
                //var targetField = targetType.GetField(sourceField.Name);

                var targetField = sourceType.GetField(sourceField.Name);
                targetField.SetValue(targetType, sourceField.GetValue(targetObject));
            }
        }
    }
}
