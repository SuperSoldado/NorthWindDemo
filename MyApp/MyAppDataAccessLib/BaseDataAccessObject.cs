using System;
using System.Collections.Generic;
using System.Text;

namespace MyAppDataAccessLib
{
    public class BaseDataAccessObject
    {
        public string GetFilteredRowNumAndSkipQuery(string tableName, string orderBy, int numberOfRowsToSkip, int numberOfRows)
        {
            string baseQuery = string.Format("select top {0} MyQuery.* from " +
                               "(SELECT ROW_NUMBER() over(ORDER BY {1}) as rownum,* FROM {2}) MyQuery" +
                               " where rownum > {3}", numberOfRows, orderBy, tableName, numberOfRowsToSkip);
            return baseQuery;
        }
    }
}
