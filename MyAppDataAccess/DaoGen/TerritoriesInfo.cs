/************************************************************************************
* Codetomat version Alpha
* Generated at: 14.12.2020
* This is an auto-generated file. 
************************************************************************************/
using System;

namespace MyApp.Data.Info
{
    public partial class TerritoriesInfo
    {
        public TerritoriesInfo()
        {
RegionID = int.MinValue;
        }

        private string _TerritoryID;

/// <summary>
/// Represent (table.field) Territories.TerritoryID
/// </summary>
public string TerritoryID
{
get { return _TerritoryID; }
set { _TerritoryID = value; }
}
private string _TerritoryDescription;

/// <summary>
/// Represent (table.field) Territories.TerritoryDescription
/// </summary>
public string TerritoryDescription
{
get { return _TerritoryDescription; }
set { _TerritoryDescription = value; }
}
private string _FK0_RegionDescription;

/// <summary>
/// Territories.RegionID --> Region.RegionDescription
/// </summary>
public string FK0_RegionDescription
{
get { return _FK0_RegionDescription; }
set { _FK0_RegionDescription = value; }
}
private int _RegionID;

/// <summary>
/// Foreing Key Description : Region.FK0_RegionDescription
/// </summary>
public int RegionID
{
get { return _RegionID; }
set { _RegionID = value; }
}
    }
}
