//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFForms.Employees
{
    public interface IWPFEmployeesDataConnection
    {
        public EmployeesDataContext GetDataContext(int EmployeeID,out string error);
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public EmployeesDataContext GetEmptyDataContext(out string error);
        ModelNotifiedForEmployees GetEmployeesByID(int EmployeeID, out string error);
        public void SaveData(ModelNotifiedForEmployees modelNotifiedForEmployees, out string error);
        public void DeleteData(ModelNotifiedForEmployees modelNotifiedForEmployees, out string error);
        public void AddData(ModelNotifiedForEmployees modelNotifiedForEmployees, out string error);

        //test
        public List<ModelNotifiedForEmployees> GetAll_Employees(out string error);
    }
}

