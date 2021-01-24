using Newtonsoft.Json;
using RESTLib.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace RESTLib.Core
{
    /// <summary>
    /// Base class used to create GenericMyTableRest
    /// </summary>
    public class GenericRESTBase
    {
        public void ParseError(string responseString, HttpResponseMessage response, out string error, out GeneralPostResponse generalPostResponse, out GeneralErrorResponseContainer generalErrorResponse)
        {
            generalErrorResponse = null;
            generalPostResponse = null;
            error = null;
            if (response == null)
            {
                error = "Catastrophic error. No HttpResponse from server.";
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(responseString))
                {
                    error = "Catastrophic error. No body from server.";
                    return;
                }

                if (response.IsSuccessStatusCode)
                {
                    generalPostResponse = JsonConvert.DeserializeObject<GeneralPostResponse>(responseString);
                }
                else
                {
                    try
                    {
                        generalErrorResponse = JsonConvert.DeserializeObject<GeneralErrorResponseContainer>(responseString);
                        string formatedError = "Error:{0} Internal:{1} Route:{2} TransactionID:{3}";
                        formatedError = string.Format(formatedError,
                            generalErrorResponse.ErrorResponse.Error,
                            generalErrorResponse.ErrorResponse.ErrorInternal,
                            generalErrorResponse.ErrorResponse.Route,
                            generalErrorResponse.ErrorResponse.TransactionID);
                        error = formatedError;
                    }
                    catch (Exception ex)
                    {
                        error = "Catastrophic 2 errors: Served crashed(1) and Client(2) can't deserialize json. Original json from server is:" + responseString;
                    }
                }
            }
        }
    }
}
