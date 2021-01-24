using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Reflection;

namespace MyAppDataAccessLib
{
    /// <summary>
    /// This class generates a list of DB parameters based on class "Info" type. 
    /// Warning: do not use MyClassInfoOvr because it dont maps the data in database.)
    /// </summary>
    public static class ParameterBuilder
    {
        /// <summary>
        /// Warning: use the "ClassInfo" type, not "ClassInfoOvr" because the parameters are supposed to be used for 
        /// the exact properties mapped in database and Class. The "ClassInfoOVR" has properties that don't go to database.        
        /// 
        /// Warning: the ParameterBuilder may result in overwhelming CPU process and memory peak due to the
        ///          relection used to generate the paramaters. This can be avoided supressing the "ParameterBuilder.GetParametersForInsert"
        ///          and the "foreach" loop, replacing it to static data. Example:
        ///          DbParameter d = motor.Command.CreateParameter();                
        ///          d.ParameterName = "ColumnNameInDatabase";
        ///          d.Value = parMyClassInfo.PropertyNameInClass;
        ///          motor.Command.Parameters.Add(d);
        /// </summary>
        /// <param name="ClassInfoType">The type used to map. Ex.: MyClass.MyProperty vs. MyTable.MyColumn</param>
        /// <param name="classInfoOvr">The class with the data used in parameter list.</param>
        /// <returns></returns>
        public static List<DbParameter> GetParametersForInsert(Type ClassInfoType, object classInfoOvr, DbCommand dbCommand)
        {
            PropertyInfo[] ListClassInfoProperties = ClassInfoType.GetProperties();
            try
            {
                List<DbParameter> paramList = new List<DbParameter>();
                foreach (PropertyInfo property in ListClassInfoProperties)
                {
                    DbParameter param = dbCommand.CreateParameter();
                    param.ParameterName = "@" + property.Name;
                    PropertyInfo propertyInfo = ClassInfoType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.Name == property.Name).FirstOrDefault();
                    if (propertyInfo != null)
                    {
                        param.Value = propertyInfo.GetValue(classInfoOvr, BindingFlags.Instance, null, null, null);

                        if (propertyInfo.PropertyType.FullName == "System.Byte[]")
                        {
                            param.DbType = System.Data.DbType.Binary;
                        }

                        //Treat null values as dbnull.
                        if (param.Value == null)
                            param.Value = DBNull.Value;
                    }
                    else
                    {
                        throw new Exception("Error at ParameterBuilder.GetParams: Could not get the property value.");
                    }

                    paramList.Add(param);
                }
                return paramList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error during parse Database->Class: " + ex.Message);
            }
        }

