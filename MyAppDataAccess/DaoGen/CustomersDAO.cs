/************************************************************************************
 * Codetomat version Alpha
 * Generated at: 14.12.2020 21:01:46
 * This is an auto-generated file. 
************************************************************************************/
using System.Collections.Generic;
using System.Data.Common;
using MyAppDataAccessLib;
using System;
using System.Text;
using DataAccessLib.Core;
using MyApp.Data.Info;
using MyApp.Data.Info;

namespace MyApp.Data.DAO
{
	public partial class CustomersDAO : BaseDataAccessObject
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="motor">Database context class. Performs the database access operations.</param>
        public CustomersDAO(Motor motor)
        {
            this.motor = motor;
            GenericDAO d = new GenericDAO(motor);
        }
        /// <summary>
        /// Main class used to centralize data access.
        /// </summary>
        protected Motor motor = null;

        protected GenericDAO genericDAO = null;

        /// <summary>
        /// Get the insert DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetInsertCommand()
        {
            string aux = "insert into Customers (CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax) values (@CustomerID,@CompanyName,@ContactName,@ContactTitle,@Address,@City,@Region,@PostalCode,@Country,@Phone,@Fax)";            
            if (GetIdentity)
                aux += ";SELECT SCOPE_IDENTITY()";
            return aux;
        }

        /// <summary>
        /// Get the select DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetSelectCommand()
        {
            return "select Customers.CustomerID,Customers.CompanyName,Customers.ContactName,Customers.ContactTitle,Customers.Address,Customers.City,Customers.Region,Customers.PostalCode,Customers.Country,Customers.Phone,Customers.Fax from Customers";            
        }

        /// <summary>
        /// Get the update DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetUpdateCommand()
        {
            return "update Customers set CompanyName=@CompanyName,ContactName=@ContactName,ContactTitle=@ContactTitle,Address=@Address,City=@City,Region=@Region,PostalCode=@PostalCode,Country=@Country,Phone=@Phone,Fax=@Fax where CustomerID=@CustomerID";
        }

        /// <summary>
        /// Get the delete DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetDeleteCommand()
        {
             return "delete from Customers";
        }

        /// <summary>
        /// Get the list of Primary Key fields.
        /// </summary>
        /// <returns>Primary key field list.</returns>
        protected virtual List<KeyValuePair<string, string>> GetPrimaryKey()
        {
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
            lst.Add(new KeyValuePair<string, string>( "CustomerID","string"));
            return lst;
        }

        /// <summary>
        /// Get "where" part used to filter
        /// </summary>
        /// <returns>PK filter</returns>
        protected virtual string GetWherePrimaryKey()
        {
            List<KeyValuePair<string, string>> lst = GetPrimaryKey();
            string where = "";            
            foreach (KeyValuePair<string, string> pk in lst)
            {
                bool isNumeric = pk.Value == "isNumeric";
                string dbParameter = pk.Key;
                if (where == "")
                {
                    where = string.Format(" Customers.{0}=@param_{0}", dbParameter);
                }
                else
                {
                    where += string.Format(" and Customers.{0}=@param_{0}", dbParameter);
                }
            }
            where = " where " + where;
            return where;
        }

        protected bool GetIdentity = false;
    
