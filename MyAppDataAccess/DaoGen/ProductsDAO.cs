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
	public partial class ProductsDAO : BaseDataAccessObject
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="motor">Database context class. Performs the database access operations.</param>
        public ProductsDAO(Motor motor)
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
            string aux = "insert into Products (ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued) values (@ProductName,@SupplierID,@CategoryID,@QuantityPerUnit,@UnitPrice,@UnitsInStock,@UnitsOnOrder,@ReorderLevel,@Discontinued)";            
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
            return "select Products.ProductID,Products.ProductName,Products.SupplierID,FK0_Suppliers.CompanyName as FK0_CompanyName,Products.CategoryID,FK1_Categories.CategoryName as FK1_CategoryName,Products.QuantityPerUnit,Products.UnitPrice,Products.UnitsInStock,Products.UnitsOnOrder,Products.ReorderLevel,Products.Discontinued from Products left join Suppliers FK0_Suppliers on(FK0_Suppliers.SupplierID=Products.SupplierID) left join Categories FK1_Categories on(FK1_Categories.CategoryID=Products.CategoryID)";            
        }

        /// <summary>
        /// Get the update DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetUpdateCommand()
        {
            return "update Products set ProductName=@ProductName,SupplierID=@SupplierID,CategoryID=@CategoryID,QuantityPerUnit=@QuantityPerUnit,UnitPrice=@UnitPrice,UnitsInStock=@UnitsInStock,UnitsOnOrder=@UnitsOnOrder,ReorderLevel=@ReorderLevel,Discontinued=@Discontinued where ProductID=@ProductID";
        }

        /// <summary>
        /// Get the delete DML command.
        /// </summary>
        /// <returns>DML Command</returns>
        protected virtual string GetDeleteCommand()
        {
             return "delete from Products";
        }

        /// <summary>
        /// Get the list of Primary Key fields.
        /// </summary>
        /// <returns>Primary key field list.</returns>
        protected virtual List<KeyValuePair<string, string>> GetPrimaryKey()
        {
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
            lst.Add(new KeyValuePair<string, string>( "ProductID","int"));
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
                    where = string.Format(" Products.{0}=@param_{0}", dbParameter);
                }
                else
                {
                    where += string.Format(" and Products.{0}=@param_{0}", dbParameter);
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
        public virtual ProductsInfo GetValueByID(int ProductID)
        {
            //ToDo: set multiple PK filter
            motor.ClearCommandParameters();
            motor.CommandText = GetSelectCommand() + GetWherePrimaryKey();            
            List<DbParameter> paramList = new List<DbParameter>();
            

DbParameter paramProductID = motor.Command.CreateParameter();
paramProductID.ParameterName = "@param_ProductID";
paramProductID.Value = ProductID;
paramList.Add(paramProductID);

    
            motor.AddCommandParameters(paramList);
            ProductsInfo InfoValue = new ProductsInfo();
            
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(ProductsInfo), dbReader);
            using (dbReader)
            {
                if (dbReader.Read())
                {
                    InfoValue = new ProductsInfo();
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
        public virtual List<ProductsInfo> GetAll(List<DataFilterExpressionDB> filterExpression)
        {
            List<ProductsInfo> AllInfoList = new List<ProductsInfo>();
            motor.ClearCommandParameters();
            motor.CommandText = GetSelectCommand() + " where 1=1 ";
            List<DbParameter> paramList = new List<DbParameter>();
            string where = "";
            foreach (DataFilterExpressionDB filter in filterExpression)
            {
                DbParameter param = motor.Command.CreateParameter();
                param.ParameterName = "@param_" + filter.FieldName;
                param.Value = filter.Filter;
                param.DbType = HelperDBType.GetDBType(typeof(ProductsInfo), filter.FieldName);                
                if (filter.FilterType == DataFilterExpressionDB._FilterType.Equal)
                {
                    param.Value = filter.Filter;
                    where += string.Format(" and Products.{0} = {1}", filter.FieldName, param.ParameterName);
                }
                else
                {
                    param.Value = "%" + filter.Filter + "%";
                    where += string.Format(" and Products.{0} like {1}", filter.FieldName, param.ParameterName);
                }
                paramList.Add(param);
            }
            motor.CommandText += where;
            motor.AddCommandParameters(paramList);

            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(ProductsInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    ProductsInfo classInfo = new ProductsInfo();
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
        public virtual List<ProductsInfo> GetAll()
        {
            List<ProductsInfo> AllInfoList = new List<ProductsInfo>();
            motor.CommandText = GetSelectCommand();
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(ProductsInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    ProductsInfo classInfo = new ProductsInfo();
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
        public List<ProductsInfo> GetAll(int numberOfRowsToSkip, int numberOfRows)
        {
            List<ProductsInfo> AllInfoList = new List<ProductsInfo>();
            motor.CommandText = base.GetFilteredRowNumAndSkipQuery("AttributeLists", "id", numberOfRowsToSkip, numberOfRows);
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(ProductsInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    ProductsInfo classInfo = new ProductsInfo();
                    classFiller.Fill(classInfo);
                    AllInfoList.Add(classInfo);
                }
            }
            return AllInfoList;
        }

        /// <summary>
        /// Insert one register in database.
        /// </summary>
        /// <param name="parProductsInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void InsertOne(ProductsInfo parProductsInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                motor.CommandText = GetInsertCommand();
                ///Warning: performance issues with this automation. See method description for details.
                List<DbParameter> paramList = ParameterBuilder.GetParametersForInsert(typeof(ProductsInfo), parProductsInfo, motor.Command);
                motor.ClearCommandParameters();
                motor.AddCommandParameters(paramList);
                motor.AddTransaction(transaction);

                
                if (GetIdentity == true)
                {
                    parProductsInfo.ProductID = motor.ExecuteScalar();    
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
        /// <param name="parProductsInfo">Item to delete</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void Delete(ProductsInfo parProductsInfo,DbTransaction transaction, out string errorMessage)
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

                List<DbParameter> paramList = ParameterBuilder.GetParametersForDelete(primaryKeys, typeof(ProductsInfo), parProductsInfo, motor.Command, out whereClausule);

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
        /// <param name="parProductsInfo">Item to update</param>
        /// <param name="transaction">Transaction context</param>
        /// <param name="errorMessage">Error message</param>
        public virtual void UpdateOne(ProductsInfo parProductsInfo, DbTransaction transaction, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                motor.CommandText = GetUpdateCommand();
                ///Warning: performance issues with this automation. See method description for details.
                List<DbParameter> paramList = ParameterBuilder.GetParametersForUpdate(typeof(ProductsInfo), parProductsInfo, motor.Command);
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
        /// <param name="filter">ProductsInfo</param>
        /// <returns>List of found records.</returns>
        public virtual List<ProductsInfo> GetSome(ProductsInfo filter)
        {
            List<ProductsInfo> AllInfoList = new List<ProductsInfo>();
            string filterWhere = string.Empty;
            List<DbParameter> paramList = null;
            GenerateWhere(filter, out filterWhere, out paramList);
            motor.ClearCommandParameters();
            motor.AddCommandParameters(paramList);
            motor.CommandText = GetSelectCommand() + " " + filterWhere;
            DbDataReader dbReader = motor.ExecuteReader();
            ClassFiller classFiller = new ClassFiller(typeof(ProductsInfo), dbReader);
            using (dbReader)
            {
                while (dbReader.Read())
                {
                    ProductsInfo ProductsInfo = new ProductsInfo();
                    ///Warning: performance issues with this automation. See method description for details.
                    classFiller.Fill(ProductsInfo);
                    AllInfoList.Add(ProductsInfo);
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
        protected void GenerateWhere(ProductsInfo filter, out string whereClausule, out List<DbParameter> paramList)
        {
            StringBuilder where = new StringBuilder();
            paramList = new List<DbParameter>();
            where.Append("where 1=1");
            
// 1) Adding filter for field ProductID
if (filter.ProductID != Int32.MinValue)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_ProductID";
param.Value = filter.ProductID;
paramList.Add(param);
where.Append(" and Products.ProductID=@param_ProductID");
}
// 2) Adding filter for field ProductName
if (filter.ProductName != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_ProductName";
param.Value = filter.ProductName;
paramList.Add(param);
where.Append(" and Products.ProductName=@param_ProductName");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_ProductName";
//param.Value = "%" + filter.ProductName "%";
//paramList.Add(param);
//where.Append(" and Products.ProductName like @param_ProductName");
}
// 3) Adding filter for field SupplierID
if (filter.SupplierID != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_SupplierID";
param.Value = filter.SupplierID;
paramList.Add(param);
where.Append(" and Products.SupplierID=@param_SupplierID");
}

// 4) Adding filter for field CompanyName
if (filter.FK0_CompanyName != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_FK0_CompanyName";
param.Value = filter.FK0_CompanyName;
paramList.Add(param);
where.Append(" and FK0_Suppliers.CompanyName=@param_FK0_CompanyName");
}
// 5) Adding filter for field CategoryID
if (filter.CategoryID != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_CategoryID";
param.Value = filter.CategoryID;
paramList.Add(param);
where.Append(" and Products.CategoryID=@param_CategoryID");
}

// 6) Adding filter for field CategoryName
if (filter.FK1_CategoryName != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_FK1_CategoryName";
param.Value = filter.FK1_CategoryName;
paramList.Add(param);
where.Append(" and FK1_Categories.CategoryName=@param_FK1_CategoryName");
}
// 7) Adding filter for field QuantityPerUnit
if (filter.QuantityPerUnit != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_QuantityPerUnit";
param.Value = filter.QuantityPerUnit;
paramList.Add(param);
where.Append(" and Products.QuantityPerUnit=@param_QuantityPerUnit");
//Hint: use the code below to add a "like" search. Warning: may cause data performance issues.
//param.ParameterName = "@param_QuantityPerUnit";
//param.Value = "%" + filter.QuantityPerUnit "%";
//paramList.Add(param);
//where.Append(" and Products.QuantityPerUnit like @param_QuantityPerUnit");
}
// 8) Adding filter for field UnitPrice
if (filter.UnitPrice != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_UnitPrice";
param.Value = filter.UnitPrice;
paramList.Add(param);
where.Append(" and Products.UnitPrice=@param_UnitPrice");
}
// 9) Adding filter for field UnitsInStock
if (filter.UnitsInStock != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_UnitsInStock";
param.Value = filter.UnitsInStock;
paramList.Add(param);
where.Append(" and Products.UnitsInStock=@param_UnitsInStock");
}
// 10) Adding filter for field UnitsOnOrder
if (filter.UnitsOnOrder != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_UnitsOnOrder";
param.Value = filter.UnitsOnOrder;
paramList.Add(param);
where.Append(" and Products.UnitsOnOrder=@param_UnitsOnOrder");
}
// 11) Adding filter for field ReorderLevel
if (filter.ReorderLevel != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_ReorderLevel";
param.Value = filter.ReorderLevel;
paramList.Add(param);
where.Append(" and Products.ReorderLevel=@param_ReorderLevel");
}
// 12) Adding filter for field Discontinued
if (filter.Discontinued != null)
{
DbParameter param = motor.Command.CreateParameter();
param.ParameterName = "@param_Discontinued";
param.Value = filter.Discontinued;
paramList.Add(param);
where.Append(" and Products.Discontinued=@param_Discontinued");
}
            
            whereClausule = where.ToString();
        }
    }
}
