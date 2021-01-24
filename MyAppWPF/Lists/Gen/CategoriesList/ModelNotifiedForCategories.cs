//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFList.Categories
{
    public partial class ModelNotifiedForCategories: INotifyPropertyChanged
    {

public ModelNotifiedForCategories()
{
    this.NewItem = true;
}


        
        private Visibility _BtnAddPictureVisibility { get; set; }
        public Visibility BtnAddPictureVisibility
        {
            get
            {
                if ((this.Picture == null) || (this.Picture.Length == 0))
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            set 
            { 
                _BtnAddPictureVisibility = value; 
                RaiseProperChanged();
            }
        }

        private Visibility _BtnExcludePictureVisibility { get; set; }
        public Visibility BtnExcludePictureVisibility
        {
            get
            {
                if ((this.Picture == null) || (this.Picture.Length == 0))
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
            set 
            { 
                _BtnExcludePictureVisibility = value; 
                RaiseProperChanged();
            }
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

private int _CategoryID;
public int CategoryID
{
    get { return _CategoryID; }
    set {  
    ItemChanged = true;
_CategoryID = value;
    RaiseProperChanged();
}
}
private string _CategoryName;
public string CategoryName
{
    get { return _CategoryName; }
    set {  
    ItemChanged = true;
_CategoryName = value;
    RaiseProperChanged();
}
}
private string _Description;
public string Description
{
    get { return _Description; }
    set {  
    ItemChanged = true;
_Description = value;
    RaiseProperChanged();
}
}
private byte[] _Picture;
public byte[] Picture
{
    get { return _Picture; }
    set {  
    ItemChanged = true;
_Picture = value;
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


