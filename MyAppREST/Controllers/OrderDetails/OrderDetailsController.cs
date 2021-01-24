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

namespace MyApp.Rest.Controllers.OrderDetails
{
    [ApiController]
    public class OrderDetailsApiController : MyCustomController
    {

        [Route("OrderDetails/GetAllX")]
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
                RESTOrderDetailsDB rESTOrderDetailsDB = new RESTOrderDetailsDB(base.RESTConfig);

                //1)Get all rows. Note: Create "select * from MyTable". DB filter must be mannually done
                List<GetOrderDetailsView> rawResult = rESTOrderDetailsDB.GetAllWithDBFilter(GeneralBodyGet, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "OrderDetails/GetAllX");
                    return errorResult;
                }

                GeneralGetResponse resultContainer = new GeneralGetResponse();
                resultContainer.Data = rawResult;
                resultContainer.ReportHeader.TotalItensAvailable = 1;
                resultContainer.ReportHeader.TotalItensRetrieved = 1;
                resultContainer.ReportHeader.TransactionID = transactionID;

                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;

                CreatedResult createdResult = new CreatedResult("OrderDetails/GetAllX", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "OrderDetails/GetAllX");
                return errorResult;
            }
        }

        [Route("OrderDetails/GetAll")]
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
                RESTOrderDetailsDB rESTOrderDetailsDB = new RESTOrderDetailsDB(base.RESTConfig);

                //1)Get all rows. Note: Create "select * from MyTable". DB filter must be mannually done
                List<GetOrderDetailsView> rawResult = rESTOrderDetailsDB.GetAll(out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "OrderDetails/GetAll");
                    return errorResult;
                }

                //2)Apply filters in result set.
                List<GetOrderDetailsView> filteredResult = rESTOrderDetailsDB.Filter(rawResult, GeneralBodyGet, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "OrderDetails/GetAll");
                    return errorResult;
                }

                //3)Cut data, apply ordering
                List<GetOrderDetailsView> orderedResultAndTrimed = rESTOrderDetailsDB.OrderByAndTrim(filteredResult, GeneralBodyGet, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "OrderDetails/GetAll");
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

                CreatedResult createdResult = new CreatedResult("OrderDetails/GetAll", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "OrderDetails/GetAll");
                return errorResult;
            }
        }       

        //[Authorize]
        [Route("OrderDetails/Update")]
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

                RESTOrderDetailsDB rESTOrderDetailsDB = new RESTOrderDetailsDB(base.RESTConfig);
                UpdateOrderDetailsView viewToInclude = rESTOrderDetailsDB.TryParse<UpdateOrderDetailsView>(GeneralBodyPost, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "OrderDetails/Insert");
                    return errorResult;
                }

                rESTOrderDetailsDB.TryUpdate(viewToInclude, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "OrderDetails/GetAll");
                    return errorResult;
                }

                resultContainer.ReportPostHeader.Message = "Success";
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                CreatedResult createdResult = new CreatedResult("OrderDetails/Update", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "OrderDetails/Create");
                return errorResult;
            }
        }

        //[Authorize]
        [Route("OrderDetails/Delete")]
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

                RESTOrderDetailsDB rESTOrderDetailsDB = new RESTOrderDetailsDB(base.RESTConfig);
                DeleteOrderDetailsView viewToDelete = rESTOrderDetailsDB.TryParse<DeleteOrderDetailsView>(GeneralBodyPost, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "OrderDetails/Insert");
                    return errorResult;
                }

                rESTOrderDetailsDB.TryDelete(viewToDelete, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "OrderDetails/GetAll");
                    return errorResult;
                }

                resultContainer.ReportPostHeader.Message = "Success";
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                CreatedResult createdResult = new CreatedResult("OrderDetails/Delete", httpResponseMessage);
                createdResult.Value = resultContainer;
                createdResult.StatusCode = 200;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "OrderDetails/Create");
                return errorResult;
            }
        }

        //[Authorize]
        [Route("OrderDetails/Create")]
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
                
                RESTOrderDetailsDB rESTOrderDetailsDB = new RESTOrderDetailsDB(base.RESTConfig);
                CreateOrderDetailsView viewToInclude = rESTOrderDetailsDB.TryParse<CreateOrderDetailsView>(GeneralBodyPost, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "OrderDetails/Insert");
                    return errorResult;
                }

                rESTOrderDetailsDB.TryInclude(viewToInclude, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "OrderDetails/GetAll");
                    return errorResult;
                }

                resultContainer.ReportPostHeader.Message = "Success";
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.Created;
                CreatedResult createdResult = new CreatedResult("OrderDetails/Create", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "OrderDetails/Create");
                return errorResult;
            }
        }        
    }
}

