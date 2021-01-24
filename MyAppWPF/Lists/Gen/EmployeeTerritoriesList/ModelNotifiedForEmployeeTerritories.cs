//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFList.EmployeeTerritories
{
    public partial class ModelNotifiedForEmployeeTerritories: INotifyPropertyChanged
    {

public ModelNotifiedForEmployeeTerritories()
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

private int _EmployeeID;
public int EmployeeID
{
    get { return _EmployeeID; }
    set {  
    ItemChanged = true;
_EmployeeID = value;
    RaiseProperChanged();
}
}
private string _TerritoryID;
public string TerritoryID
{
    get { return _TerritoryID; }
    set {  
    ItemChanged = true;
_TerritoryID = value;
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


