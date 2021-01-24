using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAppRESTLib.Helpers
{
    public class DefaultResult
    {
        public string Result { get; set; }
    }

    public class MyActionResultHelper
    {
        private string urlSource { get; set; }
        public MyActionResultHelper(HttpResponse httpResponse = null)
        {
            this.urlSource = urlSource;
            if (httpResponse != null)
            {
                httpResponse.ContentType = "application/json";
                urlSource = httpResponse.HttpContext.Request.Path;
            }
            else
            {
                this.urlSource = "Not defined";
            }
        }


        public IActionResult GetActionResult(int statusCode, object jsonResult)
        {
            CreatedResult createdResult = new CreatedResult(urlSource, jsonResult);
            createdResult.StatusCode = statusCode;
            createdResult.Value = jsonResult;
            return createdResult;
        }

        /// <summary>
        /// Returns 200 and "Result=MyResult"
        /// </summary>
        /// <param name="ResultMessage"></param>
        /// <returns></returns>
        public IActionResult GetAction201Result(string ResultMessage)
        {
            DefaultResult result = new DefaultResult();
            result.Result = ResultMessage;
            CreatedResult createdResult = new CreatedResult(urlSource, result);
            createdResult.StatusCode = 201;
            return createdResult;
        }

        /// <summary>
        /// Returns 200 and "Result=MyResult"
        /// </summary>
        /// <param name="ResultMessage"></param>
        /// <returns></returns>
        public IActionResult GetAction200Result(string ResultMessage)
        {
            DefaultResult result = new DefaultResult();
            result.Result = ResultMessage;
            CreatedResult createdResult = new CreatedResult(urlSource, result);
            createdResult.StatusCode = 200;
            return createdResult;
        }

        /// <summary>
        /// Internal server error
        /// </summary>
        /// <param name="ResultMessage"></param>
        /// <returns></returns>
        public IActionResult GetAction500Result(string ResultMessage)
        {
            DefaultResult result = new DefaultResult();
            result.Result = ResultMessage;
            CreatedResult createdResult = new CreatedResult(urlSource, result);
            createdResult.StatusCode = 500;
            return createdResult;
        }

        /// <summary>
        /// Returns 200 and json
        /// </summary>
        /// <param name="resultObject"></param>
        /// <returns></returns>
        public IActionResult GetAction200(object resultObject)
        {
            CreatedResult createdResult = new CreatedResult(urlSource, resultObject);
            createdResult.StatusCode = 200;
            return createdResult;
        }

        /// <summary>
        /// Return pre condition failed
        /// </summary>
        /// <param name="ResultMessage"></param>
        /// <returns></returns>
        public IActionResult GetAction412Result(string ResultMessage)
        {
            DefaultResult result = new DefaultResult();
            result.Result = ResultMessage;
            CreatedResult createdResult = new CreatedResult(urlSource, result);
            createdResult.StatusCode = 412;
            return createdResult;
        }
    }
}
