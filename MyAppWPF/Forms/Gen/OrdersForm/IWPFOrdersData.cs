//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFForms.Orders
{
    public interface IWPFOrdersDataConnection
    {
        public OrdersDataContext GetDataContext(int OrderID,out string error);
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public OrdersDataContext GetEmptyDataContext(out string error);
        ModelNotifiedForOrders GetOrdersByID(int OrderID, out string error);
        public void SaveData(ModelNotifiedForOrders modelNotifiedForOrders, out string error);
        public void DeleteData(ModelNotifiedForOrders modelNotifiedForOrders, out string error);
        public void AddData(ModelNotifiedForOrders modelNotifiedForOrders, out string error);

        //test
        public List<ModelNotifiedForCustomers> GetAll_Customers(out string error);
        //test
        public List<ModelNotifiedForEmployees> GetAll_Employees(out string error);
        //test
        public List<ModelNotifiedForShippers> GetAll_Shippers(out string error);
    }
}

