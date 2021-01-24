using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using MyAppXUnitTestLib;

namespace MyAppXUnitTest.Fixture
{
    public class DatabaseFixture : IDisposable
    {
        public MyAppGlobalLib.GlobalConfiguration xUnitTestConfig;
        public string BaseFilePath { get; set; }
        public DatabaseFixture()
        {
            BaseFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string configFile = BaseFilePath + "\\LocalConfiguration.json";

            MyAppGlobalLib.GlobalConfigReader myConfig = new MyAppGlobalLib.GlobalConfigReader();
            xUnitTestConfig = myConfig.Load(configFile);

            //Default connection string
            var defaultConnection = xUnitTestConfig.UnitTestConfig.ConnectionString.Where(x => x.Name == xUnitTestConfig.UnitTestConfig.MainConnectionString).FirstOrDefault();

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
            }
            // ... initialize data in the test database ...
        }

        public void Dispose()
        {
            // ... clean up test data from the database ...
        }
    }
}
