/************************************************************************************
* Codetomat version Alpha
* Generated at: 14.12.2020
* This is an auto-generated file. 
************************************************************************************/
using System;

namespace MyApp.Data.Info
{
    public partial class RegionInfo
    {
        public RegionInfo()
        {
RegionID = int.MinValue;
        }

        private int _RegionID;

/// <summary>
/// Represent (table.field) Region.RegionID
/// </summary>
public int RegionID
{
get { return _RegionID; }
set { _RegionID = value; }
}
private string _RegionDescription;

/// <summary>
/// Represent (table.field) Region.RegionDescription
/// </summary>
public string RegionDescription
{
get { return _RegionDescription; }
set { _RegionDescription = value; }
}
    }
}
