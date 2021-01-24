//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFList.Tag
{
    public interface IWPFTagDataConnection
    {
        public TagDataContext GetDataContext(out string error);
        List<ModelNotifiedForTag> GetAllTag(out string error);
        public void SaveData(ModelNotifiedForTag modelNotifiedForTag, out string error);
        public void DeleteData(ModelNotifiedForTag modelNotifiedForTag, out string error);
        public void AddData(ModelNotifiedForTag modelNotifiedForTag, out string error);

    }
}

