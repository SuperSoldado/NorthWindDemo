//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyApp.TransferObjects.REST
{
    public class UpdateTagEmployeeView
    {

private int _TagEmployeeID;
public int TagEmployeeID
{
    get { return _TagEmployeeID; }
    set {  
_TagEmployeeID = value;
}
}
private int? _EmployeeIDFK;
public int? EmployeeIDFK
{
    get { return _EmployeeIDFK; }
    set {  
_EmployeeIDFK = value;
}
}
private int? _TagFK;
public int? TagFK
{
    get { return _TagFK; }
    set {  
_TagFK = value;
}
}
private string _TagEmployeeTextDesc;
public string TagEmployeeTextDesc
{
    get { return _TagEmployeeTextDesc; }
    set {  
_TagEmployeeTextDesc = value;
}
}
    }
}