        /// <summary>
        /// Get one register using only ID as key.
        /// </summary>
        /// <returns></returns>
        public virtual CustomersInfo GetValueByID(string CustomerID)
        {
            //ToDo: set multiple PK filter
            motor.ClearCommandParameters();
            motor.CommandText = GetSelectCommand() + GetWherePrimaryKey();            
            List<DbParameter> paramList = new List<DbParameter>();
            

DbParameter paramCustomerID = motor.Command.CreateParameter();
paramCustomerID.ParameterName = "@param_CustomerID";
paramCustomerID.Value = CustomerID;
paramList.Add(paramCustomerID);

    
            motor.AddCommandParameters(paramList);
            CustomersInfo InfoValue = new CustomersInfo();
            
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(CustomersInfo), dbReader);
            using (dbReader)
            {
                if (dbReader.Read())
                {
                    InfoValue = new CustomersInfo();
                    classFiller.Fill(InfoValue);
                }
                else
                {
                    return null;
                }
            }
            return InfoValue;
        }

        /// <summary>
        /// Use parameters to apply simple filters
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public virtual List<CustomersInfo> GetAll(List<DataFilterExpressionDB> filterExpression)
        {
            List<CustomersInfo> AllInfoList = new List<CustomersInfo>();
            motor.ClearCommandParameters();
            motor.CommandText = GetSelectCommand() + " where 1=1 ";
            List<DbParameter> paramList = new List<DbParameter>();
            string where = "";
            foreach (DataFilterExpressionDB filter in filterExpression)
            {
                DbParameter param = motor.Command.CreateParameter();
                param.ParameterName = "@param_" + filter.FieldName;
                param.Value = filter.Filter;
                param.DbType = HelperDBType.GetDBType(typeof(CustomersInfo), filter.FieldName);                
                if (filter.FilterType == DataFilterExpressionDB._FilterType.Equal)
                {
                    param.Value = filter.Filter;
                    where += string.Format(" and Customers.{0} = {1}", filter.FieldName, param.ParameterName);
                }
                else
                {
                    param.Value = "%" + filter.Filter + "%";
                    where += string.Format(" and Customers.{0} like {1}", filter.FieldName, param.ParameterName);
                }
                paramList.Add(param);
            }
            motor.CommandText += where;
            motor.AddCommandParameters(paramList);

            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(CustomersInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    CustomersInfo classInfo = new CustomersInfo();
                    classFiller.Fill(classInfo);
                    AllInfoList.Add(classInfo);
                }
            }
            return AllInfoList;
        }

        /// <summary>
        /// Performs one "Select * from MyTable". Use wisely.
        /// </summary>
        /// <returns>List of found records.</returns>
        public virtual List<CustomersInfo> GetAll()
        {
            List<CustomersInfo> AllInfoList = new List<CustomersInfo>();
            motor.CommandText = GetSelectCommand();
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(CustomersInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    CustomersInfo classInfo = new CustomersInfo();
                    classFiller.Fill(classInfo);
                    AllInfoList.Add(classInfo);
                }
            }
            return AllInfoList;
        }

        /// <summary>
        /// Same as get all but filter the result set
        /// </summary>
        /// <param name="numberOfRowsToSkip">Skip first X rows</param>
        /// <param name="numberOfRows">Like "TOP" in sql server</param>
        /// <returns></returns>
        public List<CustomersInfo> GetAll(int numberOfRowsToSkip, int numberOfRows)
        {
            List<CustomersInfo> AllInfoList = new List<CustomersInfo>();
            motor.CommandText = base.GetFilteredRowNumAndSkipQuery("AttributeLists", "id", numberOfRowsToSkip, numberOfRows);
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(CustomersInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    CustomersInfo classInfo = new CustomersInfo();
                    classFiller.Fill(classInfo);
                    AllInfoList.Add(classInfo);
                }
            }
            return AllInfoList;
        }

        /// <summary>
        /// Insert one register in database.
        /// </summary>
        /// <param name="parCustomersInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void InsertOne(CustomersInfo parCustomersInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                motor.CommandText = GetInsertCommand();
                ///Warning: performance issues with this automation. See method description for details.
                List<DbParameter> paramList = ParameterBuilder.GetParametersForInsert(typeof(CustomersInfo), parCustomersInfo, motor.Command);
                motor.ClearCommandParameters();
                motor.AddCommandParameters(paramList);
                motor.AddTransaction(transaction);

                motor.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        /// <summary>
        /// Delete registers based on class values informed. MinValues and nulls are skipped.
        /// </summary>
        /// <param name="parCustomersInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void Delete(CustomersInfo parCustomersInfo,DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                string whereClausule = string.Empty;
                var pks = GetPrimaryKey();
                List<string> primaryKeys = new List<string>();
                foreach (var item in pks)
                {
                    primaryKeys.Add(item.Key);
                }

                List<DbParameter> paramList = ParameterBuilder.GetParametersForDelete(primaryKeys, typeof(CustomersInfo), parCustomersInfo, motor.Command, out whereClausule);

                motor.CommandText = GetDeleteCommand() + " " + whereClausule;
                motor.ClearCommandParameters();
                motor.AddCommandParameters(paramList);
                motor.AddTransaction(transaction);
                motor.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        /// <summary>
        /// Performs a "update [FildList] set [FieldList] where id = @id". Must have the ID informed.
        /// </summary>
        /// <param name="parCustomersInfo">Item to update</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void UpdateOne(CustomersInfo parCustomersInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                motor.CommandText = GetUpdateCommand();
                ///Warning: performance issues with this automation. See method description for details.
                List<DbParameter> paramList = ParameterBuilder.GetParametersForUpdate(typeof(CustomersInfo), parCustomersInfo, motor.Command);
                motor.ClearCommandParameters();
                motor.AddCommandParameters(paramList);
                motor.AddTransaction(transaction);
                motor.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        /// <summary>
        /// Performs one "select * from MyTable where [InformedProperties]". MinValues and nulls are skipped from filter.
        /// </summary>
        /// <param name="filter">CustomersInfo</param>
        /// <returns>List of found records.</returns>
        public virtual List<CustomersInfo> GetSome(CustomersInfo filter)
        {
            List<CustomersInfo> AllInfoList = new List<CustomersInfo>();
            string filterWhere = string.Empty;
            List<DbParameter> paramList = null;
            GenerateWhere(filter, out filterWhere, out paramList);
            motor.ClearCommandParameters();
            motor.AddCommandParameters(paramList);
            motor.CommandText = GetSelectCommand() + " " + filterWhere;
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(CustomersInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    CustomersInfo CustomersInfo = new CustomersInfo();
                    ///Warning: performance issues with this automation. See method description for details.
                    classFiller.Fill(CustomersInfo);
                    AllInfoList.Add(CustomersInfo);
                }
            }
            return AllInfoList;
        }

        /// <summary>
        /// Generate the "where" clausule, used for select data using "GetSome" method.
        /// </summary>
        /// <param name="filter">Class used to apply the filter</param>
        /// <param name="whereClausule">Result whith a string that add filter to the select comand</param>
        /// <param name="paramList">Result whith the parameters list</param>
        protected void GenerateWhere(CustomersInfo filter, out string whereClausule, out List<DbParameter> paramList)
        {
            StringBuilder where = new StringBuilder();
            paramList = new List<DbParameter>();
            where.Append("where 1=1");
            
// 1) Adding filter for field CustomerID
if (filter.CustomerID != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_CustomerID";
param.Value = filter.CustomerID;
paramList.Add(param);
where.Append(" and Customers.CustomerID=@param_CustomerID");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_CustomerID";
//param.Value = "%" + filter.CustomerID "%";
//paramList.Add(param);
//where.Append(" and Customers.CustomerID like @param_CustomerID");
}
// 2) Adding filter for field CompanyName
if (filter.CompanyName != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_CompanyName";
param.Value = filter.CompanyName;
paramList.Add(param);
where.Append(" and Customers.CompanyName=@param_CompanyName");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_CompanyName";
//param.Value = "%" + filter.CompanyName "%";
//paramList.Add(param);
//where.Append(" and Customers.CompanyName like @param_CompanyName");
}
// 3) Adding filter for field ContactName
if (filter.ContactName != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_ContactName";
param.Value = filter.ContactName;
paramList.Add(param);
where.Append(" and Customers.ContactName=@param_ContactName");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_ContactName";
//param.Value = "%" + filter.ContactName "%";
//paramList.Add(param);
//where.Append(" and Customers.ContactName like @param_ContactName");
}
// 4) Adding filter for field ContactTitle
if (filter.ContactTitle != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_ContactTitle";
param.Value = filter.ContactTitle;
paramList.Add(param);
where.Append(" and Customers.ContactTitle=@param_ContactTitle");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_ContactTitle";
//param.Value = "%" + filter.ContactTitle "%";
//paramList.Add(param);
//where.Append(" and Customers.ContactTitle like @param_ContactTitle");
}
// 5) Adding filter for field Address
if (filter.Address != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_Address";
param.Value = filter.Address;
paramList.Add(param);
where.Append(" and Customers.Address=@param_Address");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_Address";
//param.Value = "%" + filter.Address "%";
//paramList.Add(param);
//where.Append(" and Customers.Address like @param_Address");
}
// 6) Adding filter for field City
if (filter.City != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_City";
param.Value = filter.City;
paramList.Add(param);
where.Append(" and Customers.City=@param_City");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_City";
//param.Value = "%" + filter.City "%";
//paramList.Add(param);
//where.Append(" and Customers.City like @param_City");
}
// 7) Adding filter for field Region
if (filter.Region != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_Region";
param.Value = filter.Region;
paramList.Add(param);
where.Append(" and Customers.Region=@param_Region");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_Region";
//param.Value = "%" + filter.Region "%";
//paramList.Add(param);
//where.Append(" and Customers.Region like @param_Region");
}
// 8) Adding filter for field PostalCode
if (filter.PostalCode != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_PostalCode";
param.Value = filter.PostalCode;
paramList.Add(param);
where.Append(" and Customers.PostalCode=@param_PostalCode");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_PostalCode";
//param.Value = "%" + filter.PostalCode "%";
//paramList.Add(param);
//where.Append(" and Customers.PostalCode like @param_PostalCode");
}
// 9) Adding filter for field Country
if (filter.Country != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_Country";
param.Value = filter.Country;
paramList.Add(param);
where.Append(" and Customers.Country=@param_Country");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_Country";
//param.Value = "%" + filter.Country "%";
//paramList.Add(param);
//where.Append(" and Customers.Country like @param_Country");
}
// 10) Adding filter for field Phone
if (filter.Phone != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_Phone";
param.Value = filter.Phone;
paramList.Add(param);
where.Append(" and Customers.Phone=@param_Phone");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_Phone";
//param.Value = "%" + filter.Phone "%";
//paramList.Add(param);
//where.Append(" and Customers.Phone like @param_Phone");
}
// 11) Adding filter for field Fax
if (filter.Fax != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_Fax";
param.Value = filter.Fax;
paramList.Add(param);
where.Append(" and Customers.Fax=@param_Fax");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_Fax";
//param.Value = "%" + filter.Fax "%";
//paramList.Add(param);
//where.Append(" and Customers.Fax like @param_Fax");
}
            
            whereClausule = where.ToString();
        }
    }
}
