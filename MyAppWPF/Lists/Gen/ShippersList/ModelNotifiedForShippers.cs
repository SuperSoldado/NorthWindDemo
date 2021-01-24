//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFList.Shippers
{
    public partial class ModelNotifiedForShippers: INotifyPropertyChanged
    {

public ModelNotifiedForShippers()
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

private int _ShipperID;
public int ShipperID
{
    get { return _ShipperID; }
    set {  
    ItemChanged = true;
_ShipperID = value;
    RaiseProperChanged();
}
}
private string _CompanyName;
public string CompanyName
{
    get { return _CompanyName; }
    set {  
    ItemChanged = true;
_CompanyName = value;
    RaiseProperChanged();
}
}
private string _Phone;
public string Phone
{
    get { return _Phone; }
    set {  
    ItemChanged = true;
_Phone = value;
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


