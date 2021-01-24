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

namespace MyApp.WPFForms.TagEmployee
{
    public partial class WPFTagEmployeeRest : IWPFTagEmployeeDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFTagEmployeeRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }

        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public TagEmployeeDataContext GetEmptyDataContext(out string error)
        {
            TagEmployeeDataContext dataContext = new TagEmployeeDataContext();
            error = null;           
            dataContext.modelNotifiedForTagEmployeeMain = new ModelNotifiedForTagEmployee();;
            dataContext.modelNotifiedForEmployees = GetAll_Employees(out error);
            dataContext.modelNotifiedForTag = GetAll_Tag(out error);
            return dataContext;
        }

        public TagEmployeeDataContext GetDataContext(int TagEmployeeID,out string error)
        {
            TagEmployeeDataContext dataContext = new TagEmployeeDataContext();
            error = null;
            dataContext.modelNotifiedForTagEmployeeMain = GetTagEmployeeByID(TagEmployeeID, out error);
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

        public ModelNotifiedForTagEmployee GetTagEmployeeByID(int TagEmployeeID, out string error)
        {
            error = null;
            TagEmployeeGenericREST TagEmployeeGenericREST = new TagEmployeeGenericREST(wpfConfig);
            GetTagEmployeeView getTagEmployeeView = TagEmployeeGenericREST.GetByPK<GetTagEmployeeView>(TagEmployeeID, out error)[0];
            ModelNotifiedForTagEmployee modelNotifiedForTagEmployee = new ModelNotifiedForTagEmployee();
            Cloner.CopyAllTo(typeof(GetTagEmployeeView), getTagEmployeeView, typeof(ModelNotifiedForTagEmployee), modelNotifiedForTagEmployee);
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

