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

namespace MyApp.WPFList.OrderDetails
{
    public partial class WPFOrderDetailsRest : IWPFOrderDetailsDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFOrderDetailsRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }


        public OrderDetailsDataContext GetDataContext(out string error)
        {
            OrderDetailsDataContext dataContext = new OrderDetailsDataContext();
            error = null;
            dataContext.modelNotifiedForOrderDetailsMain = GetAllOrderDetails(out error);
            dataContext.modelNotifiedForOrders = GetAll_Orders(out error);
            dataContext.modelNotifiedForProducts = GetAll_Products(out error);
            dataContext.PopulateComboBoxesItemSource();


            return dataContext;
        }

        public void SaveData(ModelNotifiedForOrderDetails modelNotifiedForOrderDetails, out string error)
        {
            OrderDetailsGenericREST OrderDetailsGenericREST = new OrderDetailsGenericREST(wpfConfig);
            UpdateOrderDetailsView updateOrderDetailsView = new UpdateOrderDetailsView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForOrderDetails), modelNotifiedForOrderDetails, typeof(UpdateOrderDetailsView), updateOrderDetailsView);
            OrderDetailsGenericREST.Update(updateOrderDetailsView, out error);

        }
        
        public void AddData(ModelNotifiedForOrderDetails modelNotifiedForOrderDetails, out string error)
        {
            OrderDetailsGenericREST OrderDetailsGenericREST = new OrderDetailsGenericREST(wpfConfig);
            CreateOrderDetailsView createOrderDetailsView = new CreateOrderDetailsView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForOrderDetails), modelNotifiedForOrderDetails, typeof(CreateOrderDetailsView), createOrderDetailsView);
            OrderDetailsGenericREST.Insert(createOrderDetailsView, out error);
        }

        public void DeleteData(ModelNotifiedForOrderDetails modelNotifiedForOrderDetails, out string error)
        { 
            OrderDetailsGenericREST OrderDetailsGenericREST = new OrderDetailsGenericREST(wpfConfig);
            DeleteOrderDetailsView deleteOrderDetailsView = new DeleteOrderDetailsView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForOrderDetails), modelNotifiedForOrderDetails, typeof(DeleteOrderDetailsView), deleteOrderDetailsView);
            OrderDetailsGenericREST.Delete(deleteOrderDetailsView, out error);
        }

        public List<ModelNotifiedForOrderDetails> GetAllOrderDetails(out string error)
        {
            OrderDetailsGenericREST OrderDetailsGenericREST = new OrderDetailsGenericREST(wpfConfig);
            List<ModelNotifiedForOrderDetails> modelNotifiedForOrderDetails = OrderDetailsGenericREST.GetAll<ModelNotifiedForOrderDetails>(100, 0, out error);

            if (!string.IsNullOrEmpty(error))
            {
                return null;
            }
    
            //Initializing row status
            foreach (var item in modelNotifiedForOrderDetails)
            {
                item.ItemChanged = false;
                item.NewItem = false;
            }
    
            return modelNotifiedForOrderDetails;
        }
        
        /// <summary>
        /// Get all data to fill combo box
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<ModelNotifiedForOrders> GetAll_Orders(out string error)
        {
            OrdersGenericREST OrdersGenericREST = new OrdersGenericREST(wpfConfig);
            List<ModelNotifiedForOrders> modelNotifiedForOrders = OrdersGenericREST.GetAll<ModelNotifiedForOrders>(100, 0, out error);
            return modelNotifiedForOrders;
        }
        /// <summary>
        /// Get all data to fill combo box
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<ModelNotifiedForProducts> GetAll_Products(out string error)
        {
            ProductsGenericREST ProductsGenericREST = new ProductsGenericREST(wpfConfig);
            List<ModelNotifiedForProducts> modelNotifiedForProducts = ProductsGenericREST.GetAll<ModelNotifiedForProducts>(100, 0, out error);
            return modelNotifiedForProducts;
        }

    }
}

