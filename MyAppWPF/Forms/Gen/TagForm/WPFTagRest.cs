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

namespace MyApp.WPFForms.Tag
{
    public partial class WPFTagRest : IWPFTagDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFTagRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }

        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public TagDataContext GetEmptyDataContext(out string error)
        {
            TagDataContext dataContext = new TagDataContext();
            error = null;           
            dataContext.modelNotifiedForTagMain = new ModelNotifiedForTag();;
            return dataContext;
        }

        public TagDataContext GetDataContext(int TagID,out string error)
        {
            TagDataContext dataContext = new TagDataContext();
            error = null;
            dataContext.modelNotifiedForTagMain = GetTagByID(TagID, out error);


            return dataContext;
        }

        public void SaveData(ModelNotifiedForTag modelNotifiedForTag, out string error)
        {
            TagGenericREST TagGenericREST = new TagGenericREST(wpfConfig);
            UpdateTagView updateTagView = new UpdateTagView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTag), modelNotifiedForTag, typeof(UpdateTagView), updateTagView);
            TagGenericREST.Update(updateTagView, out error);

        }
        
        public void AddData(ModelNotifiedForTag modelNotifiedForTag, out string error)
        {
            TagGenericREST TagGenericREST = new TagGenericREST(wpfConfig);
            CreateTagView createTagView = new CreateTagView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTag), modelNotifiedForTag, typeof(CreateTagView), createTagView);
            TagGenericREST.Insert(createTagView, out error);
        }

        public void DeleteData(ModelNotifiedForTag modelNotifiedForTag, out string error)
        { 
            TagGenericREST TagGenericREST = new TagGenericREST(wpfConfig);
            DeleteTagView deleteTagView = new DeleteTagView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTag), modelNotifiedForTag, typeof(DeleteTagView), deleteTagView);
            TagGenericREST.Delete(deleteTagView, out error);
        }

        public ModelNotifiedForTag GetTagByID(int TagID, out string error)
        {
            error = null;
            TagGenericREST TagGenericREST = new TagGenericREST(wpfConfig);
            GetTagView getTagView = TagGenericREST.GetByPK<GetTagView>(TagID, out error)[0];
            ModelNotifiedForTag modelNotifiedForTag = new ModelNotifiedForTag();
            Cloner.CopyAllTo(typeof(GetTagView), getTagView, typeof(ModelNotifiedForTag), modelNotifiedForTag);
            return modelNotifiedForTag;
        }
        

    }
}

