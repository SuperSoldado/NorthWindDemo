//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFForms.Products
{
    public partial class ModelNotifiedForCategories: INotifyPropertyChanged
    {

        

//Track[0012]

/// <summary>
/// Identify if the class data changed.
/// </summary>
public bool ItemChanged {get; set;}


private int _CategoryID;
public int CategoryID
{
    get { return _CategoryID; }
    set {  
    ItemChanged = true;
_CategoryID = value;
}
}

private string _CategoryName;
public string CategoryName
{
    get { return _CategoryName; }
    set {  
    ItemChanged = true;
_CategoryName = value;
}
}

private string _Description;
public string Description
{
    get { return _Description; }
    set {  
    ItemChanged = true;
_Description = value;
}
}

private byte[] _Picture;
public byte[] Picture
{
    get { return _Picture; }
    set {  
    ItemChanged = true;
_Picture = value;
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


