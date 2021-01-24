using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Data;
using System;
using System.Text;

namespace MyAppDataAccessLib
{
    /// <summary>
    /// Paramters used during a "select distinct" SQL
    /// </summary>
    public class GetDistinctParameters
    {
        /// <summary>
        /// Filter aplied on distinc query
        /// </summary>
        public string Filter = "";

        /// <summary>
        /// By default, bring top 10 values.
        /// </summary>
        public int TopRows = 10;

        /// <summary>
        /// Where MyColumn is not null
        /// </summary>
        public bool IgnoreNullValues = true;

        /// <summary>
        /// Filter aplied to the query.
        /// </summary>
        public FilterType FilterToApply = FilterType.NoFilter;

        /// <summary>
        /// Order by applied asc/dsc
        /// </summary>
        public OrdeByType OrderBy = OrdeByType.Ascending;

        /// <summary>
        /// Order by applied asc/dsc
        /// </summary>
        public enum OrdeByType { Ascending, Descending }

        /// <summary>
        /// Filter aplied to the query.
        /// </summary>
        public enum FilterType { StartWith, Contains, EndsWith, NoFilter }
    }
}

namespace MyAppDataAccessLib
{
    

    public class MyDistinctData
    {
        public string Result { get; set; }
    }

    /// <summary>
    /// Permits access to database in a generic way.
    /// </summary>
    public class GenericDAO
    {
        public GenericDAO(Motor motor)
        {
            this.motor = motor;
        }

        public GenericDAO(string provider, string connectionStringSystem)
        {
            Motor motor = new Motor(provider, connectionStringSystem);
            motor.OpenConnection();
            this.motor = motor;
        }

        private Motor motor = null;

        //Example:
        //string connectionString = "Data Source=FREDDYWIN81;Initial Catalog=Northwind;Integrated Security=True;";            
        //Motor m = new Motor(connectionString);
        //m.OpenConnection();
        //GenericDAO d = new GenericDAO(m);
        //List<DistinctData> l1 = new List<DistinctData>();
        //l1 = (List<DistinctData>)d.GetData("select 'abc' as MyData", typeof(DistinctData));
        //MessageBox.Show(l1[0].MyData);

        public List<MyDistinctData> GetSimpleDistinctData(string tableName, string column)
        {
            string query = string.Format("select distinct({0}) as Result from {1}", column, tableName);
            List<MyDistinctData> list = (List<MyDistinctData>)GetData(query, typeof(MyDistinctData));
            return list;
        }

        public List<string> GetDistinctData(string tableName, string column)
        {
            return GetDistinctData(tableName, column, null);
        }

        public List<string> GetDistinctData(string tableName, string column, GetDistinctParameters parameters)
        {
            string rawQuery = "select distinct {0} ({1}) as Result from {2} {3} order by 1 {4}";
            string query = "";
            if (parameters == null)
            {
                query = string.Format(rawQuery, "", column, tableName, "", "ASC");
            }
            else
            {
                string topRows = " top " + parameters.TopRows.ToString();
                string where = "";
                switch (parameters.FilterToApply)
                {
                    case GetDistinctParameters.FilterType.NoFilter:
                        break;
                    case GetDistinctParameters.FilterType.Contains:
                        where = string.Format("where {0} like ('%{1}%')", column, parameters.Filter);
                        break;
                    case GetDistinctParameters.FilterType.StartWith:
                        where = string.Format("where {0} like ('{1}%')", column, parameters.Filter);
                        break;
                    case GetDistinctParameters.FilterType.EndsWith:
                        where = string.Format("where {0} like ('%{1}')", column, parameters.Filter);
                        break;
                }
                query = string.Format(rawQuery, topRows, column, tableName, where, "ASC");
            }
            //string query = string.Format("select distinct({0}) as Result from {1} order by 1", column, tableName);
            List<string> myData = GetDataAsString(query);
            return myData;
        }

        public long GetMaxPlusOne(string column, string tableName)
        {
            string query = string.Format("select (max({0}))+1 as Result from {1} ",column,  tableName);
            //string query = string.Format("select distinct({0}) as Result from {1} order by 1", column, tableName);
            List<string> myData = GetDataAsString(query);
            long result = long.Parse(myData[0]);
            return result;
        }

        /// <summary>
        /// Give me one "SQL query" and one "class type" and return a list<class type>        
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <param name="type">Type with fields that mach query</param>
        /// <returns>List of data</returns>
        public IList GetData(string query, Type type)
        {
            Type customList = typeof(List<>).MakeGenericType(type);
            IList objectList = (IList)Activator.CreateInstance(customList);

            motor.CommandText = query;
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(type, dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    var obj = Activator.CreateInstance(type);
                    //classFiller.FillField(obj);
                    classFiller.Fill(obj);
                    objectList.Add(obj);
                }
            }

            //motor.CloseConnection();

            return objectList;
        }

        public List<string> GetDataAsString(string query)
        {
            List<string> objectList = new List<string>();

            motor.CommandText = query;
            DbDataReader dbReader = motor.ExecuteReader();
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    string row = "";
                    for (int i = 0; i < dbReader.FieldCount; i++)
                    {
                        row += dbReader[i].ToString();
                        if ((dbReader.FieldCount - i) > 1)
                            row += ";";
                    }
                    objectList.Add(row);
                }
            }

            return objectList;
        }
    }
}
