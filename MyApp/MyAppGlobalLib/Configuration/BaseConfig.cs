using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MyAppGlobalLib
{
    public class BaseConfig : IBaseGlobalConfig, INotifyPropertyChanged
    {
        public BaseConfig()
        {
            ConnectionString = new List<MyConnection>();
            ProjectFiles = new List<ProjectFile>();
        }

        public List<ProjectFile> ProjectFiles { get; set; }

        public string MainConnectionString { get; set; }

        public List<MyConnection> ConnectionString { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

       
        private string _GetMainConnectionString {get; set;}
        public string GetMainConnectionString 
        {
            get 
            {
                if ((ConnectionString == null) || (ConnectionString.Count == 0))
                {
                    return "MainConnectionString not set";
                }
                
                string result = ConnectionString.Where(x => x.Name == this.MainConnectionString).FirstOrDefault().Value;
                return result;
            }
            set
            {
                bool found = false;
                foreach (var item in ConnectionString)
                {
                    if (item.Name == "MainConnectionString")
                    {
                        found = true;
                        item.Value = value;
                        break;
                    }
                }

                if (!found)
                {
                    MyConnection myConnection = new MyConnection();
                    myConnection.Description = "Main connection string";
                    myConnection.Value = value;
                    myConnection.Name = this.MainConnectionString;
                    _GetMainConnectionString = value;
                }                
            }
        }
    }
}
