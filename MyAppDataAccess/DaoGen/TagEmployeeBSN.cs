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
	public partial class TagEmployeeBsn
    {
        public IBaseGlobalConfig baseGlobalConfig;

        /// <summary>
        /// Use this contructor for single connection operations.
        /// </summary>
        public TagEmployeeBsn(IBaseGlobalConfig baseGlobalConfig)
        {
            this.baseGlobalConfig = baseGlobalConfig;
            this.motor = new Motor(baseGlobalConfig.GetMainConnectionString);
            GenerateDataAcces();
            this.TagEmployeeDAO = new TagEmployeeDAO(motor);
        }

        /// <summary>
        /// Use this constructor when using multi transactional operations.
        /// </summary>
        /// <param name="closeConnectionWhenFinish">If true, close the connection after any CRUD operation</param>
        /// <param name="motor">Motor class from caller</param>
        public TagEmployeeBsn(bool closeConnectionWhenFinish, Motor motor)
        {
            this.motor = motor;
            this.TagEmployeeDAO = new TagEmployeeDAO(motor);
            this.closeConnectionWhenFinish = closeConnectionWhenFinish;
        }
        /// <summary>
        /// Motor is the main BD class. Encapsulates DbProviderFactory, DbConnection, DbCommand, DbDataReader, DbTransaction
        /// </summary>
        public Motor motor = null;

        /// <summary>
        /// The Data Access Object overrided class.
        /// </summary>
        public TagEmployeeDAO TagEmployeeDAO = null;

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
        public virtual TagEmployeeInfo GetValueByID(int TagEmployeeID)
        {
            motor.OpenConnection();
            TagEmployeeInfo value = TagEmployeeDAO.GetValueByID(TagEmployeeID);
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
        public virtual List<TagEmployeeInfo> GetAll(List<DataFilterExpressionDB> filterExpression)
        {
            motor.OpenConnection();
            List<TagEmployeeInfo> list = TagEmployeeDAO.GetAll(filterExpression);
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
        public virtual List<TagEmployeeInfo> GetAll()
        {
            motor.OpenConnection();
            List<TagEmployeeInfo> list = TagEmployeeDAO.GetAll();
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
        public virtual List<TagEmployeeInfo> GetAll(int numberOfRowsToSkip, int numberOfRows)
        {
            motor.OpenConnection();
            List<TagEmployeeInfo> list = TagEmployeeDAO.GetAll(numberOfRowsToSkip, numberOfRows);
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
        public virtual List<TagEmployeeInfo> GetSome(TagEmployeeInfo filter)
        {
            motor.OpenConnection();
            List<TagEmployeeInfo> list = TagEmployeeDAO.GetSome(filter);
            if (this.closeConnectionWhenFinish)
            {
                motor.CloseConnection();
            }
            return list;
        }


        /// <summary>
        /// Performs one "insert into" database command.
        /// Will commit the transaction and close the connection (if configured to). Use for independent insert.
        /// </summary>
        /// <param name="TagEmployeeInfo">Object to insert.</param>
        /// <param name="errorMessage">Error message if exception is throwed</param>
        public virtual void InsertOne(TagEmployeeInfo parTagEmployeeInfo, out string errorMessage)
        {
            errorMessage = string.Empty;        
            
    
            //1) Start the transaction context.
            DbTransaction transaction = motor.BeginTransaction();
            

            //2) Call the overload of this method, which call the DAO but does not commit.
            this.InsertOne(parTagEmployeeInfo, transaction, out errorMessage);
            
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
        /// <param name="TagEmployeeInfo">Object to insert.</param>
        /// <param name="transaction">Inform "DBTransaction".</param>
        /// <param name="errorMessage">Error message if exception is throwed.</param>
        public virtual void InsertOne(TagEmployeeInfo parTagEmployeeInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = string.Empty;
            
//If is trying to insert FKValue without the ID but has the unique description,
//the system will try to get the class with the ID and populate it.
if ((parTagEmployeeInfo.EmployeeIDFK == null) && (parTagEmployeeInfo.FK0_LastName != null))
{
EmployeesInfo fkClass = Get_EmployeeIDFKID_FKEmployees(parTagEmployeeInfo, transaction);
parTagEmployeeInfo.EmployeeIDFK = fkClass.EmployeeID;
}
if ((parTagEmployeeInfo.TagFK == null) && (parTagEmployeeInfo.FK1_TextDesc != null))
{
TagInfo fkClass = Get_TagFKID_FKTag(parTagEmployeeInfo, transaction);
parTagEmployeeInfo.TagFK = fkClass.TagID;
}

            TagEmployeeDAO.InsertOne(parTagEmployeeInfo, transaction, out errorMessage);
            //By default, the caller of this method will do the commit.
            //motor.Commit();
            //motor.CloseConnection();
        }

/// <summary>
/// Perform a search to find the class "EmployeesInfo" using as key the field "Employees".
/// </summary>
/// <param name="parTagEmployeeInfo">Main class that contains the aggregation.</param>
/// <returns>Foreing key attched class.</returns>
public virtual EmployeesInfo Get_EmployeeIDFKID_FKEmployees(TagEmployeeInfo parTagEmployeeInfo, DbTransaction transaction)
{
EmployeesInfo filter = new EmployeesInfo();
filter.LastName = parTagEmployeeInfo.FK0_LastName;
EmployeesBsn myClass = new EmployeesBsn(false, this.motor);
List<EmployeesInfo> list = myClass.GetSome(filter);
if (list.Count == 0)
{
//This error occurs when try to search for the ID in one table, but it does not find the value.
//Ex.: Select id,SomeField from myTable where SomeField='myValue') If no data return, this error will trigger.
throw new Exception(string.Format("Can not define ID for parTagEmployeeInfo.", parTagEmployeeInfo.FK0_LastName));
/* [Hint] The code below do one insert in the table "Employees" informing only the "EmployeeID" field.
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
throw new Exception(string.Format("Can not define ID for parTagEmployeeInfo. Theres more then one ID value for this field. ", parTagEmployeeInfo.FK0_LastName));
}
else
{
//Return the only one class found.
return list[0];
}
}
/// <summary>
/// Perform a search to find the class "TagInfo" using as key the field "Tag".
/// </summary>
/// <param name="parTagEmployeeInfo">Main class that contains the aggregation.</param>
/// <returns>Foreing key attched class.</returns>
public virtual TagInfo Get_TagFKID_FKTag(TagEmployeeInfo parTagEmployeeInfo, DbTransaction transaction)
{
TagInfo filter = new TagInfo();
filter.TextDesc = parTagEmployeeInfo.FK1_TextDesc;
TagBsn myClass = new TagBsn(false, this.motor);
List<TagInfo> list = myClass.GetSome(filter);
if (list.Count == 0)
{
//This error occurs when try to search for the ID in one table, but it does not find the value.
//Ex.: Select id,SomeField from myTable where SomeField='myValue') If no data return, this error will trigger.
throw new Exception(string.Format("Can not define ID for parTagEmployeeInfo.", parTagEmployeeInfo.FK1_TextDesc));
/* [Hint] The code below do one insert in the table "Tag" informing only the "TagID" field.
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
throw new Exception(string.Format("Can not define ID for parTagEmployeeInfo. Theres more then one ID value for this field. ", parTagEmployeeInfo.FK1_TextDesc));
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
        /// <param name="parTagEmployeeInfo">Item to delete</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void Delete(TagEmployeeInfo parTagEmployeeInfo, out string errorMessage)
        {
            errorMessage = string.Empty;

            //1) Start the transaction context.
            DbTransaction transaction = motor.BeginTransaction();

            //2) Call the overload of this method, which call the DAO but does not commit.
            this.Delete(parTagEmployeeInfo, transaction, out errorMessage);

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
        /// <param name="parTagEmployeeInfo">Item to delete</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void DeleteByID(TagEmployeeInfo parTagEmployeeInfo, out string errorMessage)
        {
            TagEmployeeInfo newParam = new TagEmployeeInfo();
            newParam.TagEmployeeID = parTagEmployeeInfo.TagEmployeeID;
            this.Delete(newParam, out errorMessage);
        }

        /// <summary>
        /// Delete registers based on class values informed. MinValues and nulls are skipped.
        /// Must have "MultipleActiveResultSets=True" on connection string.
        /// </summary>
        /// <param name="parTagEmployeeInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void Delete(TagEmployeeInfo parTagEmployeeInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = string.Empty;
            TagEmployeeDAO.Delete(parTagEmployeeInfo, transaction, out errorMessage);
            //By default, the caller of this method will do the commit.
            //motor.Commit();
            //motor.CloseConnection();
        }

        /// <summary>
        /// Delete registers based on class ID informed in transactional context. Other values are skipped.
        /// Must have "MultipleActiveResultSets=True" on connection string.
        /// </summary>
        /// <param name="parTagEmployeeInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void DeleteByID(TagEmployeeInfo parTagEmployeeInfo, DbTransaction transaction, out string errorMessage)
        {
            TagEmployeeInfo newParam = new TagEmployeeInfo();
            newParam.TagEmployeeID = parTagEmployeeInfo.TagEmployeeID;
            this.Delete(newParam, transaction, out errorMessage);
            //By default, the caller of this method will do the commit.
            //motor.Commit();
            //motor.CloseConnection();
        }

        /// <summary>
        /// Performs one "update" database command.
        /// Will commit the transaction and close the connection. Use for independent delete.
        /// </summary>
        /// <param name="TagEmployeeInfo">Object to update.</param>
        /// <param name="errorMessage">Error message if exception is throwed</param>
        public virtual void UpdateOne(TagEmployeeInfo parTagEmployeeInfo, out string errorMessage)
        {
            errorMessage = string.Empty;
            DbTransaction transaction = motor.BeginTransaction();

            this.UpdateOne(parTagEmployeeInfo, transaction, out errorMessage);
            motor.Commit();
            motor.CloseConnection();
        }

        /// <summary>
        /// Performs one "update" database command in a transactional context.
        /// * The method uses a transaction object already created and does not close the connection.
        /// * Must have "MultipleActiveResultSets=True" on connection string.
        /// </summary>
        /// <param name="TagEmployeeInfo">Object to update.</param>
        /// <param name="transaction">Inform "DBTransaction".</param>
        /// <param name="errorMessage">Error message if exception is throwed.</param>
        public virtual void UpdateOne(TagEmployeeInfo parTagEmployeeInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = string.Empty;
    
//If is trying to insert FKValue without the ID but has the unique description,
//the system will try to get the class with the ID and populate it.
if ((parTagEmployeeInfo.EmployeeIDFK == null) && (parTagEmployeeInfo.FK0_LastName != null))
{
EmployeesInfo fkClass = Get_EmployeeIDFKID_FKEmployees(parTagEmployeeInfo, transaction);
parTagEmployeeInfo.EmployeeIDFK = fkClass.EmployeeID;
}
if ((parTagEmployeeInfo.TagFK == null) && (parTagEmployeeInfo.FK1_TextDesc != null))
{
TagInfo fkClass = Get_TagFKID_FKTag(parTagEmployeeInfo, transaction);
parTagEmployeeInfo.TagFK = fkClass.TagID;
}

            TagEmployeeDAO.UpdateOne(parTagEmployeeInfo, transaction, out errorMessage);
            //By default, the caller of this method will do the commit.
            //motor.Commit();
            //motor.CloseConnection();
        }
    }
}
