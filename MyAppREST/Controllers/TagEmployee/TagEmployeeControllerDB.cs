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
    
namespace MyApp.Rest.Controllers.TagEmployee
{
    public class RESTTagEmployeeDB : RESTBaseDB
    {
        private RESTConfig restConfig { get; set; }
        public RESTTagEmployeeDB(RESTConfig restConfig)
        {
            this.restConfig = restConfig;
        }

        public void TryDelete(DeleteTagEmployeeView viewToDelete, out RestExceptionError error)
        {
            error = null;
            TagEmployeeInfo dbViewToDelete = new TagEmployeeInfo();
            try
            {
                Cloner.CopyAllTo(typeof(DeleteTagEmployeeView), viewToDelete, typeof(TagEmployeeInfo), dbViewToDelete);
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error parsing data for (TagEmployee.TryDelete/Parsing)";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }

            try
            {
                TagEmployeeBsn bsn = new TagEmployeeBsn(restConfig);
                string dbError = null;
                bsn.Delete(dbViewToDelete, out dbError);
                if (dbError != null)
                {
                    error = new RestExceptionError();
                    error.InternalMessage = "Internal Error Save data for [TagEmployee.TryDelete]";
                    error.ExceptionMessage = dbError;
                    error.SourceError = RestExceptionError._SourceError.ServerSide;
                    error.StackTrace = "";
                }
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error Update data for [TagEmployee.TryDelete]";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }
        }

        public void TryUpdate(UpdateTagEmployeeView viewToUpdate, out RestExceptionError error)
        {
            error = null;
            TagEmployeeInfo dbViewToInclude = new TagEmployeeInfo();
            try
            {
                Cloner.CopyAllTo(typeof(UpdateTagEmployeeView), viewToUpdate, typeof(TagEmployeeInfo), dbViewToInclude);
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error parsing data for (TagEmployee.TryUpdate/Parsing)";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }

            try
            {
                TagEmployeeBsn bsn = new TagEmployeeBsn(restConfig);
                string dbError = null;
                bsn.UpdateOne(dbViewToInclude, out dbError);
                if (dbError != null)
                {
                    error = new RestExceptionError();
                    error.InternalMessage = "Internal Error Save data for [TagEmployee.TryUpdate]";
                    error.ExceptionMessage = dbError;
                    error.SourceError = RestExceptionError._SourceError.ServerSide;
                    error.StackTrace = "";
                }
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error Update data for [TagEmployee.TryUpdate]";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }
        }

        public void TryInclude(CreateTagEmployeeView viewToInclude, out RestExceptionError error)
        {
            error = null;
            TagEmployeeInfo dbViewToInclude = new TagEmployeeInfo();
            try
            {                
                Cloner.CopyAllTo(typeof(CreateTagEmployeeView), viewToInclude, typeof(TagEmployeeInfo), dbViewToInclude);                
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error parsing data for (TagEmployee.TryInclude/Parsing)";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }

            try
            {
                TagEmployeeBsn bsn = new TagEmployeeBsn(restConfig);
                string dbError = null;
                bsn.InsertOne(dbViewToInclude, out dbError);
                if (dbError != null)
                {
                    error = new RestExceptionError();
                    error.InternalMessage = "Internal Error Save data for [TagEmployee.TryInclude/Save]";
                    error.ExceptionMessage = dbError;
                    error.SourceError = RestExceptionError._SourceError.ServerSide;
                    error.StackTrace = "";
                }
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error Save data for [TagEmployee.TryInclude/Save]";
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
                error.InternalMessage = "No data from client to include [TagEmployee.TryParse]";
                error.ExceptionMessage = "No data";
                error.SourceError = RestExceptionError._SourceError.ClientSide;
                error.StackTrace = "RESTTagEmployeeDB.TryParse";
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
                error.InternalMessage = "Error parsing data from body to view for [TagEmployee.TryParse]";
                return default(T);
            }            
        }

        /// <summary>
        /// Filters a raw list based on GeneralBodyGet.Filters
        /// </summary>
        public List<GetTagEmployeeView> Filter(List<GetTagEmployeeView> rawList, GeneralBodyGet GeneralBodyGet, out RestExceptionError error)
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
                List<GetTagEmployeeView> result = null;
                if (linqQuery != null)
                {
                    result = DynamicLinqQuery(rawList, linqQuery.Filter, out error);
                }
                else
                {
                    var filteredList = base.FilterRawList(rawList, GeneralBodyGet, out error);
                    result = filteredList.Cast<GetTagEmployeeView>().ToList();
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
        /// "Where(GetAllTagEmployeeView => (GetAllTagEmployeeView.TagEmployeeID = 4))"
        /// </summary>
        /// <param name="list"></param>
        /// <param name="GeneralBodyGet"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<GetTagEmployeeView> DynamicLinqQuery(List<GetTagEmployeeView> list, string linqQuery, out RestExceptionError error)
        {
            try
            {
                error = null;

                IQueryable<GetTagEmployeeView> queryableData = list.AsQueryable<GetTagEmployeeView>();
                var externals = new Dictionary<string, object>();
                externals.Add("list", queryableData);
                string query = "list." + linqQuery;//Example: "list.Where(GetTagEmployeeView => (GetTagEmployeeView.TagEmployeeID = 4))";
                var expression = System.Linq.Dynamic.DynamicExpression.Parse(typeof(IQueryable<GetTagEmployeeView>), query, new[] { externals });
                var result = queryableData.Provider.CreateQuery<GetTagEmployeeView>(expression);
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

        public List<GetTagEmployeeView> OrderByAndTrim(List<GetTagEmployeeView> list, GeneralBodyGet GeneralBodyGet, out RestExceptionError error)
        {
            try
            {
                error = null;
                if ((list == null) || (GeneralBodyGet == null))
                {
                    return list;
                }
                IQueryable<GetTagEmployeeView> queryableData = list.AsQueryable<GetTagEmployeeView>();                
                
                List<GetTagEmployeeView> filteredList = null;
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

        public List<GetTagEmployeeView> GetAll(out RestExceptionError error)
        {
            try
            {
                error = null;
                TagEmployeeBsn bsn = new TagEmployeeBsn(restConfig);
                List<TagEmployeeInfo> dbItems = bsn.GetAll();
                List<GetTagEmployeeView> result = new List<GetTagEmployeeView>();
                foreach (TagEmployeeInfo item in dbItems)
                {
                    GetTagEmployeeView view = new GetTagEmployeeView();
                    Cloner.CopyAllTo(typeof(TagEmployeeInfo), item, typeof(GetTagEmployeeView), view);
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
        public List<GetTagEmployeeView> GetAllWithDBFilter(GeneralBodyGet generalBodyGet, out RestExceptionError error)
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
                TagEmployeeBsn bsn = new TagEmployeeBsn(restConfig);
                List<DataFilterExpressionDB> dbFilter = HelperRESTFilterToDB.FilterRestFilterToDBExpression(generalBodyGet.Filters);
                List<TagEmployeeInfo> dbItems = bsn.GetAll(dbFilter);
                List<GetTagEmployeeView> result = new List<GetTagEmployeeView>();
                foreach (TagEmployeeInfo item in dbItems)
                {
                    GetTagEmployeeView view = new GetTagEmployeeView();
                    Cloner.CopyAllTo(typeof(TagEmployeeInfo), item, typeof(GetTagEmployeeView), view);
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
