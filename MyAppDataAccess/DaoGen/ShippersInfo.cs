/************************************************************************************
* Codetomat version Alpha
* Generated at: 14.12.2020
* This is an auto-generated file. 
************************************************************************************/
using System;

namespace MyApp.Data.Info
{
    public partial class ShippersInfo
    {
        public ShippersInfo()
        {
ShipperID = int.MinValue;
        }

        private int _ShipperID;

/// <summary>
/// Represent (table.field) Shippers.ShipperID
/// </summary>
public int ShipperID
{
get { return _ShipperID; }
set { _ShipperID = value; }
}
private string _CompanyName;

/// <summary>
/// Represent (table.field) Shippers.CompanyName
/// </summary>
public string CompanyName
{
get { return _CompanyName; }
set { _CompanyName = value; }
}
private string _Phone;

/// <summary>
/// Represent (table.field) Shippers.Phone
/// </summary>
public string Phone
{
get { return _Phone; }
set { _Phone = value; }
}
    }
}
