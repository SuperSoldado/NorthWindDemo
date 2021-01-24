//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyApp.TransferObjects.REST
{
    public class CreateEmployeeTerritoriesView
    {

private int _EmployeeID;
public int EmployeeID
{
    get { return _EmployeeID; }
    set {  
_EmployeeID = value;
}
}
private string _TerritoryID;
public string TerritoryID
{
    get { return _TerritoryID; }
    set {  
_TerritoryID = value;
}
}
    }
}
