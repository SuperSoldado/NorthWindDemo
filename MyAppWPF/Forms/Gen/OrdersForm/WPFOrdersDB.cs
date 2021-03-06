//Track[0003] Template:WPF_Shared_DB.html
using System;
using System.Collections.Generic;
using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using MyApp.TransferObjects.REST;
namespace MyApp.WPFForms.Orders
{
    public class WPFOrdersDB : IWPFOrdersDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFOrdersDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public OrdersDataContext GetDataContext(int OrderID,out string error)
        {
            OrdersDataContext dataContext = new OrdersDataContext();            
            error=null;
            dataContext.modelNotifiedForOrdersMain = GetOrdersByID(OrderID, out error);
    
            dataContext.modelNotifiedForCustomers = GetAll_Customers(out error);
            dataContext.modelNotifiedForEmployees = GetAll_Employees(out error);
            dataContext.modelNotifiedForShippers = GetAll_Shippers(out error);
            dataContext.PopulateComboBoxesItemSource();
            

            return dataContext;
        }

        
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public OrdersDataContext GetEmptyDataContext(out string error)
        {
            OrdersDataContext dataContext = new OrdersDataContext();
            error=null;
            dataContext.modelNotifiedForOrdersMain = new ModelNotifiedForOrders();
    

            return dataContext;
        }



        public ModelNotifiedForOrders GetOrdersByID(int OrderID, out string error)
        {
            error = null;
            OrdersBsn bsn = new OrdersBsn(wpfConfig);
            OrdersInfo dbItem = bsn.GetValueByID(OrderID);
            ModelNotifiedForOrders item = new ModelNotifiedForOrders();
            Cloner.CopyAllTo(typeof(OrdersInfo), dbItem, typeof(ModelNotifiedForOrders), item);
            return item;
        }
        
        
        /// <summary>
        /// Retrieve all data from Customers table. Used to fill combo box.
        /// </summary>
        /// <returns>List of Customers</returns>
        public List<ModelNotifiedForCustomers> GetAll_Customers(out string error)
        {
            error = null;
            CustomersBsn bsn = new CustomersBsn(wpfConfig);
            List<CustomersInfo> dbItems = bsn.GetAll();
            List<ModelNotifiedForCustomers> notifiedItems = new List<ModelNotifiedForCustomers>();

            foreach (CustomersInfo dbItem in dbItems)
            {
                ModelNotifiedForCustomers itemToAdd = new ModelNotifiedForCustomers();
                Cloner.CopyAllTo(typeof(CustomersInfo), dbItem, typeof(ModelNotifiedForCustomers), itemToAdd);
                notifiedItems.Add(itemToAdd);
            }
            return notifiedItems;
        }
        /// <summary>
        /// Retrieve all data from Employees table. Used to fill combo box.
        /// </summary>
        /// <returns>List of Employees</returns>
        public List<ModelNotifiedForEmployees> GetAll_Employees(out string error)
        {
            error = null;
            EmployeesBsn bsn = new EmployeesBsn(wpfConfig);
            List<EmployeesInfo> dbItems = bsn.GetAll();
            List<ModelNotifiedForEmployees> notifiedItems = new List<ModelNotifiedForEmployees>();

            foreach (EmployeesInfo dbItem in dbItems)
            {
                ModelNotifiedForEmployees itemToAdd = new ModelNotifiedForEmployees();
                Cloner.CopyAllTo(typeof(EmployeesInfo), dbItem, typeof(ModelNotifiedForEmployees), itemToAdd);
                notifiedItems.Add(itemToAdd);
            }
            return notifiedItems;
        }
        /// <summary>
        /// Retrieve all data from Shippers table. Used to fill combo box.
        /// </summary>
        /// <returns>List of Shippers</returns>
        public List<ModelNotifiedForShippers> GetAll_Shippers(out string error)
        {
            error = null;
            ShippersBsn bsn = new ShippersBsn(wpfConfig);
            List<ShippersInfo> dbItems = bsn.GetAll();
            List<ModelNotifiedForShippers> notifiedItems = new List<ModelNotifiedForShippers>();

            foreach (ShippersInfo dbItem in dbItems)
            {
                ModelNotifiedForShippers itemToAdd = new ModelNotifiedForShippers();
                Cloner.CopyAllTo(typeof(ShippersInfo), dbItem, typeof(ModelNotifiedForShippers), itemToAdd);
                notifiedItems.Add(itemToAdd);
            }
            return notifiedItems;
        }
        
        public void SaveData(ModelNotifiedForOrders modelNotifiedForOrders, out string error)
        {
            OrdersBsn bsn = new OrdersBsn(wpfConfig);
            OrdersInfo dbItem = new OrdersInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForOrders), modelNotifiedForOrders, typeof(OrdersInfo), dbItem);
            
            bsn.UpdateOne(dbItem, out error);
        }

        public void AddData(ModelNotifiedForOrders modelNotifiedForOrders, out string error)
        {
            OrdersBsn bsn = new OrdersBsn(wpfConfig);
            OrdersInfo dbItem = new OrdersInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForOrders), modelNotifiedForOrders, typeof(OrdersInfo), dbItem);
            bsn.InsertOne(dbItem, out error);
            modelNotifiedForOrders.NewItem = false;
            Cloner.CopyAllTo(typeof(OrdersInfo), dbItem, typeof(ModelNotifiedForOrders), modelNotifiedForOrders);
        }
        
        public void DeleteData(ModelNotifiedForOrders modelNotifiedForOrders, out string error)
        {
            OrdersBsn bsn = new OrdersBsn(wpfConfig);
            OrdersInfo dbItem = new OrdersInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForOrders), modelNotifiedForOrders, typeof(OrdersInfo), dbItem);
            bsn.DeleteByID(dbItem, out error);
        }
    }
}

