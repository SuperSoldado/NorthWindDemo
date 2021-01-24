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
namespace MyApp.WPFList.Categories
{
    public class WPFCategoriesDB : IWPFCategoriesDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFCategoriesDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public CategoriesDataContext GetDataContext(out string error)
        {
            CategoriesDataContext dataContext = new CategoriesDataContext();            
            error=null;
            dataContext.modelNotifiedForCategoriesMain = GetAllCategories(out error);
    
            

            return dataContext;
        }

        

        //private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
            //Notify("IncludeFolders");
        //}


        
        public List<ModelNotifiedForCategories> GetAllCategories(out string error)
        {
            error = null;
            try
            {
                CategoriesBsn bsn = new CategoriesBsn(wpfConfig);
                List<CategoriesInfo> dbItems = bsn.GetAll();
                List<ModelNotifiedForCategories> notifiedItems = new List<ModelNotifiedForCategories>();

                foreach (CategoriesInfo dbItem in dbItems)
                {
                    ModelNotifiedForCategories itemToAdd = new ModelNotifiedForCategories();
                    Cloner.CopyAllTo(typeof(CategoriesInfo), dbItem, typeof(ModelNotifiedForCategories), itemToAdd);
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

