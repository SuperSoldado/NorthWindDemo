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

namespace MyApp.WPFList.Products
{
    public partial class WPFProductsRest : IWPFProductsDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFProductsRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }


        public ProductsDataContext GetDataContext(out string error)
        {
            ProductsDataContext dataContext = new ProductsDataContext();
            error = null;
            dataContext.modelNotifiedForProductsMain = GetAllProducts(out error);
            dataContext.modelNotifiedForSuppliers = GetAll_Suppliers(out error);
            dataContext.modelNotifiedForCategories = GetAll_Categories(out error);
            dataContext.PopulateComboBoxesItemSource();


            return dataContext;
        }

        public void SaveData(ModelNotifiedForProducts modelNotifiedForProducts, out string error)
        {
            ProductsGenericREST ProductsGenericREST = new ProductsGenericREST(wpfConfig);
            UpdateProductsView updateProductsView = new UpdateProductsView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForProducts), modelNotifiedForProducts, typeof(UpdateProductsView), updateProductsView);
            ProductsGenericREST.Update(updateProductsView, out error);

        }
        
        public void AddData(ModelNotifiedForProducts modelNotifiedForProducts, out string error)
        {
            ProductsGenericREST ProductsGenericREST = new ProductsGenericREST(wpfConfig);
            CreateProductsView createProductsView = new CreateProductsView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForProducts), modelNotifiedForProducts, typeof(CreateProductsView), createProductsView);
            ProductsGenericREST.Insert(createProductsView, out error);
        }

        public void DeleteData(ModelNotifiedForProducts modelNotifiedForProducts, out string error)
        { 
            ProductsGenericREST ProductsGenericREST = new ProductsGenericREST(wpfConfig);
            DeleteProductsView deleteProductsView = new DeleteProductsView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForProducts), modelNotifiedForProducts, typeof(DeleteProductsView), deleteProductsView);
            ProductsGenericREST.Delete(deleteProductsView, out error);
        }

        public List<ModelNotifiedForProducts> GetAllProducts(out string error)
        {
            ProductsGenericREST ProductsGenericREST = new ProductsGenericREST(wpfConfig);
            List<ModelNotifiedForProducts> modelNotifiedForProducts = ProductsGenericREST.GetAll<ModelNotifiedForProducts>(100, 0, out error);

            if (!string.IsNullOrEmpty(error))
            {
                return null;
            }
    
            //Initializing row status
            foreach (var item in modelNotifiedForProducts)
            {
                item.ItemChanged = false;
                item.NewItem = false;
            }
    
            return modelNotifiedForProducts;
        }
        
        /// <summary>
        /// Get all data to fill combo box
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<ModelNotifiedForSuppliers> GetAll_Suppliers(out string error)
        {
            SuppliersGenericREST SuppliersGenericREST = new SuppliersGenericREST(wpfConfig);
            List<ModelNotifiedForSuppliers> modelNotifiedForSuppliers = SuppliersGenericREST.GetAll<ModelNotifiedForSuppliers>(100, 0, out error);
            return modelNotifiedForSuppliers;
        }
        /// <summary>
        /// Get all data to fill combo box
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<ModelNotifiedForCategories> GetAll_Categories(out string error)
        {
            CategoriesGenericREST CategoriesGenericREST = new CategoriesGenericREST(wpfConfig);
            List<ModelNotifiedForCategories> modelNotifiedForCategories = CategoriesGenericREST.GetAll<ModelNotifiedForCategories>(100, 0, out error);
            return modelNotifiedForCategories;
        }

    }
}

