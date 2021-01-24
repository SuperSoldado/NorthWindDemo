/************************************************************************************
* Codetomat version Alpha
* Generated at: 14.12.2020
* This is an auto-generated file. 
************************************************************************************/
using System;

namespace MyApp.Data.Info
{
    public partial class TagEmployeeInfo
    {
        public TagEmployeeInfo()
        {
TagEmployeeID = int.MinValue;
        }

        private int _TagEmployeeID;

/// <summary>
/// Represent (table.field) TagEmployee.TagEmployeeID
/// </summary>
public int TagEmployeeID
{
get { return _TagEmployeeID; }
set { _TagEmployeeID = value; }
}
private string _FK0_LastName;

/// <summary>
/// TagEmployee.EmployeeIDFK --> Employees.LastName
/// </summary>
public string FK0_LastName
{
get { return _FK0_LastName; }
set { _FK0_LastName = value; }
}
private int? _EmployeeIDFK;

/// <summary>
/// Foreing Key Description : Employees.FK0_LastName
/// </summary>
public int? EmployeeIDFK
{
get { return _EmployeeIDFK; }
set { _EmployeeIDFK = value; }
}
private string _FK1_TextDesc;

/// <summary>
/// TagEmployee.TagFK --> Tag.TextDesc
/// </summary>
public string FK1_TextDesc
{
get { return _FK1_TextDesc; }
set { _FK1_TextDesc = value; }
}
private int? _TagFK;

/// <summary>
/// Foreing Key Description : Tag.FK1_TextDesc
/// </summary>
public int? TagFK
{
get { return _TagFK; }
set { _TagFK = value; }
}
private string _TagEmployeeTextDesc;

/// <summary>
/// Represent (table.field) TagEmployee.TagEmployeeTextDesc
/// </summary>
public string TagEmployeeTextDesc
{
get { return _TagEmployeeTextDesc; }
set { _TagEmployeeTextDesc = value; }
}
    }
}
