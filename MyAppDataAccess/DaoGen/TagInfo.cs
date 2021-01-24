/************************************************************************************
* Codetomat version Alpha
* Generated at: 14.12.2020
* This is an auto-generated file. 
************************************************************************************/
using System;

namespace MyApp.Data.Info
{
    public partial class TagInfo
    {
        public TagInfo()
        {
TagID = int.MinValue;
        }

        private int _TagID;

/// <summary>
/// Represent (table.field) Tag.TagID
/// </summary>
public int TagID
{
get { return _TagID; }
set { _TagID = value; }
}
private string _TextDesc;

/// <summary>
/// Represent (table.field) Tag.TextDesc
/// </summary>
public string TextDesc
{
get { return _TextDesc; }
set { _TextDesc = value; }
}
private string _TagType;

/// <summary>
/// Represent (table.field) Tag.TagType
/// </summary>
public string TagType
{
get { return _TagType; }
set { _TagType = value; }
}
    }
}
