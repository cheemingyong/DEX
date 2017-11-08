// ASP.NET Maker 2017
// Copyright (c) e.World Technology Limited. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using AspNetMaker2017.Models;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Headers;
using static AspNetMaker2017.Models.DEX;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Cors;
using System.Security.Cryptography;
//
// Controllers
//

namespace AspNetMaker2017.Controllers
{
    public partial class HomeController : Controller
    {
        [Route("api/Catalogs/Banks"), HttpPost, Produces("application/json")]
        [EnableCors("MyPolicy")]
        public async Task<IActionResult> transpay_banks_list([FromBody] JObject RequestData)
        {
            try
            {
                var response = new HttpResponseMessage();
                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString("<RSAKeyValue><Modulus>xhIyo2B4oil/H8/8D6eT9tH1MB4iKCFelfUXwlavvPQQpbFHgrw3V74vhnjMMoBsYroLgB8eoPjWQHQ+jz7Aiq6BVDra5NmHX+yk82r/VXIuu6FhbLb/J+Alva7OXV8Y8XZS2uWhOrrhO280WZWPQJN3r6sUpQ/iEsNWXGQEI00=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");
                    var TransPayApiSystemId = "32d77933-732d-49b6-a9eb-fd6eeca7d7f1";
                    var TransPayApiUserName = Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes("APILITE.TPAY"), false));
                    var TransPayApiUserPassword = Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes("APIlite@123"), false));
                    var TransPayApiBranchId = Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes("TP0000"), false));
                    var TransPayApiAuthorizationTokenXml = "" +
                    "<Authentication>" +
                    "<Id>" + TransPayApiSystemId + "</Id>" +
                    "<UserName>" + TransPayApiUserName + "</UserName>" +
                    "<Password>" + TransPayApiUserPassword + "</Password>" +
                    "<BranchId>" + TransPayApiBranchId + "</BranchId>" +
                    "</Authentication>";
                    //curl - i - k - H "Accept: application/json" - H "Content-Type: application/json" - H "Authorization: Credentials <INSERT_GENERATED_TOKEN_HERE>" - X GET https://demo-api.transfast.net/api/catalogs/banks?countryisocode=NP”
                    //var StreetAddress = RequestData.SelectToken("streetaddress");
                    using (var client = new HttpClient())
                    {
                        string url = "https://demo-api.transfast.net/api/catalogs/banks?countryisocode=SG";
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/xml");
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/xml; charset=utf-8");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Credentials", TransPayApiAuthorizationTokenXml);
                        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Credentials", "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/PjxBdXRoZW50aWNhdGlvbj4NCiAgPElkPjMyZDc3OTMzLTczMmQtNDliNi1hOWViLWZkNmVlY2E3ZDdmMTwvSWQ+DQogIDxVc2VyTmFtZT5ENWVYZFZTQlR6ZnM3NmdtbUVpS1JLbzFMQXlmckZKVlNBMnhzV01iOEptMUJEdUdWaFh2dXZYNlVMWmlvalN3akdJVGh5dStlR1VxZ3RlUVhyb0RQdG90TFArb1IxWEFYUE1SMXVQc0d5VjJ0eVEvZkZLaWJVUkxzbHFtN1Q1cklRaXZNU1ZUeUluRHBhUW5EZVk2K2pBVHREYjUvQ2hmQlhEejV4YXhlK0U9PC9Vc2VyTmFtZT4NCiAgPFBhc3N3b3JkPkNnQU1lZU1LSGZiSjgzUHAzTG9NLzdkY0JYa3ZLUGk0MVhmVy9FMUdwSWlCUmtFYVRlRVJYay8xVFkyc1NZVGtWWm02bXdIYmFHUnIxL0dHNyswdlJJdkxBT2pZNjJGNVIySXZSRE9HZnE5Z2dRczBKN0hpbUVLZ3E0c2ppV1VVdkY1d0xySE9tbWR0L1Qvekp3c1pmdUtUVDUrdU40M3JmSEg1dU92MFpJTT08L1Bhc3N3b3JkPg0KICA8QnJhbmNoSWQ+TGc2LzliZ0RENEVHRUY0SVZXNy83ZjlmalUwZUNpZHVmL0dVRVlERHY5eDkybTJUWHFzSWJBZ2loMU1zd3BMNFF5VmdUYS9GSUhaZXhFLzdrclBtak5qZlhFOXhNRWdrbUs3Wm41NDg0cVNYYTltK2JZY21aVURIb2NrcW1FVkFaYnB0NkRIOThGSldCRkRwWkpKY1NNR3dPT29HNVk5amNhN2tyejNJS1JzPTwvQnJhbmNoSWQ+DQo8L0F1dGhlbnRpY2F0aW9uPg==");
                        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", TransPayApiAuthorizationTokenXml);
                        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/PjxBdXRoZW50aWNhdGlvbj4NCiAgPElkPjMyZDc3OTMzLTczMmQtNDliNi1hOWViLWZkNmVlY2E3ZDdmMTwvSWQ+DQogIDxVc2VyTmFtZT5ENWVYZFZTQlR6ZnM3NmdtbUVpS1JLbzFMQXlmckZKVlNBMnhzV01iOEptMUJEdUdWaFh2dXZYNlVMWmlvalN3akdJVGh5dStlR1VxZ3RlUVhyb0RQdG90TFArb1IxWEFYUE1SMXVQc0d5VjJ0eVEvZkZLaWJVUkxzbHFtN1Q1cklRaXZNU1ZUeUluRHBhUW5EZVk2K2pBVHREYjUvQ2hmQlhEejV4YXhlK0U9PC9Vc2VyTmFtZT4NCiAgPFBhc3N3b3JkPkNnQU1lZU1LSGZiSjgzUHAzTG9NLzdkY0JYa3ZLUGk0MVhmVy9FMUdwSWlCUmtFYVRlRVJYay8xVFkyc1NZVGtWWm02bXdIYmFHUnIxL0dHNyswdlJJdkxBT2pZNjJGNVIySXZSRE9HZnE5Z2dRczBKN0hpbUVLZ3E0c2ppV1VVdkY1d0xySE9tbWR0L1Qvekp3c1pmdUtUVDUrdU40M3JmSEg1dU92MFpJTT08L1Bhc3N3b3JkPg0KICA8QnJhbmNoSWQ+TGc2LzliZ0RENEVHRUY0SVZXNy83ZjlmalUwZUNpZHVmL0dVRVlERHY5eDkybTJUWHFzSWJBZ2loMU1zd3BMNFF5VmdUYS9GSUhaZXhFLzdrclBtak5qZlhFOXhNRWdrbUs3Wm41NDg0cVNYYTltK2JZY21aVURIb2NrcW1FVkFaYnB0NkRIOThGSldCRkRwWkpKY1NNR3dPT29HNVk5amNhN2tyejNJS1JzPTwvQnJhbmNoSWQ+DQo8L0F1dGhlbnRpY2F0aW9uPg==");
                        response = client.GetAsync(url).Result;
                        using (HttpContent content = response.Content)
                        {
                            return this.Content(content.ReadAsStringAsync().Result, "text/plain");
                        }
                    }
                }
                response.EnsureSuccessStatusCode();
                return this.Content("{}", "text/plain");
            }
            catch (Exception ex)
            {
                return this.Content(ex.Message.ToString(), "text/plain");
            }
            return this.Content("{}", "text/plain");
        }

        [Route("api/getCurrRateFee"), HttpPost, Produces("application/json")]
        [EnableCors("MyPolicy")]
        public async Task<IActionResult> ProcessHTML([FromBody] JObject RequestData)
        {
            string strCurrencyRateFee = "";
            string CurrencyRatePrefix = "";
            decimal SellRate = 0;
            if (RequestData != null)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        var response = new HttpResponseMessage();
                        HttpRequestMessage request = request = new HttpRequestMessage();
                        // if (this.HttpContext.Request.Path.Value.Contains("api/getCurrRateFee"))
                        //{
                        Language = Language ?? new cLanguage();

                        var CurrCode = RequestData.SelectToken("currencycode");
                        var CurrAmount = RequestData.SelectToken("amount");

                        var rsCurrencyRate = ew_ExecuteRecords("" +
                        "SELECT " +
                        "[vCurrencyCode]" +
                        ",[CurrencyCode]" +
                        ",[CurrencyDescription]" +
                        ",[UNITS]" +
                        ",[TTSellRateType]" +
                        ",[TTSellRate]" +
                        ",[TTBuyRate]" +
                        ",[TTMinBid]" +
                        ",[TTMaxBid]" +
                        ",[TTSellFixedPrice]" +
                        ",[TTMinAsk]" +
                        ",[TTMaxAsk]" +
                        ",[TTSellSpotPipsMargin]" +
                        ",[TTSellSpotPricePercentMargin]" +
                        ",[TTAvgCost]" +
                        ",[TTStockBalance]" +
                        "  FROM [DEX].[dbo].[vw_CurrencyRemittance] " +
                        " where [CurrencyCode] = '" + CurrCode + "'");

                        foreach (var rs in rsCurrencyRate)
                        {
                            if (AspNetMaker2017.Startup.CurrencyRates.ContainsKey("OANDA:" + rs["currencycode"].ToString()))
                            {
                                CurrencyRatePrefix = "OANDA:";
                            }
                            else if (AspNetMaker2017.Startup.CurrencyRates.ContainsKey("FX_IDC:" + rs["currencycode"].ToString()))
                            {
                                CurrencyRatePrefix = "FX_IDC:";
                            }
                            if (AspNetMaker2017.Startup.CurrencyRates.ContainsKey(CurrencyRatePrefix + rs["currencycode"].ToString()))
                            {
                                if ((AspNetMaker2017.Startup.CurrencyRates[CurrencyRatePrefix.ToString() + rs["currencycode"].ToString()] + "") != "")
                                {
                                    strCurrencyRateFee += "{";
                                    strCurrencyRateFee += "\"Spot Rate\":\"" + AspNetMaker2017.Startup.CurrencyRates[CurrencyRatePrefix + rs["currencycode"].ToString()].ToString() + "\"";
                                }
                                else
                                {
                                    strCurrencyRateFee += "{";
                                    strCurrencyRateFee += "\"Spot Rate\":\"" + rs["TTSellFixedPrice"].ToString() + "\"";
                                }

                            }
                            else
                            {
                                strCurrencyRateFee += "{";
                                strCurrencyRateFee += "\"Spot Rate\":\"" + rs["TTSellFixedPrice"].ToString() + "\"";
                            }

                            if (decimal.Parse(rs["TTStockBalance"].ToString()) > decimal.Parse(CurrAmount.ToString()))
                            {
                                if (rs["TTSellRateType"].ToString().IndexOf("Fixed") >= 0)
                                {
                                    strCurrencyRateFee += ",";
                                    SellRate = decimal.Parse(rs["TTSellFixedPrice"].ToString());
                                    strCurrencyRateFee += "\"Sell Rate\":\"" + rs["TTSellFixedPrice"].ToString() + "\"";
                                }
                                else if (rs["TTSellRateType"].ToString().IndexOf("Percent") >= 0)
                                {
                                    strCurrencyRateFee += ",";
                                    SellRate = decimal.Parse(AspNetMaker2017.Startup.CurrencyRates[CurrencyRatePrefix + rs["currencycode"].ToString()].ToString()) * (1 + (decimal.Parse(rs["TTSellSpotPricePercentMargin"].ToString()) / 100));
                                    strCurrencyRateFee += "\"Sell Rate\":\"" + SellRate + "\"";
                                }
                                else if (rs["TTSellRateType"].ToString().IndexOf("Pips") >= 0)
                                {
                                    strCurrencyRateFee += ",";
                                    SellRate = decimal.Parse(AspNetMaker2017.Startup.CurrencyRates[CurrencyRatePrefix + rs["currencycode"].ToString()].ToString()) + (decimal.Parse(rs["TTSellSpotPipsMargin"].ToString()) / 10000);
                                    strCurrencyRateFee += "\"Sell Rate\":\"" + SellRate + "\"";
                                }

                            }

                            strCurrencyRateFee += ",";
                            strCurrencyRateFee += "\"Buy Rate\":\"" + rs["TTBuyRate"].ToString() + "\"";
                            strCurrencyRateFee += ",";
                            strCurrencyRateFee += "\"Last Avg Cost\":\"" + rs["TTAvgCost"].ToString() + "\"";
                            strCurrencyRateFee += ",";
                            strCurrencyRateFee += "\"Stock Balance\":\"" + rs["TTStockBalance"].ToString() + "\"";
                            strCurrencyRateFee += ",";
                            if (SellRate < decimal.Parse(rs["TTAvgCost"].ToString()))
                            {
                                strCurrencyRateFee += "\"Approval\":\"True\"";
                            }
                            else
                            {
                                strCurrencyRateFee += "\"Approval\":\"False\"";
                            }
                            strCurrencyRateFee += "}";
                        }
                        //}
                        await Task.Run(() =>
                        {
                        });
                        response.EnsureSuccessStatusCode();
                        return this.Content(strCurrencyRateFee, "text/plain");
                    }
                    catch (Exception ex)
                    {
                        return this.Content(ex.Message.ToString(), "text/plain");
                    }
                }
            }
            return this.Content("{}", "text/plain");
        }

        [Route("api/getCurrRates/Asia")]
        [EnableCors("MyPolicy")]
        public IActionResult getCurrRatesAsia()
        {
            Language = Language ?? new cLanguage();
            //AspNetMaker2017.Startup.CurrencyRates = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery("http://localhost:5002/api/setCurrRates?1=1&OANDA:CADSGD=1.09142&OANDA:EURSGD=1.58969&FX_IDC:MYRSGD=0.315&USDSGD=1.35784&OANDA:NZDSGD=1.02269");
            var rsCurrencyRates = ew_ExecuteRecords("" +
            "select [CurrencyCode],[CurrencyDescription],[UNITS],[TTSpotRate] " +
            " from Currency ");
            string strCurrencyRates = "";
            string CurrencyRatePrefix = "";
            decimal SellRate = 0;
            strCurrencyRates += "{";
            foreach (var rs in rsCurrencyRates)
            {
                if (AspNetMaker2017.Startup.CurrencyRates.ContainsKey("OANDA:" + rs["currencycode"].ToString()))
                {
                    CurrencyRatePrefix = "OANDA:";
                }
                else if (AspNetMaker2017.Startup.CurrencyRates.ContainsKey("FX_IDC:" + rs["currencycode"].ToString()))
                {
                    CurrencyRatePrefix = "FX_IDC:";
                }
                else
                {
                    strCurrencyRates += "\"" + rs["currencycode"] + "\":";
                    strCurrencyRates += "{";
                    strCurrencyRates += "\"Spot Rate\":" + rs["TTSpotRate"].ToString(); ;
                }

                if (AspNetMaker2017.Startup.CurrencyRates.ContainsKey(CurrencyRatePrefix + rs["currencycode"].ToString()))
                {
                    if ((AspNetMaker2017.Startup.CurrencyRates[CurrencyRatePrefix.ToString() + rs["currencycode"].ToString()] + "") != "")
                    {
                        strCurrencyRates += "\"" + rs["currencycode"] + "\":";
                        strCurrencyRates += "{";
                        strCurrencyRates += "\"Spot Rate\":" + AspNetMaker2017.Startup.CurrencyRates[CurrencyRatePrefix + rs["currencycode"].ToString()].ToString();
                    }
                    else
                    {
                        strCurrencyRates += "\"" + rs["currencycode"] + "\":";
                        strCurrencyRates += "{";
                        strCurrencyRates += "\"Spot Rate\":" + rs["TTSpotRate"].ToString();
                    }

                }
                strCurrencyRates += "}";
                strCurrencyRates += ",";
            }
            strCurrencyRates += "}";
            strCurrencyRates = strCurrencyRates.Replace(",}", "}");
            return this.Content(strCurrencyRates, "text/plain");
        }

        [Route("api/getCurrRates")]
        [EnableCors("MyPolicy")]
        public IActionResult getCurrRates()
        {
            Language = Language ?? new cLanguage();
            //AspNetMaker2017.Startup.CurrencyRates = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery("http://localhost:5002/api/setCurrRates?1=1&OANDA:CADSGD=1.09142&OANDA:EURSGD=1.58969&FX_IDC:MYRSGD=0.315&USDSGD=1.35784&OANDA:NZDSGD=1.02269");
            var rsCurrencyRates = ew_ExecuteRecords("" +
            "select [CurrencyCode],[CurrencyDescription],[UNITS]," +
            "[TTSellRateType],[TTSellFixedPrice]," +
            "[TTSellSpotPricePercentMargin],[TTSellSpotPipsMargin],[TTAvgCost],[TTStockBalance] from vw_CurrencyRemittance ");
            string strCurrencyRates = "";
            string CurrencyRatePrefix = "";
            decimal SellRate = 0;
            strCurrencyRates += "{";
            foreach (var rs in rsCurrencyRates)
            {
                if (AspNetMaker2017.Startup.CurrencyRates.ContainsKey("OANDA:" + rs["currencycode"].ToString()))
                {
                    CurrencyRatePrefix = "OANDA:";
                }
                else if (AspNetMaker2017.Startup.CurrencyRates.ContainsKey("FX_IDC:" + rs["currencycode"].ToString()))
                {
                    CurrencyRatePrefix = "FX_IDC:";
                }
                else
                {
                    strCurrencyRates += "\"" + rs["currencycode"] + "\":";
                    strCurrencyRates += "{";
                    strCurrencyRates += "\"Spot Rate\":" + rs["TTSellFixedPrice"].ToString(); ;
                }

                if (AspNetMaker2017.Startup.CurrencyRates.ContainsKey(CurrencyRatePrefix + rs["currencycode"].ToString()))
                {
                    if ((AspNetMaker2017.Startup.CurrencyRates[CurrencyRatePrefix.ToString() + rs["currencycode"].ToString()] + "") != "")
                    {
                        strCurrencyRates += "\"" + rs["currencycode"] + "\":";
                        strCurrencyRates += "{";
                        strCurrencyRates += "\"Spot Rate\":" + AspNetMaker2017.Startup.CurrencyRates[CurrencyRatePrefix + rs["currencycode"].ToString()].ToString();
                    }
                    else
                    {
                        strCurrencyRates += "\"" + rs["currencycode"] + "\":";
                        strCurrencyRates += "{";
                        strCurrencyRates += "\"Spot Rate\":" + rs["TTSellFixedPrice"].ToString();
                    }

                }
                if (rs["TTSellRateType"].ToString().IndexOf("Price") >= 0)
                {
                    strCurrencyRates += ",";
                    SellRate = decimal.Parse(rs["TTSellFixedPrice"].ToString());
                    strCurrencyRates += "\"Sell Rate\":" + rs["TTSellFixedPrice"].ToString();
                }
                else if (rs["TTSellRateType"].ToString().IndexOf("Percent") >= 0)
                {
                    strCurrencyRates += ",";
                    SellRate = decimal.Parse(rs["TTAvgCost"].ToString()) * (1 + (decimal.Parse(rs["TTSellSpotPricePercentMargin"].ToString()) / 100));
                    strCurrencyRates += "\"Sell Rate\":" + SellRate;
                }
                else if (rs["TTSellRateType"].ToString().IndexOf("Pips") >= 0)
                {
                    strCurrencyRates += ",";
                    SellRate = decimal.Parse(rs["TTAvgCost"].ToString()) + (decimal.Parse(rs["TTSellSpotPipsMargin"].ToString()) / 10000);
                    strCurrencyRates += "\"Sell Rate\":" + SellRate;
                }
                strCurrencyRates += ",";
                strCurrencyRates += "\"Last Avg Cost\":" + rs["TTAvgCost"].ToString();
                strCurrencyRates += ",";
                strCurrencyRates += "\"Stock Balance\":" + rs["TTStockBalance"].ToString();
                strCurrencyRates += ",";
                if (SellRate < decimal.Parse(rs["TTAvgCost"].ToString()))
                {
                    strCurrencyRates += "\"Approval\":\"True\"";
                }
                else
                {
                    strCurrencyRates += "\"Approval\":\"False\"";
                }
                strCurrencyRates += "}";
                strCurrencyRates += ",";
            }
            strCurrencyRates += "}";
            strCurrencyRates = strCurrencyRates.Replace(",}", "}");
            return this.Content(strCurrencyRates, "text/plain");
        }

        [Route("api/remittance/bank"), HttpPost, Produces("application/json")]
        [EnableCors("MyPolicy")]
        public async Task<IActionResult> getBanks([FromBody] JObject RequestData)
        {
            if (RequestData != null)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("", "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/PjxBdXRoZW50aWNhdGlvbj4NCiAgPElkPjMyZDc3OTMzLTczMmQtNDliNi1hOWViLWZkNmVlY2E3ZDdmMTwvSWQ+DQogIDxVc2VyTmFtZT5ENWVYZFZTQlR6ZnM3NmdtbUVpS1JLbzFMQXlmckZKVlNBMnhzV01iOEptMUJEdUdWaFh2dXZYNlVMWmlvalN3akdJVGh5dStlR1VxZ3RlUVhyb0RQdG90TFArb1IxWEFYUE1SMXVQc0d5VjJ0eVEvZkZLaWJVUkxzbHFtN1Q1cklRaXZNU1ZUeUluRHBhUW5EZVk2K2pBVHREYjUvQ2hmQlhEejV4YXhlK0U9PC9Vc2VyTmFtZT4NCiAgPFBhc3N3b3JkPkNnQU1lZU1LSGZiSjgzUHAzTG9NLzdkY0JYa3ZLUGk0MVhmVy9FMUdwSWlCUmtFYVRlRVJYay8xVFkyc1NZVGtWWm02bXdIYmFHUnIxL0dHNyswdlJJdkxBT2pZNjJGNVIySXZSRE9HZnE5Z2dRczBKN0hpbUVLZ3E0c2ppV1VVdkY1d0xySE9tbWR0L1Qvekp3c1pmdUtUVDUrdU40M3JmSEg1dU92MFpJTT08L1Bhc3N3b3JkPg0KICA8QnJhbmNoSWQ+TGc2LzliZ0RENEVHRUY0SVZXNy83ZjlmalUwZUNpZHVmL0dVRVlERHY5eDkybTJUWHFzSWJBZ2loMU1zd3BMNFF5VmdUYS9GSUhaZXhFLzdrclBtak5qZlhFOXhNRWdrbUs3Wm41NDg0cVNYYTltK2JZY21aVURIb2NrcW1FVkFaYnB0NkRIOThGSldCRkRwWkpKY1NNR3dPT29HNVk5amNhN2tyejNJS1JzPTwvQnJhbmNoSWQ+DQo8L0F1dGhlbnRpY2F0aW9uPg==");
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/PjxBdXRoZW50aWNhdGlvbj4NCiAgPElkPjMyZDc3OTMzLTczMmQtNDliNi1hOWViLWZkNmVlY2E3ZDdmMTwvSWQ+DQogIDxVc2VyTmFtZT5ENWVYZFZTQlR6ZnM3NmdtbUVpS1JLbzFMQXlmckZKVlNBMnhzV01iOEptMUJEdUdWaFh2dXZYNlVMWmlvalN3akdJVGh5dStlR1VxZ3RlUVhyb0RQdG90TFArb1IxWEFYUE1SMXVQc0d5VjJ0eVEvZkZLaWJVUkxzbHFtN1Q1cklRaXZNU1ZUeUluRHBhUW5EZVk2K2pBVHREYjUvQ2hmQlhEejV4YXhlK0U9PC9Vc2VyTmFtZT4NCiAgPFBhc3N3b3JkPkNnQU1lZU1LSGZiSjgzUHAzTG9NLzdkY0JYa3ZLUGk0MVhmVy9FMUdwSWlCUmtFYVRlRVJYay8xVFkyc1NZVGtWWm02bXdIYmFHUnIxL0dHNyswdlJJdkxBT2pZNjJGNVIySXZSRE9HZnE5Z2dRczBKN0hpbUVLZ3E0c2ppV1VVdkY1d0xySE9tbWR0L1Qvekp3c1pmdUtUVDUrdU40M3JmSEg1dU92MFpJTT08L1Bhc3N3b3JkPg0KICA8QnJhbmNoSWQ+TGc2LzliZ0RENEVHRUY0SVZXNy83ZjlmalUwZUNpZHVmL0dVRVlERHY5eDkybTJUWHFzSWJBZ2loMU1zd3BMNFF5VmdUYS9GSUhaZXhFLzdrclBtak5qZlhFOXhNRWdrbUs3Wm41NDg0cVNYYTltK2JZY21aVURIb2NrcW1FVkFaYnB0NkRIOThGSldCRkRwWkpKY1NNR3dPT29HNVk5amNhN2tyejNJS1JzPTwvQnJhbmNoSWQ+DQo8L0F1dGhlbnRpY2F0aW9uPg==");
                        var response = new HttpResponseMessage();
                        //await client.PostAsync(new Uri("https://demo-api.transfast.net/api/catalogs/bank"), new StringContent(RequestData.ToString(), Encoding.UTF8, "application/json"))
                        await client.GetAsync(new Uri("https://demo-api.transfast.net/api/catalogs/bank"))
                              .ContinueWith(responseTask =>
                              {
                                  response = responseTask.Result;
                              });
                        return this.Content(response.Content.ReadAsStringAsync().Result, "text/plain");
                    }
                    catch (Exception ex)
                    {
                        return this.Content(ex.Message.ToString(), "text/plain");
                    }
                }
            }
            return this.Content("{}", "text/plain");
        }

        [Route("api/remittance/bank/countries"), HttpPost, Produces("application/json")]
        [EnableCors("MyPolicy")]
        public async Task<IActionResult> getBankCountries([FromBody] JObject RequestData)
        {
            if (RequestData != null)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("", "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/PjxBdXRoZW50aWNhdGlvbj4NCiAgPElkPjMyZDc3OTMzLTczMmQtNDliNi1hOWViLWZkNmVlY2E3ZDdmMTwvSWQ+DQogIDxVc2VyTmFtZT5ENWVYZFZTQlR6ZnM3NmdtbUVpS1JLbzFMQXlmckZKVlNBMnhzV01iOEptMUJEdUdWaFh2dXZYNlVMWmlvalN3akdJVGh5dStlR1VxZ3RlUVhyb0RQdG90TFArb1IxWEFYUE1SMXVQc0d5VjJ0eVEvZkZLaWJVUkxzbHFtN1Q1cklRaXZNU1ZUeUluRHBhUW5EZVk2K2pBVHREYjUvQ2hmQlhEejV4YXhlK0U9PC9Vc2VyTmFtZT4NCiAgPFBhc3N3b3JkPkNnQU1lZU1LSGZiSjgzUHAzTG9NLzdkY0JYa3ZLUGk0MVhmVy9FMUdwSWlCUmtFYVRlRVJYay8xVFkyc1NZVGtWWm02bXdIYmFHUnIxL0dHNyswdlJJdkxBT2pZNjJGNVIySXZSRE9HZnE5Z2dRczBKN0hpbUVLZ3E0c2ppV1VVdkY1d0xySE9tbWR0L1Qvekp3c1pmdUtUVDUrdU40M3JmSEg1dU92MFpJTT08L1Bhc3N3b3JkPg0KICA8QnJhbmNoSWQ+TGc2LzliZ0RENEVHRUY0SVZXNy83ZjlmalUwZUNpZHVmL0dVRVlERHY5eDkybTJUWHFzSWJBZ2loMU1zd3BMNFF5VmdUYS9GSUhaZXhFLzdrclBtak5qZlhFOXhNRWdrbUs3Wm41NDg0cVNYYTltK2JZY21aVURIb2NrcW1FVkFaYnB0NkRIOThGSldCRkRwWkpKY1NNR3dPT29HNVk5amNhN2tyejNJS1JzPTwvQnJhbmNoSWQ+DQo8L0F1dGhlbnRpY2F0aW9uPg==");
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization","PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/PjxBdXRoZW50aWNhdGlvbj4NCiAgPElkPjMyZDc3OTMzLTczMmQtNDliNi1hOWViLWZkNmVlY2E3ZDdmMTwvSWQ+DQogIDxVc2VyTmFtZT5ENWVYZFZTQlR6ZnM3NmdtbUVpS1JLbzFMQXlmckZKVlNBMnhzV01iOEptMUJEdUdWaFh2dXZYNlVMWmlvalN3akdJVGh5dStlR1VxZ3RlUVhyb0RQdG90TFArb1IxWEFYUE1SMXVQc0d5VjJ0eVEvZkZLaWJVUkxzbHFtN1Q1cklRaXZNU1ZUeUluRHBhUW5EZVk2K2pBVHREYjUvQ2hmQlhEejV4YXhlK0U9PC9Vc2VyTmFtZT4NCiAgPFBhc3N3b3JkPkNnQU1lZU1LSGZiSjgzUHAzTG9NLzdkY0JYa3ZLUGk0MVhmVy9FMUdwSWlCUmtFYVRlRVJYay8xVFkyc1NZVGtWWm02bXdIYmFHUnIxL0dHNyswdlJJdkxBT2pZNjJGNVIySXZSRE9HZnE5Z2dRczBKN0hpbUVLZ3E0c2ppV1VVdkY1d0xySE9tbWR0L1Qvekp3c1pmdUtUVDUrdU40M3JmSEg1dU92MFpJTT08L1Bhc3N3b3JkPg0KICA8QnJhbmNoSWQ+TGc2LzliZ0RENEVHRUY0SVZXNy83ZjlmalUwZUNpZHVmL0dVRVlERHY5eDkybTJUWHFzSWJBZ2loMU1zd3BMNFF5VmdUYS9GSUhaZXhFLzdrclBtak5qZlhFOXhNRWdrbUs3Wm41NDg0cVNYYTltK2JZY21aVURIb2NrcW1FVkFaYnB0NkRIOThGSldCRkRwWkpKY1NNR3dPT29HNVk5amNhN2tyejNJS1JzPTwvQnJhbmNoSWQ+DQo8L0F1dGhlbnRpY2F0aW9uPg==");
                        var response = new HttpResponseMessage();
                        //await client.PostAsync(new Uri("https://demo-api.transfast.net/api/catalogs/countries"), new StringContent(RequestData.ToString(), Encoding.UTF8, "application/json"))
                        await client.GetAsync(new Uri("https://demo-api.transfast.net/api/catalogs/countries"))
                              .ContinueWith(responseTask =>
                              {
                                  response = responseTask.Result;
                              });
                        return this.Content(response.Content.ReadAsStringAsync().Result, "text/plain");
                    }
                    catch (Exception ex)
                    {
                        return this.Content(ex.Message.ToString(), "text/plain");
                    }
                }
            }
            return this.Content("{}", "text/plain");
        }

        public class CurrRate
        {
            [JsonProperty("curr_code")]
            public string curr_code { get; set; }
            [JsonProperty("bid")]
            public string bid { get; set; }
            [JsonProperty("offer")]
            public string offer { get; set; }
            [JsonProperty("dailyhigh")]
            public string dailyhigh { get; set; }
            [JsonProperty("dailylow")]
            public string dailylow { get; set; }
        }

        [Route("api/OCBCfx/getCurrRates")]
        [EnableCors("MyPolicy")]
        public IActionResult getOCBCfxCurrRates()
        {
            return this.Content(AspNetMaker2017.Startup.CurrencyRatesOCBCfx, "text/plain");
        }

        [Route("api/OCBCfx/setCurrRates")]
        public void setOCBCfxCurrRates()
        {
            Dictionary<string, Microsoft.Extensions.Primitives.StringValues>  curr_rates = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(Request.QueryString.ToString());
            var curr_code = curr_rates["curr_code"].ToString();
            var inverse_curr_code = curr_rates["curr_code"].ToString().Substring(3, 3) + curr_rates["curr_code"].ToString().Substring(0, 3);

            if (AspNetMaker2017.Startup.CurrencyRatesOCBCfx + "" !="") { 
                var CurrencyRatesOCBCfx_json = JsonConvert.DeserializeObject<List<CurrRate>>("["+AspNetMaker2017.Startup.CurrencyRatesOCBCfx+"]");
                AspNetMaker2017.Startup.CurrencyRatesOCBCfx = JsonConvert.SerializeObject(CurrencyRatesOCBCfx_json.Where(i => i.curr_code != inverse_curr_code && i.curr_code != curr_code)).Replace("[","").Replace("]","");
                //AspNetMaker2017.Startup.CurrencyRatesOCBCfx = JsonConvert.SerializeObject(CurrencyRatesOCBCfx_json).Replace("[", "").Replace("]", "");
                //AspNetMaker2017.Startup.CurrencyRatesOCBCfx = "[" + CurrencyRatesOCBCfx_json.Where(i => i.curr_code != inverse_curr_code && i.curr_code != curr_code).ToString() + "]";
            }

            AspNetMaker2017.Startup.CurrencyRatesOCBCfx += JsonConvert.SerializeObject(Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(Request.QueryString.ToString())).Replace("[","").Replace("]","");

            AspNetMaker2017.Startup.CurrencyRatesOCBCfx += "" +
             "{" +
             "\"curr_code\": \"" + inverse_curr_code + "\", " +
             "\"bid\": \"" + Math.Round(1 / Decimal.Parse(curr_rates["bid"].ToString()), 5).ToString() + "\", " +
             "\"offer\": \"" + Math.Round(1 / Decimal.Parse(curr_rates["offer"].ToString()), 5).ToString() + "\", " +
             "\"dailyhigh\": \"" + Math.Round(1 / Decimal.Parse(curr_rates["dailyhigh"].ToString()), 5).ToString() + "\", " +
             "\"dailylow\": \"" + Math.Round(1 / Decimal.Parse(curr_rates["dailylow"].ToString()), 5).ToString() + "\" " +
             "}";

            AspNetMaker2017.Startup.CurrencyRatesOCBCfx = AspNetMaker2017.Startup.CurrencyRatesOCBCfx.Replace("}{", "},{");
        }

        [Route("api/setCurrRates")]
        public void setCurrRates()
        {
            //  https://www.tradingview.com/widget/market-overview/
            //  https://s.tradingview.com/marketoverviewwidgetembed/#%7B%22showChart%22%3Afalse%2C%22locale%22%3A%22en%22%2C%22width%22%3A%22100%25%22%2C%22height%22%3A%22100%25%22%2C%22tabs%22%3A%5B%7B%22title%22%3A%22Forex%22%2C%22symbols%22%3A%5B%7B%22s%22%3A%22FX_IDC%3AEURSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3AAUDSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3AGBPSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3AJPYSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3ACADSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3ACHFSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3ANZDSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3AINRSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3AMYRSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3APHPSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3ATHBSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3AKRWSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3AHKDSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3AAEDSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3ASARSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3AIDRSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3AVNDSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3ADKKSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3ANOKSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3ASEKSGD%22%7D%2C%7B%22s%22%3A%22FX_IDC%3AZARSGD%22%7D%5D%7D%5D%2C%22utm_source%22%3A%22www.tradingview.com%22%2C%22utm_medium%22%3A%22widget%22%2C%22utm_campaign%22%3A%22marketoverview%22%7D
            AspNetMaker2017.Startup.CurrencyRates = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(Request.QueryString.ToString());
            AspNetMaker2017.Startup.CurrencyRates.Remove("OANDA:SGDCHF");
            //var jsonCurrencyRates = "update currency set currency.[TTSpotRate] = currency_spot_rates.[value] from currency, OPENJSON('" + JsonConvert.SerializeObject(AspNetMaker2017.Startup.CurrencyRates).Replace(" ","").Replace("[", "").Replace("]", "") + "')  as currency_spot_rates where currency_spot_rates.[key] COLLATE DATABASE_DEFAULT LIKE '%' + currency.CurrencyCode + '%' ";
            Language = Language ?? new cLanguage();
            ew_Execute("update currency set currency.[TTSpotRate] = currency_spot_rates.[value] from currency, OPENJSON('" + JsonConvert.SerializeObject(AspNetMaker2017.Startup.CurrencyRates).Replace(" ", "").Replace("[", "").Replace("]", "") + "')  as currency_spot_rates where currency_spot_rates.[key] COLLATE DATABASE_DEFAULT LIKE '%' + currency.CurrencyCode + '%' ");
        }

        // menu
        [Route("ewmenu")]
		[Route("Home/ewmenu")]
		public IActionResult ewmenu()
		{
			return View();
		}

		// mobilemenu
		[Route("ewmobilemenu")]
		[Route("Home/ewmobilemenu")]
		public IActionResult ewmobilemenu()
		{
			return View();
		}

		// file
		[Route("ewfile")]
		[Route("Home/ewfile")]
		public IActionResult ewfile()
		{

			// Create page object
			_file = new c_file(this);

			// Execute page
			return _file.Page_Init() ?? _file.Page_Main();
		}

		// lookup
		[Route("ewlookup")]
		[Route("Home/ewlookup")]
		public IActionResult ewlookup()
		{

			// Create page object
			_lookup = new c_lookup(this);

			// Execute page
			return _lookup.Page_Init() ?? _lookup.Page_Main();
		}

		// modallookup
		[Route("ewmodallookup")]
		[Route("Home/ewmodallookup")]
		public IActionResult ewmodallookup()
		{

			// Create page object
			_modallookup = new c_modallookup(this);

			// Execute page
			return _modallookup.Page_Init() ?? _modallookup.Page_Main();
		}

		// ewupload
		[Route("ewupload")]
		[Route("Home/ewupload")]
		public IActionResult ewupload()
		{

			// Create page object
			_ewupload = new c_ewupload(this);

			// Execute page
			return _ewupload.Page_Init() ?? _ewupload.Page_Main();
		}

		// session
		[Route("ewsession")]
		[Route("Home/ewsession")]
		public IActionResult ewsession()
		{

			// Create page object
			_session = new c_session(this);

			// Execute page
			return _session.Page_Init() ?? _session.Page_Main();
		}

		// ewemail
		[Route("ewemail")]
		[Route("Home/ewemail")]
		public IActionResult ewemail()
		{
			return View();
		}

		// default
		[Route("")]
		[Route("Index")]
		[Route("Home/Index")]
		public IActionResult Index()
		{

			// Create page object
			_Index = new c_Index(this);

			// Execute page
			return _Index.Page_Init() ?? _Index.Page_Main();
		}

		// error
		[Route("Error")]
		[Route("Home/Error")]
		public IActionResult Error()
		{

			// Create page object
			_error = new c_error(this);

			// Execute page
			return _error.Page_Init() ?? _error.Page_Main();
		}
	}
}
