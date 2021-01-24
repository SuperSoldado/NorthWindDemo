//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyApp.TransferObjects.REST
{
    public class GetCustomerDemographicsView
    {

private string _CustomerTypeID;
public string CustomerTypeID
{
    get { return _CustomerTypeID; }
    set {  
_CustomerTypeID = value;
}
}
private string _CustomerDesc;
public string CustomerDesc
{
    get { return _CustomerDesc; }
    set {  
_CustomerDesc = value;
}
}
    }
}
