//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyApp.TransferObjects.REST
{
    public class CreateTerritoriesView
    {

private string _TerritoryID;
public string TerritoryID
{
    get { return _TerritoryID; }
    set {  
_TerritoryID = value;
}
}
private string _TerritoryDescription;
public string TerritoryDescription
{
    get { return _TerritoryDescription; }
    set {  
_TerritoryDescription = value;
}
}
private int _RegionID;
public int RegionID
{
    get { return _RegionID; }
    set {  
_RegionID = value;
}
}
    }
}
