using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Cryptography;
using MerchantApp.Models;
using Newtonsoft;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using com.fss.plugin;
using java.nio.channels;
using System.Net.Http;
using sun.security.util;
using Newtonsoft.Json.Linq;
using Microsoft.Ajax.Utilities;
using FSS.Pipe;
using iPayBenefitPipe = FSS.Pipe.iPayBenefitPipe;
using System.Configuration;
using System.Web.Util;
using javax.servlet.jsp.tagext;

namespace MerchantApp.Controllers
{

    public class HomeController : Controller
    {
        //public ActionResult Benefit()
        //{
        //    return View();
        //}
        public ActionResult InitiatePayment(decimal amount)
        
        {
            string amt = amount.ToString();
            // Create an instance of iPayBenefitPipe
            var pipe = new FSS.Pipe.iPayBenefitPipe();
            int OrderIDbp = new Random().Next(10000, 99999);

            string successURL = ConfigurationManager.AppSettings["ResponseURL"].ToString();
            string errorURL = ConfigurationManager.AppSettings["ErrorURL"].ToString();

            // Set required properties
            pipe.setAmt(amt); // Amount
            pipe.setCurrencyCode("048"); // Currency code (e.g., BHD)
            pipe.setAction("1"); // Action (e.g., 1 for Purchase)
            pipe.setPassword("02589752");
            pipe.setTranportalID("02589752");
            pipe.setTrackId(OrderIDbp.ToString());
        //pipe.setResponseURL(successURL);
        //pipe.setErrorURL(errorURL);

        
            // Optionally set UDF fields or other parameters
            pipe.setUdf1("1");
            pipe.setUdf2("2");
            pipe.setUdf2("3");
            pipe.setUdf2("4");
            pipe.setUdf2("5");
            pipe.setResponseURL("https://www.paymentgateway.premium-pos.com/Home/ParseResponse");
            pipe.setErrorURL("https://www.paymentgateway.premium-pos.com/Home/ParseResponseError");
            try
            {
                // Perform the transaction
                var paymentResponse = pipe.PerformTransaction();

                var red = paymentResponse.result; 
               

                if (!string.IsNullOrEmpty(paymentResponse.status) && paymentResponse.status == "1")
                {
                    // Redirect user to a confirmation page
                    ViewBag.Message = "Payment initiated successfully.";
                    ViewBag.TransactionId = pipe.getTransactionID();
                    return Redirect(red);
                     
                }
                else
                {
                    // Handle failure
                    ViewBag.Message = "Payment initiation failed.";
                    ViewBag.Error = paymentResponse.errorText;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ViewBag.Message = "An error occurred.";
                ViewBag.Error = ex.Message;
            }

            return View();
        }
      
        public ActionResult ParseResponse()
        {
            return Content("REDIRECT=https://www.paymentgateway.premium-pos.com/Home/Result");
        }
        public ActionResult Success()
        {
            return Content("Success");
        }
        
        [HttpPost]
        public ActionResult Result()
        {
            BenefitResponse responseModel = new BenefitResponse();
            iPayBenefitPipe pipe = new iPayBenefitPipe();
            string trandata = Request.Form["trandata"].ToString();

            string paymentID = "";
            string result = "";
            string responseCode = "";
            string response = "";
            string transactionID = "";
            string referenceID = "";
            string trackID = "";
            string amount = "";
            string UDF1 = "";
            string UDF2 = "";
            string UDF3 = "";
            string UDF4 = "";
            string UDF5 = "";
            string authCode = "";
            string postDate = "";
            string errorText = "";
            

            if (!String.IsNullOrEmpty(Request.Form["trandata"]))
            {
                
                int parse = pipe.ParseResponse(trandata);
                if (parse == 1)
                {
                    paymentID = pipe.getPaymentID();
                    result = pipe.getResult();
                    transactionID = pipe.getTransactionID();
                    referenceID = pipe.getReferenceID();
                    trackID = pipe.getTrackId();
                    amount = pipe.getAmt();
                    UDF1 = pipe.getUdf1();
                    UDF2 = pipe.getUdf2();
                    UDF3 = pipe.getUdf3();
                    UDF4 = pipe.getUdf4();
                    UDF5 = pipe.getUdf5();
                    authCode = pipe.getAuthCode();
                    postDate = pipe.getTranDate();
                    responseCode =pipe.getAuthRespCode();
                }
                else
                {
                    errorText = pipe.getErrorText();
                }
            }
            else if (Request.Form["ErrorText"] != null)
            {
                paymentID = Request.Form["paymentid"];
                trackID = Request.Form["Trackid"];
                amount = Request.Form["amt"];
                UDF1 = Request.Form["UDF1"];
                UDF2 = Request.Form["UDF2"];
                UDF3 = Request.Form["UDF3"];
                UDF4 = Request.Form["UDF4"];
                UDF5 = Request.Form["UDF5"];
                errorText = Request.Form["ErrorText"];
            }
            else
            {
                errorText = "Unknown Exception";
            }

            // Remove any HTML/CSS/JavaScript from the page. Also, you MUST NOT write anything on the page EXCEPT the word "REDIRECT=" (in upper-case only) followed by a URL.
            // If anything else is written on the page then you will not be able to complete the process.
            if (result == "CAPTURED")
            {
                responseModel.status = "1";
                responseModel.result = "Success";

                //string jsonResponse = JsonConvert.SerializeObject(responseModel);
                //return Content("REDIRECT=https://www.paymentgateway.premium-pos.com/Home/Success");
                //return Content(jsonResponse);


                string redirectUrl = $"https://www.paymentgateway.premium-pos.com/Home/Success?status={responseModel.status}&result={Uri.EscapeDataString(responseModel.result)}";

                // Redirect the user to the URL with query parameters
                return Redirect(redirectUrl);

            }
            else if (result == "NOT CAPTURED" || result == "CANCELED" || result == "DENIED BY RISK" || result == "HOST TIMEOUT")
            {
                if (result == "NOT CAPTURED")
                {
                    responseModel.status = "0";
                    switch (responseCode)
                    {
                        case "05":
                            response = "Please contact issuer";
                            break;
                        case "14":
                            response = "Invalid card number";
                            break;
                        case "33":
                            response = "Expired card";
                            break;
                        case "36":
                            response = "Restricted card";
                            break;
                        case "38":
                            response = "Allowable PIN tries exceeded";
                            break;
                        case "51":
                            response = "Insufficient funds";
                            break;
                        case "54":
                            response = "Expired card";
                            break;
                        case "55":
                            response = "Incorrect PIN";
                            break;
                        case "61":
                            response = "Exceeds withdrawal amount limit";
                            break;
                        case "62":
                            response = "Restricted Card";
                            break;
                        case "65":
                            response = "Exceeds withdrawal frequency limit";
                            break;
                        case "75":
                            response = "Allowable number PIN tries exceeded";
                            break;
                        case "76":
                            response = "Ineligible account";
                            break;
                        case "78":
                            response = "Refer to Issuer";
                            break;
                        case "91":
                            response = "Issuer is inoperative";
                            break;
                        default:
                            // for unlisted values, please generate a proper user-friendly message
                            response = "Unable to process transaction temporarily. Try again later or try using another card.";
                            break;
                    }
                    responseModel.result = response;
                }
                else if (result == "CANCELED")
                {
                    responseModel.status = "0";
                    responseModel.result = "Transaction was canceled by user.";
                }
                else if (result == "DENIED BY RISK")
                {
                    responseModel.status = "0";
                    responseModel.result = "Maximum number of transactions has exceeded the daily limit.";
                }
                else if (result == "HOST TIMEOUT")
                {
                    responseModel.status = "0";
                    responseModel.result = "Unable to process transaction temporarily. Try again later.";
                }

                string redirectUrl = $"https://www.paymentgateway.premium-pos.com/Home/declined?status={responseModel.status}&result={Uri.EscapeDataString(responseModel.result)}";

                // Redirect the user to the URL with query parameters
                return Redirect(redirectUrl);

                //string jsonResponse = JsonConvert.SerializeObject(responseModel);
                ////return Content("REDIRECT=https://www.paymentgateway.premium-pos.com/Home/declined");
                //return Content(jsonResponse);
            }
            else
            {
                if (result == "CANCELED")
                {
                    responseModel.status = "0";
                    responseModel.result = "Transaction was canceled by user.";
                }
                if (result == "DENIED BY RISK")
                {
                    responseModel.status = "0";
                    responseModel.result = "Maximum number of transactions has exceeded the daily limit.";
                }
                if (result == "HOST TIMEOUT")
                {
                    responseModel.status = "0";
                    responseModel.result = "Unable to process transaction temporarily. Try again later.";
                }

                string redirectUrl = $"https://www.paymentgateway.premium-pos.com/Home/declined?status={responseModel.status}&result={Uri.EscapeDataString(responseModel.result)}";

                // Redirect the user to the URL with query parameters
                return Redirect(redirectUrl);

                //string jsonResponse = JsonConvert.SerializeObject(responseModel);
                ////return Content("REDIRECT=https://www.paymentgateway.premium-pos.com/Home/ResultFailed");
                //return Content(jsonResponse);
            }
            //return Content(response);
        }
        public ActionResult declined()
        {
             
            return Content("Declined");
        }
        public ActionResult paymentdeclined()
        {

            return Content("declined");
        }
        public ActionResult ResultFailed()
        {
             
            return Content("Failed");
        }
        private ActionResult ProcessResponse(bool isError)
        {
            string trandata = Request.Form["trandata"];
            //string trandata = Request.QueryString["trandata"]; // Explicitly use QueryString
            string errorText = "";

            try
            {
                if (string.IsNullOrEmpty(trandata))
                {
                    return Content("Error: Transaction data is empty.");
                }

                // Decrypt `trandata` using the resource key
                string resourceKey = "50298093185450298093185450298093"; // Replace with secure configuration
                resources res = new resources();
                trandata = decrypt(StringToByteArray(trandata), resourceKey, res.IV);

                if (!string.IsNullOrEmpty(trandata))
                {
                    // Parse the decrypted `trandata`
                    JArray json = JArray.Parse(HttpUtility.UrlDecode(trandata));
                    getDecryptedValues(((JObject)json.First).ToObject<iPayBenefitPipe>());

                    // Return appropriate response based on the `isError` flag
                    if (isError)
                    {
                        return Content("Error: Payment not successful.");
                    }

                    return Content("REDIRECT=https://paymentgateway.premium-pos.com/Home/ParseResponse");
                }
                else
                {
                    return Content("Error: Decrypted transaction data is empty.");
                }
            }
            catch (Exception ex)
            {
                errorText = ex.Message;

                // Log the exception (ensure logging is set up in your project)
                Console.WriteLine($"Error processing transaction data: {ex}");

                return Content("Error: Internal server error.");
            }
        }
        //public string ParseResponse()
        //{
        //   string trandata = Request.Form["trandata"];
        //    //string trandata = Request.QueryString["trandata"];

        //    string errorText = "";
        //    try
        //    {                
        //        string resourceKey = "50298093185450298093185450298093";
        //        resources res = new resources();
        //        trandata = decrypt(StringToByteArray(trandata), resourceKey, res.IV);

        //        if (!String.IsNullOrEmpty(trandata))
        //        {
        //            JArray json = JArray.Parse(HttpUtility.UrlDecode(trandata));//,Encoding.UTF8));
        //            //JArray json = JArray.Parse(Uri.EscapeDataString(trandata));//(trandata,Encoding.UTF8));



        //            getDecryptedValues(((JObject)json.First).ToObject<iPayBenefitPipe>());

        //            return trandata;
        //        }
        //        else
        //        {

        //            return "";
        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        errorText = Ex.Message.ToString();
        //        return "trandata is empty";
        //    }

        //}
        //public ActionResult ParseResponseError()
        //{

        //    string trandata = Request.Form["trandata"];
        //    //string trandata = Request.QueryString["trandata"];

        //    string errorText = "";
        //    try
        //    {
        //        string resourceKey = "50298093185450298093185450298093";
        //        resources res = new resources();
        //        trandata = decrypt(StringToByteArray(trandata), resourceKey, res.IV);

        //        if (!String.IsNullOrEmpty(trandata))
        //        {
        //            JArray json = JArray.Parse(HttpUtility.UrlDecode(trandata));//,Encoding.UTF8));
        //            //JArray json = JArray.Parse(Uri.EscapeDataString(trandata));//(trandata,Encoding.UTF8));



        //            getDecryptedValues(((JObject)json.First).ToObject<iPayBenefitPipe>());

        //            return Content("Error: Payment not successful.");
        //        }
        //        else
        //        {

        //            return Content("Error: Payment not successful.");
        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        errorText = Ex.Message.ToString();
        //        return Content("Error: Internal server error.");
        //    }

        //}
        private void getDecryptedValues(iPayBenefitPipe obj)
        {
            
            string paymentId = "";
            string date = "";
            string result = "";
            string transId = "";
            string @ref = "";
            string authCode = "";
            string authRespCode = "";
            string udf1 = "";
            string udf2 = "";
            string udf3 = "";
            string udf4 = "";
            string udf5 = "";
            string trackId = "";
            paymentId = obj.getPaymentID();
            date = obj.getTranDate();
            result = obj.getResult();
            transId = obj.getTransactionID();
            @ref = obj.getReferenceID();
            authCode = obj.getAuthCode();
            authRespCode = obj.getAuthRespCode();
            udf1 = obj.getUdf1();
            udf2 = obj.getUdf2();
            udf3 = obj.getUdf3();
            udf4 = obj.getUdf4();
            udf5 = obj.getUdf5();
            trackId = obj.getTrackId();
        }
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            .ToArray();
        }
        private static AesManaged CreateAes(string resourceKey, string initVector)
        {

            var aes = new AesManaged();
            aes.Key = System.Text.Encoding.UTF8.GetBytes(resourceKey); //UTF8-Encoding
            aes.IV = System.Text.Encoding.UTF8.GetBytes(initVector);//UT8-Encoding
            return aes;
        }
        public static string decrypt(byte[] text, string resourceKey, string initVector)
        {
            using (var aes = CreateAes(resourceKey, initVector))
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor();
                using (MemoryStream ms = new MemoryStream(text))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            return reader.ReadToEnd();

                        }
                    }
                }

            }
        }
        //public ActionResult Response(int OrderID)
        //{

        //        string trandata = "";
        //        string paymentID = "";
        //        string result = "";
        //        string responseCode = "";
        //        string response = "";
        //        string transactionID = "";
        //        string referenceID = "";
        //        string trackID = "";
        //        string amount = "";
        //        string UDF1 = "";
        //        string UDF2 = "";
        //        string UDF3 = "";
        //        string UDF4 = "";
        //        string UDF5 = "";
        //        string authCode = "";
        //        string postDate = "";
        //        string errorCode = "";
        //        string errorText = "";
        //        if (OrderID == 0)
        //        {
        //            return Content("OrderID is 0");
        //        }
        //        else {

        //            if (Request.Form["trandata"] !=null)
        //            {
        //                var pipe = new FSS.Pipe.iPayBenefitPipe();
        //            pipe.setResourceKey("50298093185450298093185450298093"); // Set your resource key
        //            trandata = Request.Form["trandata"].ToString();                  
        //            int parse = pipe.ParseResponse(trandata);
        //            if (parse == 0)
        //            {
        //                paymentID = pipe.getPaymentID();
        //                result = pipe.getResult();
        //                responseCode = pipe.getAuthRespCode();
        //                transactionID = pipe.getTransactionID();
        //                referenceID = pipe.getReferenceID();
        //                trackID = pipe.getTrackId();
        //                amount = pipe.getAmt();
        //                UDF1 = pipe.getUdf1();
        //                UDF2 = pipe.getUdf2();
        //                UDF3 = pipe.getUdf3();
        //                UDF4 = pipe.getUdf4();
        //                UDF5 = pipe.getUdf5();
        //                authCode = pipe.getAuthCode();
        //                postDate = pipe.getTranDate();
        //                errorCode = pipe.getErrorCode();
        //                errorText = pipe.getErrorText();
        //            }
        //            else
        //            {
        //                errorText = pipe.getErrorText();
        //            }
        //        }
        //        else if (Request.Form["ErrorText"] != null)
        //        {
        //            paymentID = Request.Form["paymentid"];
        //            trackID = Request.Form["Trackid"];
        //            amount = Request.Form["amt"];
        //            UDF1 = Request.Form["UDF1"];
        //            UDF2 = Request.Form["UDF2"];
        //            UDF3 = Request.Form["UDF3"];
        //            UDF4 = Request.Form["UDF4"];
        //            UDF5 = Request.Form["UDF5"];
        //            errorText = Request.Form["ErrorText"];
        //        }
        //        else
        //        {
        //            errorText = "Unknown Exception";
        //        }


        //        if (result == "CAPTURED")
        //        {
        //            var url = "https://paymentgateway.premium-pos.com/Home/Response?OrderID=" + OrderID;
        //            return Content("REDIRECT=" + url);
        //        }
        //        else if (result == "NOT CAPTURED" || result == "CANCELED" || result == "DENIED BY RISK" || result == "HOST TIMEOUT")
        //        {
        //            if (result == "NOT CAPTURED")
        //            {
        //                switch (responseCode)
        //                {
        //                    case "05":
        //                        response = "Please contact issuer";
        //                        break;

        //                    case "38":
        //                        response = "Allowable PIN tries exceeded";
        //                        break;
        //                    case "51":
        //                        response = "Insufficient funds";
        //                        break;
        //                    case "54":
        //                        response = "Expired card";
        //                        break;
        //                    case "55":
        //                        response = "Incorrect PIN";
        //                        break;
        //                    case "61":
        //                        response = "Exceeds withdrawal amount limit";
        //                        break;
        //                    case "62":
        //                        response = "Restricted Card";
        //                        break;
        //                    case "65":
        //                        response = "Exceeds withdrawal frequency limit";
        //                        break;
        //                    case "75":
        //                        response = "Allowable number PIN tries exceeded";
        //                        break;
        //                    case "76":
        //                        response = "Ineligible account";
        //                        break;
        //                    case "78":
        //                        response = "Refer to Issuer";
        //                        break;
        //                    case "91":
        //                        response = "Issuer is inoperative";
        //                        break;
        //                    default:
        //                        // for unlisted values, please generate a proper user-friendly message
        //                        response = "Unable to process transaction temporarily. Try again later or try using another card.";
        //                        break;
        //                }
        //            }
        //            else if (result == "CANCELED")
        //            {
        //                response = "Transaction was canceled by user.";
        //            }
        //            else if (result == "DENIED BY RISK")
        //            {
        //                response = "Maximum number of transactions has exceeded the daily limit.";
        //            }
        //            else if (result == "HOST TIMEOUT")
        //            {
        //                response = "Unable to process transaction temporarily. Try again later.";
        //            }
        //            return Content("REDIRECT=https://paymentgateway.premium-pos.com/Home/Response?OrderID=0");
        //        }
        //        else
        //        {

        //            return Content("REDIRECT=https://paymentgateway.premium-pos.com/Home/Response?OrderID=0");
        //        }

        //        //    if (result == 1)
        //        //{
        //        //    ViewBag.Message = "Payment successful!";
        //        //    ViewBag.TransactionId = pipe.getTransactionID();
        //        //    ViewBag.AuthCode = pipe.getAuthCode();
        //        //}
        //        //else
        //        //{
        //        //    ViewBag.Message = "Payment failed.";
        //        //    ViewBag.Error = pipe.getErrorText();
        //        //}
        //    }
        //    return View();
        //}

        public ActionResult Error()
        {
            // Handle errors from the payment gateway
             
            ViewBag.Message = "An unknown error occurred.";
            return View();
        }
        //private const string ResourceKey = "50298093185450298093185450298093";
        //private const string InitializationVector = "PGKEYENCDECIVSPC";
        //private const string PaymentEndpoint = "https://test.benefit-gateway.bh/payment/API/hosted.htm";

        //public ActionResult Benefit()
        // {
        //    return View();
        // }
        //[HttpGet]
        //public ActionResult Success()
        //{
        //    var content = "Payment successful! Thank you for your purchase.";
        //    return Json(content);
        //}

        //[HttpGet]
        //public ActionResult Failure()
        //{
        //    var content = "Payment failed or canceled. Please try again.";
        //    return Json(content);
        //}

        //[HttpPost]
        //public ActionResult InitiatePayment()
        //{
        //    // Plain trandata
        //    var plainTrandata = new
        //    {
        //        amt = "12.00",
        //        action = "1",
        //        password = "02589752",
        //        id = "02589752",
        //        currencycode = "048",
        //        trackId = "123",
        //        udf1 = "",
        //        udf2 = "udf2text",
        //        udf3 = "udf3text",
        //        udf4 = "udf4text",
        //        udf5 = "udf5text",
        //        responseURL = "http://chongspgbenefit.premium-pos.com/home/Success",
        //        errorURL = "http://chongspgbenefit.premium-pos.com/home/Failure"
        //    };

        //    string plainJson = JsonConvert.SerializeObject(plainTrandata);
        //    string encryptedTrandata = EncryptionHelper.Encrypt(plainJson, ResourceKey, InitializationVector);

        //    var benefitRequest = new BenefitRequest
        //    {
        //        id = "02589752",
        //        trandata = encryptedTrandata
        //    };

        //    // Send request to Benefit Payment Gateway
        //    using (var client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Add("Accept", "application/json");
        //        var content = new StringContent(JsonConvert.SerializeObject(benefitRequest), Encoding.UTF8, "application/json");
        //        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        //        var response = client.PostAsync(PaymentEndpoint, content).Result;

        //        string responseData = response.Content.ReadAsStringAsync().Result;
        //        Console.WriteLine(responseData);  

        //        if (response.IsSuccessStatusCode)
        //        {
        //            try
        //            {
        //                var benefitResponse = JsonConvert.DeserializeObject<BenefitResponse>(responseData);
        //                if (benefitResponse.status == "1")
        //                {
        //                    string paymentPageUrl = benefitResponse.result.Split(':')[1];
        //                    return Redirect(paymentPageUrl);
        //                }
        //                else
        //                {
        //                    ViewBag.Error = benefitResponse.errorText;
        //                }
        //            }
        //            catch (JsonException ex)
        //            {

        //                ViewBag.Error = "Error parsing response: " + ex.Message;
        //                ViewBag.RawResponse = responseData;
        //            }
        //        }
        //        else
        //        {

        //            ViewBag.Error = "Payment request failed with status: " + response.StatusCode;
        //            ViewBag.RawResponse = responseData; 
        //        }
        //    }

        //    return View();

        //}
        //[HttpGet]
        //public ActionResult PaymentResponse(string trandata)
        //{
        //    string decryptedData = EncryptionHelper.Decrypt(trandata, ResourceKey, InitializationVector);
        //    ViewBag.Response = decryptedData;
        //    return View();
        //}


    }
   
}