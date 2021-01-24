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

namespace MyApp.WPFList.Tag
{
    public partial class WPFTagRest : IWPFTagDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFTagRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }


        public TagDataContext GetDataContext(out string error)
        {
            TagDataContext dataContext = new TagDataContext();
            error = null;
            dataContext.modelNotifiedForTagMain = GetAllTag(out error);


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

        public List<ModelNotifiedForTag> GetAllTag(out string error)
        {
            TagGenericREST TagGenericREST = new TagGenericREST(wpfConfig);
            List<ModelNotifiedForTag> modelNotifiedForTag = TagGenericREST.GetAll<ModelNotifiedForTag>(100, 0, out error);

            if (!string.IsNullOrEmpty(error))
            {
                return null;
            }
    
            //Initializing row status
            foreach (var item in modelNotifiedForTag)
            {
                item.ItemChanged = false;
                item.NewItem = false;
            }
    
            return modelNotifiedForTag;
        }
        

    }
}

