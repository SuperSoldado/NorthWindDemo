//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyApp.TransferObjects.REST
{
    public class UpdateEmployeesView
    {

private int _EmployeeID;
public int EmployeeID
{
    get { return _EmployeeID; }
    set {  
_EmployeeID = value;
}
}
private string _LastName;
public string LastName
{
    get { return _LastName; }
    set {  
_LastName = value;
}
}
private string _FirstName;
public string FirstName
{
    get { return _FirstName; }
    set {  
_FirstName = value;
}
}
private string _Title;
public string Title
{
    get { return _Title; }
    set {  
_Title = value;
}
}
private string _TitleOfCourtesy;
public string TitleOfCourtesy
{
    get { return _TitleOfCourtesy; }
    set {  
_TitleOfCourtesy = value;
}
}
private DateTime? _BirthDate;
public DateTime? BirthDate
{
    get { return _BirthDate; }
    set {  
_BirthDate = value;
}
}
private DateTime? _HireDate;
public DateTime? HireDate
{
    get { return _HireDate; }
    set {  
_HireDate = value;
}
}
private string _Address;
public string Address
{
    get { return _Address; }
    set {  
_Address = value;
}
}
private string _City;
public string City
{
    get { return _City; }
    set {  
_City = value;
}
}
private string _Region;
public string Region
{
    get { return _Region; }
    set {  
_Region = value;
}
}
private string _PostalCode;
public string PostalCode
{
    get { return _PostalCode; }
    set {  
_PostalCode = value;
}
}
private string _Country;
public string Country
{
    get { return _Country; }
    set {  
_Country = value;
}
}
private string _HomePhone;
public string HomePhone
{
    get { return _HomePhone; }
    set {  
_HomePhone = value;
}
}
private string _Extension;
public string Extension
{
    get { return _Extension; }
    set {  
_Extension = value;
}
}
private byte[] _Photo;
public byte[] Photo
{
    get { return _Photo; }
    set {  
_Photo = value;
}
}
private string _Notes;
public string Notes
{
    get { return _Notes; }
    set {  
_Notes = value;
}
}
private int? _ReportsTo;
public int? ReportsTo
{
    get { return _ReportsTo; }
    set {  
_ReportsTo = value;
}
}
private string _PhotoPath;
public string PhotoPath
{
    get { return _PhotoPath; }
    set {  
_PhotoPath = value;
}
}
    }
}
