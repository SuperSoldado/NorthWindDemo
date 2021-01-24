using MyAppXUnitTestLib;
using System;
using System.IO;
using System.Reflection;
using MyAppXUnitTestLib.Rest;
using Xunit;
using System.Threading.Tasks;
using MyAppXUnitTestLib;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading;

//[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]
namespace MyAppXUnitTest
{

    public class UnitTest1 
    {
        
        //[Fact]
        public void Test1()
        {
            //string conn = base.xUnitTestConfig.UnitTestConfig.GetMainConnectionString;
            int x = 1;
            int y = 1;
            int z = x + y;
            Assert.True((z == 2), "z is not 2");
        }

        //[Fact]
        public void Test2()
        {
            //string conn = base.xUnitTestConfig.UnitTestConfig.GetMainConnectionString;
            int x = 1;
            int y = 1;
            int z = x + y;
            //Thread.Sleep(7000);
            Assert.True((z == 2), "z is not 2");
        }
        /*
        [Fact]
        public async Task GenericHttpRequest()
        {
            string testFilePath = Path.Combine(BaseFilePath, "RestTest\\MyTest");
            //string testOutputFilePath = Path.Combine(BaseFilePath, "RestTest\\MyTest\\Output");
            string testOutputFilePath = "c:\\temp";
            string error = null;

            GenericHTTPTester GenericHTTPTester = new GenericHTTPTester(base.xUnitTestConfig.UnitTestConfig.BaseHttpsURL);
            GenericHTTPTester.Load(testFilePath, out error);
            Assert.Null(error);

            WebApiResponse WebApiResponse = await GenericHTTPTester.Dispatch();
            Assert.True((GenericHTTPTester.ResponseDataReceived.StatusCode == GenericHTTPTester.ResponseDataExpected.StatusCode), "Status code mismatch");


            //this is optional
            GenericHTTPTester.Persist(testOutputFilePath, out error);
            Assert.Null(error);

            HeadersTestResult headersTestResult = GenericHTTPTester.CompareHeaders(out error);
            Assert.Null(error);
            Assert.True((headersTestResult.headersWrongValue.Count == 0), "Wrong header content detected.");
            Assert.True((headersTestResult.headersNotFoundInRequest.Count == 0), "Response missed some headers.");

            //string result1 = GenericHTTPTester.CompareBody(out error); this is used in json objects
            string result2 = GenericHTTPTester.CompareArray(out error);
            Assert.Null(error);
            Assert.True((result2==""), result2);
        }*/
    }
}
