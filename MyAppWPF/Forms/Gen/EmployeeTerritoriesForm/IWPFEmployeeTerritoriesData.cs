//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFForms.EmployeeTerritories
{
    public interface IWPFEmployeeTerritoriesDataConnection
    {
        public EmployeeTerritoriesDataContext GetDataContext(int EmployeeID,string TerritoryID,out string error);
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public EmployeeTerritoriesDataContext GetEmptyDataContext(out string error);
        ModelNotifiedForEmployeeTerritories GetEmployeeTerritoriesByID(int EmployeeID,string TerritoryID, out string error);
        public void SaveData(ModelNotifiedForEmployeeTerritories modelNotifiedForEmployeeTerritories, out string error);
        public void DeleteData(ModelNotifiedForEmployeeTerritories modelNotifiedForEmployeeTerritories, out string error);
        public void AddData(ModelNotifiedForEmployeeTerritories modelNotifiedForEmployeeTerritories, out string error);

        //test
        public List<ModelNotifiedForEmployees> GetAll_Employees(out string error);
        //test
        public List<ModelNotifiedForTerritories> GetAll_Territories(out string error);
    }
}

