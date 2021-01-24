//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyApp.TransferObjects.REST
{
    public class GetTagView
    {

private int _TagID;
public int TagID
{
    get { return _TagID; }
    set {  
_TagID = value;
}
}
private string _TextDesc;
public string TextDesc
{
    get { return _TextDesc; }
    set {  
_TextDesc = value;
}
}
private string _TagType;
public string TagType
{
    get { return _TagType; }
    set {  
_TagType = value;
}
}
    }
}
