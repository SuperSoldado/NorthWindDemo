using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTLib.Core
{
    /// <summary>
    /// Base class used in RESTMyTableNameDB. Contains Generic functionsS
    /// </summary>
    public class RESTBaseDB
    {

        public System.Collections.IList FilterRawList(System.Collections.IList rawList, GeneralBodyGet GeneralBodyGet, out RestExceptionError error)
        {
            try
            {
                error = null;
                if (GeneralBodyGet == null || GeneralBodyGet.Filters == null)
                {
                    return rawList;
                }
                HelperRestFilter helperRestFilter = new HelperRestFilter();
                List<object> filteredList = new List<object>();
                Type listType = rawList[0].GetType();

                foreach (object item in rawList)
                {
                    bool ignore = helperRestFilter.Ignore(GeneralBodyGet.Filters, listType, item);
                    if (!ignore)
                    {
                        filteredList.Add(item);
                    }
                }

                return filteredList;
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Error in filtering";
                error.ExceptionMessage = ex.Message;
            }
            return null;
        }
    }
}
