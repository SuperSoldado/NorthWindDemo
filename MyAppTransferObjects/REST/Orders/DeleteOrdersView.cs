//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyApp.TransferObjects.REST
{
    public class DeleteOrdersView
    {

private int _OrderID;
public int OrderID
{
    get { return _OrderID; }
    set {  
_OrderID = value;
}
}
private string _CustomerID;
public string CustomerID
{
    get { return _CustomerID; }
    set {  
_CustomerID = value;
}
}
private int? _EmployeeID;
public int? EmployeeID
{
    get { return _EmployeeID; }
    set {  
_EmployeeID = value;
}
}
private DateTime? _OrderDate;
public DateTime? OrderDate
{
    get { return _OrderDate; }
    set {  
_OrderDate = value;
}
}
private DateTime? _RequiredDate;
public DateTime? RequiredDate
{
    get { return _RequiredDate; }
    set {  
_RequiredDate = value;
}
}
private DateTime? _ShippedDate;
public DateTime? ShippedDate
{
    get { return _ShippedDate; }
    set {  
_ShippedDate = value;
}
}
private int? _ShipVia;
public int? ShipVia
{
    get { return _ShipVia; }
    set {  
_ShipVia = value;
}
}
private decimal? _Freight;
public decimal? Freight
{
    get { return _Freight; }
    set {  
_Freight = value;
}
}
private string _ShipName;
public string ShipName
{
    get { return _ShipName; }
    set {  
_ShipName = value;
}
}
private string _ShipAddress;
public string ShipAddress
{
    get { return _ShipAddress; }
    set {  
_ShipAddress = value;
}
}
private string _ShipCity;
public string ShipCity
{
    get { return _ShipCity; }
    set {  
_ShipCity = value;
}
}
private string _ShipRegion;
public string ShipRegion
{
    get { return _ShipRegion; }
    set {  
_ShipRegion = value;
}
}
private string _ShipPostalCode;
public string ShipPostalCode
{
    get { return _ShipPostalCode; }
    set {  
_ShipPostalCode = value;
}
}
private string _ShipCountry;
public string ShipCountry
{
    get { return _ShipCountry; }
    set {  
_ShipCountry = value;
}
}
    }
}
