//Track[0003] Template:WPF_Shared_DB.html
using System;
using System.Collections.Generic;
using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using MyApp.TransferObjects.REST;
namespace MyApp.WPFForms.EmployeeTerritories
{
    public class WPFEmployeeTerritoriesDB : IWPFEmployeeTerritoriesDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFEmployeeTerritoriesDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public EmployeeTerritoriesDataContext GetDataContext(int EmployeeID,string TerritoryID,out string error)
        {
            EmployeeTerritoriesDataContext dataContext = new EmployeeTerritoriesDataContext();            
            error=null;
            dataContext.modelNotifiedForEmployeeTerritoriesMain = GetEmployeeTerritoriesByID(EmployeeID,TerritoryID, out error);
    
            dataContext.modelNotifiedForEmployees = GetAll_Employees(out error);
            dataContext.modelNotifiedForTerritories = GetAll_Territories(out error);
            dataContext.PopulateComboBoxesItemSource();
            

            return dataContext;
        }

        
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public EmployeeTerritoriesDataContext GetEmptyDataContext(out string error)
        {
            EmployeeTerritoriesDataContext dataContext = new EmployeeTerritoriesDataContext();
            error=null;
            dataContext.modelNotifiedForEmployeeTerritoriesMain = new ModelNotifiedForEmployeeTerritories();
    

            return dataContext;
        }



        public ModelNotifiedForEmployeeTerritories GetEmployeeTerritoriesByID(int EmployeeID,string TerritoryID, out string error)
        {
            error = null;
            EmployeeTerritoriesBsn bsn = new EmployeeTerritoriesBsn(wpfConfig);
            EmployeeTerritoriesInfo dbItem = bsn.GetValueByID(EmployeeID,TerritoryID);
            ModelNotifiedForEmployeeTerritories item = new ModelNotifiedForEmployeeTerritories();
            Cloner.CopyAllTo(typeof(EmployeeTerritoriesInfo), dbItem, typeof(ModelNotifiedForEmployeeTerritories), item);
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
        /// Retrieve all data from Territories table. Used to fill combo box.
        /// </summary>
        /// <returns>List of Territories</returns>
        public List<ModelNotifiedForTerritories> GetAll_Territories(out string error)
        {
            error = null;
            TerritoriesBsn bsn = new TerritoriesBsn(wpfConfig);
            List<TerritoriesInfo> dbItems = bsn.GetAll();
            List<ModelNotifiedForTerritories> notifiedItems = new List<ModelNotifiedForTerritories>();

            foreach (TerritoriesInfo dbItem in dbItems)
            {
                ModelNotifiedForTerritories itemToAdd = new ModelNotifiedForTerritories();
                Cloner.CopyAllTo(typeof(TerritoriesInfo), dbItem, typeof(ModelNotifiedForTerritories), itemToAdd);
                notifiedItems.Add(itemToAdd);
            }
            return notifiedItems;
        }
        
        public void SaveData(ModelNotifiedForEmployeeTerritories modelNotifiedForEmployeeTerritories, out string error)
        {
            EmployeeTerritoriesBsn bsn = new EmployeeTerritoriesBsn(wpfConfig);
            EmployeeTerritoriesInfo dbItem = new EmployeeTerritoriesInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForEmployeeTerritories), modelNotifiedForEmployeeTerritories, typeof(EmployeeTerritoriesInfo), dbItem);
            
            bsn.UpdateOne(dbItem, out error);
        }

        public void AddData(ModelNotifiedForEmployeeTerritories modelNotifiedForEmployeeTerritories, out string error)
        {
            EmployeeTerritoriesBsn bsn = new EmployeeTerritoriesBsn(wpfConfig);
            EmployeeTerritoriesInfo dbItem = new EmployeeTerritoriesInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForEmployeeTerritories), modelNotifiedForEmployeeTerritories, typeof(EmployeeTerritoriesInfo), dbItem);
            bsn.InsertOne(dbItem, out error);
            modelNotifiedForEmployeeTerritories.NewItem = false;
            Cloner.CopyAllTo(typeof(EmployeeTerritoriesInfo), dbItem, typeof(ModelNotifiedForEmployeeTerritories), modelNotifiedForEmployeeTerritories);
        }
        
        public void DeleteData(ModelNotifiedForEmployeeTerritories modelNotifiedForEmployeeTerritories, out string error)
        {
            EmployeeTerritoriesBsn bsn = new EmployeeTerritoriesBsn(wpfConfig);
            EmployeeTerritoriesInfo dbItem = new EmployeeTerritoriesInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForEmployeeTerritories), modelNotifiedForEmployeeTerritories, typeof(EmployeeTerritoriesInfo), dbItem);
            bsn.DeleteByID(dbItem, out error);
        }
    }
}

