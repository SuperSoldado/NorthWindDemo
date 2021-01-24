//Track[0006] WPF_Shared_Notified.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyApp.TransferObjects.REST
{
    public class GetCategoriesView
    {

private int _CategoryID;
public int CategoryID
{
    get { return _CategoryID; }
    set {  
_CategoryID = value;
}
}
private string _CategoryName;
public string CategoryName
{
    get { return _CategoryName; }
    set {  
_CategoryName = value;
}
}
private string _Description;
public string Description
{
    get { return _Description; }
    set {  
_Description = value;
}
}
private byte[] _Picture;
public byte[] Picture
{
    get { return _Picture; }
    set {  
_Picture = value;
}
}
    }
}
