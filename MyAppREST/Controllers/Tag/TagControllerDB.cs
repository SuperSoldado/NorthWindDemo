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
    
namespace MyApp.Rest.Controllers.Tag
{
    public class RESTTagDB : RESTBaseDB
    {
        private RESTConfig restConfig { get; set; }
        public RESTTagDB(RESTConfig restConfig)
        {
            this.restConfig = restConfig;
        }

        public void TryDelete(DeleteTagView viewToDelete, out RestExceptionError error)
        {
            error = null;
            TagInfo dbViewToDelete = new TagInfo();
            try
            {
                Cloner.CopyAllTo(typeof(DeleteTagView), viewToDelete, typeof(TagInfo), dbViewToDelete);
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error parsing data for (Tag.TryDelete/Parsing)";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }

            try
            {
                TagBsn bsn = new TagBsn(restConfig);
                string dbError = null;
                bsn.Delete(dbViewToDelete, out dbError);
                if (dbError != null)
                {
                    error = new RestExceptionError();
                    error.InternalMessage = "Internal Error Save data for [Tag.TryDelete]";
                    error.ExceptionMessage = dbError;
                    error.SourceError = RestExceptionError._SourceError.ServerSide;
                    error.StackTrace = "";
                }
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error Update data for [Tag.TryDelete]";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }
        }

        public void TryUpdate(UpdateTagView viewToUpdate, out RestExceptionError error)
        {
            error = null;
            TagInfo dbViewToInclude = new TagInfo();
            try
            {
                Cloner.CopyAllTo(typeof(UpdateTagView), viewToUpdate, typeof(TagInfo), dbViewToInclude);
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error parsing data for (Tag.TryUpdate/Parsing)";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }

            try
            {
                TagBsn bsn = new TagBsn(restConfig);
                string dbError = null;
                bsn.UpdateOne(dbViewToInclude, out dbError);
                if (dbError != null)
                {
                    error = new RestExceptionError();
                    error.InternalMessage = "Internal Error Save data for [Tag.TryUpdate]";
                    error.ExceptionMessage = dbError;
                    error.SourceError = RestExceptionError._SourceError.ServerSide;
                    error.StackTrace = "";
                }
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error Update data for [Tag.TryUpdate]";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }
        }

        public void TryInclude(CreateTagView viewToInclude, out RestExceptionError error)
        {
            error = null;
            TagInfo dbViewToInclude = new TagInfo();
            try
            {                
                Cloner.CopyAllTo(typeof(CreateTagView), viewToInclude, typeof(TagInfo), dbViewToInclude);                
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error parsing data for (Tag.TryInclude/Parsing)";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }

            try
            {
                TagBsn bsn = new TagBsn(restConfig);
                string dbError = null;
                bsn.InsertOne(dbViewToInclude, out dbError);
                if (dbError != null)
                {
                    error = new RestExceptionError();
                    error.InternalMessage = "Internal Error Save data for [Tag.TryInclude/Save]";
                    error.ExceptionMessage = dbError;
                    error.SourceError = RestExceptionError._SourceError.ServerSide;
                    error.StackTrace = "";
                }
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error Save data for [Tag.TryInclude/Save]";
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
                error.InternalMessage = "No data from client to include [Tag.TryParse]";
                error.ExceptionMessage = "No data";
                error.SourceError = RestExceptionError._SourceError.ClientSide;
                error.StackTrace = "RESTTagDB.TryParse";
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
                error.InternalMessage = "Error parsing data from body to view for [Tag.TryParse]";
                return default(T);
            }            
        }

        /// <summary>
        /// Filters a raw list based on GeneralBodyGet.Filters
        /// </summary>
        public List<GetTagView> Filter(List<GetTagView> rawList, GeneralBodyGet GeneralBodyGet, out RestExceptionError error)
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
                List<GetTagView> result = null;
                if (linqQuery != null)
                {
                    result = DynamicLinqQuery(rawList, linqQuery.Filter, out error);
                }
                else
                {
                    var filteredList = base.FilterRawList(rawList, GeneralBodyGet, out error);
                    result = filteredList.Cast<GetTagView>().ToList();
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
        /// "Where(GetAllTagView => (GetAllTagView.TagID = 4))"
        /// </summary>
        /// <param name="list"></param>
        /// <param name="GeneralBodyGet"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<GetTagView> DynamicLinqQuery(List<GetTagView> list, string linqQuery, out RestExceptionError error)
        {
            try
            {
                error = null;

                IQueryable<GetTagView> queryableData = list.AsQueryable<GetTagView>();
                var externals = new Dictionary<string, object>();
                externals.Add("list", queryableData);
                string query = "list." + linqQuery;//Example: "list.Where(GetTagView => (GetTagView.TagID = 4))";
                var expression = System.Linq.Dynamic.DynamicExpression.Parse(typeof(IQueryable<GetTagView>), query, new[] { externals });
                var result = queryableData.Provider.CreateQuery<GetTagView>(expression);
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

        public List<GetTagView> OrderByAndTrim(List<GetTagView> list, GeneralBodyGet GeneralBodyGet, out RestExceptionError error)
        {
            try
            {
                error = null;
                if ((list == null) || (GeneralBodyGet == null))
                {
                    return list;
                }
                IQueryable<GetTagView> queryableData = list.AsQueryable<GetTagView>();                
                
                List<GetTagView> filteredList = null;
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

        public List<GetTagView> GetAll(out RestExceptionError error)
        {
            try
            {
                error = null;
                TagBsn bsn = new TagBsn(restConfig);
                List<TagInfo> dbItems = bsn.GetAll();
                List<GetTagView> result = new List<GetTagView>();
                foreach (TagInfo item in dbItems)
                {
                    GetTagView view = new GetTagView();
                    Cloner.CopyAllTo(typeof(TagInfo), item, typeof(GetTagView), view);
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
        public List<GetTagView> GetAllWithDBFilter(GeneralBodyGet generalBodyGet, out RestExceptionError error)
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
                TagBsn bsn = new TagBsn(restConfig);
                List<DataFilterExpressionDB> dbFilter = HelperRESTFilterToDB.FilterRestFilterToDBExpression(generalBodyGet.Filters);
                List<TagInfo> dbItems = bsn.GetAll(dbFilter);
                List<GetTagView> result = new List<GetTagView>();
                foreach (TagInfo item in dbItems)
                {
                    GetTagView view = new GetTagView();
                    Cloner.CopyAllTo(typeof(TagInfo), item, typeof(GetTagView), view);
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
