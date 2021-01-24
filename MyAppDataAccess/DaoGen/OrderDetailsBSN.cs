/************************************************************************************
 * Codetomat version Alpha
 * Generated at: 14.12.2020 21:01:46
 * This is an auto-generated file. 
************************************************************************************/
using MyAppDataAccessLib;
using System.Data.Common;
using System.Collections.Generic;
using System;
using MyAppGlobalLib;
using DataAccessLib.Core;
using MyApp.Data.Info;
using MyApp.Data.DAO;

namespace MyApp.Data.Business
{
	public partial class OrderDetailsBsn
    {
        public IBaseGlobalConfig baseGlobalConfig;

        /// <summary>
        /// Use this contructor for single connection operations.
        /// </summary>
        public OrderDetailsBsn(IBaseGlobalConfig baseGlobalConfig)
        {
            this.baseGlobalConfig = baseGlobalConfig;
            this.motor = new Motor(baseGlobalConfig.GetMainConnectionString);
            GenerateDataAcces();
            this.OrderDetailsDAO = new OrderDetailsDAO(motor);
        }

        /// <summary>
        /// Use this constructor when using multi transactional operations.
        /// </summary>
        /// <param name="closeConnectionWhenFinish">If true, close the connection after any CRUD operation</param>
        /// <param name="motor">Motor class from caller</param>
        public OrderDetailsBsn(bool closeConnectionWhenFinish, Motor motor)
        {
            this.motor = motor;
            this.OrderDetailsDAO = new OrderDetailsDAO(motor);
            this.closeConnectionWhenFinish = closeConnectionWhenFinish;
        }
        /// <summary>
        /// Motor is the main BD class. Encapsulates DbProviderFactory, DbConnection, DbCommand, DbDataReader, DbTransaction
        /// </summary>
        public Motor motor = null;

        /// <summary>
        /// The Data Access Object overrided class.
        /// </summary>
        public OrderDetailsDAO OrderDetailsDAO = null;

        /// <summary>
        /// If true, closes the connection after any DAO operation is done.
        /// </summary>
        protected bool closeConnectionWhenFinish = true;

        /// <summary>
        /// Override this method to get a diferent database provider or connection string.
        /// </summary>
        public virtual void GenerateDataAcces()
        {
            //MyAppGlobalLib.GlobalConfig g = new MyAppGlobalLib.GlobalConfig();
        }

        /// <summary>
        /// Retrieves the data using only the primary key ID. Ex.: "Select * from MyTable where id=1"
        /// </summary>
        /// <returns>The class filled if found.</returns>        
        public virtual OrderDetailsInfo GetValueByID(int OrderID,int ProductID)
        {
            motor.OpenConnection();
            OrderDetailsInfo value = OrderDetailsDAO.GetValueByID(OrderID,ProductID);
            if (this.closeConnectionWhenFinish)
            {
                motor.CloseConnection();
            }
            return value;
        }

        /// <summary>
        /// Performs one "Select * from MyTable where MyColumn=MyFilter".
        /// </summary>
        /// <returns>List of found records.</returns>
        public virtual List<OrderDetailsInfo> GetAll(List<DataFilterExpressionDB> filterExpression)
        {
            motor.OpenConnection();
            List<OrderDetailsInfo> list = OrderDetailsDAO.GetAll(filterExpression);
            if (this.closeConnectionWhenFinish)
            {
                motor.CloseConnection();
            }
            return list;
        }

        /// <summary>
        /// Performs one "Select * from MyTable". Use wisely.
        /// </summary>
        /// <returns>List of found records.</returns>
        public virtual List<OrderDetailsInfo> GetAll()
        {
            motor.OpenConnection();
            List<OrderDetailsInfo> list = OrderDetailsDAO.GetAll();
            if (this.closeConnectionWhenFinish)
            {
                motor.CloseConnection();
            }
            return list;
        }

        /// <summary>
        /// Same as get all but filter the result set
        /// </summary>
        /// <param name="numberOfRowsToSkip">Skip first X rows</param>
        /// <param name="numberOfRows">Like "TOP" in sql server</param>
        /// <returns></returns>
        public virtual List<OrderDetailsInfo> GetAll(int numberOfRowsToSkip, int numberOfRows)
        {
            motor.OpenConnection();
            List<OrderDetailsInfo> list = OrderDetailsDAO.GetAll(numberOfRowsToSkip, numberOfRows);
            if (this.closeConnectionWhenFinish)
            {
                motor.CloseConnection();
            }
            return list;
        }

