using System;
using System.Collections.Generic;
using System.Text;

namespace RESTLib.Core
{
    public class ReportPostHeader
    {
        public ReportPostHeader()
        {
            TransactionID = null;
        }

        public string TransactionID { get; set; }
        public string Message { get; set; }
    }    

    /// <summary>
    /// Generic response for most HTTP PostRequests
    /// </summary>
    public class GeneralPostResponse
    {
        public GeneralPostResponse()
        {
            this.ReportPostHeader = new ReportPostHeader();
        }

        public ReportPostHeader ReportPostHeader { get; set; }

        //Freddy: Future eventual use. Disabled for now. Can be used whern the post result in generic output data.
        //public Object Data { get; set; }
    }
}
