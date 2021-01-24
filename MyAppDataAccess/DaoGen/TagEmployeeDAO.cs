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
	public partial class TagEmployeeDAO : BaseDataAccessObject
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="motor">Database context class. Performs the database access operations.</param>
        public TagEmployeeDAO(Motor motor)
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
            string aux = "insert into TagEmployee (EmployeeIDFK,TagFK,TagEmployeeTextDesc) values (@EmployeeIDFK,@TagFK,@TagEmployeeTextDesc)";            
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
            return "select TagEmployee.TagEmployeeID,TagEmployee.EmployeeIDFK,FK0_Employees.LastName as FK0_LastName,TagEmployee.TagFK,FK1_Tag.TextDesc as FK1_TextDesc,TagEmployee.TagEmployeeTextDesc from TagEmployee left join Employees FK0_Employees on(FK0_Employees.EmployeeID=TagEmployee.EmployeeIDFK) left join Tag FK1_Tag on(FK1_Tag.TagID=TagEmployee.TagFK)";            
        }

        /// <summary>
        /// Get the update DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetUpdateCommand()
        {
            return "update TagEmployee set EmployeeIDFK=@EmployeeIDFK,TagFK=@TagFK,TagEmployeeTextDesc=@TagEmployeeTextDesc where TagEmployeeID=@TagEmployeeID";
        }

        /// <summary>
        /// Get the delete DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetDeleteCommand()
        {
             return "delete from TagEmployee";
        }

        /// <summary>
        /// Get the list of Primary Key fields.
        /// </summary>
        /// <returns>Primary key field list.</returns>
        protected virtual List<KeyValuePair<string, string>> GetPrimaryKey()
        {
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
            lst.Add(new KeyValuePair<string, string>( "TagEmployeeID","int"));
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
                    where = string.Format(" TagEmployee.{0}=@param_{0}", dbParameter);
                }
                else
                {
                    where += string.Format(" and TagEmployee.{0}=@param_{0}", dbParameter);
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
        public virtual TagEmployeeInfo GetValueByID(int TagEmployeeID)
        {
            //ToDo: set multiple PK filter
            motor.ClearCommandParameters();
            motor.CommandText = GetSelectCommand() + GetWherePrimaryKey();            
            List<DbParameter> paramList = new List<DbParameter>();
            

DbParameter paramTagEmployeeID = motor.Command.CreateParameter();
paramTagEmployeeID.ParameterName = "@param_TagEmployeeID";
paramTagEmployeeID.Value = TagEmployeeID;
paramList.Add(paramTagEmployeeID);

    
            motor.AddCommandParameters(paramList);
            TagEmployeeInfo InfoValue = new TagEmployeeInfo();
            
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(TagEmployeeInfo), dbReader);
            using (dbReader)
            {
                if (dbReader.Read())
                {
                    InfoValue = new TagEmployeeInfo();
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
        public virtual List<TagEmployeeInfo> GetAll(List<DataFilterExpressionDB> filterExpression)
        {
            List<TagEmployeeInfo> AllInfoList = new List<TagEmployeeInfo>();
            motor.ClearCommandParameters();
            motor.CommandText = GetSelectCommand() + " where 1=1 ";
            List<DbParameter> paramList = new List<DbParameter>();
            string where = "";
            foreach (DataFilterExpressionDB filter in filterExpression)
            {
                DbParameter param = motor.Command.CreateParameter();
                param.ParameterName = "@param_" + filter.FieldName;
                param.Value = filter.Filter;
                param.DbType = HelperDBType.GetDBType(typeof(TagEmployeeInfo), filter.FieldName);                
                if (filter.FilterType == DataFilterExpressionDB._FilterType.Equal)
                {
                    param.Value = filter.Filter;
                    where += string.Format(" and TagEmployee.{0} = {1}", filter.FieldName, param.ParameterName);
                }
                else
                {
                    param.Value = "%" + filter.Filter + "%";
                    where += string.Format(" and TagEmployee.{0} like {1}", filter.FieldName, param.ParameterName);
                }
                paramList.Add(param);
            }
            motor.CommandText += where;
            motor.AddCommandParameters(paramList);

            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(TagEmployeeInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    TagEmployeeInfo classInfo = new TagEmployeeInfo();
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
        public virtual List<TagEmployeeInfo> GetAll()
        {
            List<TagEmployeeInfo> AllInfoList = new List<TagEmployeeInfo>();
            motor.CommandText = GetSelectCommand();
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(TagEmployeeInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    TagEmployeeInfo classInfo = new TagEmployeeInfo();
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
        public List<TagEmployeeInfo> GetAll(int numberOfRowsToSkip, int numberOfRows)
        {
            List<TagEmployeeInfo> AllInfoList = new List<TagEmployeeInfo>();
            motor.CommandText = base.GetFilteredRowNumAndSkipQuery("AttributeLists", "id", numberOfRowsToSkip, numberOfRows);
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(TagEmployeeInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    TagEmployeeInfo classInfo = new TagEmployeeInfo();
                    classFiller.Fill(classInfo);
                    AllInfoList.Add(classInfo);
                }
            }
            return AllInfoList;
        }

        /// <summary>
        /// Insert one register in database.
        /// </summary>
        /// <param name="parTagEmployeeInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void InsertOne(TagEmployeeInfo parTagEmployeeInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                motor.CommandText = GetInsertCommand();
                ///Warning: performance issues with this automation. See method description for details.
                List<DbParameter> paramList = ParameterBuilder.GetParametersForInsert(typeof(TagEmployeeInfo), parTagEmployeeInfo, motor.Command);
                motor.ClearCommandParameters();
                motor.AddCommandParameters(paramList);
                motor.AddTransaction(transaction);

                
                if (GetIdentity == true)
                {
                    parTagEmployeeInfo.TagEmployeeID = motor.ExecuteScalar();    
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
        /// <param name="parTagEmployeeInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void Delete(TagEmployeeInfo parTagEmployeeInfo,DbTransaction transaction, out string errorMessage)
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

                List<DbParameter> paramList = ParameterBuilder.GetParametersForDelete(primaryKeys, typeof(TagEmployeeInfo), parTagEmployeeInfo, motor.Command, out whereClausule);

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
        /// <param name="parTagEmployeeInfo">Item to update</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void UpdateOne(TagEmployeeInfo parTagEmployeeInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                motor.CommandText = GetUpdateCommand();
                ///Warning: performance issues with this automation. See method description for details.
                List<DbParameter> paramList = ParameterBuilder.GetParametersForUpdate(typeof(TagEmployeeInfo), parTagEmployeeInfo, motor.Command);
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
        /// <param name="filter">TagEmployeeInfo</param>
        /// <returns>List of found records.</returns>
        public virtual List<TagEmployeeInfo> GetSome(TagEmployeeInfo filter)
        {
            List<TagEmployeeInfo> AllInfoList = new List<TagEmployeeInfo>();
            string filterWhere = string.Empty;
            List<DbParameter> paramList = null;
            GenerateWhere(filter, out filterWhere, out paramList);
            motor.ClearCommandParameters();
            motor.AddCommandParameters(paramList);
            motor.CommandText = GetSelectCommand() + " " + filterWhere;
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(TagEmployeeInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    TagEmployeeInfo TagEmployeeInfo = new TagEmployeeInfo();
                    ///Warning: performance issues with this automation. See method description for details.
                    classFiller.Fill(TagEmployeeInfo);
                    AllInfoList.Add(TagEmployeeInfo);
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
        protected void GenerateWhere(TagEmployeeInfo filter, out string whereClausule, out List<DbParameter> paramList)
        {
            StringBuilder where = new StringBuilder();
            paramList = new List<DbParameter>();
            where.Append("where 1=1");
            
// 1) Adding filter for field TagEmployeeID
if (filter.TagEmployeeID != Int32.MinValue)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_TagEmployeeID";
param.Value = filter.TagEmployeeID;
paramList.Add(param);
where.Append(" and TagEmployee.TagEmployeeID=@param_TagEmployeeID");
}
// 2) Adding filter for field EmployeeIDFK
if (filter.EmployeeIDFK != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_EmployeeIDFK";
param.Value = filter.EmployeeIDFK;
paramList.Add(param);
where.Append(" and TagEmployee.EmployeeIDFK=@param_EmployeeIDFK");
}

// 3) Adding filter for field LastName
if (filter.FK0_LastName != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_FK0_LastName";
param.Value = filter.FK0_LastName;
paramList.Add(param);
where.Append(" and FK0_Employees.LastName=@param_FK0_LastName");
}
// 4) Adding filter for field TagFK
if (filter.TagFK != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_TagFK";
param.Value = filter.TagFK;
paramList.Add(param);
where.Append(" and TagEmployee.TagFK=@param_TagFK");
}

// 5) Adding filter for field TextDesc
if (filter.FK1_TextDesc != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_FK1_TextDesc";
param.Value = filter.FK1_TextDesc;
paramList.Add(param);
where.Append(" and FK1_Tag.TextDesc=@param_FK1_TextDesc");
}
// 6) Adding filter for field TagEmployeeTextDesc
if (filter.TagEmployeeTextDesc != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_TagEmployeeTextDesc";
param.Value = filter.TagEmployeeTextDesc;
paramList.Add(param);
where.Append(" and TagEmployee.TagEmployeeTextDesc=@param_TagEmployeeTextDesc");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_TagEmployeeTextDesc";
//param.Value = "%" + filter.TagEmployeeTextDesc "%";
//paramList.Add(param);
//where.Append(" and TagEmployee.TagEmployeeTextDesc like @param_TagEmployeeTextDesc");
}
            
            whereClausule = where.ToString();
        }
    }
}
