//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyApp.TransferObjects.REST
{
    public class UpdateProductsView
    {

private int _ProductID;
public int ProductID
{
    get { return _ProductID; }
    set {  
_ProductID = value;
}
}
private string _ProductName;
public string ProductName
{
    get { return _ProductName; }
    set {  
_ProductName = value;
}
}
private int? _SupplierID;
public int? SupplierID
{
    get { return _SupplierID; }
    set {  
_SupplierID = value;
}
}
private int? _CategoryID;
public int? CategoryID
{
    get { return _CategoryID; }
    set {  
_CategoryID = value;
}
}
private string _QuantityPerUnit;
public string QuantityPerUnit
{
    get { return _QuantityPerUnit; }
    set {  
_QuantityPerUnit = value;
}
}
private decimal? _UnitPrice;
public decimal? UnitPrice
{
    get { return _UnitPrice; }
    set {  
_UnitPrice = value;
}
}
private Int16? _UnitsInStock;
public Int16? UnitsInStock
{
    get { return _UnitsInStock; }
    set {  
_UnitsInStock = value;
}
}
private Int16? _UnitsOnOrder;
public Int16? UnitsOnOrder
{
    get { return _UnitsOnOrder; }
    set {  
_UnitsOnOrder = value;
}
}
private Int16? _ReorderLevel;
public Int16? ReorderLevel
{
    get { return _ReorderLevel; }
    set {  
_ReorderLevel = value;
}
}
private bool _Discontinued;
public bool Discontinued
{
    get { return _Discontinued; }
    set {  
_Discontinued = value;
}
}
    }
}
