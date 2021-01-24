//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFForms.Orders
{
    public partial class ModelNotifiedForCustomers: INotifyPropertyChanged
    {

        

//Track[0012]

/// <summary>
/// Identify if the class data changed.
/// </summary>
public bool ItemChanged {get; set;}


private string _CustomerID;
public string CustomerID
{
    get { return _CustomerID; }
    set {  
    ItemChanged = true;
_CustomerID = value;
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

private string _ContactName;
public string ContactName
{
    get { return _ContactName; }
    set {  
    ItemChanged = true;
_ContactName = value;
}
}

private string _ContactTitle;
public string ContactTitle
{
    get { return _ContactTitle; }
    set {  
    ItemChanged = true;
_ContactTitle = value;
}
}

private string _Address;
public string Address
{
    get { return _Address; }
    set {  
    ItemChanged = true;
_Address = value;
}
}

private string _City;
public string City
{
    get { return _City; }
    set {  
    ItemChanged = true;
_City = value;
}
}

private string _Region;
public string Region
{
    get { return _Region; }
    set {  
    ItemChanged = true;
_Region = value;
}
}

private string _PostalCode;
public string PostalCode
{
    get { return _PostalCode; }
    set {  
    ItemChanged = true;
_PostalCode = value;
}
}

private string _Country;
public string Country
{
    get { return _Country; }
    set {  
    ItemChanged = true;
_Country = value;
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

private string _Fax;
public string Fax
{
    get { return _Fax; }
    set {  
    ItemChanged = true;
_Fax = value;
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


