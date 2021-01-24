using DataAccessLib.Core;
using RESTLib.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RESTLib.Core
{
    public static class HelperRESTFilterToDB
    {
        public static List<DataFilterExpressionDB> FilterRestFilterToDBExpression(List<DataFilterExpressionREST> dataFilterExpressionREST)
        {
            List<DataFilterExpressionDB> dbFilters = new List<DataFilterExpressionDB>();
            foreach (var item in dataFilterExpressionREST)
            {
                DataFilterExpressionDB dbFilter = new DataFilterExpressionDB();
                dbFilter.FieldName = item.FieldName;
                dbFilter.Filter = item.Filter;
                switch (item.FilterType)
                {
                    case DataFilterExpressionREST._FilterType.Equal:
                        dbFilter.FilterType = DataFilterExpressionDB._FilterType.Equal;
                        break;
                    case DataFilterExpressionREST._FilterType.Contains:
                        dbFilter.FilterType = DataFilterExpressionDB._FilterType.Contains;
                        break;
                    case DataFilterExpressionREST._FilterType.Dynamic:
                        continue;
                    default:
                        break;
                }
                dbFilters.Add(dbFilter);
            }

            return dbFilters;
        }
    }
}
