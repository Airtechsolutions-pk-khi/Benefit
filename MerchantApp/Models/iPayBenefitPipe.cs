using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FSS.Pipe
{
    public class iPayBenefitPipe
    {
        int transID = new Random().Next(10000, 99999);
        [JsonProperty]
        protected string amt = "";
        [JsonProperty]
        protected string action = "";
        [JsonProperty]
        protected string password = "";
        [JsonProperty]
        protected string id = "";
        [JsonProperty]
        protected string resourceKey = "43507724619143507724619143507724";
        [JsonProperty]
        protected string currencycode = "";
        [JsonProperty]
        protected string trackId = "";
        [JsonProperty]
        protected string udf1 = "";
        [JsonProperty]
        protected string udf2 = "";
        [JsonProperty]
        protected string udf3 = "";
        [JsonProperty]
        protected string udf4 = "";
        [JsonProperty]
        protected string udf5 = "";
        [JsonProperty]
        protected string expYear = "";
        [JsonProperty]
        protected string expMonth = "";
        [JsonProperty]
        protected string member = "";
        [JsonProperty]
        protected string cardNo = "";
        [JsonProperty]
        protected string cardType = "D";
        [JsonProperty]
        protected string paymentData = "";
        [JsonProperty]
        protected string paymentMethod = "";
        [JsonProperty]
        protected string transactionIdentifier = "";
        [JsonProperty]
        protected string responseURL = "";
        [JsonProperty]
        protected string errorURL = "";
        [JsonProperty]
        protected string paymentId = "";
        [JsonProperty]
        protected string trandata = "";
        [JsonProperty]
        protected string result = "";
        [JsonProperty]
        protected string authRespCode = "";
        [JsonProperty]
        protected string response = "";
        [JsonProperty]
        protected string transId = "";
        [JsonProperty]
        protected string @ref = "";
        [JsonProperty]
        protected string authCode = "";
        [JsonProperty]
        protected string date = "";
        [JsonProperty]
        protected string errorCode = "";
        [JsonProperty]
        protected string errorText = "";

        public iPayBenefitPipe() { }


        public void setAmt(String Amt)
        {
            this.amt = Amt;
        }
        public void setResourceKey(String ResourceKey)
        {
            this.resourceKey = ResourceKey;
        }
        public void setAction(String Action)
        {
            this.action = Action;
        }
        public void setPassword(String Password)
        {
            this.password = Password;
        }
        public void setTranportalID(String TranportalID)
        {
            this.id = TranportalID;
        }
        public void setCurrencyCode(String CurrencyCode)
        {
            this.currencycode = CurrencyCode;
        }
        public void setTrackId(String TrackId)
        {
            this.trackId = TrackId;
        }
        public void setUdf1(String Udf1)
        {
            this.udf1 = Udf1;
        }
        public void setUdf2(String Udf2)
        {
            this.udf2 = Udf2;
        }
        public void setUdf3(String Udf3)
        {
            this.udf3 = Udf3;
        }
        public void setUdf4(String Udf4)
        {
            this.udf4 = Udf4;
        }
        public void setUdf5(String Udf5)
        {
            this.udf5 = Udf5;
        }
        public void setExpYear(String ExpYear)
        {
            this.expYear = ExpYear;
        }
        public void setExpMonth(String ExpMonth)
        {
            this.expMonth = ExpMonth;
        }
        public void setMember(String Member)
        {
            this.member = Member;
        }
        public void setCardNo(String CardNo)
        {
            this.cardNo = CardNo;
        }
        public void setCardType(String CardType)
        {
            this.cardType = CardType;
        }
        public void setPaymentData(String PaymentData)
        {
            this.paymentData = PaymentData;
        }
        public void setPaymentMethod(String PaymentMethod)
        {
            this.paymentMethod = PaymentMethod;
        }
        public void setTransactionIdentifier(String TransactionIdentifier)
        {
            this.transactionIdentifier = TransactionIdentifier;
        }
        public void setResponseURL(String ResponseURL)
        {
            this.responseURL = ResponseURL;
        }
        public void setErrorURL(String ErrorURL)
        {
            this.errorURL = ErrorURL;
        }

        //Response
        public void setPaymentID(String PaymentID)
        {
            this.paymentId = PaymentID;
        }
        public void setTrandata(String Trandata)
        {
            this.trandata = Trandata;
        }
        public void setResult(String Result)
        {
            this.result = Result;
        }
        public void setAuthRespCode(String AuthRespCode)
        {
            this.authRespCode = AuthRespCode;
        }
        public void setResponse(String Response)
        {
            this.response = Response;
        }
        public void setTransactionID(String TransactionID)
        {
            this.transId = TransactionID;
        }
        public void setReferenceID(String ReferenceID)
        {
            this.@ref = ReferenceID;
        }
        public void setTranDate(String TranDate)
        {
            this.date = TranDate;
        }
        public void setErrorCode(String ErrorCode)
        {
            this.errorCode = ErrorCode;
        }
        public void setErrorText(String ErrorText)
        {
            this.errorText = ErrorText;
        }

        public String getAmt()
        {
            return this.amt;
        }
        public String getResourceKey()
        {
            return this.resourceKey;
        }
        public String getAction()
        {
            return this.action;
        }
        public String getPassword()
        {
            return this.password;
        }
        public String getTranportalID()
        {
            return this.id;
        }
        public String getCurrencyCode()
        {
            return this.currencycode;
        }
        public String getTrackId()
        {
            return this.trackId;
        }
        public String getUdf1()
        {
            return this.udf1;
        }
        public String getUdf2()
        {
            return this.udf2;
        }
        public String getUdf3()
        {
            return this.udf3;
        }
        public String getUdf4()
        {
            return this.udf4;
        }
        public String getUdf5()
        {
            return this.udf5;
        }
        public String getExpYear()
        {
            return this.expYear;
        }
        public String getExpMonth()
        {
            return this.expMonth;
        }
        public String getMember()
        {
            return this.member;
        }
        public String getCardNo()
        {
            return this.cardNo;
        }
        public String getCardType()
        {
            return this.cardType;
        }
        public String getPaymentData()
        {
            return this.paymentData;
        }
        public String getPaymentMethod()
        {
            return this.paymentMethod;
        }
        public String getTransactionIdentifier()
        {
            return this.transactionIdentifier;
        }
        public String getResponseURL()
        {
            return this.responseURL;
        }
        public String getErrorURL()
        {
            return this.errorURL;
        }
        public String getPaymentID()
        {
            return this.paymentId;
        }
        public String getTrandata()
        {
            return this.trandata;
        }
        public String getResult()
        {
            return this.result;
        }
        public String getAuthRespCode()
        {
            return this.authRespCode;
        }
        public String getResponse()
        {
            return this.response;
        }
        public String getTransactionID()
        {
            return this.transId;
        }
        public String getReferenceID()
        {
            return this.@ref;
        }
        public String getAuthCode()
        {
            return this.authCode;
        }
        public String getTranDate()
        {
            return this.date;
        }
        public String getErrorCode()
        {
            return this.errorCode;
        }
        public String getErrorText()
        {
            return this.errorText;
        }


        private static AesManaged CreateAes(string resourceKey, string initVector)
        {

            var aes = new AesManaged();
            aes.Key = System.Text.Encoding.UTF8.GetBytes(resourceKey); //UTF8-Encoding
            aes.IV = System.Text.Encoding.UTF8.GetBytes(initVector);//UT8-Encoding
            return aes;
        }

        public static string encrypt(string text, string resourceKey, string initVector)
        {
            using (AesManaged aes = CreateAes(resourceKey, initVector))
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aes.CreateEncryptor();
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(text);
                        return String.Concat(Array.ConvertAll(ms.ToArray(), x => x.ToString("X2")));
                    }
                }
            }
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


        public String createRequestData()
        {             
            string finalData = "";
            FinalRequest request = new FinalRequest();
            request.id = this.id;
            List<iPayBenefitPipe> list = new List<iPayBenefitPipe>();
            list.Add(new iPayBenefitPipe
            {
                amt =this.amt,
                action = this.action,
                password = this.password,
                id = this.id,
                //resourceKey = this.resourceKey,
                trackId = this.trackId,
                udf1 = "UDF1",
                udf2 = "UDF2",
                udf3 = "UDF3",
                udf4 = "UDF4",
                udf5 = "UDF5",
                currencycode = this.currencycode,
                cardType = this.cardType,
                responseURL = this.responseURL,
                errorURL = this.errorURL,
                 
            });
            var content = JsonConvert.SerializeObject(list, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            resources res = new resources();

            request.trandata = encrypt(content, this.resourceKey, res.IV);
            List<FinalRequest> FinalRequestlist = new List<FinalRequest>();
            FinalRequestlist.Add(request);
            
            finalData = JsonConvert.SerializeObject(FinalRequestlist, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return (finalData);
        }

        public PaymentResponse PerformTransaction()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
            resources res = new resources();
            PaymentResponse results = new PaymentResponse();
            
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(res.EndPoint);
            webRequest.ContentType = "application/json";
            webRequest.Accept = "application/json";
            webRequest.Method = "POST";
            using (StreamWriter requestStream = new StreamWriter(webRequest.GetRequestStream()))
            {
                String paymentObject = createRequestData();
                requestStream.WriteLine(paymentObject);
            }
            
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            using (StreamReader responseStream = new StreamReader(webResponse.GetResponseStream()))
            {
                string resultsString = responseStream.ReadToEnd();

                try
                {
                    List<PaymentResponse> PG_Response = JsonConvert.DeserializeObject<List<PaymentResponse>>(resultsString);
                    results = PG_Response[0];
                }
                catch (Exception e)
                {
                    results.Exception = e.Message;
                }
            }
            return results;
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            .ToArray();
        }

        public int ParseResponse(string trandata)
        {
            try
            {
                resources res = new resources();
                trandata = decrypt(StringToByteArray(trandata), this.resourceKey, res.IV);

                if (!String.IsNullOrEmpty(trandata))
                {
                    JArray json = JArray.Parse(HttpUtility.UrlDecode(trandata));//,Encoding.UTF8));
                    //JArray json = JArray.Parse(Uri.EscapeDataString(trandata));//(trandata,Encoding.UTF8));



                    getDecryptedValues(((JObject)json.First).ToObject<iPayBenefitPipe>());

                    return 1;
                }
                else
                {

                    return 0;
                }

            }
            catch (Exception Ex)
            {
                this.errorText = Ex.Message.ToString();
                return 0;
            }

        }


        private void getDecryptedValues(iPayBenefitPipe obj)
        {
            this.paymentId = obj.getPaymentID();
            this.date = obj.getTranDate();
            this.result = obj.getResult();
            this.transId = obj.getTransactionID();
            this.@ref = obj.getReferenceID();
            this.authCode = obj.getAuthCode();
            this.authRespCode = obj.getAuthRespCode();
            this.udf1 = obj.getUdf1();
            this.udf2 = obj.getUdf2();
            this.udf3 = obj.getUdf3();
            this.udf4 = obj.getUdf4();
            this.udf5 = obj.getUdf5();
            this.trackId = obj.getTrackId();
        }

    }

    public class FinalRequest
    {
        public String id = "";
        public String trandata = "";
    }

    public class PaymentResponse
    {
        public string status = "";
        public string result = "";
        public string error = "";
        public string errorText = "";
        public string Exception = "";
    }



    public class resources
    {
        public string EndPoint = "https://test.benefit-gateway.bh/payment/API/hosted.htm";
        public string IV = "PGKEYENCDECIVSPC";
    }

     

}
