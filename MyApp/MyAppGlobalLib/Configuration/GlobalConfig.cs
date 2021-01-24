using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyAppGlobalLib
{
    public class GlobalConfiguration
    {
        /// <summary>
        /// Used in REST project
        /// </summary>
        public RESTConfig RESTConfig { get; set; }

        public UnitTestConfig UnitTestConfig { get; set; }

        public WPFConfig WPFConfig { get; set; }
    }

    public class RootObject
    {
        public GlobalConfiguration GlobalConfiguration { get; set; }
    }       

    /// <summary>
    /// Global config file for all solution EXCEPT MyAppXUnitTestLib that uses this class but loads "LocalConfiguration.json"
    /// </summary>
    public class GlobalConfigReader
    {
        /// <summary>
        /// for now is static. ToDo: read from json
        /// </summary>
        public GlobalConfigReader()
        {
            this.ConnectionString = new List<MyConnection>();
        }


        public GlobalConfiguration Load(string jsonConfig)
        {
            string text = File.ReadAllText(jsonConfig);
            RootObject config = JsonConvert.DeserializeObject<RootObject>(text);
            return config.GlobalConfiguration;
        }

        public GlobalConfiguration Load()
        {
            string jsonConfig = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\GlobalConfiguration.json";
            return Load(jsonConfig);
        }

        public void Save(GlobalConfiguration globalConfiguration)
        {
            RootObject rootObject = new RootObject();
            rootObject.GlobalConfiguration = globalConfiguration;
            string fileToSave = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\GlobalConfiguration.json";
            string jsonAsString = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
            System.IO.File.WriteAllText(fileToSave, jsonAsString);
        }


        public List<MyConnection> ConnectionString { get; set; }

        public string GetConnectionString()
        {
#if DEBUG
            MyConnection path = ConnectionString.Where(x => x.Name == GlobalEnums.ConnectionString.LocalDebug.ToString()).FirstOrDefault();
            return path.Value;
#endif
        }

        public string GetConnectionString(GlobalEnums.ConnectionString connectionString)
        {
            MyConnection path = ConnectionString.Where(x => x.Name == connectionString.ToString()).FirstOrDefault();
            return path.Value;
        }
    }
}
