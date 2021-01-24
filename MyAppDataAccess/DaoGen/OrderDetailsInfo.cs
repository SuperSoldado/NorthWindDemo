/************************************************************************************
* Codetomat version Alpha
* Generated at: 14.12.2020
* This is an auto-generated file. 
************************************************************************************/
using System;

namespace MyApp.Data.Info
{
    public partial class OrderDetailsInfo
    {
        public OrderDetailsInfo()
        {
OrderID = int.MinValue;
ProductID = int.MinValue;
UnitPrice = decimal.MinValue;
Quantity = Int16.MinValue;
Discount = decimal.MinValue;
        }

        private string _FK0_ShipName;

/// <summary>
/// OrderDetails.OrderID --> Orders.ShipName
/// </summary>
public string FK0_ShipName
{
get { return _FK0_ShipName; }
set { _FK0_ShipName = value; }
}
private int _OrderID;

/// <summary>
/// Foreing Key Description : Orders.FK0_ShipName
/// </summary>
public int OrderID
{
get { return _OrderID; }
set { _OrderID = value; }
}
private string _FK1_ProductName;

/// <summary>
/// OrderDetails.ProductID --> Products.ProductName
/// </summary>
public string FK1_ProductName
{
get { return _FK1_ProductName; }
set { _FK1_ProductName = value; }
}
private int _ProductID;

/// <summary>
/// Foreing Key Description : Products.FK1_ProductName
/// </summary>
public int ProductID
{
get { return _ProductID; }
set { _ProductID = value; }
}
private decimal _UnitPrice;

/// <summary>
/// Represent (table.field) OrderDetails.UnitPrice
/// </summary>
public decimal UnitPrice
{
get { return _UnitPrice; }
set { _UnitPrice = value; }
}
private Int16 _Quantity;

/// <summary>
/// Represent (table.field) OrderDetails.Quantity
/// </summary>
public Int16 Quantity
{
get { return _Quantity; }
set { _Quantity = value; }
}
private decimal _Discount;

/// <summary>
/// Represent (table.field) OrderDetails.Discount
/// </summary>
public decimal Discount
{
get { return _Discount; }
set { _Discount = value; }
}
    }
}
