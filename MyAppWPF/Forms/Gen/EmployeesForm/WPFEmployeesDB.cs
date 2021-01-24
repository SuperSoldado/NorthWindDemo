//Track[0003] Template:WPF_Shared_DB.html
using System;
using System.Collections.Generic;
using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using System.Linq;
using MyApp.TransferObjects.REST;
namespace MyApp.WPFForms.Employees
{
    public class WPFEmployeesDB : IWPFEmployeesDataConnection
    {
        private WPFConfig wpfConfig { get; set; }
        public WPFEmployeesDB(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }
        
        //Track [0004]
        public EmployeesDataContext GetDataContext(int EmployeeID,out string error)
        {
            EmployeesDataContext dataContext = new EmployeesDataContext();            
            error=null;
            dataContext.modelNotifiedForEmployeesMain = GetEmployeesByID(EmployeeID, out error);
    
            dataContext.modelNotifiedForEmployees = GetAll_Employees(out error);
            dataContext.PopulateComboBoxesItemSource();
            
            this.LoadNxNComboFor_EmployeeTerritories(dataContext, out error);

            return dataContext;
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
            EmployeeTerritoriesBsn bsn = new EmployeeTerritoriesBsn(wpfConfig);
            foreach (ModelNotifiedForTerritories item in modelNotifiedForEmployees.LookDownComboDataTerritories)
            {
                if (item.ItemChanged)
                {
                    item.ItemChanged = false;
                    EmployeeTerritoriesInfo itemToAddOrDelete = new EmployeeTerritoriesInfo();
                    //Setting NxN object to include/delete
itemToAddOrDelete.EmployeeID = modelNotifiedForEmployees.EmployeeID;
itemToAddOrDelete.TerritoryID = item.TerritoryID;

                    if (item.Check_Status)
                    {
                        bsn.InsertOne(itemToAddOrDelete, out error);
                    }
                    else
                    {
                        bsn.Delete(itemToAddOrDelete, out error);
                    }
                }
            }
        }

        /// <summary>
        /// Get all itens to populate NXN relation used in ComboNxNEmployeeTerritories
        /// </summary>
        public List<ModelNotifiedForEmployeeTerritories> GetAllEmployeeTerritories(int EmployeeID, out string error)
        {
            error = null;
            try
            {
                EmployeeTerritoriesBsn bsn = new EmployeeTerritoriesBsn(wpfConfig);
                EmployeeTerritoriesInfo filter = new EmployeeTerritoriesInfo();
                filter.EmployeeID = EmployeeID;

                List<EmployeeTerritoriesInfo> dbItems = bsn.GetSome(filter);
                List<ModelNotifiedForEmployeeTerritories> notifiedItems = new List<ModelNotifiedForEmployeeTerritories>();

                foreach (EmployeeTerritoriesInfo dbItem in dbItems)
                {
                    ModelNotifiedForEmployeeTerritories itemToAdd = new ModelNotifiedForEmployeeTerritories();
                    Cloner.CopyAllTo(typeof(EmployeeTerritoriesInfo), dbItem, typeof(ModelNotifiedForEmployeeTerritories), itemToAdd);
                    itemToAdd.ItemChanged = false;
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

        /// <summary>
        /// Get all LookUp itens to populate NXN relation used in ComboNxNEmployeeTerritories
        /// </summary>
        public List<ModelNotifiedForTerritories> GetAllTerritories(out string error)
        {
            error = null;
            try
            {
                TerritoriesBsn bsn = new TerritoriesBsn(wpfConfig);
                List<TerritoriesInfo> dbItems = bsn.GetAll();
                List<ModelNotifiedForTerritories> notifiedItems = new List<ModelNotifiedForTerritories>();

                foreach (TerritoriesInfo dbItem in dbItems)
                {
                    ModelNotifiedForTerritories itemToAdd = new ModelNotifiedForTerritories();
                    Cloner.CopyAllTo(typeof(TerritoriesInfo), dbItem, typeof(ModelNotifiedForTerritories), itemToAdd);
                    itemToAdd.ItemChanged = false;
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
        #endregion
        
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public EmployeesDataContext GetEmptyDataContext(out string error)
        {
            EmployeesDataContext dataContext = new EmployeesDataContext();
            error=null;
            dataContext.modelNotifiedForEmployeesMain = new ModelNotifiedForEmployees();
    

            return dataContext;
        }



        public ModelNotifiedForEmployees GetEmployeesByID(int EmployeeID, out string error)
        {
            error = null;
            EmployeesBsn bsn = new EmployeesBsn(wpfConfig);
            EmployeesInfo dbItem = bsn.GetValueByID(EmployeeID);
            ModelNotifiedForEmployees item = new ModelNotifiedForEmployees();
            Cloner.CopyAllTo(typeof(EmployeesInfo), dbItem, typeof(ModelNotifiedForEmployees), item);
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
        
        public void SaveData(ModelNotifiedForEmployees modelNotifiedForEmployees, out string error)
        {
            EmployeesBsn bsn = new EmployeesBsn(wpfConfig);
            EmployeesInfo dbItem = new EmployeesInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForEmployees), modelNotifiedForEmployees, typeof(EmployeesInfo), dbItem);
            
            //Saving NxN data fro: EmployeeTerritories
            SaveNxNComboFor_EmployeeTerritories(modelNotifiedForEmployees, out error);

            bsn.UpdateOne(dbItem, out error);
        }

        public void AddData(ModelNotifiedForEmployees modelNotifiedForEmployees, out string error)
        {
            EmployeesBsn bsn = new EmployeesBsn(wpfConfig);
            EmployeesInfo dbItem = new EmployeesInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForEmployees), modelNotifiedForEmployees, typeof(EmployeesInfo), dbItem);
            bsn.InsertOne(dbItem, out error);
            modelNotifiedForEmployees.NewItem = false;
            Cloner.CopyAllTo(typeof(EmployeesInfo), dbItem, typeof(ModelNotifiedForEmployees), modelNotifiedForEmployees);
        }
        
        public void DeleteData(ModelNotifiedForEmployees modelNotifiedForEmployees, out string error)
        {
            EmployeesBsn bsn = new EmployeesBsn(wpfConfig);
            EmployeesInfo dbItem = new EmployeesInfo();
            Cloner.CopyAllTo(typeof(ModelNotifiedForEmployees), modelNotifiedForEmployees, typeof(EmployeesInfo), dbItem);
            bsn.DeleteByID(dbItem, out error);
        }
    }
}

