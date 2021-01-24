/************************************************************************************
* Codetomat version Alpha
* Generated at: 14.12.2020
* This is an auto-generated file. 
************************************************************************************/
using System;

namespace MyApp.Data.Info
{
    public partial class OrdersInfo
    {
        public OrdersInfo()
        {
OrderID = int.MinValue;
        }

        private int _OrderID;

/// <summary>
/// Represent (table.field) Orders.OrderID
/// </summary>
public int OrderID
{
get { return _OrderID; }
set { _OrderID = value; }
}
private string _FK0_CompanyName;

/// <summary>
/// Orders.CustomerID --> Customers.CompanyName
/// </summary>
public string FK0_CompanyName
{
get { return _FK0_CompanyName; }
set { _FK0_CompanyName = value; }
}
private string _CustomerID;

/// <summary>
/// Foreing Key Description : Customers.FK0_CompanyName
/// </summary>
public string CustomerID
{
get { return _CustomerID; }
set { _CustomerID = value; }
}
private string _FK1_LastName;

/// <summary>
/// Orders.EmployeeID --> Employees.LastName
/// </summary>
public string FK1_LastName
{
get { return _FK1_LastName; }
set { _FK1_LastName = value; }
}
private int? _EmployeeID;

/// <summary>
/// Foreing Key Description : Employees.FK1_LastName
/// </summary>
public int? EmployeeID
{
get { return _EmployeeID; }
set { _EmployeeID = value; }
}
private DateTime? _OrderDate;

/// <summary>
/// Represent (table.field) Orders.OrderDate
/// </summary>
public DateTime? OrderDate
{
get { return _OrderDate; }
set { _OrderDate = value; }
}
private DateTime? _RequiredDate;

/// <summary>
/// Represent (table.field) Orders.RequiredDate
/// </summary>
public DateTime? RequiredDate
{
get { return _RequiredDate; }
set { _RequiredDate = value; }
}
private DateTime? _ShippedDate;

/// <summary>
/// Represent (table.field) Orders.ShippedDate
/// </summary>
public DateTime? ShippedDate
{
get { return _ShippedDate; }
set { _ShippedDate = value; }
}
private string _FK2_CompanyName;

/// <summary>
/// Orders.ShipVia --> Shippers.CompanyName
/// </summary>
public string FK2_CompanyName
{
get { return _FK2_CompanyName; }
set { _FK2_CompanyName = value; }
}
private int? _ShipVia;

/// <summary>
/// Foreing Key Description : Shippers.FK2_CompanyName
/// </summary>
public int? ShipVia
{
get { return _ShipVia; }
set { _ShipVia = value; }
}
private decimal? _Freight;

/// <summary>
/// Represent (table.field) Orders.Freight
/// </summary>
public decimal? Freight
{
get { return _Freight; }
set { _Freight = value; }
}
private string _ShipName;

/// <summary>
/// Represent (table.field) Orders.ShipName
/// </summary>
public string ShipName
{
get { return _ShipName; }
set { _ShipName = value; }
}
private string _ShipAddress;

/// <summary>
/// Represent (table.field) Orders.ShipAddress
/// </summary>
public string ShipAddress
{
get { return _ShipAddress; }
set { _ShipAddress = value; }
}
private string _ShipCity;

/// <summary>
/// Represent (table.field) Orders.ShipCity
/// </summary>
public string ShipCity
{
get { return _ShipCity; }
set { _ShipCity = value; }
}
private string _ShipRegion;

/// <summary>
/// Represent (table.field) Orders.ShipRegion
/// </summary>
public string ShipRegion
{
get { return _ShipRegion; }
set { _ShipRegion = value; }
}
private string _ShipPostalCode;

/// <summary>
/// Represent (table.field) Orders.ShipPostalCode
/// </summary>
public string ShipPostalCode
{
get { return _ShipPostalCode; }
set { _ShipPostalCode = value; }
}
private string _ShipCountry;

/// <summary>
/// Represent (table.field) Orders.ShipCountry
/// </summary>
public string ShipCountry
{
get { return _ShipCountry; }
set { _ShipCountry = value; }
}
    }
}
