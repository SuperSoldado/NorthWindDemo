//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFForms.EmployeeTerritories
{
    public partial class ModelNotifiedForTerritories: INotifyPropertyChanged
    {

        

//Track[0012]

/// <summary>
/// Identify if the class data changed.
/// </summary>
public bool ItemChanged {get; set;}


private string _TerritoryID;
public string TerritoryID
{
    get { return _TerritoryID; }
    set {  
    ItemChanged = true;
_TerritoryID = value;
}
}

private string _TerritoryDescription;
public string TerritoryDescription
{
    get { return _TerritoryDescription; }
    set {  
    ItemChanged = true;
_TerritoryDescription = value;
}
}

private int _RegionID;
public int RegionID
{
    get { return _RegionID; }
    set {  
    ItemChanged = true;
_RegionID = value;
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


