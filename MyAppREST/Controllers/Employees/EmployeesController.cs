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

namespace MyApp.Rest.Controllers.Employees
{
    [ApiController]
    public class EmployeesApiController : MyCustomController
    {

        [Route("Employees/GetAllX")]
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
                RESTEmployeesDB rESTEmployeesDB = new RESTEmployeesDB(base.RESTConfig);

                //1)Get all rows. Note: Create "select * from MyTable". DB filter must be mannually done
                List<GetEmployeesView> rawResult = rESTEmployeesDB.GetAllWithDBFilter(GeneralBodyGet, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Employees/GetAllX");
                    return errorResult;
                }

                GeneralGetResponse resultContainer = new GeneralGetResponse();
                resultContainer.Data = rawResult;
                resultContainer.ReportHeader.TotalItensAvailable = 1;
                resultContainer.ReportHeader.TotalItensRetrieved = 1;
                resultContainer.ReportHeader.TransactionID = transactionID;

                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;

                CreatedResult createdResult = new CreatedResult("Employees/GetAllX", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "Employees/GetAllX");
                return errorResult;
            }
        }

        [Route("Employees/GetAll")]
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
                RESTEmployeesDB rESTEmployeesDB = new RESTEmployeesDB(base.RESTConfig);

                //1)Get all rows. Note: Create "select * from MyTable". DB filter must be mannually done
                List<GetEmployeesView> rawResult = rESTEmployeesDB.GetAll(out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Employees/GetAll");
                    return errorResult;
                }

                //2)Apply filters in result set.
                List<GetEmployeesView> filteredResult = rESTEmployeesDB.Filter(rawResult, GeneralBodyGet, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Employees/GetAll");
                    return errorResult;
                }

                //3)Cut data, apply ordering
                List<GetEmployeesView> orderedResultAndTrimed = rESTEmployeesDB.OrderByAndTrim(filteredResult, GeneralBodyGet, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Employees/GetAll");
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

                CreatedResult createdResult = new CreatedResult("Employees/GetAll", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "Employees/GetAll");
                return errorResult;
            }
        }       

        //[Authorize]
        [Route("Employees/Update")]
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

                RESTEmployeesDB rESTEmployeesDB = new RESTEmployeesDB(base.RESTConfig);
                UpdateEmployeesView viewToInclude = rESTEmployeesDB.TryParse<UpdateEmployeesView>(GeneralBodyPost, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Employees/Insert");
                    return errorResult;
                }

                rESTEmployeesDB.TryUpdate(viewToInclude, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Employees/GetAll");
                    return errorResult;
                }

                resultContainer.ReportPostHeader.Message = "Success";
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                CreatedResult createdResult = new CreatedResult("Employees/Update", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "Employees/Create");
                return errorResult;
            }
        }

        //[Authorize]
        [Route("Employees/Delete")]
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

                RESTEmployeesDB rESTEmployeesDB = new RESTEmployeesDB(base.RESTConfig);
                DeleteEmployeesView viewToDelete = rESTEmployeesDB.TryParse<DeleteEmployeesView>(GeneralBodyPost, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Employees/Insert");
                    return errorResult;
                }

                rESTEmployeesDB.TryDelete(viewToDelete, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Employees/GetAll");
                    return errorResult;
                }

                resultContainer.ReportPostHeader.Message = "Success";
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                CreatedResult createdResult = new CreatedResult("Employees/Delete", httpResponseMessage);
                createdResult.Value = resultContainer;
                createdResult.StatusCode = 200;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "Employees/Create");
                return errorResult;
            }
        }

        //[Authorize]
        [Route("Employees/Create")]
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
                
                RESTEmployeesDB rESTEmployeesDB = new RESTEmployeesDB(base.RESTConfig);
                CreateEmployeesView viewToInclude = rESTEmployeesDB.TryParse<CreateEmployeesView>(GeneralBodyPost, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError412_PreConditionFailed(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Employees/Insert");
                    return errorResult;
                }

                rESTEmployeesDB.TryInclude(viewToInclude, out restExceptionError);
                if (restExceptionError != null)
                {
                    CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, restExceptionError.ExceptionMessage, restExceptionError.InternalMessage, "Employees/GetAll");
                    return errorResult;
                }

                resultContainer.ReportPostHeader.Message = "Success";
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.Created;
                CreatedResult createdResult = new CreatedResult("Employees/Create", httpResponseMessage);
                createdResult.Value = resultContainer;
                return createdResult;
            }
            catch (Exception ex)
            {
                CreatedResult errorResult = helperRestError.GetError500_InternalServerError(transactionID, ex.Message, "x", "Employees/Create");
                return errorResult;
            }
        }        
    }
}

