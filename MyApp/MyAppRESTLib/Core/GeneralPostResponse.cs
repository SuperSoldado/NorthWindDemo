using System;
using System.Collections.Generic;
using System.Text;

namespace RESTLib.Core
{
    public class ReportHeaderGET
    {
        public ReportHeaderGET()
        {
            TotalItensAvailable = int.MinValue;
            TotalItensRetrieved = int.MinValue;
            TransactionID = null;
            MessageFromServer = null;
        }

        public string TransactionID { get; set; }
        public int TotalItensAvailable { get; set; }
        public int TotalItensRetrieved { get; set; }
        public string MessageFromServer { get; set; }
    }    

    /// <summary>
    /// Generic response for most HTTP GetRequests
    /// </summary>
    public class GeneralGetResponse
    {
        public GeneralGetResponse()
        {
            this.ReportHeader = new ReportHeaderGET();
        }

        public ReportHeaderGET ReportHeader { get; set; }

        public Object Data { get; set; }
    }
}
