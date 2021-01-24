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
	public partial class CategoriesBsn
    {
        public IBaseGlobalConfig baseGlobalConfig;

        /// <summary>
        /// Use this contructor for single connection operations.
        /// </summary>
        public CategoriesBsn(IBaseGlobalConfig baseGlobalConfig)
        {
            this.baseGlobalConfig = baseGlobalConfig;
            this.motor = new Motor(baseGlobalConfig.GetMainConnectionString);
            GenerateDataAcces();
            this.CategoriesDAO = new CategoriesDAO(motor);
        }

        /// <summary>
        /// Use this constructor when using multi transactional operations.
        /// </summary>
        /// <param name="closeConnectionWhenFinish">If true, close the connection after any CRUD operation</param>
        /// <param name="motor">Motor class from caller</param>
        public CategoriesBsn(bool closeConnectionWhenFinish, Motor motor)
        {
            this.motor = motor;
            this.CategoriesDAO = new CategoriesDAO(motor);
            this.closeConnectionWhenFinish = closeConnectionWhenFinish;
        }
        /// <summary>
        /// Motor is the main BD class. Encapsulates DbProviderFactory, DbConnection, DbCommand, DbDataReader, DbTransaction
        /// </summary>
        public Motor motor = null;

        /// <summary>
        /// The Data Access Object overrided class.
        /// </summary>
        public CategoriesDAO CategoriesDAO = null;

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
        public virtual CategoriesInfo GetValueByID(int CategoryID)
        {
            motor.OpenConnection();
            CategoriesInfo value = CategoriesDAO.GetValueByID(CategoryID);
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
        public virtual List<CategoriesInfo> GetAll(List<DataFilterExpressionDB> filterExpression)
        {
            motor.OpenConnection();
            List<CategoriesInfo> list = CategoriesDAO.GetAll(filterExpression);
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
        public virtual List<CategoriesInfo> GetAll()
        {
            motor.OpenConnection();
            List<CategoriesInfo> list = CategoriesDAO.GetAll();
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
        public virtual List<CategoriesInfo> GetAll(int numberOfRowsToSkip, int numberOfRows)
        {
            motor.OpenConnection();
            List<CategoriesInfo> list = CategoriesDAO.GetAll(numberOfRowsToSkip, numberOfRows);
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
        public virtual List<CategoriesInfo> GetSome(CategoriesInfo filter)
        {
            motor.OpenConnection();
            List<CategoriesInfo> list = CategoriesDAO.GetSome(filter);
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
        /// <param name="CategoriesInfo">Object to insert.</param>
        /// <param name="errorMessage">Error message if exception is throwed</param>
        public virtual void InsertOne(CategoriesInfo parCategoriesInfo, out string errorMessage)
        {
            errorMessage = string.Empty;        
            
    
            //1) Start the transaction context.
            DbTransaction transaction = motor.BeginTransaction();
            

            //2) Call the overload of this method, which call the DAO but does not commit.
            this.InsertOne(parCategoriesInfo, transaction, out errorMessage);
            
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
        /// <param name="CategoriesInfo">Object to insert.</param>
        /// <param name="transaction">Inform "DBTransaction".</param>
        /// <param name="errorMessage">Error message if exception is throwed.</param>
        public virtual void InsertOne(CategoriesInfo parCategoriesInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = string.Empty;
            

            CategoriesDAO.InsertOne(parCategoriesInfo, transaction, out errorMessage);
            //By default, the caller of this method will do the commit.
            //motor.Commit();
            //motor.CloseConnection();
        }


        /// <summary>
        /// Delete registers based on class values informed. MinValues and nulls are skipped.
        /// </summary>
        /// <param name="parCategoriesInfo">Item to delete</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void Delete(CategoriesInfo parCategoriesInfo, out string errorMessage)
        {
            errorMessage = string.Empty;

            //1) Start the transaction context.
            DbTransaction transaction = motor.BeginTransaction();

            //2) Call the overload of this method, which call the DAO but does not commit.
            this.Delete(parCategoriesInfo, transaction, out errorMessage);

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
        /// <param name="parCategoriesInfo">Item to delete</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void DeleteByID(CategoriesInfo parCategoriesInfo, out string errorMessage)
        {
            CategoriesInfo newParam = new CategoriesInfo();
            newParam.CategoryID = parCategoriesInfo.CategoryID;
            this.Delete(newParam, out errorMessage);
        }

        /// <summary>
        /// Delete registers based on class values informed. MinValues and nulls are skipped.
        /// Must have "MultipleActiveResultSets=True" on connection string.
        /// </summary>
        /// <param name="parCategoriesInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void Delete(CategoriesInfo parCategoriesInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = string.Empty;
            CategoriesDAO.Delete(parCategoriesInfo, transaction, out errorMessage);
            //By default, the caller of this method will do the commit.
            //motor.Commit();
            //motor.CloseConnection();
        }

        /// <summary>
        /// Delete registers based on class ID informed in transactional context. Other values are skipped.
        /// Must have "MultipleActiveResultSets=True" on connection string.
        /// </summary>
        /// <param name="parCategoriesInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void DeleteByID(CategoriesInfo parCategoriesInfo, DbTransaction transaction, out string errorMessage)
        {
            CategoriesInfo newParam = new CategoriesInfo();
            newParam.CategoryID = parCategoriesInfo.CategoryID;
            this.Delete(newParam, transaction, out errorMessage);
            //By default, the caller of this method will do the commit.
            //motor.Commit();
            //motor.CloseConnection();
        }

        /// <summary>
        /// Performs one "update" database command.
        /// Will commit the transaction and close the connection. Use for independent delete.
        /// </summary>
        /// <param name="CategoriesInfo">Object to update.</param>
        /// <param name="errorMessage">Error message if exception is throwed</param>
        public virtual void UpdateOne(CategoriesInfo parCategoriesInfo, out string errorMessage)
        {
            errorMessage = string.Empty;
            DbTransaction transaction = motor.BeginTransaction();

            this.UpdateOne(parCategoriesInfo, transaction, out errorMessage);
            motor.Commit();
            motor.CloseConnection();
        }

        /// <summary>
        /// Performs one "update" database command in a transactional context.
        /// * The method uses a transaction object already created and does not close the connection.
        /// * Must have "MultipleActiveResultSets=True" on connection string.
        /// </summary>
        /// <param name="CategoriesInfo">Object to update.</param>
        /// <param name="transaction">Inform "DBTransaction".</param>
        /// <param name="errorMessage">Error message if exception is throwed.</param>
        public virtual void UpdateOne(CategoriesInfo parCategoriesInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = string.Empty;
    

            CategoriesDAO.UpdateOne(parCategoriesInfo, transaction, out errorMessage);
            //By default, the caller of this method will do the commit.
            //motor.Commit();
            //motor.CloseConnection();
        }
    }
}
