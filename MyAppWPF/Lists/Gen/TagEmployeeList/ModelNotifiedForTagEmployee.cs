//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFList.TagEmployee
{
    public partial class ModelNotifiedForTagEmployee: INotifyPropertyChanged
    {

public ModelNotifiedForTagEmployee()
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

private int _TagEmployeeID;
public int TagEmployeeID
{
    get { return _TagEmployeeID; }
    set {  
    ItemChanged = true;
_TagEmployeeID = value;
    RaiseProperChanged();
}
}
private int? _EmployeeIDFK;
public int? EmployeeIDFK
{
    get { return _EmployeeIDFK; }
    set {  
    ItemChanged = true;
_EmployeeIDFK = value;
    RaiseProperChanged();
}
}
private int? _TagFK;
public int? TagFK
{
    get { return _TagFK; }
    set {  
    ItemChanged = true;
_TagFK = value;
    RaiseProperChanged();
}
}
private string _TagEmployeeTextDesc;
public string TagEmployeeTextDesc
{
    get { return _TagEmployeeTextDesc; }
    set {  
    ItemChanged = true;
_TagEmployeeTextDesc = value;
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