        /// <summary>
        /// Perform one "select" command to database. Filter the data using "filter" class.
        /// </summary>
        /// <param name="filter">Class to use as filter</param>
        /// <returns>List with filtered data</returns>
        public virtual List<OrderDetailsInfo> GetSome(OrderDetailsInfo filter)
        {
            motor.OpenConnection();
            List<OrderDetailsInfo> list = OrderDetailsDAO.GetSome(filter);
            if (this.closeConnectionWhenFinish)
            {
                motor.CloseConnection();
            }
            return list;
        }

        /// <summary>
        /// Primitive way to solve Primary Keys that are not "Identity" in Database. 
        /// NOTE: Microsoft recomends use "identity" types in primary key and let SQL Server solve the PK
        /// </summary>
        /// <param name="parOrderDetailsInfo"></param>
        public void SolvePrimaryKeyBeforeInsert(OrderDetailsInfo parOrderDetailsInfo)
        { 
            if (parOrderDetailsInfo.ProductID == int.MinValue || parOrderDetailsInfo.ProductID == 0)
            { 
                GenericDAO genericBSN = new GenericDAO(motor);
                long nextID = genericBSN.GetMaxPlusOne("ProductID", "OrderDetails");
                parOrderDetailsInfo.ProductID = (int)nextID;
            }            
        }

        /// <summary>
        /// Performs one "insert into" database command.
        /// Will commit the transaction and close the connection (if configured to). Use for independent insert.
        /// </summary>
        /// <param name="OrderDetailsInfo">Object to insert.</param>
        /// <param name="errorMessage">Error message if exception is throwed</param>
        public virtual void InsertOne(OrderDetailsInfo parOrderDetailsInfo, out string errorMessage)
        {
            errorMessage = string.Empty;        
            
    
            //1) Start the transaction context.
            DbTransaction transaction = motor.BeginTransaction();
            
            SolvePrimaryKeyBeforeInsert(parOrderDetailsInfo);

            //2) Call the overload of this method, which call the DAO but does not commit.
            this.InsertOne(parOrderDetailsInfo, transaction, out errorMessage);
            
            //3) Commit the transaction.
            motor.Commit();
            
            //4) Close the conection (if configured to do so).
            if (this.closeConnectionWhenFinish)
            {
                motor.CloseConnection();
            }
        }

