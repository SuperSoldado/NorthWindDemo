using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace DataAccessLib.Core
{
    public static class HelperDBType
    {
        /// <summary>
        /// Parse string from type into DB type for parameters
        /// </summary>
        /// <param name="ClassInfoType"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static DbType GetDBType(Type ClassInfoType, string columnName)
        {
            PropertyInfo[] ListClassInfoProperties = ClassInfoType.GetProperties();
            foreach (PropertyInfo property in ListClassInfoProperties)
            {
                if (property.Name.ToLower() != columnName.ToLower())
                {
                    continue;
                }
                Type t = property.PropertyType;
                if (t == typeof(string))
                {
                    return DbType.String;
                }
                if (t == typeof(Int32))
                {
                    return DbType.Int32;
                }
                if (t == typeof(Int64))
                {
                    return DbType.Int64;
                }
                if (t == typeof(Decimal))
                {
                    return DbType.Decimal;
                }
                if (t == typeof(DateTime))
                {
                    return DbType.DateTime;
                }
                if (t == typeof(Boolean))
                {
                    return DbType.Boolean;
                }
            }
            throw new Exception(string.Format("Error parsing {0} field.", columnName));
        }
    }
}
