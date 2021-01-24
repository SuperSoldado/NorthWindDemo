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
namespace MyApp.WPFList.Tag
{
    public class WPFTagDB : IWPFTagDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFTagDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public TagDataContext GetDataContext(out string error)
        {
            TagDataContext dataContext = new TagDataContext();            
            error=null;
            dataContext.modelNotifiedForTagMain = GetAllTag(out error);
    
            

            return dataContext;
        }

        

        //private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
            //Notify("IncludeFolders");
        //}


        
        public List<ModelNotifiedForTag> GetAllTag(out string error)
        {
            error = null;
            try
            {
                TagBsn bsn = new TagBsn(wpfConfig);
                List<TagInfo> dbItems = bsn.GetAll();
                List<ModelNotifiedForTag> notifiedItems = new List<ModelNotifiedForTag>();

                foreach (TagInfo dbItem in dbItems)
                {
                    ModelNotifiedForTag itemToAdd = new ModelNotifiedForTag();
                    Cloner.CopyAllTo(typeof(TagInfo), dbItem, typeof(ModelNotifiedForTag), itemToAdd);
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
        
        
        public void SaveData(ModelNotifiedForTag modelNotifiedForTag, out string error)
        {
            TagBsn bsn = new TagBsn(wpfConfig);
            TagInfo dbItem = new TagInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTag), modelNotifiedForTag, typeof(TagInfo), dbItem);
            
            bsn.UpdateOne(dbItem, out error);
        }

        public void AddData(ModelNotifiedForTag modelNotifiedForTag, out string error)
        {
            TagBsn bsn = new TagBsn(wpfConfig);
            TagInfo dbItem = new TagInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTag), modelNotifiedForTag, typeof(TagInfo), dbItem);
            bsn.InsertOne(dbItem, out error);
            modelNotifiedForTag.NewItem = false;
            Cloner.CopyAllTo(typeof(TagInfo), dbItem, typeof(ModelNotifiedForTag), modelNotifiedForTag);
        }
        
        public void DeleteData(ModelNotifiedForTag modelNotifiedForTag, out string error)
        {
            TagBsn bsn = new TagBsn(wpfConfig);
            TagInfo dbItem = new TagInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTag), modelNotifiedForTag, typeof(TagInfo), dbItem);
            bsn.DeleteByID(dbItem, out error);
        }
    }
}

