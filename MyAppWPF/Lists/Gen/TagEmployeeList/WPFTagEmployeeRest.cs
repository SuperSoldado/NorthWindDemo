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

namespace MyApp.WPFList.TagEmployee
{
    public partial class WPFTagEmployeeRest : IWPFTagEmployeeDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFTagEmployeeRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }


        public TagEmployeeDataContext GetDataContext(out string error)
        {
            TagEmployeeDataContext dataContext = new TagEmployeeDataContext();
            error = null;
            dataContext.modelNotifiedForTagEmployeeMain = GetAllTagEmployee(out error);
            dataContext.modelNotifiedForEmployees = GetAll_Employees(out error);
            dataContext.modelNotifiedForTag = GetAll_Tag(out error);
            dataContext.PopulateComboBoxesItemSource();


            return dataContext;
        }

        public void SaveData(ModelNotifiedForTagEmployee modelNotifiedForTagEmployee, out string error)
        {
            TagEmployeeGenericREST TagEmployeeGenericREST = new TagEmployeeGenericREST(wpfConfig);
            UpdateTagEmployeeView updateTagEmployeeView = new UpdateTagEmployeeView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTagEmployee), modelNotifiedForTagEmployee, typeof(UpdateTagEmployeeView), updateTagEmployeeView);
            TagEmployeeGenericREST.Update(updateTagEmployeeView, out error);

        }
        
        public void AddData(ModelNotifiedForTagEmployee modelNotifiedForTagEmployee, out string error)
        {
            TagEmployeeGenericREST TagEmployeeGenericREST = new TagEmployeeGenericREST(wpfConfig);
            CreateTagEmployeeView createTagEmployeeView = new CreateTagEmployeeView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTagEmployee), modelNotifiedForTagEmployee, typeof(CreateTagEmployeeView), createTagEmployeeView);
            TagEmployeeGenericREST.Insert(createTagEmployeeView, out error);
        }

        public void DeleteData(ModelNotifiedForTagEmployee modelNotifiedForTagEmployee, out string error)
        { 
            TagEmployeeGenericREST TagEmployeeGenericREST = new TagEmployeeGenericREST(wpfConfig);
            DeleteTagEmployeeView deleteTagEmployeeView = new DeleteTagEmployeeView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTagEmployee), modelNotifiedForTagEmployee, typeof(DeleteTagEmployeeView), deleteTagEmployeeView);
            TagEmployeeGenericREST.Delete(deleteTagEmployeeView, out error);
        }

        public List<ModelNotifiedForTagEmployee> GetAllTagEmployee(out string error)
        {
            TagEmployeeGenericREST TagEmployeeGenericREST = new TagEmployeeGenericREST(wpfConfig);
            List<ModelNotifiedForTagEmployee> modelNotifiedForTagEmployee = TagEmployeeGenericREST.GetAll<ModelNotifiedForTagEmployee>(100, 0, out error);

            if (!string.IsNullOrEmpty(error))
            {
                return null;
            }
    
            //Initializing row status
            foreach (var item in modelNotifiedForTagEmployee)
            {
                item.ItemChanged = false;
                item.NewItem = false;
            }
    
            return modelNotifiedForTagEmployee;
        }
        
        /// <summary>
        /// Get all data to fill combo box
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<ModelNotifiedForEmployees> GetAll_Employees(out string error)
        {
            EmployeesGenericREST EmployeesGenericREST = new EmployeesGenericREST(wpfConfig);
            List<ModelNotifiedForEmployees> modelNotifiedForEmployees = EmployeesGenericREST.GetAll<ModelNotifiedForEmployees>(100, 0, out error);
            return modelNotifiedForEmployees;
        }
        /// <summary>
        /// Get all data to fill combo box
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<ModelNotifiedForTag> GetAll_Tag(out string error)
        {
            TagGenericREST TagGenericREST = new TagGenericREST(wpfConfig);
            List<ModelNotifiedForTag> modelNotifiedForTag = TagGenericREST.GetAll<ModelNotifiedForTag>(100, 0, out error);
            return modelNotifiedForTag;
        }

    }
}

