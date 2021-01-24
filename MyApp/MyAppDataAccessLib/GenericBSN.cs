using System.Collections.Generic;
using System;

namespace MyAppDataAccessLib
{
    /// <summary>
    /// This class bridges GenericDAO and ASPX pages.
    /// </summary>
    public class GenericBSN
    {
        private Motor motor = null;
        public GenericBSN(string provider, string connectionString)
        {
            motor = new Motor(provider, connectionString);
        }

        public List<string> GetDistinctData(string table, string column, GetDistinctParameters distincParameters)
        {
            List<string> distinctList = new List<string>();
            GenericDAO d = new GenericDAO(motor);
            try
            {
                motor.OpenConnection();
                distinctList = d.GetDistinctData(table, column, distincParameters);
                motor.CloseConnection();
                return distinctList;
            }
            catch (Exception ex)
            {
                motor.CloseConnection();
                distinctList.Add(ex.Message);
            }
            return distinctList;
        }

        public List<string> GetDistinctData(string table, string column)
        {
            return GetDistinctData(table, column, null);
        }
    }
}
