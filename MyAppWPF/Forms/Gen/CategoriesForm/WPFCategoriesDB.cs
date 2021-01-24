//Track[0003] Template:WPF_Shared_DB.html
using System;
using System.Collections.Generic;
using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using MyApp.TransferObjects.REST;
namespace MyApp.WPFForms.Categories
{
    public class WPFCategoriesDB : IWPFCategoriesDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFCategoriesDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public CategoriesDataContext GetDataContext(int CategoryID,out string error)
        {
            CategoriesDataContext dataContext = new CategoriesDataContext();            
            error=null;
            dataContext.modelNotifiedForCategoriesMain = GetCategoriesByID(CategoryID, out error);
    
            

            return dataContext;
        }

        
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public CategoriesDataContext GetEmptyDataContext(out string error)
        {
            CategoriesDataContext dataContext = new CategoriesDataContext();
            error=null;
            dataContext.modelNotifiedForCategoriesMain = new ModelNotifiedForCategories();
    

            return dataContext;
        }



        public ModelNotifiedForCategories GetCategoriesByID(int CategoryID, out string error)
        {
            error = null;
            CategoriesBsn bsn = new CategoriesBsn(wpfConfig);
            CategoriesInfo dbItem = bsn.GetValueByID(CategoryID);
            ModelNotifiedForCategories item = new ModelNotifiedForCategories();
            Cloner.CopyAllTo(typeof(CategoriesInfo), dbItem, typeof(ModelNotifiedForCategories), item);
            return item;
        }
        
        
        
        public void SaveData(ModelNotifiedForCategories modelNotifiedForCategories, out string error)
        {
            CategoriesBsn bsn = new CategoriesBsn(wpfConfig);
            CategoriesInfo dbItem = new CategoriesInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCategories), modelNotifiedForCategories, typeof(CategoriesInfo), dbItem);
            
            bsn.UpdateOne(dbItem, out error);
        }

        public void AddData(ModelNotifiedForCategories modelNotifiedForCategories, out string error)
        {
            CategoriesBsn bsn = new CategoriesBsn(wpfConfig);
            CategoriesInfo dbItem = new CategoriesInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCategories), modelNotifiedForCategories, typeof(CategoriesInfo), dbItem);
            bsn.InsertOne(dbItem, out error);
            modelNotifiedForCategories.NewItem = false;
            Cloner.CopyAllTo(typeof(CategoriesInfo), dbItem, typeof(ModelNotifiedForCategories), modelNotifiedForCategories);
        }
        
        public void DeleteData(ModelNotifiedForCategories modelNotifiedForCategories, out string error)
        {
            CategoriesBsn bsn = new CategoriesBsn(wpfConfig);
            CategoriesInfo dbItem = new CategoriesInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForCategories), modelNotifiedForCategories, typeof(CategoriesInfo), dbItem);
            bsn.DeleteByID(dbItem, out error);
        }
    }
}

