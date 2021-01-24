//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFForms.Shippers
{
    public interface IWPFShippersDataConnection
    {
        public ShippersDataContext GetDataContext(int ShipperID,out string error);
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public ShippersDataContext GetEmptyDataContext(out string error);
        ModelNotifiedForShippers GetShippersByID(int ShipperID, out string error);
        public void SaveData(ModelNotifiedForShippers modelNotifiedForShippers, out string error);
        public void DeleteData(ModelNotifiedForShippers modelNotifiedForShippers, out string error);
        public void AddData(ModelNotifiedForShippers modelNotifiedForShippers, out string error);

    }
}

