//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFList.CustomerDemographics
{
    public partial class ModelNotifiedForCustomerDemographics: INotifyPropertyChanged
    {

public ModelNotifiedForCustomerDemographics()
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

private string _CustomerTypeID;
public string CustomerTypeID
{
    get { return _CustomerTypeID; }
    set {  
    ItemChanged = true;
_CustomerTypeID = value;
    RaiseProperChanged();
}
}
private string _CustomerDesc;
public string CustomerDesc
{
    get { return _CustomerDesc; }
    set {  
    ItemChanged = true;
_CustomerDesc = value;
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


