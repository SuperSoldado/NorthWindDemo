//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFList.Suppliers
{
    public partial class ModelNotifiedForSuppliers: INotifyPropertyChanged
    {

public ModelNotifiedForSuppliers()
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

private int _SupplierID;
public int SupplierID
{
    get { return _SupplierID; }
    set {  
    ItemChanged = true;
_SupplierID = value;
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
private string _ContactName;
public string ContactName
{
    get { return _ContactName; }
    set {  
    ItemChanged = true;
_ContactName = value;
    RaiseProperChanged();
}
}
private string _ContactTitle;
public string ContactTitle
{
    get { return _ContactTitle; }
    set {  
    ItemChanged = true;
_ContactTitle = value;
    RaiseProperChanged();
}
}
private string _Address;
public string Address
{
    get { return _Address; }
    set {  
    ItemChanged = true;
_Address = value;
    RaiseProperChanged();
}
}
private string _City;
public string City
{
    get { return _City; }
    set {  
    ItemChanged = true;
_City = value;
    RaiseProperChanged();
}
}
private string _Region;
public string Region
{
    get { return _Region; }
    set {  
    ItemChanged = true;
_Region = value;
    RaiseProperChanged();
}
}
private string _PostalCode;
public string PostalCode
{
    get { return _PostalCode; }
    set {  
    ItemChanged = true;
_PostalCode = value;
    RaiseProperChanged();
}
}
private string _Country;
public string Country
{
    get { return _Country; }
    set {  
    ItemChanged = true;
_Country = value;
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
private string _Fax;
public string Fax
{
    get { return _Fax; }
    set {  
    ItemChanged = true;
_Fax = value;
    RaiseProperChanged();
}
}
private string _HomePage;
public string HomePage
{
    get { return _HomePage; }
    set {  
    ItemChanged = true;
_HomePage = value;
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


