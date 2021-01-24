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

namespace MyApp.WPFForms.Employees
{
    public partial class WPFEmployeesRest : IWPFEmployeesDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFEmployeesRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }

        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public EmployeesDataContext GetEmptyDataContext(out string error)
        {
            EmployeesDataContext dataContext = new EmployeesDataContext();
            error = null;           
            dataContext.modelNotifiedForEmployeesMain = new ModelNotifiedForEmployees();;
            dataContext.modelNotifiedForEmployees = GetAll_Employees(out error);
            return dataContext;
        }

        public EmployeesDataContext GetDataContext(int EmployeeID,out string error)
        {
            EmployeesDataContext dataContext = new EmployeesDataContext();
            error = null;
            dataContext.modelNotifiedForEmployeesMain = GetEmployeesByID(EmployeeID, out error);
            dataContext.modelNotifiedForEmployees = GetAll_Employees(out error);
            dataContext.PopulateComboBoxesItemSource();

            this.LoadNxNComboFor_EmployeeTerritories(dataContext, out error);

            return dataContext;
        }

        public void SaveData(ModelNotifiedForEmployees modelNotifiedForEmployees, out string error)
        {
            EmployeesGenericREST EmployeesGenericREST = new EmployeesGenericREST(wpfConfig);
            UpdateEmployeesView updateEmployeesView = new UpdateEmployeesView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForEmployees), modelNotifiedForEmployees, typeof(UpdateEmployeesView), updateEmployeesView);
            EmployeesGenericREST.Update(updateEmployeesView, out error);

            //Saving NxN data for: EmployeeTerritories
            SaveNxNComboFor_EmployeeTerritories(modelNotifiedForEmployees, out error);

        }
        
        public void AddData(ModelNotifiedForEmployees modelNotifiedForEmployees, out string error)
        {
            EmployeesGenericREST EmployeesGenericREST = new EmployeesGenericREST(wpfConfig);
            CreateEmployeesView createEmployeesView = new CreateEmployeesView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForEmployees), modelNotifiedForEmployees, typeof(CreateEmployeesView), createEmployeesView);
            EmployeesGenericREST.Insert(createEmployeesView, out error);
        }

        public void DeleteData(ModelNotifiedForEmployees modelNotifiedForEmployees, out string error)
        { 
            EmployeesGenericREST EmployeesGenericREST = new EmployeesGenericREST(wpfConfig);
            DeleteEmployeesView deleteEmployeesView = new DeleteEmployeesView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForEmployees), modelNotifiedForEmployees, typeof(DeleteEmployeesView), deleteEmployeesView);
            EmployeesGenericREST.Delete(deleteEmployeesView, out error);
        }

        public ModelNotifiedForEmployees GetEmployeesByID(int EmployeeID, out string error)
        {
            error = null;
            EmployeesGenericREST EmployeesGenericREST = new EmployeesGenericREST(wpfConfig);
            GetEmployeesView getEmployeesView = EmployeesGenericREST.GetByPK<GetEmployeesView>(EmployeeID, out error)[0];
            ModelNotifiedForEmployees modelNotifiedForEmployees = new ModelNotifiedForEmployees();
            Cloner.CopyAllTo(typeof(GetEmployeesView), getEmployeesView, typeof(ModelNotifiedForEmployees), modelNotifiedForEmployees);
            return modelNotifiedForEmployees;
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

        #region NXN loaders for ComboNxNEmployeeTerritories
        private void LoadNxNComboFor_EmployeeTerritories(EmployeesDataContext dataContext, out string error)
        {
            List<ModelNotifiedForTerritories> allTerritories = GetAllTerritories(out error);
            if (dataContext == null)
            {
                return;
            }
            var item = dataContext.modelNotifiedForEmployeesMain;
            List<ModelNotifiedForEmployeeTerritories> listEmployeeTerritories = GetAllEmployeeTerritories(item.EmployeeID, out error);
            List<ModelNotifiedForTerritories> comboItens = new List<ModelNotifiedForTerritories>();               
            
            foreach (ModelNotifiedForTerritories item2 in allTerritories)
            {
                ModelNotifiedForEmployeeTerritories aux = listEmployeeTerritories.Where(x => x.TerritoryID == item2.TerritoryID).FirstOrDefault();
                bool existsInDB = (aux != null);
                ModelNotifiedForTerritories newComboItem = new ModelNotifiedForTerritories();
                Cloner.CopyAllTo(typeof(ModelNotifiedForTerritories), item2, typeof(ModelNotifiedForTerritories), newComboItem);
                if (existsInDB)
                {
                    newComboItem.Check_Status = true;
                }
                else
                {
                    newComboItem.Check_Status = false;
                }
                comboItens.Add(newComboItem);
            }
            
            item.LookDownComboDataTerritories = comboItens.OrderBy(x => x.TerritoryDescription).ToList();

        }

        private void SaveNxNComboFor_EmployeeTerritories(ModelNotifiedForEmployees modelNotifiedForEmployees, out string error)
        {
            error = null;
            EmployeeTerritoriesGenericREST EmployeeTerritoriesGenericREST = new EmployeeTerritoriesGenericREST(wpfConfig);

            foreach (ModelNotifiedForTerritories item in modelNotifiedForEmployees.LookDownComboDataTerritories)
            {
                if (item.ItemChanged)
                {                    
                    if (item.Check_Status)
                    {
                        CreateEmployeeTerritoriesView itemToAdd = new CreateEmployeeTerritoriesView();
                        //Setting NxN object to include/delete
itemToAdd.EmployeeID = modelNotifiedForEmployees.EmployeeID;
itemToAdd.TerritoryID = item.TerritoryID;

                        EmployeeTerritoriesGenericREST.Insert(itemToAdd, out error);
                    }
                    else
                    {
                        DeleteEmployeeTerritoriesView itemToDelete = new DeleteEmployeeTerritoriesView();
                        //Setting NxN object to include/delete
itemToDelete.EmployeeID = modelNotifiedForEmployees.EmployeeID;
itemToDelete.TerritoryID = item.TerritoryID;

                        EmployeeTerritoriesGenericREST.Delete(itemToDelete, out error);
                    }

                    if (error != null)
                    {
                        return;
                    }
                    
                    item.ItemChanged = false;
                }
            }
        }

        /// <summary>
        /// Get all itens to populate NXN relation used in ComboNxNEmployeeTerritories
        /// </summary>
        public List<ModelNotifiedForEmployeeTerritories> GetAllEmployeeTerritories(int EmployeeID, out string error)
        {
            EmployeeTerritoriesGenericREST EmployeeTerritoriesGenericREST = new EmployeeTerritoriesGenericREST(wpfConfig);
            List<DataFilterExpressionREST> dataFilterExpressionRESTList = new List<DataFilterExpressionREST>();
            DataFilterExpressionREST dataFilterExpressionREST = null;
            dataFilterExpressionREST = new DataFilterExpressionREST();
            dataFilterExpressionREST.FieldName= "EmployeeID";
            dataFilterExpressionREST.FilterType= DataFilterExpressionREST._FilterType.Equal;
            dataFilterExpressionREST.Filter = EmployeeID.ToString();
            dataFilterExpressionRESTList.Add(dataFilterExpressionREST);

            List<ModelNotifiedForEmployeeTerritories> modelNotifiedForEmployeeTerritories = EmployeeTerritoriesGenericREST.GetAllX<ModelNotifiedForEmployeeTerritories>(dataFilterExpressionRESTList, out error);
            if (!string.IsNullOrEmpty(error))
            {
                return null;
            }
    
            //Initializing row status
            foreach (var item in modelNotifiedForEmployeeTerritories)
            {
                item.ItemChanged = false;
            }
    
            return modelNotifiedForEmployeeTerritories;
            return null;
        }

        /// <summary>
        /// Get all LookUp itens to populate NXN relation used in ComboNxNEmployeeTerritories
        /// </summary>
        public List<ModelNotifiedForTerritories> GetAllTerritories(out string error)
        {
            error = null;
            TerritoriesGenericREST TerritoriesGenericREST = new TerritoriesGenericREST(wpfConfig);
            List<ModelNotifiedForTerritories> modelNotifiedForTerritories = TerritoriesGenericREST.GetAll<ModelNotifiedForTerritories>(100, 0, out error);

            if (!string.IsNullOrEmpty(error))
            {
                return null;
            }
    
            //Initializing row status
            foreach (var item in modelNotifiedForTerritories)
            {
                item.ItemChanged = false;
            }
    
            return modelNotifiedForTerritories;
        }
        #endregion
    }
}

