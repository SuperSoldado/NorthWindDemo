using Microsoft.AspNetCore.Mvc;
using MyAppGlobalLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace MyAppRESTLib
{
    public class MyCustomController : ControllerBase
    {
        public RESTConfig RESTConfig;
        public MyCustomController()
        {
            /*string configFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\GlobalConfiguration.json";
            MyAppGlobalLib.GlobalConfigReader myConfig = new MyAppGlobalLib.GlobalConfigReader();
            RESTConfig = myConfig.Load(configFile).RESTConfig;*/
            //GetConfig c = new GetConfig();
            var config = GetConfigRest.GetInstance();
            RESTConfig = config.RESTConfig;
        }

    }
}
