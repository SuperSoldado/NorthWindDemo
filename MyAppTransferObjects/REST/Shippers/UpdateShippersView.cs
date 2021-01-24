//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyApp.TransferObjects.REST
{
    public class UpdateShippersView
    {

private int _ShipperID;
public int ShipperID
{
    get { return _ShipperID; }
    set {  
_ShipperID = value;
}
}
private string _CompanyName;
public string CompanyName
{
    get { return _CompanyName; }
    set {  
_CompanyName = value;
}
}
private string _Phone;
public string Phone
{
    get { return _Phone; }
    set {  
_Phone = value;
}
}
    }
}
