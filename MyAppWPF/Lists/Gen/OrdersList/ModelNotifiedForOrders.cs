//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFList.Orders
{
    public partial class ModelNotifiedForOrders: INotifyPropertyChanged
    {

public ModelNotifiedForOrders()
{
    this.NewItem = true;
}


        

//Track[0011]

/// <summary>
/// Identify if the class data changed.
/// </summary>
public bool ItemChanged {get; set;}

/// <summary>
/// If true, indicates a new item (used for insert). Default is 'true'. 
/// During load data operation (load from DB, fill the class), it is set to false.
/// </summary>
public bool NewItem { get; set; }

private int _OrderID;
public int OrderID
{
    get { return _OrderID; }
    set {  
    ItemChanged = true;
_OrderID = value;
    RaiseProperChanged();
}
}
private string _CustomerID;
public string CustomerID
{
    get { return _CustomerID; }
    set {  
    ItemChanged = true;
_CustomerID = value;
    RaiseProperChanged();
}
}
private int? _EmployeeID;
public int? EmployeeID
{
    get { return _EmployeeID; }
    set {  
    ItemChanged = true;
_EmployeeID = value;
    RaiseProperChanged();
}
}
private DateTime? _OrderDate;
public DateTime? OrderDate
{
    get { return _OrderDate; }
    set {  
    ItemChanged = true;
_OrderDate = value;
    RaiseProperChanged();
}
}
private DateTime? _RequiredDate;
public DateTime? RequiredDate
{
    get { return _RequiredDate; }
    set {  
    ItemChanged = true;
_RequiredDate = value;
    RaiseProperChanged();
}
}
private DateTime? _ShippedDate;
public DateTime? ShippedDate
{
    get { return _ShippedDate; }
    set {  
    ItemChanged = true;
_ShippedDate = value;
    RaiseProperChanged();
}
}
private int? _ShipVia;
public int? ShipVia
{
    get { return _ShipVia; }
    set {  
    ItemChanged = true;
_ShipVia = value;
    RaiseProperChanged();
}
}
private decimal? _Freight;
public decimal? Freight
{
    get { return _Freight; }
    set {  
    ItemChanged = true;
_Freight = value;
    RaiseProperChanged();
}
}
private string _ShipName;
public string ShipName
{
    get { return _ShipName; }
    set {  
    ItemChanged = true;
_ShipName = value;
    RaiseProperChanged();
}
}
private string _ShipAddress;
public string ShipAddress
{
    get { return _ShipAddress; }
    set {  
    ItemChanged = true;
_ShipAddress = value;
    RaiseProperChanged();
}
}
private string _ShipCity;
public string ShipCity
{
    get { return _ShipCity; }
    set {  
    ItemChanged = true;
_ShipCity = value;
    RaiseProperChanged();
}
}
private string _ShipRegion;
public string ShipRegion
{
    get { return _ShipRegion; }
    set {  
    ItemChanged = true;
_ShipRegion = value;
    RaiseProperChanged();
}
}
private string _ShipPostalCode;
public string ShipPostalCode
{
    get { return _ShipPostalCode; }
    set {  
    ItemChanged = true;
_ShipPostalCode = value;
    RaiseProperChanged();
}
}
private string _ShipCountry;
public string ShipCountry
{
    get { return _ShipCountry; }
    set {  
    ItemChanged = true;
_ShipCountry = value;
    RaiseProperChanged();
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


