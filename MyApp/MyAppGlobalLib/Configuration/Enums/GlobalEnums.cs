using System;
using System.Collections.Generic;
using System.Text;

namespace MyAppGlobalLib
{
    public class GlobalEnums
    {
        public enum ConnectionString { LocalDebug, LocalUnitTest, Stage, TeamCityMain, Production }

        /// <summary>
        /// Defines how a directory is configured
        /// </summary>
        public enum AppFolderType
        {
            /// <summary>
            /// Relative path from another base path. Ex.: \mySubsolderA\\mySubsolderB\
            /// </summary>
            Relative,

            /// <summary>
            /// Complete file system path. Ex.: c:\MyFolder\MySubFolder
            /// </summary>
            Fixed,

            /// <summary>
            /// Path having place holders like "c:\MyPath\{0}\MySubFolder"
            /// </summary>
            Dynamic
        }

        /// <summary>
        /// Used in wpf frmDisplayBynaryData to show data in a pop up.
        /// </summary>
        public enum MimeTypes
        {
            NotDefined, Image, Pdf, Text
        }

        public enum DefaultAppFolders
        {
            /// <summary>
            /// Temporary folder in local system
            /// </summary>
            Temp,
            
            /// <summary>
            /// Path to WPFLists translations
            /// </summary>
            WPFListLanguage,
            
            /// <summary>
            /// Path to WPFForms translations
            /// </summary>
            WPFFormLanguage
        }
    }
}
