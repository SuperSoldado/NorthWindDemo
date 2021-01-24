//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFForms.Territories
{
    public partial class ModelNotifiedForRegion: INotifyPropertyChanged
    {

        

//Track[0012]

/// <summary>
/// Identify if the class data changed.
/// </summary>
public bool ItemChanged {get; set;}


private int _RegionID;
public int RegionID
{
    get { return _RegionID; }
    set {  
    ItemChanged = true;
_RegionID = value;
}
}

private string _RegionDescription;
public string RegionDescription
{
    get { return _RegionDescription; }
    set {  
    ItemChanged = true;
_RegionDescription = value;
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


