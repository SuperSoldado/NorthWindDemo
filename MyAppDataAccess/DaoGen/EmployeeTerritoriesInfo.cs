/************************************************************************************
* Codetomat version Alpha
* Generated at: 14.12.2020
* This is an auto-generated file. 
************************************************************************************/
using System;

namespace MyApp.Data.Info
{
    public partial class EmployeeTerritoriesInfo
    {
        public EmployeeTerritoriesInfo()
        {
EmployeeID = int.MinValue;
        }

        private string _FK0_LastName;

/// <summary>
/// EmployeeTerritories.EmployeeID --> Employees.LastName
/// </summary>
public string FK0_LastName
{
get { return _FK0_LastName; }
set { _FK0_LastName = value; }
}
private int _EmployeeID;

/// <summary>
/// Foreing Key Description : Employees.FK0_LastName
/// </summary>
public int EmployeeID
{
get { return _EmployeeID; }
set { _EmployeeID = value; }
}
private string _FK1_TerritoryDescription;

/// <summary>
/// EmployeeTerritories.TerritoryID --> Territories.TerritoryDescription
/// </summary>
public string FK1_TerritoryDescription
{
get { return _FK1_TerritoryDescription; }
set { _FK1_TerritoryDescription = value; }
}
private string _TerritoryID;

/// <summary>
/// Foreing Key Description : Territories.FK1_TerritoryDescription
/// </summary>
public string TerritoryID
{
get { return _TerritoryID; }
set { _TerritoryID = value; }
}
    }
}
