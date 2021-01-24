//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFForms.Products
{
    public partial class ModelNotifiedForProducts: INotifyPropertyChanged
    {

public ModelNotifiedForProducts()
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

private int _ProductID;
public int ProductID
{
    get { return _ProductID; }
    set {  
    ItemChanged = true;
_ProductID = value;
    RaiseProperChanged();
}
}
private string _ProductName;
public string ProductName
{
    get { return _ProductName; }
    set {  
    ItemChanged = true;
_ProductName = value;
    RaiseProperChanged();
}
}
private int? _SupplierID;
public int? SupplierID
{
    get { return _SupplierID; }
    set {  
    ItemChanged = true;
_SupplierID = value;
    RaiseProperChanged();
}
}
private int? _CategoryID;
public int? CategoryID
{
    get { return _CategoryID; }
    set {  
    ItemChanged = true;
_CategoryID = value;
    RaiseProperChanged();
}
}
private string _QuantityPerUnit;
public string QuantityPerUnit
{
    get { return _QuantityPerUnit; }
    set {  
    ItemChanged = true;
_QuantityPerUnit = value;
    RaiseProperChanged();
}
}
private decimal? _UnitPrice;
public decimal? UnitPrice
{
    get { return _UnitPrice; }
    set {  
    ItemChanged = true;
_UnitPrice = value;
    RaiseProperChanged();
}
}
private Int16? _UnitsInStock;
public Int16? UnitsInStock
{
    get { return _UnitsInStock; }
    set {  
    ItemChanged = true;
_UnitsInStock = value;
    RaiseProperChanged();
}
}
private Int16? _UnitsOnOrder;
public Int16? UnitsOnOrder
{
    get { return _UnitsOnOrder; }
    set {  
    ItemChanged = true;
_UnitsOnOrder = value;
    RaiseProperChanged();
}
}
private Int16? _ReorderLevel;
public Int16? ReorderLevel
{
    get { return _ReorderLevel; }
    set {  
    ItemChanged = true;
_ReorderLevel = value;
    RaiseProperChanged();
}
}
private bool _Discontinued;
public bool Discontinued
{
    get { return _Discontinued; }
    set {  
    ItemChanged = true;
_Discontinued = value;
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


