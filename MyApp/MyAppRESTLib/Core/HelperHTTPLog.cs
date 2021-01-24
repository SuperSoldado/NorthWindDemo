using System;
using System.Collections.Generic;
using System.Text;

namespace RESTLib.Core
{
    public interface IHelperHTTPLog
    {
        public void RegisterLog(HTTPLog HTTPLog);
    }

    public class HTTPLog
    {
        public enum LogSeverity { Low, Mid, High }
        
        /// <summary>
        /// Defines if the log is from the begin(body, header, query string), middle process or exit (with status code returned)
        /// </summary>
        public enum LogType { 
            /// <summary>
            /// Register first entry point. Only registered ONCE per request
            /// </summary>
            Begin,

            /// <summary>
            /// Register what exited. Only registered ONCE per request
            /// </summary>
            Exit, 
            
            /// <summary>
            /// Logs between 'Begin' and 'End'
            /// </summary>
            MidProcess}
        public string TransactionID { get; set; }
        public string HttpBody { get; set; }
        public string HttpHeaders { get; set; }
        public string HttpQueryString { get; set; }
        public string HttpUserName { get; set; }
        public DateTime? RequestDate { get; set; }
    }

    public class HelperHTTPLog : IHelperHTTPLog
    {
        public enum WhatToLog  { All, Nothing}
        public WhatToLog _WhatToLog { get; set; }

        /// <summary>
        /// Defines what is going to be loged
        /// </summary>
        /// <param name="whatToLog">Log target</param>
        public HelperHTTPLog(WhatToLog whatToLog)
        {
            this._WhatToLog = whatToLog;
        }

        public void RegisterLog(HTTPLog HTTPLog)
        {
            if (_WhatToLog == WhatToLog.Nothing)
            {
                return;
            }

            if (_WhatToLog == WhatToLog.All)
            {
                //to do write log here;
                return;
            }
        }
    }
}
