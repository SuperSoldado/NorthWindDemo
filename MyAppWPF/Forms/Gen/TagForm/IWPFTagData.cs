//Track[0005] WPF_Shared_IData.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;

namespace MyApp.WPFForms.Tag
{
    public interface IWPFTagDataConnection
    {
        public TagDataContext GetDataContext(int TagID,out string error);
        /// <summary>
        /// Empty data context is used when the from is loaded without ID (the from is in "insert mode")
        /// </summary>
        public TagDataContext GetEmptyDataContext(out string error);
        ModelNotifiedForTag GetTagByID(int TagID, out string error);
        public void SaveData(ModelNotifiedForTag modelNotifiedForTag, out string error);
        public void DeleteData(ModelNotifiedForTag modelNotifiedForTag, out string error);
        public void AddData(ModelNotifiedForTag modelNotifiedForTag, out string error);

    }
}

