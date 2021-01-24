//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFForms.TagEmployee
{
    public partial class ModelNotifiedForEmployees: INotifyPropertyChanged
    {

        

//Track[0012]

/// <summary>
/// Identify if the class data changed.
/// </summary>
public bool ItemChanged {get; set;}


private int _EmployeeID;
public int EmployeeID
{
    get { return _EmployeeID; }
    set {  
    ItemChanged = true;
_EmployeeID = value;
}
}

private string _LastName;
public string LastName
{
    get { return _LastName; }
    set {  
    ItemChanged = true;
_LastName = value;
}
}

private string _FirstName;
public string FirstName
{
    get { return _FirstName; }
    set {  
    ItemChanged = true;
_FirstName = value;
}
}

private string _Title;
public string Title
{
    get { return _Title; }
    set {  
    ItemChanged = true;
_Title = value;
}
}

private string _TitleOfCourtesy;
public string TitleOfCourtesy
{
    get { return _TitleOfCourtesy; }
    set {  
    ItemChanged = true;
_TitleOfCourtesy = value;
}
}

private DateTime _BirthDate;
public DateTime BirthDate
{
    get { return _BirthDate; }
    set {  
    ItemChanged = true;
_BirthDate = value;
}
}

private DateTime _HireDate;
public DateTime HireDate
{
    get { return _HireDate; }
    set {  
    ItemChanged = true;
_HireDate = value;
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

private string _HomePhone;
public string HomePhone
{
    get { return _HomePhone; }
    set {  
    ItemChanged = true;
_HomePhone = value;
}
}

private string _Extension;
public string Extension
{
    get { return _Extension; }
    set {  
    ItemChanged = true;
_Extension = value;
}
}

private byte[] _Photo;
public byte[] Photo
{
    get { return _Photo; }
    set {  
    ItemChanged = true;
_Photo = value;
}
}

private string _Notes;
public string Notes
{
    get { return _Notes; }
    set {  
    ItemChanged = true;
_Notes = value;
}
}

private int _ReportsTo;
public int ReportsTo
{
    get { return _ReportsTo; }
    set {  
    ItemChanged = true;
_ReportsTo = value;
}
}

private string _PhotoPath;
public string PhotoPath
{
    get { return _PhotoPath; }
    set {  
    ItemChanged = true;
_PhotoPath = value;
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


