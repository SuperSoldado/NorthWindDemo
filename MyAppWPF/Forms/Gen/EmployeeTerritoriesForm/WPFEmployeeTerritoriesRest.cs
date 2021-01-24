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

namespace MyApp.WPFForms.EmployeeTerritories
{
    public partial class WPFEmployeeTerritoriesRest : IWPFEmployeeTerritoriesDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFEmployeeTerritoriesRest(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }

        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public EmployeeTerritoriesDataContext GetEmptyDataContext(out string error)
        {
            EmployeeTerritoriesDataContext dataContext = new EmployeeTerritoriesDataContext();
            error = null;           
            dataContext.modelNotifiedForEmployeeTerritoriesMain = new ModelNotifiedForEmployeeTerritories();;
            dataContext.modelNotifiedForEmployees = GetAll_Employees(out error);
            dataContext.modelNotifiedForTerritories = GetAll_Territories(out error);
            return dataContext;
        }

        public EmployeeTerritoriesDataContext GetDataContext(int EmployeeID,string TerritoryID,out string error)
        {
            EmployeeTerritoriesDataContext dataContext = new EmployeeTerritoriesDataContext();
            error = null;
            dataContext.modelNotifiedForEmployeeTerritoriesMain = GetEmployeeTerritoriesByID(EmployeeID,TerritoryID, out error);
            dataContext.modelNotifiedForEmployees = GetAll_Employees(out error);
            dataContext.modelNotifiedForTerritories = GetAll_Territories(out error);
            dataContext.PopulateComboBoxesItemSource();


            return dataContext;
        }

        public void SaveData(ModelNotifiedForEmployeeTerritories modelNotifiedForEmployeeTerritories, out string error)
        {
            EmployeeTerritoriesGenericREST EmployeeTerritoriesGenericREST = new EmployeeTerritoriesGenericREST(wpfConfig);
            UpdateEmployeeTerritoriesView updateEmployeeTerritoriesView = new UpdateEmployeeTerritoriesView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForEmployeeTerritories), modelNotifiedForEmployeeTerritories, typeof(UpdateEmployeeTerritoriesView), updateEmployeeTerritoriesView);
            EmployeeTerritoriesGenericREST.Update(updateEmployeeTerritoriesView, out error);

        }
        
        public void AddData(ModelNotifiedForEmployeeTerritories modelNotifiedForEmployeeTerritories, out string error)
        {
            EmployeeTerritoriesGenericREST EmployeeTerritoriesGenericREST = new EmployeeTerritoriesGenericREST(wpfConfig);
            CreateEmployeeTerritoriesView createEmployeeTerritoriesView = new CreateEmployeeTerritoriesView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForEmployeeTerritories), modelNotifiedForEmployeeTerritories, typeof(CreateEmployeeTerritoriesView), createEmployeeTerritoriesView);
            EmployeeTerritoriesGenericREST.Insert(createEmployeeTerritoriesView, out error);
        }

        public void DeleteData(ModelNotifiedForEmployeeTerritories modelNotifiedForEmployeeTerritories, out string error)
        { 
            EmployeeTerritoriesGenericREST EmployeeTerritoriesGenericREST = new EmployeeTerritoriesGenericREST(wpfConfig);
            DeleteEmployeeTerritoriesView deleteEmployeeTerritoriesView = new DeleteEmployeeTerritoriesView();
            Cloner.CopyAllTo(typeof(ModelNotifiedForEmployeeTerritories), modelNotifiedForEmployeeTerritories, typeof(DeleteEmployeeTerritoriesView), deleteEmployeeTerritoriesView);
            EmployeeTerritoriesGenericREST.Delete(deleteEmployeeTerritoriesView, out error);
        }

        public ModelNotifiedForEmployeeTerritories GetEmployeeTerritoriesByID(int EmployeeID,string TerritoryID, out string error)
        {
            error = null;
            EmployeeTerritoriesGenericREST EmployeeTerritoriesGenericREST = new EmployeeTerritoriesGenericREST(wpfConfig);
            GetEmployeeTerritoriesView getEmployeeTerritoriesView = EmployeeTerritoriesGenericREST.GetByPK<GetEmployeeTerritoriesView>(EmployeeID,TerritoryID, out error)[0];
            ModelNotifiedForEmployeeTerritories modelNotifiedForEmployeeTerritories = new ModelNotifiedForEmployeeTerritories();
            Cloner.CopyAllTo(typeof(GetEmployeeTerritoriesView), getEmployeeTerritoriesView, typeof(ModelNotifiedForEmployeeTerritories), modelNotifiedForEmployeeTerritories);
            return modelNotifiedForEmployeeTerritories;
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
        public List<ModelNotifiedForTerritories> GetAll_Territories(out string error)
        {
            TerritoriesGenericREST TerritoriesGenericREST = new TerritoriesGenericREST(wpfConfig);
            List<ModelNotifiedForTerritories> modelNotifiedForTerritories = TerritoriesGenericREST.GetAll<ModelNotifiedForTerritories>(100, 0, out error);
            return modelNotifiedForTerritories;
        }

    }
}

