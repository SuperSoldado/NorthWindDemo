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
	public partial class CustomerDemographicsDAO : BaseDataAccessObject
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="motor">Database context class. Performs the database access operations.</param>
        public CustomerDemographicsDAO(Motor motor)
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
            string aux = "insert into CustomerDemographics (CustomerTypeID,CustomerDesc) values (@CustomerTypeID,@CustomerDesc)";            
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
            return "select CustomerDemographics.CustomerTypeID,CustomerDemographics.CustomerDesc from CustomerDemographics";            
        }

        /// <summary>
        /// Get the update DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetUpdateCommand()
        {
            return "update CustomerDemographics set CustomerDesc=@CustomerDesc where CustomerTypeID=@CustomerTypeID";
        }

        /// <summary>
        /// Get the delete DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetDeleteCommand()
        {
             return "delete from CustomerDemographics";
        }

        /// <summary>
        /// Get the list of Primary Key fields.
        /// </summary>
        /// <returns>Primary key field list.</returns>
        protected virtual List<KeyValuePair<string, string>> GetPrimaryKey()
        {
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
            lst.Add(new KeyValuePair<string, string>( "CustomerTypeID","string"));
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
                    where = string.Format(" CustomerDemographics.{0}=@param_{0}", dbParameter);
                }
                else
                {
                    where += string.Format(" and CustomerDemographics.{0}=@param_{0}", dbParameter);
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
        public virtual CustomerDemographicsInfo GetValueByID(string CustomerTypeID)
        {
            //ToDo: set multiple PK filter
            motor.ClearCommandParameters();
            motor.CommandText = GetSelectCommand() + GetWherePrimaryKey();            
            List<DbParameter> paramList = new List<DbParameter>();
            

DbParameter paramCustomerTypeID = motor.Command.CreateParameter();
paramCustomerTypeID.ParameterName = "@param_CustomerTypeID";
paramCustomerTypeID.Value = CustomerTypeID;
paramList.Add(paramCustomerTypeID);

    
            motor.AddCommandParameters(paramList);
            CustomerDemographicsInfo InfoValue = new CustomerDemographicsInfo();
            
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(CustomerDemographicsInfo), dbReader);
            using (dbReader)
            {
                if (dbReader.Read())
                {
                    InfoValue = new CustomerDemographicsInfo();
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
        public virtual List<CustomerDemographicsInfo> GetAll(List<DataFilterExpressionDB> filterExpression)
        {
            List<CustomerDemographicsInfo> AllInfoList = new List<CustomerDemographicsInfo>();
            motor.ClearCommandParameters();
            motor.CommandText = GetSelectCommand() + " where 1=1 ";
            List<DbParameter> paramList = new List<DbParameter>();
            string where = "";
            foreach (DataFilterExpressionDB filter in filterExpression)
            {
                DbParameter param = motor.Command.CreateParameter();
                param.ParameterName = "@param_" + filter.FieldName;
                param.Value = filter.Filter;
                param.DbType = HelperDBType.GetDBType(typeof(CustomerDemographicsInfo), filter.FieldName);                
                if (filter.FilterType == DataFilterExpressionDB._FilterType.Equal)
                {
                    param.Value = filter.Filter;
                    where += string.Format(" and CustomerDemographics.{0} = {1}", filter.FieldName, param.ParameterName);
                }
                else
                {
                    param.Value = "%" + filter.Filter + "%";
                    where += string.Format(" and CustomerDemographics.{0} like {1}", filter.FieldName, param.ParameterName);
                }
                paramList.Add(param);
            }
            motor.CommandText += where;
            motor.AddCommandParameters(paramList);

            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(CustomerDemographicsInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    CustomerDemographicsInfo classInfo = new CustomerDemographicsInfo();
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
        public virtual List<CustomerDemographicsInfo> GetAll()
        {
            List<CustomerDemographicsInfo> AllInfoList = new List<CustomerDemographicsInfo>();
            motor.CommandText = GetSelectCommand();
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(CustomerDemographicsInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    CustomerDemographicsInfo classInfo = new CustomerDemographicsInfo();
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
        public List<CustomerDemographicsInfo> GetAll(int numberOfRowsToSkip, int numberOfRows)
        {
            List<CustomerDemographicsInfo> AllInfoList = new List<CustomerDemographicsInfo>();
            motor.CommandText = base.GetFilteredRowNumAndSkipQuery("AttributeLists", "id", numberOfRowsToSkip, numberOfRows);
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(CustomerDemographicsInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    CustomerDemographicsInfo classInfo = new CustomerDemographicsInfo();
                    classFiller.Fill(classInfo);
                    AllInfoList.Add(classInfo);
                }
            }
            return AllInfoList;
        }

        /// <summary>
        /// Insert one register in database.
        /// </summary>
        /// <param name="parCustomerDemographicsInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void InsertOne(CustomerDemographicsInfo parCustomerDemographicsInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                motor.CommandText = GetInsertCommand();
                ///Warning: performance issues with this automation. See method description for details.
                List<DbParameter> paramList = ParameterBuilder.GetParametersForInsert(typeof(CustomerDemographicsInfo), parCustomerDemographicsInfo, motor.Command);
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
        /// <param name="parCustomerDemographicsInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void Delete(CustomerDemographicsInfo parCustomerDemographicsInfo,DbTransaction transaction, out string errorMessage)
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

                List<DbParameter> paramList = ParameterBuilder.GetParametersForDelete(primaryKeys, typeof(CustomerDemographicsInfo), parCustomerDemographicsInfo, motor.Command, out whereClausule);

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
        /// <param name="parCustomerDemographicsInfo">Item to update</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void UpdateOne(CustomerDemographicsInfo parCustomerDemographicsInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                motor.CommandText = GetUpdateCommand();
                ///Warning: performance issues with this automation. See method description for details.
                List<DbParameter> paramList = ParameterBuilder.GetParametersForUpdate(typeof(CustomerDemographicsInfo), parCustomerDemographicsInfo, motor.Command);
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
        /// <param name="filter">CustomerDemographicsInfo</param>
        /// <returns>List of found records.</returns>
        public virtual List<CustomerDemographicsInfo> GetSome(CustomerDemographicsInfo filter)
        {
            List<CustomerDemographicsInfo> AllInfoList = new List<CustomerDemographicsInfo>();
            string filterWhere = string.Empty;
            List<DbParameter> paramList = null;
            GenerateWhere(filter, out filterWhere, out paramList);
            motor.ClearCommandParameters();
            motor.AddCommandParameters(paramList);
            motor.CommandText = GetSelectCommand() + " " + filterWhere;
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(CustomerDemographicsInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    CustomerDemographicsInfo CustomerDemographicsInfo = new CustomerDemographicsInfo();
                    ///Warning: performance issues with this automation. See method description for details.
                    classFiller.Fill(CustomerDemographicsInfo);
                    AllInfoList.Add(CustomerDemographicsInfo);
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
        protected void GenerateWhere(CustomerDemographicsInfo filter, out string whereClausule, out List<DbParameter> paramList)
        {
            StringBuilder where = new StringBuilder();
            paramList = new List<DbParameter>();
            where.Append("where 1=1");
            
// 1) Adding filter for field CustomerTypeID
if (filter.CustomerTypeID != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_CustomerTypeID";
param.Value = filter.CustomerTypeID;
paramList.Add(param);
where.Append(" and CustomerDemographics.CustomerTypeID=@param_CustomerTypeID");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_CustomerTypeID";
//param.Value = "%" + filter.CustomerTypeID "%";
//paramList.Add(param);
//where.Append(" and CustomerDemographics.CustomerTypeID like @param_CustomerTypeID");
}
// 2) Adding filter for field CustomerDesc
if (filter.CustomerDesc != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_CustomerDesc";
param.Value = filter.CustomerDesc;
paramList.Add(param);
where.Append(" and CustomerDemographics.CustomerDesc=@param_CustomerDesc");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_CustomerDesc";
//param.Value = "%" + filter.CustomerDesc "%";
//paramList.Add(param);
//where.Append(" and CustomerDemographics.CustomerDesc like @param_CustomerDesc");
}
            
            whereClausule = where.ToString();
        }
    }
}
