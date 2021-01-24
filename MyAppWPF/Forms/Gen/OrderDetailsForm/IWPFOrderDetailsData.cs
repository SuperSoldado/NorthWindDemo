//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFForms.OrderDetails
{
    public interface IWPFOrderDetailsDataConnection
    {
        public OrderDetailsDataContext GetDataContext(int OrderID,int ProductID,out string error);
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public OrderDetailsDataContext GetEmptyDataContext(out string error);
        ModelNotifiedForOrderDetails GetOrderDetailsByID(int OrderID,int ProductID, out string error);
        public void SaveData(ModelNotifiedForOrderDetails modelNotifiedForOrderDetails, out string error);
        public void DeleteData(ModelNotifiedForOrderDetails modelNotifiedForOrderDetails, out string error);
        public void AddData(ModelNotifiedForOrderDetails modelNotifiedForOrderDetails, out string error);

        //test
        public List<ModelNotifiedForOrders> GetAll_Orders(out string error);
        //test
        public List<ModelNotifiedForProducts> GetAll_Products(out string error);
    }
}

