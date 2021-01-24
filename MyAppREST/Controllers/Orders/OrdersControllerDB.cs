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
    
namespace MyApp.Rest.Controllers.Orders
{
    public class RESTOrdersDB : RESTBaseDB
    {
        private RESTConfig restConfig { get; set; }
        public RESTOrdersDB(RESTConfig restConfig)
        {
            this.restConfig = restConfig;
        }

        public void TryDelete(DeleteOrdersView viewToDelete, out RestExceptionError error)
        {
            error = null;
            OrdersInfo dbViewToDelete = new OrdersInfo();
            try
            {
                Cloner.CopyAllTo(typeof(DeleteOrdersView), viewToDelete, typeof(OrdersInfo), dbViewToDelete);
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error parsing data for (Orders.TryDelete/Parsing)";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }

            try
            {
                OrdersBsn bsn = new OrdersBsn(restConfig);
                string dbError = null;
                bsn.Delete(dbViewToDelete, out dbError);
                if (dbError != null)
                {
                    error = new RestExceptionError();
                    error.InternalMessage = "Internal Error Save data for [Orders.TryDelete]";
                    error.ExceptionMessage = dbError;
                    error.SourceError = RestExceptionError._SourceError.ServerSide;
                    error.StackTrace = "";
                }
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error Update data for [Orders.TryDelete]";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }
        }

        public void TryUpdate(UpdateOrdersView viewToUpdate, out RestExceptionError error)
        {
            error = null;
            OrdersInfo dbViewToInclude = new OrdersInfo();
            try
            {
                Cloner.CopyAllTo(typeof(UpdateOrdersView), viewToUpdate, typeof(OrdersInfo), dbViewToInclude);
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error parsing data for (Orders.TryUpdate/Parsing)";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }

            try
            {
                OrdersBsn bsn = new OrdersBsn(restConfig);
                string dbError = null;
                bsn.UpdateOne(dbViewToInclude, out dbError);
                if (dbError != null)
                {
                    error = new RestExceptionError();
                    error.InternalMessage = "Internal Error Save data for [Orders.TryUpdate]";
                    error.ExceptionMessage = dbError;
                    error.SourceError = RestExceptionError._SourceError.ServerSide;
                    error.StackTrace = "";
                }
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error Update data for [Orders.TryUpdate]";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }
        }

        public void TryInclude(CreateOrdersView viewToInclude, out RestExceptionError error)
        {
            error = null;
            OrdersInfo dbViewToInclude = new OrdersInfo();
            try
            {                
                Cloner.CopyAllTo(typeof(CreateOrdersView), viewToInclude, typeof(OrdersInfo), dbViewToInclude);                
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error parsing data for (Orders.TryInclude/Parsing)";
                error.ExceptionMessage = ex.Message;
                error.SourceError = RestExceptionError._SourceError.ServerSide;
                error.StackTrace = ex.StackTrace;
            }

            try
            {
                OrdersBsn bsn = new OrdersBsn(restConfig);
                string dbError = null;
                bsn.InsertOne(dbViewToInclude, out dbError);
                if (dbError != null)
                {
                    error = new RestExceptionError();
                    error.InternalMessage = "Internal Error Save data for [Orders.TryInclude/Save]";
                    error.ExceptionMessage = dbError;
                    error.SourceError = RestExceptionError._SourceError.ServerSide;
                    error.StackTrace = "";
                }
            }
            catch (Exception ex)
            {
                error = new RestExceptionError();
                error.InternalMessage = "Internal Error Save data for [Orders.TryInclude/Save]";
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
                error.InternalMessage = "No data from client to include [Orders.TryParse]";
                error.ExceptionMessage = "No data";
                error.SourceError = RestExceptionError._SourceError.ClientSide;
                error.StackTrace = "RESTOrdersDB.TryParse";
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
                error.InternalMessage = "Error parsing data from body to view for [Orders.TryParse]";
                return default(T);
            }            
        }

        /// <summary>
        /// Filters a raw list based on GeneralBodyGet.Filters
        /// </summary>
        public List<GetOrdersView> Filter(List<GetOrdersView> rawList, GeneralBodyGet GeneralBodyGet, out RestExceptionError error)
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
                List<GetOrdersView> result = null;
                if (linqQuery != null)
                {
                    result = DynamicLinqQuery(rawList, linqQuery.Filter, out error);
                }
                else
                {
                    var filteredList = base.FilterRawList(rawList, GeneralBodyGet, out error);
                    result = filteredList.Cast<GetOrdersView>().ToList();
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
        /// "Where(GetAllOrdersView => (GetAllOrdersView.OrdersID = 4))"
        /// </summary>
        /// <param name="list"></param>
        /// <param name="GeneralBodyGet"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<GetOrdersView> DynamicLinqQuery(List<GetOrdersView> list, string linqQuery, out RestExceptionError error)
        {
            try
            {
                error = null;

                IQueryable<GetOrdersView> queryableData = list.AsQueryable<GetOrdersView>();
                var externals = new Dictionary<string, object>();
                externals.Add("list", queryableData);
                string query = "list." + linqQuery;//Example: "list.Where(GetOrdersView => (GetOrdersView.OrdersID = 4))";
                var expression = System.Linq.Dynamic.DynamicExpression.Parse(typeof(IQueryable<GetOrdersView>), query, new[] { externals });
                var result = queryableData.Provider.CreateQuery<GetOrdersView>(expression);
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

        public List<GetOrdersView> OrderByAndTrim(List<GetOrdersView> list, GeneralBodyGet GeneralBodyGet, out RestExceptionError error)
        {
            try
            {
                error = null;
                if ((list == null) || (GeneralBodyGet == null))
                {
                    return list;
                }
                IQueryable<GetOrdersView> queryableData = list.AsQueryable<GetOrdersView>();                
                
                List<GetOrdersView> filteredList = null;
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

        public List<GetOrdersView> GetAll(out RestExceptionError error)
        {
            try
            {
                error = null;
                OrdersBsn bsn = new OrdersBsn(restConfig);
                List<OrdersInfo> dbItems = bsn.GetAll();
                List<GetOrdersView> result = new List<GetOrdersView>();
                foreach (OrdersInfo item in dbItems)
                {
                    GetOrdersView view = new GetOrdersView();
                    Cloner.CopyAllTo(typeof(OrdersInfo), item, typeof(GetOrdersView), view);
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
        public List<GetOrdersView> GetAllWithDBFilter(GeneralBodyGet generalBodyGet, out RestExceptionError error)
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
                OrdersBsn bsn = new OrdersBsn(restConfig);
                List<DataFilterExpressionDB> dbFilter = HelperRESTFilterToDB.FilterRestFilterToDBExpression(generalBodyGet.Filters);
                List<OrdersInfo> dbItems = bsn.GetAll(dbFilter);
                List<GetOrdersView> result = new List<GetOrdersView>();
                foreach (OrdersInfo item in dbItems)
                {
                    GetOrdersView view = new GetOrdersView();
                    Cloner.CopyAllTo(typeof(OrdersInfo), item, typeof(GetOrdersView), view);
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
