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
	public partial class TagDAO : BaseDataAccessObject
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="motor">Database context class. Performs the database access operations.</param>
        public TagDAO(Motor motor)
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
            string aux = "insert into Tag (TextDesc,TagType) values (@TextDesc,@TagType)";            
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
            return "select Tag.TagID,Tag.TextDesc,Tag.TagType from Tag";            
        }

        /// <summary>
        /// Get the update DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetUpdateCommand()
        {
            return "update Tag set TextDesc=@TextDesc,TagType=@TagType where TagID=@TagID";
        }

        /// <summary>
        /// Get the delete DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetDeleteCommand()
        {
             return "delete from Tag";
        }

        /// <summary>
        /// Get the list of Primary Key fields.
        /// </summary>
        /// <returns>Primary key field list.</returns>
        protected virtual List<KeyValuePair<string, string>> GetPrimaryKey()
        {
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
            lst.Add(new KeyValuePair<string, string>( "TagID","int"));
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
                    where = string.Format(" Tag.{0}=@param_{0}", dbParameter);
                }
                else
                {
                    where += string.Format(" and Tag.{0}=@param_{0}", dbParameter);
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
        public virtual TagInfo GetValueByID(int TagID)
        {
            //ToDo: set multiple PK filter
            motor.ClearCommandParameters();
            motor.CommandText = GetSelectCommand() + GetWherePrimaryKey();            
            List<DbParameter> paramList = new List<DbParameter>();
            

DbParameter paramTagID = motor.Command.CreateParameter();
paramTagID.ParameterName = "@param_TagID";
paramTagID.Value = TagID;
paramList.Add(paramTagID);

    
            motor.AddCommandParameters(paramList);
            TagInfo InfoValue = new TagInfo();
            
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(TagInfo), dbReader);
            using (dbReader)
            {
                if (dbReader.Read())
                {
                    InfoValue = new TagInfo();
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
        public virtual List<TagInfo> GetAll(List<DataFilterExpressionDB> filterExpression)
        {
            List<TagInfo> AllInfoList = new List<TagInfo>();
            motor.ClearCommandParameters();
            motor.CommandText = GetSelectCommand() + " where 1=1 ";
            List<DbParameter> paramList = new List<DbParameter>();
            string where = "";
            foreach (DataFilterExpressionDB filter in filterExpression)
            {
                DbParameter param = motor.Command.CreateParameter();
                param.ParameterName = "@param_" + filter.FieldName;
                param.Value = filter.Filter;
                param.DbType = HelperDBType.GetDBType(typeof(TagInfo), filter.FieldName);                
                if (filter.FilterType == DataFilterExpressionDB._FilterType.Equal)
                {
                    param.Value = filter.Filter;
                    where += string.Format(" and Tag.{0} = {1}", filter.FieldName, param.ParameterName);
                }
                else
                {
                    param.Value = "%" + filter.Filter + "%";
                    where += string.Format(" and Tag.{0} like {1}", filter.FieldName, param.ParameterName);
                }
                paramList.Add(param);
            }
            motor.CommandText += where;
            motor.AddCommandParameters(paramList);

            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(TagInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    TagInfo classInfo = new TagInfo();
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
        public virtual List<TagInfo> GetAll()
        {
            List<TagInfo> AllInfoList = new List<TagInfo>();
            motor.CommandText = GetSelectCommand();
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(TagInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    TagInfo classInfo = new TagInfo();
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
        public List<TagInfo> GetAll(int numberOfRowsToSkip, int numberOfRows)
        {
            List<TagInfo> AllInfoList = new List<TagInfo>();
            motor.CommandText = base.GetFilteredRowNumAndSkipQuery("AttributeLists", "id", numberOfRowsToSkip, numberOfRows);
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(TagInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    TagInfo classInfo = new TagInfo();
                    classFiller.Fill(classInfo);
                    AllInfoList.Add(classInfo);
                }
            }
            return AllInfoList;
        }

        /// <summary>
        /// Insert one register in database.
        /// </summary>
        /// <param name="parTagInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void InsertOne(TagInfo parTagInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                motor.CommandText = GetInsertCommand();
                ///Warning: performance issues with this automation. See method description for details.
                List<DbParameter> paramList = ParameterBuilder.GetParametersForInsert(typeof(TagInfo), parTagInfo, motor.Command);
                motor.ClearCommandParameters();
                motor.AddCommandParameters(paramList);
                motor.AddTransaction(transaction);

                
                if (GetIdentity == true)
                {
                    parTagInfo.TagID = motor.ExecuteScalar();    
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
        /// <param name="parTagInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void Delete(TagInfo parTagInfo,DbTransaction transaction, out string errorMessage)
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

                List<DbParameter> paramList = ParameterBuilder.GetParametersForDelete(primaryKeys, typeof(TagInfo), parTagInfo, motor.Command, out whereClausule);

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
        /// <param name="parTagInfo">Item to update</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void UpdateOne(TagInfo parTagInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                motor.CommandText = GetUpdateCommand();
                ///Warning: performance issues with this automation. See method description for details.
                List<DbParameter> paramList = ParameterBuilder.GetParametersForUpdate(typeof(TagInfo), parTagInfo, motor.Command);
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
        /// <param name="filter">TagInfo</param>
        /// <returns>List of found records.</returns>
        public virtual List<TagInfo> GetSome(TagInfo filter)
        {
            List<TagInfo> AllInfoList = new List<TagInfo>();
            string filterWhere = string.Empty;
            List<DbParameter> paramList = null;
            GenerateWhere(filter, out filterWhere, out paramList);
            motor.ClearCommandParameters();
            motor.AddCommandParameters(paramList);
            motor.CommandText = GetSelectCommand() + " " + filterWhere;
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(TagInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    TagInfo TagInfo = new TagInfo();
                    ///Warning: performance issues with this automation. See method description for details.
                    classFiller.Fill(TagInfo);
                    AllInfoList.Add(TagInfo);
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
        protected void GenerateWhere(TagInfo filter, out string whereClausule, out List<DbParameter> paramList)
        {
            StringBuilder where = new StringBuilder();
            paramList = new List<DbParameter>();
            where.Append("where 1=1");
            
// 1) Adding filter for field TagID
if (filter.TagID != Int32.MinValue)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_TagID";
param.Value = filter.TagID;
paramList.Add(param);
where.Append(" and Tag.TagID=@param_TagID");
}
// 2) Adding filter for field TextDesc
if (filter.TextDesc != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_TextDesc";
param.Value = filter.TextDesc;
paramList.Add(param);
where.Append(" and Tag.TextDesc=@param_TextDesc");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_TextDesc";
//param.Value = "%" + filter.TextDesc "%";
//paramList.Add(param);
//where.Append(" and Tag.TextDesc like @param_TextDesc");
}
// 3) Adding filter for field TagType
if (filter.TagType != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_TagType";
param.Value = filter.TagType;
paramList.Add(param);
where.Append(" and Tag.TagType=@param_TagType");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_TagType";
//param.Value = "%" + filter.TagType "%";
//paramList.Add(param);
//where.Append(" and Tag.TagType like @param_TagType");
}
            
            whereClausule = where.ToString();
        }
    }
}
