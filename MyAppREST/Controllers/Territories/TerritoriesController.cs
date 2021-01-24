using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MyAppRESTLib;
using RESTLib.Core;
using MyApp.TransferObjects.REST;

namespace MyApp.Rest.Controllers.Territories
{
    [ApiController]
    public class TerritoriesApiController : MyCustomController
    {

        [Route("Territories/GetAllX")]
        [HttpGet]
        public IActionResult GetAllX(GeneralBodyGet GeneralBodyGet)
        {
            string transactionID = Guid.NewGuid().ToString();
            string currentURL = this.Request.Method;
            HelperRestError helperRestError = new HelperRestError();
            HelperHTTPLog helperHTTPLog = new HelperHTTPLog(HelperHTTPLog.WhatToLog.All);
            try
            {
                RestExceptionError restExceptionError = null;
                RESTTerritoriesDB rESTTerritoriesDB = new RESTTerritoriesDB(base.RESTConfig);

                //1)Get all rows. Note: Create "select * from MyTable". DB filter must be mannually done
                List<GetTerritoriesView> rawResult = rESTTerritoriesDB.GetAllWithDBFilter(GeneralBodyGet, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Territories/GetAllX");
                    return errorResult;
                }

                GeneralGetResponse resultContainer = new GeneralGetResponse();
                resultContainer.Data = rawResult;
                resultContainer.ReportHeader.TotalItensAvailable = 1;
                resultContainer.ReportHeader.TotalItensRetrieved = 1;
                resultContainer.ReportHeader.TransactionID = transactionID;

                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;

                CreatedResult createdResult = new CreatedResult("Territories/GetAllX", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "Territories/GetAllX");
                return errorResult;
            }
        }

        [Route("Territories/GetAll")]
        [HttpGet]
        public IActionResult GetAll(GeneralBodyGet GeneralBodyGet)
        {
            string transactionID = Guid.NewGuid().ToString();
            string currentURL = this.Request.Method;
            HelperRestError helperRestError = new HelperRestError();
            HelperHTTPLog helperHTTPLog = new HelperHTTPLog(HelperHTTPLog.WhatToLog.All);
            try
            {
                RestExceptionError restExceptionError = null;
                RESTTerritoriesDB rESTTerritoriesDB = new RESTTerritoriesDB(base.RESTConfig);

                //1)Get all rows. Note: Create "select * from MyTable". DB filter must be mannually done
                List<GetTerritoriesView> rawResult = rESTTerritoriesDB.GetAll(out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Territories/GetAll");
                    return errorResult;
                }

                //2)Apply filters in result set.
                List<GetTerritoriesView> filteredResult = rESTTerritoriesDB.Filter(rawResult, GeneralBodyGet, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Territories/GetAll");
                    return errorResult;
                }

                //3)Cut data, apply ordering
                List<GetTerritoriesView> orderedResultAndTrimed = rESTTerritoriesDB.OrderByAndTrim(filteredResult, GeneralBodyGet, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Territories/GetAll");
                    return errorResult;
                }

                var finalResult = orderedResultAndTrimed;

                GeneralGetResponse resultContainer = new GeneralGetResponse();
                resultContainer.Data = finalResult;
                resultContainer.ReportHeader.TotalItensAvailable = rawResult.Count;
                resultContainer.ReportHeader.TotalItensRetrieved = finalResult.Count;
                resultContainer.ReportHeader.TransactionID = transactionID;

                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;

                CreatedResult createdResult = new CreatedResult("Territories/GetAll", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "Territories/GetAll");
                return errorResult;
            }
        }       

        //[Authorize]
        [Route("Territories/Update")]
        [HttpPost]
        public IActionResult Update(GeneralBodyPost GeneralBodyPost)
        {
            string transactionID = Guid.NewGuid().ToString();
            string currentURL = this.Request.Method;
            HelperRestError helperRestError = new HelperRestError();
            HelperHTTPLog helperHTTPLog = new HelperHTTPLog(HelperHTTPLog.WhatToLog.All);
            try
            {
                GeneralPostResponse resultContainer = new GeneralPostResponse();
                resultContainer.ReportPostHeader.TransactionID = transactionID;
                RestExceptionError restExceptionError = null;

                RESTTerritoriesDB rESTTerritoriesDB = new RESTTerritoriesDB(base.RESTConfig);
                UpdateTerritoriesView viewToInclude = rESTTerritoriesDB.TryParse<UpdateTerritoriesView>(GeneralBodyPost, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Territories/Insert");
                    return errorResult;
                }

                rESTTerritoriesDB.TryUpdate(viewToInclude, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Territories/GetAll");
                    return errorResult;
                }

                resultContainer.ReportPostHeader.Message = "Success";
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                CreatedResult createdResult = new CreatedResult("Territories/Update", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "Territories/Create");
                return errorResult;
            }
        }

        //[Authorize]
        [Route("Territories/Delete")]
        [HttpDelete]
        public IActionResult Delete(GeneralBodyPost GeneralBodyPost)
        {
            string transactionID = Guid.NewGuid().ToString();
            string currentURL = this.Request.Method;
            HelperRestError helperRestError = new HelperRestError();
            HelperHTTPLog helperHTTPLog = new HelperHTTPLog(HelperHTTPLog.WhatToLog.All);
            try
            {
                GeneralPostResponse resultContainer = new GeneralPostResponse();
                resultContainer.ReportPostHeader.TransactionID = transactionID;
                RestExceptionError restExceptionError = null;

                RESTTerritoriesDB rESTTerritoriesDB = new RESTTerritoriesDB(base.RESTConfig);
                DeleteTerritoriesView viewToDelete = rESTTerritoriesDB.TryParse<DeleteTerritoriesView>(GeneralBodyPost, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Territories/Insert");
                    return errorResult;
                }

                rESTTerritoriesDB.TryDelete(viewToDelete, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Territories/GetAll");
                    return errorResult;
                }

                resultContainer.ReportPostHeader.Message = "Success";
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                CreatedResult createdResult = new CreatedResult("Territories/Delete", httpResponseMessage);
                createdResult.Value = resultContainer;
                createdResult.StatusCode = 200;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "Territories/Create");
                return errorResult;
            }
        }

        //[Authorize]
        [Route("Territories/Create")]
        [HttpPost]
        public IActionResult Create(GeneralBodyPost GeneralBodyPost)
        {
            string transactionID = Guid.NewGuid().ToString();
            string currentURL = this.Request.Method;
            HelperRestError helperRestError = new HelperRestError();
            HelperHTTPLog helperHTTPLog = new HelperHTTPLog(HelperHTTPLog.WhatToLog.All);
            try
            {
                GeneralPostResponse resultContainer = new GeneralPostResponse();
                resultContainer.ReportPostHeader.TransactionID = transactionID;
                RestExceptionError restExceptionError = null;
                
                RESTTerritoriesDB rESTTerritoriesDB = new RESTTerritoriesDB(base.RESTConfig);
                CreateTerritoriesView viewToInclude = rESTTerritoriesDB.TryParse<CreateTerritoriesView>(GeneralBodyPost, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Territories/Insert");
                    return errorResult;
                }

                rESTTerritoriesDB.TryInclude(viewToInclude, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Territories/GetAll");
                    return errorResult;
                }

                resultContainer.ReportPostHeader.Message = "Success";
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.Created;
                CreatedResult createdResult = new CreatedResult("Territories/Create", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "Territories/Create");
                return errorResult;
            }
        }        
    }
}

