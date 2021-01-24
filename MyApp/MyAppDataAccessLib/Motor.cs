using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace MyAppDataAccessLib
{
    /// <summary>
    /// This class encapsulates all data access used by DAO Classes.
    /// The objective is centralize all the data access for easily maintain 
    /// the factory model. It also give the possibility of logging all the data
    /// access writing code just in one class.
    /// </summary>
    public class Motor
    {

        private DbProviderFactory Provider = null;
        public DbConnection Connection = null;

        //precisa ser public????
        public DbCommand Command = null;
        private DbDataReader dbReader = null;
        public DbTransaction dbTransaction = null;

        ~Motor()
        {
            CloseReader();
            CloseConnection();
        }

        public DbTransaction BeginTransaction()
        {
            this.OpenConnection();
            if (this.dbTransaction == null)
            {
                this.dbTransaction = this.Connection.BeginTransaction();
            }

            else
            {
                /* testando. ver se funciona com multiplos inserts (auto complete que salva) */
                if (this.dbTransaction.Connection == null)
                {
                    //dbTransaction.Connection.Open();
                    this.dbTransaction = this.Connection.BeginTransaction();//será isso?
                }
            }
            return this.dbTransaction;
        }

        public void Commit()
        {
            this.dbTransaction.Commit();
        }

        public void Rollback()
        {
            this.dbTransaction.Rollback();
        }

        public Motor(string connectionString)
        {
            //new provider for .net core. 
            //https://weblog.west-wind.com/posts/2017/nov/27/working-around-the-lack-of-dynamic-dbproviderfactory-loading-in-net-core
            this.Provider = SqlClientFactory.Instance;



            //this.Provider = DbProviderFactories.GetFactory("System.Data.SqlClient"); Original .net code
            this.Connection = Provider.CreateConnection();
            this.Connection.ConnectionString = connectionString;
            this.Command = Connection.CreateCommand();
        }

        public Motor(string provider, string connectionString)
        {
            this.Provider = DbProviderFactories.GetFactory(provider);
            this.Connection = Provider.CreateConnection();
            this.Connection.ConnectionString = connectionString;
            this.Command = Connection.CreateCommand();
        }

        #region Events: this events are holes to be filled in future. They supposed to log data via configuration (on//off)
        protected virtual void EventBeforeOpenConnection()
        {
            //ToDo: open spot for log pourpose.
        }

        protected virtual void EventAfterOpenConnection()
        {
            //ToDo: open spot for log pourpose.
        }

        protected virtual void EventBeforeCloseConnection()
        {
            //ToDo: open spot for log pourpose.
        }

        protected virtual void EventAfterCloseConnection()
        {
            //ToDo: open spot for log pourpose.
        }

        protected virtual void EventAfterExecuteReader()
        {
            //ToDo: open spot for log pourpose.
        }

        protected virtual void EventBeforeExecuteReader()
        {
            System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
            string log = t.ToString();
        }
        #endregion

        /// <summary>
        /// Open a connection (is is not already opened)
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                if (this.Connection.State == ConnectionState.Open)
                {
                    return;
                }

                if (this.Connection.State == ConnectionState.Connecting)
                {
                    int tentative = 0;
                    while (this.Connection.State == ConnectionState.Connecting)
                    {
                        Thread.Sleep(100);
                        tentative++;
                        if (tentative == 5)
                        {
                            throw new Exception("After 5 tentatives, the conection state is still 'connecting'.");    
                        }
                    }
                }

                this.EventBeforeOpenConnection();

                if (this.Connection.State != ConnectionState.Open)
                {
                    this.Connection.Open();
                }

                if (this.Command != null)
                {
                    this.Command = this.Connection.CreateCommand();
                }

                this.EventAfterOpenConnection();
            }
            catch (Exception ex)
            {
                string e = "Error during conection open. Current conection state is:" + this.Connection.State;
                throw new Exception(e);
            }


        }

        /// <summary>
        /// Close the connection.
        /// </summary>
        public void CloseConnection()
        {
            this.EventBeforeCloseConnection();

            if (this.Connection.State != ConnectionState.Closed)
            {
                this.Connection.Close();
            }
            this.EventAfterCloseConnection();
        }

        /// <summary>
        /// Close the DBDataReader.
        /// </summary>
        public void CloseReader()
        {
            if (this.dbReader != null)
            {
                this.dbReader.Close();
            }
        }

        /// <summary>
        /// Holds the command used by the DBCommand.
        /// </summary>
        public string CommandText
        {
            set { this.Command.CommandText = value; }
            get { return this.Command.CommandText; }
        }

        /// <summary>
        /// Attach a DBTransaction to the DBCommand.
        /// </summary>
        /// <param name="transaction">DBTransaction to attach</param>
        public void AddTransaction(DbTransaction transaction)
        {
            this.Command.Transaction = transaction;
            //testando
            //this.Command.Transaction.Connection.Open();
        }

        /// <summary>
        /// Add parameters to DBCommand.
        /// </summary>
        /// <param name="commandParameters">List of params to attach</param>
        public void AddCommandParameters(List<DbParameter> commandParameters)
        {
            foreach (DbParameter param in commandParameters)
            {
                this.Command.Parameters.Add(param);
            }
        }

        public void ClearCommandParameters()
        {
            this.Command.Parameters.Clear();
        }

        /// <summary>
        /// Execute a DBCommand.ExecuteNonQuery.
        /// </summary>
        public void ExecuteNonQuery()
        {
            this.Command.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute a DBCommand.ExecuteScalar.
        /// </summary>
        public int ExecuteScalar()
        {
            object o = this.Command.ExecuteScalar();
            int id = Convert.ToInt32(o);
            return id;
        }

        /// <summary>
        /// Execute a DBCommand.ExecuteReader returning DbDataReader.
        /// </summary>
        /// <returns></returns>
        public DbDataReader ExecuteReader()
        {
            this.EventBeforeExecuteReader();

            if (this.dbTransaction != null)
            {
                this.Command.Transaction = this.dbTransaction;
            }

            try
            {
                this.dbReader = this.Command.ExecuteReader();
            }
            catch (Exception ex)
            {
                string error = this.Command.CommandText;
                new Exception(error + " - " + ex.Message);
            }

            this.EventAfterExecuteReader();
            return this.dbReader;
        }
    }
}