        /// <summary>
        /// Performs one "insert into" database command in a transactional context.
        /// * The method uses a transaction object already created and does not close the connection.
        /// * Must have "MultipleActiveResultSets=True" on connection string.
        /// </summary>
        /// <param name="OrderDetailsInfo">Object to insert.</param>
        /// <param name="transaction">Inform "DBTransaction".</param>
        /// <param name="errorMessage">Error message if exception is throwed.</param>
        public virtual void InsertOne(OrderDetailsInfo parOrderDetailsInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = string.Empty;
            
//If is trying to insert FKValue without the ID but has the unique description,
//the system will try to get the class with the ID and populate it.
if ((parOrderDetailsInfo.OrderID == Int32.MinValue) && (parOrderDetailsInfo.FK0_ShipName != null))
{
OrdersInfo fkClass = Get_OrderIDID_FKOrders(parOrderDetailsInfo, transaction);
parOrderDetailsInfo.OrderID = fkClass.OrderID;
}
if ((parOrderDetailsInfo.ProductID == Int32.MinValue) && (parOrderDetailsInfo.FK1_ProductName != null))
{
ProductsInfo fkClass = Get_ProductIDID_FKProducts(parOrderDetailsInfo, transaction);
parOrderDetailsInfo.ProductID = fkClass.ProductID;
}

            OrderDetailsDAO.InsertOne(parOrderDetailsInfo, transaction, out errorMessage);
            //By default, the caller of this method will do the commit.
            //motor.Commit();
            //motor.CloseConnection();
        }

/// <summary>
/// Perform a search to find the class "OrdersInfo" using as key the field "Orders".
/// </summary>
/// <param name="parOrderDetailsInfo">Main class that contains the aggregation.</param>
/// <returns>Foreing key attched class.</returns>
public virtual OrdersInfo Get_OrderIDID_FKOrders(OrderDetailsInfo parOrderDetailsInfo, DbTransaction transaction)
{
OrdersInfo filter = new OrdersInfo();
filter.ShipName = parOrderDetailsInfo.FK0_ShipName;
OrdersBsn myClass = new OrdersBsn(false, this.motor);
List<OrdersInfo> list = myClass.GetSome(filter);
if (list.Count == 0)
{
//This error occurs when try to search for the ID in one table, but it does not find the value.
//Ex.: Select id,SomeField from myTable where SomeField='myValue') If no data return, this error will trigger.
throw new Exception(string.Format("Can not define ID for parOrderDetailsInfo.", parOrderDetailsInfo.FK0_ShipName));
/* [Hint] The code below do one insert in the table "Orders" informing only the "OrderID" field.
 * [Warning] The code may crash if other fields are necessary.
 * [Instructions] Comment the exception above. Uncomment the code below.
 */
//string errorMsg = string.Empty;
//myClass.InsertOne(filter, transaction, out errorMsg);
//if (errorMsg != string.Empty)
//{
//throw new Exception(errorMsg);
//}
//else
//{
//return filter;
//}
}
if (list.Count > 1)
{
//This error occurs when try to search for the ID in one table, but it return more then one value.
//Ex.: Select id,SomeField from myTable where SomeField='myValue') If more then one line return, this error will trigger.
throw new Exception(string.Format("Can not define ID for parOrderDetailsInfo. Theres more then one ID value for this field. ", parOrderDetailsInfo.FK0_ShipName));
}
else
{
//Return the only one class found.
return list[0];
}
}
/// <summary>
/// Perform a search to find the class "ProductsInfo" using as key the field "Products".
/// </summary>
/// <param name="parOrderDetailsInfo">Main class that contains the aggregation.</param>
/// <returns>Foreing key attched class.</returns>
public virtual ProductsInfo Get_ProductIDID_FKProducts(OrderDetailsInfo parOrderDetailsInfo, DbTransaction transaction)
{
ProductsInfo filter = new ProductsInfo();
filter.ProductName = parOrderDetailsInfo.FK1_ProductName;
ProductsBsn myClass = new ProductsBsn(false, this.motor);
List<ProductsInfo> list = myClass.GetSome(filter);
if (list.Count == 0)
{
//This error occurs when try to search for the ID in one table, but it does not find the value.
//Ex.: Select id,SomeField from myTable where SomeField='myValue') If no data return, this error will trigger.
throw new Exception(string.Format("Can not define ID for parOrderDetailsInfo.", parOrderDetailsInfo.FK1_ProductName));
/* [Hint] The code below do one insert in the table "Products" informing only the "ProductID" field.
 * [Warning] The code may crash if other fields are necessary.
 * [Instructions] Comment the exception above. Uncomment the code below.
 */
//string errorMsg = string.Empty;
//myClass.InsertOne(filter, transaction, out errorMsg);
//if (errorMsg != string.Empty)
//{
//throw new Exception(errorMsg);
//}
//else
//{
//return filter;
//}
}
if (list.Count > 1)
{
//This error occurs when try to search for the ID in one table, but it return more then one value.
//Ex.: Select id,SomeField from myTable where SomeField='myValue') If more then one line return, this error will trigger.
throw new Exception(string.Format("Can not define ID for parOrderDetailsInfo. Theres more then one ID value for this field. ", parOrderDetailsInfo.FK1_ProductName));
}
else
{
//Return the only one class found.
return list[0];
}
}

        /// <summary>
        /// Delete registers based on class values informed. MinValues and nulls are skipped.
        /// </summary>
        /// <param name="parOrderDetailsInfo">Item to delete</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void Delete(OrderDetailsInfo parOrderDetailsInfo, out string errorMessage)
        {
            errorMessage = string.Empty;

            //1) Start the transaction context.
            DbTransaction transaction = motor.BeginTransaction();

            //2) Call the overload of this method, which call the DAO but does not commit.
            this.Delete(parOrderDetailsInfo, transaction, out errorMessage);

            //3) Commit the transaction.
            motor.Commit();

            //4) Close the conection (if configured to do so).
            if (this.closeConnectionWhenFinish)
            {
                motor.CloseConnection();
            }
        }

