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

namespace MyApp.Rest.Controllers.CustomerCustomerDemo
{
    [ApiController]
    public class CustomerCustomerDemoApiController : MyCustomController
    {

        [Route("CustomerCustomerDemo/GetAllX")]
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
                RESTCustomerCustomerDemoDB rESTCustomerCustomerDemoDB = new RESTCustomerCustomerDemoDB(base.RESTConfig);

                //1)Get all rows. Note: Create "select * from MyTable". DB filter must be mannually done
                List<GetCustomerCustomerDemoView> rawResult = rESTCustomerCustomerDemoDB.GetAllWithDBFilter(GeneralBodyGet, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "CustomerCustomerDemo/GetAllX");
                    return errorResult;
                }

                GeneralGetResponse resultContainer = new GeneralGetResponse();
                resultContainer.Data = rawResult;
                resultContainer.ReportHeader.TotalItensAvailable = 1;
                resultContainer.ReportHeader.TotalItensRetrieved = 1;
                resultContainer.ReportHeader.TransactionID = transactionID;

                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;

                CreatedResult createdResult = new CreatedResult("CustomerCustomerDemo/GetAllX", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "CustomerCustomerDemo/GetAllX");
                return errorResult;
            }
        }

        [Route("CustomerCustomerDemo/GetAll")]
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
                RESTCustomerCustomerDemoDB rESTCustomerCustomerDemoDB = new RESTCustomerCustomerDemoDB(base.RESTConfig);

                //1)Get all rows. Note: Create "select * from MyTable". DB filter must be mannually done
                List<GetCustomerCustomerDemoView> rawResult = rESTCustomerCustomerDemoDB.GetAll(out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "CustomerCustomerDemo/GetAll");
                    return errorResult;
                }

                //2)Apply filters in result set.
                List<GetCustomerCustomerDemoView> filteredResult = rESTCustomerCustomerDemoDB.Filter(rawResult, GeneralBodyGet, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "CustomerCustomerDemo/GetAll");
                    return errorResult;
                }

                //3)Cut data, apply ordering
                List<GetCustomerCustomerDemoView> orderedResultAndTrimed = rESTCustomerCustomerDemoDB.OrderByAndTrim(filteredResult, GeneralBodyGet, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "CustomerCustomerDemo/GetAll");
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

                CreatedResult createdResult = new CreatedResult("CustomerCustomerDemo/GetAll", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "CustomerCustomerDemo/GetAll");
                return errorResult;
            }
        }       

        //[Authorize]
        [Route("CustomerCustomerDemo/Update")]
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

                RESTCustomerCustomerDemoDB rESTCustomerCustomerDemoDB = new RESTCustomerCustomerDemoDB(base.RESTConfig);
                UpdateCustomerCustomerDemoView viewToInclude = rESTCustomerCustomerDemoDB.TryParse<UpdateCustomerCustomerDemoView>(GeneralBodyPost, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "CustomerCustomerDemo/Insert");
                    return errorResult;
                }

                rESTCustomerCustomerDemoDB.TryUpdate(viewToInclude, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "CustomerCustomerDemo/GetAll");
                    return errorResult;
                }

                resultContainer.ReportPostHeader.Message = "Success";
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                CreatedResult createdResult = new CreatedResult("CustomerCustomerDemo/Update", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "CustomerCustomerDemo/Create");
                return errorResult;
            }
        }

        //[Authorize]
        [Route("CustomerCustomerDemo/Delete")]
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

                RESTCustomerCustomerDemoDB rESTCustomerCustomerDemoDB = new RESTCustomerCustomerDemoDB(base.RESTConfig);
                DeleteCustomerCustomerDemoView viewToDelete = rESTCustomerCustomerDemoDB.TryParse<DeleteCustomerCustomerDemoView>(GeneralBodyPost, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "CustomerCustomerDemo/Insert");
                    return errorResult;
                }

                rESTCustomerCustomerDemoDB.TryDelete(viewToDelete, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "CustomerCustomerDemo/GetAll");
                    return errorResult;
                }

                resultContainer.ReportPostHeader.Message = "Success";
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                CreatedResult createdResult = new CreatedResult("CustomerCustomerDemo/Delete", httpResponseMessage);
                createdResult.Value = resultContainer;
                createdResult.StatusCode = 200;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "CustomerCustomerDemo/Create");
                return errorResult;
            }
        }

        //[Authorize]
        [Route("CustomerCustomerDemo/Create")]
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
                
                RESTCustomerCustomerDemoDB rESTCustomerCustomerDemoDB = new RESTCustomerCustomerDemoDB(base.RESTConfig);
                CreateCustomerCustomerDemoView viewToInclude = rESTCustomerCustomerDemoDB.TryParse<CreateCustomerCustomerDemoView>(GeneralBodyPost, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "CustomerCustomerDemo/Insert");
                    return errorResult;
                }

                rESTCustomerCustomerDemoDB.TryInclude(viewToInclude, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "CustomerCustomerDemo/GetAll");
                    return errorResult;
                }

                resultContainer.ReportPostHeader.Message = "Success";
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.Created;
                CreatedResult createdResult = new CreatedResult("CustomerCustomerDemo/Create", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "CustomerCustomerDemo/Create");
                return errorResult;
            }
        }        
    }
}

