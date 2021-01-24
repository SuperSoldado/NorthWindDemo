using System;
using System.Collections.Generic;
using System.Text;

namespace MyAppGlobalLib
{
     public interface IBaseGlobalConfig
    {
        List<ProjectFile> ProjectFiles { get; set; }
        string MainConnectionString { get; set; }
        List<MyConnection> ConnectionString { get; set; }
        public string GetMainConnectionString { get;}
    }
}