        /// <summary>
        /// Assemble one generic filter list. Ignore null and min values (like datetime, int, decimal)
        /// </summary>
        /// <param name="ClassInfoType">ClassInfo parameter</param>
        /// <param name="classInfoOvr">The value to apply the filter.</param>
        /// <param name="dbCommand">DbCommand for generate parameters.</param>
        /// <param name="whereClausule">string that represents the filter.</param>
        /// <returns></returns>
        public static List<DbParameter> GetParametersForSelect(Type ClassInfoType, object classInfoOvr, DbCommand dbCommand, out string whereClausule)
        {
            whereClausule = string.Empty;
            StringBuilder where = new StringBuilder();
            List<DbParameter> paramList = new List<DbParameter>();
            where.Append("where 1=1");

            //Iterate into the Info (not infoOvr).
            PropertyInfo[] ListClassInfoProperties = ClassInfoType.GetProperties();
            try
            {
                foreach (PropertyInfo property in ListClassInfoProperties)
                {
                    //Get the InfoOvr data.
                    PropertyInfo propertyInfo = ClassInfoType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.Name == property.Name).FirstOrDefault();
                    object valueToFilter = propertyInfo.GetValue(classInfoOvr, BindingFlags.Instance, null, null, null);
                    if (propertyInfo == null || valueToFilter == null)
                    {
                        //Dont process null values. Dont process custom fields.
                        continue;
                    }

                    //New code: for nullable stuff.
                    Type t = property.PropertyType;
                    if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        t = t.GetGenericArguments()[0];
                    }

                    //Warning:
                    //The automation skips filtering integer/decimal/datetime values that are not nullable and equals minvalue.
                    if (t == typeof(Int32))
                    {
                        if ((Int32)valueToFilter == Int32.MinValue)
                            continue;
                    }

                    if (t == typeof(Decimal))
                    {
                        if ((Decimal)valueToFilter == Decimal.MinValue)
                            continue;
                    }

                    DbParameter param = dbCommand.CreateParameter();
                    param.ParameterName = "@" + property.Name;

                    if (t == typeof(DateTime))//ignore hours
                    {
                        if ((DateTime)valueToFilter == DateTime.MinValue)
                            continue;

                        DateTime d = (DateTime)valueToFilter;
                        //If hour is equal zero, ignore the time part.
                        if (d.Hour == 0)
                        {
                            where.Append(" and dateadd(d, datediff(d,0, " + property.Name + "), 0) = @" + property.Name);
                            d = new DateTime(d.Year, d.Month, d.Day);
                        }
                        else//Otherwise filter with mask.
                        {
                            where.Append(" and " + property.Name + " = @" + property.Name);
                        }
                        param.Value = d;
                    }
                    else if (t == typeof(string))
                    {
                        where.Append(" and " + property.Name + " like @" + property.Name);
                        param.Value = "%" + valueToFilter + "%";
                    }
                    else
                    {
                        where.Append(" and " + property.Name + "=@" + property.Name);
                        param.Value = valueToFilter;
                    }
                    paramList.Add(param);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error during parse Database->Class: " + ex.Message);
            }
            whereClausule = where.ToString();
            return paramList;
        }

        public static List<DbParameter> GetParametersForUpdate(Type ClassInfoType, object classInfoOvr, DbCommand dbCommand)
        {
            PropertyInfo[] ListClassInfoProperties = ClassInfoType.GetProperties();
            try
            {
                List<DbParameter> paramList = new List<DbParameter>();
                foreach (PropertyInfo property in ListClassInfoProperties)
                {
                    DbParameter param = dbCommand.CreateParameter();
                    param.ParameterName = "@" + property.Name;
                    PropertyInfo propertyInfo = ClassInfoType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.Name == property.Name).FirstOrDefault();

                    if ((propertyInfo.GetMethod).ReturnType.FullName == "System.Byte[]")
                    {
                        param.DbType = System.Data.DbType.Binary;
                        //SqlDbType.Binary aki
                    }

                    if (propertyInfo != null)
                    {
                        param.Value = propertyInfo.GetValue(classInfoOvr, BindingFlags.Instance, null, null, null);

                        //Treat null values as dbnull.
                        if (param.Value == null)
                            param.Value = DBNull.Value;
                    }
                    else
                    {
                        throw new Exception("Error at ParameterBuilder.GetParams: Could not get the property value.");
                    }

                    paramList.Add(param);
                }
                return paramList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error during parse Database->Class: " + ex.Message);
            }
        }

        public static List<DbParameter> GetParametersForDelete(List<string> primareKeyList, Type ClassInfoType, object classInfoOvr, DbCommand dbCommand, out string whereClausule)
        {
            whereClausule = "";
            foreach (string PKColumnName in primareKeyList)
            {
                if (whereClausule == "")
                {
                    whereClausule = string.Format(" where {0}=@{0}", PKColumnName);
                }
                else
                {
                    whereClausule += string.Format(" and {0}=@{0}", PKColumnName);
                }
            }


            PropertyInfo[] ListClassInfoProperties = ClassInfoType.GetProperties();
            try
            {
                List<DbParameter> paramList = new List<DbParameter>();
                foreach (PropertyInfo property in ListClassInfoProperties)
                {
                    bool isPk = false;
                    foreach (string s in primareKeyList)
                    {
                        if (s == property.Name)
                        {
                            isPk = true;
                            break;
                        }
                    }
                    if (isPk == false)
                    {
                        continue;
                    }
                    //Reach this code only if is part of table's primary key.
                    DbParameter param = dbCommand.CreateParameter();
                    param.ParameterName = "@" + property.Name;
                    PropertyInfo propertyInfo = ClassInfoType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.Name == property.Name).FirstOrDefault();
                    if (propertyInfo != null)
                    {
                        param.Value = propertyInfo.GetValue(classInfoOvr, BindingFlags.Instance, null, null, null);
                        //Treat null values as dbnull.
                        if (param.Value == null)
                            param.Value = DBNull.Value;
                    }
                    else
                    {
                        throw new Exception("Error at ParameterBuilder.GetParams: Could not get the property value.");
                    }

                    paramList.Add(param);
                }
                return paramList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error during parse Database->Class: " + ex.Message);
            }
        }

        private static string GetPKContent(string PKColumnName, dynamic objectToRead)
        {
            var site = System.Runtime.CompilerServices.CallSite<Func<
                    System.Runtime.CompilerServices.CallSite, object, object>>.Create(
                    Microsoft.CSharp.RuntimeBinder.Binder.GetMember(0, PKColumnName, objectToRead.GetType(), new[] { 
                    Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo.Create(0, null) }));
            string s = site.Target(site, objectToRead);
            return s;
        }

        public static List<DbParameter> GetParametersForDelete(Type ClassInfoType, object classInfoOvr, DbCommand dbCommand, out string whereClausule)
        {
            whereClausule = "";
            return GetParametersForDelete("", ClassInfoType, classInfoOvr, dbCommand, out whereClausule);
        }

        /// <summary>
        /// Generate one parameter list and the where clausule.
        /// </summary>
        /// <param name="PKColumnName">Optional. If informed create "where myPKColumn=@myPkColumn" without </param>
        /// <param name="ClassInfoType">Class type used to reflect.</param>
        /// <param name="classInfoOvr">Class with the data.</param>
        /// <param name="dbCommand">dbCommand object</param>
        /// <param name="whereClausule">The filter used on delete command.</param>
        /// <returns>Parameter list</returns>
        public static List<DbParameter> GetParametersForDelete(string PKColumnName, Type ClassInfoType, object classInfoOvr, DbCommand dbCommand, out string whereClausule)
        {
            whereClausule = string.Empty;
            StringBuilder where = new StringBuilder();
            List<DbParameter> paramList = new List<DbParameter>();
            where.Append("where 1=1");

            if(!string.IsNullOrEmpty(PKColumnName))
            {
                //If PK is explicid informed, create only the PK.
                dynamic dynamicClass = classInfoOvr;
                string IDContent = GetPKContent(PKColumnName, dynamicClass);

                if (!string.IsNullOrEmpty(IDContent))
                {
                    DbParameter paramID = dbCommand.CreateParameter();
                    paramID.ParameterName = "@" + PKColumnName;
                    paramID.Value = IDContent;
                    paramList.Add(paramID);
                    whereClausule = string.Format(" where {0}=@{0}", PKColumnName);
                    return paramList;
                }
            }

            //Iterate into the Info (not infoOvr).
            PropertyInfo[] ListClassInfoProperties = ClassInfoType.GetProperties();
            try
            {
                foreach (PropertyInfo property in ListClassInfoProperties)
                {
                    //Get the InfoOvr data.
                    PropertyInfo propertyInfo = ClassInfoType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.Name == property.Name).FirstOrDefault();
                    object valueToFilter = propertyInfo.GetValue(classInfoOvr, BindingFlags.Instance, null, null, null);
                    if (propertyInfo == null || valueToFilter == null)
                    {
                        //Dont process null values. Dont process custom fields.
                        continue;
                    }

                    //For nullable stuff.
                    Type t = property.PropertyType;
                    if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        t = t.GetGenericArguments()[0];
                    }

                    //Warning:
                    //The automation skips filtering integer/decimal/datetime values that are not nullable and equals minvalue.
                    if (t == typeof(Int32))
                    {
                        if ((Int32)valueToFilter == Int32.MinValue)
                            continue;
                    }

                    if (t == typeof(Decimal))
                    {
                        if ((Decimal)valueToFilter == Decimal.MinValue)
                            continue;
                    }

                    DbParameter param = dbCommand.CreateParameter();
                    param.ParameterName = "@" + property.Name;

                    if (t == typeof(DateTime))//ignore hours
                    {
                        if ((DateTime)valueToFilter == DateTime.MinValue)
                            continue;

                        DateTime d = (DateTime)valueToFilter;
                        //If hour is equal zero, ignore the time part.
                        if (d.Hour == 0)
                        {
                            where.Append(" and dateadd(d, datediff(d,0, " + property.Name + "), 0) = @" + property.Name);
                            d = new DateTime(d.Year, d.Month, d.Day);
                        }
                        else//Otherwise filter with mask.
                        {
                            where.Append(" and " + property.Name + " = @" + property.Name);
                        }
                        param.Value = d;
                    }
                    else if (t == typeof(string))
                    {
                        where.Append(" and " + property.Name + " like @" + property.Name);
                        param.Value = "%" + valueToFilter + "%";
                    }
                    else
                    {
                        where.Append(" and " + property.Name + "=@" + property.Name);
                        param.Value = valueToFilter;
                    }
                    paramList.Add(param);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error during parse Database->Class: " + ex.Message);
            }
            whereClausule = where.ToString();
            return paramList;
        }
    }
}
