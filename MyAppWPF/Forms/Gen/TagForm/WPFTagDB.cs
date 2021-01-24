//Track[0003] Template:WPF_Shared_DB.html
using System;
using System.Collections.Generic;
using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using MyApp.TransferObjects.REST;
namespace MyApp.WPFForms.Tag
{
    public class WPFTagDB : IWPFTagDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFTagDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public TagDataContext GetDataContext(int TagID,out string error)
        {
            TagDataContext dataContext = new TagDataContext();            
            error=null;
            dataContext.modelNotifiedForTagMain = GetTagByID(TagID, out error);
    
            

            return dataContext;
        }

        
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public TagDataContext GetEmptyDataContext(out string error)
        {
            TagDataContext dataContext = new TagDataContext();
            error=null;
            dataContext.modelNotifiedForTagMain = new ModelNotifiedForTag();
    

            return dataContext;
        }



        public ModelNotifiedForTag GetTagByID(int TagID, out string error)
        {
            error = null;
            TagBsn bsn = new TagBsn(wpfConfig);
            TagInfo dbItem = bsn.GetValueByID(TagID);
            ModelNotifiedForTag item = new ModelNotifiedForTag();
            Cloner.CopyAllTo(typeof(TagInfo), dbItem, typeof(ModelNotifiedForTag), item);
            return item;
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

