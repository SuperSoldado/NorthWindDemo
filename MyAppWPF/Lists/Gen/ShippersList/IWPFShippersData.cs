//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFList.Shippers
{
    public interface IWPFShippersDataConnection
    {
        public ShippersDataContext GetDataContext(out string error);
        List<ModelNotifiedForShippers> GetAllShippers(out string error);
        public void SaveData(ModelNotifiedForShippers modelNotifiedForShippers, out string error);
        public void DeleteData(ModelNotifiedForShippers modelNotifiedForShippers, out string error);
        public void AddData(ModelNotifiedForShippers modelNotifiedForShippers, out string error);

    }
}

