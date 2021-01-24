//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFList.TagEmployee
{
    public interface IWPFTagEmployeeDataConnection
    {
        public TagEmployeeDataContext GetDataContext(out string error);
        List<ModelNotifiedForTagEmployee> GetAllTagEmployee(out string error);
        public void SaveData(ModelNotifiedForTagEmployee modelNotifiedForTagEmployee, out string error);
        public void DeleteData(ModelNotifiedForTagEmployee modelNotifiedForTagEmployee, out string error);
        public void AddData(ModelNotifiedForTagEmployee modelNotifiedForTagEmployee, out string error);

        //test
        public List<ModelNotifiedForEmployees> GetAll_Employees(out string error);
        //test
        public List<ModelNotifiedForTag> GetAll_Tag(out string error);
    }
}

