using System;
using System.Collections.Generic;
using System.Text;

namespace MyAppRESTLib
{
    public class GenericRESTFilter
    {
        /// <summary>
        /// Works likr "TOP" in sql server. Limit the number of registers to retrieve
        /// </summary>
        public int? NumberOfRows { get; set; }

        /// <summary>
        /// Skip X registers
        /// </summary>
        public int? NumberOfRowsToSkip { get; set; }
    }
}
