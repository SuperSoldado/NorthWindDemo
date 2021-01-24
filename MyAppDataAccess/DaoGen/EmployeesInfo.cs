/************************************************************************************
* Codetomat version Alpha
* Generated at: 14.12.2020
* This is an auto-generated file. 
************************************************************************************/
using System;

namespace MyApp.Data.Info
{
    public partial class EmployeesInfo
    {
        public EmployeesInfo()
        {
EmployeeID = int.MinValue;
        }

        private int _EmployeeID;

/// <summary>
/// Represent (table.field) Employees.EmployeeID
/// </summary>
public int EmployeeID
{
get { return _EmployeeID; }
set { _EmployeeID = value; }
}
private string _LastName;

/// <summary>
/// Represent (table.field) Employees.LastName
/// </summary>
public string LastName
{
get { return _LastName; }
set { _LastName = value; }
}
private string _FirstName;

/// <summary>
/// Represent (table.field) Employees.FirstName
/// </summary>
public string FirstName
{
get { return _FirstName; }
set { _FirstName = value; }
}
private string _Title;

/// <summary>
/// Represent (table.field) Employees.Title
/// </summary>
public string Title
{
get { return _Title; }
set { _Title = value; }
}
private string _TitleOfCourtesy;

/// <summary>
/// Represent (table.field) Employees.TitleOfCourtesy
/// </summary>
public string TitleOfCourtesy
{
get { return _TitleOfCourtesy; }
set { _TitleOfCourtesy = value; }
}
private DateTime? _BirthDate;

/// <summary>
/// Represent (table.field) Employees.BirthDate
/// </summary>
public DateTime? BirthDate
{
get { return _BirthDate; }
set { _BirthDate = value; }
}
private DateTime? _HireDate;

/// <summary>
/// Represent (table.field) Employees.HireDate
/// </summary>
public DateTime? HireDate
{
get { return _HireDate; }
set { _HireDate = value; }
}
private string _Address;

/// <summary>
/// Represent (table.field) Employees.Address
/// </summary>
public string Address
{
get { return _Address; }
set { _Address = value; }
}
private string _City;

/// <summary>
/// Represent (table.field) Employees.City
/// </summary>
public string City
{
get { return _City; }
set { _City = value; }
}
private string _Region;

/// <summary>
/// Represent (table.field) Employees.Region
/// </summary>
public string Region
{
get { return _Region; }
set { _Region = value; }
}
private string _PostalCode;

/// <summary>
/// Represent (table.field) Employees.PostalCode
/// </summary>
public string PostalCode
{
get { return _PostalCode; }
set { _PostalCode = value; }
}
private string _Country;

/// <summary>
/// Represent (table.field) Employees.Country
/// </summary>
public string Country
{
get { return _Country; }
set { _Country = value; }
}
private string _HomePhone;

/// <summary>
/// Represent (table.field) Employees.HomePhone
/// </summary>
public string HomePhone
{
get { return _HomePhone; }
set { _HomePhone = value; }
}
private string _Extension;

/// <summary>
/// Represent (table.field) Employees.Extension
/// </summary>
public string Extension
{
get { return _Extension; }
set { _Extension = value; }
}
private byte[] _Photo;

/// <summary>
/// Represent (table.field) Employees.Photo
/// </summary>
public byte[] Photo
{
get { return _Photo; }
set { _Photo = value; }
}
private string _Notes;

/// <summary>
/// Represent (table.field) Employees.Notes
/// </summary>
public string Notes
{
get { return _Notes; }
set { _Notes = value; }
}
private string _FK0_FirstName;

/// <summary>
/// Employees.ReportsTo --> Employees.FirstName
/// </summary>
public string FK0_FirstName
{
get { return _FK0_FirstName; }
set { _FK0_FirstName = value; }
}
private int? _ReportsTo;

/// <summary>
/// Foreing Key Description : Employees.FK0_FirstName
/// </summary>
public int? ReportsTo
{
get { return _ReportsTo; }
set { _ReportsTo = value; }
}
private string _PhotoPath;

/// <summary>
/// Represent (table.field) Employees.PhotoPath
/// </summary>
public string PhotoPath
{
get { return _PhotoPath; }
set { _PhotoPath = value; }
}
    }
}
