using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace RESTLib.Core
{
    public class HelperRestError 
    {
        public CreatedResult GetError500_InternalServerError(string transactionID, string errorMessage, string errorInternal, string route)
        {
            /*General_500ErrorResponse general_500ErrorResponse = new General_500ErrorResponse();
            general_500ErrorResponse.TransactionID = transactionID;
            general_500ErrorResponse.Error = errorMessage;
            general_500ErrorResponse.Route = route;
            general_500ErrorResponse.ErrorInternal = null;
            
            GeneralErrorResponseContainer generalErrorResponseContainer = new GeneralErrorResponseContainer();
            generalErrorResponseContainer.Warning = System.Net.HttpStatusCode.InternalServerError.ToString();
            generalErrorResponseContainer.ErrorResponse = general_500ErrorResponse;
            CreatedResult createdResult = new CreatedResult(route, null);
            createdResult.StatusCode = 500;
            createdResult.Value = generalErrorResponseContainer;
            return createdResult;*/

            GeneralErrorResponse general_500ErrorResponse = new GeneralErrorResponse();
            general_500ErrorResponse.TransactionID = transactionID;
            general_500ErrorResponse.Error = errorMessage;
            general_500ErrorResponse.Route = route;
            general_500ErrorResponse.ErrorInternal = errorInternal;
            GeneralErrorResponseContainer generalErrorResponseContainer = new GeneralErrorResponseContainer();
            generalErrorResponseContainer.Warning = System.Net.HttpStatusCode.PreconditionFailed.ToString();
            generalErrorResponseContainer.ErrorResponse = general_500ErrorResponse;
            CreatedResult createdResult = new CreatedResult(route, null);
            createdResult.StatusCode = 412;
            createdResult.Value = generalErrorResponseContainer;
            return createdResult;
        }

        public CreatedResult GetError412_PreConditionFailed(string transactionID, string errorMessage, string errorInternal,string route)
        {
            GeneralErrorResponse general_412ErrorResponse = new GeneralErrorResponse();
            general_412ErrorResponse.TransactionID = transactionID;
            general_412ErrorResponse.Error = errorMessage;
            general_412ErrorResponse.Route = route;
            general_412ErrorResponse.ErrorInternal = errorInternal;
            GeneralErrorResponseContainer generalErrorResponseContainer = new GeneralErrorResponseContainer();
            generalErrorResponseContainer.Warning = System.Net.HttpStatusCode.PreconditionFailed.ToString();
            generalErrorResponseContainer.ErrorResponse = general_412ErrorResponse;
            CreatedResult createdResult = new CreatedResult(route, null);
            createdResult.StatusCode = 412;
            createdResult.Value = generalErrorResponseContainer;
            return createdResult;
        }
    }
}
