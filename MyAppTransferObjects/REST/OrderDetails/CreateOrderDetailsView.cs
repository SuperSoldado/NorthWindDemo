//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyApp.TransferObjects.REST
{
    public class CreateOrderDetailsView
    {

private int _OrderID;
public int OrderID
{
    get { return _OrderID; }
    set {  
_OrderID = value;
}
}
private int _ProductID;
public int ProductID
{
    get { return _ProductID; }
    set {  
_ProductID = value;
}
}
private decimal _UnitPrice;
public decimal UnitPrice
{
    get { return _UnitPrice; }
    set {  
_UnitPrice = value;
}
}
private Int16 _Quantity;
public Int16 Quantity
{
    get { return _Quantity; }
    set {  
_Quantity = value;
}
}
private decimal _Discount;
public decimal Discount
{
    get { return _Discount; }
    set {  
_Discount = value;
}
}
    }
}
