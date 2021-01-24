//Track[0003] Template:WPF_Shared_DB.html
using System;
using System.Collections.Generic;
using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using MyApp.TransferObjects.REST;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace MyApp.WPFList.OrderDetails
{
    public class WPFOrderDetailsDB : IWPFOrderDetailsDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFOrderDetailsDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public OrderDetailsDataContext GetDataContext(out string error)
        {
            OrderDetailsDataContext dataContext = new OrderDetailsDataContext();            
            error=null;
            dataContext.modelNotifiedForOrderDetailsMain = GetAllOrderDetails(out error);
    
            dataContext.modelNotifiedForOrders = GetAll_Orders(out error);
            dataContext.modelNotifiedForProducts = GetAll_Products(out error);
            dataContext.PopulateComboBoxesItemSource();
            

            return dataContext;
        }

        

        //private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
            //Notify("IncludeFolders");
        //}


        
        public List<ModelNotifiedForOrderDetails> GetAllOrderDetails(out string error)
        {
            error = null;
            try
            {
                OrderDetailsBsn bsn = new OrderDetailsBsn(wpfConfig);
                List<OrderDetailsInfo> dbItems = bsn.GetAll();
                List<ModelNotifiedForOrderDetails> notifiedItems = new List<ModelNotifiedForOrderDetails>();

                foreach (OrderDetailsInfo dbItem in dbItems)
                {
                    ModelNotifiedForOrderDetails itemToAdd = new ModelNotifiedForOrderDetails();
                    Cloner.CopyAllTo(typeof(OrderDetailsInfo), dbItem, typeof(ModelNotifiedForOrderDetails), itemToAdd);
                    itemToAdd.ItemChanged = false;
                    itemToAdd.NewItem = false;
                    notifiedItems.Add(itemToAdd);
                }

                return notifiedItems;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return null;
        }
        
        /// <summary>
        /// Retrieve all data from Orders table. Used to fill combo box.
        /// </summary>
        /// <returns>List of Orders</returns>
        public List<ModelNotifiedForOrders> GetAll_Orders(out string error)
        {
            error = null;
            OrdersBsn bsn = new OrdersBsn(wpfConfig);
            List<OrdersInfo> dbItems = bsn.GetAll();
            List<ModelNotifiedForOrders> notifiedItems = new List<ModelNotifiedForOrders>();

            foreach (OrdersInfo dbItem in dbItems)
            {
                ModelNotifiedForOrders itemToAdd = new ModelNotifiedForOrders();
                Cloner.CopyAllTo(typeof(OrdersInfo), dbItem, typeof(ModelNotifiedForOrders), itemToAdd);
                notifiedItems.Add(itemToAdd);
            }
            return notifiedItems;
        }
        /// <summary>
        /// Retrieve all data from Products table. Used to fill combo box.
        /// </summary>
        /// <returns>List of Products</returns>
        public List<ModelNotifiedForProducts> GetAll_Products(out string error)
        {
            error = null;
            ProductsBsn bsn = new ProductsBsn(wpfConfig);
            List<ProductsInfo> dbItems = bsn.GetAll();
            List<ModelNotifiedForProducts> notifiedItems = new List<ModelNotifiedForProducts>();

            foreach (ProductsInfo dbItem in dbItems)
            {
                ModelNotifiedForProducts itemToAdd = new ModelNotifiedForProducts();
                Cloner.CopyAllTo(typeof(ProductsInfo), dbItem, typeof(ModelNotifiedForProducts), itemToAdd);
                notifiedItems.Add(itemToAdd);
            }
            return notifiedItems;
        }
        
        public void SaveData(ModelNotifiedForOrderDetails modelNotifiedForOrderDetails, out string error)
        {
            OrderDetailsBsn bsn = new OrderDetailsBsn(wpfConfig);
            OrderDetailsInfo dbItem = new OrderDetailsInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForOrderDetails), modelNotifiedForOrderDetails, typeof(OrderDetailsInfo), dbItem);
            
            bsn.UpdateOne(dbItem, out error);
        }

        public void AddData(ModelNotifiedForOrderDetails modelNotifiedForOrderDetails, out string error)
        {
            OrderDetailsBsn bsn = new OrderDetailsBsn(wpfConfig);
            OrderDetailsInfo dbItem = new OrderDetailsInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForOrderDetails), modelNotifiedForOrderDetails, typeof(OrderDetailsInfo), dbItem);
            bsn.InsertOne(dbItem, out error);
            modelNotifiedForOrderDetails.NewItem = false;
            Cloner.CopyAllTo(typeof(OrderDetailsInfo), dbItem, typeof(ModelNotifiedForOrderDetails), modelNotifiedForOrderDetails);
        }
        
        public void DeleteData(ModelNotifiedForOrderDetails modelNotifiedForOrderDetails, out string error)
        {
            OrderDetailsBsn bsn = new OrderDetailsBsn(wpfConfig);
            OrderDetailsInfo dbItem = new OrderDetailsInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForOrderDetails), modelNotifiedForOrderDetails, typeof(OrderDetailsInfo), dbItem);
            bsn.DeleteByID(dbItem, out error);
        }
    }
}

