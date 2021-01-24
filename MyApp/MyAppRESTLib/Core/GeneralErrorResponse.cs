using System;
using System.Collections.Generic;
using System.Text;

namespace RESTLib.Core
{
    public class GeneralErrorResponseContainer
    {
        public string Warning { get; set; }
        public GeneralErrorResponse ErrorResponse { get; set; }
    }

    public interface IGeneralErrorResponse_obsolete
    {
        public string TransactionID { get; set; }
        public string Route { get; set; }
        public string Error { get; set; }
        public string ErrorInternal { get; set; }
    }

    public class GeneralErrorResponse 
    {
        public string TransactionID { get; set; }
        public string Route { get; set; }
        public string Error { get; set; }
        public string ErrorInternal { get; set; }
    }

    /// <summary>
    /// Reports internal error
    /// </summary>
    public class General_500ErrorResponse_obsolete : GeneralErrorResponse
    {

    }

    /// <summary>
    /// Reports PreConditionFailed
    /// </summary>
    public class General_412ErrorResponse_obsolete : GeneralErrorResponse
    {

    }
}
