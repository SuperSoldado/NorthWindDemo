using MyAppGlobalLib;
using MyAppGlobalLib.Helpers;
using Newtonsoft.Json;
using RESTLib.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MyApp.TransferObjects.REST
{
    /// <summary>
    /// Provides REST middleware
    /// </summary>
    public class SuppliersGenericREST : GenericRESTBase
    {
        private WPFConfig wpfConfig { get; set; }
        public SuppliersGenericREST(WPFConfig wpfConfig)
        {
            this.wpfConfig = wpfConfig;
        }

        public void Delete(DeleteSuppliersView deleteData, out string error)
        {
            error = null;
            try
            {
                Uri basePath = new Uri(wpfConfig.RESTBasePath);
                Uri requestUri = new Uri(basePath, "Suppliers/Delete");
                GeneralBodyPost postRequest = new GeneralBodyPost();
                postRequest.Data = deleteData;
                string requestBody = JsonConvert.SerializeObject(postRequest);
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = requestUri,
                        Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
                    };
                    var response = client.SendAsync(request).Result;

                    var responseContent = response.Content;
                    string responseString = responseContent.ReadAsStringAsync().Result;
                    GeneralPostResponse generalPostResponse = null;
                    GeneralErrorResponseContainer generalErrorResponse = null;
                    base.ParseError(responseString, response, out error, out generalPostResponse, out generalErrorResponse);
                }
            }
            catch (Exception ex)
            {
                error = "Error trying post data. Request crash: " + ex.Message;
            }
        }

        public void Insert(CreateSuppliersView insertData, out string error)
        {
            error = null;
            try
            {
                Uri basePath = new Uri(wpfConfig.RESTBasePath);
                Uri requestUri = new Uri(basePath, "Suppliers/Create");
                GeneralBodyPost postRequest = new GeneralBodyPost();
                postRequest.Data = insertData;
                string requestBody = JsonConvert.SerializeObject(postRequest);
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = requestUri,
                        Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
                    };
                    var response = client.SendAsync(request).Result;

                    var responseContent = response.Content;
                    string responseString = responseContent.ReadAsStringAsync().Result;
                    GeneralPostResponse generalPostResponse = null;
                    GeneralErrorResponseContainer generalErrorResponse = null;
                    base.ParseError(responseString, response, out error, out generalPostResponse, out generalErrorResponse);
                }
            }
            catch (Exception ex)
            {
                error = "Error trying post data. Request crash: " + ex.Message;
            }
        }

        public void Update(UpdateSuppliersView updateData, out string error)
        {
            error = null;
            try
            {
                Uri basePath = new Uri(wpfConfig.RESTBasePath);
                Uri requestUri = new Uri(basePath, "Suppliers/Update");
                GeneralBodyPost postRequest = new GeneralBodyPost();
                postRequest.Data = updateData;
                string requestBody = JsonConvert.SerializeObject(postRequest);
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = requestUri,
                        Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
                    };
                    var response = client.SendAsync(request).Result;

                    var responseContent = response.Content;
                    string responseString = responseContent.ReadAsStringAsync().Result;
                    GeneralPostResponse generalPostResponse = null;
                    GeneralErrorResponseContainer generalErrorResponse = null;
                    base.ParseError(responseString, response, out error, out generalPostResponse, out generalErrorResponse);
                }
            }
            catch (Exception ex)
            {
                error = "Error trying post data. Request crash: " + ex.Message;
            }
        }

        public List<T> GetByPK<T>(int SupplierID, out string error)
        {
            List<DataFilterExpressionREST> dataFilterExpressionRESTList = new List<DataFilterExpressionREST>();
            DataFilterExpressionREST dataFilterExpressionREST = null;
            dataFilterExpressionREST = new DataFilterExpressionREST();
            dataFilterExpressionREST.FieldName= "SupplierID";
            dataFilterExpressionREST.FilterType= DataFilterExpressionREST._FilterType.Equal;
            dataFilterExpressionREST.Filter = SupplierID.ToString();
            dataFilterExpressionRESTList.Add(dataFilterExpressionREST);

            return GetAllX<T>(dataFilterExpressionRESTList, out error);
        }

        public List<T> GetAllX<T>(List<DataFilterExpressionREST> dataFilterExpressionRESTs, out string error)
        {
            error = null;
            try
            {
                Uri basePath = new Uri(wpfConfig.RESTBasePath);
                Uri requestUri = new Uri(basePath, "Suppliers/GetAllX");
                GeneralBodyGet getRequest = new GeneralBodyGet();
                getRequest.NumberOfItens = 0;
                getRequest.ItensToSkip = 0;  
                getRequest.Filters = dataFilterExpressionRESTs;
                string requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(getRequest);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = requestUri,
                        Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
                    };
                    var response = client.SendAsync(request).Result;

                    var responseContent = response.Content;
                    string responseString = responseContent.ReadAsStringAsync().Result;
                    GeneralGetResponse generalGetResponse = JsonConvert.DeserializeObject<GeneralGetResponse>(responseString);
                    if (response.IsSuccessStatusCode)
                    {
                        Newtonsoft.Json.Linq.JArray jArray = (Newtonsoft.Json.Linq.JArray)generalGetResponse.Data;
                        List<GetSuppliersView> dataRetrieved = jArray.ToObject<List<GetSuppliersView>>();
                        List<T> result = new List<T>();
                        foreach (GetSuppliersView item in dataRetrieved)
                        {
                            T modelNotifiedForSuppliersNew = (T)Activator.CreateInstance(typeof(T));
                            Cloner.CopyAllTo(typeof(GetSuppliersView), item, typeof(T), modelNotifiedForSuppliersNew);
                            result.Add(modelNotifiedForSuppliersNew);
                        }
                        return result;
                    }
                    else
                    {
                        error = "Server side refused this request and returned status {0}. Reason {1}. TransactionID:{0}";
                        error = string.Format(error, response.StatusCode, generalGetResponse.ReportHeader.MessageFromServer, generalGetResponse.ReportHeader.TransactionID);
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                error = "Error trying during data request. Request crash: " + ex.Message;
            }
            return null;
        }

        public List<T> GetAll<T>(int numberOfItens, int itensToSkip, out string error)
        {
            error = null;
            try
            {
                Uri basePath = new Uri(wpfConfig.RESTBasePath);
                Uri requestUri = new Uri(basePath, "Suppliers/GetAll");
                GeneralBodyGet getRequest = new GeneralBodyGet();
                getRequest.NumberOfItens = numberOfItens;
                getRequest.ItensToSkip = itensToSkip;
                string requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(getRequest);
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = requestUri,
                        Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
                    };
                    var response = client.SendAsync(request).Result;

                    var responseContent = response.Content;
                    string responseString = responseContent.ReadAsStringAsync().Result;
                    GeneralGetResponse generalGetResponse = JsonConvert.DeserializeObject<GeneralGetResponse>(responseString);
                    if (response.IsSuccessStatusCode)
                    {
                        Newtonsoft.Json.Linq.JArray jArray = (Newtonsoft.Json.Linq.JArray)generalGetResponse.Data;
                        List<GetSuppliersView> dataRetrieved = jArray.ToObject<List<GetSuppliersView>>();
                        List<T> result = new List<T>();
                        foreach (GetSuppliersView item in dataRetrieved)
                        {
                            T modelNotifiedForSuppliersNew = (T)Activator.CreateInstance(typeof(T));
                            Cloner.CopyAllTo(typeof(GetSuppliersView), item, typeof(T), modelNotifiedForSuppliersNew);
                            result.Add(modelNotifiedForSuppliersNew);
                        }
                        return result;
                    }
                    else
                    {
                        error = "Server side refused this request and returned status {0}. Reason {1}. TransactionID:{0}";
                        error = string.Format(error, response.StatusCode, generalGetResponse.ReportHeader.MessageFromServer, generalGetResponse.ReportHeader.TransactionID);
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                error = "Error trying during data request. Request crash: " + ex.Message;
            }
            return null;
        }
    }
}
