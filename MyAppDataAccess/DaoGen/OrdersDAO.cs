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
	public partial class OrdersDAO : BaseDataAccessObject
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="motor">Database context class. Performs the database access operations.</param>
        public OrdersDAO(Motor motor)
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
            string aux = "insert into Orders (CustomerID,EmployeeID,OrderDate,RequiredDate,ShippedDate,ShipVia,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry) values (@CustomerID,@EmployeeID,@OrderDate,@RequiredDate,@ShippedDate,@ShipVia,@Freight,@ShipName,@ShipAddress,@ShipCity,@ShipRegion,@ShipPostalCode,@ShipCountry)";            
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
            return "select Orders.OrderID,Orders.CustomerID,FK0_Customers.CompanyName as FK0_CompanyName,Orders.EmployeeID,FK1_Employees.LastName as FK1_LastName,Orders.OrderDate,Orders.RequiredDate,Orders.ShippedDate,Orders.ShipVia,FK2_Shippers.CompanyName as FK2_CompanyName,Orders.Freight,Orders.ShipName,Orders.ShipAddress,Orders.ShipCity,Orders.ShipRegion,Orders.ShipPostalCode,Orders.ShipCountry from Orders left join Customers FK0_Customers on(FK0_Customers.CustomerID=Orders.CustomerID) left join Employees FK1_Employees on(FK1_Employees.EmployeeID=Orders.EmployeeID) left join Shippers FK2_Shippers on(FK2_Shippers.ShipperID=Orders.ShipVia)";            
        }

        /// <summary>
        /// Get the update DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetUpdateCommand()
        {
            return "update Orders set CustomerID=@CustomerID,EmployeeID=@EmployeeID,OrderDate=@OrderDate,RequiredDate=@RequiredDate,ShippedDate=@ShippedDate,ShipVia=@ShipVia,Freight=@Freight,ShipName=@ShipName,ShipAddress=@ShipAddress,ShipCity=@ShipCity,ShipRegion=@ShipRegion,ShipPostalCode=@ShipPostalCode,ShipCountry=@ShipCountry where OrderID=@OrderID";
        }

        /// <summary>
        /// Get the delete DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetDeleteCommand()
        {
             return "delete from Orders";
        }

        /// <summary>
        /// Get the list of Primary Key fields.
        /// </summary>
        /// <returns>Primary key field list.</returns>
        protected virtual List<KeyValuePair<string, string>> GetPrimaryKey()
        {
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
            lst.Add(new KeyValuePair<string, string>( "OrderID","int"));
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
                    where = string.Format(" Orders.{0}=@param_{0}", dbParameter);
                }
                else
                {
                    where += string.Format(" and Orders.{0}=@param_{0}", dbParameter);
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
        public virtual OrdersInfo GetValueByID(int OrderID)
        {
            //ToDo: set multiple PK filter
            motor.ClearCommandParameters();
            motor.CommandText = GetSelectCommand() + GetWherePrimaryKey();            
            List<DbParameter> paramList = new List<DbParameter>();
            

DbParameter paramOrderID = motor.Command.CreateParameter();
paramOrderID.ParameterName = "@param_OrderID";
paramOrderID.Value = OrderID;
paramList.Add(paramOrderID);

    
            motor.AddCommandParameters(paramList);
            OrdersInfo InfoValue = new OrdersInfo();
            
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(OrdersInfo), dbReader);
            using (dbReader)
            {
                if (dbReader.Read())
                {
                    InfoValue = new OrdersInfo();
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
        public virtual List<OrdersInfo> GetAll(List<DataFilterExpressionDB> filterExpression)
        {
            List<OrdersInfo> AllInfoList = new List<OrdersInfo>();
            motor.ClearCommandParameters();
            motor.CommandText = GetSelectCommand() + " where 1=1 ";
            List<DbParameter> paramList = new List<DbParameter>();
            string where = "";
            foreach (DataFilterExpressionDB filter in filterExpression)
            {
                DbParameter param = motor.Command.CreateParameter();
                param.ParameterName = "@param_" + filter.FieldName;
                param.Value = filter.Filter;
                param.DbType = HelperDBType.GetDBType(typeof(OrdersInfo), filter.FieldName);                
                if (filter.FilterType == DataFilterExpressionDB._FilterType.Equal)
                {
                    param.Value = filter.Filter;
                    where += string.Format(" and Orders.{0} = {1}", filter.FieldName, param.ParameterName);
                }
                else
                {
                    param.Value = "%" + filter.Filter + "%";
                    where += string.Format(" and Orders.{0} like {1}", filter.FieldName, param.ParameterName);
                }
                paramList.Add(param);
            }
            motor.CommandText += where;
            motor.AddCommandParameters(paramList);

            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(OrdersInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    OrdersInfo classInfo = new OrdersInfo();
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
        public virtual List<OrdersInfo> GetAll()
        {
            List<OrdersInfo> AllInfoList = new List<OrdersInfo>();
            motor.CommandText = GetSelectCommand();
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(OrdersInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    OrdersInfo classInfo = new OrdersInfo();
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
        public List<OrdersInfo> GetAll(int numberOfRowsToSkip, int numberOfRows)
        {
            List<OrdersInfo> AllInfoList = new List<OrdersInfo>();
            motor.CommandText = base.GetFilteredRowNumAndSkipQuery("AttributeLists", "id", numberOfRowsToSkip, numberOfRows);
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(OrdersInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    OrdersInfo classInfo = new OrdersInfo();
                    classFiller.Fill(classInfo);
                    AllInfoList.Add(classInfo);
                }
            }
            return AllInfoList;
        }

        /// <summary>
        /// Insert one register in database.
        /// </summary>
        /// <param name="parOrdersInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void InsertOne(OrdersInfo parOrdersInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                motor.CommandText = GetInsertCommand();
                ///Warning: performance issues with this automation. See method description for details.
                List<DbParameter> paramList = ParameterBuilder.GetParametersForInsert(typeof(OrdersInfo), parOrdersInfo, motor.Command);
                motor.ClearCommandParameters();
                motor.AddCommandParameters(paramList);
                motor.AddTransaction(transaction);

                
                if (GetIdentity == true)
                {
                    parOrdersInfo.OrderID = motor.ExecuteScalar();    
                }                    
                else
                {
                    motor.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        /// <summary>
        /// Delete registers based on class values informed. MinValues and nulls are skipped.
        /// </summary>
        /// <param name="parOrdersInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void Delete(OrdersInfo parOrdersInfo,DbTransaction transaction, out string errorMessage)
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

                List<DbParameter> paramList = ParameterBuilder.GetParametersForDelete(primaryKeys, typeof(OrdersInfo), parOrdersInfo, motor.Command, out whereClausule);

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
        /// <param name="parOrdersInfo">Item to update</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void UpdateOne(OrdersInfo parOrdersInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                motor.CommandText = GetUpdateCommand();
                ///Warning: performance issues with this automation. See method description for details.
                List<DbParameter> paramList = ParameterBuilder.GetParametersForUpdate(typeof(OrdersInfo), parOrdersInfo, motor.Command);
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
        /// <param name="filter">OrdersInfo</param>
        /// <returns>List of found records.</returns>
        public virtual List<OrdersInfo> GetSome(OrdersInfo filter)
        {
            List<OrdersInfo> AllInfoList = new List<OrdersInfo>();
            string filterWhere = string.Empty;
            List<DbParameter> paramList = null;
            GenerateWhere(filter, out filterWhere, out paramList);
            motor.ClearCommandParameters();
            motor.AddCommandParameters(paramList);
            motor.CommandText = GetSelectCommand() + " " + filterWhere;
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(OrdersInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    OrdersInfo OrdersInfo = new OrdersInfo();
                    ///Warning: performance issues with this automation. See method description for details.
                    classFiller.Fill(OrdersInfo);
                    AllInfoList.Add(OrdersInfo);
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
        protected void GenerateWhere(OrdersInfo filter, out string whereClausule, out List<DbParameter> paramList)
        {
            StringBuilder where = new StringBuilder();
            paramList = new List<DbParameter>();
            where.Append("where 1=1");
            
// 1) Adding filter for field OrderID
if (filter.OrderID != Int32.MinValue)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_OrderID";
param.Value = filter.OrderID;
paramList.Add(param);
where.Append(" and Orders.OrderID=@param_OrderID");
}
// 2) Adding filter for field CustomerID
if (filter.CustomerID != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_CustomerID";
param.Value = filter.CustomerID;
paramList.Add(param);
where.Append(" and Orders.CustomerID=@param_CustomerID");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_CustomerID";
//param.Value = "%" + filter.CustomerID "%";
//paramList.Add(param);
//where.Append(" and Orders.CustomerID like @param_CustomerID");
}

// 3) Adding filter for field CompanyName
if (filter.FK0_CompanyName != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_FK0_CompanyName";
param.Value = filter.FK0_CompanyName;
paramList.Add(param);
where.Append(" and FK0_Customers.CompanyName=@param_FK0_CompanyName");
}
// 4) Adding filter for field EmployeeID
if (filter.EmployeeID != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_EmployeeID";
param.Value = filter.EmployeeID;
paramList.Add(param);
where.Append(" and Orders.EmployeeID=@param_EmployeeID");
}

// 5) Adding filter for field LastName
if (filter.FK1_LastName != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_FK1_LastName";
param.Value = filter.FK1_LastName;
paramList.Add(param);
where.Append(" and FK1_Employees.LastName=@param_FK1_LastName");
}
// 6) Adding filter for field OrderDate
if (filter.OrderDate != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_OrderDate";
param.Value = filter.OrderDate;
paramList.Add(param);
where.Append(" and Orders.OrderDate=@param_OrderDate");
}
// 7) Adding filter for field RequiredDate
if (filter.RequiredDate != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_RequiredDate";
param.Value = filter.RequiredDate;
paramList.Add(param);
where.Append(" and Orders.RequiredDate=@param_RequiredDate");
}
// 8) Adding filter for field ShippedDate
if (filter.ShippedDate != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_ShippedDate";
param.Value = filter.ShippedDate;
paramList.Add(param);
where.Append(" and Orders.ShippedDate=@param_ShippedDate");
}
// 9) Adding filter for field ShipVia
if (filter.ShipVia != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_ShipVia";
param.Value = filter.ShipVia;
paramList.Add(param);
where.Append(" and Orders.ShipVia=@param_ShipVia");
}

// 10) Adding filter for field CompanyName
if (filter.FK2_CompanyName != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_FK2_CompanyName";
param.Value = filter.FK2_CompanyName;
paramList.Add(param);
where.Append(" and FK2_Shippers.CompanyName=@param_FK2_CompanyName");
}
// 11) Adding filter for field Freight
if (filter.Freight != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_Freight";
param.Value = filter.Freight;
paramList.Add(param);
where.Append(" and Orders.Freight=@param_Freight");
}
// 12) Adding filter for field ShipName
if (filter.ShipName != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_ShipName";
param.Value = filter.ShipName;
paramList.Add(param);
where.Append(" and Orders.ShipName=@param_ShipName");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_ShipName";
//param.Value = "%" + filter.ShipName "%";
//paramList.Add(param);
//where.Append(" and Orders.ShipName like @param_ShipName");
}
// 13) Adding filter for field ShipAddress
if (filter.ShipAddress != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_ShipAddress";
param.Value = filter.ShipAddress;
paramList.Add(param);
where.Append(" and Orders.ShipAddress=@param_ShipAddress");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_ShipAddress";
//param.Value = "%" + filter.ShipAddress "%";
//paramList.Add(param);
//where.Append(" and Orders.ShipAddress like @param_ShipAddress");
}
// 14) Adding filter for field ShipCity
if (filter.ShipCity != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_ShipCity";
param.Value = filter.ShipCity;
paramList.Add(param);
where.Append(" and Orders.ShipCity=@param_ShipCity");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_ShipCity";
//param.Value = "%" + filter.ShipCity "%";
//paramList.Add(param);
//where.Append(" and Orders.ShipCity like @param_ShipCity");
}
// 15) Adding filter for field ShipRegion
if (filter.ShipRegion != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_ShipRegion";
param.Value = filter.ShipRegion;
paramList.Add(param);
where.Append(" and Orders.ShipRegion=@param_ShipRegion");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_ShipRegion";
//param.Value = "%" + filter.ShipRegion "%";
//paramList.Add(param);
//where.Append(" and Orders.ShipRegion like @param_ShipRegion");
}
// 16) Adding filter for field ShipPostalCode
if (filter.ShipPostalCode != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_ShipPostalCode";
param.Value = filter.ShipPostalCode;
paramList.Add(param);
where.Append(" and Orders.ShipPostalCode=@param_ShipPostalCode");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_ShipPostalCode";
//param.Value = "%" + filter.ShipPostalCode "%";
//paramList.Add(param);
//where.Append(" and Orders.ShipPostalCode like @param_ShipPostalCode");
}
// 17) Adding filter for field ShipCountry
if (filter.ShipCountry != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_ShipCountry";
param.Value = filter.ShipCountry;
paramList.Add(param);
where.Append(" and Orders.ShipCountry=@param_ShipCountry");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_ShipCountry";
//param.Value = "%" + filter.ShipCountry "%";
//paramList.Add(param);
//where.Append(" and Orders.ShipCountry like @param_ShipCountry");
}
            
            whereClausule = where.ToString();
        }
    }
}
