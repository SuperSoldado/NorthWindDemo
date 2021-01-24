//Track[0007] WPF_Shared_REST.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using RESTLib.Core;
using MyApp.TransferObjects.REST;

namespace MyApp.WPFList.Orders
{
    public partial class WPFOrdersRest : IWPFOrdersDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFOrdersRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }


        public OrdersDataContext GetDataContext(out string error)
        {
            OrdersDataContext dataContext = new OrdersDataContext();
            error = null;
            dataContext.modelNotifiedForOrdersMain = GetAllOrders(out error);
            dataContext.modelNotifiedForCustomers = GetAll_Customers(out error);
            dataContext.modelNotifiedForEmployees = GetAll_Employees(out error);
            dataContext.modelNotifiedForShippers = GetAll_Shippers(out error);
            dataContext.PopulateComboBoxesItemSource();


            return dataContext;
        }

        public void SaveData(ModelNotifiedForOrders modelNotifiedForOrders, out string error)
        {
            OrdersGenericREST OrdersGenericREST = new OrdersGenericREST(wpfConfig);
            UpdateOrdersView updateOrdersView = new UpdateOrdersView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForOrders), modelNotifiedForOrders, typeof(UpdateOrdersView), updateOrdersView);
            OrdersGenericREST.Update(updateOrdersView, out error);

        }
        
        public void AddData(ModelNotifiedForOrders modelNotifiedForOrders, out string error)
        {
            OrdersGenericREST OrdersGenericREST = new OrdersGenericREST(wpfConfig);
            CreateOrdersView createOrdersView = new CreateOrdersView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForOrders), modelNotifiedForOrders, typeof(CreateOrdersView), createOrdersView);
            OrdersGenericREST.Insert(createOrdersView, out error);
        }

        public void DeleteData(ModelNotifiedForOrders modelNotifiedForOrders, out string error)
        { 
            OrdersGenericREST OrdersGenericREST = new OrdersGenericREST(wpfConfig);
            DeleteOrdersView deleteOrdersView = new DeleteOrdersView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForOrders), modelNotifiedForOrders, typeof(DeleteOrdersView), deleteOrdersView);
            OrdersGenericREST.Delete(deleteOrdersView, out error);
        }

        public List<ModelNotifiedForOrders> GetAllOrders(out string error)
        {
            OrdersGenericREST OrdersGenericREST = new OrdersGenericREST(wpfConfig);
            List<ModelNotifiedForOrders> modelNotifiedForOrders = OrdersGenericREST.GetAll<ModelNotifiedForOrders>(100, 0, out error);

            if (!string.IsNullOrEmpty(error))
            {
                return null;
            }
    
            //Initializing row status
            foreach (var item in modelNotifiedForOrders)
            {
                item.ItemChanged = false;
                item.NewItem = false;
            }
    
            return modelNotifiedForOrders;
        }
        
        /// <summary>
        /// Get all data to fill combo box
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<ModelNotifiedForCustomers> GetAll_Customers(out string error)
        {
            CustomersGenericREST CustomersGenericREST = new CustomersGenericREST(wpfConfig);
            List<ModelNotifiedForCustomers> modelNotifiedForCustomers = CustomersGenericREST.GetAll<ModelNotifiedForCustomers>(100, 0, out error);
            return modelNotifiedForCustomers;
        }
        /// <summary>
        /// Get all data to fill combo box
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<ModelNotifiedForEmployees> GetAll_Employees(out string error)
        {
            EmployeesGenericREST EmployeesGenericREST = new EmployeesGenericREST(wpfConfig);
            List<ModelNotifiedForEmployees> modelNotifiedForEmployees = EmployeesGenericREST.GetAll<ModelNotifiedForEmployees>(100, 0, out error);
            return modelNotifiedForEmployees;
        }
        /// <summary>
        /// Get all data to fill combo box
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<ModelNotifiedForShippers> GetAll_Shippers(out string error)
        {
            ShippersGenericREST ShippersGenericREST = new ShippersGenericREST(wpfConfig);
            List<ModelNotifiedForShippers> modelNotifiedForShippers = ShippersGenericREST.GetAll<ModelNotifiedForShippers>(100, 0, out error);
            return modelNotifiedForShippers;
        }

    }
}

