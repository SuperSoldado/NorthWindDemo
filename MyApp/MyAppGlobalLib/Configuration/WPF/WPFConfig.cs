using System;
using System.Collections.Generic;
using System.Text;

namespace MyAppGlobalLib
{
    public class WPFConfig : BaseConfig
    {
        /// <summary>
        /// Language used to translate labels and messages in UI
        /// </summary>
        public string AppLanguage { get; set; }
        /// <summary>
        /// Defines how WPF forms retrieve data
        /// </summary>
        public enum ConnectionType { REST, SqlServer }

        /// <summary>
        /// Defines how WPF forms retrieve data
        /// </summary>
        public ConnectionType connectionType { get; set; }

        public List<AppFolder> AppFolders { get; set; }

        /// <summary>
        /// Rest base path to add requests
        /// </summary>
        public string RESTBasePath { get; set; }
    }
}
