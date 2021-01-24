using MyAppRESTLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using MyAppRESTLib.Helpers;

namespace MyAppREST.Controllers
{
    public class HttpResponseMessageResult : IActionResult
    {
        private readonly HttpResponseMessage _responseMessage;

        public HttpResponseMessageResult(HttpResponseMessage responseMessage)
        {
            _responseMessage = responseMessage; // could add throw if null
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)_responseMessage.StatusCode;

            foreach (var header in _responseMessage.Headers)
            {
                context.HttpContext.Response.Headers.TryAdd(header.Key, new StringValues(header.Value.ToArray()));
            }

            using (var stream = await _responseMessage.Content.ReadAsStreamAsync())
            {
                await stream.CopyToAsync(context.HttpContext.Response.Body);
                await context.HttpContext.Response.Body.FlushAsync();
            }
        }
    }

    public class MyJson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class TestController : MyCustomController
    {
        public MyJson myJson = new MyJson();
        public TestController()
        {
            myJson.FirstName = "Freddy";
            myJson.LastName = "Ullrich";
        }

        //private HttpResponseMessage CreateResponse<T>(this HttpRequestMessage requestMessage, HttpStatusCode statusCode, T content)
        //{
        //    return new HttpResponseMessage()
        //    {
        //        StatusCode = statusCode,
        //        Content = new StringContent(JsonConvert.SerializeObject(content))
        //    };
        //}

        [Route("t04")]
        [HttpGet]
        public HttpResponseMessage GetCustomAdv1()
        {
            //dont work, return class jsonfied and 200
            //https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-3.0
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.Created;
            response.Content = new StringContent(JsonConvert.SerializeObject(myJson));
            return response;
        }

        [Route("t05")]
        [HttpGet]
        public HttpResponseMessage GetCustomAdv2()
        {
            //dont work, return class jsonfied and 200
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            return response;
        }


        [Route("t02")]
        [HttpGet]
        public IActionResult GetCustom()
        {
            return Created("", myJson);
        }

        [Route("t01")]
        [HttpGet]
        public IActionResult GetOK()
        {
            return Ok(myJson);
        }

        [Route("t03")]
        [HttpGet]
        public IActionResult GetCreatedResult()
        {
            Response.Headers.Add("X-Total-Count", "666");
            CreatedResult createdResult = new CreatedResult("MyLocation", myJson);
            createdResult.StatusCode = 201;
            createdResult.Value = myJson;
            return createdResult;
        }

        [Route("t06")]
        [HttpGet]
        public IActionResult GetAction201Result()
        {
            Response.Headers.Add("X-Total-Count", "666");
            Response.ContentType = "application/json";
            MyActionResultHelper myActionResult = new MyActionResultHelper(Response);
            string result = "12345";
            var createdResult = myActionResult.GetAction201Result(result);
            return createdResult;
        }
    }

  
}
