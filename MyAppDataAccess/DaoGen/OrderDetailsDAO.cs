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
	public partial class OrderDetailsDAO : BaseDataAccessObject
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="motor">Database context class. Performs the database access operations.</param>
        public OrderDetailsDAO(Motor motor)
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
            string aux = "insert into OrderDetails (OrderID,ProductID,UnitPrice,Quantity,Discount) values (@OrderID,@ProductID,@UnitPrice,@Quantity,@Discount)";            
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
            return "select OrderDetails.OrderID,FK0_Orders.ShipName as FK0_ShipName,OrderDetails.ProductID,FK1_Products.ProductName as FK1_ProductName,OrderDetails.UnitPrice,OrderDetails.Quantity,OrderDetails.Discount from OrderDetails left join Orders FK0_Orders on(FK0_Orders.OrderID=OrderDetails.OrderID) left join Products FK1_Products on(FK1_Products.ProductID=OrderDetails.ProductID)";            
        }

        /// <summary>
        /// Get the update DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetUpdateCommand()
        {
            return "update OrderDetails set UnitPrice=@UnitPrice,Quantity=@Quantity,Discount=@Discount where OrderID=@OrderID and ProductID=@ProductID";
        }

        /// <summary>
        /// Get the delete DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetDeleteCommand()
        {
             return "delete from OrderDetails";
        }

        /// <summary>
        /// Get the list of Primary Key fields.
        /// </summary>
        /// <returns>Primary key field list.</returns>
        protected virtual List<KeyValuePair<string, string>> GetPrimaryKey()
        {
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
            lst.Add(new KeyValuePair<string, string>( "OrderID","int"));lst.Add(new KeyValuePair<string, string>( "ProductID","int"));
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
                    where = string.Format(" OrderDetails.{0}=@param_{0}", dbParameter);
                }
                else
                {
                    where += string.Format(" and OrderDetails.{0}=@param_{0}", dbParameter);
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
        public virtual OrderDetailsInfo GetValueByID(int OrderID,int ProductID)
        {
            //ToDo: set multiple PK filter
            motor.ClearCommandParameters();
            motor.CommandText = GetSelectCommand() + GetWherePrimaryKey();            
            List<DbParameter> paramList = new List<DbParameter>();
            

DbParameter paramOrderID = motor.Command.CreateParameter();
paramOrderID.ParameterName = "@param_OrderID";
paramOrderID.Value = OrderID;
paramList.Add(paramOrderID);


DbParameter paramProductID = motor.Command.CreateParameter();
paramProductID.ParameterName = "@param_ProductID";
paramProductID.Value = ProductID;
paramList.Add(paramProductID);

    
            motor.AddCommandParameters(paramList);
            OrderDetailsInfo InfoValue = new OrderDetailsInfo();
            
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(OrderDetailsInfo), dbReader);
            using (dbReader)
            {
                if (dbReader.Read())
                {
                    InfoValue = new OrderDetailsInfo();
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
        public virtual List<OrderDetailsInfo> GetAll(List<DataFilterExpressionDB> filterExpression)
        {
            List<OrderDetailsInfo> AllInfoList = new List<OrderDetailsInfo>();
            motor.ClearCommandParameters();
            motor.CommandText = GetSelectCommand() + " where 1=1 ";
            List<DbParameter> paramList = new List<DbParameter>();
            string where = "";
            foreach (DataFilterExpressionDB filter in filterExpression)
            {
                DbParameter param = motor.Command.CreateParameter();
                param.ParameterName = "@param_" + filter.FieldName;
                param.Value = filter.Filter;
                param.DbType = HelperDBType.GetDBType(typeof(OrderDetailsInfo), filter.FieldName);                
                if (filter.FilterType == DataFilterExpressionDB._FilterType.Equal)
                {
                    param.Value = filter.Filter;
                    where += string.Format(" and OrderDetails.{0} = {1}", filter.FieldName, param.ParameterName);
                }
                else
                {
                    param.Value = "%" + filter.Filter + "%";
                    where += string.Format(" and OrderDetails.{0} like {1}", filter.FieldName, param.ParameterName);
                }
                paramList.Add(param);
            }
            motor.CommandText += where;
            motor.AddCommandParameters(paramList);

            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(OrderDetailsInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    OrderDetailsInfo classInfo = new OrderDetailsInfo();
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
        public virtual List<OrderDetailsInfo> GetAll()
        {
            List<OrderDetailsInfo> AllInfoList = new List<OrderDetailsInfo>();
            motor.CommandText = GetSelectCommand();
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(OrderDetailsInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    OrderDetailsInfo classInfo = new OrderDetailsInfo();
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
        public List<OrderDetailsInfo> GetAll(int numberOfRowsToSkip, int numberOfRows)
        {
            List<OrderDetailsInfo> AllInfoList = new List<OrderDetailsInfo>();
            motor.CommandText = base.GetFilteredRowNumAndSkipQuery("AttributeLists", "id", numberOfRowsToSkip, numberOfRows);
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(OrderDetailsInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    OrderDetailsInfo classInfo = new OrderDetailsInfo();
                    classFiller.Fill(classInfo);
                    AllInfoList.Add(classInfo);
                }
            }
            return AllInfoList;
        }

        /// <summary>
        /// Insert one register in database.
        /// </summary>
        /// <param name="parOrderDetailsInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void InsertOne(OrderDetailsInfo parOrderDetailsInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                motor.CommandText = GetInsertCommand();
                ///Warning: performance issues with this automation. See method description for details.
                List<DbParameter> paramList = ParameterBuilder.GetParametersForInsert(typeof(OrderDetailsInfo), parOrderDetailsInfo, motor.Command);
                motor.ClearCommandParameters();
                motor.AddCommandParameters(paramList);
                motor.AddTransaction(transaction);

                
                if (GetIdentity == true)
                {
                    parOrderDetailsInfo.ProductID = motor.ExecuteScalar();    
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
        /// <param name="parOrderDetailsInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void Delete(OrderDetailsInfo parOrderDetailsInfo,DbTransaction transaction, out string errorMessage)
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

                List<DbParameter> paramList = ParameterBuilder.GetParametersForDelete(primaryKeys, typeof(OrderDetailsInfo), parOrderDetailsInfo, motor.Command, out whereClausule);

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
        /// <param name="parOrderDetailsInfo">Item to update</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void UpdateOne(OrderDetailsInfo parOrderDetailsInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                motor.CommandText = GetUpdateCommand();
                ///Warning: performance issues with this automation. See method description for details.
                List<DbParameter> paramList = ParameterBuilder.GetParametersForUpdate(typeof(OrderDetailsInfo), parOrderDetailsInfo, motor.Command);
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
        /// <param name="filter">OrderDetailsInfo</param>
        /// <returns>List of found records.</returns>
        public virtual List<OrderDetailsInfo> GetSome(OrderDetailsInfo filter)
        {
            List<OrderDetailsInfo> AllInfoList = new List<OrderDetailsInfo>();
            string filterWhere = string.Empty;
            List<DbParameter> paramList = null;
            GenerateWhere(filter, out filterWhere, out paramList);
            motor.ClearCommandParameters();
            motor.AddCommandParameters(paramList);
            motor.CommandText = GetSelectCommand() + " " + filterWhere;
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(OrderDetailsInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    OrderDetailsInfo OrderDetailsInfo = new OrderDetailsInfo();
                    ///Warning: performance issues with this automation. See method description for details.
                    classFiller.Fill(OrderDetailsInfo);
                    AllInfoList.Add(OrderDetailsInfo);
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
        protected void GenerateWhere(OrderDetailsInfo filter, out string whereClausule, out List<DbParameter> paramList)
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
where.Append(" and OrderDetails.OrderID=@param_OrderID");
}

// 2) Adding filter for field ShipName
if (filter.FK0_ShipName != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_FK0_ShipName";
param.Value = filter.FK0_ShipName;
paramList.Add(param);
where.Append(" and FK0_Orders.ShipName=@param_FK0_ShipName");
}
// 3) Adding filter for field ProductID
if (filter.ProductID != Int32.MinValue)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_ProductID";
param.Value = filter.ProductID;
paramList.Add(param);
where.Append(" and OrderDetails.ProductID=@param_ProductID");
}

// 4) Adding filter for field ProductName
if (filter.FK1_ProductName != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_FK1_ProductName";
param.Value = filter.FK1_ProductName;
paramList.Add(param);
where.Append(" and FK1_Products.ProductName=@param_FK1_ProductName");
}
// 5) Adding filter for field UnitPrice
if (filter.UnitPrice != decimal.MinValue)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_UnitPrice";
param.Value = filter.UnitPrice;
paramList.Add(param);
where.Append(" and OrderDetails.UnitPrice=@param_UnitPrice");
}
// 6) Adding filter for field Quantity
if (filter.Quantity != Int16.MinValue)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_Quantity";
param.Value = filter.Quantity;
paramList.Add(param);
where.Append(" and OrderDetails.Quantity=@param_Quantity");
}
// 7) Adding filter for field Discount
if (filter.Discount != decimal.MinValue)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_Discount";
param.Value = filter.Discount;
paramList.Add(param);
where.Append(" and OrderDetails.Discount=@param_Discount");
}
            
            whereClausule = where.ToString();
        }
    }
}
