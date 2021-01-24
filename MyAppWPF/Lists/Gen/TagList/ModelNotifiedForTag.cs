//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFList.Tag
{
    public partial class ModelNotifiedForTag: INotifyPropertyChanged
    {

public ModelNotifiedForTag()
{
    this.NewItem = true;
}


        

//Track[0011]

/// <summary>
/// Identify if the class data changed.
/// </summary>
public bool ItemChanged {get; set;}

/// <summary>
/// If true, indicates a new item (used for insert). Default is 'true'. 
/// During load data operation (load from DB, fill the class), it is set to false.
/// </summary>
public bool NewItem { get; set; }

private int _TagID;
public int TagID
{
    get { return _TagID; }
    set {  
    ItemChanged = true;
_TagID = value;
    RaiseProperChanged();
}
}
private string _TextDesc;
public string TextDesc
{
    get { return _TextDesc; }
    set {  
    ItemChanged = true;
_TextDesc = value;
    RaiseProperChanged();
}
}
private string _TagType;
public string TagType
{
    get { return _TagType; }
    set {  
    ItemChanged = true;
_TagType = value;
    RaiseProperChanged();
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


