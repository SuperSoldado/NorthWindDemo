using System;
using System.IO;
using System.Reflection;
using System.Linq;

namespace MyAppXUnitTestLib
{
    public class MyDatabaseFixture : IDisposable
    {
        public MyAppGlobalLib.GlobalConfiguration xUnitTestConfig;
        public string BaseFilePath { get; set; }
        public MyDatabaseFixture()
        {
            /*BaseFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string configFile = BaseFilePath + "\\LocalConfiguration.json";

            MyAppGlobalLib.GlobalConfigReader myConfig = new MyAppGlobalLib.GlobalConfigReader();
            xUnitTestConfig = myConfig.Load(configFile);

            //Default connection string
            var defaultConnection = xUnitTestConfig.UnitTestConfig.ConnectionString.Where(x => x.Name == "DefaultConnectionString").FirstOrDefault();

            //Reset DB connection String
            var resetDB = xUnitTestConfig.UnitTestConfig.ConnectionString.Where(x => x.Name == "ResetDatabaseConnectionString").FirstOrDefault();

            string resetDBConnectionString = resetDB.Value;
            string dbScriptFile = Path.Join(BaseFilePath, defaultConnection.File);

            //string error = ResetDatabank(unitTestConnectionString);
            string error = null;
            DabaseReset dbReset = new DabaseReset();
            error = dbReset.ResetDatabank(resetDBConnectionString, dbScriptFile);

            if (error != null)
            {
                throw new Exception(error);
            }*/
        }

        public void Dispose()
        {
            // ... clean up test data from the database ...
        }
    }

    public class BaseTest
    {
        public MyAppGlobalLib.GlobalConfiguration xUnitTestConfig;
        public string BaseFilePath { get; set; }
        public BaseTest()
        {
            BaseFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string configFile = BaseFilePath + "\\LocalConfiguration.json";

            MyAppGlobalLib.GlobalConfigReader myConfig = new MyAppGlobalLib.GlobalConfigReader();
            xUnitTestConfig = myConfig.Load(configFile);

            //Default connection string
            var defaultConnection = xUnitTestConfig.UnitTestConfig.ConnectionString.Where(x => x.Name == "DefaultConnectionString").FirstOrDefault();

            //Reset DB connection String
            var resetDB = xUnitTestConfig.UnitTestConfig.ConnectionString.Where(x => x.Name == "ResetDatabaseConnectionString").FirstOrDefault();

            string resetDBConnectionString = resetDB.Value;
            string dbScriptFile = Path.Join(BaseFilePath, defaultConnection.File);            

        }

    }
}
