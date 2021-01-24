//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFForms.OrderDetails
{
    public partial class ModelNotifiedForOrders: INotifyPropertyChanged
    {

        

//Track[0012]

/// <summary>
/// Identify if the class data changed.
/// </summary>
public bool ItemChanged {get; set;}


private int _OrderID;
public int OrderID
{
    get { return _OrderID; }
    set {  
    ItemChanged = true;
_OrderID = value;
}
}

private string _CustomerID;
public string CustomerID
{
    get { return _CustomerID; }
    set {  
    ItemChanged = true;
_CustomerID = value;
}
}

private int _EmployeeID;
public int EmployeeID
{
    get { return _EmployeeID; }
    set {  
    ItemChanged = true;
_EmployeeID = value;
}
}

private DateTime _OrderDate;
public DateTime OrderDate
{
    get { return _OrderDate; }
    set {  
    ItemChanged = true;
_OrderDate = value;
}
}

private DateTime _RequiredDate;
public DateTime RequiredDate
{
    get { return _RequiredDate; }
    set {  
    ItemChanged = true;
_RequiredDate = value;
}
}

private DateTime _ShippedDate;
public DateTime ShippedDate
{
    get { return _ShippedDate; }
    set {  
    ItemChanged = true;
_ShippedDate = value;
}
}

private int _ShipVia;
public int ShipVia
{
    get { return _ShipVia; }
    set {  
    ItemChanged = true;
_ShipVia = value;
}
}

private decimal _Freight;
public decimal Freight
{
    get { return _Freight; }
    set {  
    ItemChanged = true;
_Freight = value;
}
}

private string _ShipName;
public string ShipName
{
    get { return _ShipName; }
    set {  
    ItemChanged = true;
_ShipName = value;
}
}

private string _ShipAddress;
public string ShipAddress
{
    get { return _ShipAddress; }
    set {  
    ItemChanged = true;
_ShipAddress = value;
}
}

private string _ShipCity;
public string ShipCity
{
    get { return _ShipCity; }
    set {  
    ItemChanged = true;
_ShipCity = value;
}
}

private string _ShipRegion;
public string ShipRegion
{
    get { return _ShipRegion; }
    set {  
    ItemChanged = true;
_ShipRegion = value;
}
}

private string _ShipPostalCode;
public string ShipPostalCode
{
    get { return _ShipPostalCode; }
    set {  
    ItemChanged = true;
_ShipPostalCode = value;
}
}

private string _ShipCountry;
public string ShipCountry
{
    get { return _ShipCountry; }
    set {  
    ItemChanged = true;
_ShipCountry = value;
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


