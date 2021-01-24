using MyAppGlobalLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using MyAppWPFLib;
using System.IO;
using System.Linq;
using static MyAppGlobalLib.GlobalEnums;

namespace MyAppWPFLib
{
    public static class LanguageHelper
    {
      public static WPFLanguage GetLanguageForForms(string language, string tableName)
        {
            GlobalConfiguration config = GlobalHelper.ReadConfiguration();
            AppFolder appFolder = config.WPFConfig.AppFolders.Where(x => x.Name == DefaultAppFolders.WPFFormLanguage.ToString()).FirstOrDefault();
            if (appFolder == null)
            {
                //Setup in "GlobalConfiguration.json" is missing
                return null;
            }

            string directory = string.Format(appFolder.Value, tableName);

            if (!Directory.Exists(directory))
            {
                //directory does not exists.
                return null;
            }

            string fileName = tableName + " " + language + ".json";
            string completeFileName = Path.Combine(directory, fileName);
            if (!File.Exists(completeFileName))
            {
                //translation file not found;
                return null;
            }

            WPFLanguageContainer container = new WPFLanguageContainer();
            WPFLanguage wPFLanguage = container.ReadFile(completeFileName);
            return wPFLanguage;
        }

        public static WPFLanguage GetLanguageForList(string language, string tableName)
        {
            GlobalConfiguration config = GlobalHelper.ReadConfiguration();
            AppFolder appFolder =  config.WPFConfig.AppFolders.Where(x => x.Name == DefaultAppFolders.WPFListLanguage.ToString()).FirstOrDefault();
            if (appFolder == null)
            {
                //Setup in "GlobalConfiguration.json" is missing
                return null;
            }

            string directory = string.Format(appFolder.Value, tableName);

            if (!Directory.Exists(directory))
            {
                //directory does not exists.
                return null;
            }

            string fileName = tableName + " " + language + ".json";
            string completeFileName = Path.Combine(directory, fileName);
            if (!File.Exists(completeFileName))
            {
                //translation file not found;
                return null;
            }

            WPFLanguageContainer container = new WPFLanguageContainer();
            WPFLanguage wPFLanguage = container.ReadFile(completeFileName);
            return wPFLanguage;
        }
    }
}
