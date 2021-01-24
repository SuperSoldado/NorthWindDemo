/************************************************************************************
* Codetomat version Alpha
* Generated at: 14.12.2020
* This is an auto-generated file. 
************************************************************************************/
using System;

namespace MyApp.Data.Info
{
    public partial class CategoriesInfo
    {
        public CategoriesInfo()
        {
CategoryID = int.MinValue;
        }

        private int _CategoryID;

/// <summary>
/// Represent (table.field) Categories.CategoryID
/// </summary>
public int CategoryID
{
get { return _CategoryID; }
set { _CategoryID = value; }
}
private string _CategoryName;

/// <summary>
/// Represent (table.field) Categories.CategoryName
/// </summary>
public string CategoryName
{
get { return _CategoryName; }
set { _CategoryName = value; }
}
private string _Description;

/// <summary>
/// Represent (table.field) Categories.Description
/// </summary>
public string Description
{
get { return _Description; }
set { _Description = value; }
}
private byte[] _Picture;

/// <summary>
/// Represent (table.field) Categories.Picture
/// </summary>
public byte[] Picture
{
get { return _Picture; }
set { _Picture = value; }
}
    }
}
