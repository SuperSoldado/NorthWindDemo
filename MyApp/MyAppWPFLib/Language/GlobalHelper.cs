using MyAppGlobalLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MyAppWPFLib
{
    public static class GlobalHelper
    {
        public static GlobalConfiguration ReadConfiguration()
        {
            string configFile = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\GlobalConfiguration.json";
            GlobalConfigReader globalConfigReader = new GlobalConfigReader();
            GlobalConfiguration config = globalConfigReader.Load(configFile);
            return config;
        }
    }
}
