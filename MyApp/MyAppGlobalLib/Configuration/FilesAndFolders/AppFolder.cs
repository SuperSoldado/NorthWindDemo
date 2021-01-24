using System;
using System.Collections.Generic;
using System.Text;

namespace MyAppGlobalLib
{
    /// <summary>
    /// Define a folder used in the system.
    /// </summary>
    public class AppFolder
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public GlobalEnums.AppFolderType AppFolderType { get; set; }
    }
}
