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
    
namespace MyApp.Rest.Controllers.CustomerDemographics
{
    public class RESTCustomerDemographicsDB : RESTBaseDB
    {
        private RESTConfig restConfig { get; set; }
        public RESTCustomerDemographicsDB(RESTConfig restConfig)
        {
            this.restConfig = restConfig;
        }

        public void TryDelete(DeleteCustomerDemographicsView viewToDelete, out RestExceptionError error)
        {
            error = null;
            CustomerDemographicsInfo dbViewToDelete = new CustomerDemographicsInfo();
            try
            {
                Cloner.CopyAllTo(typeof(DeleteCustomerDemographicsView), viewToDelete, typeof(CustomerDemographicsInfo), dbViewToDelete);
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error parsing data for (CustomerDemographics.TryDelete/Parsing)";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }

            try
            {
                CustomerDemographicsBsn bsn = new CustomerDemographicsBsn(restConfig);
                string dbError = null;
                bsn.Delete(dbViewToDelete, out dbError);
                if (dbError != null)
                {
                    error = new RestExceptionError();
                    error.InternalMessage = "Internal Error Save data for [CustomerDemographics.TryDelete]";
                    error.ExceptionMessage = dbError;
                    error.SourceError = RestExceptionError._SourceError.ServerSide;
                    error.StackTrace = "";
                }
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error Update data for [CustomerDemographics.TryDelete]";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }
        }

        public void TryUpdate(UpdateCustomerDemographicsView viewToUpdate, out RestExceptionError error)
        {
            error = null;
            CustomerDemographicsInfo dbViewToInclude = new CustomerDemographicsInfo();
            try
            {
                Cloner.CopyAllTo(typeof(UpdateCustomerDemographicsView), viewToUpdate, typeof(CustomerDemographicsInfo), dbViewToInclude);
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error parsing data for (CustomerDemographics.TryUpdate/Parsing)";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }

            try
            {
                CustomerDemographicsBsn bsn = new CustomerDemographicsBsn(restConfig);
                string dbError = null;
                bsn.UpdateOne(dbViewToInclude, out dbError);
                if (dbError != null)
                {
                    error = new RestExceptionError();
                    error.InternalMessage = "Internal Error Save data for [CustomerDemographics.TryUpdate]";
                    error.ExceptionMessage = dbError;
                    error.SourceError = RestExceptionError._SourceError.ServerSide;
                    error.StackTrace = "";
                }
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error Update data for [CustomerDemographics.TryUpdate]";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }
        }

        public void TryInclude(CreateCustomerDemographicsView viewToInclude, out RestExceptionError error)
        {
            error = null;
            CustomerDemographicsInfo dbViewToInclude = new CustomerDemographicsInfo();
            try
            {                
                Cloner.CopyAllTo(typeof(CreateCustomerDemographicsView), viewToInclude, typeof(CustomerDemographicsInfo), dbViewToInclude);                
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error parsing data for (CustomerDemographics.TryInclude/Parsing)";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }

            try
            {
                CustomerDemographicsBsn bsn = new CustomerDemographicsBsn(restConfig);
                string dbError = null;
                bsn.InsertOne(dbViewToInclude, out dbError);
                if (dbError != null)
                {
                    error = new RestExceptionError();
                    error.InternalMessage = "Internal Error Save data for [CustomerDemographics.TryInclude/Save]";
                    error.ExceptionMessage = dbError;
                    error.SourceError = RestExceptionError._SourceError.ServerSide;
                    error.StackTrace = "";
                }
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error Save data for [CustomerDemographics.TryInclude/Save]";
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
                error.InternalMessage = "No data from client to include [CustomerDemographics.TryParse]";
                error.ExceptionMessage = "No data";
                error.SourceError = RestExceptionError._SourceError.ClientSide;
                error.StackTrace = "RESTCustomerDemographicsDB.TryParse";
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
                error.InternalMessage = "Error parsing data from body to view for [CustomerDemographics.TryParse]";
                return default(T);
            }            
        }

        /// <summary>
        /// Filters a raw list based on GeneralBodyGet.Filters
        /// </summary>
        public List<GetCustomerDemographicsView> Filter(List<GetCustomerDemographicsView> rawList, GeneralBodyGet GeneralBodyGet, out RestExceptionError error)
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
                List<GetCustomerDemographicsView> result = null;
                if (linqQuery != null)
                {
                    result = DynamicLinqQuery(rawList, linqQuery.Filter, out error);
                }
                else
                {
                    var filteredList = base.FilterRawList(rawList, GeneralBodyGet, out error);
                    result = filteredList.Cast<GetCustomerDemographicsView>().ToList();
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
        /// "Where(GetAllCustomerDemographicsView => (GetAllCustomerDemographicsView.CustomerDemographicsID = 4))"
        /// </summary>
        /// <param name="list"></param>
        /// <param name="GeneralBodyGet"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<GetCustomerDemographicsView> DynamicLinqQuery(List<GetCustomerDemographicsView> list, string linqQuery, out RestExceptionError error)
        {
            try
            {
                error = null;

                IQueryable<GetCustomerDemographicsView> queryableData = list.AsQueryable<GetCustomerDemographicsView>();
                var externals = new Dictionary<string, object>();
                externals.Add("list", queryableData);
                string query = "list." + linqQuery;//Example: "list.Where(GetCustomerDemographicsView => (GetCustomerDemographicsView.CustomerDemographicsID = 4))";
                var expression = System.Linq.Dynamic.DynamicExpression.Parse(typeof(IQueryable<GetCustomerDemographicsView>), query, new[] { externals });
                var result = queryableData.Provider.CreateQuery<GetCustomerDemographicsView>(expression);
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

        public List<GetCustomerDemographicsView> OrderByAndTrim(List<GetCustomerDemographicsView> list, GeneralBodyGet GeneralBodyGet, out RestExceptionError error)
        {
            try
            {
                error = null;
                if ((list == null) || (GeneralBodyGet == null))
                {
                    return list;
                }
                IQueryable<GetCustomerDemographicsView> queryableData = list.AsQueryable<GetCustomerDemographicsView>();                
                
                List<GetCustomerDemographicsView> filteredList = null;
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

        public List<GetCustomerDemographicsView> GetAll(out RestExceptionError error)
        {
            try
            {
                error = null;
                CustomerDemographicsBsn bsn = new CustomerDemographicsBsn(restConfig);
                List<CustomerDemographicsInfo> dbItems = bsn.GetAll();
                List<GetCustomerDemographicsView> result = new List<GetCustomerDemographicsView>();
                foreach (CustomerDemographicsInfo item in dbItems)
                {
                    GetCustomerDemographicsView view = new GetCustomerDemographicsView();
                    Cloner.CopyAllTo(typeof(CustomerDemographicsInfo), item, typeof(GetCustomerDemographicsView), view);
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
        public List<GetCustomerDemographicsView> GetAllWithDBFilter(GeneralBodyGet generalBodyGet, out RestExceptionError error)
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
                CustomerDemographicsBsn bsn = new CustomerDemographicsBsn(restConfig);
                List<DataFilterExpressionDB> dbFilter = HelperRESTFilterToDB.FilterRestFilterToDBExpression(generalBodyGet.Filters);
                List<CustomerDemographicsInfo> dbItems = bsn.GetAll(dbFilter);
                List<GetCustomerDemographicsView> result = new List<GetCustomerDemographicsView>();
                foreach (CustomerDemographicsInfo item in dbItems)
                {
                    GetCustomerDemographicsView view = new GetCustomerDemographicsView();
                    Cloner.CopyAllTo(typeof(CustomerDemographicsInfo), item, typeof(GetCustomerDemographicsView), view);
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
