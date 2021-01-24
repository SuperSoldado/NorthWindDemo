//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyApp.TransferObjects.REST
{
    public class GetRegionView
    {

private int _RegionID;
public int RegionID
{
    get { return _RegionID; }
    set {  
_RegionID = value;
}
}
private string _RegionDescription;
public string RegionDescription
{
    get { return _RegionDescription; }
    set {  
_RegionDescription = value;
}
}
    }
}
