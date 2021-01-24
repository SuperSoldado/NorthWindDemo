//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace MyApp.WPFList.OrderDetails
{
    public partial class ModelNotifiedForOrderDetails: INotifyPropertyChanged
    {

public ModelNotifiedForOrderDetails()
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
private decimal _UnitPrice;
public decimal UnitPrice
{
    get { return _UnitPrice; }
    set {  
    ItemChanged = true;
_UnitPrice = value;
    RaiseProperChanged();
}
}
private Int16 _Quantity;
public Int16 Quantity
{
    get { return _Quantity; }
    set {  
    ItemChanged = true;
_Quantity = value;
    RaiseProperChanged();
}
}
private decimal _Discount;
public decimal Discount
{
    get { return _Discount; }
    set {  
    ItemChanged = true;
_Discount = value;
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


