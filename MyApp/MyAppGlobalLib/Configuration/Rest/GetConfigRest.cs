using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace MyAppGlobalLib
{
    /// <summary>
    /// Singlton REST config
    /// </summary>
    public class GetConfigRest : IGetConfigRest
    {
        public  RESTConfig RESTConfig = null;
        private static readonly GetConfigRest _GetConfigRest = new GetConfigRest();

        public static GetConfigRest GetInstance() => _GetConfigRest;

        public GetConfigRest()
        {
            string configFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\GlobalConfiguration.json";
            GlobalConfigReader globalConfigReader = new GlobalConfigReader();
            RESTConfig = globalConfigReader.Load(configFile).RESTConfig;
        }
    }

    public interface IGetConfigRest
    {
    }
}
