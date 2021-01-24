//Track[0003] Template:WPF_Shared_DB.html
using System;
using System.Collections.Generic;
using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using MyApp.TransferObjects.REST;
namespace MyApp.WPFForms.TagEmployee
{
    public class WPFTagEmployeeDB : IWPFTagEmployeeDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFTagEmployeeDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public TagEmployeeDataContext GetDataContext(int TagEmployeeID,out string error)
        {
            TagEmployeeDataContext dataContext = new TagEmployeeDataContext();            
            error=null;
            dataContext.modelNotifiedForTagEmployeeMain = GetTagEmployeeByID(TagEmployeeID, out error);
    
            dataContext.modelNotifiedForEmployees = GetAll_Employees(out error);
            dataContext.modelNotifiedForTag = GetAll_Tag(out error);
            dataContext.PopulateComboBoxesItemSource();
            

            return dataContext;
        }

        
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public TagEmployeeDataContext GetEmptyDataContext(out string error)
        {
            TagEmployeeDataContext dataContext = new TagEmployeeDataContext();
            error=null;
            dataContext.modelNotifiedForTagEmployeeMain = new ModelNotifiedForTagEmployee();
    

            return dataContext;
        }



        public ModelNotifiedForTagEmployee GetTagEmployeeByID(int TagEmployeeID, out string error)
        {
            error = null;
            TagEmployeeBsn bsn = new TagEmployeeBsn(wpfConfig);
            TagEmployeeInfo dbItem = bsn.GetValueByID(TagEmployeeID);
            ModelNotifiedForTagEmployee item = new ModelNotifiedForTagEmployee();
            Cloner.CopyAllTo(typeof(TagEmployeeInfo), dbItem, typeof(ModelNotifiedForTagEmployee), item);
            return item;
        }
        
        
        /// <summary>
        /// Retrieve all data from Employees table. Used to fill combo box.
        /// </summary>
        /// <returns>List of Employees</returns>
        public List<ModelNotifiedForEmployees> GetAll_Employees(out string error)
        {
            error = null;
            EmployeesBsn bsn = new EmployeesBsn(wpfConfig);
            List<EmployeesInfo> dbItems = bsn.GetAll();
            List<ModelNotifiedForEmployees> notifiedItems = new List<ModelNotifiedForEmployees>();

            foreach (EmployeesInfo dbItem in dbItems)
            {
                ModelNotifiedForEmployees itemToAdd = new ModelNotifiedForEmployees();
                Cloner.CopyAllTo(typeof(EmployeesInfo), dbItem, typeof(ModelNotifiedForEmployees), itemToAdd);
                notifiedItems.Add(itemToAdd);
            }
            return notifiedItems;
        }
        /// <summary>
        /// Retrieve all data from Tag table. Used to fill combo box.
        /// </summary>
        /// <returns>List of Tag</returns>
        public List<ModelNotifiedForTag> GetAll_Tag(out string error)
        {
            error = null;
            TagBsn bsn = new TagBsn(wpfConfig);
            List<TagInfo> dbItems = bsn.GetAll();
            List<ModelNotifiedForTag> notifiedItems = new List<ModelNotifiedForTag>();

            foreach (TagInfo dbItem in dbItems)
            {
                ModelNotifiedForTag itemToAdd = new ModelNotifiedForTag();
                Cloner.CopyAllTo(typeof(TagInfo), dbItem, typeof(ModelNotifiedForTag), itemToAdd);
                notifiedItems.Add(itemToAdd);
            }
            return notifiedItems;
        }
        
        public void SaveData(ModelNotifiedForTagEmployee modelNotifiedForTagEmployee, out string error)
        {
            TagEmployeeBsn bsn = new TagEmployeeBsn(wpfConfig);
            TagEmployeeInfo dbItem = new TagEmployeeInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTagEmployee), modelNotifiedForTagEmployee, typeof(TagEmployeeInfo), dbItem);
            
            bsn.UpdateOne(dbItem, out error);
        }

        public void AddData(ModelNotifiedForTagEmployee modelNotifiedForTagEmployee, out string error)
        {
            TagEmployeeBsn bsn = new TagEmployeeBsn(wpfConfig);
            TagEmployeeInfo dbItem = new TagEmployeeInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTagEmployee), modelNotifiedForTagEmployee, typeof(TagEmployeeInfo), dbItem);
            bsn.InsertOne(dbItem, out error);
            modelNotifiedForTagEmployee.NewItem = false;
            Cloner.CopyAllTo(typeof(TagEmployeeInfo), dbItem, typeof(ModelNotifiedForTagEmployee), modelNotifiedForTagEmployee);
        }
        
        public void DeleteData(ModelNotifiedForTagEmployee modelNotifiedForTagEmployee, out string error)
        {
            TagEmployeeBsn bsn = new TagEmployeeBsn(wpfConfig);
            TagEmployeeInfo dbItem = new TagEmployeeInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForTagEmployee), modelNotifiedForTagEmployee, typeof(TagEmployeeInfo), dbItem);
            bsn.DeleteByID(dbItem, out error);
        }
    }
}

