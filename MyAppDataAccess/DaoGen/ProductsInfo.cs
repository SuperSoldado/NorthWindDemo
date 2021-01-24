/************************************************************************************
* Codetomat version Alpha
* Generated at: 14.12.2020
* This is an auto-generated file. 
************************************************************************************/
using System;

namespace MyApp.Data.Info
{
    public partial class ProductsInfo
    {
        public ProductsInfo()
        {
ProductID = int.MinValue;
        }

        private int _ProductID;

/// <summary>
/// Represent (table.field) Products.ProductID
/// </summary>
public int ProductID
{
get { return _ProductID; }
set { _ProductID = value; }
}
private string _ProductName;

/// <summary>
/// Represent (table.field) Products.ProductName
/// </summary>
public string ProductName
{
get { return _ProductName; }
set { _ProductName = value; }
}
private string _FK0_CompanyName;

/// <summary>
/// Products.SupplierID --> Suppliers.CompanyName
/// </summary>
public string FK0_CompanyName
{
get { return _FK0_CompanyName; }
set { _FK0_CompanyName = value; }
}
private int? _SupplierID;

/// <summary>
/// Foreing Key Description : Suppliers.FK0_CompanyName
/// </summary>
public int? SupplierID
{
get { return _SupplierID; }
set { _SupplierID = value; }
}
private string _FK1_CategoryName;

/// <summary>
/// Products.CategoryID --> Categories.CategoryName
/// </summary>
public string FK1_CategoryName
{
get { return _FK1_CategoryName; }
set { _FK1_CategoryName = value; }
}
private int? _CategoryID;

/// <summary>
/// Foreing Key Description : Categories.FK1_CategoryName
/// </summary>
public int? CategoryID
{
get { return _CategoryID; }
set { _CategoryID = value; }
}
private string _QuantityPerUnit;

/// <summary>
/// Represent (table.field) Products.QuantityPerUnit
/// </summary>
public string QuantityPerUnit
{
get { return _QuantityPerUnit; }
set { _QuantityPerUnit = value; }
}
private decimal? _UnitPrice;

/// <summary>
/// Represent (table.field) Products.UnitPrice
/// </summary>
public decimal? UnitPrice
{
get { return _UnitPrice; }
set { _UnitPrice = value; }
}
private Int16? _UnitsInStock;

/// <summary>
/// Represent (table.field) Products.UnitsInStock
/// </summary>
public Int16? UnitsInStock
{
get { return _UnitsInStock; }
set { _UnitsInStock = value; }
}
private Int16? _UnitsOnOrder;

/// <summary>
/// Represent (table.field) Products.UnitsOnOrder
/// </summary>
public Int16? UnitsOnOrder
{
get { return _UnitsOnOrder; }
set { _UnitsOnOrder = value; }
}
private Int16? _ReorderLevel;

/// <summary>
/// Represent (table.field) Products.ReorderLevel
/// </summary>
public Int16? ReorderLevel
{
get { return _ReorderLevel; }
set { _ReorderLevel = value; }
}
private bool? _Discontinued;

/// <summary>
/// Represent (table.field) Products.Discontinued
/// </summary>
public bool? Discontinued
{
get { return _Discontinued; }
set { _Discontinued = value; }
}
    }
}
