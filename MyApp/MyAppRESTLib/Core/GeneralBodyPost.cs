using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTLib.Core
{

    /// <summary>
    /// Represents generic body used for post data
    /// </summary>
    public class GeneralBodyPost
    {
        public GeneralBodyPost()
        {
        }
        
        /// <summary>
        /// Filtering list
        /// </summary>
        public object Data { get; set; }
    }
}
