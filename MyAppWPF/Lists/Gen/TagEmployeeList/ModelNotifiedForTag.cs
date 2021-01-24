//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFList.TagEmployee
{
    public partial class ModelNotifiedForTag: INotifyPropertyChanged
    {

        

//Track[0012]

/// <summary>
/// Identify if the class data changed.
/// </summary>
public bool ItemChanged {get; set;}


private int _TagID;
public int TagID
{
    get { return _TagID; }
    set {  
    ItemChanged = true;
_TagID = value;
}
}

private string _TextDesc;
public string TextDesc
{
    get { return _TextDesc; }
    set {  
    ItemChanged = true;
_TextDesc = value;
}
}

private string _TagType;
public string TagType
{
    get { return _TagType; }
    set {  
    ItemChanged = true;
_TagType = value;
}
}

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}


