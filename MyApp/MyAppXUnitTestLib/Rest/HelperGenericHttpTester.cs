﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAppXUnitTestLib.Rest
{
    public static class HelperGenericHttpTester
    {
        /// <summary>
        /// Deep compare two NewtonSoft JObjects. If they don't match, returns text diffs
        /// </summary>
        /// <param name="source">The expected results</param>
        /// <param name="target">The actual results</param>
        /// <returns>Text string</returns>

        public static StringBuilder CompareObjects(JObject source, JObject target)
        {
            StringBuilder returnString = new StringBuilder();
            foreach (KeyValuePair<string, JToken> sourcePair in source)
            {
                if (sourcePair.Value.Type == JTokenType.Object)
                {
                    if (target.GetValue(sourcePair.Key) == null)
                    {
                        returnString.Append("Key " + sourcePair.Key
                                            + " not found" + Environment.NewLine);
                    }
                    else if (target.GetValue(sourcePair.Key).Type != JTokenType.Object)
                    {
                        returnString.Append("Key " + sourcePair.Key
                                            + " is not an object in target" + Environment.NewLine);
                    }
                    else
                    {
                        returnString.Append(CompareObjects(sourcePair.Value.ToObject<JObject>(),
                            target.GetValue(sourcePair.Key).ToObject<JObject>()));
                    }
                }
                else if (sourcePair.Value.Type == JTokenType.Array)
                {
                    if (target.GetValue(sourcePair.Key) == null)
                    {
                        returnString.Append("Key " + sourcePair.Key
                                            + " not found" + Environment.NewLine);
                    }
                    else
                    {
                        returnString.Append(CompareArrays(sourcePair.Value.ToObject<JArray>(),
                            target.GetValue(sourcePair.Key).ToObject<JArray>(), sourcePair.Key));
                    }
                }
                else
                {
                    JToken expected = sourcePair.Value;
                    var actual = target.SelectToken(sourcePair.Key);
                    if (actual == null)
                    {
                        returnString.Append("Key " + sourcePair.Key
                                            + " not found" + Environment.NewLine);
                    }
                    else
                    {
                        //Freddy: added this ignore
                        if (sourcePair.Value.ToString() == "[ignore]")
                        {
                            continue;
                        }

                        if (!JToken.DeepEquals(expected, actual))
                        {
                            returnString.Append("Key " + sourcePair.Key + ": "
                                                + sourcePair.Value + " !=  "
                                                + target.Property(sourcePair.Key).Value
                                                + Environment.NewLine);
                        }
                    }
                }
            }
            return returnString;
        }

        /// <summary>
        /// Deep compare two NewtonSoft JArrays. If they don't match, returns text diffs
        /// </summary>
        /// <param name="source">The expected results</param>
        /// <param name="target">The actual results</param>
        /// <param name="arrayName">The name of the array to use in the text diff</param>
        /// <returns>Text string</returns>

        public static StringBuilder CompareArrays(JArray source, JArray target, string arrayName = "")
        {
            var returnString = new StringBuilder();
            for (var index = 0; index < source.Count; index++)
            {

                var expected = source[index];
                if (expected.Type == JTokenType.Object)
                {
                    var actual = (index >= target.Count) ? new JObject() : target[index];
                    returnString.Append(CompareObjects(expected.ToObject<JObject>(),
                        actual.ToObject<JObject>()));
                }
                else
                {

                    var actual = (index >= target.Count) ? "" : target[index];
                    if (!JToken.DeepEquals(expected, actual))
                    {
                        if (String.IsNullOrEmpty(arrayName))
                        {
                            returnString.Append("Index " + index + ": " + expected
                                                + " != " + actual + Environment.NewLine);
                        }
                        else
                        {
                            returnString.Append("Key " + arrayName
                                                + "[" + index + "]: " + expected
                                                + " != " + actual + Environment.NewLine);
                        }
                    }
                }
            }
            return returnString;
        }

        public static HeadersTestResult CompareHeaders(ResponseData ResponseDataExpected, ResponseData ResponseDataReceived, out string error)
        {
            error = null;
            try
            {
                HeadersTestResult headersTestResult = new HeadersTestResult();
                foreach (var header in ResponseDataExpected.Headers)
                {
                    if (header.Key.StartsWith("-"))
                    {
                        string aux = header.Key.Substring(1, header.Key.Length - 1);

                        if (ResponseDataReceived.Headers.ContainsKey(aux))
                        {
                            headersTestResult.headersOK.Add(aux);
                        }
                        else
                        {
                            headersTestResult.headersNotFoundInResponse.Add(aux);
                        }
                        continue;
                    }
                    else
                    {
                        string headerValueInResponse = "";
                        ResponseDataReceived.Headers.TryGetValue(header.Key, out headerValueInResponse);

                        if (string.IsNullOrEmpty(headerValueInResponse))
                        {
                            //response dont have my header.
                            headersTestResult.headersNotFoundInResponse.Add(header.Key);
                            continue;
                        }
                        else
                        {
                            //response has my header
                            if (headerValueInResponse.Trim() == header.Value.Trim())
                            {
                                headersTestResult.headersOK.Add(header.Value);
                            }
                            else
                            {
                                headersTestResult.headersWrongValue.Add(header.Value);
                            }
                        }

                    }
                }

                return headersTestResult;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return null;
        }
    }
}
