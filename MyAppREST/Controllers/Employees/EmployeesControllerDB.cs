using MyApp.Data.Business;
using MyApp.Data.Info;
using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using RESTLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using Newtonsoft.Json;
using DataAccessLib.Core;
using MyApp.TransferObjects.REST;
    
namespace MyApp.Rest.Controllers.Employees
{
    public class RESTEmployeesDB : RESTBaseDB
    {
        private RESTConfig restConfig { get; set; }
        public RESTEmployeesDB(RESTConfig restConfig)
        {
            this.restConfig = restConfig;
        }

        public void TryDelete(DeleteEmployeesView viewToDelete, out RestExceptionError error)
        {
            error = null;
            EmployeesInfo dbViewToDelete = new EmployeesInfo();
            try
            {
                Cloner.CopyAllTo(typeof(DeleteEmployeesView), viewToDelete, typeof(EmployeesInfo), dbViewToDelete);
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error parsing data for (Employees.TryDelete/Parsing)";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }

            try
            {
                EmployeesBsn bsn = new EmployeesBsn(restConfig);
                string dbError = null;
                bsn.Delete(dbViewToDelete, out dbError);
                if (dbError != null)
                {
                    error = new RestExceptionError();
                    error.InternalMessage = "Internal Error Save data for [Employees.TryDelete]";
                    error.ExceptionMessage = dbError;
                    error.SourceError = RestExceptionError._SourceError.ServerSide;
                    error.StackTrace = "";
                }
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error Update data for [Employees.TryDelete]";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }
        }

        public void TryUpdate(UpdateEmployeesView viewToUpdate, out RestExceptionError error)
        {
            error = null;
            EmployeesInfo dbViewToInclude = new EmployeesInfo();
            try
            {
                Cloner.CopyAllTo(typeof(UpdateEmployeesView), viewToUpdate, typeof(EmployeesInfo), dbViewToInclude);
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error parsing data for (Employees.TryUpdate/Parsing)";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }

            try
            {
                EmployeesBsn bsn = new EmployeesBsn(restConfig);
                string dbError = null;
                bsn.UpdateOne(dbViewToInclude, out dbError);
                if (dbError != null)
                {
                    error = new RestExceptionError();
                    error.InternalMessage = "Internal Error Save data for [Employees.TryUpdate]";
                    error.ExceptionMessage = dbError;
                    error.SourceError = RestExceptionError._SourceError.ServerSide;
                    error.StackTrace = "";
                }
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error Update data for [Employees.TryUpdate]";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }
        }

        public void TryInclude(CreateEmployeesView viewToInclude, out RestExceptionError error)
        {
            error = null;
            EmployeesInfo dbViewToInclude = new EmployeesInfo();
            try
            {                
                Cloner.CopyAllTo(typeof(CreateEmployeesView), viewToInclude, typeof(EmployeesInfo), dbViewToInclude);                
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error parsing data for (Employees.TryInclude/Parsing)";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }

            try
            {
                EmployeesBsn bsn = new EmployeesBsn(restConfig);
                string dbError = null;
                bsn.InsertOne(dbViewToInclude, out dbError);
                if (dbError != null)
                {
                    error = new RestExceptionError();
                    error.InternalMessage = "Internal Error Save data for [Employees.TryInclude/Save]";
                    error.ExceptionMessage = dbError;
                    error.SourceError = RestExceptionError._SourceError.ServerSide;
                    error.StackTrace = "";
                }
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error Save data for [Employees.TryInclude/Save]";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }
        }

        public T TryParse<T>(GeneralBodyPost generalBodyPost, out RestExceptionError error)
        {
            error = null; 
            if ((generalBodyPost == null) || generalBodyPost.Data == null)
            {
                error = new RestExceptionError();
                error.InternalMessage = "No data from client to include [Employees.TryParse]";
                error.ExceptionMessage = "No data";
                error.SourceError = RestExceptionError._SourceError.ClientSide;
                error.StackTrace = "RESTEmployeesDB.TryParse";
                return default(T);
            }

            try
            {                
                string jsonAsString = generalBodyPost.Data.ToString();
                var view = JsonConvert.DeserializeObject<T>(jsonAsString);
                return view;
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ClientSide;
                error.InternalMessage = "Error parsing data from body to view for [Employees.TryParse]";
                return default(T);
            }            
        }

        /// <summary>
        /// Filters a raw list based on GeneralBodyGet.Filters
        /// </summary>
        public List<GetEmployeesView> Filter(List<GetEmployeesView> rawList, GeneralBodyGet GeneralBodyGet, out RestExceptionError error)
        {
            try
            {
                error = null;
                if ((GeneralBodyGet == null) || (GeneralBodyGet.Filters == null) || (GeneralBodyGet.Filters.Count == 0))
                {
                    return rawList;
                }

                //Search request data if dynamic filtering is send.
                DataFilterExpressionREST linqQuery = GeneralBodyGet.Filters.Where(x => x.FilterType == DataFilterExpressionREST._FilterType.Dynamic).FirstOrDefault();
                List<GetEmployeesView> result = null;
                if (linqQuery != null)
                {
                    result = DynamicLinqQuery(rawList, linqQuery.Filter, out error);
                }
                else
                {
                    var filteredList = base.FilterRawList(rawList, GeneralBodyGet, out error);
                    result = filteredList.Cast<GetEmployeesView>().ToList();
                }
                
                if (error != null)
                {
                    return null;
                }
                return result;
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.ExceptionMessage = ex.Message;
                error.InternalMessage = "Error ocurred during 'Filter' ";
            }
            return null;
        }

        /// <summary>
        /// Get a list and apply linq send from request. Request must send one request using "Dynamic" filter type. 
        /// Examples:
        /// "Where(GetAllEmployeesView => (GetAllEmployeesView.EmployeesID = 4))"
        /// </summary>
        /// <param name="list"></param>
        /// <param name="GeneralBodyGet"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<GetEmployeesView> DynamicLinqQuery(List<GetEmployeesView> list, string linqQuery, out RestExceptionError error)
        {
            try
            {
                error = null;

                IQueryable<GetEmployeesView> queryableData = list.AsQueryable<GetEmployeesView>();
                var externals = new Dictionary<string, object>();
                externals.Add("list", queryableData);
                string query = "list." + linqQuery;//Example: "list.Where(GetEmployeesView => (GetEmployeesView.EmployeesID = 4))";
                var expression = System.Linq.Dynamic.DynamicExpression.Parse(typeof(IQueryable<GetEmployeesView>), query, new[] { externals });
                var result = queryableData.Provider.CreateQuery<GetEmployeesView>(expression);
                return result.ToList();

            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.ExceptionMessage = ex.Message;
                error.InternalMessage = "Error ocurred during 'DynamicLinqQuery' ";
            }
            return null;
        }

        public List<GetEmployeesView> OrderByAndTrim(List<GetEmployeesView> list, GeneralBodyGet GeneralBodyGet, out RestExceptionError error)
        {
            try
            {
                error = null;
                if ((list == null) || (GeneralBodyGet == null))
                {
                    return list;
                }
                IQueryable<GetEmployeesView> queryableData = list.AsQueryable<GetEmployeesView>();                
                
                List<GetEmployeesView> filteredList = null;
                //1)Ordering
                if (GeneralBodyGet.OrderBy != null)
                {
                    filteredList = queryableData.OrderBy(GeneralBodyGet.OrderBy).ToList();
                }
                else
                {
                    filteredList = queryableData.ToList();
                }

                //2)Skipping
                if (GeneralBodyGet.ItensToSkip > 0)
                {
                    filteredList = filteredList.Skip(GeneralBodyGet.ItensToSkip).ToList();
                }

                //3)Trimming
                if (GeneralBodyGet.NumberOfItens > 0)
                {
                    filteredList = filteredList.Take(GeneralBodyGet.NumberOfItens).ToList();
                }

                return filteredList;
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.ExceptionMessage = ex.Message;
                error.InternalMessage = "Error ocurred during 'OrderByAndTrim' ";
            }
            return null;
        }

        public List<GetEmployeesView> GetAll(out RestExceptionError error)
        {
            try
            {
                error = null;
                EmployeesBsn bsn = new EmployeesBsn(restConfig);
                List<EmployeesInfo> dbItems = bsn.GetAll();
                List<GetEmployeesView> result = new List<GetEmployeesView>();
                foreach (EmployeesInfo item in dbItems)
                {
                    GetEmployeesView view = new GetEmployeesView();
                    Cloner.CopyAllTo(typeof(EmployeesInfo), item, typeof(GetEmployeesView), view);
                    result.Add(view);
                }

                return result;
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.ExceptionMessage = ex.Message;
            }
            return null;
        }

        /// <summary>
        /// Get all itens filtering direct in DB. Up: More optimized because filters DB. Down: less flexible, don't support dynamic filters.
        /// </summary>
        /// <param name="generalBodyGet"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<GetEmployeesView> GetAllWithDBFilter(GeneralBodyGet generalBodyGet, out RestExceptionError error)
        {
            try
            {
                if ((generalBodyGet == null) || (generalBodyGet.Filters == null) || (generalBodyGet.Filters.Count == 0))
                {
                    error = new RestExceptionError();
                    error.ExceptionMessage = "";
                    error.InternalMessage = "Url does not contains filter section";
                }

                error = null;
                EmployeesBsn bsn = new EmployeesBsn(restConfig);
                List<DataFilterExpressionDB> dbFilter = HelperRESTFilterToDB.FilterRestFilterToDBExpression(generalBodyGet.Filters);
                List<EmployeesInfo> dbItems = bsn.GetAll(dbFilter);
                List<GetEmployeesView> result = new List<GetEmployeesView>();
                foreach (EmployeesInfo item in dbItems)
                {
                    GetEmployeesView view = new GetEmployeesView();
                    Cloner.CopyAllTo(typeof(EmployeesInfo), item, typeof(GetEmployeesView), view);
                    result.Add(view);
                }

                return result;
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.ExceptionMessage = ex.Message;
            }
            return null;
        }
    }
}
