using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLib.Core
{
    /// <summary>
    /// Represent a filter expression later translated to SQL where clausule.
    /// </summary>
    public class DataFilterExpressionDB
    {
        public enum _FilterType { Contains, Equal }
        public string FieldName { get; set; }
        public string Filter { get; set; }
        public _FilterType FilterType { get; set; }
        /*
        private string _FilterExpressionType { get; set; }
        public string FilterExpressionType 
        { 
            get { return _FilterExpressionType; }
            set 
            { 
                _FilterExpressionType = value;
                try
                {
                    if (value.ToLower() == _FilterType.Contains.ToString().ToLower())
                    {
                        FilterType = _FilterType.Contains;
                    }
                    else
                    if (value.ToLower() == _FilterType.Equal.ToString().ToLower())
                    {
                        FilterType = _FilterType.Equal;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Can't convert " + value + " 'FilterType' " + ex.Message);
                }
            }
        }*/

        /*public int RegionID
        {
            get { return _RegionID; }
            set { _RegionID = value; }
        }
        private string _RegionDescription;*/
    }

    public class DataFilterExpressionHelper
    {
        public string ConvertToWhereClausule(List<DataFilterExpressionDB> dataFilterExpressionList/*, Type type*/)
        {
            string whereClausule = " 1=1 ";
            /*foreach (var item in dataFilterExpressionList)
            {
                if (item.FilterType == DataFilterExpression._FilterType.Equal)
                {
                    if (whereClausule == "")
                        whereClausule += item.FieldName +  " = " + item.Filter
                    
                    whereClausule += " and "

                }
            }*/

            return whereClausule;
        }
    }
}
