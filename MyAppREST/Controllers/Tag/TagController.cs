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

namespace MyApp.Rest.Controllers.Tag
{
    [ApiController]
    public class TagApiController : MyCustomController
    {

        [Route("Tag/GetAllX")]
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
                RESTTagDB rESTTagDB = new RESTTagDB(base.RESTConfig);

                //1)Get all rows. Note: Create "select * from MyTable". DB filter must be mannually done
                List<GetTagView> rawResult = rESTTagDB.GetAllWithDBFilter(GeneralBodyGet, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Tag/GetAllX");
                    return errorResult;
                }

                GeneralGetResponse resultContainer = new GeneralGetResponse();
                resultContainer.Data = rawResult;
                resultContainer.ReportHeader.TotalItensAvailable = 1;
                resultContainer.ReportHeader.TotalItensRetrieved = 1;
                resultContainer.ReportHeader.TransactionID = transactionID;

                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;

                CreatedResult createdResult = new CreatedResult("Tag/GetAllX", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "Tag/GetAllX");
                return errorResult;
            }
        }

        [Route("Tag/GetAll")]
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
                RESTTagDB rESTTagDB = new RESTTagDB(base.RESTConfig);

                //1)Get all rows. Note: Create "select * from MyTable". DB filter must be mannually done
                List<GetTagView> rawResult = rESTTagDB.GetAll(out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Tag/GetAll");
                    return errorResult;
                }

                //2)Apply filters in result set.
                List<GetTagView> filteredResult = rESTTagDB.Filter(rawResult, GeneralBodyGet, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Tag/GetAll");
                    return errorResult;
                }

                //3)Cut data, apply ordering
                List<GetTagView> orderedResultAndTrimed = rESTTagDB.OrderByAndTrim(filteredResult, GeneralBodyGet, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Tag/GetAll");
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

                CreatedResult createdResult = new CreatedResult("Tag/GetAll", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "Tag/GetAll");
                return errorResult;
            }
        }       

        //[Authorize]
        [Route("Tag/Update")]
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

                RESTTagDB rESTTagDB = new RESTTagDB(base.RESTConfig);
                UpdateTagView viewToInclude = rESTTagDB.TryParse<UpdateTagView>(GeneralBodyPost, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Tag/Insert");
                    return errorResult;
                }

                rESTTagDB.TryUpdate(viewToInclude, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Tag/GetAll");
                    return errorResult;
                }

                resultContainer.ReportPostHeader.Message = "Success";
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                CreatedResult createdResult = new CreatedResult("Tag/Update", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "Tag/Create");
                return errorResult;
            }
        }

        //[Authorize]
        [Route("Tag/Delete")]
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

                RESTTagDB rESTTagDB = new RESTTagDB(base.RESTConfig);
                DeleteTagView viewToDelete = rESTTagDB.TryParse<DeleteTagView>(GeneralBodyPost, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Tag/Insert");
                    return errorResult;
                }

                rESTTagDB.TryDelete(viewToDelete, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Tag/GetAll");
                    return errorResult;
                }

                resultContainer.ReportPostHeader.Message = "Success";
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                CreatedResult createdResult = new CreatedResult("Tag/Delete", httpResponseMessage);
                createdResult.Value = resultContainer;
                createdResult.StatusCode = 200;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "Tag/Create");
                return errorResult;
            }
        }

        //[Authorize]
        [Route("Tag/Create")]
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
                
                RESTTagDB rESTTagDB = new RESTTagDB(base.RESTConfig);
                CreateTagView viewToInclude = rESTTagDB.TryParse<CreateTagView>(GeneralBodyPost, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Tag/Insert");
                    return errorResult;
                }

                rESTTagDB.TryInclude(viewToInclude, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Tag/GetAll");
                    return errorResult;
                }

                resultContainer.ReportPostHeader.Message = "Success";
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.Created;
                CreatedResult createdResult = new CreatedResult("Tag/Create", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "Tag/Create");
                return errorResult;
            }
        }        
    }
}

