using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyAppXUnitTestLib.Rest
{
    public class HeadersTestResult
    {
        /// <summary>
        /// This are headers where the expected value don't match the test
        /// </summary>
        public List<string> headersWrongValue = new List<string>();

        /// <summary>
        /// This are headers that are supposed to be response but are not
        /// </summary>
        public List<string> headersNotFoundInResponse = new List<string>();

        /// <summary>
        /// This are headers found in response but the request file headers don't have them
        /// </summary>
        public List<string> headersNotFoundInRequest = new List<string>();

        /// <summary>
        /// Passed
        /// </summary>
        public List<string> headersOK = new List<string>();
    }

    public class WebApiResponse
    {
        public string Error { get; set; }
    }
    /// <summary>
    /// Data to create http request
    /// </summary>
    public class RequestData
    {
        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public string VerbToUse { get; set; }
        public string Body { get; set; }

        public string BodyFile { get; set; }

        public string URL { get; set; }
        public string HeaderFile { get; set; }
        //public object BodyAsBinary { get; set; }
    }

    /// <summary>
    /// static data to compare
    /// </summary>
    public class ResponseDataToAssert
    {
        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public string StatusCode { get; set; }
        public string Body { get; set; }
        public string Output { get; set; }

        //public object BodyAsBinary { get; set; }
    }

    /// <summary>
    /// data received from http request
    /// </summary>
    public class ResponseData
    {
        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public string StatusCode { get; set; }
        public string Body { get; set; }
        public string Output { get; set; }

        //public object BodyAsBinary { get; set; }
    }

    public class GenericHTTPTester
    {
        public GenericHTTPTester(string basePath)
        {
            UrlBasePath = basePath;
        }

        public string UrlBasePath { get; set; }
        public Dictionary<string, string> Setup = new Dictionary<string, string>();
        public RequestData RequestData = new RequestData();
        public ResponseData ResponseDataExpected = new ResponseData();
        public ResponseData ResponseDataReceived = new ResponseData();

        private Dictionary<string, string> StringToKeyValue(string file)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            var allConfig = File.ReadAllLines(file);
            var logList = new List<string>(allConfig);
            foreach (string line in logList)
            {
                if (line.StartsWith("//") || string.IsNullOrEmpty(line))
                {
                    continue;
                }
                string[] keyvalue = line.Split('=');
                if (keyvalue.Length == 2)
                {
                    values.Add(keyvalue[0], keyvalue[1]);
                }
            }

            return values;
        }

        public string CompareArray(out string error)
        {
            error = null;
            try
            {
                JArray jsonBodyReceived = JArray.Parse(this.ResponseDataReceived.Body);
                JArray jsonBodyExpected = JArray.Parse(this.ResponseDataExpected.Body);

                //Freddy: comparing children tokens
                if (jsonBodyReceived.Count != jsonBodyExpected.Count)
                {
                    string result = string.Format("Expected has {0} itens and and response have {1} itens", jsonBodyExpected.Count, jsonBodyReceived.Count);
                    return result;
                }

                StringBuilder s = HelperGenericHttpTester.CompareArrays(jsonBodyExpected, jsonBodyReceived);
                return s.ToString();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        public string CompareBody(out string error)
        {
            error = null;
            try
            {
                JObject jsonBodyReceived = JObject.Parse(this.ResponseDataReceived.Body);
                JObject jsonBodyExpected = JObject.Parse(this.ResponseDataExpected.Body);

                StringBuilder s = HelperGenericHttpTester.CompareObjects(jsonBodyExpected, jsonBodyReceived);
                return s.ToString();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Compare expected headers
        /// </summary>
        public HeadersTestResult CompareHeaders(out string error)
        {
            //todo fazer loop em cima do response.headers.
            error = null;

            HeadersTestResult headersTestResult = HelperGenericHttpTester.CompareHeaders(this.ResponseDataExpected, this.ResponseDataReceived, out error);
            return headersTestResult;
        }

        /// <summary>
        /// persist body and headers
        /// </summary>
        /// <param name="outputPath"></param>
        /// <param name="error"></param>
        public void Persist(string outputPath, out string error)
        {
            error = null;
            try
            {
                //save body
                string bodyFile = Path.Combine(outputPath, this.RequestData.BodyFile);
                File.WriteAllText(bodyFile, this.ResponseDataReceived.Body);

                //Save headers
                string headerFile = Path.Combine(outputPath, this.RequestData.HeaderFile);
                string allHeaders = "";
                foreach (var item in this.ResponseDataReceived.Headers)
                {
                    string content = item.Key + "=" + item.Value;
                    allHeaders += content + Environment.NewLine;
                }
                File.WriteAllText(headerFile, allHeaders);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
        }

        /// <summary>
        /// create http request. Only for json data. Dont support binary
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<WebApiResponse> Dispatch(string url = null)
        {
            if (url == null)
            {
                url = this.RequestData.URL;
            }

            WebApiResponse webApiResponse = new WebApiResponse();
            webApiResponse.Error = null;
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url),
                    Content = new StringContent(this.RequestData.Body, Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request).ConfigureAwait(false);

                Stream recieveStream = await response.Content.ReadAsStreamAsync();
                StreamReader readStream = new StreamReader(recieveStream, Encoding.UTF8);
                this.ResponseDataReceived.Body = readStream.ReadToEnd();
                this.ResponseDataReceived.StatusCode = ((int)response.StatusCode).ToString();

                foreach (var header in response.Headers)
                {
                    string allHeaderValue = "";
                    foreach (var item in header.Value)
                    {
                        if (allHeaderValue == "")
                            allHeaderValue += item;
                        else
                            allHeaderValue += "," + item;
                    }
                    this.ResponseDataReceived.Headers.Add(header.Key, allHeaderValue);
                }

                recieveStream.Close();
                readStream.Close();
            }
            catch (Exception ex)
            {
                webApiResponse.Error = ex.Message;
            }

            return webApiResponse;
        }

        public void Load(string path, out string error)
        {
            error = null;
            string[] files = Directory.GetFiles(path, "Setup.txt", SearchOption.AllDirectories);

            if (files == null)
            {
                error = "File Setup.txt not found";
            }

            string pathAndFile = Path.Combine(path, "Setup.txt");

            try
            {
                if (File.Exists(pathAndFile))
                {
                    var allConfig = File.ReadAllLines(pathAndFile);
                    var logList = new List<string>(allConfig);

                    foreach (string line in logList)
                    {
                        if (string.IsNullOrEmpty(line))
                            continue;
                        if (line.StartsWith("//"))
                            continue;

                        string[] keyvalue = line.Split('=');
                        string aux = null;

                        if (keyvalue[0] == "ExpectedStatusCode")
                        {
                            this.ResponseDataExpected.StatusCode = keyvalue[1];
                        }

                        if (keyvalue[0] == "VerbToUse")
                        {
                            this.RequestData.VerbToUse = keyvalue[1];
                        }

                        if (keyvalue[0] == "URL")
                        {
                            if (keyvalue[1].Contains("[basepath]"))
                                this.RequestData.URL = keyvalue[1].Replace("[basepath]", this.UrlBasePath);
                            else
                                this.RequestData.URL = keyvalue[1];
                        }

                        if (keyvalue[0] == "BodyFile")
                        {
                            aux = Path.Combine(path, keyvalue[1]);
                            string text = File.ReadAllText(aux, Encoding.UTF8);
                            this.RequestData.Body = text;
                            this.RequestData.BodyFile = "out_" + keyvalue[1];
                        }

                        if (keyvalue[0] == "BodyExpectedResponse")
                        {
                            aux = Path.Combine(path, keyvalue[1]);
                            string text = File.ReadAllText(aux, Encoding.UTF8);
                            this.ResponseDataExpected.Body = text;
                        }

                        if (keyvalue[0] == "HeaderFile")
                        {
                            string headerFile = Path.Combine(path, keyvalue[1]);
                            this.RequestData.Headers = StringToKeyValue(headerFile);
                            this.RequestData.HeaderFile = "out_" + keyvalue[1];
                        }
                        
                        if (keyvalue[0] == "HeaderExpectedResponse")
                        {
                            string headerFile = Path.Combine(path, keyvalue[1]);
                            this.ResponseDataExpected.Headers = StringToKeyValue(headerFile);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error = "Error in loading file " + ex.Message;
            }
        }
    }
}
