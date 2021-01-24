//Track[0003] Template:WPF_Shared_DB.html
using System;
using System.Collections.Generic;
using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using MyApp.TransferObjects.REST;
namespace MyApp.WPFForms.Products
{
    public class WPFProductsDB : IWPFProductsDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFProductsDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public ProductsDataContext GetDataContext(int ProductID,out string error)
        {
            ProductsDataContext dataContext = new ProductsDataContext();            
            error=null;
            dataContext.modelNotifiedForProductsMain = GetProductsByID(ProductID, out error);
    
            dataContext.modelNotifiedForSuppliers = GetAll_Suppliers(out error);
            dataContext.modelNotifiedForCategories = GetAll_Categories(out error);
            dataContext.PopulateComboBoxesItemSource();
            

            return dataContext;
        }

        
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public ProductsDataContext GetEmptyDataContext(out string error)
        {
            ProductsDataContext dataContext = new ProductsDataContext();
            error=null;
            dataContext.modelNotifiedForProductsMain = new ModelNotifiedForProducts();
    

            return dataContext;
        }



        public ModelNotifiedForProducts GetProductsByID(int ProductID, out string error)
        {
            error = null;
            ProductsBsn bsn = new ProductsBsn(wpfConfig);
            ProductsInfo dbItem = bsn.GetValueByID(ProductID);
            ModelNotifiedForProducts item = new ModelNotifiedForProducts();
            Cloner.CopyAllTo(typeof(ProductsInfo), dbItem, typeof(ModelNotifiedForProducts), item);
            return item;
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

