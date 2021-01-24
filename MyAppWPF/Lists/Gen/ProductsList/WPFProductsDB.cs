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
namespace MyApp.WPFList.Products
{
    public class WPFProductsDB : IWPFProductsDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFProductsDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public ProductsDataContext GetDataContext(out string error)
        {
            ProductsDataContext dataContext = new ProductsDataContext();            
            error=null;
            dataContext.modelNotifiedForProductsMain = GetAllProducts(out error);
    
            dataContext.modelNotifiedForSuppliers = GetAll_Suppliers(out error);
            dataContext.modelNotifiedForCategories = GetAll_Categories(out error);
            dataContext.PopulateComboBoxesItemSource();
            

            return dataContext;
        }

        

        //private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
            //Notify("IncludeFolders");
        //}


        
        public List<ModelNotifiedForProducts> GetAllProducts(out string error)
        {
            error = null;
            try
            {
                ProductsBsn bsn = new ProductsBsn(wpfConfig);
                List<ProductsInfo> dbItems = bsn.GetAll();
                List<ModelNotifiedForProducts> notifiedItems = new List<ModelNotifiedForProducts>();

                foreach (ProductsInfo dbItem in dbItems)
                {
                    ModelNotifiedForProducts itemToAdd = new ModelNotifiedForProducts();
                    Cloner.CopyAllTo(typeof(ProductsInfo), dbItem, typeof(ModelNotifiedForProducts), itemToAdd);
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
        /// Retrieve all data from Suppliers table. Used to fill combo box.
        /// </summary>
        /// <returns>List of Suppliers</returns>
        public List<ModelNotifiedForSuppliers> GetAll_Suppliers(out string error)
        {
            error = null;
            SuppliersBsn bsn = new SuppliersBsn(wpfConfig);
            List<SuppliersInfo> dbItems = bsn.GetAll();
            List<ModelNotifiedForSuppliers> notifiedItems = new List<ModelNotifiedForSuppliers>();

            foreach (SuppliersInfo dbItem in dbItems)
            {
                ModelNotifiedForSuppliers itemToAdd = new ModelNotifiedForSuppliers();
                Cloner.CopyAllTo(typeof(SuppliersInfo), dbItem, typeof(ModelNotifiedForSuppliers), itemToAdd);
                notifiedItems.Add(itemToAdd);
            }
            return notifiedItems;
        }
        /// <summary>
        /// Retrieve all data from Categories table. Used to fill combo box.
        /// </summary>
        /// <returns>List of Categories</returns>
        public List<ModelNotifiedForCategories> GetAll_Categories(out string error)
        {
            error = null;
            CategoriesBsn bsn = new CategoriesBsn(wpfConfig);
            List<CategoriesInfo> dbItems = bsn.GetAll();
            List<ModelNotifiedForCategories> notifiedItems = new List<ModelNotifiedForCategories>();

            foreach (CategoriesInfo dbItem in dbItems)
            {
                ModelNotifiedForCategories itemToAdd = new ModelNotifiedForCategories();
                Cloner.CopyAllTo(typeof(CategoriesInfo), dbItem, typeof(ModelNotifiedForCategories), itemToAdd);
                notifiedItems.Add(itemToAdd);
            }
            return notifiedItems;
        }
        
        public void SaveData(ModelNotifiedForProducts modelNotifiedForProducts, out string error)
        {
            ProductsBsn bsn = new ProductsBsn(wpfConfig);
            ProductsInfo dbItem = new ProductsInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForProducts), modelNotifiedForProducts, typeof(ProductsInfo), dbItem);
            
            bsn.UpdateOne(dbItem, out error);
        }

        public void AddData(ModelNotifiedForProducts modelNotifiedForProducts, out string error)
        {
            ProductsBsn bsn = new ProductsBsn(wpfConfig);
            ProductsInfo dbItem = new ProductsInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForProducts), modelNotifiedForProducts, typeof(ProductsInfo), dbItem);
            bsn.InsertOne(dbItem, out error);
            modelNotifiedForProducts.NewItem = false;
            Cloner.CopyAllTo(typeof(ProductsInfo), dbItem, typeof(ModelNotifiedForProducts), modelNotifiedForProducts);
        }
        
        public void DeleteData(ModelNotifiedForProducts modelNotifiedForProducts, out string error)
        {
            ProductsBsn bsn = new ProductsBsn(wpfConfig);
            ProductsInfo dbItem = new ProductsInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForProducts), modelNotifiedForProducts, typeof(ProductsInfo), dbItem);
            bsn.DeleteByID(dbItem, out error);
        }
    }
}

