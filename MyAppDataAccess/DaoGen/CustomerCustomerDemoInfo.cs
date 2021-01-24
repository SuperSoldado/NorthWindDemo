/************************************************************************************
* Codetomat version Alpha
* Generated at: 14.12.2020
* This is an auto-generated file. 
************************************************************************************/
using System;

namespace MyApp.Data.Info
{
    public partial class CustomerCustomerDemoInfo
    {
        public CustomerCustomerDemoInfo()
        {
        }

        private string _FK0_CompanyName;

/// <summary>
/// CustomerCustomerDemo.CustomerID --> Customers.CompanyName
/// </summary>
public string FK0_CompanyName
{
get { return _FK0_CompanyName; }
set { _FK0_CompanyName = value; }
}
private string _CustomerID;

/// <summary>
/// Foreing Key Description : Customers.FK0_CompanyName
/// </summary>
public string CustomerID
{
get { return _CustomerID; }
set { _CustomerID = value; }
}
private string _FK1_CustomerDesc;

/// <summary>
/// CustomerCustomerDemo.CustomerTypeID --> CustomerDemographics.CustomerDesc
/// </summary>
public string FK1_CustomerDesc
{
get { return _FK1_CustomerDesc; }
set { _FK1_CustomerDesc = value; }
}
private string _CustomerTypeID;

/// <summary>
/// Foreing Key Description : CustomerDemographics.FK1_CustomerDesc
/// </summary>
public string CustomerTypeID
{
get { return _CustomerTypeID; }
set { _CustomerTypeID = value; }
}
    }
}