        /// <summary>
        /// Delete registers based on ID informed. Other values are skipped.
        /// </summary>
        /// <param name="parOrderDetailsInfo">Item to delete</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void DeleteByID(OrderDetailsInfo parOrderDetailsInfo, out string errorMessage)
        {
            OrderDetailsInfo newParam = new OrderDetailsInfo();
            newParam.ProductID = parOrderDetailsInfo.ProductID;
            this.Delete(newParam, out errorMessage);
        }

        /// <summary>
        /// Delete registers based on class values informed. MinValues and nulls are skipped.
        /// Must have "MultipleActiveResultSets=True" on connection string.
        /// </summary>
        /// <param name="parOrderDetailsInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void Delete(OrderDetailsInfo parOrderDetailsInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = string.Empty;
            OrderDetailsDAO.Delete(parOrderDetailsInfo, transaction, out errorMessage);
            //By default, the caller of this method will do the commit.
            //motor.Commit();
            //motor.CloseConnection();
        }

        /// <summary>
        /// Delete registers based on class ID informed in transactional context. Other values are skipped.
        /// Must have "MultipleActiveResultSets=True" on connection string.
        /// </summary>
        /// <param name="parOrderDetailsInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void DeleteByID(OrderDetailsInfo parOrderDetailsInfo, DbTransaction transaction, out string errorMessage)
        {
            OrderDetailsInfo newParam = new OrderDetailsInfo();
            newParam.ProductID = parOrderDetailsInfo.ProductID;
            this.Delete(newParam, transaction, out errorMessage);
            //By default, the caller of this method will do the commit.
            //motor.Commit();
            //motor.CloseConnection();
        }

        /// <summary>
        /// Performs one "update" database command.
        /// Will commit the transaction and close the connection. Use for independent delete.
        /// </summary>
        /// <param name="OrderDetailsInfo">Object to update.</param>
        /// <param name="errorMessage">Error message if exception is throwed</param>
        public virtual void UpdateOne(OrderDetailsInfo parOrderDetailsInfo, out string errorMessage)
        {
            errorMessage = string.Empty;
            DbTransaction transaction = motor.BeginTransaction();

            this.UpdateOne(parOrderDetailsInfo, transaction, out errorMessage);
            motor.Commit();
            motor.CloseConnection();
        }

        /// <summary>
        /// Performs one "update" database command in a transactional context.
        /// * The method uses a transaction object already created and does not close the connection.
        /// * Must have "MultipleActiveResultSets=True" on connection string.
        /// </summary>
        /// <param name="OrderDetailsInfo">Object to update.</param>
        /// <param name="transaction">Inform "DBTransaction".</param>
        /// <param name="errorMessage">Error message if exception is throwed.</param>
        public virtual void UpdateOne(OrderDetailsInfo parOrderDetailsInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = string.Empty;
    
//If is trying to insert FKValue without the ID but has the unique description,
//the system will try to get the class with the ID and populate it.
if ((parOrderDetailsInfo.OrderID == Int32.MinValue) && (parOrderDetailsInfo.FK0_ShipName != null))
{
OrdersInfo fkClass = Get_OrderIDID_FKOrders(parOrderDetailsInfo, transaction);
parOrderDetailsInfo.OrderID = fkClass.OrderID;
}
if ((parOrderDetailsInfo.ProductID == Int32.MinValue) && (parOrderDetailsInfo.FK1_ProductName != null))
{
ProductsInfo fkClass = Get_ProductIDID_FKProducts(parOrderDetailsInfo, transaction);
parOrderDetailsInfo.ProductID = fkClass.ProductID;
}

            OrderDetailsDAO.UpdateOne(parOrderDetailsInfo, transaction, out errorMessage);
            //By default, the caller of this method will do the commit.
            //motor.Commit();
            //motor.CloseConnection();
        }
    }
}
