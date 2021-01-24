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

namespace MyApp.WPFList.Categories
{
    public partial class WPFCategoriesRest : IWPFCategoriesDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFCategoriesRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }


        public CategoriesDataContext GetDataContext(out string error)
        {
            CategoriesDataContext dataContext = new CategoriesDataContext();
            error = null;
            dataContext.modelNotifiedForCategoriesMain = GetAllCategories(out error);


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

        public List<ModelNotifiedForCategories> GetAllCategories(out string error)
        {
            CategoriesGenericREST CategoriesGenericREST = new CategoriesGenericREST(wpfConfig);
            List<ModelNotifiedForCategories> modelNotifiedForCategories = CategoriesGenericREST.GetAll<ModelNotifiedForCategories>(100, 0, out error);

            if (!string.IsNullOrEmpty(error))
            {
                return null;
            }
    
            //Initializing row status
            foreach (var item in modelNotifiedForCategories)
            {
                item.ItemChanged = false;
                item.NewItem = false;
            }
    
            return modelNotifiedForCategories;
        }
        

    }
}

