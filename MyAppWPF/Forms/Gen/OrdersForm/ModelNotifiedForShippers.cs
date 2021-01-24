//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFForms.Orders
{
    public partial class ModelNotifiedForShippers: INotifyPropertyChanged
    {

        

//Track[0012]

/// <summary>
/// Identify if the class data changed.
/// </summary>
public bool ItemChanged {get; set;}


private int _ShipperID;
public int ShipperID
{
    get { return _ShipperID; }
    set {  
    ItemChanged = true;
_ShipperID = value;
}
}

private string _CompanyName;
public string CompanyName
{
    get { return _CompanyName; }
    set {  
    ItemChanged = true;
_CompanyName = value;
}
}

private string _Phone;
public string Phone
{
    get { return _Phone; }
    set {  
    ItemChanged = true;
_Phone = value;
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


