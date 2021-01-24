//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFForms.Region
{
    public partial class ModelNotifiedForRegion: INotifyPropertyChanged
    {

public ModelNotifiedForRegion()
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
private string _RegionDescription;
public string RegionDescription
{
    get { return _RegionDescription; }
    set {  
    ItemChanged = true;
_RegionDescription = value;
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


