//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFForms.CustomerCustomerDemo
{
    public partial class ModelNotifiedForCustomerDemographics: INotifyPropertyChanged
    {

        

//Track[0012]

/// <summary>
/// Identify if the class data changed.
/// </summary>
public bool ItemChanged {get; set;}


private string _CustomerTypeID;
public string CustomerTypeID
{
    get { return _CustomerTypeID; }
    set {  
    ItemChanged = true;
_CustomerTypeID = value;
}
}

private string _CustomerDesc;
public string CustomerDesc
{
    get { return _CustomerDesc; }
    set {  
    ItemChanged = true;
_CustomerDesc = value;
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


