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

namespace MyApp.WPFForms.Categories
{
    public partial class WPFCategoriesRest : IWPFCategoriesDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFCategoriesRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }

        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public CategoriesDataContext GetEmptyDataContext(out string error)
        {
            CategoriesDataContext dataContext = new CategoriesDataContext();
            error = null;           
            dataContext.modelNotifiedForCategoriesMain = new ModelNotifiedForCategories();;
            return dataContext;
        }

        public CategoriesDataContext GetDataContext(int CategoryID,out string error)
        {
            CategoriesDataContext dataContext = new CategoriesDataContext();
            error = null;
            dataContext.modelNotifiedForCategoriesMain = GetCategoriesByID(CategoryID, out error);


            return dataContext;
        }

        public void SaveData(ModelNotifiedForCategories modelNotifiedForCategories, out string error)
        {
            CategoriesGenericREST CategoriesGenericREST = new CategoriesGenericREST(wpfConfig);
            UpdateCategoriesView updateCategoriesView = new UpdateCategoriesView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCategories), modelNotifiedForCategories, typeof(UpdateCategoriesView), updateCategoriesView);
            CategoriesGenericREST.Update(updateCategoriesView, out error);

        }
        
        public void AddData(ModelNotifiedForCategories modelNotifiedForCategories, out string error)
        {
            CategoriesGenericREST CategoriesGenericREST = new CategoriesGenericREST(wpfConfig);
            CreateCategoriesView createCategoriesView = new CreateCategoriesView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCategories), modelNotifiedForCategories, typeof(CreateCategoriesView), createCategoriesView);
            CategoriesGenericREST.Insert(createCategoriesView, out error);
        }

        public void DeleteData(ModelNotifiedForCategories modelNotifiedForCategories, out string error)
        { 
            CategoriesGenericREST CategoriesGenericREST = new CategoriesGenericREST(wpfConfig);
            DeleteCategoriesView deleteCategoriesView = new DeleteCategoriesView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCategories), modelNotifiedForCategories, typeof(DeleteCategoriesView), deleteCategoriesView);
            CategoriesGenericREST.Delete(deleteCategoriesView, out error);
        }

        public ModelNotifiedForCategories GetCategoriesByID(int CategoryID, out string error)
        {
            error = null;
            CategoriesGenericREST CategoriesGenericREST = new CategoriesGenericREST(wpfConfig);
            GetCategoriesView getCategoriesView = CategoriesGenericREST.GetByPK<GetCategoriesView>(CategoryID, out error)[0];
            ModelNotifiedForCategories modelNotifiedForCategories = new ModelNotifiedForCategories();
            Cloner.CopyAllTo(typeof(GetCategoriesView), getCategoriesView, typeof(ModelNotifiedForCategories), modelNotifiedForCategories);
            return modelNotifiedForCategories;
        }
        

    }
}

