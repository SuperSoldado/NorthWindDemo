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
    /// This class is used for DAO classes to fill the "SomethingInfo" with the data reader. 
    /// How it works: it maps the query "Select col1,col2,col3 from table1" to the info class. So, there must be
    /// a "Table1" class to do "Table1.col1 = (int)reader["col1"]"  Supposing it is a "int" type.
    /// </summary>
    public class ClassFiller
    {
        /// <summary>
        /// Reader used to get the data.
        /// </summary>
        private DbDataReader dbReader;

        /// <summary>
        /// List the columns used in the "select a,b,c..." 
        /// </summary>
        private List<string> ListColumnsInQuery = null;

        /// <summary>
        /// List of properties that the class has, but the query don't. 
        /// </summary>
        List<string> ListDontProcess = null;

        /// <summary>
        /// List of properties that the class has.
        /// </summary>
        PropertyInfo[] ListClassInfoProperties = null;

        private Type Type = null;

        

        public ClassFiller(Type ClassInfoType, DbDataReader dbReader)
        {
            this.Type = ClassInfoType;
            this.dbReader = dbReader;
            ListColumnsInQuery = GetColumnsInsideQuery(dbReader);
            ListClassInfoProperties = ClassInfoType.GetProperties();
            ListDontProcess = GetDontProcess(ListColumnsInQuery, ListClassInfoProperties);
        }
        ///// <summary>
        ///// Contains the ist of properties (Ex.: MyClass.MyProperty1) that are not in the reader object. 
        ///// Sometimes some properties are not in database and are for calculations or other purposes.
        ///// </summary>
        //private List<string> DontProcess = new List<string>();
        //private List<string> ColumnsInQuery = new List<string>();
        private List<string> GetDontProcess(List<string> ColumnsInQuery, System.Reflection.PropertyInfo[] properties)
        {
            List<string> DontProcess = new List<string>();
            foreach (PropertyInfo property in properties)
            {
                bool found = false;
                foreach (string s in ColumnsInQuery)
                {
                    if (s.ToUpper() == property.Name.ToUpper())
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    DontProcess.Add(property.Name);
                }
            }
            return DontProcess;
        }

        /// <summary>
        /// Fill a basic "Info" class with the data in DataReader.
        ///
        /// WARNING: Due to reflection process, the "ClassFiller" class may result overwhelming CPU process 
        /// Should consider dump the data using static access. How replace classFiller: comment the line  
        /// "classFiller.Fill(MyClassInfo);" and manually add the parameters. Example:
        /// myClassInfoOvr = new myClassInfoOvr();
        /// myClassInfoOvr.MyNumber =  (int)dbReader["MyNumber"];
        /// </summary>             
        /// <param name="classInfo">Class to be filled</param>        
        /// <param name="reader">Data Reader with all the data</param>
        public void Fill(object classInfo/*, DbDataReader reader*/)
        {
            try
            {
                foreach (PropertyInfo property in ListClassInfoProperties)
                {
                    if (!PropertyExistsInReader(property.Name))
                        continue;

                    //New code: for nullable stuff... damn, this sucks!
                    Type t = property.PropertyType;
                    if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        t = t.GetGenericArguments()[0];
                    }

                    if (t == typeof(String))
                    {
                        FillString(classInfo, property, dbReader);
                        continue;
                    }

                    if (t == typeof(Int16))
                    {
                        FillShort(classInfo, property, dbReader);
                        continue;
                    }

                    if (t == typeof(Int32))
                    {
                        FillInt(classInfo, property, dbReader);
                        continue;
                    }

                    if (t == typeof(Int64))
                    {
                        FillInt64(classInfo, property, dbReader);
                        continue;
                    }

                    if (t == typeof(Decimal))
                    {
                        FillDecimal(classInfo, property, dbReader);
                        continue;
                    }

                    if (t == typeof(Double))
                    {
                        FillDouble(classInfo, property, dbReader);
                        continue;
                    }

                    if (t == typeof(DateTime))
                    {
                        FillDateTime(classInfo, property, dbReader);
                        continue;
                    }

                    if (t == typeof(bool))
                    {
                        FillBool(classInfo, property, dbReader);
                        continue;
                    }

                    if (t == typeof(Guid))
                    {
                        FillGuid(classInfo, property, dbReader);
                        continue;
                    }

                    if (t == typeof(byte[]))
                    {
                        FillByteArray(classInfo, property, dbReader);
                        continue;
                    }

                    throw new Exception("Type whithout implementation: " + property.Name + " at ClassFiller.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error during parse Database->Class: " + ex.Message);
            }
        }

        public void FillField(object classInfo)
        {
            try
            {
                FieldInfo[] fields = this.Type.GetFields();

                foreach (FieldInfo field in fields)
                {
                    if (!PropertyExistsInReader(field.Name))
                        continue;

                    //New code: for nullable stuff... damn, this sucks!
                    Type t = field.FieldType;
                    if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        t = t.GetGenericArguments()[0];
                    }

                    if (t == typeof(String))
                    {
                        FillString(classInfo, field, dbReader);
                        continue;
                    }

                    if (t == typeof(Int32))
                    {
                        FillInt(classInfo, field, dbReader);
                        continue;
                    }

                    if (t == typeof(Int64))
                    {
                        FillInt64(classInfo, field, dbReader);
                        continue;
                    }

                    if (t == typeof(Decimal))
                    {
                        FillDecimal(classInfo, field, dbReader);
                        continue;
                    }

                    if (t == typeof(Double))
                    {
                        FillDouble(classInfo, field, dbReader);
                        continue;
                    }

                    if (t == typeof(DateTime))
                    {
                        FillDateTime(classInfo, field, dbReader);
                        continue;
                    }

                    if (t == typeof(bool))
                    {
                        FillBool(classInfo, field, dbReader);
                        continue;
                    }

                    if (t == typeof(byte[]))
                    {
                        FillByteArray(classInfo, field, dbReader);
                        continue;
                    }

                    if (t == typeof(Guid))
                    {
                        FillGuid(classInfo, field, dbReader);
                        continue;
                    }

                    throw new Exception("Type whithout implementation: " + field.Name + " at ClassFiller.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error during parse Database->Class: " + ex.Message);
            }
        }

        /// <summary>
        /// Find is the myClassInfo.MyProperty existis inside a reader (select MyProperty,XPTO from myClass)
        /// </summary>
        /// <param name="dbReader">Reader used</param>
        /// <param name="nameInDataReader">Name of the column insede reader.</param>
        /// <returns></returns>
        public bool PropertyExistsInReader(string nameInDataReader)
        {
            foreach (string s in ListDontProcess)
            {
                if (s == nameInDataReader)
                    return false;
            }
            return true;
        }


        private List<string> GetColumnsInsideQuery(DbDataReader dbReader)
        {
            System.Data.DataTable schemaTable;
            //Retrieve column schema into a DataTable.
            schemaTable = dbReader.GetSchemaTable();
            List<string> dbReaderColumns = new List<string>();
            foreach (System.Data.DataRow myField in schemaTable.Rows)
            {
                //For each property of the field...
                foreach (System.Data.DataColumn myProperty in schemaTable.Columns)
                {
                    //Display the field name and value.
                    if (myProperty.ColumnName == "ColumnName")
                    {
                        dbReaderColumns.Add(myField[myProperty].ToString());
                        break;
                    }
                }
            }
            return dbReaderColumns;
        }

        private void FillInt(object classInfo, PropertyInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                property.SetValue(classInfo, (int)reader[property.Name]);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        private void FillShort(object classInfo, PropertyInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                property.SetValue(classInfo, (short)reader[property.Name]);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        private void FillGuid(object classInfo, PropertyInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                property.SetValue(classInfo, (Guid)reader[property.Name]);
            }
            else
            {
                property.SetValue(classInfo, Guid.Empty);
            }
        }

        private void FillBool(object classInfo, PropertyInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                property.SetValue(classInfo, (bool)reader[property.Name]);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        private void FillByteArray(object classInfo, PropertyInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                property.SetValue(classInfo, (byte[])reader[property.Name]);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        private void FillInt64(object classInfo, PropertyInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                property.SetValue(classInfo, (Int64)reader[property.Name]);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        private void FillDecimal(object classInfo, PropertyInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                //When comes a Double value from Reader (Ex.: when theres a column like "float" in SQlServer) must convert. 
                //Could not make work dynamic cast.... :-(
                decimal z = Convert.ToDecimal(reader[property.Name]);
                property.SetValue(classInfo, z);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        private void FillDouble(object classInfo, PropertyInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                //When comes a Double value from Reader (Ex.: when theres a column like "float" in SQlServer) must convert. 
                //Could not make work dynamic cast.... :-(
                double z = Convert.ToDouble(reader[property.Name]);
                property.SetValue(classInfo, z);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        private void FillDateTime(object classInfo, PropertyInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                property.SetValue(classInfo, (DateTime)reader[property.Name]);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        private void FillString(object classInfo, PropertyInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                property.SetValue(classInfo, (string)reader[property.Name]);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        //-----
        private void FillInt(object classInfo, FieldInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                property.SetValue(classInfo, (int)reader[property.Name]);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        private void FillBool(object classInfo, FieldInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                property.SetValue(classInfo, (bool)reader[property.Name]);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        private void FillByteArray(object classInfo, FieldInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                property.SetValue(classInfo, (byte[])reader[property.Name]);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        private void FillInt64(object classInfo, FieldInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                property.SetValue(classInfo, (Int64)reader[property.Name]);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        private void FillDecimal(object classInfo, FieldInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                //When comes a Double value from Reader (Ex.: when theres a column like "float" in SQlServer) must convert. 
                //Could not make work dynamic cast.... :-(
                decimal z = Convert.ToDecimal(reader[property.Name]);
                property.SetValue(classInfo, z);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        private void FillGuid(object classInfo, FieldInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                //When comes a Double value from Reader (Ex.: when theres a column like "float" in SQlServer) must convert. 
                //Could not make work dynamic cast.... :-(
                Guid z = (Guid)reader[property.Name];
                property.SetValue(classInfo, z);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        private void FillDouble(object classInfo, FieldInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                //When comes a Double value from Reader (Ex.: when theres a column like "float" in SQlServer) must convert. 
                //Could not make work dynamic cast.... :-(
                double z = Convert.ToDouble(reader[property.Name]);
                property.SetValue(classInfo, z);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        private void FillDateTime(object classInfo, FieldInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                property.SetValue(classInfo, (DateTime)reader[property.Name]);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }

        private void FillString(object classInfo, FieldInfo property, DbDataReader reader)
        {
            if (!(reader[property.Name] is System.DBNull))
            {
                property.SetValue(classInfo, (string)reader[property.Name]);
            }
            else
            {
                property.SetValue(classInfo, null);
            }
        }
    }
}
