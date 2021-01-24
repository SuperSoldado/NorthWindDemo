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
	public partial class EmployeesDAO : BaseDataAccessObject
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="motor">Database context class. Performs the database access operations.</param>
        public EmployeesDAO(Motor motor)
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
            string aux = "insert into Employees (LastName,FirstName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,City,Region,PostalCode,Country,HomePhone,Extension,Photo,Notes,ReportsTo,PhotoPath) values (@LastName,@FirstName,@Title,@TitleOfCourtesy,@BirthDate,@HireDate,@Address,@City,@Region,@PostalCode,@Country,@HomePhone,@Extension,@Photo,@Notes,@ReportsTo,@PhotoPath)";            
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
            return "select Employees.EmployeeID,Employees.LastName,Employees.FirstName,Employees.Title,Employees.TitleOfCourtesy,Employees.BirthDate,Employees.HireDate,Employees.Address,Employees.City,Employees.Region,Employees.PostalCode,Employees.Country,Employees.HomePhone,Employees.Extension,Employees.Photo,Employees.Notes,Employees.ReportsTo,FK0_Employees.FirstName as FK0_FirstName,Employees.PhotoPath from Employees left join Employees FK0_Employees on(FK0_Employees.EmployeeID=Employees.ReportsTo)";            
        }

        /// <summary>
        /// Get the update DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetUpdateCommand()
        {
            return "update Employees set LastName=@LastName,FirstName=@FirstName,Title=@Title,TitleOfCourtesy=@TitleOfCourtesy,BirthDate=@BirthDate,HireDate=@HireDate,Address=@Address,City=@City,Region=@Region,PostalCode=@PostalCode,Country=@Country,HomePhone=@HomePhone,Extension=@Extension,Photo=@Photo,Notes=@Notes,ReportsTo=@ReportsTo,PhotoPath=@PhotoPath where EmployeeID=@EmployeeID";
        }

        /// <summary>
        /// Get the delete DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetDeleteCommand()
        {
             return "delete from Employees";
        }

        /// <summary>
        /// Get the list of Primary Key fields.
        /// </summary>
        /// <returns>Primary key field list.</returns>
        protected virtual List<KeyValuePair<string, string>> GetPrimaryKey()
        {
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
            lst.Add(new KeyValuePair<string, string>( "EmployeeID","int"));
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
                    where = string.Format(" Employees.{0}=@param_{0}", dbParameter);
                }
                else
                {
                    where += string.Format(" and Employees.{0}=@param_{0}", dbParameter);
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
        public virtual EmployeesInfo GetValueByID(int EmployeeID)
        {
            //ToDo: set multiple PK filter
            motor.ClearCommandParameters();
            motor.CommandText = GetSelectCommand() + GetWherePrimaryKey();            
            List<DbParameter> paramList = new List<DbParameter>();
            

DbParameter paramEmployeeID = motor.Command.CreateParameter();
paramEmployeeID.ParameterName = "@param_EmployeeID";
paramEmployeeID.Value = EmployeeID;
paramList.Add(paramEmployeeID);

    
            motor.AddCommandParameters(paramList);
            EmployeesInfo InfoValue = new EmployeesInfo();
            
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(EmployeesInfo), dbReader);
            using (dbReader)
            {
                if (dbReader.Read())
                {
                    InfoValue = new EmployeesInfo();
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
        public virtual List<EmployeesInfo> GetAll(List<DataFilterExpressionDB> filterExpression)
        {
            List<EmployeesInfo> AllInfoList = new List<EmployeesInfo>();
            motor.ClearCommandParameters();
            motor.CommandText = GetSelectCommand() + " where 1=1 ";
            List<DbParameter> paramList = new List<DbParameter>();
            string where = "";
            foreach (DataFilterExpressionDB filter in filterExpression)
            {
                DbParameter param = motor.Command.CreateParameter();
                param.ParameterName = "@param_" + filter.FieldName;
                param.Value = filter.Filter;
                param.DbType = HelperDBType.GetDBType(typeof(EmployeesInfo), filter.FieldName);                
                if (filter.FilterType == DataFilterExpressionDB._FilterType.Equal)
                {
                    param.Value = filter.Filter;
                    where += string.Format(" and Employees.{0} = {1}", filter.FieldName, param.ParameterName);
                }
                else
                {
                    param.Value = "%" + filter.Filter + "%";
                    where += string.Format(" and Employees.{0} like {1}", filter.FieldName, param.ParameterName);
                }
                paramList.Add(param);
            }
            motor.CommandText += where;
            motor.AddCommandParameters(paramList);

            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(EmployeesInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    EmployeesInfo classInfo = new EmployeesInfo();
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
        public virtual List<EmployeesInfo> GetAll()
        {
            List<EmployeesInfo> AllInfoList = new List<EmployeesInfo>();
            motor.CommandText = GetSelectCommand();
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(EmployeesInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    EmployeesInfo classInfo = new EmployeesInfo();
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
        public List<EmployeesInfo> GetAll(int numberOfRowsToSkip, int numberOfRows)
        {
            List<EmployeesInfo> AllInfoList = new List<EmployeesInfo>();
            motor.CommandText = base.GetFilteredRowNumAndSkipQuery("AttributeLists", "id", numberOfRowsToSkip, numberOfRows);
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(EmployeesInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    EmployeesInfo classInfo = new EmployeesInfo();
                    classFiller.Fill(classInfo);
                    AllInfoList.Add(classInfo);
                }
            }
            return AllInfoList;
        }

        /// <summary>
        /// Insert one register in database.
        /// </summary>
        /// <param name="parEmployeesInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void InsertOne(EmployeesInfo parEmployeesInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                motor.CommandText = GetInsertCommand();
                ///Warning: performance issues with this automation. See method description for details.
                List<DbParameter> paramList = ParameterBuilder.GetParametersForInsert(typeof(EmployeesInfo), parEmployeesInfo, motor.Command);
                motor.ClearCommandParameters();
                motor.AddCommandParameters(paramList);
                motor.AddTransaction(transaction);

                
                if (GetIdentity == true)
                {
                    parEmployeesInfo.EmployeeID = motor.ExecuteScalar();    
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
        /// <param name="parEmployeesInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void Delete(EmployeesInfo parEmployeesInfo,DbTransaction transaction, out string errorMessage)
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

                List<DbParameter> paramList = ParameterBuilder.GetParametersForDelete(primaryKeys, typeof(EmployeesInfo), parEmployeesInfo, motor.Command, out whereClausule);

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
        /// <param name="parEmployeesInfo">Item to update</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void UpdateOne(EmployeesInfo parEmployeesInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                motor.CommandText = GetUpdateCommand();
                ///Warning: performance issues with this automation. See method description for details.
                List<DbParameter> paramList = ParameterBuilder.GetParametersForUpdate(typeof(EmployeesInfo), parEmployeesInfo, motor.Command);
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
        /// <param name="filter">EmployeesInfo</param>
        /// <returns>List of found records.</returns>
        public virtual List<EmployeesInfo> GetSome(EmployeesInfo filter)
        {
            List<EmployeesInfo> AllInfoList = new List<EmployeesInfo>();
            string filterWhere = string.Empty;
            List<DbParameter> paramList = null;
            GenerateWhere(filter, out filterWhere, out paramList);
            motor.ClearCommandParameters();
            motor.AddCommandParameters(paramList);
            motor.CommandText = GetSelectCommand() + " " + filterWhere;
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(EmployeesInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    EmployeesInfo EmployeesInfo = new EmployeesInfo();
                    ///Warning: performance issues with this automation. See method description for details.
                    classFiller.Fill(EmployeesInfo);
                    AllInfoList.Add(EmployeesInfo);
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
        protected void GenerateWhere(EmployeesInfo filter, out string whereClausule, out List<DbParameter> paramList)
        {
            StringBuilder where = new StringBuilder();
            paramList = new List<DbParameter>();
            where.Append("where 1=1");
            
// 1) Adding filter for field EmployeeID
if (filter.EmployeeID != Int32.MinValue)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_EmployeeID";
param.Value = filter.EmployeeID;
paramList.Add(param);
where.Append(" and Employees.EmployeeID=@param_EmployeeID");
}
// 2) Adding filter for field LastName
if (filter.LastName != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_LastName";
param.Value = filter.LastName;
paramList.Add(param);
where.Append(" and Employees.LastName=@param_LastName");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_LastName";
//param.Value = "%" + filter.LastName "%";
//paramList.Add(param);
//where.Append(" and Employees.LastName like @param_LastName");
}
// 3) Adding filter for field FirstName
if (filter.FirstName != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_FirstName";
param.Value = filter.FirstName;
paramList.Add(param);
where.Append(" and Employees.FirstName=@param_FirstName");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_FirstName";
//param.Value = "%" + filter.FirstName "%";
//paramList.Add(param);
//where.Append(" and Employees.FirstName like @param_FirstName");
}
// 4) Adding filter for field Title
if (filter.Title != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_Title";
param.Value = filter.Title;
paramList.Add(param);
where.Append(" and Employees.Title=@param_Title");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_Title";
//param.Value = "%" + filter.Title "%";
//paramList.Add(param);
//where.Append(" and Employees.Title like @param_Title");
}
// 5) Adding filter for field TitleOfCourtesy
if (filter.TitleOfCourtesy != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_TitleOfCourtesy";
param.Value = filter.TitleOfCourtesy;
paramList.Add(param);
where.Append(" and Employees.TitleOfCourtesy=@param_TitleOfCourtesy");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_TitleOfCourtesy";
//param.Value = "%" + filter.TitleOfCourtesy "%";
//paramList.Add(param);
//where.Append(" and Employees.TitleOfCourtesy like @param_TitleOfCourtesy");
}
// 6) Adding filter for field BirthDate
if (filter.BirthDate != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_BirthDate";
param.Value = filter.BirthDate;
paramList.Add(param);
where.Append(" and Employees.BirthDate=@param_BirthDate");
}
// 7) Adding filter for field HireDate
if (filter.HireDate != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_HireDate";
param.Value = filter.HireDate;
paramList.Add(param);
where.Append(" and Employees.HireDate=@param_HireDate");
}
// 8) Adding filter for field Address
if (filter.Address != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_Address";
param.Value = filter.Address;
paramList.Add(param);
where.Append(" and Employees.Address=@param_Address");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_Address";
//param.Value = "%" + filter.Address "%";
//paramList.Add(param);
//where.Append(" and Employees.Address like @param_Address");
}
// 9) Adding filter for field City
if (filter.City != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_City";
param.Value = filter.City;
paramList.Add(param);
where.Append(" and Employees.City=@param_City");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_City";
//param.Value = "%" + filter.City "%";
//paramList.Add(param);
//where.Append(" and Employees.City like @param_City");
}
// 10) Adding filter for field Region
if (filter.Region != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_Region";
param.Value = filter.Region;
paramList.Add(param);
where.Append(" and Employees.Region=@param_Region");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_Region";
//param.Value = "%" + filter.Region "%";
//paramList.Add(param);
//where.Append(" and Employees.Region like @param_Region");
}
// 11) Adding filter for field PostalCode
if (filter.PostalCode != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_PostalCode";
param.Value = filter.PostalCode;
paramList.Add(param);
where.Append(" and Employees.PostalCode=@param_PostalCode");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_PostalCode";
//param.Value = "%" + filter.PostalCode "%";
//paramList.Add(param);
//where.Append(" and Employees.PostalCode like @param_PostalCode");
}
// 12) Adding filter for field Country
if (filter.Country != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_Country";
param.Value = filter.Country;
paramList.Add(param);
where.Append(" and Employees.Country=@param_Country");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_Country";
//param.Value = "%" + filter.Country "%";
//paramList.Add(param);
//where.Append(" and Employees.Country like @param_Country");
}
// 13) Adding filter for field HomePhone
if (filter.HomePhone != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_HomePhone";
param.Value = filter.HomePhone;
paramList.Add(param);
where.Append(" and Employees.HomePhone=@param_HomePhone");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_HomePhone";
//param.Value = "%" + filter.HomePhone "%";
//paramList.Add(param);
//where.Append(" and Employees.HomePhone like @param_HomePhone");
}
// 14) Adding filter for field Extension
if (filter.Extension != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_Extension";
param.Value = filter.Extension;
paramList.Add(param);
where.Append(" and Employees.Extension=@param_Extension");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_Extension";
//param.Value = "%" + filter.Extension "%";
//paramList.Add(param);
//where.Append(" and Employees.Extension like @param_Extension");
}
// 15) Adding filter for field Photo
if (filter.Photo != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_Photo";
param.Value = filter.Photo;
paramList.Add(param);
where.Append(" and Employees.Photo=@param_Photo");
}
// 16) Adding filter for field Notes
if (filter.Notes != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_Notes";
param.Value = filter.Notes;
paramList.Add(param);
where.Append(" and Employees.Notes=@param_Notes");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_Notes";
//param.Value = "%" + filter.Notes "%";
//paramList.Add(param);
//where.Append(" and Employees.Notes like @param_Notes");
}
// 17) Adding filter for field ReportsTo
if (filter.ReportsTo != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_ReportsTo";
param.Value = filter.ReportsTo;
paramList.Add(param);
where.Append(" and Employees.ReportsTo=@param_ReportsTo");
}

// 18) Adding filter for field FirstName
if (filter.FK0_FirstName != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_FK0_FirstName";
param.Value = filter.FK0_FirstName;
paramList.Add(param);
where.Append(" and FK0_Employees.FirstName=@param_FK0_FirstName");
}
// 19) Adding filter for field PhotoPath
if (filter.PhotoPath != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_PhotoPath";
param.Value = filter.PhotoPath;
paramList.Add(param);
where.Append(" and Employees.PhotoPath=@param_PhotoPath");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_PhotoPath";
//param.Value = "%" + filter.PhotoPath "%";
//paramList.Add(param);
//where.Append(" and Employees.PhotoPath like @param_PhotoPath");
}
            
            whereClausule = where.ToString();
        }
    }
}
