//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFForms.Territories
{
    public partial class ModelNotifiedForTerritories: INotifyPropertyChanged
    {

public ModelNotifiedForTerritories()
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
private string _TerritoryDescription;
public string TerritoryDescription
{
    get { return _TerritoryDescription; }
    set {  
    ItemChanged = true;
_TerritoryDescription = value;
    RaiseProperChanged();
}
}
private int _RegionID;
public int RegionID
{
    get { return _RegionID; }
    set {  
    ItemChanged = true;
_RegionID = value;
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


